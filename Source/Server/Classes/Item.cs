using System.Data.SQLite;
using static System.Convert;
using static SabertoothServer.Server;
using System.Data.SqlClient;

namespace SabertoothServer
{
    public class Item
    {
        public int Id { get; set; }
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
        public int Rarity { get; set; }
        public Item() { }

        public Item(ItemType type)
        {
            Type = (int)type;
        }

        public Item(string name, int sprite, int damage, int armor, int type, int attackspeed, int reloadspeed,
                    int healthRestore, int foodRestore, int drinkRestore, int str, int agi, int end, int sta, int clip, int maxclip, int ammotype, 
                    int value, int projNum, int price, int rarity)
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
            Rarity = rarity;
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
            Price = 1;
            Rarity = 0;

            if (DBType == Globals.SQL_DATABASE_REMOTE.ToString())
            {
                string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
                using (var sql = new SqlConnection(connection))
                {
                    sql.Open();
                    string command = "INSERT INTO ITEMS";
                    command += "(NAME,SPRITE,DAMAGE,ARMOR,TYPE,ATTACKSPEED,RELOADSPEED,HEALTHRESTORE,HUNGERRESTORE,HYDRATERESTORE,STRENGTH,AGILITY,ENDURANCE,STAMINA,CLIP,MAXCLIP,AMMOTYPE,VALUE,PROJ,PRICE,RARITY) VALUES ";
                    command += "(@name,@sprite,@damage,@armor,@type,@attackspeed,@reloadspeed,@healthrestore,@hungerrestore,@hydraterestore,@strength,@agility,@endurance,@stamina,@clip,@maxclip,@ammotype,@value,@proj,@price,@rarity)";
                    using (var cmd = new SqlCommand(command, sql))
                    {
                        cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = Name;
                        cmd.Parameters.Add(new SqlParameter("@sprite", System.Data.DbType.Int32)).Value = Sprite;
                        cmd.Parameters.Add(new SqlParameter("@damage", System.Data.DbType.Int32)).Value = Damage;
                        cmd.Parameters.Add(new SqlParameter("@armor", System.Data.DbType.Int32)).Value = Armor;
                        cmd.Parameters.Add(new SqlParameter("@type", System.Data.DbType.Int32)).Value = Type;
                        cmd.Parameters.Add(new SqlParameter("@attackspeed", System.Data.DbType.Int32)).Value = AttackSpeed;
                        cmd.Parameters.Add(new SqlParameter("@reloadspeed", System.Data.DbType.Int32)).Value = ReloadSpeed;
                        cmd.Parameters.Add(new SqlParameter("@healthrestore", System.Data.DbType.Int32)).Value = HealthRestore;
                        cmd.Parameters.Add(new SqlParameter("@hungerrestore", System.Data.DbType.Int32)).Value = HungerRestore;
                        cmd.Parameters.Add(new SqlParameter("@hydraterestore", System.Data.DbType.Int32)).Value = HydrateRestore;
                        cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = Strength;
                        cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = Agility;
                        cmd.Parameters.Add(new SqlParameter("@endurance", System.Data.DbType.Int32)).Value = Endurance;
                        cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = Stamina;
                        cmd.Parameters.Add(new SqlParameter("@clip", System.Data.DbType.Int32)).Value = Clip;
                        cmd.Parameters.Add(new SqlParameter("@maxclip", System.Data.DbType.Int32)).Value = MaxClip;
                        cmd.Parameters.Add(new SqlParameter("@ammotype", System.Data.DbType.Int32)).Value = ItemAmmoType;
                        cmd.Parameters.Add(new SqlParameter("@value", System.Data.DbType.Int32)).Value = Value;
                        cmd.Parameters.Add(new SqlParameter("@proj", System.Data.DbType.Int32)).Value = ProjectileNumber;
                        cmd.Parameters.Add(new SqlParameter("@price", System.Data.DbType.Int32)).Value = Price;
                        cmd.Parameters.Add(new SqlParameter("@rarity", System.Data.DbType.Int32)).Value = Rarity;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            else
            {
                using (var conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
                {
                    using (var cmd = new SQLiteCommand(conn))
                    {
                        conn.Open();
                        string command;
                        command = "INSERT INTO ITEMS";
                        command = command + "(NAME,SPRITE,DAMAGE,ARMOR,TYPE,ATTACKSPEED,RELOADSPEED,HEALTHRESTORE,HUNGERRESTORE,HYDRATERESTORE,STRENGTH,AGILITY,ENDURANCE,STAMINA,CLIP,MAXCLIP,AMMOTYPE,VALUE,PROJ,PRICE,RARITY)";
                        command = command + " VALUES ";
                        command = command + "(@name,@sprite,@damage,@armor,@type,@attackspeed,@reloadspeed,@healthrestore,@hungerrestore,@hydraterestore,@strength,@agility,@endurance,@stamina,@clip,@maxclip,@ammotype,@value,@proj,@price,@rarity);";
                        cmd.CommandText = command;
                        cmd.Parameters.Add("@name", System.Data.DbType.String).Value = Name;
                        cmd.Parameters.Add("@sprite", System.Data.DbType.Int32).Value = Sprite;
                        cmd.Parameters.Add("@damage", System.Data.DbType.Int32).Value = Damage;
                        cmd.Parameters.Add("@armor", System.Data.DbType.Int32).Value = Armor;
                        cmd.Parameters.Add("@type", System.Data.DbType.Int32).Value = Type;
                        cmd.Parameters.Add("@attackspeed", System.Data.DbType.Int32).Value = AttackSpeed;
                        cmd.Parameters.Add("@reloadspeed", System.Data.DbType.Int32).Value = ReloadSpeed;
                        cmd.Parameters.Add("@healthrestore", System.Data.DbType.Int32).Value = HealthRestore;
                        cmd.Parameters.Add("@hungerrestore", System.Data.DbType.Int32).Value = HungerRestore;
                        cmd.Parameters.Add("@hydraterestore", System.Data.DbType.Int32).Value = HydrateRestore;
                        cmd.Parameters.Add("@strength", System.Data.DbType.Int32).Value = Strength;
                        cmd.Parameters.Add("@agility", System.Data.DbType.Int32).Value = Agility;
                        cmd.Parameters.Add("@endurance", System.Data.DbType.Int32).Value = Endurance;
                        cmd.Parameters.Add("@stamina", System.Data.DbType.Int32).Value = Stamina;
                        cmd.Parameters.Add("@clip", System.Data.DbType.Int32).Value = Clip;
                        cmd.Parameters.Add("@maxclip", System.Data.DbType.Int32).Value = MaxClip;
                        cmd.Parameters.Add("@ammotype", System.Data.DbType.Int32).Value = ItemAmmoType;
                        cmd.Parameters.Add("@value", System.Data.DbType.Int32).Value = Value;
                        cmd.Parameters.Add("@proj", System.Data.DbType.Int32).Value = ProjectileNumber;
                        cmd.Parameters.Add("@price", System.Data.DbType.Int32).Value = Price;
                        cmd.Parameters.Add("@rarity", System.Data.DbType.Int32).Value = Rarity;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void SaveItemToDatabase(int itemNum)
        {
            if (DBType == Globals.SQL_DATABASE_REMOTE.ToString())
            {
                string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
                using (var sql = new SqlConnection(connection))
                {
                    sql.Open();
                    string command;
                    command = "UPDATE ITEMS SET ";
                    command += "NAME = @name, SPRITE = @sprite, DAMAGE = @damage, ARMOR = @armor, TYPE = @type, ATTACKSPEED = @attackspeed, RELOADSPEED = @reloadspeed, HEALTHRESTORE = @healthrestore, HUNGERRESTORE = @hungerrestore, HYDRATERESTORE = @hydraterestore, ";
                    command += "STRENGTH = @strength, AGILITY = @agility, ENDURANCE = @endurance, STAMINA = @stamina, CLIP = @clip, MAXCLIP = @maxclip, AMMOTYPE = @ammotype, VALUE = @value, PROJ = @proj, PRICE = @price, RARITY = @rarity ";
                    command += "WHERE ID = @id;";
                    using (var cmd = new SqlCommand(command, sql))
                    {
                        cmd.Parameters.Add(new SqlParameter("id", System.Data.SqlDbType.Int)).Value = itemNum;
                        cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = Name;
                        cmd.Parameters.Add(new SqlParameter("@sprite", System.Data.DbType.Int32)).Value = Sprite;
                        cmd.Parameters.Add(new SqlParameter("@damage", System.Data.DbType.Int32)).Value = Damage;
                        cmd.Parameters.Add(new SqlParameter("@armor", System.Data.DbType.Int32)).Value = Armor;
                        cmd.Parameters.Add(new SqlParameter("@type", System.Data.DbType.Int32)).Value = Type;
                        cmd.Parameters.Add(new SqlParameter("@attackspeed", System.Data.DbType.Int32)).Value = AttackSpeed;
                        cmd.Parameters.Add(new SqlParameter("@reloadspeed", System.Data.DbType.Int32)).Value = ReloadSpeed;
                        cmd.Parameters.Add(new SqlParameter("@healthrestore", System.Data.DbType.Int32)).Value = HealthRestore;
                        cmd.Parameters.Add(new SqlParameter("@hungerrestore", System.Data.DbType.Int32)).Value = HungerRestore;
                        cmd.Parameters.Add(new SqlParameter("@hydraterestore", System.Data.DbType.Int32)).Value = HydrateRestore;
                        cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = Strength;
                        cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = Agility;
                        cmd.Parameters.Add(new SqlParameter("@endurance", System.Data.DbType.Int32)).Value = Endurance;
                        cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = Stamina;
                        cmd.Parameters.Add(new SqlParameter("@clip", System.Data.DbType.Int32)).Value = Clip;
                        cmd.Parameters.Add(new SqlParameter("@maxclip", System.Data.DbType.Int32)).Value = MaxClip;
                        cmd.Parameters.Add(new SqlParameter("@ammotype", System.Data.DbType.Int32)).Value = ItemAmmoType;
                        cmd.Parameters.Add(new SqlParameter("@value", System.Data.DbType.Int32)).Value = Value;
                        cmd.Parameters.Add(new SqlParameter("@proj", System.Data.DbType.Int32)).Value = ProjectileNumber;
                        cmd.Parameters.Add(new SqlParameter("@price", System.Data.DbType.Int32)).Value = Price;
                        cmd.Parameters.Add(new SqlParameter("@rarity", System.Data.DbType.Int32)).Value = Rarity;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            else
            {
                using (var conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
                {
                    using (var cmd = new SQLiteCommand(conn))
                    {
                        conn.Open();
                        string command;
                        command = "UPDATE ITEMS SET ";
                        command = command + "NAME = @name, SPRITE = @sprite, DAMAGE = @damage, ARMOR = @armor, TYPE = @type, ATTACKSPEED = @attackspeed, RELOADSPEED = @reloadspeed, HEALTHRESTORE = @healthrestore, HUNGERRESTORE = @hungerrestore, HYDRATERESTORE = @hydraterestore, ";
                        command = command + "STRENGTH = @strength, AGILITY = @agility, ENDURANCE = @endurance, STAMINA = @stamina, CLIP = @clip, MAXCLIP = @maxclip, AMMOTYPE = @ammotype, VALUE = @value, PROJ = @proj, PRICE = @price, RARITY = @rarity ";
                        command = command + "WHERE rowid = " + itemNum + ";";
                        cmd.CommandText = command;
                        cmd.Parameters.Add("@name", System.Data.DbType.String).Value = Name;
                        cmd.Parameters.Add("@sprite", System.Data.DbType.Int32).Value = Sprite;
                        cmd.Parameters.Add("@damage", System.Data.DbType.Int32).Value = Damage;
                        cmd.Parameters.Add("@armor", System.Data.DbType.Int32).Value = Armor;
                        cmd.Parameters.Add("@type", System.Data.DbType.Int32).Value = Type;
                        cmd.Parameters.Add("@attackspeed", System.Data.DbType.Int32).Value = AttackSpeed;
                        cmd.Parameters.Add("@reloadspeed", System.Data.DbType.Int32).Value = ReloadSpeed;
                        cmd.Parameters.Add("@healthrestore", System.Data.DbType.Int32).Value = HealthRestore;
                        cmd.Parameters.Add("@hungerrestore", System.Data.DbType.Int32).Value = HungerRestore;
                        cmd.Parameters.Add("@hydraterestore", System.Data.DbType.Int32).Value = HydrateRestore;
                        cmd.Parameters.Add("@strength", System.Data.DbType.Int32).Value = Strength;
                        cmd.Parameters.Add("@agility", System.Data.DbType.Int32).Value = Agility;
                        cmd.Parameters.Add("@endurance", System.Data.DbType.Int32).Value = Endurance;
                        cmd.Parameters.Add("@stamina", System.Data.DbType.Int32).Value = Stamina;
                        cmd.Parameters.Add("@clip", System.Data.DbType.Int32).Value = Clip;
                        cmd.Parameters.Add("@maxclip", System.Data.DbType.Int32).Value = MaxClip;
                        cmd.Parameters.Add("@ammotype", System.Data.DbType.Int32).Value = ItemAmmoType;
                        cmd.Parameters.Add("@value", System.Data.DbType.Int32).Value = Value;
                        cmd.Parameters.Add("@proj", System.Data.DbType.Int32).Value = ProjectileNumber;
                        cmd.Parameters.Add("@price", System.Data.DbType.Int32).Value = Price;
                        cmd.Parameters.Add("@rarity", System.Data.DbType.Int32).Value = Rarity;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void LoadItemFromDatabase(int itemNum)
        {
            if (DBType == Globals.SQL_DATABASE_REMOTE.ToString())
            {
                string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
                using (var sql = new SqlConnection(connection))
                {
                    sql.Open();
                    string command;
                    command = "SELECT * FROM ITEMS WHERE ID=@id";
                    using (SqlCommand cmd = new SqlCommand(command, sql))
                    {
                        cmd.Parameters.Add(new SqlParameter("id", System.Data.SqlDbType.Int)).Value = itemNum;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Name = reader[1].ToString();
                                Sprite = ToInt32(reader[2]);
                                Damage = ToInt32(reader[3]);
                                Armor = ToInt32(reader[4]);
                                Type = ToInt32(reader[5]);
                                AttackSpeed = ToInt32(reader[6]);
                                ReloadSpeed = ToInt32(reader[7]);
                                HealthRestore = ToInt32(reader[8]);
                                HungerRestore = ToInt32(reader[9]);
                                HydrateRestore = ToInt32(reader[10]);
                                Strength = ToInt32(reader[11]);
                                Agility = ToInt32(reader[12]);
                                Endurance = ToInt32(reader[13]);
                                Stamina = ToInt32(reader[14]);
                                Clip = ToInt32(reader[15]);
                                MaxClip = ToInt32(reader[16]);
                                ItemAmmoType = ToInt32(reader[17]);
                                Value = ToInt32(reader[18]);
                                ProjectileNumber = ToInt32(reader[19]);
                                Price = ToInt32(reader[20]);
                                Rarity = ToInt32(reader[21]);
                            }
                        }
                    }
                }
            }
            else
            {
                using (var conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
                {
                    using (var cmd = new SQLiteCommand(conn))
                    {
                        conn.Open();
                        cmd.CommandText = "SELECT * FROM ITEMS WHERE rowid = " + itemNum;

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
                                Rarity = ToInt32(read["RARITY"].ToString());
                            }
                        }
                    }
                }
            }
        }

        public void LoadNameFromDatabase(int itemNum)
        {
            if (DBType == Globals.SQL_DATABASE_REMOTE.ToString())
            {
                string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
                using (var sql = new SqlConnection(connection))
                {
                    sql.Open();
                    string command;
                    command = "SELECT NAME FROM ITEMS WHERE ID=@id";
                    using (SqlCommand cmd = new SqlCommand(command, sql))
                    {
                        cmd.Parameters.Add(new SqlParameter("id", System.Data.SqlDbType.Int)).Value = itemNum;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Name = reader[0].ToString();
                            }
                        }
                    }
                }
            }
            else
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

    public enum Rarity
    {
        Normal,
        Uncommon,
        Rare,
        UltraRare,
        Legendary,
        Admin
    }
}
