using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using CameraClient.Sources.Net;

namespace CameraClient
{
    #region Public Delegates

        // delegates used to call MainForm functions from worker thread
        public delegate void DelegateSetImage(Image s);

    #endregion

    public partial class MainForm : Form
    {
        // worker thread
        Thread workerThread;

        //Delegate
        public DelegateSetImage delegateSetImage;

        public MainForm()
        {
            InitializeComponent();

            
            // initialize delegates
            delegateSetImage = new DelegateSetImage(this.setImage);

            // create worker thread instance
            //workerThread = new Thread(new ThreadStart(this.readStream));
            //workerThread.Name = "Network Stream Worker Thread";
            //workerThread.Start();

        }

        public void setImage(Image image)
        {
            pctCamera.Image = image;
        }

        public void readStream()
        {
            Client client = new Client();
            
            client.Connect("169.254.2.1");
            
            while (true)
            {
                Application.DoEvents();
                try
                {
                    Image picture = client.getImage();
                    //Console.WriteLine("ok");
                    //this.Invoke(this.delegateSetImage, new Object[] { picture });
                    pctCamera.Image = picture;
                }
                catch(Exception exc)
                {
                    Console.WriteLine(exc);
                }
            }

         //   client.Disconnect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            readStream();
        }
    }
}