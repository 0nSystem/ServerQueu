using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TresEnRayaApp;

namespace ServerQueu
{
    public class GameTask
    {
        public readonly Session Session;
        public readonly PipeClients Pipe;
        public readonly ConcurrentQueue<string> ToRead=new ConcurrentQueue<string>();
        public readonly ConcurrentQueue<string> ToWrite=new ConcurrentQueue<string>();
        
        //Tres en raya para controllar
        public GameTask(ref Session session)
        {   
            Session = session;
            Pipe= new PipeClients(ref Session,ref ToRead,ref ToWrite);
            
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
