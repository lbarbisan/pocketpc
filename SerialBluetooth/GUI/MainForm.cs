using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using SerialBluetooth.Business.Parallax;
using System.Threading;

namespace SerialBluetooth.GUI
{
    public partial class MainForm : Form
    {
        private BoeBotControl control = new BoeBotControl();

        private string error;

        private int leftSpeed = 100;
        private int rightSpeed = 100;

        private int oldDiff;
        private int oldSpeed;
        
        public MainForm()
        {
            InitializeComponent();

            control.Connect(6, out error);
            control.Start();
            control.ExecuteMain();
            control.SendSpeed(leftSpeed, rightSpeed);

            control.Start();
        }

        private void pnl_MouseMove(object sender, MouseEventArgs e)
        {
            move(e.X, e.Y);
        }

        private void pnl_MouseUp(object sender, MouseEventArgs e)
        {
            control.SendSpeed(100, 100);
        }

        private void move(int x, int y)
        {
            int speed = (21 - ((y * 21) / pnl.Height)) * 10 - 10;
            int diff = (((x * 21) / pnl.Width) - 10) * 10;

            if (speed == oldSpeed && diff == oldDiff)
            {
                return;
            }
            else
            {
                oldDiff = diff;
                oldSpeed = speed;
            }

            //Turn Left
            if (diff >= 0)
            {
                if (speed >= 100)
                {
                    leftSpeed = 200 - diff;
                    rightSpeed = speed;
                }
                else
                {
                    leftSpeed = speed;
                    rightSpeed = diff;
                }
            }
            //Turn Right    
            else
            {
                if (speed >= 100)
                {
                    leftSpeed = speed;
                    rightSpeed = 200 + diff;
                }
                else
                {
                    leftSpeed = -diff;
                    rightSpeed = speed;
                }
            }

            control.SendSpeed(leftSpeed, rightSpeed);
        }

        private void pnl_MouseDown(object sender, MouseEventArgs e)
        {
            move(e.X, e.Y);
        }
    }
}