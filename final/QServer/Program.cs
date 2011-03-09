using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net.Sockets;
using System.Threading;

namespace QServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DBLayer.SetDns("DSN=PostgreSQL30");

            SocketLayer sl = new SocketLayer();
            sl.StartListen();
            while (true)
                Thread.Sleep(1);
            Console.ReadKey();
            sl.StopListen();
            Console.ReadKey();
        }
    }
}
