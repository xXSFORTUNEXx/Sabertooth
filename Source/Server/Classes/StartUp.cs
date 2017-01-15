using Lidgren.Network;
using static Server.Classes.LogWriter;
using static System.Console;
using static System.IO.Directory;
using System.Data.SQLite;

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
            WriteLog("Checking directories and database...", "Server");
            WriteLine("Checking directories and database...");
            s_Server = new NetServer(s_Config);
            s_Server.Start();
            WriteLine("Forwarding ports...");
            WriteLog("Forwarding ports...", "Server");
            s_Server.UPnP.ForwardPort(14242, "Sabertooth");
            Server srvrServer = new Server();
            WriteLine("Server Started...");
            WriteLog("Server started...", "Server");
            srvrServer.LoadServerConfig();
            srvrServer.ServerLoop(s_Server);
        }

        static void CheckDirectories()
        {
            bool exists = false;

            if (!Exists("Maps"))
            {
                CreateDirectory("Maps");
                exists = true;
            }
            if (!Exists("Database"))
            {
                CreateDirectory("Database");
                CreateDatabase();
            }
            if (exists)
            {
                WriteLog("Directories and database created...", "Server");
            }
        }

        static void CreateDatabase()
        {
            SQLiteConnection.CreateFile("Database/Sabertooth.db");
            SQLiteConnection s_Database = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;");
            //s_Database.SetPassword("SabertoothData1379");
            s_Database.Open();
            string sql;
            SQLiteCommand sql_Command;

            sql = "CREATE TABLE `PLAYERS`";
            sql = sql + "(`NAME` TEXT, `PASSWORD` TEXT, `X` INTEGER, `Y` INTEGER, `MAP` INTEGER, `DIRECTION` INTEGER, `AIMDIRECTION` INTEGER, ";
            sql = sql + "`SPRITE` INTEGER, `LEVEL` INTEGER, `POINTS` INTEGER, `HEALTH` INTEGER, `MAXHEALTH` INTEGER, `EXPERIENCE` INTEGER, `MONEY` INTEGER, `ARMOR` INTEGER, `HUNGER` INTEGER, ";
            sql = sql + "`HYDRATION` INTEGER, `STRENGTH` INTEGER, `AGILITY` INTEGER, `ENDURANCE` INTEGER, `STAMINA` INTEGER, `PISTOLAMMO` INTEGER, `ASSAULTAMMO` INTEGER, ";
            sql = sql + "`ROCKETAMMO` INTEGER, `GRENADEAMMO` INTEGER)";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Command.ExecuteNonQuery();

            sql = "CREATE TABLE `MAINWEAPONS`";
            sql = sql + "(`OWNER` TEXT, `NAME` TEXT, `CLIP` INTEGER, `MAXCLIP` INTEGER, `SPRITE` INTEGER, `DAMAGE` INTEGER, `ARMOR` INTEGER, `TYPE` INTEGER, `ATTACKSPEED` INTEGER, `RELOADSPEED` INTEGER, ";
            sql = sql + "`HEALTHRESTORE` INTEGER, `HUNGERRESTORE` INTEGER, `HYDRATERESTORE` INTEGER, `STRENGTH` INTEGER, `AGILITY` INTEGER, `ENDURANCE` INTEGER, `STAMINA` INTEGER, `AMMOTYPE` INTEGER, `VALUE` INTEGER)";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Command.ExecuteNonQuery();

            sql = "CREATE TABLE `SECONDARYWEAPONS`";
            sql = sql + "(`OWNER` TEXT, `NAME` TEXT, `CLIP` INTEGER, `MAXCLIP` INTEGER, `SPRITE` INTEGER, `DAMAGE` INTEGER, `ARMOR` INTEGER, `TYPE` INTEGER, `ATTACKSPEED` INTEGER, `RELOADSPEED` INTEGER, ";
            sql = sql + "`HEALTHRESTORE` INTEGER, `HUNGERRESTORE` INTEGER, `HYDRATERESTORE` INTEGER, `STRENGTH` INTEGER, `AGILITY` INTEGER, `ENDURANCE` INTEGER, `STAMINA` INTEGER, `AMMOTYPE` INTEGER, `VALUE` INTEGER)";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Command.ExecuteNonQuery();

            sql = "CREATE TABLE `EQUIPMENT`";
            sql = sql + "(`OWNER` TEXT, `ID` INTEGER, `NAME` TEXT, `SPRITE` INTEGER, `DAMAGE` INTEGER, `ARMOR` INTEGER, `TYPE` INTEGER, `ATTACKSPEED` INTEGER, `RELOADSPEED` INTEGER, `HEALTHRESTORE` INTEGER, `HUNGERRESTORE` INTEGER, ";
            sql = sql + "`HYDRATERESTORE` INTEGER, `STRENGTH` INTEGER, `AGILITY` INTEGER, `ENDURANCE` INTEGER, `STAMINA` INTEGER, `CLIP` INTEGER, `MAXCLIP` INTEGER, `AMMOTYPE` INTEGER, `VALUE` INTEGER)";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Command.ExecuteNonQuery();

            sql = "CREATE TABLE `INVENTORY`";
            sql = sql + "(`OWNER` TEXT, `ID` INTEGER, `NAME` TEXT, `SPRITE` INTEGER, `DAMAGE` INTEGER, `ARMOR` INTEGER, `TYPE` INTEGER, `ATTACKSPEED` INTEGER, `RELOADSPEED` INTEGER, `HEALTHRESTORE` INTEGER, `HUNGERRESTORE` INTEGER, ";
            sql = sql + "`HYDRATERESTORE` INTEGER, `STRENGTH` INTEGER, `AGILITY` INTEGER, `ENDURANCE` INTEGER, `STAMINA` INTEGER, `CLIP` INTEGER, `MAXCLIP` INTEGER, `AMMOTYPE` INTEGER, `VALUE` INTEGER)";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Command.ExecuteNonQuery();

            sql = "CREATE TABLE `BANK`";
            sql = sql + "(`OWNER` TEXT, `ID` INTEGER, `NAME` TEXT, `SPRITE` INTEGER, `DAMAGE` INTEGER, `ARMOR` INTEGER, `TYPE` INTEGER, `ATTACKSPEED` INTEGER, `RELOADSPEED` INTEGER, `HEALTHRESTORE` INTEGER, `HUNGERRESTORE` INTEGER, ";
            sql = sql + "`HYDRATERESTORE` INTEGER, `STRENGTH` INTEGER, `AGILITY` INTEGER, `ENDURANCE` INTEGER, `STAMINA` INTEGER, `CLIP` INTEGER, `MAXCLIP` INTEGER, `AMMOTYPE` INTEGER, `VALUE` INTEGER)";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Command.ExecuteNonQuery();

            sql = "CREATE TABLE `ITEMS`";
            sql = sql + "(`NAME` TEXT, `SPRITE` INTEGER, `DAMAGE` INTEGER, `ARMOR` INTEGER, `TYPE` INTEGER, `ATTACKSPEED` INTEGER, `RELOADSPEED` INTEGER, `HEALTHRESTORE` INTEGER, `HUNGERRESTORE` INTEGER, ";
            sql = sql + "`HYDRATERESTORE` INTEGER, `STRENGTH` INTEGER, `AGILITY` INTEGER, `ENDURANCE` INTEGER, `STAMINA` INTEGER, `CLIP` INTEGER, `MAXCLIP` INTEGER, `AMMOTYPE` INTEGER, `VALUE` INTEGER)";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Command.ExecuteNonQuery();

            sql = "CREATE TABLE `NPCS`";
            sql = sql + "(`NAME` TEXT, `X` INTEGER, `Y` INTEGER, `DIRECTION` INTEGER, `SPRITE` INTEGER, `STEP` INTEGER, `OWNER` INTEGER, `BEHAVIOR` INTEGER, `SPAWNTIME` INTEGER, `HEALTH` INTEGER, `MAXHEALTH` INTEGER, `DAMAGE` INTEGER, `DESX` INTEGER, `DESY` INTEGER, ";
            sql = sql + "`EXP` INTEGER, `MONEY` INTEGER, `RANGE` INTEGER)";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Command.ExecuteNonQuery();

            sql = "CREATE TABLE `PROJECTILES`";
            sql = sql + "(`NAME` TEXT, `DAMAGE` INTEGER, `RANGE` INTEGER, `SPRITE` INTEGER, `TYPE` INTEGER, `SPEED` INTEGER)";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Command.ExecuteNonQuery();

            s_Database.Close();
        }
    }
}
