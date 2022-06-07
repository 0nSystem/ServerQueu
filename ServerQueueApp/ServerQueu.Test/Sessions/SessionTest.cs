using NUnit.Framework;
using ServerQueu.Sessions;
using TresEnRayaApp;

namespace ServerQueu.Test
{
    public class SessionTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void BasicFuncionSession()
        {
            int maxSession = 2;

            var session = new Session<SessionInfo>(maxSession);
            Assert.AreEqual(0,session.SessionsInfo.Count);
            Assert.IsFalse(session.CompleteClients());

            var sessionInfo1 = new SessionInfo(1, new System.Net.Sockets.TcpClient());
            session.AddClient(sessionInfo1);
            Assert.AreEqual(1,session.SessionsInfo.Count);
            Assert.IsFalse(session.CompleteClients());

            var sessionInfo2 = new SessionInfo(2, new System.Net.Sockets.TcpClient());
            session.AddClient(sessionInfo2);
            Assert.AreEqual(2, session.SessionsInfo.Count);
            Assert.IsTrue(session.CompleteClients());
        }
    }
}