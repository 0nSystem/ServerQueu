using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TresEnRayaApp;

namespace ServerQueu
{
    public class PipeClients
    {
        private readonly Session Session;
        private readonly ConcurrentQueue<string> LectureToProcess;
        private readonly ConcurrentQueue<string> ResponseToClients;
        public PipeClients(ref Session session,ref ConcurrentQueue<string> LectureToProcess,ref ConcurrentQueue<string> ResponseToClients)
        {
            Session = session;
            this.LectureToProcess = LectureToProcess;
            this.ResponseToClients = ResponseToClients;
        }

        public void SendMessages()
        {
            if (ResponseToClients.TryDequeue(out var reponseToSend))
            {
                if (Session.Client1 != null)
                {
                    byte[] bufferToSend = Encoding.UTF8.GetBytes(reponseToSend);
                    var stream = Session.Client1.GetStream();
                    stream.Write(bufferToSend, 0, bufferToSend.Length);

                }
                if (Session.Client2 != null)
                {
                    byte[] bufferToSend = Encoding.UTF8.GetBytes(reponseToSend);
                    var stream = Session.Client2.GetStream();
                    stream.Write(bufferToSend, 0, bufferToSend.Length);

                }
            }
            

        }
        public void GetMessages()
        {
            if (Session.Client1 != null)
            {
                int sizeData = Session.Client1.Available;
                if (sizeData > 0)
                {
                    byte[] buffer=new byte[sizeData];
                    var stream = Session.Client1.GetStream();
                    stream.Read(buffer, 0, buffer.Length);

                    string mensaje=Encoding.UTF8.GetString(buffer,0,buffer.Length);
                    
                    LectureToProcess.Enqueue(mensaje);
                }
            }

            if (Session.Client2!=null)
            {
                int sizeData=Session.Client2.Available;
                if (sizeData>0)
                {
                    byte[] buffer=new byte[sizeData];
                    var stream = Session.Client2.GetStream();
                    stream.Read(buffer, 0, buffer.Length);

                    string mensaje = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                    
                    LectureToProcess.Enqueue(mensaje);
                }
            }
        }
        
    }
}
