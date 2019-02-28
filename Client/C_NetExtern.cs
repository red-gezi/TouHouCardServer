using NetworkCommsDotNet.Connections;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;


public static class NetExtern
{
    public static string ToJson(this object target)
    {
        return JsonConvert.SerializeObject(target);
    }
    public static T ToObject<T>(this string Data)
    {
        return JsonConvert.DeserializeObject<T>(Data);
    }
    public static void SendMessge(this Connection con, string Tag, object Info)
    {
        con.SendObject(Tag, Info.ToJson());
    }
    public static string SendReceiveMessge(this Connection con, string SengTag, string ReceiveTag, object Info,int LimitTime=5)
    {
        string resMsg = con.SendReceiveObject<string, string>(SengTag, ReceiveTag, LimitTime*1000, Info.ToJson());
        return resMsg;
    }
}
