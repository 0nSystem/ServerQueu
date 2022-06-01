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
            var handlerSessions = new HandlerSessionListener();
            Assert.AreEqual(0, handlerSessions.Sessions.Count);

            handlerSessions.AddClient(new System.Net.Sockets.TcpClient());
            Assert.AreEqual(1, handlerSessions.Sessions.Count);
            int countNowSessionsWithIndex = handlerSessions.Sessions.Count-1;
            Assert.IsFalse(handlerSessions.Sessions[countNowSessionsWithIndex].CompleteClients());

            handlerSessions.AddClient(new System.Net.Sockets.TcpClient());
            Assert.AreEqual(1, handlerSessions.Sessions.Count);
            Assert.IsTrue(handlerSessions.Sessions[countNowSessionsWithIndex].CompleteClients());
        }
    }
}
