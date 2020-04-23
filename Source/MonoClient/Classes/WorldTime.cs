using System;
using static System.Environment;
using static Mono_Client.Globals;

namespace Mono_Client
{
    public class WorldTime
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
        public bool updateTime = false;

        string dateFormat = "MMM dd, yyyy hh:mm:ss tt";

        public string Time { get; set; }

        public WorldTime() { }

        public WorldTime(int year, int month, int day, int hour, int minute, int second)
        {
            g_Second = second;
            g_Minute = minute;
            g_Hour = hour;
            g_DayOfWeek = day;
            g_Month = month;
            g_Year = year;

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

                if (g_Hour > 19 || g_Hour < 7) { g_Night = true; g_Day = false; }
                else { g_Day = true; g_Night = false; }

                g_GameTime = new DateTime(g_Year, g_Month, g_DayOfWeek, g_Hour, g_Minute, g_Second, DateTimeKind.Utc);
                Time = g_GameTime.ToString(dateFormat);
                timeTick = TickCount;
            }
        }
    }
}
