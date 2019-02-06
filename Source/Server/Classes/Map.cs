using Lidgren.Network;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using static System.Console;
using System.Data.SqlClient;
using static System.Environment;
using System.ComponentModel;
using static System.Convert;
using static SabertoothServer.Server;
using static SabertoothServer.Globals;
using static System.IO.File;
using static SabertoothServer.Logging;

namespace SabertoothServer
{
    public class Map
    {
        #region Properties
        [Category("Properties"), Description("Name of the map.")]
        public string Name { get; set; }
        [Category("Properties"), Description("Revision of the map.")]
        public int Revision { get; set; }
        [Category("Border"), Description("Top connected map.")]
        public int TopMap { get; set; }
        [Category("Border"), Description("Bottom connected map.")]
        public int BottomMap { get; set; }
        [Category("Border"), Description("Left connected map.")]
        public int LeftMap { get; set; }
        [Category("Border"), Description("Right connected map.")]
        public int RightMap { get; set; }
        [Category("Brightness"), Description("Brightness of the map. (0 - 255)")]
        public int Brightness { get; set; }
        public int Id { get; set; }
        public bool IsInstance { get; set; }
        #endregion

        #region Classes
        public Tile[,] Ground = new Tile[50, 50];
        public Tile[,] Mask = new Tile[50, 50];
        public Tile[,] Fringe = new Tile[50, 50];
        public Tile[,] MaskA = new Tile[50, 50];
        public Tile[,] FringeA = new Tile[50, 50];
        public MapNpc[] m_MapNpc = new MapNpc[MAX_MAP_NPCS];
        public MapNpc[] r_MapNpc = new MapNpc[MAX_MAP_POOL_NPCS];
        public MapProj[] m_MapProj = new MapProj[MAX_MAP_PROJECTILES];
        public MapItem[] m_MapItem = new MapItem[MAX_MAP_ITEMS];
        public BloodSplat[] m_BloodSplats = new BloodSplat[MAX_BLOOD_SPLATS];
        #endregion

        #region Database
        private byte[] ToByteArray(object source)
        {
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, source);
                return stream.ToArray();
            }
        }

        private static object ByteArrayToObject(byte[] arrBytes)
        {
            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = binForm.Deserialize(memStream);
                return obj;
            }
        }

        public void CreateMapInDatabase()
        {
            Name = "Default";
            TopMap = 0;
            BottomMap = 0;
            LeftMap = 0;
            RightMap = 0;
            Revision = 0;
            Brightness = 0;

            for (int i = 0; i < 10; i++)
            {
                m_MapNpc[i] = new MapNpc("None", 0, 0, 0);
            }

            for (int i = 0; i < 20; i++)
            {
                m_MapItem[i] = new MapItem("None", 0, 0, 0);
            }

            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    Ground[x, y] = new Tile();
                    Mask[x, y] = new Tile();
                    Fringe[x, y] = new Tile();
                    MaskA[x, y] = new Tile();
                    FringeA[x, y] = new Tile();

                    Ground[x, y].TileX = 0;
                    Ground[x, y].TileY = 32;
                    Ground[x, y].TileW = 32;
                    Ground[x, y].TileH = 32;
                    Ground[x, y].Tileset = 0;
                    Ground[x, y].Type = (int)TileType.None;
                    Ground[x, y].SpawnNum = 0;
                    Ground[x, y].SpawnAmount = 0;

                    Mask[x, y].TileX = 0;
                    Mask[x, y].TileY = 0;
                    Mask[x, y].TileW = 32;
                    Mask[x, y].TileH = 32;
                    Mask[x, y].Tileset = 0;

                    Fringe[x, y].TileX = 0;
                    Fringe[x, y].TileY = 0;
                    Fringe[x, y].TileW = 0;
                    Fringe[x, y].TileH = 0;
                    Fringe[x, y].Tileset = 0;

                    MaskA[x, y].TileX = 0;
                    MaskA[x, y].TileY = 0;
                    MaskA[x, y].TileW = 0;
                    MaskA[x, y].TileH = 0;
                    MaskA[x, y].Tileset = 0;

                    FringeA[x, y].TileX = 0;
                    FringeA[x, y].TileY = 0;
                    FringeA[x, y].TileW = 0;
                    FringeA[x, y].TileH = 0;
                    FringeA[x, y].Tileset = 0;
                }
            }

            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            string script = ReadAllText("SQL Data Scripts/INSERT MAP.sql");
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                using (var cmd = new SqlCommand(script, sql))
                {
                    byte[] m_Npc = ToByteArray(m_MapNpc);
                    byte[] m_Item = ToByteArray(m_MapItem);
                    byte[] m_Ground = ToByteArray(Ground);
                    byte[] m_Mask = ToByteArray(Mask);
                    byte[] m_MaskA = ToByteArray(MaskA);
                    byte[] m_Fringe = ToByteArray(Fringe);
                    byte[] m_FringeA = ToByteArray(FringeA);
                        
                    cmd.Parameters.Add(new SqlParameter("@name", System.Data.SqlDbType.Text)).Value = Name;
                    cmd.Parameters.Add(new SqlParameter("@revision", System.Data.SqlDbType.Int)).Value = Revision;
                    cmd.Parameters.Add(new SqlParameter("@top", System.Data.SqlDbType.Int)).Value = TopMap;
                    cmd.Parameters.Add(new SqlParameter("@bottom", System.Data.SqlDbType.Int)).Value = BottomMap;
                    cmd.Parameters.Add(new SqlParameter("@left", System.Data.SqlDbType.Int)).Value = LeftMap;
                    cmd.Parameters.Add(new SqlParameter("@right", System.Data.SqlDbType.Int)).Value = RightMap;
                    cmd.Parameters.Add(new SqlParameter("@brightness", System.Data.SqlDbType.Int)).Value = Brightness;
                    cmd.Parameters.Add(new SqlParameter("@npc", System.Data.SqlDbType.VarBinary)).Value = m_Npc;
                    cmd.Parameters.Add(new SqlParameter("@item", System.Data.SqlDbType.VarBinary)).Value = m_Item;
                    cmd.Parameters.Add(new SqlParameter("@ground", System.Data.SqlDbType.VarBinary)).Value = m_Ground;
                    cmd.Parameters.Add(new SqlParameter("@mask", System.Data.SqlDbType.VarBinary)).Value = m_Mask;
                    cmd.Parameters.Add(new SqlParameter("@maska", System.Data.SqlDbType.VarBinary)).Value = m_MaskA;
                    cmd.Parameters.Add(new SqlParameter("@fringe", System.Data.SqlDbType.VarBinary)).Value = m_Fringe;
                    cmd.Parameters.Add(new SqlParameter("@fringea", System.Data.SqlDbType.VarBinary)).Value = m_FringeA;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void SaveMapInDatabase(int mapNum)
        {
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            string script = ReadAllText("SQL Data Scripts/SAVE MAP.sql");
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                using (var cmd = new SqlCommand(script, sql))
                {
                    byte[] m_Npc = ToByteArray(m_MapNpc);
                    byte[] m_Item = ToByteArray(m_MapItem);
                    byte[] m_Ground = ToByteArray(Ground);
                    byte[] m_Mask = ToByteArray(Mask);
                    byte[] m_MaskA = ToByteArray(MaskA);
                    byte[] m_Fringe = ToByteArray(Fringe);
                    byte[] m_FringeA = ToByteArray(FringeA);

                    cmd.Parameters.Add(new SqlParameter("id", System.Data.SqlDbType.Int)).Value = mapNum;
                    cmd.Parameters.Add(new SqlParameter("@name", System.Data.SqlDbType.Text)).Value = Name;
                    cmd.Parameters.Add(new SqlParameter("@revision", System.Data.SqlDbType.Int)).Value = Revision;
                    cmd.Parameters.Add(new SqlParameter("@top", System.Data.SqlDbType.Int)).Value = TopMap;
                    cmd.Parameters.Add(new SqlParameter("@bottom", System.Data.SqlDbType.Int)).Value = BottomMap;
                    cmd.Parameters.Add(new SqlParameter("@left", System.Data.SqlDbType.Int)).Value = LeftMap;
                    cmd.Parameters.Add(new SqlParameter("@right", System.Data.SqlDbType.Int)).Value = RightMap;
                    cmd.Parameters.Add(new SqlParameter("@brightness", System.Data.SqlDbType.Int)).Value = Brightness;
                    cmd.Parameters.Add(new SqlParameter("@npc", System.Data.SqlDbType.VarBinary)).Value = m_Npc;
                    cmd.Parameters.Add(new SqlParameter("@item", System.Data.SqlDbType.VarBinary)).Value = m_Item;
                    cmd.Parameters.Add(new SqlParameter("@ground", System.Data.SqlDbType.VarBinary)).Value = m_Ground;
                    cmd.Parameters.Add(new SqlParameter("@mask", System.Data.SqlDbType.VarBinary)).Value = m_Mask;
                    cmd.Parameters.Add(new SqlParameter("@maska", System.Data.SqlDbType.VarBinary)).Value = m_MaskA;
                    cmd.Parameters.Add(new SqlParameter("@fringe", System.Data.SqlDbType.VarBinary)).Value = m_Fringe;
                    cmd.Parameters.Add(new SqlParameter("@fringea", System.Data.SqlDbType.VarBinary)).Value = m_FringeA;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void LoadMapFromDatabase(int mapNum)
        {
            for (int i = 0; i < 20; i++)
            {
                if (i < 10)
                {
                    m_MapNpc[i] = new MapNpc();
                }
                r_MapNpc[i] = new MapNpc();
                m_MapItem[i] = new MapItem();
            }

            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            string script = ReadAllText("SQL Data Scripts/LOAD MAP.sql");
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                using (SqlCommand cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = mapNum;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Id = ToInt32(reader[0]);
                            Name = reader[1].ToString();
                            Revision = ToInt32(reader[2]);
                            TopMap = ToInt32(reader[3]);
                            BottomMap = ToInt32(reader[4]);
                            LeftMap = ToInt32(reader[5]);
                            RightMap = ToInt32(reader[6]);
                            Brightness = ToInt32(reader[7]);

                            byte[] buffer;
                            object load;

                            buffer = (byte[])reader[8];
                            load = ByteArrayToObject(buffer);
                            m_MapNpc = (MapNpc[])load;

                            buffer = (byte[])reader[9];
                            load = ByteArrayToObject(buffer);
                            m_MapItem = (MapItem[])load;

                            buffer = (byte[])reader[10];
                            load = ByteArrayToObject(buffer);
                            Ground = (Tile[,])load;

                            buffer = (byte[])reader[11];
                            load = ByteArrayToObject(buffer);
                            Mask = (Tile[,])load;

                            buffer = (byte[])reader[12];
                            load = ByteArrayToObject(buffer);
                            MaskA = (Tile[,])load;

                            buffer = (byte[])reader[13];
                            load = ByteArrayToObject(buffer);
                            Fringe = (Tile[,])load;

                            buffer = (byte[])reader[14];
                            load = ByteArrayToObject(buffer);
                            FringeA = (Tile[,])load;
                        }
                    }
                }
            }
        }

        public void LoadMapNameFromDatabase(int mapNum)
        {
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                string command;
                command = "SELECT NAME FROM MAPS WHERE ID=@id";
                using (SqlCommand cmd = new SqlCommand(command, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("id", System.Data.SqlDbType.Int)).Value = mapNum;
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

        #region Projectiles
        public int FindOpenProjSlot()
        {
            for (int i = 0; i < MAX_MAP_PROJECTILES; i++)
            {
                if (m_MapProj[i] == null)
                {
                    return i;
                }
            }
            return MAX_MAP_PROJECTILES;
        }

        public void ClearProjSlot(int mapIndex, int slot)
        {
            if (m_MapProj[slot] != null)
            {
                m_MapProj[slot] = null;
                for (int i = 0; i < MAX_PLAYERS; i++)
                {
                    if (players[i].Connection != null && players[i].Map == mapIndex)
                    {
                        SendClearProjectileTo(players[i].Connection, mapIndex, slot);
                    }
                }
            }
        }

        public void CreateProjectile(int mapIndex, int playerIndex)
        {
            int slot = FindOpenProjSlot();
            if (slot == MAX_MAP_PROJECTILES) { WriteMessageLog("Bullet max reached!"); return; }

            int projNum = players[playerIndex].mainWeapon.ProjectileNumber - 1;
            int damage = players[playerIndex].mainWeapon.Damage + projectiles[projNum].Damage;
            m_MapProj[slot] = new MapProj
            {
                Name = projectiles[projNum].Name,
                Sprite = projectiles[projNum].Sprite,
                Type = projectiles[projNum].Type,
                Speed = projectiles[projNum].Speed,
                Damage = damage,
                Range = projectiles[projNum].Range,
                X = (players[playerIndex].X + OFFSET_X),
                Y = (players[playerIndex].Y + OFFSET_Y),
                Owner = playerIndex,
                Direction = players[playerIndex].AimDirection
            };

            for (int i = 0; i < MAX_PLAYERS; i++)
            {
                if (players[i].Connection != null && players[i].Map == mapIndex)
                {
                    SendNewProjectileTo(players[i].Connection, mapIndex, slot, projNum);
                }
            }
        }

        void SendNewProjectileTo(NetConnection p_Conn, int mapIndex, int slot, int projNum)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.CreateProj);
            outMSG.WriteVariableInt32(slot);
            outMSG.WriteVariableInt32(m_MapProj[slot].ProjNum);
            outMSG.WriteVariableInt32(m_MapProj[slot].X);
            outMSG.WriteVariableInt32(m_MapProj[slot].Y);
            outMSG.WriteVariableInt32(m_MapProj[slot].Direction);            
            SabertoothServer.netServer.SendMessage(outMSG, p_Conn, NetDeliveryMethod.ReliableOrdered);
        }

        void SendClearProjectileTo(NetConnection p_Conn, int mapIndex, int slot)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.ClearProj);
            outMSG.Write(maps[mapIndex].Name);
            outMSG.WriteVariableInt32(slot);
            SabertoothServer.netServer.SendMessage(outMSG, p_Conn, NetDeliveryMethod.ReliableOrdered);
        }
        #endregion

        #region Blood
        public int FindOpenBloodSlot()
        {
            for (int i = 0; i < MAX_BLOOD_SPLATS; i++)
            {
                if (m_BloodSplats[i] == null)
                {
                    return i;
                }
            }
            return MAX_BLOOD_SPLATS;
        }

        public void ClearBloodSlot(int mapIndex, int slot)
        {
            if (m_BloodSplats[slot] != null)
            {
                m_BloodSplats[slot] = null;
                for (int i = 0; i < MAX_PLAYERS; i++)
                {
                    if (players[i].Connection != null && players[i].Map == mapIndex)
                    {
                        SendClearBloodSplatTo(players[i].Connection, mapIndex, slot);
                    }
                }
            }
        }

        public void CreateBloodSplat(int mapIndex, int x, int y)
        {
            int slot = FindOpenBloodSlot();
            if (slot == MAX_BLOOD_SPLATS)
            {
                WriteMessageLog("Bloodspat max reached! Resetting...");
                for (int b = 0; b < MAX_BLOOD_SPLATS; b++)
                {
                    ClearBloodSlot(mapIndex, b);
                }
                return;
            }
            m_BloodSplats[slot] = new BloodSplat(x, y);

            for (int i = 0; i < MAX_PLAYERS; i++)
            {
                if (players[i].Connection != null && players[i].Map == mapIndex)
                {
                    SendCreateBloodSplatTo(players[i].Connection, mapIndex, slot);
                }
            }
        }

        public void SendCreateBloodSplatTo(NetConnection p_Conn, int mapIndex, int slot)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.CreateBlood);
            outMSG.WriteVariableInt32(slot);
            outMSG.WriteVariableInt32(m_BloodSplats[slot].X);
            outMSG.WriteVariableInt32(m_BloodSplats[slot].Y);
            SabertoothServer.netServer.SendMessage(outMSG, p_Conn, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendClearBloodSplatTo(NetConnection p_Conn, int mapIndex, int slot)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.ClearBlood);
            outMSG.Write(maps[mapIndex].Name);
            outMSG.WriteVariableInt32(slot);
            SabertoothServer.netServer.SendMessage(outMSG, p_Conn, NetDeliveryMethod.ReliableOrdered);
        }
        #endregion
    }

    [Serializable()]
    public class MapNpc
    {
        #region Properties
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int NpcNum { get; set; }
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

        public bool IsSpawned;
        public bool DidMove;
        public int Target;
        public double s_LastPoint;
        public int spawnTick;
        public int SpawnX;
        public int SpawnY;
        public int aiTick;

        public MapNpc() { }

        public MapNpc(string name, int x, int y, int npcnum)
        {
            Name = name;
            X = x;
            Y = y;
            npcnum = NpcNum;
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

        public void AttackPlayer(int index)
        {
            players[index].Health -= Damage;
            if (players[index].Health <= 0)
            {
                players[index].Health = players[index].MaxHealth;
                players[index].X = 0;
                players[index].Y = 0;
                int day = players[index].LifeDay;
                int hour = players[index].LifeHour;
                int minute = players[index].LifeMinute;
                int second = players[index].LifeSecond;
                players[index].LifeDay = 0;
                players[index].LifeHour = 0;
                players[index].LifeMinute = 0;
                players[index].LifeSecond = 0;
                if (NewLongestLife(players[index]))
                {
                    players[index].LongestLifeDay = day;
                    players[index].LongestLifeHour = hour;
                    players[index].LongestLifeMinute = minute;
                    players[index].LongestLifeSecond = second;
                }
                HandleData.SendPlayers();
                string deathMsg = players[index].Name + " has been killed by " + Name + ".";
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

        public void NpcAI(int s_CanMove, int s_Direction, int mapNum)
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
                                    if (maps[mapNum].Ground[X, Y + 1].Type == (int)TileType.Blocked || maps[mapNum].Ground[X, Y + 1].Type == (int)TileType.NpcAvoid)
                                    {
                                        Direction = (int)Directions.Down;
                                        DidMove = true;
                                        return;
                                    }
                                    for (int i = 0; i < 10; i++)
                                    {
                                        if (maps[mapNum].m_MapNpc[i].IsSpawned)
                                        {
                                            if ((Y + 1) == maps[mapNum].m_MapNpc[i].Y && X == maps[mapNum].m_MapNpc[i].X)
                                            {
                                                Direction = (int)Directions.Down;
                                                DidMove = true;
                                                return;
                                            }
                                        }
                                    }
                                    for (int i = 0; i < 20; i++)
                                    {
                                        if (maps[mapNum].r_MapNpc[i].IsSpawned)
                                        {
                                            if ((Y + 1) == maps[mapNum].r_MapNpc[i].Y && X == maps[mapNum].r_MapNpc[i].X)
                                            {
                                                Direction = (int)Directions.Down;
                                                DidMove = true;
                                                return;
                                            }
                                        }
                                    }
                                    for (int p = 0; p < 5; p++)
                                    {
                                        if (players[p].Connection != null)
                                        {
                                            if ((Y + 1) == (players[p].Y + 9) && X == (players[p].X + 12))
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
                                    if (maps[mapNum].Ground[X - 1, Y].Type == (int)TileType.Blocked || maps[mapNum].Ground[X - 1, Y].Type == (int)TileType.NpcAvoid)
                                    {
                                        Direction = (int)Directions.Left;
                                        DidMove = true;
                                        return;
                                    }
                                    for (int i = 0; i < 10; i++)
                                    {
                                        if (maps[mapNum].m_MapNpc[i].IsSpawned)
                                        {
                                            if ((X - 1) == maps[mapNum].m_MapNpc[i].X && Y == maps[mapNum].m_MapNpc[i].Y)
                                            {
                                                Direction = (int)Directions.Left;
                                                DidMove = true;
                                                return;
                                            }
                                        }
                                    }
                                    for (int i = 0; i < 20; i++)
                                    {
                                        if (maps[mapNum].r_MapNpc[i].IsSpawned)
                                        {
                                            if ((X - 1) == maps[mapNum].r_MapNpc[i].X && Y == maps[mapNum].r_MapNpc[i].Y)
                                            {
                                                Direction = (int)Directions.Left;
                                                DidMove = true;
                                                return;
                                            }
                                        }
                                    }
                                    for (int p = 0; p < 5; p++)
                                    {
                                        if (players[p].Connection != null)
                                        {
                                            if ((X - 1) == (players[p].X + 12) && Y == (players[p].Y + 9))
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
                                    if (maps[mapNum].Ground[X + 1, Y].Type == (int)TileType.Blocked || maps[mapNum].Ground[X + 1, Y].Type == (int)TileType.NpcAvoid)
                                    {
                                        Direction = (int)Directions.Right;
                                        DidMove = true;
                                        return;
                                    }
                                    for (int i = 0; i < 10; i++)
                                    {
                                        if (maps[mapNum].m_MapNpc[i].IsSpawned)
                                        {
                                            if ((X + 1) == maps[mapNum].m_MapNpc[i].X && Y == maps[mapNum].m_MapNpc[i].Y)
                                            {
                                                Direction = (int)Directions.Right;
                                                DidMove = true;
                                                return;
                                            }
                                        }
                                    }
                                    for (int i = 0; i < 20; i++)
                                    {
                                        if (maps[mapNum].r_MapNpc[i].IsSpawned)
                                        {
                                            if ((X + 1) == maps[mapNum].r_MapNpc[i].X && Y == maps[mapNum].r_MapNpc[i].Y)
                                            {
                                                Direction = (int)Directions.Right;
                                                DidMove = true;
                                                return;
                                            }
                                        }
                                    }
                                    for (int p = 0; p < 5; p++)
                                    {
                                        if (players[p].Connection != null)
                                        {
                                            if ((X + 1) == (players[p].X + 12) && Y == (players[p].Y + 9))
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
                                    if (maps[mapNum].Ground[X, Y - 1].Type == (int)TileType.Blocked || maps[mapNum].Ground[X, Y - 1].Type == (int)TileType.NpcAvoid)
                                    {
                                        Direction = (int)Directions.Up;
                                        DidMove = true;
                                        return;
                                    }
                                    for (int i = 0; i < 10; i++)
                                    {
                                        if (maps[mapNum].m_MapNpc[i].IsSpawned)
                                        {
                                            if ((Y - 1) == maps[mapNum].m_MapNpc[i].Y && X == maps[mapNum].m_MapNpc[i].X)
                                            {
                                                Direction = (int)Directions.Up;
                                                DidMove = true;
                                                return;
                                            }
                                        }
                                    }
                                    for (int i = 0; i < 20; i++)
                                    {
                                        if (maps[mapNum].r_MapNpc[i].IsSpawned)
                                        {
                                            if ((Y - 1) == maps[mapNum].r_MapNpc[i].Y && X == maps[mapNum].r_MapNpc[i].X)
                                            {
                                                Direction = (int)Directions.Up;
                                                DidMove = true;
                                                return;
                                            }
                                        }
                                    }
                                    for (int p = 0; p < 5; p++)
                                    {
                                        if (players[p].Connection != null)
                                        {
                                            if ((Y - 1) == (players[p].Y + 9) && X == (players[p].X + 12))
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
                        if (players[p].Connection != null && players[p].Name != null && mapNum == players[p].Map)
                        {
                            int s_PlayerX = players[p].X + OFFSET_X;
                            int s_PlayerY = players[p].Y + OFFSET_Y;
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

                    if ((X + Range) < (players[Target].X + OFFSET_X) || (X - Range) > (players[Target].X + OFFSET_X)) { goto case (int)BehaviorType.Friendly; }
                    if ((Y + Range) < (players[Target].Y + OFFSET_Y) || (Y - Range) > (players[Target].Y + OFFSET_Y)) { goto case (int)BehaviorType.Friendly; }

                    if (X != players[Target].X)
                    {
                        if (X > players[Target].X + OFFSET_X && X > 0)
                        {
                            if (maps[mapNum].Ground[X - 1, Y].Type == (int)TileType.Blocked || maps[mapNum].Ground[X - 1, Y].Type == (int)TileType.NpcAvoid)
                            {
                                Direction = (int)Directions.Left;
                                DidMove = true;
                                return;
                            }
                            /*for (int i = 0; i < 10; i++)
                            {
                                if (s_Map[mapNum].m_MapNpc[i].IsSpawned)
                                {
                                    if ((X - 1) == s_Map[mapNum].m_MapNpc[i].X && Y == s_Map[mapNum].m_MapNpc[i].Y)
                                    {
                                        Direction = (int)Directions.Left;
                                        DidMove = true;
                                        return;
                                    }
                                }
                            }
                            for (int i = 0; i < 20; i++)
                            {
                                if (s_Map[mapNum].r_MapNpc[i].IsSpawned)
                                {
                                    if ((X - 1) == s_Map[mapNum].r_MapNpc[i].X && Y == s_Map[mapNum].r_MapNpc[i].Y)
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
                        else if (X < players[Target].X + OFFSET_X && X < 50)
                        {
                            if (maps[mapNum].Ground[X + 1, Y].Type == (int)TileType.Blocked || maps[mapNum].Ground[X + 1, Y].Type == (int)TileType.NpcAvoid)
                            {
                                Direction = (int)Directions.Right;
                                DidMove = true;
                                return;
                            }
                            /*for (int i = 0; i < 10; i++)
                            {
                                if (s_Map[mapNum].m_MapNpc[i].IsSpawned)
                                {
                                    if ((X + 1) == s_Map[mapNum].m_MapNpc[i].X && Y == s_Map[mapNum].m_MapNpc[i].Y)
                                    {
                                        Direction = (int)Directions.Right;
                                        DidMove = true;
                                        return;
                                    }
                                }
                            }
                            for (int i = 0; i < 20; i++)
                            {
                                if (s_Map[mapNum].r_MapNpc[i].IsSpawned)
                                {
                                    if ((X + 1) == s_Map[mapNum].r_MapNpc[i].X && Y == s_Map[mapNum].r_MapNpc[i].Y)
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

                    if (Y != players[Target].Y)
                    {
                        if (Y > players[Target].Y + OFFSET_Y && Y > 0)
                        {
                            if (maps[mapNum].Ground[X, Y - 1].Type == (int)TileType.Blocked || maps[mapNum].Ground[X, Y - 1].Type == (int)TileType.NpcAvoid)
                            {
                                Direction = (int)Directions.Up;
                                DidMove = true;
                                return;
                            }
                            /*for (int i = 0; i < 10; i++)
                            {
                                if (s_Map[mapNum].m_MapNpc[i].IsSpawned)
                                {
                                    if ((Y - 1) == s_Map[mapNum].m_MapNpc[i].Y && X == s_Map[mapNum].m_MapNpc[i].X)
                                    {
                                        Direction = (int)Directions.Up;
                                        DidMove = true;
                                        return;
                                    }
                                }
                            }
                            for (int i = 0; i < 20; i++)
                            {
                                if (s_Map[mapNum].r_MapNpc[i].IsSpawned)
                                {
                                    if ((Y - 1) == s_Map[mapNum].r_MapNpc[i].Y && X == s_Map[mapNum].r_MapNpc[i].X)
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
                        else if (Y < players[Target].Y + OFFSET_Y && Y < 50)
                        {
                            if (maps[mapNum].Ground[X, Y + 1].Type == (int)TileType.Blocked || maps[mapNum].Ground[X, Y + 1].Type == (int)TileType.NpcAvoid)
                            {
                                Direction = (int)Directions.Down;
                                DidMove = true;
                                return;
                            }
                            /*for (int i = 0; i < 10; i++)
                            {
                                if (s_Map[mapNum].m_MapNpc[i].IsSpawned)
                                {
                                    if ((Y + 1) == s_Map[mapNum].m_MapNpc[i].Y && X == s_Map[mapNum].m_MapNpc[i].X)
                                    {
                                        Direction = (int)Directions.Down;
                                        DidMove = true;
                                        return;
                                    }
                                }
                            }
                            for (int i = 0; i < 20; i++)
                            {
                                if (s_Map[mapNum].r_MapNpc[i].IsSpawned)
                                {
                                    if ((Y + 1) == s_Map[mapNum].r_MapNpc[i].Y && X == s_Map[mapNum].r_MapNpc[i].X)
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

                    if (X == players[Target].X + OFFSET_X && Y == players[Target].Y + OFFSET_Y)
                    {
                        AttackPlayer(Target);
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

    public class MapProj : Projectile
    {
        public int ProjNum { get; set; }

        public MapProj() { }

        public MapProj(string name, int x, int y, int projnum)
        {
            Name = name;
            X = x;
            Y = y;
            ProjNum = projnum;
        }
    }

    public class BloodSplat
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int TexX { get; set; }
        public int TexY { get; set; }
        public bool Active { get; set; }

        public BloodSplat() { }

        public BloodSplat(int x, int y)
        {
            X = x;
            Y = y;
            Active = true;
        }
    }

    [Serializable()]
    public class MapItem
    {
        #region Properties
        public string Name { get; set; }
        public int Sprite { get; set; }
        public int Damage { get; set; }
        public int Armor { get; set; }
        public int Type { get; set; }
        public int AttackSpeed { get; set; }
        public int ReloadSpeed { get; set; }
        public int HealthRestore { get; set; }
        public int HungerRestore { get; set; }
        public int HydrateRestore { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Endurance { get; set; }
        public int Stamina { get; set; }
        public int Clip { get; set; }
        public int MaxClip { get; set; }
        public int ItemAmmoType { get; set; }
        public int Value { get; set; }
        public int ProjectileNumber { get; set; }
        public int Price { get; set; }
        public int Rarity { get; set; }
        public int ItemNum { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        #endregion

        public int ExpireTick;

        public bool IsSpawned;

        public MapItem() { }

        public MapItem(string name, int x, int y, int itemnum)
        {
            Name = name;
            X = x;
            Y = y;
            ItemNum = itemnum;
        }
    }

    [Serializable()]
    public class Tile
    {
        public int TileX { get; set; }
        public int TileY { get; set; }
        public int TileW { get; set; }
        public int TileH { get; set; }
        public int Tileset { get; set; }

        public int Type { get; set; }
        public bool Flagged { get; set; }

        public int SpawnNum { get; set; }
        public int SpawnAmount { get; set; }

        public int ChestNum { get; set; }
        public int CurrentSpawn;

        public bool NeedsSpawned;
        public int NeedsSpawnedTick;

        public double LightRadius { get; set; }
        public int Map { get; set; }
        public int MapX { get; set; }
        public int MapY { get; set; }

        public Tile()
        {
            TileX = 0;
            TileY = 0;
            TileW = 0;
            TileH = 0;

            Tileset = 0;
            Type = (int)TileType.None;
            Flagged = false;
            SpawnNum = 0;
            ChestNum = 0;
            LightRadius = 0;
            Map = 0;
            MapX = 0;
            MapY = 0;
        }
    }

    public enum TileType
    {
        None,
        Blocked,
        NpcSpawn,
        SpawnPool,
        NpcAvoid,
        MapItem,
        Chest,
        Warp
    }

    public enum TileLayers
    {
        Ground,
        Mask,
        Fringe,
        MaskA,
        FringeA
    }
}
