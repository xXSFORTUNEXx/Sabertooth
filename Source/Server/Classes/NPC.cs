using Microsoft.VisualBasic;
using System;
using System.IO;
using static Microsoft.VisualBasic.Interaction;
using Lidgren.Network;
using System.Data.SQLite;
using static System.Convert;

namespace Server.Classes
{
    class Npc
    {
        SQLiteConnection s_Database;
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
        public Npc() { }

        //Detailed NPC
        public Npc(string name, int x, int y, int direction, int sprite, int step, int owner, int behavior, int spawnTime, int health, int maxhealth, int damage)
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
        public Npc(int x, int y)
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
            Health = 100;
            maxHealth = 100;
            Damage = 10;
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
                            if (movementMap.Ground[X, Y + 1].Type == (int)TileType.Blocked)
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
                            if (movementMap.Ground[X - 1, Y].Type == (int)TileType.Blocked)
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
                            if (movementMap.Ground[X + 1, Y].Type == (int)TileType.Blocked)
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
                            if (movementMap.Ground[X, Y - 1].Type == (int)TileType.Blocked)
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

        public void CreateNpcInDatabase()
        {
            s_Database = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;");
            s_Database.Open();
            string sql;
            SQLiteCommand sql_Command;

            sql = "INSERT INTO `NPCS`";
            sql = sql + "(`NAME`,`X`,`Y`,`DIRECTION`,`SPRITE`,`STEP`,`OWNER`,`BEHAVIOR`,`SPAWNTIME`,`HEALTH`,`MAXHEALTH`,`DAMAGE`)";
            sql = sql + " VALUES ";
            sql = sql + "('" + Name + "','" + X + "','" + Y + "','" + Direction + "','" + Sprite + "','" + Step + "','" + Owner + "','" + Behavior + "',";
            sql = sql + "'" + SpawnTime + "','" + Health + "','" + maxHealth + "','" + Damage + "');";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Command.ExecuteNonQuery();
            s_Database.Close();
        }

        public void SaveNpcToDatabase(int npcNum)
        {
            s_Database = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;");
            s_Database.Open();
            string sql;
            SQLiteCommand sql_Command;

            sql = "UPDATE NPCS SET ";
            sql = sql + "NAME = '" + Name + "', X = '" + X + "', Y = '" + Y + "', DIRECTION = '" + Direction + "', SPRITE = '" + Sprite + "', STEP = '" + Step + "', ";
            sql = sql + "OWNER = '" + Owner + "', BEHAVIOR = '" + Behavior + "', SPAWNTIME = '" + SpawnTime + "', HEALTH = '" + Health + "', MAXHEALTH = '" + maxHealth + "', DAMAGE = '" + Damage + "' ";
            sql = sql + "WHERE rowid = '" + npcNum + "';"; 
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Command.ExecuteNonQuery();
            s_Database.Close();
        }

        public void LoadNpcFromDatabase(int npcNum)
        {
            s_Database = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;");
            s_Database.Open();
            string sql;

            sql = "SELECT * FROM `NPCS` WHERE rowid = " + (npcNum + 1);

            SQLiteCommand sql_Command = new SQLiteCommand(sql, s_Database);
            SQLiteDataReader sql_Reader = sql_Command.ExecuteReader();

            while (sql_Reader.Read())
            {
                Name = sql_Reader["NAME"].ToString();
                X = ToInt32(sql_Reader["X"].ToString());
                Y = ToInt32(sql_Reader["Y"].ToString());
                Direction = ToInt32(sql_Reader["DIRECTION"].ToString());
                Sprite = ToInt32(sql_Reader["SPRITE"].ToString());
                Step = ToInt32(sql_Reader["STEP"].ToString());
                Owner = ToInt32(sql_Reader["OWNER"].ToString());
                Behavior = ToInt32(sql_Reader["BEHAVIOR"].ToString());
                SpawnTime = ToInt32(sql_Reader["SPAWNTIME"].ToString());
                Health = ToInt32(sql_Reader["HEALTH"].ToString());
                maxHealth = ToInt32(sql_Reader["MAXHEALTH"].ToString());
                Damage = ToInt32(sql_Reader["DAMAGE"].ToString());
            }
            s_Database.Close();
        }
    }

    public enum BehaviorType
    {
        Friendly,
        Passive,
        Aggressive
    }
}
