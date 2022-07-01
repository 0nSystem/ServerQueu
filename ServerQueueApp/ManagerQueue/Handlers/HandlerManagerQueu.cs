using ManagerQueue.Controller;
using ServerQueu.Sessions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TresEnRayaApp;

namespace ManagerQueue.Handlers
{
    public class HandlerManagerQueu<T> :IHandlerManagerQueu<T> where T:SessionInfo
    {
        private ConcurrentQueue<Session<T>> Sessions;
        private ControllerSession<T> ControllerSession;

        protected readonly TaskFactory TaskFactory;
        protected List<Task> TaskListProccesOrRunning
        {
            get
            {
                lock (TaskListProccesOrRunning)
                {
                    return TaskListProccesOrRunning;
                }
            }
            set
            {
                lock (TaskListProccesOrRunning)
                {
                    TaskListProccesOrRunning = value;
                }
            }
        }
        public HandlerManagerQueu(ConcurrentQueue<Session<T>> sessions,ControllerSession<T> controllerSession,TaskFactory taskFactory)
        {
            Sessions = sessions;
            ControllerSession = controllerSession;
            TaskFactory = taskFactory;
        }
        public HandlerManagerQueu(ConcurrentQueue<Session<T>> sessions, ControllerSession<T> controllerSession)
        {
            Sessions = sessions;
            ControllerSession = controllerSession;
            TaskFactory = new TaskFactory();
        }

        public bool RunElement()
        {
            if (Sessions.TryDequeue(out var newSession))
            {
                Action? actionTaskSession=ControllerSession.MakeTaskSession(newSession);
                if (actionTaskSession==null)
                {
                    return false;
                }
                Task task=TaskFactory.StartNew(actionTaskSession);
                return AddListAndRunTask(task);
            }

            return false;
        }
        public bool CanRunElement()
        {
            return Sessions.Count > 0;
        }
        protected bool AddListAndRunTask(Task task)
        {
            int listProccesOrRunned = TaskListProccesOrRunning.Count;
            TaskListProccesOrRunning.Add(task);
            task.Start();
            
            if (listProccesOrRunned>=TaskListProccesOrRunning.Count)
            {
                return false;
            }

            if (task.Status!=TaskStatus.Running
                ||task.Status!=TaskStatus.RanToCompletion)
            {
                return false;
            }

            return true;
        }
    }
}
