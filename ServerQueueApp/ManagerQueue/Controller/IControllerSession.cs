using ServerQueu.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TresEnRayaApp;

namespace ManagerQueue.Controller
{
    public interface IControllerSession<T> where T : SessionInfo
    {
        public Action? MakeTaskSession(Session<T> session);
    }
}
