using Microsoft.VisualBasic;
using System;
using System.IO;
using static Microsoft.VisualBasic.Interaction;

namespace Server.Classes
{
    class NPC
    {
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Direction { get; set; }
        public int Sprite { get; set; }
        public int Step { get; set; }
        public int Owner { get; set; }
        public int Behavior { get; set; }
        public int SpawnTime { get; set; }

        //Only needed on live server and client no editors
        public bool isSpawned;

        public NPC() { }

        public NPC(string name, int x, int y, int direction, int sprite, int step, int owner, int behavior, int spawnTime)
        {
            Name = name;
            X = x;
            Y = y;
            Direction = direction;
            Sprite = sprite;
            Step = step;
            Owner = owner;
            Behavior = behavior;
            SpawnTime = spawnTime;
            isSpawned = false;
        }

        public NPC(int x, int y)
        {
            Name = "Default";
            X = x;
            Y = y;
            Direction = (int)Directions.Down;
            Sprite = 0;
            Step = 0;
            Owner = 0;
            Behavior = (int)BehaviorType.Friendly;
            SpawnTime = 5000;
            isSpawned = false;
        }

        public void SaveNPC(int npcNum)
        {
            FileStream fileStream = File.OpenWrite("NPCS/Npc" + npcNum + ".bin");
            BinaryWriter binaryWriter = new BinaryWriter(fileStream);
            LogWriter.WriteLog("Saving default Npcs...", "Server");
            binaryWriter.Write(Name);
            binaryWriter.Write(X);
            binaryWriter.Write(Y);
            binaryWriter.Write(Direction);
            binaryWriter.Write(Sprite);
            binaryWriter.Write(Step);
            binaryWriter.Write(Owner);
            binaryWriter.Write(Behavior);
            binaryWriter.Write(SpawnTime);
            binaryWriter.Flush();
            binaryWriter.Close();
        }

        public void LoadNPC(int npcNum)
        {
            FileStream fileStream = File.OpenRead("NPCS/Npc" + npcNum + ".bin");
            BinaryReader binaryReader = new BinaryReader(fileStream);
            LogWriter.WriteLog("Loading npc...", "Server");
            try
            {
                Name = binaryReader.ReadString();
                X = binaryReader.ReadInt32();
                Y = binaryReader.ReadInt32();
                Direction = binaryReader.ReadInt32();
                Sprite = binaryReader.ReadInt32();
                Step = binaryReader.ReadInt32();
                Owner = binaryReader.ReadInt32();
                SpawnTime = binaryReader.ReadInt32();
                Behavior = binaryReader.ReadInt32();
            }
            catch (Exception e)
            {
                MsgBox(e.GetType() + ": " + e.Message, MsgBoxStyle.Critical, "Error");
            }
            binaryReader.Close();
        }
    }

    public enum BehaviorType
    {
        Friendly,
        Passive,
        Aggressive
    }
}
