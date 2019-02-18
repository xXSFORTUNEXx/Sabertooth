using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SabertoothClient
{
    public class Quests
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StartMessage { get; set; }
        public string InProgressMessage { get; set; }
        public string CompleteMessage { get; set; }
        public int PrerequisiteQuest { get; set; }
        public int LevelRequired { get; set; }
        public int[] ItemNum = new int[5];
        public int[] ItemValue = new int[5];
        public int[] NpcNum = new int[3];
        public int[] NpcValue = new int[3];
        public int[] RewardItem = new int[5];
        public int[] RewardValue = new int[5];
        public int Experience { get; set; }
        public int Money { get; set; }
        public int Type { get; set; }

        public Quests() { }
    }
    public enum QuestType : int
    {
        None,
        TalkToNpc,
        KillNpc,
        GetItemForNpc
    }
}
