using NUnit.Framework;
using ServerQueu.Sessions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TresEnRayaApp;

namespace ServerQueu.Test.Pipe
{
    public class PipeClients
    {

        private int Port = 45621;
        private string Ip="127.0.0.1";
        [Test]
        public void BasicFuntionWithPipe()
        {
            var collection = new System.Collections.Concurrent.ConcurrentQueue<Session<SessionInfo>>();
            var handler = new HandlerSessionListener<SessionInfo>(2, collection);

            var listenerQueuServer = new ListenerQueuServer<SessionInfo>(Ip, Port, 3, handler);
            listenerQueuServer.RunThreads();

            var client1 = new TcpClient(Ip, Port);
            var client2 = new TcpClient(Ip, Port);

            Thread.Sleep(500);

            Assert.AreEqual(1, collection.Count);

            if (collection.TryDequeue(out var session))
            {
                var collectionToRead = new ConcurrentQueue<string>();
                var collectionToWrite = new ConcurrentQueue<string>();
                var PipeClien = new PipeClients<SessionInfo>(session, collectionToRead, collectionToWrite);

                string mensaje = "HolaMundo";
                collectionToWrite.Enqueue(mensaje);
                PipeClien.SendMessages();

                var streamClien1 = client1.GetStream();
                var streamClien2 = client2.GetStream();

                byte[] bufferClien1 = new byte[mensaje.Length];
                byte[] bufferClien2 = new byte[mensaje.Length];

                streamClien1.Read(bufferClien1, 0, bufferClien1.Length);
                streamClien2.Read(bufferClien2, 0, bufferClien2.Length);

                streamClien1.Write(bufferClien1, 0, bufferClien1.Length);
                streamClien2.Write(bufferClien2, 0, bufferClien2.Length);
                PipeClien.GetMessages();

                Assert.AreEqual(2, collectionToRead.Count);
                collectionToRead.TryDequeue(out var mensaje1);
                collectionToRead.TryDequeue(out var mensaje2);
                listenerQueuServer.Close();

                Assert.Multiple(() =>
                {
                    Assert.AreEqual(mensaje, Encoding.UTF8.GetString(bufferClien1));
                    Assert.AreEqual(mensaje, Encoding.UTF8.GetString(bufferClien2));
                    Assert.AreEqual(mensaje, mensaje1);
                    Assert.AreEqual(mensaje, mensaje2);
                });

            }
            else
            {
                Assert.Fail();
            }

        }
    }
}
