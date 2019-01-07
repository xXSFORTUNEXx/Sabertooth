using static System.Convert;
using static SabertoothServer.Server;
using System.Data.SqlClient;
using static SabertoothServer.Globals;
using static System.IO.File;

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

            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            string script = ReadAllText("SQL Data Scripts/INSERT ITEM.sql");
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                using (var cmd = new SqlCommand(script, sql))
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

        public void SaveItemToDatabase(int itemNum)
        {
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            string script = ReadAllText("SQL Data Scripts/SAVE ITEM.sql");
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                using (var cmd = new SqlCommand(script, sql))
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

        public void LoadItemFromDatabase(int itemNum)
        {
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            string script = ReadAllText("SQL Data Scripts/LOAD ITEM.sql");
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                using (SqlCommand cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = itemNum;
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

        public void LoadNameFromDatabase(int itemNum)
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
