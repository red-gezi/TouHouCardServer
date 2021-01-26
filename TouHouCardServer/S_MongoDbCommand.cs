using MongoDB.Driver;
using NetworkCommsDotNet.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TouHouCardServer
{
    class S_MongoDbCommand
    {
        static MongoClient client;
        static IMongoDatabase db;
        static IMongoCollection<PlayerInfo> Collection;
        public static void Init()
        {
            client = new MongoClient("mongodb://127.0.0.1:27017");
            db = client.GetDatabase("Gezi");
            Collection = db.GetCollection<PlayerInfo>("PlayerInfo");
        }
        public static int RegisterInfo(string Name, string Password)
        {
            var CheckUserExistQuery = Builders<PlayerInfo>.Filter.Where(info => info.Name == Name);
            if (Collection.Find(CheckUserExistQuery).CountDocuments() > 0)
            {
                return -1;//已存在
            }
            else
            {
                Collection.InsertOne(new PlayerInfo(Name, Password, new List<CardDeck>() { new CardDeck("gezi", 0, new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }) }));
                return 1;//成功
            }
        }
        public static int LoginInfo(string Name, string Password, out PlayerInfo UserInfo)
        {
            var LoadUserInfoQuery = Builders<PlayerInfo>.Filter.Where(info => info.Name == Name && info.Password == Password);
            var CheckUserExistQuery = Builders<PlayerInfo>.Filter.Where(info => info.Name == Name);
            UserInfo = null;
            if (Collection.Find(LoadUserInfoQuery).ToList().Count > 0)
            {
                UserInfo = Collection.Find(LoadUserInfoQuery).ToList()[0];
            }
            //1正确,-1密码错误,-2无此账号
            return UserInfo != null ? 1 : Collection.Find(CheckUserExistQuery).CountDocuments() > 0 ? -1 : -2;
        }
    }
}
