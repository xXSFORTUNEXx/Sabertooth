using Lidgren.Network;
using static Server.Classes.LogWriter;
using static System.Console;
using static System.IO.Directory;

namespace Server.Classes
{
    class StartUp
    {

        // Run to check how many lines of code my project has
        //Ctrl+Shift+F, use regular expression, ^(?([^\r\n])\s)*[^\s+?/]+[^\n]*$

        static NetServer s_Server;
        static NetPeerConfiguration s_Config;

        static void Main(string[] args)
        {
            Title = "Sabertooth Server";
            WriteLine(@"  _____       _               _              _   _     ");
            WriteLine(@" / ____|     | |             | |            | | | |    ");
            WriteLine(@"| (___   __ _| |__   ___ _ __| |_ ___   ___ | |_| |__  ");
            WriteLine(@" \___ \ / _` | '_ \ / _ \ '__| __/ _ \ / _ \| __| '_ \ ");
            WriteLine(@" ____) | (_| | |_) |  __/ |  | || (_) | (_) | |_| | | |");
            WriteLine(@"|_____/ \__,_|_.__/ \___|_|   \__\___/ \___/ \__|_| |_|");
            WriteLine(@"                              Created by Steven Fortune");
            WriteLine("Loading...Please wait...");
            WriteLog("Loading...Please wait...", "Server");

            s_Config = new NetPeerConfiguration("sabertooth");
            s_Config.Port = 14242;
            s_Config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            s_Config.EnableMessageType(NetIncomingMessageType.ConnectionLatencyUpdated);
            s_Config.EnableMessageType(NetIncomingMessageType.DiscoveryRequest);
            s_Config.DisableMessageType(NetIncomingMessageType.DebugMessage);
            s_Config.DisableMessageType(NetIncomingMessageType.Error);
            s_Config.DisableMessageType(NetIncomingMessageType.NatIntroductionSuccess);
            s_Config.DisableMessageType(NetIncomingMessageType.Receipt);
            s_Config.DisableMessageType(NetIncomingMessageType.UnconnectedData);
            s_Config.DisableMessageType(NetIncomingMessageType.VerboseDebugMessage);
            s_Config.DisableMessageType(NetIncomingMessageType.WarningMessage);
            s_Config.UseMessageRecycling = true;
            s_Config.MaximumTransmissionUnit = 1500;
            s_Config.MaximumConnections = 5;
            s_Config.EnableUPnP = true;

            WriteLine("Enabling message types...");
            CheckDirectories();
            WriteLog("Checking directories...", "Server");
            WriteLine("Checking directories...");
            s_Server = new NetServer(s_Config);
            s_Server.Start();
            WriteLine("Forwarding ports...");
            WriteLog("Forwarding ports...", "Server");
            s_Server.UPnP.ForwardPort(14242, "Sabertooth");
            Server srvrServer = new Server();
            WriteLine("Server Started...");
            WriteLog("Server started...", "Server");
            srvrServer.LoadServerConfig();
            WriteLine("Configuration loaded...");
            srvrServer.ServerLoop(s_Server);
        }

        static void CheckDirectories()
        {
            bool exists = false;

            if (!Exists("Players"))
            {
                CreateDirectory("Players");
                exists = true;
            }
            if (!Exists("Maps"))
            {
                CreateDirectory("Maps");
                exists = true;
            }
            if (!Exists("NPCS"))
            {
                CreateDirectory("NPCS");
                exists = true;
            }
            if (!Exists("Items"))
            {
                CreateDirectory("Items");
                exists = true;
            }
            if (!Exists("Projectiles"))
            {
                CreateDirectory("Projectiles");
                exists = true;
            }
            if (exists)
            {
                WriteLog("Directories created...", "Server");
            }
        }
    }
}
