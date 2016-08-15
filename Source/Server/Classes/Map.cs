﻿using Microsoft.VisualBasic;
using System;
using System.IO;
using static Microsoft.VisualBasic.Interaction;

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

        public MapNpc[] mapNpc = new MapNpc[10];

        public void GenerateMap(int mapNum)
        {
            LogWriter.WriteLog("Generating map #" + mapNum, "Server");

            Name = "Default";

            for (int i = 0; i < 10; i++)
            {
                mapNpc[i] = new MapNpc("None", 0, 0, 0);
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

                    Ground[x, y].tileX = 0;
                    Ground[x, y].tileY = 32;
                    Ground[x, y].tileW = 32;
                    Ground[x, y].tileH = 32;
                    Ground[x, y].Tileset = 0;
                    Ground[x, y].type = (int)TileType.None;
                    Ground[x, y].spawnNum = 0;

                    Mask[x, y].tileX = 0;
                    Mask[x, y].tileY = 0;
                    Mask[x, y].tileW = 32;
                    Mask[x, y].tileH = 32;
                    Mask[x, y].Tileset = 0;

                    Fringe[x, y].tileX = 0;
                    Fringe[x, y].tileY = 0;
                    Fringe[x, y].tileW = 0;
                    Fringe[x, y].tileH = 0;
                    Fringe[x, y].Tileset = 0;

                    MaskA[x, y].tileX = 0;
                    MaskA[x, y].tileY = 0;
                    MaskA[x, y].tileW = 0;
                    MaskA[x, y].tileH = 0;
                    MaskA[x, y].Tileset = 0;

                    FringeA[x, y].tileX = 0;
                    FringeA[x, y].tileY = 0;
                    FringeA[x, y].tileW = 0;
                    FringeA[x, y].tileH = 0;
                    FringeA[x, y].Tileset = 0;
                }
            }
        }

        public void SaveMap(int mapNum)
        {
            FileStream fileStream = File.OpenWrite("Maps/Map" + mapNum + ".bin");
            BinaryWriter binaryWriter = new BinaryWriter(fileStream);
            LogWriter.WriteLog("Saving map #" + mapNum, "Server");
            binaryWriter.Write(Name);

            for (int i = 0; i < 10; i++)
            {
                binaryWriter.Write(mapNpc[i].Name);
                binaryWriter.Write(mapNpc[i].X);
                binaryWriter.Write(mapNpc[i].Y);
                binaryWriter.Write(mapNpc[i].npcNum);
            }

            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    //Ground
                    binaryWriter.Write(Ground[x, y].tileX);
                    binaryWriter.Write(Ground[x, y].tileY);
                    binaryWriter.Write(Ground[x, y].tileW);
                    binaryWriter.Write(Ground[x, y].tileH);
                    binaryWriter.Write(Ground[x, y].Tileset);
                    binaryWriter.Write(Ground[x, y].type);
                    binaryWriter.Write(Ground[x, y].spawnNum);
                    //Mask
                    binaryWriter.Write(Mask[x, y].tileX);
                    binaryWriter.Write(Mask[x, y].tileY);
                    binaryWriter.Write(Mask[x, y].tileW);
                    binaryWriter.Write(Mask[x, y].tileH);
                    binaryWriter.Write(Mask[x, y].Tileset);
                    //Fringe
                    binaryWriter.Write(Fringe[x, y].tileX);
                    binaryWriter.Write(Fringe[x, y].tileY);
                    binaryWriter.Write(Fringe[x, y].tileW);
                    binaryWriter.Write(Fringe[x, y].tileH);
                    binaryWriter.Write(Fringe[x, y].Tileset);

                    binaryWriter.Write(MaskA[x, y].tileX);
                    binaryWriter.Write(MaskA[x, y].tileY);
                    binaryWriter.Write(MaskA[x, y].tileW);
                    binaryWriter.Write(MaskA[x, y].tileH);
                    binaryWriter.Write(MaskA[x, y].Tileset);

                    binaryWriter.Write(FringeA[x, y].tileX);
                    binaryWriter.Write(FringeA[x, y].tileY);
                    binaryWriter.Write(FringeA[x, y].tileW);
                    binaryWriter.Write(FringeA[x, y].tileH);
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
                    mapNpc[i] = new MapNpc();
                    mapNpc[i].Name = binaryReader.ReadString();
                    mapNpc[i].X = binaryReader.ReadInt32();
                    mapNpc[i].Y = binaryReader.ReadInt32();
                    mapNpc[i].npcNum = binaryReader.ReadInt32();
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
                        Ground[x, y].tileX = binaryReader.ReadInt32();
                        Ground[x, y].tileY = binaryReader.ReadInt32();
                        Ground[x, y].tileW = binaryReader.ReadInt32();
                        Ground[x, y].tileH = binaryReader.ReadInt32();
                        Ground[x, y].Tileset = binaryReader.ReadInt32();
                        Ground[x, y].type = binaryReader.ReadInt32();
                        Ground[x, y].spawnNum = binaryReader.ReadInt32();
                        //Mask
                        Mask[x, y].tileX = binaryReader.ReadInt32();
                        Mask[x, y].tileY = binaryReader.ReadInt32();
                        Mask[x, y].tileW = binaryReader.ReadInt32();
                        Mask[x, y].tileH = binaryReader.ReadInt32();
                        Mask[x, y].Tileset = binaryReader.ReadInt32();
                        //Fringe
                        Fringe[x, y].tileX = binaryReader.ReadInt32();
                        Fringe[x, y].tileY = binaryReader.ReadInt32();
                        Fringe[x, y].tileW = binaryReader.ReadInt32();
                        Fringe[x, y].tileH = binaryReader.ReadInt32();
                        Fringe[x, y].Tileset = binaryReader.ReadInt32();

                        MaskA[x, y].tileX = binaryReader.ReadInt32();
                        MaskA[x, y].tileY = binaryReader.ReadInt32();
                        MaskA[x, y].tileW = binaryReader.ReadInt32();
                        MaskA[x, y].tileH = binaryReader.ReadInt32();
                        MaskA[x, y].Tileset = binaryReader.ReadInt32();

                        FringeA[x, y].tileX = binaryReader.ReadInt32();
                        FringeA[x, y].tileY = binaryReader.ReadInt32();
                        FringeA[x, y].tileW = binaryReader.ReadInt32();
                        FringeA[x, y].tileH = binaryReader.ReadInt32();
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

    class MapNpc
    {
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int npcNum { get; set; }

        public MapNpc() { }

        public MapNpc(string name, int x, int y, int npcnum)
        {
            Name = name;
            X = x;
            Y = y;
            npcnum = npcNum;
        }
    }

    class Tile
    {
        public int tileX { get; set; }
        public int tileY { get; set; }
        public int tileW { get; set; }
        public int tileH { get; set; }
        public int Tileset { get; set; }

        public int type { get; set; }
        public bool flagged { get; set; }

        public int spawnNum { get; set; }

        public Tile()
        {
            tileX = 0;
            tileY = 0;
            tileW = 0;
            tileH = 0;

            Tileset = 0;
            type = (int)TileType.None;
            flagged = false;
            spawnNum = 0;
        }
    }

    public enum TileType
    {
        None,
        Blocked,
        NPCSpawn
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
