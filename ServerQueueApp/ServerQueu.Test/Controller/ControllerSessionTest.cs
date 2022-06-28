using NUnit.Framework;
using ServerQueu.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TresEnRayaApp;

namespace ServerQueu.Test.Controller
{
    class ControllerSessionTest
    {
        [Test]
        public void basicFuntion()
        {
            ControllerSession<SessionInfo> controllerSession= new ControllerSession<SessionInfo>((session)=>new TaskServerQueu<SessionInfo>(session));
            Session<SessionInfo> session = new Session<SessionInfo>(0);
            Assert.IsTrue(controllerSession.ExecuteSession(session));
        }
    }
}
