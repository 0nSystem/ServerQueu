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
            if (Sessions.Count==0)
            {
               AddNewSession(tcpClient);
            }
            else if (Sessions[Sessions.Count-1].CompleteClients())
            {
                AddNewSession(tcpClient);
            }
            else
            {
                Sessions[Sessions.Count-1].AddClient(tcpClient);
            }

        }

        private void AddNewSession(TcpClient tcpClient)
        {
            Session session = new Session();
            session.AddClient(tcpClient);
            Sessions.Add(session);
        }
    }
}
