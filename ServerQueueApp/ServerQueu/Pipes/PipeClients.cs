using ServerQueu.Pipes;
using ServerQueu.Sessions;
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
    public class PipeClients<T>: IPipeClient<T> where T : SessionInfo
    {
        private readonly Session<T> Session;
        private readonly ConcurrentQueue<string> LectureToProcess;
        private readonly ConcurrentQueue<string> ResponseToClients;
        public PipeClients(ref Session<T> session,ref ConcurrentQueue<string> LectureToProcess,ref ConcurrentQueue<string> ResponseToClients)
        {
            Session = session;
            this.LectureToProcess = LectureToProcess;
            this.ResponseToClients = ResponseToClients;
        }

        public void SendMessages()
        {
            if (ResponseToClients.TryDequeue(out var reponseToSend))
            {
                byte[] bufferToSend = Encoding.UTF8.GetBytes(reponseToSend);
                
                foreach (var session in Session.SessionsInfo)
                {
                    if (session.TcpClient!=null)
                    {
                        var stream = session.TcpClient.GetStream();
                        stream.Write(bufferToSend, 0, bufferToSend.Length);
                    }
                }
            }
            

        }
        public void GetMessages()
        {

            foreach(var session in Session.SessionsInfo)
            {

                if (session.TcpClient!=null)
                {
                    int sizeData = session.TcpClient.Available;
                    if (sizeData > 0)
                    {
                        var stream = session.TcpClient.GetStream();
                        byte[] buffer = new byte[sizeData];
                        stream.Read(buffer, 0, buffer.Length);

                        string mensaje = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                        LectureToProcess.Enqueue(mensaje);
                    }
                    
                }
            }
        }
        
    }
}
