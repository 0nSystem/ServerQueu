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
            var collectionSession = new ConcurrentQueue<Session>();
            collectionSession.Enqueue(new Session());
            HandlerSessionListener handler=new HandlerSessionListener(ref collectionSession);
            
            ListenerQueuServer listenerQueuServer = new ListenerQueuServer(IP,Port,2,handler);
            listenerQueuServer.RunThreads();


            var managerQueu = new ManagerQueu(ref collectionSession);
            Assert.IsTrue(managerQueu.Sessions.Count == 1);
            Assert.IsTrue(managerQueu.RunFirstElement());
            Assert.IsTrue(managerQueu.Sessions.Count == 0);


        }
    }
}
