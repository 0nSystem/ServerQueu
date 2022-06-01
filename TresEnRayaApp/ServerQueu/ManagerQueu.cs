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

        public void RunQueu()
        {
            IEnumerator<Session> sessionEnum= Sessions.GetEnumerator();
            
            while (sessionEnum.MoveNext())
            {
                Session session = sessionEnum.Current;
                GameTask gameTask = new GameTask(session);
                gameTask.RunTask();
            }
            
        }


        



    }
}
