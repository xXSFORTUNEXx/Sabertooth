using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using static System.Convert;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Editor.Classes
{
    class Shop
    {
        SQLiteConnection e_Database;
        public string Name { get; set; }
        public ShopItem[] shopItem = new ShopItem[25];

        public Shop() { }

        public Shop(string name)
        {
            Name = name;

            for (int i = 0; i < 25; i++)
            {
                shopItem[i] = new ShopItem("None", 0);
            }
        }

        public void CreateShopInDatabase()
        {
            Name = "Default";

            e_Database = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;");
            e_Database.Open();
            string sql;
            SQLiteCommand sql_Command;

            sql = "INSERT INTO `SHOPS`";
            sql = sql + "(`NAME`,";

            for (int i = 0; i < 25; i++)
            {
                sql = sql + "`SHOPITEMID" + i + "`,";
            }

            sql = sql + ") VALUES ";
            sql = sql + "('" + Name + "',";
            
            for (int i = 0; i < 25; i++)
            {
                sql = sql + "'" + shopItem[i].ItemNum + "',";
            }

            sql = sql + ");";
            sql_Command = new SQLiteCommand(sql, e_Database);
            sql_Command.ExecuteNonQuery();
            e_Database.Close();
        }

        public void SaveShopInDatabase(int shopNum)
        {
            e_Database = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;");
            e_Database.Open();
            string sql;
            SQLiteCommand sql_Command;
            sql = "UPDATE SHOPS SET ";
            sql = sql + "NAME = '" + Name + "', ";
            for (int i = 0; i < 25; i++)
            {
                sql = sql + " SHOPITEMID = '" + shopItem[i].ItemNum + "', ";
            }
            sql = sql + "WHERE rowid = '" + shopNum + "';";
            sql_Command = new SQLiteCommand(sql, e_Database);
            sql_Command.ExecuteNonQuery();
            e_Database.Close();
        }

        public void LoadShopFromDatabase(int shopNum)
        {
            e_Database = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;");
            e_Database.Open();
            string sql;

            sql = "SELECT * FROM SHOPS WHERE rowid = " + shopNum;

            SQLiteCommand sql_Command = new SQLiteCommand(sql, e_Database);
            SQLiteDataReader sql_Reader = sql_Command.ExecuteReader();

            while (sql_Reader.Read())
            {
                Name = sql_Reader["NAME"].ToString();
                for (int i = 0; i < 25; i++)
                {
                    shopItem[i].ItemNum = ToInt32(sql_Reader["SHOPITEMID" + i].ToString());
                }
            }
            e_Database.Close();
        }

        public void LoadShopNameFromDatabase(int shopNum)
        {
            e_Database = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;");
            e_Database.Open();
            string sql;

            sql = "SELECT * FROM SHOPS WHERE rowid = " + shopNum;

            SQLiteCommand sql_Command = new SQLiteCommand(sql, e_Database);
            SQLiteDataReader sql_Reader = sql_Command.ExecuteReader();

            while (sql_Reader.Read())
            {
                Name = sql_Reader["NAME"].ToString();
            }
            e_Database.Close();
        }
    }

    class ShopItem : Item
    {
        SQLiteConnection e_Database;
        public int ItemNum { get; set; }
        public int Price { get; set; }

        public ShopItem(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public void CreateShopItemInDatabase()
        {
            Name = "Default";
            Sprite = 0;
            Damage = 1;
            Armor = 0;
            Type = (int)ItemType.None;
            AttackSpeed = 0;
            ReloadSpeed = 0;
            HealthRestore = 0;
            HungerRestore = 0;
            HydrateRestore = 0;
            Strength = 0;
            Agility = 0;
            Endurance = 0;
            Stamina = 0;
            Clip = 0;
            MaxClip = 0;
            ItemAmmoType = 0;
            Value = 0;
            Price = 1;

            e_Database = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;");
            e_Database.Open();
            string sql;
            SQLiteCommand sql_Command;
            sql = "INSERT INTO `SHOPITEMS`";
            sql = sql + "(`NAME`,`SPRITE`,`DAMAGE`,`ARMOR`,`TYPE`,`ATTACKSPEED`,`RELOADSPEED`,`HEALTHRESTORE`,`HUNGERRESTORE`,`HYDRATERESTORE`,";
            sql = sql + "`STRENGTH`,`AGILITY`,`ENDURANCE`,`STAMINA`,`CLIP`,`MAXCLIP`,`AMMOTYPE`,`VALUE`,`PRICE`)";
            sql = sql + " VALUES ";
            sql = sql + "('" + Name + "','" + Sprite + "','" + Damage + "','" + Armor + "','" + Type + "','" + AttackSpeed + "','" + ReloadSpeed + "','" + HealthRestore + "','" + HungerRestore + "',";
            sql = sql + "'" + HydrateRestore + "','" + Strength + "','" + Agility + "','" + Endurance + "','" + Stamina + "','" + Clip + "','" + MaxClip + "','" + ItemAmmoType + "','" + Value + "','" + Price + "');";
            sql_Command = new SQLiteCommand(sql, e_Database);
            sql_Command.ExecuteNonQuery();
            e_Database.Close();
        }

        public void SaveShopItemInDatabase(int id)
        {
            e_Database = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;");
            e_Database.Open();
            string sql;
            SQLiteCommand sql_Command;
            sql = "UPDATE SHOPITEMS SET ";
            sql = sql + "NAME = '" + Name + "', SPRITE = '" + Sprite + "', DAMAGE = '" + Damage + "', ARMOR = '" + Armor + "', TYPE = '" + Type + "', ATTACKSPEED = '" + AttackSpeed + "', ";
            sql = sql + "RELOADSPEED = '" + ReloadSpeed + "', HEALTHRESTORE = '" + HealthRestore + "', HUNGERRESTORE = '" + HungerRestore + "', HYDRATERESTORE = '" + HydrateRestore + "', ";
            sql = sql + "STRENGTH = '" + Strength + "', AGILITY = '" + Agility + "', ENDURANCE = '" + Endurance + "', STAMINA = '" + Stamina + "', CLIP = '" + Clip + "', MAXCLIP = '" + MaxClip + "', AMMOTYPE = '" + ItemAmmoType + "', ";
            sql = sql + "VALUE = '" + Value + "', PRICE = '" + Price + "' ";
            sql = sql + "WHERE rowid = '" + id + "';";
            sql_Command = new SQLiteCommand(sql, e_Database);
            sql_Command.ExecuteNonQuery();
            e_Database.Close();
        }

        public void LoadShopItemFromDatabase(int id)
        {
            e_Database = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;");
            e_Database.Open();
            string sql;

            sql = "SELECT * FROM SHOPITEMS WHERE rowid = " + id;

            SQLiteCommand sql_Command = new SQLiteCommand(sql, e_Database);
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
                Price = ToInt32(sql_Reader["PRICE"].ToString());
            }
            e_Database.Close();
        }

        public void LoadShopItemNameFromDatabase(int id)
        {
            e_Database = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;");
            e_Database.Open();
            string sql;

            sql = "SELECT * FROM SHOPITEMS WHERE rowid = " + id;

            SQLiteCommand sql_Command = new SQLiteCommand(sql, e_Database);
            SQLiteDataReader sql_Reader = sql_Command.ExecuteReader();

            while (sql_Reader.Read())
            {
                Name = sql_Reader["NAME"].ToString();
            }
            e_Database.Close();
        }
    }
}
