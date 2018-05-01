﻿using Lidgren.Network;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using static System.Console;
using System.Data.SQLite;
using System.Data.SqlClient;
using static System.Environment;
using System.ComponentModel;
using static System.Convert;
using static SabertoothServer.Server;

namespace SabertoothServer
{
    public class Map
    {
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
        public Tile[,] Ground = new Tile[50, 50];
        public Tile[,] Mask = new Tile[50, 50];
        public Tile[,] Fringe = new Tile[50, 50];
        public Tile[,] MaskA = new Tile[50, 50];
        public Tile[,] FringeA = new Tile[50, 50];
        public MapNpc[] m_MapNpc = new MapNpc[10];
        public MapNpc[] r_MapNpc = new MapNpc[20];
        public MapProj[] m_MapProj = new MapProj[200];
        public MapItem[] m_MapItem = new MapItem[20];

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

            if (localDB == "0")
            {
                string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
                using (var sql = new SqlConnection(connection))
                {
                    using (var cmd = new SqlCommand(connection))
                    {
                        byte[] m_Npc = ToByteArray(m_MapNpc);
                        byte[] m_Item = ToByteArray(m_MapItem);
                        byte[] m_Ground = ToByteArray(Ground);
                        byte[] m_Mask = ToByteArray(Mask);
                        byte[] m_MaskA = ToByteArray(MaskA);
                        byte[] m_Fringe = ToByteArray(Fringe);
                        byte[] m_FringeA = ToByteArray(FringeA);
                        string command;

                        sql.Open();
                        command = "INSERT INTO MAPS (NAME,REVISION,UP,DOWN,LEFTSIDE,RIGHTSIDE,BRIGHTNESS,NPC,ITEM,GROUND,MASK,MASKA,FRINGE,FRINGEA) ";
                        command = command + " VALUES ";
                        command = command + "(@name,@revision,@top,@bottom,@left,@right,@brightness,@npc,@item,@ground,@mask,@maska,@fringe,@fringea)";
                        cmd.CommandText = command;
                        cmd.Parameters.Add("@name", System.Data.SqlDbType.Text).Value = Name;
                        cmd.Parameters.Add("@revision", System.Data.SqlDbType.Int).Value = Revision;
                        cmd.Parameters.Add("@top", System.Data.SqlDbType.Int).Value = TopMap;
                        cmd.Parameters.Add("@bottom", System.Data.SqlDbType.Int).Value = BottomMap;
                        cmd.Parameters.Add("@left", System.Data.SqlDbType.Int).Value = LeftMap;
                        cmd.Parameters.Add("@right", System.Data.SqlDbType.Int).Value = RightMap;
                        cmd.Parameters.Add("@brightness", System.Data.SqlDbType.Int).Value = Brightness;
                        cmd.Parameters.Add("@npc", System.Data.SqlDbType.VarBinary).Value = m_Npc;
                        cmd.Parameters.Add("@item", System.Data.SqlDbType.VarBinary).Value = m_Item;
                        cmd.Parameters.Add("@ground", System.Data.SqlDbType.VarBinary).Value = m_Ground;
                        cmd.Parameters.Add("@mask", System.Data.SqlDbType.VarBinary).Value = m_Mask;
                        cmd.Parameters.Add("@maska", System.Data.SqlDbType.VarBinary).Value = m_MaskA;
                        cmd.Parameters.Add("@fringe", System.Data.SqlDbType.VarBinary).Value = m_Fringe;
                        cmd.Parameters.Add("@fringea", System.Data.SqlDbType.VarBinary).Value = m_FringeA;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            else
            {
                using (var conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
                {
                    using (var cmd = new SQLiteCommand(conn))
                    {
                        byte[] m_Npc = ToByteArray(m_MapNpc);
                        byte[] m_Item = ToByteArray(m_MapItem);
                        byte[] m_Ground = ToByteArray(Ground);
                        byte[] m_Mask = ToByteArray(Mask);
                        byte[] m_MaskA = ToByteArray(MaskA);
                        byte[] m_Fringe = ToByteArray(Fringe);
                        byte[] m_FringeA = ToByteArray(FringeA);
                        string sql;

                        conn.Open();
                        sql = "INSERT INTO `MAPS` (`NAME`,`REVISION`,`UP`,`DOWN`,`LEFTSIDE`,`RIGHTSIDE`,`BRIGHTNESS`,`NPC`,`ITEM`,`GROUND`,`MASK`,`MASKA`,`FRINGE`,`FRINGEA`) ";
                        sql = sql + " VALUES ";
                        sql = sql + "(@name, @revision, @top, @bottom, @left, @right, @brightness, @npc, @item, @ground, @mask, @maska, @fringe, @fringea)";
                        cmd.CommandText = sql;
                        cmd.Parameters.Add("@name", System.Data.DbType.String).Value = Name;
                        cmd.Parameters.Add("@revision", System.Data.DbType.Int32).Value = Revision;
                        cmd.Parameters.Add("@top", System.Data.DbType.Int32).Value = TopMap;
                        cmd.Parameters.Add("@bottom", System.Data.DbType.Int32).Value = BottomMap;
                        cmd.Parameters.Add("@left", System.Data.DbType.Int32).Value = LeftMap;
                        cmd.Parameters.Add("@right", System.Data.DbType.Int32).Value = RightMap;
                        cmd.Parameters.Add("@brightness", System.Data.DbType.Int32).Value = Brightness;
                        cmd.Parameters.Add("@npc", System.Data.DbType.Binary).Value = m_Npc;
                        cmd.Parameters.Add("@item", System.Data.DbType.Binary).Value = m_Item;
                        cmd.Parameters.Add("@ground", System.Data.DbType.Binary).Value = m_Ground;
                        cmd.Parameters.Add("@mask", System.Data.DbType.Binary).Value = m_Mask;
                        cmd.Parameters.Add("@maska", System.Data.DbType.Binary).Value = m_MaskA;
                        cmd.Parameters.Add("@fringe", System.Data.DbType.Binary).Value = m_Fringe;
                        cmd.Parameters.Add("@fringea", System.Data.DbType.Binary).Value = m_FringeA;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void SaveMapInDatabase(int mapNum)
        {
            using (var conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
            {
                using (var cmd = new SQLiteCommand(conn))
                {
                    byte[] m_Npc = ToByteArray(m_MapNpc);
                    byte[] m_Item = ToByteArray(m_MapItem);
                    byte[] m_Ground = ToByteArray(Ground);
                    byte[] m_Mask = ToByteArray(Mask);
                    byte[] m_MaskA = ToByteArray(MaskA);
                    byte[] m_Fringe = ToByteArray(Fringe);
                    byte[] m_FringeA = ToByteArray(FringeA);
                    string sql;

                    conn.Open();
                    sql = "UPDATE MAPS SET NAME = @name, REVISION = @revision, UP = @top, DOWN = @bottom, LEFTSIDE = @left, RIGHTSIDE = @right, BRIGHTNESS = @brightness, NPC = @npc, ITEM = @item, GROUND = @ground, MASK = @mask, MASKA = @maska, ";
                    sql = sql + "FRINGE = @fringe, FRINGEA = @fringea WHERE rowid = " + mapNum;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@name", System.Data.DbType.String).Value = Name;
                    cmd.Parameters.Add("@revision", System.Data.DbType.Int32).Value = Revision;
                    cmd.Parameters.Add("@top", System.Data.DbType.Int32).Value = TopMap;
                    cmd.Parameters.Add("@bottom", System.Data.DbType.Int32).Value = BottomMap;
                    cmd.Parameters.Add("@left", System.Data.DbType.Int32).Value = LeftMap;
                    cmd.Parameters.Add("@right", System.Data.DbType.Int32).Value = RightMap;
                    cmd.Parameters.Add("@brightness", System.Data.DbType.Int32).Value = Brightness;
                    cmd.Parameters.Add("@npc", System.Data.DbType.Binary).Value = m_Npc;
                    cmd.Parameters.Add("@item", System.Data.DbType.Binary).Value = m_Item;
                    cmd.Parameters.Add("@ground", System.Data.DbType.Binary).Value = m_Ground;
                    cmd.Parameters.Add("@mask", System.Data.DbType.Binary).Value = m_Mask;
                    cmd.Parameters.Add("@maska", System.Data.DbType.Binary).Value = m_MaskA;
                    cmd.Parameters.Add("@fringe", System.Data.DbType.Binary).Value = m_Fringe;
                    cmd.Parameters.Add("@fringea", System.Data.DbType.Binary).Value = m_FringeA;
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
            using (var conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
            {
                using (var cmd = new SQLiteCommand(conn))
                {
                    conn.Open();
                    cmd.CommandText = "SELECT * FROM MAPS WHERE rowid = " + mapNum;
                    using (SQLiteDataReader read = cmd.ExecuteReader(System.Data.CommandBehavior.SequentialAccess))
                    {
                        while (read.Read())
                        {
                            Name = read["NAME"].ToString();
                            Revision = ToInt32(read["REVISION"].ToString());
                            TopMap = ToInt32(read["UP"].ToString());
                            BottomMap = ToInt32(read["DOWN"].ToString());
                            LeftMap = ToInt32(read["LEFTSIDE"].ToString());
                            RightMap = ToInt32(read["RIGHTSIDE"].ToString());
                            Brightness = ToInt32(read["BRIGHTNESS"].ToString());
                            byte[] buffer;
                            object load;

                            buffer = (byte[])read["NPC"];
                            load = ByteArrayToObject(buffer);
                            m_MapNpc = (MapNpc[])load;

                            buffer = (byte[])read["ITEM"];
                            load = ByteArrayToObject(buffer);
                            m_MapItem = (MapItem[])load;

                            buffer = (byte[])read["GROUND"];
                            load = ByteArrayToObject(buffer);
                            Ground = (Tile[,])load;

                            buffer = (byte[])read["MASK"];
                            load = ByteArrayToObject(buffer);
                            Mask = (Tile[,])load;

                            buffer = (byte[])read["MASKA"];
                            load = ByteArrayToObject(buffer);
                            MaskA = (Tile[,])load;

                            buffer = (byte[])read["FRINGE"];
                            load = ByteArrayToObject(buffer);
                            Fringe = (Tile[,])load;

                            buffer = (byte[])read["FRINGEA"];
                            load = ByteArrayToObject(buffer);
                            FringeA = (Tile[,])load;
                        }
                    }
                }
            }
        }

        public void LoadMapNameFromDatabase(int mapNum)
        {
            using (var conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
            {
                using (var cmd = new SQLiteCommand(conn))
                {
                    conn.Open();
                    cmd.CommandText = "SELECT * FROM MAPS WHERE rowid = " + mapNum;
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

        public int FindOpenProjSlot()
        {
            for (int i = 0; i < 200; i++)
            {
                if (m_MapProj[i] == null)
                {
                    return i;
                }
            }
            return 200;
        }

        public void ClearProjSlot(int mapIndex, int slot)
        {
            if (m_MapProj[slot] != null)
            {
                m_MapProj[slot] = null;
                for (int i = 0; i < 5; i++)
                {
                    if (players[i].Connection != null && players[i].Map == mapIndex)
                    {
                        SendClearProjectileToAll(players[i].Connection, mapIndex, slot);
                    }
                }
            }
        }

        void SendClearProjectileToAll(NetConnection p_Conn, int mapIndex, int slot)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.ClearProj);
            outMSG.Write(maps[mapIndex].Name);
            outMSG.WriteVariableInt32(slot);
            SabertoothServer.netServer.SendMessage(outMSG, p_Conn, NetDeliveryMethod.ReliableOrdered);
        }

        public void CreateProjectile(int mapIndex, int playerIndex)
        {
            int slot = FindOpenProjSlot();

            if (slot == 200) { WriteLine("Bullet max reached!"); return; }

            int projNum = players[playerIndex].mainWeapon.ProjectileNumber - 1;
            int damage = players[playerIndex].mainWeapon.Damage + projectiles[projNum].Damage;
            m_MapProj[slot] = new MapProj();
            m_MapProj[slot].Name = projectiles[projNum].Name;
            m_MapProj[slot].Sprite = projectiles[projNum].Sprite;
            m_MapProj[slot].Type = projectiles[projNum].Type;
            m_MapProj[slot].Speed = projectiles[projNum].Speed; 
            m_MapProj[slot].Damage = damage;
            m_MapProj[slot].Range = projectiles[projNum].Range;
            m_MapProj[slot].X = (players[playerIndex].X + 12);
            m_MapProj[slot].Y = (players[playerIndex].Y + 9);
            m_MapProj[slot].Owner = playerIndex;
            m_MapProj[slot].Direction = players[playerIndex].AimDirection;

            for (int i = 0; i < 5; i++)
            {
                if (players[i].Connection != null && players[i].Map == mapIndex)
                {
                    SendNewProjectileToAll(players[i].Connection, mapIndex, slot, projNum);
                }
            }
        }

        void SendNewProjectileToAll(NetConnection p_Conn, int mapIndex, int slot, int projNum)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.CreateProj);
            outMSG.WriteVariableInt32(slot);
            outMSG.WriteVariableInt32(m_MapProj[slot].projNum);
            outMSG.WriteVariableInt32(m_MapProj[slot].X);
            outMSG.WriteVariableInt32(m_MapProj[slot].Y);
            outMSG.WriteVariableInt32(m_MapProj[slot].Direction);            
            SabertoothServer.netServer.SendMessage(outMSG, p_Conn, NetDeliveryMethod.ReliableOrdered);
        }
    }

    [Serializable()]
    public class MapNpc
    {
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
        public bool IsSpawned;
        public bool DidMove;
        public int Target;
        public double s_LastPoint;
        public int spawnTick;
        public int SpawnX;
        public int SpawnY;

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
                        if (players[p].Connection != null && players[p].Name != null)
                        {
                            int s_PlayerX = players[p].X + 12;
                            int s_PlayerY = players[p].Y + 9;
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

                    if ((X + Range) < (players[Target].X + 12) || (X - Range) > (players[Target].X + 12)) { goto case (int)BehaviorType.Friendly; }
                    if ((Y + Range) < (players[Target].Y + 9) || (Y - Range) > (players[Target].Y + 9)) { goto case (int)BehaviorType.Friendly; }

                    if (X != players[Target].X)
                    {
                        if (X > players[Target].X + 12 && X > 0)
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
                        else if (X < players[Target].X + 12 && X < 50)
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
                        if (Y > players[Target].Y + 9 && Y > 0)
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
                        else if (Y < players[Target].Y + 9 && Y < 50)
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

                    if (X == players[Target].X + 12 && Y == players[Target].Y + 9)
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
        public int projNum { get; set; }

        public MapProj() { }

        public MapProj(string name, int x, int y, int projnum)
        {
            Name = name;
            X = x;
            Y = y;
            projNum = projnum;
        }
    }

    [Serializable()]
    public class MapItem
    {
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
        Chest
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
