using System;
using System.Threading;
using System.Windows.Forms;

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
            /*
             * кусок для запуска сервера при запуске клиента
                        string[] str = { "8888", "PostgreSQL30" };
                        Thread t = new Thread(o => QServer.Program.Main(str));
                        t.Start();
                        Thread.Sleep(1000);
            */
            ClientSettings cs;
            ClientSettings.LoadSettings(out cs);
            if (cs != null)
            {
                SocketClient.SetServer(cs.Server, cs.Port);
            }
            else
            {
                Settings settings = new Settings();
                if (settings.ShowDialog() == DialogResult.OK)
                {
                    //save settings
                    if (ClientSettings.SaveSettings(new ClientSettings(settings.Server, settings.Port)))
                    {
                        SocketClient.SetServer(settings.Server, settings.Port);
                    }
                }
                else
                {
                    return;
                }
            }

            Application.Run(new TabelForm());
/*
            t.Abort();
  */
        }
    }
}
