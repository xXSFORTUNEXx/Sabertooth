using System.Data.SQLite;
using static System.Convert;

namespace Server.Classes
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
        public int Price { get; set; }

        public Item() { }

        public Item(ItemType type)
        {
            Type = (int)type;
        }

        public Item(string name, int sprite, int damage, int armor, int type, int attackspeed, int reloadspeed,
                    int healthRestore, int foodRestore, int drinkRestore, int str, int agi, int end, int sta, int clip, int maxclip, int ammotype, 
                    int value, int projNum, int price)
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
            Value = value;
            ProjectileNumber = projNum;
            Price = price;
        }

        public void CreateItemInDatabase()
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
            {
                conn.Open();
                string sql;
                sql = "INSERT INTO `ITEMS`";
                sql = sql + "(`NAME`,`SPRITE`,`DAMAGE`,`ARMOR`,`TYPE`,`ATTACKSPEED`,`RELOADSPEED`,`HEALTHRESTORE`,`HUNGERRESTORE`,`HYDRATERESTORE`,";
                sql = sql + "`STRENGTH`,`AGILITY`,`ENDURANCE`,`STAMINA`,`CLIP`,`MAXCLIP`,`AMMOTYPE`,`VALUE`,`PROJ`)";
                sql = sql + " VALUES ";
                sql = sql + "('" + Name + "','" + Sprite + "','" + Damage + "','" + Armor + "','" + Type + "','" + AttackSpeed + "','" + ReloadSpeed + "','" + HealthRestore + "','" + HungerRestore + "',";
                sql = sql + "'" + HydrateRestore + "','" + Strength + "','" + Agility + "','" + Endurance + "','" + Stamina + "','" + Clip + "','" + MaxClip + "','" + ItemAmmoType + "','" + Value + "',' " + Price + "');";

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
                sql = sql + "VALUE = '" + Value + "', PROJ = '" + ProjectileNumber + "', PRICE = '" + Price + "' ";
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
                            Price = ToInt32(read["PRICE"].ToString());
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
