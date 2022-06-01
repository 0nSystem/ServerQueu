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
        private readonly Session Session;
        public GameTask(Session session)
        {
            Session = session;

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
