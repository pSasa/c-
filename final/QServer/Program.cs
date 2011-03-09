using System;
using System.Threading;

namespace QServer
{
    public class Program
    {
        private static void Usage()
        {
            Console.WriteLine("Usage: QServer.exe port DNS");
        }
        public static void Main(string[] args)
        {
            int port;
            string dns;
            if (args.Length != 2)
            {
                Usage();
                return;
            }
            if (!Int32.TryParse(args[0], out port))
            {
                Usage();
                return;
            }
            dns = args[1];

            DBLayer.SetDns("DSN=" + dns);
            if(!DBLayer.Test())
            {
                Console.WriteLine("Ошибка при установке соединения с DB");
                return;
            }

            SocketLayer sl = new SocketLayer();
            sl.StartListen(port);
            while (true)
                Thread.Sleep(1000);
            Console.ReadKey();
            sl.StopListen();
        }
    }
}
