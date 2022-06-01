using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TresEnRayaApp;

namespace ServerQueu
{
    public class GameTask
    {

        public readonly PipeClients Pipe;
        public GameTask(ref Session session)
        {
            Pipe= new PipeClients(ref session);
            
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
