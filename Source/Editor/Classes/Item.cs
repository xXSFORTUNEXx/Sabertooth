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
    class Item
    {
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
        public int ProjectileNumber { get; set; }

        const int Max_ItemPics = 8;
        Texture[] ItemPic = new Texture[Max_ItemPics];
        Sprite ItemSprite = new Sprite();

        public Item()
        {
            for (int p = 0; p < Max_ItemPics; p++)
            {
                ItemPic[p] = new Texture("Resources/Items/" + (p + 1) + ".png");
            }
        }        

        public void DrawItem(RenderWindow e_Window, int itemPic, int x, int y)
        {
            ItemSprite.Texture = ItemPic[itemPic];
            ItemSprite.TextureRect = new IntRect(0, 0, 32, 32);
            ItemSprite.Position = new Vector2f(x * 32, y * 32);

            e_Window.Draw(ItemSprite);
        }

        public void CreateItemInDatabase()
        {
            Name = "Default";
            Sprite = 1;
            Damage = 0;
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
            ProjectileNumber = 1;

            using (SQLiteConnection conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
            {
                conn.Open();
                string sql;
                sql = "INSERT INTO `ITEMS`";
                sql = sql + "(`NAME`,`SPRITE`,`DAMAGE`,`ARMOR`,`TYPE`,`ATTACKSPEED`,`RELOADSPEED`,`HEALTHRESTORE`,`HUNGERRESTORE`,`HYDRATERESTORE`,";
                sql = sql + "`STRENGTH`,`AGILITY`,`ENDURANCE`,`STAMINA`,`CLIP`,`MAXCLIP`,`AMMOTYPE`,`VALUE`,`PROJ`)";
                sql = sql + " VALUES ";
                sql = sql + "('" + Name + "','" + Sprite + "','" + Damage + "','" + Armor + "','" + Type + "','" + AttackSpeed + "','" + ReloadSpeed + "','" + HealthRestore + "','" + HungerRestore + "',";
                sql = sql + "'" + HydrateRestore + "','" + Strength + "','" + Agility + "','" + Endurance + "','" + Stamina + "','" + Clip + "','" + MaxClip + "','" + ItemAmmoType + "','" + Value + "');";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void SaveItemToDatabase(int itemNum)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
            {
                conn.Open();
                string sql;
                sql = "UPDATE ITEMS SET ";
                sql = sql + "NAME = '" + Name + "', SPRITE = '" + Sprite + "', DAMAGE = '" + Damage + "', ARMOR = '" + Armor + "', TYPE = '" + Type + "', ATTACKSPEED = '" + AttackSpeed + "', ";
                sql = sql + "RELOADSPEED = '" + ReloadSpeed + "', HEALTHRESTORE = '" + HealthRestore + "', HUNGERRESTORE = '" + HungerRestore + "', HYDRATERESTORE = '" + HydrateRestore + "', ";
                sql = sql + "STRENGTH = '" + Strength + "', AGILITY = '" + Agility + "', ENDURANCE = '" + Endurance + "', STAMINA = '" + Stamina + "', CLIP = '" + Clip + "', MAXCLIP = '" + MaxClip + "', AMMOTYPE = '" + ItemAmmoType + "', ";
                sql = sql + "VALUE = '" + Value + "', PROJ = '" + ProjectileNumber + "' ";
                sql = sql + "WHERE rowid = '" + itemNum + "';";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void LoadItemFromDatabase(int itemNum)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
            {
                conn.Open();
                string sql;

                sql = "SELECT * FROM ITEMS WHERE rowid = " + itemNum;

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            Name = read["NAME"].ToString();
                            Sprite = ToInt32(read["SPRITE"].ToString());
                            Damage = ToInt32(read["DAMAGE"].ToString());
                            Armor = ToInt32(read["ARMOR"].ToString());
                            Type = ToInt32(read["TYPE"].ToString());
                            AttackSpeed = ToInt32(read["ATTACKSPEED"].ToString());
                            ReloadSpeed = ToInt32(read["RELOADSPEED"].ToString());
                            HealthRestore = ToInt32(read["HEALTHRESTORE"].ToString());
                            HungerRestore = ToInt32(read["HUNGERRESTORE"].ToString());
                            HydrateRestore = ToInt32(read["HYDRATERESTORE"].ToString());
                            Strength = ToInt32(read["STRENGTH"].ToString());
                            Agility = ToInt32(read["AGILITY"].ToString());
                            Endurance = ToInt32(read["ENDURANCE"].ToString());
                            Stamina = ToInt32(read["STAMINA"].ToString());
                            Clip = ToInt32(read["CLIP"].ToString());
                            MaxClip = ToInt32(read["MAXCLIP"].ToString());
                            ItemAmmoType = ToInt32(read["AMMOTYPE"].ToString());
                            Value = ToInt32(read["VALUE"].ToString());
                            ProjectileNumber = ToInt32(read["PROJ"].ToString());
                        }
                    }
                }
            }
        }

        public void LoadNameFromDatabase(int itemNum)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
            {
                conn.Open();
                string sql;

                sql = "SELECT * FROM ITEMS WHERE rowid = " + itemNum;

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            Name = read["NAME"].ToString();
                        }
                    }
                }
            }
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
