using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TresEnRayaApp
{
    public class ListenerQueuServer 
    {
        private readonly string Ip;
        private readonly int Port;
        private readonly int Backlog;

        private TcpListener? TcpSocketServer=null;
        private Thread? Thread=null;
        private HandlerSessionListener? HandlerSessionListener=null;
        public ListenerQueuServer(string ip,int port,int backlog)
        {
            this.Ip = ip;
            this.Port = port;
            this.Backlog = backlog;
        }

        public void RunThread()
        {
            EnabledTcpSocketServer();
            
            Thread= new Thread(() =>
            {
                if (TcpSocketServer != null&&HandlerSessionListener!=null)
                {
                    while (TcpSocketServer.Server.IsBound)
                    {
                        TcpClient tcpClient= TcpSocketServer.AcceptTcpClient();
                        HandlerSessionListener.AddClient(tcpClient);
                    }
                }

            });

            Thread.Start();
        }


        private void EnabledTcpSocketServer()
        {
            TcpSocketServer=new TcpListener(System.Net.IPAddress.Parse(Ip),Port);
            TcpSocketServer.Start(Backlog);
            HandlerSessionListener = new HandlerSessionListener();
        }

    }
}
