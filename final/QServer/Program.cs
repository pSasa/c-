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
            //ThreadPool.QueueUserWorkItem(o=>test());
            while (true)
                Thread.Sleep(1);
            Console.ReadKey();
            sl.StopListen();
            Console.ReadKey();
        }

        static public void test()
        {
            TcpClient client = new TcpClient();
            client.Connect("127.0.0.1", 8888);
            Request request = new Request();
            request.type = RequestType.GetAllPerson;
            request.param = null;
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(client.GetStream(), request);
            client.GetStream().Flush();

            BinaryFormatter deserializer = new BinaryFormatter();
            Response clone = (Response)deserializer.Deserialize(client.GetStream());
            Console.Write(clone.type);
        }
    }

}
