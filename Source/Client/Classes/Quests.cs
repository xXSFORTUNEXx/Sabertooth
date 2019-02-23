using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SabertoothClient.Globals;

namespace SabertoothClient
{
    public class Quests
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StartMessage { get; set; }
        public string InProgressMessage { get; set; }
        public string CompleteMessage { get; set; }
        public string Description { get; set; }
        public int PrerequisiteQuest { get; set; }
        public int LevelRequired { get; set; }
        public int[] ItemNum = new int[MAX_QUEST_ITEMS_REQ];
        public int[] ItemValue = new int[MAX_QUEST_ITEMS_REQ];
        public int[] NpcNum = new int[MAX_QUEST_NPCS_REQ];
        public int[] NpcValue = new int[MAX_QUEST_NPCS_REQ];
        public int[] RewardItem = new int[MAX_QUEST_REWARDS];
        public int[] RewardValue = new int[MAX_QUEST_REWARDS];
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

    public enum QuestStatus : int
    {
        NotStarted,
        Inprogress,
        Complete
    }
}
