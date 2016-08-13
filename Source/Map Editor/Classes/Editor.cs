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

namespace Editor.Classes
{
    class Editor
    {
        //Editor
        public RenderWindow editorWindow;
        static GUI editorUI;
        static Canvas Canvas;
        static Gwen.Input.SFML svrInput;
        View editorView = new View();
        //Map editor 
        public Map editMap = new Map();
        Texture[] TileSet = new Texture[67];
        Sprite Tiles = new Sprite();
        Sprite editorTile = new Sprite();
        Sprite editorGrid = new Sprite();
        RenderText svrText = new RenderText();
        RectangleShape selectTile = new RectangleShape(new Vector2f(32, 32));
        //Npc editor
        public NPC editNpc;
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
                TileSet[i].Dispose();
            }
            TileSet = null;

            editMap.Dispose();
            editMap = null;
            editorView.Dispose();
            editorView = null;
            Tiles.Dispose();
            Tiles = null;
            editorTile.Dispose();
            editorTile = null;
            editorGrid.Dispose();
            editorGrid = null;
            svrText.Dispose();
            svrText = null;
            selectTile.Dispose();
            selectTile = null;

            disposed = true;
        }

        public Editor()
        {
            Console.WriteLine("Loading tilesets...");
            for (int i = 0; i < 67; i++)
            {
                TileSet[i] = new Texture("Resources/Tilesets/" + (i + 1) + ".png");
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
            editorWindow = new RenderWindow(new VideoMode(1216, 608), "Editor Suite");
            editorWindow.Closed += new EventHandler(onClose);
            editorWindow.MouseMoved += new EventHandler<MouseMoveEventArgs>(onMouseMove);
            editorWindow.KeyPressed += new EventHandler<KeyEventArgs>(onKeyPress);
            editorWindow.KeyReleased += new EventHandler<KeyEventArgs>(onKeyReleased);
            editorWindow.TextEntered += new EventHandler<TextEventArgs>(onTextEntered);
            editorWindow.MouseButtonPressed += new EventHandler<MouseButtonEventArgs>(onMouseButton);
            editorWindow.MouseButtonReleased += new EventHandler<MouseButtonEventArgs>(onMouseButtonRelease);
            editorWindow.MouseWheelScrolled += new EventHandler<MouseWheelScrollEventArgs>(onMouseScroll);

            //setup gwen
            Gwen.Renderer.SFML gwenRenderer = new Gwen.Renderer.SFML(editorWindow);
            Gwen.Skin.TexturedBase skin = new Gwen.Skin.TexturedBase(gwenRenderer, "Resources/Skins/DefaultSkin.png");
            Gwen.Font defaultFont = new Gwen.Font(gwenRenderer, "Resources/Fonts/Tahoma.ttf");
            gwenRenderer.LoadFont(defaultFont);
            skin.SetDefaultFont(defaultFont.FaceName);
            defaultFont.Dispose();
            Canvas = new Canvas(skin);
            Canvas.SetSize(1216, 608);
            Canvas.ShouldDrawBackground = true;
            Canvas.BackgroundColor = System.Drawing.Color.Transparent;
            Canvas.KeyboardInputEnabled = true;
            svrInput = new Gwen.Input.SFML();   //attach gwen and sfml input classes
            svrInput.Initialize(Canvas, editorWindow);  //initalize the input both with the canvas and the window
            editorUI = new GUI(Canvas, defaultFont, gwenRenderer, editMap);

            while (editorWindow.IsOpen == true)
            {
                editorWindow.DispatchEvents();

                editorView.Reset(new FloatRect(0, 0, 1216, 608));
                editorView.Move(new Vector2f(viewX * picX, viewY * picY));
                editorWindow.SetView(editorView);

                editorWindow.Clear();

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
                editorWindow.Display();
            }
            Canvas.Dispose();
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
            svrInput.ProcessMessage(new Gwen.Input.SFMLKeyEventArgs(e, true));
            CheckDevelopmentKeys(mainWindow, e);
            CheckMovementKeys(e);
        }

        public void onMouseMove(object sender, MouseMoveEventArgs e)
        {
            svrInput.ProcessMessage(e);

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
                                                editMap.Ground[clickX + x, clickY + y].tileX = editorTileX * picX;
                                                editMap.Ground[clickX + x, clickY + y].tileY = editorTileY * picY;
                                                editMap.Ground[clickX + x, clickY + y].tileH = picX;
                                                editMap.Ground[clickX + x, clickY + y].tileW = picY;
                                                editMap.Ground[clickX + x, clickY + y].Tileset = editorTileset;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        editMap.Ground[clickX, clickY].tileX = editorTileX * picX;
                                        editMap.Ground[clickX, clickY].tileY = editorTileY * picY;
                                        editMap.Ground[clickX, clickY].tileH = picX;
                                        editMap.Ground[clickX, clickY].tileW = picY;
                                        editMap.Ground[clickX, clickY].Tileset = editorTileset;
                                    }
                                    break;
                                case (int)TileLayers.Mask:
                                    if (editorBrushSize > 1)
                                    {
                                        for (int x = 0; x < editorBrushSize; x++)
                                        {
                                            for (int y = 0; y < editorBrushSize; y++)
                                            {
                                                editMap.Mask[clickX + x, clickY + y].tileX = editorTileX * picX;
                                                editMap.Mask[clickX + x, clickY + y].tileY = editorTileY * picY;
                                                editMap.Mask[clickX + x, clickY + y].tileH = picX;
                                                editMap.Mask[clickX + x, clickY + y].tileW = picY;
                                                editMap.Mask[clickX + x, clickY + y].Tileset = editorTileset;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        editMap.Mask[clickX, clickY].tileX = editorTileX * picX;
                                        editMap.Mask[clickX, clickY].tileY = editorTileY * picY;
                                        editMap.Mask[clickX, clickY].tileH = picX;
                                        editMap.Mask[clickX, clickY].tileW = picY;
                                        editMap.Mask[clickX, clickY].Tileset = editorTileset;
                                    }
                                    break;
                                case (int)TileLayers.Fringe:
                                    if (editorBrushSize > 1)
                                    {
                                        for (int x = 0; x < editorBrushSize; x++)
                                        {
                                            for (int y = 0; y < editorBrushSize; y++)
                                            {
                                                editMap.Fringe[clickX + x, clickY + y].tileX = editorTileX * picX;
                                                editMap.Fringe[clickX + x, clickY + y].tileY = editorTileY * picY;
                                                editMap.Fringe[clickX + x, clickY + y].tileH = picX;
                                                editMap.Fringe[clickX + x, clickY + y].tileW = picY;
                                                editMap.Fringe[clickX + x, clickY + y].Tileset = editorTileset;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        editMap.Fringe[clickX, clickY].tileX = editorTileX * picX;
                                        editMap.Fringe[clickX, clickY].tileY = editorTileY * picY;
                                        editMap.Fringe[clickX, clickY].tileH = picX;
                                        editMap.Fringe[clickX, clickY].tileW = picY;
                                        editMap.Fringe[clickX, clickY].Tileset = editorTileset;
                                    }
                                    break;
                                case (int)TileLayers.MaskA:
                                    if (editorBrushSize > 1)
                                    {
                                        for (int x = 0; x < editorBrushSize; x++)
                                        {
                                            for (int y = 0; y < editorBrushSize; y++)
                                            {
                                                editMap.MaskA[clickX + x, clickY + y].tileX = editorTileX * picX;
                                                editMap.MaskA[clickX + x, clickY + y].tileY = editorTileY * picY;
                                                editMap.MaskA[clickX + x, clickY + y].tileH = picX;
                                                editMap.MaskA[clickX + x, clickY + y].tileW = picY;
                                                editMap.MaskA[clickX + x, clickY + y].Tileset = editorTileset;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        editMap.MaskA[clickX, clickY].tileX = editorTileX * picX;
                                        editMap.MaskA[clickX, clickY].tileY = editorTileY * picY;
                                        editMap.MaskA[clickX, clickY].tileH = picX;
                                        editMap.MaskA[clickX, clickY].tileW = picY;
                                        editMap.MaskA[clickX, clickY].Tileset = editorTileset;
                                    }
                                    break;
                                case (int)TileLayers.FringeA:
                                    if (editorBrushSize > 1)
                                    {
                                        for (int x = 0; x < editorBrushSize; x++)
                                        {
                                            for (int y = 0; y < editorBrushSize; y++)
                                            {
                                                editMap.FringeA[clickX + x, clickY + y].tileX = editorTileX * picX;
                                                editMap.FringeA[clickX + x, clickY + y].tileY = editorTileY * picY;
                                                editMap.FringeA[clickX + x, clickY + y].tileH = picX;
                                                editMap.FringeA[clickX + x, clickY + y].tileW = picY;
                                                editMap.FringeA[clickX + x, clickY + y].Tileset = editorTileset;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        editMap.FringeA[clickX, clickY].tileX = editorTileX * picX;
                                        editMap.FringeA[clickX, clickY].tileY = editorTileY * picY;
                                        editMap.FringeA[clickX, clickY].tileH = picX;
                                        editMap.FringeA[clickX, clickY].tileW = picY;
                                        editMap.FringeA[clickX, clickY].Tileset = editorTileset;
                                    }
                                    break;
                            }
                        }
                        else if (editMode == (int)EditorModes.Type)
                        {
                            editMap.Ground[clickX, clickY].type = selectedType;
                            editMap.Ground[clickX, clickY].spawnNum = selectedNpc;
                        }
                    }
                    else if (Mouse.IsButtonPressed(Mouse.Button.Right))
                    {
                        if (editMode == (int)EditorModes.Layer)
                        {
                            switch (selectedLayer)
                            {
                                case (int)TileLayers.Ground:
                                    editMap.Ground[clickX, clickY].tileX = 0;
                                    editMap.Ground[clickX, clickY].tileY = 0;
                                    editMap.Ground[clickX, clickY].tileH = 0;
                                    editMap.Ground[clickX, clickY].tileW = 0;
                                    editMap.Ground[clickX, clickY].Tileset = 0;
                                    break;
                                case (int)TileLayers.Mask:
                                    editMap.Mask[clickX, clickY].tileX = 0;
                                    editMap.Mask[clickX, clickY].tileY = 0;
                                    editMap.Mask[clickX, clickY].tileH = 0;
                                    editMap.Mask[clickX, clickY].tileW = 0;
                                    editMap.Mask[clickX, clickY].Tileset = 0;
                                    break;
                                case (int)TileLayers.Fringe:
                                    editMap.Fringe[clickX, clickY].tileX = editorTileX * picX;
                                    editMap.Fringe[clickX, clickY].tileY = editorTileY * picY;
                                    editMap.Fringe[clickX, clickY].tileH = editorTileH;
                                    editMap.Fringe[clickX, clickY].tileW = editorTileW;
                                    editMap.Fringe[clickX, clickY].Tileset = 0;
                                    break;
                                case (int)TileLayers.MaskA:
                                    editMap.MaskA[clickX, clickY].tileX = editorTileX * picX;
                                    editMap.MaskA[clickX, clickY].tileY = editorTileY * picY;
                                    editMap.MaskA[clickX, clickY].tileH = editorTileH;
                                    editMap.MaskA[clickX, clickY].tileW = editorTileW;
                                    editMap.MaskA[clickX, clickY].Tileset = 0;
                                    break;
                                case (int)TileLayers.FringeA:
                                    editMap.FringeA[clickX, clickY].tileX = editorTileX * picX;
                                    editMap.FringeA[clickX, clickY].tileY = editorTileY * picY;
                                    editMap.FringeA[clickX, clickY].tileH = editorTileH;
                                    editMap.FringeA[clickX, clickY].tileW = editorTileW;
                                    editMap.FringeA[clickX, clickY].Tileset = 0;
                                    break;
                            }
                        }
                        else if (editMode == (int)EditorModes.Type)
                        {
                            editMap.Ground[clickX, clickY].type = (int)TileType.None;
                            editMap.Ground[clickX, clickY].spawnNum = 0;
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
                svrInput.ProcessMessage(new Gwen.Input.SFMLMouseButtonEventArgs(e, true));

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
                                                editMap.Ground[clickX + x, clickY + y].tileX = (editorTileX + x) * picX;
                                                editMap.Ground[clickX + x, clickY + y].tileY = (editorTileY + y) * picY;
                                                editMap.Ground[clickX + x, clickY + y].tileH = picX;
                                                editMap.Ground[clickX + x, clickY + y].tileW = picY;
                                                editMap.Ground[clickX + x, clickY + y].Tileset = editorTileset;
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
                                                    editMap.Ground[clickX + x, clickY + y].tileX = editorTileX * picX;
                                                    editMap.Ground[clickX + x, clickY + y].tileY = editorTileY * picY;
                                                    editMap.Ground[clickX + x, clickY + y].tileH = picX;
                                                    editMap.Ground[clickX + x, clickY + y].tileW = picY;
                                                    editMap.Ground[clickX + x, clickY + y].Tileset = editorTileset;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            editMap.Ground[clickX, clickY].tileX = editorTileX * picX;
                                            editMap.Ground[clickX, clickY].tileY = editorTileY * picY;
                                            editMap.Ground[clickX, clickY].tileH = picX;
                                            editMap.Ground[clickX, clickY].tileW = picY;
                                            editMap.Ground[clickX, clickY].Tileset = editorTileset;
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
                                                editMap.Mask[clickX + x, clickY + y].tileX = (editorTileX + x) * picX;
                                                editMap.Mask[clickX + x, clickY + y].tileY = (editorTileY + y) * picY;
                                                editMap.Mask[clickX + x, clickY + y].tileH = picX;
                                                editMap.Mask[clickX + x, clickY + y].tileW = picY;
                                                editMap.Mask[clickX + x, clickY + y].Tileset = editorTileset;
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
                                                    editMap.Mask[clickX + x, clickY + y].tileX = editorTileX * picX;
                                                    editMap.Mask[clickX + x, clickY + y].tileY = editorTileY * picY;
                                                    editMap.Mask[clickX + x, clickY + y].tileH = picX;
                                                    editMap.Mask[clickX + x, clickY + y].tileW = picY;
                                                    editMap.Mask[clickX + x, clickY + y].Tileset = editorTileset;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            editMap.Mask[clickX, clickY].tileX = editorTileX * picX;
                                            editMap.Mask[clickX, clickY].tileY = editorTileY * picY;
                                            editMap.Mask[clickX, clickY].tileH = picX;
                                            editMap.Mask[clickX, clickY].tileW = picY;
                                            editMap.Mask[clickX, clickY].Tileset = editorTileset;
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
                                                editMap.Fringe[clickX + x, clickY + y].tileX = (editorTileX + x) * picX;
                                                editMap.Fringe[clickX + x, clickY + y].tileY = (editorTileY + y) * picY;
                                                editMap.Fringe[clickX + x, clickY + y].tileH = picX;
                                                editMap.Fringe[clickX + x, clickY + y].tileW = picY;
                                                editMap.Fringe[clickX + x, clickY + y].Tileset = editorTileset;
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
                                                    editMap.Fringe[clickX + x, clickY + y].tileX = editorTileX * picX;
                                                    editMap.Fringe[clickX + x, clickY + y].tileY = editorTileY * picY;
                                                    editMap.Fringe[clickX + x, clickY + y].tileH = picX;
                                                    editMap.Fringe[clickX + x, clickY + y].tileW = picY;
                                                    editMap.Fringe[clickX + x, clickY + y].Tileset = editorTileset;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            editMap.Fringe[clickX, clickY].tileX = editorTileX * picX;
                                            editMap.Fringe[clickX, clickY].tileY = editorTileY * picY;
                                            editMap.Fringe[clickX, clickY].tileH = picX;
                                            editMap.Fringe[clickX, clickY].tileW = picY;
                                            editMap.Fringe[clickX, clickY].Tileset = editorTileset;
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
                                                editMap.MaskA[clickX + x, clickY + y].tileX = (editorTileX + x) * picX;
                                                editMap.MaskA[clickX + x, clickY + y].tileY = (editorTileY + y) * picY;
                                                editMap.MaskA[clickX + x, clickY + y].tileH = picX;
                                                editMap.MaskA[clickX + x, clickY + y].tileW = picY;
                                                editMap.MaskA[clickX + x, clickY + y].Tileset = editorTileset;
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
                                                    editMap.MaskA[clickX + x, clickY + y].tileX = editorTileX * picX;
                                                    editMap.MaskA[clickX + x, clickY + y].tileY = editorTileY * picY;
                                                    editMap.MaskA[clickX + x, clickY + y].tileH = picX;
                                                    editMap.MaskA[clickX + x, clickY + y].tileW = picY;
                                                    editMap.MaskA[clickX + x, clickY + y].Tileset = editorTileset;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            editMap.MaskA[clickX, clickY].tileX = editorTileX * picX;
                                            editMap.MaskA[clickX, clickY].tileY = editorTileY * picY;
                                            editMap.MaskA[clickX, clickY].tileH = picX;
                                            editMap.MaskA[clickX, clickY].tileW = picY;
                                            editMap.MaskA[clickX, clickY].Tileset = editorTileset;
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
                                                editMap.FringeA[clickX + x, clickY + y].tileX = (editorTileX + x) * picX;
                                                editMap.FringeA[clickX + x, clickY + y].tileY = (editorTileY + y) * picY;
                                                editMap.FringeA[clickX + x, clickY + y].tileH = picX;
                                                editMap.FringeA[clickX + x, clickY + y].tileW = picY;
                                                editMap.FringeA[clickX + x, clickY + y].Tileset = editorTileset;
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
                                                    editMap.FringeA[clickX + x, clickY + y].tileX = editorTileX * picX;
                                                    editMap.FringeA[clickX + x, clickY + y].tileY = editorTileY * picY;
                                                    editMap.FringeA[clickX + x, clickY + y].tileH = picX;
                                                    editMap.FringeA[clickX + x, clickY + y].tileW = picY;
                                                    editMap.FringeA[clickX + x, clickY + y].Tileset = editorTileset;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            editMap.FringeA[clickX, clickY].tileX = editorTileX * picX;
                                            editMap.FringeA[clickX, clickY].tileY = editorTileY * picY;
                                            editMap.FringeA[clickX, clickY].tileH = picX;
                                            editMap.FringeA[clickX, clickY].tileW = picY;
                                            editMap.FringeA[clickX, clickY].Tileset = editorTileset;
                                        }
                                    }
                                    break;
                            }
                        }
                        else if (editMode == (int)EditorModes.Type)
                        {
                            editMap.Ground[clickX, clickY].type = selectedType;
                            editMap.Ground[clickX, clickY].spawnNum = selectedNpc;
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
                                    editMap.Ground[clickX, clickY].tileX = 0;
                                    editMap.Ground[clickX, clickY].tileY = 0;
                                    editMap.Ground[clickX, clickY].tileH = 0;
                                    editMap.Ground[clickX, clickY].tileW = 0;
                                    editMap.Ground[clickX, clickY].Tileset = 0;
                                    break;
                                case (int)TileLayers.Mask:
                                    editMap.Mask[clickX, clickY].tileX = 0;
                                    editMap.Mask[clickX, clickY].tileY = 0;
                                    editMap.Mask[clickX, clickY].tileH = 0;
                                    editMap.Mask[clickX, clickY].tileW = 0;
                                    editMap.Mask[clickX, clickY].Tileset = 0;
                                    break;
                                case (int)TileLayers.Fringe:
                                    editMap.Fringe[clickX, clickY].tileX = 0;
                                    editMap.Fringe[clickX, clickY].tileY = 0;
                                    editMap.Fringe[clickX, clickY].tileH = 0;
                                    editMap.Fringe[clickX, clickY].tileW = 0;
                                    editMap.Fringe[clickX, clickY].Tileset = 0;
                                    break;
                                case (int)TileLayers.MaskA:
                                    editMap.MaskA[clickX, clickY].tileX = 0;
                                    editMap.MaskA[clickX, clickY].tileY = 0;
                                    editMap.MaskA[clickX, clickY].tileH = 0;
                                    editMap.MaskA[clickX, clickY].tileW = 0;
                                    editMap.MaskA[clickX, clickY].Tileset = 0;
                                    break;
                                case (int)TileLayers.FringeA:
                                    editMap.FringeA[clickX, clickY].tileX = 0;
                                    editMap.FringeA[clickX, clickY].tileY = 0;
                                    editMap.FringeA[clickX, clickY].tileH = 0;
                                    editMap.FringeA[clickX, clickY].tileW = 0;
                                    editMap.FringeA[clickX, clickY].Tileset = 0;
                                    break;
                            }
                        }
                        else if (editMode == (int)EditorModes.Type)
                        {
                            editMap.Ground[clickX, clickY].type = (int)TileType.None;
                            editMap.Ground[clickX, clickY].spawnNum = 0;
                        }
                    }
                    else if (e.Button == Mouse.Button.Middle)
                    {
                        isInput = "Click: Middle";

                        editMap.Ground[clickX, clickY].tileX = 0;
                        editMap.Ground[clickX, clickY].tileY = 32;
                        editMap.Ground[clickX, clickY].tileH = 32;
                        editMap.Ground[clickX, clickY].tileW = 32;
                        editMap.Ground[clickX, clickY].Tileset = 0;
                        editMap.Ground[clickX, clickY].type = (int)TileType.None;
                        editMap.Mask[clickX, clickY].tileX = 0;
                        editMap.Mask[clickX, clickY].tileY = 0;
                        editMap.Mask[clickX, clickY].tileH = 0;
                        editMap.Mask[clickX, clickY].tileW = 0;
                        editMap.Mask[clickX, clickY].Tileset = 0;
                        editMap.Mask[clickX, clickY].type = (int)TileType.None;
                        editMap.Fringe[clickX, clickY].tileX = 0;
                        editMap.Fringe[clickX, clickY].tileY = 0;
                        editMap.Fringe[clickX, clickY].tileH = 0;
                        editMap.Fringe[clickX, clickY].tileW = 0;
                        editMap.Fringe[clickX, clickY].Tileset = 0;
                        editMap.Fringe[clickX, clickY].type = (int)TileType.None;
                        editMap.MaskA[clickX, clickY].tileX = 0;
                        editMap.MaskA[clickX, clickY].tileY = 0;
                        editMap.MaskA[clickX, clickY].tileH = 0;
                        editMap.MaskA[clickX, clickY].tileW = 0;
                        editMap.MaskA[clickX, clickY].Tileset = 0;
                        editMap.MaskA[clickX, clickY].type = (int)TileType.None;
                        editMap.FringeA[clickX, clickY].tileX = 0;
                        editMap.FringeA[clickX, clickY].tileY = 0;
                        editMap.FringeA[clickX, clickY].tileH = 0;
                        editMap.FringeA[clickX, clickY].tileW = 0;
                        editMap.FringeA[clickX, clickY].Tileset = 0;
                        editMap.FringeA[clickX, clickY].type = (int)TileType.None;
                    }
                }
                catch (Exception)
                {
                    //MsgBox("Out of bounds!", MsgBoxStyle.OkOnly, "Error");
                }
            }

            if (inNpcEditor == true)
            {
                svrInput.ProcessMessage(new Gwen.Input.SFMLMouseButtonEventArgs(e, true));
            }
        }

        public void onMouseButtonRelease(object sender, MouseButtonEventArgs e)
        {
            svrInput.ProcessMessage(new Gwen.Input.SFMLMouseButtonEventArgs(e, false));
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
            svrInput.ProcessMessage(new Gwen.Input.SFMLKeyEventArgs(e, false));
        }

        public void onTextEntered(object sender, TextEventArgs e)
        {
            svrInput.ProcessMessage(e);
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

        void SetNewEditorValues()
        {
            editMode = editorUI.editMode;
            selectedLayer = editorUI.editLayer;
            editorTileset = editorUI.editTileset;
            selectedType = editorUI.editType;
            gridActive = editorUI.editGrid;
            utiLayers = editorUI.editshowTypes;
            editorBrushSize = editorUI.brushSize;
            selectedNpc = editorUI.editselectNpc;

            editorUI.edittileX = editorTileX;
            editorUI.edittileY = editorTileY;
            editorUI.edittileH = editorTileH;
            editorUI.edittileW = editorTileW;
        }

        void DrawMapEditingScreen()
        {
            int realX = ((CurX / picX));
            int realY = ((CurY / picY));
            //int realX = ((CurX / picX) + (viewX));
            //int realY = ((CurY / picY) + (viewY));
            int realCurX = ((CurX) + (viewX * picX));
            int realCurY = ((CurY) + (viewY * picY));

            editorWindow.SetFramerateLimit(60);

            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    editMap.DrawTile(editorWindow, new Vector2f((x * picX), (y * picY)), editMap.Ground[x, y].tileX, editMap.Ground[x, y].tileY, editMap.Ground[x, y].tileW, editMap.Ground[x, y].tileH, editMap.Ground[x, y].Tileset);

                    if (editMap.Mask[x, y].tileX > 0 || editMap.Mask[x, y].tileY > 0)
                    {
                        editMap.DrawTile(editorWindow, new Vector2f((x * picX), (y * picY)), editMap.Mask[x, y].tileX, editMap.Mask[x, y].tileY, editMap.Mask[x, y].tileW, editMap.Mask[x, y].tileH, editMap.Mask[x, y].Tileset);
                    }
                    if (editMap.MaskA[x, y].tileX > 0 || editMap.MaskA[x, y].tileY > 0)
                    {
                        editMap.DrawTile(editorWindow, new Vector2f((x * picX), (y * picY)), editMap.MaskA[x, y].tileX, editMap.MaskA[x, y].tileY, editMap.MaskA[x, y].tileW, editMap.MaskA[x, y].tileH, editMap.MaskA[x, y].Tileset);
                    }
                    if (editMap.Fringe[x, y].tileX > 0 || editMap.Fringe[x, y].tileY > 0)
                    {
                        editMap.DrawTile(editorWindow, new Vector2f((x * picX), (y * picY)), editMap.Fringe[x, y].tileX, editMap.Fringe[x, y].tileY, editMap.Fringe[x, y].tileW, editMap.Fringe[x, y].tileH, editMap.Fringe[x, y].Tileset);
                    }
                    if (editMap.FringeA[x, y].tileX > 0 || editMap.FringeA[x, y].tileY > 0)
                    {
                        editMap.DrawTile(editorWindow, new Vector2f((x * picX), (y * picY)), editMap.FringeA[x, y].tileX, editMap.FringeA[x, y].tileY, editMap.FringeA[x, y].tileW, editMap.FringeA[x, y].tileH, editMap.FringeA[x, y].Tileset);
                    }
                }
            }

            if (editMode == (int)EditorModes.Layer)
            {
                if (editorBrushSize > 1)
                {
                    editorTile.Texture = TileSet[editorTileset];
                    editorTile.TextureRect = new IntRect(editorTileX * picX, editorTileY * picY, picX, picY);
                    for (int x = 0; x < editorBrushSize; x++)
                    {
                        for (int y = 0; y < editorBrushSize; y++)
                        {
                            editorTile.Position = new Vector2f(((x + realX) * picX), ((y + realY) * picY));
                            editorWindow.Draw(editorTile);
                        }
                    }
                }
                else
                {
                    editorTile.Texture = TileSet[editorTileset];
                    editorTile.TextureRect = new IntRect(editorTileX * picX, editorTileY * picY, editorTileW * picX, editorTileH * picY);
                    editorTile.Position = new Vector2f((realX * picX), (realY * picY));
                    editorWindow.Draw(editorTile);
                }
            }

            if (gridActive == true)
            {
                editorGrid.Texture = new Texture("Resources/Skins/Grid.png");
                editorGrid.TextureRect = new IntRect(0, 0, 32, 32);

                for (int x = 0; x < 38; x++)
                {
                    for (int y = 0; y < 19; y++)
                    {
                        editorGrid.Position = new Vector2f((x + viewX) * picX, (y + viewY) * picY);
                        editorWindow.Draw(editorGrid);
                    }
                }
            }

            if (utiLayers == true)
            {
                for (int x = 0; x < 50; x++)
                {
                    for (int y = 0; y < 50; y++)
                    {
                        if (editMap.Ground[x, y].type > 0)
                        {
                            switch (editMap.Ground[x, y].type)
                            {
                                case (int)TileType.None:
                                    break;

                                case (int)TileType.Blocked:
                                    svrText.DrawText(editorWindow, "B", new Vector2f((x * picX) + 12, (y * picY) + 7), 12, Color.Red);
                                    break;
                                case (int)TileType.NPCSpawn:
                                    svrText.DrawText(editorWindow, "S", new Vector2f((x * picX) + 12, (y * picY) + 7), 12, Color.Yellow);
                                    break;
                            }
                        }
                    }
                }
            }

            svrText.DrawText(editorWindow, "FPS: " + CalculateFrameRate(), new Vector2f((viewX * picX) + 5, (viewY * picY) + 0), 12, Color.White); //Draw fps
            svrText.DrawText(editorWindow, "MapX: " + realX + " MapY: " + realY, new Vector2f((viewX * picX) + 5, (viewY * picY) + 10), 12, Color.Yellow);
            svrText.DrawText(editorWindow, "CurX: " + realCurX + " CurY: " + realCurY, new Vector2f((viewX * picX) + 5, (viewY * picY) + 20), 12, Color.Yellow);
            svrText.DrawText(editorWindow, "ViewX: " + viewX + " ViewY: " + viewY, new Vector2f((viewX * picX) + 5, (viewY * picY) + 30), 12, Color.Yellow);
            svrText.DrawText(editorWindow, isInput, new Vector2f((viewX * picX) + 5, (viewY * picY) + 40), 12, Color.Yellow);

        }

        void DrawTileSelectionScreen()
        {
            SetNewEditorValues();
            editorWindow.SetFramerateLimit(32);
            Tiles.Position = new Vector2f(0, 0);
            Tiles.Texture = TileSet[editorTileset];
            Tiles.TextureRect = new IntRect(0, 0, (int)TileSet[editorTileset].Size.X, (int)TileSet[editorTileset].Size.Y);

            editorTile.Position = new Vector2f(395, 212);
            editorTile.Texture = TileSet[editorTileset];
            editorTile.TextureRect = new IntRect(editorTileX * picX, editorTileY * picY, editorTileW * picX, editorTileH * picY);

            selectTile.OutlineColor = Color.Red;
            selectTile.OutlineThickness = 1;
            selectTile.FillColor = Color.Transparent;
            selectTile.Position = new Vector2f(editorTileX * picX, editorTileY * picY);
            selectTile.Size = new Vector2f(editorTileW * picX, editorTileH * picY);

            editorWindow.Draw(Tiles);
            editorWindow.Draw(selectTile);
            editorWindow.SetView(editorWindow.DefaultView); //set it back to default view
            editorWindow.Draw(editorTile);
            svrText.DrawText(editorWindow, "FPS: " + CalculateFrameRate(), new Vector2f(5, 0), 12, Color.White);
            svrText.DrawText(editorWindow, "Edit Mode: " + editNames[editMode], new Vector2f(395, 32), 12, Color.White);
            svrText.DrawText(editorWindow, "Layer Selected: " + layerNames[selectedLayer], new Vector2f(395, 62), 12, Color.White);
            svrText.DrawText(editorWindow, "Type Selected: " + typeNames[selectedType], new Vector2f(395, 92), 12, Color.White);
            svrText.DrawText(editorWindow, "Tileset: " + editorTileset, new Vector2f(395, 122), 12, Color.White);
            svrText.DrawText(editorWindow, "Tile Selected: ", new Vector2f(395, 152), 12, Color.White);
            svrText.DrawText(editorWindow, "Width: " + (editorTileW * picX) + " Height: " + (editorTileH * picY), new Vector2f(395, 182), 12, Color.White);
            Canvas.RenderCanvas();  //draw the ui
        }

        void DrawNpcEditorScreen()
        {
            editorWindow.SetFramerateLimit(32);
            editorWindow.SetView(editorWindow.DefaultView);

            if (editNpc == null)
            {
                editNpc = new NPC();
                editNpc.LoadNPC();
                if (editNpc.Name != null)
                {
                    editorUI.editorNpc = editNpc;
                    editorUI.CreateNpcToolWindow(Canvas);
                    editorUI.CreateNpcEditWindow(Canvas);
                    editorUI.LoadNpcDataIntoUI();
                }
                else
                {
                    editNpc = null;
                }
            }

            svrText.DrawText(editorWindow, "FPS: " + CalculateFrameRate(), new Vector2f(5, 0), 12, Color.White);
            Canvas.RenderCanvas();
        }

        void CheckDevelopmentKeys(RenderWindow mainWindow, KeyEventArgs e)
        {
            switch (e.Code)
            {
                //Escapes editor and saves all
                case Keyboard.Key.Escape:
                    editMap.SaveMap();
                    mainWindow.Close();
                    break;
                //Opens map editor UI
                case Keyboard.Key.Z:
                    if (utiWindow == true)
                    {
                        utiWindow = false;
                        editorUI.maptoolsWin.Close();
                        viewX = storedViewX;
                        viewY = storedViewY;
                        storedViewX = 0;
                        storedViewY = 0;
                    }
                    else
                    {
                        utiWindow = true;
                        inNpcEditor = false;
                        editorUI.CreateToolWindow(Canvas);
                        if (editNpc != null) { editorUI.npcWin.Hide(); editorUI.npctoolWin.Hide(); }
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
                        if (editNpc != null) { editorUI.npcWin.Show(); editorUI.npctoolWin.Show(); }

                        utiWindow = false;
                        if (editorUI.maptoolsWin != null) { editorUI.maptoolsWin.Close(); }
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
