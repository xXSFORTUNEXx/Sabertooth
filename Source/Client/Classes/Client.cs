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
            Logging.WriteMessageLog("Enabling message types...");

            ShowWindow(handle, SW_HIDE);    //Show/Hide console window SW_HIDE = hide, SW_SHOW = show
            
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
        public static Animation[] animations = new Animation[MAX_ANIMATIONS];
        public static Spell[] spells = new Spell[MAX_SPELLS];
        public static Map map = new Map();
        public static MiniMap miniMap = new MiniMap();
        public static View view = new View();
        public static WorldTime worldTime = new WorldTime();
        public static Texture curTexture;
        public static Sprite curSprite = new Sprite();

        #region Local Variables
        static int lastTick;
        static int lastFrameRate;  
        static int frameRate; 
        static int fps; 
        static int discoverTick;        
        static int walkTick;
        static int saveTime;
        static int menuTick;
        static bool miniMapVis;
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

            for (int i = 0; i < MAX_ANIMATIONS; i++)
            {
                animations[i] = new Animation();
            }

            for (int i = 0; i < MAX_SPELLS; i++)
            {
                spells[i] = new Spell();
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

            if (gui.Ready)
            {
                if (e.Code == Keyboard.Key.Return)
                {
                    if (gui.inputChat != null)
                    {
                        if (!gui.chatWindow.IsVisible) { gui.chatWindow.Show(); }
                        if (gui.inputChat.HasFocus == false)
                        {
                            gui.chatWindow.Focus();
                            gui.inputChat.Focus();
                        }
                    }
                }

                if (e.Code == Keyboard.Key.T)
                {
                    if (gui.inputChat != null)
                    {
                        if (gui.inputChat.HasFocus) { return; }

                        if (!gui.chatWindow.IsVisible)
                        {
                            gui.chatWindow.Show();
                        }
                        else
                        {
                            gui.chatWindow.Hide();
                        }
                    }
                }

                if (e.Code == Keyboard.Key.M)
                {
                    if (gui.inputChat.HasFocus) { return; }

                    if (miniMapVis)
                    {
                        miniMapVis = false;
                    }
                    else
                    {
                        miniMapVis = true;
                    }
                }

                //Character Tab
                if (e.Code == Keyboard.Key.C)
                {
                    if (gui.menuWindow != null)
                    {
                        if (gui.inputChat.HasFocus) { return; }

                        if (gui.menuWindow.IsVisible)
                        {
                            if (!gui.charTab.IsActive)
                            {
                                gui.charTab.Press();
                            }
                            else
                            {
                                gui.menuWindow.Hide();
                                gui.RemoveStatWindow();
                            }
                        }
                        else
                        {
                            gui.menuWindow.Show();
                            gui.charTab.Press();
                        }
                    }
                }
                //Spell Tab
                if (e.Code == Keyboard.Key.P)
                {
                    if (gui.menuWindow != null)
                    {
                        if (gui.inputChat.HasFocus) { return; }

                        if (gui.menuWindow.IsVisible)
                        {
                            if (!gui.spellsTab.IsActive)
                            {
                                gui.spellsTab.Press();
                            }
                            else
                            {
                                gui.menuWindow.Hide();
                                gui.RemoveSpellStatWindow();

                            }
                        }
                        else
                        {
                            gui.menuWindow.Show();
                            gui.spellsTab.Press();
                        }
                    }
                }
                //Equip Tab
                if (e.Code == Keyboard.Key.J)
                {
                    if (gui.menuWindow != null)
                    {
                        if (gui.inputChat.HasFocus) { return; }

                        if (gui.menuWindow.IsVisible)
                        {
                            if (!gui.equipTab.IsActive)
                            {
                                gui.equipTab.Press();
                            }
                            else
                            {
                                gui.menuWindow.Hide();
                                gui.RemoveStatWindow();

                            }
                        }
                        else
                        {
                            gui.menuWindow.Show();
                            gui.equipTab.Press();
                        }
                    }
                }
                //Pack Tab
                if (e.Code == Keyboard.Key.B)
                {
                    if (gui.menuWindow != null)
                    {
                        if (gui.inputChat.HasFocus) { return; }

                        if (gui.menuWindow.IsVisible)
                        {
                            if (!gui.packTab.IsActive)
                            {
                                gui.packTab.Press();
                            }
                            else
                            {
                                gui.menuWindow.Hide();
                                gui.RemoveStatWindow();

                            }
                        }
                        else
                        {
                            gui.menuWindow.Show();
                            gui.packTab.Press();
                        }
                    }
                }

                //Debug
                if (e.Code == Keyboard.Key.BackSlash)
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

        static void DrawPlayerDisplayText()
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
                                for (int n = 0; n < MAX_DISPLAY_TEXT; n++)
                                {
                                    if (players[i].displayText[n].displayText.DisplayedString != "EMPTY")
                                    {
                                        renderWindow.Draw(players[i].displayText[n]);
                                    }                                        
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
        }

        static void DrawNpcDisplayText()
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
                if (map.m_MapNpc[i].X > minX && map.m_MapNpc[i].X < maxX)
                {
                    if (map.m_MapNpc[i].Y > minY && map.m_MapNpc[i].Y < maxY)
                    {
                        for (int n = 0; n < MAX_DISPLAY_TEXT; n++)
                        {
                            if (map.m_MapNpc[i].dText[n].displayText.DisplayedString != "EMPTY")
                            {
                                renderWindow.Draw(map.m_MapNpc[i].dText[n]);
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

        static void DrawLowerAnimations()
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

            for (int i = 0; i < MAX_MAP_ANIMATIONS; i++)
            {
                if (map.m_Animation[i] != null)
                {
                    if (map.m_Animation[i].RenderBelowTarget.Equals(true))
                    {
                        if (map.m_Animation[i].X > minX && map.m_Animation[i].X < maxX)
                        {
                            if (map.m_Animation[i].X > minX && map.m_Animation[i].X < maxX)
                            {
                                renderWindow.Draw(map.m_Animation[i]);
                            }
                        }
                    }
                }
            }
        }

        static void DrawUpperAnimations()
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

            for (int i = 0; i < MAX_MAP_ANIMATIONS; i++)
            {
                if (map.m_Animation[i] != null)
                {
                    if (map.m_Animation[i].RenderBelowTarget.Equals(false))
                    {
                        if (map.m_Animation[i].X > minX && map.m_Animation[i].X < maxX)
                        {
                            if (map.m_Animation[i].X > minX && map.m_Animation[i].X < maxX)
                            {
                                renderWindow.Draw(map.m_Animation[i]);
                            }
                        }
                    }
                }
            }
        }

        static void DrawIndexPlayer()
        {
            renderWindow.Draw(players[HandleData.myIndex]);
        }

        static void DrawCursor()
        {            
            int curX = Gwen.Input.InputHandler.MousePosition.X;
            int curY = Gwen.Input.InputHandler.MousePosition.Y;
            curSprite.Position = new Vector2f(curX, curY);
            Player player = players[HandleData.myIndex];

            int mX = (curX / PIC_X) + (player.X);
            int mY = (curY / PIC_Y) + (player.Y);
            int target = -1;
            bool isNPC = false;

            for (int i = 0; i < MAX_MAP_NPCS; i++)
            {
                if (map.m_MapNpc[i].IsSpawned)
                {
                    if (map.m_MapNpc[i].X == mX && map.m_MapNpc[i].Y == mY)
                    {
                        switch (map.m_MapNpc[i].Behavior)
                        {
                            case (int)BehaviorType.Aggressive:
                                curTexture = new Texture("Resources/attack.png");
                                break;

                            case (int)BehaviorType.Passive:
                                curTexture = new Texture("Resources/talk.png");
                                break;
                        }
                        target = i;
                        isNPC = true;
                        break;
                    }
                }
            }

            if (!isNPC)
            {
                curTexture = new Texture("Resources/cursor.png");
            }

            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                if (target > -1)
                {
                    player.Target = target;
                }
            }

            if (Mouse.IsButtonPressed(Mouse.Button.Right))
            {
                if (target > -1)
                {
                    int pX = 0;
                    int pY = 0;
                    double dX = 0;
                    double dY = 0;
                    double dFinal = 0;
                    double dPoint = 0;

                    switch (map.m_MapNpc[target].Behavior)
                    {
                        case (int)BehaviorType.Passive:
                            pX = player.X + OFFSET_X;
                            pY = player.Y + OFFSET_Y;
                            dX = pX - map.m_MapNpc[target].X;
                            dY = pY - map.m_MapNpc[target].Y;
                            dFinal = dX * dX + dY * dY;
                            dPoint = Math.Sqrt(dFinal);

                            if (dPoint < 6)
                            {
                                player.SendInteraction(target, 0);
                                player.Target = target;
                            }
                            break;

                        case (int)BehaviorType.Aggressive:
                            player.Target = target;
                            player.Attacking = true;
                            break;
                    }
                }
            }
            
            curSprite.Texture = curTexture;
            renderWindow.Draw(curSprite);
        }

        static void DrawGraphics()
        {
            if (map.Name != null && gui.Ready) //empty map but also check for the GUI to make sure its ready
            {
                renderWindow.Draw(map); //draw the maps ground and mask layers
                DrawChests();   //draw chests ontop of those                
                DrawBlood();    //draw blood from combat (maybe switch with item?)    
                DrawLowerAnimations();   //draw the animations lower layer so its set to 1
                DrawNpcs(); //draw the npcs                
                DrawPlayers();  //now the other players in the world                
                DrawIndexPlayer();  //our main player of the current client instance    
                DrawUpperAnimations();
                map.DrawFringe(renderWindow);   //draw the final layer of tiles over everything else

                //draw combat test
                DrawNpcDisplayText();
                DrawPlayerDisplayText();

                //Process actual movement in any direction
                players[HandleData.myIndex].CheckMovement();                                                                                                                                                      
                ProcessMovement();

                //Check and see if the player is changing directions (have quite a few ways to accomplish this, which is best?)
                //players[HandleData.myIndex].CheckChangeDirection();
                players[HandleData.myIndex].CheckDirection(Gwen.Input.InputHandler.MousePosition.X, Gwen.Input.InputHandler.MousePosition.Y);

                //Lets check for specific input (maybe put hotkey before all events related to input?)
                players[HandleData.myIndex].CheckHotBarKeyPress();
                //players[HandleData.myIndex].CheckPlayerInteraction();
                if (players[HandleData.myIndex].Target > 0)
                {
                    players[HandleData.myIndex].MeleeCombatLoop(map.m_MapNpc[players[HandleData.myIndex].Target]);
                }
                if (players[HandleData.myIndex].CurrentSpell > -1)
                {
                    players[HandleData.myIndex].SpellCastLoop(spells[players[HandleData.myIndex].CurrentSpell]);
                }
                //players[HandleData.myIndex].CheckAttack();                
                players[HandleData.myIndex].CheckForTabTarget();
            }

            renderWindow.SetView(renderWindow.DefaultView); //set the view for the window to default (this is so the UI doesnt move when the characters view does)

            if (map.Name != null && gui.Ready)  //empty map but also check for the GUI to make sure its ready
            {
                map.DrawBrightness();   //draw the brightness from the enviro and the players
                hud.UpdateHealthBar();  //update the on screen health bar
                hud.UpdateManaBar();    //update the on screen mana bar
                hud.UpdateExpBar(); //update the on screen xp bar
                renderWindow.Draw(hud); //draw all the hud from above with its updates
                if (miniMapVis) { miniMap.UpdateMiniMap(); renderWindow.Draw(miniMap); }    //update the minimap                
                DrawCursor();
            }

            canvas.RenderCanvas();  //render the canvas for the GUI WAAAAY WAAAAY up there            
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
            if (TickCount - walkTick < 100) { return; }

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
            walkTick = TickCount;
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

            //Joystick.Update();
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

    public class DisplayText : Drawable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Text displayText = new Text();
        Font font = new Font("Resources/Fonts/Arial.ttf");
        int currentLoop = -1;
        int loopTick = 0;
        int offsetX = 0;

        public DisplayText()
        {
            displayText.Font = font;
            displayText.Color = Color.Red;
            displayText.CharacterSize = 14;
            displayText.Style = Text.Styles.Regular;
            displayText.DisplayedString = "EMPTY";
        }

        public void CreateDisplayText(int vital, int x, int y, int color, string start = "", string end = "")
        {
            Random rnd = new Random();
            X = x;
            Y = y;
            currentLoop = 0;
            loopTick = TickCount;
            if (vital == 0) { displayText.DisplayedString = start + end; }
            else { displayText.DisplayedString = start + vital + end; }
            offsetX = rnd.Next(-8, 16);

            switch (color)
            {
                case (int)DisplayTextMsg.Normal:
                    displayText.Color = Color.White;
                    break;

                case (int)DisplayTextMsg.Warning:
                    displayText.Color = Color.Blue;
                    break;

                case (int)DisplayTextMsg.Damage:
                    displayText.Color = Color.Red;
                    break;

                case (int)DisplayTextMsg.Healing:
                    displayText.Color = Color.Green;
                    break;
            }
        }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            if (TickCount - loopTick > 500)
            {
                if (currentLoop > -1)
                {
                    currentLoop += 1;
                    loopTick = TickCount;
                }
            }

            int locX = (X * PIC_X) + offsetX;
            int locY = ((Y * PIC_Y) - 12) - (10 * currentLoop);
            displayText.Position = new Vector2f(locX, locY);
            target.Draw(displayText);

            if (currentLoop == 3)
            {
                displayText.DisplayedString = "EMPTY";
                currentLoop = -1;
                loopTick = 0;
            }
        }
    }

    public enum DisplayTextMsg : int
    {
        Normal,
        Warning,
        Damage,
        Healing
    }
}