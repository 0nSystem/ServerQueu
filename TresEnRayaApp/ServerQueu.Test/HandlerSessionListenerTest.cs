using System;
using NUnit.Framework;
using TresEnRayaApp;

namespace ServerQueu.Test
{
    
    public class HandlerSessionListenerTest
    {
        [Test]
        public void BasicFuntion()
        {
            Assert.Pass();

        }

        [Test]
        public void AddClientesInHandlerSession()
        {
            var collectionSession = new System.Collections.Concurrent.ConcurrentQueue<Session>();
            var handlerSessions = new HandlerSessionListener(ref collectionSession);
            Assert.AreEqual(0, handlerSessions.Sessions.Count);

            handlerSessions.AddClient(new System.Net.Sockets.TcpClient());
            handlerSessions.AddClient(new System.Net.Sockets.TcpClient());

            Assert.AreEqual(1, handlerSessions.Sessions.Count);
            Assert.IsTrue(handlerSessions.Sessions.ToArray()[0].CompleteClients());

            handlerSessions.AddClient(new System.Net.Sockets.TcpClient());
            handlerSessions.AddClient(new System.Net.Sockets.TcpClient());

            Assert.AreEqual(2, handlerSessions.Sessions.Count);
            Assert.IsTrue(handlerSessions.Sessions.ToArray()[1].CompleteClients());
        }
    }
}
