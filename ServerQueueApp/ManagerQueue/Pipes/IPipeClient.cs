using ServerQueu.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerQueue.Pipes
{
    public interface IPipeClient<T> where T : SessionInfo
    {
        void SendMessages();
        void GetMessages();
    }
}
