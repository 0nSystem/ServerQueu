using NUnit.Framework;
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
        [Test]
        public void BasicFuntion()
        {
            var collectionSession = new ConcurrentQueue<Session>();
            
            var client1 = new TcpClient();
            var client2 = new TcpClient();
            var session=new Session();
            session.AddClient(client1);
            session.AddClient(client2);
            collectionSession.Enqueue(session);

            var managerQueu = new ManagerQueu(ref collectionSession);
            Assert.IsTrue(managerQueu.Sessions.Count == 1);
            Assert.IsTrue(managerQueu.RunQueu());
            Assert.IsTrue(managerQueu.Sessions.Count == 0);


        }
    }
}
