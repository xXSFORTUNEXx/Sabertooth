using System;
using static System.Environment;

namespace Server.Classes
{
    class WorldTime
    {
        DateTime g_GameTime;
        public int g_Second { get; set; }
        public int g_Minute { get; set; }
        public int g_Hour { get; set; }
        public int g_DayOfWeek { get; set; }
        public int g_Month { get; set; }
        public int g_Year { get; set; }
        
        public bool g_Day { get; set; }
        public bool g_Night { get; set; }
        int timeTick;

        string dateFormat = "MMM dd, yyyy hh:mm:ss tt";

        public string Time { get; set; }

        public WorldTime()
        {
            g_Second = DateTime.Now.Second;
            g_Minute = DateTime.Now.Minute;
            g_Hour = DateTime.Now.Hour;
            g_DayOfWeek = DateTime.Now.Day;
            g_Month = DateTime.Now.Month;
            g_Year = DateTime.Now.Year;

            g_GameTime = new DateTime(g_Year, g_Month, g_DayOfWeek, g_Hour, g_Minute, g_Second, DateTimeKind.Utc);
            timeTick = TickCount;
        }

        public void UpdateTime()
        {
            if (TickCount - timeTick >= 1000)
            {
                if (g_Second < 59)
                {
                    g_Second += 1;
                }
                else
                {
                    g_Second = 0;
                    g_Minute += 1;
                }

                if (g_Minute >= 60)
                {
                    g_Minute = 0;
                    g_Hour += 1;
                }

                if (g_Hour == 24)
                {
                    g_Hour = 0;
                    g_DayOfWeek += 1;
                }

                if (g_Hour > 20 || g_Hour < 6) { g_Night = true; }
                else { g_Day = true; }

                g_GameTime = new DateTime(g_Year, g_Month, g_DayOfWeek, g_Hour, g_Minute, g_Second, DateTimeKind.Utc);
                Time = g_GameTime.ToString(dateFormat);
                timeTick = TickCount;
            }
        }
    }
}
