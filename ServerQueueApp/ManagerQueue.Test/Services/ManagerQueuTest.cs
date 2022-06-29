using NUnit.Framework;
using ServerQueu.Handlers;
using ServerQueu.Sessions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TresEnRayaApp;

namespace ServerQueu.Test
{
    public class ManagerQueuTest
    {

        private string IP=default!;
        private int Port;

        [SetUp]
        public void SetUp()
        {
            IP="127.0.0.1";
            Port = 8000;
        }
        [Test]
        public void BasicFuntion()
        {
            var collectionSession = new ConcurrentQueue<Session<SessionInfo>>();
            collectionSession.Enqueue(new Session<SessionInfo>(2));
            HandlerSessionListener<SessionInfo> handler=new HandlerSessionListener<SessionInfo>(2,collectionSession);
            
            ListenerQueuServer<SessionInfo> listenerQueuServer = new ListenerQueuServer<SessionInfo>(IP,Port,2,handler);
            listenerQueuServer.RunThreads();

            ControllerSession<SessionInfo>.FactoryTaskServerQueu factoryTask= (session) => new TaskServerQueu<SessionInfo>(session);
            ControllerSession<SessionInfo>.TaskActionServerQueu taskActionServerQueu = (taskServer) => () => { };
            ControllerSession<SessionInfo> controllerSession = new ControllerSession<SessionInfo>(factoryTask,taskActionServerQueu);

            var handlerManagerQueu = new HandlerManagerQueu<SessionInfo>(collectionSession,controllerSession);
            var managerQueu = new ManagerQueu<SessionInfo>(handlerManagerQueu);
            Assert.AreEqual(1, collectionSession.Count);
            managerQueu.Run();
            Thread.Sleep(500);
            managerQueu.Finish = true;
            Assert.AreEqual(0, collectionSession.Count);
            if (managerQueu.Thread!=null)
            {
                Assert.AreEqual(ThreadState.Stopped, managerQueu.Thread.ThreadState);
            }
            else
            {
                Assert.Fail();
            }


        }
    }
}
