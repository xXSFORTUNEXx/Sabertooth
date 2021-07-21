using static System.Convert;
using static SabertoothServer.Server;
using System.Data.SqlClient;
using static SabertoothServer.Globals;
using static System.IO.File;
using System;

namespace SabertoothServer
{
    public class Spell
    {
        public string Name { get; set; }

        public int ID { get; set; }
        public int Level { get; set; }
        public int Icon { get; set; }
        public int Vital { get; set; } //was orig damage but vital makes more since cause it can be damage or health for hots/dots
        public int HealthCost { get; set; } //this is if a spell cost health instead of mana to cast (not sure about this yet)
        public int ManaCost { get; set; }   //probably the norm resource for casting spells/buffs
        public int CoolDown { get; set; }   //time it takes before the spell can be casted again
        public int CastTime { get; set; }   //time it takes to cast the spell
        public int Charges { get; set; }    //you can learn a spell with charges and restore the charges at a well or something
        public int TotalTick { get; set; } //Total amount of time for a hot/dot
        public int TickInterval { get; set; }   //how often the dot/hot should tick damage/heal
        public int SpellType { get; set; }
        public int Range { get; set; }
        public int Animation { get; set; }
        public bool AOE { get; set; }   //area of effect
        public int Distance { get; set; }

        public Spell() { }

        public Spell(string name,
            int icon, int level, int vital, int hpcost, int mpcost, int cooldown, int casttime, int charges, int totaltick, int tickint, int spelltype,
            int range, bool aoe, int distance)
        {
            Name = name;
            Level = level;
            Icon = icon;
            Vital = vital;
            HealthCost = hpcost;
            ManaCost = mpcost;
            CoolDown = cooldown;
            CastTime = casttime;
            Charges = charges;
            TotalTick = totaltick;
            TickInterval = tickint;
            SpellType = spelltype;
            Range = range;
            AOE = aoe;
            Distance = distance;
        }

        public void CreateSpellInDatabase()
        {
            Name = "Default";
            Level = 0;
            Icon = 1;
            Vital = 0;
            HealthCost = 0;
            ManaCost = 0;
            CoolDown = 100;
            CastTime = 100;
            Charges = 0;
            TotalTick = 0;
            TickInterval = 100;
            SpellType = 0;
            Range = 0;
            AOE = false;
            Distance = 0;

            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            string script = ReadAllText("SQL Data Scripts/Insert_Spell.sql");
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = Name;
                    cmd.Parameters.Add(new SqlParameter("@level", System.Data.DbType.Int32)).Value = Level;
                    cmd.Parameters.Add(new SqlParameter("@icon", System.Data.DbType.Int32)).Value = Icon;
                    cmd.Parameters.Add(new SqlParameter("@vital", System.Data.DbType.Int32)).Value = Vital;
                    cmd.Parameters.Add(new SqlParameter("@hpcost", System.Data.DbType.Int32)).Value = HealthCost;
                    cmd.Parameters.Add(new SqlParameter("@mpcost", System.Data.DbType.Int32)).Value = ManaCost;
                    cmd.Parameters.Add(new SqlParameter("@cooldown", System.Data.DbType.Int32)).Value = CoolDown;
                    cmd.Parameters.Add(new SqlParameter("@casttime", System.Data.DbType.Int32)).Value = CastTime;
                    cmd.Parameters.Add(new SqlParameter("@charges", System.Data.DbType.Int32)).Value = Charges;
                    cmd.Parameters.Add(new SqlParameter("@totaltick", System.Data.DbType.Int32)).Value = TotalTick;
                    cmd.Parameters.Add(new SqlParameter("@tickinterval", System.Data.DbType.Int32)).Value = TickInterval;
                    cmd.Parameters.Add(new SqlParameter("@spelltype", System.Data.DbType.Int32)).Value = SpellType;
                    cmd.Parameters.Add(new SqlParameter("@range", System.Data.DbType.Int32)).Value = Range;
                    cmd.Parameters.Add(new SqlParameter("@animation", System.Data.DbType.Int32)).Value = Animation;
                    cmd.Parameters.Add(new SqlParameter("@aoe", System.Data.DbType.Int32)).Value = AOE;
                    cmd.Parameters.Add(new SqlParameter("@distance", System.Data.DbType.Int32)).Value = Distance;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void SaveSpellInDatabase(int spellNum)
        {
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            string script = ReadAllText("SQL Data Scripts/Save_Spell.sql");
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@id", System.Data.DbType.Int32)).Value = spellNum;
                    cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = Name;
                    cmd.Parameters.Add(new SqlParameter("@level", System.Data.DbType.Int32)).Value = Level;
                    cmd.Parameters.Add(new SqlParameter("@icon", System.Data.DbType.Int32)).Value = Icon;
                    cmd.Parameters.Add(new SqlParameter("@vital", System.Data.DbType.Int32)).Value = Vital;
                    cmd.Parameters.Add(new SqlParameter("@hpcost", System.Data.DbType.Int32)).Value = HealthCost;
                    cmd.Parameters.Add(new SqlParameter("@mpcost", System.Data.DbType.Int32)).Value = ManaCost;
                    cmd.Parameters.Add(new SqlParameter("@cooldown", System.Data.DbType.Int32)).Value = CoolDown;
                    cmd.Parameters.Add(new SqlParameter("@casttime", System.Data.DbType.Int32)).Value = CastTime;
                    cmd.Parameters.Add(new SqlParameter("@charges", System.Data.DbType.Int32)).Value = Charges;
                    cmd.Parameters.Add(new SqlParameter("@totaltick", System.Data.DbType.Int32)).Value = TotalTick;
                    cmd.Parameters.Add(new SqlParameter("@tickinterval", System.Data.DbType.Int32)).Value = TickInterval;
                    cmd.Parameters.Add(new SqlParameter("@spelltype", System.Data.DbType.Int32)).Value = SpellType;
                    cmd.Parameters.Add(new SqlParameter("@range", System.Data.DbType.Int32)).Value = Range;
                    cmd.Parameters.Add(new SqlParameter("@animation", System.Data.DbType.Int32)).Value = Animation;
                    cmd.Parameters.Add(new SqlParameter("@aoe", System.Data.DbType.Int32)).Value = AOE;
                    cmd.Parameters.Add(new SqlParameter("@distance", System.Data.DbType.Int32)).Value = Distance;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void LoadSpellFromDatabase(int spellNum)
        {
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            string script = ReadAllText("SQL Data Scripts/Load_Spell.sql");
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                int i;
                using (SqlCommand cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = spellNum;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            i = 1;
                            Name = reader[i].ToString(); i += 1;
                            Level = ToInt32(reader[i]); i += 1;
                            Icon = ToInt32(reader[i]); i += 1;
                            Vital = ToInt32(reader[i]); i += 1;
                            HealthCost = ToInt32(reader[i]); i += 1;
                            ManaCost = ToInt32(reader[i]); i += 1;
                            CoolDown = ToInt32(reader[i]); i += 1;
                            CastTime = ToInt32(reader[i]); i += 1;
                            Charges = ToInt32(reader[i]); i += 1;
                            TotalTick = ToInt32(reader[i]); i += 1;
                            TickInterval = ToInt32(reader[i]); i += 1;
                            SpellType = ToInt32(reader[i]); i += 1;
                            Range = ToInt32(reader[i]); i += 1;
                            Animation = ToInt32(reader[i]); i += 1;
                            AOE = ToBoolean(reader[i]); i += 1;
                            Distance = ToInt32(reader[i]);
                        }
                    }
                }
            }
        }

        public void LoadNameFromDatabase(int spellNum)
        {
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                string command;
                command = "SELECT Name FROM Spells WHERE ID=@id";
                using (SqlCommand cmd = new SqlCommand(command, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("id", System.Data.SqlDbType.Int)).Value = spellNum;
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

    public enum SpellType : int
    {
        None,
        Damage,
        Heal,
        Buff,
        Debuff,
        Dash,
        Shield
    }
}
