using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;
using Gwen.Control;
using System.Drawing;
using static System.Environment;
using static System.Convert;

namespace Client.Classes
{
    class HandleData
    {
        public string ipAddress; //So we can change the port XML instead of directly in the code
        public string port;  //So we can change the port from XML and not directly in the code
        public int clientIndex; //The index of this client

        //This is where we process the 
        public void DataMessage(NetClient svrClient, Canvas svrCanvas, GUI svrGUI, Player[] svrPlayer, Map svrMap, ClientConfig cConfig, NPC[] svrNpc)
        {
            NetIncomingMessage incMSG;
            ipAddress = cConfig.ipAddress;
            port = cConfig.port;

            if ((incMSG = svrClient.ReadMessage()) != null)
            {
                switch (incMSG.MessageType)
                {
                    case NetIncomingMessageType.DiscoveryResponse:
                        HandleDiscoveryResponse(incMSG, svrClient);
                        break;

                    case NetIncomingMessageType.Data:
                        switch (incMSG.ReadByte())
                        {
                            case (byte)PacketTypes.Connection:
                                HandleConnectionData(incMSG, svrClient);
                                break;

                            case (byte)PacketTypes.ErrorMessage:
                                HandleErrorMessage(incMSG, svrClient, svrCanvas);
                                break;

                            case (byte)PacketTypes.Login:
                                HandleLoginData(incMSG, svrClient, svrCanvas, svrGUI);
                                break;

                            case (byte)PacketTypes.UserData:
                                clientIndex = incMSG.ReadInt32();
                                svrGUI.guiIndex = clientIndex;
                                Console.WriteLine("Client index: " + clientIndex);
                                HandlePlayerData(incMSG, svrClient, svrPlayer, clientIndex);
                                break;

                            case (byte)PacketTypes.ChatMessage:
                                HandleChatMessage(incMSG, svrGUI);
                                break;

                            case (byte)PacketTypes.MapData:
                                HandleMapData(svrClient, incMSG, svrMap);
                                Console.WriteLine("Recieved map data...");
                                break;

                            case (byte)PacketTypes.Users:
                                HandlePlayers(svrClient, incMSG, svrPlayer);
                                break;

                            case (byte)PacketTypes.UpdateMoveData:
                                HandleUpdateMoveData(incMSG, svrPlayer, clientIndex);
                                break;

                            case (byte)PacketTypes.DirData:
                                HandleDirectionData(incMSG, svrPlayer, clientIndex);
                                break;

                            case (byte)PacketTypes.Npcs:
                                HandleNpcs(incMSG, svrNpc);
                                Console.WriteLine("Recieved npc data...");
                                break;

                            case (byte)PacketTypes.MapNpc:
                                HandleMapNpcs(incMSG, svrMap);
                                break;

                            case (byte)PacketTypes.NpcData:
                                if (svrMap.Name != null)
                                {
                                    HandleNpcData(incMSG, svrMap);
                                }
                                break;
                        }
                        break;
                }
            }
            svrClient.Recycle(incMSG);
        }

        //Handle incoming NPC data
        void HandleNpcs(NetIncomingMessage incMSG, NPC[] svrNpc)
        {
            for (int i = 0; i < 10; i++)
            {
                if (svrNpc[i] != null)
                {
                    svrNpc[i].Name = incMSG.ReadString();
                    svrNpc[i].X = incMSG.ReadInt32();
                    svrNpc[i].Y = incMSG.ReadInt32();
                    svrNpc[i].Direction = incMSG.ReadInt32();
                    svrNpc[i].Sprite = incMSG.ReadInt32();
                    svrNpc[i].Step = incMSG.ReadInt32();
                    svrNpc[i].Owner = incMSG.ReadInt32();
                    svrNpc[i].Behavior = incMSG.ReadInt32();
                    svrNpc[i].SpawnTime = incMSG.ReadInt32();
                    svrNpc[i].isSpawned = incMSG.ReadBoolean();
                }
            }
        }

        //Handle incoming NPC data
        void HandleMapNpcs(NetIncomingMessage incMSG, Map svrMap)
        {
            for (int i = 0; i < 10; i++)
            {
                if (svrMap.mapNpc[i] != null)
                {
                    svrMap.mapNpc[i].Name = incMSG.ReadString();
                    svrMap.mapNpc[i].X = incMSG.ReadInt32();
                    svrMap.mapNpc[i].Y = incMSG.ReadInt32();
                    svrMap.mapNpc[i].Direction = incMSG.ReadInt32();
                    svrMap.mapNpc[i].Sprite = incMSG.ReadInt32();
                    svrMap.mapNpc[i].Step = incMSG.ReadInt32();
                    svrMap.mapNpc[i].Owner = incMSG.ReadInt32();
                    svrMap.mapNpc[i].Behavior = incMSG.ReadInt32();
                    svrMap.mapNpc[i].SpawnTime = incMSG.ReadInt32();
                    svrMap.mapNpc[i].isSpawned = incMSG.ReadBoolean();
                }
            }
        }

        //Handle incoming data for a single npc
        void HandleNpcData(NetIncomingMessage incMSG, Map svrMap)
        {
            int npcNum = incMSG.ReadInt32();

            svrMap.mapNpc[npcNum].Name = incMSG.ReadString();
            svrMap.mapNpc[npcNum].X = incMSG.ReadInt32();
            svrMap.mapNpc[npcNum].Y = incMSG.ReadInt32();
            svrMap.mapNpc[npcNum].Direction = incMSG.ReadInt32();
            svrMap.mapNpc[npcNum].Sprite = incMSG.ReadInt32();
            svrMap.mapNpc[npcNum].Step = incMSG.ReadInt32();
            svrMap.mapNpc[npcNum].Owner = incMSG.ReadInt32();
            svrMap.mapNpc[npcNum].Behavior = incMSG.ReadInt32();
            svrMap.mapNpc[npcNum].SpawnTime = incMSG.ReadInt32();
            svrMap.mapNpc[npcNum].isSpawned = incMSG.ReadBoolean();
        }

        //Handle player direction packet
        void HandleDirectionData(NetIncomingMessage incMSG, Player[] svrPlayer, int clientIndex)
        {
            int index = incMSG.ReadInt32();
            int direction = incMSG.ReadInt32();

            if (index == clientIndex) { return; }

            svrPlayer[index].Direction = direction;
        }

        //handle incoming movement data
        void HandleUpdateMoveData(NetIncomingMessage incMSG, Player[] svrPlayer, int clientIndex)
        {
            int index = incMSG.ReadInt32();
            int x = incMSG.ReadInt32();
            int y = incMSG.ReadInt32();
            int direction = incMSG.ReadInt32();
            int step = incMSG.ReadInt32();

            if (index == clientIndex) { return; }
            if (step == svrPlayer[index].Step) { return; }

            svrPlayer[index].tempX = x;
            svrPlayer[index].tempY = y;
            svrPlayer[index].tempDir = direction;
            svrPlayer[index].tempStep = step;
        }

        //Discovery response sent from the server
        void HandleDiscoveryResponse(NetIncomingMessage incMSG, NetClient svrClient)
        {
            Console.WriteLine("Found Server: " + incMSG.ReadString() + " @ " + incMSG.SenderEndPoint);
            NetOutgoingMessage outMSG = svrClient.CreateMessage();
            outMSG.Write((byte)PacketTypes.Connection);
            outMSG.Write("sabertooth");
            svrClient.Connect(ipAddress, ToInt32(port), outMSG);
        }

        //Assuring we are connected to the server
        void HandleConnectionData(NetIncomingMessage incMSG, NetClient svrClient)
        {
            if (svrClient.ServerConnection != null) { return; }
            Console.WriteLine("Connected to server!");
        }

        //Packet incoming for chat message
        void HandleChatMessage(NetIncomingMessage incMSG, GUI svrGUI)
        {
            string msg = incMSG.ReadString();
            int msgLength = msg.Length;
            int maxLength = 80;

            if (msgLength > maxLength)
            {
                string[] splitMsg = new string[2];
                int splitLength = msgLength - maxLength;
                splitMsg[0] = msg.Substring(0, maxLength);
                splitMsg[1] = msg.Substring(maxLength, splitLength);
                svrGUI.outputChat.AddRow(splitMsg[0]);
                svrGUI.outputChat.AddRow(splitMsg[1]);
            }
            else
            {
                svrGUI.outputChat.AddRow(msg);
            }
            svrGUI.outputChat.ScrollToBottom();
            svrGUI.outputChat.UnselectAll();
        }

        //Packet for succesful login, after this is done we get all the maps, npc, other players the whole nine yards
        void HandleLoginData(NetIncomingMessage incMSG, NetClient svrClient, Canvas svrCanvas, GUI svrGUI)
        {
            Console.WriteLine("Login successful!");
            svrCanvas.DeleteAllChildren();
            svrGUI.CreateDebugWindow(svrCanvas);
            svrGUI.CreateChatWindow(svrCanvas);
            svrGUI.AddText("Welcome to Sabertooth!");
        }

        //Player incoming data for the clients index
        void HandlePlayerData(NetIncomingMessage incMSG, NetClient svrClient, Player[] svrPlayer, int clientIndex)
        {
            svrPlayer[clientIndex].Name = incMSG.ReadString();
            svrPlayer[clientIndex].X = incMSG.ReadInt32();
            svrPlayer[clientIndex].Y = incMSG.ReadInt32();
            svrPlayer[clientIndex].Map = incMSG.ReadInt32();
            svrPlayer[clientIndex].Direction = incMSG.ReadInt32();
            svrPlayer[clientIndex].Sprite = incMSG.ReadInt32();
            svrPlayer[clientIndex].offsetX = 12;
            svrPlayer[clientIndex].offsetY = 9;
        }

        //Error message handler
        void HandleErrorMessage(NetIncomingMessage incMSG, NetClient svrClient, Canvas svrCanvas)
        {
            string msg = incMSG.ReadString();
            string caption = incMSG.ReadString();
            MessageBox msgBox = new MessageBox(svrCanvas, msg, caption);
            msgBox.Position(Gwen.Pos.Center);
        }

        //Handle the incoming map data whether it be logging in or changing maps.
        void HandleMapData(NetClient svrClient, NetIncomingMessage incMSG, Map svrMap)
        {
            svrMap.Name = incMSG.ReadString();

            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    svrMap.Ground[x, y] = new Tile();
                    svrMap.Mask[x, y] = new Tile();
                    svrMap.Fringe[x, y] = new Tile();
                    svrMap.MaskA[x, y] = new Tile();
                    svrMap.FringeA[x, y] = new Tile();

                    //ground
                    svrMap.Ground[x, y].tileX = incMSG.ReadInt32();
                    svrMap.Ground[x, y].tileY = incMSG.ReadInt32();
                    svrMap.Ground[x, y].tileW = incMSG.ReadInt32();
                    svrMap.Ground[x, y].tileH = incMSG.ReadInt32();
                    svrMap.Ground[x, y].Tileset = incMSG.ReadInt32();
                    svrMap.Ground[x, y].type = incMSG.ReadInt32();
                    svrMap.Ground[x, y].spawnNum = incMSG.ReadInt32();
                    //mask
                    svrMap.Mask[x, y].tileX = incMSG.ReadInt32();
                    svrMap.Mask[x, y].tileY = incMSG.ReadInt32();
                    svrMap.Mask[x, y].tileW = incMSG.ReadInt32();
                    svrMap.Mask[x, y].tileH = incMSG.ReadInt32();
                    svrMap.Mask[x, y].Tileset = incMSG.ReadInt32();
                    //fringe
                    svrMap.Fringe[x, y].tileX = incMSG.ReadInt32();
                    svrMap.Fringe[x, y].tileY = incMSG.ReadInt32();
                    svrMap.Fringe[x, y].tileW = incMSG.ReadInt32();
                    svrMap.Fringe[x, y].tileH = incMSG.ReadInt32();
                    svrMap.Fringe[x, y].Tileset = incMSG.ReadInt32();
                    //mask a
                    svrMap.MaskA[x, y].tileX = incMSG.ReadInt32();
                    svrMap.MaskA[x, y].tileY = incMSG.ReadInt32();
                    svrMap.MaskA[x, y].tileW = incMSG.ReadInt32();
                    svrMap.MaskA[x, y].tileH = incMSG.ReadInt32();
                    svrMap.MaskA[x, y].Tileset = incMSG.ReadInt32();
                    //fringe a
                    svrMap.FringeA[x, y].tileX = incMSG.ReadInt32();
                    svrMap.FringeA[x, y].tileY = incMSG.ReadInt32();
                    svrMap.FringeA[x, y].tileW = incMSG.ReadInt32();
                    svrMap.FringeA[x, y].tileH = incMSG.ReadInt32();
                    svrMap.FringeA[x, y].Tileset = incMSG.ReadInt32();
                }
            }
            svrMap.SaveMap();
        }

        //Handle the players on login, we just use the handleplayer for a single player or the client index this is all of the players currently connected
        void HandlePlayers(NetClient svrClient, NetIncomingMessage incMSG, Player[] svrPlayer)
        {
            for (int i = 0; i < 5; i++)
            {
                svrPlayer[i].Name = incMSG.ReadString();
                svrPlayer[i].X = incMSG.ReadInt32();
                svrPlayer[i].Y = incMSG.ReadInt32();
                svrPlayer[i].Map = incMSG.ReadInt32();
                svrPlayer[i].Direction = incMSG.ReadInt32();
                svrPlayer[i].Sprite = incMSG.ReadInt32();
                svrPlayer[i].offsetX = 12;
                svrPlayer[i].offsetY = 9;
            }
        }
    }

    //Packet headers in the form of enumerations
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
