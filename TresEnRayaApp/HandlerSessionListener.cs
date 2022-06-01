using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TresEnRayaApp
{
    public class HandlerSessionListener
    {
        public List<Session> Sessions = new List<Session>();

        public HandlerSessionListener()
        {

        }
        public void AddClient(TcpClient tcpClient)
        {
            if (Sessions.Count==0|Sessions[Sessions.Count].CompleteClients())
            {
                Session session = new Session();
                session.AddClient(tcpClient);
                Sessions.Add(session);
            }
            else
            {
                Sessions[Sessions.Count].AddClient(tcpClient);
            }

        }

        
    }
}
