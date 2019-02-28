using MongoDB.Bson;
using MongoDB.Driver;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static NetworkCommsDotNet.NetworkComms;

namespace TouHouCardServer
{
    class S_Program
    {

        //class Info
        //{
        //    public ObjectId id;
        //    public string Name;
        //    public string Password;
        //    //public List<Deck> Decks = new List<Deck>();

        //    //public Info(string name, string password, List<Deck> decks)
        //    //{
        //    //    Name = name;
        //    //    Password = password;
        //    //    Decks = decks;
        //    //}

        //}
        static void Main(string[] args)
        {
            S_MongoDbCommand.Init();
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 495);
            Connection.StartListening(ConnectionType.TCP, ip);
            Bind("Regist", RegistResponse);//绑定标签与函数
            Bind("Login", LoginResponse);//绑定标签与函数
            Bind("Join", JoinResponse);//绑定标签与函数
            Console.WriteLine("开始监听");
            Console.ReadLine();
        }
        public static void Bind(string Tag, PacketHandlerCallBackDelegate<string> Func)
        {
            AppendGlobalIncomingPacketHandler(Tag, Func);
        }
        private static void RegistResponse(PacketHeader packetHeader, Connection connection, string msg)
        {
            GeneralCommand<string> ReciverMsg = msg.ToObject<GeneralCommand<string>>();
            int ResultNum = S_MongoDbCommand.RegisterInfo(ReciverMsg.Datas[0], ReciverMsg.Datas[1]);
            Console.WriteLine("收到了" + msg);
            connection.SendMessge("RegistResult", new GeneralCommand<int>(ResultNum));
        }
        List<Connection> connections = new List<Connection>();
        private static void LoginResponse(PacketHeader packetHeader, Connection connection, string msg)
        {
            //Console.WriteLine(connection.GetHashCode());
            GeneralCommand<string> ReciverMsg = msg.ToObject<GeneralCommand<string>>();
            int ResultNum = S_MongoDbCommand.LoginInfo(ReciverMsg.Datas[0], ReciverMsg.Datas[1], out PlayerInfo userinfo);
            Console.WriteLine("查询结果"+userinfo.ToJson());
            //Console.WriteLine("收到了" + msg);
            connection.SendMessge("LoginResult", new GeneralCommand(ResultNum.ToJson(),userinfo.ToJson()));
            //connection.Dispose();
        }
        private static void JoinResponse(PacketHeader packetHeader, Connection connection, string UserInfo)
        {
            S_RoomControl.JoinRoom(connection, UserInfo);
        }
    }

}
