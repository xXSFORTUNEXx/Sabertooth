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
using static System.Convert;
using static SabertoothClient.Globals;
using KeyEventArgs = SFML.Window.KeyEventArgs;
using MessageBox = System.Windows.Forms.MessageBox;
using System.Data.SQLite;
using System.Diagnostics;
using System.ComponentModel;


namespace SabertoothClient
{
    public static class SabertoothClient
    {
        public static NetClient netClient;

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        #region Configuration
        public static string Username { get; set; }
        public static string Password { get; set; }
        public static bool Remember { get; set; }
        public static string IPAddress { get; set; }
        public static string Port { get; set; }
        public static string CurrentVersion { get; set; }
        public static bool VSync { get; set; }
        public static bool Fullscreen { get; set; }
        public static bool LanConnection { get; set; }
        public static Styles style { get; set; }
        #endregion

        [STAThread]
        static void Main(string[] args)
        {
            var handle = GetConsoleWindow();

            Console.Title = "Sabertooth Console - Debug Info";
            Logging.WriteMessageLog(@"  _____       _               _              _   _     ");
            Logging.WriteMessageLog(@" / ____|     | |             | |            | | | |    ");
            Logging.WriteMessageLog(@"| (___   __ _| |__   ___ _ __| |_ ___   ___ | |_| |__  ");
            Logging.WriteMessageLog(@" \___ \ / _` | '_ \ / _ \ '__| __/ _ \ / _ \| __| '_ \ ");
            Logging.WriteMessageLog(@" ____) | (_| | |_) |  __/ |  | || (_) | (_) | |_| | | |");
            Logging.WriteMessageLog(@"|_____/ \__,_|_.__/ \___|_|   \__\___/ \___/ \__|_| |_|");
            Logging.WriteMessageLog(@"                              Created by Steven Fortune");
            Logging.WriteMessageLog("Initializing client...");

            NetPeerConfiguration netConfig = new NetPeerConfiguration("sabertooth")
            {
                UseMessageRecycling = true,
                MaximumConnections = 1,
                MaximumTransmissionUnit = 1500,
                EnableUPnP = false,
                ConnectionTimeout = CONNECTION_TIMEOUT,
                SimulatedRandomLatency = SIMULATED_RANDOM_LATENCY,
                SimulatedMinimumLatency = SIMULATED_MINIMUM_LATENCY,
                SimulatedLoss = SIMULATED_PACKET_LOSS,
                SimulatedDuplicatesChance = SIMULATED_DUPLICATES_CHANCE
            };

            netConfig.EnableMessageType(NetIncomingMessageType.DiscoveryResponse);
            netConfig.EnableMessageType(NetIncomingMessageType.ConnectionLatencyUpdated);
            netConfig.DisableMessageType(NetIncomingMessageType.DebugMessage);
            netConfig.DisableMessageType(NetIncomingMessageType.Error);
            netConfig.DisableMessageType(NetIncomingMessageType.NatIntroductionSuccess);
            netConfig.DisableMessageType(NetIncomingMessageType.Receipt);
            netConfig.DisableMessageType(NetIncomingMessageType.UnconnectedData);
            netConfig.DisableMessageType(NetIncomingMessageType.VerboseDebugMessage);
            netConfig.DisableMessageType(NetIncomingMessageType.WarningMessage);
            ShowWindow(handle, SW_HIDE);
            Logging.WriteMessageLog("Enabling message types...");
            netClient = new NetClient(netConfig);
            netClient.Start();
            Logging.WriteMessageLog("Network configuration complete...");
            LoadConfiguration();
            Client.GameLoop();
        }

        public static void LoadConfiguration()
        {
            if (!File.Exists("Config.xml"))
            {
                Remember = false;
                IPAddress = "127.0.0.1";
                Port = "14242";
                CurrentVersion = "1.0";
                VSync = false;
                Fullscreen = false;
                LanConnection = true;
                SaveConfiguration();
                CreateMapCache();
                Logging.WriteMessageLog("Config and map cache created!");
            }

            XmlReader reader = XmlReader.Create("Config.xml");
            reader.ReadToFollowing("Username");
            Username = reader.ReadElementContentAsString();
            reader.ReadToFollowing("Password");
            Password = reader.ReadElementContentAsString();
            reader.ReadToFollowing("Remember");
            Remember = reader.ReadElementContentAsBoolean();
            reader.ReadToFollowing("IPAddress");
            IPAddress = reader.ReadElementContentAsString();
            reader.ReadToFollowing("Port");
            Port = reader.ReadElementContentAsString();
            reader.ReadToFollowing("Version");
            CurrentVersion = reader.ReadElementContentAsString();
            reader.ReadToFollowing("VSync");
            VSync = reader.ReadElementContentAsBoolean();
            reader.ReadToFollowing("Fullscreen");
            Fullscreen = reader.ReadElementContentAsBoolean();
            reader.ReadToFollowing("LAN");
            LanConnection = reader.ReadElementContentAsBoolean();
            reader.Close();

            Logging.WriteMessageLog("Configuration file loaded...");

            if (Fullscreen) { style = Styles.Fullscreen; }
            else { style = SCREEN_STYLE; }
        }

        public static void SaveConfiguration()
        {
            XmlWriterSettings configData = new XmlWriterSettings()
            {
                Indent = true
            };
            XmlWriter writer = XmlWriter.Create("Config.xml", configData);
            writer.WriteStartDocument();
            writer.WriteStartElement("Configuration");
            writer.WriteElementString("Username", Username);
            writer.WriteElementString("Password", Password);
            writer.WriteElementString("Remember", ToInt32(Remember).ToString());
            writer.WriteElementString("IPAddress", IPAddress);
            writer.WriteElementString("Port", Port);
            writer.WriteElementString("Version", CurrentVersion);
            writer.WriteElementString("VSync", ToInt32(VSync).ToString());
            writer.WriteElementString("Fullscreen", ToInt32(Fullscreen).ToString());
            writer.WriteElementString("LAN", ToInt32(LanConnection).ToString());
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
            Logging.WriteMessageLog("Configuration Saved!");
        }

        public static void CreateMapCache()
        {
            if (!File.Exists("MapCache.db"))
            {
                using (var conn = new SQLiteConnection("Data Source=MapCache.db;Version=3;"))
                {
                    using (var cmd = new SQLiteCommand(conn))
                    {
                        conn.Open();
                        string sql;
                        sql = "CREATE TABLE MAPS";
                        sql = sql + "(ID INTEGER,NAME TEXT,REVISION INTEGER,TOP INTEGER,BOTTOM INTEGER,LEFT INTEGER,RIGHT INTEGER,BRIGHTNESS INTEGER,MAXX INTEGER,MAXY INTEGER,NPC BLOB,ITEM BLOB,GROUND BLOB,MASK BLOB,MASKA BLOB,FRINGE BLOB,FRINGEA BLOB)";
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }

    public static class Client
    {
        public static RenderWindow renderWindow = new RenderWindow(new VideoMode(SCREEN_WIDTH, SCREEN_HEIGHT), GAME_TITLE, SabertoothClient.style);
        static Gwen.Renderer.SFML gwenRenderer = new Gwen.Renderer.SFML(renderWindow);
        static Gwen.Skin.TexturedBase skin = new Gwen.Skin.TexturedBase(gwenRenderer, "Resources/Skins/DefaultSkin.png");
        static Gwen.Font defaultFont = new Gwen.Font(gwenRenderer, "Resources/Fonts/Tahoma.ttf");
        public static Canvas canvas = new Canvas(skin);    
        public static Gwen.Input.SFML sFML = new Gwen.Input.SFML();  
        public static HUD hud = new HUD();
        public static GUI gui = new GUI();
        public static Player[] players = new Player[MAX_PLAYERS];
        public static Npc[] npcs = new Npc[MAX_NPCS];
        public static Shop[] shops = new Shop[MAX_SHOPS];
        public static Item[] items = new Item[MAX_ITEMS];
        public static Chat[] chats = new Chat[MAX_CHATS];
        public static Quests[] quests = new Quests[MAX_QUESTS];
        public static Chest[] chests = new Chest[MAX_CHESTS];
        public static Map map = new Map();
        public static MiniMap miniMap = new MiniMap();
        public static View view = new View();
        public static WorldTime worldTime = new WorldTime();

        #region Local Variables
        static int lastTick;
        static int lastFrameRate;  
        static int frameRate; 
        static int fps; 
        static int discoverTick;
        //static int timeTick;
        static int walkTick;
        static int attackTick;
        static int pickupTick;
        static int saveTime;
        static int menuTick;
        #endregion

        public static void GameLoop()  
        {
            renderWindow.Closed += new EventHandler(OnClose);
            renderWindow.KeyReleased += window_KeyReleased;
            renderWindow.KeyPressed += OnKeyPressed;
            renderWindow.MouseButtonPressed += window_MouseButtonPressed;
            renderWindow.MouseButtonReleased += window_MouseButtonReleased;
            renderWindow.MouseMoved += window_MouseMoved;
            renderWindow.TextEntered += window_TextEntered;            
            gwenRenderer.LoadFont(defaultFont);
            skin.SetDefaultFont(defaultFont.FaceName);
            defaultFont.Dispose();

            if (SabertoothClient.VSync == true) { renderWindow.SetVerticalSyncEnabled(true); }
            else { renderWindow.SetFramerateLimit(MAX_FPS); }


            canvas.SetSize(CANVAS_WIDTH, CANVAS_HEIGHT);
            canvas.ShouldDrawBackground = true;
            canvas.BackgroundColor = System.Drawing.Color.Transparent;
            canvas.KeyboardInputEnabled = true;
            sFML.Initialize(canvas, renderWindow);
            gui.CreateMainWindow(canvas);

            InitArrays();

            Thread commandThread = new Thread(() => CommandWindow());
            commandThread.Start();

            while (renderWindow.IsOpen)
            {
                CheckForConnection();
                UpdatePlayerTime();
                UpdateView(); 
                DrawGraphics();
                renderWindow.Display();                
            }

            if (SabertoothClient.netClient.ServerConnection != null)
            {                
                players[HandleData.myIndex].SendUpdatePlayerTime();
            }      

            canvas.Dispose(); 
            skin.Dispose();
            gwenRenderer.Dispose();
            SabertoothClient.netClient.Disconnect("shutdown");
            Thread.Sleep(500);
            Exit(0);
        }

        public static void LaunchUpdateClient()
        {
            try
            {
                using (Process sClient = new Process())
                {
                    sClient.StartInfo.UseShellExecute = true;
                    sClient.StartInfo.FileName = "UpdateClient.exe";
                    sClient.Start();
                    Exit(0);
                }
            }
            catch (Exception e)
            {
                Logging.WriteMessageLog(e.Message);
            }
        }

        static void CommandWindow()
        {
            string input;
            while (true)
            {
                Console.Write(">");
                input = Console.ReadLine().ToLower();
                Logging.WriteLog("Command: " + input, "Commands");

                #region Commands
                switch (input)
                {
                    case "cmcache":
                        SabertoothClient.CreateMapCache();
                        Logging.WriteMessageLog("Map cache created successfully!", "Commands");
                        break;

                    case "help":
                        Logging.WriteMessageLog("Commands:", "Commands");
                        Logging.WriteMessageLog("cmcache - create mapcache.db", "Commands");
                        break;

                    default:
                        Logging.WriteMessageLog("Please enter a valid command!", "Commands");
                        break;
                }
                #endregion
            }
        }

        #region Initialize Methods
        static void InitArrays()
        {
            for (int i = 0; i < MAX_PLAYERS; i++)
            {
                players[i] = new Player();                
            }

            for (int i = 0; i < MAX_NPCS; i++)
            {
                npcs[i] = new Npc();
            }

            for (int i = 0; i < MAX_ITEMS; i++)
            {
                items[i] = new Item();
            }

            for (int i = 0; i < MAX_SHOPS; i++)
            {
                shops[i] = new Shop();
            }

            for (int i = 0; i < MAX_CHATS; i++)
            {
                chats[i] = new Chat();
            }

            for (int i = 0; i < MAX_QUESTS; i++)
            {
                quests[i] = new Quests();
            }

            for (int i = 0; i < MAX_CHESTS; i++)
            {
                chests[i] = new Chest();
            }
            Logging.WriteMessageLog("All array successfully created!");
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
            sFML.ProcessMessage(e);
        }

        static void window_MouseMoved(object sender, MouseMoveEventArgs e)
        {
            sFML.ProcessMessage(e);
        }

        static void window_MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            sFML.ProcessMessage(new Gwen.Input.SFMLMouseButtonEventArgs(e, true));
        }

        static void window_MouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            sFML.ProcessMessage(new Gwen.Input.SFMLMouseButtonEventArgs(e, false));
        }

        static void window_KeyReleased(object sender, KeyEventArgs e)
        {
            sFML.ProcessMessage(new Gwen.Input.SFMLKeyEventArgs(e, false));
        }

        static void OnKeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape)
                renderWindow.Close();

            if (e.Code == Keyboard.Key.F12)
            {
                Image img = renderWindow.Capture();
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
                sFML.ProcessMessage(new Gwen.Input.SFMLKeyEventArgs(e, true));
            }

            if (e.Code == Keyboard.Key.Return)
            {
                if (gui.inputChat != null)
                {
                    if (gui.inputChat.HasFocus == false)
                    {
                        gui.chatWindow.Focus();
                        gui.inputChat.Focus();
                    }
                }
            }

            if (e.Code == Keyboard.Key.Tab)
            {
                if (gui.chatWindow != null)
                {
                    if (gui.inputChat.HasFocus) { return; }

                    if (gui.chatWindow.IsVisible)
                    {
                        gui.chatWindow.Hide();
                    }
                    else
                    {
                        gui.chatWindow.Show();
                    }
                }
                if (gui.d_Window != null)
                {
                    if (gui.inputChat.HasFocus) { return; }

                    if (gui.d_Window.IsVisible)
                    {
                        gui.d_Window.Hide();
                    }
                    else
                    {
                        gui.d_Window.Show();
                    }
                }
                if (gui.menuWindow != null)
                {
                    if (gui.inputChat.HasFocus) { return; }

                    if (gui.menuWindow.IsVisible)
                    {
                        gui.menuWindow.Hide();
                    }
                    else
                    {
                        gui.menuWindow.Show();
                    }
                }
            }

            if (e.Code == Keyboard.Key.M)
            {
                if (gui.menuWindow != null)
                {
                    if (gui.inputChat.HasFocus) { return; }

                    if (gui.menuWindow.IsVisible)
                    {
                        gui.menuWindow.Hide();
                        gui.RemoveStatWindow();
                    }
                    else
                    {
                        gui.menuWindow.Show();
                        gui.charTab.Focus();
                    }
                }
            }

            if (e.Code == Keyboard.Key.C)
            {
                if (gui.chatWindow != null)
                {
                    if (gui.inputChat.HasFocus) { return; }

                    if (gui.chatWindow.IsVisible)
                    {
                        gui.chatWindow.Hide();
                    }
                    else
                    {
                        gui.chatWindow.Show();
                    }
                }
            }

            if (e.Code == Keyboard.Key.B)
            {
                if (gui.d_Window != null)
                {
                    if (gui.inputChat.HasFocus) { return; }

                    if (gui.d_Window.IsVisible)
                    {
                        gui.d_Window.Hide();
                    }
                    else
                    {
                        gui.d_Window.Show();
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
        static void DrawPlayers()
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

            for (int i = 0; i < MAX_PLAYERS; i++)
            {
                if (players[i] != null && players[i].Name != "")
                {
                    if (players[i].Map == players[HandleData.myIndex].Map)
                    {
                        if (players[i].X + OFFSET_X > minX && players[i].X + OFFSET_X < maxX)
                        {
                            if (players[i].Y + OFFSET_Y > minY && players[i].Y + OFFSET_Y < maxY)
                            {
                                if (i != HandleData.myIndex)
                                {
                                    renderWindow.Draw(players[i]);
                                }
                            }
                        }
                    }
                }
            }
        }

        static void DrawNpcs()
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

            for (int i = 0; i < MAX_MAP_NPCS; i++)
            {
                if (map.m_MapNpc[i].IsSpawned)
                {
                    if (map.m_MapNpc[i].X > minX && map.m_MapNpc[i].X < maxX)
                    {
                        if (map.m_MapNpc[i].Y > minY && map.m_MapNpc[i].Y < maxY)
                        {
                            if (map.m_MapNpc[i].Sprite > 0)
                            {
                                renderWindow.Draw(map.m_MapNpc[i]);
                            }
                        }
                    }     
                }
            }

            for (int i = 0; i < MAX_MAP_POOL_NPCS; i++)
            {
                if (map.r_MapNpc[i].IsSpawned)
                {
                    if (map.r_MapNpc[i].X > minX && map.r_MapNpc[i].X < maxX)
                    {
                        if (map.r_MapNpc[i].Y > minY && map.r_MapNpc[i].Y < maxY)
                        {
                            if (map.r_MapNpc[i].Sprite > 0)
                            {
                                renderWindow.Draw(map.r_MapNpc[i]);
                            }
                        }
                    }
                }
            }
        }

        static void DrawMapItems()
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

            for (int i = 0; i < MAX_MAP_ITEMS; i++)
            {
                if (map.m_MapItem[i].IsSpawned)
                {
                    if (map.m_MapItem[i].X > minX && map.m_MapItem[i].X < maxX)
                    {
                        if (map.m_MapItem[i].Y > minY && map.m_MapItem[i].Y < maxY)
                        {
                            if (map.m_MapItem[i].Sprite > 0)
                            {
                                renderWindow.Draw(map.m_MapItem[i]);
                            }
                        }
                    }
                }
            }
        }

        static void DrawBlood()
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

            for (int i = 0; i < MAX_BLOOD_SPLATS; i++)
            {
                if (map.m_BloodSplats[i] != null)
                {
                    if (map.m_BloodSplats[i].X > minX && map.m_BloodSplats[i].X < maxX)
                    {
                        if (map.m_BloodSplats[i].X > minX && map.m_BloodSplats[i].X < maxX)
                        {
                            renderWindow.Draw(map.m_BloodSplats[i]);
                        }
                    }
                }
            }
        }

        static void DrawChests()
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
                    if (x > 0 && y > 0 && x < map.MaxX && y < map.MaxY)
                    {
                        if (map.Ground[x, y].Type == (int)TileType.Chest)
                        {
                            
                            map.DrawChest(x, y, isChestEmpty(map.Ground[x, y].ChestNum));
                        }
                    }
                }
            }
        }

        static void DrawIndexPlayer()
        {
            renderWindow.Draw(players[HandleData.myIndex]);
        }

        static void DrawGraphics()
        {
            if (map.Name != null && gui.Ready)
            {
                renderWindow.Draw(map);
                DrawChests();
                DrawMapItems();
                DrawBlood();
                DrawNpcs();
                DrawPlayers();
                DrawIndexPlayer();
                map.DrawFringe(renderWindow);
                if (TickCount - walkTick > 100)
                {
                    players[HandleData.myIndex].CheckMovement();
                    //players[HandleData.myIndex].CheckControllerMovement();
                    players[HandleData.myIndex].CheckChangeDirection();
                    //players[HandleData.myIndex].CheckControllerChangeDirection();
                    players[HandleData.myIndex].CheckPlayerInteraction();
                    //players[HandleData.myIndex].CheckControllerPlayerInteraction();
                    //players[HandleData.myIndex].CheckControllerButtonPress();
                    players[HandleData.myIndex].CheckHotBarKeyPress();
                    players[HandleData.myIndex].CheckDirection(Gwen.Input.InputHandler.MousePosition.X, Gwen.Input.InputHandler.MousePosition.Y);
                    ProcessMovement();
                    walkTick = TickCount;
                }
                if (TickCount - attackTick > 25)
                {
                    players[HandleData.myIndex].CheckAttack();
                    players[HandleData.myIndex].CheckControllerAttack();
                    attackTick = TickCount;
                }
                if (TickCount - pickupTick > 100)
                {
                    players[HandleData.myIndex].CheckItemPickUp();
                    players[HandleData.myIndex].CheckControllerItemPickUp();
                    pickupTick = TickCount;
                }
            }
            renderWindow.SetView(renderWindow.DefaultView);
            if (map.Name != null && gui.Ready)
            {
                map.DrawBrightness();
                hud.UpdateHealthBar();
                hud.UpdateManaBar();
                hud.UpdateExpBar();
                renderWindow.Draw(hud);
                miniMap.UpdateMiniMap();
                renderWindow.Draw(miniMap);
            }
            canvas.RenderCanvas();
        }
        #endregion

        #region Check & Update Methods
        static void UpdatePlayerTime()
        {
            if (SabertoothClient.netClient.ServerConnection == null || players[HandleData.myIndex].Name == null) { return; }

            players[HandleData.myIndex].UpdatePlayerTime();

            if (TickCount - saveTime >= 297000)
            {
                players[HandleData.myIndex].SendUpdatePlayerTime();
                saveTime = TickCount;
            }
        }

        static void ProcessMovement()
        {
            for (int i = 0; i < 5; i++)
            {
                if (players[i].tempStep != 5 && i != HandleData.myIndex)
                {
                    players[i].X = players[i].tempX;
                    players[i].Y = players[i].tempY;
                    players[i].Direction = players[i].tempDir;
                    players[i].AimDirection = players[i].tempaimDir;
                    players[i].Step = players[i].tempStep;

                    players[i].tempStep = 5;
                }
            }
        }

        static void UpdateTitle(int fps)
        {
            string title = GAME_TITLE + " FPS: " + fps + " - Version: " + VERSION;

            if (players[HandleData.myIndex].Name != null) { title += " - Logged: " + players[HandleData.myIndex].Name; }
            if (worldTime.updateTime == true) { title += " - Time: " + worldTime.Time; }

            renderWindow.SetTitle(title);
        }

        static void CheckForConnection()
        {
            if (SabertoothClient.netClient.ServerConnection == null)
            {
                if (TickCount - discoverTick >= DISCOVERY_TIMER)
                {

                    Logging.WriteMessageLog("Connecting to server...");
                    if (SabertoothClient.LanConnection)
                    {
                        int port = ToInt32(SabertoothClient.Port);
                        SabertoothClient.netClient.DiscoverLocalPeers(port);
                    }
                    else
                    {
                        try
                        {
                            NetOutgoingMessage outMSG = SabertoothClient.netClient.CreateMessage();
                            outMSG.Write((byte)PacketTypes.Connection);
                            outMSG.Write("sabertooth");
                            SabertoothClient.netClient.Connect(SabertoothClient.IPAddress, ToInt32(SabertoothClient.Port), outMSG);
                        }
                        catch (Exception e)
                        {
                            Logging.WriteMessageLog(e.Message);
                        }
                    }
                    discoverTick = TickCount;
                }
            }
            //Logging.WriteMessageLog("Status: " + SabertoothClient.netClient.ConnectionStatus.ToString());            
        }

        static void UpdateView()
        {
            view.Reset(new FloatRect(0, 0, SCREEN_WIDTH, SCREEN_HEIGHT));
            view.Move(new Vector2f(players[HandleData.myIndex].X * PIC_X, players[HandleData.myIndex].Y * PIC_Y));
            HandleData.HandleDataMessage();

            gui.SetIndexPlayer();
            hud.SetPlayerIndex();
            miniMap.SetPlayerIndexMap();

            renderWindow.SetActive();
            renderWindow.DispatchEvents();
            renderWindow.Clear();
            //Gl.glClear(Gl.GL_DEPTH_BUFFER_BIT | Gl.GL_COLOR_BUFFER_BIT);

            renderWindow.SetView(view);

            fps = CalculateFrameRate();

            if (TickCount - menuTick > UPDATE_MENU_TIMER)
            {
                if (gui.menuWindow != null && gui.menuWindow.IsVisible) { gui.UpdateMenuWindow(); }
                if (gui.hotBarWindow != null && gui.hotBarWindow.IsVisible) { gui.UpdateHotBar(); }
                if (players[HandleData.myIndex].inShop) { gui.UpdateShopWindow(); }
                if (gui.d_Window != null && gui.d_Window.IsVisible) { gui.UpdateDebugWindow(fps); }
                if (gui.bankWindow != null && gui.bankWindow.IsVisible) { gui.UpdateBankWindow(); }
                if (gui.chestWindow != null && gui.chestWindow.IsVisible) { gui.UpdateChestWindow(); }
                if (worldTime.updateTime == true) { worldTime.UpdateTime(); }
                menuTick = TickCount;
            }

            UpdateTitle(fps);

            Joystick.Update();
        }
        #endregion

        #region Misc Methods
        static bool isChestEmpty(int chestNum)
        {
            for (int i = 0; i < MAX_CHESTS; i++)
            {
                if (chests[chestNum].ChestItem[i].Name != "None")
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
    }
}