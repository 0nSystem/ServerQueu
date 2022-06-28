using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TresEnRayaApp;

namespace ServerQueu.Sessions
{
    public class ControllerSession<T>:IControllerSession<T> where T : SessionInfo
    {
        public delegate TaskServerQueu<T> FactoryTaskServerQueu(Session<T> session);

        protected readonly FactoryTaskServerQueu FactoryTask;
        public ControllerSession(FactoryTaskServerQueu factoryTask)
        {
            FactoryTask=factoryTask;
        }

        public bool ExecuteSession(Session<T> session)
        {
            if (FactoryTask==null)
            {
                return false;
            }

            return FactoryTask.Invoke(session).RunTask();
        }
    }
}
