using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerQueu.Sessions
{
    public class SessionInfo
    {
        private int id=0;
        private TcpClient? tcpClient=null;

        public int Id
        {
            get => id;
            set
            {
                if(id==0)
                    id = value;
            }
        }
        public TcpClient? TcpClient
        {
            set
            {
                if (tcpClient == null)
                {
                    tcpClient = value;
                }
            }
            get => tcpClient;
        }
        public SessionInfo(int Id,TcpClient tcpClient)
        {
            this.id = Id;
            this.tcpClient = tcpClient;
        }
        public SessionInfo()
        {

        }

    }
}
