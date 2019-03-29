using System;
using System.IO;

namespace SabertoothServer
{
    public static class Logging
    {
        /// <summary>
        /// Writes a console message and saves to a log file.
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="logType"></param>
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

        /// <summary>
        /// Requests console input and saves to a log file.
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="logType"></param>
        public static void WriteMessageLogLine(string logMessage, string logType = "Server")
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

        /// <summary>
        /// Saves only to a log file.
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="logType"></param>
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
