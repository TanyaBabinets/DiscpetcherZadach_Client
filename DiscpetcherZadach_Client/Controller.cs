using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiscpetcherZadach_Client
{
    internal class Controller
    {
        public TcpClient client;
        public delegate void SendList(List<MyProcess> LP);
        public event SendList sender;
        public void Connect(int action, MyProcess proc)
        {
            // соединяемся с удаленным устройством
            try
            {
                client = new TcpClient(IPAddress.Loopback.ToString(), 49152);//иниц.
                NetworkStream network = client.GetStream();//получить поток клиента
                MemoryStream stream = new MemoryStream();
                BinaryFormatter formatter = new BinaryFormatter();
                List<string> actions = null;
                switch (action)  // наши действия с кнопками и процессами
                {
                    case 1:
                        actions = new List<string>() { action.ToString(), 0.ToString() };
                        break;
                    case 2:
                        actions = new List<string>() { action.ToString(), proc.IdProc.ToString() };
                        break;
                }

                formatter.Serialize(stream, actions);
                byte[] data = stream.ToArray(); 
                stream.Close();
                network.Write(data, 0, data.Length);//передаем данные серверу, дата это массив байт

                ////////////////////////////////////////////////////////////////
              //это мы получаем от сервера
                data = new byte[client.ReceiveBufferSize]; //  размер буфера для клиента
                stream= new MemoryStream(data);
                network.Read(data, 0, data.Length); //считываем то, что сервер отправил обратно клиенту
                Dictionary<int, string> dict = (Dictionary<int, string>)formatter.Deserialize(stream);  
                List<MyProcess> myProcesses = new List<MyProcess>();
                foreach (var i in dict)
                {
                    myProcesses.Add(new MyProcess() { IdProc = i.Key, name = i.Value, }) ;
                }
                sender(myProcesses);
              
                stream.Close(); 
                network.Close();
                client.Close(); 


            }
            catch (Exception ex)
            {
                MessageBox.Show("Клиент: " + ex.Message);
            }
        }


      



    }
}
