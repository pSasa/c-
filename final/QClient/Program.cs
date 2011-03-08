using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace QClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Thread t = new Thread(o => QServer.Program.Main(null));
//            t.Start();
            Thread.Sleep(1000);

            SocketClient.SetServer("127.0.0.1", 8888);
            Application.Run(new Form1());
            t.Abort();
        }
    }
}
