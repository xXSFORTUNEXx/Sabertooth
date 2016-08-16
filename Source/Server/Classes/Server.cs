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
        Player[] svrPlayer = new Player[5];
        NPC[] svrNpc = new NPC[10];
        HandleData handleData = new HandleData();
        Map[] svrMap = new Map[10];
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

        public void ServerLoop(NetServer svrServer)
        {
            InitPlayerArray();
            InitMap();
            InitNPC();

            Console.WriteLine("Listening for connections...");
            LogWriter.WriteLog("Server is listening for connections...", "Server");

            while (true)
            {
                //Console.Title = "Sabertooth Server - Bind IP: " + svrServer.Socket.LocalEndPoint + " CPS: " + CalculateCycleRate();
                handleData.HandleDataMessage(svrServer, svrPlayer, svrMap, svrNpc);

                if (TickCount - saveTick > saveTime)
                {
                    SavePlayers();
                    saveTick = TickCount;
                }
                CheckNPCSpawn(svrServer);
                CheckNpcAI(svrServer);
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
                svrPlayer[i] = new Player();
            }
        }

        void SavePlayers()
        {
            Console.WriteLine("Saving players...");
            LogWriter.WriteLog("Saving players...", "Server");
            for (int i = 0; i < 5; i++)
            {
                if (svrPlayer[i].Name != null)
                {
                    svrPlayer[i].SavePlayerXML();
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
                    svrMap[i] = new Map();
                    svrMap[i].GenerateMap(i);
                    svrMap[i].SaveMap(i);
                }
                else
                {
                    svrMap[i] = new Map();
                    svrMap[i].LoadMap(i);
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
                    svrNpc[i] = new NPC("Default", 10, 10, (int)Directions.Down, 0, 0, 0, (int)BehaviorType.Friendly, 5000);
                    svrNpc[i].SaveNPC(i);
                }
                else
                {
                    svrNpc[i] = new NPC();
                    svrNpc[i].LoadNPC(i);
                }
            }

            //Load in data we need for mapnpcs
            for (int i = 0; i < 10; i++)
            {
                for (int n = 0; n < 10; n++)
                {
                    int num = svrMap[i].mapNpc[n].npcNum;
                    svrMap[i].mapNpc[n].Name = svrNpc[num].Name;
                    svrMap[i].mapNpc[n].X = svrNpc[num].X;
                    svrMap[i].mapNpc[n].Y = svrNpc[num].Y;
                    svrMap[i].mapNpc[n].Direction = svrNpc[num].Direction;
                    svrMap[i].mapNpc[n].Step = svrNpc[num].Step;
                    svrMap[i].mapNpc[n].Sprite = svrNpc[num].Sprite;
                    svrMap[i].mapNpc[n].Behavior = svrNpc[num].Behavior;
                    svrMap[i].mapNpc[n].Owner = svrNpc[num].Owner;
                    svrMap[i].mapNpc[n].isSpawned = svrNpc[num].isSpawned;
                }
            }
        }

        void CheckNpcAI(NetServer svrServer)
        {
            if (TickCount - aiTick > aiTime)
            {
                for (int i = 0; i < 10; i++)
                {
                    for (int n = 0; n < 10; n++)
                    {
                        if (svrMap[i].mapNpc[n].isSpawned == true)
                        {
                            int canMove = RND.Next(0, 100);
                            int dir = RND.Next(0, 3);

                            svrMap[i].mapNpc[n].NpcAI(canMove, dir, svrMap[i]);

                            if (svrMap[i].mapNpc[n].didMove == true)
                            {
                                svrMap[i].mapNpc[n].didMove = false;
                                
                                for (int p = 0; p < 5; p++)
                                {
                                    if (svrPlayer[p].Connection != null && svrPlayer[p].Map == i)
                                    {
                                        handleData.SendMapNpcData(svrServer, svrPlayer[p].Connection, svrMap[i], n);
                                    }
                                }
                            }
                        }
                    }
                }
                aiTick = TickCount;
            }
        }

        void CheckNPCSpawn(NetServer svrServer)
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
                            if (svrMap[i].Ground[x, y].type == (int)TileType.NPCSpawn)
                            {
                                int npcNum = svrMap[i].Ground[x, y].spawnNum;

                                if (svrMap[i].mapNpc[npcNum].isSpawned == false)
                                {
                                    svrMap[i].mapNpc[npcNum].X = x;
                                    svrMap[i].mapNpc[npcNum].Y = y;
                                    svrMap[i].mapNpc[npcNum].isSpawned = true;

                                    for (int p = 0; p < 5; p++)
                                    {
                                        if (svrPlayer[p].Connection != null && i == svrPlayer[p].Map)
                                        {
                                            handleData.SendMapNpcData(svrServer, svrPlayer[p].Connection, svrMap[i], npcNum);
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
