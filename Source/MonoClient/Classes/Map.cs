using System.Data.SQLite;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using static System.Convert;
using static Mono_Client.Client;
using static Mono_Client.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Mono_Client
{
    public class Map
    {
        #region Properties
        public string Name { get; set; }
        public int Revision { get; set; }
        public int TopMap { get; set; }
        public int BottomMap { get; set; }
        public int LeftMap { get; set; }
        public int RightMap { get; set; }
        public int Brightness { get; set; }
        public int Id { get; set; }
        public int MaxX { get; set; }
        public int MaxY { get; set; }
        public bool IsInstance { get; set; }
        #endregion

        #region Classes
        public Tile[,] Ground;
        public Tile[,] Mask;
        public Tile[,] Fringe;
        public Tile[,] MaskA;
        public Tile[,] FringeA;
        public MapNpc[] m_MapNpc = new MapNpc[MAX_MAP_NPCS];
        public MapNpc[] r_MapNpc = new MapNpc[MAX_MAP_POOL_NPCS];
        public MapProj[] m_MapProj = new MapProj[MAX_MAP_PROJECTILES];
        public MapItem[] m_MapItem = new MapItem[MAX_MAP_ITEMS];
        public BloodSplat[] m_BloodSplats = new BloodSplat[MAX_BLOOD_SPLATS];
        #endregion

        public Map() { }

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

        public void MapDatabaseCache(int index)
        {
            bool exists = false;
            int currentRevision = -1;

            using (var conn = new SQLiteConnection("Data Source=MapCache.db;Version=3;"))
            {
                using (var cmd = new SQLiteCommand(conn))
                {
                    conn.Open();
                    try
                    {
                        cmd.CommandText = "SELECT * FROM MAPS WHERE ID = " + index;
                        using (SQLiteDataReader read = cmd.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                exists = true;
                                currentRevision = ToInt32(read["REVISION"].ToString());
                            }
                        }
                    }
                    finally
                    {
                        if (!exists)
                        {
                            byte[] m_Ground = ToByteArray(Ground);
                            byte[] m_Mask = ToByteArray(Mask);
                            byte[] m_MaskA = ToByteArray(MaskA);
                            byte[] m_Fringe = ToByteArray(Fringe);
                            byte[] m_FringeA = ToByteArray(FringeA);
                            string sql;

                            sql = "INSERT INTO MAPS (ID,NAME,REVISION,TOP,BOTTOM,LEFT,RIGHT,BRIGHTNESS,MAXX,MAXY,GROUND,MASK,MASKA,FRINGE,FRINGEA) ";
                            sql = sql + " VALUES ";
                            sql = sql + "(@id,@name,@revision,@top,@bottom,@left,@right,@brightness,@maxx,@maxy,@ground,@mask,@maska,@fringe,@fringea)";
                            cmd.CommandText = sql;
                            cmd.Parameters.Add("@id", System.Data.DbType.Int32).Value = Id;
                            cmd.Parameters.Add("@name", System.Data.DbType.String).Value = Name;
                            cmd.Parameters.Add("@revision", System.Data.DbType.Int32).Value = Revision;
                            cmd.Parameters.Add("@top", System.Data.DbType.Int32).Value = TopMap;
                            cmd.Parameters.Add("@bottom", System.Data.DbType.Int32).Value = BottomMap;
                            cmd.Parameters.Add("@left", System.Data.DbType.Int32).Value = LeftMap;
                            cmd.Parameters.Add("@right", System.Data.DbType.Int32).Value = RightMap;
                            cmd.Parameters.Add("@brightness", System.Data.DbType.Int32).Value = Brightness;
                            cmd.Parameters.Add("@maxx", System.Data.DbType.Int32).Value = MaxX;
                            cmd.Parameters.Add("@maxy", System.Data.DbType.Int32).Value = MaxY;
                            cmd.Parameters.Add("@ground", System.Data.DbType.Binary).Value = m_Ground;
                            cmd.Parameters.Add("@mask", System.Data.DbType.Binary).Value = m_Mask;
                            cmd.Parameters.Add("@maska", System.Data.DbType.Binary).Value = m_MaskA;
                            cmd.Parameters.Add("@fringe", System.Data.DbType.Binary).Value = m_Fringe;
                            cmd.Parameters.Add("@fringea", System.Data.DbType.Binary).Value = m_FringeA;
                            cmd.ExecuteNonQuery();
                        }
                        else if (Revision > currentRevision)
                        {
                            byte[] m_Ground = ToByteArray(Ground);
                            byte[] m_Mask = ToByteArray(Mask);
                            byte[] m_MaskA = ToByteArray(MaskA);
                            byte[] m_Fringe = ToByteArray(Fringe);
                            byte[] m_FringeA = ToByteArray(FringeA);
                            string sql;

                            sql = "UPDATE MAPS SET NAME = @name,REVISION = @revision,TOP = @top,BOTTOM = @bottom,LEFT = @left,RIGHT = @right,BRIGHTNESS = @brightness,MAXX = @maxx,MAXY = @maxy";
                            sql = sql + "GROUND = @ground,MASK = @mask,MASKA = @maska,FRINGE = @fringe,FRINGEA = @fringea WHERE ID = " + index;
                            cmd.CommandText = sql;
                            cmd.Parameters.Add("@name", System.Data.DbType.String).Value = Name;
                            cmd.Parameters.Add("@revision", System.Data.DbType.Int32).Value = Revision;
                            cmd.Parameters.Add("@top", System.Data.DbType.Int32).Value = TopMap;
                            cmd.Parameters.Add("@bottom", System.Data.DbType.Int32).Value = BottomMap;
                            cmd.Parameters.Add("@left", System.Data.DbType.Int32).Value = LeftMap;
                            cmd.Parameters.Add("@right", System.Data.DbType.Int32).Value = RightMap;
                            cmd.Parameters.Add("@brightness", System.Data.DbType.Int32).Value = Brightness;
                            cmd.Parameters.Add("@maxx", System.Data.DbType.Int32).Value = MaxX;
                            cmd.Parameters.Add("@maxy", System.Data.DbType.Int32).Value = MaxY;
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
        }

        public void LoadMapFromCache(int index)
        {
            for (int i = 0; i < 20; i++)
            {
                if (i < 10)
                {
                    m_MapNpc[i] = new MapNpc();
                }
                r_MapNpc[i] = new MapNpc();
                m_MapItem[i] = new MapItem();

                using (var conn = new SQLiteConnection("Data Source=Cache/MapCache.db;Version=3;"))
                {
                    using (var cmd = new SQLiteCommand(conn))
                    {
                        conn.Open();
                        cmd.CommandText = "SELECT * FROM MAPS WHERE ID = " + index;
                        using (SQLiteDataReader read = cmd.ExecuteReader(System.Data.CommandBehavior.SequentialAccess))
                        {
                            while (read.Read())
                            {
                                Id = ToInt32(read["ID"].ToString());
                                Name = read["NAME"].ToString();
                                Revision = ToInt32(read["REVISION"].ToString());
                                TopMap = ToInt32(read["TOP"].ToString());
                                BottomMap = ToInt32(read["BOTTOM"].ToString());
                                LeftMap = ToInt32(read["LEFT"].ToString());
                                RightMap = ToInt32(read["RIGHT"].ToString());
                                Brightness = ToInt32(read["BRIGHTNESS"].ToString());
                                MaxX = ToInt32(read["MAXX"].ToString());
                                MaxY = ToInt32(read["MAXY"].ToString());

                                byte[] buffer;
                                object load;

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
        }
        #endregion

        #region Graphics
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            int minX;
            int minY;
            int maxX;
            int maxY;
            if (SCREEN_WIDTH == 1024 && SCREEN_HEIGHT == 768)
            {
                minX = (players[HandleData.myIndex].X + 16) - 16;
                minY = (players[HandleData.myIndex].Y + 11) - 11;
                maxX = (players[HandleData.myIndex].X + 16) + 17;
                maxY = (players[HandleData.myIndex].Y + 11) + 16;
            }
            else
            {
                minX = (players[HandleData.myIndex].X + 12) - 12;
                minY = (players[HandleData.myIndex].Y + 9) - 9;
                maxX = (players[HandleData.myIndex].X + 12) + 13;
                maxY = (players[HandleData.myIndex].Y + 9) + 11;
            }

            for (int x = minX; x < maxX; x++)
            {
                for (int y = minY; y < maxY; y++)
                {
                    if (x >= 0 && y >= 0 && x < MaxX && y < MaxY)
                    {
                        int fx = (x * 32);
                        int fy = (y * 32);
                        int tx, ty, w, h, set;

                        tx = (Ground[x, y].TileX);
                        ty = (Ground[x, y].TileY);
                        w = (Ground[x, y].TileW);
                        h = (Ground[x, y].TileH);
                        set = (Ground[x, y].Tileset);
                        spriteBatch.Draw(TileSet[set], new Rectangle(fx, fy, w, h), new Rectangle(tx, ty, w, h), Color.White);

                        if (Mask[x, y].TileX > 0 || Mask[x, y].TileY > 0)
                        {
                            tx = (Mask[x, y].TileX);
                            ty = (Mask[x, y].TileY);
                            w = (Mask[x, y].TileW);
                            h = (Mask[x, y].TileH);
                            set = (Mask[x, y].Tileset);
                            spriteBatch.Draw(TileSet[set], new Rectangle(fx, fy, w, h), new Rectangle(tx, ty, w, h), Color.White);
                        }

                        if (MaskA[x, y].TileX > 0 || MaskA[x, y].TileY > 0)
                        {
                            tx = (MaskA[x, y].TileX);
                            ty = (MaskA[x, y].TileY);
                            w = (MaskA[x, y].TileW);
                            h = (MaskA[x, y].TileH);
                            set = (MaskA[x, y].Tileset);
                            spriteBatch.Draw(TileSet[set], new Rectangle(fx, fy, w, h), new Rectangle(tx, ty, w, h), Color.White);
                        }
                    }
                }
            }
        }

        public void DrawFringe(SpriteBatch spriteBatch)
        {
            int minX;
            int minY;
            int maxX;
            int maxY;
            if (SCREEN_WIDTH == 1024 && SCREEN_HEIGHT == 768)
            {
                minX = (players[HandleData.myIndex].X + 16) - 16;
                minY = (players[HandleData.myIndex].Y + 11) - 11;
                maxX = (players[HandleData.myIndex].X + 16) + 17;
                maxY = (players[HandleData.myIndex].Y + 11) + 16;
            }
            else
            {
                minX = (players[HandleData.myIndex].X + 12) - 12;
                minY = (players[HandleData.myIndex].Y + 9) - 9;
                maxX = (players[HandleData.myIndex].X + 12) + 13;
                maxY = (players[HandleData.myIndex].Y + 9) + 11;
            }

            for (int x = minX; x < maxX; x++)
            {
                for (int y = minY; y < maxY; y++)
                {
                    if (x >= 0 && y >= 0 && x < MaxX && y < MaxY)
                    {
                        int fx = (x * 32);
                        int fy = (y * 32);
                        int tx, ty, w, h, set;

                        if (Fringe[x, y].TileX > 0 || Fringe[x, y].TileY > 0)
                        {
                            tx = (Fringe[x, y].TileX);
                            ty = (Fringe[x, y].TileY);
                            w = (Fringe[x, y].TileW);
                            h = (Fringe[x, y].TileH);
                            set = (Fringe[x, y].Tileset);
                            spriteBatch.Draw(TileSet[set], new Rectangle(fx, fy, w, h), new Rectangle(tx, ty, w, h), Color.White);
                        }

                        if (FringeA[x, y].TileX > 0 || FringeA[x, y].TileY > 0)
                        {
                            tx = (FringeA[x, y].TileX);
                            ty = (FringeA[x, y].TileY);
                            w = (FringeA[x, y].TileW);
                            h = (FringeA[x, y].TileH);
                            set = (FringeA[x, y].Tileset);
                            spriteBatch.Draw(TileSet[set], new Rectangle(fx, fy, w, h), new Rectangle(tx, ty, w, h), Color.White);
                        }
                    }
                }
            }
        }

        public void DrawChest(int x, int y, bool empty, SpriteBatch spriteBatch)
        {
            int cX = 0;
            if (empty) { cX = 32; }
            spriteBatch.Draw(chestSprite, new Rectangle((x*32), (y*32), 32, 32), new Rectangle(cX, 0, 32, 32), Color.White);
        }
        #endregion
    }

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
        #endregion

        public bool IsSpawned;

        public MapNpc() { }

        public MapNpc(string name, int x, int y, int npcnum)
        {
            Name = name;
            X = x;
            Y = y;
            npcnum = NpcNum;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            int x = (X * 32);
            int y = (Y * 32) - 16;
            int step = (Step * 32);
            int dir = (Direction * 48);
            spriteBatch.Draw(c_Sprite[Sprite - 1], new Rectangle((x * 32), (y * 32), 32, 48), new Rectangle(step, dir, 32, 32), Color.White);
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

    public class BloodSplat
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int TexX { get; set; }
        public int TexY { get; set; }
        public bool Active { get; set; }


        public BloodSplat() { }

        public void LoadBloodTexture(ContentManager contentManager)
        {
            
        }

        public BloodSplat(int x, int y, int tx, int ty)
        {
            X = x;
            Y = y;
            TexX = tx;
            TexY = ty;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            int tx = (TexX * PIC_X);
            int ty = (TexY * PIC_Y);
            spriteBatch.Draw(c_bloodSprite, new Rectangle((X * 32), (Y * 32), 32, 48), new Rectangle(tx, ty, 32, 32), Color.White);
        }
    }

    public class MapItem
    {
        #region Properties
        public string Name { get; set; }
        public int ItemNum { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
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
        #endregion

        public bool IsSpawned;


        public MapItem() { }

        public MapItem(string name, int x, int y, int itemnum)
        {
            Name = name;
            X = x;
            Y = y;
            ItemNum = itemnum;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(c_ItemSprite[Sprite - 1], new Rectangle((X * 32), (Y * 32), 32, 48), new Rectangle(0, 0, 32, 32), Color.White);
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
            SpawnAmount = 0;
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
