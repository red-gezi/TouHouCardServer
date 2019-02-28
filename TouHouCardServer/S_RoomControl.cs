using NetworkCommsDotNet.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouHouCardServer
{
    class S_RoomControl
    {
        static List<Room> Rooms = new List<Room>();
        public static void CreatRoom(Connection Plaery, string UserInfo)
        {
            List<int> RoomdIds = Rooms.Select(room => room.RoomId).ToList();
            for (int i = 0; ; i++)
            {
                if (!RoomdIds.Contains(i))
                {
                    Room TargetRoom = new Room(i);
                    Rooms.Add(TargetRoom);
                    TargetRoom.Creat(Plaery, UserInfo);
                    break;
                }
            }
        }
        public static void JoinRoom(Connection Player, string UserInfo)
        {
            Console.WriteLine("房间数" + Rooms.Count);
            if (Rooms.Where(room => room.IsEmpty).Count() > 0)
            {
                Room TargetRoom = Rooms.First(room => room.IsEmpty);
                TargetRoom.Join(Player, UserInfo);
                TargetRoom.Start();
                Console.WriteLine("加入房间");

            }
            else
            {
                CreatRoom(Player, UserInfo);
                Console.WriteLine("创建房间");

            }
            Console.WriteLine("kong" + Rooms[0].IsEmpty);

        }
        class Room
        {
            public int RoomId;
            public bool IsEmpty => P2 == null;
            Connection P1;
            Connection P2;
            string Player1Info;
            string Player2Info;
            public Room(int roomId)
            {
                RoomId = roomId;
            }
            public void Creat(Connection Player, string UserInfo)
            {
                P1 = Player;
                Player1Info = UserInfo;
            }
            public void Join(Connection Player, string UserInfo)
            {
                P2 = Player;
                Player2Info = UserInfo;
            }
            public void Start()
            {
                Console.WriteLine("发送数据");
                Console.WriteLine(Player1Info);
                Console.WriteLine(Player2Info);
                P1.SendMessge("JoinResult", Player2Info);
                P2.SendMessge("JoinResult", Player1Info);
            }
        }
    }
}
