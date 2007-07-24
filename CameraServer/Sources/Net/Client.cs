using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Drawing;
using System.Diagnostics;
using System.IO;

namespace CameraClient.Sources.Net
{
    class Client
    {
        private TcpClient client;

        public Client(): base() {}

        public void Connect(string serverIP)
        {
            try
            {   
                // Create a TcpClient.                
                // The client requires a TcpServer that is connected               
                // to the same address specified by the server and port             
                // combination.                 
                Int32 port = 1520;
                client = new TcpClient(serverIP, port);
                
            }
            catch (ArgumentNullException e)
            {
                Trace.WriteLine("ArgumentNullException: " + e);                
            }
            catch (SocketException e)
            {
                Trace.WriteLine("SocketException: " + e);                
            }
        }

        public Image getImage()
        {
            Image picture=null;
            if (client.GetStream() != null)
            {
                try
                {
                    MemoryStream stream = new MemoryStream();
                    NetworkStream netWorkStream = client.GetStream();
                
                    byte[] bufferLength = new byte[4];
                    client.GetStream().Read(bufferLength, 0, 4);
                    int length = BitConverter.ToInt32(bufferLength,0);

                    byte[] buffer = new byte[length];

                    client.GetStream().Read(buffer, 0, length);
                    stream.Write(buffer,0,length);
                    stream.Seek(0, SeekOrigin.Begin);

                    picture = Bitmap.FromStream(stream, true, true);
                }
                catch(ArgumentException)
                {

                }
            }
            return picture;
        }

        public void Disconnect()
        {
            // Close everything.                 
            client.Close();
        }
    }
}
