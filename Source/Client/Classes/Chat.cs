using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Classes
{
    class Chat
    {
        public string Name { get; set; }
        public string MainMessage { get; set; }
        public string[] Option = new string[4];
        public int[] NextChat = new int[4];
        public int ShopNum { get; set; }
        public int MissionNum { get; set; }
        public int[] ItemNum = new int[3];
        public int[] ItemVal = new int[3];
        public int Money { get; set; }
        public int Type { get; set; }

        public Chat() { }

        public Chat(string name, string msg, string opt1, string opt2, string opt3, string opt4, int[] nextchat, int shopnum, int missionnum, int[] itemNum, int[] itemVal, int money, int type)
        {
            Name = name;
            MainMessage = msg;
            Option[0] = opt1;
            Option[1] = opt2;
            Option[2] = opt3;
            Option[3] = opt4;
            NextChat = nextchat;
            ShopNum = shopnum;
            MissionNum = missionnum;
            ItemNum = itemNum;
            ItemVal = itemVal;
            Money = money;
            Type = type;
        }
    }

    public enum ChatTypes : int
    {
        None,
        OpenShop,
        OpenBank,
        Reward,
        Mission
    }
}
