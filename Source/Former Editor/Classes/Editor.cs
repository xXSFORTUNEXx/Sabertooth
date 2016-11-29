using Microsoft.Win32.SafeHandles;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Runtime.InteropServices;
using static Microsoft.VisualBasic.Interaction;
using Microsoft.VisualBasic;
using System.Threading;
using static System.Environment;
using Gwen.Control;
using System.Data.SQLite;

namespace Editor.Classes
{
    class Editor
    {
        SQLiteConnection s_Database;
        //Editor
        public RenderWindow e_Window;
        static GUI e_GUI;
        static Canvas e_Canvas;
        static Gwen.Input.SFML e_Input;
        View e_View = new View();
        //Map editor 
        public Map e_Map = new Map();
        Texture[] e_Tileset = new Texture[67];
        Sprite e_Tiles = new Sprite();
        Sprite e_Tile = new Sprite();
        Sprite e_Grid = new Sprite();
        RenderText e_Text = new RenderText();
        RectangleShape e_selectTile = new RectangleShape(new Vector2f(32, 32));
        //Npc editor
        public NPC[] e_Npc = new NPC[10];
        public bool inNpcEditor;
        //Editor
        public int viewX { get; set; }
        public int viewY { get; set; }
        public int viewOffsetX = 38;
        public int viewOffsetY = 19;
        public int storedViewX;
        public int storedViewY;
        public int CurX;
        public int CurY;
        public string isInput;
        //Map Editor
        public int editorTileX { get; set; }
        public int editorTileY { get; set; }
        public int editorTileW { get; set; }
        public int editorTileH { get; set; }
        public int editorTileset { get; set; }
        public int editorBrushSize { get; set; }
        public bool utiWindow;
        public bool utiLayers;
        public int selectedLayer;
        public string[] layerNames = { "Ground", "Mask", "Fringe", "MaskA", "FringA" };
        public int selectedType;
        public string[] typeNames = { "None", "Blocked", "NPC Spawn" };
        public int editMode;
        public string[] editNames = { "Layer", "Type", "Tilesets" };
        public bool gridActive;
        public int selectedNpc;
        private static int lastTick;
        private static int lastFrameRate;
        private static int frameRate;
        //Graphic constants
        public const int picX = 32;
        public const int picY = 32;

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

            for (int i = 0; i < 67; i++)
            {
                e_Tileset[i].Dispose();
            }
            e_Tileset = null;

            e_Map.Dispose();
            e_Map = null;
            e_View.Dispose();
            e_View = null;
            e_Tiles.Dispose();
            e_Tiles = null;
            e_Tile.Dispose();
            e_Tile = null;
            e_Grid.Dispose();
            e_Grid = null;
            e_Text.Dispose();
            e_Text = null;
            e_selectTile.Dispose();
            e_selectTile = null;

            disposed = true;
        }

        public Editor()
        {
            Console.WriteLine("Loading tilesets...");
            for (int i = 0; i < 67; i++)
            {
                e_Tileset[i] = new Texture("Resources/Tilesets/" + (i + 1) + ".png");
            }
            selectedLayer = (int)TileLayers.Ground;
            selectedType = (int)TileType.None;
            editorTileset = 0;
            editorTileH = 1;
            editorTileW = 1;
            utiWindow = false;
            utiLayers = false;
            inNpcEditor = false;
            isInput = "None";
        }

        public void EditorLoop()
        {
            e_Window = new RenderWindow(new VideoMode(1216, 608), "Editor Suite");
            e_Window.Closed += new EventHandler(onClose);
            e_Window.MouseMoved += new EventHandler<MouseMoveEventArgs>(onMouseMove);
            e_Window.KeyPressed += new EventHandler<KeyEventArgs>(onKeyPress);
            e_Window.KeyReleased += new EventHandler<KeyEventArgs>(onKeyReleased);
            e_Window.TextEntered += new EventHandler<TextEventArgs>(onTextEntered);
            e_Window.MouseButtonPressed += new EventHandler<MouseButtonEventArgs>(onMouseButton);
            e_Window.MouseButtonReleased += new EventHandler<MouseButtonEventArgs>(onMouseButtonRelease);
            e_Window.MouseWheelScrolled += new EventHandler<MouseWheelScrollEventArgs>(onMouseScroll);

            PopulateDatabase();

            //setup gwen
            Gwen.Renderer.SFML gwenRenderer = new Gwen.Renderer.SFML(e_Window);
            Gwen.Skin.TexturedBase skin = new Gwen.Skin.TexturedBase(gwenRenderer, "Resources/Skins/DefaultSkin.png");
            Gwen.Font defaultFont = new Gwen.Font(gwenRenderer, "Resources/Fonts/Tahoma.ttf");
            gwenRenderer.LoadFont(defaultFont);
            skin.SetDefaultFont(defaultFont.FaceName);
            defaultFont.Dispose();
            e_Canvas = new Canvas(skin);
            e_Canvas.SetSize(1216, 608);
            e_Canvas.ShouldDrawBackground = true;
            e_Canvas.BackgroundColor = System.Drawing.Color.Transparent;
            e_Canvas.KeyboardInputEnabled = true;
            e_Input = new Gwen.Input.SFML();   //attach gwen and sfml input classes
            e_Input.Initialize(e_Canvas, e_Window);  //initalize the input both with the canvas and the window
            e_GUI = new GUI(e_Canvas, defaultFont, gwenRenderer, e_Map);

            while (e_Window.IsOpen == true)
            {
                e_Window.DispatchEvents();

                e_View.Reset(new FloatRect(0, 0, 1216, 608));
                e_View.Move(new Vector2f(viewX * picX, viewY * picY));
                e_Window.SetView(e_View);

                e_Window.Clear();

                if (utiWindow == true)
                {
                    DrawTileSelectionScreen();
                }
                else if (inNpcEditor == true)
                {
                    DrawNpcEditorScreen();
                }
                else
                {
                    DrawMapEditingScreen();
                }
                e_Window.Display();
            }
            e_Canvas.Dispose();
            Thread.Sleep(650);
            Exit(0);
        }

        static void onClose(object sender, EventArgs e)
        {
            RenderWindow editorWindow = (RenderWindow)sender;
            editorWindow.Close();
        }

        public void onKeyPress(object sender, KeyEventArgs e)
        {
            RenderWindow mainWindow = (RenderWindow)sender;
            e_Input.ProcessMessage(new Gwen.Input.SFMLKeyEventArgs(e, true));
            CheckDevelopmentKeys(mainWindow, e);
            CheckMovementKeys(e);
        }

        public void onMouseMove(object sender, MouseMoveEventArgs e)
        {
            e_Input.ProcessMessage(e);

            RenderWindow editorWindow = (RenderWindow)sender;
            CurX = e.X;
            CurY = e.Y;
            int clickX = (e.X / 32);
            int clickY = (e.Y / 32);

            if (editorTileH > 1 || editorTileW > 1)
            {
                return;
            }

            if (utiWindow == false)
            {
                try
                {
                    if (Mouse.IsButtonPressed(Mouse.Button.Left))
                    {
                        if (editMode == (int)EditorModes.Layer)
                        {
                            switch (selectedLayer)
                            {
                                case (int)TileLayers.Ground:
                                    if (editorBrushSize > 1)
                                    {
                                        for (int x = 0; x < editorBrushSize; x++)
                                        {
                                            for (int y = 0; y < editorBrushSize; y++)
                                            {
                                                e_Map.Ground[clickX + x, clickY + y].tileX = editorTileX * picX;
                                                e_Map.Ground[clickX + x, clickY + y].tileY = editorTileY * picY;
                                                e_Map.Ground[clickX + x, clickY + y].tileH = picX;
                                                e_Map.Ground[clickX + x, clickY + y].tileW = picY;
                                                e_Map.Ground[clickX + x, clickY + y].Tileset = editorTileset;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        e_Map.Ground[clickX, clickY].tileX = editorTileX * picX;
                                        e_Map.Ground[clickX, clickY].tileY = editorTileY * picY;
                                        e_Map.Ground[clickX, clickY].tileH = picX;
                                        e_Map.Ground[clickX, clickY].tileW = picY;
                                        e_Map.Ground[clickX, clickY].Tileset = editorTileset;
                                    }
                                    break;
                                case (int)TileLayers.Mask:
                                    if (editorBrushSize > 1)
                                    {
                                        for (int x = 0; x < editorBrushSize; x++)
                                        {
                                            for (int y = 0; y < editorBrushSize; y++)
                                            {
                                                e_Map.Mask[clickX + x, clickY + y].tileX = editorTileX * picX;
                                                e_Map.Mask[clickX + x, clickY + y].tileY = editorTileY * picY;
                                                e_Map.Mask[clickX + x, clickY + y].tileH = picX;
                                                e_Map.Mask[clickX + x, clickY + y].tileW = picY;
                                                e_Map.Mask[clickX + x, clickY + y].Tileset = editorTileset;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        e_Map.Mask[clickX, clickY].tileX = editorTileX * picX;
                                        e_Map.Mask[clickX, clickY].tileY = editorTileY * picY;
                                        e_Map.Mask[clickX, clickY].tileH = picX;
                                        e_Map.Mask[clickX, clickY].tileW = picY;
                                        e_Map.Mask[clickX, clickY].Tileset = editorTileset;
                                    }
                                    break;
                                case (int)TileLayers.Fringe:
                                    if (editorBrushSize > 1)
                                    {
                                        for (int x = 0; x < editorBrushSize; x++)
                                        {
                                            for (int y = 0; y < editorBrushSize; y++)
                                            {
                                                e_Map.Fringe[clickX + x, clickY + y].tileX = editorTileX * picX;
                                                e_Map.Fringe[clickX + x, clickY + y].tileY = editorTileY * picY;
                                                e_Map.Fringe[clickX + x, clickY + y].tileH = picX;
                                                e_Map.Fringe[clickX + x, clickY + y].tileW = picY;
                                                e_Map.Fringe[clickX + x, clickY + y].Tileset = editorTileset;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        e_Map.Fringe[clickX, clickY].tileX = editorTileX * picX;
                                        e_Map.Fringe[clickX, clickY].tileY = editorTileY * picY;
                                        e_Map.Fringe[clickX, clickY].tileH = picX;
                                        e_Map.Fringe[clickX, clickY].tileW = picY;
                                        e_Map.Fringe[clickX, clickY].Tileset = editorTileset;
                                    }
                                    break;
                                case (int)TileLayers.MaskA:
                                    if (editorBrushSize > 1)
                                    {
                                        for (int x = 0; x < editorBrushSize; x++)
                                        {
                                            for (int y = 0; y < editorBrushSize; y++)
                                            {
                                                e_Map.MaskA[clickX + x, clickY + y].tileX = editorTileX * picX;
                                                e_Map.MaskA[clickX + x, clickY + y].tileY = editorTileY * picY;
                                                e_Map.MaskA[clickX + x, clickY + y].tileH = picX;
                                                e_Map.MaskA[clickX + x, clickY + y].tileW = picY;
                                                e_Map.MaskA[clickX + x, clickY + y].Tileset = editorTileset;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        e_Map.MaskA[clickX, clickY].tileX = editorTileX * picX;
                                        e_Map.MaskA[clickX, clickY].tileY = editorTileY * picY;
                                        e_Map.MaskA[clickX, clickY].tileH = picX;
                                        e_Map.MaskA[clickX, clickY].tileW = picY;
                                        e_Map.MaskA[clickX, clickY].Tileset = editorTileset;
                                    }
                                    break;
                                case (int)TileLayers.FringeA:
                                    if (editorBrushSize > 1)
                                    {
                                        for (int x = 0; x < editorBrushSize; x++)
                                        {
                                            for (int y = 0; y < editorBrushSize; y++)
                                            {
                                                e_Map.FringeA[clickX + x, clickY + y].tileX = editorTileX * picX;
                                                e_Map.FringeA[clickX + x, clickY + y].tileY = editorTileY * picY;
                                                e_Map.FringeA[clickX + x, clickY + y].tileH = picX;
                                                e_Map.FringeA[clickX + x, clickY + y].tileW = picY;
                                                e_Map.FringeA[clickX + x, clickY + y].Tileset = editorTileset;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        e_Map.FringeA[clickX, clickY].tileX = editorTileX * picX;
                                        e_Map.FringeA[clickX, clickY].tileY = editorTileY * picY;
                                        e_Map.FringeA[clickX, clickY].tileH = picX;
                                        e_Map.FringeA[clickX, clickY].tileW = picY;
                                        e_Map.FringeA[clickX, clickY].Tileset = editorTileset;
                                    }
                                    break;
                            }
                        }
                        else if (editMode == (int)EditorModes.Type)
                        {
                            e_Map.Ground[clickX, clickY].type = selectedType;
                            e_Map.Ground[clickX, clickY].spawnNum = selectedNpc;
                        }
                    }
                    else if (Mouse.IsButtonPressed(Mouse.Button.Right))
                    {
                        if (editMode == (int)EditorModes.Layer)
                        {
                            switch (selectedLayer)
                            {
                                case (int)TileLayers.Ground:
                                    e_Map.Ground[clickX, clickY].tileX = 0;
                                    e_Map.Ground[clickX, clickY].tileY = 0;
                                    e_Map.Ground[clickX, clickY].tileH = 0;
                                    e_Map.Ground[clickX, clickY].tileW = 0;
                                    e_Map.Ground[clickX, clickY].Tileset = 0;
                                    break;
                                case (int)TileLayers.Mask:
                                    e_Map.Mask[clickX, clickY].tileX = 0;
                                    e_Map.Mask[clickX, clickY].tileY = 0;
                                    e_Map.Mask[clickX, clickY].tileH = 0;
                                    e_Map.Mask[clickX, clickY].tileW = 0;
                                    e_Map.Mask[clickX, clickY].Tileset = 0;
                                    break;
                                case (int)TileLayers.Fringe:
                                    e_Map.Fringe[clickX, clickY].tileX = editorTileX * picX;
                                    e_Map.Fringe[clickX, clickY].tileY = editorTileY * picY;
                                    e_Map.Fringe[clickX, clickY].tileH = editorTileH;
                                    e_Map.Fringe[clickX, clickY].tileW = editorTileW;
                                    e_Map.Fringe[clickX, clickY].Tileset = 0;
                                    break;
                                case (int)TileLayers.MaskA:
                                    e_Map.MaskA[clickX, clickY].tileX = editorTileX * picX;
                                    e_Map.MaskA[clickX, clickY].tileY = editorTileY * picY;
                                    e_Map.MaskA[clickX, clickY].tileH = editorTileH;
                                    e_Map.MaskA[clickX, clickY].tileW = editorTileW;
                                    e_Map.MaskA[clickX, clickY].Tileset = 0;
                                    break;
                                case (int)TileLayers.FringeA:
                                    e_Map.FringeA[clickX, clickY].tileX = editorTileX * picX;
                                    e_Map.FringeA[clickX, clickY].tileY = editorTileY * picY;
                                    e_Map.FringeA[clickX, clickY].tileH = editorTileH;
                                    e_Map.FringeA[clickX, clickY].tileW = editorTileW;
                                    e_Map.FringeA[clickX, clickY].Tileset = 0;
                                    break;
                            }
                        }
                        else if (editMode == (int)EditorModes.Type)
                        {
                            e_Map.Ground[clickX, clickY].type = (int)TileType.None;
                            e_Map.Ground[clickX, clickY].spawnNum = 0;
                        }
                    }
                }
                catch (Exception)
                {
                    //MsgBox("Out of bounds!", MsgBoxStyle.OkOnly, "Error");
                }

            }
        }

        public void onMouseButton(object sender, MouseButtonEventArgs e)
        {
            RenderWindow editorWindow = (RenderWindow)sender;
            int clickX = (e.X / 32) + (viewX);
            int clickY = (e.Y / 32) + (viewY);

            if (utiWindow == true)
            {
                e_Input.ProcessMessage(new Gwen.Input.SFMLMouseButtonEventArgs(e, true));

                try
                {
                    if (e.Button == Mouse.Button.Left)
                    {
                        if (e.X < 384)
                        {
                            isInput = "Click: Left";

                            editorTileX = e.X / 32 + (viewX);
                            editorTileY = e.Y / 32 + (viewY);
                        }
                    }
                }
                catch (Exception)
                {
                    //MsgBox("Out of bounds!", MsgBoxStyle.OkOnly, "Error");
                }

            }
            else
            {
                try
                {
                    if (e.Button == Mouse.Button.Left)
                    {
                        isInput = "Click: Left";

                        if (editMode == (int)EditorModes.Layer)
                        {
                            switch (selectedLayer)
                            {
                                case (int)TileLayers.Ground:
                                    if (editorTileH > 1 || editorTileW > 1)
                                    {
                                        for (int x = 0; x < editorTileW; x++)
                                        {
                                            for (int y = 0; y < editorTileH; y++)
                                            {
                                                e_Map.Ground[clickX + x, clickY + y].tileX = (editorTileX + x) * picX;
                                                e_Map.Ground[clickX + x, clickY + y].tileY = (editorTileY + y) * picY;
                                                e_Map.Ground[clickX + x, clickY + y].tileH = picX;
                                                e_Map.Ground[clickX + x, clickY + y].tileW = picY;
                                                e_Map.Ground[clickX + x, clickY + y].Tileset = editorTileset;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (editorBrushSize > 1)
                                        {
                                            for (int x = 0; x < editorBrushSize; x++)
                                            {
                                                for (int y = 0; y < editorBrushSize; y++)
                                                {
                                                    e_Map.Ground[clickX + x, clickY + y].tileX = editorTileX * picX;
                                                    e_Map.Ground[clickX + x, clickY + y].tileY = editorTileY * picY;
                                                    e_Map.Ground[clickX + x, clickY + y].tileH = picX;
                                                    e_Map.Ground[clickX + x, clickY + y].tileW = picY;
                                                    e_Map.Ground[clickX + x, clickY + y].Tileset = editorTileset;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            e_Map.Ground[clickX, clickY].tileX = editorTileX * picX;
                                            e_Map.Ground[clickX, clickY].tileY = editorTileY * picY;
                                            e_Map.Ground[clickX, clickY].tileH = picX;
                                            e_Map.Ground[clickX, clickY].tileW = picY;
                                            e_Map.Ground[clickX, clickY].Tileset = editorTileset;
                                        }
                                    }
                                    break;
                                case (int)TileLayers.Mask:
                                    if (editorTileH > 1 || editorTileW > 1)
                                    {
                                        for (int x = 0; x < editorTileW; x++)
                                        {
                                            for (int y = 0; y < editorTileH; y++)
                                            {
                                                e_Map.Mask[clickX + x, clickY + y].tileX = (editorTileX + x) * picX;
                                                e_Map.Mask[clickX + x, clickY + y].tileY = (editorTileY + y) * picY;
                                                e_Map.Mask[clickX + x, clickY + y].tileH = picX;
                                                e_Map.Mask[clickX + x, clickY + y].tileW = picY;
                                                e_Map.Mask[clickX + x, clickY + y].Tileset = editorTileset;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (editorBrushSize > 1)
                                        {
                                            for (int x = 0; x < editorBrushSize; x++)
                                            {
                                                for (int y = 0; y < editorBrushSize; y++)
                                                {
                                                    e_Map.Mask[clickX + x, clickY + y].tileX = editorTileX * picX;
                                                    e_Map.Mask[clickX + x, clickY + y].tileY = editorTileY * picY;
                                                    e_Map.Mask[clickX + x, clickY + y].tileH = picX;
                                                    e_Map.Mask[clickX + x, clickY + y].tileW = picY;
                                                    e_Map.Mask[clickX + x, clickY + y].Tileset = editorTileset;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            e_Map.Mask[clickX, clickY].tileX = editorTileX * picX;
                                            e_Map.Mask[clickX, clickY].tileY = editorTileY * picY;
                                            e_Map.Mask[clickX, clickY].tileH = picX;
                                            e_Map.Mask[clickX, clickY].tileW = picY;
                                            e_Map.Mask[clickX, clickY].Tileset = editorTileset;
                                        }
                                    }
                                    break;
                                case (int)TileLayers.Fringe:
                                    if (editorTileH > 1 || editorTileW > 1)
                                    {
                                        for (int x = 0; x < editorTileW; x++)
                                        {
                                            for (int y = 0; y < editorTileH; y++)
                                            {
                                                e_Map.Fringe[clickX + x, clickY + y].tileX = (editorTileX + x) * picX;
                                                e_Map.Fringe[clickX + x, clickY + y].tileY = (editorTileY + y) * picY;
                                                e_Map.Fringe[clickX + x, clickY + y].tileH = picX;
                                                e_Map.Fringe[clickX + x, clickY + y].tileW = picY;
                                                e_Map.Fringe[clickX + x, clickY + y].Tileset = editorTileset;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (editorBrushSize > 1)
                                        {
                                            for (int x = 0; x < editorBrushSize; x++)
                                            {
                                                for (int y = 0; y < editorBrushSize; y++)
                                                {
                                                    e_Map.Fringe[clickX + x, clickY + y].tileX = editorTileX * picX;
                                                    e_Map.Fringe[clickX + x, clickY + y].tileY = editorTileY * picY;
                                                    e_Map.Fringe[clickX + x, clickY + y].tileH = picX;
                                                    e_Map.Fringe[clickX + x, clickY + y].tileW = picY;
                                                    e_Map.Fringe[clickX + x, clickY + y].Tileset = editorTileset;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            e_Map.Fringe[clickX, clickY].tileX = editorTileX * picX;
                                            e_Map.Fringe[clickX, clickY].tileY = editorTileY * picY;
                                            e_Map.Fringe[clickX, clickY].tileH = picX;
                                            e_Map.Fringe[clickX, clickY].tileW = picY;
                                            e_Map.Fringe[clickX, clickY].Tileset = editorTileset;
                                        }
                                    }
                                    break;
                                case (int)TileLayers.MaskA:
                                    if (editorTileH > 1 || editorTileW > 1)
                                    {
                                        for (int x = 0; x < editorTileW; x++)
                                        {
                                            for (int y = 0; y < editorTileH; y++)
                                            {
                                                e_Map.MaskA[clickX + x, clickY + y].tileX = (editorTileX + x) * picX;
                                                e_Map.MaskA[clickX + x, clickY + y].tileY = (editorTileY + y) * picY;
                                                e_Map.MaskA[clickX + x, clickY + y].tileH = picX;
                                                e_Map.MaskA[clickX + x, clickY + y].tileW = picY;
                                                e_Map.MaskA[clickX + x, clickY + y].Tileset = editorTileset;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (editorBrushSize > 1)
                                        {
                                            for (int x = 0; x < editorBrushSize; x++)
                                            {
                                                for (int y = 0; y < editorBrushSize; y++)
                                                {
                                                    e_Map.MaskA[clickX + x, clickY + y].tileX = editorTileX * picX;
                                                    e_Map.MaskA[clickX + x, clickY + y].tileY = editorTileY * picY;
                                                    e_Map.MaskA[clickX + x, clickY + y].tileH = picX;
                                                    e_Map.MaskA[clickX + x, clickY + y].tileW = picY;
                                                    e_Map.MaskA[clickX + x, clickY + y].Tileset = editorTileset;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            e_Map.MaskA[clickX, clickY].tileX = editorTileX * picX;
                                            e_Map.MaskA[clickX, clickY].tileY = editorTileY * picY;
                                            e_Map.MaskA[clickX, clickY].tileH = picX;
                                            e_Map.MaskA[clickX, clickY].tileW = picY;
                                            e_Map.MaskA[clickX, clickY].Tileset = editorTileset;
                                        }
                                    }
                                    break;
                                case (int)TileLayers.FringeA:
                                    if (editorTileH > 1 || editorTileW > 1)
                                    {
                                        for (int x = 0; x < editorTileW; x++)
                                        {
                                            for (int y = 0; y < editorTileH; y++)
                                            {
                                                e_Map.FringeA[clickX + x, clickY + y].tileX = (editorTileX + x) * picX;
                                                e_Map.FringeA[clickX + x, clickY + y].tileY = (editorTileY + y) * picY;
                                                e_Map.FringeA[clickX + x, clickY + y].tileH = picX;
                                                e_Map.FringeA[clickX + x, clickY + y].tileW = picY;
                                                e_Map.FringeA[clickX + x, clickY + y].Tileset = editorTileset;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (editorBrushSize > 1)
                                        {
                                            for (int x = 0; x < editorBrushSize; x++)
                                            {
                                                for (int y = 0; y < editorBrushSize; y++)
                                                {
                                                    e_Map.FringeA[clickX + x, clickY + y].tileX = editorTileX * picX;
                                                    e_Map.FringeA[clickX + x, clickY + y].tileY = editorTileY * picY;
                                                    e_Map.FringeA[clickX + x, clickY + y].tileH = picX;
                                                    e_Map.FringeA[clickX + x, clickY + y].tileW = picY;
                                                    e_Map.FringeA[clickX + x, clickY + y].Tileset = editorTileset;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            e_Map.FringeA[clickX, clickY].tileX = editorTileX * picX;
                                            e_Map.FringeA[clickX, clickY].tileY = editorTileY * picY;
                                            e_Map.FringeA[clickX, clickY].tileH = picX;
                                            e_Map.FringeA[clickX, clickY].tileW = picY;
                                            e_Map.FringeA[clickX, clickY].Tileset = editorTileset;
                                        }
                                    }
                                    break;
                            }
                        }
                        else if (editMode == (int)EditorModes.Type)
                        {
                            e_Map.Ground[clickX, clickY].type = selectedType;
                            e_Map.Ground[clickX, clickY].spawnNum = selectedNpc;
                        }
                    }
                    else if (e.Button == Mouse.Button.Right)
                    {
                        isInput = "Click: Right";

                        if (editMode == (int)EditorModes.Layer)
                        {
                            switch (selectedLayer)
                            {
                                case (int)TileLayers.Ground:
                                    e_Map.Ground[clickX, clickY].tileX = 0;
                                    e_Map.Ground[clickX, clickY].tileY = 0;
                                    e_Map.Ground[clickX, clickY].tileH = 0;
                                    e_Map.Ground[clickX, clickY].tileW = 0;
                                    e_Map.Ground[clickX, clickY].Tileset = 0;
                                    break;
                                case (int)TileLayers.Mask:
                                    e_Map.Mask[clickX, clickY].tileX = 0;
                                    e_Map.Mask[clickX, clickY].tileY = 0;
                                    e_Map.Mask[clickX, clickY].tileH = 0;
                                    e_Map.Mask[clickX, clickY].tileW = 0;
                                    e_Map.Mask[clickX, clickY].Tileset = 0;
                                    break;
                                case (int)TileLayers.Fringe:
                                    e_Map.Fringe[clickX, clickY].tileX = 0;
                                    e_Map.Fringe[clickX, clickY].tileY = 0;
                                    e_Map.Fringe[clickX, clickY].tileH = 0;
                                    e_Map.Fringe[clickX, clickY].tileW = 0;
                                    e_Map.Fringe[clickX, clickY].Tileset = 0;
                                    break;
                                case (int)TileLayers.MaskA:
                                    e_Map.MaskA[clickX, clickY].tileX = 0;
                                    e_Map.MaskA[clickX, clickY].tileY = 0;
                                    e_Map.MaskA[clickX, clickY].tileH = 0;
                                    e_Map.MaskA[clickX, clickY].tileW = 0;
                                    e_Map.MaskA[clickX, clickY].Tileset = 0;
                                    break;
                                case (int)TileLayers.FringeA:
                                    e_Map.FringeA[clickX, clickY].tileX = 0;
                                    e_Map.FringeA[clickX, clickY].tileY = 0;
                                    e_Map.FringeA[clickX, clickY].tileH = 0;
                                    e_Map.FringeA[clickX, clickY].tileW = 0;
                                    e_Map.FringeA[clickX, clickY].Tileset = 0;
                                    break;
                            }
                        }
                        else if (editMode == (int)EditorModes.Type)
                        {
                            e_Map.Ground[clickX, clickY].type = (int)TileType.None;
                            e_Map.Ground[clickX, clickY].spawnNum = 0;
                        }
                    }
                    else if (e.Button == Mouse.Button.Middle)
                    {
                        isInput = "Click: Middle";

                        e_Map.Ground[clickX, clickY].tileX = 0;
                        e_Map.Ground[clickX, clickY].tileY = 32;
                        e_Map.Ground[clickX, clickY].tileH = 32;
                        e_Map.Ground[clickX, clickY].tileW = 32;
                        e_Map.Ground[clickX, clickY].Tileset = 0;
                        e_Map.Ground[clickX, clickY].type = (int)TileType.None;
                        e_Map.Mask[clickX, clickY].tileX = 0;
                        e_Map.Mask[clickX, clickY].tileY = 0;
                        e_Map.Mask[clickX, clickY].tileH = 0;
                        e_Map.Mask[clickX, clickY].tileW = 0;
                        e_Map.Mask[clickX, clickY].Tileset = 0;
                        e_Map.Mask[clickX, clickY].type = (int)TileType.None;
                        e_Map.Fringe[clickX, clickY].tileX = 0;
                        e_Map.Fringe[clickX, clickY].tileY = 0;
                        e_Map.Fringe[clickX, clickY].tileH = 0;
                        e_Map.Fringe[clickX, clickY].tileW = 0;
                        e_Map.Fringe[clickX, clickY].Tileset = 0;
                        e_Map.Fringe[clickX, clickY].type = (int)TileType.None;
                        e_Map.MaskA[clickX, clickY].tileX = 0;
                        e_Map.MaskA[clickX, clickY].tileY = 0;
                        e_Map.MaskA[clickX, clickY].tileH = 0;
                        e_Map.MaskA[clickX, clickY].tileW = 0;
                        e_Map.MaskA[clickX, clickY].Tileset = 0;
                        e_Map.MaskA[clickX, clickY].type = (int)TileType.None;
                        e_Map.FringeA[clickX, clickY].tileX = 0;
                        e_Map.FringeA[clickX, clickY].tileY = 0;
                        e_Map.FringeA[clickX, clickY].tileH = 0;
                        e_Map.FringeA[clickX, clickY].tileW = 0;
                        e_Map.FringeA[clickX, clickY].Tileset = 0;
                        e_Map.FringeA[clickX, clickY].type = (int)TileType.None;
                    }
                }
                catch (Exception)
                {
                    //MsgBox("Out of bounds!", MsgBoxStyle.OkOnly, "Error");
                }
            }

            if (inNpcEditor == true)
            {
                e_Input.ProcessMessage(new Gwen.Input.SFMLMouseButtonEventArgs(e, true));
            }
        }

        public void onMouseButtonRelease(object sender, MouseButtonEventArgs e)
        {
            e_Input.ProcessMessage(new Gwen.Input.SFMLMouseButtonEventArgs(e, false));
        }

        public void onMouseScroll(object sender, MouseWheelScrollEventArgs e)
        {
            if (utiWindow == true)
            {
                if (e.Delta > 0)
                {
                    if (viewY > 0)
                    {
                        viewY -= 1;
                    }
                }
                else
                {
                    if (viewY < (50 - viewOffsetY))
                    {
                        viewY += 1;
                    }
                }
            }
        }

        public void onKeyReleased(object sender, KeyEventArgs e)
        {
            e_Input.ProcessMessage(new Gwen.Input.SFMLKeyEventArgs(e, false));
        }

        public void onTextEntered(object sender, TextEventArgs e)
        {
            e_Input.ProcessMessage(e);
        }

        static int CalculateFrameRate()
        {
            if (Environment.TickCount - lastTick >= 1000)
            {
                lastFrameRate = frameRate;
                frameRate = 0;
                lastTick = Environment.TickCount;
            }
            frameRate++;
            return lastFrameRate;
        }

        void PopulateDatabase()
        {
            s_Database = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;");
            s_Database.Open();
            string sql;

            sql = "SELECT COUNT(*) FROM `NPCS`";

            SQLiteCommand sql_Command = new SQLiteCommand(sql, s_Database);
            int result = int.Parse(sql_Command.ExecuteScalar().ToString());

            if (result == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    e_Npc[i] = new NPC("None", 10, 10, (int)Directions.Down, 0, 0, 0, (int)BehaviorType.Friendly, 5000, 100, 100, 10);
                    e_Npc[i].CreateNpcInDatabase();
                }
            }

        }

        void SetNewEditorValues()
        {
            editMode = e_GUI.editMode;
            selectedLayer = e_GUI.editLayer;
            editorTileset = e_GUI.editTileset;
            selectedType = e_GUI.editType;
            gridActive = e_GUI.editGrid;
            utiLayers = e_GUI.editshowTypes;
            editorBrushSize = e_GUI.brushSize;
            selectedNpc = e_GUI.editselectNpc;

            e_GUI.edittileX = editorTileX;
            e_GUI.edittileY = editorTileY;
            e_GUI.edittileH = editorTileH;
            e_GUI.edittileW = editorTileW;
        }

        void DrawMapEditingScreen()
        {
            int realX = ((CurX / picX));
            int realY = ((CurY / picY));
            //int realX = ((CurX / picX) + (viewX));
            //int realY = ((CurY / picY) + (viewY));
            int realCurX = ((CurX) + (viewX * picX));
            int realCurY = ((CurY) + (viewY * picY));

            e_Window.SetFramerateLimit(60);

            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    e_Map.DrawTile(e_Window, new Vector2f((x * picX), (y * picY)), e_Map.Ground[x, y].tileX, e_Map.Ground[x, y].tileY, e_Map.Ground[x, y].tileW, e_Map.Ground[x, y].tileH, e_Map.Ground[x, y].Tileset);

                    if (e_Map.Mask[x, y].tileX > 0 || e_Map.Mask[x, y].tileY > 0)
                    {
                        e_Map.DrawTile(e_Window, new Vector2f((x * picX), (y * picY)), e_Map.Mask[x, y].tileX, e_Map.Mask[x, y].tileY, e_Map.Mask[x, y].tileW, e_Map.Mask[x, y].tileH, e_Map.Mask[x, y].Tileset);
                    }
                    if (e_Map.MaskA[x, y].tileX > 0 || e_Map.MaskA[x, y].tileY > 0)
                    {
                        e_Map.DrawTile(e_Window, new Vector2f((x * picX), (y * picY)), e_Map.MaskA[x, y].tileX, e_Map.MaskA[x, y].tileY, e_Map.MaskA[x, y].tileW, e_Map.MaskA[x, y].tileH, e_Map.MaskA[x, y].Tileset);
                    }
                    if (e_Map.Fringe[x, y].tileX > 0 || e_Map.Fringe[x, y].tileY > 0)
                    {
                        e_Map.DrawTile(e_Window, new Vector2f((x * picX), (y * picY)), e_Map.Fringe[x, y].tileX, e_Map.Fringe[x, y].tileY, e_Map.Fringe[x, y].tileW, e_Map.Fringe[x, y].tileH, e_Map.Fringe[x, y].Tileset);
                    }
                    if (e_Map.FringeA[x, y].tileX > 0 || e_Map.FringeA[x, y].tileY > 0)
                    {
                        e_Map.DrawTile(e_Window, new Vector2f((x * picX), (y * picY)), e_Map.FringeA[x, y].tileX, e_Map.FringeA[x, y].tileY, e_Map.FringeA[x, y].tileW, e_Map.FringeA[x, y].tileH, e_Map.FringeA[x, y].Tileset);
                    }
                }
            }

            if (editMode == (int)EditorModes.Layer)
            {
                if (editorBrushSize > 1)
                {
                    e_Tile.Texture = e_Tileset[editorTileset];
                    e_Tile.TextureRect = new IntRect(editorTileX * picX, editorTileY * picY, picX, picY);
                    for (int x = 0; x < editorBrushSize; x++)
                    {
                        for (int y = 0; y < editorBrushSize; y++)
                        {
                            e_Tile.Position = new Vector2f(((x + realX) * picX), ((y + realY) * picY));
                            e_Window.Draw(e_Tile);
                        }
                    }
                }
                else
                {
                    e_Tile.Texture = e_Tileset[editorTileset];
                    e_Tile.TextureRect = new IntRect(editorTileX * picX, editorTileY * picY, editorTileW * picX, editorTileH * picY);
                    e_Tile.Position = new Vector2f((realX * picX), (realY * picY));
                    e_Window.Draw(e_Tile);
                }
            }

            if (gridActive == true)
            {
                e_Grid.Texture = new Texture("Resources/Skins/Grid.png");
                e_Grid.TextureRect = new IntRect(0, 0, 32, 32);

                for (int x = 0; x < 38; x++)
                {
                    for (int y = 0; y < 19; y++)
                    {
                        e_Grid.Position = new Vector2f((x + viewX) * picX, (y + viewY) * picY);
                        e_Window.Draw(e_Grid);
                    }
                }
            }

            if (utiLayers == true)
            {
                for (int x = 0; x < 50; x++)
                {
                    for (int y = 0; y < 50; y++)
                    {
                        if (e_Map.Ground[x, y].type > 0)
                        {
                            switch (e_Map.Ground[x, y].type)
                            {
                                case (int)TileType.None:
                                    break;

                                case (int)TileType.Blocked:
                                    e_Text.DrawText(e_Window, "B", new Vector2f((x * picX) + 12, (y * picY) + 7), 12, Color.Red);
                                    break;
                                case (int)TileType.NPCSpawn:
                                    e_Text.DrawText(e_Window, "S", new Vector2f((x * picX) + 12, (y * picY) + 7), 12, Color.Yellow);
                                    break;
                            }
                        }
                    }
                }
            }

            e_Text.DrawText(e_Window, "FPS: " + CalculateFrameRate(), new Vector2f((viewX * picX) + 5, (viewY * picY) + 0), 12, Color.White); //Draw fps
            e_Text.DrawText(e_Window, "MapX: " + realX + " MapY: " + realY, new Vector2f((viewX * picX) + 5, (viewY * picY) + 10), 12, Color.Yellow);
            e_Text.DrawText(e_Window, "CurX: " + realCurX + " CurY: " + realCurY, new Vector2f((viewX * picX) + 5, (viewY * picY) + 20), 12, Color.Yellow);
            e_Text.DrawText(e_Window, "ViewX: " + viewX + " ViewY: " + viewY, new Vector2f((viewX * picX) + 5, (viewY * picY) + 30), 12, Color.Yellow);
            e_Text.DrawText(e_Window, isInput, new Vector2f((viewX * picX) + 5, (viewY * picY) + 40), 12, Color.Yellow);

        }

        void DrawTileSelectionScreen()
        {
            SetNewEditorValues();
            e_Window.SetFramerateLimit(32);
            e_Tiles.Position = new Vector2f(0, 0);
            e_Tiles.Texture = e_Tileset[editorTileset];
            e_Tiles.TextureRect = new IntRect(0, 0, (int)e_Tileset[editorTileset].Size.X, (int)e_Tileset[editorTileset].Size.Y);

            e_Tile.Position = new Vector2f(395, 212);
            e_Tile.Texture = e_Tileset[editorTileset];
            e_Tile.TextureRect = new IntRect(editorTileX * picX, editorTileY * picY, editorTileW * picX, editorTileH * picY);

            e_selectTile.OutlineColor = Color.Red;
            e_selectTile.OutlineThickness = 1;
            e_selectTile.FillColor = Color.Transparent;
            e_selectTile.Position = new Vector2f(editorTileX * picX, editorTileY * picY);
            e_selectTile.Size = new Vector2f(editorTileW * picX, editorTileH * picY);

            e_Window.Draw(e_Tiles);
            e_Window.Draw(e_selectTile);
            e_Window.SetView(e_Window.DefaultView); //set it back to default view
            e_Window.Draw(e_Tile);
            e_Text.DrawText(e_Window, "FPS: " + CalculateFrameRate(), new Vector2f(5, 0), 12, Color.White);
            e_Text.DrawText(e_Window, "Edit Mode: " + editNames[editMode], new Vector2f(395, 32), 12, Color.White);
            e_Text.DrawText(e_Window, "Layer Selected: " + layerNames[selectedLayer], new Vector2f(395, 62), 12, Color.White);
            e_Text.DrawText(e_Window, "Type Selected: " + typeNames[selectedType], new Vector2f(395, 92), 12, Color.White);
            e_Text.DrawText(e_Window, "Tileset: " + editorTileset, new Vector2f(395, 122), 12, Color.White);
            e_Text.DrawText(e_Window, "Tile Selected: ", new Vector2f(395, 152), 12, Color.White);
            e_Text.DrawText(e_Window, "Width: " + (editorTileW * picX) + " Height: " + (editorTileH * picY), new Vector2f(395, 182), 12, Color.White);
            e_Canvas.RenderCanvas();  //draw the ui
        }

        void DrawNpcEditorScreen()
        {
            e_Window.SetFramerateLimit(32);
            e_Window.SetView(e_Window.DefaultView);

            if (e_Npc == null)
            {
                e_Npc[1] = new NPC();
                e_Npc[1].LoadNpcFromDatabase(1);
                if (e_Npc[1].Name != null)
                {
                    e_GUI.e_Npc = e_Npc[1];
                    e_GUI.CreateNpcToolWindow(e_Canvas);
                    e_GUI.CreateNpcEditWindow(e_Canvas);
                    e_GUI.LoadNpcDataIntoUI();
                }
                else
                {
                    e_Npc = null;
                }
            }

            e_Text.DrawText(e_Window, "FPS: " + CalculateFrameRate(), new Vector2f(5, 0), 12, Color.White);
            e_Canvas.RenderCanvas();
        }

        void CheckDevelopmentKeys(RenderWindow mainWindow, KeyEventArgs e)
        {
            switch (e.Code)
            {
                //Escapes editor and saves all
                case Keyboard.Key.Escape:
                    e_Map.SaveMap();
                    mainWindow.Close();
                    break;
                //Opens map editor UI
                case Keyboard.Key.Z:
                    if (utiWindow == true)
                    {
                        utiWindow = false;
                        e_GUI.maptoolsWin.Close();
                        viewX = storedViewX;
                        viewY = storedViewY;
                        storedViewX = 0;
                        storedViewY = 0;
                    }
                    else if (!inNpcEditor == true)
                    {
                        utiWindow = true;
                        inNpcEditor = false;
                        e_GUI.CreateToolWindow(e_Canvas);
                        if (e_Npc != null) { e_GUI.npcWin.Hide(); e_GUI.npctoolWin.Hide(); }
                        storedViewX = viewX;
                        storedViewY = viewY;
                        viewX = 0;
                        viewY = 0;
                    }
                    break;
                //Opens npc editor ui
                case Keyboard.Key.X:
                    if (inNpcEditor == true)
                    {
                        inNpcEditor = false;
                    }
                    else
                    {
                        inNpcEditor = true;
                        if (e_Npc != null) { e_GUI.npcWin.Show(); e_GUI.npctoolWin.Show(); }

                        utiWindow = false;
                        if (e_GUI.maptoolsWin != null) { e_GUI.maptoolsWin.Close(); }
                    }
                    break;
                //Change tile size
                case Keyboard.Key.Down:
                    if (utiWindow == true)
                    {
                        editorTileH += 1;
                    }
                    break;
                case Keyboard.Key.Right:
                    if (utiWindow == true)
                    {
                        editorTileW += 1;
                    }
                    break;
                case Keyboard.Key.Up:
                    if (utiWindow == true)
                    {
                        if (editorTileH > 1)
                        {
                            editorTileH -= 1;
                        }
                    }
                    break;
                case Keyboard.Key.Left:
                    if (utiWindow == true)
                    {
                        if (editorTileW > 1)
                        {
                            editorTileW -= 1;
                        }
                    }
                    break;
            }
        }

        void CheckMovementKeys(KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.W)
            {
                if (viewY > 0)
                {
                    viewY -= 1;
                }
            }

            if (e.Code == Keyboard.Key.S)
            {
                if (viewY < (50 - viewOffsetY))
                {
                    viewY += 1;
                }
            }

            if (e.Code == Keyboard.Key.A)
            {
                if (viewX > 0)
                {
                    viewX -= 1;
                }
            }

            if (e.Code == Keyboard.Key.D)
            {
                if (viewX < (50 - viewOffsetX))
                {
                    viewX += 1;
                }
            }
        }
    }

    public enum EditorModes
    {
        Layer,
        Type,
        Tilesets
    }
}
