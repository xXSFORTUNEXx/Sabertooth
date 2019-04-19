using Lidgren.Network;
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using static System.Environment;

namespace UpdateServer
{
    static class UpdateServer
    {
        private static NetServer updateServer;
        private static List<string> files = new List<string>();
        private static bool isRunning;
        private static int totalFiles;

        static void Main(string[] args)
        {
            Console.Title = "Sabertooth Update Server";
            Logging.WriteMessageLog(@"  _____       _               _              _   _     ");
            Logging.WriteMessageLog(@" / ____|     | |             | |            | | | |    ");
            Logging.WriteMessageLog(@"| (___   __ _| |__   ___ _ __| |_ ___   ___ | |_| |__  ");
            Logging.WriteMessageLog(@" \___ \ / _` | '_ \ / _ \ '__| __/ _ \ / _ \| __| '_ \ ");
            Logging.WriteMessageLog(@" ____) | (_| | |_) |  __/ |  | || (_) | (_) | |_| | | |");
            Logging.WriteMessageLog(@"|_____/ \__,_|_.__/ \___|_|   \__\___/ \___/ \__|_| |_|");
            Logging.WriteMessageLog(@"                              Created by Steven Fortune");
            Logging.WriteMessageLog("Setting up configuration...");
            NetPeerConfiguration config = new NetPeerConfiguration("update");
            config.Port = 14243;
            updateServer = new NetServer(config);
            Logging.WriteMessageLog("Checking directories...");
            CheckDirectories();
            LoadFiles();
            updateServer.Start();
            UpdateServerLoop();
            updateServer.Shutdown("Exiting");
            Exit(0);
        }

        static void CheckDirectories()
        {
            if (!Directory.Exists("Updates"))
            {
                Directory.CreateDirectory("Updates");
            }
        }

        static void LoadFiles()
        {
            files.Clear();
            totalFiles = Directory.GetFiles("Updates", "*", SearchOption.AllDirectories).Length;
            string[] dirFiles = Directory.GetFiles("Updates", "*", SearchOption.AllDirectories);

            Logging.WriteMessageLog("Files in update directory: " + totalFiles);

            for (int i = 0; i < totalFiles; i++)
            {
                string filePath = Path.GetFullPath(dirFiles[i]);
                files.Add(filePath);
                Logging.WriteMessageLog(filePath);
            }
        }

        static void CheckFiles()
        {
            foreach (var file in files)
            {
                Logging.WriteMessageLog(file);
            }
        }

        static void UpdateServerLoop()
        {
            Thread commandThread = new Thread(() => CommandWindow());
            commandThread.Start();

            isRunning = true;
            while (isRunning)
            {
                NetIncomingMessage incMSG;
                while ((incMSG = updateServer.ReadMessage()) != null)
                {
                    switch (incMSG.MessageType)
                    {
                        case NetIncomingMessageType.DebugMessage:
                        case NetIncomingMessageType.WarningMessage:
                        case NetIncomingMessageType.ErrorMessage:
                        case NetIncomingMessageType.VerboseDebugMessage:
                            Logging.WriteMessageLog(incMSG.ReadString());
                            break;
                        case NetIncomingMessageType.StatusChanged:
                            NetConnectionStatus status = (NetConnectionStatus)incMSG.ReadByte();
                            switch (status)
                            {
                                case NetConnectionStatus.Connected:
                                    incMSG.SenderConnection.Tag = new StreamingClient(incMSG.SenderConnection, files, files.Count);
                                    Logging.WriteMessageLog("Starting streaming to " + incMSG.SenderConnection);
                                    break;

                                default:
                                    Logging.WriteMessageLog(incMSG.SenderConnection + ": " + status + " (" + incMSG.ReadString() + ")");
                                    break;
                            }
                            break;
                    }
                    updateServer.Recycle(incMSG);
                }

                foreach (NetConnection conn in updateServer.Connections)
                {
                    StreamingClient client = conn.Tag as StreamingClient;
                    if (client != null)
                    {
                        client.Heartbeat();
                    }
                }
                Thread.Sleep(0);
            }
        }
        static void CommandWindow()
        {
            string input;
            while (true)
            {
                Console.Write(">");
                input = Console.ReadLine().ToLower();
                bool isDynamic = false;
                Logging.WriteLog("Command: " + input, "Commands");

                #region Commands
                switch (input)
                {
                    case "shutdown":
                        isRunning = false;
                        break;

                    case "exit":
                        isRunning = false;
                        break;

                    case "reloadfiles":
                        LoadFiles();
                        break;

                    case "checkfiles":
                        CheckFiles();
                        break;

                    case "help":
                        Logging.WriteMessageLog("Commands:", "Commands");
                        Logging.WriteMessageLog("help - shows a list of commands and their use", "Commands");
                        Logging.WriteMessageLog("shutdown - shuts down the server", "Commands");
                        Logging.WriteMessageLog("exit - shuts down the server", "Commands");
                        Logging.WriteMessageLog("reloadfiles - reloads the files being updated from the updates directory", "Commands");
                        Logging.WriteMessageLog("checkfiles - checks what files are in the update queue", "Commands");
                        break;

                    default:
                        if (!isDynamic) { Logging.WriteMessageLog("Please enter a valid command!", "Commands"); }
                        break;
                }
                #endregion
            }
        }
    }


    public class StreamingClient
    {
        private FileStream m_inputStream;
        private int m_sentOffset;
        private int m_chunkLen;
        private byte[] m_tmpBuffer;
        private NetConnection m_connection;
        private int totalFiles;
        private int currentFile;
        private List<string> files = new List<string>();

        public StreamingClient(NetConnection conn, List<string> fileList, int total)
        {
            m_connection = conn;
            files = fileList;
            totalFiles = total;
            currentFile = 0;
            m_inputStream = new FileStream(files[currentFile], FileMode.Open, FileAccess.Read, FileShare.Read);
            m_chunkLen = m_connection.Peer.Configuration.MaximumTransmissionUnit - 20;
            m_tmpBuffer = new byte[m_chunkLen];
            m_sentOffset = 0;
        }

        public void Heartbeat()
        {
            if (m_inputStream == null)
                return;

            if (m_connection.CanSendImmediately(NetDeliveryMethod.ReliableOrdered, 1))
            {
                // send another part of the file!
                long remaining = m_inputStream.Length - m_sentOffset;
                int sendBytes = (remaining > m_chunkLen ? m_chunkLen : (int)remaining);

                // just assume we can read the whole thing in one Read()
                m_inputStream.Read(m_tmpBuffer, 0, sendBytes);

                NetOutgoingMessage om;
                if (m_sentOffset == 0)
                {
                    // first message; send length, chunk length and file name
                    om = m_connection.Peer.CreateMessage(sendBytes + 8);
                    om.Write((ulong)m_inputStream.Length);
                    om.Write(Path.GetFileName(files[currentFile]));
                    om.Write(currentFile);
                    om.Write(totalFiles);
                    m_connection.SendMessage(om, NetDeliveryMethod.ReliableOrdered, 1);
                    long size = new FileInfo(files[currentFile]).Length;
                    Logging.WriteMessageLog("Sending file: " + Path.GetFileName(files[currentFile]) + " size: " + size + " bytes");
                }

                om = m_connection.Peer.CreateMessage(sendBytes + 8);
                om.Write(m_tmpBuffer, 0, sendBytes);

                m_connection.SendMessage(om, NetDeliveryMethod.ReliableOrdered, 1);
                m_sentOffset += sendBytes;

                //Program.Output("Sent " + m_sentOffset + "/" + m_inputStream.Length + " bytes to " + m_connection);

                if (remaining - sendBytes <= 0)
                {
                    Logging.WriteMessageLog("File complete, checking for more files...");
                    m_inputStream.Flush();
                    m_inputStream.Close();
                    m_inputStream.Dispose();
                    m_inputStream = null;
                    currentFile += 1;

                    if (currentFile < totalFiles)
                    {
                        m_inputStream = new FileStream(files[currentFile], FileMode.Open, FileAccess.Read, FileShare.Read);
                        m_chunkLen = m_connection.Peer.Configuration.MaximumTransmissionUnit - 20;
                        m_tmpBuffer = new byte[m_chunkLen];
                        m_sentOffset = 0;
                    }
                }
            }
        }
    }

    enum Packets : byte
    {
        FileTransfer,
        CloseClient
    }
}
