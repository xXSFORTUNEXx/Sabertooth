﻿#define DEBUG
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

namespace SabertoothClient
{
    public static class SabertoothClient
    {
        public static NetClient netClient;

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

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

            NetPeerConfiguration netConfig = new NetPeerConfiguration("sabertooth")
            {
                UseMessageRecycling = true,
                MaximumConnections = 1,
                MaximumTransmissionUnit = 1500,
                EnableUPnP = false,
                ConnectionTimeout = Globals.CONNECTION_TIMEOUT,
                SimulatedRandomLatency = Globals.SIMULATED_RANDOM_LATENCY,
                SimulatedMinimumLatency = Globals.SIMULATED_MINIMUM_LATENCY,
                SimulatedLoss = Globals.SIMULATED_PACKET_LOSS,
                SimulatedDuplicatesChance = Globals.SIMULATED_DUPLICATES_CHANCE
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


            Console.WriteLine("Enabling message types...");
            netClient = new NetClient(netConfig);
            netClient.Start();
            Console.WriteLine("Network configuration complete...");
            /*#if DEBUG
            ShowWindow(handle, Globals.SW_SHOW);
            #else
            ShowWindow(handle, Globals.SW_HIDE);
            #endif*/
            Client.GameLoop();
        }
    }

    public class ClientConfig
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Remember { get; set; }
        public string IPAddress { get; set; }
        public string Port { get; set; }
        public string Version { get; set; }

        public ClientConfig()
        {
            CheckIfConfigExists();
        }

        public void CheckIfConfigExists()
        {
            if (!File.Exists("Config.xml"))
            {
                Remember = "0";
                IPAddress = "127.0.0.1";
                Port = "14242";
                Version = "1.0";
                SaveConfig();
            }
            LoadConfig();
            CreateMapCache();
        }

        public void LoadConfig()
        {
            XmlReader reader = XmlReader.Create("Config.xml");
            reader.ReadToFollowing("Username");
            Username = reader.ReadElementContentAsString();
            reader.ReadToFollowing("Password");
            Password = reader.ReadElementContentAsString();
            reader.ReadToFollowing("Remember");
            Remember = reader.ReadElementContentAsString();
            reader.ReadToFollowing("IPAddress");
            IPAddress = reader.ReadElementContentAsString();
            reader.ReadToFollowing("Port");
            Port = reader.ReadElementContentAsString();
            reader.ReadToFollowing("Version");
            Version = reader.ReadElementContentAsString();
            reader.Close();
        }

        public void SaveConfig()
        {
            XmlWriterSettings configData = new XmlWriterSettings();
            configData.Indent = true;
            XmlWriter writer = XmlWriter.Create("Config.xml", configData);
            writer.WriteStartDocument();
            writer.WriteStartElement("Configuration");
            writer.WriteElementString("Username", Username);
            writer.WriteElementString("Password", Password);
            writer.WriteElementString("Remember", Remember);
            writer.WriteElementString("IPAddress", IPAddress);
            writer.WriteElementString("Port", Port);
            writer.WriteElementString("Version", Version);
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

    public static class Client
    {
        public static RenderWindow renderWindow;
        public static Canvas canvas;    
        public static Gwen.Input.SFML sFML = new Gwen.Input.SFML();  
        public static GUI gui = new GUI();
        public static HUD hud = new HUD();
        public static Player[] players = new Player[Globals.MAX_PLAYERS];
        public static Npc[] npcs = new Npc[Globals.MAX_NPCS];
        public static Shop[] shops = new Shop[Globals.MAX_SHOPS];
        public static Item[] items = new Item[Globals.MAX_ITEMS];
        public static Projectile[] projectiles = new Projectile[Globals.MAX_PROJECTILES];
        public static Chat[] chats = new Chat[Globals.MAX_CHATS];
        public static Chest[] chests = new Chest[Globals.MAX_CHESTS];
        public static Map map = new Map();
        public static MiniMap miniMap = new MiniMap();
        public static View view = new View();
        public static WorldTime worldTime = new WorldTime();
        public static ClientConfig clientConfig = new ClientConfig();
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

        public static void GameLoop()  
        {
            renderWindow = new RenderWindow(new VideoMode(Globals.SCREEN_WIDTH, Globals.SCREEN_HEIGHT), Globals.GAME_TITLE, Styles.Close);
            renderWindow.Closed += new EventHandler(OnClose);
            renderWindow.KeyReleased += window_KeyReleased;
            renderWindow.KeyPressed += OnKeyPressed;
            renderWindow.MouseButtonPressed += window_MouseButtonPressed;
            renderWindow.MouseButtonReleased += window_MouseButtonReleased;
            renderWindow.MouseMoved += window_MouseMoved;
            renderWindow.TextEntered += window_TextEntered;
            renderWindow.SetFramerateLimit(Globals.MAX_FPS);
            Gwen.Renderer.SFML gwenRenderer = new Gwen.Renderer.SFML(renderWindow);
            Gwen.Skin.TexturedBase skin = new Gwen.Skin.TexturedBase(gwenRenderer, "Resources/Skins/DefaultSkin.png");

            Gwen.Font defaultFont = new Gwen.Font(gwenRenderer, "Resources/Fonts/Tahoma.ttf");
            gwenRenderer.LoadFont(defaultFont);
            skin.SetDefaultFont(defaultFont.FaceName);
            defaultFont.Dispose();

            canvas = new Canvas(skin);
            canvas.SetSize(Globals.SCREEN_WIDTH, Globals.SCREEN_HEIGHT);
            canvas.ShouldDrawBackground = true;
            canvas.BackgroundColor = System.Drawing.Color.Transparent;
            canvas.KeyboardInputEnabled = true;
            sFML.Initialize(canvas, renderWindow);
            gui.CreateMainWindow(canvas);

            InitArrays();

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
                players[HandleData.myIndex].SendUpdateClip();
                players[HandleData.myIndex].SendUpdatePlayerTime();
                players[HandleData.myIndex].SendUpdateLifeTime();
            }      

            canvas.Dispose(); 
            skin.Dispose();
            gwenRenderer.Dispose();
            SabertoothClient.netClient.Disconnect("shutdown");
            Thread.Sleep(500);
            Exit(0);
        }

        #region Initialize Methods
        static void InitArrays()
        {
            for (int i = 0; i < Globals.MAX_PLAYERS; i++)
            {
                players[i] = new Player();
            }

            for (int i = 0; i < Globals.MAX_NPCS; i++)
            {
                npcs[i] = new Npc();
            }

            for (int i = 0; i < Globals.MAX_ITEMS; i++)
            {
                items[i] = new Item();
            }

            for (int i = 0; i < Globals.MAX_PROJECTILES; i++)
            {
                projectiles[i] = new Projectile();
            }

            for (int i = 0; i < Globals.MAX_SHOPS; i++)
            {
                shops[i] = new Shop();
            }

            for (int i = 0; i < Globals.MAX_CHATS; i++)
            {
                chats[i] = new Chat();
            }

            for (int i = 0; i < Globals.MAX_CHESTS; i++)
            {
                chests[i] = new Chest();
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
            for (int i = 0; i < Globals.MAX_PLAYERS; i++)
            {
                if (players[i].Name != "")
                {
                    if (i != HandleData.myIndex && players[i].Map == players[HandleData.myIndex].Map)
                    {
                        renderWindow.Draw(players[i]);
                    }
                }
            }
        }

        static void DrawNpcs()
        {
            int minX = (players[HandleData.myIndex].X + 12) - 12;
            int minY = (players[HandleData.myIndex].Y + 9) - 9;
            int maxX = (players[HandleData.myIndex].X + 12) + 13;
            int maxY = (players[HandleData.myIndex].Y + 9) + 11;

            for (int i = 0; i < Globals.MAX_MAP_NPCS; i++)
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

            for (int i = 0; i < Globals.MAX_MAP_POOL_NPCS; i++)
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
            int minX = (players[HandleData.myIndex].X + 12) - 12;
            int minY = (players[HandleData.myIndex].Y + 9) - 9;
            int maxX = (players[HandleData.myIndex].X + 12) + 13;
            int maxY = (players[HandleData.myIndex].Y + 9) + 11;

            for (int i = 0; i < Globals.MAX_MAP_ITEMS; i++)
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

        static void DrawChests()
        {
            int minX = (players[HandleData.myIndex].X + 12) - 12;
            int minY = (players[HandleData.myIndex].Y + 9) - 9;
            int maxX = (players[HandleData.myIndex].X + 12) + 13;
            int maxY = (players[HandleData.myIndex].Y + 9) + 11;

            for (int x = minX; x < maxX; x++)
            {
                for (int y = minY; y < maxY; y++)
                {
                    if (x > 0 && y > 0 && x < Globals.MAX_MAP_X && y < Globals.MAX_MAP_Y)
                    {
                        if (map.Ground[x, y].Type == (int)TileType.Chest)
                        {
                            
                            map.DrawChest(x, y, isChestEmpty(map.Ground[x, y].ChestNum));
                        }
                    }
                }
            }
        }

        static void DrawProjectiles()
        {
            int minX = (players[HandleData.myIndex].X + 12) - 12;
            int minY = (players[HandleData.myIndex].Y + 9) - 9;
            int maxX = (players[HandleData.myIndex].X + 12) + 13;
            int maxY = (players[HandleData.myIndex].Y + 9) + 11;

            for (int i = 0; i < Globals.MAX_DRAWN_PROJECTILES; i++)
            {
                if (map.m_MapProj[i] != null)
                {
                    if (map.m_MapProj[i].X > minX && map.m_MapProj[i].X < maxX)
                    {
                        if (map.m_MapProj[i].Y > minY && map.m_MapProj[i].Y < maxY)
                        {
                            renderWindow.Draw(map.m_MapProj[i]);
                        }
                    }
                    map.m_MapProj[i].CheckMovment(i);
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
                DrawNpcs();
                DrawPlayers();
                DrawIndexPlayer();
                DrawProjectiles();
                map.DrawFringe(renderWindow);
                if (TickCount - walkTick > 100)
                {
                    players[HandleData.myIndex].CheckMovement();
                    players[HandleData.myIndex].CheckControllerMovement();
                    players[HandleData.myIndex].CheckChangeDirection();
                    players[HandleData.myIndex].CheckControllerChangeDirection();
                    players[HandleData.myIndex].CheckReload();
                    players[HandleData.myIndex].CheckControllerReload();
                    players[HandleData.myIndex].CheckPlayerInteraction();
                    players[HandleData.myIndex].CheckControllerPlayerInteraction();
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
                hud.UpdateExpBar();
                hud.UpdateClipBar();
                hud.UpdateHungerBar();
                hud.UpdateHydrationBar();
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
            players[HandleData.myIndex].UpdateLifeTime();

            if (TickCount - saveTime >= 297000)
            {
                players[HandleData.myIndex].SendUpdatePlayerTime();
                players[HandleData.myIndex].SendUpdateLifeTime();
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
            string title = Globals.GAME_TITLE + " FPS: " + fps;

            if (players[HandleData.myIndex].Name != null) { title += " - Logged: " + players[HandleData.myIndex].Name; }
            if (worldTime.updateTime == true) { title += " - Time: " + worldTime.Time; }

            renderWindow.SetTitle(title);
        }

        static void CheckForConnection()
        {
            if (SabertoothClient.netClient.ServerConnection == null)
            {
                if (TickCount - discoverTick >= Globals.DISCOVERY_TIMER)
                {
                    Console.WriteLine("Connecting to server...");
                    SabertoothClient.netClient.DiscoverLocalPeers(Globals.SERVER_PORT);
                    discoverTick = TickCount;
                }
            }
            //Console.WriteLine("Status: " + SabertoothClient.netClient.ConnectionStatus.ToString());            
        }

        static void UpdateView()
        {
            view.Reset(new FloatRect(0, 0, Globals.SCREEN_WIDTH, Globals.SCREEN_HEIGHT));
            view.Move(new Vector2f(players[HandleData.myIndex].X * Globals.PIC_X, players[HandleData.myIndex].Y * Globals.PIC_Y));
            HandleData.HandleDataMessage();

            renderWindow.SetActive();
            renderWindow.DispatchEvents();
            renderWindow.Clear();
            //Gl.glClear(Gl.GL_DEPTH_BUFFER_BIT | Gl.GL_COLOR_BUFFER_BIT);

            renderWindow.SetView(view);

            fps = CalculateFrameRate();

            if (gui.menuWindow != null && gui.menuWindow.IsVisible) { gui.UpdateMenuWindow(); }
            if (players[HandleData.myIndex].inShop) { gui.UpdateShopWindow(); }
            if (gui.d_Window != null && gui.d_Window.IsVisible) { gui.UpdateDebugWindow(fps); }
            if (gui.bankWindow != null && gui.bankWindow.IsVisible) { gui.UpdateBankWindow(); }
            if (gui.chestWindow != null && gui.chestWindow.IsVisible) { gui.UpdateChestWindow(); }
            if (worldTime.updateTime == true) { worldTime.UpdateTime(); }

            UpdateTitle(fps);

            Joystick.Update();
        }
        #endregion

        #region Misc Methods
        static bool isChestEmpty(int chestNum)
        {
            for (int i = 0; i < Globals.MAX_CHESTS; i++)
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

    public static class Globals
    {
        //Server Globals
        public const byte NO = 0;
        public const byte YES = 1;
        public const int MAX_PLAYERS = 5;
        public const int MAX_NPCS = 10;
        public const int MAX_ITEMS = 50;
        public const int MAX_PROJECTILES = 10;
        public const int MAX_MAPS = 10;
        public const int MAX_MAP_NPCS = 10;
        public const int MAX_MAP_POOL_NPCS = 20;
        public const int MAX_MAP_ITEMS = 20;
        public const int MAX_MAP_X = 50;
        public const int MAX_MAP_Y = 50;
        public const int MAX_SHOPS = 10;
        public const int MAX_CHATS = 15;
        public const int MAX_CHESTS = 10;
        public const int MAX_CHEST_ITEMS = 10;
        //Config Globals
        public const string GAME_TITLE = "Sabertooth";
        public const string IP_ADDRESS = "10.16.0.8";
        public const int SERVER_PORT = 14242;
        public const float CONNECTION_TIMEOUT = 5.0f;   //Was 25.0
        public const float SIMULATED_RANDOM_LATENCY = 0f;   //0.085f
        public const float SIMULATED_MINIMUM_LATENCY = 0.000f;  //0.065f
        public const float SIMULATED_PACKET_LOSS = 0f;  //0.5f
        public const float SIMULATED_DUPLICATES_CHANCE = 0f; //0.5f
        public const string VERSION = "1.0"; //For beta and alpha
        //Client Globals
        public const int SCREEN_WIDTH = 800;
        public const int SCREEN_HEIGHT = 600;
        public const int CANVAS_WIDTH = 800;
        public const int CANVAS_HEIGHT = 600;
        public const int MAX_FPS = 85;
        public const Styles SCREEN_STYLE = Styles.Close;
        public const int MAX_DRAWN_PROJECTILES = 200;
        public const int DISCOVERY_TIMER = 6500;    //6500 / 1000 = 6.5 seconds
        public const int PIC_X = 32;
        public const int PIC_Y = 32;
        public const int SW_HIDE = 0;
        public const int SW_SHOW = 5;
    }
}