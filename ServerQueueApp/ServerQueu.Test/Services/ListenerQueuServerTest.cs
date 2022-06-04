using System;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using NUnit.Framework;
using TresEnRayaApp;

namespace ServerQueu.Test
{
    public class ListenerQueuServerTest
    {
        int Port = 55555;
        string Ip="127.0.0.1";

        [Test]
        public void Basic()
        {
            var collection = new System.Collections.Concurrent.ConcurrentQueue<Session>();
            var handler=new HandlerSessionListener(ref collection);
            var listenerQueuServer = new ListenerQueuServer(Ip,Port,2,handler);
            
            Assert.IsNull(listenerQueuServer.ThreadListener);

            listenerQueuServer.RunThreads();
            if (listenerQueuServer.ThreadListener!=null)
            {
                Assert.AreEqual(ThreadState.Running, listenerQueuServer.ThreadListener.ThreadState);
                listenerQueuServer.Close();
            }
            else
            {
                Assert.Fail();
            }
        }
        [Test]
        public void BasicFuntionWithPipe()
        {
            var collection= new System.Collections.Concurrent.ConcurrentQueue<Session>();
            var handler=new HandlerSessionListener(ref collection);

            var listenerQueuServer = new ListenerQueuServer(Ip, Port, 3,handler);
            listenerQueuServer.RunThreads();

            var client1 = new TcpClient(Ip, Port);
            var client2 = new TcpClient(Ip, Port);

            Thread.Sleep(1000);

            Assert.AreEqual(1,collection.Count);

            if (collection.TryDequeue(out var session))
            {
                var collectionToRead = new ConcurrentQueue<string>();
                var collectionToWrite = new ConcurrentQueue<string>();
                var PipeClien = new PipeClients(ref session,ref collectionToRead,ref collectionToWrite);

                string mensaje = "HolaMundo";
                collectionToWrite.Enqueue(mensaje);
                PipeClien.SendMessages();

                var streamClien1=client1.GetStream();
                var streamClien2=client2.GetStream();
                
                byte[] bufferClien1 = new byte[mensaje.Length];
                byte[] bufferClien2 = new byte[mensaje.Length];

                streamClien1.Read(bufferClien1, 0, bufferClien1.Length);
                streamClien2.Read(bufferClien2,0,bufferClien2.Length);

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
