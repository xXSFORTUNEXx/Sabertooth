using Microsoft.VisualBasic;
using System;
using System.IO;
using static Microsoft.VisualBasic.Interaction;
using Lidgren.Network;
using System.Data.SQLite;
using static System.Convert;

namespace Server.Classes
{
    class Item
    {
        SQLiteConnection s_Database;
        public string Name { get; set; }
        public int Sprite { get; set; }
        public int Damage { get; set; }
        public int Armor { get; set; }
        public int Type { get; set; }
        public int AttackSpeed { get; set; }
        public int ReloadSpeed { get; set; }
        public int HealthRestore { get; set; }
        public int HungerRestore { get; set; }
        public int HydrateRestore { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Endurance { get; set; }
        public int Stamina { get; set; }
        public int Clip { get; set; }
        public int MaxClip { get; set; }
        public int ItemAmmoType { get; set; }
        public int Value { get; set; }

        public Item() { }

        public Item(ItemType type)
        {
            Type = (int)type;
        }

        public Item(string name, int sprite, int damage, int armor, int type, int attackspeed, int reloadspeed,
                    int healthRestore, int foodRestore, int drinkRestore, int str, int agi, int end, int sta, int clip, int maxclip, int ammotype)
        {
            Name = name;
            Sprite = sprite;
            Damage = damage;
            Armor = armor;
            Type = type;
            AttackSpeed = attackspeed;
            ReloadSpeed = reloadspeed;
            HealthRestore = healthRestore;
            HungerRestore = foodRestore;
            HydrateRestore = drinkRestore;
            Strength = str;
            Agility = agi;
            Endurance = end;
            Stamina = sta;
            Clip = clip;
            MaxClip = maxclip;
            ItemAmmoType = ammotype;
        }

        public void CreateItemInDatabase()
        {
            s_Database = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;");
            s_Database.Open();
            string sql;
            SQLiteCommand sql_Command;
            sql = "INSERT INTO `ITEMS`";
            sql = sql + "(`NAME`,`SPRITE`,`DAMAGE`,`ARMOR`,`TYPE`,`ATTACKSPEED`,`RELOADSPEED`,`HEALTHRESTORE`,`HUNGERRESTORE`,`HYDRATERESTORE`,";
            sql = sql + "`STRENGTH`,`AGILITY`,`ENDURANCE`,`STAMINA`,`CLIP`,`MAXCLIP`,`AMMOTYPE`,`VALUE`)";
            sql = sql + " VALUES ";
            sql = sql + "('" + Name + "','" + Sprite + "','" + Damage + "','" + Armor + "','" + Type + "','" + AttackSpeed + "','" + ReloadSpeed + "','" + HealthRestore + "','" + HungerRestore + "',";
            sql = sql + "'" + HydrateRestore + "','" + Strength + "','" + Agility + "','" + Endurance + "','" + Stamina + "','" + Clip + "','" + MaxClip + "','" + ItemAmmoType + "','" + Value + "');";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Command.ExecuteNonQuery();
            s_Database.Close();
        }

        public void SaveItemToDatabase(int itemNum)
        {
            s_Database = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;");
            s_Database.Open();
            string sql;
            SQLiteCommand sql_Command;
            sql = "UPDATE ITEMS SET ";
            sql = sql + "NAME = '" + Name + "', SPRITE = '" + Sprite + "', DAMAGE = '" + Damage + "', ARMOR = '" + Armor + "', TYPE = '" + Type + "', ATTACKSPEED = '" + AttackSpeed + "', ";
            sql = sql + "RELOADSPEED = '" + ReloadSpeed + "', HEALTHRESTORE = '" + HealthRestore + "', HUNGERRESTORE = '" + HungerRestore + "', HYDRATERESTORE = '" + HydrateRestore + "', ";
            sql = sql + "STRENGTH = '" + Strength + "', AGILITY = '" + Agility + "', ENDURANCE = '" + Endurance + "', STAMINA = '" + Stamina + "', CLIP = '" + Clip + "', MAXCLIP = '" + MaxClip + "', AMMOTYPE = '" + ItemAmmoType + "', ";
            sql = sql + "VALUE = '" + Value + "' ";
            sql = sql + "WHERE rowid = '" + itemNum + "';";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Command.ExecuteNonQuery();
            s_Database.Close();
        }

        public void LoadItemFromDatabase(int itemNum)
        {
            s_Database = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;");
            s_Database.Open();
            string sql;

            sql = "SELECT * FROM ITEMS WHERE rowid = " + itemNum;

            SQLiteCommand sql_Command = new SQLiteCommand(sql, s_Database);
            SQLiteDataReader sql_Reader = sql_Command.ExecuteReader();

            while (sql_Reader.Read())
            {
                Name = sql_Reader["NAME"].ToString();
                Sprite = ToInt32(sql_Reader["SPRITE"].ToString());
                Damage = ToInt32(sql_Reader["DAMAGE"].ToString());
                Armor = ToInt32(sql_Reader["ARMOR"].ToString());
                Type = ToInt32(sql_Reader["TYPE"].ToString());
                AttackSpeed = ToInt32(sql_Reader["ATTACKSPEED"].ToString());
                ReloadSpeed = ToInt32(sql_Reader["RELOADSPEED"].ToString());
                HealthRestore = ToInt32(sql_Reader["HEALTHRESTORE"].ToString());
                HungerRestore = ToInt32(sql_Reader["HUNGERRESTORE"].ToString());
                HydrateRestore = ToInt32(sql_Reader["HYDRATERESTORE"].ToString());
                Strength = ToInt32(sql_Reader["STRENGTH"].ToString());
                Agility = ToInt32(sql_Reader["AGILITY"].ToString());
                Endurance = ToInt32(sql_Reader["ENDURANCE"].ToString());
                Stamina = ToInt32(sql_Reader["STAMINA"].ToString());
                Clip = ToInt32(sql_Reader["CLIP"].ToString());
                MaxClip = ToInt32(sql_Reader["MAXCLIP"].ToString());
                ItemAmmoType = ToInt32(sql_Reader["AMMOTYPE"].ToString());
                Value = ToInt32(sql_Reader["VALUE"].ToString());
            }
            s_Database.Close();
        }
    }

    public enum ItemType
    {
        None,
        MeleeWeapon,
        RangedWeapon,
        Currency,
        Food,
        Drink,
        FirstAid,
        Shirt,
        Pants,
        Shoes,
        Other
    }

    public enum AmmoType
    {
        None,
        Pistol,
        AssaultRifle,
        Rocket,
        Grenade
    }
}
