using System;
using static System.Environment;
using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace Client.Classes
{
    class GameTime
    {
        DateTime g_GameTime;
        public int g_Second { get; set; }
        public int g_Minute { get; set; }
        public int g_Hour { get; set; }
        public DayOfWeek g_DayOfWeek { get; set; }
        public int g_Month { get; set; }
        public int g_Year { get; set; }

        public NightOverlay n_Overlay = new NightOverlay();

        public bool g_Day { get; set; }
        public bool g_Night { get; set; }
        int timeTick;
        public bool updateTime = false;

        string dateFormat = "MMM dd, yyyy hh:mm:ss tt";

        public string Time { get; set; }

        public GameTime() { }

        public GameTime(int year, int month, DayOfWeek day, int hour, int minute, int second)
        {
            g_Second = second;
            g_Minute = minute;
            g_Hour = hour;
            g_DayOfWeek = day;
            g_Month = month;
            g_Year = year;

            g_GameTime = new DateTime(g_Year, g_Month, (int)g_DayOfWeek, g_Hour, g_Minute, g_Second, DateTimeKind.Utc);
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
                    g_DayOfWeek += 1;
                }

                if (g_Hour > 18 || g_Hour < 7) { g_Night = true; g_Day = false; }
                else { g_Day = true; g_Night = false; }

                g_GameTime = new DateTime(g_Year, g_Month, (int)g_DayOfWeek, g_Hour, g_Minute, g_Second, DateTimeKind.Utc);
                Time = g_GameTime.ToString(dateFormat);
                timeTick = TickCount;
            }
        }
    }

    class NightOverlay: Drawable
    {
        Texture overlay = new Texture("Resources/Skins/NightOverlay.png");
        VertexArray v_overlay = new VertexArray(PrimitiveType.Quads, 4);

        public virtual void Draw(RenderTarget target, RenderStates states)
        {            
            v_overlay[0] = new Vertex(new Vector2f(0, 0), new Vector2f(0, 0));
            v_overlay[1] = new Vertex(new Vector2f(800, 0), new Vector2f(800, 0));
            v_overlay[2] = new Vertex(new Vector2f(800, 600), new Vector2f(800, 600));
            v_overlay[3] = new Vertex(new Vector2f(0, 600), new Vector2f(0, 600));

            states.Texture = overlay;
            target.Draw(v_overlay, states);
        }
    }
}
