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
        [Category("ID"), Description("Maps identification number.")]
        public int Id { get; set; }
        public bool IsInstance { get; set; }
        #endregion

        public int MaxX;
        public int MaxY;

        #region Classes
        public Tile[,] Ground;
        public Tile[,] Mask;
        public Tile[,] Fringe;
        public Tile[,] MaskA;
        public Tile[,] FringeA;
        public MapNpc[] m_MapNpc = new MapNpc[MAX_MAP_NPCS];
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
            MaxX = MAX_MAP_X;
            MaxY = MAX_MAP_Y;

            for (int i = 0; i < MAX_MAP_NPCS; i++)
            {
                m_MapNpc[i] = new MapNpc("None", 0, 0, 0);
            }

            Ground = new Tile[MaxX, MaxY];
            Mask = new Tile[MaxX, MaxY];
            MaskA = new Tile[MaxX, MaxY];
            Fringe = new Tile[MaxX, MaxY];
            FringeA = new Tile[MaxX, MaxY];

            for (int x = 0; x < MaxX; x++)
            {
                for (int y = 0; y < MaxY; y++)
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
            string script = ReadAllText("SQL Data Scripts/Insert_Map.sql");
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                using (var cmd = new SqlCommand(script, sql))
                {
                    byte[] m_Npc = ToByteArray(m_MapNpc);                    
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
                    cmd.Parameters.Add(new SqlParameter("@maxx", System.Data.SqlDbType.Int)).Value = MaxX;
                    cmd.Parameters.Add(new SqlParameter("@maxy", System.Data.SqlDbType.Int)).Value = MaxY;
                    cmd.Parameters.Add(new SqlParameter("@npc", System.Data.SqlDbType.VarBinary)).Value = m_Npc;                    
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
            string script = ReadAllText("SQL Data Scripts/Save_Map.sql");
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                using (var cmd = new SqlCommand(script, sql))
                {
                    byte[] m_Npc = ToByteArray(m_MapNpc);                    
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
                    cmd.Parameters.Add(new SqlParameter("@maxx", System.Data.SqlDbType.Int)).Value = MaxX;
                    cmd.Parameters.Add(new SqlParameter("@maxy", System.Data.SqlDbType.Int)).Value = MaxY;
                    cmd.Parameters.Add(new SqlParameter("@npc", System.Data.SqlDbType.VarBinary)).Value = m_Npc;                    
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

            for (int m = 0; m < MAX_MAP_NPCS; m++)
            {
                m_MapNpc[m] = new MapNpc();
            }

            int i = 0;
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            string script = ReadAllText("SQL Data Scripts/Load_Map.sql");
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
                            Id = ToInt32(reader[i]); i += 1;
                            Name = reader[i].ToString(); i += 1;
                            Revision = ToInt32(reader[i]); i += 1;
                            TopMap = ToInt32(reader[i]); i += 1;
                            BottomMap = ToInt32(reader[i]); i += 1;
                            LeftMap = ToInt32(reader[i]); i += 1;
                            RightMap = ToInt32(reader[i]); i += 1;
                            Brightness = ToInt32(reader[i]); i += 1;
                            MaxX = ToInt32(reader[i]); i += 1;
                            MaxY = ToInt32(reader[i]); i += 1;

                            Ground = new Tile[MaxX, MaxY];
                            Mask = new Tile[MaxX, MaxY];
                            MaskA = new Tile[MaxX, MaxY];
                            Fringe = new Tile[MaxX, MaxY];
                            FringeA = new Tile[MaxX, MaxY];

                            byte[] buffer;
                            object load;

                            buffer = (byte[])reader[i]; i += 1;
                            load = ByteArrayToObject(buffer);
                            m_MapNpc = (MapNpc[])load;

                            buffer = (byte[])reader[i]; i += 1;
                            load = ByteArrayToObject(buffer);
                            Ground = (Tile[,])load;

                            buffer = (byte[])reader[i]; i += 1;
                            load = ByteArrayToObject(buffer);
                            Mask = (Tile[,])load;

                            buffer = (byte[])reader[i]; i += 1;
                            load = ByteArrayToObject(buffer);
                            MaskA = (Tile[,])load;

                            buffer = (byte[])reader[i]; i += 1;
                            load = ByteArrayToObject(buffer);
                            Fringe = (Tile[,])load;

                            buffer = (byte[])reader[i];
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
                command = "SELECT Name FROM Maps WHERE ID=@id";
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

        public void RestructureMap(int newx, int newy, int oldx, int oldy)
        {
            //Create temp arrays to hold what data we already have
            Tile[,] tGround = new Tile[oldx, oldy];
            Tile[,] tMask = new Tile[oldx, oldy];
            Tile[,] tMaskA = new Tile[oldx, oldy];
            Tile[,] tFringe = new Tile[oldx, oldy];
            Tile[,] tFringeA = new Tile[oldx, oldy];

            //Set the temp data to the current data
            for (int x = 0; x < oldx; x++)
            {
                for (int y = 0; y < oldy; y++)
                {
                    tGround[x, y] = Ground[x, y];
                    tMask[x, y] = Mask[x, y];
                    tMaskA[x, y] = MaskA[x, y];
                    tFringe[x, y] = Fringe[x, y];
                    tFringeA[x, y] = FringeA[x, y];
                }
            }

            //Adjust the new max length
            MaxX = newx;
            MaxY = newy;

            //Reset the max length of the array to fit the new length
            Ground = new Tile[MaxX, MaxY];
            Mask = new Tile[MaxX, MaxY];
            MaskA = new Tile[MaxX, MaxY];
            Fringe = new Tile[MaxX, MaxY];
            FringeA = new Tile[MaxX, MaxY];

            //Create all arrays
            for (int x = 0; x < newx; x++)
            {
                for (int y = 0; y < newy; y++)
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
            

            //Offload the old data
            if (newx > oldx && newy > oldy)
            {
                for (int x = 0; x < oldx; x++)
                {
                    for (int y = 0; y < oldy; y++)
                    {
                        Ground[x, y] = tGround[x, y];
                        Mask[x, y] = tMask[x, y];
                        MaskA[x, y] = tMaskA[x, y];
                        Fringe[x, y] = tFringe[x, y];
                        FringeA[x, y] = tFringeA[x, y];
                    }
                }
            }
            else
            {
                for (int x = 0; x < newx; x++)
                {
                    for (int y = 0; y < newy; y++)
                    {
                        Ground[x, y] = tGround[x, y];
                        Mask[x, y] = tMask[x, y];
                        MaskA[x, y] = tMaskA[x, y];
                        Fringe[x, y] = tFringe[x, y];
                        FringeA[x, y] = tFringeA[x, y];
                    }
                }
            }

            //Save the new map
            SaveMapInDatabase(Id);
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
    public class MapNpc : Npc
    {
        public int NpcNum { get; set; }
        public int aiTick;

        public MapNpc() { }

        public MapNpc(string name, int x, int y, int npcnum)
        {
            Name = name;
            X = x;
            Y = y;
            npcnum = NpcNum;
        }

        public override int DamageNpc(Player s_Player, Map s_Map, Spell s_Spell, int attackType)
        {
            Random rnd = new Random();
            int damage;
            if (attackType == 0)
            {
                damage = rnd.Next((s_Player.MainHand.Damage / 2), (s_Player.MainHand.Damage)) + rnd.Next(0, s_Player.OffHand.Damage);
            }
            else
            {
                damage = s_Spell.Vital; 
            }

            Health -= damage;

            if (Health <= 0)
            {
                IsSpawned = false;
                Health = MaxHealth;
                spawnTick = TickCount;
                s_Player.Experience += Exp;
                s_Player.Wallet += Money;
                s_Player.CheckPlayerLevelUp();
                //s_Player.SavePlayerToDatabase();                
            }

            return damage;
        }

        public override void AttackPlayer(int index)
        {
            players[index].Health -= Damage;

            if (players[index].Health <= 0)
            {
                players[index].Health = players[index].MaxHealth;
                players[index].X = 0;
                players[index].Y = 0;
                HandleData.SendPlayers();
                string deathMsg = players[index].Name + " has been killed by " + Name + ".";
                HandleData.SendServerMessageToAll(deathMsg);
            }
            else
            {
                HandleData.SendUpdateHealthData(index, players[index].Health, Damage);
            }
        }

        public override void NpcAI(int s_CanMove, int s_Direction, int mapNum)
        {
            DidMove = false;

            switch (Behavior)
            {
                case (int)BehaviorType.Friendly:
                case (int)BehaviorType.Passive:

                    if (s_CanMove > 80)
                    {
                        switch (s_Direction)
                        {
                            case (int)Directions.Down:
                                if (Y < maps[mapNum].MaxY - 1)
                                {
                                    if (maps[mapNum].Ground[X, Y + 1].Type == (int)TileType.Blocked || maps[mapNum].Ground[X, Y + 1].Type == (int)TileType.NpcAvoid)
                                    {
                                        Direction = (int)Directions.Down;
                                        DidMove = true;
                                        return;
                                    }
                                    for (int i = 0; i < MAX_MAP_NPCS; i++)
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
                                    for (int p = 0; p < MAX_PLAYERS; p++)
                                    {
                                        if (players[p].Connection != null)
                                        {
                                            if ((Y + 1) == (players[p].Y + OFFSET_Y) && X == (players[p].X + OFFSET_X))
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
                                    for (int i = 0; i < MAX_MAP_NPCS; i++)
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
                                    for (int p = 0; p < MAX_PLAYERS; p++)
                                    {
                                        if (players[p].Connection != null)
                                        {
                                            if ((X - 1) == (players[p].X + OFFSET_X) && Y == (players[p].Y + OFFSET_Y))
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
                                if (X < maps[mapNum].MaxX - 1)
                                {
                                    if (maps[mapNum].Ground[X + 1, Y].Type == (int)TileType.Blocked || maps[mapNum].Ground[X + 1, Y].Type == (int)TileType.NpcAvoid)
                                    {
                                        Direction = (int)Directions.Right;
                                        DidMove = true;
                                        return;
                                    }
                                    for (int i = 0; i < MAX_MAP_NPCS; i++)
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
                                    for (int p = 0; p < MAX_PLAYERS; p++)
                                    {
                                        if (players[p].Connection != null)
                                        {
                                            if ((X + 1) == (players[p].X + OFFSET_X) && Y == (players[p].Y + OFFSET_Y))
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
                                    for (int i = 0; i < MAX_MAP_NPCS; i++)
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
                                    for (int p = 0; p < MAX_PLAYERS; p++)
                                    {
                                        if (players[p].Connection != null)
                                        {
                                            if ((Y - 1) == (players[p].Y + OFFSET_Y) && X == (players[p].X + OFFSET_X))
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

                case (int)BehaviorType.Aggressive:

                    //lets check for players to persue
                    for (int p = 0; p < MAX_PLAYERS; p++)
                    {
                        //check for players online and on the same map
                        if (players[p].Connection != null && players[p].Name != null && mapNum == players[p].Map)
                        {
                            //This checks the distance between two points and really used when we have multiple targets
                            //it checks to see who is closer, this will get changed but I was proud that I could make it work
                            int s_PlayerX = players[p].X + OFFSET_X;
                            int s_PlayerY = players[p].Y + OFFSET_Y;
                            double s_DisX = X - s_PlayerX;
                            double s_DisY = Y - s_PlayerY;
                            double s_Final = s_DisX * s_DisX + s_DisY * s_DisY;
                            double s_DisPoint = Math.Sqrt(s_Final);

                            //Checks to see if the new point is closer than the last
                            if (s_DisPoint < s_LastPoint)
                            {
                                Target = p; //if so its our new target BBY
                            }

                            s_LastPoint = s_DisPoint;   //mark the new target as our last target so we can check if someone is closer than they are
                        }
                    }

                    //Check to see if the target we had is in range, if not then we just wander like a gud passive should
                    if ((X + Range) < (players[Target].X + OFFSET_X) || (X - Range) > (players[Target].X + OFFSET_X)) { goto case (int)BehaviorType.Passive; }
                    if ((Y + Range) < (players[Target].Y + OFFSET_Y) || (Y - Range) > (players[Target].Y + OFFSET_Y)) { goto case (int)BehaviorType.Passive; }

                    if (X != players[Target].X + OFFSET_X)
                    {
                        if (X > players[Target].X + OFFSET_X && X > 0)
                        {
                            //Check if there is a blocked tile or one to avoid infront of our path (Needs proper pathfinder)
                            if (maps[mapNum].Ground[X - 1, Y].Type == (int)TileType.Blocked || 
                                maps[mapNum].Ground[X - 1, Y].Type == (int)TileType.NpcAvoid)
                            {
                                Direction = (int)Directions.Left;
                                DidMove = true;
                                return;
                            }

                            //Check if the player is infront of us, if so then we can attack then because fuck them    
                            if (((X - 1) == (players[Target].X + OFFSET_X) && Y == (players[Target].Y + OFFSET_Y)))
                            {
                                AttackPlayer(Target);
                                Direction = (int)Directions.Left;
                                DidMove = true;
                                return;
                            }

                            //If all else fails then we move closer to our target
                            Direction = (int)Directions.Left;
                            X -= 1;
                            DidMove = true;
                        }
                        else if (X < players[Target].X + OFFSET_X && X < maps[mapNum].MaxX)
                        {
                            //Check if there is a blocked tile or one to avoid infront of our path (Needs proper pathfinder)
                            if (maps[mapNum].Ground[X + 1, Y].Type == (int)TileType.Blocked || 
                                maps[mapNum].Ground[X + 1, Y].Type == (int)TileType.NpcAvoid)
                            {
                                Direction = (int)Directions.Right;
                                DidMove = true;
                                return;
                            }

                            //Check if the player is infront of us, if so then we can attack then because fuck them    
                            if (((X + 1) == (players[Target].X + OFFSET_X) && Y == (players[Target].Y + OFFSET_Y)))
                            {
                                AttackPlayer(Target);
                                Direction = (int)Directions.Right;
                                DidMove = true;
                                return;
                            }

                            //If all else fails then we move closer to our target
                            Direction = (int)Directions.Right;
                            X += 1;
                            DidMove = true;
                        }
                    }

                    if (Y != players[Target].Y + OFFSET_Y)
                    {
                        if (Y > players[Target].Y + OFFSET_Y && Y > 0)
                        {
                            //Check if there is a blocked tile or one to avoid infront of our path (Needs proper pathfinder)
                            if (maps[mapNum].Ground[X, Y - 1].Type == (int)TileType.Blocked || 
                                maps[mapNum].Ground[X, Y - 1].Type == (int)TileType.NpcAvoid)
                            {
                                Direction = (int)Directions.Up;
                                DidMove = true;
                                return;
                            }

                            //Check if the player is infront of us, if so then we can attack then because fuck them                            
                            if (((Y - 1) == (players[Target].Y + OFFSET_Y) && X == (players[Target].X + OFFSET_X)))
                            {
                                AttackPlayer(Target);
                                Direction = (int)Directions.Up;
                                DidMove = true;
                                return;
                            }

                            //If all else fails then we move closer to our target
                            Direction = (int)Directions.Up;
                            Y -= 1;
                            DidMove = true;
                        }
                        else if (Y < players[Target].Y + OFFSET_Y && Y < maps[mapNum].MaxY - 1)
                        {
                            //Check if there is a blocked tile or one to avoid infront of our path (Needs proper pathfinder)
                            if (maps[mapNum].Ground[X, Y + 1].Type == (int)TileType.Blocked || 
                                maps[mapNum].Ground[X, Y + 1].Type == (int)TileType.NpcAvoid)
                            {
                                Direction = (int)Directions.Down;
                                DidMove = true;
                                return;
                            }

                            //Check if the player is infront of us, if so then we can attack then because fuck them      
                            if (((Y + 1) == (players[Target].Y + OFFSET_Y) && X == (players[Target].X + OFFSET_X)))
                            {
                                AttackPlayer(Target);
                                Direction = (int)Directions.Down;
                                DidMove = true;
                                return;
                            }

                            //If all else fails then we move closer to our target
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
        NpcAvoid,
        Chest,
        Warp,
        Animation
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
