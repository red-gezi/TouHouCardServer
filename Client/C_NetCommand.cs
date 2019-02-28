using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using NetworkCommsDotNet.Connections.TCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static NetInfoModel;

namespace Client
{
    public class NetCommand
    {
        static IPEndPoint ip = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 495);
        static ConnectionInfo connInfo = new ConnectionInfo(ip);
        static Connection Client = TCPConnection.GetConnection(connInfo);
        public static void Init()
        {
            Client = TCPConnection.GetConnection(connInfo);
        }
        public static void free(string data)
        {
            Client.SendMessge("free", data);
        }
        public static string Register(string name, string password)
        {
            return Client.SendReceiveMessge("Regist", "RegistResult", new GeneralCommand<string>(name, password));
        }
        public static string Login(string name, string password)
        {
            return Client.SendReceiveMessge("Login", "LoginResult", new GeneralCommand<string>(name, password));
        }
        public static void JoinRoom(GeneralCommand UserInfo)
        {
            Client.SendMessge("Join", UserInfo);

            //string Msg = "";
            //try
            //{
            //Msg = Client.SendReceiveMessge("Join", "JoinResult", new GeneralCommand(UserInfo), 5);
            //}
            //catch (Exception)
            //{
            //    Console.WriteLine("连接超时");
            //}
            //return Msg;
        }
    }
}
