using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Convert;
using Lidgren.Network;
using static SabertoothServer.Server;
using System.Data.SqlClient;
using static SabertoothServer.Globals;
using static System.IO.File;

namespace SabertoothServer
{
    public class Chat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MainMessage { get; set; }
        public string[] Option = new string[4];
        public int[] NextChat = new int[4];
        public int ShopNum { get; set; }
        public int MissionNum { get; set; }
        public int[] ItemNum = new int[3];
        public int[] ItemVal = new int[3];
        public int Money { get; set; }
        public int Type { get; set; }

        public Chat() { }

        public Chat(string name, string msg, string opt1, string opt2, string opt3, string opt4, int[] nextchat, int shopnum, int missionnum, int[] itemNum, int[] itemVal, int money, int type)
        {
            Name = name;
            MainMessage = msg;
            Option[0] = opt1;
            Option[1] = opt2;
            Option[2] = opt3;
            Option[3] = opt4;
            NextChat = nextchat;
            ShopNum = shopnum;
            MissionNum = missionnum;
            ItemNum = itemNum;
            ItemVal = itemVal;
            Money = money;
            Type = type;
        }

        public void CreateChatInDatabase()
        {
            Name = "Default";
            MainMessage = "None";
            Option[0] = "None";
            Option[1] = "None";
            Option[2] = "None";
            Option[3] = "None";
            NextChat[0] = 0;
            NextChat[1] = 0;
            NextChat[2] = 0;
            NextChat[3] = 0;
            ShopNum = 0;
            MissionNum = 0;
            for (int i = 0; i < 3; i++)
            {
                ItemNum[i] = 0;
                ItemVal[i] = 1;
            }
            Money = 0;
            Type = (int)ChatTypes.None;

            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            string script = ReadAllText("SQL Data Scripts/INSERT CHAT.sql");
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = Name;
                    cmd.Parameters.Add(new SqlParameter("@msg", System.Data.DbType.String)).Value = MainMessage;
                    cmd.Parameters.Add(new SqlParameter("@optiona", System.Data.DbType.String)).Value = Option[0];
                    cmd.Parameters.Add(new SqlParameter("@optionb", System.Data.DbType.String)).Value = Option[1];
                    cmd.Parameters.Add(new SqlParameter("@optionc", System.Data.DbType.String)).Value = Option[2];
                    cmd.Parameters.Add(new SqlParameter("@optiond", System.Data.DbType.String)).Value = Option[3];
                    cmd.Parameters.Add(new SqlParameter("@nextchata", System.Data.DbType.Int32)).Value = NextChat[0];
                    cmd.Parameters.Add(new SqlParameter("@nextchatb", System.Data.DbType.Int32)).Value = NextChat[1];
                    cmd.Parameters.Add(new SqlParameter("@nextchatc", System.Data.DbType.Int32)).Value = NextChat[2];
                    cmd.Parameters.Add(new SqlParameter("@nextchatd", System.Data.DbType.Int32)).Value = NextChat[3];
                    cmd.Parameters.Add(new SqlParameter("@shopnum", System.Data.DbType.Int32)).Value = ShopNum;
                    cmd.Parameters.Add(new SqlParameter("@missionnum", System.Data.DbType.Int32)).Value = MissionNum;
                    cmd.Parameters.Add(new SqlParameter("@itema", System.Data.DbType.Int32)).Value = ItemNum[0];
                    cmd.Parameters.Add(new SqlParameter("@itemb", System.Data.DbType.Int32)).Value = ItemNum[1];
                    cmd.Parameters.Add(new SqlParameter("@itemc", System.Data.DbType.Int32)).Value = ItemNum[2];
                    cmd.Parameters.Add(new SqlParameter("@vala", System.Data.DbType.Int32)).Value = ItemVal[0];
                    cmd.Parameters.Add(new SqlParameter("@valb", System.Data.DbType.Int32)).Value = ItemVal[1];
                    cmd.Parameters.Add(new SqlParameter("@valc", System.Data.DbType.Int32)).Value = ItemVal[2];
                    cmd.Parameters.Add(new SqlParameter("@money", System.Data.DbType.Int32)).Value = Money;
                    cmd.Parameters.Add(new SqlParameter("@type", System.Data.DbType.Int32)).Value = Type;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void SaveChatInDatabase(int chatNum)
        {
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            string script = ReadAllText("SQL Data Scripts/SAVE CHAT.sql");
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();

                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("id", System.Data.SqlDbType.Int)).Value = chatNum;
                    cmd.Parameters.Add(new SqlParameter("name", System.Data.SqlDbType.Text)).Value = Name;
                    cmd.Parameters.Add(new SqlParameter("@msg", System.Data.DbType.String)).Value = MainMessage;
                    cmd.Parameters.Add(new SqlParameter("@optiona", System.Data.DbType.String)).Value = Option[0];
                    cmd.Parameters.Add(new SqlParameter("@optionb", System.Data.DbType.String)).Value = Option[1];
                    cmd.Parameters.Add(new SqlParameter("@optionc", System.Data.DbType.String)).Value = Option[2];
                    cmd.Parameters.Add(new SqlParameter("@optiond", System.Data.DbType.String)).Value = Option[3];
                    cmd.Parameters.Add(new SqlParameter("@nextchata", System.Data.DbType.Int32)).Value = NextChat[0];
                    cmd.Parameters.Add(new SqlParameter("@nextchatb", System.Data.DbType.Int32)).Value = NextChat[1];
                    cmd.Parameters.Add(new SqlParameter("@nextchatc", System.Data.DbType.Int32)).Value = NextChat[2];
                    cmd.Parameters.Add(new SqlParameter("@nextchatd", System.Data.DbType.Int32)).Value = NextChat[3];
                    cmd.Parameters.Add(new SqlParameter("@shopnum", System.Data.DbType.Int32)).Value = ShopNum;
                    cmd.Parameters.Add(new SqlParameter("@missionnum", System.Data.DbType.Int32)).Value = MissionNum;
                    cmd.Parameters.Add(new SqlParameter("@itema", System.Data.DbType.Int32)).Value = ItemNum[0];
                    cmd.Parameters.Add(new SqlParameter("@itemb", System.Data.DbType.Int32)).Value = ItemNum[1];
                    cmd.Parameters.Add(new SqlParameter("@itemc", System.Data.DbType.Int32)).Value = ItemNum[2];
                    cmd.Parameters.Add(new SqlParameter("@vala", System.Data.DbType.Int32)).Value = ItemVal[0];
                    cmd.Parameters.Add(new SqlParameter("@valb", System.Data.DbType.Int32)).Value = ItemVal[1];
                    cmd.Parameters.Add(new SqlParameter("@valc", System.Data.DbType.Int32)).Value = ItemVal[2];
                    cmd.Parameters.Add(new SqlParameter("@money", System.Data.DbType.Int32)).Value = Money;
                    cmd.Parameters.Add(new SqlParameter("@type", System.Data.DbType.Int32)).Value = Type;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void LoadChatFromDatabase(int chatNum)
        {
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            string script = ReadAllText("SQL Data Scripts/LOAD CHAT.sql");
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                using (SqlCommand cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = chatNum;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Name = reader[1].ToString();
                            MainMessage = reader[2].ToString();
                            Option[0] = reader[3].ToString();
                            Option[1] = reader[4].ToString();
                            Option[2] = reader[5].ToString();
                            Option[3] = reader[6].ToString();
                            NextChat[0] = ToInt32(reader[7]);
                            NextChat[1] = ToInt32(reader[8]);
                            NextChat[2] = ToInt32(reader[9]);
                            NextChat[3] = ToInt32(reader[10]);
                            ShopNum = ToInt32(reader[11]);
                            MissionNum = ToInt32(reader[12]);
                            ItemNum[0] = ToInt32(reader[13]);
                            ItemNum[1] = ToInt32(reader[14]);
                            ItemNum[2] = ToInt32(reader[15]);
                            ItemVal[0] = ToInt32(reader[16]);
                            ItemVal[1] = ToInt32(reader[17]);
                            ItemVal[2] = ToInt32(reader[18]);
                            Money = ToInt32(reader[19]);
                            Type = ToInt32(reader[20]);
                        }
                    }
                }
            }
        }

        public void LoadChatNameFromDatabase(int chatNum)
        {
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                string command;
                command = "SELECT NAME FROM CHAT WHERE ID=@id";
                using (SqlCommand cmd = new SqlCommand(command, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("id", System.Data.SqlDbType.Int)).Value = chatNum;
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

    public enum ChatTypes : int
    {
        None,
        OpenShop,
        OpenBank,
        Reward,
        Mission
    }
}
