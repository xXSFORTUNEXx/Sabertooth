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

            for (int i = 0; i < 25; i++)
            {
                Backpack[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            }
        }

        public Player(NetConnection conn)
        {
            Connection = conn;
        }

        public Player()
        {
            for (int i = 0; i < 25; i++)
            {
                Backpack[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            }
        }

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

        public void CheckPickup(NetServer s_Server, Map s_Map, Player[] s_Player, Item[] s_Item, int index)
        {
            for (int c = 0; c < 20; c++)
            {
                if (s_Map.mapItem[c] != null && s_Map.mapItem[c].IsSpawned)
                {
                    if ((X + 12) == s_Map.mapItem[c].X && (Y + 9) == s_Map.mapItem[c].Y)
                    {
                        PickUpItem(s_Server, s_Player, s_Map.mapItem[c], s_Map, index, c);
                        break;
                    }
                }
            }
        }

        void PickUpItem(NetServer s_Server, Player[] s_Player, Item s_Item, Map s_Map, int index, int itemNum)
        {
            HandleData hData = new HandleData();
            int itemSlot = FindOpenInvSlot(Backpack);

            if (itemSlot < 25)
            {
                Backpack[itemSlot].Name = s_Item.Name;
                Backpack[itemSlot].Sprite = s_Item.Sprite;
                Backpack[itemSlot].Damage = s_Item.Damage;
                Backpack[itemSlot].Armor = s_Item.Armor;
                Backpack[itemSlot].Type = s_Item.Type;
                Backpack[itemSlot].AttackSpeed = s_Item.AttackSpeed;
                Backpack[itemSlot].ReloadSpeed = s_Item.ReloadSpeed;
                Backpack[itemSlot].HealthRestore = s_Item.HealthRestore;
                Backpack[itemSlot].HungerRestore = s_Item.HungerRestore;
                Backpack[itemSlot].HydrateRestore = s_Item.HydrateRestore;
                Backpack[itemSlot].Strength = s_Item.Strength;
                Backpack[itemSlot].Agility = s_Item.Agility;
                Backpack[itemSlot].Endurance = s_Item.Endurance;
                Backpack[itemSlot].Stamina = s_Item.Stamina;
                Backpack[itemSlot].Clip = s_Item.Clip;
                Backpack[itemSlot].MaxClip = s_Item.MaxClip;
                Backpack[itemSlot].ItemAmmoType = s_Item.ItemAmmoType;
                Backpack[itemSlot].Value = s_Item.Value;

                int TileX = s_Map.mapItem[itemNum].X;
                int TileY = s_Map.mapItem[itemNum].Y;
                s_Map.Ground[TileX, TileY].NeedsSpawnedTick = TickCount;
                s_Map.mapItem[itemNum].Name = "None";
                s_Map.mapItem[itemNum].IsSpawned = false;

                for (int p = 0; p < 5; p++)
                {
                    if (s_Player[p].Connection != null && Map == s_Player[p].Map)
                    {
                        hData.SendMapItemData(s_Server, s_Player[p].Connection, s_Map, itemNum);
                    }
                }
                hData.SendPlayerInv(s_Server, s_Player, index);
            }
            else
            {
                hData.SendServerMessage(s_Server, "Inventory is full!");
                return;
            }
        }

        public int FindOpenInvSlot(Item[] s_Backpack)
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
            sql = sql + "('" + Name + "','" + mainWeapon.Name + "','" + mainWeapon.Clip + "','" + mainWeapon.MaxClip + "','" + mainWeapon.Sprite + "','" + mainWeapon.Damage + "','" + mainWeapon.Armor + "',";
            sql = sql + "'" + mainWeapon.Type + "','" + mainWeapon.AttackSpeed + "','" + mainWeapon.ReloadSpeed + "','" + mainWeapon.HealthRestore + "','" + mainWeapon.HungerRestore + "','" + mainWeapon.HydrateRestore + "',";
            sql = sql + "'" + mainWeapon.Strength + "','" + mainWeapon.Agility + "','" + mainWeapon.Endurance + "','" + mainWeapon.Stamina + "','" + mainWeapon.ItemAmmoType + "')";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Command.ExecuteNonQuery();

            sql = "INSERT INTO `SECONDARYWEAPONS`";
            sql = sql + "(`OWNER`,`NAME`,`CLIP`,`MAXCLIP`,`SPRITE`,`DAMAGE`,`ARMOR`,`TYPE`,`ATTACKSPEED`,`RELOADSPEED`,`HEALTHRESTORE`,`HUNGERRESTORE`,`HYDRATERESTORE`,`STRENGTH`,";
            sql = sql + "`AGILITY`,`ENDURANCE`,`STAMINA`,`AMMOTYPE`)";
            sql = sql + " VALUES ";
            sql = sql + "('" + Name + "','" + offWeapon.Name + "','" + offWeapon.Clip + "','" + offWeapon.MaxClip + "','" + offWeapon.Sprite + "','" + offWeapon.Damage + "','" + offWeapon.Armor + "',";
            sql = sql + "'" + offWeapon.Type + "','" + offWeapon.AttackSpeed + "','" + offWeapon.ReloadSpeed + "','" + offWeapon.HealthRestore + "','" + offWeapon.HungerRestore + "','" + offWeapon.HydrateRestore + "',";
            sql = sql + "'" + offWeapon.Strength + "','" + offWeapon.Agility + "','" + offWeapon.Endurance + "','" + offWeapon.Stamina + "','" + offWeapon.ItemAmmoType + "')";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Command.ExecuteNonQuery();
            s_Database.Close();
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

            sql = "DELETE FROM INVENTORY WHERE OWNER = '" + Name + "'";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Command.ExecuteNonQuery();

            for (int i = 0; i < 25; i++)
            {
                if (Backpack[i].Name != "None")
                {
                    sql = "INSERT INTO `INVENTORY`";
                    sql = sql + "(`OWNER`,`ID`,`NAME`,`SPRITE`,`DAMAGE`,`ARMOR`,`TYPE`,`ATTACKSPEED`,`RELOADSPEED`,`HEALTHRESTORE`,`HUNGERRESTORE`,`HYDRATERESTORE`,";
                    sql = sql + "`STRENGTH`,`AGILITY`,`ENDURANCE`,`STAMINA`,`CLIP`,`MAXCLIP`,`AMMOTYPE`)";
                    sql = sql + " VALUES ";
                    sql = sql + "('" + Name + "','" + i + "','" + Backpack[i].Name + "','" + Backpack[i].Sprite + "','" + Backpack[i].Damage + "','" + Backpack[i].Armor + "','" + Backpack[i].Type + "','" + Backpack[i].AttackSpeed + "','" + Backpack[i].ReloadSpeed + "','" + Backpack[i].HealthRestore + "','" + Backpack[i].HungerRestore + "',";
                    sql = sql + "'" + Backpack[i].HydrateRestore + "','" + Backpack[i].Strength + "','" + Backpack[i].Agility + "','" + Backpack[i].Endurance + "','" + Backpack[i].Stamina + "','" + Backpack[i].Clip + "','" + Backpack[i].MaxClip + "','" + Backpack[i].ItemAmmoType + "');";
                    sql_Command = new SQLiteCommand(sql, s_Database);
                    sql_Command.ExecuteNonQuery();
                }
            }
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
                mainWeapon.MaxClip = ToInt32(sql_Reader["MAXCLIP"].ToString());
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
                mainWeapon.ItemAmmoType = ToInt32(sql_Reader["AMMOTYPE"].ToString());
            }

            sql = "SELECT * FROM `SECONDARYWEAPONS` WHERE OWNER = '" + Name + "'";

            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Reader = sql_Command.ExecuteReader();

            while (sql_Reader.Read())
            {
                offWeapon.Name = sql_Reader["NAME"].ToString();
                offWeapon.Clip = ToInt32(sql_Reader["CLIP"].ToString());
                offWeapon.MaxClip = ToInt32(sql_Reader["MAXCLIP"].ToString());
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
                offWeapon.ItemAmmoType = ToInt32(sql_Reader["AMMOTYPE"].ToString());
            }

            sql = "SELECT COUNT(*) FROM `INVENTORY` WHERE OWNER = '" + Name + "'";
            sql_Command = new SQLiteCommand(sql, s_Database);
            int result = int.Parse(sql_Command.ExecuteScalar().ToString());

            if (result > 0)
            {
                for (int i = 0; i < result; i++)
                {
                    sql = "SELECT * FROM `INVENTORY` WHERE OWNER = '" + Name + "' AND ID = " + i + ";";

                    sql_Command = new SQLiteCommand(sql, s_Database);
                    sql_Reader = sql_Command.ExecuteReader();

                    while (sql_Reader.Read())
                    {
                        Backpack[i].Name = sql_Reader["NAME"].ToString();
                        Backpack[i].Sprite = ToInt32(sql_Reader["SPRITE"].ToString());
                        Backpack[i].Damage = ToInt32(sql_Reader["DAMAGE"].ToString());
                        Backpack[i].Armor = ToInt32(sql_Reader["ARMOR"].ToString());
                        Backpack[i].Type = ToInt32(sql_Reader["TYPE"].ToString());
                        Backpack[i].AttackSpeed = ToInt32(sql_Reader["ATTACKSPEED"].ToString());
                        Backpack[i].ReloadSpeed = ToInt32(sql_Reader["RELOADSPEED"].ToString());
                        Backpack[i].HealthRestore = ToInt32(sql_Reader["HEALTHRESTORE"].ToString());
                        Backpack[i].HungerRestore = ToInt32(sql_Reader["HUNGERRESTORE"].ToString());
                        Backpack[i].HydrateRestore = ToInt32(sql_Reader["HYDRATERESTORE"].ToString());
                        Backpack[i].Strength = ToInt32(sql_Reader["STRENGTH"].ToString());
                        Backpack[i].Agility = ToInt32(sql_Reader["AGILITY"].ToString());
                        Backpack[i].Endurance = ToInt32(sql_Reader["ENDURANCE"].ToString());
                        Backpack[i].Stamina = ToInt32(sql_Reader["STAMINA"].ToString());
                        Backpack[i].Clip = ToInt32(sql_Reader["CLIP"].ToString());
                        Backpack[i].MaxClip = ToInt32(sql_Reader["MAXCLIP"].ToString());
                        Backpack[i].ItemAmmoType = ToInt32(sql_Reader["AMMOTYPE"].ToString());
                    }
                }
            }

            s_Database.Close();
        }
    }

    public enum EquipSlots
    {
        MainWeapon,
        OffWeapon
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
