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
                response.param = e.Message;
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

        #region обработка студентов
        public bool GetAllPerson(out Person[] res)
        {
            Request request = new Request();
            request.type = RequestType.GetAllPerson;
            request.param = null;
            Response responce = Send(request);
            if (AnalizeResult(responce))
            {
                res = (Person[])responce.param;
                return true;
            }
            res = null;
            return false;
        }

        public bool GetPerson(int id, out Person p)
        {
            Request request = new Request();
            request.type = RequestType.GetPerson;
            request.param = id;
            Response responce = Send(request);
            if (AnalizeResult(responce))
            {
                p = (Person)responce.param;
                return true;
            }
            p = null;
            return false;
        }

        public bool SavePerson(ref Person p)
        {
            Request request = new Request();
            request.type = RequestType.SavePerson;
            request.param = p;
            Response responce = Send(request);
            return AnalizeResult(responce);
        }

        public bool DeletePerson(int id)
        {
            Request request = new Request();
            request.type = RequestType.DeletePerson;
            request.param = id;
            Response responce = Send(request);
            return AnalizeResult(responce);
        }
        #endregion

        #region анализ ответа от сервера
        private bool AnalizeResult(Response responce)
        {
            if (responce.type == ResponseType.Fail)
            {
                //do sometrhing
                MessageBox.Show(responce.param.ToString());
                return false;
            }
            return true;
        }
        #endregion
    }
}
