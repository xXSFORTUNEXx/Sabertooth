using System;
using System.IO;

namespace SabertoothServer
{
    public static class Logging
    {
        public static void WriteMessageLog(string logMessage, string logType = "Server")
        {
            if (!Directory.Exists("Logs")) { Directory.CreateDirectory("Logs"); }
            string dir = "Logs/" + logType + " " + DateTime.Now.ToString("yyyMMdd") + ".txt";
            string final = "[" + DateTime.Now.ToString("HH:mm:ss") + "] - " + logMessage;

            using (StreamWriter logFile = File.AppendText(dir))
            {
                logFile.WriteLine(final);
                logFile.Flush();
            }
            Console.WriteLine(final);
        }

        public static void WriteLog(string logMessage, string logType = "Server")
        {
            if (!Directory.Exists("Logs")) { Directory.CreateDirectory("Logs"); }
            string dir = "Logs/" + logType + " " + DateTime.Now.ToString("yyyMMdd") + ".txt";
            string final = "[" + DateTime.Now.ToString("HH:mm:ss") + "] - " + logMessage;

            using (StreamWriter logFile = File.AppendText(dir))
            {
                logFile.WriteLine(final);
                logFile.Flush();
            }
        }
    }
}
