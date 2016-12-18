using Gwen.Control;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System;
using Tao.OpenGl;
using static System.Environment;
using KeyEventArgs = SFML.Window.KeyEventArgs;
using MessageBox = System.Windows.Forms.MessageBox;
using Lidgren.Network;
using System.Threading;

namespace Client.Classes
{
    class Game
    {
        static RenderWindow c_Window;  //create our main sfml window
        static Canvas c_Canvas;    //create the canvas
        static Gwen.Input.SFML c_Input;    //create input class so gwen can access sfml's input
        static GUI c_GUI;  //create the gui class
        HandleData handleData;  //create the handle data class (udp packet shit)
        Player[] c_Player = new Player[5]; //create player class array
        Npc[] c_Npc = new Npc[10]; //create the npc class array
        Item[] c_Item = new Item[50];   //Create item array
        Projectile[] c_Proj = new Projectile[10]; //create projectile array
        const int Max_Sprites = 200;
        Texture[] c_Sprite = new Texture[Max_Sprites]; //set the players texture to a texture
        Map c_Map = new Map(); //create map class
        View c_View = new View();  //create view for the plaer
        RenderText c_Text = new RenderText();
        ClientConfig c_Config;
        static int lastTick;    //timer for last tick fps
        static int lastFrameRate;   //previous framerate from fps cycle
        static int frameRate;   //current frame rate of fps cycle
        static int fps; //current fps
        static int discoverTick;    //timer to check for server discovery
        static int walkTick;    //timer for walking

        public void GameLoop(NetClient c_Client, ClientConfig c_Config)   //main game loop (loop contains all the must be processed to make the game work)
        {
            c_Window = new RenderWindow(new VideoMode(800, 600), "Sabertooth");    //create the window, width and height
            c_Window.Closed += new EventHandler(OnClose);  //event for if he window closes
            c_Window.KeyReleased += window_KeyReleased;    //event for key releases
            c_Window.KeyPressed += OnKeyPressed;   //event for key pressed
            c_Window.MouseButtonPressed += window_MouseButtonPressed;  //event for mouse button pressed
            c_Window.MouseButtonReleased += window_MouseButtonReleased;    //event for mouse button released
            c_Window.MouseMoved += window_MouseMoved;  //event for mouse movement
            c_Window.TextEntered += window_TextEntered;    //event for text being entered for gwen
            c_Window.SetFramerateLimit(60);    //set the max framerate for our window
            this.c_Config = c_Config;
            Gwen.Renderer.SFML gwenRenderer = new Gwen.Renderer.SFML(c_Window);    //create the renderer that allows gwen to access SFML
            Gwen.Skin.TexturedBase skin = new Gwen.Skin.TexturedBase(gwenRenderer, "Resources/Skins/DefaultSkin.png");  //load the texture for gwen's skin
            Gwen.Font defaultFont = new Gwen.Font(gwenRenderer, "Resources/Fonts/Tahoma.ttf");  //load the font for gwen
            gwenRenderer.LoadFont(defaultFont); //Load the font
            skin.SetDefaultFont(defaultFont.FaceName);  //set the default font
            defaultFont.Dispose();  //dispose of the font
            c_Canvas = new Canvas(skin);   //create the canvas with the skin we loaded
            c_Canvas.SetSize(800, 600);    //set canvas size
            c_Canvas.ShouldDrawBackground = true;  //should we draw the background
            c_Canvas.BackgroundColor = System.Drawing.Color.Transparent;   //draw the background but transparent
            c_Canvas.KeyboardInputEnabled = true;  //enable input from the keyboard for gwen
            c_Input = new Gwen.Input.SFML();   //attach gwen and sfml input classes
            c_Input.Initialize(c_Canvas, c_Window);  //initalize the input both with the canvas and the window
            c_GUI = new GUI(c_Client, c_Canvas, defaultFont, gwenRenderer, c_Player, c_Config);   //create the gui class
            c_GUI.CreateMainWindow(c_Canvas); //create the main window with our login controls

            handleData = new HandleData();  //create handle data 

            for (int i = 0; i < Max_Sprites; i++)   //load our sprite textures
            {
                c_Sprite[i] = new Texture("Resources/Characters/" + (i + 1) + ".png");
            }

            SetupPlayerArray(); //create the player array
            SetupNpcArray();    //create the npc array
            SetupItemArray();   //create the item array
            SetupProjectileArray(); //create the projectile array

            while (c_Window.IsOpen)    //the actual game loop runs as long as the window is open
            {
                CheckForConnection(c_Client);  //check for the server connection
                UpdateView(c_Client, c_Config, c_Npc, c_Item);  //update the players view
                DrawGraphics(c_Client);    //draw graphics like maps, players, npcs, items, sprites
                c_Window.Display();    //display everything we put on the screen
            }

            c_Player[handleData.c_Index].SendUpdateClip(c_Client, handleData.c_Index);            

            //once the loop exits we will clean everything up
            c_Canvas.Dispose();    //dispose of the canvas
            skin.Dispose(); //dispose of the skin
            gwenRenderer.Dispose(); //dispose of the renderer          
            c_Client.Shutdown("Shutting Down");    //run the shutdown void and give it an argument
            Thread.Sleep(500);  //thread needs to sleep before we close the application otherwise it wont
            Exit(0);    //exit the application with the code of 0 meaning everything went smooth
        }

        static void OnClose(object sender, EventArgs args)
        {
            RenderWindow srvrWindow = (RenderWindow)sender;
            srvrWindow.Close();
        }

        static void window_TextEntered(object sender, TextEventArgs e)
        {
            c_Input.ProcessMessage(e);
        }

        static void window_MouseMoved(object sender, MouseMoveEventArgs e)
        {
            c_Input.ProcessMessage(e);
        }

        static void window_MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            c_Input.ProcessMessage(new Gwen.Input.SFMLMouseButtonEventArgs(e, true));
        }

        static void window_MouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            c_Input.ProcessMessage(new Gwen.Input.SFMLMouseButtonEventArgs(e, false));
        }

        static void window_KeyReleased(object sender, KeyEventArgs e)
        {
            c_Input.ProcessMessage(new Gwen.Input.SFMLKeyEventArgs(e, false));
        }

        static void OnKeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape)
                c_Window.Close();

            if (e.Code == Keyboard.Key.F12)
            {
                Image img = c_Window.Capture();
                if (img.Pixels == null)
                {
                    MessageBox.Show("Failed to capture window");
                }
                string path = String.Format("screenshot-{0:D2}{1:D2}{2:D2}.png", DateTime.Now.Hour, DateTime.Now.Minute,
                                            DateTime.Now.Second);
                if (!img.SaveToFile(path))
                    MessageBox.Show(path, "Failed to save screenshot");
                img.Dispose();
            }
            else
            {
                c_Input.ProcessMessage(new Gwen.Input.SFMLKeyEventArgs(e, true));
            }

            if (e.Code == Keyboard.Key.Return)
            {
                if (c_GUI.inputChat != null)
                {
                    if (c_GUI.inputChat.HasFocus == false)
                    {
                        c_GUI.chatWindow.Focus();
                        c_GUI.inputChat.Focus();
                    }
                }
            }

            if (e.Code == Keyboard.Key.Tab)
            {
                if (c_GUI.chatWindow == null || c_GUI.d_Window == null) { return; }
                if (c_GUI.chatWindow.IsVisible == true || c_GUI.d_Window.IsVisible == true)
                {
                    c_GUI.chatWindow.Hide();
                    c_GUI.d_Window.Hide();
                }
                else if (c_GUI.d_Window.IsVisible == false || c_GUI.d_Window.IsVisible == false)
                {
                    c_GUI.chatWindow.Show();
                    c_GUI.d_Window.Show();
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

        private void SetupPlayerArray()
        {
            for (int i = 0; i < 5; i++)
            {
                c_Player[i] = new Player();
            }
        }

        private void SetupNpcArray()
        {
            for (int i = 0; i < 10; i++)
            {
                c_Npc[i] = new Npc();
            }
        }

        private void SetupItemArray()
        {
            for (int i = 0; i < 50; i++)
            {
                c_Item[i] = new Item();
            }
        }

        private void SetupProjectileArray()
        {
            for (int i = 0; i < 10; i++)
            {
                c_Proj[i] = new Projectile();
            }
        }

        void DrawLowLevelTiles()
        {
            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    if (x > 0 && y > 0 && x < 50 && y < 50)
                    {
                        c_Map.DrawTile(c_Window, new Vector2f(x * 32, y * 32), c_Map.Ground[x, y].tileX, c_Map.Ground[x, y].tileY, c_Map.Ground[x, y].tileW, c_Map.Ground[x, y].tileH, c_Map.Ground[x, y].Tileset);

                        if (c_Map.Mask[x, y].tileX > 0 || c_Map.Mask[x, y].tileY > 0)
                        {
                            c_Map.DrawTile(c_Window, new Vector2f(x * 32, y * 32), c_Map.Mask[x, y].tileX, c_Map.Mask[x, y].tileY, c_Map.Mask[x, y].tileW, c_Map.Mask[x, y].tileH, c_Map.Mask[x, y].Tileset);
                        }
                        if (c_Map.MaskA[x, y].tileX > 0 || c_Map.MaskA[x, y].tileY > 0)
                        {
                            c_Map.DrawTile(c_Window, new Vector2f(x * 32, y * 32), c_Map.MaskA[x, y].tileX, c_Map.MaskA[x, y].tileY, c_Map.MaskA[x, y].tileW, c_Map.MaskA[x, y].tileH, c_Map.MaskA[x, y].Tileset);
                        }
                    }
                }
            }
        }

        void DrawUpperLevelTiles()
        {
            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    if (x > 0 && y > 0 && x < 50 && y < 50)
                    {
                        if (c_Map.Fringe[x, y].tileX > 0 || c_Map.Fringe[x, y].tileY > 0)
                        {
                            c_Map.DrawTile(c_Window, new Vector2f(x * 32, y * 32), c_Map.Fringe[x, y].tileX, c_Map.Fringe[x, y].tileY, c_Map.Fringe[x, y].tileW, c_Map.Fringe[x, y].tileH, c_Map.Fringe[x, y].Tileset);
                        }
                        if (c_Map.FringeA[x, y].tileX > 0 || c_Map.FringeA[x, y].tileY > 0)
                        {
                            c_Map.DrawTile(c_Window, new Vector2f(x * 32, y * 32), c_Map.FringeA[x, y].tileX, c_Map.FringeA[x, y].tileY, c_Map.FringeA[x, y].tileW, c_Map.FringeA[x, y].tileH, c_Map.FringeA[x, y].Tileset);
                        }
                    }
                }
            }
        }

        void DrawPlayers()
        {
            for (int i = 0; i < 5; i++)
            {
                if (c_Player[i].Name != "")
                {
                    if (i != handleData.c_Index && c_Player[i].Map == c_Player[handleData.c_Index].Map)
                    {
                        c_Player[i].DrawPlayer(c_Window, c_Sprite[c_Player[i].Sprite]);
                        c_Player[i].DrawPlayerName(c_Window);
                    }
                }
            }
        }

        void DrawNpcs()
        {
            for (int i = 0; i < 10; i++)
            {
                if (c_Map.mapNpc[i].isSpawned == true)
                {
                    c_Map.mapNpc[i].DrawNpc(c_Window, c_Sprite[(c_Map.mapNpc[i].Sprite - 1)]);
                }
            }
        }

        void DrawProjectiles(NetClient c_Client)
        {
            for (int i = 0; i < 200; i++)
            {
                if (c_Map.mapProj[i] != null)
                {
                    c_Map.mapProj[i].DrawProjectile(c_Window);
                    c_Map.mapProj[i].CheckMovment(c_Client, c_Window, c_Map, i);
                }
            }
        }

        void DrawIndexPlayer()
        {
            c_Player[handleData.c_Index].DrawPlayer(c_Window, c_Sprite[c_Player[handleData.c_Index].Sprite]);
            c_Player[handleData.c_Index].DrawPlayerName(c_Window);
        }

        void ProcessMovement()
        {
            for (int i = 0; i < 5; i++)
            {
                if (c_Player[i].tempStep != 5 && i != handleData.c_Index)
                {
                    c_Player[i].X = c_Player[i].tempX;
                    c_Player[i].Y = c_Player[i].tempY;
                    c_Player[i].Direction = c_Player[i].tempDir;
                    c_Player[i].AimDirection = c_Player[i].tempaimDir;
                    c_Player[i].Step = c_Player[i].tempStep;

                    c_Player[i].tempStep = 5;
                }
            }
        }

        void UpdateTitle(int fps)
        {
            c_Window.SetTitle("Sabertooth - Logged: " + c_Player[handleData.c_Index].Name + " FPS: " + fps);
        }

        void CheckForConnection(NetClient c_Client)
        {
            if (c_Client.ServerConnection == null)
            {
                if (TickCount - discoverTick >= 6500)
                {
                    Console.WriteLine("Connecting to server...");
                    c_Client.DiscoverLocalPeers(14242);
                    discoverTick = TickCount;
                }
            }
        }

        void DrawGraphics(NetClient c_Client)
        {
            if (c_Map.Name != null)
            {
                DrawLowLevelTiles();
                DrawNpcs();
                DrawPlayers();
                DrawIndexPlayer();
                DrawProjectiles(c_Client);
                DrawUpperLevelTiles();
                if (TickCount - walkTick > 100)
                {
                    c_Player[handleData.c_Index].CheckMovement(c_Client, handleData.c_Index, c_Window, c_Map, c_GUI);
                    c_Player[handleData.c_Index].CheckAttack(c_Client, c_GUI, c_Window, handleData.c_Index);
                    ProcessMovement();
                    walkTick = TickCount;
                }

            }
            c_Window.SetView(c_Window.DefaultView);   //change the default view back
            c_Canvas.RenderCanvas();   //draw the canvas so it doesnt move
        }

        void UpdateView(NetClient c_Client, ClientConfig c_Config, Npc[] c_Npc, Item[] c_Item)
        {
            UpdateTitle(fps);   //update the title with the fps
            c_View.Reset(new FloatRect(0, 0, 800, 600));
            c_View.Move(new Vector2f(c_Player[handleData.c_Index].X * 32, c_Player[handleData.c_Index].Y * 32));
            handleData.DataMessage(c_Client, c_Canvas, c_GUI, c_Player, c_Map, c_Config, c_Npc, c_Item, c_Proj); 
            c_Window.SetActive();
            c_Window.DispatchEvents();
            c_Window.Clear();
            Gl.glClear(Gl.GL_DEPTH_BUFFER_BIT | Gl.GL_COLOR_BUFFER_BIT);
            c_Window.SetView(c_View);
            fps = CalculateFrameRate();
            c_GUI.UpdateDebugWindow(fps, c_Player, handleData.c_Index);
        }
    }
}