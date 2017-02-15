﻿using System;
using System.Data.SQLite;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Lidgren.Network;
using static System.Convert;

namespace Server.Classes
{
    public class Chest
    {
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

            using (var conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
            {
                using (var cmd = new SQLiteCommand(conn))
                {
                    byte[] chestData = ToByteArray(ChestItem);

                    conn.Open();
                    string command;
                    command = "INSERT INTO CHESTS (NAME,MONEY,EXPERIENCE,REQUIREDLEVEL,TRAPLEVEL,KEY,DAMAGE,NPCSPAWN,SPAWNAMOUNT,CHESTITEM) VALUES ";
                    command = command + "(@name,@money,@experience,@requiredlevel,@traplevel,@key,@damage,@npcspawn,@spawnamount,@chestitem)";
                    cmd.CommandText = command;
                    cmd.Parameters.Add("@name", System.Data.DbType.String).Value = Name;
                    cmd.Parameters.Add("@money", System.Data.DbType.Int32).Value = Money;
                    cmd.Parameters.Add("@experience", System.Data.DbType.Int32).Value = Experience;
                    cmd.Parameters.Add("@requiredlevel", System.Data.DbType.Int32).Value = RequiredLevel;
                    cmd.Parameters.Add("@traplevel", System.Data.DbType.Int32).Value = TrapLevel;
                    cmd.Parameters.Add("@key", System.Data.DbType.Int32).Value = Key;
                    cmd.Parameters.Add("@damage", System.Data.DbType.Int32).Value = Damage;
                    cmd.Parameters.Add("@npcspawn", System.Data.DbType.Int32).Value = NpcSpawn;
                    cmd.Parameters.Add("@spawnamount", System.Data.DbType.Int32).Value = SpawnAmount;
                    cmd.Parameters.Add("@chestitem", System.Data.DbType.Binary).Value = chestData;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void SaveChestInDatabase(int chestNum)
        {
            using (var conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
            {
                using (var cmd = new SQLiteCommand(conn))
                {
                    byte[] chestData = ToByteArray(ChestItem);

                    conn.Open();
                    string command;
                    command = "UPDATE CHESTS SET NAME = @name, MONEY = @money, EXPERIENCE = @experience, REQUIREDLEVEL = @requiredlevel, TRAPLEVEL = @traplevel, ";
                    command = command + "KEY = @key, DAMAGE = @damage, NPCSPAWN = @npcspawn, SPAWNAMOUNT = @spawnamount, CHESTITEM = @chestitem WHERE rowid = " + chestNum + ";";
                    cmd.CommandText = command;
                    cmd.Parameters.Add("@name", System.Data.DbType.String).Value = Name;
                    cmd.Parameters.Add("@money", System.Data.DbType.Int32).Value = Money;
                    cmd.Parameters.Add("@experience", System.Data.DbType.Int32).Value = Experience;
                    cmd.Parameters.Add("@requiredlevel", System.Data.DbType.Int32).Value = RequiredLevel;
                    cmd.Parameters.Add("@traplevel", System.Data.DbType.Int32).Value = TrapLevel;
                    cmd.Parameters.Add("@key", System.Data.DbType.Int32).Value = Key;
                    cmd.Parameters.Add("@damage", System.Data.DbType.Int32).Value = Damage;
                    cmd.Parameters.Add("@npcspawn", System.Data.DbType.Int32).Value = NpcSpawn;
                    cmd.Parameters.Add("@spawnamount", System.Data.DbType.Int32).Value = SpawnAmount;
                    cmd.Parameters.Add("@chestitem", System.Data.DbType.Binary).Value = chestData;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void LoadChestFromDatabase(int chestNum)
        {
            using (var conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
            {
                using (var cmd = new SQLiteCommand(conn))
                {
                    conn.Open();
                    string command;
                    command = "SELECT * FROM CHESTS WHERE rowid = " + chestNum;
                    cmd.CommandText = command;
                    using (SQLiteDataReader read = cmd.ExecuteReader(System.Data.CommandBehavior.SequentialAccess))
                    {
                        while (read.Read())
                        {
                            Name = read["NAME"].ToString();
                            Money = ToInt32(read["MONEY"].ToString());
                            Experience = ToInt32(read["EXPERIENCE"].ToString());
                            RequiredLevel = ToInt32(read["REQUIREDLEVEL"].ToString());
                            TrapLevel = ToInt32(read["TRAPLEVEL"].ToString());
                            Key = ToInt32(read["KEY"].ToString());
                            Damage = ToInt32(read["DAMAGE"].ToString());
                            NpcSpawn = ToInt32(read["NPCSPAWN"].ToString());
                            SpawnAmount = ToInt32(read["SPAWNAMOUNT"].ToString());
                            byte[] buffer = (byte[])read["CHESTITEM"];
                            object load = ByteArrayToObject(buffer);
                            ChestItem = (ChestItem[])load;
                        }
                    }
                }
            }
        }

        public void LoadChestNameFromDatabase(int chestNum)
        {
            using (var conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
            {
                using (var cmd = new SQLiteCommand(conn))
                {
                    conn.Open();
                    string command;
                    command = "SELECT * FROM CHESTS WHERE rowid = " + chestNum;
                    cmd.CommandText = command;
                    using (SQLiteDataReader read = cmd.ExecuteReader(System.Data.CommandBehavior.SequentialAccess))
                    {
                        while (read.Read())
                        {
                            Name = read["NAME"].ToString();
                        }
                    }
                }
            }
        }

        public void TakeItemFromChest(NetIncomingMessage incMSG, NetServer s_Server, Player[] s_Player, Item s_Item, int chestSlot, int index)
        {
            HandleData hData = new HandleData();
            int slot = s_Player[index].FindOpenInvSlot(s_Player[index].Backpack);

            if (slot < 25)
            {
                s_Player[index].Backpack[slot] = s_Item;
                ChestItem[chestSlot] = new ChestItem("None", 0, 1);
                hData.SendServerMessageTo(s_Player[index].Connection, s_Server, "You took " + s_Item.Name + " from the chest.");
                hData.SendPlayerInv(s_Server, s_Player, index);
                hData.SendChestData(incMSG, s_Server, this, index);
            }
            else
            {
                hData.SendServerMessageTo(s_Player[index].Connection, s_Server, "Your backpack is full!");
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