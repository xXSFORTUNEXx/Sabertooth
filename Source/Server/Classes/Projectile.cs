using Microsoft.VisualBasic;
using System;
using System.IO;
using static Microsoft.VisualBasic.Interaction;
using Lidgren.Network;

namespace Server.Classes
{
    class Projectile
    {
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Direction { get; set; }
        public int Damage { get; set; }
        public int Range { get; set; }
        public int Sprite { get; set; }
        public int Owner { get; set; }
        public int Type { get; set; }
        public int Speed { get; set; }

        public Projectile() { }

        public Projectile(string name, int x, int y)
        {
            Name = name;
            X = x;
            Y = y;
        }

        public Projectile(string name, int damage, int range, int sprite, int owner, int type, int speed)
        {
            Name = name;
            Damage = damage;
            Range = range;
            Sprite = sprite;
            Owner = owner;
            Type = type;
            Speed = speed;
        }

        public void LoadProjectile(int projNum)
        {
            FileStream fileStream = File.OpenRead("Items/Item" + projNum + ".bin");
            BinaryReader binaryReader = new BinaryReader(fileStream);
            LogWriter.WriteLog("Loading projectile...", "Server");
            try
            {
                Name = binaryReader.ReadString();
                Damage = binaryReader.ReadInt32();
                Range = binaryReader.ReadInt32();
                Sprite = binaryReader.ReadInt32();
                Type = binaryReader.ReadInt32();
                Speed = binaryReader.ReadInt32();
            }
            catch (Exception e)
            {
                MsgBox(e.GetType() + ": " + e.Message, MsgBoxStyle.Critical, "Error");
            }
            binaryReader.Close();
        }

        public void SaveProjectile(int projNum)
        {
            FileStream fileStream = File.OpenWrite("Projectiles/Projectile" + projNum + ".bin");
            BinaryWriter binaryWriter = new BinaryWriter(fileStream);
            LogWriter.WriteLog("Saving default Projectiles...", "Server");
            binaryWriter.Write(Name);
            binaryWriter.Write(Damage);
            binaryWriter.Write(Range);
            binaryWriter.Write(Sprite);
            binaryWriter.Write(Type);
            binaryWriter.Write(Speed);
            binaryWriter.Flush();
            binaryWriter.Close();
        }
    }

    enum ProjType
    {
        Bullet,
        Thrown,
        Explosive
    }
}
