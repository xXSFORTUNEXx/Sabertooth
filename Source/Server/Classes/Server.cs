using Lidgren.Network;
using System;
using System.Net;
using System.Threading;
using System.Xml;
using static System.Environment;
using static System.IO.File;
using static System.Convert;
using System.IO;
using System.Data.SQLite;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

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
                Port = Globals.SERVER_PORT,
                UseMessageRecycling = true,
                MaximumConnections = Globals.MAX_PLAYERS,
                EnableUPnP = false,
                ConnectionTimeout = Globals.CONNECTION_TIMEOUT,
                SimulatedRandomLatency = Globals.SIMULATED_RANDOM_LATENCY,
                SimulatedMinimumLatency = Globals.SIMULATED_MINIMUM_LATENCY,
                SimulatedLoss = Globals.SIMULATED_PACKET_LOSS,
                SimulatedDuplicatesChance = Globals.SIMULATED_DUPLICATES_CHANCE
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
            Server.CheckSQL();
            netServer = new NetServer(netConfig);
            netServer.Start();
            Logging.WriteMessageLog("Network configuration complete...");
            Server.ServerLoop();
        }
    }

    public static class Server
    {
        public static Player[] players = new Player[Globals.MAX_PLAYERS];
        public static Npc[] npcs = new Npc[Globals.MAX_NPCS];
        public static Item[] items = new Item[Globals.MAX_ITEMS];
        public static Projectile[] projectiles = new Projectile[Globals.MAX_PROJECTILES];
        public static Map[] maps = new Map[Globals.MAX_MAPS];
        public static Shop[] shops = new Shop[Globals.MAX_SHOPS];
        public static Chat[] chats = new Chat[Globals.MAX_CHATS];
        public static Chest[] chests = new Chest[Globals.MAX_CHESTS];
        public static WorldTime worldTime = new WorldTime();
        public static Random RND = new Random();
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
        public static string DBType;
        public static string sqlServer;
        public static string sqlDatabase;
        static int lastTick;
        static int lastFrameRate;
        static int frameRate;
        static int fps;

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
                Thread.Sleep(10);
            }
            DisconnectClients();
            Logging.WriteMessageLog("Disconnecting clients...");
            Thread.Sleep(2500);
            SabertoothServer.netServer.Shutdown("Shutting down");
            Logging.WriteMessageLog("Shutting down...");
            Thread.Sleep(500);
            Exit(0);
        }

        public static void CheckSQL()
        {
            //MSSQL Database (remote)
            if (Server.DBType == Globals.SQL_DATABASE_REMOTE.ToString())
            {
                try
                {
                    //string connection = "Data Source=" + Server.sqlServer + ";Initial Catalog=" + Server.sqlDatabase + ";Integrated Security=True";
                    string connection = "Data Source=" + Server.sqlServer + ";Integrated Security=True";
                    using (var sql = new SqlConnection(connection))
                    {
                        sql.Open();
                        Logging.WriteMessageLog("Established SQL Server connection!", "SQL");
                        string command = "IF DB_ID('Sabertooth') IS NULL CREATE DATABASE Sabertooth;";
                        using (var cmd = new SqlCommand(command, sql))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    CreateDatabase();
                }
                catch (Exception e)
                {
                    Logging.WriteMessageLog("Error esablishing SQL connection, Check log for details...", "SQL");
                    Logging.WriteLog(e.Message, "SQL");
                }
            }
            //SQLite Database (local)
            else
            {
                if (!Directory.Exists("Database"))
                {
                    Directory.CreateDirectory("Database");
                    CreateDatabase();
                }
                try
                {
                    string connection = "Data Source=Database/Sabertooth.db;Version=3;";
                    using (var sql = new SQLiteConnection(connection))
                    {
                        sql.Open();
                        Logging.WriteMessageLog("Established SQLite connection!", "SQL");
                    }
                }
                catch (Exception e)
                {
                    Logging.WriteMessageLog("Error esablishing SQL connection, Check log for details...", "SQL");
                    Logging.WriteLog(e.Message, "SQL");
                }
            }
        }

        public static void CreateDatabase()
        {
            //MSSQL Database (remote)
            if (Server.DBType == Globals.SQL_DATABASE_REMOTE.ToString())
            {
                string connection = "Data Source=" + Server.sqlServer + ";Initial Catalog=" + Server.sqlDatabase + ";Integrated Security=True";
                using (var sql = new SqlConnection(connection))
                {
                    sql.Open();
                    string command;
                    command = "IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'PLAYERS')";
                    command += "CREATE TABLE PLAYERS";
                    command += "(ID int IDENTITY(1,1) PRIMARY KEY,NAME VARCHAR(25), PASSWORD VARCHAR(25), X INTEGER, Y INTEGER, MAP INTEGER, DIRECTION INTEGER, AIMDIRECTION INTEGER,";
                    command += "SPRITE INTEGER, LEVEL INTEGER, POINTS INTEGER, HEALTH INTEGER, MAXHEALTH INTEGER, EXPERIENCE INTEGER, MONEY INTEGER, ARMOR INTEGER, HUNGER INTEGER,";
                    command += "HYDRATION INTEGER, STRENGTH INTEGER, AGILITY INTEGER, ENDURANCE INTEGER, STAMINA INTEGER, PISTOLAMMO INTEGER, ASSAULTAMMO INTEGER,";
                    command += "ROCKETAMMO INTEGER, GRENADEAMMO INTEGER, LIGHTRADIUS INTEGER, DAYS INTEGER, HOURS INTEGER, MINUTES INTEGER, SECONDS INTEGER, LDAYS INTEGER, LHOURS INTEGER, LMINUTES INTEGER, LSECONDS INTEGER,";
                    command += "LLDAYS INTEGER, LLHOURS INTEGER, LLMINUTES INTEGER, LLSECONDS INTEGER, LASTLOGGED TEXT)";
                    command += "IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'MAINWEAPONS')";
                    command += "CREATE TABLE MAINWEAPONS";
                    command += "(ID int IDENTITY(1,1) PRIMARY KEY, OWNER VARCHAR(25), NAME TEXT, CLIP INTEGER, MAXCLIP INTEGER, SPRITE INTEGER, DAMAGE INTEGER, ARMOR INTEGER, TYPE INTEGER, ATTACKSPEED INTEGER, RELOADSPEED INTEGER,";
                    command += "HEALTHRESTORE INTEGER, HUNGERRESTORE INTEGER, HYDRATERESTORE INTEGER, STRENGTH INTEGER, AGILITY INTEGER, ENDURANCE INTEGER, STAMINA INTEGER, AMMOTYPE INTEGER, VALUE INTEGER,";
                    command += "PROJ INTEGER, PRICE INTEGER, RARITY INTEGER)";
                    command += "IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'SECONDARYWEAPONS')";
                    command += "CREATE TABLE SECONDARYWEAPONS";
                    command += "(ID int IDENTITY(1,1) PRIMARY KEY, OWNER VARCHAR(25), NAME TEXT, CLIP INTEGER, MAXCLIP INTEGER, SPRITE INTEGER, DAMAGE INTEGER, ARMOR INTEGER, TYPE INTEGER, ATTACKSPEED INTEGER, RELOADSPEED INTEGER,";
                    command += "HEALTHRESTORE INTEGER, HUNGERRESTORE INTEGER, HYDRATERESTORE INTEGER, STRENGTH INTEGER, AGILITY INTEGER, ENDURANCE INTEGER, STAMINA INTEGER, AMMOTYPE INTEGER, VALUE INTEGER,";
                    command += "PROJ INTEGER, PRICE INTEGER, RARITY INTEGER)";
                    command += "IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'EQUIPMENT')";
                    command += "CREATE TABLE EQUIPMENT";
                    command += "(ID int IDENTITY(1,1) PRIMARY KEY, OWNER VARCHAR(25), SLOT INTEGER, NAME TEXT, SPRITE INTEGER, DAMAGE INTEGER, ARMOR INTEGER, TYPE INTEGER, ATTACKSPEED INTEGER, RELOADSPEED INTEGER, HEALTHRESTORE INTEGER, HUNGERRESTORE INTEGER,";
                    command += "HYDRATERESTORE INTEGER, STRENGTH INTEGER, AGILITY INTEGER, ENDURANCE INTEGER, STAMINA INTEGER, CLIP INTEGER, MAXCLIP INTEGER, AMMOTYPE INTEGER, VALUE INTEGER,";
                    command += "PROJ INTEGER, PRICE INTEGER, RARITY INTEGER)";
                    command += "IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'INVENTORY')";
                    command += "CREATE TABLE INVENTORY";
                    command += "(ID int IDENTITY(1,1) PRIMARY KEY, OWNER VARCHAR(25), SLOT INTEGER, NAME TEXT, SPRITE INTEGER, DAMAGE INTEGER, ARMOR INTEGER, TYPE INTEGER, ATTACKSPEED INTEGER, RELOADSPEED INTEGER, HEALTHRESTORE INTEGER, HUNGERRESTORE INTEGER,";
                    command += "HYDRATERESTORE INTEGER, STRENGTH INTEGER, AGILITY INTEGER, ENDURANCE INTEGER, STAMINA INTEGER, CLIP INTEGER, MAXCLIP INTEGER, AMMOTYPE INTEGER, VALUE INTEGER,";
                    command += "PROJ INTEGER, PRICE INTEGER, RARITY INTEGER)";
                    command += "IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'BANK')";
                    command += "CREATE TABLE BANK";
                    command += "(ID int IDENTITY(1,1) PRIMARY KEY, OWNER VARCHAR(25), SLOT INTEGER, NAME TEXT, SPRITE INTEGER, DAMAGE INTEGER, ARMOR INTEGER, TYPE INTEGER, ATTACKSPEED INTEGER, RELOADSPEED INTEGER, HEALTHRESTORE INTEGER, HUNGERRESTORE INTEGER,";
                    command += "HYDRATERESTORE INTEGER, STRENGTH INTEGER, AGILITY INTEGER, ENDURANCE INTEGER, STAMINA INTEGER, CLIP INTEGER, MAXCLIP INTEGER, AMMOTYPE INTEGER, VALUE INTEGER,";
                    command += "PROJ INTEGER, PRICE INTEGER, RARITY INTEGER)";
                    command += "IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'ITEMS')";
                    command += "CREATE TABLE ITEMS";
                    command += "(ID int IDENTITY(1,1) PRIMARY KEY,NAME TEXT, SPRITE INTEGER, DAMAGE INTEGER, ARMOR INTEGER, TYPE INTEGER, ATTACKSPEED INTEGER, RELOADSPEED INTEGER, HEALTHRESTORE INTEGER, HUNGERRESTORE INTEGER,";
                    command += "HYDRATERESTORE INTEGER, STRENGTH INTEGER, AGILITY INTEGER, ENDURANCE INTEGER, STAMINA INTEGER, CLIP INTEGER, MAXCLIP INTEGER, AMMOTYPE INTEGER, VALUE INTEGER,";
                    command += "PROJ INTEGER, PRICE INTEGER, RARITY INTEGER)";
                    command += "IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'NPCS')";
                    command += "CREATE TABLE NPCS";
                    command += "(ID int IDENTITY(1,1) PRIMARY KEY,NAME TEXT, X INTEGER, Y INTEGER, DIRECTION INTEGER, SPRITE INTEGER, STEP INTEGER, OWNER INTEGER, BEHAVIOR INTEGER, SPAWNTIME INTEGER, HEALTH INTEGER, MAXHEALTH INTEGER, DAMAGE INTEGER, DESX INTEGER, DESY INTEGER,";
                    command += "EXP INTEGER, MONEY INTEGER, RANGE INTEGER, SHOPNUM INTEGER, CHATNUM INTEGER)";
                    command += "IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'PROJECTILES')";
                    command += "CREATE TABLE PROJECTILES";
                    command += "(ID int IDENTITY(1,1) PRIMARY KEY,NAME TEXT, DAMAGE INTEGER, RANGE INTEGER, SPRITE INTEGER, TYPE INTEGER, SPEED INTEGER)";
                    command += "IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'SHOPS')";
                    command += "CREATE TABLE SHOPS";
                    command += "(ID int IDENTITY(1,1) PRIMARY KEY,NAME TEXT, ITEMDATA VARBINARY(MAX))";
                    command += "IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'CHAT')";
                    command += "CREATE TABLE CHAT";
                    command += "(ID int IDENTITY(1,1) PRIMARY KEY,NAME TEXT,MAINMESSAGE TEXT,OPTIONA TEXT,OPTIONB TEXT,OPTIONC TEXT,OPTIOND TEXT,NEXTCHATA INTEGER,NEXTCHATB INTEGER,NEXTCHATC INTEGER,NEXTCHATD INTEGER,SHOPNUM INTEGER,MISSIONNUM INTEGER,ITEMA INTEGER,ITEMB INTEGER,ITEMC INTEGER,VALA INTEGER,";
                    command += "VALB INTEGER,VALC INTEGER,MONEY INTEGER,TYPE INTEGER)";
                    command += "IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'MAPS')";
                    command += "CREATE TABLE MAPS";
                    command += "(ID int IDENTITY(1,1) PRIMARY KEY,NAME TEXT,REVISION INTEGER,UP INTEGER,DOWN INTEGER,LEFTSIDE INTEGER,RIGHTSIDE INTEGER,BRIGHTNESS INTEGER,NPC VARBINARY(MAX),ITEM VARBINARY(MAX), GROUND VARBINARY(MAX),MASK VARBINARY(MAX),MASKA VARBINARY(MAX),FRINGE VARBINARY(MAX),FRINGEA VARBINARY(MAX))";
                    command += "IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'CHESTS')";
                    command += "CREATE TABLE CHESTS";
                    command += "(ID int IDENTITY(1,1) PRIMARY KEY,NAME TEXT,MONEY INTEGER,EXPERIENCE INTEGER,REQUIREDLEVEL INTEGER,TRAPLEVEL INTEGER,REQKEY INTEGER,DAMAGE INTEGER,NPCSPAWN INTEGER,SPAWNAMOUNT INTEGER,CHESTITEM VARBINARY(MAX))";

                    using (var cmd = new SqlCommand(command, sql))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            //SQLite Database (local)
            else
            {
                using (var conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
                {
                    using (var cmd = new SQLiteCommand(conn))
                    {
                        conn.Open();
                        string command;

                        command = "CREATE TABLE `PLAYERS`";
                        command = command + "(`NAME` TEXT, `PASSWORD` TEXT, `X` INTEGER, `Y` INTEGER, `MAP` INTEGER, `DIRECTION` INTEGER, `AIMDIRECTION` INTEGER, ";
                        command = command + "`SPRITE` INTEGER, `LEVEL` INTEGER, `POINTS` INTEGER, `HEALTH` INTEGER, `MAXHEALTH` INTEGER, `EXPERIENCE` INTEGER, `MONEY` INTEGER, `ARMOR` INTEGER, `HUNGER` INTEGER, ";
                        command = command + "`HYDRATION` INTEGER, `STRENGTH` INTEGER, `AGILITY` INTEGER, `ENDURANCE` INTEGER, `STAMINA` INTEGER, `PISTOLAMMO` INTEGER, `ASSAULTAMMO` INTEGER, ";
                        command = command + "`ROCKETAMMO` INTEGER, `GRENADEAMMO` INTEGER, `LIGHTRADIUS` INTEGER, `DAYS` INTEGER, `HOURS` INTEGER, `MINUTES` INTEGER, `SECONDS` INTEGER, `LDAYS` INTEGER, `LHOURS` INTEGER, `LMINUTES` INTEGER, `LSECONDS` INTEGER, ";
                        command = command + "`LLDAYS` INTEGER, `LLHOURS` INTEGER, `LLMINUTES` INTEGER, `LLSECONDS` INTEGER, `LASTLOGGED` INTEGER)";
                        cmd.CommandText = command;
                        cmd.ExecuteNonQuery();

                        command = "CREATE TABLE `MAINWEAPONS`";
                        command = command + "(`OWNER` TEXT, `NAME` TEXT, `CLIP` INTEGER, `MAXCLIP` INTEGER, `SPRITE` INTEGER, `DAMAGE` INTEGER, `ARMOR` INTEGER, `TYPE` INTEGER, `ATTACKSPEED` INTEGER, `RELOADSPEED` INTEGER, ";
                        command = command + "`HEALTHRESTORE` INTEGER, `HUNGERRESTORE` INTEGER, `HYDRATERESTORE` INTEGER, `STRENGTH` INTEGER, `AGILITY` INTEGER, `ENDURANCE` INTEGER, `STAMINA` INTEGER, `AMMOTYPE` INTEGER, `VALUE` INTEGER, ";
                        command = command + "`PROJ` INTEGER, `PRICE` INTEGER, `RARITY` INTEGER)";
                        cmd.CommandText = command;
                        cmd.ExecuteNonQuery();

                        command = "CREATE TABLE `SECONDARYWEAPONS`";
                        command = command + "(`OWNER` TEXT, `NAME` TEXT, `CLIP` INTEGER, `MAXCLIP` INTEGER, `SPRITE` INTEGER, `DAMAGE` INTEGER, `ARMOR` INTEGER, `TYPE` INTEGER, `ATTACKSPEED` INTEGER, `RELOADSPEED` INTEGER, ";
                        command = command + "`HEALTHRESTORE` INTEGER, `HUNGERRESTORE` INTEGER, `HYDRATERESTORE` INTEGER, `STRENGTH` INTEGER, `AGILITY` INTEGER, `ENDURANCE` INTEGER, `STAMINA` INTEGER, `AMMOTYPE` INTEGER, `VALUE` INTEGER, ";
                        command = command + "`PROJ` INTEGER, `PRICE` INTEGER, `RARITY` INTEGER)";
                        cmd.CommandText = command;
                        cmd.ExecuteNonQuery();

                        command = "CREATE TABLE `EQUIPMENT`";
                        command = command + "(`OWNER` TEXT, `ID` INTEGER, `NAME` TEXT, `SPRITE` INTEGER, `DAMAGE` INTEGER, `ARMOR` INTEGER, `TYPE` INTEGER, `ATTACKSPEED` INTEGER, `RELOADSPEED` INTEGER, `HEALTHRESTORE` INTEGER, `HUNGERRESTORE` INTEGER, ";
                        command = command + "`HYDRATERESTORE` INTEGER, `STRENGTH` INTEGER, `AGILITY` INTEGER, `ENDURANCE` INTEGER, `STAMINA` INTEGER, `CLIP` INTEGER, `MAXCLIP` INTEGER, `AMMOTYPE` INTEGER, `VALUE` INTEGER, ";
                        command = command + "`PROJ` INTEGER, `PRICE` INTEGER, `RARITY` INTEGER)";
                        cmd.CommandText = command;
                        cmd.ExecuteNonQuery();

                        command = "CREATE TABLE `INVENTORY`";
                        command = command + "(`OWNER` TEXT, `ID` INTEGER, `NAME` TEXT, `SPRITE` INTEGER, `DAMAGE` INTEGER, `ARMOR` INTEGER, `TYPE` INTEGER, `ATTACKSPEED` INTEGER, `RELOADSPEED` INTEGER, `HEALTHRESTORE` INTEGER, `HUNGERRESTORE` INTEGER, ";
                        command = command + "`HYDRATERESTORE` INTEGER, `STRENGTH` INTEGER, `AGILITY` INTEGER, `ENDURANCE` INTEGER, `STAMINA` INTEGER, `CLIP` INTEGER, `MAXCLIP` INTEGER, `AMMOTYPE` INTEGER, `VALUE` INTEGER, ";
                        command = command + "`PROJ` INTEGER, `PRICE` INTEGER, `RARITY` INTEGER)";
                        cmd.CommandText = command;
                        cmd.ExecuteNonQuery();

                        command = "CREATE TABLE `BANK`";
                        command = command + "(`OWNER` TEXT, `ID` INTEGER, `NAME` TEXT, `SPRITE` INTEGER, `DAMAGE` INTEGER, `ARMOR` INTEGER, `TYPE` INTEGER, `ATTACKSPEED` INTEGER, `RELOADSPEED` INTEGER, `HEALTHRESTORE` INTEGER, `HUNGERRESTORE` INTEGER, ";
                        command = command + "`HYDRATERESTORE` INTEGER, `STRENGTH` INTEGER, `AGILITY` INTEGER, `ENDURANCE` INTEGER, `STAMINA` INTEGER, `CLIP` INTEGER, `MAXCLIP` INTEGER, `AMMOTYPE` INTEGER, `VALUE` INTEGER, ";
                        command = command + "`PROJ` INTEGER, `PRICE` INTEGER, `RARITY` INTEGER)";
                        cmd.CommandText = command;
                        cmd.ExecuteNonQuery();

                        command = "CREATE TABLE `ITEMS`";
                        command = command + "(`NAME` TEXT, `SPRITE` INTEGER, `DAMAGE` INTEGER, `ARMOR` INTEGER, `TYPE` INTEGER, `ATTACKSPEED` INTEGER, `RELOADSPEED` INTEGER, `HEALTHRESTORE` INTEGER, `HUNGERRESTORE` INTEGER, ";
                        command = command + "`HYDRATERESTORE` INTEGER, `STRENGTH` INTEGER, `AGILITY` INTEGER, `ENDURANCE` INTEGER, `STAMINA` INTEGER, `CLIP` INTEGER, `MAXCLIP` INTEGER, `AMMOTYPE` INTEGER, `VALUE` INTEGER, ";
                        command = command + "`PROJ` INTEGER, `PRICE` INTEGER, `RARITY` INTEGER)";
                        cmd.CommandText = command;
                        cmd.ExecuteNonQuery();

                        command = "CREATE TABLE `NPCS`";
                        command = command + "(`NAME` TEXT, `X` INTEGER, `Y` INTEGER, `DIRECTION` INTEGER, `SPRITE` INTEGER, `STEP` INTEGER, `OWNER` INTEGER, `BEHAVIOR` INTEGER, `SPAWNTIME` INTEGER, `HEALTH` INTEGER, `MAXHEALTH` INTEGER, `DAMAGE` INTEGER, `DESX` INTEGER, `DESY` INTEGER, ";
                        command = command + "`EXP` INTEGER, `MONEY` INTEGER, `RANGE` INTEGER, `SHOPNUM` INTEGER, `CHATNUM` INTEGER)";
                        cmd.CommandText = command;
                        cmd.ExecuteNonQuery();

                        command = "CREATE TABLE `PROJECTILES`";
                        command = command + "(`NAME` TEXT, `DAMAGE` INTEGER, `RANGE` INTEGER, `SPRITE` INTEGER, `TYPE` INTEGER, `SPEED` INTEGER)";
                        cmd.CommandText = command;
                        cmd.ExecuteNonQuery();

                        command = "CREATE TABLE `SHOPS`";
                        command = command + "(`NAME` TEXT, `ITEMDATA` BLOB)";
                        cmd.CommandText = command;
                        cmd.ExecuteNonQuery();

                        command = "CREATE TABLE `CHAT`";
                        command = command + "(`NAME` TEXT,`MAINMESSAGE` TEXT,`OPTIONA` TEXT,`OPTIONB` TEXT,`OPTIONC` TEXT,`OPTIOND` TEXT,`NEXTCHATA` INTEGER,`NEXTCHATB` INTEGER,`NEXTCHATC` INTEGER,`NEXTCHATD` INTEGER,`SHOPNUM` INTEGER,`MISSIONNUM` INTEGER,`ITEMA` INTEGER,`ITEMB` INTEGER,`ITEMC` INTEGER,`VALA` INTEGER,";
                        command = command + "`VALB` INTEGER,`VALC` INTEGER,`MONEY` INTEGER,`TYPE` INTEGER)";
                        cmd.CommandText = command;
                        cmd.ExecuteNonQuery();

                        command = "CREATE TABLE `MAPS`";
                        command = command + "(`NAME` TEXT,`REVISION` INTEGER,`UP` INTEGER,`DOWN` INTEGER,`LEFTSIDE` INTEGER,`RIGHTSIDE` INTEGER,`BRIGHTNESS` INTEGER,`NPC` BLOB,`ITEM` BLOB, `GROUND` BLOB,`MASK` BLOB,`MASKA` BLOB,`FRINGE` BLOB,`FRINGEA` BLOB)";
                        cmd.CommandText = command;
                        cmd.ExecuteNonQuery();

                        command = "CREATE TABLE CHESTS";
                        command = command + "(NAME TEXT,MONEY INTEGER,EXPERIENCE INTEGER,REQUIREDLEVEL INTEGER,TRAPLEVEL INTEGER,REQKEY INTEGER,DAMAGE INTEGER,NPCSPAWN INTEGER,SPAWNAMOUNT INTEGER,CHESTITEM BLOB)";
                        cmd.CommandText = command;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private static void InitArrays()
        {
            #region Players
            Logging.WriteMessageLog("Creating player array...");
            for (int i = 0; i < Globals.MAX_PLAYERS; i++)
            {
                players[i] = new Player();
            }
            Logging.WriteMessageLog("Player array loaded successfully");
            #endregion

            #region Maps
            Logging.WriteMessageLog("Loading maps...");
            for (int i = 0; i < Globals.MAX_MAPS; i++)
            {
                    maps[i] = new Map();
                    maps[i].LoadMapFromDatabase(i + 1);
            }
            Logging.WriteMessageLog("Maps loaded successfully");
            #endregion

            #region Items
            Logging.WriteMessageLog("Loading npcs...");
            for (int i = 0; i < Globals.MAX_ITEMS; i++)
            {
                items[i] = new Item();
                items[i].LoadItemFromDatabase(i + 1);
            }
            Logging.WriteMessageLog("Items loaded successfully");
            #endregion

            #region Projectiles
            Logging.WriteMessageLog("Loading projectiles...");
            for (int i = 0; i < Globals.MAX_PROJECTILES; i++)
            {
                projectiles[i] = new Projectile();
                projectiles[i].LoadProjectileFromDatabase(i + 1);
            }
            Logging.WriteMessageLog("Projectiles loaded successfully");
            #endregion

            #region Npcs
            Logging.WriteMessageLog("Loading npcs...");
            for (int i = 0; i < Globals.MAX_NPCS; i++)
            {
                npcs[i] = new Npc();
                npcs[i].LoadNpcFromDatabase((i + 1));
            }
            for (int i = 0; i < Globals.MAX_MAPS; i++)
            {
                for (int n = 0; n < 10; n++)
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
                    }
                }
            }
            Logging.WriteMessageLog("Npcs loaded successfully");
            #endregion

            #region Shops
            Logging.WriteMessageLog("Loading shops...");
            for (int i = 0; i < Globals.MAX_SHOPS; i++)
            {
                shops[i] = new Shop();
                shops[i].LoadShopFromDatabase(i + 1);
            }

            Logging.WriteMessageLog("Shops loaded successfully");
            #endregion

            #region Chats
            Logging.WriteMessageLog("Loading chats...");
            for (int i = 0; i < Globals.MAX_CHATS; i++)
            {
                chats[i] = new Chat();
                chats[i].LoadChatFromDatabase(i + 1);
            }
            Logging.WriteMessageLog("Chats loaded successfully");
            #endregion

            #region Chests
            Logging.WriteMessageLog("Loading chests...");
            for (int i = 0; i < Globals.MAX_CHESTS; i++)
            {
                chests[i] = new Chest();
                chests[i].LoadChestFromDatabase(i + 1);
            }
            Logging.WriteMessageLog("Chests loaded successfully");
            #endregion

            //final
            Logging.WriteMessageLog("Server is listening for connections...");
        }

        #region Server Check Voids
        static void UpTime()
        {
            if (TickCount - suptimeTick > Globals.A_MILLISECOND)
            {
                if (sSecond < Globals.SECONDS_IN_MINUTE)
                {
                    sSecond += 1;
                }
                else
                {
                    sSecond = 0;
                    sMinute += 1;
                }

                if (sMinute >= Globals.MINUTES_IN_HOUR)
                {
                    sMinute = 0;
                    sHour += 1;
                }
                if (sHour == Globals.HOURS_IN_DAY)
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

            for (int i = 0; i < Globals.MAX_PLAYERS; i++)
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
            for (int i = 0; i < Globals.MAX_PLAYERS; i++)
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
            for (int i = 0; i < Globals.MAX_PLAYERS; i++)
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

            for (int i = 0; i < Globals.MAX_MAPS; i++)
            {
                if (maps[i] != null && maps[i].Name != null)
                {
                    if (CheckIfMapHasPlayers(i))
                    {
                        for (int n = 0; n < Globals.MAX_MAP_ITEMS; n++)
                        {
                            if (maps[i].m_MapItem[n].ExpireTick > 0 && maps[i].m_MapItem[n].IsSpawned)
                            {
                                if (TickCount - maps[i].m_MapItem[n].ExpireTick > 300000)
                                {
                                    maps[i].m_MapItem[n] = new MapItem("None", 0, 0, 0);

                                    for (int p = 0; p < Globals.MAX_PLAYERS; p++)
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
            for (int i = 0; i < Globals.MAX_MAPS; i++)
            {
                if (maps[i] != null && maps[i].Name != null)
                {
                    if (CheckIfMapHasPlayers(i))
                    {
                        for (int x = 0; x < Globals.MAX_MAP_X; x++)
                        {
                            for (int y = 0; y < Globals.MAX_MAP_Y; y++)
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

                                                for (int p = 0; p < Globals.MAX_PLAYERS; p++)
                                                {
                                                    if (players[p].Connection != null && i == players[p].Map)
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
            for (int i = 0; i < Globals.MAX_MAPS; i++)
            {
                if (maps[i] != null && maps[i].Name != null)
                {
                    if (CheckIfMapHasPlayers(i))
                    {
                        for (int x = 0; x < Globals.MAX_MAP_X; x++)
                        {
                            for (int y = 0; y < Globals.MAX_MAP_Y; y++)
                            {
                                switch (maps[i].Ground[x, y].Type)
                                {
                                    case (int)TileType.NpcSpawn:
                                        for (int c = 0; c < Globals.MAX_MAP_NPCS; c++)
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

                                                        for (int p = 0; p < Globals.MAX_PLAYERS; p++)
                                                        {
                                                            if (players[p].Connection != null && i == players[p].Map)
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
                                        if (maps[i].Ground[x, y].SpawnNum > 0)
                                        {
                                            for (int n = 0; n < Globals.MAX_MAP_POOL_NPCS; n++)
                                            {
                                                if (maps[i].Ground[x, y].SpawnAmount > maps[i].Ground[x, y].CurrentSpawn)
                                                {
                                                    if (!maps[i].r_MapNpc[n].IsSpawned)
                                                    {
                                                        if (TickCount - maps[i].r_MapNpc[n].spawnTick > (maps[i].r_MapNpc[n].SpawnTime * 1000))
                                                        {
                                                            int num = (maps[i].Ground[x, y].SpawnNum - 1);

                                                            if (num > -1)
                                                            {
                                                                maps[i].r_MapNpc[n].NpcNum = num;
                                                                int genX = (x + RND.Next(1, 3));
                                                                int genY = (y + RND.Next(1, 3));
                                                                maps[i].r_MapNpc[n].Name = npcs[num].Name;
                                                                maps[i].r_MapNpc[n].X = genX;
                                                                maps[i].r_MapNpc[n].Y = genY;
                                                                maps[i].r_MapNpc[n].Health = npcs[num].Health;
                                                                maps[i].r_MapNpc[n].MaxHealth = npcs[num].MaxHealth;
                                                                maps[i].r_MapNpc[n].SpawnX = x;
                                                                maps[i].r_MapNpc[n].SpawnY = y;
                                                                maps[i].r_MapNpc[n].Direction = npcs[num].Direction;
                                                                maps[i].r_MapNpc[n].Step = npcs[num].Step;
                                                                maps[i].r_MapNpc[n].Sprite = npcs[num].Sprite;
                                                                maps[i].r_MapNpc[n].Behavior = npcs[num].Behavior;
                                                                maps[i].r_MapNpc[n].Owner = npcs[num].Owner;
                                                                maps[i].r_MapNpc[n].Damage = npcs[num].Damage;
                                                                maps[i].r_MapNpc[n].DesX = npcs[num].DesX;
                                                                maps[i].r_MapNpc[n].DesY = npcs[num].DesY;
                                                                maps[i].r_MapNpc[n].Exp = npcs[num].Exp;
                                                                maps[i].r_MapNpc[n].Money = npcs[num].Money;
                                                                maps[i].r_MapNpc[n].SpawnTime = npcs[num].SpawnTime;
                                                                maps[i].r_MapNpc[n].IsSpawned = true;
                                                                maps[i].r_MapNpc[n].Range = npcs[num].Range;
                                                                maps[i].Ground[x, y].CurrentSpawn += 1;

                                                                for (int p = 0; p < Globals.MAX_PLAYERS; p++)
                                                                {
                                                                    if (players[p].Connection != null && i == players[p].Map)
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
                for (int i = 0; i < Globals.MAX_MAPS; i++)
                {
                    if (CheckIfMapHasPlayers(i))
                    {
                        for (int n = 0; n < Globals.MAX_MAP_NPCS; n++)
                        {
                            if (maps[i].m_MapNpc[n].IsSpawned)
                            {
                                int canMove = RND.Next(0, 100);
                                int dir = RND.Next(0, 3);

                                maps[i].m_MapNpc[n].NpcAI(canMove, dir, i);

                                if (maps[i].m_MapNpc[n].DidMove)
                                {
                                    maps[i].m_MapNpc[n].DidMove = false;

                                    for (int p = 0; p < Globals.MAX_PLAYERS; p++)
                                    {
                                        if (players[p].Connection != null && players[p].Map == i)
                                        {
                                            HandleData.SendUpdateNpcLoc(players[p].Connection, i, n);
                                        }
                                    }
                                }
                            }
                        }
                        for (int c = 0; c < Globals.MAX_MAP_POOL_NPCS; c++)
                        {
                            if (maps[i].r_MapNpc[c].IsSpawned)
                            {
                                int canMove = RND.Next(0, 100);
                                int dir = RND.Next(0, 3);

                                maps[i].r_MapNpc[c].NpcAI(canMove, dir, i);

                                if (maps[i].r_MapNpc[c].DidMove)
                                {
                                    maps[i].r_MapNpc[c].DidMove = false;

                                    for (int p = 0; p < 5; p++)
                                    {
                                        if (players[p].Connection != null && players[p].Map == i)
                                        {
                                            HandleData.SendUpdatePoolNpcLoc(players[p].Connection, i, c);
                                        }
                                    }
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

                //Dynamic Commands
                //Add a latency to all clients for testing
                if (input.Length >= 11 && input.Substring(0, 10) == "minlatency")
                {
                    string floatseconds = input.Substring(11);
                    int intseconds = ToInt32(floatseconds);
                    string mseconds = floatseconds.Insert(0, "0.0");
                    float delay = ToSingle(mseconds);

                    if (intseconds >= 15 || intseconds == 0)
                    {
                        SabertoothServer.netServer.Configuration.SimulatedMinimumLatency = delay;
                        Logging.WriteMessageLog("Minimum latency is now " + floatseconds + "ms");
                    }
                    else
                    {
                        Logging.WriteMessageLog("Value must be greater or equal to 14, value can be 0 to remove latency");
                    }
                    isDynamic = true;
                }

                //Checks for a 4 octect ip address (wrote this cause I wasnt paying attention to how the server pulls its ip from the host)
                if (input.Length >= 13 && input.Substring(0, 12) == "newnetserver")
                {
                    string ipaddress = input.Substring(13); //Create substring of the IP address
                    string[] octect = ipaddress.Split('.'); //Make sure we have 4 octets by splitting the ip address into seperate strings for each octet
                    bool[] failed = { false, false };
                    int check = 0;

                    if (octect.Length == 4) { failed[0] = true; }
                    if (failed[0] != false)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (octect[i] != null && octect[i] != "")
                            {
                                if (ToInt32(octect[i]) >= 0 && ToInt32(octect[i]) <= 255)
                                {
                                    check += 1;
                                }
                            }
                        }
                    }

                    if (check == 4) { failed[1] = true; }

                    if (failed[0] == true && failed[1] == true)
                    {
                        Console.WriteLine("Valid IP: " + ipaddress);
                        
                    }
                    else
                    {
                        Console.WriteLine("Invalid IP: " + ipaddress);
                    }
                    isDynamic = true;
                }

                if (input.Length >= 11 && input.Substring(0, 10) == "sqlcommand")
                {
                    string command = input.Substring(11);
                    try
                    {
                        string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
                        using (var sql = new SqlConnection(connection))
                        {
                            sql.Open();
                            using (var cmd = new SqlCommand(command, sql))
                            {
                                cmd.ExecuteNonQuery();
                                Logging.WriteMessageLog("Command: " + cmd.CommandText);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Logging.WriteMessageLog(e.Message, "SQL");
                    }
                    isDynamic = true;
                }

                if (input.Length >= 7 && input.Substring(0, 7) == "account")    //Check for account command
                {
                    if (input.Substring(8, 6) == "create")    //Create
                    {
                        if (input.Length >= 14)
                        {
                            string restofInfo = input.Substring(14);  //Get whats left of the string after account create (username and pass)  
                            string[] finalInfo = restofInfo.Split(' '); //Split the username and password into their own strings
                            if (finalInfo[1].Length >= 3 && finalInfo[2].Length >= 3)   //Make sure they are both at least three characters long
                            {
                                Player ac_Player = new Player(finalInfo[1], finalInfo[2], 0, 0, 0, 0, 0, 1, 100, 100, 100, 0,
                                                                100, 10, 100, 100, 5, 5, 5, 5, 1000);   //Create the player in an array so we can save it
                                ac_Player.CreatePlayerInDatabase();
                                Logging.WriteMessageLog("Account create! Username: " + finalInfo[1] + ", Password: " + finalInfo[2], "Commands"); //Let the operator know
                            }
                            else { Logging.WriteMessageLog("USERNAME and PASSWORD must be 3 characters each!", "Commands"); } //Dont fuck it up by making basic shit

                        }
                    }
                    else if (input.Substring(8, 6) == "delete")
                    {
                        if (input.Length >= 14)
                        {
                            string restofInfo = input.Substring(14);
                            if (AccountExist(restofInfo))
                            {
                                Console.Write("Are you sure? (y/n)");
                                string answer = Console.ReadLine();
                                if (answer == "y") { Delete("Players / " + restofInfo + ".xml"); return; }
                            }
                            else { Logging.WriteMessageLog("Account doesnt exist!", "Commands"); return; }
                        }
                    }
                    else { Logging.WriteMessageLog("Please enter a valid command!", "Commands"); return; }  //Did you provide a modifier?
                    isDynamic = true;
                }

                //Basic commands
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
                        for (int i = 0; i < Globals.MAX_PLAYERS; i++)
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
                    case "accounts":

                        break;
                    case "help":    //Help command which displays all commands, modifiers, and possible arguments
                        Logging.WriteMessageLog("Commands:", "Commands");
                        Logging.WriteMessageLog("info - shows the servers stats", "Commands");
                        Logging.WriteMessageLog("uptime - shows server uptime.", "Commands");
                        Logging.WriteMessageLog("saveall - saves all players", "Commands");
                        Logging.WriteMessageLog("shutdown - shuts down the server", "Commands");
                        Logging.WriteMessageLog("exit - shuts down the server", "Commands");
                        break;
                    case "dhelp":
                        Logging.WriteMessageLog("Dynamic Commands:", "Commands");
                        Logging.WriteMessageLog("minlatency miliseconds - Ex: minlatency 65 (will add 65ms to latency, value must be >= 15)", "Commands");
                        Logging.WriteMessageLog("sqlcommand command - Ex: sqlcommand select * from players (query all players from db, experimental)", "Commands");
                        Logging.WriteMessageLog("newnetserver ipaddress - Ex: newnetserver 192.168.1.2 (checks for valid ipv4 address)", "Commands");
                        Logging.WriteMessageLog("account create UN PW - ex: account create sfortune fortune (creates an account with default stats)", "Commands");
                        break;
                    default:    //If you entered something that wasnt a command or pure garbage
                        if (!isDynamic) { Logging.WriteMessageLog("Please enter a valid command!", "Commands"); }                        
                        break;
                }
            }
        }
        static int CalculateFrameRate()
        {
            if (TickCount - lastTick >= Globals.A_MILLISECOND)
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
            for (int i = 0; i < Globals.MAX_PLAYERS; i++)
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
            for (int i = 0; i < Globals.MAX_PLAYERS; i++)
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
            if (Exists("Players/" + name + ".xml"))
            {
                return true;
            }
            return false;
        }

        public static int FindOpenMapItemSlot(Map s_Map)
        {
            for (int i = 0; i < Globals.MAX_MAP_ITEMS; i++)
            {
                if (s_Map.m_MapItem[i].Name == "None" && !s_Map.m_MapItem[i].IsSpawned || s_Map.m_MapItem[i].Name == null && !s_Map.m_MapItem[i].IsSpawned)
                {
                    return i;
                }
            }
            return Globals.MAX_MAP_ITEMS;
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
            writer.WriteElementString("SQLServer", Globals.SQL_SERVER_NAME);
            writer.WriteElementString("Database", Globals.SQL_SERVER_DATABASE);
            writer.WriteElementString("DBType", "0");
            writer.WriteElementString("Version", Globals.VERSION);
            writer.WriteElementString("RegenTime", Globals.HEALTH_REGEN_TIME);
            writer.WriteElementString("HungerTime", Globals.HUNGER_DEGEN_TIME);
            writer.WriteElementString("HydrationTime", Globals.HYDRATION_DEGEN_TIME);
            writer.WriteElementString("SaveTime", Globals.AUTOSAVE_TIME);
            writer.WriteElementString("SpawnTime", Globals.SPAWN_TIME);
            writer.WriteElementString("AiTime", Globals.AI_TIME);
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
            reader.ReadToFollowing("DBType");
            DBType = reader.ReadElementContentAsString();
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

    public static class Globals
    {
        //Globals
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
        public const int PLAYER_START_X = 8;
        public const int PLAYER_START_Y = 8;
        //Config Globals
        public const string GAME_TITLE = "Sabertooth";
        public const string IP_ADDRESS = "10.16.0.3";
        public const int SERVER_PORT = 14242;
        public const float CONNECTION_TIMEOUT = 5.0f;   //Was 25.0
        public const float SIMULATED_RANDOM_LATENCY = 0f;   //0.085f
        public const float SIMULATED_MINIMUM_LATENCY = 0.000f;  //0.065f
        public const float SIMULATED_PACKET_LOSS = 0f;  //0.5f
        public const float SIMULATED_DUPLICATES_CHANCE = 0f; //0.5f
        public const string VERSION = "1.0"; //For beta and alpha
        //Server Globals
        public const string SMTP_IP_ADDRESS = "";
        public const int SMTP_SERVER_PORT = 25;
        public const string SMTP_USER_CREDS = "";
        public const string SMTP_PASS_CREDS = "";
        public const string SQL_SERVER_NAME = @"FDESKTOP-01\SFORTUNESQL";
        public const string SQL_SERVER_DATABASE = "Sabertooth";
        public const string SQL_LOCAL_DATABASE = "Database/Sabertooth.db";
        public const string HEALTH_REGEN_TIME = "60000"; //60000 / 1000 = 1 MIN
        public const string HUNGER_DEGEN_TIME = "600000"; //600000 / 1000 = 10 MIN
        public const string HYDRATION_DEGEN_TIME = "300000"; //300000 / 1000 = 5 MIN
        public const string AUTOSAVE_TIME = "300000"; //300000 / 1000 = 5 MIN
        public const string SPAWN_TIME = "1000"; //1000 / 1000 = 1 SECOND
        public const string AI_TIME = "1000"; //1000 / 1000 = 1 SECOND
        public const int SQL_DATABASE_REMOTE = 0;
        public const int SQL_DATABASE_LOCAL = 1;
        public const int A_MILLISECOND = 1000;
        public const int SECONDS_IN_MINUTE = 60;
        public const int MINUTES_IN_HOUR = 60;
        public const int HOURS_IN_DAY = 24;
        public const int DAYS_IN_YEAR = 365;
        //Editor Globals
        public const uint SCREEN_WIDTH = 800;
        public const uint SCREEN_HEIGHT = 600;
        public const int MAX_FPS = 85;
        public const int PIC_X = 32;
        public const int PIC_Y = 32;
    }
}
