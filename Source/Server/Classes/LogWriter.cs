using System;
using System.IO;
using static System.IO.Directory;

namespace Server.Classes
{
    static class LogWriter
    {
        public static void WriteLog(string entry, string logType)
        {
            if (!Exists("Logs"))
            {
                CreateDirectory("Logs");                
            }

            string dir = "Logs/" + logType + " " + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            string finalEntry = "[" + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + "] " + entry;
            StreamWriter logFile = File.AppendText(dir);
            logFile.WriteLine(finalEntry);
            logFile.Flush();
            logFile.Close();
        }
    }
}
