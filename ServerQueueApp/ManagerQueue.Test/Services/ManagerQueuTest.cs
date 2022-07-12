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
    [TestFixture]
    public class ManagerQueuTest
    {

        private string IP= "127.0.0.1";
        private int Port = 5556;
        private ConcurrentQueue<Session<SessionInfo>> collectionSession = new ConcurrentQueue<Session<SessionInfo>>();

        private ControllerSession<SessionInfo> controllerSession;
        private HandlerManagerQueu<SessionInfo> handlerManagerQueu;
        private ManagerQueu<SessionInfo> managerQueue;

        [SetUp]
        public void SetUp()
        {
            
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

            int pause = 200;
            ManagerQueu<SessionInfo>.SECS_TO_RESUME = pause;

            int countWithOneSession = collectionSession.Count;
            
            managerQueue.Run();
            if (managerQueue.Thread==null)
            {
                Assert.Fail();
            }
            ThreadState stateRunning = managerQueue.Thread!.ThreadState;

            Thread.Sleep(pause*2);
            int countWithZeroSession=collectionSession.Count;

            managerQueue.Finish = false;
            Thread.Sleep(pause);

            ThreadState stateStopped = managerQueue.Thread!.ThreadState;

            Assert.Multiple(() =>
            {
                Assert.AreEqual(1, countWithOneSession);
                Assert.AreEqual(ThreadState.Running, stateRunning);
                Assert.AreEqual(0, countWithZeroSession);
                Assert.AreEqual(ThreadState.Stopped, stateStopped);
            });
        }
    }
}
