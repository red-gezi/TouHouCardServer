using NetworkCommsDotNet.Connections;
using Newtonsoft.Json;

namespace TouHouCardServer
{
    public static class S_Extern
    {
        public static string ToJson(this object target)
        {
            return JsonConvert.SerializeObject(target);
        }
        public static T ToObject<T>(this string Data)
        {
            return JsonConvert.DeserializeObject<T>(Data);
        }
        public static void SendMessge(this Connection con, string Tag, string Info)
        {
            con.SendObject(Tag, Info);
        }
        public static void SendMessge(this Connection con, string Tag, object Info)
        {
            con.SendObject(Tag, Info.ToJson());
        }
        public static void SendReceiveMessge(this Connection con, string SengTag, string ReceiveTag, object Info)
        {
            string resMsg = con.SendReceiveObject<string, string>(SengTag, ReceiveTag, 5000, Info.ToJson());
        }
    }

}
