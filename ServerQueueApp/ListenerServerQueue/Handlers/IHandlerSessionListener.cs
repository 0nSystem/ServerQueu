using ServerQueu.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerQueu.Handlers
{
    public interface IHandlerSessionListener<T> where T : SessionInfo
    {
        void AddClient(T sessionInfo);


    }
}
