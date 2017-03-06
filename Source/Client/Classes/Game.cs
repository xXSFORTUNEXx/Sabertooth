#undef DEBUG
using Gwen.Control;
using Lidgren.Network;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Xml;
using static System.Environment;
using KeyEventArgs = SFML.Window.KeyEventArgs;
using MessageBox = System.Windows.Forms.MessageBox;
using System.Data.SQLite;

namespace Client.Classes
{
    class StartUp
    {
        static NetClient c_Client;
        static NetPeerConfiguration c_Config;

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        [STAThread]
        static void Main(string[] args)
        {
            var handle = GetConsoleWindow();
            Console.Title = "Sabertooth Console - Debug Info";
            Console.WriteLine(@"  _____       _               _              _   _     ");
            Console.WriteLine(@" / ____|     | |             | |            | | | |    ");
            Console.WriteLine(@"| (___   __ _| |__   ___ _ __| |_ ___   ___ | |_| |__  ");
            Console.WriteLine(@" \___ \ / _` | '_ \ / _ \ '__| __/ _ \ / _ \| __| '_ \ ");
            Console.WriteLine(@" ____) | (_| | |_) |  __/ |  | || (_) | (_) | |_| | | |");
            Console.WriteLine(@"|_____/ \__,_|_.__/ \___|_|   \__\___/ \___/ \__|_| |_|");
            Console.WriteLine(@"                              Created by Steven Fortune");
            Console.WriteLine("Initializing client...");
            ClientConfig cConfig = new ClientConfig();
            c_Config = new NetPeerConfiguration("sabertooth");
            c_Config.EnableMessageType(NetIncomingMessageType.DiscoveryResponse);
            c_Config.EnableMessageType(NetIncomingMessageType.ConnectionLatencyUpdated);
            c_Config.DisableMessageType(NetIncomingMessageType.DebugMessage);
            c_Config.DisableMessageType(NetIncomingMessageType.Error);
            c_Config.DisableMessageType(NetIncomingMessageType.NatIntroductionSuccess);
            c_Config.DisableMessageType(NetIncomingMessageType.Receipt);
            c_Config.DisableMessageType(NetIncomingMessageType.UnconnectedData);
            c_Config.DisableMessageType(NetIncomingMessageType.VerboseDebugMessage);
            c_Config.DisableMessageType(NetIncomingMessageType.WarningMessage);
            c_Config.UseMessageRecycling = true;
            c_Config.MaximumConnections = 1;
            c_Config.MaximumTransmissionUnit = 1500;
            c_Config.ConnectionTimeout = 25.0f;
            //c_Config.SimulatedMinimumLatency = 0.065f;
            //c_Config.SimulatedRandomLatency = 0.085f;
            //c_Config.SimulatedLoss = 0.5f;
            //c_Config.SimulatedDuplicatesChance = 0.1f;
            Console.WriteLine("Enabling message types...");
            c_Client = new NetClient(c_Config);
            Console.WriteLine("Loading config...");
            c_Client.Start();
            Console.WriteLine("Client started...");
            Game c_Game = new Game();
            #if DEBUG
            ShowWindow(handle, SW_SHOW);
            #else
            ShowWindow(handle, SW_HIDE);
            #endif
            c_Game.GameLoop(c_Client, cConfig);
        }
    }

    class ClientConfig
    {
        public string savedUser { get; set; }
        public string savedPass { get; set; }
        public string saveCreds { get; set; }
        public string ipAddress { get; set; }
        public string port { get; set; }
        public string version { get; set; }

        public ClientConfig()
        {
            CheckIfConfigExists();
        }

        public void CheckIfConfigExists()
        {
            if (!File.Exists("Config.xml"))
            {
                saveCreds = "0";
                ipAddress = "127.0.0.1";
                port = "14242";
                version = "1.0";
                SaveConfig();
            }
            LoadConfig();
            CreateMapCache();
        }

        public void LoadConfig()
        {
            XmlReader reader = XmlReader.Create("Config.xml");
            reader.ReadToFollowing("savedUser");
            savedUser = reader.ReadElementContentAsString();
            reader.ReadToFollowing("savedPass");
            savedPass = reader.ReadElementContentAsString();
            reader.ReadToFollowing("saveCreds");
            saveCreds = reader.ReadElementContentAsString();
            reader.ReadToFollowing("ipAddress");
            ipAddress = reader.ReadElementContentAsString();
            reader.ReadToFollowing("port");
            port = reader.ReadElementContentAsString();
            reader.ReadToFollowing("version");
            version = reader.ReadElementContentAsString();
            reader.Close();
        }

        public void SaveConfig()
        {
            XmlWriterSettings configData = new XmlWriterSettings();
            configData.Indent = true;
            XmlWriter writer = XmlWriter.Create("Config.xml", configData);
            writer.WriteStartDocument();
            writer.WriteStartElement("Configuration");
            writer.WriteElementString("savedUser", savedUser);
            writer.WriteElementString("savedPass", savedPass);
            writer.WriteElementString("saveCreds", saveCreds);
            writer.WriteElementString("ipAddress", ipAddress);
            writer.WriteElementString("port", port);
            writer.WriteElementString("version", version);
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }

        public void CreateMapCache()
        {
            if (!Directory.Exists("Cache")) { Directory.CreateDirectory("Cache"); }

            if (!File.Exists("Cache/MapCache.db"))
            {
                using (var conn = new SQLiteConnection("Data Source=Cache/MapCache.db;Version=3;"))
                {
                    using (var cmd = new SQLiteCommand(conn))
                    {
                        conn.Open();
                        string sql;
                        sql = "CREATE TABLE `MAPS`";
                        sql = sql + "(`NAME` TEXT,`REVISION` INTEGER,`TOP` INTEGER,`BOTTOM` INTEGER,`LEFT` INTEGER,`RIGHT` INTEGER,`GROUND` BLOB,`MASK` BLOB,`MASKA` BLOB,`FRINGE` BLOB,`FRINGEA` BLOB)";
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }

    class Game
    {
        static RenderWindow c_Window;
        static Canvas c_Canvas;    
        static Gwen.Input.SFML c_Input;  
        static GUI c_GUI;
        HUD p_HUD = new HUD();
        HandleData handleData; 
        Player[] c_Player = new Player[5]; 
        Npc[] c_Npc = new Npc[10];
        Shop[] c_Shop = new Shop[10];
        Item[] c_Item = new Item[50];   
        Projectile[] c_Proj = new Projectile[10];
        Chat[] c_Chat = new Chat[15];
        Chest[] c_Chest = new Chest[10];
        Map c_Map = new Map();
        MiniMap m_Map = new MiniMap();
        View c_View = new View();
        WorldTime g_GameTime;
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
            c_Window = new RenderWindow(new VideoMode(800, 600), "Sabertooth", Styles.Close);
            c_Window.Closed += new EventHandler(OnClose);
            c_Window.KeyReleased += window_KeyReleased;
            c_Window.KeyPressed += OnKeyPressed;
            c_Window.MouseButtonPressed += window_MouseButtonPressed;
            c_Window.MouseButtonReleased += window_MouseButtonReleased;
            c_Window.MouseMoved += window_MouseMoved;
            c_Window.TextEntered += window_TextEntered;
            c_Window.SetFramerateLimit(60);
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
            c_GUI = new GUI(c_Client, c_Canvas, defaultFont, gwenRenderer, c_Player, c_Config, c_Shop, c_Item, c_Chat, c_Chest);
            c_GUI.CreateMainWindow(c_Canvas);

            handleData = new HandleData();
            g_GameTime = new WorldTime();

            InitArrays();

            while (c_Window.IsOpen)
            {
                CheckForConnection(c_Client);
                UpdateView(c_Client, c_Config, c_Npc, c_Item, c_Shop, c_Chest, g_GameTime); 
                DrawGraphics(c_Client, c_Player);
                c_Window.Display();                
            }

            if (c_Client.ServerConnection != null) { c_Player[handleData.c_Index].SendUpdateClip(c_Client, handleData.c_Index); }      

            c_Canvas.Dispose(); 
            skin.Dispose();
            gwenRenderer.Dispose();
            c_Client.Disconnect("bye");
            Thread.Sleep(500);
            Exit(0);
        }

        #region Initialize Methods
        private void InitArrays()
        {
            for (int i = 0; i < 5; i++)
            {
                c_Player[i] = new Player();
            }

            for (int i = 0; i < 10; i++)
            {
                c_Npc[i] = new Npc();
            }

            for (int i = 0; i < 50; i++)
            {
                c_Item[i] = new Item();
            }

            for (int i = 0; i < 10; i++)
            {
                c_Proj[i] = new Projectile();
            }

            for (int i = 0; i < 10; i++)
            {
                c_Shop[i] = new Shop();
            }

            for (int i = 0; i < 15; i++)
            {
                c_Chat[i] = new Chat();
            }

            for (int i = 0; i < 10; i++)
            {
                c_Chest[i] = new Chest();
            }
        }
        #endregion

        #region Events
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
                //Image img = c_Window.Capture();
                //if (img.Pixels == null)
                //{
                //    MessageBox.Show("Failed to capture window");
                //}
                //if (!Directory.Exists("Screenshots")) { Directory.CreateDirectory("Screenshots"); }
                //string path = string.Format("Screenshots/Screenshot-{0:D2}{1:D2}{2:D2}.png", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                //if (!img.SaveToFile(path))
                //{
                //    MessageBox.Show(path, "Failed to save screenshot");
                //    img.Dispose();
                //}
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
        #endregion

        #region Draw Methods
        void DrawPlayers()
        {
            for (int i = 0; i < 5; i++)
            {
                if (c_Player[i].Name != "")
                {
                    if (i != handleData.c_Index && c_Player[i].Map == c_Player[handleData.c_Index].Map)
                    {
                        c_Window.Draw(c_Player[i]);
                    }
                }
            }
        }

        void DrawNpcs(Player c_Player)
        {
            int minX = (c_Player.X + 12) - 12;
            int minY = (c_Player.Y + 9) - 9;
            int maxX = (c_Player.X + 12) + 13;
            int maxY = (c_Player.Y + 9) + 11;

            for (int i = 0; i < 10; i++)
            {
                if (c_Map.m_MapNpc[i].IsSpawned)
                {
                    if (c_Map.m_MapNpc[i].X > minX && c_Map.m_MapNpc[i].X < maxX)
                    {
                        if (c_Map.m_MapNpc[i].Y > minY && c_Map.m_MapNpc[i].Y < maxY)
                        {
                            if (c_Map.m_MapNpc[i].Sprite > 0)
                            {
                                c_Window.Draw(c_Map.m_MapNpc[i]);
                            }
                        }
                    }     
                }
            }

            for (int i = 0; i < 20; i++)
            {
                if (c_Map.r_MapNpc[i].IsSpawned)
                {
                    if (c_Map.r_MapNpc[i].X > minX && c_Map.r_MapNpc[i].X < maxX)
                    {
                        if (c_Map.r_MapNpc[i].Y > minY && c_Map.r_MapNpc[i].Y < maxY)
                        {
                            if (c_Map.r_MapNpc[i].Sprite > 0)
                            {
                                c_Window.Draw(c_Map.r_MapNpc[i]);
                            }
                        }
                    }
                }
            }
        }

        void DrawMapItems(Player c_Player)
        {
            int minX = (c_Player.X + 12) - 12;
            int minY = (c_Player.Y + 9) - 9;
            int maxX = (c_Player.X + 12) + 13;
            int maxY = (c_Player.Y + 9) + 11;

            for (int i = 0; i < 20; i++)
            {
                if (c_Map.m_MapItem[i].IsSpawned)
                {
                    if (c_Map.m_MapItem[i].X > minX && c_Map.m_MapItem[i].X < maxX)
                    {
                        if (c_Map.m_MapItem[i].Y > minY && c_Map.m_MapItem[i].Y < maxY)
                        {
                            if (c_Map.m_MapItem[i].Sprite > 0)
                            {
                                c_Window.Draw(c_Map.m_MapItem[i]);
                            }
                        }
                    }
                }
            }
        }

        void DrawChests(Player c_Player)
        {
            int minX = (c_Player.X + 12) - 12;
            int minY = (c_Player.Y + 9) - 9;
            int maxX = (c_Player.X + 12) + 13;
            int maxY = (c_Player.Y + 9) + 11;

            for (int x = minX; x < maxX; x++)
            {
                for (int y = minY; y < maxY; y++)
                {
                    if (x > 0 && y > 0 && x < 50 && y < 50)
                    {
                        if (c_Map.Ground[x, y].Type == (int)TileType.Chest)
                        {
                            
                            c_Map.DrawChest(c_Window, x, y, isChestEmpty(c_Map.Ground[x, y].ChestNum));
                        }
                    }
                }
            }
        }

        void DrawProjectiles(NetClient c_Client, Player c_Player)
        {
            int minX = (c_Player.X + 12) - 12;
            int minY = (c_Player.Y + 9) - 9;
            int maxX = (c_Player.X + 12) + 13;
            int maxY = (c_Player.Y + 9) + 11;

            for (int i = 0; i < 200; i++)
            {
                if (c_Map.m_MapProj[i] != null)
                {
                    if (c_Map.m_MapProj[i].X > minX && c_Map.m_MapProj[i].X < maxX)
                    {
                        if (c_Map.m_MapProj[i].Y > minY && c_Map.m_MapProj[i].Y < maxY)
                        {
                            c_Window.Draw(c_Map.m_MapProj[i]);
                        }
                    }
                    c_Map.m_MapProj[i].CheckMovment(c_Client, c_Window, c_Map, i);
                }
            }
        }

        void DrawIndexPlayer()
        {
            c_Window.Draw(c_Player[handleData.c_Index]);
        }

        void DrawGraphics(NetClient c_Client, Player[] c_Player)
        {
            if (c_Map.Name != null && c_GUI.Ready)
            {
                c_Map.UpdateMapPlayer(c_Player[handleData.c_Index]);
                c_Window.Draw(c_Map);
                DrawChests(c_Player[handleData.c_Index]);
                DrawMapItems(c_Player[handleData.c_Index]);
                DrawNpcs(c_Player[handleData.c_Index]);
                DrawPlayers();
                DrawIndexPlayer();
                DrawProjectiles(c_Client, c_Player[handleData.c_Index]);
                c_Map.DrawFringe(c_Window);
                if (TickCount - walkTick > 100)
                {
                    this.c_Player[handleData.c_Index].CheckMovement(c_Client, handleData.c_Index, c_Window, c_Map, c_GUI);
                    this.c_Player[handleData.c_Index].CheckControllerMovement(c_Client, c_Window, c_Map, c_GUI, handleData.c_Index);
                    this.c_Player[handleData.c_Index].CheckChangeDirection(c_Client, c_GUI, c_Window, handleData.c_Index);
                    this.c_Player[handleData.c_Index].CheckControllerChangeDirection(c_Client, c_GUI, c_Window, handleData.c_Index);
                    this.c_Player[handleData.c_Index].CheckReload(c_Client, handleData.c_Index);
                    this.c_Player[handleData.c_Index].CheckControllerReload(c_Client, handleData.c_Index);
                    this.c_Player[handleData.c_Index].CheckPlayerInteraction(c_Client, c_GUI, c_Window, c_Map, handleData.c_Index);
                    this.c_Player[handleData.c_Index].CheckControllerPlayerInteraction(c_Client, c_GUI, c_Window, c_Map, handleData.c_Index);
                    ProcessMovement();
                    walkTick = TickCount;
                }
                if (TickCount - attackTick > 25)
                {
                    this.c_Player[handleData.c_Index].CheckAttack(c_Client, c_GUI, c_Window, handleData.c_Index);
                    this.c_Player[handleData.c_Index].CheckControllerAttack(c_Client, c_GUI, c_Window, handleData.c_Index);
                    attackTick = TickCount;
                }
                if (TickCount - pickupTick > 100)
                {
                    this.c_Player[handleData.c_Index].CheckItemPickUp(c_Client, c_GUI, c_Window, handleData.c_Index);
                    this.c_Player[handleData.c_Index].CheckControllerItemPickUp(c_Client, c_GUI, c_Window, handleData.c_Index);
                    pickupTick = TickCount;
                }
            }
            c_Window.SetView(c_Window.DefaultView);
            if (c_Map.Name != null && c_GUI.Ready)
            {
                c_Map.DrawBrightness(c_Window, c_Player, g_GameTime, handleData.c_Index);
                p_HUD.UpdateHealthBar(c_Player[handleData.c_Index]);
                p_HUD.UpdateExpBar(c_Player[handleData.c_Index]);
                p_HUD.UpdateClipBar(c_Player[handleData.c_Index]);
                p_HUD.UpdateHungerBar(c_Player[handleData.c_Index]);
                p_HUD.UpdateHydrationBar(c_Player[handleData.c_Index]);
                c_Window.Draw(p_HUD);
                m_Map.UpdateMiniMap(c_Player[handleData.c_Index], c_Map);
                c_Map.UpdateMapPlayer(c_Player[handleData.c_Index]);
                c_Window.Draw(m_Map);
            }
            c_Canvas.RenderCanvas();
        }
        #endregion

        #region Check & Update Methods
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
            string title = "Sabertooth - FPS: " + fps;

            if (c_Player[handleData.c_Index].Name != null) { title += " - Logged: " + c_Player[handleData.c_Index].Name; }
            if (g_GameTime.updateTime == true) { title += " - Time: " + g_GameTime.Time; }

            c_Window.SetTitle(title);
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
            Console.WriteLine("Status: " + c_Client.ConnectionStatus.ToString());            
        }

        void UpdateView(NetClient c_Client, ClientConfig c_Config, Npc[] c_Npc, Item[] c_Item, Shop[] c_Shop, Chest[] c_Chest, WorldTime g_GameTime)
        {
            c_View.Reset(new FloatRect(0, 0, 800, 600));
            c_View.Move(new Vector2f(c_Player[handleData.c_Index].X * 32, c_Player[handleData.c_Index].Y * 32));
            handleData.DataMessage(c_Client, c_Canvas, c_GUI, c_Player, c_Map, c_Config, c_Npc, c_Item, c_Proj, c_Shop, c_Chat, c_Chest, g_GameTime); 

            c_Window.SetActive();
            c_Window.DispatchEvents();
            c_Window.Clear();
            //Gl.glClear(Gl.GL_DEPTH_BUFFER_BIT | Gl.GL_COLOR_BUFFER_BIT);

            c_Window.SetView(c_View);

            fps = CalculateFrameRate();

            if (c_GUI.menuWindow != null && c_GUI.menuWindow.IsVisible) { c_GUI.UpdateMenuWindow(c_Player[handleData.c_Index]); }
            if (c_Player[handleData.c_Index].inShop) { c_GUI.UpdateShopWindow(c_Shop[c_Player[handleData.c_Index].shopNum]); }
            if (c_GUI.d_Window != null && c_GUI.d_Window.IsVisible) { c_GUI.UpdateDebugWindow(fps, c_Player, handleData.c_Index); }
            if (c_GUI.bankWindow != null && c_GUI.bankWindow.IsVisible) { c_GUI.UpdateBankWindow(c_Player[handleData.c_Index]); }
            if (c_GUI.chestWindow != null && c_GUI.chestWindow.IsVisible) { c_GUI.UpdateChestWindow(c_Chest[c_Player[handleData.c_Index].chestNum]); }
            if (g_GameTime.updateTime == true) { g_GameTime.UpdateTime(); }

            UpdateTitle(fps);

            Joystick.Update();
        }
        #endregion

        #region Misc Methods
        bool isChestEmpty(int chestNum)
        {
            for (int i = 0; i < 10; i++)
            {
                if (c_Chest[chestNum].ChestItem[i].Name != "None")
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
    }
}