using System;
using System.IO;

namespace Server.Classes
{
    static class LogWriter
    {
        public static void WriteLog(string entry, string logType)
        {
            string dir = "Logs/" + logType + " " + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            string finalEntry = "[" + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + "] " + entry;
            StreamWriter logFile = File.AppendText(dir);
            logFile.WriteLine(finalEntry);
            logFile.Flush();
            logFile.Close();
        }
    }
}
