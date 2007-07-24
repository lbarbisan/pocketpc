using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace TimedCamera.Source.Net
{
    class Server
    {
        // Create an instance of the TcpListener class.             
        private TcpListener tcpListener = null;
        private TcpClient tcpClient;
        private NetworkStream stream;

        public NetworkStream Stream
        {
            get { return stream; }
            set { stream = value; }
        }

        public Server() { }

        public void stopListener()
        {
            try
            {
                stream.Close();
                tcpClient.Close();
                tcpListener.Stop();
            }
            catch
            {
                //NE FAIS RIEN
            }
        }

        public void startListener()
        {

            IPAddress ipAddress = new IPAddress(new byte[] { 169, 254, 2, 1 }); 
            try
            {
                // Set the listener on the local IP address                 
                // and specify the port.                 
                tcpListener = new TcpListener(ipAddress, 1520);
                tcpListener.Start();

                // Create a TCP socket.                 
                // If you ran this server on the desktop, you could use                 
                // Socket socket = tcpListener.AcceptSocket()                 
                // for greater flexibility.                 
                tcpClient = tcpListener.AcceptTcpClient();

                // Read the data stream from the client.                 
                stream = tcpClient.GetStream();
            }
            catch
            {
                //NE FAIS RIEN
            }
        }

        public void send(byte[] data)
        {
            stream.Write(data, 0, data.Length);
            stream.Flush();
        }
    }
}
