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
        public int HealthRestore { get; set; }
        public int ManaRestore { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }        
        public int Stamina { get; set; }
        public int Energy { get; set; }
        public int Value { get; set; }        
        public int Price { get; set; }
        public int Rarity { get; set; }
        public int CoolDown { get; set; }
        public int AddMaxHealth { get; set; }
        public int AddMaxMana { get; set; }
        public int BonusXP { get; set; }
        public int SpellNum { get; set; }
        public bool Stackable { get; set; }
        public int MaxStack { get; set; }

        public Item() { }

        public Item(ItemType type)
        {
            Type = (int)type;
        }

        public Item(string name, int sprite, int damage, int armor, int type, int attackspeed, int hpRestore, int mprestore, int str, int agi, int intel, int ene, int sta, int value, int price, int rarity, 
            int cooldown, int addmaxhp, int addmaxmp, int bonusxp, int spellnum, bool stacked, int maxStack)
        {
            Name = name;
            Sprite = sprite;
            Damage = damage;
            Armor = armor;
            Type = type;
            AttackSpeed = attackspeed;
            HealthRestore = hpRestore;
            ManaRestore = mprestore;
            Strength = str;
            Agility = agi;
            Intelligence = intel;
            Energy = ene;
            Stamina = sta;
            Value = value;            
            Price = price;
            Rarity = rarity;
            CoolDown = cooldown;
            AddMaxHealth = addmaxhp;
            AddMaxMana = addmaxmp;
            BonusXP = bonusxp;
            SpellNum = spellnum;
            Stackable = stacked;
            MaxStack = maxStack;
        }

        public void CreateItemInDatabase()
        {
            Name = "Default";
            Sprite = 1;
            Damage = 0;
            Armor = 0;
            Type = (int)ItemType.None;
            AttackSpeed = 0;            
            HealthRestore = 0;
            ManaRestore = 0;
            Strength = 0;
            Agility = 0;
            Intelligence = 0;
            Energy = 0;
            Stamina = 0;
            Value = 1;
            Price = 1;
            Rarity = 0;
            CoolDown = 0;
            AddMaxHealth = 0;
            AddMaxMana = 0;
            BonusXP = 0;
            SpellNum = 0;
            Stackable = false;
            MaxStack = MAX_STACK_SIZE;

            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            string script = ReadAllText("SQL Data Scripts/Insert_Item.sql");
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
                    cmd.Parameters.Add(new SqlParameter("@healthrestore", System.Data.DbType.Int32)).Value = HealthRestore;
                    cmd.Parameters.Add(new SqlParameter("@manarestore", System.Data.DbType.Int32)).Value = ManaRestore;
                    cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = Strength;
                    cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = Agility;
                    cmd.Parameters.Add(new SqlParameter("@intelligence", System.Data.DbType.Int32)).Value = Intelligence;
                    cmd.Parameters.Add(new SqlParameter("@energy", System.Data.DbType.Int32)).Value = Energy;
                    cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = Stamina;
                    cmd.Parameters.Add(new SqlParameter("@value", System.Data.DbType.Int32)).Value = Value;
                    cmd.Parameters.Add(new SqlParameter("@price", System.Data.DbType.Int32)).Value = Price;
                    cmd.Parameters.Add(new SqlParameter("@rarity", System.Data.DbType.Int32)).Value = Rarity;
                    cmd.Parameters.Add(new SqlParameter("@cooldown", System.Data.DbType.Int32)).Value = CoolDown;
                    cmd.Parameters.Add(new SqlParameter("@addmaxhp", System.Data.DbType.Int32)).Value = AddMaxHealth;
                    cmd.Parameters.Add(new SqlParameter("@addmaxmp", System.Data.DbType.Int32)).Value = AddMaxMana;
                    cmd.Parameters.Add(new SqlParameter("@bonusxp", System.Data.DbType.Int32)).Value = BonusXP;
                    cmd.Parameters.Add(new SqlParameter("@spellnum", System.Data.DbType.Int32)).Value = SpellNum;
                    cmd.Parameters.Add(new SqlParameter("@stack", System.Data.DbType.Boolean)).Value = Stackable;
                    cmd.Parameters.Add(new SqlParameter("@maxstack", System.Data.DbType.Int32)).Value = MaxStack;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void SaveItemToDatabase(int itemNum)
        {
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            string script = ReadAllText("SQL Data Scripts/Save_Item.sql");
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
                    cmd.Parameters.Add(new SqlParameter("@healthrestore", System.Data.DbType.Int32)).Value = HealthRestore;
                    cmd.Parameters.Add(new SqlParameter("@manarestore", System.Data.DbType.Int32)).Value = ManaRestore;
                    cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = Strength;
                    cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = Agility;
                    cmd.Parameters.Add(new SqlParameter("@intelligence", System.Data.DbType.Int32)).Value = Intelligence;
                    cmd.Parameters.Add(new SqlParameter("@energy", System.Data.DbType.Int32)).Value = Energy;
                    cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = Stamina;
                    cmd.Parameters.Add(new SqlParameter("@value", System.Data.DbType.Int32)).Value = Value;
                    cmd.Parameters.Add(new SqlParameter("@price", System.Data.DbType.Int32)).Value = Price;
                    cmd.Parameters.Add(new SqlParameter("@rarity", System.Data.DbType.Int32)).Value = Rarity;
                    cmd.Parameters.Add(new SqlParameter("@cooldown", System.Data.DbType.Int32)).Value = CoolDown;
                    cmd.Parameters.Add(new SqlParameter("@addmaxhp", System.Data.DbType.Int32)).Value = AddMaxHealth;
                    cmd.Parameters.Add(new SqlParameter("@addmaxmp", System.Data.DbType.Int32)).Value = AddMaxMana;
                    cmd.Parameters.Add(new SqlParameter("@bonusxp", System.Data.DbType.Int32)).Value = BonusXP;
                    cmd.Parameters.Add(new SqlParameter("@spellnum", System.Data.DbType.Int32)).Value = SpellNum;
                    cmd.Parameters.Add(new SqlParameter("@stack", System.Data.DbType.Boolean)).Value = Stackable;
                    cmd.Parameters.Add(new SqlParameter("@maxstack", System.Data.DbType.Int32)).Value = MaxStack;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void LoadItemFromDatabase(int itemNum)
        {
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            string script = ReadAllText("SQL Data Scripts/Load_Item.sql");
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                int i;
                using (SqlCommand cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = itemNum;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            i = 1;
                            Name = reader[i].ToString(); i += 1;
                            Sprite = ToInt32(reader[i]); i += 1;
                            Damage = ToInt32(reader[i]); i += 1;
                            Armor = ToInt32(reader[i]); i += 1;
                            Type = ToInt32(reader[i]); i += 1;
                            AttackSpeed = ToInt32(reader[i]); i += 1;
                            HealthRestore = ToInt32(reader[i]); i += 1;
                            ManaRestore = ToInt32(reader[i]); i += 1;
                            Strength = ToInt32(reader[i]); i += 1;
                            Agility = ToInt32(reader[i]); i += 1;
                            Intelligence = ToInt32(reader[i]); i += 1;
                            Energy = ToInt32(reader[i]); i += 1;
                            Stamina = ToInt32(reader[i]); i += 1;
                            Value = ToInt32(reader[i]); i += 1;
                            Price = ToInt32(reader[i]); i += 1;
                            Rarity = ToInt32(reader[i]); i += 1;
                            CoolDown = ToInt32(reader[i]); i += 1;
                            AddMaxHealth = ToInt32(reader[i]); i += 1;
                            AddMaxMana = ToInt32(reader[i]); i += 1;
                            BonusXP = ToInt32(reader[i]); i += 1;
                            SpellNum = ToInt32(reader[i]); i += 1;
                            Stackable = ToBoolean(reader[i]); i += 1;
                            MaxStack = ToInt32(reader[i]);
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
                command = "SELECT Name FROM Items WHERE ID=@id";
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
        OffHand,
        MainHand,
        Currency,
        Food,
        Drink,
        Potion,
        Shirt,
        Pants,
        Shoes,
        Book,
        Other
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
