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
        Map svrMap = new Map();
        Random RND = new Random();
        static int lastTick;
        static int lastCycleRate;
        static int cycleRate;
        private int saveTick;
        private int saveTime = 300000;
        private int aiTick;
        private int aiTime = 1000;

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

        void CheckNPCSpawn(NetServer svrServer)
        {
            //Check for map spawning
            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    if (svrMap.Ground[x, y].type == (int)TileType.NPCSpawn)
                    {
                        int npcNum = svrMap.Ground[x, y].spawnNum;

                        if (svrNpc[npcNum].isSpawned == false)
                        {
                            svrNpc[npcNum].X = x;
                            svrNpc[npcNum].Y = y;
                            svrNpc[npcNum].isSpawned = true;
                            handleData.SendNpcData(svrServer, svrNpc, npcNum);
                        }
                    }
                }
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
            if (!File.Exists("Maps/Map.bin"))
            {
                svrMap.GenerateMap("Home");
                svrMap.SaveMap();
            }
            svrMap.LoadMap();
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
        }

        void CheckNpcAI(NetServer svrServer)
        {
            if (TickCount - aiTick > aiTime)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (svrNpc[i].isSpawned == true)
                    {
                        int canMove = RND.Next(0, 100);
                        int dir = RND.Next(0, 3);

                        svrNpc[i].NpcAI(canMove, dir, svrMap);

                        if (svrNpc[i].didMove == true)
                        {
                            svrNpc[i].didMove = false;
                            handleData.SendNpcData(svrServer, svrNpc, i);
                        }
                    }
                }
                aiTick = TickCount;
            }
        }
    }
}
