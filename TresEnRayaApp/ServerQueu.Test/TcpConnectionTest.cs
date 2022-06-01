using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ServerQueu.Test
{
    public class TcpConnectionTest
    {
        private string Ip = "127.0.0.1";
        private int Port = 4422;

        [Test]
        public void BasicTry()
        {
            string mensaje = "Hola TestBasico";

            TcpListener tcpListener = new TcpListener(System.Net.IPAddress.Parse(Ip), Port);
            TcpClient tcpClient = new TcpClient();

            tcpListener.Start();
            Assert.IsTrue(tcpListener.Server.IsBound);

            tcpClient.Connect(Ip, Port);
            NetworkStream networkStreamClienteToServer = tcpClient.GetStream();
            byte[] menssage = Encoding.ASCII.GetBytes(mensaje);
            networkStreamClienteToServer.Write(menssage, 0, menssage.Length);

            TcpClient clienteAccepted = tcpListener.AcceptTcpClient();
            Assert.IsTrue(tcpClient.Connected);

            NetworkStream networkStream = clienteAccepted.GetStream();
            Assert.IsTrue(clienteAccepted.Available > 0);

            byte[] buffer = new byte[clienteAccepted.Available];
            networkStream.Read(buffer, 0, buffer.Length);
            string msg_recieved=Encoding.ASCII.GetString(buffer, 0, buffer.Length);

            Assert.AreEqual(menssage,msg_recieved);

            networkStream.Close();
            clienteAccepted.Close();
            tcpClient.Close();
            tcpListener.Stop();
        }
    }
}
