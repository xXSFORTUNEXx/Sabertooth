﻿using Lidgren.Network;
using System;
using System.IO;
using System.Threading;
using System.Xml;
using static Server.Classes.LogWriter;
using static System.Console;
using static System.Environment;
using static System.IO.File;
using System.Net;
using System.Data.SQLite;
using static System.Convert;

namespace Server.Classes
{
    class Server
    {
        Player[] s_Player = new Player[5];
        Npc[] s_Npc = new Npc[10];
        Item[] s_Item = new Item[50];
        HandleData handleData = new HandleData();
        Projectile[] s_Proj = new Projectile[10];
        Map[] s_Map = new Map[10];
        Random RND = new Random();
        static string s_userCommand;
        public bool isRunning;
        private int saveTick;
        private int aiTick;
        private int regenTick;
        private int regenTime;
        private int hungerTime; 
        private int hydrationTime;
        private int saveTime;
        private int spawnTime;
        private int aiTime;

        private int removeTime;
        public int s_Second;
        public int s_Minute;
        public int s_Hour;
        public int s_Day;
        public int s_uptimeTick;
        public string upTime;
        public string s_Version;

        public void ServerLoop(NetServer s_Server)
        {
            handleData.s_Version = s_Version;
            InitPlayerArray();
            InitMap();
            InitItems();
            InitNPC();
            InitProjectiles();
            InitFinal();

            Thread s_Command = new Thread(CommandWindow);
            s_Command.Start();

            isRunning = true;
            while (isRunning)
            {
                handleData.HandleDataMessage(s_Server, s_Player, s_Map, s_Npc, s_Item, s_Proj);
                SavePlayers();
                CheckNpcSpawn(s_Server);
                CheckItemSpawn(s_Server);
                CheckClearMapItem(s_Server);
                CheckHealthRegen(s_Server);
                CheckVitalLoss(s_Server);
                CheckNpcAi(s_Server);
                CheckCommands(s_Server, s_Player);
                UpTime();
            }
            DisconnectClients(s_Server);
            WriteLine("Disconnecting clients...");
            Thread.Sleep(2500);
            s_Server.Shutdown("Shutting down");
            WriteLine("Shutting down...");
            Thread.Sleep(2500);
            Exit(0);
        }

        #region Server Init Voids
        void InitPlayerArray()
        {
            WriteLine("Creating player array...");
            WriteLog("Creating player array...", "Server");
            for (int i = 0; i < 5; i++)
            {
                s_Player[i] = new Player();
            }

            WriteLine("Array created successfully!");
        }

        void InitMap()
        {
            bool exists = true;

            for (int i = 0; i < 10; i++)
            {
                if (!Exists("Maps/Map" + i + ".bin"))
                {
                    s_Map[i] = new Map();
                    s_Map[i].GenerateMap(i);
                    s_Map[i].SaveMap(i);
                    exists = false;
                }
                else
                {
                    s_Map[i] = new Map();
                    s_Map[i].LoadMap(i);
                }
            }

            if (!exists)
            { 
                WriteLine("Saving maps...");
                WriteLog("Saving maps...", "Server");
            }
            WriteLine("Loading maps...");
            WriteLog("Loading maps...", "Server");
            WriteLine("Maps loaded successfully!");
        }

        void InitItems()
        {
            WriteLine("Loading items...");
            WriteLog("Loading npcs...", "Server");

            for (int i = 0; i < 50; i++)
            {
                s_Item[i] = new Item();
                s_Item[i].LoadItemFromDatabase(i + 1);
            }
            WriteLine("Items loaded successfully!");
        }

        void InitProjectiles()
        {
            WriteLine("Loading projectiles...");
            WriteLog("Loading projectiles...", "Server");

            for  (int i = 0; i < 10; i++)
            {
                s_Proj[i] = new Projectile();
                s_Proj[i].LoadProjectileFromDatabase(i);
            }
            WriteLine("Projectiles loaded successfully!");
        }

        void InitNPC()
        {
            WriteLine("Loading npcs...");
            WriteLog("Loading npcs...", "Server");

            for (int i = 0; i < 10; i++)
            {
                s_Npc[i] = new Npc();
                s_Npc[i].LoadNpcFromDatabase((i + 1));
            }

            //Load in data we need for mapnpcs
            for (int i = 0; i < 10; i++)
            {
                for (int n = 0; n < 10; n++)
                {
                    int num = (s_Map[i].m_MapNpc[n].NpcNum - 1);

                    if (num > -1)
                    {
                        s_Map[i].m_MapNpc[n].Name = s_Npc[num].Name;
                        s_Map[i].m_MapNpc[n].X = s_Npc[num].X;
                        s_Map[i].m_MapNpc[n].Y = s_Npc[num].Y;
                        s_Map[i].m_MapNpc[n].Health = s_Npc[num].Health;
                        s_Map[i].m_MapNpc[n].MaxHealth = s_Npc[num].MaxHealth;
                        s_Map[i].m_MapNpc[n].Direction = s_Npc[num].Direction;
                        s_Map[i].m_MapNpc[n].Step = s_Npc[num].Step;
                        s_Map[i].m_MapNpc[n].Sprite = s_Npc[num].Sprite;
                        s_Map[i].m_MapNpc[n].Behavior = s_Npc[num].Behavior;
                        s_Map[i].m_MapNpc[n].Owner = s_Npc[num].Owner;
                        s_Map[i].m_MapNpc[n].IsSpawned = s_Npc[num].IsSpawned;
                        s_Map[i].m_MapNpc[n].Damage = s_Npc[num].Damage;
                        s_Map[i].m_MapNpc[n].DesX = s_Npc[num].DesX;
                        s_Map[i].m_MapNpc[n].DesY = s_Npc[num].DesY;
                        s_Map[i].m_MapNpc[n].Exp = s_Npc[num].Exp;
                        s_Map[i].m_MapNpc[n].Money = s_Npc[num].Money;
                        s_Map[i].m_MapNpc[n].SpawnTime = s_Npc[num].SpawnTime;
                        s_Map[i].m_MapNpc[n].Range = s_Npc[num].Range;
                    }
                }
            }
            WriteLine("Npcs loaded successfully!");
        }

        void InitFinal()
        {
            WriteLine("Listening for connections...Waiting...");
            WriteLog("Server is listening for connections...", "Server");
        }
        #endregion

        #region Server Check Voids
        void CheckHealthRegen(NetServer s_Server)
        {
            if (TickCount - regenTick > regenTime)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (s_Player[i].Name != null)
                    {
                        WriteLine("Checking for health regen...");
                        WriteLog("Checking for health regin...", "Server");

                        s_Player[i].RegenHealth();
                        handleData.SendUpdateHealthData(s_Server, i, s_Player[i].Health);
                    }
                }
                regenTick = TickCount;
            }
        }

        void CheckVitalLoss(NetServer s_Server)
        {
            for (int i = 0; i < 5; i++)
            {
                //Check for hunger
                if (TickCount - s_Player[i].hungerTick > hungerTime)
                {
                    if (s_Player[i].Name != null)
                    {
                        WriteLine("Checking for hunger loss...");
                        WriteLog("Checking for hunger loss...", "Server");

                        s_Player[i].VitalLoss("food");
                        handleData.SendUpdateVitalData(s_Server, i, "food", s_Player[i].Hunger);
                    }
                    s_Player[i].hungerTick = TickCount;
                }

                if (TickCount - s_Player[i].hydrationTick > hydrationTime)
                {
                    if (s_Player[i].Name != null)
                    {
                        WriteLine("Checking for hydration loss...");
                        WriteLog("Checking for hydration loss...", "Server");

                        s_Player[i].VitalLoss("water");
                        handleData.SendUpdateVitalData(s_Server, i, "water", s_Player[i].Hydration);
                    }
                    s_Player[i].hydrationTick = TickCount;
                }
            }
        }

        bool CheckIfMapHasPlayers(int mapNum, Player[] s_Player)
        {
            for (int i = 0; i < 5; i++)
            {
                if (s_Player[i].Connection != null && s_Player[i].Map == mapNum)
                {
                    return true;
                }
            }
            return false;
        }

        void CheckClearMapItem(NetServer s_Server)
        {
            if (TickCount - removeTime > 1000)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (s_Map[i] != null && s_Map[i].Name != null)
                    {
                        if (CheckIfMapHasPlayers(i, s_Player))
                        {
                            for (int n = 0; n < 20; n++)
                            {
                                if (s_Map[i].mapItem[n].ExpireTick > 0 && s_Map[i].mapItem[n].IsSpawned)
                                {
                                    if (TickCount - s_Map[i].mapItem[n].ExpireTick > 300000)
                                    {
                                        s_Map[i].mapItem[n] = new MapItem("None", 0, 0, 0);

                                        for (int p = 0; p < 5; p++)
                                        {
                                            if (s_Player[p].Connection != null && i == s_Player[p].Map)
                                            {
                                                handleData.SendMapItemData(s_Server, s_Player[p].Connection, s_Map[i], n);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                removeTime = TickCount;
            }
        }

        void CheckItemSpawn(NetServer s_Server)
        {
            for (int i = 0; i < 10; i++)
            {
                if (s_Map[i] != null && s_Map[i].Name != null)
                {
                    if (CheckIfMapHasPlayers(i, s_Player))
                    {
                        for (int x = 0; x < 50; x++)
                        {
                            for (int y = 0; y < 50; y++)
                            {
                                if (s_Map[i].Ground[x, y].Type == (int)TileType.MapItem)
                                {
                                    if (s_Map[i].Ground[x, y].SpawnNum > 0)
                                    {
                                        if (s_Map[i].Ground[x, y].NeedsSpawnedTick > 0)
                                        {
                                            if (TickCount - s_Map[i].Ground[x, y].NeedsSpawnedTick > 300000 && s_Map[i].Ground[x, y].NeedsSpawned)
                                            {
                                                s_Map[i].Ground[x, y].NeedsSpawned = false;
                                                s_Map[i].Ground[x, y].NeedsSpawnedTick = 0;
                                            }
                                        }

                                        if (!s_Map[i].Ground[x, y].NeedsSpawned)
                                        {
                                            int slot = FindOpenMapItemSlot(s_Map[i]);
                                            if (slot < 20)
                                            {
                                                int itemNum = s_Map[i].Ground[x, y].SpawnNum - 1;
                                                s_Map[i].mapItem[slot].ItemNum = itemNum + 1;
                                                s_Map[i].mapItem[slot].Name = s_Item[itemNum].Name;
                                                s_Map[i].mapItem[slot].X = x;
                                                s_Map[i].mapItem[slot].Y = y;
                                                s_Map[i].mapItem[slot].Sprite = s_Item[itemNum].Sprite;
                                                s_Map[i].mapItem[slot].Damage = s_Item[itemNum].Damage;
                                                s_Map[i].mapItem[slot].Armor = s_Item[itemNum].Armor;
                                                s_Map[i].mapItem[slot].Type = s_Item[itemNum].Type;
                                                s_Map[i].mapItem[slot].AttackSpeed = s_Item[itemNum].AttackSpeed;
                                                s_Map[i].mapItem[slot].ReloadSpeed = s_Item[itemNum].ReloadSpeed;
                                                s_Map[i].mapItem[slot].HealthRestore = s_Item[itemNum].HealthRestore;
                                                s_Map[i].mapItem[slot].HungerRestore = s_Item[itemNum].HungerRestore;
                                                s_Map[i].mapItem[slot].HydrateRestore = s_Item[itemNum].HydrateRestore;
                                                s_Map[i].mapItem[slot].Strength = s_Item[itemNum].Strength;
                                                s_Map[i].mapItem[slot].Agility = s_Item[itemNum].Agility;
                                                s_Map[i].mapItem[slot].Endurance = s_Item[itemNum].Endurance;
                                                s_Map[i].mapItem[slot].Stamina = s_Item[itemNum].Stamina;
                                                s_Map[i].mapItem[slot].Clip = s_Item[itemNum].Clip;
                                                s_Map[i].mapItem[slot].MaxClip = s_Item[itemNum].MaxClip;
                                                s_Map[i].mapItem[slot].ItemAmmoType = s_Item[itemNum].ItemAmmoType;
                                                s_Map[i].mapItem[slot].Value = s_Item[itemNum].Value;
                                                s_Map[i].mapItem[slot].IsSpawned = true;
                                                s_Map[i].Ground[x, y].NeedsSpawned = true;

                                                for (int p = 0; p < 5; p++)
                                                {
                                                    if (s_Player[p].Connection != null && i == s_Player[p].Map)
                                                    {
                                                        handleData.SendMapItemData(s_Server, s_Player[p].Connection, s_Map[i], slot);
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

        void CheckNpcSpawn(NetServer s_Server)
        {
            for (int i = 0; i < 10; i++)
            {
                if (s_Map[i] != null && s_Map[i].Name != null)
                {
                    if (CheckIfMapHasPlayers(i, s_Player))
                    {
                        for (int x = 0; x < 50; x++)
                        {
                            for (int y = 0; y < 50; y++)
                            {
                                switch (s_Map[i].Ground[x, y].Type)
                                {
                                    case (int)TileType.NpcSpawn:
                                        for (int c = 0; c < 10; c++)
                                        {
                                            if (s_Map[i].Ground[x, y].SpawnNum == (c + 1))
                                            {
                                                if (!s_Map[i].m_MapNpc[c].IsSpawned && s_Map[i].m_MapNpc[c].Name != "None")
                                                {
                                                    if (TickCount - s_Map[i].m_MapNpc[c].spawnTick > (s_Map[i].m_MapNpc[c].SpawnTime * 1000))
                                                    {
                                                        s_Map[i].m_MapNpc[c].X = x;
                                                        s_Map[i].m_MapNpc[c].Y = y;
                                                        s_Map[i].m_MapNpc[c].IsSpawned = true;

                                                        for (int p = 0; p < 5; p++)
                                                        {
                                                            if (s_Player[p].Connection != null && i == s_Player[p].Map)
                                                            {
                                                                handleData.SendMapNpcData(s_Server, s_Player[p].Connection, s_Map[i], c);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        break;

                                    case (int)TileType.SpawnPool:
                                        if (s_Map[i].Ground[x, y].SpawnNum > 0)
                                        {
                                            for (int n = 0; n < 20; n++)
                                            {
                                                if (s_Map[i].Ground[x, y].SpawnAmount > s_Map[i].Ground[x, y].CurrentSpawn)
                                                {
                                                    if (!s_Map[i].r_MapNpc[n].IsSpawned)
                                                    {
                                                        if (TickCount - s_Map[i].r_MapNpc[n].spawnTick > (s_Map[i].r_MapNpc[n].SpawnTime * 1000))
                                                        {
                                                            int num = (s_Map[i].Ground[x, y].SpawnNum - 1);

                                                            if (num > -1)
                                                            {
                                                                s_Map[i].r_MapNpc[n].NpcNum = num;
                                                                int genX = (x + RND.Next(1, 3));
                                                                int genY = (y + RND.Next(1, 3));
                                                                s_Map[i].r_MapNpc[n].Name = s_Npc[num].Name;
                                                                s_Map[i].r_MapNpc[n].X = genX;
                                                                s_Map[i].r_MapNpc[n].Y = genY;
                                                                s_Map[i].r_MapNpc[n].Health = s_Npc[num].Health;
                                                                s_Map[i].r_MapNpc[n].MaxHealth = s_Npc[num].MaxHealth;
                                                                s_Map[i].r_MapNpc[n].SpawnX = x;
                                                                s_Map[i].r_MapNpc[n].SpawnY = y;
                                                                s_Map[i].r_MapNpc[n].Direction = s_Npc[num].Direction;
                                                                s_Map[i].r_MapNpc[n].Step = s_Npc[num].Step;
                                                                s_Map[i].r_MapNpc[n].Sprite = s_Npc[num].Sprite;
                                                                s_Map[i].r_MapNpc[n].Behavior = s_Npc[num].Behavior;
                                                                s_Map[i].r_MapNpc[n].Owner = s_Npc[num].Owner;
                                                                s_Map[i].r_MapNpc[n].Damage = s_Npc[num].Damage;
                                                                s_Map[i].r_MapNpc[n].DesX = s_Npc[num].DesX;
                                                                s_Map[i].r_MapNpc[n].DesY = s_Npc[num].DesY;
                                                                s_Map[i].r_MapNpc[n].Exp = s_Npc[num].Exp;
                                                                s_Map[i].r_MapNpc[n].Money = s_Npc[num].Money;
                                                                s_Map[i].r_MapNpc[n].SpawnTime = s_Npc[num].SpawnTime;
                                                                s_Map[i].r_MapNpc[n].IsSpawned = true;
                                                                s_Map[i].r_MapNpc[n].Range = s_Npc[num].Range;
                                                                s_Map[i].Ground[x, y].CurrentSpawn += 1;

                                                                for (int p = 0; p < 5; p++)
                                                                {
                                                                    if (s_Player[p].Connection != null && i == s_Player[p].Map)
                                                                    {
                                                                        handleData.SendPoolNpcData(s_Server, s_Player[p].Connection, s_Map[i], n);
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

        void CheckNpcAi(NetServer s_Server)
        {
            if (TickCount - aiTick > aiTime)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (CheckIfMapHasPlayers(i, s_Player))
                    {
                        for (int n = 0; n < 10; n++)
                        {
                            if (s_Map[i].m_MapNpc[n].IsSpawned)
                            {
                                int canMove = RND.Next(0, 100);
                                int dir = RND.Next(0, 3);

                                s_Map[i].m_MapNpc[n].NpcAI(canMove, dir, s_Map[i], s_Player, s_Server);

                                if (s_Map[i].m_MapNpc[n].DidMove)
                                {
                                    s_Map[i].m_MapNpc[n].DidMove = false;

                                    for (int p = 0; p < 5; p++)
                                    {
                                        if (s_Player[p].Connection != null && s_Player[p].Map == i)
                                        {
                                            handleData.SendMapNpcData(s_Server, s_Player[p].Connection, s_Map[i], n);
                                        }
                                    }
                                }
                            }
                        }
                        for (int c = 0; c < 20; c++)
                        {
                            if (s_Map[i].r_MapNpc[c].IsSpawned)
                            {
                                int canMove = RND.Next(0, 100);
                                int dir = RND.Next(0, 3);

                                s_Map[i].r_MapNpc[c].NpcAI(canMove, dir, s_Map[i], s_Player, s_Server);

                                if (s_Map[i].r_MapNpc[c].DidMove)
                                {
                                    s_Map[i].r_MapNpc[c].DidMove = false;

                                    for (int p = 0; p < 5; p++)
                                    {
                                        if (s_Player[p].Connection != null && s_Player[p].Map == i)
                                        {
                                            handleData.SendPoolNpcData(s_Server, s_Player[p].Connection, s_Map[i], c);
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

        void CheckCommands(NetServer s_Server, Player[] s_Player)
        {
            if (s_userCommand != null)
            {
                //Dynamic Commands
                if (s_userCommand.Length >= 7 && s_userCommand.Substring(0, 7) == "account")    //Check for account command
                {
                    if (s_userCommand.Substring(8, 6) == "create")    //Create
                    {
                        if (s_userCommand.Length >= 14)
                        {
                            string restofInfo = s_userCommand.Substring(14);  //Get whats left of the string after account create (username and pass)  
                            string[] finalInfo = restofInfo.Split(' '); //Split the username and password into their own strings
                            if (finalInfo[1].Length >= 3 && finalInfo[2].Length >= 3)   //Make sure they are both at least three characters long
                            {
                                Player ac_Player = new Player(finalInfo[1], finalInfo[2], 0, 0, 0, 0, 0, 1, 100, 100, 100, 0,
                                                              100, 10, 100, 100, 5, 5, 5, 5, 1000);   //Create the player in an array so we can save it
                                ac_Player.CreatePlayerInDatabase();
                                WriteLine("Account create! Username: " + finalInfo[1] + ", Password: " + finalInfo[2]); //Let the operator know
                            }
                            else { WriteLine("USERNAME and PASSWORD must be 3 characters each!"); } //Dont fuck it up by making basic shit

                            s_userCommand = null;   //Clear the command
                            return; //Get da fuck out
                        }
                    }
                    else if (s_userCommand.Substring(8, 6) == "delete")
                    {
                        if (s_userCommand.Length >= 14)
                        {
                            string restofInfo = s_userCommand.Substring(14);
                            if (AccountExist(restofInfo))
                            {
                                Write("Are you sure? (y/n)");
                                string answer = ReadLine();
                                if (answer == "y") { Delete("Players / " + restofInfo + ".xml"); s_userCommand = null; return; }
                            }
                            else { WriteLine("Account doesnt exist!"); s_userCommand = null; return; }
                        }
                    }
                    else { WriteLine("Please enter a valid command!"); s_userCommand = null; return; }  //Did you provide a modifier?
                }
                //Basic commands
                switch (s_userCommand)  //Basic commands can be ran in a switch statement since they dont require modifiers and arguments
                {
                    case "shutdown":    //Shutdow the server in about 3 seconds
                        isRunning = false;  //Break the loop
                        break;
                    case "exit":    //Same as shutdown command but shorter and it was the first command it wrote
                        isRunning = false;  //Break the loop
                        break;
                    case "save all":    //Save all players (online) which just saves all accounts to their respective XML files
                        SaveAll();  //The void for this command
                        break;
                    case "reload":  //Reload which actually requires a modifier but not any arguments like account
                        WriteLine("reload command needs argument (eg reload npcs)");    //If you dont provide a modifier
                        break;
                    case "reload database":
                        WriteLine("Reloading database...");
                        InitNPC();
                        InitMap();
                        InitItems();
                        InitProjectiles();
                        break;
                    case "reload npcs": //Reload NPCS
                        WriteLine("Reloading Npcs..."); //Let the op know
                        InitNPC();  //The same void thats ran when we first load them from their BIN files
                        break;
                    case "reload maps": //Reloads maps
                        WriteLine("Reloading Maps..."); //Let the op know
                        InitMap();  //Same on that loads when the server boots just like npcs
                        break;
                    case "reload items":    //Reload items
                        WriteLine("Reloading Items...");
                        InitItems();
                        break;
                    case "reload projectiles":
                        WriteLine("Reloading projectiles...");
                        InitProjectiles();
                        break;
                    case "info":
                        string hostName = Dns.GetHostName();
                        WriteLine("Statistics: ");
                        WriteLine("Version: " + s_Version);
                        WriteLine(upTime);
                        WriteLine("Host Name: " + hostName);
                        WriteLine("Server Address: " + NetUtility.Resolve(hostName));
                        WriteLine("Port: " + s_Server.Port);
                        WriteLine(s_Server.Statistics.ToString());
                        WriteLine("Connections: ");
                        for (int i = 0; i < 5; i++)
                        {
                            if (s_Player[i].Connection != null)
                            {
                                WriteLine(s_Player[i].Connection + " Logged in as: " + s_Player[i].Name);
                            }
                            else
                            {
                                WriteLine("Open");
                            }
                        }
                        break;
                    case "uptime":
                        WriteLine(upTime);
                        break;
                    case "help":    //Help command which displays all commands, modifiers, and possible arguments
                        WriteLine("Commands:");
                        WriteLine("reload npcs - reloads npcs from SQL database");
                        WriteLine("reload maps - reloads maps from SQL database");
                        WriteLine("reload items - reloads items from SQL database");
                        WriteLine("reload projectiles - reloads projectiles from SQL database");
                        WriteLine("reload database - reloads the entire SQL database except players");
                        WriteLine("account create UN PW - creates an account with generic stats, must provide username and password");
                        WriteLine("info - shows the servers stat, hosts, ip, connections");
                        WriteLine("uptime - shows server uptime.");
                        WriteLine("save all - saves all players");
                        WriteLine("shutdown - shuts down the server");
                        break;
                    default:    //If you entered something that wasnt a command or pure garbage
                        WriteLine("Please enter a valid command!");
                        break;
                }
                s_userCommand = null;   //Clear the command
            }
        }
        #endregion

        #region Server Voids
        void SaveAll()
        {
            WriteLine("Saving players...");
            WriteLog("Saving players...", "Server");
            for (int i = 0; i < 5; i++)
            {
                if (s_Player[i].Name != null)
                {
                    s_Player[i].SavePlayerToDatabase();
                }
            }
            WriteLine("Players saved!");
        }

        void SavePlayers()
        {
            if (TickCount - saveTick > saveTime)
            {
                WriteLine("Saving players...");
                WriteLog("Saving players...", "Server");
                for (int i = 0; i < 5; i++)
                {
                    if (s_Player[i].Name != null)
                    {
                        s_Player[i].SavePlayerToDatabase();
                    }
                }
                saveTick = TickCount;
                WriteLine("Players saved successfully!");
            }
        }

        static bool AccountExist(string name)
        {
            if (Exists("Players/" + name + ".xml"))
            {
                return true;
            }
            return false;
        }

        public int FindOpenMapItemSlot(Map s_Map)
        {
            for (int i = 0; i < 20; i++)
            {
                if (s_Map.mapItem[i].Name == "None" && !s_Map.mapItem[i].IsSpawned)
                {
                    return i;
                }
            }
            return 20;
        }

        public void DisconnectClients(NetServer s_Server)
        {
            NetOutgoingMessage outMSG = s_Server.CreateMessage();

            outMSG.Write((byte)PacketTypes.Shutdown);
            s_Server.SendToAll(outMSG, NetDeliveryMethod.Unreliable);
        }

        public void SaveServerConfig()
        {
            XmlWriterSettings userData = new XmlWriterSettings();
            userData.Indent = true;
            XmlWriter writer = XmlWriter.Create("Config.xml", userData);
            WriteLog("Config XML file saved.", "Server");
            writer.WriteStartDocument();
            //writer.WriteComment("This file is generated by the server.");
            writer.WriteStartElement("ConfigData");
            writer.WriteElementString("Version", "1.0");
            writer.WriteElementString("RegenTime", "60000");
            writer.WriteElementString("HungerTime", "600000");
            writer.WriteElementString("HydrationTime", "300000");
            writer.WriteElementString("SaveTime", "300000");
            writer.WriteElementString("SpawnTime", "1000");
            writer.WriteElementString("AiTime", "1000");
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }

        public void LoadServerConfig()
        {
            if (!Exists("Config.xml"))
            {
                SaveServerConfig();
                WriteLine("Creating config XML...");
            }

            WriteLine("Loading config XML...");
            XmlReader reader = XmlReader.Create("Config.xml");
            WriteLog("Config XML file loaded.", "Server");
            reader.ReadToFollowing("Version");
            s_Version = reader.ReadElementContentAsString();
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
            WriteLine("Config XML loaded successfully!");
        }

        static void CommandWindow()
        {
            //WriteLine("Enter commands below, type help for commands:");
            do
            {
                Write("");
                s_userCommand = ReadLine();
            } while (s_userCommand != null);
        }

        void UpTime()
        {
            if (TickCount - s_uptimeTick > 1000)
            {
                if (s_Second < 60)
                {
                    s_Second += 1;
                }
                else
                {
                    s_Second = 0;
                    s_Minute += 1;
                }

                if (s_Minute >= 60)
                {
                    s_Minute = 0;
                    s_Hour += 1;
                }
                if (s_Hour == 24)
                {
                    s_Hour = 0;
                    s_Day += 1;
                }
                upTime = "Uptime - Days: " + s_Day + " Hours: " + s_Hour + " Minutes: " + s_Minute + " Seconds: " + s_Second;
                s_uptimeTick = TickCount;
            }
        }
        #endregion

    }
}
