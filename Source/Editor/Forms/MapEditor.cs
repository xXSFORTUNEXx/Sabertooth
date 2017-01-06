using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SFML.Window;
using SFML.Graphics;
using SFML.System;
using static System.Environment;
using Editor.Classes;
using static System.Windows.Forms.Application;
using System.Drawing;
using System.Data.SQLite;

namespace Editor.Forms
{
    public partial class MapEditor : Form
    {
        RenderWindow e_Window;
        RenderText e_Text = new RenderText();
        Map e_Map = new Map();
        SFML.Graphics.View e_View = new SFML.Graphics.View();
        Texture[] e_Tileset = new Texture[68];
        Sprite e_SelectedTile = new Sprite();
        Texture e_GridTexture = new Texture("Resources/Tilesets/Grid.png");
        Sprite e_Grid = new Sprite();
        SQLiteConnection e_Database;
        Npc e_Npc = new Npc();
        Texture[] e_Texture = new Texture[204];
        int e_ViewX { get; set; }
        int e_ViewY { get; set; }
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
        static int lastFrameRate;
        static int frameRate;
        static int lastTick;

        public MapEditor()
        {
            InitializeComponent();
            Visible = true;

            picMap.KeyDown += new KeyEventHandler(EditorKeyDown);
            picMap.MouseDown += new MouseEventHandler(EditorMouseDown);
            picMap.MouseMove += new MouseEventHandler(EditorMouseMove);
            picTileset.MouseDown += new MouseEventHandler(EditorTilesetMouseDown);
            picTileset.Paint += new PaintEventHandler(EditorTilesetPaint);
            picTileset.KeyDown += new KeyEventHandler(EditorTilesetKeyDown);

            for (int i = 0; i < 68; i++)
            {
                cmbTileset.Items.Add("Tileset: " + (i + 1));
                e_Tileset[i] = new Texture("Resources/Tilesets/" + (i + 1) + ".png");
            }

            for (int s = 0; s < 204; s++)
            {
                e_Texture[s] = new Texture("Resources/Characters/" + (s + 1) + ".png");
            }

            e_Database = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;");
            e_Database.Open();
            string sql;

            sql = "SELECT COUNT(*) FROM NPCS";

            SQLiteCommand sql_Command = new SQLiteCommand(sql, e_Database);
            int result = int.Parse(sql_Command.ExecuteScalar().ToString());
            e_Database.Close();

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

            e_Map.LoadMap();

            cmbNpc1.SelectedIndex = e_Map.mapNpc[0].npcNum;
            cmbNpc2.SelectedIndex = e_Map.mapNpc[1].npcNum;
            cmbNpc3.SelectedIndex = e_Map.mapNpc[2].npcNum;
            cmbNpc4.SelectedIndex = e_Map.mapNpc[3].npcNum;
            cmbNpc5.SelectedIndex = e_Map.mapNpc[4].npcNum;
            cmbNpc6.SelectedIndex = e_Map.mapNpc[5].npcNum;
            cmbNpc7.SelectedIndex = e_Map.mapNpc[6].npcNum;
            cmbNpc8.SelectedIndex = e_Map.mapNpc[7].npcNum;
            cmbNpc9.SelectedIndex = e_Map.mapNpc[8].npcNum;
            cmbNpc10.SelectedIndex = e_Map.mapNpc[9].npcNum;
            txtName.Text = e_Map.Name;

            MapEditorLoop();
        }

        void MapEditorLoop()
        {
            e_Window = new RenderWindow(picMap.Handle);
            e_Window.SetFramerateLimit(40);
            cmbTileset.SelectedIndex = 0;
            e_Layer = (int)TileLayers.Ground;
            e_Type = (int)TileType.None;

            while (Visible)
            {
                if (tabTools.SelectedTab == tabTiles) { picTileset.Invalidate(); }
                
                e_View.Reset(new FloatRect(0, 0, 800, 600));
                e_View.Move(new Vector2f(e_ViewX * 32, e_ViewY * 32));
                e_Window.SetView(e_View);

                DoEvents();
                e_Window.DispatchEvents();

                e_Window.Clear();
                DrawTiles();
                DrawNpcs();
                DrawTypes();
                DrawGrid();
                Text = "Map Editor - FPS: " + CalculateFrameRate();                
                e_Window.Display();
            }
            Visible = false;
        }

        void DrawTiles()
        {
            if (pnlMapNpcs.Visible) { return; }

            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    e_Map.DrawTile(e_Window, new Vector2f((x * 32), (y * 32)), e_Map.Ground[x, y].tileX, e_Map.Ground[x, y].tileY, e_Map.Ground[x, y].tileW, e_Map.Ground[x, y].tileH, e_Map.Ground[x, y].Tileset);

                    if (e_Map.Mask[x, y].tileX > 0 || e_Map.Mask[x, y].tileY > 0)
                    {
                        e_Map.DrawTile(e_Window, new Vector2f((x * 32), (y * 32)), e_Map.Mask[x, y].tileX, e_Map.Mask[x, y].tileY, e_Map.Mask[x, y].tileW, e_Map.Mask[x, y].tileH, e_Map.Mask[x, y].Tileset);
                    }
                    if (e_Map.MaskA[x, y].tileX > 0 || e_Map.MaskA[x, y].tileY > 0)
                    {
                        e_Map.DrawTile(e_Window, new Vector2f((x * 32), (y * 32)), e_Map.MaskA[x, y].tileX, e_Map.MaskA[x, y].tileY, e_Map.MaskA[x, y].tileW, e_Map.MaskA[x, y].tileH, e_Map.MaskA[x, y].Tileset);
                    }
                    if (e_Map.Fringe[x, y].tileX > 0 || e_Map.Fringe[x, y].tileY > 0)
                    {
                        e_Map.DrawTile(e_Window, new Vector2f((x * 32), (y * 32)), e_Map.Fringe[x, y].tileX, e_Map.Fringe[x, y].tileY, e_Map.Fringe[x, y].tileW, e_Map.Fringe[x, y].tileH, e_Map.Fringe[x, y].Tileset);
                    }
                    if (e_Map.FringeA[x, y].tileX > 0 || e_Map.FringeA[x, y].tileY > 0)
                    {
                        e_Map.DrawTile(e_Window, new Vector2f((x * 32), (y * 32)), e_Map.FringeA[x, y].tileX, e_Map.FringeA[x, y].tileY, e_Map.FringeA[x, y].tileW, e_Map.FringeA[x, y].tileH, e_Map.FringeA[x, y].Tileset);
                    }
                }
            }

            if (picMap.Focused)
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
                        if (e_Map.Ground[x, y].type != (int)TileType.None)
                        {
                            switch (e_Map.Ground[x, y].type)
                            {
                                case (int)TileType.Blocked:
                                    e_Text.DrawText(e_Window, "B", new Vector2f((x * 32) + 12, (y * 32) + 7), 14, SFML.Graphics.Color.Red);
                                    break;
                                case (int)TileType.NpcSpawn:
                                    e_Text.DrawText(e_Window, "N", new Vector2f((x * 32) + 12, (y * 32) + 7), 14, SFML.Graphics.Color.Yellow);
                                    break;
                                case (int)TileType.SpawnPool:
                                    e_Text.DrawText(e_Window, "S", new Vector2f((x * 32) + 12, (y * 32) + 7), 14, SFML.Graphics.Color.Green);
                                    break;
                                case (int)TileType.NpcAvoid:
                                    e_Text.DrawText(e_Window, "A", new Vector2f((x * 32) + 12, (y * 32) + 7), 14, SFML.Graphics.Color.White);
                                    break;
                                default:
                                    break;
                            }
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

                for (int x = 0; x < 25; x++)
                {
                    for (int y = 0; y < 19; y++)
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
                        if (e_Map.Ground[x, y].type == (int)TileType.NpcSpawn)
                        {
                            if (e_Map.Ground[x, y].SpawnNum > 0)
                            {
                                int npcNum = e_Map.Ground[x, y].SpawnNum;
                                e_Npc.LoadNpcFromDatabase(npcNum);
                                e_Npc.DrawNpc(e_Window, e_Texture[e_Npc.Sprite - 1], x, y);
                            }
                        }
                    }
                }
            }
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
                                        e_Map.Ground[(e_CursorX + x), (e_CursorY + y)].tileX = (e_TileX + x) * 32;
                                        e_Map.Ground[(e_CursorX + x), (e_CursorY + y)].tileY = (e_TileY + y) * 32;
                                        e_Map.Ground[(e_CursorX + x), (e_CursorY + y)].tileW = 32;
                                        e_Map.Ground[(e_CursorX + x), (e_CursorY + y)].tileH = 32;
                                        e_Map.Ground[(e_CursorX + x), (e_CursorY + y)].Tileset = e_SelectTileset;
                                    }
                                }
                            }
                            else
                            {
                                e_Map.Ground[(e_CursorX), (e_CursorY)].tileX = (e_TileX * 32);
                                e_Map.Ground[(e_CursorX), (e_CursorY)].tileY = (e_TileY * 32);
                                e_Map.Ground[(e_CursorX), (e_CursorY)].tileW = 32;
                                e_Map.Ground[(e_CursorX), (e_CursorY)].tileH = 32;
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
                                        e_Map.Mask[(e_CursorX + x), (e_CursorY + y)].tileX = (e_TileX + x) * 32;
                                        e_Map.Mask[(e_CursorX + x), (e_CursorY + y)].tileY = (e_TileY + y) * 32;
                                        e_Map.Mask[(e_CursorX + x), (e_CursorY + y)].tileW = 32;
                                        e_Map.Mask[(e_CursorX + x), (e_CursorY + y)].tileH = 32;
                                        e_Map.Mask[(e_CursorX + x), (e_CursorY + y)].Tileset = e_SelectTileset;
                                    }
                                }
                            }
                            else
                            {
                                e_Map.Mask[(e_CursorX), (e_CursorY)].tileX = (e_TileX * 32);
                                e_Map.Mask[(e_CursorX), (e_CursorY)].tileY = (e_TileY * 32);
                                e_Map.Mask[(e_CursorX), (e_CursorY)].tileW = 32;
                                e_Map.Mask[(e_CursorX), (e_CursorY)].tileH = 32;
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
                                        e_Map.MaskA[(e_CursorX + x), (e_CursorY + y)].tileX = (e_TileX + x) * 32;
                                        e_Map.MaskA[(e_CursorX + x), (e_CursorY + y)].tileY = (e_TileY + y) * 32;
                                        e_Map.MaskA[(e_CursorX + x), (e_CursorY + y)].tileW = 32;
                                        e_Map.MaskA[(e_CursorX + x), (e_CursorY + y)].tileH = 32;
                                        e_Map.MaskA[(e_CursorX + x), (e_CursorY + y)].Tileset = e_SelectTileset;
                                    }
                                }
                            }
                            else
                            {
                                e_Map.MaskA[(e_CursorX), (e_CursorY)].tileX = (e_TileX * 32);
                                e_Map.MaskA[(e_CursorX), (e_CursorY)].tileY = (e_TileY * 32);
                                e_Map.MaskA[(e_CursorX), (e_CursorY)].tileW = 32;
                                e_Map.MaskA[(e_CursorX), (e_CursorY)].tileH = 32;
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
                                        e_Map.Fringe[(e_CursorX + x), (e_CursorY + y)].tileX = (e_TileX + x) * 32;
                                        e_Map.Fringe[(e_CursorX + x), (e_CursorY + y)].tileY = (e_TileY + y) * 32;
                                        e_Map.Fringe[(e_CursorX + x), (e_CursorY + y)].tileW = 32;
                                        e_Map.Fringe[(e_CursorX + x), (e_CursorY + y)].tileH = 32;
                                        e_Map.Fringe[(e_CursorX + x), (e_CursorY + y)].Tileset = e_SelectTileset;
                                    }
                                }
                            }
                            else
                            {
                                e_Map.Fringe[(e_CursorX), (e_CursorY)].tileX = (e_TileX * 32);
                                e_Map.Fringe[(e_CursorX), (e_CursorY)].tileY = (e_TileY * 32);
                                e_Map.Fringe[(e_CursorX), (e_CursorY)].tileW = 32;
                                e_Map.Fringe[(e_CursorX), (e_CursorY)].tileH = 32;
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
                                        e_Map.FringeA[(e_CursorX + x), (e_CursorY + y)].tileX = (e_TileX + x) * 32;
                                        e_Map.FringeA[(e_CursorX + x), (e_CursorY + y)].tileY = (e_TileY + y) * 32;
                                        e_Map.FringeA[(e_CursorX + x), (e_CursorY + y)].tileW = 32;
                                        e_Map.FringeA[(e_CursorX + x), (e_CursorY + y)].tileH = 32;
                                        e_Map.FringeA[(e_CursorX + x), (e_CursorY + y)].Tileset = e_SelectTileset;
                                    }
                                }
                            }
                            else
                            {
                                e_Map.FringeA[(e_CursorX), (e_CursorY)].tileX = (e_TileX * 32);
                                e_Map.FringeA[(e_CursorX), (e_CursorY)].tileY = (e_TileY * 32);
                                e_Map.FringeA[(e_CursorX), (e_CursorY)].tileW = 32;
                                e_Map.FringeA[(e_CursorX), (e_CursorY)].tileH = 32;
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
                            e_Map.Ground[(e_CursorX), (e_CursorY)].tileX = 0;
                            e_Map.Ground[(e_CursorX), (e_CursorY)].tileY = 0;
                            e_Map.Ground[(e_CursorX), (e_CursorY)].tileW = 0;
                            e_Map.Ground[(e_CursorX), (e_CursorY)].tileH = 0;
                            e_Map.Ground[(e_CursorX), (e_CursorY)].Tileset = 0;
                            break;
                        case (int)TileLayers.Mask:
                            e_Map.Mask[(e_CursorX), (e_CursorY)].tileX = 0;
                            e_Map.Mask[(e_CursorX), (e_CursorY)].tileY = 0;
                            e_Map.Mask[(e_CursorX), (e_CursorY)].tileW = 0;
                            e_Map.Mask[(e_CursorX), (e_CursorY)].tileH = 0;
                            e_Map.Mask[(e_CursorX), (e_CursorY)].Tileset = 0;
                            break;
                        case (int)TileLayers.MaskA:
                            e_Map.MaskA[(e_CursorX), (e_CursorY)].tileX = 0;
                            e_Map.MaskA[(e_CursorX), (e_CursorY)].tileY = 0;
                            e_Map.MaskA[(e_CursorX), (e_CursorY)].tileW = 0;
                            e_Map.MaskA[(e_CursorX), (e_CursorY)].tileH = 0;
                            e_Map.MaskA[(e_CursorX), (e_CursorY)].Tileset = 0;
                            break;
                        case (int)TileLayers.Fringe:
                            e_Map.Fringe[(e_CursorX), (e_CursorY)].tileX = 0;
                            e_Map.Fringe[(e_CursorX), (e_CursorY)].tileY = 0;
                            e_Map.Fringe[(e_CursorX), (e_CursorY)].tileW = 0;
                            e_Map.Fringe[(e_CursorX), (e_CursorY)].tileH = 0;
                            e_Map.Fringe[(e_CursorX), (e_CursorY)].Tileset = 0;
                            break;
                        case (int)TileLayers.FringeA:
                            e_Map.FringeA[(e_CursorX), (e_CursorY)].tileX = 0;
                            e_Map.FringeA[(e_CursorX), (e_CursorY)].tileY = 0;
                            e_Map.FringeA[(e_CursorX), (e_CursorY)].tileW = 0;
                            e_Map.FringeA[(e_CursorX), (e_CursorY)].tileH = 0;
                            e_Map.FringeA[(e_CursorX), (e_CursorY)].Tileset = 0;
                            break;
                    }
                }
            }
            else if (tabTools.SelectedTab == tabTypes)
            {
                if (e.Button == MouseButtons.Left)
                {
                    e_Map.Ground[e_CursorX, e_CursorY].type = e_Type;
                    if (e_Type == (int)TileType.NpcSpawn) { e_Map.Ground[e_CursorX, e_CursorY].SpawnNum = e_SpawnNumber; }
                    if (e_Type == (int)TileType.SpawnPool) { e_Map.Ground[e_CursorX, e_CursorY].SpawnNum = e_SpawnNumber; e_Map.Ground[e_CursorX, e_CursorY].SpawnAmount = e_SpawnAmount; }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    e_Map.Ground[e_CursorX, e_CursorY].type = (int)TileType.None;
                }
            }
            lblButtonDown.Text = "Button Down: " + e.Button.ToString();
        }

        private void EditorMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
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
                                        e_Map.Ground[(e_CursorX + x), (e_CursorY + y)].tileX = (e_TileX + x) * 32;
                                        e_Map.Ground[(e_CursorX + x), (e_CursorY + y)].tileY = (e_TileY + y) * 32;
                                        e_Map.Ground[(e_CursorX + x), (e_CursorY + y)].tileW = 32;
                                        e_Map.Ground[(e_CursorX + x), (e_CursorY + y)].tileH = 32;
                                        e_Map.Ground[(e_CursorX + x), (e_CursorY + y)].Tileset = e_SelectTileset;
                                    }
                                }
                            }
                            else
                            {
                                e_Map.Ground[(e_CursorX), (e_CursorY)].tileX = (e_TileX * 32);
                                e_Map.Ground[(e_CursorX), (e_CursorY)].tileY = (e_TileY * 32);
                                e_Map.Ground[(e_CursorX), (e_CursorY)].tileW = 32;
                                e_Map.Ground[(e_CursorX), (e_CursorY)].tileH = 32;
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
                                        e_Map.Mask[(e_CursorX + x), (e_CursorY + y)].tileX = (e_TileX + x) * 32;
                                        e_Map.Mask[(e_CursorX + x), (e_CursorY + y)].tileY = (e_TileY + y) * 32;
                                        e_Map.Mask[(e_CursorX + x), (e_CursorY + y)].tileW = 32;
                                        e_Map.Mask[(e_CursorX + x), (e_CursorY + y)].tileH = 32;
                                        e_Map.Mask[(e_CursorX + x), (e_CursorY + y)].Tileset = e_SelectTileset;
                                    }
                                }
                            }
                            else
                            {
                                e_Map.Mask[(e_CursorX), (e_CursorY)].tileX = (e_TileX * 32);
                                e_Map.Mask[(e_CursorX), (e_CursorY)].tileY = (e_TileY * 32);
                                e_Map.Mask[(e_CursorX), (e_CursorY)].tileW = 32;
                                e_Map.Mask[(e_CursorX), (e_CursorY)].tileH = 32;
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
                                        e_Map.MaskA[(e_CursorX + x), (e_CursorY + y)].tileX = (e_TileX + x) * 32;
                                        e_Map.MaskA[(e_CursorX + x), (e_CursorY + y)].tileY = (e_TileY + y) * 32;
                                        e_Map.MaskA[(e_CursorX + x), (e_CursorY + y)].tileW = 32;
                                        e_Map.MaskA[(e_CursorX + x), (e_CursorY + y)].tileH = 32;
                                        e_Map.MaskA[(e_CursorX + x), (e_CursorY + y)].Tileset = e_SelectTileset;
                                    }
                                }
                            }
                            else
                            {
                                e_Map.MaskA[(e_CursorX), (e_CursorY)].tileX = (e_TileX * 32);
                                e_Map.MaskA[(e_CursorX), (e_CursorY)].tileY = (e_TileY * 32);
                                e_Map.MaskA[(e_CursorX), (e_CursorY)].tileW = 32;
                                e_Map.MaskA[(e_CursorX), (e_CursorY)].tileH = 32;
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
                                        e_Map.Fringe[(e_CursorX + x), (e_CursorY + y)].tileX = (e_TileX + x) * 32;
                                        e_Map.Fringe[(e_CursorX + x), (e_CursorY + y)].tileY = (e_TileY + y) * 32;
                                        e_Map.Fringe[(e_CursorX + x), (e_CursorY + y)].tileW = 32;
                                        e_Map.Fringe[(e_CursorX + x), (e_CursorY + y)].tileH = 32;
                                        e_Map.Fringe[(e_CursorX + x), (e_CursorY + y)].Tileset = e_SelectTileset;
                                    }
                                }
                            }
                            else
                            {
                                e_Map.Fringe[(e_CursorX), (e_CursorY)].tileX = (e_TileX * 32);
                                e_Map.Fringe[(e_CursorX), (e_CursorY)].tileY = (e_TileY * 32);
                                e_Map.Fringe[(e_CursorX), (e_CursorY)].tileW = 32;
                                e_Map.Fringe[(e_CursorX), (e_CursorY)].tileH = 32;
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
                                        e_Map.FringeA[(e_CursorX + x), (e_CursorY + y)].tileX = (e_TileX + x) * 32;
                                        e_Map.FringeA[(e_CursorX + x), (e_CursorY + y)].tileY = (e_TileY + y) * 32;
                                        e_Map.FringeA[(e_CursorX + x), (e_CursorY + y)].tileW = 32;
                                        e_Map.FringeA[(e_CursorX + x), (e_CursorY + y)].tileH = 32;
                                        e_Map.FringeA[(e_CursorX + x), (e_CursorY + y)].Tileset = e_SelectTileset;
                                    }
                                }
                            }
                            else
                            {
                                e_Map.FringeA[(e_CursorX), (e_CursorY)].tileX = (e_TileX * 32);
                                e_Map.FringeA[(e_CursorX), (e_CursorY)].tileY = (e_TileY * 32);
                                e_Map.FringeA[(e_CursorX), (e_CursorY)].tileW = 32;
                                e_Map.FringeA[(e_CursorX), (e_CursorY)].tileH = 32;
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
                            e_Map.Ground[(e_CursorX), (e_CursorY)].tileX = 0;
                            e_Map.Ground[(e_CursorX), (e_CursorY)].tileY = 0;
                            e_Map.Ground[(e_CursorX), (e_CursorY)].tileW = 0;
                            e_Map.Ground[(e_CursorX), (e_CursorY)].tileH = 0;
                            e_Map.Ground[(e_CursorX), (e_CursorY)].Tileset = 0;
                            break;
                        case (int)TileLayers.Mask:
                            e_Map.Mask[(e_CursorX), (e_CursorY)].tileX = 0;
                            e_Map.Mask[(e_CursorX), (e_CursorY)].tileY = 0;
                            e_Map.Mask[(e_CursorX), (e_CursorY)].tileW = 0;
                            e_Map.Mask[(e_CursorX), (e_CursorY)].tileH = 0;
                            e_Map.Mask[(e_CursorX), (e_CursorY)].Tileset = 0;
                            break;
                        case (int)TileLayers.MaskA:
                            e_Map.MaskA[(e_CursorX), (e_CursorY)].tileX = 0;
                            e_Map.MaskA[(e_CursorX), (e_CursorY)].tileY = 0;
                            e_Map.MaskA[(e_CursorX), (e_CursorY)].tileW = 0;
                            e_Map.MaskA[(e_CursorX), (e_CursorY)].tileH = 0;
                            e_Map.MaskA[(e_CursorX), (e_CursorY)].Tileset = 0;
                            break;
                        case (int)TileLayers.Fringe:
                            e_Map.Fringe[(e_CursorX), (e_CursorY)].tileX = 0;
                            e_Map.Fringe[(e_CursorX), (e_CursorY)].tileY = 0;
                            e_Map.Fringe[(e_CursorX), (e_CursorY)].tileW = 0;
                            e_Map.Fringe[(e_CursorX), (e_CursorY)].tileH = 0;
                            e_Map.Fringe[(e_CursorX), (e_CursorY)].Tileset = 0;
                            break;
                        case (int)TileLayers.FringeA:
                            e_Map.FringeA[(e_CursorX), (e_CursorY)].tileX = 0;
                            e_Map.FringeA[(e_CursorX), (e_CursorY)].tileY = 0;
                            e_Map.FringeA[(e_CursorX), (e_CursorY)].tileW = 0;
                            e_Map.FringeA[(e_CursorX), (e_CursorY)].tileH = 0;
                            e_Map.FringeA[(e_CursorX), (e_CursorY)].Tileset = 0;
                            break;
                    }
                }
            }
            else if (tabTools.SelectedTab == tabTypes)
            {
                if (e.Button == MouseButtons.Left)
                {
                    e_Map.Ground[e_CursorX, e_CursorY].type = e_Type;
                    e_Map.Ground[e_CursorX, e_CursorY].type = e_Type;
                    if (e_Type == (int)TileType.NpcSpawn) { e_Map.Ground[e_CursorX, e_CursorY].SpawnNum = e_SpawnNumber; }
                    if (e_Type == (int)TileType.SpawnPool) { e_Map.Ground[e_CursorX, e_CursorY].SpawnNum = e_SpawnNumber; e_Map.Ground[e_CursorX, e_CursorY].SpawnAmount = e_SpawnAmount; }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    e_Map.Ground[e_CursorX, e_CursorY].type = (int)TileType.None;
                }
            }
            lblButtonDown.Text = "Button Down: " + e.Button.ToString();
            lblMouseLoc.Text = "Mouse X: " + e_CursorX + ", Mouse Y: " + e_CursorY;
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
        }

        private void radBlocked_CheckedChanged(object sender, EventArgs e)
        {
            e_Type = (int)TileType.Blocked;
            lblType.Text = "Type: Blocked";
            pnlNpcSpawn.Visible = false;
        }

        private void mnuOpen_Click(object sender, EventArgs e)
        {
            e_Map.LoadMap();
        }

        private void mnuSave_Click(object sender, EventArgs e)
        {
            e_Map.SaveMap();
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
        }

        private void radNpcAvoid_CheckedChanged(object sender, EventArgs e)
        {
            e_Type = (int)TileType.NpcAvoid;
            lblType.Text = "Type: Npc Avoid";
            pnlNpcSpawn.Visible = false;
        }

        private void radSpawnPool_CheckedChanged(object sender, EventArgs e)
        {
            e_Type = (int)TileType.SpawnPool;
            lblType.Text = "Type: Spawn Pool";
            e_SpawnNumber = 1;
            e_SpawnAmount = 1;
            pnlNpcSpawn.Visible = true;
            scrlSpawnAmount.Enabled = true;
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

        private void mnuFillLayer_Click(object sender, EventArgs e)
        {
            switch (e_Layer)
            {
                case (int)TileLayers.Ground:
                    for (int x = 0; x < 50; x++)
                    {
                        for (int y = 0; y < 50; y++)
                        {
                            e_Map.Ground[x, y].tileX = (e_TileX * 32);
                            e_Map.Ground[x, y].tileY = (e_TileY * 32);
                            e_Map.Ground[x, y].tileW = 32;
                            e_Map.Ground[x, y].tileH = 32;
                            e_Map.Ground[x, y].Tileset = e_SelectTileset;
                        }
                    }
                    break;
                case (int)TileLayers.Mask:
                    for (int x = 0; x < 50; x++)
                    {
                        for (int y = 0; y < 50; y++)
                        {
                            e_Map.Mask[x, y].tileX = (e_TileX * 32);
                            e_Map.Mask[x, y].tileY = (e_TileY * 32);
                            e_Map.Mask[x, y].tileW = 32;
                            e_Map.Mask[x, y].tileH = 32;
                            e_Map.Mask[x, y].Tileset = e_SelectTileset;
                        }
                    }
                    break;
                case (int)TileLayers.MaskA:
                    for (int x = 0; x < 50; x++)
                    {
                        for (int y = 0; y < 50; y++)
                        {
                            e_Map.MaskA[x, y].tileX = (e_TileX * 32);
                            e_Map.MaskA[x, y].tileY = (e_TileY * 32);
                            e_Map.MaskA[x, y].tileW = 32;
                            e_Map.MaskA[x, y].tileH = 32;
                            e_Map.MaskA[x, y].Tileset = e_SelectTileset;
                        }
                    }
                    break;
                case (int)TileLayers.Fringe:
                    for (int x = 0; x < 50; x++)
                    {
                        for (int y = 0; y < 50; y++)
                        {
                            e_Map.Fringe[x, y].tileX = (e_TileX * 32);
                            e_Map.Fringe[x, y].tileY = (e_TileY * 32);
                            e_Map.Fringe[x, y].tileW = 32;
                            e_Map.Fringe[x, y].tileH = 32;
                            e_Map.Fringe[x, y].Tileset = e_SelectTileset;
                        }
                    }
                    break;
                case (int)TileLayers.FringeA:
                    for (int x = 0; x < 50; x++)
                    {
                        for (int y = 0; y < 50; y++)
                        {
                            e_Map.FringeA[x, y].tileX = (e_TileX * 32);
                            e_Map.FringeA[x, y].tileY = (e_TileY * 32);
                            e_Map.FringeA[x, y].tileW = 32;
                            e_Map.FringeA[x, y].tileH = 32;
                            e_Map.FringeA[x, y].Tileset = e_SelectTileset;
                        }
                    }
                    break;
            }
        }

        private void btnCloseNpcs_Click(object sender, EventArgs e)
        {
            pnlMapNpcs.Visible = false;
        }

        private void mapNpcsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlMapNpcs.Visible = true;
        }

        private void cmbNpc1_SelectedIndexChanged(object sender, EventArgs e)
        {
            e_Map.mapNpc[0].npcNum = cmbNpc1.SelectedIndex;
        }

        private void cmbNpc2_SelectedIndexChanged(object sender, EventArgs e)
        {
            e_Map.mapNpc[1].npcNum = cmbNpc2.SelectedIndex;
        }

        private void cmbNpc3_SelectedIndexChanged(object sender, EventArgs e)
        {
            e_Map.mapNpc[2].npcNum = cmbNpc3.SelectedIndex;
        }

        private void cmbNpc4_SelectedIndexChanged(object sender, EventArgs e)
        {
            e_Map.mapNpc[3].npcNum = cmbNpc4.SelectedIndex;
        }

        private void cmbNpc5_SelectedIndexChanged(object sender, EventArgs e)
        {
            e_Map.mapNpc[4].npcNum = cmbNpc5.SelectedIndex;
        }

        private void cmbNpc6_SelectedIndexChanged(object sender, EventArgs e)
        {
            e_Map.mapNpc[5].npcNum = cmbNpc6.SelectedIndex;
        }

        private void cmbNpc7_SelectedIndexChanged(object sender, EventArgs e)
        {
            e_Map.mapNpc[6].npcNum = cmbNpc7.SelectedIndex;
        }

        private void cmbNpc8_SelectedIndexChanged(object sender, EventArgs e)
        {
            e_Map.mapNpc[7].npcNum = cmbNpc8.SelectedIndex;
        }

        private void cmbNpc9_SelectedIndexChanged(object sender, EventArgs e)
        {
            e_Map.mapNpc[8].npcNum = cmbNpc9.SelectedIndex;
        }

        private void cmbNpc10_SelectedIndexChanged(object sender, EventArgs e)
        {
            e_Map.mapNpc[9].npcNum = cmbNpc10.SelectedIndex;
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

        private void mnuDebug_Click(object sender, EventArgs e)
        {
            if (mnuDebug.Checked)
            {
                pnlDebug.Visible = true;
            }
            else
            {
                pnlDebug.Visible = false;
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            e_Map.Name = txtName.Text;
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
