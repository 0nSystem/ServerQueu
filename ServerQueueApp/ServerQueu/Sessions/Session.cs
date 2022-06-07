using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using ServerQueu.Sessions;

namespace TresEnRayaApp
{
    public class Session<T> :ISession<T> where T:SessionInfo
    {
        public List<T> SessionsInfo { get; }
        public readonly int MaxClients;

        public Session(int MaxClients)
        {
            this.MaxClients = MaxClients;
            SessionsInfo = new List<T>();
        }

        public void AddClient(T sessionInfo)
        {
            SessionsInfo.Add(sessionInfo);
        }

        public bool CompleteClients()
        {
            if (SessionsInfo.Count<MaxClients)
                return false;

            return true;
        }
    }
}
