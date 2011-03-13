using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using QServer;

namespace QClient
{
    sealed class SocketClient
    {
        static private string Address;
        static private int Port;

        static public void SetServer(string address, int port)
        {
            Address = address;
            Port = port;
        }

        public Response Send(Request request)
        {
            Response response = null;
            TcpClient client = null;
            try
            {
                client = new TcpClient();
                client.Connect(Address, Port);
                BinaryFormatter serializer = new BinaryFormatter();
                serializer.Serialize(client.GetStream(), request);
                client.GetStream().Flush();
                BinaryFormatter deserializer = new BinaryFormatter();
                response = (Response)deserializer.Deserialize(client.GetStream());
            }
            catch (SocketException e)
            {
                response = new Response();
                response.type = ResponseType.Fail;
                response.param = "Ошибка соединения с сервером.\nВозможно Cервер не запущет или неправильные настройкаи клиента.\n\nСообщение:\n" + e.Message;
            }
            finally
            {
                if (client != null)
                {
                    client.Close();
                }
            }
            return response;
        }

        #region обработка контейнерв
        public bool GetAllItems(ref Item[] res, Item helper)
        {
            Request request = new Request();
            request.type = helper.GatAllType;
            request.param = null;
            Response responce = Send(request);
            if (AnalizeResult(responce))
            {
                res = (Item[])responce.param;
                return true;
            }
            res = null;
            return false;
        }

        public bool GetItem(int id, ref Item item)
        {
            Request request = new Request();
            request.type = item.GetType;
            request.param = id;
            Response responce = Send(request);
            if (AnalizeResult(responce))
            {
                item = (Item)responce.param;
                return true;
            }
            item = null;
            return false;
        }

        public bool SaveItem(ref Item p)
        {
            Request request = new Request();
            request.type = p.SaveType;
            request.param = p;
            Response responce = Send(request);
            return AnalizeResult(responce);
        }

        public bool DeleteItem(Item item)
        {
            Request request = new Request();
            request.type = item.DeleteType;
            request.param = item.id;
            Response responce = Send(request);
            return AnalizeResult(responce);
        }
        #endregion

        #region анализ ответа от сервера
        private bool AnalizeResult(Response responce)
        {
            if (responce.type == ResponseType.Fail)
            {
                //do sometrhing else
                MessageBox.Show(responce.param.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        #endregion
    }
}
