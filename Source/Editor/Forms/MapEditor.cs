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

namespace Editor.Forms
{
    public partial class MapEditor : Form
    {
        RenderWindow e_Window;
        RenderText e_Text = new RenderText();
        Map e_Map = new Map();
        SFML.Graphics.View e_View = new SFML.Graphics.View();
        public int ViewX { get; set; }
        public int ViewY { get; set; }
        public int CurX;
        public int CurY;
        static int lastFrameRate;
        static int frameRate;
        static int lastTick;

        public MapEditor()
        {
            InitializeComponent();
            Visible = true;
            KeyPress += new KeyPressEventHandler(EditorKeyPress);
            e_Map.LoadMap();
            MapEditorLoop();
        }

        void MapEditorLoop()
        {
            e_Window = new RenderWindow(picMap.Handle);
            e_Window.SetFramerateLimit(25);

            while (Visible)
            {
                e_View.Reset(new FloatRect(0, 0, 800, 600));
                e_View.Move(new Vector2f(ViewX * 32, ViewY * 32));
                e_Window.SetView(e_View);
                DoEvents();
                e_Window.DispatchEvents();
                e_Window.Clear();
                DrawTiles();
                Text = "Map Editor - FPS: " + CalculateFrameRate();                
                e_Window.Display();
            }
            Visible = false;
        }

        private void EditorKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == 'w')
            {
                if (ViewY > 0)
                {
                    ViewY -= 1;
                }
            }
            if (e.KeyChar == 's')
            {
                if (ViewY < 50)
                {
                    ViewY += 1;
                }
            }
            if (e.KeyChar == 'a')
            {
                if (ViewX > 0)
                {
                    ViewX -= 1;
                }
            }

            if (e.KeyChar == 'd')
            {
                if (ViewX < 50)
                {
                    ViewX += 1;
                }
            }
        }

        void DrawTiles()
        {
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
        Font e_Font = new Font("Resources/Fonts/Arial.ttf");
        Text e_Text = new Text();

        public void DrawText(RenderWindow e_Window, string eText, Vector2f position, uint e_Size, Color e_Color)
        {
            e_Text.Font = e_Font; //set the font
            e_Text.CharacterSize = e_Size;  //set it size
            e_Text.Position = position;    //set its location on the screen
            e_Text.DisplayedString = eText;    //what is actually being displayed (text)
            e_Text.Color = e_Color; //the color of the text we are drawing

            e_Window.Draw(e_Text);  //window drawing function
        }
    }
}
