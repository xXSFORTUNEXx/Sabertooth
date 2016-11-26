using Microsoft.VisualBasic;
using System;
using System.IO;
using static Microsoft.VisualBasic.Interaction;
using Lidgren.Network;
using System.Data.SQLite;
using static System.Convert;

namespace Server.Classes
{
    class Projectile
    {
        SQLiteConnection s_Database;
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

        public void CreateProjectileInDatabase()
        {
            s_Database = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;");
            s_Database.Open();
            string sql;
            SQLiteCommand sql_Command;
            sql = "INSERT INTO `PROJECTILES`";
            sql = sql + "(`NAME`,`DAMAGE`,`RANGE`,`SPRITE`,`TYPE`,`SPEED`)";
            sql = sql + " VALUES ";
            sql = sql + "('" + Name + "','" + Damage + "','" + Range + "','" + Sprite + "','" + Type + "','" + Speed + "');";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Command.ExecuteNonQuery();
            s_Database.Close();
        }

        public void SaveProjectileToDatabase(int projNum)
        {
            s_Database = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;");
            s_Database.Open();
            string sql;
            SQLiteCommand sql_Command;

            sql = "UPDATE PROJECTILES SET ";
            sql = sql + "NAME = '" + Name + "', DAMAGE = '" + Damage + "', RANGE = '" + Range + "', SPRITE = '" + Sprite + "', TYPE = '" + Type + "', SPEED = '" + Speed + "' ";
            sql = sql + "WHERE rownid = '" + projNum + "';";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Command.ExecuteNonQuery();
            s_Database.Close();
        }

        public void LoadProjectileFromDatabase(int projNum)
        {
            s_Database = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;");
            s_Database.Open();
            string sql;

            sql = "SELECT * FROM `PROJECTILES` WHERE rowid = " + (projNum + 1);

            SQLiteCommand sql_Command = new SQLiteCommand(sql, s_Database);
            SQLiteDataReader sql_Reader = sql_Command.ExecuteReader();

            while (sql_Reader.Read())
            {
                Name = sql_Reader["NAME"].ToString();
                Damage = ToInt32(sql_Reader["DAMAGE"].ToString());
                Range = ToInt32(sql_Reader["RANGE"].ToString());
                Sprite = ToInt32(sql_Reader["SPRITE"].ToString());
                Type = ToInt32(sql_Reader["TYPE"].ToString());
                Speed = ToInt32(sql_Reader["SPEED"].ToString());
            }
            s_Database.Close();
        }
    }

    enum ProjType
    {
        Bullet,
        Thrown,
        Explosive
    }
}
