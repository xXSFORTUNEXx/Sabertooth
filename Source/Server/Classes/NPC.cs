using Lidgren.Network;
using System;
using System.Data.SQLite;
using static System.Convert;
using static System.Environment;

namespace Server.Classes
{
    public class Npc
    {
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
        public bool IsSpawned;
        public bool DidMove;
        public int Target;
        public double s_LastPoint;
        public int spawnTick;
        public int SpawnX;
        public int SpawnY;

        HandleData sendData = new HandleData();

        public Npc() { }

        public Npc(string name, int x, int y, int direction, int sprite, int step, int owner, int behavior, int spawnTime, int health, int maxhealth, int damage, int desx, int desy,
                    int exp, int money, int range, int shopnum, int chatnum)
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
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
            {
                conn.Open();
                string sql;
                sql = "INSERT INTO NPCS";
                sql = sql + "(`NAME`,`X`,`Y`,`DIRECTION`,`SPRITE`,`STEP`,`OWNER`,`BEHAVIOR`,`SPAWNTIME`,`HEALTH`,`MAXHEALTH`,`DAMAGE`,`DESX`,`DESY`,`EXP`,`MONEY`,`RANGE`,`SHOPNUM`,`CHATNUM`)";
                sql = sql + " VALUES ";
                sql = sql + "('" + Name + "','" + X + "','" + Y + "','" + Direction + "','" + Sprite + "','" + Step + "','" + Owner + "','" + Behavior + "',";
                sql = sql + "'" + SpawnTime + "','" + Health + "','" + MaxHealth + "','" + Damage + "','" + DesX + "','" + DesY + "','" + Exp + "','" + Money + "','" + Range + "','" + ShopNum + "','" + ChatNum + "');";

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
                sql = sql + "EXP = '" + Exp + "', MONEY = '" + Money + "', RANGE = '" + Range + "', SHOPNUM = '" + ShopNum + "', CHATNUM = '" + ChatNum + "' ";
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
                            ChatNum = ToInt32(read["CHATNUM"].ToString());
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
                s_Player[index].LifeDay = 0;
                s_Player[index].LifeHour = 0;
                s_Player[index].LifeMinute = 0;
                s_Player[index].LifeSecond = 0;
                if (NewLongestLife(s_Player[index]))
                {
                    s_Player[index].LongestLifeDay = day;
                    s_Player[index].LongestLifeHour = hour;
                    s_Player[index].LongestLifeMinute = minute;
                    s_Player[index].LongestLifeSecond = second;
                }
                sendData.SendPlayers(s_Server, s_Player);
                string deathMsg = s_Player[index].Name + " has been killed by " + Name + ".";
                sendData.SendServerMessageToAll(s_Server, deathMsg);
            }
            else
            {
                sendData.SendUpdatePlayerStats(s_Server, s_Player, index);
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

                    for (int p = 0; p < 5; p++)
                    {
                        if (s_Player[p].Connection != null && s_Player[p].Name != null)
                        {
                            int s_PlayerX = s_Player[p].X + 12;
                            int s_PlayerY = s_Player[p].Y + 9;
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

                    if ((X + Range) < (s_Player[Target].X + 12) || (X - Range) > (s_Player[Target].X + 12)) { goto case (int)BehaviorType.Friendly; }
                    if ((Y + Range) < (s_Player[Target].Y + 9) || (Y - Range) > (s_Player[Target].Y + 9)) { goto case (int)BehaviorType.Friendly; }

                    if (X != s_Player[Target].X)
                    {
                        if (X > s_Player[Target].X + 12 && X > 0)
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
                        else if (X < s_Player[Target].X + 12 && X < 50)
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
                        if (Y > s_Player[Target].Y + 9 && Y > 0)
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
                        else if (Y < s_Player[Target].Y + 9 && Y < 50)
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

                    if (X == s_Player[Target].X + 12 && Y == s_Player[Target].Y + 9)
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
