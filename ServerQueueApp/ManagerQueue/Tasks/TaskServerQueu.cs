using ServerQueu.Services;
using ServerQueu.Sessions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TresEnRayaApp;

namespace ServerQueu
{
    public class TaskServerQueu<T>:ITaskServerQueu<T> where T : SessionInfo
    {

        protected Session<T>? Session;
        protected PipeClients<T>? Pipe;
        

        protected readonly ConcurrentQueue<string> ToRead=new ConcurrentQueue<string>();
        protected readonly ConcurrentQueue<string> ToWrite=new ConcurrentQueue<string>();
        
        //Tres en raya para controllar
        public TaskServerQueu(Session<T> session)
        {
            Session = session;
            Pipe = new PipeClients<T>(Session, ToRead, ToWrite);
        }
        protected Action<TaskServerQueu<T>> MakeAction()
        {
            return (TaskServerQueu) => { };
            
        }
        public Action<TaskServerQueu<T>>? GenerateActionTask()
        {
            
            if (!AuthenticationBeforeGenerateTask(this))
            {
                return null;
            }
            Action<TaskServerQueu<T>> task= MakeAction();
            

            if (!AuthenticationAfterGenerateTask(this))
            {
                return null;
            }

            return task;
        }
        public bool AuthenticationBeforeGenerateTask(TaskServerQueu<T> taskServerQueu)
        {
            if (taskServerQueu.Session==null)
            {
                return false;
            }
            if (taskServerQueu.Session !=null&&!taskServerQueu.Session.CompleteClients())
            {
                return false;
            }
            if (taskServerQueu.Pipe ==null)
            {
                return false;
            }

            return true;
        }
        public bool AuthenticationAfterGenerateTask(TaskServerQueu<T> taskServerQueu)
        {
            return true;
        }
    }
}
