using static System.Convert;
using static SabertoothServer.Server;
using System.Data.SqlClient;
using static SabertoothServer.Globals;
using static System.IO.File;

namespace SabertoothServer
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

            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            string script = ReadAllText("SQL Data Scripts/Insert_Proj.sql");
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();                    
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = Name;
                    cmd.Parameters.Add(new SqlParameter("@damage", System.Data.DbType.Int32)).Value = Damage;
                    cmd.Parameters.Add(new SqlParameter("@range", System.Data.DbType.Int32)).Value = Range;
                    cmd.Parameters.Add(new SqlParameter("@sprite", System.Data.DbType.Int32)).Value = Sprite;
                    cmd.Parameters.Add(new SqlParameter("@type", System.Data.DbType.Int32)).Value = Type;
                    cmd.Parameters.Add(new SqlParameter("@speed", System.Data.DbType.Int32)).Value = Speed;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void SaveProjectileToDatabase(int projNum)
        {
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            string script = ReadAllText("SQL Data Scripts/Save_Proj.sql");
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("id", System.Data.SqlDbType.Int)).Value = projNum;
                    cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = Name;
                    cmd.Parameters.Add(new SqlParameter("@damage", System.Data.DbType.Int32)).Value = Damage;
                    cmd.Parameters.Add(new SqlParameter("@range", System.Data.DbType.Int32)).Value = Range;
                    cmd.Parameters.Add(new SqlParameter("@sprite", System.Data.DbType.Int32)).Value = Sprite;
                    cmd.Parameters.Add(new SqlParameter("@type", System.Data.DbType.Int32)).Value = Type;
                    cmd.Parameters.Add(new SqlParameter("@speed", System.Data.DbType.Int32)).Value = Speed;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void LoadProjectileFromDatabase(int projNum)
        {
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            string script = ReadAllText("SQL Data Scripts/Load_Proj.sql");
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                using (SqlCommand cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = projNum;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Name = reader[1].ToString();
                            Damage = ToInt32(reader[2]);
                            Range = ToInt32(reader[3]);
                            Sprite = ToInt32(reader[4]);
                            Type = ToInt32(reader[5]);
                            Speed = ToInt32(reader[6]);
                        }
                    }
                }
            }
        }

        public void LoadNameFromDatabase(int projNum)
        {
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                string command;
                command = "SELECT Name FROM Projectiles WHERE ID=@id";
                using (SqlCommand cmd = new SqlCommand(command, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("id", System.Data.SqlDbType.Int)).Value = projNum;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Name = reader[0].ToString();
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
