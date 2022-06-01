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

        private TcpListener? _tcpSocketServer = null;
        public TcpListener? TcpSocketServer { get { return _tcpSocketServer; } }

        private Thread? _thread = null;
        public Thread? Thread
        {
            get { return _thread; }
        }

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
            
            _thread= new Thread(() =>
            {
                if (_tcpSocketServer != null&&HandlerSessionListener!=null)
                {
                    while (_tcpSocketServer.Server.IsBound)
                    {
                        TcpClient tcpClient= _tcpSocketServer.AcceptTcpClient();
                        HandlerSessionListener.AddClient(tcpClient);
                    }
                }

            });

            _thread.Start();
        }


        private void EnabledTcpSocketServer()
        {
            _tcpSocketServer=new TcpListener(System.Net.IPAddress.Parse(Ip),Port);
            _tcpSocketServer.Start(Backlog);
            HandlerSessionListener = new HandlerSessionListener();
        }

    }
}
