using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SabertoothClient.Globals;

namespace SabertoothClient
{
    public static class Logging
    {
        public static void WriteMessageLog(string logMessage, string logType = "Client")
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

        public static void WriteMessageLogLine(string logMessage, string logType = "Client")
        {
            if (!Directory.Exists("Logs")) { Directory.CreateDirectory("Logs"); }
            string dir = "Logs/" + logType + " " + DateTime.Now.ToString("yyyMMdd") + ".txt";
            string final = "[" + DateTime.Now.ToString("HH:mm:ss") + "] - " + logMessage;

            using (StreamWriter logFile = File.AppendText(dir))
            {
                logFile.WriteLine(final);
                logFile.Flush();
            }
            Console.Write(final);
        }

        public static void WriteLog(string logMessage, string logType = "Client")
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
