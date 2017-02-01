#undef DEBUG
using Lidgren.Network;
using System;
using System.Data.SQLite;
using System.Threading;
using static System.Convert;

namespace Server.Classes
{
    class HandleData
    {
        public string s_Version;

        public void HandleDataMessage(NetServer s_Server, Player[] s_Player, Map[] s_Map, Npc[] s_Npc, Item[] s_Item, Projectile[] s_Proj)
        {
            NetIncomingMessage incMSG;

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
                                HandleRangedAttack(incMSG, s_Server, s_Player, s_Proj, s_Map);
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
                                HandleAttackNpcProj(incMSG, s_Server, s_Player, s_Map, s_Proj);
                                break;

                            case (byte)PacketTypes.ItemPickup:
                                HandleItemPickup(incMSG, s_Server, s_Player, s_Map, s_Item);
                                break;

                            case (byte)PacketTypes.UnequipItem:
                                HandleUnequipItem(incMSG, s_Server, s_Player);
                                break;

                            case (byte)PacketTypes.EquipItem:
                                HandleEquipItem(incMSG, s_Server, s_Player);
                                break;

                            case (byte)PacketTypes.DropItem:
                                HandleDropItem(incMSG, s_Server, s_Player, s_Map);
                                break;

                            default:
                                Console.WriteLine("Unknown packet header.");
                                break;
                        }
                        break;

                    case NetIncomingMessageType.StatusChanged:
                        HandleStatusChange(incMSG, s_Server, s_Player);
                        break;
                }
                #if DEBUG
                Console.WriteLine("Packet Size: " + incMSG.LengthBytes + " Bytes, " + incMSG.LengthBits + " bits");
                #endif
            }
            s_Server.Recycle(incMSG);
        }

        #region Handle Incoming Data
        void HandleDropItem(NetIncomingMessage incMSG, NetServer s_Server, Player[] s_Player, Map[] s_Map)
        {
            int index = incMSG.ReadVariableInt32();
            int slot = incMSG.ReadVariableInt32();
            int mapNum = s_Player[index].Map;

            s_Player[index].DropItem(s_Server, s_Map, s_Player, index, slot, mapNum);
        }

        void HandleEquipItem(NetIncomingMessage incMSG, NetServer s_Server, Player[] s_Player)
        {
            int index = incMSG.ReadVariableInt32();
            int slot = incMSG.ReadVariableInt32();

            s_Player[index].EquipItem(s_Server, s_Player, index, slot);
        }

        void HandleUnequipItem(NetIncomingMessage incMSG, NetServer s_Server, Player[] s_Player)
        {
            int index = incMSG.ReadVariableInt32();
            int equip = incMSG.ReadVariableInt32();

            s_Player[index].UnequipItem(s_Server, s_Player, index, equip);
        }

        void HandleRequestInvUpdate(NetIncomingMessage incMSG, NetServer s_Server, Player[] s_Player)
        {
            int index = incMSG.ReadVariableInt32();

            SendPlayerInv(s_Server, s_Player, index);
        }

        void HandleItemPickup(NetIncomingMessage incMSG, NetServer s_Server, Player[] s_Player, Map[] s_Map, Item[] s_Item)
        {
            int index = incMSG.ReadVariableInt32();
            int pMap = s_Player[index].Map;

            s_Player[index].CheckPickup(s_Server, s_Map[pMap], s_Player, s_Item, index);
        }

        void HandleAttackNpcProj(NetIncomingMessage incMSG, NetServer s_Server, Player[] s_Player, Map[] s_Map, Projectile[] s_Proj)
        {
            int slot = incMSG.ReadVariableInt32();
            int npc = incMSG.ReadVariableInt32();
            int owner = incMSG.ReadVariableInt32();
            int type = incMSG.ReadVariableInt32();
            int c_Map = s_Player[owner].Map;
            int damage = s_Player[owner].mainWeapon.Damage + s_Proj[s_Player[owner].mainWeapon.ProjectileNumber].Damage;

            if (type == 0)
            {
                if (s_Map[c_Map].m_MapNpc[npc].IsSpawned == false) { return; }
                if (s_Map[c_Map].mapProj[slot] == null) { return; }

                s_Map[c_Map].ClearProjSlot(s_Server, s_Map, s_Player, c_Map, slot);
                bool updatePlayer = s_Map[c_Map].m_MapNpc[npc].DamageNpc(s_Player[owner], s_Map[c_Map], damage);

                for (int p = 0; p < 5; p++)
                {
                    if (s_Player[p].Connection != null && c_Map == s_Player[p].Map)
                    {
                        SendNpcVitalData(s_Server, s_Player[p].Connection, s_Map[c_Map], npc);
                    }
                }
                if (updatePlayer) { SendUpdatePlayerStats(s_Server, s_Player, owner); }
            }
            else
            {
                if (s_Map[c_Map].r_MapNpc[npc].IsSpawned == false) { return; }
                if (s_Map[c_Map].mapProj[slot] == null) { return; }

                s_Map[c_Map].ClearProjSlot(s_Server, s_Map, s_Player, c_Map, slot);
                bool updatePlayer = s_Map[c_Map].r_MapNpc[npc].DamageNpc(s_Player[owner], s_Map[c_Map], damage);

                for (int p = 0; p < 5; p++)
                {
                    if (s_Player[p].Connection != null && c_Map == s_Player[p].Map)
                    {
                        SendPoolNpcVitalData(s_Server, s_Player[p].Connection, s_Map[c_Map], npc);
                    }
                }
                if (updatePlayer) { SendUpdatePlayerStats(s_Server, s_Player, owner); }
            }
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
            s_Map[c_Map].ClearProjSlot(s_Server, s_Map, s_Player, c_Map, slot);
        }

        void HandleUpdateAmmo(NetIncomingMessage incMSG, NetServer s_Server, Player[] s_Player)
        {
            int index = incMSG.ReadVariableInt32();

            s_Player[index].PistolAmmo = incMSG.ReadVariableInt32();
            s_Player[index].AssaultAmmo = incMSG.ReadVariableInt32();
            s_Player[index].RocketAmmo = incMSG.ReadVariableInt32();
            s_Player[index].GrenadeAmmo = incMSG.ReadVariableInt32();
        }

        void HandleRangedAttack(NetIncomingMessage incMSG, NetServer s_Server, Player[] s_Player, Projectile[] s_Proj, Map[] s_Map)
        {
            int index = incMSG.ReadVariableInt32();
            int cMap = s_Player[index].Map;

            s_Map[cMap].CreateProjectile(s_Server, s_Player, s_Proj, cMap, index);
        }

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

        void HandleDiscoveryRequest(NetIncomingMessage incMSG, NetServer s_Server)
        {
            Console.WriteLine("Client discovered @ " + incMSG.SenderEndPoint.ToString());
            NetOutgoingMessage outMSG = s_Server.CreateMessage();
            outMSG.Write("Sabertooth Server");
            s_Server.SendDiscoveryResponse(outMSG, incMSG.SenderEndPoint);
        }

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
                        s_Player[i] = new Player(username, password, 0, 0, 0, 0, 0, 1, 100, 100, 100, 0, 100, 10, 100, 100, 1, 1, 1, 1, 1000, incMSG.SenderConnection);
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

        void HandleLoginRequest(NetIncomingMessage incMSG, NetServer s_Server, Player[] s_Player, Map[] s_Map, Npc[] s_Npc, Item[] s_Item, Projectile[] s_Proj)
        {
            string username = incMSG.ReadString();
            string password = incMSG.ReadString();
            string version = incMSG.ReadString();

            if (version != s_Version) { SendErrorMessage("Incorrect client version, please run the updater!", "Outdated Version!", incMSG, s_Server); return; }

            if (!AlreadyLogged(username, s_Player))
            {
                if (AccountExist(username) && CheckPassword(username, password))
                {
                    int i = OpenSlot(s_Player);
                    if (i < 5)
                    {
                        s_Player[i] = new Player(username, password, incMSG.SenderConnection);
                        s_Player[i].LoadPlayerFromDatabase();
                        int currentMap = s_Player[i].Map;
                        Console.WriteLine("Account login by: " + username + ", " + password);
                        SendAcceptLogin(s_Server, s_Player, i);
                        SendPlayerData(incMSG, s_Server, s_Player, i);
                        SendPlayers(s_Server, s_Player);
                        SendPlayerInv(s_Server, s_Player, i);
                        SendPlayerEquipment(s_Server, s_Player, i);
                        SendNpcs(incMSG, s_Server, s_Npc);
                        SendItems(incMSG, s_Server, s_Item);
                        SendProjectiles(incMSG, s_Server, s_Proj);
                        SendMapData(incMSG, s_Server, s_Map[currentMap], s_Player);
                        SendMapNpcs(incMSG, s_Server, s_Map[currentMap]);
                        SendPoolMapNpcs(incMSG, s_Server, s_Map[currentMap]);
                        SendMapItems(incMSG, s_Server, s_Map[currentMap]);
                        Console.WriteLine("Data sent to " + username + ", IP: " + incMSG.SenderConnection);
                        string welcomeMsg = username + " has joined Sabertooth!";
                        SendServerMessage(s_Server, welcomeMsg);
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
                SendPlayers(s_Server, s_Player);
                Console.WriteLine("Player saved!");
                LogWriter.WriteLog("Player saved...", "Server");
            }
        }
        #endregion

        #region Send Outgoing Data
        public void SendServerMessage(NetServer s_Server, string message)
        {
            NetOutgoingMessage outMSG = s_Server.CreateMessage();
            outMSG.Write((byte)PacketTypes.ChatMessage);
            outMSG.Write(message);
            s_Server.SendToAll(outMSG, NetDeliveryMethod.ReliableOrdered);
        } 

        void SendUpdateDirection(NetServer s_Server, NetConnection playerConn, int index, int direction, int aimdirection)
        {
            NetOutgoingMessage outMSG = s_Server.CreateMessage();
            outMSG.Write((byte)PacketTypes.DirData);
            outMSG.WriteVariableInt32(index);
            outMSG.WriteVariableInt32(direction);
            outMSG.WriteVariableInt32(aimdirection);
            s_Server.SendMessage(outMSG, playerConn, NetDeliveryMethod.Unreliable);
        }

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

            s_Server.SendMessage(outMSG, playerConn, NetDeliveryMethod.Unreliable);
        }

        public void SendUpdateHealthData(NetServer s_Server, int index, int health)
        {
            NetOutgoingMessage outMSG = s_Server.CreateMessage();
            outMSG.Write((byte)PacketTypes.HealthData);
            outMSG.WriteVariableInt32(index);
            outMSG.WriteVariableInt32(health);

            s_Server.SendToAll(outMSG, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendUpdateVitalData(NetServer s_Server, int index, string vitalName, int vital)
        {
            NetOutgoingMessage outMSG = s_Server.CreateMessage();
            outMSG.Write((byte)PacketTypes.VitalLoss);
            outMSG.WriteVariableInt32(index);
            outMSG.Write(vitalName);
            outMSG.WriteVariableInt32(vital);

            s_Server.SendToAll(outMSG, NetDeliveryMethod.ReliableOrdered);
        }

        void SendAcceptLogin(NetServer s_Server, Player[] s_Player, int index)
        {
            NetOutgoingMessage outMSG = s_Server.CreateMessage();
            outMSG.Write((byte)PacketTypes.Login);
            s_Server.SendMessage(outMSG, s_Player[index].Connection, NetDeliveryMethod.ReliableOrdered);
        }

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
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.MaxClip);
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
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.ItemAmmoType);
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.Value);
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.ProjectileNumber);
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.Price);

            //Secondary weapon
            outMSG.Write(s_Player[index].offWeapon.Name);
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.Clip);
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.MaxClip);
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
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.ItemAmmoType);
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.Value);
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.ProjectileNumber);
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.Price);

            s_Server.SendMessage(outMSG, s_Player[index].Connection, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendUpdatePlayerStats(NetServer s_Server, Player[] s_Player, int index)
        {
            NetOutgoingMessage outMSG = s_Server.CreateMessage();
            outMSG.Write((byte)PacketTypes.UpdatePlayerStats);
            //outMSG.Write(index);
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

            s_Server.SendMessage(outMSG, s_Player[index].Connection, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendPlayerInv(NetServer s_Server, Player[] s_Player, int index)
        {
            NetOutgoingMessage outMSG = s_Server.CreateMessage();
            outMSG.Write((byte)PacketTypes.PlayerInv);
            for (int i = 0; i < 25; i++)
            {
                outMSG.Write(s_Player[index].Backpack[i].Name);
                outMSG.WriteVariableInt32(s_Player[index].Backpack[i].Sprite);
                outMSG.WriteVariableInt32(s_Player[index].Backpack[i].Damage);
                outMSG.WriteVariableInt32(s_Player[index].Backpack[i].Armor);
                outMSG.WriteVariableInt32(s_Player[index].Backpack[i].Type);
                outMSG.WriteVariableInt32(s_Player[index].Backpack[i].AttackSpeed);
                outMSG.WriteVariableInt32(s_Player[index].Backpack[i].ReloadSpeed);
                outMSG.WriteVariableInt32(s_Player[index].Backpack[i].HealthRestore);
                outMSG.WriteVariableInt32(s_Player[index].Backpack[i].HungerRestore);
                outMSG.WriteVariableInt32(s_Player[index].Backpack[i].HydrateRestore);
                outMSG.WriteVariableInt32(s_Player[index].Backpack[i].Strength);
                outMSG.WriteVariableInt32(s_Player[index].Backpack[i].Agility);
                outMSG.WriteVariableInt32(s_Player[index].Backpack[i].Endurance);
                outMSG.WriteVariableInt32(s_Player[index].Backpack[i].Stamina);
                outMSG.WriteVariableInt32(s_Player[index].Backpack[i].Clip);
                outMSG.WriteVariableInt32(s_Player[index].Backpack[i].MaxClip);
                outMSG.WriteVariableInt32(s_Player[index].Backpack[i].ItemAmmoType);
                outMSG.WriteVariableInt32(s_Player[index].Backpack[i].Value);
                outMSG.WriteVariableInt32(s_Player[index].Backpack[i].ProjectileNumber);
                outMSG.WriteVariableInt32(s_Player[index].Backpack[i].Price);
            }
            s_Server.SendMessage(outMSG, s_Player[index].Connection, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendPlayerEquipment(NetServer s_Server, Player[] s_Player, int index)
        {
            NetOutgoingMessage outMSG = s_Server.CreateMessage();
            outMSG.Write((byte)PacketTypes.PlayerEquip);

            outMSG.Write(s_Player[index].Chest.Name);
            outMSG.WriteVariableInt32(s_Player[index].Chest.Sprite);
            outMSG.WriteVariableInt32(s_Player[index].Chest.Damage);
            outMSG.WriteVariableInt32(s_Player[index].Chest.Armor);
            outMSG.WriteVariableInt32(s_Player[index].Chest.Type);
            outMSG.WriteVariableInt32(s_Player[index].Chest.AttackSpeed);
            outMSG.WriteVariableInt32(s_Player[index].Chest.ReloadSpeed);
            outMSG.WriteVariableInt32(s_Player[index].Chest.HealthRestore);
            outMSG.WriteVariableInt32(s_Player[index].Chest.HungerRestore);
            outMSG.WriteVariableInt32(s_Player[index].Chest.HydrateRestore);
            outMSG.WriteVariableInt32(s_Player[index].Chest.Strength);
            outMSG.WriteVariableInt32(s_Player[index].Chest.Agility);
            outMSG.WriteVariableInt32(s_Player[index].Chest.Endurance);
            outMSG.WriteVariableInt32(s_Player[index].Chest.Stamina);
            outMSG.WriteVariableInt32(s_Player[index].Chest.Clip);
            outMSG.WriteVariableInt32(s_Player[index].Chest.MaxClip);
            outMSG.WriteVariableInt32(s_Player[index].Chest.ItemAmmoType);
            outMSG.WriteVariableInt32(s_Player[index].Chest.Value);
            outMSG.WriteVariableInt32(s_Player[index].Chest.ProjectileNumber);
            outMSG.WriteVariableInt32(s_Player[index].Chest.Price);

            outMSG.Write(s_Player[index].Legs.Name);
            outMSG.WriteVariableInt32(s_Player[index].Legs.Sprite);
            outMSG.WriteVariableInt32(s_Player[index].Legs.Damage);
            outMSG.WriteVariableInt32(s_Player[index].Legs.Armor);
            outMSG.WriteVariableInt32(s_Player[index].Legs.Type);
            outMSG.WriteVariableInt32(s_Player[index].Legs.AttackSpeed);
            outMSG.WriteVariableInt32(s_Player[index].Legs.ReloadSpeed);
            outMSG.WriteVariableInt32(s_Player[index].Legs.HealthRestore);
            outMSG.WriteVariableInt32(s_Player[index].Legs.HungerRestore);
            outMSG.WriteVariableInt32(s_Player[index].Legs.HydrateRestore);
            outMSG.WriteVariableInt32(s_Player[index].Legs.Strength);
            outMSG.WriteVariableInt32(s_Player[index].Legs.Agility);
            outMSG.WriteVariableInt32(s_Player[index].Legs.Endurance);
            outMSG.WriteVariableInt32(s_Player[index].Legs.Stamina);
            outMSG.WriteVariableInt32(s_Player[index].Legs.Clip);
            outMSG.WriteVariableInt32(s_Player[index].Legs.MaxClip);
            outMSG.WriteVariableInt32(s_Player[index].Legs.ItemAmmoType);
            outMSG.WriteVariableInt32(s_Player[index].Legs.Value);
            outMSG.WriteVariableInt32(s_Player[index].Legs.ProjectileNumber);
            outMSG.WriteVariableInt32(s_Player[index].Legs.Price);

            outMSG.Write(s_Player[index].Feet.Name);
            outMSG.WriteVariableInt32(s_Player[index].Feet.Sprite);
            outMSG.WriteVariableInt32(s_Player[index].Feet.Damage);
            outMSG.WriteVariableInt32(s_Player[index].Feet.Armor);
            outMSG.WriteVariableInt32(s_Player[index].Feet.Type);
            outMSG.WriteVariableInt32(s_Player[index].Feet.AttackSpeed);
            outMSG.WriteVariableInt32(s_Player[index].Feet.ReloadSpeed);
            outMSG.WriteVariableInt32(s_Player[index].Feet.HealthRestore);
            outMSG.WriteVariableInt32(s_Player[index].Feet.HungerRestore);
            outMSG.WriteVariableInt32(s_Player[index].Feet.HydrateRestore);
            outMSG.WriteVariableInt32(s_Player[index].Feet.Strength);
            outMSG.WriteVariableInt32(s_Player[index].Feet.Agility);
            outMSG.WriteVariableInt32(s_Player[index].Feet.Endurance);
            outMSG.WriteVariableInt32(s_Player[index].Feet.Stamina);
            outMSG.WriteVariableInt32(s_Player[index].Feet.Clip);
            outMSG.WriteVariableInt32(s_Player[index].Feet.MaxClip);
            outMSG.WriteVariableInt32(s_Player[index].Feet.ItemAmmoType);
            outMSG.WriteVariableInt32(s_Player[index].Feet.Value);
            outMSG.WriteVariableInt32(s_Player[index].Feet.ProjectileNumber);
            outMSG.WriteVariableInt32(s_Player[index].Feet.Price);

            s_Server.SendMessage(outMSG, s_Player[index].Connection, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendWeaponsUpdate(NetServer s_Server, Player[] s_Player, int index)
        {
            NetOutgoingMessage outMSG = s_Server.CreateMessage();
            outMSG.Write((byte)PacketTypes.UpdateWeapons);
            //Main weapon
            outMSG.Write(s_Player[index].mainWeapon.Name);
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.Clip);
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.MaxClip);
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
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.ItemAmmoType);
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.Value);
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.ProjectileNumber);
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.Price);

            //Secondary weapon
            outMSG.Write(s_Player[index].offWeapon.Name);
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.Clip);
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.MaxClip);
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
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.ItemAmmoType);
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.Value);
            outMSG.WriteVariableInt32(s_Player[index].offWeapon.ProjectileNumber);
            outMSG.WriteVariableInt32(s_Player[index].mainWeapon.Price);
            s_Server.SendMessage(outMSG, s_Player[index].Connection, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendPlayers(NetServer s_Server, Player[] s_Player)
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
        }

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
        }

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
            outMSG.WriteVariableInt32(s_Item[index].MaxClip);
            outMSG.WriteVariableInt32(s_Item[index].ItemAmmoType);
            outMSG.WriteVariableInt32(s_Item[index].Value);
            outMSG.WriteVariableInt32(s_Item[index].ProjectileNumber);
            outMSG.WriteVariableInt32(s_Item[index].Price);
            s_Server.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
        }

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
                outMSG.WriteVariableInt32(s_Item[i].MaxClip);
                outMSG.WriteVariableInt32(s_Item[i].ItemAmmoType);
                outMSG.WriteVariableInt32(s_Item[i].Value);
                outMSG.WriteVariableInt32(s_Item[i].ProjectileNumber);
                outMSG.WriteVariableInt32(s_Item[i].Price);
            }
            s_Server.SendToAll(outMSG, NetDeliveryMethod.ReliableOrdered);
        }

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
        }

        void SendPoolMapNpcs(NetIncomingMessage incMSG, NetServer s_Server, Map s_Map)
        {
            NetOutgoingMessage outMSG = s_Server.CreateMessage();
            outMSG.Write((byte)PacketTypes.PoolNpcs);
            for (int i = 0; i < 20; i++)
            {
                outMSG.Write(s_Map.r_MapNpc[i].Name);
                outMSG.WriteVariableInt32(s_Map.r_MapNpc[i].X);
                outMSG.WriteVariableInt32(s_Map.r_MapNpc[i].Y);
                outMSG.WriteVariableInt32(s_Map.r_MapNpc[i].Direction);
                outMSG.WriteVariableInt32(s_Map.r_MapNpc[i].Sprite);
                outMSG.WriteVariableInt32(s_Map.r_MapNpc[i].Step);
                outMSG.WriteVariableInt32(s_Map.r_MapNpc[i].Owner);
                outMSG.WriteVariableInt32(s_Map.r_MapNpc[i].Behavior);
                outMSG.WriteVariableInt32(s_Map.r_MapNpc[i].SpawnTime);
                outMSG.WriteVariableInt32(s_Map.r_MapNpc[i].Health);
                outMSG.WriteVariableInt32(s_Map.r_MapNpc[i].MaxHealth);
                outMSG.WriteVariableInt32(s_Map.r_MapNpc[i].Damage);
                outMSG.Write(s_Map.r_MapNpc[i].IsSpawned);
            }
            s_Server.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
        }

        void SendMapNpcs(NetIncomingMessage incMSG, NetServer s_Server, Map s_Map)
        {
            NetOutgoingMessage outMSG = s_Server.CreateMessage();
            outMSG.Write((byte)PacketTypes.MapNpc);
            for (int i = 0; i < 10; i++)
            {
                outMSG.Write(s_Map.m_MapNpc[i].Name);
                outMSG.WriteVariableInt32(s_Map.m_MapNpc[i].X);
                outMSG.WriteVariableInt32(s_Map.m_MapNpc[i].Y);
                outMSG.WriteVariableInt32(s_Map.m_MapNpc[i].Direction);
                outMSG.WriteVariableInt32(s_Map.m_MapNpc[i].Sprite);
                outMSG.WriteVariableInt32(s_Map.m_MapNpc[i].Step);
                outMSG.WriteVariableInt32(s_Map.m_MapNpc[i].Owner);
                outMSG.WriteVariableInt32(s_Map.m_MapNpc[i].Behavior);
                outMSG.WriteVariableInt32(s_Map.m_MapNpc[i].SpawnTime);
                outMSG.WriteVariableInt32(s_Map.m_MapNpc[i].Health);
                outMSG.WriteVariableInt32(s_Map.m_MapNpc[i].MaxHealth);
                outMSG.WriteVariableInt32(s_Map.m_MapNpc[i].Damage);
                outMSG.Write(s_Map.m_MapNpc[i].IsSpawned);
            }
            s_Server.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendPoolNpcData(NetServer s_Server, NetConnection p_Conn, Map s_Map, int npcNum)
        {
            NetOutgoingMessage outMSG = s_Server.CreateMessage();
            outMSG.Write((byte)PacketTypes.PoolNpcData);
            outMSG.WriteVariableInt32(npcNum);
            outMSG.Write(s_Map.r_MapNpc[npcNum].Name);
            outMSG.WriteVariableInt32(s_Map.r_MapNpc[npcNum].X);
            outMSG.WriteVariableInt32(s_Map.r_MapNpc[npcNum].Y);
            outMSG.WriteVariableInt32(s_Map.r_MapNpc[npcNum].Direction);
            outMSG.WriteVariableInt32(s_Map.r_MapNpc[npcNum].Sprite);
            outMSG.WriteVariableInt32(s_Map.r_MapNpc[npcNum].Step);
            outMSG.WriteVariableInt32(s_Map.r_MapNpc[npcNum].Owner);
            outMSG.WriteVariableInt32(s_Map.r_MapNpc[npcNum].Behavior);
            outMSG.WriteVariableInt32(s_Map.r_MapNpc[npcNum].SpawnTime);
            outMSG.WriteVariableInt32(s_Map.r_MapNpc[npcNum].Health);
            outMSG.WriteVariableInt32(s_Map.r_MapNpc[npcNum].MaxHealth);
            outMSG.WriteVariableInt32(s_Map.r_MapNpc[npcNum].Damage);
            outMSG.Write(s_Map.r_MapNpc[npcNum].IsSpawned);

            s_Server.SendMessage(outMSG, p_Conn, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendUpdatePoolNpcLoc(NetServer s_Server, NetConnection p_Conn, Map s_Map, int npcNum)
        {
            NetOutgoingMessage outMSG = s_Server.CreateMessage();
            outMSG.Write((byte)PacketTypes.PoolNpcDirecion);
            outMSG.WriteVariableInt32(npcNum);
            outMSG.WriteVariableInt32(s_Map.r_MapNpc[npcNum].X);
            outMSG.WriteVariableInt32(s_Map.r_MapNpc[npcNum].Y);
            outMSG.WriteVariableInt32(s_Map.r_MapNpc[npcNum].Direction);
            outMSG.WriteVariableInt32(s_Map.r_MapNpc[npcNum].Step);
            s_Server.SendMessage(outMSG, p_Conn, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendMapNpcData(NetServer s_Server, NetConnection p_Conn, Map s_Map, int npcNum)
        {
            NetOutgoingMessage outMSG = s_Server.CreateMessage();
            outMSG.Write((byte)PacketTypes.NpcData);
            outMSG.WriteVariableInt32(npcNum);
            outMSG.Write(s_Map.m_MapNpc[npcNum].Name);
            outMSG.WriteVariableInt32(s_Map.m_MapNpc[npcNum].X);
            outMSG.WriteVariableInt32(s_Map.m_MapNpc[npcNum].Y);
            outMSG.WriteVariableInt32(s_Map.m_MapNpc[npcNum].Direction);
            outMSG.WriteVariableInt32(s_Map.m_MapNpc[npcNum].Sprite);
            outMSG.WriteVariableInt32(s_Map.m_MapNpc[npcNum].Step);
            outMSG.WriteVariableInt32(s_Map.m_MapNpc[npcNum].Owner);
            outMSG.WriteVariableInt32(s_Map.m_MapNpc[npcNum].Behavior);
            outMSG.WriteVariableInt32(s_Map.m_MapNpc[npcNum].SpawnTime);
            outMSG.WriteVariableInt32(s_Map.m_MapNpc[npcNum].Health);
            outMSG.WriteVariableInt32(s_Map.m_MapNpc[npcNum].MaxHealth);
            outMSG.WriteVariableInt32(s_Map.m_MapNpc[npcNum].Damage);
            outMSG.Write(s_Map.m_MapNpc[npcNum].IsSpawned);

            s_Server.SendMessage(outMSG, p_Conn, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendUpdateNpcLoc(NetServer s_Server, NetConnection p_Conn, Map s_Map, int npcNum)
        {
            NetOutgoingMessage outMSG = s_Server.CreateMessage();
            outMSG.Write((byte)PacketTypes.NpcDirection);
            outMSG.WriteVariableInt32(npcNum);
            outMSG.WriteVariableInt32(s_Map.m_MapNpc[npcNum].X);
            outMSG.WriteVariableInt32(s_Map.m_MapNpc[npcNum].Y);
            outMSG.WriteVariableInt32(s_Map.m_MapNpc[npcNum].Direction);
            outMSG.WriteVariableInt32(s_Map.m_MapNpc[npcNum].Step);            

            s_Server.SendMessage(outMSG, p_Conn, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendNpcVitalData(NetServer s_Server, NetConnection p_Conn, Map s_Map, int npcNum)
        {
            NetOutgoingMessage outMSG = s_Server.CreateMessage();
            outMSG.Write((byte)PacketTypes.NpcVitals);
            outMSG.WriteVariableInt32(npcNum);
            outMSG.WriteVariableInt32(s_Map.m_MapNpc[npcNum].Health);
            outMSG.Write(s_Map.m_MapNpc[npcNum].IsSpawned);            

            s_Server.SendMessage(outMSG, p_Conn, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendPoolNpcVitalData(NetServer s_Server, NetConnection p_Conn, Map s_Map, int npcNum)
        {
            NetOutgoingMessage outMSG = s_Server.CreateMessage();
            outMSG.Write((byte)PacketTypes.PoolNpcVitals);
            outMSG.WriteVariableInt32(npcNum);
            outMSG.WriteVariableInt32(s_Map.r_MapNpc[npcNum].Health);
            outMSG.Write(s_Map.r_MapNpc[npcNum].IsSpawned);            

            s_Server.SendMessage(outMSG, p_Conn, NetDeliveryMethod.ReliableOrdered);
        }

        void SendMapItems(NetIncomingMessage incMSG, NetServer s_Server, Map s_Map)
        {
            NetOutgoingMessage outMSG = s_Server.CreateMessage();
            outMSG.Write((byte)PacketTypes.MapItems);
            for (int i = 0; i < 20; i++)
            {
                outMSG.Write(s_Map.mapItem[i].Name);
                outMSG.WriteVariableInt32(s_Map.mapItem[i].X);
                outMSG.WriteVariableInt32(s_Map.mapItem[i].Y);
                outMSG.WriteVariableInt32(s_Map.mapItem[i].Sprite);
                outMSG.WriteVariableInt32(s_Map.mapItem[i].Damage);
                outMSG.WriteVariableInt32(s_Map.mapItem[i].Armor);
                outMSG.WriteVariableInt32(s_Map.mapItem[i].Type);
                outMSG.WriteVariableInt32(s_Map.mapItem[i].HealthRestore);
                outMSG.WriteVariableInt32(s_Map.mapItem[i].HungerRestore);
                outMSG.WriteVariableInt32(s_Map.mapItem[i].HydrateRestore);
                outMSG.WriteVariableInt32(s_Map.mapItem[i].Strength);
                outMSG.WriteVariableInt32(s_Map.mapItem[i].Agility);
                outMSG.WriteVariableInt32(s_Map.mapItem[i].Endurance);
                outMSG.WriteVariableInt32(s_Map.mapItem[i].Stamina);
                outMSG.WriteVariableInt32(s_Map.mapItem[i].Clip);
                outMSG.WriteVariableInt32(s_Map.mapItem[i].MaxClip);
                outMSG.WriteVariableInt32(s_Map.mapItem[i].ItemAmmoType);
                outMSG.WriteVariableInt32(s_Map.mapItem[i].Value);
                outMSG.WriteVariableInt32(s_Map.mapItem[i].ProjectileNumber);
                outMSG.WriteVariableInt32(s_Map.mapItem[i].Price);
                outMSG.Write(s_Map.mapItem[i].IsSpawned);
            }            

            s_Server.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendMapItemData(NetServer s_Server, NetConnection p_Conn, Map s_Map, int itemNum)
        {
            NetOutgoingMessage outMSG = s_Server.CreateMessage();
            outMSG.Write((byte)PacketTypes.MapItemData);
            outMSG.WriteVariableInt32(itemNum);
            outMSG.Write(s_Map.mapItem[itemNum].Name);
            outMSG.WriteVariableInt32(s_Map.mapItem[itemNum].X);
            outMSG.WriteVariableInt32(s_Map.mapItem[itemNum].Y);
            outMSG.WriteVariableInt32(s_Map.mapItem[itemNum].Sprite);
            outMSG.WriteVariableInt32(s_Map.mapItem[itemNum].Damage);
            outMSG.WriteVariableInt32(s_Map.mapItem[itemNum].Armor);
            outMSG.WriteVariableInt32(s_Map.mapItem[itemNum].Type);
            outMSG.WriteVariableInt32(s_Map.mapItem[itemNum].HealthRestore);
            outMSG.WriteVariableInt32(s_Map.mapItem[itemNum].HungerRestore);
            outMSG.WriteVariableInt32(s_Map.mapItem[itemNum].HydrateRestore);
            outMSG.WriteVariableInt32(s_Map.mapItem[itemNum].Strength);
            outMSG.WriteVariableInt32(s_Map.mapItem[itemNum].Agility);
            outMSG.WriteVariableInt32(s_Map.mapItem[itemNum].Endurance);
            outMSG.WriteVariableInt32(s_Map.mapItem[itemNum].Stamina);
            outMSG.WriteVariableInt32(s_Map.mapItem[itemNum].Clip);
            outMSG.WriteVariableInt32(s_Map.mapItem[itemNum].MaxClip);
            outMSG.WriteVariableInt32(s_Map.mapItem[itemNum].ItemAmmoType);
            outMSG.WriteVariableInt32(s_Map.mapItem[itemNum].Value);
            outMSG.WriteVariableInt32(s_Map.mapItem[itemNum].ProjectileNumber);
            outMSG.WriteVariableInt32(s_Map.mapItem[itemNum].Price);
            outMSG.Write(s_Map.mapItem[itemNum].IsSpawned);            

            s_Server.SendMessage(outMSG, p_Conn, NetDeliveryMethod.ReliableOrdered);
        }

        void SendErrorMessage(string message, string caption, NetIncomingMessage incMSG, NetServer s_Server)
        {
            NetOutgoingMessage outMSG = s_Server.CreateMessage();
            outMSG.Write((byte)PacketTypes.ErrorMessage);
            outMSG.Write(message);
            outMSG.Write(caption);            
            s_Server.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
        }

        void SendMapData(NetIncomingMessage incMSG, NetServer s_Server, Map s_Map, Player[] s_Player)
        {
            NetOutgoingMessage outMSG = s_Server.CreateMessage(67529);
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
        }
        #endregion

        #region Processing and Checking Voids
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
                        SendPlayers(s_Server, s_Player);
                        SendMapNpcs(incMSG, s_Server, s_Map[mapNum]);
                        SendPoolMapNpcs(incMSG, s_Server, s_Map[mapNum]);
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
                    SendPlayers(s_Server, s_Player);
                    Console.WriteLine("Command:" + msg + " Spritenum: " + spriteNum);
                    return;
                }
            }
        }

        bool AlreadyLogged(string name, Player[] s_Player)
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

        int OpenSlot(Player[] s_Player)
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

        int GetPlayerConnection(NetIncomingMessage incMSG, Player[] s_Player)
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

        bool CheckPassword(string name, string pass)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
            {
                conn.Open();
                string sql;

                sql = "SELECT * FROM `PLAYERS` WHERE NAME = '" + name + "'";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            string compare = read["PASSWORD"].ToString();

                            if (pass == compare)
                            {
                                return true;
                            }
                        }
                        return false;
                    }
                }
            }
        }

        bool AccountExist(string name)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
            {
                conn.Open();
                string sql;

                sql = "SELECT * FROM `PLAYERS` WHERE NAME = '" + name + "'";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            if (read["NAME"].ToString() == name)
                            {
                                return true;
                            }
                        }
                        return false;
                    }
                }
            }
        }
        #endregion
    }

    public enum PacketTypes : byte
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
        AttackNpcProj,
        UpdatePlayerStats,
        PoolNpcs,
        PoolNpcData,
        PlayerInv,
        MapItems,
        MapItemData,
        ItemPickup,
        UnequipItem,
        EquipItem,
        DropItem,
        PlayerEquip,
        NpcDirection,
        PoolNpcDirecion,
        NpcVitals,
        PoolNpcVitals
    }
}
