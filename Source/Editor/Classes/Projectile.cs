using Microsoft.VisualBasic;
using System;
using System.IO;
using System.Data.SQLite;
using static System.Convert;

namespace Editor.Classes
{
    class Projectile
    {
        SQLiteConnection e_Database;
        public string Name { get; set; }
        public int Damage { get; set; }
        public int Range { get; set; }
        public int Sprite { get; set; }
        public int Type { get; set; }
        public int Speed { get; set; }
        const int Max_ProjPics = 2;

        public Projectile() { }

        public void CreateProjectileInDatabase()
        {

            Name = "Default";
            Sprite = 1;
            Damage = 0;
            Range = 0;
            Type = 0;
            Speed = 0;

            e_Database = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;");
            e_Database.Open();
            string sql;
            SQLiteCommand sql_Command;
            sql = "INSERT INTO `PROJECTILES`";
            sql = sql + "(`NAME`,`DAMAGE`,`RANGE`,`SPRITE`,`TYPE`,`SPEED`)";
            sql = sql + " VALUES ";
            sql = sql + "('" + Name + "','" + Damage + "','" + Range + "','" + Sprite + "','" + Type + "','" + Speed + "');";
            sql_Command = new SQLiteCommand(sql, e_Database);
            sql_Command.ExecuteNonQuery();
            e_Database.Close();
        }

        public void SaveProjectileToDatabase(int projNum)
        {
            e_Database = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;");
            e_Database.Open();
            string sql;
            SQLiteCommand sql_Command;

            sql = "UPDATE PROJECTILES SET ";
            sql = sql + "NAME = '" + Name + "', DAMAGE = '" + Damage + "', RANGE = '" + Range + "', SPRITE = '" + Sprite + "', TYPE = '" + Type + "', SPEED = '" + Speed + "' ";
            sql = sql + "WHERE rowid = '" + projNum + "';";
            sql_Command = new SQLiteCommand(sql, e_Database);
            sql_Command.ExecuteNonQuery();
            e_Database.Close();
        }

        public void LoadProjectileFromDatabase(int projNum)
        {
            e_Database = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;");
            e_Database.Open();
            string sql;

            sql = "SELECT * FROM `PROJECTILES` WHERE rowid = " + (projNum);

            SQLiteCommand sql_Command = new SQLiteCommand(sql, e_Database);
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
            e_Database.Close();
        }

        public void LoadNameFromDatabase(int projNum)
        {
            e_Database = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;");
            e_Database.Open();
            string sql;

            sql = "SELECT * FROM `PROJECTILES` WHERE rowid = " + (projNum);

            SQLiteCommand sql_Command = new SQLiteCommand(sql, e_Database);
            SQLiteDataReader sql_Reader = sql_Command.ExecuteReader();

            while (sql_Reader.Read())
            {
                Name = sql_Reader["NAME"].ToString();
            }
            e_Database.Close();
        }
    }

    enum ProjType
    {
        Bullet,
        Thrown,
        Explosive
    }
}
