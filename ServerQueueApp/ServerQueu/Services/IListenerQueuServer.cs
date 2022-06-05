using ServerQueu.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerQueu.Services
{
    public interface IListenerQueuServer<T> where T : SessionInfo
    {
        void RunThreads();
        void Close();

    }
}
