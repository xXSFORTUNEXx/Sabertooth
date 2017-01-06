using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;
using System.Xml;
using static System.Environment;
using static System.Convert;
using System.IO;
using System.Threading;
using System.Data.SQLite;

namespace Server.Classes
{
    class Player
    {
        public NetConnection Connection;
        public SQLiteConnection s_Database;
        public Item mainWeapon = new Item();
        public Item offWeapon = new Item();
        public Item[] Backpack = new Item[25];
        Random RND = new Random();

        public string Name { get; set; }
        public string Pass { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Map { get; set; }
        public int Direction { get; set; }
        public int AimDirection { get; set; }
        public int Sprite { get; set; }
        public int Level { get; set; }
        public int Points { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Hunger { get; set; }
        public int Hydration { get; set; }
        public int Experience { get; set; }
        public int Money { get; set; }
        public int Armor { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Endurance { get; set; }
        public int Stamina { get; set; }
        public int Step { get; set; }        
        public int PistolAmmo { get; set; }
        public int AssaultAmmo { get; set; }
        public int RocketAmmo { get; set; }
        public int GrenadeAmmo { get; set; }
        public int hungerTick;
        public int hydrationTick;

        public Player(string name, string pass, int x, int y, int direction, int aimdirection, int map, int level, int points, int health, int maxhealth, int exp, int money, 
                      int armor, int hunger, int hydration, int str, int agi, int end, int sta, int defaultAmmo, NetConnection conn)
        {
            Name = name;
            Pass = pass;
            X = x;
            Y = y;
            Map = map;
            Direction = direction;
            AimDirection = aimdirection;
            Level = level;
            Points = points;
            Experience = exp;
            Money = money;
            Armor = armor;
            Hunger = hunger;
            Hydration = hydration;
            Strength = str;
            Agility = agi;
            Endurance = end;
            Stamina = sta;
            Connection = conn;
            MaxHealth = maxhealth;
            Health = MaxHealth;
            hungerTick = TickCount;
            hydrationTick = TickCount;
            PistolAmmo = defaultAmmo;
            AssaultAmmo = defaultAmmo;
            RocketAmmo = 5;
            GrenadeAmmo = 3;

            mainWeapon = new Item("Assault Rifle", 1, 50, 0, (int)ItemType.RangedWeapon, 150, 1000, 0, 0, 0, 0, 0, 0, 0, 30, 30, (int)AmmoType.AssaultRifle);
            offWeapon = new Item("Knife", 1, 100, 0, (int)ItemType.MeleeWeapon, 650, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, (int)ItemType.None);

            for (int i = 0; i < 25; i++)
            {
                Backpack[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            }
        }

        public Player(string name, string pass, int x, int y, int direction, int aimdirection, int map, int level, int points, int health, int maxhealth, int exp, int money,
                      int armor, int hunger, int hydration, int str, int agi, int end, int sta, int defaultAmmo)
        {
            Name = name;
            Pass = pass;
            X = x;
            Y = y;
            Map = map;
            Direction = direction;
            AimDirection = aimdirection;
            Level = level;
            Points = points;
            Experience = exp;
            Money = money;
            Armor = armor;
            Hunger = hunger;
            Hydration = hydration;
            Strength = str;
            Agility = agi;
            Endurance = end;
            Stamina = sta;
            MaxHealth = maxhealth;
            Health = MaxHealth;
            PistolAmmo = defaultAmmo;
            AssaultAmmo = defaultAmmo;
            RocketAmmo = 5;
            GrenadeAmmo = 3;

            mainWeapon = new Item("Pistol", 1, 50, 0, (int)ItemType.RangedWeapon, 750, 1500, 0, 0, 0, 0, 0, 0, 0, 8, 8, (int)AmmoType.Pistol);
            offWeapon = new Item("Knife", 1, 100, 0, (int)ItemType.MeleeWeapon, 650, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, (int)ItemType.None);

            for (int i = 0; i < 25; i++)
            {
                Backpack[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            }
        }

        public Player(string name, string pass, NetConnection conn)
        {
            Name = name;
            Pass = pass;
            Connection = conn;
            hungerTick = TickCount;
            hydrationTick = TickCount;
        }

        public Player(NetConnection conn)
        {
            Connection = conn;
        }

        public Player() { }

        public void RegenHealth()
        {
            if (Health < MaxHealth)
            {
                Health += (Stamina * 10);
            }

            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }
        }

        public void VitalLoss(string vital)
        {
            if (vital == "food")
            {
                if (Hunger <= 0)
                {
                    //Basically we start die
                    Console.WriteLine("We start to die...");
                }
                else
                {
                    Hunger -= 10;
                }
            }

            if (vital == "water")
            {
                if (Hydration <= 0)
                {
                    //Basically we start die
                    Console.WriteLine("We start to die...");
                }
                else
                {
                    Hydration -= 10;
                }
            }
        }

        public void CheckPlayerLevelUp()
        {
            if (Experience == (Level * 1000))
            {
                Level += 1;
                Experience = 0;
                Points += 100;
                Hunger = 100;
                Hydration = 100;
                MaxHealth += (Level * 5) + RND.Next(0, 100);
                Health = MaxHealth;
                Money += 50;
                Strength += RND.Next(1, 3);
                Agility += RND.Next(1, 3);
                Endurance += RND.Next(1, 3);
                Stamina += RND.Next(1, 3);                
            }
            else if (Experience > (Level * 1000))
            {
                Level += 1;
                Experience = (Experience - (Level * 1000));
                Points += 100;
                Hunger = 100;
                Hydration = 100;
                MaxHealth += (Level * 5) + RND.Next(0, 100);
                Health = MaxHealth;
                Money += 50;
                Strength += RND.Next(1, 3);
                Agility += RND.Next(1, 3);
                Endurance += RND.Next(1, 3);
                Stamina += RND.Next(1, 3);
            }
            else { return; }
        }

        public void CreatePlayerInDatabase()
        {
            s_Database = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;");
            s_Database.Open();
            string sql;
            SQLiteCommand sql_Command;
            sql = "INSERT INTO `PLAYERS`";
            sql = sql + "(`NAME`,`PASSWORD`,`X`,`Y`,`MAP`,`DIRECTION`,`AIMDIRECTION`,`SPRITE`,`LEVEL`,`POINTS`,`HEALTH`,`MAXHEALTH`,`EXPERIENCE`,`MONEY`,`ARMOR`,";
            sql = sql + "`HUNGER`,`HYDRATION`,`STRENGTH`,`AGILITY`,`ENDURANCE`,`STAMINA`,`PISTOLAMMO`,`ASSAULTAMMO`,`ROCKETAMMO`,`GRENADEAMMO`)";
            sql = sql + " VALUES ";
            sql = sql + "('" + Name + "','" + Pass + "','" + X + "','" + Y + "','" + Map + "','" + Direction + "','" + AimDirection + "','" + Sprite + "','" + Level + "',";
            sql = sql + "'" + Points + "','" + Health + "','" + MaxHealth + "','" + Experience + "','" + Money + "','" + Armor + "','" + Hunger + "','" + Hydration + "','" + Strength + "','" + Agility + "',";
            sql = sql + "'" + Endurance + "','" + Stamina + "','" + PistolAmmo + "','" + AssaultAmmo + "','" + RocketAmmo + "','" + GrenadeAmmo + "')";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Command.ExecuteNonQuery();

            sql = "INSERT INTO `MAINWEAPONS`";
            sql = sql + "(`OWNER`,`NAME`,`CLIP`,`MAXCLIP`,`SPRITE`,`DAMAGE`,`ARMOR`,`TYPE`,`ATTACKSPEED`,`RELOADSPEED`,`HEALTHRESTORE`,`HUNGERRESTORE`,`HYDRATERESTORE`,`STRENGTH`,";
            sql = sql + "`AGILITY`,`ENDURANCE`,`STAMINA`,`AMMOTYPE`)";
            sql = sql + " VALUES ";
            sql = sql + "('" + Name + "','" + mainWeapon.Name + "','" + mainWeapon.Clip + "','" + mainWeapon.maxClip + "','" + mainWeapon.Sprite + "','" + mainWeapon.Damage + "','" + mainWeapon.Armor + "',";
            sql = sql + "'" + mainWeapon.Type + "','" + mainWeapon.AttackSpeed + "','" + mainWeapon.ReloadSpeed + "','" + mainWeapon.HealthRestore + "','" + mainWeapon.HungerRestore + "','" + mainWeapon.HydrateRestore + "',";
            sql = sql + "'" + mainWeapon.Strength + "','" + mainWeapon.Agility + "','" + mainWeapon.Endurance + "','" + mainWeapon.Stamina + "','" + mainWeapon.ammoType + "')";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Command.ExecuteNonQuery();

            sql = "INSERT INTO `SECONDARYWEAPONS`";
            sql = sql + "(`OWNER`,`NAME`,`CLIP`,`MAXCLIP`,`SPRITE`,`DAMAGE`,`ARMOR`,`TYPE`,`ATTACKSPEED`,`RELOADSPEED`,`HEALTHRESTORE`,`HUNGERRESTORE`,`HYDRATERESTORE`,`STRENGTH`,";
            sql = sql + "`AGILITY`,`ENDURANCE`,`STAMINA`,`AMMOTYPE`)";
            sql = sql + " VALUES ";
            sql = sql + "('" + Name + "','" + offWeapon.Name + "','" + offWeapon.Clip + "','" + offWeapon.maxClip + "','" + offWeapon.Sprite + "','" + offWeapon.Damage + "','" + offWeapon.Armor + "',";
            sql = sql + "'" + offWeapon.Type + "','" + offWeapon.AttackSpeed + "','" + offWeapon.ReloadSpeed + "','" + offWeapon.HealthRestore + "','" + offWeapon.HungerRestore + "','" + offWeapon.HydrateRestore + "',";
            sql = sql + "'" + offWeapon.Strength + "','" + offWeapon.Agility + "','" + offWeapon.Endurance + "','" + offWeapon.Stamina + "','" + offWeapon.ammoType + "')";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Command.ExecuteNonQuery();
            s_Database.Close();
        }

        static int FindOpenInvSlot(Item[] s_Backpack)
        {
            for (int i = 0; i < 25; i++)
            {
                if (s_Backpack[i].Name == "None")
                {
                    return i;
                }
            }
            return 25;
        }

        public void SavePlayerToDatabase()
        {
            s_Database = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;");
            s_Database.Open();
            string sql;
            SQLiteCommand sql_Command;
            sql = "UPDATE PLAYERS SET ";
            sql = sql + "NAME = '" + Name + "', PASSWORD = '" + Pass + "', X = '" + X + "', Y = '" + Y + "', MAP = '" + Map + "', DIRECTION = '" + Direction + "', ";
            sql = sql + "AIMDIRECTION = '" + AimDirection + "', SPRITE = '" + Sprite + "', LEVEL = '" + Level + "', POINTS = '" + Points + "', HEALTH = '" + Health + "', MAXHEALTH = '" + MaxHealth + "', EXPERIENCE = '" + Experience + "', ";
            sql = sql + "MONEY = '" + Money + "', ARMOR = '" + Armor + "', HUNGER = '" + Hunger + "', HYDRATION = '" + Hydration + "', STRENGTH = '" + Strength + "', AGILITY = '" + Agility + "', ENDURANCE = '" + Endurance + "', ";
            sql = sql + "STAMINA = '" + Stamina + "', PISTOLAMMO = '" + PistolAmmo + "', ASSAULTAMMO = '" + AssaultAmmo + "', ROCKETAMMO = '" + RocketAmmo + "', GRENADEAMMO = '" + GrenadeAmmo + "' ";
            sql = sql + "WHERE NAME = '" + Name + "';";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Command.ExecuteNonQuery();

            sql = "UPDATE MAINWEAPONS SET ";
            sql = sql + "CLIP = '" + mainWeapon.Clip + "' ";
            sql = sql + "WHERE OWNER = '" + Name + "';";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Command.ExecuteNonQuery();

            sql = "UPDATE SECONDARYWEAPONS SET ";
            sql = sql + "CLIP = '" + offWeapon.Clip + "' ";
            sql = sql + "WHERE OWNER = '" + Name + "';";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Command.ExecuteNonQuery();
            s_Database.Close();
        }

        public void LoadPlayerFromDatabase()
        {
            s_Database = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;");
            s_Database.Open();
            string sql;

            sql = "SELECT * FROM `PLAYERS` WHERE NAME = '" + Name + "'";

            SQLiteCommand sql_Command = new SQLiteCommand(sql, s_Database);
            SQLiteDataReader sql_Reader = sql_Command.ExecuteReader();

            while (sql_Reader.Read())
            {
                Name = sql_Reader["NAME"].ToString();
                Pass = sql_Reader["PASSWORD"].ToString();
                X = ToInt32(sql_Reader["X"].ToString());
                Y = ToInt32(sql_Reader["Y"].ToString());
                Map = ToInt32(sql_Reader["MAP"].ToString());
                Direction = ToInt32(sql_Reader["DIRECTION"].ToString());
                AimDirection = ToInt32(sql_Reader["AIMDIRECTION"].ToString());
                Sprite = ToInt32(sql_Reader["SPRITE"].ToString());
                Level = ToInt32(sql_Reader["LEVEL"].ToString());
                Points = ToInt32(sql_Reader["POINTS"].ToString());
                Health = ToInt32(sql_Reader["HEALTH"].ToString());
                MaxHealth = ToInt32(sql_Reader["MAXHEALTH"].ToString());
                Experience = ToInt32(sql_Reader["EXPERIENCE"].ToString());
                Money = ToInt32(sql_Reader["MONEY"].ToString());
                Armor = ToInt32(sql_Reader["ARMOR"].ToString());
                Hunger = ToInt32(sql_Reader["HUNGER"].ToString());
                Hydration = ToInt32(sql_Reader["HYDRATION"].ToString());
                Strength = ToInt32(sql_Reader["STRENGTH"].ToString());
                Agility = ToInt32(sql_Reader["AGILITY"].ToString());
                Endurance = ToInt32(sql_Reader["ENDURANCE"].ToString());
                Stamina = ToInt32(sql_Reader["STAMINA"].ToString());
                PistolAmmo = ToInt32(sql_Reader["PISTOLAMMO"].ToString());
                AssaultAmmo = ToInt32(sql_Reader["ASSAULTAMMO"].ToString());
                RocketAmmo = ToInt32(sql_Reader["ROCKETAMMO"].ToString());
                GrenadeAmmo = ToInt32(sql_Reader["GRENADEAMMO"].ToString());
            }

            sql = "SELECT * FROM `MAINWEAPONS` WHERE OWNER = '" + Name + "'";

            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Reader = sql_Command.ExecuteReader();

            while (sql_Reader.Read())
            {
                mainWeapon.Name = sql_Reader["NAME"].ToString();
                mainWeapon.Clip = ToInt32(sql_Reader["CLIP"].ToString());
                mainWeapon.maxClip = ToInt32(sql_Reader["MAXCLIP"].ToString());
                mainWeapon.Sprite = ToInt32(sql_Reader["SPRITE"].ToString());
                mainWeapon.Damage = ToInt32(sql_Reader["DAMAGE"].ToString());
                mainWeapon.Armor = ToInt32(sql_Reader["ARMOR"].ToString());
                mainWeapon.Type = ToInt32(sql_Reader["TYPE"].ToString());
                mainWeapon.AttackSpeed = ToInt32(sql_Reader["ATTACKSPEED"].ToString());
                mainWeapon.ReloadSpeed = ToInt32(sql_Reader["RELOADSPEED"].ToString());
                mainWeapon.HealthRestore = ToInt32(sql_Reader["HEALTHRESTORE"].ToString());
                mainWeapon.HungerRestore = ToInt32(sql_Reader["HUNGERRESTORE"].ToString());
                mainWeapon.HydrateRestore = ToInt32(sql_Reader["HYDRATERESTORE"].ToString());
                mainWeapon.Strength = ToInt32(sql_Reader["STRENGTH"].ToString());
                mainWeapon.Agility = ToInt32(sql_Reader["AGILITY"].ToString());
                mainWeapon.Endurance = ToInt32(sql_Reader["ENDURANCE"].ToString());
                mainWeapon.Stamina = ToInt32(sql_Reader["STAMINA"].ToString());
                mainWeapon.ammoType = ToInt32(sql_Reader["AMMOTYPE"].ToString());
            }

            sql = "SELECT * FROM `SECONDARYWEAPONS` WHERE OWNER = '" + Name + "'";

            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Reader = sql_Command.ExecuteReader();

            while (sql_Reader.Read())
            {
                offWeapon.Name = sql_Reader["NAME"].ToString();
                offWeapon.Clip = ToInt32(sql_Reader["CLIP"].ToString());
                offWeapon.maxClip = ToInt32(sql_Reader["MAXCLIP"].ToString());
                offWeapon.Sprite = ToInt32(sql_Reader["SPRITE"].ToString());
                offWeapon.Damage = ToInt32(sql_Reader["DAMAGE"].ToString());
                offWeapon.Armor = ToInt32(sql_Reader["ARMOR"].ToString());
                offWeapon.Type = ToInt32(sql_Reader["TYPE"].ToString());
                offWeapon.AttackSpeed = ToInt32(sql_Reader["ATTACKSPEED"].ToString());
                offWeapon.ReloadSpeed = ToInt32(sql_Reader["RELOADSPEED"].ToString());
                offWeapon.HealthRestore = ToInt32(sql_Reader["HEALTHRESTORE"].ToString());
                offWeapon.HungerRestore = ToInt32(sql_Reader["HUNGERRESTORE"].ToString());
                offWeapon.HydrateRestore = ToInt32(sql_Reader["HYDRATERESTORE"].ToString());
                offWeapon.Strength = ToInt32(sql_Reader["STRENGTH"].ToString());
                offWeapon.Agility = ToInt32(sql_Reader["AGILITY"].ToString());
                offWeapon.Endurance = ToInt32(sql_Reader["ENDURANCE"].ToString());
                offWeapon.Stamina = ToInt32(sql_Reader["STAMINA"].ToString());
                offWeapon.ammoType = ToInt32(sql_Reader["AMMOTYPE"].ToString());
            }
            s_Database.Close();
        }
    }

    public enum Directions
    {
        Down,
        Left,
        Right,
        Up,
        DownLeft,
        DownRight,
        UpLeft,
        UpRight
    }
}
