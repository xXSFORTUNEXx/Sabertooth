using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Editor.Classes;
using Editor.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Data.SQLite;
using SFML.Graphics;
using SFML.Window;
using static System.Convert;
using static System.IO.Directory;

namespace Editor
{
    public partial class Editor : Form
    {
        public Editor()
        {
            InitializeComponent();
            CheckDirectories();
        }

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            string unlockPass = txtUnlock.Text;
            if (unlockPass == "fortune") { pnlLock.Visible = false; }
            else { lblIncorrect.Visible = true; }            
        }

        private void CheckDirectories()
        {
            if (!Exists("Maps"))
            {
                CreateDirectory("Maps");
            }
            if (!File.Exists("Maps/Map0.bin"))
            {
                Map e_Map = new Map();
                e_Map.CreateDefaultMap(e_Map);
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

        private void btnItemEditor_Click(object sender, EventArgs e)
        {
            ItemEditor e_ItemEditor = new ItemEditor();
            e_ItemEditor.ShowDialog();
        }

        private void btnNpcEditor_Click(object sender, EventArgs e)
        {
            NpcEditor e_NpcEditor = new NpcEditor();
            e_NpcEditor.ShowDialog();
        }

        private void btnMapEditor_Click(object sender, EventArgs e)
        {
            MapEditor e_MapEditor = new MapEditor();
        }
    }
}
