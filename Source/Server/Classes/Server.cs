using Lidgren.Network;
using System;
using System.Net;
using System.Threading;
using System.Xml;
using static System.Environment;
using static System.IO.File;
using static System.Convert;
using System.IO;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using static SabertoothServer.Globals;

namespace SabertoothServer
{
    public class SabertoothServer
    {

        // Run to check how many lines of code my project has
        //Ctrl+Shift+F, use regular expression, ^(?([^\r\n])\s)*[^\s+?/]+[^\n]*$

        public static NetServer netServer;

        static void Main(string[] args)
        {
            Console.Title = "Sabertooth Server";
            Logging.WriteMessageLog(@"  _____       _               _              _   _     ");
            Logging.WriteMessageLog(@" / ____|     | |             | |            | | | |    ");
            Logging.WriteMessageLog(@"| (___   __ _| |__   ___ _ __| |_ ___   ___ | |_| |__  ");
            Logging.WriteMessageLog(@" \___ \ / _` | '_ \ / _ \ '__| __/ _ \ / _ \| __| '_ \ ");
            Logging.WriteMessageLog(@" ____) | (_| | |_) |  __/ |  | || (_) | (_) | |_| | | |");
            Logging.WriteMessageLog(@"|_____/ \__,_|_.__/ \___|_|   \__\___/ \___/ \__|_| |_|");
            Logging.WriteMessageLog(@"                              Created by Steven Fortune");
            Logging.WriteMessageLog("Loading...Please wait...");
            Logging.WriteMessageLog("Loading...Please wait...Init network configuration...");
            Logging.WriteMessageLog("Init network configuration...");

            NetPeerConfiguration netConfig = new NetPeerConfiguration("sabertooth")
            {
                Port = SERVER_PORT,
                UseMessageRecycling = true,
                MaximumConnections = MAX_PLAYERS,
                EnableUPnP = false,
                ConnectionTimeout = CONNECTION_TIMEOUT,
                SimulatedRandomLatency = SIMULATED_RANDOM_LATENCY,
                SimulatedMinimumLatency = SIMULATED_MINIMUM_LATENCY,
                SimulatedLoss = SIMULATED_PACKET_LOSS,
                SimulatedDuplicatesChance = SIMULATED_DUPLICATES_CHANCE
            };

            Logging.WriteMessageLog("Enabling message types...");
            netConfig.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            netConfig.EnableMessageType(NetIncomingMessageType.ConnectionLatencyUpdated);
            netConfig.EnableMessageType(NetIncomingMessageType.DiscoveryRequest);
            Logging.WriteMessageLog("Disabling message types...");
            netConfig.DisableMessageType(NetIncomingMessageType.DebugMessage);
            netConfig.DisableMessageType(NetIncomingMessageType.Error);
            netConfig.DisableMessageType(NetIncomingMessageType.NatIntroductionSuccess);
            netConfig.DisableMessageType(NetIncomingMessageType.Receipt);
            netConfig.DisableMessageType(NetIncomingMessageType.UnconnectedData);
            netConfig.DisableMessageType(NetIncomingMessageType.VerboseDebugMessage);
            netConfig.DisableMessageType(NetIncomingMessageType.WarningMessage);
            Logging.WriteMessageLog("Message Types Enabled: ConApproval, Latency Updates, Discovery Requests");
            Logging.WriteMessageLog("Message Types Disabled: Debug, NAT Intro Success, Receipt, UnconnectedData, Verbose Debug, Warning Message");

            Server.LoadConfiguration();
            Server.CheckSQLConnection();
            netServer = new NetServer(netConfig);
            netServer.Start();
            Logging.WriteMessageLog("Network configuration complete...");
            Server.ServerLoop();
        }
    }

    public static class Server
    {
        #region Classes
        public static Player[] players = new Player[MAX_PLAYERS];
        public static Npc[] npcs = new Npc[MAX_NPCS];
        public static Item[] items = new Item[MAX_ITEMS];
        public static Projectile[] projectiles = new Projectile[MAX_PROJECTILES];
        public static Map[] maps = new Map[MAX_MAPS];
        public static Shop[] shops = new Shop[MAX_SHOPS];
        public static Chat[] chats = new Chat[MAX_CHATS];
        public static Chest[] chests = new Chest[MAX_CHESTS];
        public static WorldTime worldTime = new WorldTime();
        public static Instance instance = new Instance();
        public static Random RND = new Random();
        #endregion

        #region Variables
        public static bool isRunning;
        private static int saveTick;
        private static int aiTick;
        private static int regenTick;
        private static int regenTime;
        private static int hungerTime;
        private static int hydrationTime;
        private static int saveTime;
        private static int spawnTime;
        private static int aiTime;
        private static int removeTime;
        public static int sSecond;
        public static int sMinute;
        public static int sHour;
        public static int sDay;
        public static int suptimeTick;
        public static string upTime;
        public static string sVersion;
        public static string sqlServer;
        public static string sqlDatabase;
        static int lastTick;
        static int lastFrameRate;
        static int frameRate;
        static int fps;
        #endregion

        public static void ServerLoop()
        {
            InitArrays();

            Thread commandThread = new Thread(() => CommandWindow());
            commandThread.Start();

            isRunning = true;
            while (isRunning)
            {
                worldTime.UpdateTime();
                UpdateTitle();
                HandleData.HandleDataMessage();
                SavePlayers();
                CheckNpcSpawn();
                CheckItemSpawn();
                CheckClearMapItem();
                CheckHealthRegen();
                CheckVitalLoss();
                CheckNpcAi();
                UpTime();
                fps = CalculateFrameRate();
                Thread.Sleep(1);
            }
            DisconnectClients();
            Logging.WriteMessageLog("Disconnecting clients...");
            Thread.Sleep(2500);
            SabertoothServer.netServer.Shutdown("Shutting down");
            Logging.WriteMessageLog("Shutting down...");
            Thread.Sleep(500);
            Exit(0);
        }

        private static void InitArrays()
        {
            #region Players
            Logging.WriteMessageLog("Creating player array...");
            for (int i = 0; i < MAX_PLAYERS; i++)
            {
                players[i] = new Player();
            }
            Logging.WriteMessageLog("Player array loaded successfully");
            #endregion

            #region Maps
            Logging.WriteMessageLog("Loading maps...");
            for (int i = 0; i < MAX_MAPS; i++)
            {
                maps[i] = new Map();
                maps[i].LoadMapFromDatabase(i + 1);
            }
            Logging.WriteMessageLog("Maps loaded successfully");
            #endregion

            #region Items
            Logging.WriteMessageLog("Loading npcs...");
            for (int i = 0; i < MAX_ITEMS; i++)
            {
                items[i] = new Item();
                items[i].LoadItemFromDatabase(i + 1);
            }
            Logging.WriteMessageLog("Items loaded successfully");
            #endregion

            #region Projectiles
            Logging.WriteMessageLog("Loading projectiles...");
            for (int i = 0; i < MAX_PROJECTILES; i++)
            {
                projectiles[i] = new Projectile();
                projectiles[i].LoadProjectileFromDatabase(i + 1);
            }
            Logging.WriteMessageLog("Projectiles loaded successfully");
            #endregion

            #region Npcs
            Logging.WriteMessageLog("Loading npcs...");
            for (int i = 0; i < MAX_NPCS; i++)
            {
                npcs[i] = new Npc();
                npcs[i].LoadNpcFromDatabase((i + 1));
            }
            for (int i = 0; i < MAX_MAPS; i++)
            {
                for (int n = 0; n < MAX_MAPS; n++)
                {
                    int num = (maps[i].m_MapNpc[n].NpcNum - 1);

                    if (num > -1)
                    {
                        maps[i].m_MapNpc[n].Name = npcs[num].Name;
                        maps[i].m_MapNpc[n].X = npcs[num].X;
                        maps[i].m_MapNpc[n].Y = npcs[num].Y;
                        maps[i].m_MapNpc[n].Health = npcs[num].Health;
                        maps[i].m_MapNpc[n].MaxHealth = npcs[num].MaxHealth;
                        maps[i].m_MapNpc[n].Direction = npcs[num].Direction;
                        maps[i].m_MapNpc[n].Step = npcs[num].Step;
                        maps[i].m_MapNpc[n].Sprite = npcs[num].Sprite;
                        maps[i].m_MapNpc[n].Behavior = npcs[num].Behavior;
                        maps[i].m_MapNpc[n].Owner = npcs[num].Owner;
                        maps[i].m_MapNpc[n].IsSpawned = npcs[num].IsSpawned;
                        maps[i].m_MapNpc[n].Damage = npcs[num].Damage;
                        maps[i].m_MapNpc[n].DesX = npcs[num].DesX;
                        maps[i].m_MapNpc[n].DesY = npcs[num].DesY;
                        maps[i].m_MapNpc[n].Exp = npcs[num].Exp;
                        maps[i].m_MapNpc[n].Money = npcs[num].Money;
                        maps[i].m_MapNpc[n].SpawnTime = npcs[num].SpawnTime;
                        maps[i].m_MapNpc[n].Range = npcs[num].Range;
                        maps[i].m_MapNpc[n].ShopNum = npcs[num].ShopNum;
                        maps[i].m_MapNpc[n].ChatNum = npcs[num].ChatNum;
                        maps[i].m_MapNpc[n].Speed = npcs[num].Speed;
                    }
                }
            }
            Logging.WriteMessageLog("Npcs loaded successfully");
            #endregion

            #region Shops
            Logging.WriteMessageLog("Loading shops...");
            for (int i = 0; i < MAX_SHOPS; i++)
            {
                shops[i] = new Shop();
                shops[i].LoadShopFromDatabase(i + 1);
            }

            Logging.WriteMessageLog("Shops loaded successfully");
            #endregion

            #region Chats
            Logging.WriteMessageLog("Loading chats...");
            for (int i = 0; i < MAX_CHATS; i++)
            {
                chats[i] = new Chat();
                chats[i].LoadChatFromDatabase(i + 1);
            }
            Logging.WriteMessageLog("Chats loaded successfully");
            #endregion

            #region Chests
            Logging.WriteMessageLog("Loading chests...");
            for (int i = 0; i < MAX_CHESTS; i++)
            {
                chests[i] = new Chest();
                chests[i].LoadChestFromDatabase(i + 1);
            }
            Logging.WriteMessageLog("Chests loaded successfully");
            #endregion

            //final
            Logging.WriteMessageLog("Server is listening for connections...");
        }

        #region Database
        public static void CheckSQLConnection()
        {
            string connection = "Data Source=" + sqlServer + ";Integrated Security=True";
            string script = ReadAllText("SQL Scripts/DATABASE.sql");
            try
            {
                using (var sql = new SqlConnection(connection))
                {
                    sql.Open();
                    using (var cmd = new SqlCommand(script, sql))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                Logging.WriteMessageLog("Established SQL Server connection!", "SQL");
                CheckDatabaseTables();
            }
            catch (Exception e)
            {
                Logging.WriteMessageLog("Error esablishing SQL connection, Check log for details...", "SQL");
                Logging.WriteLog(e.Message, "SQL");
            }
        }

        public static void CheckDatabaseTables()
        {
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            string script;
            try
            {
                using (var sql = new SqlConnection(connection))
                {
                    sql.Open();

                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = sql;
                        script = ReadAllText("SQL Scripts/PLAYERS.sql");
                        script += ReadAllText("SQL Scripts/MAINWEAPONS.sql");
                        script += ReadAllText("SQL Scripts/SECONDARYWEAPONS.sql");
                        script += ReadAllText("SQL Scripts/EQUIPMENT.sql");
                        script += ReadAllText("SQL Scripts/INVENTORY.sql");
                        script += ReadAllText("SQL Scripts/BANK.sql");
                        script += ReadAllText("SQL Scripts/ITEMS.sql");
                        script += ReadAllText("SQL Scripts/NPCS.sql");
                        script += ReadAllText("SQL Scripts/PROJECTILES.sql");
                        script += ReadAllText("SQL Scripts/SHOPS.sql");
                        script += ReadAllText("SQL Scripts/CHAT.sql");
                        script += ReadAllText("SQL Scripts/MAPS.sql");
                        script += ReadAllText("SQL Scripts/CHESTS.sql");
                        script += ReadAllText("SQL Scripts/STATS.sql");
                        cmd.CommandText = script;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Logging.WriteMessageLog("SQL execution error, Check log for details...", "SQL");
                Logging.WriteLog(e.Message, "SQL");
            }
        }
        #endregion

        #region Server Logic
        static void UpTime()
        {
            if (TickCount - suptimeTick > A_MILLISECOND)
            {
                if (sSecond < SECONDS_IN_MINUTE)
                {
                    sSecond += 1;
                }
                else
                {
                    sSecond = 0;
                    sMinute += 1;
                }

                if (sMinute >= MINUTES_IN_HOUR)
                {
                    sMinute = 0;
                    sHour += 1;
                }
                if (sHour == HOURS_IN_DAY)
                {
                    sHour = 0;
                    sDay += 1;
                }
                upTime = "Uptime - Days: " + sDay + " Hours: " + sHour + " Minutes: " + sMinute + " Seconds: " + sSecond;
                suptimeTick = TickCount;
            }
        }

        static bool CheckHealthRegen()
        {
            if (TickCount - regenTick < regenTime) { return false; }

            for (int i = 0; i < MAX_PLAYERS; i++)
            {
                if (players[i].Name != null)
                {
                    Logging.WriteMessageLog("Checking for health regin...");

                    players[i].RegenHealth();
                    HandleData.SendUpdateHealthData(i, players[i].Health);
                }
            }
            regenTick = TickCount;
            return true;
        }

        static void CheckVitalLoss()
        {
            for (int i = 0; i < MAX_PLAYERS; i++)
            {
                if (players[i].Name != null)
                {
                    //Check for hunger
                    if (TickCount - players[i].hungerTick >= hungerTime)
                    {
                        Logging.WriteMessageLog("Checking for hunger loss...");

                        players[i].VitalLoss("food");
                        HandleData.SendUpdateVitalData(i, "food", players[i].Hunger);
                        players[i].hungerTick = TickCount;
                    }

                    if (TickCount - players[i].hydrationTick >= hydrationTime)
                    {

                        Logging.WriteMessageLog("Checking for hydration loss...");

                        players[i].VitalLoss("water");
                        HandleData.SendUpdateVitalData(i, "water", players[i].Hydration);
                        players[i].hydrationTick = TickCount;
                    }
                }
            }
        }

        static bool CheckIfMapHasPlayers(int mapNum)
        {
            for (int i = 0; i < MAX_PLAYERS; i++)
            {
                if (players[i].Connection != null && players[i].Map == mapNum)
                {
                    return true;
                }
            }
            return false;
        }

        static bool CheckClearMapItem()
        {
            if (TickCount - removeTime < 1000) { return false; }

            for (int i = 0; i < MAX_MAPS; i++)
            {
                if (maps[i] != null && maps[i].Name != null)
                {
                    if (CheckIfMapHasPlayers(i))
                    {
                        for (int n = 0; n < MAX_MAP_ITEMS; n++)
                        {
                            if (maps[i].m_MapItem[n].ExpireTick > 0 && maps[i].m_MapItem[n].IsSpawned)
                            {
                                if (TickCount - maps[i].m_MapItem[n].ExpireTick > 300000)
                                {
                                    maps[i].m_MapItem[n] = new MapItem("None", 0, 0, 0);

                                    for (int p = 0; p < MAX_PLAYERS; p++)
                                    {
                                        if (players[p].Connection != null && i == players[p].Map)
                                        {
                                            HandleData.SendMapItemData(players[p].Connection, i, n);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            removeTime = TickCount;
            return true;
        }

        static void CheckItemSpawn()
        {
            for (int i = 0; i < MAX_MAPS; i++)
            {
                if (maps[i] != null && maps[i].Name != null)
                {
                    if (CheckIfMapHasPlayers(i))
                    {
                        for (int x = 0; x < maps[i].MaxX; x++)
                        {
                            for (int y = 0; y < maps[i].MaxY; y++)
                            {
                                if (maps[i].Ground[x, y].Type == (int)TileType.MapItem)
                                {
                                    if (maps[i].Ground[x, y].SpawnNum > 0)
                                    {
                                        if (maps[i].Ground[x, y].NeedsSpawnedTick > 0)
                                        {
                                            if (TickCount - maps[i].Ground[x, y].NeedsSpawnedTick > 300000 && maps[i].Ground[x, y].NeedsSpawned)
                                            {
                                                maps[i].Ground[x, y].NeedsSpawned = false;
                                                maps[i].Ground[x, y].NeedsSpawnedTick = 0;
                                            }
                                        }

                                        if (!maps[i].Ground[x, y].NeedsSpawned)
                                        {
                                            int slot = FindOpenMapItemSlot(maps[i]);
                                            if (slot < 20)
                                            {
                                                int itemNum = maps[i].Ground[x, y].SpawnNum - 1;
                                                maps[i].m_MapItem[slot].ItemNum = itemNum + 1;
                                                maps[i].m_MapItem[slot].Name = items[itemNum].Name;
                                                maps[i].m_MapItem[slot].X = x;
                                                maps[i].m_MapItem[slot].Y = y;
                                                maps[i].m_MapItem[slot].Sprite = items[itemNum].Sprite;
                                                maps[i].m_MapItem[slot].Damage = items[itemNum].Damage;
                                                maps[i].m_MapItem[slot].Armor = items[itemNum].Armor;
                                                maps[i].m_MapItem[slot].Type = items[itemNum].Type;
                                                maps[i].m_MapItem[slot].AttackSpeed = items[itemNum].AttackSpeed;
                                                maps[i].m_MapItem[slot].ReloadSpeed = items[itemNum].ReloadSpeed;
                                                maps[i].m_MapItem[slot].HealthRestore = items[itemNum].HealthRestore;
                                                maps[i].m_MapItem[slot].HungerRestore = items[itemNum].HungerRestore;
                                                maps[i].m_MapItem[slot].HydrateRestore = items[itemNum].HydrateRestore;
                                                maps[i].m_MapItem[slot].Strength = items[itemNum].Strength;
                                                maps[i].m_MapItem[slot].Agility = items[itemNum].Agility;
                                                maps[i].m_MapItem[slot].Endurance = items[itemNum].Endurance;
                                                maps[i].m_MapItem[slot].Stamina = items[itemNum].Stamina;
                                                maps[i].m_MapItem[slot].Clip = items[itemNum].Clip;
                                                maps[i].m_MapItem[slot].MaxClip = items[itemNum].MaxClip;
                                                maps[i].m_MapItem[slot].ItemAmmoType = items[itemNum].ItemAmmoType;
                                                maps[i].m_MapItem[slot].ProjectileNumber = items[itemNum].ProjectileNumber;
                                                maps[i].m_MapItem[slot].Price = items[itemNum].Price;
                                                maps[i].m_MapItem[slot].Value = maps[i].Ground[x, y].SpawnAmount;
                                                maps[i].m_MapItem[slot].Rarity = items[itemNum].Rarity;
                                                maps[i].m_MapItem[slot].IsSpawned = true;
                                                maps[i].Ground[x, y].NeedsSpawned = true;

                                                for (int p = 0; p < MAX_PLAYERS; p++)
                                                {
                                                    if (players[p].Connection != null && players[p].Active == "Y" && i == players[p].Map)
                                                    {
                                                        HandleData.SendMapItemData(players[p].Connection, i, slot);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        static void CheckNpcSpawn()
        {
            for (int i = 0; i < MAX_MAPS; i++)
            {
                if (maps[i] != null && maps[i].Name != null)
                {
                    if (CheckIfMapHasPlayers(i))
                    {
                        for (int x = 0; x < maps[i].MaxX; x++)
                        {
                            for (int y = 0; y < maps[i].MaxY; y++)
                            {
                                switch (maps[i].Ground[x, y].Type)
                                {
                                    case (int)TileType.NpcSpawn:
                                        for (int c = 0; c < MAX_MAP_NPCS; c++)
                                        {
                                            if (maps[i].Ground[x, y].SpawnNum == (c + 1))
                                            {
                                                if (!maps[i].m_MapNpc[c].IsSpawned && maps[i].m_MapNpc[c].Name != "None")
                                                {
                                                    if (TickCount - maps[i].m_MapNpc[c].spawnTick > (maps[i].m_MapNpc[c].SpawnTime * 1000))
                                                    {
                                                        maps[i].m_MapNpc[c].X = x;
                                                        maps[i].m_MapNpc[c].Y = y;
                                                        maps[i].m_MapNpc[c].IsSpawned = true;

                                                        for (int p = 0; p < MAX_PLAYERS; p++)
                                                        {
                                                            if (players[p].Connection != null && players[p].Active == "Y" && i == players[p].Map)
                                                            {
                                                                HandleData.SendMapNpcData(players[p].Connection, i, c);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        break;

                                    case (int)TileType.SpawnPool:
                                        if (maps[i].Ground[x, y].SpawnNum > 0)  //Usually greater than one otherwise we should use npcspawn
                                        {
                                            for (int n = 0; n < maps[i].Ground[x, y].SpawnAmount; n++)
                                            {
                                                if (maps[i].Ground[x, y].CurrentSpawn < maps[i].Ground[x, y].SpawnAmount)
                                                {
                                                    if (!maps[i].r_MapNpc[n].IsSpawned)
                                                    {
                                                        if (TickCount - maps[i].r_MapNpc[n].spawnTick > (maps[i].r_MapNpc[n].SpawnTime * 1000))
                                                        {
                                                            int npcNum = maps[i].m_MapNpc[maps[i].Ground[x, y].SpawnNum - 1].NpcNum - 1;

                                                            //if (npcNum > 0)
                                                            {
                                                                maps[i].r_MapNpc[n].NpcNum = npcNum;
                                                                int genX = (x + RND.Next(1, 3));
                                                                int genY = (y + RND.Next(1, 3));
                                                                maps[i].r_MapNpc[n].Name = npcs[npcNum].Name;
                                                                maps[i].r_MapNpc[n].X = genX;
                                                                maps[i].r_MapNpc[n].Y = genY;
                                                                maps[i].r_MapNpc[n].Health = npcs[npcNum].Health;
                                                                maps[i].r_MapNpc[n].MaxHealth = npcs[npcNum].MaxHealth;
                                                                maps[i].r_MapNpc[n].SpawnX = x;
                                                                maps[i].r_MapNpc[n].SpawnY = y;
                                                                maps[i].r_MapNpc[n].Direction = npcs[npcNum].Direction;
                                                                maps[i].r_MapNpc[n].Step = npcs[npcNum].Step;
                                                                maps[i].r_MapNpc[n].Sprite = npcs[npcNum].Sprite;
                                                                maps[i].r_MapNpc[n].Behavior = npcs[npcNum].Behavior;
                                                                maps[i].r_MapNpc[n].Owner = npcs[npcNum].Owner;
                                                                maps[i].r_MapNpc[n].Damage = npcs[npcNum].Damage;
                                                                maps[i].r_MapNpc[n].DesX = npcs[npcNum].DesX;
                                                                maps[i].r_MapNpc[n].DesY = npcs[npcNum].DesY;
                                                                maps[i].r_MapNpc[n].Exp = npcs[npcNum].Exp;
                                                                maps[i].r_MapNpc[n].Money = npcs[npcNum].Money;
                                                                maps[i].r_MapNpc[n].SpawnTime = npcs[npcNum].SpawnTime;
                                                                maps[i].r_MapNpc[n].IsSpawned = true;
                                                                maps[i].r_MapNpc[n].Range = npcs[npcNum].Range;
                                                                maps[i].r_MapNpc[n].Speed = npcs[npcNum].Speed;
                                                                maps[i].Ground[x, y].CurrentSpawn += 1;

                                                                for (int p = 0; p < MAX_PLAYERS; p++)
                                                                {
                                                                    if (players[p].Connection != null && players[p].Active == "Y" && i == players[p].Map)
                                                                    {
                                                                        HandleData.SendPoolNpcData(players[p].Connection, i, n);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }

        static void CheckNpcAi()
        {
            if (TickCount - aiTick > aiTime)
            {
                for (int i = 0; i < MAX_MAPS; i++)
                {
                    if (CheckIfMapHasPlayers(i))
                    {
                        for (int n = 0; n < MAX_MAP_NPCS; n++)
                        {
                            if (maps[i].m_MapNpc[n].IsSpawned)
                            {
                                if (TickCount - maps[i].m_MapNpc[n].aiTick > (maps[i].m_MapNpc[n].Speed))
                                {
                                    int canMove = RND.Next(0, 100);
                                    int dir = RND.Next(0, 3);

                                    maps[i].m_MapNpc[n].NpcAI(canMove, dir, i);

                                    if (maps[i].m_MapNpc[n].DidMove)
                                    {
                                        maps[i].m_MapNpc[n].DidMove = false;

                                        for (int p = 0; p < MAX_PLAYERS; p++)
                                        {
                                            if (players[p].Connection != null && players[p].Active == "Y" && players[p].Map == i)
                                            {
                                                HandleData.SendUpdateNpcLoc(players[p].Connection, i, n);
                                            }
                                        }
                                    }
                                    maps[i].m_MapNpc[n].aiTick = TickCount;
                                }
                            }
                        }
                        for (int c = 0; c < MAX_MAP_POOL_NPCS; c++)
                        {
                            if (maps[i].r_MapNpc[c].IsSpawned)
                            {
                                if (TickCount - maps[i].r_MapNpc[c].aiTick > (maps[i].r_MapNpc[c].Speed))
                                {
                                    int canMove = RND.Next(0, 100);
                                    int dir = RND.Next(0, 3);

                                    maps[i].r_MapNpc[c].NpcAI(canMove, dir, i);

                                    if (maps[i].r_MapNpc[c].DidMove)
                                    {
                                        maps[i].r_MapNpc[c].DidMove = false;

                                        for (int p = 0; p < 5; p++)
                                        {
                                            if (players[p].Connection != null && players[p].Active == "Y" && players[p].Map == i)
                                            {
                                                HandleData.SendUpdatePoolNpcLoc(players[p].Connection, i, c);
                                            }
                                        }
                                    }
                                    maps[i].r_MapNpc[c].aiTick = TickCount;
                                }
                            }
                        }
                    }
                }
                aiTick = TickCount;
            }
        }

        static void CommandWindow()
        {
            string input;
            while (true)
            {
                Console.Write(">");
                input = Console.ReadLine().ToLower();
                bool isDynamic = false;
                Logging.WriteLog("Command: " + input, "Commands");   //Log which command was used

                #region Commands
                switch (input)  //Basic commands can be ran in a switch statement since they dont require modifiers and arguments
                {
                    case "shutdown":    //Shutdown the server in about 3 seconds
                        isRunning = false;  //Break the loop
                        break;

                    case "exit":    //Same as shutdown command but shorter and it was the first command it wrote
                        isRunning = false;  //Break the loop
                        break;

                    case "saveall":    //Save all players (online) which just saves all accounts to their respective XML files
                        SaveAll();  //The void for this command
                        break;

                    case "info":
                        string hostName = Dns.GetHostName();
                        float latency = SabertoothServer.netServer.Configuration.SimulatedMinimumLatency;
                        Logging.WriteMessageLog("Statistics: ", "Commands");
                        Logging.WriteMessageLog("Version: " + sVersion, "Commands");
                        Logging.WriteMessageLog(upTime, "Commands");                        
                        Logging.WriteMessageLog("CPS: " + fps, "Commands");
                        Logging.WriteMessageLog("Public IP: " + GetPublicIPAddress());
                        Logging.WriteMessageLog("Host Name: " + hostName, "Commands");
                        Logging.WriteMessageLog("Server Address: " + NetUtility.Resolve(hostName), "Commands");
                        Logging.WriteMessageLog("Port: " + SabertoothServer.netServer.Port, "Commands");
                        Logging.WriteMessageLog(SabertoothServer.netServer.Statistics.ToString(), "Commands");
                        if (latency > 0.000) { Logging.WriteMessageLog("Configured Latency: " + SabertoothServer.netServer.Configuration.SimulatedMinimumLatency.ToString().Trim('.', '0') + "ms", "Commands"); }
                        for (int i = 0; i < MAX_PLAYERS; i++)
                        {
                            if (players[i].Connection != null)
                            {
                                Logging.WriteMessageLog(players[i].Connection + " Logged in as: " + players[i].Name + " Latency: " + players[i].Connection.AverageRoundtripTime.ToString(".0#0").TrimStart('0', '.', '0') + "ms", "Commands");
                                Logging.WriteMessageLog(players[i].Connection.Statistics.ToString(), "Commands");
                            }
                            else
                            {
                                Logging.WriteMessageLog("Open", "Commands");
                            }
                        }
                        break;

                    case "uptime":
                        Logging.WriteMessageLog(upTime, "Commands");
                        Logging.WriteMessageLog("Local Time: " + worldTime.Time, "Commands");
                        break;

                    case "createaccount":
                        Logging.WriteMessageLogLine("Username: ", "Commands");
                        string name = Console.ReadLine();
                        Logging.WriteMessageLogLine("Password: ", "Commands");
                        string pass = Console.ReadLine();
                        Logging.WriteMessageLogLine("Email Address: ", "Commands");
                        string email = Console.ReadLine();

                        if (name.Length >= 3 && pass.Length >= 3 && email.Length >= 5)
                        {
                            Player c_Player = new Player(name, pass, email, 0, 0, 0, 0, 0, 1, 100, 100, 100, 0, 0, 0, 100, 100, 1, 1, 1, 1, 1000);
                            c_Player.CreatePlayerInDatabase();
                            Logging.WriteMessageLog("Account created! UN: " + name + " EA: " + email, "Commands");
                        } else { Logging.WriteMessageLog("Name, Password or Email invalid. Please try again!"); }
                        break;

                    case "execsql":
                        string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
                        Logging.WriteMessageLogLine("Script location: ", "Commands");
                        string scriptFile = Console.ReadLine();
                        if (Exists(scriptFile) || scriptFile == null)
                        {
                            string script = ReadAllText(scriptFile);

                            using (var sql = new SqlConnection(connection))
                            {
                                sql.Open();
                                using (var cmd = new SqlCommand(script, sql))
                                {
                                    cmd.ExecuteNonQuery();
                                }
                                Logging.WriteMessageLog("Script executed successfully!");
                            }
                        }
                        else
                        {
                            Logging.WriteMessageLog("Invalid file/location...");
                        }
                        break;

                    case "activeacc":
                        Logging.WriteMessageLogLine("Username: ", "Commands");
                        string aname = Console.ReadLine();

                        if (AccountExist(aname))
                        {
                            Player c_Player = new Player();
                            c_Player.LoadPlayerIDFromDatabase(aname);
                            c_Player.LoadPlayerNameFromDatabase(c_Player.Id);

                            if (!c_Player.IsAccountActive())
                            {
                                c_Player.UpdateAccountStatusInDatabase();
                                Logging.WriteMessageLog("Account " + aname + " now activated!");
                            }
                            else { Logging.WriteMessageLog("Accounts is already active!"); }
                        }
                        else { Logging.WriteMessageLog("Accounts doesnt exist!"); }
                        break;

                    case "projcheck":
                        Logging.WriteMessageLogLine("Map number: ", "Commands");
                        string m_Num = Console.ReadLine();
                        
                        int total = 0;
                        for (int i = 0; i < MAX_MAP_PROJECTILES; i++)
                        {
                            if (maps[ToInt32(m_Num)].m_MapProj[i] != null)
                            {
                                total += 1;
                            }
                        }
                        Logging.WriteMessageLog("Currently [" + total + "] projectiles in use", "Commands");

                        if (total > 0)
                        {
                            Logging.WriteMessageLogLine("View them? (y/n): ", "Commands");
                            string info = Console.ReadLine();
                            if (info.ToLower() == "y")
                            {
                                for (int i = 0; i < MAX_MAP_PROJECTILES; i++)
                                {
                                    if (maps[ToInt32(m_Num)].m_MapProj[i] != null)
                                    {
                                        Logging.WriteLog("Name: " + maps[ToInt32(m_Num)].m_MapProj[i].Name, "Commands");
                                        Logging.WriteLog("X: " + maps[ToInt32(m_Num)].m_MapProj[i].X, "Commands");
                                        Logging.WriteLog("Y: " + maps[ToInt32(m_Num)].m_MapProj[i].Y, "Commands");
                                        Logging.WriteLog("Direction: " + maps[ToInt32(m_Num)].m_MapProj[i].Direction, "Commands");
                                        Logging.WriteLog("Damage: " + maps[ToInt32(m_Num)].m_MapProj[i].Damage, "Commands");
                                        Logging.WriteLog("Range: " + maps[ToInt32(m_Num)].m_MapProj[i].Range, "Commands");
                                        Logging.WriteLog("Sprite: " + maps[ToInt32(m_Num)].m_MapProj[i].Sprite, "Commands");
                                        Logging.WriteLog("Owner:" + players[maps[ToInt32(m_Num)].m_MapProj[i].Owner].Name, "Commands");
                                        Logging.WriteLog("Type: " + maps[ToInt32(m_Num)].m_MapProj[i].Type, "Commands");
                                        Logging.WriteLog("Speed: " + maps[ToInt32(m_Num)].m_MapProj[i].Speed, "Commands");
                                    }
                                }
                                Logging.WriteMessageLog("Projectiles written to file..check log..");
                            }
                        }

                        Logging.WriteMessageLogLine("Would you like to reset them? (y/n): ", "Commands");
                        string reset = Console.ReadLine();
                        if (reset.ToLower() == "y")
                        {
                            for (int i = 0; i < MAX_MAP_PROJECTILES; i++)
                            {
                                maps[ToInt32(m_Num)].m_MapProj[i] = null;
                            }
                            Logging.WriteMessageLog("Projectiles reset...", "Commands");
                        }
                        break;

                    case "minlat":
                        Logging.WriteMessageLogLine("Set latency too: ", "Commands");
                        string lat = Console.ReadLine();
                        int intseconds = ToInt32(lat);
                        string msec = lat.Insert(0, "0.0");
                        float delay = ToSingle(msec);

                        if (intseconds < 15 || intseconds > 150) { Logging.WriteMessageLog("Invalid command format: > 15 and < 150", "Commands"); return; }

                        SabertoothServer.netServer.Configuration.SimulatedMinimumLatency = delay;
                        Logging.WriteMessageLog("Minimum latency is now " + lat + "ms");
                        break;

                    case "help":    //Help command which displays all commands, modifiers, and possible arguments
                        Logging.WriteMessageLog("Commands:", "Commands");
                        Logging.WriteMessageLog("info - shows the servers stats", "Commands");
                        Logging.WriteMessageLog("uptime - shows server uptime.", "Commands");
                        Logging.WriteMessageLog("saveall - saves all players", "Commands");
                        Logging.WriteMessageLog("minlat - sets latency", "Commands");
                        Logging.WriteMessageLog("execsql - executes a sql script", "Commands");
                        Logging.WriteMessageLog("activeacc - actives account with name provided", "Commands");
                        Logging.WriteMessageLog("projcheck - follow prompts to check map projectiles", "Commands");
                        Logging.WriteMessageLog("createaccount - follow prompts to create an account", "Commands");
                        Logging.WriteMessageLog("shutdown - shuts down the server", "Commands");
                        Logging.WriteMessageLog("exit - shuts down the server", "Commands");
                        break;

                    default:    //If you entered something that wasnt a command or pure garbage
                        if (!isDynamic) { Logging.WriteMessageLog("Please enter a valid command!", "Commands"); }                        
                        break;
                }
                #endregion
            }
        }

        static int CalculateFrameRate()
        {
            if (TickCount - lastTick >= A_MILLISECOND)
            {
                lastFrameRate = frameRate;
                frameRate = 0;
                lastTick = TickCount;
            }
            frameRate++;
            return lastFrameRate;
        }
        #endregion

        #region Server Voids
        static void SaveAll()
        {
            Logging.WriteMessageLog("Saving players...");
            for (int i = 0; i < MAX_PLAYERS; i++)
            {
                if (players[i].Name != null)
                {
                    players[i].SavePlayerToDatabase();
                }
            }
            Logging.WriteMessageLog("Players saved!");
        }

        static bool SavePlayers()
        {
            if (TickCount - saveTick < saveTime) { return false; }
            Logging.WriteMessageLog("Saving players...");
            for (int i = 0; i < MAX_PLAYERS; i++)
            {
                if (players[i].Name != null)
                {
                    players[i].SavePlayerToDatabase();
                }
            }
            saveTick = TickCount;
            Logging.WriteMessageLog("Players saved successfully!");
            return true;
        }

        static bool AccountExist(string name)
        {
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                string command;
                command = "SELECT * FROM PLAYERS WHERE NAME=@name";
                using (var cmd = new SqlCommand(command, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = name;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader[1].ToString().ToLower() == name.ToLower())
                            {
                                return true;
                            }
                        }
                        return false;
                    }
                }
            }
        }

        public static int FindOpenMapItemSlot(Map s_Map)
        {
            for (int i = 0; i < MAX_MAP_ITEMS; i++)
            {
                if (s_Map.m_MapItem[i].Name == "None" && !s_Map.m_MapItem[i].IsSpawned || s_Map.m_MapItem[i].Name == null && !s_Map.m_MapItem[i].IsSpawned)
                {
                    return i;
                }
            }
            return MAX_MAP_ITEMS;
        }

        public static void DisconnectClients()
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();

            outMSG.Write((byte)PacketTypes.Shutdown);
            SabertoothServer.netServer.SendToAll(outMSG, NetDeliveryMethod.Unreliable);
        }

        public static void SaveConfiguration()
        {
            XmlWriterSettings userData = new XmlWriterSettings
            {
                Indent = true
            };
            XmlWriter writer = XmlWriter.Create("Config.xml", userData);
            Logging.WriteMessageLog("Config XML file saved.");
            writer.WriteStartDocument();
            //writer.WriteComment("This file is generated by the server.");
            writer.WriteStartElement("ConfigData");
            writer.WriteElementString("SQLServer", SQL_SERVER_NAME);
            writer.WriteElementString("Database", SQL_SERVER_DATABASE);
            writer.WriteElementString("Version", VERSION);
            writer.WriteElementString("RegenTime", HEALTH_REGEN_TIME);
            writer.WriteElementString("HungerTime", HUNGER_DEGEN_TIME);
            writer.WriteElementString("HydrationTime", HYDRATION_DEGEN_TIME);
            writer.WriteElementString("SaveTime", AUTOSAVE_TIME);
            writer.WriteElementString("SpawnTime", SPAWN_TIME);
            writer.WriteElementString("AiTime", AI_TIME);
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }

        public static void LoadConfiguration()
        {
            if (!Exists("Config.xml"))
            {
                SaveConfiguration();
                Logging.WriteMessageLog("Creating config XML...");
            }

            Logging.WriteMessageLog("Loading config XML...");
            XmlReader reader = XmlReader.Create("Config.xml");
            Logging.WriteMessageLog("Config XML file found, importing settings...");
            reader.ReadToFollowing("SQLServer");
            sqlServer = reader.ReadElementContentAsString();
            reader.ReadToFollowing("Database");
            sqlDatabase = reader.ReadElementContentAsString();
            reader.ReadToFollowing("Version");
            sVersion = reader.ReadElementContentAsString();
            reader.ReadToFollowing("RegenTime");
            regenTime = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("HungerTime");
            hungerTime = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("HydrationTime");
            hydrationTime = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("SaveTime");
            saveTime = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("SpawnTime");
            spawnTime = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("AiTime");
            aiTime = reader.ReadElementContentAsInt();
            reader.Close();
            Logging.WriteMessageLog("Config XML imported successfully!");
        }

        static void UpdateTitle()
        {
            Console.Title = "Sabertooth Server - " + worldTime.Time;
        }

        private static string GetPublicIPAddress()
        {
            try
            {
                string externalIP;
                externalIP = (new WebClient()).DownloadString("http://checkip.dyndns.org/");
                externalIP = (new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}")).Matches(externalIP)[0].ToString();
                return externalIP;
            }
            catch { return null; }
        }
        #endregion
    }
}