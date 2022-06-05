using System;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using NUnit.Framework;
using ServerQueu.Sessions;
using TresEnRayaApp;

namespace ServerQueu.Test
{
    public class ListenerQueuServerTest
    {
        int Port = 55555;
        string Ip="127.0.0.1";

        [Test]
        public void Basic()
        {
            var collection = new System.Collections.Concurrent.ConcurrentQueue<Session<SessionInfo>>();
            var handler=new HandlerSessionListener<SessionInfo>(2,ref collection);
            var listenerQueuServer = new ListenerQueuServer<SessionInfo>(Ip,Port,2,handler);
            
            Assert.IsNull(listenerQueuServer.ThreadListener);

            listenerQueuServer.RunThreads();
            if (listenerQueuServer.ThreadListener!=null)
            {
                var stateThread = listenerQueuServer.ThreadListener.ThreadState;
                listenerQueuServer.Close();
                Assert.AreEqual(ThreadState.Running,stateThread);
                
            }
            else
            {
                Assert.Fail();
            }
        }
        
    }
}
