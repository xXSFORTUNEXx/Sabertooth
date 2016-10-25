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
        public int Type { get; set; }

        public Item() { }

        public Item(string name, int sprite, int damage, int type)
        {
            Name = name;
            Sprite = sprite;
            Damage = damage;
            Type = type;
        }

        public void SaveItem(int itemNum)
        {
            FileStream fileStream = File.OpenWrite("Items/Item" + itemNum + ".bin");
            BinaryWriter binaryWriter = new BinaryWriter(fileStream);
            LogWriter.WriteLog("Saving default Items...", "Server");
            binaryWriter.Write(Name);
            binaryWriter.Write(Sprite);
            binaryWriter.Write(Damage);
            binaryWriter.Write(Type);
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
                Type = binaryReader.ReadInt32();
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
        Shoes
    }
}
