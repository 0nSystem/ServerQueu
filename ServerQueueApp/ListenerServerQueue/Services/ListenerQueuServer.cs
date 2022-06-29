using ServerQueu.Handlers;
using ServerQueu.Services;
using ServerQueu.Sessions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TresEnRayaApp
{
    public class ListenerQueuServer<T>: IListenerQueuServer<T> where T : SessionInfo,new()
    {
        private int Id;
        private readonly string Ip;
        private readonly int Port;
        private readonly int Backlog;
        private bool Finish;

        public TcpListener? TcpSocketServer { get; private set; } = null;
        
        public Thread? ThreadListener { get; private set; } = null;

        private IHandlerSessionListener<T>? HandlerSessionListener=null;

        

        public ListenerQueuServer(string ip,int port,int backlog, IHandlerSessionListener<T> handlerSessionListener)
        {
            this.Ip = ip;
            this.Port = port;
            this.Backlog = backlog;
            HandlerSessionListener=handlerSessionListener;
        }

        public void RunThreads()
        {
            EnabledTcpSocketServerToStart();
            
            ThreadListener= new Thread(() =>
            {
                if (TcpSocketServer != null&&HandlerSessionListener!=null)
                {
                    while (TcpSocketServer.Server.IsBound&& !Finish)
                    {
                        TcpClient tcpClient= TcpSocketServer.AcceptTcpClient();
                        var sessionInfo = new T
                        {
                            TcpClient = tcpClient,
                            Id = Id
                        };
                        HandlerSessionListener.AddClient(sessionInfo);
                    }
                    TcpSocketServer.Stop();
                }

            });

            ThreadListener.Start();
        }
        public void Close()
        {
            Finish=true;
        }

        private void EnabledTcpSocketServerToStart()
        {
            TcpSocketServer=new TcpListener(System.Net.IPAddress.Parse(Ip),Port);
            TcpSocketServer.Start(Backlog);
            Finish=false;
            Id=0;
        }

    }
}
