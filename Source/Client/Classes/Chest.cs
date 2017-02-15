using SFML.Graphics;
using SFML.System;
using System.Data.SQLite;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using static System.Convert;

namespace Client.Classes
{
    class Chest
    {
        public string Name { get; set; }
        public int Money { get; set; }
        public int Experience { get; set; }
        public int RequiredLevel { get; set; }
        public int TrapLevel { get; set; }
        public int Key { get; set; }
        public int Damage { get; set; }
        public int NpcSpawn { get; set; }
        public int SpawnAmount { get; set; }
        public ChestItem[] ChestItem = new ChestItem[10];

        public Chest()
        {
            for (int i = 0; i < 10; i++)
            {
                ChestItem[i] = new ChestItem("None", 0, 1);
            }
        }

        public Chest(string name, int money, int exp, int reqlvl, int traplvl, int key, int damage, int npcspawn, int npcamount)
        {
            Name = name;
            Money = money;
            Experience = exp;
            RequiredLevel = reqlvl;
            TrapLevel = traplvl;
            Key = key;
            Damage = damage;
            NpcSpawn = npcspawn;
            SpawnAmount = npcamount;

            for (int i = 0; i < 10; i++)
            {
                ChestItem[i] = new ChestItem("None", 0, 1);
            }
        }
    }
    
    public class ChestItem
    {
        public string Name { get; set; }
        public int ItemNum { get; set; }
        public int Value { get; set; }

        public ChestItem() { }

        public ChestItem(string name, int itemnum, int value)
        {
            Name = name;
            ItemNum = itemnum;
            Value = value;
        }
    }
}
