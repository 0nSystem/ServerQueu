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
            Assert.AreEqual(0, collectionSession.Count);

            handlerSessions.AddClient(new System.Net.Sockets.TcpClient());
            handlerSessions.AddClient(new System.Net.Sockets.TcpClient());

            Assert.AreEqual(1, collectionSession.Count);
            Assert.IsTrue(collectionSession.ToArray()[0].CompleteClients());

            handlerSessions.AddClient(new System.Net.Sockets.TcpClient());
            handlerSessions.AddClient(new System.Net.Sockets.TcpClient());

            Assert.AreEqual(2, collectionSession.Count);
            Assert.IsTrue(collectionSession.ToArray()[1].CompleteClients());
        }
    }
}
