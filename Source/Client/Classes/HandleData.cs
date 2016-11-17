using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            ClientConfig c_Config, NPC[] c_Npc, Item[] c_Item, Projectile[] c_Proj)
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

                            case (byte)PacketTypes.PlayerData:
                                c_Index = incMSG.ReadInt32();
                                c_GUI.g_Index = c_Index;
                                HandlePlayerData(incMSG, c_Client, c_Player, c_Index);
                                break;

                            case (byte)PacketTypes.ChatMessage:
                                HandleChatMessage(incMSG, c_GUI);
                                break;

                            case (byte)PacketTypes.MapData:
                                HandleMapData(c_Client, incMSG, c_Map, c_GUI, c_Canvas);
                                break;

                            case (byte)PacketTypes.Players:
                                HandlePlayers(c_Client, incMSG, c_Player);
                                break;

                            case (byte)PacketTypes.UpdateMoveData:
                                HandleUpdateMoveData(incMSG, c_Player, c_Index);
                                break;

                            case (byte)PacketTypes.DirData:
                                HandleUpdateDirectionData(incMSG, c_Player, c_Index);
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

                            case (byte)PacketTypes.Shutdown:
                                HandleServerShutdown(c_Client, c_Canvas);
                                break;

                            case (byte)PacketTypes.ProjData:
                                HandleProjData(incMSG, c_Proj);
                                break;

                            case (byte)PacketTypes.Projectiles:
                                HandleProjectiles(incMSG, c_Proj);
                                break;

                            case (byte)PacketTypes.UpdateAmmo:
                                HandleUpdateAmmo(incMSG, c_Player);
                                break;

                            case (byte)PacketTypes.CreateProj:
                                HandleCreateProjectile(incMSG, c_Player, c_Map);
                                break;

                            case (byte)PacketTypes.ClearProj:
                                HandleClearProjectile(incMSG, c_Player, c_Map);
                                break;
                        }
                        break;
                }
            }
            c_Client.Recycle(incMSG);
        }

        //Handle the server shutting down via commands
        void HandleServerShutdown(NetClient c_Client, Canvas c_Canvas)
        {
            c_Canvas.Dispose();
            c_Client.Shutdown("Disconnect");
            Exit(0);
        }

        //Clear the projectile
        void HandleClearProjectile(NetIncomingMessage incMSG, Player[] c_Player, Map c_Map)
        {
            string mapName = incMSG.ReadString();
            if (mapName != c_Map.Name) { return; }

            int slot = incMSG.ReadVariableInt32();
            c_Map.mapProj[slot] = null;
        }

        //Create a projectile
        void HandleCreateProjectile(NetIncomingMessage incMSG, Player[] c_Player, Map c_Map)
        {
            int slot = incMSG.ReadVariableInt32();
            string mapName = incMSG.ReadString();
            
            if (mapName != c_Map.Name) { return; }

            c_Map.mapProj[slot] = new MapProj();

            c_Map.mapProj[slot].Name = incMSG.ReadString();
            c_Map.mapProj[slot].X = incMSG.ReadVariableInt32();
            c_Map.mapProj[slot].Y = incMSG.ReadVariableInt32();
            c_Map.mapProj[slot].Direction = incMSG.ReadVariableInt32();
            c_Map.mapProj[slot].Speed = incMSG.ReadVariableInt32();
            c_Map.mapProj[slot].Owner = incMSG.ReadVariableInt32();
            c_Map.mapProj[slot].Sprite = incMSG.ReadVariableInt32();
            c_Map.mapProj[slot].Type = incMSG.ReadVariableInt32();
        }

        //Update clip and remaining ammo
        void HandleUpdateAmmo(NetIncomingMessage incMSG, Player[] c_Player)
        {
            int index = incMSG.ReadVariableInt32();

            c_Player[index].mainWeapon.Clip = incMSG.ReadVariableInt32();
            c_Player[index].PistolAmmo = incMSG.ReadVariableInt32();
            c_Player[index].AssaultAmmo = incMSG.ReadVariableInt32();
            c_Player[index].RocketAmmo = incMSG.ReadVariableInt32();
            c_Player[index].GrenadeAmmo = incMSG.ReadVariableInt32();
        }

        //Update all projectiles in the array
        void HandleProjectiles(NetIncomingMessage incMSG, Projectile[] c_Proj)
        {
            for (int i = 0; i < 10; i++)
            {
                if (c_Proj[i] != null)
                {
                    c_Proj[i].Name = incMSG.ReadString();
                    c_Proj[i].Damage = incMSG.ReadVariableInt32();
                    c_Proj[i].Range = incMSG.ReadVariableInt32();
                    c_Proj[i].Sprite = incMSG.ReadVariableInt32();
                    c_Proj[i].Type = incMSG.ReadVariableInt32();
                    c_Proj[i].Speed = incMSG.ReadVariableInt32();
                }
            }
        }

        //Handle one new piece of the projectile array
        void HandleProjData(NetIncomingMessage incMSG, Projectile[] c_Proj)
        {
            int index = incMSG.ReadVariableInt32();

            c_Proj[index].Name = incMSG.ReadString();
            c_Proj[index].Damage = incMSG.ReadVariableInt32();
            c_Proj[index].Range = incMSG.ReadVariableInt32();
            c_Proj[index].Sprite = incMSG.ReadVariableInt32();
            c_Proj[index].Type = incMSG.ReadVariableInt32();
            c_Proj[index].Speed = incMSG.ReadVariableInt32();
        }

        //Handles data for incoming items
        void HandleItems(NetIncomingMessage incMSG, Item[] c_Item)
        {
            for (int i = 0; i < 50; i++)
            {
                if (c_Item[i] != null)
                {
                    c_Item[i].Name = incMSG.ReadString();
                    c_Item[i].Sprite = incMSG.ReadVariableInt32();
                    c_Item[i].Damage = incMSG.ReadVariableInt32();
                    c_Item[i].Armor = incMSG.ReadVariableInt32();
                    c_Item[i].Type = incMSG.ReadVariableInt32();
                    c_Item[i].HealthRestore = incMSG.ReadVariableInt32();
                    c_Item[i].HungerRestore = incMSG.ReadVariableInt32();
                    c_Item[i].HydrateRestore = incMSG.ReadVariableInt32();
                    c_Item[i].Strength = incMSG.ReadVariableInt32();
                    c_Item[i].Agility = incMSG.ReadVariableInt32();
                    c_Item[i].Endurance = incMSG.ReadVariableInt32();
                    c_Item[i].Stamina = incMSG.ReadVariableInt32();
                    c_Item[i].Clip = incMSG.ReadVariableInt32();
                    c_Item[i].maxClip = incMSG.ReadVariableInt32();
                    c_Item[i].ammoType = incMSG.ReadVariableInt32();
                }
            }

            Console.WriteLine("Items data received from server! IP: " + incMSG.SenderConnection);
        }

        //Handles data for incoming items
        void HandleItemData(NetIncomingMessage incMSG, Item[] c_Item)
        {
            int index = incMSG.ReadVariableInt32();

            c_Item[index].Name = incMSG.ReadString();
            c_Item[index].Sprite = incMSG.ReadVariableInt32();
            c_Item[index].Damage = incMSG.ReadVariableInt32();
            c_Item[index].Armor = incMSG.ReadVariableInt32();
            c_Item[index].Type = incMSG.ReadVariableInt32();
            c_Item[index].HealthRestore = incMSG.ReadVariableInt32();
            c_Item[index].HungerRestore = incMSG.ReadVariableInt32();
            c_Item[index].HydrateRestore = incMSG.ReadVariableInt32();
            c_Item[index].Strength = incMSG.ReadVariableInt32();
            c_Item[index].Agility = incMSG.ReadVariableInt32();
            c_Item[index].Endurance = incMSG.ReadVariableInt32();
            c_Item[index].Stamina = incMSG.ReadVariableInt32();
            c_Item[index].Clip = incMSG.ReadVariableInt32();
            c_Item[index].maxClip = incMSG.ReadVariableInt32();
            c_Item[index].ammoType = incMSG.ReadVariableInt32();

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
                    c_Npc[i].X = incMSG.ReadVariableInt32();
                    c_Npc[i].Y = incMSG.ReadVariableInt32();
                    c_Npc[i].Direction = incMSG.ReadVariableInt32();
                    c_Npc[i].Sprite = incMSG.ReadVariableInt32();
                    c_Npc[i].Step = incMSG.ReadVariableInt32();
                    c_Npc[i].Owner = incMSG.ReadVariableInt32();
                    c_Npc[i].Behavior = incMSG.ReadVariableInt32();
                    c_Npc[i].SpawnTime = incMSG.ReadVariableInt32();
                    c_Npc[i].Health = incMSG.ReadVariableInt32();
                    c_Npc[i].maxHealth = incMSG.ReadVariableInt32();
                    c_Npc[i].Damage = incMSG.ReadVariableInt32();
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
                    c_Map.mapNpc[i].X = incMSG.ReadVariableInt32();
                    c_Map.mapNpc[i].Y = incMSG.ReadVariableInt32();
                    c_Map.mapNpc[i].Direction = incMSG.ReadVariableInt32();
                    c_Map.mapNpc[i].Sprite = incMSG.ReadVariableInt32();
                    c_Map.mapNpc[i].Step = incMSG.ReadVariableInt32();
                    c_Map.mapNpc[i].Owner = incMSG.ReadVariableInt32();
                    c_Map.mapNpc[i].Behavior = incMSG.ReadVariableInt32();
                    c_Map.mapNpc[i].SpawnTime = incMSG.ReadVariableInt32();
                    c_Map.mapNpc[i].Health = incMSG.ReadVariableInt32();
                    c_Map.mapNpc[i].maxHealth = incMSG.ReadVariableInt32();
                    c_Map.mapNpc[i].Damage = incMSG.ReadVariableInt32();
                    c_Map.mapNpc[i].isSpawned = incMSG.ReadBoolean();
                }
            }
            Console.WriteLine("Map NPC data received from server! IP: " + incMSG.SenderConnection);
        }

        //Handle incoming data for a single npc
        void HandleNpcData(NetIncomingMessage incMSG, Map c_Map)
        {
            int npcNum = incMSG.ReadVariableInt32();

            c_Map.mapNpc[npcNum].Name = incMSG.ReadString();
            c_Map.mapNpc[npcNum].X = incMSG.ReadVariableInt32();
            c_Map.mapNpc[npcNum].Y = incMSG.ReadVariableInt32();
            c_Map.mapNpc[npcNum].Direction = incMSG.ReadVariableInt32();
            c_Map.mapNpc[npcNum].Sprite = incMSG.ReadVariableInt32();
            c_Map.mapNpc[npcNum].Step = incMSG.ReadVariableInt32();
            c_Map.mapNpc[npcNum].Owner = incMSG.ReadVariableInt32();
            c_Map.mapNpc[npcNum].Behavior = incMSG.ReadVariableInt32();
            c_Map.mapNpc[npcNum].SpawnTime = incMSG.ReadVariableInt32();
            c_Map.mapNpc[npcNum].Health = incMSG.ReadVariableInt32();
            c_Map.mapNpc[npcNum].maxHealth = incMSG.ReadVariableInt32();
            c_Map.mapNpc[npcNum].Damage = incMSG.ReadVariableInt32();
            c_Map.mapNpc[npcNum].isSpawned = incMSG.ReadBoolean();
            Console.WriteLine("NPC data received from server! Index: " + npcNum + " IP: " + incMSG.SenderConnection);
        }

        //Handle player direction packet
        void HandleUpdateDirectionData(NetIncomingMessage incMSG, Player[] c_Player, int clientIndex)
        {
            int index = incMSG.ReadVariableInt32();
            int direction = incMSG.ReadVariableInt32();
            int aimdirection = incMSG.ReadVariableInt32();

            Console.WriteLine("Direction data received from server! Index: " + index + " IP: " + incMSG.SenderConnection);

            if (index == clientIndex) { return; }

            c_Player[index].Direction = direction;
            c_Player[index].AimDirection = aimdirection;
        }

        //handle incoming movement data
        void HandleUpdateMoveData(NetIncomingMessage incMSG, Player[] c_Player, int clientIndex)
        {
            int index = incMSG.ReadVariableInt32();
            int x = incMSG.ReadVariableInt32();
            int y = incMSG.ReadVariableInt32();
            int direction = incMSG.ReadVariableInt32();
            int aimdirection = incMSG.ReadVariableInt32();
            int step = incMSG.ReadVariableInt32();

            Console.WriteLine("Move data recieved from server! Index: " + index + " IP: " + incMSG.SenderConnection);

            if (index == clientIndex) { return; }
            if (step == c_Player[index].Step) { return; }

            c_Player[index].tempX = x;
            c_Player[index].tempY = y;
            c_Player[index].tempDir = direction;
            c_Player[index].tempaimDir = aimdirection; 
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
            c_GUI.CreateLoadingWindow(c_Canvas);
        }

        //Handles incoming vital data
        void HandleVitalData(NetIncomingMessage incMSG, Player[] c_Player)
        {
            int index = incMSG.ReadVariableInt32();
            string vitalName = incMSG.ReadString();
            int vital = incMSG.ReadVariableInt32();

            if (vitalName == "food") { c_Player[index].Hunger = vital; }
            if (vitalName == "water") { c_Player[index].Hydration = vital; }

            Console.WriteLine("Vital data received from server! Index: " + index + " Vital: " + vitalName +  "Amount: " + vital + " IP: " + incMSG.SenderConnection);
        }

        //Handles incoming health data
        void HandleHealthData(NetIncomingMessage incMSG, Player[] c_Player)
        {
            int index = incMSG.ReadVariableInt32();
            int health = incMSG.ReadVariableInt32();

            c_Player[index].Health = health;
            Console.WriteLine("Health data received from server! Index:" + index + " Amount: " + health + " IP: " + incMSG.SenderConnection);
        }

        //Player incoming data for the clients index
        void HandlePlayerData(NetIncomingMessage incMSG, NetClient c_Client, Player[] c_Player, int clientIndex)
        {
            c_Player[clientIndex].Name = incMSG.ReadString();
            c_Player[clientIndex].X = incMSG.ReadVariableInt32();
            c_Player[clientIndex].Y = incMSG.ReadVariableInt32();
            c_Player[clientIndex].Map = incMSG.ReadVariableInt32();
            c_Player[clientIndex].Direction = incMSG.ReadVariableInt32();
            c_Player[clientIndex].AimDirection = incMSG.ReadVariableInt32();
            c_Player[clientIndex].Sprite = incMSG.ReadVariableInt32();
            c_Player[clientIndex].Level = incMSG.ReadVariableInt32();
            c_Player[clientIndex].Health = incMSG.ReadVariableInt32();
            c_Player[clientIndex].Hunger = incMSG.ReadVariableInt32();
            c_Player[clientIndex].maxHealth = incMSG.ReadVariableInt32();
            c_Player[clientIndex].Hydration = incMSG.ReadVariableInt32();
            c_Player[clientIndex].Experience = incMSG.ReadVariableInt32();
            c_Player[clientIndex].Money = incMSG.ReadVariableInt32();
            c_Player[clientIndex].Armor = incMSG.ReadVariableInt32();
            c_Player[clientIndex].Strength = incMSG.ReadVariableInt32();
            c_Player[clientIndex].Agility = incMSG.ReadVariableInt32();
            c_Player[clientIndex].Endurance = incMSG.ReadVariableInt32();
            c_Player[clientIndex].Stamina = incMSG.ReadVariableInt32();
            c_Player[clientIndex].PistolAmmo = incMSG.ReadVariableInt32();
            c_Player[clientIndex].AssaultAmmo = incMSG.ReadVariableInt32();
            c_Player[clientIndex].RocketAmmo = incMSG.ReadVariableInt32();
            c_Player[clientIndex].GrenadeAmmo = incMSG.ReadVariableInt32();
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
                c_Player[i].X = incMSG.ReadVariableInt32();
                c_Player[i].Y = incMSG.ReadVariableInt32();
                c_Player[i].Map = incMSG.ReadVariableInt32();
                c_Player[i].Direction = incMSG.ReadVariableInt32();
                c_Player[i].AimDirection = incMSG.ReadVariableInt32();
                c_Player[i].Sprite = incMSG.ReadVariableInt32();
                c_Player[i].Level = incMSG.ReadVariableInt32();
                c_Player[i].Health = incMSG.ReadVariableInt32();
                c_Player[i].maxHealth = incMSG.ReadVariableInt32();
                c_Player[i].Hunger = incMSG.ReadVariableInt32();
                c_Player[i].Hydration = incMSG.ReadVariableInt32();
                c_Player[i].Experience = incMSG.ReadVariableInt32();
                c_Player[i].Money = incMSG.ReadVariableInt32();
                c_Player[i].Armor = incMSG.ReadVariableInt32();
                c_Player[i].Strength = incMSG.ReadVariableInt32();
                c_Player[i].Agility = incMSG.ReadVariableInt32();
                c_Player[i].Endurance = incMSG.ReadVariableInt32();
                c_Player[i].Stamina = incMSG.ReadVariableInt32();
                c_Player[i].PistolAmmo = incMSG.ReadVariableInt32();
                c_Player[i].AssaultAmmo = incMSG.ReadVariableInt32();
                c_Player[i].RocketAmmo = incMSG.ReadVariableInt32();
                c_Player[i].GrenadeAmmo = incMSG.ReadVariableInt32();
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
        void HandleMapData(NetClient c_Client, NetIncomingMessage incMSG, Map c_Map, GUI c_GUI, Canvas c_Canvas)
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
                    c_Map.Ground[x, y].tileX = incMSG.ReadVariableInt32();
                    c_Map.Ground[x, y].tileY = incMSG.ReadVariableInt32();
                    c_Map.Ground[x, y].tileW = incMSG.ReadVariableInt32();
                    c_Map.Ground[x, y].tileH = incMSG.ReadVariableInt32();
                    c_Map.Ground[x, y].Tileset = incMSG.ReadVariableInt32();
                    c_Map.Ground[x, y].type = incMSG.ReadVariableInt32();
                    c_Map.Ground[x, y].spawnNum = incMSG.ReadVariableInt32();
                    //mask
                    c_Map.Mask[x, y].tileX = incMSG.ReadVariableInt32();
                    c_Map.Mask[x, y].tileY = incMSG.ReadVariableInt32();
                    c_Map.Mask[x, y].tileW = incMSG.ReadVariableInt32();
                    c_Map.Mask[x, y].tileH = incMSG.ReadVariableInt32();
                    c_Map.Mask[x, y].Tileset = incMSG.ReadVariableInt32();
                    //fringe
                    c_Map.Fringe[x, y].tileX = incMSG.ReadVariableInt32();
                    c_Map.Fringe[x, y].tileY = incMSG.ReadVariableInt32();
                    c_Map.Fringe[x, y].tileW = incMSG.ReadVariableInt32();
                    c_Map.Fringe[x, y].tileH = incMSG.ReadVariableInt32();
                    c_Map.Fringe[x, y].Tileset = incMSG.ReadVariableInt32();
                    //mask a
                    c_Map.MaskA[x, y].tileX = incMSG.ReadVariableInt32();
                    c_Map.MaskA[x, y].tileY = incMSG.ReadVariableInt32();
                    c_Map.MaskA[x, y].tileW = incMSG.ReadVariableInt32();
                    c_Map.MaskA[x, y].tileH = incMSG.ReadVariableInt32();
                    c_Map.MaskA[x, y].Tileset = incMSG.ReadVariableInt32();
                    //fringe a
                    c_Map.FringeA[x, y].tileX = incMSG.ReadVariableInt32();
                    c_Map.FringeA[x, y].tileY = incMSG.ReadVariableInt32();
                    c_Map.FringeA[x, y].tileW = incMSG.ReadVariableInt32();
                    c_Map.FringeA[x, y].tileH = incMSG.ReadVariableInt32();
                    c_Map.FringeA[x, y].Tileset = incMSG.ReadVariableInt32();
                }
            }
            c_Map.SaveMap();
            Console.WriteLine("Map data received from server! IP: " + incMSG.SenderConnection);

            //Loading is complete after the map
            LoadMainGUI(c_GUI, c_Canvas);
        }

        //Loads the main GUI once the loading is finished
        void LoadMainGUI(GUI c_GUI, Canvas c_Canvas)
        {
            c_Canvas.DeleteAllChildren();
            c_GUI.CreateDebugWindow(c_Canvas);
            c_GUI.CreateChatWindow(c_Canvas);
            c_GUI.AddText("Welcome to Sabertooth!");
        }
    }

    //Packet headers in the form of enumerations
    public enum PacketTypes
    {
        Connection,
        Register,
        ErrorMessage,
        Login,
        PlayerData,
        ChatMessage,
        MapData,
        Players,
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
        Items,
        Shutdown,
        Attack,
        ProjData,
        Projectiles,
        UpdateAmmo,
        CreateProj,
        ClearProj,
        UpdateProj
    }
}
