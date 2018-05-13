using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using static System.Convert;
using Lidgren.Network;
using static SabertoothServer.Server;
using System.Data.SqlClient;

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

            if (DBType == Globals.SQL_DATABASE_REMOTE.ToString())
            {
                string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
                using (var sql = new SqlConnection(connection))
                {
                    sql.Open();
                    string command;
                    command = "INSERT INTO CHAT (NAME,MAINMESSAGE,OPTIONA,OPTIONB,OPTIONC,OPTIOND,NEXTCHATA,NEXTCHATB,NEXTCHATC,NEXTCHATD,SHOPNUM,MISSIONNUM,ITEMA,ITEMB,ITEMC,VALA,VALB,VALC,MONEY,TYPE) VALUES ";
                    command += "(@name,@msg,@optiona,@optionb,@optionc,@optiond,@nextchata,@nextchatb,@nextchatc,@nextchatd,@shopnum,@missionnum,@itema,@itemb,@itemc,@vala,@valb,@valc,@money,@type)";
                    using (var cmd = new SqlCommand(command, sql))
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
            else
            {
                using (var conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
                {
                    using (var cmd = new SQLiteCommand(conn))
                    {
                        conn.Open();
                        string command;
                        command = "INSERT INTO `CHAT` (`NAME`,`MAINMESSAGE`,`OPTIONA`,`OPTIONB`,`OPTIONC`,`OPTIOND`,`NEXTCHATA`,`NEXTCHATB`,`NEXTCHATC`,`NEXTCHATD`,`SHOPNUM`,`MISSIONNUM`,`ITEMA`,`ITEMB`,`ITEMC`,`VALA`,`VALB`,`VALC`,`MONEY`,`TYPE`) ";
                        command = command + "VALUES ";
                        command = command + "(@name,@msg,@optiona,@optionb,@optionc,@optiond,@nextchata,@nextchatb,@nextchatc,@nextchatd,@shopnum,@missionnum,@itema,@itemb,@itemc,@vala,@valb,@valc,@money,@type);";
                        cmd.CommandText = command;
                        cmd.Parameters.Add("@name", System.Data.DbType.String).Value = Name;
                        cmd.Parameters.Add("@msg", System.Data.DbType.String).Value = MainMessage;
                        cmd.Parameters.Add("@optiona", System.Data.DbType.String).Value = Option[0];
                        cmd.Parameters.Add("@optionb", System.Data.DbType.String).Value = Option[1];
                        cmd.Parameters.Add("@optionc", System.Data.DbType.String).Value = Option[2];
                        cmd.Parameters.Add("@optiond", System.Data.DbType.String).Value = Option[3];
                        cmd.Parameters.Add("@nextchata", System.Data.DbType.Int32).Value = NextChat[0];
                        cmd.Parameters.Add("@nextchatb", System.Data.DbType.Int32).Value = NextChat[1];
                        cmd.Parameters.Add("@nextchatc", System.Data.DbType.Int32).Value = NextChat[2];
                        cmd.Parameters.Add("@nextchatd", System.Data.DbType.Int32).Value = NextChat[3];
                        cmd.Parameters.Add("@shopnum", System.Data.DbType.Int32).Value = ShopNum;
                        cmd.Parameters.Add("@missionnum", System.Data.DbType.Int32).Value = MissionNum;
                        cmd.Parameters.Add("@itema", System.Data.DbType.Int32).Value = ItemNum[0];
                        cmd.Parameters.Add("@itemb", System.Data.DbType.Int32).Value = ItemNum[1];
                        cmd.Parameters.Add("@itemc", System.Data.DbType.Int32).Value = ItemNum[2];
                        cmd.Parameters.Add("@vala", System.Data.DbType.Int32).Value = ItemVal[0];
                        cmd.Parameters.Add("@valb", System.Data.DbType.Int32).Value = ItemVal[1];
                        cmd.Parameters.Add("@valc", System.Data.DbType.Int32).Value = ItemVal[2];
                        cmd.Parameters.Add("@money", System.Data.DbType.Int32).Value = Money;
                        cmd.Parameters.Add("@type", System.Data.DbType.Int32).Value = Type;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void SaveChatInDatabase(int chatNum)
        {
            if (DBType == Globals.SQL_DATABASE_REMOTE.ToString())
            {
                string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
                using (var sql = new SqlConnection(connection))
                {
                    sql.Open();
                    string command;
                    command = "UPDATE CHAT SET ";
                    command = command + "NAME = @name, MAINMESSAGE = @msg, OPTIONA = @optiona, OPTIONB = @optionb, OPTIONC = @optionc, OPTIOND = @optionc, NEXTCHATA = @nextchata, NEXTCHATB = @nextchatb, NEXTCHATC = @nextchatc, NEXTCHATD = @nextchatd, ";
                    command = command + "SHOPNUM = @shopnum, MISSIONNUM = @missionnum, ITEMA = @itema, ITEMB = @itemb, ITEMC = @itemc, VALA = @vala, VALB = @valb, VALC = @valc, MONEY = @money, TYPE = @type ";
                    command = command + "WHERE ID=@id";
                    using (var cmd = new SqlCommand(command, sql))
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
            else
            {
                using (var conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
                {
                    using (var cmd = new SQLiteCommand(conn))
                    {
                        conn.Open();
                        string command;
                        command = "UPDATE CHAT SET ";
                        command = command + "NAME = @name, MAINMESSAGE = @msg, OPTIONA = @optiona, OPTIONB = @optionb, OPTIONC = @optionc, OPTIOND = @optionc, NEXTCHATA = @nextchata, NEXTCHATB = @nextchatb, NEXTCHATC = @nextchatc, NEXTCHATD = @nextchatd, ";
                        command = command + "SHOPNUM = @shopnum, MISSIONNUM = @missionnum, ITEMA = @itema, ITEMB = @itemb, ITEMC = @itemc, VALA = @vala, VALB = @valb, VALC = @valc, MONEY = @money, TYPE = @type ";
                        command = command + "WHERE rowid = " + chatNum + ";";
                        cmd.CommandText = command;
                        cmd.Parameters.Add("@name", System.Data.DbType.String).Value = Name;
                        cmd.Parameters.Add("@msg", System.Data.DbType.String).Value = MainMessage;
                        cmd.Parameters.Add("@optiona", System.Data.DbType.String).Value = Option[0];
                        cmd.Parameters.Add("@optionb", System.Data.DbType.String).Value = Option[1];
                        cmd.Parameters.Add("@optionc", System.Data.DbType.String).Value = Option[2];
                        cmd.Parameters.Add("@optiond", System.Data.DbType.String).Value = Option[3];
                        cmd.Parameters.Add("@nextchata", System.Data.DbType.Int32).Value = NextChat[0];
                        cmd.Parameters.Add("@nextchatb", System.Data.DbType.Int32).Value = NextChat[1];
                        cmd.Parameters.Add("@nextchatc", System.Data.DbType.Int32).Value = NextChat[2];
                        cmd.Parameters.Add("@nextchatd", System.Data.DbType.Int32).Value = NextChat[3];
                        cmd.Parameters.Add("@shopnum", System.Data.DbType.Int32).Value = ShopNum;
                        cmd.Parameters.Add("@missionnum", System.Data.DbType.Int32).Value = MissionNum;
                        cmd.Parameters.Add("@itema", System.Data.DbType.Int32).Value = ItemNum[0];
                        cmd.Parameters.Add("@itemb", System.Data.DbType.Int32).Value = ItemNum[1];
                        cmd.Parameters.Add("@itemc", System.Data.DbType.Int32).Value = ItemNum[2];
                        cmd.Parameters.Add("@vala", System.Data.DbType.Int32).Value = ItemVal[0];
                        cmd.Parameters.Add("@valb", System.Data.DbType.Int32).Value = ItemVal[1];
                        cmd.Parameters.Add("@valc", System.Data.DbType.Int32).Value = ItemVal[2];
                        cmd.Parameters.Add("@money", System.Data.DbType.Int32).Value = Money;
                        cmd.Parameters.Add("@type", System.Data.DbType.Int32).Value = Type;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void LoadChatFromDatabase(int chatNum)
        {
            if (DBType == Globals.SQL_DATABASE_REMOTE.ToString())
            {
                string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
                using (var sql = new SqlConnection(connection))
                {
                    sql.Open();
                    string command;
                    command = "SELECT * FROM CHAT WHERE ID=@id";
                    using (SqlCommand cmd = new SqlCommand(command, sql))
                    {
                        cmd.Parameters.Add(new SqlParameter("id", System.Data.SqlDbType.Int)).Value = chatNum;
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
            else
            {
                using (var conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
                {
                    using (var cmd = new SQLiteCommand(conn))
                    {
                        conn.Open();
                        cmd.CommandText = "SELECT * FROM `CHAT` WHERE rowid = " + chatNum;
                        using (SQLiteDataReader read = cmd.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                Name = read["NAME"].ToString();
                                MainMessage = read["MAINMESSAGE"].ToString();
                                Option[0] = read["OPTIONA"].ToString();
                                Option[1] = read["OPTIONB"].ToString();
                                Option[2] = read["OPTIONC"].ToString();
                                Option[3] = read["OPTIOND"].ToString();
                                NextChat[0] = ToInt32(read["NEXTCHATA"].ToString());
                                NextChat[1] = ToInt32(read["NEXTCHATB"].ToString());
                                NextChat[2] = ToInt32(read["NEXTCHATC"].ToString());
                                NextChat[3] = ToInt32(read["NEXTCHATD"].ToString());
                                ShopNum = ToInt32(read["SHOPNUM"].ToString());
                                MissionNum = ToInt32(read["MISSIONNUM"].ToString());
                                ItemNum[0] = ToInt32(read["ITEMA"].ToString());
                                ItemNum[1] = ToInt32(read["ITEMB"].ToString());
                                ItemNum[2] = ToInt32(read["ITEMC"].ToString());
                                ItemVal[0] = ToInt32(read["VALA"].ToString());
                                ItemVal[1] = ToInt32(read["VALB"].ToString());
                                ItemVal[2] = ToInt32(read["VALC"].ToString());
                                Money = ToInt32(read["MONEY"].ToString());
                                Type = ToInt32(read["TYPE"].ToString());
                            }
                        }
                    }
                }
            }
        }

        public void LoadChatNameFromDatabase(int chatNum)
        {
            if (DBType == Globals.SQL_DATABASE_REMOTE.ToString())
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
            else
            {

            }
            using (var conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
            {
                using (var cmd = new SQLiteCommand(conn))
                {
                    conn.Open();
                    cmd.CommandText = "SELECT * FROM CHAT WHERE rowid = " + chatNum;
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

    public enum ChatTypes : int
    {
        None,
        OpenShop,
        OpenBank,
        Reward,
        Mission
    }
}
