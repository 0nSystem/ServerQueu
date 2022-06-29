using ServerQueu.Sessions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TresEnRayaApp;

namespace ServerQueu.Handlers
{
    public class HandlerManagerQueu<T> :IHandlerManagerQueu<T> where T:SessionInfo
    {
        private ConcurrentQueue<Session<T>> Sessions;
        private IControllerSession<T> ControllerSession;
        public HandlerManagerQueu(ConcurrentQueue<Session<T>> sessions,IControllerSession<T> controllerSession)
        {
            Sessions = sessions;
            ControllerSession = controllerSession;
        }

        public bool RunElement()
        {
            if (Sessions.TryDequeue(out var newSession))
            {
                return ControllerSession.ExecuteSession(newSession);
            }

            return false;
        }
    }
}
