using Microsoft.VisualBasic;
using System;
using System.IO;
using System.Data.SQLite;
using static System.Convert;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace Editor.Classes
{
    class Npc
    {
        SQLiteConnection e_Database;
        Sprite e_Sprite = new Sprite();
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
        public int MaxHealth { get; set; }
        public int Damage { get; set; }
        public int DesX { get; set; }
        public int DesY { get; set; }
        public int Exp { get; set; }
        public int Money { get; set; }

        public Npc() { }

        public Npc(string name, int x, int y, int direction, int sprite, int step, int owner, int behavior, int spawnTime, int health, int maxhealth, int damage, int desx, int desy, int exp, int money)
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
            Health = health;
            MaxHealth = maxhealth;
            Damage = damage;
            DesX = desx;
            DesY = desy;
            Exp = exp;
            Money = money;
        }

        public Npc(int x, int y)
        {
            Name = "Default";
            X = x;
            Y = y;
            Direction = 0;
            Sprite = 0;
            Step = 0;
            Owner = 0;
            Behavior = (int)BehaviorType.Friendly;
            SpawnTime = 5000;
            Health = 100;
            MaxHealth = 100;
            Damage = 10;
            DesX = 0;
            DesY = 0;
            Exp = 100;
            Money = 0;
        }

        public void CreateNpcInDatabase()
        {
            Name = "Default";
            X = 0;
            Y = 0;
            Direction = 0;
            Sprite = 1;
            Step = 0;
            Owner = 0;
            Behavior = 0;
            SpawnTime = 0;
            Health = 0;
            MaxHealth = 0;
            Damage = 0;
            DesX = 0;
            DesY = 0;
            Exp = 0;
            Money = 0;

            e_Database = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;");
            e_Database.Open();
            string sql;
            SQLiteCommand sql_Command;

            sql = "INSERT INTO NPCS";
            sql = sql + "(`NAME`,`X`,`Y`,`DIRECTION`,`SPRITE`,`STEP`,`OWNER`,`BEHAVIOR`,`SPAWNTIME`,`HEALTH`,`MAXHEALTH`,`DAMAGE`,`DESX`,`DESY`,`EXP`,`MONEY`)";
            sql = sql + " VALUES ";
            sql = sql + "('" + Name + "','" + X + "','" + Y + "','" + Direction + "','" + Sprite + "','" + Step + "','" + Owner + "','" + Behavior + "',";
            sql = sql + "'" + SpawnTime + "','" + Health + "','" + MaxHealth + "','" + Damage + "','" + DesX + "','" + DesY + "','" + Exp + "','" + Money + "');";
            sql_Command = new SQLiteCommand(sql, e_Database);
            sql_Command.ExecuteNonQuery();
            e_Database.Close();
        }

        public void SaveNpcToDatabase(int npcNum)
        {
            e_Database = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;");
            e_Database.Open();
            string sql;
            SQLiteCommand sql_Command;

            sql = "UPDATE NPCS SET ";
            sql = sql + "NAME = '" + Name + "', X = '" + X + "', Y = '" + Y + "', DIRECTION = '" + Direction + "', SPRITE = '" + Sprite + "', STEP = '" + Step + "', ";
            sql = sql + "OWNER = '" + Owner + "', BEHAVIOR = '" + Behavior + "', SPAWNTIME = '" + SpawnTime + "', HEALTH = '" + Health + "', MAXHEALTH = '" + MaxHealth + "', DAMAGE = '" + Damage + "', DESX = '" + DesX + "', DESY = '" + DesY + "', ";
            sql = sql + "EXP = '" + Exp + "', MONEY = '" + Money + "' ";
            sql = sql + "WHERE rowid = '" + npcNum + "';";
            sql_Command = new SQLiteCommand(sql, e_Database);
            sql_Command.ExecuteNonQuery();
            e_Database.Close();
        }

        public void LoadNpcFromDatabase(int npcNum)
        {
            e_Database = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;");
            e_Database.Open();
            string sql;

            sql = "SELECT * FROM NPCS WHERE rowid = " + npcNum;

            SQLiteCommand sql_Command = new SQLiteCommand(sql, e_Database);
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
                MaxHealth = ToInt32(sql_Reader["MAXHEALTH"].ToString());
                Damage = ToInt32(sql_Reader["DAMAGE"].ToString());
                DesX = ToInt32(sql_Reader["DESX"].ToString());
                DesY = ToInt32(sql_Reader["DESY"].ToString());
                Exp = ToInt32(sql_Reader["EXP"].ToString());
                Money = ToInt32(sql_Reader["MONEY"].ToString());
            }
            e_Database.Close();
        }

        public void LoadNpcNameFromDatabase(int npcNum)
        {

            e_Database = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;");
            e_Database.Open();
            string sql;

            sql = "SELECT * FROM NPCS WHERE rowid = " + npcNum;

            SQLiteCommand sql_Command = new SQLiteCommand(sql, e_Database);
            SQLiteDataReader sql_Reader = sql_Command.ExecuteReader();

            while (sql_Reader.Read())
            {
                Name = sql_Reader["NAME"].ToString();
            }
            e_Database.Close();
        }

        public void DrawNpc(RenderWindow e_Window, Texture e_Texture, int x, int y)
        {
            e_Sprite.Texture = e_Texture;
            e_Sprite.TextureRect = new IntRect(0, 0, 32, 48);
            e_Sprite.Position = new Vector2f(x * 32, (y * 32) - 16);

            e_Window.Draw(e_Sprite);
        }
    }

    public enum BehaviorType
    {
        Friendly,
        Passive,
        Aggressive,
        ToLocation
    }
}
