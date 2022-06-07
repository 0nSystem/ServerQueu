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
        public readonly Session<T> Session;
        public readonly PipeClients<T> Pipe;
        public readonly ConcurrentQueue<string> ToRead=new ConcurrentQueue<string>();
        public readonly ConcurrentQueue<string> ToWrite=new ConcurrentQueue<string>();
        
        //Tres en raya para controllar
        public TaskServerQueu(ref Session<T> session)
        {   
            Session = session;
            Pipe= new PipeClients<T>(ref Session,ref ToRead,ref ToWrite);
            
        }

        public bool RunTask()
        {
            return ThreadPool.QueueUserWorkItem(CreateSessionTask);
        }

        private static void CreateSessionTask(Object? stateInfo)
        {

        }

        
    }
}
