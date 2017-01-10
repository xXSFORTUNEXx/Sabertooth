using Gwen.Control;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using Gwen.UnitTest;
using System.IO;
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
        static RenderWindow c_Window;
        static Canvas c_Canvas;    
        static Gwen.Input.SFML c_Input;  
        static GUI c_GUI;  
        HandleData handleData; 
        Player[] c_Player = new Player[5]; 
        Npc[] c_Npc = new Npc[10]; 
        Item[] c_Item = new Item[50];   
        Projectile[] c_Proj = new Projectile[10]; 
        Texture[] c_Sprite = new Texture[204];
        Texture[] c_ItemSprite = new Texture[4];
        Map c_Map = new Map();
        View c_View = new View(); 
        RenderText c_Text = new RenderText();
        ClientConfig c_Config;
        static int lastTick;
        static int lastFrameRate;  
        static int frameRate; 
        static int fps; 
        static int discoverTick;    
        static int walkTick;
        static int attackTick;
        static int pickupTick;

        public void GameLoop(NetClient c_Client, ClientConfig c_Config)  
        {
            c_Window = new RenderWindow(new VideoMode(800, 600), "Sabertooth");    
            c_Window.Closed += new EventHandler(OnClose);
            c_Window.KeyReleased += window_KeyReleased;    
            c_Window.KeyPressed += OnKeyPressed;  
            c_Window.MouseButtonPressed += window_MouseButtonPressed; 
            c_Window.MouseButtonReleased += window_MouseButtonReleased;
            c_Window.MouseMoved += window_MouseMoved;  
            c_Window.TextEntered += window_TextEntered;  
            c_Window.SetFramerateLimit(65);    
            this.c_Config = c_Config;
            Gwen.Renderer.SFML gwenRenderer = new Gwen.Renderer.SFML(c_Window);   
            Gwen.Skin.TexturedBase skin = new Gwen.Skin.TexturedBase(gwenRenderer, "Resources/Skins/DefaultSkin.png"); 
            Gwen.Font defaultFont = new Gwen.Font(gwenRenderer, "Resources/Fonts/Tahoma.ttf");  
            gwenRenderer.LoadFont(defaultFont);
            skin.SetDefaultFont(defaultFont.FaceName);  
            defaultFont.Dispose(); 
            c_Canvas = new Canvas(skin);   
            c_Canvas.SetSize(800, 600);    
            c_Canvas.ShouldDrawBackground = true;  
            c_Canvas.BackgroundColor = System.Drawing.Color.Transparent;   
            c_Canvas.KeyboardInputEnabled = true;
            c_Input = new Gwen.Input.SFML(); 
            c_Input.Initialize(c_Canvas, c_Window); 
            c_GUI = new GUI(c_Client, c_Canvas, defaultFont, gwenRenderer, c_Player, c_Config);  
            c_GUI.CreateMainWindow(c_Canvas); 

            handleData = new HandleData(); 

            for (int i = 0; i < 204; i++)   
            {
                c_Sprite[i] = new Texture("Resources/Characters/" + (i + 1) + ".png");
            }
            for (int i =0; i < 4; i++)
            {
                c_ItemSprite[i] = new Texture("Resources/Items/" + (i + 1) + ".png");
            }

            SetupPlayerArray(); 
            SetupNpcArray();    
            SetupItemArray();  
            SetupProjectileArray();

            while (c_Window.IsOpen)    
            {
                CheckForConnection(c_Client);  
                UpdateView(c_Client, c_Config, c_Npc, c_Item); 
                DrawGraphics(c_Client);
                UpdateOverlay();
                c_Window.Display(); 
            }

            c_Player[handleData.c_Index].SendUpdateClip(c_Client, handleData.c_Index);            

            c_Canvas.Dispose(); 
            skin.Dispose();
            gwenRenderer.Dispose();        
            c_Client.Shutdown("Shutting Down");  
            Thread.Sleep(500);  
            Exit(0);  
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
                if (!Directory.Exists("Screenshots")) { Directory.CreateDirectory("Screenshots"); }
                string path = string.Format("Screenshots/Screenshot-{0:D2}{1:D2}{2:D2}.png", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                if (!img.SaveToFile(path))
                {
                    MessageBox.Show(path, "Failed to save screenshot");
                    img.Dispose();
                }
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
                if (c_GUI.chatWindow != null)
                {
                    if (c_GUI.inputChat.HasFocus) { return; }

                    if (c_GUI.chatWindow.IsVisible)
                    {
                        c_GUI.chatWindow.Hide();
                    }
                    else
                    {
                        c_GUI.chatWindow.Show();
                    }
                }
                if (c_GUI.d_Window != null)
                {
                    if (c_GUI.inputChat.HasFocus) { return; }

                    if (c_GUI.d_Window.IsVisible)
                    {
                        c_GUI.d_Window.Hide();
                    }
                    else
                    {
                        c_GUI.d_Window.Show();
                    }
                }
                if (c_GUI.menuWindow != null)
                {
                    if (c_GUI.inputChat.HasFocus) { return; }

                    if (c_GUI.menuWindow.IsVisible)
                    {
                        c_GUI.menuWindow.Hide();
                    }
                    else
                    {
                        c_GUI.menuWindow.Show();
                    }
                }
            }

            if (e.Code == Keyboard.Key.M)
            {
                if (c_GUI.menuWindow != null)
                {
                    if (c_GUI.inputChat.HasFocus) { return; }

                    if (c_GUI.menuWindow.IsVisible)
                    {
                        c_GUI.menuWindow.Hide();
                    }
                    else
                    {
                        c_GUI.menuWindow.Show();
                        c_GUI.charTab.Focus();
                    }
                }
            }

            if (e.Code == Keyboard.Key.C)
            {
                if (c_GUI.inputChat.HasFocus) { return; }
                if (c_GUI.chatWindow != null)
                {
                    if (c_GUI.chatWindow.IsVisible)
                    {
                        c_GUI.chatWindow.Hide();
                    }
                    else
                    {
                        c_GUI.chatWindow.Show();
                    }
                }
            }

            if (e.Code == Keyboard.Key.B)
            {
                if (c_GUI.d_Window != null)
                {
                    if (c_GUI.inputChat.HasFocus) { return; }

                    if (c_GUI.d_Window.IsVisible)
                    {
                        c_GUI.d_Window.Hide();
                    }
                    else
                    {
                        c_GUI.d_Window.Show();
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
                if (c_Map.m_MapNpc[i].IsSpawned)
                {
                    if (c_Map.m_MapNpc[i].Sprite > 0)
                    {
                        c_Map.m_MapNpc[i].DrawNpc(c_Window, c_Sprite[(c_Map.m_MapNpc[i].Sprite - 1)]);
                    }                   
                }
            }

            for (int i = 0; i < 20; i++)
            {
                if (c_Map.r_MapNpc[i].IsSpawned)
                {
                    if (c_Map.r_MapNpc[i].Sprite > 0)
                    {
                        c_Map.r_MapNpc[i].DrawNpc(c_Window, c_Sprite[(c_Map.r_MapNpc[i].Sprite - 1)]);
                    }
                }
            }
        }

        void DrawMapItems()
        {
            for (int i = 0; i < 20; i++)
            {
                if (c_Map.mapItem[i].IsSpawned)
                {
                    if (c_Map.mapItem[i].Sprite > 0)
                    {
                        c_Map.mapItem[i].DrawItem(c_Window, c_ItemSprite[c_Map.mapItem[i].Sprite - 1]);
                    }
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
                DrawMapItems();
                DrawNpcs();
                DrawPlayers();
                DrawIndexPlayer();
                DrawProjectiles(c_Client);
                DrawUpperLevelTiles();

                if (TickCount - walkTick > 100)
                {
                    c_Player[handleData.c_Index].CheckMovement(c_Client, handleData.c_Index, c_Window, c_Map, c_GUI);
                    ProcessMovement();
                    walkTick = TickCount;
                }

                if (TickCount - attackTick > 25)
                {
                    c_Player[handleData.c_Index].CheckAttack(c_Client, c_GUI, c_Window, handleData.c_Index);
                    attackTick = TickCount;
                }

                if (TickCount - pickupTick > 100)
                {
                    c_Player[handleData.c_Index].CheckItemPickUp(c_Client, c_GUI, c_Window, handleData.c_Index);
                    pickupTick = TickCount;
                }
            }
            c_Window.SetView(c_Window.DefaultView);
            c_Canvas.RenderCanvas();
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
            c_GUI.UpdateMenuWindow(c_Player[handleData.c_Index]);
        }

        void UpdateOverlay()
        {
            if (c_Player[handleData.c_Index].Name != null && c_Player[handleData.c_Index].mainWeapon.Name != null)
            {
                c_GUI.UpdateHUD(c_Player[handleData.c_Index], c_Window);
            }
        }
    }
}