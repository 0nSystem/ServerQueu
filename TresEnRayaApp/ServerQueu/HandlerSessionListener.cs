
using System.Collections.Concurrent;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TresEnRayaApp
{
    public class HandlerSessionListener
    {
        public readonly ConcurrentQueue<Session> Sessions;

        private Session? BufferSession=null;

        public HandlerSessionListener(ref ConcurrentQueue<Session> sessions)
        {
            Sessions = sessions;
        }
        public void AddClient(TcpClient tcpClient)
        {
            if (BufferSession==null)
            {
                BufferSession = new Session();
                AddNewSession(tcpClient);
            }
            else if (!BufferSession.CompleteClients())
            {
                AddNewSession(tcpClient);
            }

            if (BufferSession.CompleteClients())
            {
                TransferSessions();
            }

        }

        private void AddNewSession(TcpClient tcpClient)
        {
            if (BufferSession!=null)
            {
                BufferSession.AddClient(tcpClient);
            }
        }
        
        private void TransferSessions()
        {
            if (BufferSession!=null)
            {
                Sessions.Enqueue(BufferSession);
                BufferSession = null;
            }

        }
    }
}
