using Lidgren.Network;
using System;
using static System.Convert;
using static System.Environment;
using System.Data.SqlClient;
using static SabertoothServer.Server;
using static SabertoothServer.Globals;
using static System.IO.File;

namespace SabertoothServer
{
    [Serializable()]
    public class Npc
    {
        #region Properties
        public int Id { get; set; }
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
        public int ChatNum { get; set; }
        public int Speed { get; set; }
        #endregion

        #region Variables
        public bool IsSpawned;
        public bool DidMove;
        public int Target;
        public double s_LastPoint;
        public int spawnTick;
        public int SpawnX;
        public int SpawnY;
        #endregion

        #region Constructors
        public Npc() { }

        public Npc(string name, int x, int y, int direction, int sprite, int step, int owner, int behavior, int spawnTime, int health, int maxhealth, int damage, int desx, int desy,
                    int exp, int money, int range, int shopnum, int chatnum, int speed)
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
            ChatNum = chatnum;
            Speed = speed;
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
            ChatNum = 0;
        }
        #endregion

        #region Database
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
            ChatNum = 0;
            Speed = 1000;

            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            string script = ReadAllText("SQL Data Scripts/Insert_Npc.sql");
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                using (var cmd = new SqlCommand(script, sql))
                {

                    cmd.Parameters.Add(new SqlParameter("name", System.Data.SqlDbType.Text)).Value = Name;
                    cmd.Parameters.Add(new SqlParameter("x", System.Data.SqlDbType.Int)).Value = X;
                    cmd.Parameters.Add(new SqlParameter("y", System.Data.SqlDbType.Int)).Value = Y;
                    cmd.Parameters.Add(new SqlParameter("direction", System.Data.SqlDbType.Int)).Value = Direction;
                    cmd.Parameters.Add(new SqlParameter("sprite", System.Data.SqlDbType.Int)).Value = Sprite;
                    cmd.Parameters.Add(new SqlParameter("step", System.Data.SqlDbType.Int)).Value = Step;
                    cmd.Parameters.Add(new SqlParameter("owner", System.Data.SqlDbType.Int)).Value = Owner;
                    cmd.Parameters.Add(new SqlParameter("behavior", System.Data.SqlDbType.Int)).Value = Behavior;
                    cmd.Parameters.Add(new SqlParameter("spawntime", System.Data.SqlDbType.Int)).Value = SpawnTime;
                    cmd.Parameters.Add(new SqlParameter("health", System.Data.SqlDbType.Int)).Value = Health;
                    cmd.Parameters.Add(new SqlParameter("maxhealth", System.Data.SqlDbType.Int)).Value = MaxHealth;
                    cmd.Parameters.Add(new SqlParameter("damage", System.Data.SqlDbType.Int)).Value = Damage;
                    cmd.Parameters.Add(new SqlParameter("desx", System.Data.SqlDbType.Int)).Value = DesX;
                    cmd.Parameters.Add(new SqlParameter("desy", System.Data.SqlDbType.Int)).Value = DesY;
                    cmd.Parameters.Add(new SqlParameter("exp", System.Data.SqlDbType.Int)).Value = Exp;
                    cmd.Parameters.Add(new SqlParameter("money", System.Data.SqlDbType.Int)).Value = Money;
                    cmd.Parameters.Add(new SqlParameter("range", System.Data.SqlDbType.Int)).Value = Range;
                    cmd.Parameters.Add(new SqlParameter("shopnum", System.Data.SqlDbType.Int)).Value = ShopNum;
                    cmd.Parameters.Add(new SqlParameter("chatnum", System.Data.SqlDbType.Int)).Value = ChatNum;
                    cmd.Parameters.Add(new SqlParameter("speed", System.Data.SqlDbType.Int)).Value = Speed;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void SaveNpcToDatabase(int npcNum)
        {
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            string script = ReadAllText("SQL Data Scripts/Save_Npc.sql");
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("id", System.Data.SqlDbType.Int)).Value = npcNum;
                    cmd.Parameters.Add(new SqlParameter("name", System.Data.SqlDbType.Text)).Value = Name;
                    cmd.Parameters.Add(new SqlParameter("x", System.Data.SqlDbType.Int)).Value = X;
                    cmd.Parameters.Add(new SqlParameter("y", System.Data.SqlDbType.Int)).Value = Y;
                    cmd.Parameters.Add(new SqlParameter("direction", System.Data.SqlDbType.Int)).Value = Direction;
                    cmd.Parameters.Add(new SqlParameter("sprite", System.Data.SqlDbType.Int)).Value = Sprite;
                    cmd.Parameters.Add(new SqlParameter("step", System.Data.SqlDbType.Int)).Value = Step;
                    cmd.Parameters.Add(new SqlParameter("owner", System.Data.SqlDbType.Int)).Value = Owner;
                    cmd.Parameters.Add(new SqlParameter("behavior", System.Data.SqlDbType.Int)).Value = Behavior;
                    cmd.Parameters.Add(new SqlParameter("spawntime", System.Data.SqlDbType.Int)).Value = SpawnTime;
                    cmd.Parameters.Add(new SqlParameter("health", System.Data.SqlDbType.Int)).Value = Health;
                    cmd.Parameters.Add(new SqlParameter("maxhealth", System.Data.SqlDbType.Int)).Value = MaxHealth;
                    cmd.Parameters.Add(new SqlParameter("damage", System.Data.SqlDbType.Int)).Value = Damage;
                    cmd.Parameters.Add(new SqlParameter("desx", System.Data.SqlDbType.Int)).Value = DesX;
                    cmd.Parameters.Add(new SqlParameter("desy", System.Data.SqlDbType.Int)).Value = DesY;
                    cmd.Parameters.Add(new SqlParameter("exp", System.Data.SqlDbType.Int)).Value = Exp;
                    cmd.Parameters.Add(new SqlParameter("money", System.Data.SqlDbType.Int)).Value = Money;
                    cmd.Parameters.Add(new SqlParameter("range", System.Data.SqlDbType.Int)).Value = Range;
                    cmd.Parameters.Add(new SqlParameter("shopnum", System.Data.SqlDbType.Int)).Value = ShopNum;
                    cmd.Parameters.Add(new SqlParameter("chatnum", System.Data.SqlDbType.Int)).Value = ChatNum;
                    cmd.Parameters.Add(new SqlParameter("speed", System.Data.SqlDbType.Int)).Value = Speed;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void LoadNpcFromDatabase(int npcNum)
        {
                string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
                string script = ReadAllText("SQL Data Scripts/Load_Npc.sql");
                using (var sql = new SqlConnection(connection))
                {
                    sql.Open();
                    using (SqlCommand cmd = new SqlCommand(script, sql))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = npcNum;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Name = reader[1].ToString();
                                X = ToInt32(reader[2]);
                                Y = ToInt32(reader[3]);
                                Direction = ToInt32(reader[4]);
                                Sprite = ToInt32(reader[5]);
                                Step = ToInt32(reader[6]);
                                Owner = ToInt32(reader[7]);
                                Behavior = ToInt32(reader[8]);
                                SpawnTime = ToInt32(reader[9]);
                                Health = ToInt32(reader[10]);
                                MaxHealth = ToInt32(reader[11]);
                                Damage = ToInt32(reader[12]);
                                DesX = ToInt32(reader[13]);
                                DesY = ToInt32(reader[14]);
                                Exp = ToInt32(reader[15]);
                                Money = ToInt32(reader[16]);
                                Range = ToInt32(reader[17]);
                                ShopNum = ToInt32(reader[18]);
                                ChatNum = ToInt32(reader[19]);
                                Speed = ToInt32(reader[20]);
                            }
                        }
                    }
                }
        }

        public void LoadNpcNameFromDatabase(int npcNum)
        {
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                string command;
                command = "SELECT Name FROM Npcs WHERE ID=@id";
                using (SqlCommand cmd = new SqlCommand(command, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("id", System.Data.SqlDbType.Int)).Value = npcNum;
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
        #endregion

        public virtual int DamageNpc(Player s_Player, Map s_Map, Spell spellNum, int attackType, int index)
        {
            return 0;   //Check MapNpcs
        }

        public virtual void AttackPlayer(int index) { } //Check MapNpcs

        public virtual void NpcAI(int s_CanMove, int s_Direction, int mapNum) { }   //Check MapNpcs
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
