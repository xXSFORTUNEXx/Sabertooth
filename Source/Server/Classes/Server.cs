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
        public static Spell[] spells = new Spell[MAX_SPELLS];
        public static Quests[] quests = new Quests[MAX_QUESTS];
        public static Animation[] animations = new Animation[MAX_ANIMATIONS];
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
        private static int mregenTick;
        private static int mregenTime;
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
        static int checkitemcdTick;
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
                //CheckHealthRegen();
                CheckManaRegen();
                CheckNpcAi();
                CheckItemCoolDowns();
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
                for (int n = 0; n < MAX_MAP_NPCS; n++)
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

            #region Animations
            Logging.WriteMessageLog("Loading animations...");
            for (int i = 0; i < MAX_ANIMATIONS; i++)
            {
                animations[i] = new Animation();
                animations[i].LoadAnimationFromDatabase(i + 1);
            }
            Logging.WriteMessageLog("Animations loaded successfully");
            #endregion

            #region Spells
            Logging.WriteMessageLog("Loading spells...");
            for (int i = 0; i < MAX_SPELLS; i++)
            {
                spells[i] = new Spell();
                spells[i].LoadSpellFromDatabase(i + 1);
            }
            Logging.WriteMessageLog("Spells loaded successfully");
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
                        script += ReadAllText("SQL Scripts/Animation.sql");
                        script += ReadAllText("SQL Scripts/Spells.sql");
                        script += ReadAllText("SQL Scripts/SpellBook.sql");
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

        //Check if hp needs some regen
        static bool CheckHealthRegen()
        {
            if (TickCount - regenTick < regenTime) { return false; }

            for (int i = 0; i < MAX_PLAYERS; i++)
            {
                if (players[i].Name != null)
                {
                    Logging.WriteMessageLog("Checking for health regin...");

                    players[i].RegenHealth();
                    HandleData.SendUpdateHealthData(i, players[i].Health, -1);
                }
            }
            regenTick = TickCount;
            return true;
        }

        //Check if mp needs some regen
        static bool CheckManaRegen()
        {
            if (TickCount - mregenTick < mregenTime) { return false; }

            for (int i = 0; i < MAX_PLAYERS; i++)
            {
                if (players[i].Name != null)
                {
                    Logging.WriteMessageLog("Checking for mana regin...");

                    players[i].RegainMana();
                    HandleData.SendUpdateManaData(i, players[i].Mana);
                }
            }
            mregenTick = TickCount;
            return true;
        }
        
        //Check if a map has players
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

        //Check if npcs need to be spawned
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
                                }
                            }
                        }
                    }
                }
            }
        }

        //Run the AI for players who need it
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
                    }
                }
                aiTick = TickCount;
            }
        }

        //Check what is off cooldown for items
        static void CheckItemCoolDowns()
        {
            if (TickCount - checkitemcdTick < A_MILLISECOND) { return; }
            
            for (int p = 0; p < MAX_PLAYERS; p++)
            {
                if (players[p].Connection != null & players[p].Name != "")
                {
                    for (int i = 0; i < MAX_INV_SLOTS; i++)
                    {
                        if (players[p].Backpack[i] != null && players[p].Backpack[i].Name != "None")
                        {
                            if (players[p].Backpack[i].OnCoolDown)
                            {
                                if (players[p].Backpack[i].cooldownTick != 0)
                                {
                                    if (TickCount - players[p].Backpack[i].cooldownTick > (players[p].Backpack[i].CoolDown * A_MILLISECOND))
                                    {
                                        players[p].Backpack[i].OnCoolDown = false;
                                        HandleData.SendItemCoolDownUpdate(players[p].Connection, p, i, false);
                                        players[p].Backpack[i].cooldownTick = 0;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            checkitemcdTick = TickCount;
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
                        Logging.WriteMessageLog("Public IP Address: " + GetPublicIPAddress(), "Commands");
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
                            mregenTime = ToInt32(reader[i]); i += 1;
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