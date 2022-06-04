using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace TresEnRayaApp
{
    public class Session
    {
        public TcpClient? Client1 { get; set; }
        public TcpClient? Client2 { get; set; }

        public Session()
        {

        }

        public void AddClient(TcpClient tcpClient)
        {
            if (Client1==null)
            {
                Client1 = tcpClient;
                return;
            }
            if (Client2==null)
            {
                Client2 = tcpClient;
                return;
            }
        }
        public bool CompleteClients()
        {
            if (Client1!=null&&Client2!=null)
            {
                return true;
            }

            return false;
        }
    }
}
