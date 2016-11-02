﻿using Lidgren.Network;
using System;
using System.IO;
using System.Threading;
using System.Xml;
using static Server.Classes.LogWriter;
using static System.Console;
using static System.Environment;
using static System.IO.File;

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
        private int aiTick;
        private int spawnTick;
        private int regenTick;
        private int regenTime;
        private int hungerTime;
        private int hydrationTime;
        private int saveTime;
        private int spawnTime;
        private int aiTime;

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
                handleData.HandleDataMessage(s_Server, s_Player, s_Map, s_Npc, s_Item);
                SavePlayers();
                CheckNPCSpawn(s_Server);
                CheckNpcAI(s_Server);
                CheckHealthRegen(s_Server);
                CheckVitalLoss(s_Server);
                CheckCommands(s_Server);
            }
            WriteLine("Shutting down...");
            s_Server.Shutdown("Shutting down");
            Thread.Sleep(2500);
            Exit(0);
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
            }

            XmlReader reader = XmlReader.Create("Config.xml");
            WriteLog("Config XML file loaded.", "Server");
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
        }

        //Command window thread
        static void CommandWindow()
        {
            //WriteLine("Enter commands below, type help for commands:");
            do
            {
                Write("");
                s_userCommand = ReadLine();
            } while (s_userCommand != null);
        }

        ///Init methods
        //Creates the player array for use when players join
        void InitPlayerArray()
        {
            WriteLine("Creating player array...");
            WriteLog("Creating player array...", "Server");
            for (int i = 0; i < 5; i++)
            {
                s_Player[i] = new Player();
            }
        }

        //Loads in our maps
        void InitMap()
        {
            WriteLine("Loading maps...");
            WriteLog("Loading maps...", "Server");
            for (int i = 0; i < 10; i++)
            {
                if (!Exists("Maps/Map" + i + ".bin"))
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
            WriteLine("Loading items...");
            WriteLog("Loading npcs...", "Server");

            for (int i = 0; i < 50; i++)
            {
                if (!Exists("Items/Item" + i + ".bin"))
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
            WriteLine("Loading npcs...");
            WriteLog("Loading npcs...", "Server");

            for (int i = 0; i < 10; i++)
            {
                if (!Exists("NPCS/Npc" + i + ".bin"))
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
            WriteLine("Listening for connections...");
            WriteLog("Server is listening for connections...", "Server");
        }

        ///Saving methods
        //Saves all players online every 300000ms (5 Min)
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

        //Check and see who needs to eat some food and drink some water
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

        //Saves all online for sever command
        void SaveAll()
        {
            WriteLine("Saving players...");
            WriteLog("Saving players...", "Server");
            for (int i = 0; i < 5; i++)
            {
                if (s_Player[i].Name != null)
                {
                    s_Player[i].SavePlayerXML();
                }
            }
            WriteLine("Players saved!");
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
        void CheckCommands(NetServer s_Server)
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
                                Player ac_Player = new Player(finalInfo[1], finalInfo[2], 0, 0, 0, 0, 1, 100, 0,
                                                              100, 10, 100, 100, 5, 5, 5, 5);   //Create the player in an array so we can save it
                                ac_Player.SavePlayerXML();  //Save it
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
                    case "stats":
                        WriteLine("Statistics: ");
                        WriteLine(s_Server.Statistics);
                        break;
                    case "server":
                        WriteLine("Server Info: ");
                        break;
                    case "help":    //Help command which displays all commands, modifiers, and possible arguments
                        WriteLine("Commands:");
                        WriteLine("reload npcs - reloads all npcs from their bin files");
                        WriteLine("reload maps - reloads all maps from their bin files");
                        WriteLine("reload items - reloads all items from their bin files");
                        WriteLine("account create UN PW - creates an account with generic stats, must provide username and password");
                        WriteLine("stats - shows the servers stats");
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

        //Account exist from our handledata class because we need it for account creation from CLI
        static bool AccountExist(string name)
        {
            if (Exists("Players/" + name + ".xml"))
            {
                return true;
            }
            return false;
        }
    }
}
