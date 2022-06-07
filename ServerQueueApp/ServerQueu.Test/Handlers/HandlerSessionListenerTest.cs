using System;
using NUnit.Framework;
using ServerQueu.Sessions;
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
            var collectionSession = new System.Collections.Concurrent.ConcurrentQueue<Session<SessionInfo>>();
            var handlerSessions = new HandlerSessionListener<SessionInfo>(2,ref collectionSession);
            Assert.AreEqual(0, collectionSession.Count);

            var sessionOne = new SessionInfo(1, new System.Net.Sockets.TcpClient());
            handlerSessions.AddClient(sessionOne);
            var sessionTwo = new SessionInfo(2, new System.Net.Sockets.TcpClient());
            handlerSessions.AddClient(sessionTwo);

            Assert.AreEqual(1, collectionSession.Count);
            Assert.IsTrue(collectionSession.ToArray()[0].CompleteClients());

            handlerSessions.AddClient(new SessionInfo(3, new System.Net.Sockets.TcpClient()));
            handlerSessions.AddClient(new SessionInfo(4, new System.Net.Sockets.TcpClient()));


            Assert.AreEqual(2, collectionSession.Count);
            Assert.IsTrue(collectionSession.ToArray()[1].CompleteClients());
        }
    }
}
