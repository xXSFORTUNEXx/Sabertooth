using Microsoft.VisualBasic;
using System;
using System.IO;
using static Microsoft.VisualBasic.Interaction;
using Lidgren.Network;

namespace Server.Classes
{
    class Item
    {
        public string Name { get; set; }
        public int Sprite { get; set; }
        public int Damage { get; set; }
        public int Armor { get; set; }
        public int Type { get; set; }
        public int HealthRestore { get; set; }
        public int HungerRestore { get; set; }
        public int HydrateRestore { get; set; }

        public int addStrength { get; set; }
        public int addAgility { get; set; }
        public int addEndurance { get; set; }
        public int addStamina { get; set; }

        public Item() { }

        public Item(string name, int sprite, int damage, int armor, int type, int healthRestore, int foodRestore, int drinkRestore, int str, int agi, int end, int sta)
        {
            Name = name;
            Sprite = sprite;
            Damage = damage;
            Armor = armor;
            Type = type;
            HealthRestore = healthRestore;
            HungerRestore = foodRestore;
            HydrateRestore = drinkRestore;
            addStrength = str;
            addAgility = agi;
            addEndurance = end;
            addStamina = sta;
        }

        public void SaveItem(int itemNum)
        {
            FileStream fileStream = File.OpenWrite("Items/Item" + itemNum + ".bin");
            BinaryWriter binaryWriter = new BinaryWriter(fileStream);
            LogWriter.WriteLog("Saving default Items...", "Server");
            binaryWriter.Write(Name);
            binaryWriter.Write(Sprite);
            binaryWriter.Write(Damage);
            binaryWriter.Write(Armor);
            binaryWriter.Write(Type);
            binaryWriter.Write(HealthRestore);
            binaryWriter.Write(HungerRestore);
            binaryWriter.Write(HydrateRestore);
            binaryWriter.Write(addStrength);
            binaryWriter.Write(addAgility);
            binaryWriter.Write(addEndurance);
            binaryWriter.Write(addStamina);
            binaryWriter.Flush();
            binaryWriter.Close();
        }

        public void LoadItem(int itemNum)
        {
            FileStream fileStream = File.OpenRead("Items/Item" + itemNum + ".bin");
            BinaryReader binaryReader = new BinaryReader(fileStream);
            LogWriter.WriteLog("Loading item...", "Server");
            try
            {
                Name = binaryReader.ReadString();
                Sprite = binaryReader.ReadInt32();
                Damage = binaryReader.ReadInt32();
                Armor = binaryReader.ReadInt32();
                Type = binaryReader.ReadInt32();
                HealthRestore = binaryReader.ReadInt32();
                HungerRestore = binaryReader.ReadInt32();
                HydrateRestore = binaryReader.ReadInt32();
                addStrength = binaryReader.ReadInt32();
                addAgility = binaryReader.ReadInt32();
                addEndurance = binaryReader.ReadInt32();
                addStamina = binaryReader.ReadInt32();
            }
            catch (Exception e)
            {
                MsgBox(e.GetType() + ": " + e.Message, MsgBoxStyle.Critical, "Error");
            }
            binaryReader.Close();
        }
    }

    enum ItemType
    {
        None,
        MeleeWeapon,
        RangedWeapon,
        Currency,
        Food,
        Drink,
        FirstAid,
        Shirt,
        Pants,
        Shoes,
        Other
    }
}
