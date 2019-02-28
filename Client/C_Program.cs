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
using static NetworkCommsDotNet.NetworkComms;

namespace Client
{
    class C_Program
    {
        static PlayerInfo UserInfo;
        static string Name = "";
        static void Main(string[] args)
        {
            Name = Console.ReadLine();
            Bind("JoinResult", JoinResponse);
            Register();
            Login();
            Join();
            Console.ReadLine();

            Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(1);
                    NetCommand.free("你好");
                }
            });
        }

      

        private static void Login()
        {
            string Text = NetCommand.Login(Name, "");
            Console.WriteLine(Text);
            GeneralCommand<string> msg2 = Text.ToObject<GeneralCommand<string>>();
            msg2.Datas.ToList().ForEach(Console.WriteLine);
            UserInfo = msg2.Datas[1].ToObject<PlayerInfo>();
        }
        private static void Register()
        {
            NetCommand.Register(Name, "");
        }
        private static void Join()
        {
            NetCommand.JoinRoom(new GeneralCommand(UserInfo));
        }
        private static void JoinResponse(PacketHeader packetHeader, Connection connection, string incomingObject)
        {
            Console.WriteLine("得到" + incomingObject);
            PlayerInfo Data = incomingObject.ToObject<GeneralCommand<PlayerInfo>>().Datas[0];
            Console.WriteLine("收到" + Data.ToJson());
        }
        public static void Bind(string Tag, PacketHandlerCallBackDelegate<string> Func)
        {
            AppendGlobalIncomingPacketHandler(Tag, Func);
        }

    }
}
