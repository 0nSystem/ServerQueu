using System;
using System.Threading;
using NUnit.Framework;
using TresEnRayaApp;

namespace ServerQueu.Test
{
    public class ListenerQueuServerTest
    {
        int Port = 9999;
        string Ip="127.0.0.1";

        [Test]
        public void Basic()
        {
            
            var listenerQueuServer = new ListenerQueuServer(Ip,Port,2);
            Assert.IsNull(listenerQueuServer.Thread);

            listenerQueuServer.RunThread();
            if (listenerQueuServer.Thread!=null)
            {
                Assert.AreEqual(ThreadState.Running, listenerQueuServer.Thread.ThreadState);
            }
            else
            {
                Assert.Fail();
            }

            Assert.Pass();
        }

        public void BasicFuntion()
        {
            var listenerQueuServer = new ListenerQueuServer(Ip, Port, 2);
            listenerQueuServer.RunThread();

        }
    }
}
