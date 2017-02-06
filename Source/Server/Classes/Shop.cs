using System;
using System.Data.SQLite;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Lidgren.Network;

namespace Server.Classes
{
    public class Shop
    {
        public ShopItem[] shopItem = new ShopItem[25];

        public string Name { get; set; }

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

        public void SaveShopInDatabase(int shopNum)
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

        public void LoadShopFromDatabase(int shopNum)
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

        public void LoadShopNameFromDatabase(int shopNum)
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

        public void BuyShopItem(NetServer s_Server, Player[] s_Player, Item s_Item, int shopSlot, int index)
        {
            HandleData hData = new HandleData();
            int cash = s_Player[index].Money;
            int cost = shopItem[shopSlot].Cost;

            if (cost == 1) { cost = s_Item.Price; }

            if (cash >= cost)
            {
                int slot = s_Player[index].FindOpenInvSlot(s_Player[index].Backpack);
                if (slot < 25)
                {
                    s_Player[index].Money -= cost;
                    s_Player[index].Backpack[slot] = s_Item;
                    hData.SendServerMessageTo(s_Player[index].Connection, s_Server, "You purchased " + s_Item.Name + " for " + cost + " dollars!");
                    hData.SendPlayerInv(s_Server, s_Player, index);
                    hData.SendUpdatePlayerStats(s_Server, s_Player, index);
                }
                else
                {
                    hData.SendServerMessageTo(s_Player[index].Connection, s_Server, "Your backpack is full!");
                    return;
                }
            }
            else
            {
                hData.SendServerMessageTo(s_Player[index].Connection, s_Server, "You don't have enough money!");
                return;
            }
        }

        public void SellShopItem(NetServer s_Server, Player[] s_Player, int index, int slot)
        {
            HandleData hData = new HandleData();
            int money = s_Player[index].Money;
            int price = s_Player[index].Backpack[slot].Price;
            money += price;
            s_Player[index].Money = money;
            hData.SendServerMessageTo(s_Player[index].Connection, s_Server, "You sold " + s_Player[index].Backpack[slot].Name + " for " + price + " dollars!");
            s_Player[index].Backpack[slot] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1);            
            hData.SendPlayerInv(s_Server, s_Player, index);
            hData.SendUpdatePlayerStats(s_Server, s_Player, index);
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
