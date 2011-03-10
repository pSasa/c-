using System;
using System.Data.Common;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace QServer
{
    sealed class SocketLayer
    {
        private Thread t = null;

        #region запуск листенера ниткой
        public void StartListen(int port)
        {
            if (t != null)
            {
                throw new QServerException("Сервер уже запущен");
            }
            t = new Thread(o => Listen(port));
            t.Start();
        }

        public void Listen(int port)
        {
            TcpListener serverSocket = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
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
                        Thread.Sleep(100);
                    }
                }
            }
            catch (ThreadAbortException)
            {
                Console.WriteLine(" >> Server Ended");
                t = null;
            }
        }

        public void StopListen()
        {
            t.Abort();
            t = null;
        }
#endregion

        #region обработка запроса
        public void ProcessRequest(TcpClient tcpclient)
        {
            NetworkStream stream = null;
            try
            {
                stream = tcpclient.GetStream();
                BinaryFormatter deserializer = new BinaryFormatter();
                Object obj = deserializer.Deserialize(stream);
                if (!(obj is Request))
                {
                    throw new InvalidRequestFormat("Запрос должен содержать объект типа Request");
                }
                Request request = (Request)obj;
                DBLayer layer = DBLayer.Create();
                Object resp = null;
                switch (request.type)
                {
                    case RequestType.GetAllPerson:
                        {
                            resp = layer.GetAllPersons();
                            break;
                        }
                    case RequestType.GetPerson:
                        {
                            if (!(request.param is int))
                            {
                                throw new InvalidRequestFormat("GetPerson должен содержать объект типа int");
                            }
                            resp = layer.GetPerson((int)request.param);
                            break;
                        }
                    case RequestType.SavePerson:
                        {
                            if (!(request.param is Person))
                            {
                                throw new InvalidRequestFormat("SavePerson должен содержать объект типа Person");
                            }
                            resp = layer.SavePerson((Person)request.param);
                            break;
                        }
                    case RequestType.DeletePerson:
                        {
                            if (!(request.param is int))
                            {
                                throw new InvalidRequestFormat("SavePerson должен содержать объект типа int");
                            }
                            resp = layer.DeletePerson((int)request.param);
                            break;
                        }
                    case RequestType.GetAllSubject:
                        {
                            resp = layer.GetAllSubject();
                            break;
                        }
                    case RequestType.GetSubject:
                        {
                            if (!(request.param is int))
                            {
                                throw new InvalidRequestFormat("GetSubject должен содержать объект типа int");
                            }
                            resp = layer.GetSubject((int)request.param);
                            break;
                        }
                    case RequestType.SaveSubject:
                        {
                            if (!(request.param is Subject))
                            {
                                throw new InvalidRequestFormat("SaveSubject должен содержать объект типа Subject");
                            }
                            resp = layer.SaveSubject((Subject)request.param);
                            break;
                        }
                    case RequestType.DeleteSubject:
                        {
                            if (!(request.param is int))
                            {
                                throw new InvalidRequestFormat("SaveSubject должен содержать объект типа int");
                            }
                            resp = layer.DeleteSubject((int)request.param);
                            break;
                        }
                    case RequestType.GetAllMark:
                        {
                            resp = layer.GetAllMark();
                            break;
                        }
                    case RequestType.GetMark:
                        {
                            if (!(request.param is int))
                            {
                                throw new InvalidRequestFormat("GetMark должен содержать объект типа int");
                            }
                            resp = layer.GetMark((int)request.param);
                            break;
                        }
                    case RequestType.SaveMark:
                        {
                            if (!(request.param is Mark))
                            {
                                throw new InvalidRequestFormat("SaveMark должен содержать объект типа Mark");
                            }
                            resp = layer.SaveMark((Mark)request.param);
                            break;
                        }
                    case RequestType.DeleteMark:
                        {
                            if (!(request.param is int))
                            {
                                throw new InvalidRequestFormat("SaveMark должен содержать объект типа int");
                            }
                            resp = layer.DeleteMark((int)request.param);
                            break;
                        }
                    default:
                        {
                            throw new InvalidRequestFormat("Неизвесный запрос");
                        }
                }
                GenerateOkResponse(stream, resp);
            }
            catch (InvalidRequestFormat e)
            {
                if (stream != null)
                {
                    GenerateFailResponse(stream, e.Message);
                }
            }
            catch (DbException e)
            {
                if (stream != null)
                {
                    GenerateFailResponse(stream, "Ошибка базы данных:\n" + e.Message);
                }
            }
            catch (Exception e)
            {
                //внутреняя ошибка - сообщаем клиенту и кидаем дальше
                if (stream != null)
                {
                    GenerateFailResponse(stream, "Внутренняя ошибка сервера:\n" + e.Message);
                }
                tcpclient.Close();
                throw;
            }
            finally
            {
                tcpclient.Close();
            }
        }
        #endregion

        #region генерация ответа
        private void GenerateOkResponse(Stream stream, Object res)
        {
            GenerateResponse(stream, res, ResponseType.Ok);
        }

        private void GenerateFailResponse(Stream stream, Object res)
        {
            GenerateResponse(stream, res, ResponseType.Fail);
        }

        private void GenerateResponse(Stream stream, Object res, ResponseType type)
        {
            Response response = new Response();
            response.type = type;
            response.param = res;
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(stream, response);
        }
        #endregion
    }
}
