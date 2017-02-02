using Microsoft.Win32.SafeHandles;
using SFML.Graphics;
using SFML.System;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Server.Classes;

namespace Editor.Classes
{
    class Map : IDisposable
    {
        public Tile[,] Ground = new Tile[50, 50];
        public Tile[,] Mask = new Tile[50, 50];
        public Tile[,] Fringe = new Tile[50, 50];
        public Tile[,] MaskA = new Tile[50, 50];
        public Tile[,] FringeA = new Tile[50, 50];

        public MapNpc[] mapNpc = new MapNpc[10];

        public MapItem[] mapItem = new MapItem[20];

        const int Max_Tilesets = 2;
        Texture[] TileSet = new Texture[Max_Tilesets];
        Sprite Tiles = new Sprite();

        public string Name { get; set; }

        bool disposed = false;
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
            }

            for (int i = 0; i < Max_Tilesets; i++)
            {
                TileSet[i].Dispose();
            }
            TileSet = null;
            Tiles.Dispose();
            Tiles = null;

            disposed = true;
        }

        public Map()
        {
            for (int i = 0; i < Max_Tilesets; i++)
            {
                TileSet[i] = new Texture("Resources/Tilesets/" + (i + 1) + ".png");
            }
        }

        public void DrawTile(RenderWindow e_Window, Vector2f position, int x, int y, int w, int h, int tileSet)
        {
            Tiles.Texture = TileSet[tileSet];
            Tiles.TextureRect = new IntRect(x, y, w, h);
            Tiles.Position = position;

            e_Window.Draw(Tiles);
        }

        public void LoadMap()
        {
            OpenFileDialog loadMapDialog = new OpenFileDialog();

            loadMapDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + "Maps";
            loadMapDialog.Filter = "Bin Files (*.bin)|*.bin";
            loadMapDialog.FilterIndex = 1;
            loadMapDialog.ShowDialog();

            try
            {
                if (loadMapDialog.FileName != null)
                {
                    FileStream fileStream = File.OpenRead(loadMapDialog.FileName);
                    BinaryReader binaryReader = new BinaryReader(fileStream);

                    Name = binaryReader.ReadString();

                    for (int i = 0; i < 10; i++)
                    {
                        mapNpc[i] = new MapNpc();
                        mapNpc[i].Name = binaryReader.ReadString();
                        mapNpc[i].X = binaryReader.ReadInt32();
                        mapNpc[i].Y = binaryReader.ReadInt32();
                        mapNpc[i].NpcNum = binaryReader.ReadInt32();
                    }

                    for (int p = 0; p < 20; p++)
                    {
                        mapItem[p] = new MapItem();
                        mapItem[p].Name = binaryReader.ReadString();
                        mapItem[p].X = binaryReader.ReadInt32();
                        mapItem[p].Y = binaryReader.ReadInt32();
                        mapItem[p].ItemNum = binaryReader.ReadInt32();
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
                            Ground[x, y].SpawnNum = binaryReader.ReadInt32();
                            Ground[x, y].SpawnAmount = binaryReader.ReadInt32();
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
                            //Mask 2
                            MaskA[x, y].tileX = binaryReader.ReadInt32();
                            MaskA[x, y].tileY = binaryReader.ReadInt32();
                            MaskA[x, y].tileW = binaryReader.ReadInt32();
                            MaskA[x, y].tileH = binaryReader.ReadInt32();
                            MaskA[x, y].Tileset = binaryReader.ReadInt32();
                            //Fringe 2
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
            catch (Exception ex)
            {
                MessageBox.Show("Can't load map file!" + ex.Message);
            }
        }

        public void SaveMap()
        {
            SaveFileDialog saveMapDialog = new SaveFileDialog();

            saveMapDialog.Filter = "Bin Files (*.bin)|*.bin";
            saveMapDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + "Maps";
            saveMapDialog.FilterIndex = 1;
            saveMapDialog.ShowDialog();

            if (saveMapDialog.FileName != "")
            {
                FileStream mapStream = File.OpenWrite(saveMapDialog.FileName);
                BinaryWriter binaryWriter = new BinaryWriter(mapStream);

                binaryWriter.Write(Name);

                for (int i = 0; i < 10; i++)
                {
                    binaryWriter.Write(mapNpc[i].Name);
                    binaryWriter.Write(mapNpc[i].X);
                    binaryWriter.Write(mapNpc[i].Y);
                    binaryWriter.Write(mapNpc[i].NpcNum);
                }

                for (int p = 0; p < 20; p++)
                {
                    binaryWriter.Write(mapItem[p].Name);
                    binaryWriter.Write(mapItem[p].X);
                    binaryWriter.Write(mapItem[p].Y);
                    binaryWriter.Write(mapItem[p].ItemNum);
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
                        binaryWriter.Write(Ground[x, y].SpawnNum);
                        binaryWriter.Write(Ground[x, y].SpawnAmount);
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
        }

        public void CreateDefaultMap(Map newMap)
        {
            Console.WriteLine("Creating default map...");   //let the debugging know whats the scoop

            newMap.Name = "Home";   //name our map
            for (int i = 0; i < 10; i++)
            {
                mapNpc[i] = new MapNpc();
                mapNpc[i].Name = "None";
                mapNpc[i].X = 0;
                mapNpc[i].Y = 0;
                mapNpc[i].NpcNum = 0;
            }
            for (int p = 0; p < 20; p++)
            {
                mapItem[p] = new MapItem();
                mapItem[p].Name = "None";
                mapItem[p].X = 0;
                mapItem[p].Y = 0;
                mapItem[p].ItemNum = 0;
            }

            //Create all of the tiles with new classes and make sure they all have values of 0
            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    newMap.Ground[x, y] = new Tile();
                    newMap.Mask[x, y] = new Tile();
                    newMap.Fringe[x, y] = new Tile();
                    newMap.MaskA[x, y] = new Tile();
                    newMap.FringeA[x, y] = new Tile();

                    //Ground
                    newMap.Ground[x, y].tileX = 0;
                    newMap.Ground[x, y].tileY = 0;
                    newMap.Ground[x, y].tileW = 0;
                    newMap.Ground[x, y].tileH = 0;
                    newMap.Ground[x, y].Tileset = 0;
                    newMap.Ground[x, y].type = 0;
                    newMap.Ground[x, y].SpawnNum = 0;
                    newMap.Ground[x, y].SpawnAmount = 0;
                    //Mask
                    newMap.Mask[x, y].tileX = 0;
                    newMap.Mask[x, y].tileY = 0;
                    newMap.Mask[x, y].tileW = 0;
                    newMap.Mask[x, y].tileH = 0;
                    newMap.Mask[x, y].Tileset = 0;
                    //Fringe
                    newMap.Fringe[x, y].tileX = 0;
                    newMap.Fringe[x, y].tileY = 0;
                    newMap.Fringe[x, y].tileW = 0;
                    newMap.Fringe[x, y].tileH = 0;
                    newMap.Fringe[x, y].Tileset = 0;

                    newMap.MaskA[x, y].tileX = 0;
                    newMap.MaskA[x, y].tileY = 0;
                    newMap.MaskA[x, y].tileW = 0;
                    newMap.MaskA[x, y].tileH = 0;
                    newMap.MaskA[x, y].Tileset = 0;

                    newMap.FringeA[x, y].tileX = 0;
                    newMap.FringeA[x, y].tileY = 0;
                    newMap.FringeA[x, y].tileW = 0;
                    newMap.FringeA[x, y].tileH = 0;
                    newMap.FringeA[x, y].Tileset = 0;
                }
            }

            SaveDefaultMap(newMap);   //save what we have made so it can be loaded once we are done here
        }

        public void SaveDefaultMap(Map mapNum)
        {
            FileStream fileStream = File.OpenWrite("Maps/Map0.bin");
            BinaryWriter binaryWriter = new BinaryWriter(fileStream);

            binaryWriter.Write(mapNum.Name);

            for (int i = 0; i < 10; i++)
            {
                binaryWriter.Write(mapNpc[i].Name);
                binaryWriter.Write(mapNpc[i].X);
                binaryWriter.Write(mapNpc[i].Y);
                binaryWriter.Write(mapNpc[i].NpcNum);
            }

            for (int p = 0; p < 20; p++)
            {
                binaryWriter.Write(mapItem[p].Name);
                binaryWriter.Write(mapItem[p].X);
                binaryWriter.Write(mapItem[p].Y);
                binaryWriter.Write(mapItem[p].ItemNum);
            }

            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    //Ground
                    binaryWriter.Write(mapNum.Ground[x, y].tileX);
                    binaryWriter.Write(mapNum.Ground[x, y].tileY);
                    binaryWriter.Write(mapNum.Ground[x, y].tileW);
                    binaryWriter.Write(mapNum.Ground[x, y].tileH);
                    binaryWriter.Write(mapNum.Ground[x, y].Tileset);
                    binaryWriter.Write(mapNum.Ground[x, y].type);
                    binaryWriter.Write(mapNum.Ground[x, y].SpawnNum);
                    binaryWriter.Write(mapNum.Ground[x, y].SpawnAmount);
                    //Mask
                    binaryWriter.Write(mapNum.Mask[x, y].tileX);
                    binaryWriter.Write(mapNum.Mask[x, y].tileY);
                    binaryWriter.Write(mapNum.Mask[x, y].tileW);
                    binaryWriter.Write(mapNum.Mask[x, y].tileH);
                    binaryWriter.Write(mapNum.Mask[x, y].Tileset);
                    //Fringe
                    binaryWriter.Write(mapNum.Fringe[x, y].tileX);
                    binaryWriter.Write(mapNum.Fringe[x, y].tileY);
                    binaryWriter.Write(mapNum.Fringe[x, y].tileW);
                    binaryWriter.Write(mapNum.Fringe[x, y].tileH);
                    binaryWriter.Write(mapNum.Fringe[x, y].Tileset);

                    binaryWriter.Write(mapNum.MaskA[x, y].tileX);
                    binaryWriter.Write(mapNum.MaskA[x, y].tileY);
                    binaryWriter.Write(mapNum.MaskA[x, y].tileW);
                    binaryWriter.Write(mapNum.MaskA[x, y].tileH);
                    binaryWriter.Write(mapNum.MaskA[x, y].Tileset);

                    binaryWriter.Write(mapNum.FringeA[x, y].tileX);
                    binaryWriter.Write(mapNum.FringeA[x, y].tileY);
                    binaryWriter.Write(mapNum.FringeA[x, y].tileW);
                    binaryWriter.Write(mapNum.FringeA[x, y].tileH);
                    binaryWriter.Write(mapNum.FringeA[x, y].Tileset);
                }
            }

            binaryWriter.Flush();
            binaryWriter.Close();
        }

        public void LoadDeafultMap()
        {
            FileStream fileStream = File.OpenRead("Maps/Map0.bin");
            BinaryReader binaryReader = new BinaryReader(fileStream);

            Name = binaryReader.ReadString();

            for (int i = 0; i < 10; i++)
            {
                mapNpc[i] = new MapNpc();
                mapNpc[i].Name = binaryReader.ReadString();
                mapNpc[i].X = binaryReader.ReadInt32();
                mapNpc[i].Y = binaryReader.ReadInt32();
                mapNpc[i].NpcNum = binaryReader.ReadInt32();
            }

            for (int p = 0; p < 20; p++)
            {
                mapItem[p] = new MapItem();
                mapItem[p].Name = binaryReader.ReadString();
                mapItem[p].X = binaryReader.ReadInt32();
                mapItem[p].Y = binaryReader.ReadInt32();
                mapItem[p].ItemNum = binaryReader.ReadInt32();
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
                    Ground[x, y].SpawnNum = binaryReader.ReadInt32();
                    Ground[x, y].SpawnAmount = binaryReader.ReadInt32();
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
                    //MaskA
                    MaskA[x, y].tileX = binaryReader.ReadInt32();
                    MaskA[x, y].tileY = binaryReader.ReadInt32();
                    MaskA[x, y].tileW = binaryReader.ReadInt32();
                    MaskA[x, y].tileH = binaryReader.ReadInt32();
                    MaskA[x, y].Tileset = binaryReader.ReadInt32();
                    //FringeA
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

    class MapNpc : Npc
    {
        public int NpcNum { get; set; }

        public MapNpc() { }

        public MapNpc(string name, int x, int y, int npcnum)
        {
            Name = name;
            X = x;
            Y = y;
            NpcNum = NpcNum;
        }
    }

    class MapItem : Item
    {
        public int ItemNum { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public MapItem() { }

        public MapItem(string name, int x, int y, int itemnum)
        {
            Name = name;
            X = x;
            Y = y;
            ItemNum = itemnum;
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
        public int perk { get; set; }
        public bool flagged { get; set; }

        public int SpawnNum { get; set; }
        public int SpawnAmount { get; set; }

        public Tile()
        {
            tileX = 0;
            tileY = 0;
            tileW = 0;
            tileH = 0;

            Tileset = 0;
            type = (int)TileType.None;
            flagged = false;
            SpawnNum = 0;
        }
    }
}
