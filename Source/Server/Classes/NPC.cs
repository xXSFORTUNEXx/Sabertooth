using Microsoft.VisualBasic;
using System;
using System.IO;
using static Microsoft.VisualBasic.Interaction;
using Lidgren.Network;

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
        public int Health { get; set; }
        public int maxHealth { get; set; }
        public int Damage { get; set; }

        //Only needed on live server and client no editors
        public bool isSpawned;
        public bool didMove;

        //Empty NPC
        public NPC() { }

        //Detailed NPC
        public NPC(string name, int x, int y, int direction, int sprite, int step, int owner, int behavior, int spawnTime, int health, int maxhealth, int damage)
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
            Health = health;
            maxHealth = maxhealth;
            Damage = damage;
        }

        //One with location but other default values as well
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

        public void NpcAI(int canMove, int dir, Map movementMap)
        {
            //check if we moved
            didMove = false;

            if (canMove > 80)
            {
                //check directions
                switch (dir)
                {
                    //down
                    case (int)Directions.Down:
                        //check if they are going out of bounds
                        if (Y < 49)
                        {
                            //Check to see if the next tile is blocked
                            if (movementMap.Ground[X, Y + 1].type == (int)TileType.Blocked)
                            {
                                //just change the direction and exit
                                Direction = (int)Directions.Down;
                                didMove = true;
                                return;
                            }
                            //move the npcs over
                            Y += 1;
                            Direction = (int)Directions.Down;
                            didMove = true;
                        }
                        break;

                    case (int)Directions.Left:
                        if (X > 1)
                        {
                            if (movementMap.Ground[X - 1, Y].type == (int)TileType.Blocked)
                            {
                                Direction = (int)Directions.Left;
                                didMove = true;
                                return;
                            }
                            X -= 1;
                            Direction = (int)Directions.Left;
                            didMove = true;
                        }
                        break;

                    case (int)Directions.Right:
                        if (X < 49)
                        {
                            if (movementMap.Ground[X + 1, Y].type == (int)TileType.Blocked)
                            {
                                Direction = (int)Directions.Right;
                                didMove = true;
                                return;
                            }
                            X += 1;
                            Direction = (int)Directions.Right;
                            didMove = true;
                        }
                        break;

                    case (int)Directions.Up:
                        if (Y > 1)
                        {
                            if (movementMap.Ground[X, Y - 1].type == (int)TileType.Blocked)
                            {
                                Direction = (int)Directions.Up;
                                didMove = true;
                                return;
                            }
                            Y -= 1;
                            Direction = (int)Directions.Up;
                            didMove = true;
                        }
                        break;
                }

                if (didMove == true)
                {
                    if (Step == 3) { Step = 0; } else { Step += 1; }
                }
            }
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
            binaryWriter.Write(Health);
            binaryWriter.Write(maxHealth);
            binaryWriter.Write(Damage);
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
                Behavior = binaryReader.ReadInt32();
                SpawnTime = binaryReader.ReadInt32();
                Health = binaryReader.ReadInt32();
                maxHealth = binaryReader.ReadInt32();
                Damage = binaryReader.ReadInt32();
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
