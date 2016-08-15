using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.IO;

namespace Client.Classes
{
    class Map
    {
        public Tile[,] Ground = new Tile[50, 50];
        public Tile[,] Mask = new Tile[50, 50];
        public Tile[,] Fringe = new Tile[50, 50];
        public Tile[,] MaskA = new Tile[50, 50];
        public Tile[,] FringeA = new Tile[50, 50];

        public MapNpc[] mapNpc = new MapNpc[10];

        Texture[] TileSet = new Texture[67];
        Sprite Tiles = new Sprite();

        public string Name { get; set; }

        public Map()
        {
            for (int i = 0; i < 67; i++)
            {
                TileSet[i] = new Texture("Resources/Tilesets/" + (i + 1) + ".png");
            }
        }

        public void DrawTile(RenderWindow svrWindow, Vector2f position, int x, int y, int w, int h, int tileSet)
        {
            Tiles.Texture = TileSet[tileSet];
            Tiles.TextureRect = new IntRect(x, y, w, h);
            Tiles.Position = position;

            svrWindow.Draw(Tiles);
        }

        public void SaveMap()
        {
            if (!Directory.Exists("Cache"))
            {
                Directory.CreateDirectory("Cache");
            }
            FileStream fileStream = File.OpenWrite("Cache/Map.bin");
            BinaryWriter binaryWriter = new BinaryWriter(fileStream);

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
                    //Mask2
                    binaryWriter.Write(MaskA[x, y].tileX);
                    binaryWriter.Write(MaskA[x, y].tileY);
                    binaryWriter.Write(MaskA[x, y].tileW);
                    binaryWriter.Write(MaskA[x, y].tileH);
                    binaryWriter.Write(MaskA[x, y].Tileset);
                    //Fringe2
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

        public void LoadMap()
        {
            FileStream fileStream = File.OpenRead("Cache/Map.bin");
            BinaryReader binaryReader = new BinaryReader(fileStream);

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
                    //Mask2
                    MaskA[x, y].tileX = binaryReader.ReadInt32();
                    MaskA[x, y].tileY = binaryReader.ReadInt32();
                    MaskA[x, y].tileW = binaryReader.ReadInt32();
                    MaskA[x, y].tileH = binaryReader.ReadInt32();
                    MaskA[x, y].Tileset = binaryReader.ReadInt32();
                    //Fringe2
                    FringeA[x, y].tileX = binaryReader.ReadInt32();
                    FringeA[x, y].tileY = binaryReader.ReadInt32();
                    FringeA[x, y].tileW = binaryReader.ReadInt32();
                    FringeA[x, y].tileH = binaryReader.ReadInt32();
                    FringeA[x, y].Tileset = binaryReader.ReadInt32();
                }
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
