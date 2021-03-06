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
using System.Diagnostics;
using System.ComponentModel;

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
            Server.LoadConfigFromDatabase();
            Server.LoadVersionFromDatabase();
            netServer = new NetServer(netConfig);
            netServer.Start();            
            Logging.WriteMessageLog("Network configuration complete...");
            Server.ServerLoop();
            Server.DisconnectClients();
            netServer.Shutdown("Exiting");
            Logging.WriteMessageLog("Shutting down...");
            Thread.Sleep(500);
            Exit(0);
        }
    }

    public static class Server
    {
        #region Classes
        public static Player[] players = new Player[MAX_PLAYERS];
        public static Npc[] npcs = new Npc[MAX_NPCS];
        public static Item[] items = new Item[MAX_ITEMS];        
        public static Map[] maps = new Map[MAX_MAPS];
        public static Shop[] shops = new Shop[MAX_SHOPS];
        public static Chat[] chats = new Chat[MAX_CHATS];
        public static Chest[] chests = new Chest[MAX_CHESTS];
        public static Quests[] quests = new Quests[MAX_QUESTS];
        public static WorldTime worldTime = new WorldTime();
        public static Instance instance = new Instance();
        public static Random RND = new Random();
        public static Thread commandThread;
        #endregion

        #region Variables
        public static bool isRunning;
        private static int saveTick;
        private static int aiTick;
        private static int regenTick;
        private static int regenTime;
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
        static int usPid;
        #endregion

        public static void ServerLoop()
        {
            InitArrays();

            commandThread = new Thread(() => CommandWindow());
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
                CheckNpcAi();
                UpTime();
                fps = CalculateFrameRate();
                Thread.Sleep(1);
            }
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

            #region Quests
            Logging.WriteMessageLog("Loading quests...");
            for (int i = 0; i < MAX_QUESTS; i++)
            {
                quests[i] = new Quests();
                quests[i].LoadQuestFromDatabase(i + 1);
            }
            Logging.WriteMessageLog("Quests loaded successfully");
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
        public static bool CheckSQLConnection()
        {
            string connection = "Data Source=" + sqlServer + ";Integrated Security=True";
            string script = ReadAllText("SQL Scripts/Database.sql");
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
                return true;
            }
            catch (Exception e)
            {
                Logging.WriteMessageLog("Error esablishing SQL connection, Check log for details...", "SQL");
                Logging.WriteLog(e.Message, "SQL");
                Exit(0);
            }
            return false;
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
                        script = ReadAllText("SQL Scripts/Players.sql");
                        script += ReadAllText("SQL Scripts/Main_Weapons.sql");
                        script += ReadAllText("SQL Scripts/Secondary_Weapons.sql");
                        script += ReadAllText("SQL Scripts/Equipment.sql");
                        script += ReadAllText("SQL Scripts/Inventory.sql");
                        script += ReadAllText("SQL Scripts/Bank.sql");
                        script += ReadAllText("SQL Scripts/Items.sql");
                        script += ReadAllText("SQL Scripts/Npcs.sql");
                        script += ReadAllText("SQL Scripts/Shops.sql");
                        script += ReadAllText("SQL Scripts/Chat.sql");
                        script += ReadAllText("SQL Scripts/Quests.sql");
                        script += ReadAllText("SQL Scripts/Maps.sql");
                        script += ReadAllText("SQL Scripts/Chests.sql");
                        script += ReadAllText("SQL Scripts/Stats.sql");
                        script += ReadAllText("SQL Scripts/Quest_List.sql");
                        script += ReadAllText("SQL Scripts/Version.sql");
                        script += ReadAllText("SQL Scripts/Configuration.sql");
                        script += ReadAllText("SQL Scripts/Hotbar.sql");
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
                                                maps[i].m_MapItem[slot].HealthRestore = items[itemNum].HealthRestore;
                                                maps[i].m_MapItem[slot].ManaRestore = items[itemNum].ManaRestore;
                                                maps[i].m_MapItem[slot].Strength = items[itemNum].Strength;
                                                maps[i].m_MapItem[slot].Agility = items[itemNum].Agility;
                                                maps[i].m_MapItem[slot].Intelligence = items[itemNum].Intelligence;
                                                maps[i].m_MapItem[slot].Energy = items[itemNum].Energy;
                                                maps[i].m_MapItem[slot].Stamina = items[itemNum].Stamina;
                                                maps[i].m_MapItem[slot].Price = items[itemNum].Price;
                                                maps[i].m_MapItem[slot].Value = maps[i].Ground[x, y].SpawnAmount;
                                                maps[i].m_MapItem[slot].Rarity = items[itemNum].Rarity;
                                                maps[i].m_MapItem[slot].CoolDown = items[itemNum].CoolDown;
                                                maps[i].m_MapItem[slot].AddMaxHealth = items[itemNum].AddMaxHealth;
                                                maps[i].m_MapItem[slot].AddMaxMana = items[itemNum].AddMaxMana;
                                                maps[i].m_MapItem[slot].BonusXP = items[itemNum].BonusXP;
                                                maps[i].m_MapItem[slot].SpellNum = items[itemNum].SpellNum;
                                                maps[i].m_MapItem[slot].Stackable = items[itemNum].Stackable;
                                                maps[i].m_MapItem[slot].MaxStack = items[itemNum].MaxStack;
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
            string input = "";
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

                    case "updateserver":
                        Logging.WriteMessageLog("Launching update server...Please wait...");
                        LaunchUpdateServer();
                        Logging.WriteMessageLog("Update server launched successfully...Check window for status...");
                        break;

                    case "closeuserver":
                        Logging.WriteMessageLog("Closing update server...");
                        CloseUpdateServer();
                        Logging.WriteMessageLog("Update server closed successfully!");
                        break;

                    case "info":
                        string hostName = Dns.GetHostName();
                        float latency = SabertoothServer.netServer.Configuration.SimulatedMinimumLatency;
                        Logging.WriteMessageLog("Statistics: ", "Commands");
                        Logging.WriteMessageLog("Version: " + sVersion, "Commands");
                        Logging.WriteMessageLog(upTime, "Commands");                        
                        Logging.WriteMessageLog("CPS: " + fps, "Commands");
                        Logging.WriteMessageLog("Public IP Address: " + GetPublicIPAddress());
                        Logging.WriteMessageLog("Host Name: " + hostName, "Commands");
                        Logging.WriteMessageLog("Local IP Address: " + NetUtility.Resolve(hostName), "Commands");
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
                            Player c_Player = new Player(name, pass, email, 0, 0, 0, 0, 0, 1, 100, 100, 100, 100, 100, 0, 100, 1, 1, 1, 1, 1, 0);
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
                        
                    case "minlat":
                        try
                        {
                            Logging.WriteMessageLogLine("Set latency too: ", "Commands");
                            string lat = Console.ReadLine();
                            int intseconds = ToInt32(lat);
                            string msec = lat.Insert(0, "0.0");
                            float delay = ToSingle(msec);

                            if (intseconds < 15 || intseconds > 150) { Logging.WriteMessageLog("Invalid command format: > 15 and < 150", "Commands"); break; }

                            SabertoothServer.netServer.Configuration.SimulatedMinimumLatency = delay;
                            Logging.WriteMessageLog("Minimum latency is now " + lat + "ms");
                        }
                        catch (Exception e)
                        {
                            Logging.WriteMessageLog(e.Message);
                        }
                        break;

                    case "settime":
                        try
                        {
                            Logging.WriteMessageLog("Format: MMM dd, yyyy hh:mm:ss tt");
                            Logging.WriteMessageLogLine("Second(0-59): ");
                            string second = Console.ReadLine();
                            worldTime.g_Second = ToInt32(second);

                            Logging.WriteMessageLogLine("Minute(0-59): ");
                            string minute = Console.ReadLine();
                            worldTime.g_Minute = ToInt32(minute);

                            Logging.WriteMessageLogLine("Hour(0-24): ");
                            string hour = Console.ReadLine();
                            worldTime.g_Hour = ToInt32(hour);

                            Logging.WriteMessageLogLine("Day(1-31): ");
                            string day = Console.ReadLine();
                            worldTime.g_DayOfWeek = ToInt32(day);

                            Logging.WriteMessageLogLine("Month(1-12): ");
                            string month = Console.ReadLine();
                            worldTime.g_Month = ToInt32(month);

                            Logging.WriteMessageLogLine("Year(1-5000): ");
                            string year = Console.ReadLine();
                            worldTime.g_Year = ToInt32(year);

                            Thread.Sleep(1000);
                            Logging.WriteMessageLog("Time set to: " + worldTime.Time);

                            for (int i = 0; i < MAX_PLAYERS; i++)
                            {
                                if (players[i].Name != null && players[i].Connection.Status == NetConnectionStatus.Connected)
                                {
                                    HandleData.UpdatePlayersWithTimeChange(players[i].Connection, i);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Logging.WriteMessageLog(e.Message);
                        }
                        break;

                    case "randomtime":
                        worldTime.g_Second = RND.Next(0, 59);
                        worldTime.g_Minute = RND.Next(0, 59);
                        worldTime.g_Hour = RND.Next(0, 24);
                        worldTime.g_DayOfWeek = RND.Next(1, 31);
                        worldTime.g_Month = RND.Next(1, 12);
                        worldTime.g_Year = RND.Next(0, 5000);

                        Thread.Sleep(1000);
                        Logging.WriteMessageLog("Time set to: " + worldTime.Time);

                        for (int i = 0; i < MAX_PLAYERS; i++)
                        {
                            if (players[i].Name != null && players[i].Connection.Status == NetConnectionStatus.Connected)
                            {
                                HandleData.UpdatePlayersWithTimeChange(players[i].Connection, i);
                            }
                        }
                        break;

                    case "help":    //Help command which displays all commands, modifiers, and possible arguments
                        Logging.WriteMessageLog("Commands:", "Commands");
                        Logging.WriteMessageLog("info - shows the servers stats", "Commands");
                        Logging.WriteMessageLog("uptime - shows server uptime.", "Commands");
                        Logging.WriteMessageLog("saveall - saves all players", "Commands");
                        Logging.WriteMessageLog("minlat - sets latency", "Commands");
                        Logging.WriteMessageLog("execsql - executes a sql script", "Commands");
                        Logging.WriteMessageLog("activeacc - actives account with name provided", "Commands");                        
                        Logging.WriteMessageLog("createaccount - follow prompts to create an account", "Commands");
                        Logging.WriteMessageLog("settime - follow prompts to set the games time", "Commands");
                        Logging.WriteMessageLog("randomtime - set the game time to a random value", "Commands");
                        Logging.WriteMessageLog("shutdown - shuts down the server", "Commands");
                        Logging.WriteMessageLog("updateserver - launches the update server", "Commands");
                        Logging.WriteMessageLog("closeuserver - closes the update server", "Commands");
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
            Logging.WriteMessageLog("Disconnecting clients...");
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();

            outMSG.Write((byte)PacketTypes.Shutdown);
            SabertoothServer.netServer.SendToAll(outMSG, NetDeliveryMethod.Unreliable);
            Thread.Sleep(2500);
        }

        public static void CloseUpdateServer()
        {
            try
            {
                Process.GetProcessById(usPid).Kill();
            }
            catch (Exception e)
            {
                Logging.WriteMessageLog(e.Message);
            }
        }

        public static void LaunchUpdateServer()
        {
            try
            {
                using (Process sServer = new Process())
                {
                    sServer.StartInfo.UseShellExecute = true;
                    sServer.StartInfo.FileName = "UpdateServer.exe";
                    sServer.Start();
                    usPid = sServer.Id;
                }
            }
            catch (Exception e)
            {
                Logging.WriteMessageLog(e.Message);
            }
        }

        public static void LoadConfigFromDatabase()
        {
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            string script = ReadAllText("SQL Data Scripts/Load_Config.sql");
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                int i;
                using (SqlCommand cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = 1;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            i = 1;
                            regenTime = ToInt32(reader[i]); i += 1;
                            saveTime = ToInt32(reader[i]); i += 1;
                            spawnTime = ToInt32(reader[i]); i += 1;
                            aiTime = ToInt32(reader[i]);
                        }
                    }
                }
            }
        }

        public static void LoadVersionFromDatabase()
        {
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            string script = ReadAllText("SQL Data Scripts/Load_Version.sql");
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                using (SqlCommand cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = 1;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sVersion = reader[1].ToString();
                        }
                    }
                }
            }
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
            writer.WriteStartElement("ConfigData");
            writer.WriteElementString("SQLServer", SQL_SERVER_NAME);
            writer.WriteElementString("Database", SQL_SERVER_DATABASE);
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
            reader.Close();
            Logging.WriteMessageLog("Config XML imported successfully!");
        }

        static void UpdateTitle()
        {
            Console.Title = "Sabertooth Server - " + worldTime.Time + " - CPS: " + fps + " - Version: " + sVersion;
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