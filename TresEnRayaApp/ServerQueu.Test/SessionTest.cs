using NUnit.Framework;
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

            var session = new Session();
            Assert.IsNull(session.Client1);
            Assert.IsNull(session.Client2);
            Assert.IsFalse(session.CompleteClients());

            session.AddClient(new System.Net.Sockets.TcpClient());
            Assert.IsNotNull(session.Client1);
            Assert.IsNull(session.Client2);
            Assert.IsFalse(session.CompleteClients());

            session.AddClient(new System.Net.Sockets.TcpClient());
            Assert.IsNotNull(session.Client1);
            Assert.IsNotNull(session.Client2);
            Assert.IsTrue(session.CompleteClients());
        }
    }
}