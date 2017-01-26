using Microsoft.VisualBasic;
using System;
using System.IO;
using System.Data.SQLite;
using static System.Convert;

namespace Editor.Classes
{
    class Projectile
    {
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

            using (SQLiteConnection conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
            {
                conn.Open();
                string sql;
                sql = "INSERT INTO `PROJECTILES`";
                sql = sql + "(`NAME`,`DAMAGE`,`RANGE`,`SPRITE`,`TYPE`,`SPEED`)";
                sql = sql + " VALUES ";
                sql = sql + "('" + Name + "','" + Damage + "','" + Range + "','" + Sprite + "','" + Type + "','" + Speed + "');";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void SaveProjectileToDatabase(int projNum)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
            {
                conn.Open();
                string sql;

                sql = "UPDATE PROJECTILES SET ";
                sql = sql + "NAME = '" + Name + "', DAMAGE = '" + Damage + "', RANGE = '" + Range + "', SPRITE = '" + Sprite + "', TYPE = '" + Type + "', SPEED = '" + Speed + "' ";
                sql = sql + "WHERE rownid = '" + projNum + "';";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void LoadProjectileFromDatabase(int projNum)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
            {
                conn.Open();
                string sql;
                sql = "SELECT * FROM `PROJECTILES` WHERE rowid = " + projNum;

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            Name = read["NAME"].ToString();
                            Damage = ToInt32(read["DAMAGE"].ToString());
                            Range = ToInt32(read["RANGE"].ToString());
                            Sprite = ToInt32(read["SPRITE"].ToString());
                            Type = ToInt32(read["TYPE"].ToString());
                            Speed = ToInt32(read["SPEED"].ToString());
                        }
                    }
                }
            }
        }

        public void LoadNameFromDatabase(int projNum)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
            {
                conn.Open();
                string sql;
                sql = "SELECT * FROM `PROJECTILES` WHERE rowid = " + projNum;

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
    }

    enum ProjType
    {
        Bullet,
        Thrown,
        Explosive
    }
}
