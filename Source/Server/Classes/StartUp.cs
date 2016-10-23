﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;
using System.IO;

namespace Server.Classes
{
    class StartUp
    {
        static NetServer s_Server;
        static NetPeerConfiguration s_Config;

        static void Main(string[] args)
        {
            Console.Title = "Sabertooth Server";
            Console.WriteLine("Initializing Server...");

            s_Config = new NetPeerConfiguration("sabertooth");
            s_Config.Port = 14242;
            s_Config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            s_Config.EnableMessageType(NetIncomingMessageType.ConnectionLatencyUpdated);
            s_Config.EnableMessageType(NetIncomingMessageType.DiscoveryRequest);
            s_Config.EnableUPnP = true;

            Console.WriteLine("Enabling message types...");
            CheckDirectories();
            LogWriter.WriteLog("Initializing Server...", "Server");
            LogWriter.WriteLog("Checking directories...", "Server");
            Console.WriteLine("Checking directories...");
            s_Server = new NetServer(s_Config);
            s_Server.Start();
            Console.WriteLine("Forwarding ports...");
            LogWriter.WriteLog("Forwarding ports...", "Server");
            s_Server.UPnP.ForwardPort(14242, "Sabertooth");
            Server srvrServer = new Server();
            Console.WriteLine("Server Started...");
            LogWriter.WriteLog("Server started...", "Server");
            srvrServer.ServerLoop(s_Server);
        }

        static void CheckDirectories()
        {
            bool exists = false;

            if (!Directory.Exists("Players"))
            {
                Directory.CreateDirectory("Players");
                exists = true;
            }
            if (!Directory.Exists("Maps"))
            {
                Directory.CreateDirectory("Maps");
                exists = true;
            }
            if (!Directory.Exists("Logs"))
            {
                Directory.CreateDirectory("Logs");
                exists = true;
            }
            if (!Directory.Exists("NPCS"))
            {
                Directory.CreateDirectory("NPCS");
                exists = true;
            }

            if (exists)
            {
                LogWriter.WriteLog("Directories created...", "Server");
            }
        }
    }
}
