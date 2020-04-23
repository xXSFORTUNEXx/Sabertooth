using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Lidgren.Network;
using static Mono_Client.Globals;
using Gwen.Control;
using System.Data.SQLite;
using System.Xml;
using static System.Convert;
using System.Diagnostics;
using System.Threading;
using Microsoft.Xna.Framework.Content;

namespace Mono_Client
{
#if WINDOWS || LINUX
    public static class MonoClient
    {
        public static NetClient netClient;

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
        #endregion

        [STAThread]
        static void Main()
        {
            Logging.WriteLog(@"  _____       _               _              _   _     ");
            Logging.WriteLog(@" / ____|     | |             | |            | | | |    ");
            Logging.WriteLog(@"| (___   __ _| |__   ___ _ __| |_ ___   ___ | |_| |__  ");
            Logging.WriteLog(@" \___ \ / _` | '_ \ / _ \ '__| __/ _ \ / _ \| __| '_ \ ");
            Logging.WriteLog(@" ____) | (_| | |_) |  __/ |  | || (_) | (_) | |_| | | |");
            Logging.WriteLog(@"|_____/ \__,_|_.__/ \___|_|   \__\___/ \___/ \__|_| |_|");
            Logging.WriteLog(@"                              Created by Steven Fortune");
            Logging.WriteLog("Initializing client...");

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
            Logging.WriteLog("Enabling message types...");
            netClient = new NetClient(netConfig);
            netClient.Start();
            Logging.WriteLog("Network configuration complete...");
            LoadConfiguration();

            using (var game = new Client()) { game.Run(); }
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
                    Environment.Exit(0);
                }
            }
            catch (Exception e)
            {
                Logging.WriteMessageLog(e.Message);
            }
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
#endif

    public class Client : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Gwen.Renderer.MonoGame.Input.MonoGame m_Input;
        private Gwen.Renderer.MonoGame.MonoGame m_Renderer;
        private Gwen.Skin.SkinBase skin;
        public static Canvas canvas;
        bool changeGraphicSettings;
        public static GUI gui = new GUI();
        public static Player[] players = new Player[MAX_PLAYERS];
        public static Npc[] npcs = new Npc[MAX_NPCS];
        public static Shop[] shops = new Shop[MAX_SHOPS];
        public static Item[] items = new Item[MAX_ITEMS];
        public static Projectile[] projectiles = new Projectile[MAX_PROJECTILES];
        public static Chat[] chats = new Chat[MAX_CHATS];
        public static Quests[] quests = new Quests[MAX_QUESTS];
        public static Chest[] chests = new Chest[MAX_CHESTS];
        public static Map map = new Map();
        public static WorldTime worldTime = new WorldTime();

        static int Max_Tilesets = Directory.GetFiles("Resources/Tilesets/", "*", SearchOption.TopDirectoryOnly).Length;
        static int spriteTextures = Directory.GetFiles("Resources/Characters/", "*", SearchOption.TopDirectoryOnly).Length;
        public static int maxprojSprites = Directory.GetFiles("Resources/Projectiles/", "*", SearchOption.TopDirectoryOnly).Length;
        static int spritePics = Directory.GetFiles("Resources/Items/", "*", SearchOption.TopDirectoryOnly).Length;
        public static Texture2D[] c_ItemSprite = new Texture2D[spritePics];
        public static Texture2D[] proj_Texture = new Texture2D[maxprojSprites];
        public static Texture2D[] TileSet = new Texture2D[Max_Tilesets];
        public static Texture2D chestSprite;
        public static Texture2D[] c_Sprite = new Texture2D[spriteTextures];
        public static Texture2D c_bloodSprite;

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
        #endregion

        public Client()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            graphics.PreparingDeviceSettings += new EventHandler<PreparingDeviceSettingsEventArgs>(SetToPreserve‌​);
            Content.RootDirectory = "Content";
            changeGraphicSettings = false;
        }

        protected override void Initialize()
        {
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += new EventHandler<EventArgs>(OnClientSizeChanged);
            InitializeArrays();
            IsMouseVisible = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Gwen.Platform.Platform.Init(new Gwen.Platform.MonoGame.MonoGamePlatform());
            Gwen.Loader.LoaderBase.Init(new Gwen.Loader.MonoGame.MonoGameAssetLoader(Content));
            m_Renderer = new Gwen.Renderer.MonoGame.MonoGame(GraphicsDevice, Content, Content.Load<Effect>("GwenEffect"));
            m_Renderer.Resize(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            skin = new Gwen.Skin.TexturedBase(m_Renderer, "Skins/DefaultSkin", "Skins/DefaultSkinDefinition");
            skin.DefaultFont = new Gwen.Font(m_Renderer, "Arial", 11);
            canvas = new Canvas(skin);
            m_Input = new Gwen.Renderer.MonoGame.Input.MonoGame(this);
            m_Input.Initialize(canvas);
            canvas.SetSize(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            canvas.ShouldDrawBackground = true;
            canvas.BackgroundColor = new Gwen.Color(0, 0, 0, 0);

            LoadGameTextures(Content);

            gui.CreateMainWindow(canvas);
        }

        void LoadGameTextures(ContentManager contentManager)
        {
            chestSprite = contentManager.Load<Texture2D>("Resources/Chest");
            c_bloodSprite = contentManager.Load<Texture2D>("Resources/Blood");

            for (int i = 0; i < Max_Tilesets; i++)
            {
                TileSet[i] = contentManager.Load<Texture2D>("Resources/Tilesets/" + (i + 1));
            }

            for (int i = 0; i < spriteTextures; i++)
            {
                c_Sprite[i] = contentManager.Load<Texture2D>("Resources/Characters/" + (i + 1));
            }

            for (int i = 0; i < maxprojSprites; i++)
            {
                proj_Texture[i] = contentManager.Load<Texture2D>("Resources/Projectiles/" + (i + 1));
            }

            for (int i = 0; i < spritePics; i++)
            {
                c_ItemSprite[i] = contentManager.Load<Texture2D>("Resources/Items/" + (i + 1));
            }
        }

        protected override void UnloadContent()
        {
            if (canvas != null)
            {
                canvas.Dispose();
                canvas = null;
            }
            if (skin != null)
            {
                skin.Dispose();
                skin = null;
            }
            if (m_Renderer != null)
            {
                m_Renderer.Dispose();
                m_Renderer = null;
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (MonoClient.netClient.ServerConnection == null) { CheckForConnection(); }
            HandleData.HandleDataMessage();

            if (changeGraphicSettings)
            {
                graphics.ApplyChanges();
                changeGraphicSettings = false;
            }

            if (m_Renderer.TextCacheSize > 1000)
                m_Renderer.FlushTextCache();

            m_Input.ProcessMouseState();
            m_Input.ProcessKeyboardState();
            m_Input.ProcessTouchState();

            if (MonoClient.netClient.ServerConnection != null)
            {
                players[HandleData.myIndex].SendUpdateClip();
                players[HandleData.myIndex].SendUpdatePlayerTime();
                players[HandleData.myIndex].SendUpdateLifeTime();
            }

            UpdateLogic();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            DrawGraphics(spriteBatch);

            spriteBatch.End();

            canvas.RenderCanvas();

            base.Draw(gameTime);
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            Exit();
            base.OnExiting(sender, args);
        }

        private void DrawGraphics(SpriteBatch spriteBatch)
        {
            if (map.Name != null && gui.Ready)
            {
                map.Draw(spriteBatch);
                DrawChests(spriteBatch);
                DrawMapItems(spriteBatch);
                DrawBlood(spriteBatch);
                DrawNpcs(spriteBatch);
                DrawPlayers(spriteBatch);
                DrawIndexPlayer(spriteBatch);
                DrawProjectiles(spriteBatch);
                map.DrawFringe(spriteBatch);
            }
        }

        private void UpdateLogic()
        {
            if (map.Name != null && gui.Ready)
            {
                if (Environment.TickCount - walkTick > 100)
                {
                    players[HandleData.myIndex].CheckMovement();
                    //players[HandleData.myIndex].CheckControllerMovement();
                    players[HandleData.myIndex].CheckChangeDirection();
                    //players[HandleData.myIndex].CheckControllerChangeDirection();
                    players[HandleData.myIndex].CheckReload();
                    //players[HandleData.myIndex].CheckControllerReload();
                    players[HandleData.myIndex].CheckPlayerInteraction();
                    //players[HandleData.myIndex].CheckControllerPlayerInteraction();
                    //players[HandleData.myIndex].CheckControllerButtonPress();
                    ProcessMovement();
                    walkTick = Environment.TickCount;
                }
                if (Environment.TickCount - attackTick > 25)
                {
                    players[HandleData.myIndex].CheckAttack();
                    //players[HandleData.myIndex].CheckControllerAttack();
                    attackTick = Environment.TickCount;
                }
                if (Environment.TickCount - pickupTick > 100)
                {
                    players[HandleData.myIndex].CheckItemPickUp();
                    //players[HandleData.myIndex].CheckControllerItemPickUp();
                    pickupTick = Environment.TickCount;
                }
            }
        }

        private void DrawPlayers(SpriteBatch spriteBatch)
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
                                    players[i].Draw(spriteBatch);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void DrawNpcs(SpriteBatch spriteBatch)
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
                                map.m_MapNpc[i].Draw(spriteBatch);
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
                                map.m_MapNpc[i].Draw(spriteBatch);
                            }
                        }
                    }
                }
            }
        }

        private void DrawMapItems(SpriteBatch spriteBatch)
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
                                map.m_MapItem[i].Draw(spriteBatch);
                            }
                        }
                    }
                }
            }
        }

        private void DrawBlood(SpriteBatch spriteBatch)
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
                            map.m_BloodSplats[i].Draw(spriteBatch);
                        }
                    }
                }
            }
        }

        private void DrawChests(SpriteBatch spriteBatch)
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
                            map.DrawChest(x, y, isChestEmpty(map.Ground[x, y].ChestNum), spriteBatch);
                        }
                    }
                }
            }
        }

        private void DrawProjectiles(SpriteBatch spriteBatch)
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

            for (int i = 0; i < MAX_DRAWN_PROJECTILES; i++)
            {
                if (map.m_MapProj[i] != null)
                {
                    if (map.m_MapProj[i].X > minX && map.m_MapProj[i].X < maxX)
                    {
                        if (map.m_MapProj[i].Y > minY && map.m_MapProj[i].Y < maxY)
                        {
                            map.m_MapProj[i].Draw(spriteBatch);
                        }
                    }
                    map.m_MapProj[i].CheckMovment(i);
                }
            }
        }

        private void DrawIndexPlayer(SpriteBatch spriteBatch)
        {
            players[HandleData.myIndex].Draw(spriteBatch);
        }

        private bool isChestEmpty(int chestNum)
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

        private void OnClientSizeChanged(object sender, EventArgs e)
        {
            graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
            graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
            changeGraphicSettings = true;
            m_Renderer.Resize(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            canvas.SetSize(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
        }

        private void SetToPreserve(object sender, PreparingDeviceSettingsEventArgs eventargs)
        {
            eventargs.GraphicsDeviceInformation.PresentationParameters.R‌​enderTargetUsage = RenderTargetUsage.PreserveContents;
        }

        private void InitializeArrays()
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

            for (int i = 0; i < MAX_PROJECTILES; i++)
            {
                projectiles[i] = new Projectile();
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

        private void CheckForConnection()
        {
            if (MonoClient.netClient.ServerConnection == null)
            {
                if (Environment.TickCount - discoverTick >= DISCOVERY_TIMER)
                {

                    Logging.WriteMessageLog("Connecting to server...");
                    if (MonoClient.LanConnection)
                    {
                        int port = ToInt32(MonoClient.Port);
                        MonoClient.netClient.DiscoverLocalPeers(port);
                    }
                    else
                    {
                        try
                        {
                            NetOutgoingMessage outMSG = MonoClient.netClient.CreateMessage();
                            outMSG.Write((byte)PacketTypes.Connection);
                            outMSG.Write("sabertooth");
                            MonoClient.netClient.Connect(MonoClient.IPAddress, ToInt32(MonoClient.Port), outMSG);
                        }
                        catch (Exception e)
                        {
                            Logging.WriteMessageLog(e.Message);
                        }
                    }
                    discoverTick = Environment.TickCount;
                }
            }       
        }

        private void ProcessMovement()
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

        private void UpdatePlayerTime()
        {
            if (MonoClient.netClient.ServerConnection == null || players[HandleData.myIndex].Name == null) { return; }

            players[HandleData.myIndex].UpdatePlayerTime();
            players[HandleData.myIndex].UpdateLifeTime();

            if (Environment.TickCount - saveTime >= 297000)
            {
                players[HandleData.myIndex].SendUpdatePlayerTime();
                players[HandleData.myIndex].SendUpdateLifeTime();
                saveTime = Environment.TickCount;
            }
        }
    }
}
