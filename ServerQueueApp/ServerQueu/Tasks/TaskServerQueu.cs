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

        public bool RunTask()
        {
            
            if (!AuthenticationBeforeTask())
            {
               return false;
            }

            return ThreadPool.QueueUserWorkItem(CreateSessionTask);
        }
        public bool AuthenticationBeforeTask()
        {
            if (Session==null)
            {
                return false;
            }
            if (Session!=null&&!Session.CompleteClients())
            {
                return false;
            }
            if (Pipe==null)
            {
                return false;
            }

            return true;
        }
        protected void CreateSessionTask(Object? stateInfo)
        {
            
        }
    }
}
