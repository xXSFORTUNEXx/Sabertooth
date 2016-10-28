using Lidgren.Network;
using System;
using System.IO;
using System.Xml;
using System.Runtime.InteropServices;
using System.Threading;

namespace Client.Classes
{
    class StartUp
    {
        static NetClient c_Client;
        static NetPeerConfiguration c_Config;

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        [STAThread]
        static void Main(string[] args)
        {
            var handle = GetConsoleWindow();
            Console.Title = "Sabertooth Console - Debug Info";  //set console title
            Console.WriteLine("Initializing client...");    //inform the user whats going on
            ClientConfig cConfig = new ClientConfig();  //load client configuration
            c_Config = new NetPeerConfiguration("sabertooth");    //create a new peer config
            c_Config.EnableMessageType(NetIncomingMessageType.DiscoveryResponse); //enable message type for discovery response
            c_Config.EnableMessageType(NetIncomingMessageType.ConnectionLatencyUpdated);  //enable message for latency
            Console.WriteLine("Enabling message types..."); //inform the user whats going on
            c_Client = new NetClient(c_Config);  //create the client with the peer config
            Console.WriteLine("Loading config..."); //inform the user whats going on
            c_Client.Start();  //start the client
            Console.WriteLine("Client started..."); //let the user know whats up
            Game c_Game = new Game();  //create game class
            ShowWindow(handle, SW_HIDE);
            c_Game.GameLoop(c_Client, cConfig);    //start game loop
        }
    }

    class ClientConfig
    {
        public string savedUser { get; set; }
        public string savedPass { get; set; }
        public string saveCreds { get; set; }
        public string ipAddress { get; set; }
        public string port { get; set; }

        public ClientConfig()
        {
            CheckIfConfigExists();
        }

        public void CheckIfConfigExists()
        {
            if (!File.Exists("Config.xml"))
            {
                saveCreds = "0";
                ipAddress = "127.0.0.1";
                port = "14242";
                SaveConfig();
            }
            LoadConfig();
        }

        public void LoadConfig()
        {
            XmlReader reader = XmlReader.Create("Config.xml");
            reader.ReadToFollowing("savedUser");
            savedUser = reader.ReadElementContentAsString();
            reader.ReadToFollowing("savedPass");
            savedPass = reader.ReadElementContentAsString();
            reader.ReadToFollowing("saveCreds");
            saveCreds = reader.ReadElementContentAsString();
            reader.ReadToFollowing("ipAddress");
            ipAddress = reader.ReadElementContentAsString();
            reader.ReadToFollowing("port");
            port = reader.ReadElementContentAsString();
            reader.Close();
        }

        public void SaveConfig()
        {
            XmlWriterSettings configData = new XmlWriterSettings();
            configData.Indent = true;
            XmlWriter writer = XmlWriter.Create("Config.xml", configData);
            writer.WriteStartDocument();
            writer.WriteStartElement("Configuration");
            writer.WriteElementString("savedUser", savedUser);
            writer.WriteElementString("savedPass", savedPass);
            writer.WriteElementString("saveCreds", saveCreds);
            writer.WriteElementString("ipAddress", ipAddress);
            writer.WriteElementString("port", port);
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }
    }
}
