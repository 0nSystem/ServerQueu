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
    public class ManagerQueu
    {
        public readonly ConcurrentQueue<Session> Sessions;
        

        public ManagerQueu(ref ConcurrentQueue<Session> sessions)
        {
            Sessions = sessions;
        }

        public bool RunFirstElement()
        {
            //Pendiente de hacerlo como un servicio
            if (Sessions.TryDequeue(out var newSession))
            {
                GameTask gameTask = new GameTask(newSession);
                return gameTask.RunTask();
            }

            return false;
        }


        



    }
}
