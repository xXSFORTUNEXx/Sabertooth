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
        Player[] svrPlayer = new Player[5];
        NPC[] svrNpc = new NPC[10];
        HandleData handleData = new HandleData();
        Map svrMap = new Map();
        static int lastTick;
        static int lastCycleRate;
        static int cycleRate;
        private int saveTick;
        private static int restTick;
        private int saveTime = 300000;

        public void ServerLoop(NetServer svrServer)
        {
            InitPlayerArray();
            InitMap();
            InitNPC();

            Console.WriteLine("Listening for connections...");
            LogWriter.WriteLog("Server is listening for connections...", "Server");

            while (true)
            {
                Console.Title = "Sabertooth Server - Bind IP: " + svrServer.Socket.LocalEndPoint + " CPS: " + CalculateCycleRate();
                handleData.HandleDataMessage(svrServer, svrPlayer, svrMap, svrNpc);

                if (TickCount - saveTick > saveTime)
                {
                    SavePlayers();
                    saveTick = TickCount;
                }
                RestDuringDebug();
            }
        }

        static void RestDuringDebug()   //This is for debugging so my laptops bat doesnt die so fast
        {
            if (TickCount - restTick > 60000)
            {
                LogWriter.WriteLog("Sleeping to reduce CPU usage for debugging, needs to be disabled later causes lag", "Server");
                restTick = TickCount;
            }
            Thread.Sleep(30);
        }

        void CheckNPCSpawn()
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
                if (!File.Exists("NPCS/Npc" + (i + 1) + ".bin"))
                {
                    svrNpc[i] = new NPC("Default", 10, 10, (int)Directions.Down, 0, 0, 0, (int)BehaviorType.Friendly, 5000);
                    svrNpc[i].SaveNPC(i + 1);
                }
                else
                {
                    svrNpc[i] = new NPC();
                    svrNpc[i].LoadNPC(i + 1);
                }
            }
        }
    }
}
