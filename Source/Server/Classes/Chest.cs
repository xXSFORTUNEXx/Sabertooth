using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Lidgren.Network;
using static System.Convert;
using static SabertoothServer.Server;
using System.Data.SqlClient;
using static SabertoothServer.Globals;
using static System.IO.File;

namespace SabertoothServer
{
    public class Chest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Money { get; set; }
        public int Experience { get; set; }
        public int RequiredLevel { get; set; }
        public int TrapLevel { get; set; }
        public int Key { get; set; }
        public int Damage { get; set; }
        public int NpcSpawn { get; set; }
        public int SpawnAmount { get; set; }
        public ChestItem[] ChestItem = new ChestItem[10];        
                
        public Chest()
        {
            for (int i = 0; i < 10; i++)
            {
                ChestItem[i] = new ChestItem("None", 0, 1);
            }
        }

        private byte[] ToByteArray(object source)
        {
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, source);
                return stream.ToArray();
            }
        }

        private static Object ByteArrayToObject(byte[] arrBytes)
        {
            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = binForm.Deserialize(memStream);
                return obj;
            }
        }

        public Chest(string name, int money, int exp, int reqlvl, int traplvl, int key, int damage, int npcspawn, int npcamount)
        {
            Name = name;
            Money = money;
            Experience = exp;
            RequiredLevel = reqlvl;
            TrapLevel = traplvl;
            Key = key;
            Damage = damage;
            NpcSpawn = npcspawn;
            SpawnAmount = npcamount;

            for (int i = 0; i < 10; i++)
            {
                ChestItem[i] = new ChestItem("None", 0, 1);
            }
        }

        public void CreateChestInDatabase()
        {
            Name = "Default";
            Money = 0;
            Experience = 0;
            RequiredLevel = 0;
            TrapLevel = 0;
            Key = 0;
            Damage = 0;
            NpcSpawn = 0;
            SpawnAmount = 0;

            for (int i = 0; i < 10; i++)
            {
                ChestItem[i] = new ChestItem("None", 0, 1);
            }

            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            string script = ReadAllText("SQL Data Scripts/Insert_Chest.sql");
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();

                using (var cmd = new SqlCommand(script, sql))
                {
                    byte[] chestData = ToByteArray(ChestItem);

                    cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = Name;
                    cmd.Parameters.Add(new SqlParameter("@money", System.Data.DbType.Int32)).Value = Money;
                    cmd.Parameters.Add(new SqlParameter("@experience", System.Data.DbType.Int32)).Value = Experience;
                    cmd.Parameters.Add(new SqlParameter("@requiredlevel", System.Data.DbType.Int32)).Value = RequiredLevel;
                    cmd.Parameters.Add(new SqlParameter("@traplevel", System.Data.DbType.Int32)).Value = TrapLevel;
                    cmd.Parameters.Add(new SqlParameter("@key", System.Data.DbType.Int32)).Value = Key;
                    cmd.Parameters.Add(new SqlParameter("@damage", System.Data.DbType.Int32)).Value = Damage;
                    cmd.Parameters.Add(new SqlParameter("@npcspawn", System.Data.DbType.Int32)).Value = NpcSpawn;
                    cmd.Parameters.Add(new SqlParameter("@spawnamount", System.Data.DbType.Int32)).Value = SpawnAmount;
                    cmd.Parameters.Add(new SqlParameter("@chestitem", System.Data.DbType.Binary)).Value = chestData;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void SaveChestInDatabase(int chestNum)
        {
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            string script = ReadAllText("SQL Data Scripts/Save_Chest.sql");
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                using (var cmd = new SqlCommand(script, sql))
                {
                    byte[] chestData = ToByteArray(ChestItem);

                    cmd.Parameters.Add(new SqlParameter("id", System.Data.SqlDbType.Int)).Value = chestNum;
                    cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = Name;
                    cmd.Parameters.Add(new SqlParameter("@money", System.Data.DbType.Int32)).Value = Money;
                    cmd.Parameters.Add(new SqlParameter("@experience", System.Data.DbType.Int32)).Value = Experience;
                    cmd.Parameters.Add(new SqlParameter("@requiredlevel", System.Data.DbType.Int32)).Value = RequiredLevel;
                    cmd.Parameters.Add(new SqlParameter("@traplevel", System.Data.DbType.Int32)).Value = TrapLevel;
                    cmd.Parameters.Add(new SqlParameter("@key", System.Data.DbType.Int32)).Value = Key;
                    cmd.Parameters.Add(new SqlParameter("@damage", System.Data.DbType.Int32)).Value = Damage;
                    cmd.Parameters.Add(new SqlParameter("@npcspawn", System.Data.DbType.Int32)).Value = NpcSpawn;
                    cmd.Parameters.Add(new SqlParameter("@spawnamount", System.Data.DbType.Int32)).Value = SpawnAmount;
                    cmd.Parameters.Add(new SqlParameter("@chestitem", System.Data.DbType.Binary)).Value = chestData;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void LoadChestFromDatabase(int chestNum)
        {
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            string script = ReadAllText("SQL Data Scripts/Load_Chest.sql");
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                using (SqlCommand cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = chestNum;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Name = reader[1].ToString();
                            Money = ToInt32(reader[2]);
                            Experience = ToInt32(reader[3]);
                            RequiredLevel = ToInt32(reader[4]);
                            TrapLevel = ToInt32(reader[5]);
                            Key = ToInt32(reader[6]);
                            Damage = ToInt32(reader[7]);
                            NpcSpawn = ToInt32(reader[8]);
                            SpawnAmount = ToInt32(reader[9]);
                            byte[] buffer = (byte[])reader[10];
                            object load = ByteArrayToObject(buffer);
                            ChestItem = (ChestItem[])load;
                        }
                    }
                }
            }
        }

        public void LoadChestNameFromDatabase(int chestNum)
        {
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                string command;
                command = "SELECT Name FROM Chests WHERE ID=@id";
                using (SqlCommand cmd = new SqlCommand(command, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("id", System.Data.SqlDbType.Int)).Value = chestNum;
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

        public void TakeItemFromChest(NetIncomingMessage incMSG, int itemNum, int chestSlot, int index)
        {
            int slot = players[index].FindOpenInvSlot(players[index].Backpack);

            if (slot < MAX_INV_SLOTS)
            {
                players[index].Backpack[slot] = items[itemNum];
                HandleData.SendServerMessageTo(players[index].Connection, "You took " + items[itemNum].Name + " from the chest.");
                ChestItem[chestSlot] = new ChestItem("None", 0, 1);
                HandleData.SendPlayerInv(index);
                HandleData.SendChestData(incMSG, index);
            }
            else
            {
                HandleData.SendServerMessageTo(players[index].Connection, "Your backpack is full!");
                return;
            }
        }
    }

    [Serializable()]
    public class ChestItem
    {
        public string Name { get; set; }
        public int ItemNum { get; set; }
        public int Value { get; set; }

        public ChestItem() { }

        public ChestItem(string name, int itemnum, int value)
        {
            Name = name;
            ItemNum = itemnum;
            Value = value;
        }
    }
}
