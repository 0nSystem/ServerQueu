using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerQueu.Sessions
{
    public interface ISession<T> where T : SessionInfo
    {
        void AddClient(T sessionInfo);
        bool CompleteClients();
    }
}
