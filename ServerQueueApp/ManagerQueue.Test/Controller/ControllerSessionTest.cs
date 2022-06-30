using ManagerQueue.Controller;
using ManagerQueue.Tasks;
using NUnit.Framework;
using ServerQueu.Sessions;
using System;
using System.Threading.Tasks;
using TresEnRayaApp;

namespace ManagerQueue.Test.Controller
{
    class ControllerSessionTest
    {
        [Test]
        public void basicFuntion()
        {
            string mensaje = "hola";
            string bufferMensaje = "";
            ControllerSession<SessionInfo> controllerSession= new ControllerSession<SessionInfo>((session)=>new TaskServerQueu<SessionInfo>(session), (taskServerQueue) => () => { bufferMensaje = mensaje; });
            Session<SessionInfo> session = new Session<SessionInfo>(0);
            
            Action? action = controllerSession.MakeTaskSession(session);
            if (action != null)
            {
                Task task = Task.Run(action);
                Task.WaitAll(task);
                Assert.AreEqual(mensaje,bufferMensaje);
            }
            else
                Assert.Fail();
            
        }
    }
}
