using ServerQueu.Handlers;
using ServerQueu.Services;
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
    public class ManagerQueu<T>:IManagerQueu<T> where T : SessionInfo
    {
        public static int SECS_TO_PAUSE = 0;
        public readonly IHandlerManagerQueu<T> HandlerManagerQueu;
        public Thread? Thread { get; private set; } = null;
        public bool Finish { get; set; } = true;

        public ManagerQueu(IHandlerManagerQueu<T> handlerManagerQueu)
        {
            this.HandlerManagerQueu = handlerManagerQueu;
        }

        public void Run()
        {
            Finish = false;
            Thread = new Thread(() =>
            {
                while (!Finish)
                {
                    if (!this.HandlerManagerQueu.RunElement())
                    {
                        //Execution fail
                        Thread.Sleep(SECS_TO_PAUSE);
                    };
                }
            });
            Thread.Start();
        }


        
    }
}
