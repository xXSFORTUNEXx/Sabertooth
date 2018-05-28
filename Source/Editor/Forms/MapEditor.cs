using SabertoothServer;
using SFML.Graphics;
using SFML.System;
using System;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using static System.Convert;
using static System.Environment;
using static System.Windows.Forms.Application;
using System.Data.SqlClient;

namespace Editor.Forms
{
    public partial class MapEditor : Form
    {
        RenderWindow e_Window;
        RenderText e_Text = new RenderText();
        Map e_Map = new Map();
        SFML.Graphics.View e_View = new SFML.Graphics.View();
        Texture[] e_Tileset = new Texture[2];
        public RenderStates states;
        Sprite e_SelectedTile = new Sprite();
        Texture e_GridTexture = new Texture("Resources/Tilesets/Grid.png");
        Sprite e_Grid = new Sprite();
        Npc e_Npc = new Npc();
        Item e_Item = new Item();
        public int SelectedIndex;
        public int e_ViewX { get; set; }
        public int e_ViewY { get; set; }
        int e_OffsetX = 25;
        int e_OffsetY = 19;
        int e_CursorX;
        int e_CursorY;
        int e_TileX;
        int e_TileY;
        int e_TileW = 1;
        int e_TileH = 1;
        int e_Layer;
        int e_Type;
        int e_SelectTileset;
        int e_SpawnNumber;
        int e_SpawnAmount;
        int e_Chest;
        float e_Zoom = 1.0f;
        int e_WheelOption = 0;

        static int lastFrameRate;
        static int frameRate;
        static int lastTick;

        public VertexArray g_Tile = new VertexArray(PrimitiveType.Quads, 4);
        public VertexArray m_Tile = new VertexArray(PrimitiveType.Quads, 4);
        public VertexArray m2_Tile = new VertexArray(PrimitiveType.Quads, 4);
        public VertexArray f_Tile = new VertexArray(PrimitiveType.Quads, 4);
        public VertexArray f2_Tile = new VertexArray(PrimitiveType.Quads, 4);

        public const int Max_ItemPics = 8;
        public const int Max_Sprites = 8;
        public Texture[] ItemPic = new Texture[Max_ItemPics];
        public Sprite ItemSprite = new Sprite();
        public Texture[] SpritePic = new Texture[Max_Sprites];
        public Sprite e_Sprite = new Sprite();

        public RenderTexture brightness = new RenderTexture(Globals.SCREEN_WIDTH, Globals.SCREEN_HEIGHT);
        public Sprite brightnessSprite = new Sprite();
        public VertexArray LightParticle = new VertexArray(PrimitiveType.TrianglesFan, 18);
        public RenderStates overlayStates = new RenderStates(BlendMode.Multiply);
        public double e_LightRadius = 0;

        public MapEditor()
        {
            InitializeComponent();
            Visible = true;

            picMap.KeyDown += new KeyEventHandler(EditorKeyDown);
            picMap.MouseDown += new MouseEventHandler(EditorMouseDown);
            picMap.MouseMove += new MouseEventHandler(EditorMouseMove);
            picMap.MouseWheel += new MouseEventHandler(EditorMouseWheelScroll);
            picTileset.MouseDown += new MouseEventHandler(EditorTilesetMouseDown);
            picTileset.Paint += new PaintEventHandler(EditorTilesetPaint);
            picTileset.KeyDown += new KeyEventHandler(EditorTilesetKeyDown);

            for (int i = 0; i < 2; i++)
            {
                cmbTileset.Items.Add("Tileset: " + (i + 1));
                e_Tileset[i] = new Texture("Resources/Tilesets/" + (i + 1) + ".png");
            }

            for (int s = 0; s < Max_Sprites; s++)
            {
                SpritePic[s] = new Texture("Resources/Characters/" + (s + 1) + ".png");
            }

            for (int p = 0; p < Max_ItemPics; p++)
            {
                ItemPic[p] = new Texture("Resources/Items/" + (p + 1) + ".png");
            }

            if (Server.DBType == Globals.SQL_DATABASE_REMOTE.ToString())
            {
                string connection = "Data Source=" + Server.sqlServer + ";Initial Catalog=" + Server.sqlDatabase + ";Integrated Security=True";
                using (var sql = new SqlConnection(connection))
                {
                    sql.Open();
                    string command = "SELECT COUNT(*) FROM NPCS";
                    using (SqlCommand cmd = new SqlCommand(command, sql))
                    {
                        object count = cmd.ExecuteScalar();
                        int result = ToInt32(count);
                        for (int i = 0; i < result; i++)
                        {
                            e_Npc.LoadNpcNameFromDatabase(i + 1);
                            cmbNpc1.Items.Add(e_Npc.Name);
                            cmbNpc2.Items.Add(e_Npc.Name);
                            cmbNpc3.Items.Add(e_Npc.Name);
                            cmbNpc4.Items.Add(e_Npc.Name);
                            cmbNpc5.Items.Add(e_Npc.Name);
                            cmbNpc6.Items.Add(e_Npc.Name);
                            cmbNpc7.Items.Add(e_Npc.Name);
                            cmbNpc8.Items.Add(e_Npc.Name);
                            cmbNpc9.Items.Add(e_Npc.Name);
                            cmbNpc10.Items.Add(e_Npc.Name);
                        }
                    }

                    command = "SELECT COUNT(*) FROM MAPS";
                    using (SqlCommand cmd = new SqlCommand(command, sql))
                    {
                        object queue = cmd.ExecuteScalar();
                        int result = ToInt32(queue);
                        if (result == 0)
                        {
                            e_Map.CreateMapInDatabase();
                        }
                    }
                }
            }
            else
            {
                using (SQLiteConnection conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
                {
                    conn.Open();
                    string sql;

                    sql = "SELECT COUNT(*) FROM NPCS";

                    object queue;
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        queue = cmd.ExecuteScalar();
                    }
                    int result = ToInt32(queue);

                    for (int i = 0; i < result; i++)
                    {
                        e_Npc.LoadNpcNameFromDatabase(i + 1);
                        cmbNpc1.Items.Add(e_Npc.Name);
                        cmbNpc2.Items.Add(e_Npc.Name);
                        cmbNpc3.Items.Add(e_Npc.Name);
                        cmbNpc4.Items.Add(e_Npc.Name);
                        cmbNpc5.Items.Add(e_Npc.Name);
                        cmbNpc6.Items.Add(e_Npc.Name);
                        cmbNpc7.Items.Add(e_Npc.Name);
                        cmbNpc8.Items.Add(e_Npc.Name);
                        cmbNpc9.Items.Add(e_Npc.Name);
                        cmbNpc10.Items.Add(e_Npc.Name);
                    }
                }

                using (var conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
                {
                    using (var cmd = new SQLiteCommand(conn))
                    {
                        conn.Open();
                        cmd.CommandText = "SELECT COUNT(*) FROM MAPS";
                        object queue = cmd.ExecuteScalar();
                        int result = ToInt32(queue);
                        if (result == 0)
                        {
                            e_Map.CreateMapInDatabase();
                        }
                    }
                }
            }

            LoadMapList();
            SelectedIndex = 1;
            e_Map.LoadMapFromDatabase(SelectedIndex);
            mapProperties.SelectedObject = e_Map;

            cmbNpc1.SelectedIndex = e_Map.m_MapNpc[0].NpcNum;
            cmbNpc2.SelectedIndex = e_Map.m_MapNpc[1].NpcNum;
            cmbNpc3.SelectedIndex = e_Map.m_MapNpc[2].NpcNum;
            cmbNpc4.SelectedIndex = e_Map.m_MapNpc[3].NpcNum;
            cmbNpc5.SelectedIndex = e_Map.m_MapNpc[4].NpcNum;
            cmbNpc6.SelectedIndex = e_Map.m_MapNpc[5].NpcNum;
            cmbNpc7.SelectedIndex = e_Map.m_MapNpc[6].NpcNum;
            cmbNpc8.SelectedIndex = e_Map.m_MapNpc[7].NpcNum;
            cmbNpc9.SelectedIndex = e_Map.m_MapNpc[8].NpcNum;
            cmbNpc10.SelectedIndex = e_Map.m_MapNpc[9].NpcNum;

            MapEditorLoop();
        }

        void MapEditorLoop()
        {
            e_Window = new RenderWindow(picMap.Handle);
            e_Window.SetFramerateLimit(Globals.MAX_FPS);
            cmbTileset.SelectedIndex = 1;
            e_Layer = (int)TileLayers.Ground;
            e_Type = (int)TileType.None;

            while (Visible)
            {
                if (tabTools.SelectedTab == tabTiles) { picTileset.Invalidate(); }

                UpdateView();
                DoEvents();
                e_Window.DispatchEvents();
                e_Window.Clear();
                DrawTiles(e_Window);
                DrawNpcs();
                DrawItems();
                BrightnessOverlay();
                DrawTypes();
                DrawGrid();
                Text = "Map Editor - FPS: " + CalculateFrameRate();
                e_Window.Display();
            }
            Visible = false;
        }

        void LoadMapList()
        {
            if (Server.DBType == Globals.SQL_DATABASE_REMOTE.ToString())
            {
                string connection = "Data Source=" + Server.sqlServer + ";Initial Catalog=" + Server.sqlDatabase + ";Integrated Security=True";
                using (var sql = new SqlConnection(connection))
                {
                    sql.Open();
                    string command = "SELECT COUNT(*) FROM MAPS";
                    using (SqlCommand cmd = new SqlCommand(command, sql))
                    {
                        object count = cmd.ExecuteScalar();
                        int result = ToInt32(count);
                        treeMaps.Nodes.Clear();
                        treeMaps.BeginUpdate();
                        for (int i = 0; i < result; i++)
                        {
                            e_Map.LoadMapNameFromDatabase(i + 1);
                            treeMaps.Nodes.Add(e_Map.Name);
                        }
                        treeMaps.EndUpdate();
                    }
                }
            }
            else
            {
                using (var conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
                {
                    using (var cmd = new SQLiteCommand(conn))
                    {
                        conn.Open();
                        cmd.CommandText = "SELECT COUNT(*) FROM MAPS";
                        object queue = cmd.ExecuteScalar();
                        int result = ToInt32(queue);
                        treeMaps.Nodes.Clear();
                        treeMaps.BeginUpdate();
                        for (int i = 0; i < result; i++)
                        {
                            e_Map.LoadMapNameFromDatabase(i + 1);
                            treeMaps.Nodes.Add(e_Map.Name);
                        }
                        treeMaps.EndUpdate();
                    }
                }
            }        
        }

        void UpdateView()
        {
            e_View.Reset(new FloatRect(0, 0, Globals.SCREEN_WIDTH, Globals.SCREEN_HEIGHT));
            e_View.Move(new Vector2f(e_ViewX * Globals.PIC_X, e_ViewY * Globals.PIC_Y));
            e_View.Zoom(e_Zoom);
            brightnessSprite.Position = new Vector2f(e_ViewX * Globals.PIC_X, e_ViewY * Globals.PIC_Y);
            e_Window.SetView(e_View);
        }

        void DrawCursorLight()
        {
            int centerX = ((e_CursorX * 32) + 16) - (e_ViewX * 32);
            int centerY = 600 - ((e_CursorY * 32) + 16) + (e_ViewY * 32);
            Vector2f center = new Vector2f(centerX, centerY);
            double radius = e_LightRadius;

            LightParticle[0] = new Vertex(center, SFML.Graphics.Color.Transparent);

            for (uint i = 1; i < 18; i++)
            {
                double angle = i * 2 * Math.PI / 16 - Math.PI / 2;
                int x = (int)(center.X + radius * Math.Cos(angle));
                int y = (int)(center.Y + radius * Math.Sin(angle));
                LightParticle[i] = new Vertex(new Vector2f(x, y), SFML.Graphics.Color.White);
            }
            brightness.Draw(LightParticle, overlayStates);
        }

        void DrawMapLight()
        {
            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    if (e_Map.Ground[x, y].LightRadius > 0)
                    {
                        int centerX = ((x * 32) + 16) - (e_ViewX * 32);
                        int centerY = 600 - (((y * 32) + 16) - (e_ViewY * 32));
                        Vector2f center = new Vector2f(centerX, centerY);
                        double radius = e_Map.Ground[x, y].LightRadius;

                        LightParticle[0] = new Vertex(center, SFML.Graphics.Color.Transparent);

                        for (uint i = 1; i < 18; i++)
                        {
                            double angle = i * 2 * Math.PI / 16 - Math.PI / 2;
                            int lx = (int)(center.X + radius * Math.Cos(angle));
                            int ly = (int)(center.Y + radius * Math.Sin(angle));
                            LightParticle[i] = new Vertex(new Vector2f(lx, ly), SFML.Graphics.Color.White);
                        }
                        brightness.Draw(LightParticle, overlayStates);
                    }
                }
            }
        }

        void BrightnessOverlay()
        {
            int overlay;
            if (chkNight.Checked) { overlay = 200; }
            else { overlay = e_Map.Brightness; }
            brightnessSprite.Texture = brightness.Texture;
            brightness.Clear(new SFML.Graphics.Color(0, 0, 0, (byte)overlay));
            if (tabTools.SelectedTab == tabLight) { DrawCursorLight(); }
            DrawMapLight();
            e_Window.Draw(brightnessSprite);
        }

        void DrawTiles(RenderTarget target)
        {
            if (pnlMapNpcs.Visible) { return; }
            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    int fx = (x * 32);
                    int fy = (y * 32);
                    int tx, ty, w, h, set;

                    tx = (e_Map.Ground[x, y].TileX);
                    ty = (e_Map.Ground[x, y].TileY);
                    w = (e_Map.Ground[x, y].TileW);
                    h = (e_Map.Ground[x, y].TileH);
                    set = (e_Map.Ground[x, y].Tileset);
                    states = new RenderStates(e_Tileset[set]);
                    g_Tile[0] = new Vertex(new Vector2f(fx, fy), new Vector2f(tx, ty));
                    g_Tile[1] = new Vertex(new Vector2f(fx + w, fy), new Vector2f(tx + w, ty));
                    g_Tile[2] = new Vertex(new Vector2f(fx + w, fy + h), new Vector2f(tx + w, ty + h));
                    g_Tile[3] = new Vertex(new Vector2f(fx, fy + h), new Vector2f(tx, ty + h));

                    target.Draw(g_Tile, states);

                    if (e_Map.Mask[x, y].TileX > 0 || e_Map.Mask[x, y].TileY > 0)
                    {
                        tx = (e_Map.Mask[x, y].TileX);
                        ty = (e_Map.Mask[x, y].TileY);
                        w = (e_Map.Mask[x, y].TileW);
                        h = (e_Map.Mask[x, y].TileH);
                        set = (e_Map.Mask[x, y].Tileset);
                        states = new RenderStates(e_Tileset[set]);
                        m_Tile[0] = new Vertex(new Vector2f(fx, fy), new Vector2f(tx, ty));
                        m_Tile[1] = new Vertex(new Vector2f(fx + w, fy), new Vector2f(tx + w, ty));
                        m_Tile[2] = new Vertex(new Vector2f(fx + w, fy + h), new Vector2f(tx + w, ty + h));
                        m_Tile[3] = new Vertex(new Vector2f(fx, fy + h), new Vector2f(tx, ty + h));

                        target.Draw(m_Tile, states);
                    }

                    if (e_Map.MaskA[x, y].TileX > 0 || e_Map.MaskA[x, y].TileY > 0)
                    {
                        tx = (e_Map.MaskA[x, y].TileX);
                        ty = (e_Map.MaskA[x, y].TileY);
                        w = (e_Map.MaskA[x, y].TileW);
                        h = (e_Map.MaskA[x, y].TileH);
                        set = (e_Map.MaskA[x, y].Tileset);
                        states = new RenderStates(e_Tileset[set]);
                        m2_Tile[0] = new Vertex(new Vector2f(fx, fy), new Vector2f(tx, ty));
                        m2_Tile[1] = new Vertex(new Vector2f(fx + w, fy), new Vector2f(tx + w, ty));
                        m2_Tile[2] = new Vertex(new Vector2f(fx + w, fy + h), new Vector2f(tx + w, ty + h));
                        m2_Tile[3] = new Vertex(new Vector2f(fx, fy + h), new Vector2f(tx, ty + h));

                        target.Draw(m2_Tile, states);
                    }

                    if (e_Map.Fringe[x, y].TileX > 0 || e_Map.Fringe[x, y].TileY > 0)
                    {
                        tx = (e_Map.Fringe[x, y].TileX);
                        ty = (e_Map.Fringe[x, y].TileY);
                        w = (e_Map.Fringe[x, y].TileW);
                        h = (e_Map.Fringe[x, y].TileH);
                        set = (e_Map.Fringe[x, y].Tileset);
                        states = new RenderStates(e_Tileset[set]);
                        f_Tile[0] = new Vertex(new Vector2f(fx, fy), new Vector2f(tx, ty));
                        f_Tile[1] = new Vertex(new Vector2f(fx + w, fy), new Vector2f(tx + w, ty));
                        f_Tile[2] = new Vertex(new Vector2f(fx + w, fy + h), new Vector2f(tx + w, ty + h));
                        f_Tile[3] = new Vertex(new Vector2f(fx, fy + h), new Vector2f(tx, ty + h));
                        target.Draw(f_Tile, states);
                    }

                    if (e_Map.FringeA[x, y].TileX > 0 || e_Map.FringeA[x, y].TileY > 0)
                    {
                        tx = (e_Map.FringeA[x, y].TileX);
                        ty = (e_Map.FringeA[x, y].TileY);
                        w = (e_Map.FringeA[x, y].TileW);
                        h = (e_Map.FringeA[x, y].TileH);
                        set = (e_Map.FringeA[x, y].Tileset);
                        states = new RenderStates(e_Tileset[set]);
                        f2_Tile[0] = new Vertex(new Vector2f(fx, fy), new Vector2f(tx, ty));
                        f2_Tile[1] = new Vertex(new Vector2f(fx + w, fy), new Vector2f(tx + w, ty));
                        f2_Tile[2] = new Vertex(new Vector2f(fx + w, fy + h), new Vector2f(tx + w, ty + h));
                        f2_Tile[3] = new Vertex(new Vector2f(fx, fy + h), new Vector2f(tx, ty + h));
                        target.Draw(f2_Tile, states);
                    }
                }
            }

            if (picMap.Focused && tabTools.SelectedTab == tabLayer || tabTools.SelectedTab == tabTiles)
            {
                    DrawSelectTile(new Vector2f((e_CursorX * 32), (e_CursorY * 32)), (e_TileX * 32), (e_TileY * 32), (e_TileW * 32), (e_TileH * 32));
            }
        }

        void DrawTypes()
        {
            if (pnlMapNpcs.Visible) { return; }
            if (tabTools.SelectedTab == tabTypes)
            {
                for (int x = 0; x < 50; x++)
                {
                    for (int y = 0; y < 50; y++)
                    {
                        if (e_Map.Ground[x, y].Type != (int)TileType.None)
                        {
                            switch (e_Map.Ground[x, y].Type)
                            {
                                case (int)TileType.Blocked:
                                    e_Text.DrawText(e_Window, "B", new Vector2f((x * 32) + 12, (y * 32) + 7), 14, SFML.Graphics.Color.Red);
                                    break;
                                case (int)TileType.NpcSpawn:
                                    e_Text.DrawText(e_Window, "N", new Vector2f((x * 32) + 12, (y * 32) + 7), 14, SFML.Graphics.Color.Yellow);
                                    break;
                                case (int)TileType.SpawnPool:
                                    e_Text.DrawText(e_Window, "S", new Vector2f((x * 32) + 12, (y * 32) + 7), 14, SFML.Graphics.Color.Green);
                                    if (e_Map.Ground[x, y].SpawnAmount > 1)
                                    {
                                        e_Text.DrawText(e_Window, e_Map.Ground[x, y].SpawnAmount.ToString(), new Vector2f((x * 32) + 20, (y * 32) + 20), 14, SFML.Graphics.Color.Cyan);
                                    }
                                    break;
                                case (int)TileType.NpcAvoid:
                                    e_Text.DrawText(e_Window, "A", new Vector2f((x * 32) + 12, (y * 32) + 7), 14, SFML.Graphics.Color.White);
                                    break;
                                case (int)TileType.MapItem:
                                    e_Text.DrawText(e_Window, "I", new Vector2f((x * 32) + 12, (y * 32) + 7), 14, SFML.Graphics.Color.Cyan);
                                    if (e_Map.Ground[x, y].SpawnAmount > 1)
                                    {
                                        e_Text.DrawText(e_Window, e_Map.Ground[x, y].SpawnAmount.ToString(), new Vector2f((x * 32) + 20, (y * 32) + 20), 14, SFML.Graphics.Color.Green);
                                    }
                                    break;
                                case (int)TileType.Chest:
                                    e_Text.DrawText(e_Window, "C", new Vector2f((x * 32) + 12, (y * 32) + 7), 14, SFML.Graphics.Color.Blue);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            if (tabTools.SelectedTab == tabLight)
            {
                for (int x = 0; x < 50; x++)
                {
                    for (int y = 0; y < 50; y++)
                    {
                        if (e_Map.Ground[x, y].LightRadius > 0)
                        {
                            e_Text.DrawText(e_Window, "L", new Vector2f((x * 32) + 12, (y * 32) + 7), 14, SFML.Graphics.Color.Yellow);
                        }
                    }
                }
            }
        }

        void DrawGrid()
        {
            if (pnlMapNpcs.Visible) { return; }

            if (chkGrid.Checked)
            {
                e_Grid.Texture = e_GridTexture;

                for (int x = 0; x < 50; x++)
                {
                    for (int y = 0; y < 50; y++)
                    {
                        e_Grid.TextureRect = new IntRect(0, 0, 32, 32);
                        e_Grid.Position = new Vector2f(x * 32, y * 32);
                        e_Window.Draw(e_Grid);
                    }
                }
            }
        }

        void DrawNpcs()
        {
            if (pnlMapNpcs.Visible) { return; }

            if (chkNpc.Checked)
            {
                for (int x = 0; x < 50; x++)
                {
                    for (int y = 0; y < 50; y++)
                    {
                        if (e_Map.Ground[x, y].Type == (int)TileType.NpcSpawn)
                        {
                            if (e_Map.Ground[x, y].SpawnNum > 0)
                            {
                                int npcNum = e_Map.Ground[x, y].SpawnNum;
                                e_Npc.LoadNpcFromDatabase(npcNum);
                                DrawNpc(e_Window, SpritePic[e_Npc.Sprite - 1], x, y);
                            }
                        }
                    }
                }
            }
        }

        void DrawItems()
        {
            if (pnlMapNpcs.Visible) { return; }

            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    if (e_Map.Ground[x, y].Type == (int)TileType.MapItem)
                    {
                        if (e_Map.Ground[x, y].SpawnNum > 0)
                        {
                            int itemNum = e_Map.Ground[x, y].SpawnNum;
                            e_Item.LoadItemFromDatabase(itemNum);
                            DrawItem(e_Window, (e_Item.Sprite - 1), x, y);
                        }
                    }
                }
            }
        }

        void DrawItem(RenderWindow e_Window, int itemPic, int x, int y)
        {
            ItemSprite.Texture = ItemPic[itemPic];
            ItemSprite.TextureRect = new IntRect(0, 0, 32, 32);
            ItemSprite.Position = new Vector2f(x * 32, y * 32);

            e_Window.Draw(ItemSprite);
        }

        public void DrawNpc(RenderWindow e_Window, Texture e_Texture, int x, int y)
        {
            e_Sprite.Texture = e_Texture;
            e_Sprite.TextureRect = new IntRect(0, 0, 32, 48);
            e_Sprite.Position = new Vector2f(x * 32, (y * 32) - 16);

            e_Window.Draw(e_Sprite);
        }

        void DrawSelectTile(Vector2f position, int x, int y, int w, int h)
        {
            if (tabTools.SelectedTab == tabTypes) { return; }
            if (!picMap.Focused) { return; }
            e_SelectedTile.Texture = e_Tileset[e_SelectTileset];
            e_SelectedTile.TextureRect = new IntRect(x, y, w, h);
            e_SelectedTile.Position = position;

            e_Window.Draw(e_SelectedTile);
        }

        private void EditorKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    if (e_ViewY > 0)
                    {
                        e_ViewY -= 1;
                        scrlViewY.Value = e_ViewY;
                    }
                    break;
                case Keys.S:
                    if (e_ViewY < (50 - e_OffsetY))
                    {
                        e_ViewY += 1;
                        scrlViewY.Value = e_ViewY;
                    }
                    break;
                case Keys.A:
                    if (e_ViewX > 0)
                    {
                        e_ViewX -= 1;
                        scrlViewX.Value = e_ViewX;
                    }
                    break;
                case Keys.D:
                    if (e_ViewX < (50 - e_OffsetX))
                    {
                        e_ViewX += 1;
                        scrlViewX.Value = e_ViewX;
                    }
                    break;               
            }
            lblViewX.Text = "View X: " + e_ViewX;
            lblViewY.Text = "View Y: " + e_ViewY;
        }

        private void EditorMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            picMap.Focus();
            e_CursorX = (e.X / 32) + e_ViewX;
            e_CursorY = (e.Y / 32) + e_ViewY;
            if (tabTools.SelectedTab == tabLayer || tabTools.SelectedTab == tabTiles)
            {
                if (e.Button == MouseButtons.Left)
                {
                    switch (e_Layer)
                    {
                        case (int)TileLayers.Ground:
                            if (e_TileW > 1 || e_TileH > 1)
                            {
                                for (int x = 0; x < e_TileW; x++)
                                {
                                    for (int y = 0; y < e_TileH; y++)
                                    {
                                        e_Map.Ground[(e_CursorX + x), (e_CursorY + y)].TileX = (e_TileX + x) * 32;
                                        e_Map.Ground[(e_CursorX + x), (e_CursorY + y)].TileY = (e_TileY + y) * 32;
                                        e_Map.Ground[(e_CursorX + x), (e_CursorY + y)].TileW = 32;
                                        e_Map.Ground[(e_CursorX + x), (e_CursorY + y)].TileH = 32;
                                        e_Map.Ground[(e_CursorX + x), (e_CursorY + y)].Tileset = e_SelectTileset;
                                    }
                                }
                            }
                            else
                            {
                                e_Map.Ground[(e_CursorX), (e_CursorY)].TileX = (e_TileX * 32);
                                e_Map.Ground[(e_CursorX), (e_CursorY)].TileY = (e_TileY * 32);
                                e_Map.Ground[(e_CursorX), (e_CursorY)].TileW = 32;
                                e_Map.Ground[(e_CursorX), (e_CursorY)].TileH = 32;
                                e_Map.Ground[(e_CursorX), (e_CursorY)].Tileset = e_SelectTileset;
                            }
                            break;
                        case (int)TileLayers.Mask:
                            if (e_TileW > 1 || e_TileH > 1)
                            {
                                for (int x = 0; x < e_TileW; x++)
                                {
                                    for (int y = 0; y < e_TileH; y++)
                                    {
                                        e_Map.Mask[(e_CursorX + x), (e_CursorY + y)].TileX = (e_TileX + x) * 32;
                                        e_Map.Mask[(e_CursorX + x), (e_CursorY + y)].TileY = (e_TileY + y) * 32;
                                        e_Map.Mask[(e_CursorX + x), (e_CursorY + y)].TileW = 32;
                                        e_Map.Mask[(e_CursorX + x), (e_CursorY + y)].TileH = 32;
                                        e_Map.Mask[(e_CursorX + x), (e_CursorY + y)].Tileset = e_SelectTileset;
                                    }
                                }
                            }
                            else
                            {
                                e_Map.Mask[(e_CursorX), (e_CursorY)].TileX = (e_TileX * 32);
                                e_Map.Mask[(e_CursorX), (e_CursorY)].TileY = (e_TileY * 32);
                                e_Map.Mask[(e_CursorX), (e_CursorY)].TileW = 32;
                                e_Map.Mask[(e_CursorX), (e_CursorY)].TileH = 32;
                                e_Map.Mask[(e_CursorX), (e_CursorY)].Tileset = e_SelectTileset;
                            }
                            break;
                        case (int)TileLayers.MaskA:
                            if (e_TileW > 1 || e_TileH > 1)
                            {
                                for (int x = 0; x < e_TileW; x++)
                                {
                                    for (int y = 0; y < e_TileH; y++)
                                    {
                                        e_Map.MaskA[(e_CursorX + x), (e_CursorY + y)].TileX = (e_TileX + x) * 32;
                                        e_Map.MaskA[(e_CursorX + x), (e_CursorY + y)].TileY = (e_TileY + y) * 32;
                                        e_Map.MaskA[(e_CursorX + x), (e_CursorY + y)].TileW = 32;
                                        e_Map.MaskA[(e_CursorX + x), (e_CursorY + y)].TileH = 32;
                                        e_Map.MaskA[(e_CursorX + x), (e_CursorY + y)].Tileset = e_SelectTileset;
                                    }
                                }
                            }
                            else
                            {
                                e_Map.MaskA[(e_CursorX), (e_CursorY)].TileX = (e_TileX * 32);
                                e_Map.MaskA[(e_CursorX), (e_CursorY)].TileY = (e_TileY * 32);
                                e_Map.MaskA[(e_CursorX), (e_CursorY)].TileW = 32;
                                e_Map.MaskA[(e_CursorX), (e_CursorY)].TileH = 32;
                                e_Map.MaskA[(e_CursorX), (e_CursorY)].Tileset = e_SelectTileset;
                            }
                            break;
                        case (int)TileLayers.Fringe:
                            if (e_TileW > 1 || e_TileH > 1)
                            {
                                for (int x = 0; x < e_TileW; x++)
                                {
                                    for (int y = 0; y < e_TileH; y++)
                                    {
                                        e_Map.Fringe[(e_CursorX + x), (e_CursorY + y)].TileX = (e_TileX + x) * 32;
                                        e_Map.Fringe[(e_CursorX + x), (e_CursorY + y)].TileY = (e_TileY + y) * 32;
                                        e_Map.Fringe[(e_CursorX + x), (e_CursorY + y)].TileW = 32;
                                        e_Map.Fringe[(e_CursorX + x), (e_CursorY + y)].TileH = 32;
                                        e_Map.Fringe[(e_CursorX + x), (e_CursorY + y)].Tileset = e_SelectTileset;
                                    }
                                }
                            }
                            else
                            {
                                e_Map.Fringe[(e_CursorX), (e_CursorY)].TileX = (e_TileX * 32);
                                e_Map.Fringe[(e_CursorX), (e_CursorY)].TileY = (e_TileY * 32);
                                e_Map.Fringe[(e_CursorX), (e_CursorY)].TileW = 32;
                                e_Map.Fringe[(e_CursorX), (e_CursorY)].TileH = 32;
                                e_Map.Fringe[(e_CursorX), (e_CursorY)].Tileset = e_SelectTileset;
                            }
                            break;
                        case (int)TileLayers.FringeA:
                            if (e_TileW > 1 || e_TileH > 1)
                            {
                                for (int x = 0; x < e_TileW; x++)
                                {
                                    for (int y = 0; y < e_TileH; y++)
                                    {
                                        e_Map.FringeA[(e_CursorX + x), (e_CursorY + y)].TileX = (e_TileX + x) * 32;
                                        e_Map.FringeA[(e_CursorX + x), (e_CursorY + y)].TileY = (e_TileY + y) * 32;
                                        e_Map.FringeA[(e_CursorX + x), (e_CursorY + y)].TileW = 32;
                                        e_Map.FringeA[(e_CursorX + x), (e_CursorY + y)].TileH = 32;
                                        e_Map.FringeA[(e_CursorX + x), (e_CursorY + y)].Tileset = e_SelectTileset;
                                    }
                                }
                            }
                            else
                            {
                                e_Map.FringeA[(e_CursorX), (e_CursorY)].TileX = (e_TileX * 32);
                                e_Map.FringeA[(e_CursorX), (e_CursorY)].TileY = (e_TileY * 32);
                                e_Map.FringeA[(e_CursorX), (e_CursorY)].TileW = 32;
                                e_Map.FringeA[(e_CursorX), (e_CursorY)].TileH = 32;
                                e_Map.FringeA[(e_CursorX), (e_CursorY)].Tileset = e_SelectTileset;
                            }
                            break;
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    switch (e_Layer)
                    {
                        case (int)TileLayers.Ground:
                            e_Map.Ground[(e_CursorX), (e_CursorY)].TileX = 0;
                            e_Map.Ground[(e_CursorX), (e_CursorY)].TileY = 0;
                            e_Map.Ground[(e_CursorX), (e_CursorY)].TileW = 0;
                            e_Map.Ground[(e_CursorX), (e_CursorY)].TileH = 0;
                            e_Map.Ground[(e_CursorX), (e_CursorY)].Tileset = 0;
                            break;
                        case (int)TileLayers.Mask:
                            e_Map.Mask[(e_CursorX), (e_CursorY)].TileX = 0;
                            e_Map.Mask[(e_CursorX), (e_CursorY)].TileY = 0;
                            e_Map.Mask[(e_CursorX), (e_CursorY)].TileW = 0;
                            e_Map.Mask[(e_CursorX), (e_CursorY)].TileH = 0;
                            e_Map.Mask[(e_CursorX), (e_CursorY)].Tileset = 0;
                            break;
                        case (int)TileLayers.MaskA:
                            e_Map.MaskA[(e_CursorX), (e_CursorY)].TileX = 0;
                            e_Map.MaskA[(e_CursorX), (e_CursorY)].TileY = 0;
                            e_Map.MaskA[(e_CursorX), (e_CursorY)].TileW = 0;
                            e_Map.MaskA[(e_CursorX), (e_CursorY)].TileH = 0;
                            e_Map.MaskA[(e_CursorX), (e_CursorY)].Tileset = 0;
                            break;
                        case (int)TileLayers.Fringe:
                            e_Map.Fringe[(e_CursorX), (e_CursorY)].TileX = 0;
                            e_Map.Fringe[(e_CursorX), (e_CursorY)].TileY = 0;
                            e_Map.Fringe[(e_CursorX), (e_CursorY)].TileW = 0;
                            e_Map.Fringe[(e_CursorX), (e_CursorY)].TileH = 0;
                            e_Map.Fringe[(e_CursorX), (e_CursorY)].Tileset = 0;
                            break;
                        case (int)TileLayers.FringeA:
                            e_Map.FringeA[(e_CursorX), (e_CursorY)].TileX = 0;
                            e_Map.FringeA[(e_CursorX), (e_CursorY)].TileY = 0;
                            e_Map.FringeA[(e_CursorX), (e_CursorY)].TileW = 0;
                            e_Map.FringeA[(e_CursorX), (e_CursorY)].TileH = 0;
                            e_Map.FringeA[(e_CursorX), (e_CursorY)].Tileset = 0;
                            break;
                    }
                }
            }
            else if (tabTools.SelectedTab == tabTypes)
            {
                if (e.Button == MouseButtons.Left)
                {
                    e_Map.Ground[e_CursorX, e_CursorY].Type = e_Type;
                    if (e_Type == (int)TileType.NpcSpawn) { e_Map.Ground[e_CursorX, e_CursorY].SpawnNum = e_SpawnNumber; }
                    if (e_Type == (int)TileType.SpawnPool) { e_Map.Ground[e_CursorX, e_CursorY].SpawnNum = e_SpawnNumber; e_Map.Ground[e_CursorX, e_CursorY].SpawnAmount = e_SpawnAmount; }
                    if (e_Type == (int)TileType.MapItem) { e_Map.Ground[e_CursorX, e_CursorY].SpawnNum = e_SpawnNumber; e_Map.Ground[e_CursorX, e_CursorY].SpawnAmount = e_SpawnAmount; }
                    if (e_Type == (int)TileType.Chest) { e_Map.Ground[e_CursorX, e_CursorY].ChestNum = e_Chest; }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    e_Map.Ground[e_CursorX, e_CursorY].Type = (int)TileType.None;
                }
            }
            else if (tabTools.SelectedTab == tabLight)
            {
                if (e.Button == MouseButtons.Left)
                {
                    e_Map.Ground[e_CursorX, e_CursorY].LightRadius = e_LightRadius;
                }
                else if (e.Button == MouseButtons.Right)
                {
                    e_Map.Ground[e_CursorX, e_CursorY].LightRadius = 0;
                }
            }

            if (e.Button == MouseButtons.Middle)
            {
                e_Zoom = 1.0f;
            }
            lblButtonDown.Text = "Button Down: " + e.Button.ToString();
        }

        private void EditorMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //int multiplier = 10;
            //int finalZoom = (int)((e_Zoom - (int)e_Zoom) * multiplier);
            int finalX = (int)(800 * e_Zoom) / 32;
            int finalY = (int)(600 * e_Zoom) / 32;
            picMap.Focus();
            e_CursorX = (e.X / 32) + e_ViewX;
            e_CursorY = (e.Y / 32) + e_ViewY;

            if (e_CursorX < 0 || e_CursorX >= 50 || e_CursorY < 0 || e_CursorY >= 50) { return; }

            if (tabTools.SelectedTab == tabLayer || tabTools.SelectedTab == tabTiles)
            {
                if (e.Button == MouseButtons.Left)
                {
                    switch (e_Layer)
                    {
                        case (int)TileLayers.Ground:
                            if (e_TileW > 1 || e_TileH > 1)
                            {
                                for (int x = 0; x < e_TileW; x++)
                                {
                                    for (int y = 0; y < e_TileH; y++)
                                    {
                                        e_Map.Ground[(e_CursorX + x), (e_CursorY + y)].TileX = (e_TileX + x) * 32;
                                        e_Map.Ground[(e_CursorX + x), (e_CursorY + y)].TileY = (e_TileY + y) * 32;
                                        e_Map.Ground[(e_CursorX + x), (e_CursorY + y)].TileW = 32;
                                        e_Map.Ground[(e_CursorX + x), (e_CursorY + y)].TileH = 32;
                                        e_Map.Ground[(e_CursorX + x), (e_CursorY + y)].Tileset = e_SelectTileset;
                                    }
                                }
                            }
                            else
                            {
                                e_Map.Ground[(e_CursorX), (e_CursorY)].TileX = (e_TileX * 32);
                                e_Map.Ground[(e_CursorX), (e_CursorY)].TileY = (e_TileY * 32);
                                e_Map.Ground[(e_CursorX), (e_CursorY)].TileW = 32;
                                e_Map.Ground[(e_CursorX), (e_CursorY)].TileH = 32;
                                e_Map.Ground[(e_CursorX), (e_CursorY)].Tileset = e_SelectTileset;
                            }
                            break;
                        case (int)TileLayers.Mask:
                            if (e_TileW > 1 || e_TileH > 1)
                            {
                                for (int x = 0; x < e_TileW; x++)
                                {
                                    for (int y = 0; y < e_TileH; y++)
                                    {
                                        e_Map.Mask[(e_CursorX + x), (e_CursorY + y)].TileX = (e_TileX + x) * 32;
                                        e_Map.Mask[(e_CursorX + x), (e_CursorY + y)].TileY = (e_TileY + y) * 32;
                                        e_Map.Mask[(e_CursorX + x), (e_CursorY + y)].TileW = 32;
                                        e_Map.Mask[(e_CursorX + x), (e_CursorY + y)].TileH = 32;
                                        e_Map.Mask[(e_CursorX + x), (e_CursorY + y)].Tileset = e_SelectTileset;
                                    }
                                }
                            }
                            else
                            {
                                e_Map.Mask[(e_CursorX), (e_CursorY)].TileX = (e_TileX * 32);
                                e_Map.Mask[(e_CursorX), (e_CursorY)].TileY = (e_TileY * 32);
                                e_Map.Mask[(e_CursorX), (e_CursorY)].TileW = 32;
                                e_Map.Mask[(e_CursorX), (e_CursorY)].TileH = 32;
                                e_Map.Mask[(e_CursorX), (e_CursorY)].Tileset = e_SelectTileset;
                            }
                            break;
                        case (int)TileLayers.MaskA:
                            if (e_TileW > 1 || e_TileH > 1)
                            {
                                for (int x = 0; x < e_TileW; x++)
                                {
                                    for (int y = 0; y < e_TileH; y++)
                                    {
                                        e_Map.MaskA[(e_CursorX + x), (e_CursorY + y)].TileX = (e_TileX + x) * 32;
                                        e_Map.MaskA[(e_CursorX + x), (e_CursorY + y)].TileY = (e_TileY + y) * 32;
                                        e_Map.MaskA[(e_CursorX + x), (e_CursorY + y)].TileW = 32;
                                        e_Map.MaskA[(e_CursorX + x), (e_CursorY + y)].TileH = 32;
                                        e_Map.MaskA[(e_CursorX + x), (e_CursorY + y)].Tileset = e_SelectTileset;
                                    }
                                }
                            }
                            else
                            {
                                e_Map.MaskA[(e_CursorX), (e_CursorY)].TileX = (e_TileX * 32);
                                e_Map.MaskA[(e_CursorX), (e_CursorY)].TileY = (e_TileY * 32);
                                e_Map.MaskA[(e_CursorX), (e_CursorY)].TileW = 32;
                                e_Map.MaskA[(e_CursorX), (e_CursorY)].TileH = 32;
                                e_Map.MaskA[(e_CursorX), (e_CursorY)].Tileset = e_SelectTileset;
                            }
                            break;
                        case (int)TileLayers.Fringe:
                            if (e_TileW > 1 || e_TileH > 1)
                            {
                                for (int x = 0; x < e_TileW; x++)
                                {
                                    for (int y = 0; y < e_TileH; y++)
                                    {
                                        e_Map.Fringe[(e_CursorX + x), (e_CursorY + y)].TileX = (e_TileX + x) * 32;
                                        e_Map.Fringe[(e_CursorX + x), (e_CursorY + y)].TileY = (e_TileY + y) * 32;
                                        e_Map.Fringe[(e_CursorX + x), (e_CursorY + y)].TileW = 32;
                                        e_Map.Fringe[(e_CursorX + x), (e_CursorY + y)].TileH = 32;
                                        e_Map.Fringe[(e_CursorX + x), (e_CursorY + y)].Tileset = e_SelectTileset;
                                    }
                                }
                            }
                            else
                            {
                                e_Map.Fringe[(e_CursorX), (e_CursorY)].TileX = (e_TileX * 32);
                                e_Map.Fringe[(e_CursorX), (e_CursorY)].TileY = (e_TileY * 32);
                                e_Map.Fringe[(e_CursorX), (e_CursorY)].TileW = 32;
                                e_Map.Fringe[(e_CursorX), (e_CursorY)].TileH = 32;
                                e_Map.Fringe[(e_CursorX), (e_CursorY)].Tileset = e_SelectTileset;
                            }
                            break;
                        case (int)TileLayers.FringeA:
                            if (e_TileW > 1 || e_TileH > 1)
                            {
                                for (int x = 0; x < e_TileW; x++)
                                {
                                    for (int y = 0; y < e_TileH; y++)
                                    {
                                        e_Map.FringeA[(e_CursorX + x), (e_CursorY + y)].TileX = (e_TileX + x) * 32;
                                        e_Map.FringeA[(e_CursorX + x), (e_CursorY + y)].TileY = (e_TileY + y) * 32;
                                        e_Map.FringeA[(e_CursorX + x), (e_CursorY + y)].TileW = 32;
                                        e_Map.FringeA[(e_CursorX + x), (e_CursorY + y)].TileH = 32;
                                        e_Map.FringeA[(e_CursorX + x), (e_CursorY + y)].Tileset = e_SelectTileset;
                                    }
                                }
                            }
                            else
                            {
                                e_Map.FringeA[(e_CursorX), (e_CursorY)].TileX = (e_TileX * 32);
                                e_Map.FringeA[(e_CursorX), (e_CursorY)].TileY = (e_TileY * 32);
                                e_Map.FringeA[(e_CursorX), (e_CursorY)].TileW = 32;
                                e_Map.FringeA[(e_CursorX), (e_CursorY)].TileH = 32;
                                e_Map.FringeA[(e_CursorX), (e_CursorY)].Tileset = e_SelectTileset;
                            }
                            break;
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    switch (e_Layer)
                    {
                        case (int)TileLayers.Ground:
                            e_Map.Ground[(e_CursorX), (e_CursorY)].TileX = 0;
                            e_Map.Ground[(e_CursorX), (e_CursorY)].TileY = 0;
                            e_Map.Ground[(e_CursorX), (e_CursorY)].TileW = 0;
                            e_Map.Ground[(e_CursorX), (e_CursorY)].TileH = 0;
                            e_Map.Ground[(e_CursorX), (e_CursorY)].Tileset = 0;
                            break;
                        case (int)TileLayers.Mask:
                            e_Map.Mask[(e_CursorX), (e_CursorY)].TileX = 0;
                            e_Map.Mask[(e_CursorX), (e_CursorY)].TileY = 0;
                            e_Map.Mask[(e_CursorX), (e_CursorY)].TileW = 0;
                            e_Map.Mask[(e_CursorX), (e_CursorY)].TileH = 0;
                            e_Map.Mask[(e_CursorX), (e_CursorY)].Tileset = 0;
                            break;
                        case (int)TileLayers.MaskA:
                            e_Map.MaskA[(e_CursorX), (e_CursorY)].TileX = 0;
                            e_Map.MaskA[(e_CursorX), (e_CursorY)].TileY = 0;
                            e_Map.MaskA[(e_CursorX), (e_CursorY)].TileW = 0;
                            e_Map.MaskA[(e_CursorX), (e_CursorY)].TileH = 0;
                            e_Map.MaskA[(e_CursorX), (e_CursorY)].Tileset = 0;
                            break;
                        case (int)TileLayers.Fringe:
                            e_Map.Fringe[(e_CursorX), (e_CursorY)].TileX = 0;
                            e_Map.Fringe[(e_CursorX), (e_CursorY)].TileY = 0;
                            e_Map.Fringe[(e_CursorX), (e_CursorY)].TileW = 0;
                            e_Map.Fringe[(e_CursorX), (e_CursorY)].TileH = 0;
                            e_Map.Fringe[(e_CursorX), (e_CursorY)].Tileset = 0;
                            break;
                        case (int)TileLayers.FringeA:
                            e_Map.FringeA[(e_CursorX), (e_CursorY)].TileX = 0;
                            e_Map.FringeA[(e_CursorX), (e_CursorY)].TileY = 0;
                            e_Map.FringeA[(e_CursorX), (e_CursorY)].TileW = 0;
                            e_Map.FringeA[(e_CursorX), (e_CursorY)].TileH = 0;
                            e_Map.FringeA[(e_CursorX), (e_CursorY)].Tileset = 0;
                            break;
                    }
                }
            }
            else if (tabTools.SelectedTab == tabTypes)
            {
                if (e.Button == MouseButtons.Left)
                {
                    e_Map.Ground[e_CursorX, e_CursorY].Type = e_Type;
                    e_Map.Ground[e_CursorX, e_CursorY].Type = e_Type;
                    if (e_Type == (int)TileType.NpcSpawn) { e_Map.Ground[e_CursorX, e_CursorY].SpawnNum = e_SpawnNumber; }
                    if (e_Type == (int)TileType.SpawnPool) { e_Map.Ground[e_CursorX, e_CursorY].SpawnNum = e_SpawnNumber; e_Map.Ground[e_CursorX, e_CursorY].SpawnAmount = e_SpawnAmount; }
                    if (e_Type == (int)TileType.MapItem) { e_Map.Ground[e_CursorX, e_CursorY].SpawnNum = e_SpawnNumber; e_Map.Ground[e_CursorX, e_CursorY].SpawnAmount = e_SpawnAmount; }
                    if (e_Type == (int)TileType.Chest) { e_Map.Ground[e_CursorX, e_CursorY].ChestNum = e_Chest; }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    e_Map.Ground[e_CursorX, e_CursorY].Type = (int)TileType.None;
                }
            }
            lblButtonDown.Text = "Button Down: " + e.Button.ToString();
            lblMouseLoc.Text = "Mouse X: " + e_CursorX + ", Mouse Y: " + e_CursorY;
        }

        private void EditorMouseWheelScroll(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e_WheelOption == 0)
            {
                //Wheel up
                if (e.Delta > 0)
                {
                    e_Zoom -= 0.1f;
                }

                //Wheel down
                if (e.Delta < 0)
                {
                    e_Zoom += 0.1f;
                }
            }
            else
            {
                //Wheel up
                if (e.Delta > 0)
                {
                    if (e_ViewY > 0)
                    {
                        e_ViewY -= 1;
                        scrlViewY.Value = e_ViewY;
                    }
                }

                //Wheel down
                if (e.Delta < 0)
                {
                    if (e_ViewY < (50 - e_OffsetY))
                    {
                        e_ViewY += 1;
                        scrlViewY.Value = e_ViewY;
                    }
                }
            }
        }

        private void EditorTilesetKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    if (e_TileH > 1)
                    {
                        e_TileH -= 1;
                    }
                    break;
                case Keys.S:
                    e_TileH += 1;
                    break;
                case Keys.A:
                    if (e_TileW > 1)
                    {
                        e_TileW -= 1;
                    }
                    break;
                case Keys.D:
                    e_TileW += 1;
                    break;
            }

            lblSelectW.Text = "Select Tile W: " + e_TileW;
            lblSelectH.Text = "Select Tile H: " + e_TileH;
        }

        private void EditorTilesetMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            picTileset.Focus();
            if (e.Button == MouseButtons.Left)
            {
                e_TileX = (e.X / 32);
                e_TileY = (e.Y / 32);
            }

            lblSelectX.Text = "Select Tile X: " + e_TileX;
            lblSelectY.Text = "Select Tile Y: " + e_TileY;
            lblButtonDown.Text = "Button Down: " + e.Button.ToString();
        }

        private void EditorTilesetPaint(object sender, PaintEventArgs e)
        {
            Graphics e_Graphics = e.Graphics;
            Pen e_Pen = new Pen(System.Drawing.Color.Red, 1);
            Rectangle e_SelectedTile = new Rectangle(new Point(e_TileX * 32, e_TileY * 32), new Size(e_TileW * 32, e_TileH * 32));
            e_Graphics.DrawRectangle(e_Pen, e_SelectedTile);
        }

        private void cmbTileset_SelectedIndexChanged(object sender, EventArgs e)
        {
            picTileset.Image = System.Drawing.Image.FromFile("Resources/Tilesets/" + (cmbTileset.SelectedIndex + 1) + ".png");
            picTileset.BackColor = System.Drawing.Color.HotPink;
            e_SelectTileset = cmbTileset.SelectedIndex;
        }

        private void radGround_CheckedChanged(object sender, EventArgs e)
        {
            e_Layer = (int)TileLayers.Ground;
            lblLayer.Text = "Layer: Ground";
        }

        private void radMask_CheckedChanged(object sender, EventArgs e)
        {
            e_Layer = (int)TileLayers.Mask;
            lblLayer.Text = "Layer: Mask";
        }

        private void radMask2_CheckedChanged(object sender, EventArgs e)
        {
            e_Layer = (int)TileLayers.MaskA;
            lblLayer.Text = "Layer: Mask 2";
        }

        private void radFringe_CheckedChanged(object sender, EventArgs e)
        {
            e_Layer = (int)TileLayers.Fringe;
            lblLayer.Text = "Layer: Fringe";
        }

        private void radFringe2_CheckedChanged(object sender, EventArgs e)
        {
            e_Layer = (int)TileLayers.FringeA;
            lblLayer.Text = "Layer: Fringe A";
        }

        private void radNone_CheckedChanged(object sender, EventArgs e)
        {
            e_Type = (int)TileType.None;
            lblType.Text = "Type: None";
            pnlNpcSpawn.Visible = false;
            pnlMapItem.Visible = false;
            pnlChest.Visible = false;
        }

        private void radBlocked_CheckedChanged(object sender, EventArgs e)
        {
            e_Type = (int)TileType.Blocked;
            lblType.Text = "Type: Blocked";
            pnlNpcSpawn.Visible = false;
            pnlMapItem.Visible = false;
            pnlChest.Visible = false;
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

        private void radSpawnNpc_CheckedChanged(object sender, EventArgs e)
        {
            e_Type = (int)TileType.NpcSpawn;
            lblType.Text = "Type: Npc Spawn";
            e_SpawnNumber = 1;
            e_SpawnAmount = 1;
            pnlNpcSpawn.Visible = true;
            scrlSpawnAmount.Enabled = false;
            pnlMapItem.Visible = false;
            pnlChest.Visible = false;
        }

        private void radNpcAvoid_CheckedChanged(object sender, EventArgs e)
        {
            e_Type = (int)TileType.NpcAvoid;
            lblType.Text = "Type: Npc Avoid";
            pnlNpcSpawn.Visible = false;
            pnlMapItem.Visible = false;
            pnlChest.Visible = false;
        }

        private void radSpawnPool_CheckedChanged(object sender, EventArgs e)
        {
            e_Type = (int)TileType.SpawnPool;
            lblType.Text = "Type: Spawn Pool";
            e_SpawnNumber = 1;
            e_SpawnAmount = 1;
            pnlNpcSpawn.Visible = true;
            scrlSpawnAmount.Enabled = true;
            pnlMapItem.Visible = false;
            pnlChest.Visible = false;
        }

        private void radMapItem_CheckedChanged(object sender, EventArgs e)
        {
            e_Type = (int)TileType.MapItem;
            lblType.Text = "Type: Map Item";
            e_SpawnNumber = 1;
            e_SpawnAmount = 1;
            pnlMapItem.Visible = true;
            pnlNpcSpawn.Visible = false;
            pnlChest.Visible = false;
        }

        private void scrlItemNum_Scroll(object sender, ScrollEventArgs e)
        {
            e_SpawnNumber = scrlItemNum.Value;            
            lblItemNum.Text = "Item Number: " + scrlItemNum.Value;
        }

        private void scrlItemAmount_Scroll(object sender, ScrollEventArgs e)
        {
            e_SpawnAmount = scrlItemAmount.Value;
            lblItemAmount.Text = "Amount: " + scrlItemAmount.Value;
        }

        private void scrlNpcNum_Scroll(object sender, ScrollEventArgs e)
        {
            e_SpawnNumber = (scrlNpcNum.Value);
            lblNpcSpawn.Text = "Npc Number: " + (scrlNpcNum.Value);
        }

        private void scrlSpawnAmount_Scroll(object sender, ScrollEventArgs e)
        {
            e_SpawnAmount = (scrlSpawnAmount.Value);
            lblSpawnAmount.Text = "Amount: " + (scrlSpawnAmount.Value);
        }

        private void btnCloseNpcs_Click(object sender, EventArgs e)
        {
            pnlMapNpcs.Visible = false;
        }

        private void cmbNpc1_SelectedIndexChanged(object sender, EventArgs e)
        {
            e_Map.m_MapNpc[0].NpcNum = cmbNpc1.SelectedIndex;
        }

        private void cmbNpc2_SelectedIndexChanged(object sender, EventArgs e)
        {
            e_Map.m_MapNpc[1].NpcNum = cmbNpc2.SelectedIndex;
        }

        private void cmbNpc3_SelectedIndexChanged(object sender, EventArgs e)
        {
            e_Map.m_MapNpc[2].NpcNum = cmbNpc3.SelectedIndex;
        }

        private void cmbNpc4_SelectedIndexChanged(object sender, EventArgs e)
        {
            e_Map.m_MapNpc[3].NpcNum = cmbNpc4.SelectedIndex;
        }

        private void cmbNpc5_SelectedIndexChanged(object sender, EventArgs e)
        {
            e_Map.m_MapNpc[4].NpcNum = cmbNpc5.SelectedIndex;
        }

        private void cmbNpc6_SelectedIndexChanged(object sender, EventArgs e)
        {
            e_Map.m_MapNpc[5].NpcNum = cmbNpc6.SelectedIndex;
        }

        private void cmbNpc7_SelectedIndexChanged(object sender, EventArgs e)
        {
            e_Map.m_MapNpc[6].NpcNum = cmbNpc7.SelectedIndex;
        }

        private void cmbNpc8_SelectedIndexChanged(object sender, EventArgs e)
        {
            e_Map.m_MapNpc[7].NpcNum = cmbNpc8.SelectedIndex;
        }

        private void cmbNpc9_SelectedIndexChanged(object sender, EventArgs e)
        {
            e_Map.m_MapNpc[8].NpcNum = cmbNpc9.SelectedIndex;
        }

        private void cmbNpc10_SelectedIndexChanged(object sender, EventArgs e)
        {
            e_Map.m_MapNpc[9].NpcNum = cmbNpc10.SelectedIndex;
        }

        private void scrlViewY_Scroll(object sender, ScrollEventArgs e)
        {
            e_ViewY = scrlViewY.Value;
            lblViewY.Text = "View Y: " + (scrlViewY.Value);
            }

        private void scrlViewX_Scroll(object sender, ScrollEventArgs e)
        {
            e_ViewX = scrlViewX.Value;
            lblViewX.Text = "View X: " + (scrlViewX.Value);
        }

        private void treeMaps_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SelectedIndex = treeMaps.SelectedNode.Index + 1;
            e_Map.LoadMapFromDatabase(SelectedIndex);
            mapProperties.SelectedObject = e_Map;
        }

        private void btnNewMap_Click(object sender, EventArgs e)
        {
            e_Map.CreateMapInDatabase();
            int result;
            if (Server.DBType == Globals.SQL_DATABASE_REMOTE.ToString())
            {
                string connection = "Data Source=" + Server.sqlServer + ";Initial Catalog=" + Server.sqlDatabase + ";Integrated Security=True";
                using (var sql = new SqlConnection(connection))
                {
                    sql.Open();
                    string command = "SELECT COUNT(*) FROM MAPS";
                    using (SqlCommand cmd = new SqlCommand(command, sql))
                    {
                        object count = cmd.ExecuteScalar();
                        result = ToInt32(count);
                    }
                }
            }
            else
            {
                using (var conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
                {
                    using (var cmd = new SQLiteCommand(conn))
                    {
                        conn.Open();
                        cmd.CommandText = "SELECT COUNT(*) FROM MAPS";
                        object queue = cmd.ExecuteScalar();
                        result = ToInt32(queue);
                    }
                }
            }
            SelectedIndex = result;
            e_Map.LoadMapFromDatabase(result);
            LoadMapList();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            e_Map.SaveMapInDatabase(SelectedIndex);
            LoadMapList();
        }

        private void btnMapNpcs_Click(object sender, EventArgs e)
        {
            pnlMapNpcs.Visible = true;
        }

        private void btnFillMap_Click(object sender, EventArgs e)
        {
            switch (e_Layer)
            {
                case (int)TileLayers.Ground:
                    for (int x = 0; x < 50; x++)
                    {
                        for (int y = 0; y < 50; y++)
                        {
                            e_Map.Ground[x, y].TileX = (e_TileX * 32);
                            e_Map.Ground[x, y].TileY = (e_TileY * 32);
                            e_Map.Ground[x, y].TileW = 32;
                            e_Map.Ground[x, y].TileH = 32;
                            e_Map.Ground[x, y].Tileset = e_SelectTileset;
                        }
                    }
                    break;
                case (int)TileLayers.Mask:
                    for (int x = 0; x < 50; x++)
                    {
                        for (int y = 0; y < 50; y++)
                        {
                            e_Map.Mask[x, y].TileX = (e_TileX * 32);
                            e_Map.Mask[x, y].TileY = (e_TileY * 32);
                            e_Map.Mask[x, y].TileW = 32;
                            e_Map.Mask[x, y].TileH = 32;
                            e_Map.Mask[x, y].Tileset = e_SelectTileset;
                        }
                    }
                    break;
                case (int)TileLayers.MaskA:
                    for (int x = 0; x < 50; x++)
                    {
                        for (int y = 0; y < 50; y++)
                        {
                            e_Map.MaskA[x, y].TileX = (e_TileX * 32);
                            e_Map.MaskA[x, y].TileY = (e_TileY * 32);
                            e_Map.MaskA[x, y].TileW = 32;
                            e_Map.MaskA[x, y].TileH = 32;
                            e_Map.MaskA[x, y].Tileset = e_SelectTileset;
                        }
                    }
                    break;
                case (int)TileLayers.Fringe:
                    for (int x = 0; x < 50; x++)
                    {
                        for (int y = 0; y < 50; y++)
                        {
                            e_Map.Fringe[x, y].TileX = (e_TileX * 32);
                            e_Map.Fringe[x, y].TileY = (e_TileY * 32);
                            e_Map.Fringe[x, y].TileW = 32;
                            e_Map.Fringe[x, y].TileH = 32;
                            e_Map.Fringe[x, y].Tileset = e_SelectTileset;
                        }
                    }
                    break;
                case (int)TileLayers.FringeA:
                    for (int x = 0; x < 50; x++)
                    {
                        for (int y = 0; y < 50; y++)
                        {
                            e_Map.FringeA[x, y].TileX = (e_TileX * 32);
                            e_Map.FringeA[x, y].TileY = (e_TileY * 32);
                            e_Map.FringeA[x, y].TileW = 32;
                            e_Map.FringeA[x, y].TileH = 32;
                            e_Map.FringeA[x, y].Tileset = e_SelectTileset;
                        }
                    }
                    break;
            }
        }

        private void btnDebug_Click(object sender, EventArgs e)
        {
            if (btnDebug.Checked)
            {
                pnlDebug.Visible = true;
            }
            else
            {
                pnlDebug.Visible = false;
            }
        }

        static int CalculateFrameRate()
        {
            if (TickCount - lastTick >= 1000)
            {
                lastFrameRate = frameRate;
                frameRate = 0;
                lastTick = TickCount;
            }
            frameRate++;
            return lastFrameRate;
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start(@"C:\Users\Steve\Source\Repos\Sabertooth\Help\Map Editor Help\Map Editor.chm");
        }

        private void btnDeleteLayer_Click(object sender, EventArgs e)
        {
            string w_Message = "Are you sure you want to delete this layer?";
            string w_Caption = "Unsaved data";
            MessageBoxButtons w_Buttons = MessageBoxButtons.YesNo;
            DialogResult w_Result;
            w_Result = MessageBox.Show(w_Message, w_Caption, w_Buttons);
            if (w_Result == DialogResult.No) { return; }

            switch (e_Layer)
            {
                case (int)TileLayers.Ground:
                    for (int x = 0; x < 50; x++)
                    {
                        for (int y = 0; y < 50; y++)
                        {
                            e_Map.Ground[x, y].TileX = 0;
                            e_Map.Ground[x, y].TileY = 0;
                            e_Map.Ground[x, y].TileW = 0;
                            e_Map.Ground[x, y].TileH = 0;
                            e_Map.Ground[x, y].Tileset = 0;
                        }
                    }
                    break;
                case (int)TileLayers.Mask:
                    for (int x = 0; x < 50; x++)
                    {
                        for (int y = 0; y < 50; y++)
                        {
                            e_Map.Mask[x, y].TileX = 0;
                            e_Map.Mask[x, y].TileY = 0;
                            e_Map.Mask[x, y].TileW = 0;
                            e_Map.Mask[x, y].TileH = 0;
                            e_Map.Mask[x, y].Tileset = 0;
                        }
                    }
                    break;
                case (int)TileLayers.MaskA:
                    for (int x = 0; x < 50; x++)
                    {
                        for (int y = 0; y < 50; y++)
                        {
                            e_Map.MaskA[x, y].TileX = 0;
                            e_Map.MaskA[x, y].TileY = 0;
                            e_Map.MaskA[x, y].TileW = 0;
                            e_Map.MaskA[x, y].TileH = 0;
                            e_Map.MaskA[x, y].Tileset = 0;
                        }
                    }
                    break;
                case (int)TileLayers.Fringe:
                    for (int x = 0; x < 50; x++)
                    {
                        for (int y = 0; y < 50; y++)
                        {
                            e_Map.Fringe[x, y].TileX = 0;
                            e_Map.Fringe[x, y].TileY = 0;
                            e_Map.Fringe[x, y].TileW = 0;
                            e_Map.Fringe[x, y].TileH = 0;
                            e_Map.Fringe[x, y].Tileset = 0;
                        }
                    }
                    break;
                case (int)TileLayers.FringeA:
                    for (int x = 0; x < 50; x++)
                    {
                        for (int y = 0; y < 50; y++)
                        {
                            e_Map.FringeA[x, y].TileX = 0;
                            e_Map.FringeA[x, y].TileY = 0;
                            e_Map.FringeA[x, y].TileW = 0;
                            e_Map.FringeA[x, y].TileH = 0;
                            e_Map.FringeA[x, y].Tileset = 0;
                        }
                    }
                    break;
            }
            if (tabTypes.Focused)
            {
                for (int x = 0; x < 50; x++)
                {
                    for (int y = 0; y < 50; y++)
                    {
                        e_Map.Ground[x, y].Type = (int)TileType.None;
                    }
                }
            }
        }

        private void scrlChest_Scroll(object sender, ScrollEventArgs e)
        {
            lblChest.Text = "Chest: " + scrlChest.Value;
            e_Chest = scrlChest.Value;            
        }

        private void radChest_CheckedChanged(object sender, EventArgs e)
        {
            e_Type = (int)TileType.Chest;
            lblType.Text = "Type: Chest";
            pnlChest.Visible = true;
            pnlNpcSpawn.Visible = false;
            pnlMapItem.Visible = false;
        }

        private void scrlIntensity_Scroll(object sender, ScrollEventArgs e)
        {
            lblIntensity.Text = "Intensity: " + scrlIntensity.Value;
            e_LightRadius = scrlIntensity.Value;
        }

        private void radZoom_CheckedChanged(object sender, EventArgs e)
        {
            e_WheelOption = 0;
        }

        private void radScroll_CheckedChanged(object sender, EventArgs e)
        {
            e_WheelOption = 1;
        }
    }

    public class RenderText
    {
        SFML.Graphics.Font e_Font = new SFML.Graphics.Font("Resources/Fonts/Arial.ttf");
        Text e_Text = new Text();

        public void DrawText(RenderWindow e_Window, string eText, Vector2f position, uint e_Size, SFML.Graphics.Color e_Color)
        {
            e_Text.Font = e_Font; //set the font
            e_Text.CharacterSize = e_Size;  //set it size
            e_Text.Position = position;    //set its location on the screen
            e_Text.DisplayedString = eText;    //what is actually being displayed (text)
            e_Text.Color = e_Color; //the color of the text we are drawing
            e_Text.Style = Text.Styles.Bold;

            e_Window.Draw(e_Text);  //window drawing function
        }
    }
}
