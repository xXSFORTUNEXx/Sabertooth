﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;
using static System.Environment;
using System.IO;
using System.Threading;

namespace Server.Classes
{
    class Server
    {
        Player[] s_Player = new Player[5];
        NPC[] s_Npc = new NPC[10];
        Item[] s_Item = new Item[50];
        HandleData handleData = new HandleData();
        Map[] s_Map = new Map[10];
        Random RND = new Random();
        static string s_userCommand;
        public bool isRunning;
        private int saveTick;
        private int saveTime = 300000;
        private int aiTick;
        private int aiTime = 1000;
        private int spawnTick;
        private int spawnTime = 1000;
        private int regenTick;
        private int regenTime = 30000;
        private int hungerTick;
        private int hungerTime = 600000;
        private int hydrationTick;
        private int hydrationTime = 300000;   

        public void ServerLoop(NetServer s_Server)
        {
            InitPlayerArray();
            InitMap();
            InitItems();
            InitNPC();
            InitFinal();

            Thread s_Command = new Thread(CommandWindow);
            s_Command.Start();

            isRunning = true;
            while (isRunning)
            {
                handleData.HandleDataMessage(s_Server, s_Player, s_Map, s_Npc);
                SavePlayers();
                CheckNPCSpawn(s_Server);
                CheckNpcAI(s_Server);
                CheckHealthRegen(s_Server);
                CheckVitalLoss(s_Server);
                CheckCommands();
            }
            Console.WriteLine("Shutting down...");
            s_Server.Shutdown("Shutting down");
            Thread.Sleep(2500);
            Exit(0);
        }
        
        //Command window thread
        static void CommandWindow()
        {
            Console.WriteLine("Enter commands below, type help for commands:");
            do
            {
                Console.Write("");
                s_userCommand = Console.ReadLine();
            } while (s_userCommand != null);
        }

        ///Init methods
        //Creates the player array for use when players join
        void InitPlayerArray()
        {
            Console.WriteLine("Creating player array...");
            LogWriter.WriteLog("Creating player array...", "Server");
            for (int i = 0; i < 5; i++)
            {
                s_Player[i] = new Player();
            }
        }

        //Loads in our maps
        void InitMap()
        {
            Console.WriteLine("Loading maps...");
            LogWriter.WriteLog("Loading maps...", "Server");
            for (int i = 0; i < 10; i++)
            {
                if (!File.Exists("Maps/Map" + i + ".bin"))
                {
                    s_Map[i] = new Map();
                    s_Map[i].GenerateMap(i);
                    s_Map[i].SaveMap(i);
                }
                else
                {
                    s_Map[i] = new Map();
                    s_Map[i].LoadMap(i);
                }
            }
        }

        //Load in the items!
        void InitItems()
        {
            Console.WriteLine("Loading items...");
            LogWriter.WriteLog("Loading npcs...", "Server");

            for (int i = 0; i < 50; i++)
            {
                if (!File.Exists("Items/Item" + i + ".bin"))
                {
                    s_Item[i] = new Item("Default", 0, 0, (int)ItemType.None, 1, 1, 1, 1, 1, 1, 1, 1);
                    s_Item[i].SaveItem(i);
                }
                else
                {
                    s_Item[i] = new Item();
                    s_Item[i].LoadItem(i);
                }
            }
        }

        //Loads in our npcs
        void InitNPC()
        {
            Console.WriteLine("Loading npcs...");
            LogWriter.WriteLog("Loading npcs...", "Server");

            for (int i = 0; i < 10; i++)
            {
                if (!File.Exists("NPCS/Npc" + i + ".bin"))
                {
                    s_Npc[i] = new NPC("Default", 10, 10, (int)Directions.Down, 0, 0, 0, (int)BehaviorType.Friendly, 5000);
                    s_Npc[i].SaveNPC(i);
                }
                else
                {
                    s_Npc[i] = new NPC();
                    s_Npc[i].LoadNPC(i);
                }
            }

            //Load in data we need for mapnpcs
            for (int i = 0; i < 10; i++)
            {
                for (int n = 0; n < 10; n++)
                {
                    int num = s_Map[i].mapNpc[n].npcNum;
                    s_Map[i].mapNpc[n].Name = s_Npc[num].Name;
                    s_Map[i].mapNpc[n].X = s_Npc[num].X;
                    s_Map[i].mapNpc[n].Y = s_Npc[num].Y;
                    s_Map[i].mapNpc[n].Direction = s_Npc[num].Direction;
                    s_Map[i].mapNpc[n].Step = s_Npc[num].Step;
                    s_Map[i].mapNpc[n].Sprite = s_Npc[num].Sprite;
                    s_Map[i].mapNpc[n].Behavior = s_Npc[num].Behavior;
                    s_Map[i].mapNpc[n].Owner = s_Npc[num].Owner;
                    s_Map[i].mapNpc[n].isSpawned = s_Npc[num].isSpawned;
                }
            }
        }

        //Final checks before we really get going
        void InitFinal()
        {
            Console.WriteLine("Listening for connections...");
            LogWriter.WriteLog("Server is listening for connections...", "Server");
        }

        ///Saving methods
        //Saves all players online every 300000ms (5 Min)
        void SavePlayers()
        {
            if (TickCount - saveTick > saveTime)
            {
                Console.WriteLine("Saving players...");
                LogWriter.WriteLog("Saving players...", "Server");
                for (int i = 0; i < 5; i++)
                {
                    if (s_Player[i].Name != null)
                    {
                        s_Player[i].SavePlayerXML();
                    }
                }
                saveTick = TickCount;
            }
        }

        //Check to see who needs health regen
        void CheckHealthRegen(NetServer s_Server)
        {
            if (TickCount - regenTick > regenTime)
            {
                Console.WriteLine("Checking for health regen...");
                LogWriter.WriteLog("Checking for health regin...", "Server");
                for (int i = 0; i < 5; i++)
                {
                    if (s_Player[i].Name != null)
                    {
                        s_Player[i].RegenHealth();
                        handleData.SendUpdateHealthData(s_Server, i, s_Player[i].Health);
                    }
                }
                regenTick = TickCount;
            }
        }

        //Check and see who needs to eat some food and drink some water
        void CheckVitalLoss(NetServer s_Server)
        {
            //Check for hunger
            if (TickCount - hungerTick > hungerTime)
            {
                Console.WriteLine("Checking for hunger loss...");
                LogWriter.WriteLog("Checking for hunger loss...", "Server");
                for (int i = 0; i < 5; i++)
                {
                    if (s_Player[i].Name != null)
                    {
                        s_Player[i].VitalLoss("food");
                        handleData.SendUpdateVitalData(s_Server, i, "food", s_Player[i].Hunger);
                    }
                }
                hungerTick = TickCount;
            }

            if (TickCount - hydrationTick > hydrationTime)
            {
                Console.WriteLine("Checking for hydration loss...");
                LogWriter.WriteLog("Checking for hydration loss...", "Server");
                for (int i = 0; i < 5; i++)
                {
                    if (s_Player[i].Name != null)
                    {
                        s_Player[i].VitalLoss("water");
                        handleData.SendUpdateVitalData(s_Server, i, "water", s_Player[i].Hydration);
                    }
                }
                hydrationTick = TickCount;
            }
        }

        //Saves all online for sever command
        void SaveAll()
        {
            Console.WriteLine("Saving players...");
            LogWriter.WriteLog("Saving players...", "Server");
            for (int i = 0; i < 5; i++)
            {
                if (s_Player[i].Name != null)
                {
                    s_Player[i].SavePlayerXML();
                }
            }
            Console.WriteLine("Players saved!");
        }

        ///Regular check methods
        //Run our AI script to get the npc's moving and killing shit
        void CheckNpcAI(NetServer s_Server)
        {
            if (TickCount - aiTick > aiTime)
            {
                for (int i = 0; i < 10; i++)
                {
                    for (int n = 0; n < 10; n++)
                    {
                        if (s_Map[i].mapNpc[n].isSpawned == true)
                        {
                            int canMove = RND.Next(0, 100);
                            int dir = RND.Next(0, 3);

                            s_Map[i].mapNpc[n].NpcAI(canMove, dir, s_Map[i]);

                            if (s_Map[i].mapNpc[n].didMove == true)
                            {
                                s_Map[i].mapNpc[n].didMove = false;
                                
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
                }
                aiTick = TickCount;
            }
        }

        //Check to see if we need to perhaps spawn an npc
        void CheckNPCSpawn(NetServer s_Server)
        {
            //Check for map spawning
            if (TickCount - spawnTick > spawnTime)
            {
                for (int i = 0; i < 10; i++)
                {
                    for (int x = 0; x < 50; x++)
                    {
                        for (int y = 0; y < 50; y++)
                        {
                            if (s_Map[i].Ground[x, y].type == (int)TileType.NPCSpawn)
                            {
                                int npcNum = s_Map[i].Ground[x, y].spawnNum;

                                if (s_Map[i].mapNpc[npcNum].isSpawned == false)
                                {
                                    s_Map[i].mapNpc[npcNum].X = x;
                                    s_Map[i].mapNpc[npcNum].Y = y;
                                    s_Map[i].mapNpc[npcNum].isSpawned = true;

                                    for (int p = 0; p < 5; p++)
                                    {
                                        if (s_Player[p].Connection != null && i == s_Player[p].Map)
                                        {
                                            handleData.SendMapNpcData(s_Server, s_Player[p].Connection, s_Map[i], npcNum);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            spawnTick = TickCount;
        }

        //Check to see if the user entered any commands
        void CheckCommands()
        {
            if (s_userCommand != null)
            {
                switch (s_userCommand)
                {
                    case "shutdown":
                        isRunning = false;
                        break;
                    case "save all":
                        SaveAll();
                        break;
                    case "reload":
                        Console.WriteLine("reload command needs argument (eg reload npcs)");
                        break;
                    case "reload npcs":
                        Console.WriteLine("Reloading Npcs...");
                        InitNPC();
                        break;
                    case "reload maps":
                        Console.WriteLine("Reloading Maps...");
                        InitMap();
                        break;
                    case "reload items":
                        Console.WriteLine("Reloading Items...");
                        InitItems();
                        break;
                    case "help":
                        Console.WriteLine("Commands:");
                        Console.WriteLine("reload npcs - reloads all npcs from their bin files");
                        Console.WriteLine("reload maps - reloads all maps from their bin files");
                        Console.WriteLine("reload items - reloads all items from their bin files");
                        Console.WriteLine("save all - saves all players");
                        Console.WriteLine("shutdown - shuts down the server");
                        break;
                    default:
                        Console.WriteLine("Please enter a valid command!");
                        break;
                }
                s_userCommand = null;
            }
        }
    }
}
