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
        public int AttackSpeed { get; set; }
        public int HealthRestore { get; set; }
        public int HungerRestore { get; set; }
        public int HydrateRestore { get; set; }

        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Endurance { get; set; }
        public int Stamina { get; set; }

        public int Clip { get; set; }
        public int maxClip { get; set; }
        public int ammoType { get; set; }

        public Item() { }

        public Item(ItemType type)
        {
            Type = (int)type;
        }

        public Item(string name, int sprite, int damage, int armor, int type, int attackspeed,
                    int healthRestore, int foodRestore, int drinkRestore, int str, int agi, int end, int sta, int ammotype)
        {
            Name = name;
            Sprite = sprite;
            Damage = damage;
            Armor = armor;
            Type = type;
            AttackSpeed = attackspeed;
            HealthRestore = healthRestore;
            HungerRestore = foodRestore;
            HydrateRestore = drinkRestore;
            Strength = str;
            Agility = agi;
            Endurance = end;
            Stamina = sta;
            ammoType = ammotype;
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
            binaryWriter.Write(AttackSpeed);
            binaryWriter.Write(HealthRestore);
            binaryWriter.Write(HungerRestore);
            binaryWriter.Write(HydrateRestore);
            binaryWriter.Write(Strength);
            binaryWriter.Write(Agility);
            binaryWriter.Write(Endurance);
            binaryWriter.Write(Stamina);
            binaryWriter.Write(Clip);
            binaryWriter.Write(maxClip);
            binaryWriter.Write(ammoType);
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
                AttackSpeed = binaryReader.ReadInt32();
                HealthRestore = binaryReader.ReadInt32();
                HungerRestore = binaryReader.ReadInt32();
                HydrateRestore = binaryReader.ReadInt32();
                Strength = binaryReader.ReadInt32();
                Agility = binaryReader.ReadInt32();
                Endurance = binaryReader.ReadInt32();
                Stamina = binaryReader.ReadInt32();
                Clip = binaryReader.ReadInt32();
                maxClip = binaryReader.ReadInt32();
                ammoType = binaryReader.ReadInt32();
            }
            catch (Exception e)
            {
                MsgBox(e.GetType() + ": " + e.Message, MsgBoxStyle.Critical, "Error");
            }
            binaryReader.Close();
        }
    }

    public enum ItemType
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

    public enum AmmoType
    {
        None,
        Pistol,
        AssaultRifle,
        Rocket,
        Grenade
    }
}
