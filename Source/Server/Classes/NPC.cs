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
            string script = ReadAllText("SQL Data Scripts/INSERT NPC.sql");
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
            string script = ReadAllText("SQL Data Scripts/SAVE NPC.sql");
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
                string script = ReadAllText("SQL Data Scripts/LOAD NPC.sql");
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
                command = "SELECT NAME FROM NPCS WHERE ID=@id";
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

        #region Voids
        public bool DamageNpc(Player s_Player, Map s_Map, int damage)
        {
            Health -= damage;

            if (Health <= 0)
            {
                IsSpawned = false;
                Health = MaxHealth;
                spawnTick = TickCount;
                s_Player.Experience += Exp;
                s_Player.Money += Money;
                s_Player.Points += 100;
                s_Player.Kills += 1;
                s_Player.CheckPlayerLevelUp();
                //s_Player.SavePlayerToDatabase();
                if (SpawnX > 0 && SpawnY > 0)
                {
                    s_Map.Ground[SpawnX, SpawnY].CurrentSpawn -= 1;
                }
                return true;
            }
            return false;
        }

        public void AttackPlayer(NetServer s_Server, Player[] s_Player, int index)
        {
            s_Player[index].Health -= Damage;

            if (s_Player[index].Health <= 0)
            {
                s_Player[index].Health = s_Player[index].MaxHealth;
                s_Player[index].X = 0;
                s_Player[index].Y = 0;
                int day = s_Player[index].LifeDay;
                int hour = s_Player[index].LifeHour;
                int minute = s_Player[index].LifeMinute;
                int second = s_Player[index].LifeSecond;
                if (NewLongestLife(s_Player[index]))
                {
                    s_Player[index].LongestLifeDay = day;
                    s_Player[index].LongestLifeHour = hour;
                    s_Player[index].LongestLifeMinute = minute;
                    s_Player[index].LongestLifeSecond = second;
                }
                s_Player[index].LifeDay = 0;
                s_Player[index].LifeHour = 0;
                s_Player[index].LifeMinute = 0;
                s_Player[index].LifeSecond = 0;
                HandleData.SendPlayers();
                string deathMsg = s_Player[index].Name + " has been killed by " + Name + ".";
                HandleData.SendServerMessageToAll(deathMsg);
            }
            else
            {
                HandleData.SendUpdatePlayerStats(index);
            }
        }

        bool NewLongestLife(Player s_Player)
        {
            if (s_Player.LifeDay > s_Player.LongestLifeDay)
            {
                return true;
            }

            if (s_Player.LifeHour > s_Player.LongestLifeHour)
            {
                return true;
            }

            if (s_Player.LifeMinute > s_Player.LongestLifeMinute)
            {
                return true;
            }
            return false;
        }

        public void NpcAI(int s_CanMove, int s_Direction, Map s_Map, Player[] s_Player, NetServer s_Server)
        {
            DidMove = false;

            switch (Behavior)
            {
                case (int)BehaviorType.Friendly:

                    if (s_CanMove > 80)
                    {
                        switch (s_Direction)
                        {
                            case (int)Directions.Down:
                                if (Y < 49)
                                {
                                    if (s_Map.Ground[X, Y + 1].Type == (int)TileType.Blocked || s_Map.Ground[X, Y + 1].Type == (int)TileType.NpcAvoid)
                                    {
                                        Direction = (int)Directions.Down;
                                        DidMove = true;
                                        return;
                                    }
                                    for (int i = 0; i < 10; i++)
                                    {
                                        if (s_Map.m_MapNpc[i].IsSpawned)
                                        {
                                            if ((Y + 1) == s_Map.m_MapNpc[i].Y && X == s_Map.m_MapNpc[i].X)
                                            {
                                                Direction = (int)Directions.Down;
                                                DidMove = true;
                                                return;
                                            }
                                        }
                                    }
                                    for (int i = 0; i < 20; i++)
                                    {
                                        if (s_Map.r_MapNpc[i].IsSpawned)
                                        {
                                            if ((Y + 1) == s_Map.r_MapNpc[i].Y && X == s_Map.r_MapNpc[i].X)
                                            {
                                                Direction = (int)Directions.Down;
                                                DidMove = true;
                                                return;
                                            }
                                        }
                                    }
                                    for (int p = 0; p < 5; p++)
                                    {
                                        if (s_Player[p].Connection != null)
                                        {
                                            if ((Y + 1) == (s_Player[p].Y + 9) && X == (s_Player[p].X + 12))
                                            {
                                                Direction = (int)Directions.Down;
                                                DidMove = true;
                                                return;
                                            }
                                        }
                                    }
                                    Y += 1;
                                    Direction = (int)Directions.Down;
                                    DidMove = true;
                                }
                                break;

                            case (int)Directions.Left:
                                if (X > 1)
                                {
                                    if (s_Map.Ground[X - 1, Y].Type == (int)TileType.Blocked || s_Map.Ground[X - 1, Y].Type == (int)TileType.NpcAvoid)
                                    {
                                        Direction = (int)Directions.Left;
                                        DidMove = true;
                                        return;
                                    }
                                    for (int i = 0; i < 10; i++)
                                    {
                                        if (s_Map.m_MapNpc[i].IsSpawned)
                                        {
                                            if ((X - 1) == s_Map.m_MapNpc[i].X && Y == s_Map.m_MapNpc[i].Y)
                                            {
                                                Direction = (int)Directions.Left;
                                                DidMove = true;
                                                return;
                                            }
                                        }
                                    }
                                    for (int i = 0; i < 20; i++)
                                    {
                                        if (s_Map.r_MapNpc[i].IsSpawned)
                                        {
                                            if ((X - 1) == s_Map.r_MapNpc[i].X && Y == s_Map.r_MapNpc[i].Y)
                                            {
                                                Direction = (int)Directions.Left;
                                                DidMove = true;
                                                return;
                                            }
                                        }
                                    }
                                    for (int p = 0; p < 5; p++)
                                    {
                                        if (s_Player[p].Connection != null)
                                        {
                                            if ((X - 1) == (s_Player[p].X + 12) && Y == (s_Player[p].Y + 9))
                                            {
                                                Direction = (int)Directions.Left;
                                                DidMove = true;
                                                return;
                                            }
                                        }
                                    }
                                    X -= 1;
                                    Direction = (int)Directions.Left;
                                    DidMove = true;
                                }
                                break;

                            case (int)Directions.Right:
                                if (X < 49)
                                {
                                    if (s_Map.Ground[X + 1, Y].Type == (int)TileType.Blocked || s_Map.Ground[X + 1, Y].Type == (int)TileType.NpcAvoid)
                                    {
                                        Direction = (int)Directions.Right;
                                        DidMove = true;
                                        return;
                                    }
                                    for (int i = 0; i < 10; i++)
                                    {
                                        if (s_Map.m_MapNpc[i].IsSpawned)
                                        {
                                            if ((X + 1) == s_Map.m_MapNpc[i].X && Y == s_Map.m_MapNpc[i].Y)
                                            {
                                                Direction = (int)Directions.Right;
                                                DidMove = true;
                                                return;
                                            }
                                        }
                                    }
                                    for (int i = 0; i < 20; i++)
                                    {
                                        if (s_Map.r_MapNpc[i].IsSpawned)
                                        {
                                            if ((X + 1) == s_Map.r_MapNpc[i].X && Y == s_Map.r_MapNpc[i].Y)
                                            {
                                                Direction = (int)Directions.Right;
                                                DidMove = true;
                                                return;
                                            }
                                        }
                                    }
                                    for (int p = 0; p < 5; p++)
                                    {
                                        if (s_Player[p].Connection != null)
                                        {
                                            if ((X + 1) == (s_Player[p].X + 12) && Y == (s_Player[p].Y + 9))
                                            {
                                                Direction = (int)Directions.Right;
                                                DidMove = true;
                                                return;
                                            }
                                        }
                                    }
                                    X += 1;
                                    Direction = (int)Directions.Right;
                                    DidMove = true;
                                }
                                break;

                            case (int)Directions.Up:
                                if (Y > 1)
                                {
                                    if (s_Map.Ground[X, Y - 1].Type == (int)TileType.Blocked || s_Map.Ground[X, Y - 1].Type == (int)TileType.NpcAvoid)
                                    {
                                        Direction = (int)Directions.Up;
                                        DidMove = true;
                                        return;
                                    }
                                    for (int i = 0; i < 10; i++)
                                    {
                                        if (s_Map.m_MapNpc[i].IsSpawned)
                                        {
                                            if ((Y - 1) == s_Map.m_MapNpc[i].Y && X == s_Map.m_MapNpc[i].X)
                                            {
                                                Direction = (int)Directions.Up;
                                                DidMove = true;
                                                return;
                                            }
                                        }
                                    }
                                    for (int i = 0; i < 20; i++)
                                    {
                                        if (s_Map.r_MapNpc[i].IsSpawned)
                                        {
                                            if ((Y - 1) == s_Map.r_MapNpc[i].Y && X == s_Map.r_MapNpc[i].X)
                                            {
                                                Direction = (int)Directions.Up;
                                                DidMove = true;
                                                return;
                                            }
                                        }
                                    }
                                    for (int p = 0; p < 5; p++)
                                    {
                                        if (s_Player[p].Connection != null)
                                        {
                                            if ((Y - 1) == (s_Player[p].Y  + 9) && X == (s_Player[p].X + 12))
                                            {
                                                Direction = (int)Directions.Up;
                                                DidMove = true;
                                                return;
                                            }
                                        }
                                    }
                                    Y -= 1;
                                    Direction = (int)Directions.Up;
                                    DidMove = true;
                                }
                                break;
                        }

                        if (DidMove == true)
                        {
                            if (Step == 3) { Step = 0; } else { Step += 1; }
                        }
                    }
                break;

                case (int)BehaviorType.Passive:
                        //Not really sure we have to do anything here
                    break;

                case (int)BehaviorType.Aggressive:

                    for (int p = 0; p < MAX_PLAYERS; p++)
                    {
                        if (s_Player[p].Connection != null && s_Player[p].Name != null)
                        {
                            int s_PlayerX = s_Player[p].X + OFFSET_X;
                            int s_PlayerY = s_Player[p].Y + OFFSET_Y;
                            double s_DisX = X - s_PlayerX;
                            double s_DisY = Y - s_PlayerY;
                            double s_Final = s_DisX * s_DisX + s_DisY * s_DisY;
                            double s_DisPoint = Math.Sqrt(s_Final);

                            if (s_DisPoint < s_LastPoint)
                            {
                                Target = p;                                
                            }

                            s_LastPoint = s_DisPoint;
                        }
                    }

                    if ((X + Range) < (s_Player[Target].X + OFFSET_X) || (X - Range) > (s_Player[Target].X + 12)) { goto case (int)BehaviorType.Friendly; }
                    if ((Y + Range) < (s_Player[Target].Y + OFFSET_Y) || (Y - Range) > (s_Player[Target].Y + 9)) { goto case (int)BehaviorType.Friendly; }

                    if (X != s_Player[Target].X)
                    {
                        if (X > s_Player[Target].X + OFFSET_X && X > 0)
                        {
                            if (s_Map.Ground[X - 1, Y].Type == (int)TileType.Blocked || s_Map.Ground[X - 1, Y].Type == (int)TileType.NpcAvoid)
                            {
                                Direction = (int)Directions.Left;
                                DidMove = true;
                                return;
                            }
                            /*for (int i = 0; i < 10; i++)
                            {
                                if (s_Map.m_MapNpc[i].IsSpawned)
                                {
                                    if ((X - 1) == s_Map.m_MapNpc[i].X && Y == s_Map.m_MapNpc[i].Y)
                                    {
                                        Direction = (int)Directions.Left;
                                        DidMove = true;
                                        return;
                                    }
                                }
                            }
                            for (int i = 0; i < 20; i++)
                            {
                                if (s_Map.r_MapNpc[i].IsSpawned)
                                {
                                    if ((X - 1) == s_Map.r_MapNpc[i].X && Y == s_Map.r_MapNpc[i].Y)
                                    {
                                        Direction = (int)Directions.Left;
                                        DidMove = true;
                                        return;
                                    }
                                }
                            }
                            for (int p = 0; p < 5; p++)
                            {
                                if (s_Player[p].Connection != null)
                                {
                                    if ((X - 1) == (s_Player[p].X + 12) && Y == (s_Player[p].Y + 9))
                                    {
                                        Direction = (int)Directions.Left;
                                        DidMove = true;
                                        if (p == Target) { AttackPlayer(s_Server, s_Player, p); }                             
                                        return;
                                    }
                                }
                            }*/
                            Direction = (int)Directions.Left;
                            X -= 1;
                            DidMove = true;
                        }
                        else if (X < s_Player[Target].X + OFFSET_X && X < 50)
                        {
                            if (s_Map.Ground[X + 1, Y].Type == (int)TileType.Blocked || s_Map.Ground[X + 1, Y].Type == (int)TileType.NpcAvoid)
                            {
                                Direction = (int)Directions.Right;
                                DidMove = true;
                                return;
                            }
                            /*for (int i = 0; i < 10; i++)
                            {
                                if (s_Map.m_MapNpc[i].IsSpawned)
                                {
                                    if ((X + 1) == s_Map.m_MapNpc[i].X && Y == s_Map.m_MapNpc[i].Y)
                                    {
                                        Direction = (int)Directions.Right;
                                        DidMove = true;
                                        return;
                                    }
                                }
                            }
                            for (int i = 0; i < 20; i++)
                            {
                                if (s_Map.r_MapNpc[i].IsSpawned)
                                {
                                    if ((X + 1) == s_Map.r_MapNpc[i].X && Y == s_Map.r_MapNpc[i].Y)
                                    {
                                        Direction = (int)Directions.Right;
                                        DidMove = true;
                                        return;
                                    }
                                }
                            }
                            for (int p = 0; p < 5; p++)
                            {
                                if (s_Player[p].Connection != null)
                                {
                                    if ((X + 1) == (s_Player[p].X + 12) && Y == (s_Player[p].Y + 9))
                                    {
                                        Direction = (int)Directions.Right;
                                        DidMove = true;
                                        if (p == Target) { AttackPlayer(s_Server, s_Player, p); }
                                        return;
                                    }
                                }
                            }*/
                            Direction = (int)Directions.Right;
                            X += 1;
                            DidMove = true;
                        }
                    }

                    if (Y != s_Player[Target].Y)
                    {
                        if (Y > s_Player[Target].Y + OFFSET_Y && Y > 0)
                        {
                            if (s_Map.Ground[X, Y - 1].Type == (int)TileType.Blocked || s_Map.Ground[X, Y - 1].Type == (int)TileType.NpcAvoid)
                            {
                                Direction = (int)Directions.Up;
                                DidMove = true;
                                return;
                            }
                            /*for (int i = 0; i < 10; i++)
                            {
                                if (s_Map.m_MapNpc[i].IsSpawned)
                                {
                                    if ((Y - 1) == s_Map.m_MapNpc[i].Y && X == s_Map.m_MapNpc[i].X)
                                    {
                                        Direction = (int)Directions.Up;
                                        DidMove = true;
                                        return;
                                    }
                                }
                            }
                            for (int i = 0; i < 20; i++)
                            {
                                if (s_Map.r_MapNpc[i].IsSpawned)
                                {
                                    if ((Y - 1) == s_Map.r_MapNpc[i].Y && X == s_Map.r_MapNpc[i].X)
                                    {
                                        Direction = (int)Directions.Up;
                                        DidMove = true;
                                        return;
                                    }
                                }
                            }
                            for (int p = 0; p < 5; p++)
                            {
                                if (s_Player[p].Connection != null)
                                {
                                    if ((Y - 1) == (s_Player[p].Y + 9) && X == (s_Player[p].X + 12))
                                    {
                                        Direction = (int)Directions.Up;
                                        DidMove = true;
                                        if (p == Target) { AttackPlayer(s_Server, s_Player, p); }
                                        return;
                                    }
                                }
                            }*/
                            Direction = (int)Directions.Up;
                            Y -= 1;
                            DidMove = true;
                        }
                        else if (Y < s_Player[Target].Y + OFFSET_Y && Y < 50)
                        {
                            if (s_Map.Ground[X, Y + 1].Type == (int)TileType.Blocked || s_Map.Ground[X, Y + 1].Type == (int)TileType.NpcAvoid)
                            {
                                Direction = (int)Directions.Down;
                                DidMove = true;
                                return;
                            }
                            /*for (int i = 0; i < 10; i++)
                            {
                                if (s_Map.m_MapNpc[i].IsSpawned)
                                {
                                    if ((Y + 1) == s_Map.m_MapNpc[i].Y && X == s_Map.m_MapNpc[i].X)
                                    {
                                        Direction = (int)Directions.Down;
                                        DidMove = true;
                                        return;
                                    }
                                }
                            }
                            for (int i = 0; i < 20; i++)
                            {
                                if (s_Map.r_MapNpc[i].IsSpawned)
                                {
                                    if ((Y + 1) == s_Map.r_MapNpc[i].Y && X == s_Map.r_MapNpc[i].X)
                                    {
                                        Direction = (int)Directions.Down;
                                        DidMove = true;
                                        return;
                                    }
                                }
                            }
                            for (int p = 0; p < 5; p++)
                            {
                                if (s_Player[p].Connection != null)
                                {
                                    if ((Y + 1) == (s_Player[p].Y + 9) && X == (s_Player[p].X + 12))
                                    {
                                        Direction = (int)Directions.Down;
                                        DidMove = true;
                                        if (p == Target) { AttackPlayer(s_Server, s_Player, p); }
                                        return;
                                    }
                                }
                            }*/
                            Direction = (int)Directions.Down;
                            Y += 1;
                            DidMove = true;
                        }
                    }

                    if (X == s_Player[Target].X + OFFSET_X && Y == s_Player[Target].Y + OFFSET_Y)
                    {
                        AttackPlayer(s_Server, s_Player, Target);
                    }

                    if (DidMove == true)
                    {
                        if (Step == 3) { Step = 0; } else { Step += 1; }
                    }
                    break;

                case (int)BehaviorType.ToLocation:                   

                    if (X != DesX)
                    {
                        if (X > DesX && X > 0)
                        {
                            Direction = (int)Directions.Left;
                            X -= 1;
                            DidMove = true;
                        }
                        else if (X < DesX && X < 50)
                        {
                            Direction = (int)Directions.Right;
                            X += 1;
                            DidMove = true;
                        }
                    }

                    if (Y != DesY)
                    {
                        if (Y > DesY && Y > 0)
                        {
                            Direction = (int)Directions.Up;
                            Y -= 1;
                            DidMove = true;
                        }
                        else if (Y < DesY && Y < 50)
                        {
                            Direction = (int)Directions.Down;
                            Y += 1;
                            DidMove = true;
                        }
                    }

                    if (DidMove == true)
                    {
                        if (Step == 3) { Step = 0; } else { Step += 1; }
                    }
                    break;
            }
        }
        #endregion
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
