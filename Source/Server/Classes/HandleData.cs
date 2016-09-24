using Lidgren.Network;
using System;
using System.Threading;
using System.Xml;
using static System.Convert;
using static System.IO.File;

namespace Server.Classes
{
    class HandleData
    {
        /// <summary>
        /// This method checks for incoming messages and processes the header (name) of the packet for processing
        /// </summary>
        public void HandleDataMessage(NetServer svrServer, Player[] svrPlayer, Map[] svrMap, NPC[] svrNpc)
        {
            NetIncomingMessage incMSG;  //create incoming message

            if ((incMSG = svrServer.ReadMessage()) != null)
            {
                switch (incMSG.MessageType)
                {
                    case NetIncomingMessageType.DiscoveryRequest:
                        HandleDiscoveryRequest(incMSG, svrServer);
                        break;

                    case NetIncomingMessageType.ConnectionApproval:
                        HandleConnectionApproval(incMSG, svrServer);
                        break;

                    case NetIncomingMessageType.Data:
                        switch (incMSG.ReadByte())
                        {
                            case (byte)PacketTypes.Register:
                                HandleRegisterRequest(incMSG, svrServer, svrPlayer);
                                break;
                            case (byte)PacketTypes.Login:
                                HandleLoginRequest(incMSG, svrServer, svrPlayer, svrMap, svrNpc);
                                break;

                            case (byte)PacketTypes.ChatMessage:
                                HandleChatMessage(incMSG, svrServer, svrPlayer, svrMap);
                                break;

                            case (byte)PacketTypes.MoveData:
                                HandleMoveData(incMSG, svrServer, svrPlayer);
                                break;

                            case (byte)PacketTypes.UpdateDirection:
                                HandleDirectionData(incMSG, svrServer, svrPlayer);
                                break;
                        }
                        break;

                    case NetIncomingMessageType.StatusChanged:
                        HandleStatusChange(incMSG, svrServer, svrPlayer);
                        break;
                }
            }
            svrServer.Recycle(incMSG);
        }

        /// <summary>
        /// These methods handle data that is incoming from connected clients
        /// </summary>
        //Handle incoming packets for movement of the player
        void HandleMoveData(NetIncomingMessage incMSG, NetServer svrServer, Player[] svrPlayer)
        {
            int index = incMSG.ReadInt32();
            int x = incMSG.ReadInt32();
            int y = incMSG.ReadInt32();
            int direction = incMSG.ReadInt32();
            int step = incMSG.ReadInt32();

            if (step == svrPlayer[index].Step) { return; }
            if (x == svrPlayer[index].X && y == svrPlayer[index].Y) { return; }

            svrPlayer[index].X = x;
            svrPlayer[index].Y = y;
            svrPlayer[index].Direction = direction;
            svrPlayer[index].Step = step;

            for (int i = 0; i < 5; i++)
            {
                if (svrPlayer[i].Connection != null && svrPlayer[i].Map == svrPlayer[index].Map)
                {
                    SendUpdateMovementData(svrServer, svrPlayer[i].Connection, index, x, y, direction, step);
                }
            }
        }

        //Handles incoming direction data for players
        void HandleDirectionData(NetIncomingMessage incMSG, NetServer svrServer, Player[] svrPlayer)
        {
            int index = incMSG.ReadInt32();
            int direction = incMSG.ReadInt32();

            if (direction == svrPlayer[index].Direction) { return; }

            svrPlayer[index].Direction = direction;
            SendUpdateDirection(svrServer, index, direction);
        }

        //Handles a discovery response from the server
        void HandleDiscoveryRequest(NetIncomingMessage incMSG, NetServer svrServer)
        {
            Console.WriteLine("Client discovered @ " + incMSG.SenderEndPoint.ToString());
            NetOutgoingMessage outMSG = svrServer.CreateMessage();
            outMSG.Write("Sabertooth Server");
            svrServer.SendDiscoveryResponse(outMSG, incMSG.SenderEndPoint);
        }

        //Handles our connection getting approved from our server
        void HandleConnectionApproval(NetIncomingMessage incMSG, NetServer svrServer)
        {
            if (incMSG.ReadByte() == (byte)PacketTypes.Connection)
            {
                string connect = incMSG.ReadString();
                if (connect == "sabertooth")
                {
                    incMSG.SenderConnection.Approve();
                    Thread.Sleep(500);
                    NetOutgoingMessage outMSG = svrServer.CreateMessage();
                    outMSG.Write((byte)PacketTypes.Connection);
                    svrServer.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
                }
                else
                {
                    incMSG.SenderConnection.Deny();
                }
            }
        }

        //Handles our registration requests to the server
        void HandleRegisterRequest(NetIncomingMessage incMSG, NetServer svrServer, Player[] svrPlayer)
        {
            string username = incMSG.ReadString();
            string password = incMSG.ReadString();

            if (!AlreadyLogged(username, svrPlayer))
            {
                if (!AccountExist(username))
                {
                    int i = OpenSlot(svrPlayer);
                    if (i < 5)
                    {
                        svrPlayer[i] = new Player(username, password, incMSG.SenderConnection);
                        svrPlayer[i].SavePlayerXML();
                        Console.WriteLine("Account created, " + username + ", " + password);
                        SendErrorMessage("Account Created! Please login to play!", "Account Created", incMSG, svrServer);
                        ClearSlot(incMSG.SenderConnection, svrPlayer);
                    }
                    else
                    {
                        Console.WriteLine("Server Full!");
                        SendErrorMessage("Server is full. Please try again later.", "Server Full", incMSG, svrServer);
                    }
                }
                else
                {
                    Console.WriteLine("Account already exists!");
                    SendErrorMessage("Account already exists! Please choose another username.", "Account Exists", incMSG, svrServer);
                }
            }
            else
            {
                Console.WriteLine("Attempted multi-login!");
                SendErrorMessage("Account already logged in. If this is an error, please try again.", "Account Logged", incMSG, svrServer);
            }
        }

        //Handle logging in requests for the server
        void HandleLoginRequest(NetIncomingMessage incMSG, NetServer svrServer, Player[] svrPlayer, Map[] svrMap, NPC[] svrNpc)
        {
            string username = incMSG.ReadString();
            string password = incMSG.ReadString();

            if (!AlreadyLogged(username, svrPlayer))
            {
                if (AccountExist(username) && CheckPassword(username, password))
                {
                    int i = OpenSlot(svrPlayer);
                    if (i < 5)
                    {
                        svrPlayer[i] = new Player(username, password, incMSG.SenderConnection);
                        svrPlayer[i].LoadPlayerXML();
                        int currentMap = svrPlayer[i].Map;
                        Console.WriteLine("Account login by: " + username + ", " + password);
                        NetOutgoingMessage outMSG = svrServer.CreateMessage();
                        outMSG.Write((byte)PacketTypes.Login);
                        svrServer.SendMessage(outMSG, svrPlayer[i].Connection, NetDeliveryMethod.ReliableOrdered);
                        SendUserData(incMSG, svrServer, svrPlayer, i);
                        SendUsers(incMSG, svrServer, svrPlayer);
                        SendMapData(incMSG, svrServer, svrMap[currentMap], svrPlayer);
                        SendNpcs(incMSG, svrServer, svrNpc);
                        SendMapNpcs(incMSG, svrServer, svrMap[currentMap]);
                    }
                    else
                    {
                        Console.WriteLine("Server is full...");
                        SendErrorMessage("Server is full. Please try again later.", "Server Full", incMSG, svrServer);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid login by: " + username + ", " + password);
                    SendErrorMessage("Invalid username or password.", "Invalid Login", incMSG, svrServer);
                }
            }
            else
            {
                Console.WriteLine("Multiple login attempt by: " + username + ", " + password);
                SendErrorMessage("Account already logged in. If this is an error, please try again.", "Account Logged", incMSG, svrServer);
            }
        }
        
        //Handleing a chat message incoming
        void HandleChatMessage(NetIncomingMessage incMSG, NetServer svrServer, Player[] svrPlayer, Map[] svrMap)
        {
            string msg = incMSG.ReadString();
            //Check for an admin command
            if (msg.Substring(0, 1) == "/")
            {
                CheckCommand(msg, GetPlayerConnection(incMSG, svrPlayer), svrPlayer, incMSG, svrServer, svrMap);
                return;
            }
            else
            {
                string name = svrPlayer[GetPlayerConnection(incMSG, svrPlayer)].Name;
                string finalMsg = name + ": " + msg;
                NetOutgoingMessage outMSG = svrServer.CreateMessage();
                outMSG.Write((byte)PacketTypes.ChatMessage);
                outMSG.Write(finalMsg);
                svrServer.SendToAll(outMSG, NetDeliveryMethod.ReliableOrdered);
                LogWriter.WriteLog(finalMsg, "Chat");
                Console.WriteLine(finalMsg);
            }
        }

        /// <summary>
        /// These methods handle sending data to clients that are requesting it
        /// </summary>
        //Sends a direction update to the client so it can be processed and sent to all connected
        void SendUpdateDirection(NetServer svrServer, int index, int direction)
        {
            NetOutgoingMessage outMSG = svrServer.CreateMessage();
            outMSG.Write(index);
            outMSG.Write(direction);
            svrServer.SendToAll(outMSG, NetDeliveryMethod.ReliableOrdered);
        }

        //Sending out the data for movement to be processed by everyone connected
        void SendUpdateMovementData(NetServer svrServer, NetConnection playerConn, int index, int x, int y, int direction, int step)
        {
            NetOutgoingMessage outMSG = svrServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.UpdateMoveData);
            outMSG.Write(index);
            outMSG.Write(x);
            outMSG.Write(y);
            outMSG.Write(direction);
            outMSG.Write(step);

            svrServer.SendMessage(outMSG, playerConn, NetDeliveryMethod.ReliableOrdered);
        }

        //Send user data of the main index
        void SendUserData(NetIncomingMessage incMSG, NetServer svrServer, Player[] svrPlayer, int svrIndex)
        {
            NetOutgoingMessage outMSG = svrServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.UserData);
            outMSG.Write(svrIndex);
            outMSG.Write(svrPlayer[svrIndex].Name);
            outMSG.Write(svrPlayer[svrIndex].X);
            outMSG.Write(svrPlayer[svrIndex].Y);
            outMSG.Write(svrPlayer[svrIndex].Map);
            outMSG.Write(svrPlayer[svrIndex].Direction);
            outMSG.Write(svrPlayer[svrIndex].Sprite);
            svrServer.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
        }

        //Send user data to all
        void SendUsers(NetIncomingMessage incMSG, NetServer svrServer, Player[] svrPlayer)
        {
            NetOutgoingMessage outMSG = svrServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.Users);
            for (int i = 0; i < 5; i++)
            {
                outMSG.Write(svrPlayer[i].Name);
                outMSG.Write(svrPlayer[i].X);
                outMSG.Write(svrPlayer[i].Y);
                outMSG.Write(svrPlayer[i].Map);
                outMSG.Write(svrPlayer[i].Direction);
                outMSG.Write(svrPlayer[i].Sprite);
            }
            svrServer.SendToAll(outMSG, NetDeliveryMethod.ReliableOrdered);
        }

        //Send npc data to client
        void SendNpcs(NetIncomingMessage incMSG, NetServer svrServer, NPC[] svrNpc)
        {
            NetOutgoingMessage outMSG = svrServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.Npcs);
            for (int i = 0; i < 10; i++)
            {
                outMSG.Write(svrNpc[i].Name);
                outMSG.Write(svrNpc[i].X);
                outMSG.Write(svrNpc[i].Y);
                outMSG.Write(svrNpc[i].Direction);
                outMSG.Write(svrNpc[i].Sprite);
                outMSG.Write(svrNpc[i].Step);
                outMSG.Write(svrNpc[i].Owner);
                outMSG.Write(svrNpc[i].Behavior);
                outMSG.Write(svrNpc[i].SpawnTime);
                outMSG.Write(svrNpc[i].isSpawned);
            }
            svrServer.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
            Console.WriteLine("Sending NPS...");
            LogWriter.WriteLog("Sending npcs...", "Server");
        }

        //Send map npcs to client
        void SendMapNpcs(NetIncomingMessage incMSG, NetServer svrServer, Map svrMap)
        {
            NetOutgoingMessage outMSG = svrServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.MapNpc);
            for (int i = 0; i < 10; i++)
            {
                outMSG.Write(svrMap.mapNpc[i].Name);
                outMSG.Write(svrMap.mapNpc[i].X);
                outMSG.Write(svrMap.mapNpc[i].Y);
                outMSG.Write(svrMap.mapNpc[i].Direction);
                outMSG.Write(svrMap.mapNpc[i].Sprite);
                outMSG.Write(svrMap.mapNpc[i].Step);
                outMSG.Write(svrMap.mapNpc[i].Owner);
                outMSG.Write(svrMap.mapNpc[i].Behavior);
                outMSG.Write(svrMap.mapNpc[i].SpawnTime);
                outMSG.Write(svrMap.mapNpc[i].isSpawned);
            }
            svrServer.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
        }

        //Send npc's associated with the map the player is currently on
        public void SendMapNpcData(NetServer svrServer, NetConnection playerConn, Map svrMap, int npcNum)
        {
            NetOutgoingMessage outMSG = svrServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.NpcData);
            outMSG.Write(npcNum);
            outMSG.Write(svrMap.mapNpc[npcNum].Name);
            outMSG.Write(svrMap.mapNpc[npcNum].X);
            outMSG.Write(svrMap.mapNpc[npcNum].Y);
            outMSG.Write(svrMap.mapNpc[npcNum].Direction);
            outMSG.Write(svrMap.mapNpc[npcNum].Sprite);
            outMSG.Write(svrMap.mapNpc[npcNum].Step);
            outMSG.Write(svrMap.mapNpc[npcNum].Owner);
            outMSG.Write(svrMap.mapNpc[npcNum].Behavior);
            outMSG.Write(svrMap.mapNpc[npcNum].SpawnTime);
            outMSG.Write(svrMap.mapNpc[npcNum].isSpawned);

            svrServer.SendMessage(outMSG, playerConn, NetDeliveryMethod.ReliableOrdered);
        }

        //Sends an error message to be processed by the client
        void SendErrorMessage(string message, string caption, NetIncomingMessage incMSG, NetServer svrServer)
        {
            NetOutgoingMessage outMSG = svrServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.ErrorMessage);
            outMSG.Write(message);
            outMSG.Write(caption);
            svrServer.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
        }

        void SendMapData(NetIncomingMessage incMSG, NetServer svrServer, Map svrMap, Player[] svrPlayer)
        {
            NetOutgoingMessage outMSG = svrServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.MapData);
            outMSG.Write(svrMap.Name);

            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    //ground
                    outMSG.Write(svrMap.Ground[x, y].tileX);
                    outMSG.Write(svrMap.Ground[x, y].tileY);
                    outMSG.Write(svrMap.Ground[x, y].tileW);
                    outMSG.Write(svrMap.Ground[x, y].tileH);
                    outMSG.Write(svrMap.Ground[x, y].Tileset);
                    outMSG.Write(svrMap.Ground[x, y].type);
                    outMSG.Write(svrMap.Ground[x, y].spawnNum);
                    //mask
                    outMSG.Write(svrMap.Mask[x, y].tileX);
                    outMSG.Write(svrMap.Mask[x, y].tileY);
                    outMSG.Write(svrMap.Mask[x, y].tileW);
                    outMSG.Write(svrMap.Mask[x, y].tileH);
                    outMSG.Write(svrMap.Mask[x, y].Tileset);
                    //fringe
                    outMSG.Write(svrMap.Fringe[x, y].tileX);
                    outMSG.Write(svrMap.Fringe[x, y].tileY);
                    outMSG.Write(svrMap.Fringe[x, y].tileW);
                    outMSG.Write(svrMap.Fringe[x, y].tileH);
                    outMSG.Write(svrMap.Fringe[x, y].Tileset);
                    //mask a
                    outMSG.Write(svrMap.MaskA[x, y].tileX);
                    outMSG.Write(svrMap.MaskA[x, y].tileY);
                    outMSG.Write(svrMap.MaskA[x, y].tileW);
                    outMSG.Write(svrMap.MaskA[x, y].tileH);
                    outMSG.Write(svrMap.MaskA[x, y].Tileset);
                    //fringe a
                    outMSG.Write(svrMap.FringeA[x, y].tileX);
                    outMSG.Write(svrMap.FringeA[x, y].tileY);
                    outMSG.Write(svrMap.FringeA[x, y].tileW);
                    outMSG.Write(svrMap.FringeA[x, y].tileH);
                    outMSG.Write(svrMap.FringeA[x, y].Tileset);
                }
            }
            svrServer.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
        }

        /// <summary>
        /// These methods handle all the data from incoming messages
        /// </summary>
        static bool AlreadyLogged(string name, Player[] svrPlayer)
        {
            if (name == null) return false;

            for (int i = 0; i < 5; i++)
            {
                if (svrPlayer[i].Name != null)
                {
                    string alname = svrPlayer[i].Name.ToLower();
                    string alother = name.ToLower();
                    if (alname == alother)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        static int OpenSlot(Player[] svrPlayer)
        {
            for (int i = 0; i < 5; i++)
            {
                if (svrPlayer[i].Name == null)
                {
                    return i;
                }
            }
            return 5;
        }

        static int GetPlayerConnection(NetIncomingMessage incMSG, Player[] svrPlayer)
        {
            for (int i = 0; i < 5; i++)
            {
                if (svrPlayer[i].Connection == incMSG.SenderConnection)
                {
                    return i;
                }
            }
            return 5;
        }

        static bool CheckPassword(string name, string pass)
        {
            XmlReader reader = XmlReader.Create("Players/" + name + ".xml");

            reader.ReadToFollowing("Password");
            string comparePass = reader.ReadElementContentAsString();
            reader.Close();

            if (pass == comparePass)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static bool AccountExist(string name)
        {
            if (Exists("Players/" + name + ".xml"))
            {
                return true;
            }
            return false;
        }

        void ClearSlot(NetConnection conn, Player[] svrPlayer)
        {
            for (int i = 0; i < 5; i++)
            {
                if (svrPlayer[i] != null && svrPlayer[i].Connection == conn)
                {
                    svrPlayer[i] = null;
                    svrPlayer[i] = new Player();
                    break;
                }
            }
        }

        void HandleStatusChange(NetIncomingMessage incMSG, NetServer svrServer, Player[] svrPlayer)
        {
            Console.WriteLine(incMSG.SenderConnection.ToString() + " status changed. " + incMSG.SenderConnection.Status);
            LogWriter.WriteLog(incMSG.SenderConnection.ToString() + " status changed. " + incMSG.SenderConnection.Status, "Server");
            if (incMSG.SenderConnection.Status == NetConnectionStatus.Disconnected || incMSG.SenderConnection.Status == NetConnectionStatus.Disconnecting)
            {
                Console.WriteLine("Disconnected clearing data...");
                LogWriter.WriteLog("Disconnected clearing data...", "Server");
                Console.WriteLine("Saving player...");
                LogWriter.WriteLog("Saving player...", "Server");
                SavePlayers(svrPlayer);
                ClearSlot(incMSG.SenderConnection, svrPlayer);
                SendUsers(incMSG, svrServer, svrPlayer);
            }
        }

        void SavePlayers(Player[] svrPlayer)
        {
            for (int i = 0; i < 5; i++)
            {
                if (svrPlayer[i].Name != null)
                {
                    svrPlayer[i].SavePlayerXML();
                }
            }
        }

        void CheckCommand(string msg, int index, Player[] svrPlayer, NetIncomingMessage incMSG, NetServer svrServer, Map[] svrMap)
        {
            //Make sure it has lenghth and isnt just 1 forwardslash
            if (msg.Length >= 6)
            {
                //Check for map command
                if (msg.Substring(1, 3) == "map")
                {
                    if (msg.Length >= 6)
                    {
                        int mapNum = ToInt32(msg.Substring(5, 1));
                        svrPlayer[index].Map = mapNum;
                        SendMapData(incMSG, svrServer, svrMap[mapNum], svrPlayer);
                        SendUsers(incMSG, svrServer, svrPlayer);
                        SendMapNpcs(incMSG, svrServer, svrMap[mapNum]);
                        Console.WriteLine("Command: " + msg + " Mapnum: " + mapNum);
                        return;
                    }
                }
            }

            if (msg.Length >= 9)
            {
                if (msg.Substring(1, 6) == "sprite")
                {
                    int spriteNum;
                    if (msg.Length == 10)
                    {
                        spriteNum = ToInt32(msg.Substring(8, 2));
                    }
                    else if (msg.Length == 11)
                    {
                        spriteNum = ToInt32(msg.Substring(8, 3));
                    }
                    else
                    {
                        spriteNum = ToInt32(msg.Substring(8, 1));
                    }
                    svrPlayer[index].Sprite = spriteNum;
                    SendUsers(incMSG, svrServer, svrPlayer);
                    Console.WriteLine("Command:" + msg + " Spritenum: " + spriteNum);
                    return;
                }
            }
        }
    }

    /// <summary>
    /// Packet headers
    /// </summary>
    public enum PacketTypes
    {
        Connection,
        Register,
        ErrorMessage,
        Login,
        UserData,
        ChatMessage,
        MapData,
        Users,
        MoveData,
        UpdateMoveData,
        UpdateDirection,
        DirData,
        NpcData,
        Npcs,
        MapNpc
    }
}
