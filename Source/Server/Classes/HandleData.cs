using Lidgren.Network;
using System;
using System.Threading;
using System.Xml;
using static System.Convert;
using static System.IO.File;
using static System.Environment;
using System.Data.SQLite;

namespace Server.Classes
{
    class HandleData
    {
        public void HandleDataMessage(NetServer s_Server, Player[] s_Player, Map[] s_Map, Npc[] s_Npc, Item[] s_Item, Projectile[] s_Proj)
        {
            NetIncomingMessage incMSG;  //create incoming message

            if ((incMSG = s_Server.ReadMessage()) != null)
            {
                switch (incMSG.MessageType)
                {
                    case NetIncomingMessageType.DiscoveryRequest:
                        HandleDiscoveryRequest(incMSG, s_Server);
                        break;

                    case NetIncomingMessageType.ConnectionApproval:
                        HandleConnectionApproval(incMSG, s_Server);
                        break;

                    case NetIncomingMessageType.Data:
                        switch (incMSG.ReadByte())
                        {
                            case (byte)PacketTypes.Register:
                                HandleRegisterRequest(incMSG, s_Server, s_Player);
                                break;
                            case (byte)PacketTypes.Login:
                                HandleLoginRequest(incMSG, s_Server, s_Player, s_Map, s_Npc, s_Item, s_Proj);
                                break;

                            case (byte)PacketTypes.ChatMessage:
                                HandleChatMessage(incMSG, s_Server, s_Player, s_Map);
                                break;

                            case (byte)PacketTypes.MoveData:
                                HandleMoveData(incMSG, s_Server, s_Player);
                                break;

                            case (byte)PacketTypes.UpdateDirection:
                                HandleDirectionData(incMSG, s_Server, s_Player);
                                break;

                            case (byte)PacketTypes.RangedAttack:
                                HandleRangedAttack(incMSG, s_Server, s_Player, s_Map);
                                break;

                            case (byte)PacketTypes.UpdateAmmo:
                                HandleUpdateAmmo(incMSG, s_Server, s_Player);
                                break;

                            case (byte)PacketTypes.ClearProj:
                                HandleClearProjectile(incMSG, s_Server, s_Player, s_Map);
                                break;

                            case (byte)PacketTypes.UpdateClip:
                                HandleUpdateClip(incMSG, s_Player);
                                break;

                            case (byte)PacketTypes.AttackNpcProj:
                                HandleAttackNpcProj(incMSG, s_Server, s_Player, s_Map);
                                break;
                        }
                        break;

                    case NetIncomingMessageType.StatusChanged:
                        HandleStatusChange(incMSG, s_Server, s_Player);
                        break;
                }
            }
            s_Server.Recycle(incMSG);
        }

        void HandleAttackNpcProj(NetIncomingMessage incMSG, NetServer s_Server, Player[] s_Player, Map[] s_Map)
        {
            int slot = incMSG.ReadVariableInt32();
            int npc = incMSG.ReadVariableInt32();
            int owner = incMSG.ReadVariableInt32();
            int c_Map = s_Player[owner].Map;
            int damage = s_Player[owner].mainWeapon.Damage;

            if (s_Map[c_Map].mapNpc[npc].IsSpawned == false) { return; }
            if (s_Map[c_Map].mapProj[slot] == null) { return; }

            s_Map[c_Map].ClearProjSlot(s_Server, s_Map, c_Map, slot);
            s_Map[c_Map].mapNpc[npc].DamageNpc(damage);
        }

        void HandleUpdateClip(NetIncomingMessage incMSG, Player[] s_Player)
        {
            int index = incMSG.ReadVariableInt32();
            int mainClip = incMSG.ReadVariableInt32();
            int offClip = incMSG.ReadVariableInt32();

            s_Player[index].mainWeapon.Clip = mainClip;
            s_Player[index].offWeapon.Clip = offClip;
        }

        void HandleClearProjectile(NetIncomingMessage incMSG, NetServer s_Server, Player[] s_Player, Map[] s_Map)
        {
            int slot = incMSG.ReadVariableInt32();
            int owner = incMSG.ReadVariableInt32();
            int c_Map = s_Player[owner].Map;

            if (s_Map[c_Map].mapProj[slot] == null) { return; }
            s_Map[c_Map].ClearProjSlot(s_Server, s_Map, c_Map, slot);            
        }

        void HandleUpdateAmmo(NetIncomingMessage incMSG, NetServer s_Server, Player[] s_Player)
        {
            int index = incMSG.ReadVariableInt32();

            s_Player[index].PistolAmmo = incMSG.ReadVariableInt32();
            s_Player[index].AssaultAmmo = incMSG.ReadVariableInt32();
            s_Player[index].RocketAmmo = incMSG.ReadVariableInt32();
            s_Player[index].GrenadeAmmo = incMSG.ReadVariableInt32();
        }

        void HandleRangedAttack(NetIncomingMessage incMSG, NetServer s_Server, Player[] s_Player, Map[] s_Map)
        {
            int index = incMSG.ReadVariableInt32();
            int direction = incMSG.ReadVariableInt32();
            int aimdirection = incMSG.ReadVariableInt32();
            int cMap = s_Player[index].Map;

            s_Player[index].Direction = direction;
            s_Player[index].AimDirection = aimdirection;
            s_Map[cMap].CreateProjectile(s_Server, s_Player, index);

            for (int i = 0; i < 5; i++)
            {
                if (s_Player[i].Connection != null && s_Player[i].Map == s_Player[index].Map)
                {
                    SendUpdateDirection(s_Server, s_Player[i].Connection, index, direction, aimdirection);
                }
            }
        }

        //Handle incoming packets for movement of the player
        void HandleMoveData(NetIncomingMessage incMSG, NetServer s_Server, Player[] s_Player)
        {
            int index = incMSG.ReadVariableInt32();
            int x = incMSG.ReadVariableInt32();
            int y = incMSG.ReadVariableInt32();
            int direction = incMSG.ReadVariableInt32();
            int aimdirection = incMSG.ReadVariableInt32();
            int step = incMSG.ReadVariableInt32();

            s_Player[index].AimDirection = aimdirection;

            if (step == s_Player[index].Step) { return; }
            if (x == s_Player[index].X && y == s_Player[index].Y) { return; }

            s_Player[index].X = x;
            s_Player[index].Y = y;
            s_Player[index].Direction = direction;
            s_Player[index].Step = step;

            for (int i = 0; i < 5; i++)
            {
                if (s_Player[i].Connection != null && s_Player[i].Map == s_Player[index].Map)
                {
                    SendUpdateMovementData(s_Server, s_Player[i].Connection, index, x, y, direction, aimdirection, step);
                }
            }
        }
        
        //Handles incoming direction data for players
        void HandleDirectionData(NetIncomingMessage incMSG, NetServer s_Server, Player[] s_Player)
        {
            int index = incMSG.ReadVariableInt32();
            int direction = incMSG.ReadVariableInt32();
            int aimdirection = incMSG.ReadVariableInt32();

            s_Player[index].AimDirection = aimdirection;
            s_Player[index].Direction = direction;

            for (int i = 0; i < 5; i++)
            {
                if (s_Player[i].Connection != null && s_Player[i].Map == s_Player[index].Map)
                {
                    SendUpdateDirection(s_Server, s_Player[i].Connection, index, direction, aimdirection);
                }
            }
        }

        //Handles a discovery response from the server
        void HandleDiscoveryRequest(NetIncomingMessage incMSG, NetServer s_Server)
        {
            Console.WriteLine("Client discovered @ " + incMSG.SenderEndPoint.ToString());
            NetOutgoingMessage outMSG = s_Server.CreateMessage();
            outMSG.Write("Sabertooth Server");
            s_Server.SendDiscoveryResponse(outMSG, incMSG.SenderEndPoint);
        }

        //Handles our connection getting approved from our server
        void HandleConnectionApproval(NetIncomingMessage incMSG, NetServer s_Server)
        {
            if (incMSG.ReadByte() == (byte)PacketTypes.Connection)
            {
                string connect = incMSG.ReadString();
                if (connect == "sabertooth")
                {
                    incMSG.SenderConnection.Approve();
                    Thread.Sleep(500);
                    NetOutgoingMessage outMSG = s_Server.CreateMessage();
                    outMSG.Write((byte)PacketTypes.Connection);
                    s_Server.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
                }
                else
                {
                    incMSG.SenderConnection.Deny();
                }
            }
        }

        //Handles our registration requests to the server
        void HandleRegisterRequest(NetIncomingMessage incMSG, NetServer s_Server, Player[] s_Player)
        {
            string username = incMSG.ReadString();
            string password = incMSG.ReadString();

            if (!AlreadyLogged(username, s_Player))
            {
                if (!AccountExist(username))
                {
                    int i = OpenSlot(s_Player);
                    if (i < 5)
                    {
                        //s_Player[i] = new Player(username, password, incMSG.SenderConnection);
                        s_Player[i] = new Player(username, password, 0, 0, 0, 0, 0, 1, 100, 100, 0, 100, 10, 100, 100, 1, 1, 1, 1, 1000, incMSG.SenderConnection);
                        //s_Player[i].SavePlayerXML();
                        s_Player[i].CreatePlayerInDatabase();
                        Console.WriteLine("Account created, " + username + ", " + password);
                        SendErrorMessage("Account Created! Please login to play!", "Account Created", incMSG, s_Server);
                        ClearSlot(incMSG.SenderConnection, s_Player);
                    }
                    else
                    {
                        Console.WriteLine("Server Full!");
                        SendErrorMessage("Server is full. Please try again later.", "Server Full", incMSG, s_Server);
                    }
                }
                else
                {
                    Console.WriteLine("Account already exists!");
                    SendErrorMessage("Account already exists! Please choose another username.", "Account Exists", incMSG, s_Server);
                }
            }
            else
            {
                Console.WriteLine("Attempted multi-login!");
                SendErrorMessage("Account already logged in. If this is an error, please try again.", "Account Logged", incMSG, s_Server);
            }
        }

        //Handle logging in requests for the server
        void HandleLoginRequest(NetIncomingMessage incMSG, NetServer s_Server, Player[] s_Player, Map[] s_Map, Npc[] s_Npc, Item[] s_Item, Projectile[] s_Proj)
        {
            string username = incMSG.ReadString();
            string password = incMSG.ReadString();

            if (!AlreadyLogged(username, s_Player))
            {
                if (AccountExist(username) && CheckPassword(username, password))
                {
                    int i = OpenSlot(s_Player);
                    if (i < 5)
                    {
                        s_Player[i] = new Player(username, password, incMSG.SenderConnection);
                        //s_Player[i].LoadPlayerXML();
                        s_Player[i].LoadPlayerFromDatabase();
                        int currentMap = s_Player[i].Map;
                        Console.WriteLine("Account login by: " + username + ", " + password);
                        SendAcceptLogin(s_Server, s_Player, i);
                        SendPlayerData(incMSG, s_Server, s_Player, i);
                        SendPlayers(incMSG, s_Server, s_Player);
                        SendNpcs(incMSG, s_Server, s_Npc);
                        SendItems(incMSG, s_Server, s_Item);
                        SendProjectiles(incMSG, s_Server, s_Proj);
                        SendMapData(incMSG, s_Server, s_Map[currentMap], s_Player);
                        SendMapNpcs(incMSG, s_Server, s_Map[currentMap]);
                    }
                    else
                    {
                        Console.WriteLine("Server is full...");
                        SendErrorMessage("Server is full. Please try again later.", "Server Full", incMSG, s_Server);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid login by: " + username + ", " + password);
                    SendErrorMessage("Invalid username or password.", "Invalid Login", incMSG, s_Server);
                }
            }
            else
            {
                Console.WriteLine("Multiple login attempt by: " + username + ", " + password);
                SendErrorMessage("Account already logged in. If this is an error, please try again.", "Account Logged", incMSG, s_Server);
            }
        }
        
        //Handleing a chat message incoming
        void HandleChatMessage(NetIncomingMessage incMSG, NetServer s_Server, Player[] s_Player, Map[] s_Map)
        {
            string msg = incMSG.ReadString();
            //Check for an admin command
            if (msg.Substring(0, 1) == "/")
            {
                CheckCommand(msg, GetPlayerConnection(incMSG, s_Player), s_Player, incMSG, s_Server, s_Map);
                return;
            }
            else
            {
                string name = s_Player[GetPlayerConnection(incMSG, s_Player)].Name;
                string finalMsg = name + ": " + msg;
                NetOutgoingMessage outMSG = s_Server.CreateMessage();
                outMSG.Write((byte)PacketTypes.ChatMessage);
                outMSG.Write(finalMsg);
                s_Server.SendToAll(outMSG, NetDeliveryMethod.ReliableOrdered);
                LogWriter.WriteLog(finalMsg, "Chat");
                Console.WriteLine(finalMsg);
            }
        }

        //Handle the status change of a client
        void HandleStatusChange(NetIncomingMessage incMSG, NetServer s_Server, Player[] s_Player)
        {
            Console.WriteLine(incMSG.SenderConnection.ToString() + " status changed. " + incMSG.SenderConnection.Status);
            LogWriter.WriteLog(incMSG.SenderConnection.ToString() + " status changed. " + incMSG.SenderConnection.Status, "Server");
            if (incMSG.SenderConnection.Status == NetConnectionStatus.Disconnected || incMSG.SenderConnection.Status == NetConnectionStatus.Disconnecting)
            {
                Console.WriteLine("Disconnected clearing data...");
                LogWriter.WriteLog("Disconnected clearing data...", "Server");
                Console.WriteLine("Saving player...");
                LogWriter.WriteLog("Saving player...", "Server");
                SavePlayers(s_Player);
                ClearSlot(incMSG.SenderConnection, s_Player);
                SendPlayers(incMSG, s_Server, s_Player);
            }
        }

        //Sends a direction update to the client so it can be processed and sent to all connected
        void SendUpdateDirection(NetServer s_Server, NetConnection playerConn, int index, int direction, int aimdirection)
        {
            NetOutgoingMessage outMSG = s_Server.CreateMessage();
            outMSG.Write((byte)PacketTypes.DirData);
            outMSG.WriteVariableInt32(index);
            outMSG.WriteVariableInt32(direction);
            outMSG.WriteVariableInt32(aimdirection);
            s_Server.SendMessage(outMSG, playerConn, NetDeliveryMethod.ReliableSequenced, 2);
        }

        //Sending out the data for movement to be processed by everyone connected
        void SendUpdateMovementData(NetServer s_Server, NetConnection playerConn, int index, int x, int y, int direction, int aimdirection, int step)
        {
            NetOutgoingMessage outMSG = s_Server.CreateMessage();
            outMSG.Write((byte)PacketTypes.UpdateMoveData);
            outMSG.WriteVariableInt32(index);
            outMSG.WriteVariableInt32(x);
            outMSG.WriteVariableInt32(y);
            outMSG.WriteVariableInt32(direction);
            outMSG.WriteVariableInt32(aimdirection);
            outMSG.WriteVariableInt32(step);

            s_Server.SendMessage(outMSG, playerConn, NetDeliveryMethod.ReliableSequenced, 1);
        }

        //Update health
        public void SendUpdateHealthData(NetServer s_Server, int index, int health)
        {
            NetOutgoingMessage outMSG = s_Server.CreateMessage();
            outMSG.Write((byte)PacketTypes.HealthData);
            outMSG.WriteVariableInt32(index);
            outMSG.WriteVariableInt32(health);

            s_Server.SendToAll(outMSG, NetDeliveryMethod.ReliableOrdered);
        }

        //Update the vital data
        public void SendUpdateVitalData(NetServer s_Server, int index, string vitalName, int vital)
        {
            NetOutgoingMessage outMSG = s_Server.CreateMessage();
            outMSG.Write((byte)PacketTypes.VitalLoss);
            outMSG.WriteVariableInt32(index);
            outMSG.Write(vitalName);
            outMSG.WriteVariableInt32(vital);

            s_Server.SendToAll(outMSG, NetDeliveryMethod.ReliableOrdered);
        }

        //Sending data letting the client know we are connected and data is coming
        void SendAcceptLogin(NetServer s_Server, Player[] s_Player, int index)
        {
            NetOutgoingMessage outMSG = s_Server.CreateMessage();
            outMSG.Write((byte)PacketTypes.Login);
            s_Server.SendMessage(outMSG, s_Player[index].Connection, NetDeliveryMethod.ReliableOrdered);
        }

        //Send user data of the main index
        void SendPlayerData(NetIncomingMessage incMSG, NetServer s_Server, Player[] s_Player, int index)
        {
            NetOutgoingMessage outMSG = s_Server.CreateMessage();
            outMSG.Write((byte)PacketTypes.PlayerData);
            outMSG.Write(index);
            outMSG.Write(s_Player[index].Name);
            outMSG.WriteVariableInt32(s_Player[index].X);
            outMSG.WriteVariableInt32(s_Player[index].Y);
            outMSG.WriteVariableInt32(s_Player[index].Map);
            outMSG.WriteVariableInt32(s_Player[index].Direction);
            outMSG.WriteVariableInt32(s_Player[index].AimDirection);
            outMSG.WriteVariableInt32(s_Player[index].Sprite);
            outMSG.WriteVariableInt32(s_Player[index].Level);
            outMSG.WriteVariableInt32(s_Player[index].Points);
            outMSG.WriteVariableInt32(s_Player[index].Health);
            outMSG.WriteVariableInt32(s_Player[index].MaxHealth);
            outMSG.WriteVariableInt32(s_Player[index].Hunger);
            outMSG.WriteVariableInt32(s_Player[index].Hydration);
            outMSG.WriteVariableInt32(s_Player[index].Experience);
            outMSG.WriteVariableInt32(s_Player[index].Money);
            outMSG.WriteVariableInt32(s_Player[index].Armor);
            outMSG.WriteVariableInt32(s_Player[index].Strength);
            outMSG.WriteVariableInt32(s_Player[index].Agility);
            outMSG.WriteVariableInt32(s_Player[index].Endurance);
            outMSG.WriteVariableInt32(s_Player[index].Stamina);
            outMSG.WriteVariableInt32(s_Player[index].PistolAmmo);
            outMSG.WriteVariableInt32(s_Player[index].AssaultAmmo);
            outMSG.WriteVariableInt32(s_Player[index].RocketAmmo);
            outMSG.WriteVariableInt32(s_Player[index].GrenadeAmmo);

            //Main weapon
            outMSG.Write(s_Player[index].mainWeapon.Name);
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.Clip);
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.maxClip);
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.Sprite);
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.Damage);
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.Armor);
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.Type);
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.AttackSpeed);
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.ReloadSpeed);
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.HealthRestore);
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.HungerRestore);
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.HydrateRestore);
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.Strength);
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.Agility);
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.Endurance);
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.Stamina);
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.ammoType);

            //Secondary weapon
            outMSG.Write(s_Player[index].offWeapon.Name);
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.Clip);
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.maxClip);
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.Sprite);
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.Damage);
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.Armor);
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.Type);
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.AttackSpeed);
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.ReloadSpeed);
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.HealthRestore);
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.HungerRestore);
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.HydrateRestore);
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.Strength);
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.Agility);
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.Endurance);
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.Stamina);
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.ammoType);
            s_Server.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
        }

        //Send weapons update
        void SendWeaponsUpdate(NetServer s_Server, Player[] s_Player, int index)
        {
            NetOutgoingMessage outMSG = s_Server.CreateMessage();
            outMSG.Write((byte)PacketTypes.UpdateWeapons);
            //Main weapon
            outMSG.Write(s_Player[index].mainWeapon.Name);
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.Clip);
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.maxClip);
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.Sprite);
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.Damage);
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.Armor);
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.Type);
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.AttackSpeed);
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.ReloadSpeed);
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.HealthRestore);
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.HungerRestore);
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.HydrateRestore);
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.Strength);
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.Agility);
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.Endurance);
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.Stamina);
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.ammoType);

            //Secondary weapon
            outMSG.Write(s_Player[index].offWeapon.Name);
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.Clip);
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.maxClip);
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.Sprite);
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.Damage);
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.Armor);
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.Type);
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.AttackSpeed);
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.ReloadSpeed);
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.HealthRestore);
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.HungerRestore);
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.HydrateRestore);
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.Strength);
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.Agility);
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.Endurance);
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.Stamina);
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.ammoType);
            s_Server.SendMessage(outMSG, s_Player[index].Connection, NetDeliveryMethod.ReliableOrdered);
        }

        //Send user data to all
        void SendPlayers(NetIncomingMessage incMSG, NetServer s_Server, Player[] s_Player)
        {
            NetOutgoingMessage outMSG = s_Server.CreateMessage();
            outMSG.Write((byte)PacketTypes.Players);
            for (int i = 0; i < 5; i++)
            {
                outMSG.Write(s_Player[i].Name);
                outMSG.WriteVariableInt32(s_Player[i].X);
                outMSG.WriteVariableInt32(s_Player[i].Y);
                outMSG.WriteVariableInt32(s_Player[i].Map);
                outMSG.WriteVariableInt32(s_Player[i].Direction);
                outMSG.WriteVariableInt32(s_Player[i].AimDirection);
                outMSG.WriteVariableInt32(s_Player[i].Sprite);
                outMSG.WriteVariableInt32(s_Player[i].Level);
                outMSG.WriteVariableInt32(s_Player[i].Points);
                outMSG.WriteVariableInt32(s_Player[i].Health);
                outMSG.WriteVariableInt32(s_Player[i].MaxHealth);
                outMSG.WriteVariableInt32(s_Player[i].Hunger);
                outMSG.WriteVariableInt32(s_Player[i].Hydration);
                outMSG.WriteVariableInt32(s_Player[i].Experience);
                outMSG.WriteVariableInt32(s_Player[i].Money);
                outMSG.WriteVariableInt32(s_Player[i].Armor);
                outMSG.WriteVariableInt32(s_Player[i].Strength);
                outMSG.WriteVariableInt32(s_Player[i].Agility);
                outMSG.WriteVariableInt32(s_Player[i].Endurance);
                outMSG.WriteVariableInt32(s_Player[i].Stamina);
                outMSG.WriteVariableInt32(s_Player[i].PistolAmmo);
                outMSG.WriteVariableInt32(s_Player[i].AssaultAmmo);
                outMSG.WriteVariableInt32(s_Player[i].RocketAmmo);
                outMSG.WriteVariableInt32(s_Player[i].GrenadeAmmo);
            }
            s_Server.SendToAll(outMSG, NetDeliveryMethod.ReliableOrdered);
            Console.WriteLine("Sending players...");
            LogWriter.WriteLog("Sending players...", "Server");
        }

        //Send projectile data
        void SendProjData(NetIncomingMessage incMSG, NetServer s_Server, Projectile[] s_Proj, int index)
        {
            NetOutgoingMessage outMSG = s_Server.CreateMessage();
            outMSG.Write((byte)PacketTypes.ProjData);
            outMSG.WriteVariableInt32(index);
            outMSG.Write(s_Proj[index].Name);
            outMSG.WriteVariableInt32(s_Proj[index].Damage);
            outMSG.WriteVariableInt32(s_Proj[index].Range);
            outMSG.WriteVariableInt32(s_Proj[index].Sprite);
            outMSG.WriteVariableInt32(s_Proj[index].Type);
            outMSG.WriteVariableInt32(s_Proj[index].Speed);
            s_Server.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
        }

        void SendProjectiles(NetIncomingMessage incMSG, NetServer s_Server, Projectile[] s_Proj)
        {
            NetOutgoingMessage outMSG = s_Server.CreateMessage();
            outMSG.Write((byte)PacketTypes.Projectiles);
            for (int i = 0; i < 10; i++)
            {
                outMSG.Write(s_Proj[i].Name);
                outMSG.WriteVariableInt32(s_Proj[i].Damage);
                outMSG.WriteVariableInt32(s_Proj[i].Range);
                outMSG.WriteVariableInt32(s_Proj[i].Sprite);
                outMSG.WriteVariableInt32(s_Proj[i].Type);
                outMSG.WriteVariableInt32(s_Proj[i].Sprite);
            }
            s_Server.SendToAll(outMSG, NetDeliveryMethod.ReliableOrdered);
            Console.WriteLine("Sending projectiles...");
            LogWriter.WriteLog("Sending projectiles...", "Server");
        }

        //Send item data
        void SendItemData(NetIncomingMessage incMSG, NetServer s_Server, Item[] s_Item, int index)
        {
            NetOutgoingMessage outMSG = s_Server.CreateMessage();
            outMSG.Write((byte)PacketTypes.ItemData);
            outMSG.WriteVariableInt32(index);
            outMSG.Write(s_Item[index].Name);
            outMSG.WriteVariableInt32(s_Item[index].Sprite);
            outMSG.WriteVariableInt32(s_Item[index].Damage);
            outMSG.WriteVariableInt32(s_Item[index].Armor);
            outMSG.WriteVariableInt32(s_Item[index].Type);
            outMSG.WriteVariableInt32(s_Item[index].HealthRestore);
            outMSG.WriteVariableInt32(s_Item[index].HungerRestore);
            outMSG.WriteVariableInt32(s_Item[index].HydrateRestore);
            outMSG.WriteVariableInt32(s_Item[index].Strength);
            outMSG.WriteVariableInt32(s_Item[index].Agility);
            outMSG.WriteVariableInt32(s_Item[index].Endurance);
            outMSG.WriteVariableInt32(s_Item[index].Stamina);
            outMSG.WriteVariableInt32(s_Item[index].Clip);
            outMSG.WriteVariableInt32(s_Item[index].maxClip);
            outMSG.WriteVariableInt32(s_Item[index].ammoType);
            s_Server.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
        }

        //Send item data to all
        void SendItems(NetIncomingMessage incMSG, NetServer s_Server, Item[] s_Item)
        {
            NetOutgoingMessage outMSG = s_Server.CreateMessage();
            outMSG.Write((byte)PacketTypes.Items);
            for (int i = 0; i < 50; i++)
            {
                outMSG.Write(s_Item[i].Name);
                outMSG.WriteVariableInt32(s_Item[i].Sprite);
                outMSG.WriteVariableInt32(s_Item[i].Damage);
                outMSG.WriteVariableInt32(s_Item[i].Armor);
                outMSG.WriteVariableInt32(s_Item[i].Type);
                outMSG.WriteVariableInt32(s_Item[i].HealthRestore);
                outMSG.WriteVariableInt32(s_Item[i].HungerRestore);
                outMSG.WriteVariableInt32(s_Item[i].HydrateRestore);
                outMSG.WriteVariableInt32(s_Item[i].Strength);
                outMSG.WriteVariableInt32(s_Item[i].Agility);
                outMSG.WriteVariableInt32(s_Item[i].Endurance);
                outMSG.WriteVariableInt32(s_Item[i].Stamina);
                outMSG.WriteVariableInt32(s_Item[i].Clip);
                outMSG.WriteVariableInt32(s_Item[i].maxClip);
                outMSG.WriteVariableInt32(s_Item[i].ammoType);
            }
            s_Server.SendToAll(outMSG, NetDeliveryMethod.ReliableOrdered);
            Console.WriteLine("Sending items...");
            LogWriter.WriteLog("Sending items...", "Server");
        }

        //Send npc data to client
        void SendNpcs(NetIncomingMessage incMSG, NetServer s_Server, Npc[] s_Npc)
        {
            NetOutgoingMessage outMSG = s_Server.CreateMessage();
            outMSG.Write((byte)PacketTypes.Npcs);
            for (int i = 0; i < 10; i++)
            {
                outMSG.Write(s_Npc[i].Name);
                outMSG.WriteVariableInt32(s_Npc[i].X);
                outMSG.WriteVariableInt32(s_Npc[i].Y);
                outMSG.WriteVariableInt32(s_Npc[i].Direction);
                outMSG.WriteVariableInt32(s_Npc[i].Sprite);
                outMSG.WriteVariableInt32(s_Npc[i].Step);
                outMSG.WriteVariableInt32(s_Npc[i].Owner);
                outMSG.WriteVariableInt32(s_Npc[i].Behavior);
                outMSG.WriteVariableInt32(s_Npc[i].SpawnTime);
                outMSG.WriteVariableInt32(s_Npc[i].Health);
                outMSG.WriteVariableInt32(s_Npc[i].MaxHealth);
                outMSG.WriteVariableInt32(s_Npc[i].Damage);
                outMSG.Write(s_Npc[i].IsSpawned);
            }
            s_Server.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
            Console.WriteLine("Sending NPS...");
            LogWriter.WriteLog("Sending npcs...", "Server");
        }

        //Send map npcs to client
        void SendMapNpcs(NetIncomingMessage incMSG, NetServer s_Server, Map s_Map)
        {
            NetOutgoingMessage outMSG = s_Server.CreateMessage();
            outMSG.Write((byte)PacketTypes.MapNpc);
            for (int i = 0; i < 10; i++)
            {
                outMSG.Write(s_Map.mapNpc[i].Name);
                outMSG.WriteVariableInt32(s_Map.mapNpc[i].X);
                outMSG.WriteVariableInt32(s_Map.mapNpc[i].Y);
                outMSG.WriteVariableInt32(s_Map.mapNpc[i].Direction);
                outMSG.WriteVariableInt32(s_Map.mapNpc[i].Sprite);
                outMSG.WriteVariableInt32(s_Map.mapNpc[i].Step);
                outMSG.WriteVariableInt32(s_Map.mapNpc[i].Owner);
                outMSG.WriteVariableInt32(s_Map.mapNpc[i].Behavior);
                outMSG.WriteVariableInt32(s_Map.mapNpc[i].SpawnTime);
                outMSG.WriteVariableInt32(s_Map.mapNpc[i].Health);
                outMSG.WriteVariableInt32(s_Map.mapNpc[i].MaxHealth);
                outMSG.WriteVariableInt32(s_Map.mapNpc[i].Damage);
                outMSG.Write(s_Map.mapNpc[i].IsSpawned);
            }
            s_Server.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
        }

        //Send npc's associated with the map the player is currently on
        public void SendMapNpcData(NetServer s_Server, NetConnection playerConn, Map s_Map, int npcNum)
        {
            NetOutgoingMessage outMSG = s_Server.CreateMessage();
            outMSG.Write((byte)PacketTypes.NpcData);
            outMSG.WriteVariableInt32(npcNum);
            outMSG.Write(s_Map.mapNpc[npcNum].Name);
            outMSG.WriteVariableInt32(s_Map.mapNpc[npcNum].X);
            outMSG.WriteVariableInt32(s_Map.mapNpc[npcNum].Y);
            outMSG.WriteVariableInt32(s_Map.mapNpc[npcNum].Direction);
            outMSG.WriteVariableInt32(s_Map.mapNpc[npcNum].Sprite);
            outMSG.WriteVariableInt32(s_Map.mapNpc[npcNum].Step);
            outMSG.WriteVariableInt32(s_Map.mapNpc[npcNum].Owner);
            outMSG.WriteVariableInt32(s_Map.mapNpc[npcNum].Behavior);
            outMSG.WriteVariableInt32(s_Map.mapNpc[npcNum].SpawnTime);
            outMSG.WriteVariableInt32(s_Map.mapNpc[npcNum].Health);
            outMSG.WriteVariableInt32(s_Map.mapNpc[npcNum].MaxHealth);
            outMSG.WriteVariableInt32(s_Map.mapNpc[npcNum].Damage);
            outMSG.Write(s_Map.mapNpc[npcNum].IsSpawned);

            s_Server.SendMessage(outMSG, playerConn, NetDeliveryMethod.ReliableOrdered);
        }

        //Sends an error message to be processed by the client
        void SendErrorMessage(string message, string caption, NetIncomingMessage incMSG, NetServer s_Server)
        {
            NetOutgoingMessage outMSG = s_Server.CreateMessage();
            outMSG.Write((byte)PacketTypes.ErrorMessage);
            outMSG.Write(message);
            outMSG.Write(caption);
            s_Server.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
        }

        //Send the map to the client
        void SendMapData(NetIncomingMessage incMSG, NetServer s_Server, Map s_Map, Player[] s_Player)
        {
            NetOutgoingMessage outMSG = s_Server.CreateMessage();
            outMSG.Write((byte)PacketTypes.MapData);
            outMSG.Write(s_Map.Name);

            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    //ground
                    outMSG.WriteVariableInt32(s_Map.Ground[x, y].TileX);
                    outMSG.WriteVariableInt32(s_Map.Ground[x, y].TileY);
                    outMSG.WriteVariableInt32(s_Map.Ground[x, y].TileW);
                    outMSG.WriteVariableInt32(s_Map.Ground[x, y].TileH);
                    outMSG.WriteVariableInt32(s_Map.Ground[x, y].Tileset);
                    outMSG.WriteVariableInt32(s_Map.Ground[x, y].Type);
                    outMSG.WriteVariableInt32(s_Map.Ground[x, y].SpawnNum);
                    //mask
                    outMSG.WriteVariableInt32(s_Map.Mask[x, y].TileX);
                    outMSG.WriteVariableInt32(s_Map.Mask[x, y].TileY);
                    outMSG.WriteVariableInt32(s_Map.Mask[x, y].TileW);
                    outMSG.WriteVariableInt32(s_Map.Mask[x, y].TileH);
                    outMSG.WriteVariableInt32(s_Map.Mask[x, y].Tileset);
                    //fringe
                    outMSG.WriteVariableInt32(s_Map.Fringe[x, y].TileX);
                    outMSG.WriteVariableInt32(s_Map.Fringe[x, y].TileY);
                    outMSG.WriteVariableInt32(s_Map.Fringe[x, y].TileW);
                    outMSG.WriteVariableInt32(s_Map.Fringe[x, y].TileH);
                    outMSG.WriteVariableInt32(s_Map.Fringe[x, y].Tileset);
                    //mask a
                    outMSG.WriteVariableInt32(s_Map.MaskA[x, y].TileX);
                    outMSG.WriteVariableInt32(s_Map.MaskA[x, y].TileY);
                    outMSG.WriteVariableInt32(s_Map.MaskA[x, y].TileW);
                    outMSG.WriteVariableInt32(s_Map.MaskA[x, y].TileH);
                    outMSG.WriteVariableInt32(s_Map.MaskA[x, y].Tileset);
                    //fringe a
                    outMSG.WriteVariableInt32(s_Map.FringeA[x, y].TileX);
                    outMSG.WriteVariableInt32(s_Map.FringeA[x, y].TileY);
                    outMSG.WriteVariableInt32(s_Map.FringeA[x, y].TileW);
                    outMSG.WriteVariableInt32(s_Map.FringeA[x, y].TileH);
                    outMSG.WriteVariableInt32(s_Map.FringeA[x, y].Tileset);
                }
            }
            s_Server.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
            Console.WriteLine("Sending map...");
            LogWriter.WriteLog("Sending map...", "Server");
        }

        //Clear the slot for other players to connect
        void ClearSlot(NetConnection conn, Player[] s_Player)
        {
            for (int i = 0; i < 5; i++)
            {
                if (s_Player[i] != null && s_Player[i].Connection == conn)
                {
                    s_Player[i] = null;
                    s_Player[i] = new Player();
                    break;
                }
            }
        }

        //Saves all players
        void SavePlayers(Player[] s_Player)
        {
            for (int i = 0; i < 5; i++)
            {
                if (s_Player[i].Name != null)
                {
                    //s_Player[i].SavePlayerXML();
                    s_Player[i].SavePlayerToDatabase();
                }
            }
        }

        //Checks commands sent from the client
        void CheckCommand(string msg, int index, Player[] s_Player, NetIncomingMessage incMSG, NetServer s_Server, Map[] s_Map)
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
                        s_Player[index].Map = mapNum;
                        SendMapData(incMSG, s_Server, s_Map[mapNum], s_Player);
                        SendPlayers(incMSG, s_Server, s_Player);
                        SendMapNpcs(incMSG, s_Server, s_Map[mapNum]);
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
                    s_Player[index].Sprite = spriteNum;
                    SendPlayers(incMSG, s_Server, s_Player);
                    Console.WriteLine("Command:" + msg + " Spritenum: " + spriteNum);
                    return;
                }
            }
        }

        //Check to see if the account is already logged in
        static bool AlreadyLogged(string name, Player[] s_Player)
        {
            if (name == null) return false;

            for (int i = 0; i < 5; i++)
            {
                if (s_Player[i].Name != null)
                {
                    string alname = s_Player[i].Name.ToLower();
                    string alother = name.ToLower();
                    if (alname == alother)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //Check and assign and open slot if open
        static int OpenSlot(Player[] s_Player)
        {
            for (int i = 0; i < 5; i++)
            {
                if (s_Player[i].Name == null)
                {
                    return i;
                }
            }
            return 5;
        }

        //Get the players connection
        static int GetPlayerConnection(NetIncomingMessage incMSG, Player[] s_Player)
        {
            for (int i = 0; i < 5; i++)
            {
                if (s_Player[i].Connection == incMSG.SenderConnection)
                {
                    return i;
                }
            }
            return 5;
        }

        //Check the password to make sure its correct
        static bool CheckPassword(string name, string pass)
        {
            SQLiteConnection s_Database = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;");
            s_Database.Open();
            string sql;
            string compare;
            sql = "SELECT * FROM `PLAYERS` WHERE NAME = '" + name + "'";

            SQLiteCommand sql_Command = new SQLiteCommand(sql, s_Database);
            SQLiteDataReader sql_Reader = sql_Command.ExecuteReader();

            while (sql_Reader.Read())
            {
                compare = sql_Reader["PASSWORD"].ToString();

                if (pass == compare)
                {
                    return true;
                }
            }           
            s_Database.Close();
            return false;
        }

        //If creating an account check to see if it already exists
        static bool AccountExist(string name)
        {
            SQLiteConnection s_Database = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;");
            s_Database.Open();
            string sql;

            sql = "SELECT * FROM `PLAYERS` WHERE NAME = '" + name + "'";

            SQLiteCommand sql_Command = new SQLiteCommand(sql, s_Database);
            SQLiteDataReader sql_Reader = sql_Command.ExecuteReader();

            while (sql_Reader.Read())
            {
                if (sql_Reader["NAME"].ToString() == name)
                {
                    return true;
                }
            }
            return false;
        }
    }

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
        UpdateProj,
        UpdateWeapons,
        RangedAttack,
        UpdateClip,
        AttackNpcProj
    }
}
