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
        static RenderWindow svrWindow;  //create our main sfml window
        static Canvas svrCanvas;    //create the canvas
        static Gwen.Input.SFML svrInput;    //create input class so gwen can access sfml's input
        static GUI svrGUI;  //create the gui class
        HandleData handleData;  //create the handle data class (udp packet shit)
        Player[] svrPlayer = new Player[5]; //create player class array
        NPC[] svrNpc = new NPC[10]; //create the npc class array
        Texture[] svrSprite = new Texture[200]; //set the players texture to a texture
        Map svrMap = new Map(); //create map class
        View svrView = new View();  //create view for the plaer
        RenderText svrText = new RenderText();
        ClientConfig svrConfig;
        static int lastTick;    //timer for last tick fps
        static int lastFrameRate;   //previous framerate from fps cycle
        static int frameRate;   //current frame rate of fps cycle
        static int fps; //current fps
        static int discoverTick;    //timer to check for server discovery
        static int walkTick;    //timer for walking

        public void GameLoop(NetClient svrClient, ClientConfig cConfig)   //main game loop (loop contains all the must be processed to make the game work)
        {
            svrWindow = new RenderWindow(new VideoMode(800, 600), "Sabertooth");    //create the window, width and height
            svrWindow.Closed += new EventHandler(OnClose);  //event for if he window closes
            svrWindow.KeyReleased += window_KeyReleased;    //event for key releases
            svrWindow.KeyPressed += OnKeyPressed;   //event for key pressed
            svrWindow.MouseButtonPressed += window_MouseButtonPressed;  //event for mouse button pressed
            svrWindow.MouseButtonReleased += window_MouseButtonReleased;    //event for mouse button released
            svrWindow.MouseMoved += window_MouseMoved;  //event for mouse movement
            svrWindow.TextEntered += window_TextEntered;    //event for text being entered for gwen
            svrWindow.SetFramerateLimit(60);    //set the max framerate for our window
            svrConfig = cConfig;
            Gwen.Renderer.SFML gwenRenderer = new Gwen.Renderer.SFML(svrWindow);    //create the renderer that allows gwen to access SFML
            Gwen.Skin.TexturedBase skin = new Gwen.Skin.TexturedBase(gwenRenderer, "Resources/Skins/DefaultSkin.png");  //load the texture for gwen's skin
            Gwen.Font defaultFont = new Gwen.Font(gwenRenderer, "Resources/Fonts/Tahoma.ttf");  //load the font for gwen
            gwenRenderer.LoadFont(defaultFont); //Load the font
            skin.SetDefaultFont(defaultFont.FaceName);  //set the default font
            defaultFont.Dispose();  //dispose of the font
            svrCanvas = new Canvas(skin);   //create the canvas with the skin we loaded
            svrCanvas.SetSize(800, 600);    //set canvas size
            svrCanvas.ShouldDrawBackground = true;  //should we draw the background
            svrCanvas.BackgroundColor = System.Drawing.Color.Transparent;   //draw the background but transparent
            svrCanvas.KeyboardInputEnabled = true;  //enable input from the keyboard for gwen
            svrInput = new Gwen.Input.SFML();   //attach gwen and sfml input classes
            svrInput.Initialize(svrCanvas, svrWindow);  //initalize the input both with the canvas and the window
            svrGUI = new GUI(svrClient, svrCanvas, defaultFont, gwenRenderer, svrPlayer, cConfig);   //create the gui class
            svrGUI.CreateMainWindow(svrCanvas); //create the main window with our login controls

            handleData = new HandleData();  //create handle data 

            for (int i = 0; i < 200; i++)   //load our sprite textures
            {
                svrSprite[i] = new Texture("Resources/Characters/" + (i + 1) + ".png");
            }

            SetupPlayerArray(); //create the player array
            SetupNpcArray();    //create the npc array

            while (svrWindow.IsOpen)    //the actual game loop runs as long as the window is open
            {
                CheckForConnection(svrClient);  //check for the server connection
                UpdateView(svrClient, cConfig, svrNpc);  //update the players view
                DrawGraphics(svrClient);    //draw graphics like maps, players, npcs, items, sprites
                svrWindow.Display();    //display everything we put on the screen
            }

            //once the loop exits we will clean everything up
            svrCanvas.Dispose();    //dispose of the canvas
            skin.Dispose(); //dispose of the skin
            gwenRenderer.Dispose(); //dispose of the renderer
            svrClient.Shutdown("Shutting Down");    //run the shutdown void and give it an argument
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
            svrInput.ProcessMessage(e);
        }

        static void window_MouseMoved(object sender, MouseMoveEventArgs e)
        {
            svrInput.ProcessMessage(e);
        }

        static void window_MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            svrInput.ProcessMessage(new Gwen.Input.SFMLMouseButtonEventArgs(e, true));
        }

        static void window_MouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            svrInput.ProcessMessage(new Gwen.Input.SFMLMouseButtonEventArgs(e, false));
        }

        static void window_KeyReleased(object sender, KeyEventArgs e)
        {
            svrInput.ProcessMessage(new Gwen.Input.SFMLKeyEventArgs(e, false));
        }

        static void OnKeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape)
                svrWindow.Close();

            if (e.Code == Keyboard.Key.F12)
            {
                Image img = svrWindow.Capture();
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
                svrInput.ProcessMessage(new Gwen.Input.SFMLKeyEventArgs(e, true));
            }

            if (e.Code == Keyboard.Key.Return)
            {
                if (svrGUI.inputChat != null)
                {
                    if (svrGUI.inputChat.HasFocus == false)
                    {
                        svrGUI.chatWindow.Focus();
                        svrGUI.inputChat.Focus();
                    }
                }
            }

            if (e.Code == Keyboard.Key.Tab)
            {
                if (svrGUI.chatWindow == null || svrGUI.debugWindow == null) { return; }
                if (svrGUI.chatWindow.IsVisible == true || svrGUI.debugWindow.IsVisible == true)
                {
                    svrGUI.chatWindow.Hide();
                    svrGUI.debugWindow.Hide();
                }
                else if (svrGUI.debugWindow.IsVisible == false || svrGUI.debugWindow.IsVisible == false)
                {
                    svrGUI.chatWindow.Show();
                    svrGUI.debugWindow.Show();
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
                svrPlayer[i] = new Player();
            }
        }

        private void SetupNpcArray()
        {
            for (int i = 0; i < 10; i++)
            {
                svrNpc[i] = new NPC();
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
                        svrMap.DrawTile(svrWindow, new Vector2f(x * 32, y * 32), svrMap.Ground[x, y].tileX, svrMap.Ground[x, y].tileY, svrMap.Ground[x, y].tileW, svrMap.Ground[x, y].tileH, svrMap.Ground[x, y].Tileset);

                        if (svrMap.Mask[x, y].tileX > 0 || svrMap.Mask[x, y].tileY > 0)
                        {
                            svrMap.DrawTile(svrWindow, new Vector2f(x * 32, y * 32), svrMap.Mask[x, y].tileX, svrMap.Mask[x, y].tileY, svrMap.Mask[x, y].tileW, svrMap.Mask[x, y].tileH, svrMap.Mask[x, y].Tileset);
                        }
                        if (svrMap.MaskA[x, y].tileX > 0 || svrMap.MaskA[x, y].tileY > 0)
                        {
                            svrMap.DrawTile(svrWindow, new Vector2f(x * 32, y * 32), svrMap.MaskA[x, y].tileX, svrMap.MaskA[x, y].tileY, svrMap.MaskA[x, y].tileW, svrMap.MaskA[x, y].tileH, svrMap.MaskA[x, y].Tileset);
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
                        if (svrMap.Fringe[x, y].tileX > 0 || svrMap.Fringe[x, y].tileY > 0)
                        {
                            svrMap.DrawTile(svrWindow, new Vector2f(x * 32, y * 32), svrMap.Fringe[x, y].tileX, svrMap.Fringe[x, y].tileY, svrMap.Fringe[x, y].tileW, svrMap.Fringe[x, y].tileH, svrMap.Fringe[x, y].Tileset);
                        }
                        if (svrMap.FringeA[x, y].tileX > 0 || svrMap.FringeA[x, y].tileY > 0)
                        {
                            svrMap.DrawTile(svrWindow, new Vector2f(x * 32, y * 32), svrMap.FringeA[x, y].tileX, svrMap.FringeA[x, y].tileY, svrMap.FringeA[x, y].tileW, svrMap.FringeA[x, y].tileH, svrMap.FringeA[x, y].Tileset);
                        }
                    }
                }
            }
        }

        void DrawPlayers()
        {
            for (int i = 0; i < 5; i++)
            {
                if (svrPlayer[i].Name != "")
                {
                    if (i != handleData.clientIndex)
                    {
                        svrPlayer[i].DrawPlayer(svrWindow, svrSprite[svrPlayer[i].Sprite]);
                        svrPlayer[i].DrawPlayerName(svrWindow);
                    }
                }
            }
        }

        void DrawNpcs()
        {
            for (int i = 0; i < 10; i++)
            {
                if (svrMap.mapNpc[i].isSpawned == true)
                {
                    svrMap.mapNpc[i].DrawNpc(svrWindow, svrSprite[(svrMap.mapNpc[i].Sprite - 1)]);
                }
            }
        }

        void DrawIndexPlayer()
        {
            svrPlayer[handleData.clientIndex].DrawPlayer(svrWindow, svrSprite[svrPlayer[handleData.clientIndex].Sprite]);
            svrPlayer[handleData.clientIndex].DrawPlayerName(svrWindow);
        }

        void ProcessMovement()
        {
            for (int i = 0; i < 5; i++)
            {
                if (svrPlayer[i].tempStep != 5 && i != handleData.clientIndex)
                {
                    svrPlayer[i].X = svrPlayer[i].tempX;
                    svrPlayer[i].Y = svrPlayer[i].tempY;
                    svrPlayer[i].Direction = svrPlayer[i].tempDir;
                    svrPlayer[i].Step = svrPlayer[i].tempStep;

                    svrPlayer[i].tempStep = 5;
                }
            }
        }

        void UpdateTitle(int fps)
        {
            svrWindow.SetTitle("Sabertooth - Logged: " + svrPlayer[handleData.clientIndex].Name + " FPS: " + fps);
        }

        void CheckForConnection(NetClient svrClient)
        {
            if (svrClient.ServerConnection == null)
            {
                if (TickCount - discoverTick >= 6500)
                {
                    Console.WriteLine("Connecting to server...");
                    svrClient.DiscoverLocalPeers(14242);
                    discoverTick = TickCount;
                }
            }
        }

        void DrawGraphics(NetClient svrClient)
        {
            if (svrMap.Name != null)
            {
                DrawLowLevelTiles();
                DrawNpcs();
                DrawPlayers();
                DrawIndexPlayer();
                DrawUpperLevelTiles();
                if (TickCount - walkTick > 100)
                {
                    svrPlayer[handleData.clientIndex].CheckMovement(svrClient, handleData.clientIndex, svrWindow, svrMap, svrGUI);
                    ProcessMovement();
                    walkTick = TickCount;
                }
            }

            svrWindow.SetView(svrWindow.DefaultView);   //change the default view back
            svrCanvas.RenderCanvas();   //draw the canvas so it doesnt move
        }

        void UpdateView(NetClient svrClient, ClientConfig cConfig, NPC[] svrNpc)
        {
            UpdateTitle(fps);   //update the title with the fps
            svrView.Reset(new FloatRect(0, 0, 800, 600));
            svrView.Move(new Vector2f(svrPlayer[handleData.clientIndex].X * 32, svrPlayer[handleData.clientIndex].Y * 32));
            handleData.DataMessage(svrClient, svrCanvas, svrGUI, svrPlayer, svrMap, cConfig, svrNpc); 
            svrWindow.SetActive();
            svrWindow.DispatchEvents();
            svrWindow.Clear();
            Gl.glClear(Gl.GL_DEPTH_BUFFER_BIT | Gl.GL_COLOR_BUFFER_BIT);
            svrWindow.SetView(svrView);
            fps = CalculateFrameRate();
            svrGUI.UpdateDebugWindow(fps, svrPlayer, handleData.clientIndex);
        }
    }
}