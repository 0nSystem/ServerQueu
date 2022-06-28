
using ServerQueu.Handlers;
using ServerQueu.Sessions;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TresEnRayaApp
{
    public class HandlerSessionListener<T>: IHandlerSessionListener<T> where T:SessionInfo
    {
        private readonly ConcurrentQueue<Session<T>> Sessions;

        private Session<T>? BufferSession=null;

        private readonly int MaxClients;

        public HandlerSessionListener(int MaxClients,ConcurrentQueue<Session<T>> sessions)
        {
            this.MaxClients=MaxClients;
            Sessions = sessions;
        }

        public void AddClient(T sessionInfo)
        {
            if (BufferSession==null)
            {
                BufferSession = new Session<T>(MaxClients);
                AddNewSession(sessionInfo);
            }
            else if (!BufferSession.CompleteClients())
            {
                AddNewSession(sessionInfo);
            }

            if (BufferSession.CompleteClients())
            {
                TransferSessions();
            }

        }

        private void AddNewSession(T session)
        {
            if (BufferSession!=null)
            {
                BufferSession.AddClient(session);
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
