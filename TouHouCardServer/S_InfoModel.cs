using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouHouCardServer
{
    public class PlayerInfo
    {

        public ObjectId _id;
        public string Name;
        public string Password;
        public int Level;
        public int Rank;
        public int Faith;
        public int Recharge;
        public int UseDeckNum;
        public List<CardDeck> Deck;
        public CardDeck UseDeck => Deck[UseDeckNum];
        public PlayerInfo(string Name, string Password, List<CardDeck> Deck)
        {
            this.Name = Name;
            this.Deck = Deck;
            this.Password = Password;
            Level = 0;
            Rank = 0;
            Faith = 0;
            Recharge = 0;
            UseDeckNum = 0;
        }
    }
    public class CardDeck
    {
        public string DeckName;
        public int LeaderId;
        public List<int> CardIds;
        public CardDeck(string DeckName, int LeaderId, List<int> CardIds)
        {
            this.DeckName = DeckName;
            this.LeaderId = LeaderId;
            this.CardIds = CardIds;
        }
    }

    public class GeneralCommand
    {
        public object[] Datas;
        public GeneralCommand(params object[] Datas)
        {
            this.Datas = Datas;
        }
    }
    public class GeneralCommand<T>
    {
        public T[] Datas;
        public GeneralCommand(params T[] Datas)
        {
            this.Datas = Datas;
        }
    }
}
