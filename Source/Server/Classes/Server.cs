using System;
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
        HandleData handleData = new HandleData();
        Map[] s_Map = new Map[10];
        Random RND = new Random();
        static int lastTick;
        static int lastCycleRate;
        static int cycleRate;
        private int saveTick;
        private int saveTime = 300000;
        private int aiTick;
        private int aiTime = 1000;
        private int spawnTick;
        private int spawnTime = 1000;

        public void ServerLoop(NetServer s_Server)
        {
            InitPlayerArray();
            InitMap();
            InitNPC();

            Console.WriteLine("Listening for connections...");
            LogWriter.WriteLog("Server is listening for connections...", "Server");

            while (true)
            {
                //Console.Title = "Sabertooth Server - Bind IP: " + svrServer.Socket.LocalEndPoint + " CPS: " + CalculateCycleRate();
                handleData.HandleDataMessage(s_Server, s_Player, s_Map, s_Npc);

                if (TickCount - saveTick > saveTime)
                {
                    SavePlayers();
                    saveTick = TickCount;
                }
                CheckNPCSpawn(s_Server);
                CheckNpcAI(s_Server);
            }
        }

        static int CalculateCycleRate()
        {
            if (TickCount - lastTick >= 1000)
            {
                lastCycleRate = cycleRate;
                cycleRate = 0;
                lastTick = TickCount;
            }
            cycleRate++;
            return lastCycleRate;
        }

        void InitPlayerArray()
        {
            Console.WriteLine("Creating player array...");
            LogWriter.WriteLog("Creating player array...", "Server");
            for (int i = 0; i < 5; i++)
            {
                s_Player[i] = new Player();
            }
        }

        void SavePlayers()
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
        }

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
    }
}
