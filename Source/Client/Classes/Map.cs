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

        public LowerMap t_Map = new LowerMap();
        public UpperMap p_Map = new UpperMap();

        public MapNpc[] m_MapNpc = new MapNpc[10];
        public MapNpc[] r_MapNpc = new MapNpc[20];

        public MapProj[] mapProj = new MapProj[200];

        public MapItem[] mapItem = new MapItem[20];

        const int Max_Tilesets = 2;
        Texture[] TileSet = new Texture[Max_Tilesets];
        Sprite Tiles = new Sprite();
        VertexArray vTiles = new VertexArray(PrimitiveType.Quads, 4);
        RenderStates rStates = new RenderStates();

        public string Name { get; set; }

        public Map()
        {
            for (int i = 0; i < Max_Tilesets; i++)
            {
                TileSet[i] = new Texture("Resources/Tilesets/" + (i + 1) + ".png");
            }
        }

        public void DrawTile(RenderWindow c_Window, int px, int py, int x, int y, int w, int h, int tileSet)
        {
            vTiles[0] = new Vertex(new Vector2f((px * 32), (py * 32)), new Vector2f(x, y));
            vTiles[1] = new Vertex(new Vector2f((px * 32) + w, (py * 32)), new Vector2f(x + w, y));
            vTiles[2] = new Vertex(new Vector2f((px * 32) + w, (py * 32) + h), new Vector2f(x + w, y + h));
            vTiles[3] = new Vertex(new Vector2f((px * 32), (py * 32) + h), new Vector2f(x, y + h));
            rStates = new RenderStates(TileSet[tileSet]);

            c_Window.Draw(vTiles, rStates);
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
                m_MapNpc[i] = new MapNpc("Cache", 0, 0, 0);
                binaryWriter.Write(m_MapNpc[i].Name);
                binaryWriter.Write(m_MapNpc[i].X);
                binaryWriter.Write(m_MapNpc[i].Y);
                binaryWriter.Write(m_MapNpc[i].npcNum);
            }

            for (int p = 0; p < 20; p++)
            {
                mapItem[p] = new MapItem("Cache", 0, 0, 0);
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

        public void LoadMap()
        {
            FileStream fileStream = File.OpenRead("Cache/Map.bin");
            BinaryReader binaryReader = new BinaryReader(fileStream);

            Name = binaryReader.ReadString();

            for (int i = 0; i < 10; i++)
            {
                m_MapNpc[i] = new MapNpc();
                m_MapNpc[i].Name = binaryReader.ReadString();
                m_MapNpc[i].X = binaryReader.ReadInt32();
                m_MapNpc[i].Y = binaryReader.ReadInt32();
                m_MapNpc[i].npcNum = binaryReader.ReadInt32();
            }

            for (int i = 0; i < 20; i++)
            {
                r_MapNpc[i] = new MapNpc("None", 0, 0, 0);
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


    class UpperMap : Drawable
    {
        VertexArray f_vertices = new VertexArray();
        VertexArray f2_vertices = new VertexArray();
        Texture TileSet = new Texture("Resources/Tilesets/1.png");

        public UpperMap()
        {
            f_vertices.PrimitiveType = PrimitiveType.Quads;
            f_vertices.Resize(50 * 50 * 32);
            f2_vertices.PrimitiveType = PrimitiveType.Quads;
            f2_vertices.Resize(50 * 50 * 32);
        }

        public void Load(Map c_Map)
        {
            if (c_Map == null) { return; }

            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    int fx = (x * 32);
                    int fy = (y * 32);
                    int index = (x + y * 50) * 4;

                    int tx = (c_Map.Fringe[x, y].tileX);
                    int ty = (c_Map.Fringe[x, y].tileY);
                    int w = (c_Map.Fringe[x, y].tileW);
                    int h = (c_Map.Fringe[x, y].tileH);

                    f_vertices[(uint)index + 0] = new Vertex(new Vector2f(fx, fy), new Vector2f(tx, ty));
                    f_vertices[(uint)index + 1] = new Vertex(new Vector2f(fx + w, fy), new Vector2f(tx + w, ty));
                    f_vertices[(uint)index + 2] = new Vertex(new Vector2f(fx + w, fy + h), new Vector2f(tx + w, ty + h));
                    f_vertices[(uint)index + 3] = new Vertex(new Vector2f(fx, fy + h), new Vector2f(tx, ty + h));

                    int mx = (c_Map.FringeA[x, y].tileX);
                    int my = (c_Map.FringeA[x, y].tileY);
                    int mw = (c_Map.FringeA[x, y].tileW);
                    int mh = (c_Map.FringeA[x, y].tileH);

                    f2_vertices[(uint)index + 0] = new Vertex(new Vector2f(fx, fy), new Vector2f(mx, my));
                    f2_vertices[(uint)index + 1] = new Vertex(new Vector2f(fx + mw, fy), new Vector2f(mx + mw, my));
                    f2_vertices[(uint)index + 2] = new Vertex(new Vector2f(fx + mw, fy + mh), new Vector2f(mx + mw, my + mh));
                    f2_vertices[(uint)index + 3] = new Vertex(new Vector2f(fx, fy + mh), new Vector2f(mx, my + mh));
                }
            }
        }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            states.Texture = TileSet;

            target.Draw(f_vertices, states);
            target.Draw(f2_vertices, states);
        }
    }

    class LowerMap : Drawable
    {
        VertexArray g_vertices = new VertexArray();
        VertexArray m_vertices = new VertexArray();
        VertexArray m2_vertices = new VertexArray();
        Texture TileSet = new Texture("Resources/Tilesets/1.png");

        public LowerMap()
        {
            g_vertices.PrimitiveType = PrimitiveType.Quads;
            g_vertices.Resize(50 * 50 * 32);
            m_vertices.PrimitiveType = PrimitiveType.Quads;
            m_vertices.Resize(50 * 50 * 32);
            m2_vertices.PrimitiveType = PrimitiveType.Quads;
            m2_vertices.Resize(50 * 50 * 32);
        }

        public void Load(Map c_Map)
        {
            if (c_Map == null) { return; }

            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    int fx = (x * 32);
                    int fy = (y * 32);
                    int index = (x + y * 50) * 4;

                    int tx = (c_Map.Ground[x, y].tileX);
                    int ty = (c_Map.Ground[x, y].tileY);
                    int w = (c_Map.Ground[x, y].tileW);
                    int h = (c_Map.Ground[x, y].tileH);

                    g_vertices[(uint)index + 0] = new Vertex(new Vector2f(fx, fy), new Vector2f(tx, ty));
                    g_vertices[(uint)index + 1] = new Vertex(new Vector2f(fx + w, fy), new Vector2f(tx + w, ty));
                    g_vertices[(uint)index + 2] = new Vertex(new Vector2f(fx + w, fy + h), new Vector2f(tx + w, ty + h));
                    g_vertices[(uint)index + 3] = new Vertex(new Vector2f(fx, fy + h), new Vector2f(tx, ty + h));

                    int mx = (c_Map.Mask[x, y].tileX);
                    int my = (c_Map.Mask[x, y].tileY);
                    int mw = (c_Map.Mask[x, y].tileW);
                    int mh = (c_Map.Mask[x, y].tileH);

                    m_vertices[(uint)index + 0] = new Vertex(new Vector2f(fx, fy), new Vector2f(mx, my));
                    m_vertices[(uint)index + 1] = new Vertex(new Vector2f(fx + mw, fy), new Vector2f(mx + mw, my));
                    m_vertices[(uint)index + 2] = new Vertex(new Vector2f(fx + mw, fy + mh), new Vector2f(mx + mw, my + mh));
                    m_vertices[(uint)index + 3] = new Vertex(new Vector2f(fx, fy + mh), new Vector2f(mx, my + mh));

                    int m2x = (c_Map.MaskA[x, y].tileX);
                    int m2y = (c_Map.MaskA[x, y].tileY);
                    int m2w = (c_Map.MaskA[x, y].tileW);
                    int m2h = (c_Map.MaskA[x, y].tileH);

                    m2_vertices[(uint)index + 0] = new Vertex(new Vector2f(fx, fy), new Vector2f(m2x, m2y));
                    m2_vertices[(uint)index + 1] = new Vertex(new Vector2f(fx + m2w, fy), new Vector2f(m2x + m2w, m2y));
                    m2_vertices[(uint)index + 2] = new Vertex(new Vector2f(fx + m2w, fy + m2h), new Vector2f(m2x + m2w, m2y + m2h));
                    m2_vertices[(uint)index + 3] = new Vertex(new Vector2f(fx, fy + m2h), new Vector2f(m2x, m2y + m2h));
                }
            }
        }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            states.Texture = TileSet;

            target.Draw(g_vertices, states);
            target.Draw(m_vertices, states);
            target.Draw(m2_vertices, states);
        }
    }

    class MapNpc : Npc
    {
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

    class MapItem : Item
    {
        public int ItemNum { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsSpawned;

        VertexArray itemPic = new VertexArray(PrimitiveType.Quads, 4);
        RenderStates rStates = new RenderStates();

        public MapItem() { }

        public MapItem(string name, int x, int y, int itemnum)
        {
            Name = name;
            X = x;
            Y = y;
            ItemNum = itemnum;
        }

        public void DrawItem(RenderWindow c_Window, Texture c_Texture)
        {
            itemPic[0] = new Vertex(new Vector2f((X * 32), (Y * 32)), new Vector2f(0, 0));
            itemPic[1] = new Vertex(new Vector2f((X * 32) + 32, (Y * 32)), new Vector2f(32, 0));
            itemPic[2] = new Vertex(new Vector2f((X * 32) + 32, (Y * 32) + 32), new Vector2f(32, 32));
            itemPic[3] = new Vertex(new Vector2f((X * 32), (Y * 32) + 32), new Vector2f(0, 32));
            rStates = new RenderStates(c_Texture);

            c_Window.Draw(itemPic, rStates);
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

    public enum TileType
    {
        None,
        Blocked,
        NpcSpawn,
        SpawnPool,
        NpcAvoid,
        MapItem
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
