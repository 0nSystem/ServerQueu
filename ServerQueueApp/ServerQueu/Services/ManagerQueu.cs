using ServerQueu.Sessions;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TresEnRayaApp;

namespace ServerQueu
{
    public class ManagerQueu<T> where T : SessionInfo
    {
        public readonly ConcurrentQueue<Session<T>> Sessions;
        

        public ManagerQueu(ref ConcurrentQueue<Session<T>> sessions)
        {
            Sessions = sessions;
        }

        public bool RunFirstElement()
        {
            //Pendiente de hacerlo como un servicio
            if (Sessions.TryDequeue(out var newSession))
            {
                TaskServerQueu<T> gameTask = new TaskServerQueu<T>(ref newSession);
                return gameTask.RunTask();
            }

            return false;
        }
    }
}
