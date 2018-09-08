﻿using System;
using System.Data.SQLite;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Lidgren.Network;
using static SabertoothServer.Server;
using System.Data.SqlClient;
using static SabertoothServer.Globals;
using static System.IO.File;

namespace SabertoothServer
{
    public class Shop
    {
        public ShopItem[] shopItem = new ShopItem[25];

        public string Name { get; set; }
        public int Id { get; set; }

        public Shop()
        {
            for (int i = 0; i < 25; i++)
            {
                shopItem[i] = new ShopItem("None", 1, 0);
            }
        }

        public Shop(string name)
        {
            Name = name;
            Id = 0;

            for (int i = 0; i < 25; i++)
            {
                shopItem[i] = new ShopItem("None", 1, 0);
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

        public void CreateShopInDatabase()
        {
            Name = "Default";
            for (int i = 0; i < 25; i++)
            {
                shopItem[i] = new ShopItem("None", 1, 0);
            }

            if (DBType == SQL_DATABASE_REMOTE.ToString())
            {
                string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
                string script = ReadAllText("SQL Data Scripts/INSERT SHOP.sql");
                using (var sql = new SqlConnection(connection))
                {
                    sql.Open();
                    using (var cmd = new SqlCommand(script, sql))
                    {
                        byte[] itemData = ToByteArray(shopItem);

                        cmd.Parameters.Add(new SqlParameter("name", System.Data.SqlDbType.Text)).Value = Name;
                        cmd.Parameters.Add(new SqlParameter("itemdata", System.Data.SqlDbType.VarBinary)).Value = itemData;
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
                        byte[] itemData = ToByteArray(shopItem);

                        conn.Open();
                        cmd.CommandText = "INSERT INTO `SHOPS` (`NAME`,`ITEMDATA`) VALUES ('" + Name + "', @itemdata)";
                        cmd.Parameters.Add("@itemdata", System.Data.DbType.Binary, itemData.Length).Value = itemData;
                        cmd.ExecuteNonQuery();
                    }
                }
            }

        }

        public void SaveShopInDatabase(int shopNum)
        {
            if (DBType == SQL_DATABASE_REMOTE.ToString())
            {
                string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
                string script = ReadAllText("SQL Data Scripts/SAVE SHOP.sql");
                using (var sql = new SqlConnection(connection))
                {
                    sql.Open();
                    using (var cmd = new SqlCommand(script, sql))
                    {
                        byte[] itemData = ToByteArray(shopItem);

                        cmd.Parameters.Add(new SqlParameter("id", System.Data.SqlDbType.Int)).Value = shopNum;
                        cmd.Parameters.Add(new SqlParameter("name", System.Data.SqlDbType.Text)).Value = Name;
                        cmd.Parameters.Add(new SqlParameter("itemdata", System.Data.SqlDbType.VarBinary)).Value = itemData;
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
                        byte[] itemData = ToByteArray(shopItem);

                        conn.Open();
                        cmd.CommandText = "UPDATE SHOPS SET NAME = @name, ITEMDATA = @itemdata WHERE rowid = " + shopNum + ";";
                        cmd.Parameters.Add("@name", System.Data.DbType.String).Value = Name;
                        cmd.Parameters.Add("@itemdata", System.Data.DbType.Binary, itemData.Length).Value = itemData;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void LoadShopFromDatabase(int shopNum)
        {
            if (DBType == SQL_DATABASE_REMOTE.ToString())
            {
                string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
                string script = ReadAllText("SQL Data Scripts/LOAD SHOP.sql");
                using (var sql = new SqlConnection(connection))
                {
                    sql.Open();
                    using (SqlCommand cmd = new SqlCommand(script, sql))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = shopNum;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Name = reader[1].ToString();
                                byte[] buffer = (byte[])reader[2];
                                object load = ByteArrayToObject(buffer);
                                shopItem = (ShopItem[])load;
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
                        cmd.CommandText = "SELECT * FROM SHOPS WHERE rowid = " + shopNum;
                        using (SQLiteDataReader read = cmd.ExecuteReader(System.Data.CommandBehavior.SequentialAccess))
                        {
                            while (read.Read())
                            {
                                Name = read["NAME"].ToString();

                                byte[] buffer = (byte[])read["ITEMDATA"];
                                object load = ByteArrayToObject(buffer);
                                shopItem = (ShopItem[])load;
                            }
                        }
                    }
                }
            }
        }

        public void LoadShopNameFromDatabase(int shopNum)
        {
            if (DBType == SQL_DATABASE_REMOTE.ToString())
            {
                string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
                using (var sql = new SqlConnection(connection))
                {
                    sql.Open();
                    string command;
                    command = "SELECT NAME FROM SHOPS WHERE ID=@id";
                    using (SqlCommand cmd = new SqlCommand(command, sql))
                    {
                        cmd.Parameters.Add(new SqlParameter("id", System.Data.SqlDbType.Int)).Value = shopNum;
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
                using (var conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
                {
                    using (var cmd = new SQLiteCommand(conn))
                    {
                        conn.Open();
                        cmd.CommandText = "SELECT * FROM SHOPS WHERE rowid = " + shopNum;
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

        public void BuyShopItem(int itemNum, int shopSlot, int index)
        {
            int cash = players[index].Money;
            int cost = shopItem[shopSlot].Cost;

            if (cost == 1) { cost = items[itemNum].Price; }

            if (cash >= cost)
            {
                int slot = players[index].FindOpenInvSlot(players[index].Backpack);
                if (slot < 25)
                {
                    players[index].Money -= cost;
                    players[index].Backpack[slot] = items[itemNum];
                    HandleData.SendServerMessageTo(players[index].Connection, "You purchased " + items[itemNum].Name + " for " + cost + " dollars!");
                    HandleData.SendPlayerInv(index);
                    HandleData.SendUpdatePlayerStats(index);
                }
                else
                {
                    HandleData.SendServerMessageTo(players[index].Connection, "Your backpack is full!");
                    return;
                }
            }
            else
            {
                HandleData.SendServerMessageTo(players[index].Connection, "You don't have enough money!");
                return;
            }
        }

        public void SellShopItem(int index, int slot)
        {
            int money = players[index].Money;
            int price = players[index].Backpack[slot].Price;
            money += price;
            players[index].Money = money;
            HandleData.SendServerMessageTo(players[index].Connection, "You sold " + players[index].Backpack[slot].Name + " for " + price + " dollars!");
            players[index].Backpack[slot] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0);            
            HandleData.SendPlayerInv(index);
            HandleData.SendUpdatePlayerStats(index);
        }
    }

    [Serializable()]
    public class ShopItem
    {
        public string Name { get; set; }
        public int ItemNum { get; set; }
        public int Cost { get; set; }

        public ShopItem() { }

        public ShopItem(string name, int cost, int itemnum)
        {
            Name = name;
            Cost = cost;
            ItemNum = itemnum;
        }
    }
}
