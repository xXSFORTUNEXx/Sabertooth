using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Data.SQLite;
using static System.Convert;
using static System.IO.Directory;

namespace Editor.Classes
{
    class StartUp
    {
        // Run to check how many lines of code my project has
        //Ctrl+Shift+F, use regular expression, ^(?([^\r\n])\s)*[^\s+?/]+[^\n]*$

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        [STAThread]
        static void Main(string[] args) //main entry point
        {
            var handle = GetConsoleWindow();

            Console.Title = "Sabertooth Editor Suite";    //name the console window, eventually we wont have it anymore but until then its a debug gold
            Editor editor = new Editor();  //Create class for map editor

            CheckDirectories(editor);

            Thread.Sleep(500);
            //ShowWindow(handle, SW_HIDE);
            editor.EditorLoop();  //like with any other game lets start the loop
        }

        static void CheckDirectories(Editor e_Editor)
        {
            if (!Exists("Maps"))
            {
                CreateDirectory("Maps");
                Console.WriteLine("Checking for existing map...");
                if (!File.Exists("Maps/Map.bin"))   //check and see if the map file exists
                {
                    e_Editor.e_Map.CreateDefaultMap(e_Editor.e_Map);  //call the default map method
                }
            }
            else
            {
                Console.WriteLine("Loading map...");
                e_Editor.e_Map.LoadDeafultMap();    //load the map.bin from /maps/
            }
            if (!Exists("Database"))
            {
                CreateDirectory("Database");
                CreateDatabase();
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
            sql = sql + "`SPRITE` INTEGER, `LEVEL` INTEGER, `POINTS` INTEGER, `HEALTH` INTEGER, `EXPERIENCE` INTEGER, `MONEY` INTEGER, `ARMOR` INTEGER, `HUNGER` INTEGER, ";
            sql = sql + "`HYDRATION` INTEGER, `STRENGTH` INTEGER, `AGILITY` INTEGER, `ENDURANCE` INTEGER, `STAMINA` INTEGER, `PISTOLAMMO` INTEGER, `ASSAULTAMMO` INTEGER, ";
            sql = sql + "`ROCKETAMMO` INTEGER, `GRENADEAMMO` INTEGER)";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Command.ExecuteNonQuery();

            sql = "CREATE TABLE `MAINWEAPONS`";
            sql = sql + "(`OWNER` TEXT, `NAME` TEXT, `CLIP` INTEGER, `MAXCLIP` INTEGER, `SPRITE` INTEGER, `DAMAGE` INTEGER, `ARMOR` INTEGER, `TYPE` INTEGER, `ATTACKSPEED` INTEGER, `RELOADSPEED` INTEGER, ";
            sql = sql + "`HEALTHRESTORE` INTEGER, `HUNGERRESTORE` INTEGER, `HYDRATERESTORE` INTEGER, `STRENGTH` INTEGER, `AGILITY` INTEGER, `ENDURANCE` INTEGER, `STAMINA` INTEGER, `AMMOTYPE` INTEGER)";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Command.ExecuteNonQuery();

            sql = "CREATE TABLE `SECONDARYWEAPONS`";
            sql = sql + "(`OWNER` TEXT, `NAME` TEXT, `CLIP` INTEGER, `MAXCLIP` INTEGER, `SPRITE` INTEGER, `DAMAGE` INTEGER, `ARMOR` INTEGER, `TYPE` INTEGER, `ATTACKSPEED` INTEGER, `RELOADSPEED` INTEGER, ";
            sql = sql + "`HEALTHRESTORE` INTEGER, `HUNGERRESTORE` INTEGER, `HYDRATERESTORE` INTEGER, `STRENGTH` INTEGER, `AGILITY` INTEGER, `ENDURANCE` INTEGER, `STAMINA` INTEGER, `AMMOTYPE` INTEGER)";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Command.ExecuteNonQuery();

            sql = "CREATE TABLE `ITEMS`";
            sql = sql + "(`NAME` TEXT, `SPRITE` INTEGER, `DAMAGE` INTEGER, `ARMOR` INTEGER, `TYPE` INTEGER, `ATTACKSPEED` INTEGER, `RELOADSPEED` INTEGER, `HEALTHRESTORE` INTEGER, `HUNGERRESTORE` INTEGER, ";
            sql = sql + "`HYDRATERESTORE` INTEGER, `STRENGTH` INTEGER, `AGILITY` INTEGER, `ENDURANCE` INTEGER, `STAMINA` INTEGER, `CLIP` INTEGER, `MAXCLIP` INTEGER, `AMMOTYPE` INTEGER)";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Command.ExecuteNonQuery();

            sql = "CREATE TABLE `NPCS`";
            sql = sql + "(`NAME` TEXT, `X` INTEGER, `Y` INTEGER, `DIRECTION` INTEGER, `SPRITE` INTEGER, `STEP` INTEGER, `OWNER` INTEGER, `BEHAVIOR` INTEGER, `SPAWNTIME` INTEGER, `HEALTH` INTEGER, `MAXHEALTH` INTEGER, `DAMAGE` INTEGER)";
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
