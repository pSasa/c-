using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace QServer
{
    class SocketLayer
    {
        private Thread t = null;
        public void StartListen()
        {
            t = new Thread(Listen);
            t.Start();
        }

        public void Listen()
        {
            TcpListener serverSocket = new TcpListener(IPAddress.Parse("127.0.0.1"), 8888);
            TcpClient clientSocket = default(TcpClient);
            serverSocket.Start();
            Console.WriteLine(" >> Server Started");
            try
            {
                while (true)
                {
                    if (serverSocket.Pending())
                    {
                        clientSocket = serverSocket.AcceptTcpClient();
                        ThreadPool.QueueUserWorkItem(o => ProcessRequest(clientSocket));
                    }
                    else 
                    {
                        Thread.Sleep(1000);
                    }
                }
            }
            finally
            {
                Console.WriteLine(" >> Server Ended");
            }
        }

        public void StopListen()
        {
            t.Abort();
        }

        public void ProcessRequest(TcpClient request)
        {
            NetworkStream stream = null;
            try
            {
                stream = request.GetStream();
                BinaryFormatter deserializer = new BinaryFormatter();
                Request clone = (Request)deserializer.Deserialize(stream);
                DBLayer layer = DBLayer.Create();
                Object res = null;
                switch (clone.type)
                {
                    case RequestType.GetAllPerson:
                        {
                            res = layer.GetAllPersons();
                            break;
                        }
                    case RequestType.GetPerson:
                        {
                            if (!(clone.param is int))
                            {
                                throw new InvalidRequestFormat("GetPerson должен содержать объект типа Int32");
                            }
                            res = layer.GetPerson((int)clone.param);
                            break;
                        }
                    case RequestType.SavePerson:
                        {
                            if(!(clone.param is Person))
                            {
                                throw new InvalidRequestFormat("SavePerson должен содержать объект типа Person");
                            }
                            res = layer.SavePerson((Person)clone.param);
                            break;
                        }
                    case RequestType.DeletePerson:
                        {
                            if (!(clone.param is Person))
                            {
                                throw new InvalidRequestFormat("SavePerson должен содержать объект типа Person");
                            }
                            res = layer.DeletePerson((Person)clone.param);
                            break;
                        }
                    default:
                        {
                            throw new InvalidRequestFormat("Unknown request type");
                        }
                }
                GenerateOkResponse(stream, res);
            }
            catch (InvalidRequestFormat e)
            {
                if (stream != null)
                {
                    GenerateFailResponse(stream, e.Message);
                }
            }
            catch (DBException e)
            {
                if (stream != null)
                {
                    GenerateFailResponse(stream, e.Message);
                }
            }
            catch (Exception e)
            {
                if (stream != null)
                {
                    GenerateFailResponse(stream, "Inner Server Exception: " + e.Message);
                }
                request.Close();
                throw;
            }
            finally
            {
                request.Close();
            }
        }

        private void GenerateOkResponse(Stream stream, Object res)
        {
            GenerateResponse(stream, res,ResponseType.Ok);
        }
        private void GenerateFailResponse(Stream stream, Object res)
        {
            GenerateResponse(stream, res, ResponseType.Fail);
        }

        private void GenerateResponse(Stream stream, Object res, ResponseType type)
        {
            Response response = new Response();
            response.type = ResponseType.Ok;
            response.param = res;
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(stream, response);
        }

    }
}
