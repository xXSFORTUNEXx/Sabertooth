using System;
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
        static NetServer svrServer;
        static NetPeerConfiguration Config;

        static void Main(string[] args)
        {
            Console.Title = "Sabertooth Server";
            Console.WriteLine("Initializing Server...");

            Config = new NetPeerConfiguration("sabertooth");
            Config.Port = 14242;
            Config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            Config.EnableMessageType(NetIncomingMessageType.ConnectionLatencyUpdated);
            Config.EnableMessageType(NetIncomingMessageType.DiscoveryRequest);
            Config.EnableUPnP = true;

            Console.WriteLine("Enabling message types...");
            CheckDirectories();
            LogWriter.WriteLog("Initializing Server...", "Server");
            LogWriter.WriteLog("Checking directories...", "Server");
            Console.WriteLine("Checking directories...");
            svrServer = new NetServer(Config);
            svrServer.Start();
            Console.WriteLine("Forwarding ports...");
            LogWriter.WriteLog("Forwarding ports...", "Server");
            svrServer.UPnP.ForwardPort(14242, "Sabertooth");
            Server srvrServer = new Server();
            Console.WriteLine("Server Started...");
            LogWriter.WriteLog("Server started...", "Server");
            srvrServer.ServerLoop(svrServer);
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
