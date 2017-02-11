using System.Data.SQLite;
using static System.Convert;

namespace Server.Classes
{
    public class Projectile
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

        public void CreateProjectileInDatabase()
        {
            Name = "Default";
            Damage = 0;
            Range = 0;
            Sprite = 1;
            Type = (int)ProjType.Bullet;
            Speed = 0;

            using (var conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
            {
                using (var cmd = new SQLiteCommand(conn))
                {
                    conn.Open();
                    string command;
                    command = "INSERT INTO `PROJECTILES` (`NAME`,`DAMAGE`,`RANGE`,`SPRITE`,`TYPE`,`SPEED`) VALUES ";
                    command = command + "(@name,@damage,@range,@sprite,@type,@speed);";
                    cmd.CommandText = command;
                    cmd.Parameters.Add("@name", System.Data.DbType.String).Value = Name;
                    cmd.Parameters.Add("@damage", System.Data.DbType.Int32).Value = Damage;
                    cmd.Parameters.Add("@range", System.Data.DbType.Int32).Value = Range;
                    cmd.Parameters.Add("@sprite", System.Data.DbType.Int32).Value = Sprite;
                    cmd.Parameters.Add("@type", System.Data.DbType.Int32).Value = Type;
                    cmd.Parameters.Add("@speed", System.Data.DbType.Int32).Value = Speed;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void SaveProjectileToDatabase(int projNum)
        {
            using (var conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
            {
                using (var cmd = new SQLiteCommand(conn))
                {
                    conn.Open();
                    string command;
                    command = "UPDATE PROJECTILES SET ";
                    command = command + "NAME = @name, DAMAGE = @damage, RANGE = @range, SPRITE = @sprite, TYPE = @type, SPEED = @speed WHERE rowid = " + projNum + ";";
                    cmd.CommandText = command;
                    cmd.Parameters.Add("@name", System.Data.DbType.String).Value = Name;
                    cmd.Parameters.Add("@damage", System.Data.DbType.Int32).Value = Damage;
                    cmd.Parameters.Add("@range", System.Data.DbType.Int32).Value = Range;
                    cmd.Parameters.Add("@sprite", System.Data.DbType.Int32).Value = Sprite;
                    cmd.Parameters.Add("@type", System.Data.DbType.Int32).Value = Type;
                    cmd.Parameters.Add("@speed", System.Data.DbType.Int32).Value = Speed;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void LoadProjectileFromDatabase(int projNum)
        {
            using (var conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
            {
                using (var cmd = new SQLiteCommand(conn))
                {
                    conn.Open();
                    cmd.CommandText = "SELECT * FROM `PROJECTILES` WHERE rowid = " + projNum;
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
            using (var conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
            {
                using (var cmd = new SQLiteCommand(conn))
                {
                    conn.Open();
                    cmd.CommandText = "SELECT * FROM `PROJECTILES` WHERE rowid = " + projNum;
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

    public enum ProjType
    {
        Bullet,
        Thrown,
        Explosive
    }
}
