using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.WindowsCE.Forms;
using System.IO;
using System.Xml;
using Microsoft.WindowsMobile.Forms;
using System.Threading;
using TimedCamera.Source.Net;

namespace RobotAgent.GUI
{
    public partial class CaptureForm : Form
    {

        [DllImport("CameraCaptureDLL.DLL")]
        private static extern bool CaptureStill(string Path);

        [DllImport("CameraCaptureDLL.DLL")]
        private static extern bool InitializeGraph(IntPtr hWnd);

        private string ImageStoreLocation;

        public CaptureForm()
        {
            InitializeComponent();
        }

        private void CaptureForm_Load(object sender, EventArgs e)
        {
            this.Visible = true;

            InitializeGraph(this.Handle);
            txtDescription.Text += "Initialisation Direct Show...OK\r\n";
            ImageStoreLocation = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + @"\ImageFile.jpg";
            Server server = new Server();
            txtDescription.Text += "Initialisation Network...OK\r\n";
            server.startListener();
            txtDescription.Text += "Connection...OK\r\n";

            while (true)
            {
            //    //Wait
                Application.DoEvents();

                if (File.Exists(ImageStoreLocation))
                {
                    File.Delete(ImageStoreLocation);
                }
            //    //Capture image
                CaptureStill(ImageStoreLocation);
            //    //Bitmap
                Thread.Sleep(1000);
                Bitmap bitmap = new Bitmap(ImageStoreLocation);
                ptcCamera.Image = bitmap;

                //    //Create a file stream
                FileStream filename = new FileStream(ImageStoreLocation, FileMode.Open);
                //    //To get the size of the file for purpose of memory allocation
                FileInfo filenameInfo = new FileInfo(ImageStoreLocation);

                byte[] buffer = new byte[filenameInfo.Length + 4];
                byte[] length = BitConverter.GetBytes((int)filenameInfo.Length);

                //    //Read the content of the file and close
                Buffer.BlockCopy(length, 0, buffer, 0, 4);
                filename.Read(buffer, 4, buffer.Length - 4);
                filename.Close();

                try
                {
                    server.send(buffer);
                }
                catch
                {
                    server.stopListener();
                    txtDescription.Text += "Fin de Connection...OK\r\n";

                    server.startListener();
                    txtDescription.Text += "Connection...OK\r\n";
                }
            }
        }
   }
}