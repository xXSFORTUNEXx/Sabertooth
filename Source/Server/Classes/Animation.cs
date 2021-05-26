using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SabertoothServer.Globals;
using static SabertoothServer.Server;
using static System.IO.File;
using static System.Environment;
using System.Data.SqlClient;
using static System.Convert;

namespace SabertoothServer
{
    public class Animation
    {
        public string Name { get; set; }    //name of animation
        
        public int ID { get; set; } //setup the id
        public int SpriteNumber { get; set; }   //the graphic number for the animation
        public int FrameCountH { get; set; }    //Horizontal frame count
        public int FrameCountV { get; set; }    //Verticle frame count
        public int FrameCount { get; set; } //total frame count
        public int FrameDuration { get; set; }  //the duration of each framm in miliseconds
        public int LoopCount { get; set; }  //how many times the full frame count will loop

        public bool RenderBelowTarget { get; set; } //set the render below, right now they will default to above the target below the first fringe layer

        public Animation() { }

        public Animation(string name, 
                        int spritenumber, int fcounth, int fcountv, int fcount, int fduration, int loopcount,
                        bool rbelowtarget)
        {
            Name = name;

            SpriteNumber = spritenumber;
            FrameCountH = fcounth;
            FrameCountV = fcountv;
            FrameCount = fcount;
            FrameDuration = fduration;
            LoopCount = loopcount;
            
            RenderBelowTarget = rbelowtarget;
        }

        public void CreateAnimationInDatabase()
        {
            Name = "Default";
            SpriteNumber = 1;
            FrameCountH = 1;
            FrameCountV = 1;
            FrameCount = 1;
            FrameDuration = 100;
            LoopCount = 0;
            RenderBelowTarget = false;

            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            string script = ReadAllText("SQL Data Scripts/Insert_Animation.sql");
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@name", System.Data.SqlDbType.Text)).Value = Name;
                    cmd.Parameters.Add(new SqlParameter("@spritenumber", System.Data.SqlDbType.Int)).Value = SpriteNumber;
                    cmd.Parameters.Add(new SqlParameter("@framecounth", System.Data.SqlDbType.Int)).Value = FrameCountH;
                    cmd.Parameters.Add(new SqlParameter("@framecountv", System.Data.SqlDbType.Int)).Value = FrameCountV;
                    cmd.Parameters.Add(new SqlParameter("@framecount", System.Data.SqlDbType.Int)).Value = FrameCount;
                    cmd.Parameters.Add(new SqlParameter("@frameduration", System.Data.SqlDbType.Int)).Value = FrameDuration;
                    cmd.Parameters.Add(new SqlParameter("@loopcount", System.Data.SqlDbType.Int)).Value = LoopCount;
                    cmd.Parameters.Add(new SqlParameter("@RenderBelowTarget", System.Data.SqlDbType.Bit)).Value = RenderBelowTarget;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void SaveAnimationToDatabase(int animNum)
        {
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            string script = ReadAllText("SQL Data Scripts/Save_Animation.sql");
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = animNum;
                    cmd.Parameters.Add(new SqlParameter("@name", System.Data.SqlDbType.Text)).Value = Name;
                    cmd.Parameters.Add(new SqlParameter("@spritenumber", System.Data.SqlDbType.Int)).Value = SpriteNumber;
                    cmd.Parameters.Add(new SqlParameter("@framecounth", System.Data.SqlDbType.Int)).Value = FrameCountH;
                    cmd.Parameters.Add(new SqlParameter("@framecountv", System.Data.SqlDbType.Int)).Value = FrameCountV;
                    cmd.Parameters.Add(new SqlParameter("@framecount", System.Data.SqlDbType.Int)).Value = FrameCount;
                    cmd.Parameters.Add(new SqlParameter("@frameduration", System.Data.SqlDbType.Int)).Value = FrameDuration;
                    cmd.Parameters.Add(new SqlParameter("@loopcount", System.Data.SqlDbType.Int)).Value = LoopCount;
                    cmd.Parameters.Add(new SqlParameter("@RenderBelowTarget", System.Data.SqlDbType.Bit)).Value = RenderBelowTarget;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void LoadAnimationFromDatabase(int animNum)
        {
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            string script = ReadAllText("SQL Data Scripts/Load_Animation.sql");
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                using (SqlCommand cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = animNum;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int i = 1;
                        while (reader.Read())
                        {
                            Name = reader[i].ToString(); i += 1;
                            SpriteNumber = ToInt32(reader[i]); i += 1;
                            FrameCountH = ToInt32(reader[i]); i += 1;
                            FrameCountV = ToInt32(reader[i]); i += 1;
                            FrameCount = ToInt32(reader[i]); i += 1;
                            FrameDuration = ToInt32(reader[i]); i += 1;
                            LoopCount = ToInt32(reader[i]); i += 1;
                            RenderBelowTarget = ToBoolean(reader[i]);
                        }
                    }
                }
            }
        }

        public void LoadAnimationNameFromDatabase(int animNum)
        {
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                string command;
                command = "SELECT Name FROM Animation WHERE ID=@id";
                using (SqlCommand cmd = new SqlCommand(command, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("id", System.Data.SqlDbType.Int)).Value = animNum;
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
}
