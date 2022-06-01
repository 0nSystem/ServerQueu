using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TresEnRayaApp;

namespace ServerQueu
{
    public class PipeClients
    {
        private readonly Session Session;

        public PipeClients(ref Session session)
        {
            Session = session;
        }

        public void SendMessage()
        {

        }
        
    }
}
