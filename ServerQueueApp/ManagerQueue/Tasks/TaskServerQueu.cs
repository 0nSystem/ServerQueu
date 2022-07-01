using ManagerQueue.Pipes;
using ServerQueu.Services;
using ServerQueu.Sessions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TresEnRayaApp;

namespace ManagerQueue.Tasks
{
    public class TaskServerQueu<T>:ITaskServerQueu<T> where T : SessionInfo
    {

        protected Session<T>? Session;
        protected PipeClients<T>? Pipe;
        

        protected readonly ConcurrentQueue<string> ToRead=new ConcurrentQueue<string>();
        protected readonly ConcurrentQueue<string> ToWrite=new ConcurrentQueue<string>();
        
        public TaskServerQueu(Session<T> session)
        {
            Session = session;
            Pipe = new PipeClients<T>(Session, ToRead, ToWrite);
        }
    }
}
