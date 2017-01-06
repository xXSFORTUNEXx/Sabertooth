using Microsoft.VisualBasic;
using System;
using System.IO;
using static Microsoft.VisualBasic.Interaction;
using static System.Console;
using Lidgren.Network;
using static System.Environment;

namespace Server.Classes
{
    class Map
    {
        public string Name { get; set; }

        public Tile[,] Ground = new Tile[50, 50];
        public Tile[,] Mask = new Tile[50, 50];
        public Tile[,] Fringe = new Tile[50, 50];
        public Tile[,] MaskA = new Tile[50, 50];
        public Tile[,] FringeA = new Tile[50, 50];

        public MapNpc[] m_MapNpc = new MapNpc[10];
        public MapNpc[] r_MapNpc = new MapNpc[20];

        public MapProj[] mapProj = new MapProj[200];

        public int FindOpenProjSlot()
        {
            for (int i = 0; i < 200; i++)
            {
                if (mapProj[i] == null)
                {
                    return i;
                }
            }
            return 200;
        }

        public void ClearProjSlot(NetServer s_Server, Map[] s_Map, int mapIndex, int slot)
        {
            if (mapProj[slot] != null)
            {
                mapProj[slot] = null;
                NetOutgoingMessage outMSG = s_Server.CreateMessage();
                outMSG.Write((byte)PacketTypes.ClearProj);
                outMSG.Write(s_Map[mapIndex].Name);
                outMSG.WriteVariableInt32(slot);                
                s_Server.SendToAll(outMSG, NetDeliveryMethod.ReliableOrdered);
            }
        }

        public void CreateProjectile(NetServer s_Server, Player[] s_Player, int playerIndex)
        {
            int slot = FindOpenProjSlot();

            if (slot == 200) { WriteLine("Bullet max reached!"); return; }

            //For testing purposes
            mapProj[slot] = new MapProj();
            mapProj[slot].Name = "test";
            mapProj[slot].X = (s_Player[playerIndex].X + 12);
            mapProj[slot].Y = (s_Player[playerIndex].Y + 9);
            mapProj[slot].Direction = s_Player[playerIndex].AimDirection;
            mapProj[slot].Speed = 250;
            mapProj[slot].Owner = playerIndex;
            mapProj[slot].Sprite = 1;
            mapProj[slot].Type = (int)ProjType.Bullet;
            //im setting this locally, dont really think the client needs it
            mapProj[slot].Damage = s_Player[playerIndex].mainWeapon.Damage;

            SendNewProjectileToAll(s_Server, slot);
        }

        void SendNewProjectileToAll(NetServer s_Server, int slot)
        {
            NetOutgoingMessage outMSG = s_Server.CreateMessage();
            outMSG.Write((byte)PacketTypes.CreateProj);
            outMSG.WriteVariableInt32(slot);
            outMSG.Write(Name);
            outMSG.Write(mapProj[slot].Name);
            outMSG.WriteVariableInt32(mapProj[slot].X);
            outMSG.WriteVariableInt32(mapProj[slot].Y);
            outMSG.WriteVariableInt32(mapProj[slot].Direction);
            outMSG.WriteVariableInt32(mapProj[slot].Speed);
            outMSG.WriteVariableInt32(mapProj[slot].Owner);
            outMSG.WriteVariableInt32(mapProj[slot].Sprite);
            outMSG.WriteVariableInt32(mapProj[slot].Type);

            s_Server.SendToAll(outMSG, NetDeliveryMethod.ReliableOrdered);
        }

        public void GenerateMap(int mapNum)
        {
            LogWriter.WriteLog("Generating map #" + mapNum, "Server");

            Name = "Default";

            for (int i = 0; i < 10; i++)
            {
                m_MapNpc[i] = new MapNpc("None", 0, 0, 0);
            }

            for (int i = 0; i < 20; i++)
            {
                r_MapNpc[i] = new MapNpc("None", 0, 0, 0);
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
        }

        public void SaveMap(int mapNum)
        {
            FileStream fileStream = File.OpenWrite("Maps/Map" + mapNum + ".bin");
            BinaryWriter binaryWriter = new BinaryWriter(fileStream);
            LogWriter.WriteLog("Saving map #" + mapNum, "Server");
            binaryWriter.Write(Name + mapNum.ToString());

            for (int i = 0; i < 10; i++)
            {
                binaryWriter.Write(m_MapNpc[i].Name);
                binaryWriter.Write(m_MapNpc[i].X);
                binaryWriter.Write(m_MapNpc[i].Y);
                binaryWriter.Write(m_MapNpc[i].NpcNum);
            }

            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    //Ground
                    binaryWriter.Write(Ground[x, y].TileX);
                    binaryWriter.Write(Ground[x, y].TileY);
                    binaryWriter.Write(Ground[x, y].TileW);
                    binaryWriter.Write(Ground[x, y].TileH);
                    binaryWriter.Write(Ground[x, y].Tileset);
                    binaryWriter.Write(Ground[x, y].Type);
                    binaryWriter.Write(Ground[x, y].SpawnNum);
                    binaryWriter.Write(Ground[x, y].SpawnAmount);
                    //Mask
                    binaryWriter.Write(Mask[x, y].TileX);
                    binaryWriter.Write(Mask[x, y].TileY);
                    binaryWriter.Write(Mask[x, y].TileW);
                    binaryWriter.Write(Mask[x, y].TileH);
                    binaryWriter.Write(Mask[x, y].Tileset);
                    //Fringe
                    binaryWriter.Write(Fringe[x, y].TileX);
                    binaryWriter.Write(Fringe[x, y].TileY);
                    binaryWriter.Write(Fringe[x, y].TileW);
                    binaryWriter.Write(Fringe[x, y].TileH);
                    binaryWriter.Write(Fringe[x, y].Tileset);

                    binaryWriter.Write(MaskA[x, y].TileX);
                    binaryWriter.Write(MaskA[x, y].TileY);
                    binaryWriter.Write(MaskA[x, y].TileW);
                    binaryWriter.Write(MaskA[x, y].TileH);
                    binaryWriter.Write(MaskA[x, y].Tileset);

                    binaryWriter.Write(FringeA[x, y].TileX);
                    binaryWriter.Write(FringeA[x, y].TileY);
                    binaryWriter.Write(FringeA[x, y].TileW);
                    binaryWriter.Write(FringeA[x, y].TileH);
                    binaryWriter.Write(FringeA[x, y].Tileset);
                }
            }

            binaryWriter.Flush();
            binaryWriter.Close();
        }

        public void LoadMap(int mapNum)
        {
            FileStream fileStream = File.OpenRead("Maps/Map" + mapNum + ".bin");
            BinaryReader binaryReader = new BinaryReader(fileStream);
            LogWriter.WriteLog("Loading map #" + mapNum, "Server");
            try
            {
                Name = binaryReader.ReadString();

                for (int i = 0; i < 10; i++)
                {
                    m_MapNpc[i] = new MapNpc();
                    m_MapNpc[i].Name = binaryReader.ReadString();
                    m_MapNpc[i].X = binaryReader.ReadInt32();
                    m_MapNpc[i].Y = binaryReader.ReadInt32();
                    m_MapNpc[i].NpcNum = binaryReader.ReadInt32();
                }

                for (int i = 0; i < 20; i++)
                {
                    r_MapNpc[i] = new MapNpc();
                    r_MapNpc[i].Name = "None";
                    r_MapNpc[i].X = 0;
                    r_MapNpc[i].Y = 0;
                    r_MapNpc[i].NpcNum = 0;
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

                        //Ground
                        Ground[x, y].TileX = binaryReader.ReadInt32();
                        Ground[x, y].TileY = binaryReader.ReadInt32();
                        Ground[x, y].TileW = binaryReader.ReadInt32();
                        Ground[x, y].TileH = binaryReader.ReadInt32();
                        Ground[x, y].Tileset = binaryReader.ReadInt32();
                        Ground[x, y].Type = binaryReader.ReadInt32();
                        Ground[x, y].SpawnNum = binaryReader.ReadInt32();
                        Ground[x, y].SpawnAmount = binaryReader.ReadInt32();
                        Ground[x, y].CurrentSpawn = 0;
                        //Mask
                        Mask[x, y].TileX = binaryReader.ReadInt32();
                        Mask[x, y].TileY = binaryReader.ReadInt32();
                        Mask[x, y].TileW = binaryReader.ReadInt32();
                        Mask[x, y].TileH = binaryReader.ReadInt32();
                        Mask[x, y].Tileset = binaryReader.ReadInt32();
                        //Fringe
                        Fringe[x, y].TileX = binaryReader.ReadInt32();
                        Fringe[x, y].TileY = binaryReader.ReadInt32();
                        Fringe[x, y].TileW = binaryReader.ReadInt32();
                        Fringe[x, y].TileH = binaryReader.ReadInt32();
                        Fringe[x, y].Tileset = binaryReader.ReadInt32();

                        MaskA[x, y].TileX = binaryReader.ReadInt32();
                        MaskA[x, y].TileY = binaryReader.ReadInt32();
                        MaskA[x, y].TileW = binaryReader.ReadInt32();
                        MaskA[x, y].TileH = binaryReader.ReadInt32();
                        MaskA[x, y].Tileset = binaryReader.ReadInt32();

                        FringeA[x, y].TileX = binaryReader.ReadInt32();
                        FringeA[x, y].TileY = binaryReader.ReadInt32();
                        FringeA[x, y].TileW = binaryReader.ReadInt32();
                        FringeA[x, y].TileH = binaryReader.ReadInt32();
                        FringeA[x, y].Tileset = binaryReader.ReadInt32();
                    }
                }
            }
            catch (Exception e)
            {
                MsgBox(e.GetType() + ": " + e.Message, MsgBoxStyle.Critical, "Error");
            }
            binaryReader.Close();
        }
    }

    class MapNpc : Npc
    {
        public int NpcNum { get; set; }

        public MapNpc() { }

        public MapNpc(string name, int x, int y, int npcnum)
        {
            Name = name;
            X = x;
            Y = y;
            npcnum = NpcNum;
        }
    }

    class MapProj : Projectile
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

    class Tile
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
        public int CurrentSpawn;

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
        }
    }

    public enum TileType
    {
        None,
        Blocked,
        NpcSpawn,
        SpawnPool,
        NpcAvoid
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
