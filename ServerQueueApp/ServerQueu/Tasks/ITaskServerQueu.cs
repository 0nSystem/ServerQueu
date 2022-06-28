using ServerQueu.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TresEnRayaApp;

namespace ServerQueu.Services
{
    public interface ITaskServerQueu<T> where T : SessionInfo
    {
        bool RunTask();
        bool AuthenticationBeforeTask();
    }
}
