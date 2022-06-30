using ManagerQueue.Controller;
using ManagerQueue.Handlers;
using ManagerQueue.Services;
using ManagerQueue.Tasks;
using NUnit.Framework;
using ServerQueu;
using ServerQueu.Sessions;
using System.Collections.Concurrent;
using System.Threading;
using TresEnRayaApp;

namespace ManagerQueue.Test
{
    public class ManagerQueuTest
    {

        private string IP=default!;
        private int Port;
        private ConcurrentQueue<Session<SessionInfo>> collectionSession = new ConcurrentQueue<Session<SessionInfo>>();
        
        private HandlerSessionListener<SessionInfo> handlerListener;
        private ListenerQueuServer<SessionInfo> listenerQueuServer;

        private ControllerSession<SessionInfo> controllerSession;
        private HandlerManagerQueu<SessionInfo> handlerManagerQueu;
        private ManagerQueu<SessionInfo> managerQueue;

        [SetUp]
        public void SetUp()
        {
            IP="127.0.0.1";
            Port = 8000;
        
            handlerListener= new HandlerSessionListener<SessionInfo>(2, collectionSession);
            listenerQueuServer = new ListenerQueuServer<SessionInfo>(IP, Port,2, handlerListener);

            ControllerSession<SessionInfo>.FactoryTaskServerQueu factoryTask = (session) => new TaskServerQueu<SessionInfo>(session);
            ControllerSession<SessionInfo>.TaskActionServerQueu taskActionServerQueu = (taskServer) => () => { };
            controllerSession = new ControllerSession<SessionInfo>(factoryTask, taskActionServerQueu);
        
            handlerManagerQueu= new HandlerManagerQueu<SessionInfo>(collectionSession, controllerSession);
            managerQueue=new ManagerQueu<SessionInfo>(handlerManagerQueu);
        }
        [Test]
        public void BasicFuntion()
        {
            
            collectionSession.Enqueue(new Session<SessionInfo>(2));
            listenerQueuServer.RunThreads();


            int pause = 200;
            ManagerQueu<SessionInfo>.SECS_TO_RESUME = pause;

            Assert.AreEqual(1, collectionSession.Count);
            
            managerQueue.Run();
            if (managerQueue.Thread==null)
            {
                Assert.Fail();
                return;
            }
            Assert.AreEqual(ThreadState.Running, managerQueue.Thread.ThreadState);

            Thread.Sleep(pause*2);
            Assert.AreEqual(0,collectionSession.Count);

            managerQueue.Finish = false;
            Thread.Sleep(pause);
            Assert.AreEqual(ThreadState.Stopped, managerQueue.Thread.ThreadState);

        }
    }
}
