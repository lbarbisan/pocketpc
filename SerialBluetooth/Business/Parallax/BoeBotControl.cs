//-----------------------------------------------------------------------
//  This file is part of the Microsoft Robotics Studio Code Samples.
//
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//  Copyright (C) Parallax, Inc. All Rights Reserved.
//
//  $File: BoeBotControl.cs $ $Revision: 9 $
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Threading;

namespace SerialBluetooth.Business.Parallax
{
    /// <summary>
    /// All concurrency is done by calling service. Assume this is not thread safe
    /// </summary>
    internal class BoeBotControl
    {
        SerialPort _serialPort;
        //BasicStamp2Operations _bs2Port = null;

        bool _autonomousMode = false;
        public bool AutonMode
        {
            get { return _autonomousMode; }
            set
            {
                _autonomousMode = value;
                pwmLeft = 100;
                pwmRight = 100;
            }
        }

        bool connected = false;
        bool running = false;

        public bool Running
        {
            get { return running; }
            set { running = value; }
        }
        int _delay = 0;
        public int Delay
        {
            get { return _delay; }
            set { _delay = value; }
        }

        internal int frameCounter = 0;
        int pwmLeft = 100;
        int pwmRight = 100;
        byte msgCnt = 0;

        bool handshake = false;             // Handshake after BT connection

        const int turnCounts = 5;

        #region Constructors
        public BoeBotControl()
        {
        }

        ~BoeBotControl()
        {
            Close();
        }
        #endregion

        public bool Connect(int serialPort, out string errorMessage)
        {
            if (serialPort <= 0)
            {
                errorMessage = "The Boe-Bot serial port is not configured!";
                return false;
            }

            if (connected)
            {
                Close();
            }

            try
            {
                string ComPort = "COM" + serialPort.ToString();
                _serialPort = new SerialPort(ComPort, 9600, Parity.None, 8, StopBits.One);
                _serialPort.WriteTimeout = 60000;
                _serialPort.Open();
                _serialPort.DiscardInBuffer();
                errorMessage = string.Empty;
                connected = true;
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = string.Format("Error connecting Boe-Bot to COM{0}: {1}", serialPort, ex.Message);
                return false;
            }
        }

        public void Close()
        {
            if (!connected)
                return;
            connected = false;

            if (running)
            {
                running = false;
                Thread.Sleep(100);
            }

            if (_serialPort != null && _serialPort.IsOpen)
            {
                _serialPort.Close();
                _serialPort = null;
            }

        }

        public void Start()
        {
            running = true;
        }

        public void Stop()
        {
            running = false;
        }

        /// <summary>
        /// -1.0 to 1.0
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public void SetSpeed(double? left, double? right)
        {
            if ((left != null && (left < -1.0 || left > 1.0))
                || ((right != null) && (right < -1.0 || right > 1.0)))
                throw new SystemException("Invalid Speed!");

            if (left != null)
                pwmLeft = (int)((left * 100) + 100);

            if (right != null)
                pwmRight = (int)((-right * 100) + 100);
        }

        internal void ExecuteMain()
        {
            if (!this.running)
            {
                Thread.Sleep(250);
                return;
            }
            if (!_autonomousMode)
            {
                SendSpeed(pwmLeft, pwmRight);
            }

            // <--- 
            if (!handshake)
            {
                handshake = WaitForConnect();
            }
            else
            {
                frameCounter++;
            }
            if (Delay > 0)
            {
                Console.WriteLine("Sleeping...");
                Thread.Sleep(Delay);
            }
        }

        #region Control

        private void BotDelay(int time)
        // Boe-Bot does nothing for a time in ms.
        {
            byte[] buf = new byte[5];
            buf[2] = 98;
            buf[3] = (byte)(time % 256);
            buf[4] = (byte)(time / 256);
            SendPackets(ref buf);
        }
        #endregion

        #region Configuration

        public void SendSpeed(int left, int right)
        // 0 is full speed clockwise.  200 is full speed counterclockwise.  100 is stopped.
        {
            byte[] buf = new byte[5];
            buf[2] = 32;
            buf[3] = (byte)left;
            buf[4] = (byte)right;
            SendPackets(ref buf);
        }

        public void Maneuver(char m)
        {
            byte[] buf = new byte[5];
            buf[2] = 33;
            buf[3] = (byte)m;
            SendPackets(ref buf);
        }
        #endregion

        #region Communication
        /**
         *  Handshake happens here.  192 causes the Boe-Bot's program to restart.  It sends a 1, 
         *  PC replies with 2, then Boe-Bot replies with 3.  If Boe-Bot want's to restart handshake,
         *  it can send a 1.
         */
        private bool WaitForConnect()
        {
            byte[] buf = new byte[5];
            buf[1] = 0;
            buf[2] = 192;
            SendPackets(ref buf);
            do
            {
                if (!running)
                {
                    break;
                }

                buf[2] = (byte)2;
                SendPackets(ref buf);
            } while (buf[2] != (byte)3);
            msgCnt = (byte)buf[1];
            return true;

            //callback("*" + buf[0] + "-" + buf[1] + "-" + buf[2] + "-" + buf[3] + "-" + buf[4] + "*");

        }

        /**
         *  Now, packet is set repeatedly until reply is received from the Boe-Bot.
         */
        private bool SendPackets(ref byte[] packet)
        {
            if (packet.Length != 5) throw new SystemException("Invalid packet length!");
            packet[0] = 255;                                            // Start byte.
            packet[1] = msgCnt;                                         // Message index, incremened by Boe-Bot.
            // Must be used in next message that is
            // sent.
            while (_serialPort.BytesToRead < 5)
            {
                _serialPort.Write(packet, 0, 5);
                while (_serialPort.BytesToWrite > 0) Thread.Sleep(1);
            }
            _serialPort.Read(packet, 0, 5);                             // Get Boe-Bot's reply.
            msgCnt = packet[1];                                         // Get next message count.
         
            if (packet[2] == 1) handshake = false;                      // Boe-Bot requests reconnect.
            
            //callback("^" + bytes + "@" + abytes + "#" + aabytes + "*");// + packet[0] + "-" + packet[1] + "-" + packet[2] + "-" + packet[3] + "-" + packet[4] + "*\r\n");
            
            return true;
        }
        #endregion
    }
    enum AutonomousDirection { FWD, LEFT, RIGHT, BKWD }
    enum CommandLoop { STOP = 0, START }
}
