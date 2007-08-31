using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SerialBluetooth.GUI;

namespace SerialBluetooth
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            MainForm form = new MainForm();
            Application.Run(form);
        }
    }
}