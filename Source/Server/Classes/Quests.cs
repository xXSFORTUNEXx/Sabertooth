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
    public class Quests
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StartMessage { get; set; }
        public string InProgressMessage { get; set; }
        public string CompleteMessage { get; set; }
        public int PrerequisiteQuest { get; set; }
        public int LevelRequired { get; set; }
        public int[] ItemNum = new int[5];
        public int[] ItemValue = new int[5];
        public int[] NpcNum = new int[3];
        public int[] NpcValue = new int[3];

        public int[] RewardItem = new int[5];
        public int[] RewardValue = new int[5];
        public int Experience { get; set; }
        public int Money { get; set; }
        public int Type { get; set; }

        public Quests() { }

        public void CreateQuestInDatabase()
        {
            Name = "Default";
            StartMessage = "None";
            InProgressMessage = "None";
            CompleteMessage = "None";
            PrerequisiteQuest = 0;
            LevelRequired = 0;

            for (int i = 0; i < 5; i++)
            {
                ItemNum[i] = 0;
                ItemValue[i] = 1;
                RewardItem[i] = 0;
                RewardValue[i] = 1;
            }

            for (int n = 0; n < 3; n++)
            {
                NpcNum[n] = 0;
                NpcValue[n] = 1;
            }

            Experience = 0;
            Money = 0;
            Type = (int)QuestType.None;

            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            string script = ReadAllText("SQL Data Scripts/INSERT QUEST.sql");
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = Name;
                    cmd.Parameters.Add(new SqlParameter("@startmsg", System.Data.DbType.String)).Value = StartMessage;                    
                    cmd.Parameters.Add(new SqlParameter("@inprogressmsg", System.Data.DbType.String)).Value = InProgressMessage;
                    cmd.Parameters.Add(new SqlParameter("@completemsg", System.Data.DbType.String)).Value = CompleteMessage;
                    cmd.Parameters.Add(new SqlParameter("@prereqquest", System.Data.DbType.Int32)).Value = PrerequisiteQuest;
                    cmd.Parameters.Add(new SqlParameter("@levelreq", System.Data.DbType.Int32)).Value = LevelRequired;
                    cmd.Parameters.Add(new SqlParameter("@reqitem1", System.Data.DbType.Int32)).Value = ItemNum[0];
                    cmd.Parameters.Add(new SqlParameter("@reqitem2", System.Data.DbType.Int32)).Value = ItemNum[1];
                    cmd.Parameters.Add(new SqlParameter("@reqitem3", System.Data.DbType.Int32)).Value = ItemNum[2];
                    cmd.Parameters.Add(new SqlParameter("@reqitem4", System.Data.DbType.Int32)).Value = ItemNum[3];
                    cmd.Parameters.Add(new SqlParameter("@reqitem5", System.Data.DbType.Int32)).Value = ItemNum[4];
                    cmd.Parameters.Add(new SqlParameter("@reqitemvalue1", System.Data.DbType.Int32)).Value = ItemValue[0];
                    cmd.Parameters.Add(new SqlParameter("@reqitemvalue2", System.Data.DbType.Int32)).Value = ItemValue[1];
                    cmd.Parameters.Add(new SqlParameter("@reqitemvalue3", System.Data.DbType.Int32)).Value = ItemValue[2];
                    cmd.Parameters.Add(new SqlParameter("@reqitemvalue4", System.Data.DbType.Int32)).Value = ItemValue[3];
                    cmd.Parameters.Add(new SqlParameter("@reqitemvalue5", System.Data.DbType.Int32)).Value = ItemValue[4];
                    cmd.Parameters.Add(new SqlParameter("@reqnpc1", System.Data.DbType.Int32)).Value = NpcNum[0];
                    cmd.Parameters.Add(new SqlParameter("@reqnpc2", System.Data.DbType.Int32)).Value = NpcNum[1];
                    cmd.Parameters.Add(new SqlParameter("@reqnpc3", System.Data.DbType.Int32)).Value = NpcNum[2];
                    cmd.Parameters.Add(new SqlParameter("@reqnpcvalue1", System.Data.DbType.Int32)).Value = NpcValue[0];
                    cmd.Parameters.Add(new SqlParameter("@reqnpcvalue2", System.Data.DbType.Int32)).Value = NpcValue[1];
                    cmd.Parameters.Add(new SqlParameter("@reqnpcvalue3", System.Data.DbType.Int32)).Value = NpcValue[2];
                    cmd.Parameters.Add(new SqlParameter("@rewarditem1", System.Data.DbType.Int32)).Value = RewardItem[0];
                    cmd.Parameters.Add(new SqlParameter("@rewarditem2", System.Data.DbType.Int32)).Value = RewardItem[1];
                    cmd.Parameters.Add(new SqlParameter("@rewarditem3", System.Data.DbType.Int32)).Value = RewardItem[2];
                    cmd.Parameters.Add(new SqlParameter("@rewarditem4", System.Data.DbType.Int32)).Value = RewardItem[3];
                    cmd.Parameters.Add(new SqlParameter("@rewarditem5", System.Data.DbType.Int32)).Value = RewardItem[4];
                    cmd.Parameters.Add(new SqlParameter("@rewarditemvalue1", System.Data.DbType.Int32)).Value = RewardValue[0];
                    cmd.Parameters.Add(new SqlParameter("@rewarditemvalue2", System.Data.DbType.Int32)).Value = RewardValue[1];
                    cmd.Parameters.Add(new SqlParameter("@rewarditemvalue3", System.Data.DbType.Int32)).Value = RewardValue[2];
                    cmd.Parameters.Add(new SqlParameter("@rewarditemvalue4", System.Data.DbType.Int32)).Value = RewardValue[3];
                    cmd.Parameters.Add(new SqlParameter("@rewarditemvalue5", System.Data.DbType.Int32)).Value = RewardValue[4];
                    cmd.Parameters.Add(new SqlParameter("@exp", System.Data.DbType.Int32)).Value = Experience;
                    cmd.Parameters.Add(new SqlParameter("@money", System.Data.DbType.Int32)).Value = Money;
                    cmd.Parameters.Add(new SqlParameter("@type", System.Data.DbType.Int32)).Value = Type;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void SaveQuestInDatabase(int questNum)
        {
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            string script = ReadAllText("SQL Data Scripts/SAVE QUEST.sql");
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();

                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@id", System.Data.DbType.Int32)).Value = questNum;
                    cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = Name;
                    cmd.Parameters.Add(new SqlParameter("@startmsg", System.Data.DbType.String)).Value = StartMessage;
                    cmd.Parameters.Add(new SqlParameter("@inprogressmsg", System.Data.DbType.String)).Value = InProgressMessage;
                    cmd.Parameters.Add(new SqlParameter("@completemsg", System.Data.DbType.String)).Value = CompleteMessage;
                    cmd.Parameters.Add(new SqlParameter("@prereqquest", System.Data.DbType.Int32)).Value = PrerequisiteQuest;
                    cmd.Parameters.Add(new SqlParameter("@levelreq", System.Data.DbType.Int32)).Value = LevelRequired;
                    cmd.Parameters.Add(new SqlParameter("@reqitem1", System.Data.DbType.Int32)).Value = ItemNum[0];
                    cmd.Parameters.Add(new SqlParameter("@reqitem2", System.Data.DbType.Int32)).Value = ItemNum[1];
                    cmd.Parameters.Add(new SqlParameter("@reqitem3", System.Data.DbType.Int32)).Value = ItemNum[2];
                    cmd.Parameters.Add(new SqlParameter("@reqitem4", System.Data.DbType.Int32)).Value = ItemNum[3];
                    cmd.Parameters.Add(new SqlParameter("@reqitem5", System.Data.DbType.Int32)).Value = ItemNum[4];
                    cmd.Parameters.Add(new SqlParameter("@reqitemvalue1", System.Data.DbType.Int32)).Value = ItemValue[0];
                    cmd.Parameters.Add(new SqlParameter("@reqitemvalue2", System.Data.DbType.Int32)).Value = ItemValue[1];
                    cmd.Parameters.Add(new SqlParameter("@reqitemvalue3", System.Data.DbType.Int32)).Value = ItemValue[2];
                    cmd.Parameters.Add(new SqlParameter("@reqitemvalue4", System.Data.DbType.Int32)).Value = ItemValue[3];
                    cmd.Parameters.Add(new SqlParameter("@reqitemvalue5", System.Data.DbType.Int32)).Value = ItemValue[4];
                    cmd.Parameters.Add(new SqlParameter("@reqnpc1", System.Data.DbType.Int32)).Value = NpcNum[0];
                    cmd.Parameters.Add(new SqlParameter("@reqnpc2", System.Data.DbType.Int32)).Value = NpcNum[1];
                    cmd.Parameters.Add(new SqlParameter("@reqnpc3", System.Data.DbType.Int32)).Value = NpcNum[2];
                    cmd.Parameters.Add(new SqlParameter("@reqnpcvalue1", System.Data.DbType.Int32)).Value = NpcValue[0];
                    cmd.Parameters.Add(new SqlParameter("@reqnpcvalue2", System.Data.DbType.Int32)).Value = NpcValue[1];
                    cmd.Parameters.Add(new SqlParameter("@reqnpcvalue3", System.Data.DbType.Int32)).Value = NpcValue[2];
                    cmd.Parameters.Add(new SqlParameter("@rewarditem1", System.Data.DbType.Int32)).Value = RewardItem[0];
                    cmd.Parameters.Add(new SqlParameter("@rewarditem2", System.Data.DbType.Int32)).Value = RewardItem[1];
                    cmd.Parameters.Add(new SqlParameter("@rewarditem3", System.Data.DbType.Int32)).Value = RewardItem[2];
                    cmd.Parameters.Add(new SqlParameter("@rewarditem4", System.Data.DbType.Int32)).Value = RewardItem[3];
                    cmd.Parameters.Add(new SqlParameter("@rewarditem5", System.Data.DbType.Int32)).Value = RewardItem[4];
                    cmd.Parameters.Add(new SqlParameter("@rewarditemvalue1", System.Data.DbType.Int32)).Value = RewardValue[0];
                    cmd.Parameters.Add(new SqlParameter("@rewarditemvalue2", System.Data.DbType.Int32)).Value = RewardValue[1];
                    cmd.Parameters.Add(new SqlParameter("@rewarditemvalue3", System.Data.DbType.Int32)).Value = RewardValue[2];
                    cmd.Parameters.Add(new SqlParameter("@rewarditemvalue4", System.Data.DbType.Int32)).Value = RewardValue[3];
                    cmd.Parameters.Add(new SqlParameter("@rewarditemvalue5", System.Data.DbType.Int32)).Value = RewardValue[4];
                    cmd.Parameters.Add(new SqlParameter("@exp", System.Data.DbType.Int32)).Value = Experience;
                    cmd.Parameters.Add(new SqlParameter("@money", System.Data.DbType.Int32)).Value = Money;
                    cmd.Parameters.Add(new SqlParameter("@type", System.Data.DbType.Int32)).Value = Type;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void LoadQuestFromDatabase(int questNum)
        {
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            string script = ReadAllText("SQL Data Scripts/LOAD QUEST.sql");
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                using (SqlCommand cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = questNum;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Name = reader[1].ToString();
                            StartMessage = reader[2].ToString();
                            InProgressMessage = reader[3].ToString();
                            CompleteMessage = reader[4].ToString();
                            PrerequisiteQuest = ToInt32(reader[5]);
                            LevelRequired = ToInt32(reader[6]);
                            ItemNum[0] = ToInt32(reader[7]);
                            ItemNum[1] = ToInt32(reader[8]);
                            ItemNum[2] = ToInt32(reader[9]);
                            ItemNum[3] = ToInt32(reader[10]);
                            ItemNum[4] = ToInt32(reader[11]);
                            ItemValue[0] = ToInt32(reader[12]);
                            ItemValue[1] = ToInt32(reader[13]);
                            ItemValue[2] = ToInt32(reader[14]);
                            ItemValue[3] = ToInt32(reader[15]);
                            ItemValue[4] = ToInt32(reader[16]);
                            NpcNum[0] = ToInt32(reader[17]);
                            NpcNum[1] = ToInt32(reader[18]);
                            NpcNum[2] = ToInt32(reader[19]);
                            NpcValue[0] = ToInt32(reader[20]);
                            NpcValue[1] = ToInt32(reader[21]);
                            NpcValue[2] = ToInt32(reader[22]);
                            RewardItem[0] = ToInt32(reader[23]);
                            RewardItem[1] = ToInt32(reader[24]);
                            RewardItem[2] = ToInt32(reader[25]);
                            RewardItem[3] = ToInt32(reader[26]);
                            RewardItem[4] = ToInt32(reader[27]);
                            RewardValue[0] = ToInt32(reader[28]);
                            RewardValue[1] = ToInt32(reader[29]);
                            RewardValue[2] = ToInt32(reader[30]);
                            RewardValue[3] = ToInt32(reader[31]);
                            RewardValue[4] = ToInt32(reader[32]);
                            Experience = ToInt32(reader[33]);
                            Money = ToInt32(reader[34]);
                            Type = ToInt32(reader[35]);
                        }
                    }
                }
            }
        }

        public void LoadQuestNameFromDatabase(int questNum)
        {
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                string command;
                command = "SELECT NAME FROM  WHERE ID=@id";
                using (SqlCommand cmd = new SqlCommand(command, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("id", System.Data.SqlDbType.Int)).Value = questNum;
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

    public enum QuestType : int
    {
        None,
        TalkToNpc,
        KillNpc,
        GetItemForNpc
    }
}
