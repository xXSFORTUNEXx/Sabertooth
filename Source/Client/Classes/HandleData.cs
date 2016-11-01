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
        public string s_IPAddress; //So we can change the port XML instead of directly in the code
        public string s_Port;  //So we can change the port from XML and not directly in the code
        public int c_Index; //The index of this client

        //This is where we process the 
        public void DataMessage(NetClient c_Client, Canvas c_Canvas, GUI c_GUI, Player[] c_Player, Map c_Map, 
            ClientConfig c_Config, NPC[] c_Npc, Item[] c_Item)
        {
            NetIncomingMessage incMSG;
            s_IPAddress = c_Config.ipAddress;
            s_Port = c_Config.port;

            if ((incMSG = c_Client.ReadMessage()) != null)
            {
                switch (incMSG.MessageType)
                {
                    case NetIncomingMessageType.DiscoveryResponse:
                        HandleDiscoveryResponse(incMSG, c_Client);
                        break;

                    case NetIncomingMessageType.Data:
                        switch (incMSG.ReadByte())
                        {
                            case (byte)PacketTypes.Connection:
                                HandleConnectionData(incMSG, c_Client);
                                break;

                            case (byte)PacketTypes.ErrorMessage:
                                HandleErrorMessage(incMSG, c_Client, c_Canvas);
                                break;

                            case (byte)PacketTypes.Login:
                                HandleLoginData(incMSG, c_Client, c_Canvas, c_GUI);
                                break;

                            case (byte)PacketTypes.UserData:
                                c_Index = incMSG.ReadInt32();
                                c_GUI.g_Index = c_Index;
                                HandlePlayerData(incMSG, c_Client, c_Player, c_Index);
                                break;

                            case (byte)PacketTypes.ChatMessage:
                                HandleChatMessage(incMSG, c_GUI);
                                break;

                            case (byte)PacketTypes.MapData:
                                HandleMapData(c_Client, incMSG, c_Map);
                                break;

                            case (byte)PacketTypes.Users:
                                HandlePlayers(c_Client, incMSG, c_Player);
                                break;

                            case (byte)PacketTypes.UpdateMoveData:
                                HandleUpdateMoveData(incMSG, c_Player, c_Index);
                                break;

                            case (byte)PacketTypes.DirData:
                                HandleDirectionData(incMSG, c_Player, c_Index);
                                break;

                            case (byte)PacketTypes.Npcs:
                                HandleNpcs(incMSG, c_Npc);
                                break;

                            case (byte)PacketTypes.MapNpc:
                                HandleMapNpcs(incMSG, c_Map);
                                break;

                            case (byte)PacketTypes.NpcData:
                                if (c_Map.Name != null)
                                {
                                    HandleNpcData(incMSG, c_Map);
                                }
                                break;

                            case (byte)PacketTypes.HealthData:
                                HandleHealthData(incMSG, c_Player);
                                break;

                            case (byte)PacketTypes.VitalLoss:
                                HandleVitalData(incMSG, c_Player);
                                break;

                            case (byte)PacketTypes.ItemData:
                                HandleItemData(incMSG, c_Item);
                                break;

                            case (byte)PacketTypes.Items:
                                HandleItems(incMSG, c_Item);
                                break;
                        }
                        break;
                }
            }
            c_Client.Recycle(incMSG);
        }

        //Handles data for incoming items
        void HandleItems(NetIncomingMessage incMSG, Item[] c_Item)
        {
            for (int i = 0; i < 50; i++)
            {
                if (c_Item[i] != null)
                {
                    c_Item[i].Name = incMSG.ReadString();
                    c_Item[i].Sprite = incMSG.ReadInt32();
                    c_Item[i].Damage = incMSG.ReadInt32();
                    c_Item[i].Armor = incMSG.ReadInt32();
                    c_Item[i].Type = incMSG.ReadInt32();
                    c_Item[i].HealthRestore = incMSG.ReadInt32();
                    c_Item[i].HungerRestore = incMSG.ReadInt32();
                    c_Item[i].HydrateRestore = incMSG.ReadInt32();
                    c_Item[i].Strength = incMSG.ReadInt32();
                    c_Item[i].Agility = incMSG.ReadInt32();
                    c_Item[i].Endurance = incMSG.ReadInt32();
                    c_Item[i].Stamina = incMSG.ReadInt32();
                }
            }

            Console.WriteLine("Items data received from server! IP: " + incMSG.SenderConnection);
        }

        //Handles data for incoming items
        void HandleItemData(NetIncomingMessage incMSG, Item[] c_Item)
        {
            int index = incMSG.ReadInt32();

            c_Item[index].Name = incMSG.ReadString();
            c_Item[index].Sprite = incMSG.ReadInt32();
            c_Item[index].Damage = incMSG.ReadInt32();
            c_Item[index].Armor = incMSG.ReadInt32();
            c_Item[index].Type = incMSG.ReadInt32();
            c_Item[index].HealthRestore = incMSG.ReadInt32();
            c_Item[index].HungerRestore = incMSG.ReadInt32();
            c_Item[index].HydrateRestore = incMSG.ReadInt32();
            c_Item[index].Strength = incMSG.ReadInt32();
            c_Item[index].Agility = incMSG.ReadInt32();
            c_Item[index].Endurance = incMSG.ReadInt32();
            c_Item[index].Stamina = incMSG.ReadInt32();

            Console.WriteLine("Item data received from server! Index: " + index + " IP: " + incMSG.SenderConnection);
        }

        //Handle incoming NPC data
        void HandleNpcs(NetIncomingMessage incMSG, NPC[] c_Npc)
        {
            for (int i = 0; i < 10; i++)
            {
                if (c_Npc[i] != null)
                {
                    c_Npc[i].Name = incMSG.ReadString();
                    c_Npc[i].X = incMSG.ReadInt32();
                    c_Npc[i].Y = incMSG.ReadInt32();
                    c_Npc[i].Direction = incMSG.ReadInt32();
                    c_Npc[i].Sprite = incMSG.ReadInt32();
                    c_Npc[i].Step = incMSG.ReadInt32();
                    c_Npc[i].Owner = incMSG.ReadInt32();
                    c_Npc[i].Behavior = incMSG.ReadInt32();
                    c_Npc[i].SpawnTime = incMSG.ReadInt32();
                    c_Npc[i].isSpawned = incMSG.ReadBoolean();
                }
            }
            Console.WriteLine("NPC data received from server! IP: " + incMSG.SenderConnection);
        }

        //Handle incoming NPC data
        void HandleMapNpcs(NetIncomingMessage incMSG, Map c_Map)
        {
            for (int i = 0; i < 10; i++)
            {
                if (c_Map.mapNpc[i] != null)
                {
                    c_Map.mapNpc[i].Name = incMSG.ReadString();
                    c_Map.mapNpc[i].X = incMSG.ReadInt32();
                    c_Map.mapNpc[i].Y = incMSG.ReadInt32();
                    c_Map.mapNpc[i].Direction = incMSG.ReadInt32();
                    c_Map.mapNpc[i].Sprite = incMSG.ReadInt32();
                    c_Map.mapNpc[i].Step = incMSG.ReadInt32();
                    c_Map.mapNpc[i].Owner = incMSG.ReadInt32();
                    c_Map.mapNpc[i].Behavior = incMSG.ReadInt32();
                    c_Map.mapNpc[i].SpawnTime = incMSG.ReadInt32();
                    c_Map.mapNpc[i].isSpawned = incMSG.ReadBoolean();
                }
            }
            Console.WriteLine("Map NPC data received from server! IP: " + incMSG.SenderConnection);
        }

        //Handle incoming data for a single npc
        void HandleNpcData(NetIncomingMessage incMSG, Map c_Map)
        {
            int npcNum = incMSG.ReadInt32();

            c_Map.mapNpc[npcNum].Name = incMSG.ReadString();
            c_Map.mapNpc[npcNum].X = incMSG.ReadInt32();
            c_Map.mapNpc[npcNum].Y = incMSG.ReadInt32();
            c_Map.mapNpc[npcNum].Direction = incMSG.ReadInt32();
            c_Map.mapNpc[npcNum].Sprite = incMSG.ReadInt32();
            c_Map.mapNpc[npcNum].Step = incMSG.ReadInt32();
            c_Map.mapNpc[npcNum].Owner = incMSG.ReadInt32();
            c_Map.mapNpc[npcNum].Behavior = incMSG.ReadInt32();
            c_Map.mapNpc[npcNum].SpawnTime = incMSG.ReadInt32();
            c_Map.mapNpc[npcNum].isSpawned = incMSG.ReadBoolean();

            Console.WriteLine("NPC data received from server! Index: " + npcNum + " IP: " + incMSG.SenderConnection);
        }

        //Handle player direction packet
        void HandleDirectionData(NetIncomingMessage incMSG, Player[] c_Player, int clientIndex)
        {
            int index = incMSG.ReadInt32();
            int direction = incMSG.ReadInt32();

            Console.WriteLine("Direction data received from server! Index: " + index + " IP: " + incMSG.SenderConnection);

            if (index == clientIndex) { return; }

            c_Player[index].Direction = direction;
        }

        //handle incoming movement data
        void HandleUpdateMoveData(NetIncomingMessage incMSG, Player[] c_Player, int clientIndex)
        {
            int index = incMSG.ReadInt32();
            int x = incMSG.ReadInt32();
            int y = incMSG.ReadInt32();
            int direction = incMSG.ReadInt32();
            int step = incMSG.ReadInt32();

            Console.WriteLine("Move data recieved from server! Index: " + index + " IP: " + incMSG.SenderConnection);

            if (index == clientIndex) { return; }
            if (step == c_Player[index].Step) { return; }

            c_Player[index].tempX = x;
            c_Player[index].tempY = y;
            c_Player[index].tempDir = direction;
            c_Player[index].tempStep = step;
        }

        //Discovery response sent from the server
        void HandleDiscoveryResponse(NetIncomingMessage incMSG, NetClient c_Client)
        {
            Console.WriteLine("Found Server: " + incMSG.ReadString() + " @ " + incMSG.SenderEndPoint);
            NetOutgoingMessage outMSG = c_Client.CreateMessage();
            outMSG.Write((byte)PacketTypes.Connection);
            outMSG.Write("sabertooth");
            c_Client.Connect(s_IPAddress, ToInt32(s_Port), outMSG);
        }

        //Assuring we are connected to the server
        void HandleConnectionData(NetIncomingMessage incMSG, NetClient c_Client)
        {
            if (c_Client.ServerConnection != null) { return; }
            Console.WriteLine("Connected to server!");
        }

        //Packet incoming for chat message
        void HandleChatMessage(NetIncomingMessage incMSG, GUI c_GUI)
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
                c_GUI.outputChat.AddRow(splitMsg[0]);
                c_GUI.outputChat.AddRow(splitMsg[1]);
            }
            else
            {
                c_GUI.outputChat.AddRow(msg);
            }
            c_GUI.outputChat.ScrollToBottom();
            c_GUI.outputChat.UnselectAll();
            Console.WriteLine("Chat data receievd from server! IP: " + incMSG.SenderConnection);
        }

        //Packet for succesful login, after this is done we get all the maps, npc, other players the whole nine yards
        void HandleLoginData(NetIncomingMessage incMSG, NetClient c_Client, Canvas c_Canvas, GUI c_GUI)
        {
            Console.WriteLine("Login successful! IP: " + incMSG.SenderConnection);
            c_Canvas.DeleteAllChildren();
            c_GUI.CreateDebugWindow(c_Canvas);
            c_GUI.CreateChatWindow(c_Canvas);
            c_GUI.AddText("Welcome to Sabertooth!");
        }

        //Handles incoming vital data
        void HandleVitalData(NetIncomingMessage incMSG, Player[] c_Player)
        {
            int index = incMSG.ReadInt32();
            string vitalName = incMSG.ReadString();
            int vital = incMSG.ReadInt32();

            if (vitalName == "food") { c_Player[index].Hunger = vital; }
            if (vitalName == "water") { c_Player[index].Hydration = vital; }

            Console.WriteLine("Vital data received from server! Index: " + index + " Vital: " + vitalName + " IP: " + incMSG.SenderConnection);
        }

        //Handles incoming health data
        void HandleHealthData(NetIncomingMessage incMSG, Player[] c_Player)
        {
            int index = incMSG.ReadInt32();
            int health = incMSG.ReadInt32();

            c_Player[index].Health = health;
            Console.WriteLine("Health data received from server! Index:" + index + " Amount: " + health + " IP: " + incMSG.SenderConnection);
        }

        //Player incoming data for the clients index
        void HandlePlayerData(NetIncomingMessage incMSG, NetClient c_Client, Player[] c_Player, int clientIndex)
        {
            c_Player[clientIndex].Name = incMSG.ReadString();
            c_Player[clientIndex].X = incMSG.ReadInt32();
            c_Player[clientIndex].Y = incMSG.ReadInt32();
            c_Player[clientIndex].Map = incMSG.ReadInt32();
            c_Player[clientIndex].Direction = incMSG.ReadInt32();
            c_Player[clientIndex].Sprite = incMSG.ReadInt32();
            c_Player[clientIndex].Level = incMSG.ReadInt32();
            c_Player[clientIndex].Health = incMSG.ReadInt32();
            c_Player[clientIndex].Hunger = incMSG.ReadInt32();
            c_Player[clientIndex].maxHealth = incMSG.ReadInt32();
            c_Player[clientIndex].Hydration = incMSG.ReadInt32();
            c_Player[clientIndex].Experience = incMSG.ReadInt32();
            c_Player[clientIndex].Money = incMSG.ReadInt32();
            c_Player[clientIndex].Armor = incMSG.ReadInt32();
            c_Player[clientIndex].Strength = incMSG.ReadInt32();
            c_Player[clientIndex].Agility = incMSG.ReadInt32();
            c_Player[clientIndex].Endurance = incMSG.ReadInt32();
            c_Player[clientIndex].Stamina = incMSG.ReadInt32();
            c_Player[clientIndex].offsetX = 12;
            c_Player[clientIndex].offsetY = 9;

            Console.WriteLine("Player data received from server! Index: " + clientIndex + " Account Name: " + c_Player[clientIndex].Name + " IP: " + incMSG.SenderConnection);
        }

        //Handle the players on login, we just use the handleplayer for a single player or the client index this is all of the players currently connected
        void HandlePlayers(NetClient c_Client, NetIncomingMessage incMSG, Player[] c_Player)
        {
            for (int i = 0; i < 5; i++)
            {
                c_Player[i].Name = incMSG.ReadString();
                c_Player[i].X = incMSG.ReadInt32();
                c_Player[i].Y = incMSG.ReadInt32();
                c_Player[i].Map = incMSG.ReadInt32();
                c_Player[i].Direction = incMSG.ReadInt32();
                c_Player[i].Sprite = incMSG.ReadInt32();
                c_Player[i].Level = incMSG.ReadInt32();
                c_Player[i].Health = incMSG.ReadInt32();
                c_Player[i].maxHealth = incMSG.ReadInt32();
                c_Player[i].Hunger = incMSG.ReadInt32();
                c_Player[i].Hydration = incMSG.ReadInt32();
                c_Player[i].Experience = incMSG.ReadInt32();
                c_Player[i].Money = incMSG.ReadInt32();
                c_Player[i].Armor = incMSG.ReadInt32();
                c_Player[i].Strength = incMSG.ReadInt32();
                c_Player[i].Agility = incMSG.ReadInt32();
                c_Player[i].Endurance = incMSG.ReadInt32();
                c_Player[i].Stamina = incMSG.ReadInt32();
                c_Player[i].offsetX = 12;
                c_Player[i].offsetY = 9;
            }
            Console.WriteLine("Player data received from server! Index: All IP: " + incMSG.SenderConnection);
        }

        //Error message handler
        void HandleErrorMessage(NetIncomingMessage incMSG, NetClient c_Client, Canvas c_Canvas)
        {
            string msg = incMSG.ReadString();
            string caption = incMSG.ReadString();
            MessageBox msgBox = new MessageBox(c_Canvas, msg, caption);
            msgBox.Position(Gwen.Pos.Center);
            Console.WriteLine("Error message data received from server! IP: " + incMSG.SenderConnection);
        }

        //Handle the incoming map data whether it be logging in or changing maps.
        void HandleMapData(NetClient c_Client, NetIncomingMessage incMSG, Map c_Map)
        {
            c_Map.Name = incMSG.ReadString();

            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    c_Map.Ground[x, y] = new Tile();
                    c_Map.Mask[x, y] = new Tile();
                    c_Map.Fringe[x, y] = new Tile();
                    c_Map.MaskA[x, y] = new Tile();
                    c_Map.FringeA[x, y] = new Tile();

                    //ground
                    c_Map.Ground[x, y].tileX = incMSG.ReadInt32();
                    c_Map.Ground[x, y].tileY = incMSG.ReadInt32();
                    c_Map.Ground[x, y].tileW = incMSG.ReadInt32();
                    c_Map.Ground[x, y].tileH = incMSG.ReadInt32();
                    c_Map.Ground[x, y].Tileset = incMSG.ReadInt32();
                    c_Map.Ground[x, y].type = incMSG.ReadInt32();
                    c_Map.Ground[x, y].spawnNum = incMSG.ReadInt32();
                    //mask
                    c_Map.Mask[x, y].tileX = incMSG.ReadInt32();
                    c_Map.Mask[x, y].tileY = incMSG.ReadInt32();
                    c_Map.Mask[x, y].tileW = incMSG.ReadInt32();
                    c_Map.Mask[x, y].tileH = incMSG.ReadInt32();
                    c_Map.Mask[x, y].Tileset = incMSG.ReadInt32();
                    //fringe
                    c_Map.Fringe[x, y].tileX = incMSG.ReadInt32();
                    c_Map.Fringe[x, y].tileY = incMSG.ReadInt32();
                    c_Map.Fringe[x, y].tileW = incMSG.ReadInt32();
                    c_Map.Fringe[x, y].tileH = incMSG.ReadInt32();
                    c_Map.Fringe[x, y].Tileset = incMSG.ReadInt32();
                    //mask a
                    c_Map.MaskA[x, y].tileX = incMSG.ReadInt32();
                    c_Map.MaskA[x, y].tileY = incMSG.ReadInt32();
                    c_Map.MaskA[x, y].tileW = incMSG.ReadInt32();
                    c_Map.MaskA[x, y].tileH = incMSG.ReadInt32();
                    c_Map.MaskA[x, y].Tileset = incMSG.ReadInt32();
                    //fringe a
                    c_Map.FringeA[x, y].tileX = incMSG.ReadInt32();
                    c_Map.FringeA[x, y].tileY = incMSG.ReadInt32();
                    c_Map.FringeA[x, y].tileW = incMSG.ReadInt32();
                    c_Map.FringeA[x, y].tileH = incMSG.ReadInt32();
                    c_Map.FringeA[x, y].Tileset = incMSG.ReadInt32();
                }
            }
            c_Map.SaveMap();
            Console.WriteLine("Map data received from server! IP: " + incMSG.SenderConnection);
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
        MapNpc,
        HealthData,
        VitalLoss,
        ItemData,
        Items
    }
}
