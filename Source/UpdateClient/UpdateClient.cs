using Lidgren.Network;
using System;
using System.IO;
using System.IO.Compression;
using System.Threading;
using static System.Environment;
using System.Diagnostics;
using System.ComponentModel;

namespace UpdateClient
{
    static class UpdateClient
    {
        private static NetClient updateClient;
        private static ulong s_length;
        private static ulong s_received;
        private static FileStream s_writeStream;
        private static int s_timeStarted;
        private static int lastV;
        private static int totalFiles;
        private static int currentFile;
        private static bool isRunning;

        static void Main(string[] args)
        {
            Console.Title = "Sabertooth Update Client";
            Logging.WriteMessageLog(@"  _____       _               _              _   _     ");
            Logging.WriteMessageLog(@" / ____|     | |             | |            | | | |    ");
            Logging.WriteMessageLog(@"| (___   __ _| |__   ___ _ __| |_ ___   ___ | |_| |__  ");
            Logging.WriteMessageLog(@" \___ \ / _` | '_ \ / _ \ '__| __/ _ \ / _ \| __| '_ \ ");
            Logging.WriteMessageLog(@" ____) | (_| | |_) |  __/ |  | || (_) | (_) | |_| | | |");
            Logging.WriteMessageLog(@"|_____/ \__,_|_.__/ \___|_|   \__\___/ \___/ \__|_| |_|");
            Logging.WriteMessageLog(@"                              Created by Steven Fortune");
            Logging.WriteMessageLog("Setting up configuration...");
            NetPeerConfiguration config = new NetPeerConfiguration("update");
            updateClient = new NetClient(config);
            updateClient.Start();
            UpdateClientLoop();
            updateClient.Shutdown("Exiting");
            Disconnect();
        }

        static void UpdateClientLoop()
        {
            isRunning = true;
            while (isRunning)
            {
                Connect();
                NetIncomingMessage incMSG;
                while ((incMSG = updateClient.ReadMessage()) != null)
                {
                    switch (incMSG.MessageType)
                    {
                        case NetIncomingMessageType.Data:
                            int chunkLen = incMSG.LengthBytes;
                            if (s_length == 0)
                            {
                                s_length = incMSG.ReadUInt64();
                                string filename = incMSG.ReadString();
                                currentFile = incMSG.ReadInt32();
                                totalFiles = incMSG.ReadInt32();
                                Logging.WriteMessageLog("Downloading " + filename + "...Please Wait...");
                                s_writeStream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
                                s_timeStarted = Environment.TickCount;
                                break;
                            }

                            byte[] all = incMSG.ReadBytes(incMSG.LengthBytes);
                            s_received += (ulong)all.Length;
                            s_writeStream.Write(all, 0, all.Length);

                            int v = (int)((float)s_received / s_length * 100.0f);
                            if (lastV < v)
                            {
                                Logging.WriteMessageLog("Percent Complete: " + v + "%");
                                lastV = v;
                                int passed = Environment.TickCount - s_timeStarted;
                                double psec = (double)passed / 1000.0;
                                double bps = (double)s_received / psec;
                                //Logging.WriteMessageLog(NetUtility.ToHumanReadable((long)bps) + " per second");
                                Console.Title = "Sabertooth Update Client - Progress: " + v + "% - Download Speed: " + NetUtility.ToHumanReadable((long)bps) + " per second";
                            }

                            if (s_received >= s_length)
                            {
                                int passed = Environment.TickCount - s_timeStarted;
                                double psec = (double)passed / 1000.0;
                                double bps = (double)s_received / psec;
                                string extension = Path.GetExtension(s_writeStream.Name);
                                string fileName = Path.GetFileName(s_writeStream.Name);
                                string filePath = Path.GetDirectoryName(s_writeStream.Name);
                                string zipDir = Path.GetFileNameWithoutExtension(s_writeStream.Name);

                                Logging.WriteMessageLog("Downloaded " + Path.GetFileName(s_writeStream.Name) + " at " + NetUtility.ToHumanReadable((long)bps) + " per second");

                                s_writeStream.Flush();
                                s_writeStream.Close();
                                s_writeStream.Dispose();
                                s_writeStream = null;

                                s_length = 0;
                                s_received = 0;
                                lastV = 0;

                                ExtractZipFile(extension, zipDir, fileName, filePath);

                                if (currentFile == totalFiles - 1)
                                {
                                    isRunning = false;
                                }
                            }
                        break;
                    }
                    updateClient.Recycle(incMSG);
                }
            }
        }

        static bool ExtractZipFile(string exten, string zipDir, string fileName, string filePath)
        {
            if (exten == ".zip")
            {
                if (Directory.Exists(zipDir))
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(zipDir);

                    foreach (FileInfo file in dirInfo.GetFiles())
                    {
                        file.Delete();
                    }

                    foreach (DirectoryInfo dir in dirInfo.GetDirectories())
                    {
                        dir.Delete(true);
                    }
                    Directory.Delete(zipDir);
                }            
                Logging.WriteMessageLog("Extracting zip file " + fileName + "...");
                ZipFile.ExtractToDirectory(fileName, filePath);
                Logging.WriteMessageLog("Cleaning up...");
                File.Delete(fileName);
                return true;
            }
            return false;
        }

        static void Disconnect()
        {
            updateClient.Disconnect("File transfer complete, closing connection");
            Thread.Sleep(2000);
            LaunchClient();
            Thread.Sleep(5000);
            Exit(0);
        }

        static void LaunchClient()
        {
            try
            {
                using (Process sClient = new Process())
                {
                    sClient.StartInfo.UseShellExecute = false;
                    sClient.StartInfo.FileName = "Client.exe";
                    sClient.Start();
                }
            }
            catch (Exception e)
            {
                Logging.WriteMessageLog(e.Message);
            }
        }

        static void Connect()
        {
            if (updateClient.ServerConnection == null)
            {
                s_length = 0;
                s_received = 0;
                updateClient.Connect("10.16.0.3", 14243);
            }
        }
    }
}
