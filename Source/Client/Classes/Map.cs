using SFML.Graphics;
using SFML.System;
using System.Data.SQLite;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using static System.Environment;
using static System.Convert;
using static SabertoothClient.Client;
using static SabertoothClient.Globals;

namespace SabertoothClient
{
    public class Map : Drawable
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
        public VertexArray g_Tile = new VertexArray(PrimitiveType.Quads, 4);
        public VertexArray m_Tile = new VertexArray(PrimitiveType.Quads, 4);
        public VertexArray m2_Tile = new VertexArray(PrimitiveType.Quads, 4);
        public VertexArray f_Tile = new VertexArray(PrimitiveType.Quads, 4);
        public VertexArray f2_Tile = new VertexArray(PrimitiveType.Quads, 4);
        public VertexArray chestPic = new VertexArray(PrimitiveType.Quads, 4);
        public MapNpc[] m_MapNpc = new MapNpc[MAX_MAP_NPCS];
        public MapAnimation[] m_Animation = new MapAnimation[MAX_MAP_ANIMATIONS];
        public BloodSplat[] m_BloodSplats = new BloodSplat[MAX_BLOOD_SPLATS];
        public RenderStates ustates;
        public static int Max_Tilesets = Directory.GetFiles("Resources/Tilesets/", "*", SearchOption.TopDirectoryOnly).Length;
        Texture[] TileSet = new Texture[Max_Tilesets];
        Texture chestSprite = new Texture("Resources/Chest.png");
        RenderTexture brightness = new RenderTexture(1024, 768);
        Sprite brightnessSprite = new Sprite();
        VertexArray LightParticle = new VertexArray(PrimitiveType.TrianglesFan, 18);
        RenderStates overlayStates = new RenderStates(BlendMode.Multiply);
        #endregion

        public Map()
        {
            for (int i = 0; i < Max_Tilesets; i++)
            {
                TileSet[i] = new Texture("Resources/Tilesets/" + (i + 1) + ".png");
            }
        }

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
            for (int m = 0; m < MAX_MAP_NPCS; m++)
            {
                m_MapNpc[m] = new MapNpc();
            }

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
        #endregion

        #region Graphics
        public virtual void Draw(RenderTarget target, RenderStates states)
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
                        states.Texture = TileSet[set];
                        g_Tile[0] = new Vertex(new Vector2f(fx, fy), new Vector2f(tx, ty));
                        g_Tile[1] = new Vertex(new Vector2f(fx + w, fy), new Vector2f(tx + w, ty));
                        g_Tile[2] = new Vertex(new Vector2f(fx + w, fy + h), new Vector2f(tx + w, ty + h));
                        g_Tile[3] = new Vertex(new Vector2f(fx, fy + h), new Vector2f(tx, ty + h));

                        target.Draw(g_Tile, states);

                        if (Mask[x, y].TileX > 0 || Mask[x, y].TileY > 0)
                        {
                            tx = (Mask[x, y].TileX);
                            ty = (Mask[x, y].TileY);
                            w = (Mask[x, y].TileW);
                            h = (Mask[x, y].TileH);
                            set = (Mask[x, y].Tileset);
                            states.Texture = TileSet[set];
                            m_Tile[0] = new Vertex(new Vector2f(fx, fy), new Vector2f(tx, ty));
                            m_Tile[1] = new Vertex(new Vector2f(fx + w, fy), new Vector2f(tx + w, ty));
                            m_Tile[2] = new Vertex(new Vector2f(fx + w, fy + h), new Vector2f(tx + w, ty + h));
                            m_Tile[3] = new Vertex(new Vector2f(fx, fy + h), new Vector2f(tx, ty + h));

                            target.Draw(m_Tile, states);
                        }

                        if (MaskA[x, y].TileX > 0 || MaskA[x, y].TileY > 0)
                        {
                            tx = (MaskA[x, y].TileX);
                            ty = (MaskA[x, y].TileY);
                            w = (MaskA[x, y].TileW);
                            h = (MaskA[x, y].TileH);
                            set = (MaskA[x, y].Tileset);
                            states.Texture = TileSet[set];
                            m2_Tile[0] = new Vertex(new Vector2f(fx, fy), new Vector2f(tx, ty));
                            m2_Tile[1] = new Vertex(new Vector2f(fx + w, fy), new Vector2f(tx + w, ty));
                            m2_Tile[2] = new Vertex(new Vector2f(fx + w, fy + h), new Vector2f(tx + w, ty + h));
                            m2_Tile[3] = new Vertex(new Vector2f(fx, fy + h), new Vector2f(tx, ty + h));

                            target.Draw(m2_Tile, states);
                        }
                    }
                }
            }
        }

        public void DrawFringe(RenderTarget renderWindow)
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
                            ustates = new RenderStates(TileSet[set]);
                            f_Tile[0] = new Vertex(new Vector2f(fx, fy), new Vector2f(tx, ty));
                            f_Tile[1] = new Vertex(new Vector2f(fx + w, fy), new Vector2f(tx + w, ty));
                            f_Tile[2] = new Vertex(new Vector2f(fx + w, fy + h), new Vector2f(tx + w, ty + h));
                            f_Tile[3] = new Vertex(new Vector2f(fx, fy + h), new Vector2f(tx, ty + h));
                            renderWindow.Draw(f_Tile, ustates);
                        }

                        if (FringeA[x, y].TileX > 0 || FringeA[x, y].TileY > 0)
                        {
                            tx = (FringeA[x, y].TileX);
                            ty = (FringeA[x, y].TileY);
                            w = (FringeA[x, y].TileW);
                            h = (FringeA[x, y].TileH);
                            set = (FringeA[x, y].Tileset);
                            ustates = new RenderStates(TileSet[set]);
                            f2_Tile[0] = new Vertex(new Vector2f(fx, fy), new Vector2f(tx, ty));
                            f2_Tile[1] = new Vertex(new Vector2f(fx + w, fy), new Vector2f(tx + w, ty));
                            f2_Tile[2] = new Vertex(new Vector2f(fx + w, fy + h), new Vector2f(tx + w, ty + h));
                            f2_Tile[3] = new Vertex(new Vector2f(fx, fy + h), new Vector2f(tx, ty + h));
                            renderWindow.Draw(f2_Tile, ustates);
                        }
                    }
                }
            }
        }

        public void DrawChest(int x, int y, bool empty)
        {
            int cX = 0;
            if (empty) { cX = 32; }
            chestPic[0] = new Vertex(new Vector2f((x * 32), (y * 32)), new Vector2f(0 + cX, 0));
            chestPic[1] = new Vertex(new Vector2f((x * 32) + 32, (y * 32)), new Vector2f(32 + cX, 0));
            chestPic[2] = new Vertex(new Vector2f((x * 32) + 32, (y * 32) + 32), new Vector2f(32 + cX, 32));
            chestPic[3] = new Vertex(new Vector2f((x * 32), (y * 32) + 32), new Vector2f(0 + cX, 32));

            ustates = new RenderStates(chestSprite);
            renderWindow.Draw(chestPic, ustates);
        }

        public void DrawBrightness()
        {
            int overlay;
            if (worldTime.g_Night) { overlay = 200; }
            else { overlay = Brightness; }
            brightnessSprite.Texture = brightness.Texture;
            brightness.Clear(new Color(0, 0, 0, (byte)overlay));
            DrawMapLight();
            DrawPlayerLight();
            renderWindow.Draw(brightnessSprite);
        }

        void DrawMapLight()
        {
            for (int x = 0; x < MaxX; x++)
            {
                for (int y = 0; y < MaxY; y++)
                {
                    if (Ground[x, y].LightRadius > 0)
                    {
                        int centerX = ((x * PIC_X) - players[HandleData.myIndex].X * PIC_Y) + 16;
                        int centerY = 768 - (((y * PIC_Y) - players[HandleData.myIndex].Y * PIC_X) + 16);
                        Vector2f center = new Vector2f(centerX, centerY);
                        double radius = Ground[x, y].LightRadius;

                        LightParticle[0] = new Vertex(center, Color.Transparent);

                        for (uint i = 1; i < 18; i++)
                        {
                            double angle = i * 2 * Math.PI / 16 - Math.PI / 2;
                            int lx = (int)(center.X + radius * Math.Cos(angle));
                            int ly = (int)(center.Y + radius * Math.Sin(angle));
                            LightParticle[i] = new Vertex(new Vector2f(lx, ly), Color.White);
                        }
                        brightness.Draw(LightParticle, overlayStates);
                    }
                }
            }
        }

        void DrawPlayerLight()
        {
            int centerX = 530;
            int centerY = 400;
            Vector2f center = new Vector2f(centerX, centerY);
            double radius = players[HandleData.myIndex].LightRadius;

            LightParticle[0] = new Vertex(center, Color.Transparent);

            for (uint i = 1; i < 18; i++)
            {
                double angle = i * 2 * Math.PI / 16 - Math.PI / 2;
                int lx = (int)(center.X + radius * Math.Cos(angle));
                int ly = (int)(center.Y + radius * Math.Sin(angle));
                LightParticle[i] = new Vertex(new Vector2f(lx, ly), Color.White);
            }
            brightness.Draw(LightParticle, overlayStates);
        }
        #endregion
    }

    public class MapNpc : Npc
    {
        static int spriteTextures = Directory.GetFiles("Resources/Characters/", "*", SearchOption.TopDirectoryOnly).Length;
        Texture[] c_Sprite = new Texture[spriteTextures];
        VertexArray spritePic = new VertexArray(PrimitiveType.Quads, 4);
        VertexArray healthBar = new VertexArray(PrimitiveType.Quads, 4);
        Text npcName = new Text();
        Text npcCoords = new Text();
        Font font = new Font("Resources/Fonts/Arial.ttf");
        float barLength;
        public DisplayText[] dText = new DisplayText[MAX_DISPLAY_TEXT];

        public MapNpc()
        {
            for (int i = 0; i < spriteTextures; i++)
            {
                c_Sprite[i] = new Texture("Resources/Characters/" + (i + 1) + ".png");
            }

            npcName.Font = font;
            npcName.CharacterSize = 12;
            npcName.Color = Color.White;
            npcName.Style = Text.Styles.Regular;

            npcCoords.Font = font;
            npcCoords.CharacterSize = 12;
            npcCoords.Color = Color.Blue;
            npcCoords.Style = Text.Styles.Regular;

            for (int i = 0; i < MAX_DISPLAY_TEXT; i++)
            {
                dText[i] = new DisplayText();
            }
        }

        public MapNpc(string name, int x, int y, int npcnum)
        {
            Name = name;
            X = x;
            Y = y;
            npcnum = NpcNum;
            for (int i = 0; i < spriteTextures; i++)
            {
                c_Sprite[i] = new Texture("Resources/Characters/" + (i + 1) + ".png");
            }
        }

        public override void Draw(RenderTarget target, RenderStates state)
        {
            int x = (X * PIC_X);
            int y = (Y * PIC_Y) - 16;
            int step = (Step * SPRITE_SIZE_X);
            int dir = (Direction * SPRITE_SIZE_Y);

            int nameX = (X * PIC_X) - (Name.Length / 2);
            int nameY = (Y * PIC_Y) - PIC_Y;
            int pX = 0;
            int pY = 0;
            double dX = 0;
            double dY = 0;
            double dFinal = 0;
            double dPoint = 0;

            pX = players[HandleData.myIndex].X + OFFSET_X;
            pY = players[HandleData.myIndex].Y + OFFSET_Y;
            dX = pX - X;
            dY = pY - Y;
            dFinal = dX * dX + dY * dY;
            dPoint = Math.Sqrt(dFinal);

            npcName.Position = new Vector2f(nameX, nameY - 12);
            npcName.DisplayedString = Name;

            if (gui.d_Window.IsVisible)
            {
                npcCoords.DisplayedString = "(" + X + "," + Y + ")\n" + dPoint.ToString(string.Format("#.#"));
                npcCoords.Position = new Vector2f(nameX, nameY + 66);
            }                
            
            spritePic[0] = new Vertex(new Vector2f(x, y), new Vector2f(step, dir));
            spritePic[1] = new Vertex(new Vector2f(x + 32, y), new Vector2f(step + 32, dir));
            spritePic[2] = new Vertex(new Vector2f(x + 32, y + 48), new Vector2f(step + 32, dir + 48));
            spritePic[3] = new Vertex(new Vector2f(x, y + 48), new Vector2f(step, dir + 48));

            barLength = ((float)Health / MaxHealth) * 35;

            x = (X * PIC_X);
            y = (Y * PIC_Y) - 20;
            healthBar[0] = new Vertex(new Vector2f(x, y), Color.Red);
            healthBar[1] = new Vertex(new Vector2f(barLength + x, y), Color.Red);
            healthBar[2] = new Vertex(new Vector2f(barLength + x, y + 5), Color.Red);
            healthBar[3] = new Vertex(new Vector2f(x, y + 5), Color.Red);

            state.Texture = c_Sprite[Sprite - 1];
            target.Draw(spritePic, state);
            target.Draw(healthBar);
            //target.Draw(npcName);
            if (gui.d_Window.IsVisible) { target.Draw(npcCoords); }
        }
    }

    public class MapAnimation : Animation
    {
        static int animTextures = Directory.GetFiles("Resources/Animation/", "*", SearchOption.TopDirectoryOnly).Length;   //count the textures

        VertexArray animPic = new VertexArray(PrimitiveType.Quads, 4);  //setup the vertex array for later
        Texture[] animSprite = new Texture[animTextures];   //create textures for all the sprites loaded
        Texture[] finalTexture;
        int currentFrame;
        int animTick;
        public int X { get; set; }  //Animations X location
        public int Y { get; set; }  //animations Y location

        public MapAnimation()
        {
            for (int i = 0; i < animTextures; i++)
            {
                animSprite[i] = new Texture("Resources/Animation/" + (i + 1) + ".png");
            }
            currentFrame = 0;
        }

        public void ConfigAnimation()
        {
            try
            {
                Texture anim = animSprite[SpriteNumber - 1];
                finalTexture = new Texture[FrameCount];
                int currentCount = 0;
                int xpic = (int)anim.Size.X / FrameCountV;
                int ypic = (int)anim.Size.Y / FrameCountH;

                for (int i = 0; i < FrameCountH; i++)
                {
                    for (int j = 0; j < FrameCountV; j++)
                    {
                        Image animImg = anim.CopyToImage();
                        finalTexture[currentCount] = new Texture(animImg, new IntRect((j * xpic), (i * ypic), xpic, ypic));
                        currentCount += 1;
                    }
                }
            }
            catch
            {
                Logging.WriteMessageLog("Error loading texture");
            }
        }

        void CheckCurrentFrame()
        {
            if (TickCount - animTick > FrameDuration)
            {
                currentFrame += 1;

                if (currentFrame >= FrameCount)
                {
                    currentFrame = 0;
                }

                animTick = TickCount;
            }
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            if (SpriteNumber > 0)
            {
                int x = (X * PIC_X) - 16;   //16
                int y = (Y * PIC_Y) - 96;   //96

                CheckCurrentFrame();

                int xpic = (int)finalTexture[currentFrame].Size.X;
                int ypic = (int)finalTexture[currentFrame].Size.Y;

                animPic[0] = new Vertex(new Vector2f(x, y), new Vector2f(0, 0));
                animPic[1] = new Vertex(new Vector2f(x + xpic, y), new Vector2f(xpic, 0));
                animPic[2] = new Vertex(new Vector2f(x + xpic, y + ypic), new Vector2f(xpic, ypic));
                animPic[3] = new Vertex(new Vector2f(x, y + ypic), new Vector2f(0, ypic));

                states.Texture = finalTexture[currentFrame];
                target.Draw(animPic, states);
            }
        }
    }

    public class BloodSplat : Drawable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int TexX { get; set; }
        public int TexY { get; set; }
        public bool Active { get; set; }
        VertexArray bloodPic = new VertexArray(PrimitiveType.Quads, 4);
        Texture c_bloodSprite = new Texture("Resources/Blood.png");

        public BloodSplat() { }

        public BloodSplat(int x, int y, int tx, int ty)
        {
            X = x;
            Y = y;
            TexX = tx;
            TexY = ty;
        }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            int tx = (TexX * PIC_X);
            int ty = (TexY * PIC_Y);
            bloodPic[0] = new Vertex(new Vector2f((X * PIC_X), (Y * PIC_Y)), new Vector2f(tx, ty));
            bloodPic[1] = new Vertex(new Vector2f((X * PIC_X) + PIC_X, (Y * PIC_Y)), new Vector2f(tx + PIC_X, ty));
            bloodPic[2] = new Vertex(new Vector2f((X * PIC_X) + PIC_X, (Y * PIC_Y) + PIC_Y), new Vector2f(tx + PIC_X, ty + PIC_Y));
            bloodPic[3] = new Vertex(new Vector2f((X * PIC_X), (Y * PIC_Y) + PIC_Y), new Vector2f(tx, ty + PIC_Y));

            states.Texture = c_bloodSprite;
            target.Draw(bloodPic, states);
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

    public enum TileType : int
    {
        None,
        Blocked,
        NpcSpawn,
        NpcAvoid,
        Chest,
        Warp,
        Animation
    }

    public enum TileLayers : int
    {
        Ground,
        Mask,
        Fringe,
        MaskA,
        FringeA
    }
}
