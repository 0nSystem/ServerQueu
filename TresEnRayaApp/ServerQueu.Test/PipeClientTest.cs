using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TresEnRayaApp;

namespace ServerQueu.Test
{
    public class PipeClientTest
    {
        
        [Test]
        public void BasicFuntion()
        {
            Session session=new Session();
            var client1 = new TcpClient();
            var client2 = new TcpClient();

            var pipe = new PipeClients(ref session);
        }
    }
}
