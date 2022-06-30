using ServerQueu.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerQueue.Handlers
{
    public interface IHandlerManagerQueu<T> where T : SessionInfo
    {
        bool RunElement();
    }
}
