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
        Sprite e_Sprite = new Sprite();
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Range { get; set; }
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
        public int ShopNum { get; set; }

        public Npc() { }

        public Npc(string name, int x, int y, int direction, int sprite, int step, int owner, int behavior, int spawnTime, int health, int maxhealth, int damage, int desx, int desy, int exp, int money, int range, int shopnum)
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
            Range = range;
            ShopNum = shopnum;
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
            Range = 0;
            ShopNum = 0;
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
            Range = 0;
            ShopNum = 0;

            using (SQLiteConnection conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
            {
                conn.Open();
                string sql;
                sql = "INSERT INTO NPCS";
                sql = sql + "(`NAME`,`X`,`Y`,`DIRECTION`,`SPRITE`,`STEP`,`OWNER`,`BEHAVIOR`,`SPAWNTIME`,`HEALTH`,`MAXHEALTH`,`DAMAGE`,`DESX`,`DESY`,`EXP`,`MONEY`,`RANGE`,`SHOPNUM`)";
                sql = sql + " VALUES ";
                sql = sql + "('" + Name + "','" + X + "','" + Y + "','" + Direction + "','" + Sprite + "','" + Step + "','" + Owner + "','" + Behavior + "',";
                sql = sql + "'" + SpawnTime + "','" + Health + "','" + MaxHealth + "','" + Damage + "','" + DesX + "','" + DesY + "','" + Exp + "','" + Money + "','" + Range + "','" + ShopNum + "');";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void SaveNpcToDatabase(int npcNum)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
            {
                conn.Open();
                string sql;
                sql = "UPDATE NPCS SET ";
                sql = sql + "NAME = '" + Name + "', X = '" + X + "', Y = '" + Y + "', DIRECTION = '" + Direction + "', SPRITE = '" + Sprite + "', STEP = '" + Step + "', ";
                sql = sql + "OWNER = '" + Owner + "', BEHAVIOR = '" + Behavior + "', SPAWNTIME = '" + SpawnTime + "', HEALTH = '" + Health + "', MAXHEALTH = '" + MaxHealth + "', DAMAGE = '" + Damage + "', DESX = '" + DesX + "', DESY = '" + DesY + "', ";
                sql = sql + "EXP = '" + Exp + "', MONEY = '" + Money + "', RANGE = '" + Range + "', SHOPNUM = '" + ShopNum + "' ";
                sql = sql + "WHERE rowid = '" + npcNum + "';";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void LoadNpcFromDatabase(int npcNum)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
            {
                conn.Open();
                string sql;
                sql = "SELECT * FROM NPCS WHERE rowid = " + npcNum;

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            Name = read["NAME"].ToString();
                            X = ToInt32(read["X"].ToString());
                            Y = ToInt32(read["Y"].ToString());
                            Direction = ToInt32(read["DIRECTION"].ToString());
                            Sprite = ToInt32(read["SPRITE"].ToString());
                            Step = ToInt32(read["STEP"].ToString());
                            Owner = ToInt32(read["OWNER"].ToString());
                            Behavior = ToInt32(read["BEHAVIOR"].ToString());
                            SpawnTime = ToInt32(read["SPAWNTIME"].ToString());
                            Health = ToInt32(read["HEALTH"].ToString());
                            MaxHealth = ToInt32(read["MAXHEALTH"].ToString());
                            Damage = ToInt32(read["DAMAGE"].ToString());
                            DesX = ToInt32(read["DESX"].ToString());
                            DesY = ToInt32(read["DESY"].ToString());
                            Exp = ToInt32(read["EXP"].ToString());
                            Money = ToInt32(read["MONEY"].ToString());
                            Range = ToInt32(read["RANGE"].ToString());
                            ShopNum = ToInt32(read["SHOPNUM"].ToString());
                        }
                    }
                }
            }
        }

        public void LoadNpcNameFromDatabase(int npcNum)
        {

            using (SQLiteConnection conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
            {
                conn.Open();
                string sql;
                sql = "SELECT * FROM NPCS WHERE rowid = " + npcNum;

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            Name = read["NAME"].ToString();
                        }
                    }
                }
            }
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
        ToLocation,
        ShopOwner
    }
}
