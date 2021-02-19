using Lidgren.Network;
using System;
using System.Data.SqlClient;
using System.Threading;
using static System.Convert;
using static SabertoothServer.Server;
using AccountKeyGenClass;
using static SabertoothServer.Globals;

namespace SabertoothServer
{
    public static class HandleData
    {
        public static void HandleDataMessage()
        {
            NetIncomingMessage incMSG;

            if ((incMSG = SabertoothServer.netServer.ReadMessage()) != null)
            {
                switch (incMSG.MessageType)
                {
                    case NetIncomingMessageType.DiscoveryRequest:
                        HandleDiscoveryRequest(incMSG);
                        break;

                    case NetIncomingMessageType.ConnectionApproval:
                        HandleConnectionApproval(incMSG);
                        break;

                    case NetIncomingMessageType.Data:
                        switch (incMSG.ReadByte())
                        {
                            case (byte)PacketTypes.Register:
                                HandleRegisterRequest(incMSG);
                                break;
                            case (byte)PacketTypes.Login:
                                HandleLoginRequest(incMSG);
                                break;

                            case (byte)PacketTypes.ChatMessage:
                                HandleChatMessage(incMSG);
                                break;

                            case (byte)PacketTypes.MoveData:
                                HandleMoveData(incMSG);
                                break;

                            case (byte)PacketTypes.UpdateDirection:
                                HandleDirectionData(incMSG);
                                break;                                

                            case (byte)PacketTypes.ItemPickup:
                                HandleItemPickup(incMSG);
                                break;

                            case (byte)PacketTypes.UnequipItem:
                                HandleUnequipItem(incMSG);
                                break;

                            case (byte)PacketTypes.EquipItem:
                                HandleEquipItem(incMSG);
                                break;

                            case (byte)PacketTypes.DropItem:
                                HandleDropItem(incMSG);
                                break;

                            case (byte)PacketTypes.Interaction:
                                HandleInteraction(incMSG);
                                break;

                            case (byte)PacketTypes.BuyItem:
                                HandleBuyItem(incMSG);
                                break;

                            case (byte)PacketTypes.SellItem:
                                HandleSellItem(incMSG);
                                break;

                            case (byte)PacketTypes.NextChat:
                                HandleNextChat(incMSG);
                                break;

                            case (byte)PacketTypes.WithdrawItem:
                                HandleWithdrawItem(incMSG);
                                break;

                            case (byte)PacketTypes.DepositItem:
                                HandleDepositItem(incMSG);
                                break;

                            case (byte)PacketTypes.TakeChestItem:
                                HandleTakeChestItem(incMSG);
                                break;

                            case (byte)PacketTypes.PlayTime:
                                HandlePlayerTime(incMSG);
                                break;

                            case (byte)PacketTypes.AccountKey:
                                HandleActivationKey(incMSG);
                                break;

                            case (byte)PacketTypes.PlayerWarp:
                                HandlePlayerWarp(incMSG);
                                break;

                            case (byte)PacketTypes.MeleeAttack:
                                HandleMeleeAttack(incMSG);
                                break;

                            case (byte)PacketTypes.SendInvSwap:
                                HandleInvSwap(incMSG);
                                break;

                            case (byte)PacketTypes.SendBankSwap:
                                HandleBankSwap(incMSG);
                                break;

                            case (byte)PacketTypes.UpdateHotBar:
                                HandleUpdateHotBar(incMSG);
                                break;

                            case (byte)PacketTypes.UseHotBar:
                                HandleHotbarUse(incMSG);
                                break;

                            default:
                                Console.WriteLine("Unknown packet header.");
                                break;
                        }
                        break;

                    case NetIncomingMessageType.StatusChanged:
                        HandleStatusChange(incMSG);
                        break;
                }

                //uncomment if you want to debug
                //Console.WriteLine("Packet Size: " + incMSG.LengthBytes + " Bytes, " + incMSG.LengthBits + " bits");
            }
            SabertoothServer.netServer.Recycle(incMSG);
        }

        #region Handle Incoming Data
        static void HandleHotbarUse(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();
            int hotbarslot = incMSG.ReadVariableInt32();

            if (players[index].hotBar[hotbarslot].InvNumber > -1)
            {
                players[index].UseItem(index, players[index].hotBar[hotbarslot].InvNumber, hotbarslot);
            }
        }

        static void HandleUpdateHotBar(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();
            int slot = incMSG.ReadVariableInt32();
            int hotbarslot = incMSG.ReadVariableInt32();

            players[index].hotBar[hotbarslot].InvNumber = slot;
            SendPlayerHotBar(index);
        }

        static void HandleBankSwap(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();
            int oldSlot = incMSG.ReadVariableInt32();
            int newslot = incMSG.ReadVariableInt32();
            Item temp = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, false);

            temp = players[index].Bank[newslot];
            players[index].Bank[newslot] = players[index].Bank[oldSlot];
            players[index].Bank[oldSlot] = temp;
            SendPlayerBank(index);
        }

        static void HandleInvSwap(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();
            int oldSlot = incMSG.ReadVariableInt32();
            int newslot = incMSG.ReadVariableInt32();
            Item temp = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, false);

            temp = players[index].Backpack[newslot];
            players[index].Backpack[newslot] = players[index].Backpack[oldSlot];
            players[index].Backpack[oldSlot] = temp;
            SendPlayerInv(index);
        }

        static void HandleMeleeAttack(NetIncomingMessage incMSG)
        {
            int npcNum = incMSG.ReadVariableInt32();
            int index = incMSG.ReadVariableInt32();
            int type = incMSG.ReadVariableInt32();
            int map = players[index].Map;
            int direction = players[index].Direction;
            int damage = players[index].OffHand.Damage;

            if (type == 0)
            {
                if (!maps[map].m_MapNpc[npcNum].IsSpawned) { return; }

                maps[map].CreateBloodSplat(map, maps[map].m_MapNpc[npcNum].X, maps[map].m_MapNpc[npcNum].Y);
                bool updatePlayer = maps[map].m_MapNpc[npcNum].DamageNpc(players[index], maps[map], damage);


                switch (direction)
                {
                    case (int)Directions.Up:
                        maps[map].m_MapNpc[npcNum].Y -= 1;
                        break;

                    case (int)Directions.Down:
                        maps[map].m_MapNpc[npcNum].Y += 1;
                        break;

                    case (int)Directions.Left:
                        maps[map].m_MapNpc[npcNum].X -= 1;
                        break;

                    case (int)Directions.Right:
                        maps[map].m_MapNpc[npcNum].X += -1;
                        break;
                }

                for (int p = 0; p < MAX_PLAYERS; p++)
                {
                    if (players[p].Connection != null && map == players[p].Map)
                    {
                        SendNpcVitalData(players[p].Connection, map, npcNum);
                    }
                }
                if (updatePlayer) { SendUpdatePlayerStats(index); }
            }
            else
            {
                if (!maps[map].r_MapNpc[npcNum].IsSpawned) { return; }

                maps[map].CreateBloodSplat(map, maps[map].r_MapNpc[npcNum].X, maps[map].r_MapNpc[npcNum].Y);
                bool updatePlayer = maps[map].r_MapNpc[npcNum].DamageNpc(players[index], maps[map], damage);


                switch (direction)
                {
                    case (int)Directions.Up:
                        maps[map].r_MapNpc[npcNum].Y -= 1;
                        break;

                    case (int)Directions.Down:
                        maps[map].r_MapNpc[npcNum].Y += 1;
                        break;

                    case (int)Directions.Left:
                        maps[map].r_MapNpc[npcNum].X -= 1;
                        break;

                    case (int)Directions.Right:
                        maps[map].r_MapNpc[npcNum].X += -1;
                        break;
                }

                for (int p = 0; p < MAX_PLAYERS; p++)
                {
                    if (players[p].Connection != null && map == players[p].Map)
                    {
                        SendPoolNpcVitalData(players[p].Connection, map, npcNum);
                    }
                }
                if (updatePlayer) { SendUpdatePlayerStats(index); }
            }
        }

        static void HandlePlayerWarp(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();
            int map = players[index].Map;
            int x = players[index].X + OFFSET_X;
            int y = players[index].Y + OFFSET_Y;
            int warpMap = maps[map].Ground[x, y].Map - 1;
            int warpX = maps[map].Ground[x, y].MapX - OFFSET_X;
            int warpY = maps[map].Ground[x, y].MapY - OFFSET_Y;

            Logging.WriteMessageLog("Warp detected, Index: " + index + ", Map: " + warpMap + ", X: " + warpX + ", Y: " + warpY);

            if (warpMap < 0) { Logging.WriteMessageLog("Invalid map number upon warp: " + warpMap); return; }

            players[index].X = warpX;
            players[index].Y = warpY;
            players[index].Map = warpMap;

            SendPlayerData(incMSG, index);
            SendPlayers();
            SendMapData(incMSG, warpMap);
            SendMapNpcs(incMSG, warpMap);
            SendPoolMapNpcs(incMSG, warpMap);
            SendMapItems(incMSG, warpMap);
        }

        static void HandleActivationKey(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();
            string key = incMSG.ReadString();

            if (key == players[index].AccountKey)
            {
                players[index].Active = "Y";
                players[index].UpdateAccountStatusInDatabase();
                Logging.WriteMessageLog("Account activated! Key: " + key + " Account Name: " + players[index].Name);
                int currentMap = players[index].Map;
                Console.WriteLine("Account login by: " + players[index].Name + ", " + players[index].Pass);
                SendAcceptLogin(index);
                SendPlayerData(incMSG, index);
                SendPlayers();
                SendPlayerInv(index);
                SendPlayerBank(index);
                SendPlayerHotBar(index);
                SendPlayerEquipment(index);
                SendPlayerQuestList(index);
                SendNpcs(incMSG);
                SendItems(incMSG);                
                SendShops(incMSG);
                SendChats(incMSG);
                SendQuests(incMSG);
                SendMapData(incMSG, currentMap);
                SendMapNpcs(incMSG, currentMap);
                SendPoolMapNpcs(incMSG, currentMap);
                SendMapItems(incMSG, currentMap);
                SendChests(incMSG);
                SendDateAndTime(incMSG, index);
                players[index].UpdateLastLogged();
                Console.WriteLine("Data sent to " + players[index].Name + ", IP: " + incMSG.SenderConnection);
                string welcomeMsg = players[index].Name + " has joined Sabertooth!";
                SendServerMessageToAll(welcomeMsg);
            }
            else
            {
                Console.WriteLine("Invalid activation key..");
                SendErrorMessage("Invalid activation key! Please try again.", "Activation Key", incMSG);
            }
        }

        static void HandleTakeChestItem(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();
            int chestSlot = incMSG.ReadVariableInt32();
            int chestIndex = incMSG.ReadVariableInt32();
            int itemNum = chests[chestIndex].ChestItem[chestSlot].ItemNum - 1;

            chests[chestIndex].TakeItemFromChest(incMSG, itemNum, chestSlot, index);
        }

        static void HandleNextChat(NetIncomingMessage incMSG)
        {
            int optionSlot = incMSG.ReadVariableInt32();
            int chatIndex = incMSG.ReadVariableInt32();
            int playerIndex = incMSG.ReadVariableInt32();
            int index = chatIndex;
            if (chatIndex > 0) { index = chatIndex - 1; }
            int nextChat = chats[index].NextChat[optionSlot];
            int shop = chats[index].ShopNum;

            if (chats[index].Option[optionSlot] == "Exit")
            {
                SendCloseChat(incMSG);
                return;
            }

            if (chats[index].Option[optionSlot] == "Bank")
            {
                SendOpenBank(incMSG);
                SendCloseChat(incMSG);
                return;
            }

            if (chats[index].Option[optionSlot] == "Accept Quest")
            {
                if (players[playerIndex].CheckPlayerHasQuest(chats[index].QuestNum)) { SendCloseChat(incMSG); return; }

                int slot = players[playerIndex].FindOpenQuestListSlot();

                if (slot < MAX_PLAYER_QUEST_LIST)
                {
                    players[playerIndex].QuestList[slot] = chats[index].QuestNum;
                    players[playerIndex].QuestStatus[slot] = (int)QuestStatus.Inprogress;
                    SendPlayerQuestList(playerIndex);
                    SendCloseChat(incMSG);
                }
                return;
            }

            if (nextChat > 0)
            {
                SendOpenNextChat(incMSG, nextChat);
            }
            else if (shop > 0)
            {
                SendOpenShop(incMSG, shop - 1);
                SendCloseChat(incMSG);
            }
            else
            {
                SendCloseChat(incMSG);
            }

            for (int i = 0; i < 3; i++)
            {
                if (chats[index].ItemNum[i] > 0)
                {
                    int openSlot = players[playerIndex].FindOpenInvSlot(players[playerIndex].Backpack);

                    if (openSlot < MAX_INV_SLOTS)
                    {
                        players[playerIndex].Backpack[openSlot] = items[chats[index].ItemNum[i] - 1];
                        if (chats[index].ItemVal[i] > 1)
                        {
                            players[playerIndex].Backpack[openSlot].Value = chats[index].ItemVal[i];
                        }
                        else { players[playerIndex].Backpack[openSlot].Value = 1; }
                        SendPlayerInv(playerIndex);
                    }
                }
            }

            if (chats[index].Money > 0)
            {
                players[playerIndex].Wallet += chats[index].Money;
                SendUpdatePlayerStats(playerIndex);
            }
        }

        static void HandleSellItem(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();
            int slot = incMSG.ReadVariableInt32();
            int shopNum = incMSG.ReadVariableInt32();

            shops[shopNum].SellShopItem(index, slot);
        }

        static void HandleBuyItem(NetIncomingMessage incMSG)
        {
            int shopSlot = incMSG.ReadVariableInt32();
            int index = incMSG.ReadVariableInt32();
            int shopIndex = incMSG.ReadVariableInt32();
            int itemNum = shops[shopIndex].shopItem[shopSlot].ItemNum - 1;

            shops[shopIndex].BuyShopItem(itemNum, shopSlot, index);
        }

        static void HandleDepositItem(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();
            int slot = incMSG.ReadVariableInt32();
            int value = incMSG.ReadVariableInt32();

            players[index].DepositItem(index, slot, value);
        }

        public static void HandleWithdrawItem(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();
            int slot = incMSG.ReadVariableInt32();
            int amount = incMSG.ReadVariableInt32();

            players[index].WithdrawItem(index, slot, amount);
        }

        static void HandleInteraction(NetIncomingMessage incMSG)
        {
            int type = incMSG.ReadVariableInt32();
            int pIndex = incMSG.ReadVariableInt32();
            int mapIndex = incMSG.ReadVariableInt32();
            int index = incMSG.ReadVariableInt32();

            if (type == 0)
            {
                if (maps[mapIndex].m_MapNpc[index].ShopNum > 0) { SendOpenShop(incMSG, maps[mapIndex].m_MapNpc[index].ShopNum - 1); }
                if (maps[mapIndex].m_MapNpc[index].ChatNum > 0) { SendOpenChat(incMSG, maps[mapIndex].m_MapNpc[index].ChatNum); }
            }
            else if (type == 1)
            {
                bool change = false;
                if (chests[index].Money > 0)
                {
                    players[pIndex].Wallet += chests[index].Money;
                    SendServerMessageTo(players[pIndex].Connection, "This chest has granted you " + chests[index].Money + " dollars!");
                    chests[index].Money = 0;
                    change = true;
                }
                if (chests[index].Experience > 0)
                {
                    players[pIndex].Experience += chests[index].Experience;
                    players[pIndex].CheckPlayerLevelUp();
                    SendServerMessageTo(players[pIndex].Connection, "This chest has granted you " + chests[index].Experience + " experience!");
                    chests[index].Experience = 0;
                    change = true;
                }

                SendOpenChest(incMSG, index);
                if (change) { SendUpdatePlayerStats(pIndex); }
            }
        }

        static void HandleDropItem(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();
            int slot = incMSG.ReadVariableInt32();
            int mapNum = players[index].Map;

            players[index].DropItem(index, slot, mapNum);
        }

        static void HandleEquipItem(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();
            int slot = incMSG.ReadVariableInt32();

            players[index].EquipItem(index, slot);
        }

        static void HandleUnequipItem(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();
            int equip = incMSG.ReadVariableInt32();

            players[index].UnequipItem(index, equip);
        }

        static void HandleRequestInvUpdate(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();

            SendPlayerInv(index);
        }

        static void HandleItemPickup(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();
            int pMap = players[index].Map;

            players[index].CheckPickup(index, pMap);
        }        

        static void HandleMoveData(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();
            int x = incMSG.ReadVariableInt32();
            int y = incMSG.ReadVariableInt32();
            int direction = incMSG.ReadVariableInt32();
            int aimdirection = incMSG.ReadVariableInt32();
            int step = incMSG.ReadVariableInt32();
            int map = players[index].Map;

            players[index].AimDirection = aimdirection;

            if (step == players[index].Step) { return; }
            if (x == players[index].X && y == players[index].Y) { return; }

            players[index].X = x;
            players[index].Y = y;
            players[index].Direction = direction;
            players[index].Step = step;

            for (int i = 0; i < 5; i++)
            {
                if (players[i].Connection != null && players[i].Map == map)
                {
                    SendUpdateMovementData(players[i].Connection, index, x, y, direction, aimdirection, step);
                }
            }
        }
        
        static void HandleDirectionData(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();
            int direction = incMSG.ReadVariableInt32();
            int aimdirection = incMSG.ReadVariableInt32();

            players[index].AimDirection = aimdirection;
            players[index].Direction = direction;

            for (int i = 0; i < 5; i++)
            {
                if (players[i].Connection != null && players[i].Map == players[index].Map)
                {
                    SendUpdateDirection(players[i].Connection, index, direction, aimdirection);
                }
            }
        }

        static void HandleDiscoveryRequest(NetIncomingMessage incMSG)
        {
            Logging.WriteMessageLog("Client discovered @ " + incMSG.SenderEndPoint.ToString());
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write("Sabertooth Server");
            SabertoothServer.netServer.SendDiscoveryResponse(outMSG, incMSG.SenderEndPoint);
        }

        static void HandleConnectionApproval(NetIncomingMessage incMSG)
        {
            if (incMSG.ReadByte() == (byte)PacketTypes.Connection)
            {
                string connect = incMSG.ReadString();
                if (connect == "sabertooth")
                {
                    incMSG.SenderConnection.Approve();
                    Thread.Sleep(500);
                    NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
                    outMSG.Write((byte)PacketTypes.Connection);
                    SabertoothServer.netServer.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
                }
                else
                {
                    incMSG.SenderConnection.Deny();
                }
            }
        }

        static void HandleRegisterRequest(NetIncomingMessage incMSG)
        {
            string username = incMSG.ReadString();
            string password = incMSG.ReadString();
            string email = incMSG.ReadString();

            if (!AlreadyLogged(username))
            {
                if (!AccountExist(username))
                {
                    int i = OpenSlot();
                    if (i < 5)
                    {
                        players[i] = new Player(username, password, email, PLAYER_START_X, PLAYER_START_Y, 0, 0, 0, 1, 100, 100, 100, 100, 0, 0, 0, 1, 1, 1, 1, 1, 0, incMSG.SenderConnection);
                        players[i].CreatePlayerInDatabase();
                        Console.WriteLine("Account created, " + username + ", " + password);
                        SendErrorMessage("Account Created! Please login to play!", "Account Created", incMSG);
                        string subject = "Sabertooth Account Activation Key";
                        string body = "Welcome to Sabertooth!\n\nBelow is your key which must be entered upon first login to activate your account. Enjoy!\n\nActivation Key: " + players[i].AccountKey;
                        Email.SendActivationEmail(email, subject, body, SMTP_IP_ADDRESS, SMTP_SERVER_PORT, SMTP_USER_CREDS, SMTP_PASS_CREDS);
                        ClearSlot(incMSG.SenderConnection);
                    }
                    else
                    {
                        Console.WriteLine("Server Full!");
                        SendErrorMessage("Server is full. Please try again later.", "Server Full", incMSG);
                    }
                }
                else
                {
                    Console.WriteLine("Account already exists!");
                    SendErrorMessage("Account already exists! Please choose another username.", "Account Exists", incMSG);
                }
            }
            else
            {
                Console.WriteLine("Attempted multi-login!");
                SendErrorMessage("Account already logged in. If this is an error, please try again.", "Account Logged", incMSG);
            }
        }

        static void HandleLoginRequest(NetIncomingMessage incMSG)
        {
            string username = incMSG.ReadString();
            string password = incMSG.ReadString();
            string version = incMSG.ReadString();

            if (version != sVersion) { SendErrorMessage("An update is available, starting updater...", "Update", incMSG); return; }

            if (!AlreadyLogged(username))
            {
                if (AccountExist(username) && CheckPassword(username, password))
                {
                    int i = OpenSlot();
                    if (i < 5)
                    {
                        players[i] = new Player(username, password, incMSG.SenderConnection);
                        players[i].LoadPlayerFromDatabase();
                        if (!players[i].IsAccountActive()) { SendActivationRequest(incMSG, i); return; }
                        int currentMap = players[i].Map;
                        Console.WriteLine("Account login by: " + username + ", " + password);
                        SendAcceptLogin(i);
                        SendPlayerData(incMSG, i);
                        SendPlayers();
                        SendPlayerInv(i);
                        SendPlayerBank(i);
                        SendPlayerHotBar(i);
                        SendPlayerEquipment(i);
                        SendPlayerQuestList(i);
                        SendNpcs(incMSG);
                        SendItems(incMSG);                        
                        SendShops(incMSG);
                        SendChats(incMSG);
                        SendQuests(incMSG);
                        SendMapData(incMSG, currentMap);
                        SendMapNpcs(incMSG, currentMap);
                        SendPoolMapNpcs(incMSG, currentMap);
                        SendMapItems(incMSG, currentMap);
                        SendChests(incMSG);
                        SendDateAndTime(incMSG, i);
                        players[i].UpdateLastLogged();
                        Console.WriteLine("Data sent to " + username + ", IP: " + incMSG.SenderConnection);
                        string welcomeMsg = username + " has joined Sabertooth!";
                        SendServerMessageToAll(welcomeMsg);
                    }
                    else
                    {
                        Console.WriteLine("Server is full...");
                        SendErrorMessage("Server is full. Please try again later.", "Server Full", incMSG);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid login by: " + username + ", " + password);
                    SendErrorMessage("Invalid username or password.", "Invalid Login", incMSG);
                }
            }
            else
            {
                Console.WriteLine("Multiple login attempt by: " + username + ", " + password);
                SendErrorMessage("Account already logged in. If this is an error, please try again.", "Account Logged", incMSG);
            }
        }
        
        static void HandleChatMessage(NetIncomingMessage incMSG)
        {
            string msg = incMSG.ReadString();
            //Check for an admin command
            if (msg.Substring(0, 1) == "/")
            {
                CheckCommand(msg, GetPlayerConnection(incMSG), incMSG);
                return;
            }
            else
            {
                string name = players[GetPlayerConnection(incMSG)].Name;
                string finalMsg = name + ": " + msg;
                NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
                outMSG.Write((byte)PacketTypes.ChatMessage);
                outMSG.Write(finalMsg);
                SabertoothServer.netServer.SendToAll(outMSG, NetDeliveryMethod.ReliableOrdered);
                Logging.WriteLog(finalMsg, "Chat");
                Console.WriteLine(finalMsg);
            }
        }

        static void HandleStatusChange(NetIncomingMessage incMSG)
        {
            Logging.WriteMessageLog(incMSG.SenderConnection.ToString() + " status changed. " + incMSG.SenderConnection.Status);
            if (incMSG.SenderConnection.Status == NetConnectionStatus.Disconnected || incMSG.SenderConnection.Status == NetConnectionStatus.Disconnecting)
            {
                Logging.WriteMessageLog("Disconnected clearing data...");
                Logging.WriteMessageLog("Saving player...");
                SavePlayers();
                ClearSlot(incMSG.SenderConnection);
                SendPlayers();
                Logging.WriteMessageLog("Player saved!");
            }
        }

        static void HandlePlayerTime(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();
            int days = incMSG.ReadVariableInt32();
            int hours = incMSG.ReadVariableInt32();
            int minutes = incMSG.ReadVariableInt32();
            int seconds = incMSG.ReadVariableInt32();

            players[index].PlayDays = days;
            players[index].PlayHours = hours;
            players[index].PlayMinutes = minutes;
            players[index].PlaySeconds = seconds;
        }
        #endregion

        #region Send Outgoing Data
        static void SendDateAndTime(NetIncomingMessage incMSG, int index)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.DateandTime);
            outMSG.WriteVariableInt32(index);
            outMSG.WriteVariableInt32(worldTime.g_Year);
            outMSG.WriteVariableInt32(worldTime.g_Month);
            outMSG.WriteVariableInt32(worldTime.g_DayOfWeek);
            outMSG.WriteVariableInt32(worldTime.g_Hour);
            outMSG.WriteVariableInt32(worldTime.g_Minute);
            outMSG.WriteVariableInt32(worldTime.g_Second);

            SabertoothServer.netServer.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
        }

        public static void UpdatePlayersWithTimeChange(NetConnection conn, int index)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.DateandTime);
            outMSG.WriteVariableInt32(index);
            outMSG.WriteVariableInt32(worldTime.g_Year);
            outMSG.WriteVariableInt32(worldTime.g_Month);
            outMSG.WriteVariableInt32(worldTime.g_DayOfWeek);
            outMSG.WriteVariableInt32(worldTime.g_Hour);
            outMSG.WriteVariableInt32(worldTime.g_Minute);
            outMSG.WriteVariableInt32(worldTime.g_Second);

            SabertoothServer.netServer.SendMessage(outMSG, conn, NetDeliveryMethod.ReliableOrdered);
        }

        static void SendOpenChest(NetIncomingMessage incMSG, int index)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.OpenChest);
            outMSG.WriteVariableInt32(index);
            SabertoothServer.netServer.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
        }

        static void SendCloseChat(NetIncomingMessage incMSG)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.CloseChat);
            SabertoothServer.netServer.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
        }

        static void SendOpenNextChat(NetIncomingMessage incMSG, int index)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.NextChat);
            outMSG.WriteVariableInt32(index);
            SabertoothServer.netServer.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
        }

        static void SendOpenChat(NetIncomingMessage incMSG, int index)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.OpenChat);
            outMSG.WriteVariableInt32(index);
            SabertoothServer.netServer.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
        }

        static void SendOpenShop(NetIncomingMessage incMSG, int index)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.OpenShop);
            outMSG.WriteVariableInt32(index);
            SabertoothServer.netServer.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
        }

        static void SendOpenBank(NetIncomingMessage incMSG)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.OpenBank);
            SabertoothServer.netServer.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
        }

        public static void SendServerMessageToAll(string message)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.ChatMessage);
            outMSG.Write(message);
            SabertoothServer.netServer.SendToAll(outMSG, NetDeliveryMethod.ReliableOrdered);
        } 

        public static void SendServerMessageTo(NetConnection conn, string message)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.ChatMessage);
            outMSG.Write(message);
            SabertoothServer.netServer.SendMessage(outMSG, conn, NetDeliveryMethod.ReliableOrdered);
        }

        static void SendUpdateDirection(NetConnection playerConn, int index, int direction, int aimdirection)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.DirData);
            outMSG.WriteVariableInt32(index);
            outMSG.WriteVariableInt32(direction);
            outMSG.WriteVariableInt32(aimdirection);
            SabertoothServer.netServer.SendMessage(outMSG, playerConn, NetDeliveryMethod.Unreliable);
        }

        static void SendUpdateMovementData(NetConnection playerConn, int index, int x, int y, int direction, int aimdirection, int step)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.UpdateMoveData);
            outMSG.WriteVariableInt32(index);
            outMSG.WriteVariableInt32(x);
            outMSG.WriteVariableInt32(y);
            outMSG.WriteVariableInt32(direction);
            outMSG.WriteVariableInt32(aimdirection);
            outMSG.WriteVariableInt32(step);

            SabertoothServer.netServer.SendMessage(outMSG, playerConn, NetDeliveryMethod.Unreliable);
        }

        public static void SendUpdateHealthData(int index, int health)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.HealthData);
            outMSG.WriteVariableInt32(index);
            outMSG.WriteVariableInt32(health);

            SabertoothServer.netServer.SendToAll(outMSG, NetDeliveryMethod.ReliableOrdered);
        }

        public static void SendUpdateVitalData(int index, string vitalName, int vital)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.VitalLoss);
            outMSG.WriteVariableInt32(index);
            outMSG.Write(vitalName);
            outMSG.WriteVariableInt32(vital);

            SabertoothServer.netServer.SendToAll(outMSG, NetDeliveryMethod.ReliableOrdered);
        }

        static void SendAcceptLogin(int index)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.Login);
            SabertoothServer.netServer.SendMessage(outMSG, players[index].Connection, NetDeliveryMethod.ReliableOrdered);
        }

        static void SendPlayerData(NetIncomingMessage incMSG, int index)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.PlayerData);
            outMSG.Write(index);
            outMSG.Write(players[index].Name);
            outMSG.WriteVariableInt32(players[index].X);
            outMSG.WriteVariableInt32(players[index].Y);
            outMSG.WriteVariableInt32(players[index].Map);
            outMSG.WriteVariableInt32(players[index].Direction);
            outMSG.WriteVariableInt32(players[index].AimDirection);
            outMSG.WriteVariableInt32(players[index].Sprite);
            outMSG.WriteVariableInt32(players[index].Level);            
            outMSG.WriteVariableInt32(players[index].Health);
            outMSG.WriteVariableInt32(players[index].MaxHealth);
            outMSG.WriteVariableInt32(players[index].Mana);
            outMSG.WriteVariableInt32(players[index].MaxMana);
            outMSG.WriteVariableInt32(players[index].Experience);
            outMSG.WriteVariableInt32(players[index].Wallet);
            outMSG.WriteVariableInt32(players[index].Armor);
            outMSG.WriteVariableInt32(players[index].Strength);
            outMSG.WriteVariableInt32(players[index].Agility);
            outMSG.WriteVariableInt32(players[index].Intelligence);
            outMSG.WriteVariableInt32(players[index].Stamina);
            outMSG.WriteVariableInt32(players[index].Energy);
            outMSG.WriteVariableInt32(players[index].LightRadius);
            outMSG.WriteVariableInt32(players[index].PlayDays);
            outMSG.WriteVariableInt32(players[index].PlayHours);
            outMSG.WriteVariableInt32(players[index].PlayMinutes);
            outMSG.WriteVariableInt32(players[index].PlaySeconds);

            //Main weapon
            outMSG.Write(players[index].MainHand.Name);
            outMSG.WriteVariableInt32(players[index].MainHand.Sprite);
            outMSG.WriteVariableInt32(players[index].MainHand.Damage);
            outMSG.WriteVariableInt32(players[index].MainHand.Armor);
            outMSG.WriteVariableInt32(players[index].MainHand.Type);
            outMSG.WriteVariableInt32(players[index].MainHand.AttackSpeed);
            outMSG.WriteVariableInt32(players[index].MainHand.HealthRestore);
            outMSG.WriteVariableInt32(players[index].MainHand.ManaRestore);
            outMSG.WriteVariableInt32(players[index].MainHand.Strength);
            outMSG.WriteVariableInt32(players[index].MainHand.Agility);
            outMSG.WriteVariableInt32(players[index].MainHand.Intelligence);
            outMSG.WriteVariableInt32(players[index].MainHand.Energy);
            outMSG.WriteVariableInt32(players[index].MainHand.Stamina);
            outMSG.WriteVariableInt32(players[index].MainHand.Value);
            outMSG.WriteVariableInt32(players[index].MainHand.Price);
            outMSG.WriteVariableInt32(players[index].MainHand.Rarity);
            outMSG.WriteVariableInt32(players[index].MainHand.CoolDown);
            outMSG.WriteVariableInt32(players[index].MainHand.AddMaxHealth);
            outMSG.WriteVariableInt32(players[index].MainHand.AddMaxMana);
            outMSG.WriteVariableInt32(players[index].MainHand.BonusXP);
            outMSG.WriteVariableInt32(players[index].MainHand.SpellNum);
            outMSG.Write(players[index].MainHand.Stackable);

            //Secondary weapon
            outMSG.Write(players[index].OffHand.Name);
            outMSG.WriteVariableInt32(players[index].OffHand.Sprite);
            outMSG.WriteVariableInt32(players[index].OffHand.Damage);
            outMSG.WriteVariableInt32(players[index].OffHand.Armor);
            outMSG.WriteVariableInt32(players[index].OffHand.Type);
            outMSG.WriteVariableInt32(players[index].OffHand.AttackSpeed);
            outMSG.WriteVariableInt32(players[index].OffHand.HealthRestore);
            outMSG.WriteVariableInt32(players[index].OffHand.ManaRestore);
            outMSG.WriteVariableInt32(players[index].OffHand.Strength);
            outMSG.WriteVariableInt32(players[index].OffHand.Agility);
            outMSG.WriteVariableInt32(players[index].OffHand.Intelligence);
            outMSG.WriteVariableInt32(players[index].OffHand.Energy);
            outMSG.WriteVariableInt32(players[index].OffHand.Stamina);            
            outMSG.WriteVariableInt32(players[index].OffHand.Value);           
            outMSG.WriteVariableInt32(players[index].OffHand.Price);
            outMSG.WriteVariableInt32(players[index].OffHand.Rarity);
            outMSG.WriteVariableInt32(players[index].OffHand.CoolDown);
            outMSG.WriteVariableInt32(players[index].OffHand.AddMaxHealth);
            outMSG.WriteVariableInt32(players[index].OffHand.AddMaxMana);
            outMSG.WriteVariableInt32(players[index].OffHand.BonusXP);
            outMSG.WriteVariableInt32(players[index].OffHand.SpellNum);
            outMSG.Write(players[index].OffHand.Stackable);

            SabertoothServer.netServer.SendMessage(outMSG, players[index].Connection, NetDeliveryMethod.ReliableOrdered);
        }

        public static void SendUpdatePlayerStats(int index)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.UpdatePlayerStats);
            //outMSG.Write(index);
            outMSG.WriteVariableInt32(players[index].Level);            
            outMSG.WriteVariableInt32(players[index].Health);
            outMSG.WriteVariableInt32(players[index].MaxHealth);
            outMSG.WriteVariableInt32(players[index].Mana);
            outMSG.WriteVariableInt32(players[index].MaxMana);
            outMSG.WriteVariableInt32(players[index].Experience);
            outMSG.WriteVariableInt32(players[index].Wallet);
            outMSG.WriteVariableInt32(players[index].Armor);
            outMSG.WriteVariableInt32(players[index].Strength);
            outMSG.WriteVariableInt32(players[index].Agility);
            outMSG.WriteVariableInt32(players[index].Intelligence);
            outMSG.WriteVariableInt32(players[index].Stamina);
            outMSG.WriteVariableInt32(players[index].Energy);
            outMSG.WriteVariableInt32(players[index].LightRadius);

            SabertoothServer.netServer.SendMessage(outMSG, players[index].Connection, NetDeliveryMethod.ReliableOrdered);
        }

        public static void SendPlayerQuestList(int index)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.SendQuestList);
            for (int i = 0; i < MAX_PLAYER_QUEST_LIST; i++)
            {
                outMSG.WriteVariableInt32(players[index].QuestList[i]);
                outMSG.WriteVariableInt32(players[index].QuestStatus[i]);
            }
            SabertoothServer.netServer.SendMessage(outMSG, players[index].Connection, NetDeliveryMethod.ReliableOrdered);
        }

        public static void SendPlayerInv(int index)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.PlayerInv);
            for (int i = 0; i < MAX_INV_SLOTS; i++)
            {
                outMSG.Write(players[index].Backpack[i].Name);
                outMSG.WriteVariableInt32(players[index].Backpack[i].Sprite);
                outMSG.WriteVariableInt32(players[index].Backpack[i].Damage);
                outMSG.WriteVariableInt32(players[index].Backpack[i].Armor);
                outMSG.WriteVariableInt32(players[index].Backpack[i].Type);
                outMSG.WriteVariableInt32(players[index].Backpack[i].AttackSpeed);
                outMSG.WriteVariableInt32(players[index].Backpack[i].HealthRestore);
                outMSG.WriteVariableInt32(players[index].Backpack[i].ManaRestore);
                outMSG.WriteVariableInt32(players[index].Backpack[i].Strength);
                outMSG.WriteVariableInt32(players[index].Backpack[i].Agility);
                outMSG.WriteVariableInt32(players[index].Backpack[i].Intelligence);
                outMSG.WriteVariableInt32(players[index].Backpack[i].Energy);
                outMSG.WriteVariableInt32(players[index].Backpack[i].Stamina);
                outMSG.WriteVariableInt32(players[index].Backpack[i].Value);
                outMSG.WriteVariableInt32(players[index].Backpack[i].Price);
                outMSG.WriteVariableInt32(players[index].Backpack[i].Rarity);
                outMSG.WriteVariableInt32(players[index].Backpack[i].CoolDown);
                outMSG.WriteVariableInt32(players[index].Backpack[i].AddMaxHealth);
                outMSG.WriteVariableInt32(players[index].Backpack[i].AddMaxMana);
                outMSG.WriteVariableInt32(players[index].Backpack[i].BonusXP);
                outMSG.WriteVariableInt32(players[index].Backpack[i].SpellNum);
                outMSG.Write(players[index].Backpack[i].Stackable);
            }
            SabertoothServer.netServer.SendMessage(outMSG, players[index].Connection, NetDeliveryMethod.ReliableOrdered);
        }

        public static void SendPlayerBank(int index)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.PlayerBank);
            for (int i = 0; i < MAX_BANK_SLOTS; i++)
            {
                outMSG.Write(players[index].Bank[i].Name);
                outMSG.WriteVariableInt32(players[index].Bank[i].Sprite);
                outMSG.WriteVariableInt32(players[index].Bank[i].Damage);
                outMSG.WriteVariableInt32(players[index].Bank[i].Armor);
                outMSG.WriteVariableInt32(players[index].Bank[i].Type);
                outMSG.WriteVariableInt32(players[index].Bank[i].AttackSpeed);
                outMSG.WriteVariableInt32(players[index].Bank[i].HealthRestore);
                outMSG.WriteVariableInt32(players[index].Bank[i].ManaRestore);
                outMSG.WriteVariableInt32(players[index].Bank[i].Strength);
                outMSG.WriteVariableInt32(players[index].Bank[i].Agility);
                outMSG.WriteVariableInt32(players[index].Bank[i].Intelligence);
                outMSG.WriteVariableInt32(players[index].Bank[i].Energy);
                outMSG.WriteVariableInt32(players[index].Bank[i].Stamina);
                outMSG.WriteVariableInt32(players[index].Bank[i].Value);
                outMSG.WriteVariableInt32(players[index].Bank[i].Price);
                outMSG.WriteVariableInt32(players[index].Bank[i].Rarity);
                outMSG.WriteVariableInt32(players[index].Bank[i].CoolDown);
                outMSG.WriteVariableInt32(players[index].Bank[i].AddMaxHealth);
                outMSG.WriteVariableInt32(players[index].Bank[i].AddMaxMana);
                outMSG.WriteVariableInt32(players[index].Bank[i].BonusXP);
                outMSG.WriteVariableInt32(players[index].Bank[i].SpellNum);
                outMSG.Write(players[index].Bank[i].Stackable);
            }
            SabertoothServer.netServer.SendMessage(outMSG, players[index].Connection, NetDeliveryMethod.ReliableOrdered);
        }

        public static void SendPlayerHotBar(int index)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.SendHotBar);
            for (int i = 0; i < MAX_PLAYER_HOTBAR; i++)
            {
                outMSG.Write(players[index].hotBar[i].HotKey.ToString());
                outMSG.WriteVariableInt32(players[index].hotBar[i].SpellNumber);
                outMSG.WriteVariableInt32(players[index].hotBar[i].InvNumber);
            }
            SabertoothServer.netServer.SendMessage(outMSG, players[index].Connection, NetDeliveryMethod.ReliableOrdered);
        }

        public static void SendPlayerEquipment(int index)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.PlayerEquip);

            outMSG.Write(players[index].Chest.Name);
            outMSG.WriteVariableInt32(players[index].Chest.Sprite);
            outMSG.WriteVariableInt32(players[index].Chest.Damage);
            outMSG.WriteVariableInt32(players[index].Chest.Armor);
            outMSG.WriteVariableInt32(players[index].Chest.Type);
            outMSG.WriteVariableInt32(players[index].Chest.AttackSpeed);
            outMSG.WriteVariableInt32(players[index].Chest.HealthRestore);
            outMSG.WriteVariableInt32(players[index].Chest.ManaRestore);
            outMSG.WriteVariableInt32(players[index].Chest.Strength);
            outMSG.WriteVariableInt32(players[index].Chest.Agility);
            outMSG.WriteVariableInt32(players[index].Chest.Intelligence);
            outMSG.WriteVariableInt32(players[index].Chest.Energy);
            outMSG.WriteVariableInt32(players[index].Chest.Stamina);
            outMSG.WriteVariableInt32(players[index].Chest.Value);
            outMSG.WriteVariableInt32(players[index].Chest.Price);
            outMSG.WriteVariableInt32(players[index].Chest.Rarity);
            outMSG.WriteVariableInt32(players[index].Chest.CoolDown);
            outMSG.WriteVariableInt32(players[index].Chest.AddMaxHealth);
            outMSG.WriteVariableInt32(players[index].Chest.AddMaxMana);
            outMSG.WriteVariableInt32(players[index].Chest.BonusXP);
            outMSG.WriteVariableInt32(players[index].Chest.SpellNum);
            outMSG.Write(players[index].Chest.Stackable);

            outMSG.Write(players[index].Legs.Name);
            outMSG.WriteVariableInt32(players[index].Legs.Sprite);
            outMSG.WriteVariableInt32(players[index].Legs.Damage);
            outMSG.WriteVariableInt32(players[index].Legs.Armor);
            outMSG.WriteVariableInt32(players[index].Legs.Type);
            outMSG.WriteVariableInt32(players[index].Legs.AttackSpeed);
            outMSG.WriteVariableInt32(players[index].Legs.HealthRestore);
            outMSG.WriteVariableInt32(players[index].Legs.ManaRestore);
            outMSG.WriteVariableInt32(players[index].Legs.Strength);
            outMSG.WriteVariableInt32(players[index].Legs.Agility);
            outMSG.WriteVariableInt32(players[index].Legs.Intelligence);
            outMSG.WriteVariableInt32(players[index].Legs.Energy);
            outMSG.WriteVariableInt32(players[index].Legs.Stamina);
            outMSG.WriteVariableInt32(players[index].Legs.Value);
            outMSG.WriteVariableInt32(players[index].Legs.Price);
            outMSG.WriteVariableInt32(players[index].Legs.Rarity);
            outMSG.WriteVariableInt32(players[index].Legs.CoolDown);
            outMSG.WriteVariableInt32(players[index].Legs.AddMaxHealth);
            outMSG.WriteVariableInt32(players[index].Legs.AddMaxMana);
            outMSG.WriteVariableInt32(players[index].Legs.BonusXP);
            outMSG.WriteVariableInt32(players[index].Legs.SpellNum);
            outMSG.Write(players[index].Legs.Stackable);

            outMSG.Write(players[index].Feet.Name);
            outMSG.WriteVariableInt32(players[index].Feet.Sprite);
            outMSG.WriteVariableInt32(players[index].Feet.Damage);
            outMSG.WriteVariableInt32(players[index].Feet.Armor);
            outMSG.WriteVariableInt32(players[index].Feet.Type);
            outMSG.WriteVariableInt32(players[index].Feet.AttackSpeed);
            outMSG.WriteVariableInt32(players[index].Feet.HealthRestore);
            outMSG.WriteVariableInt32(players[index].Feet.ManaRestore);
            outMSG.WriteVariableInt32(players[index].Feet.Strength);
            outMSG.WriteVariableInt32(players[index].Feet.Agility);
            outMSG.WriteVariableInt32(players[index].Feet.Intelligence);
            outMSG.WriteVariableInt32(players[index].Feet.Energy);
            outMSG.WriteVariableInt32(players[index].Feet.Stamina);
            outMSG.WriteVariableInt32(players[index].Feet.Value);
            outMSG.WriteVariableInt32(players[index].Feet.Price);
            outMSG.WriteVariableInt32(players[index].Feet.Rarity);
            outMSG.WriteVariableInt32(players[index].Feet.CoolDown);
            outMSG.WriteVariableInt32(players[index].Feet.AddMaxHealth);
            outMSG.WriteVariableInt32(players[index].Feet.AddMaxMana);
            outMSG.WriteVariableInt32(players[index].Feet.BonusXP);
            outMSG.WriteVariableInt32(players[index].Feet.SpellNum);
            outMSG.Write(players[index].Feet.Stackable);

            SabertoothServer.netServer.SendMessage(outMSG, players[index].Connection, NetDeliveryMethod.ReliableOrdered);
        }

        public static void SendWeaponsUpdate(int index)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.UpdateWeapons);
            //Main weapon
            outMSG.Write(players[index].MainHand.Name);
            outMSG.WriteVariableInt32(players[index].MainHand.Sprite);
            outMSG.WriteVariableInt32(players[index].MainHand.Damage);
            outMSG.WriteVariableInt32(players[index].MainHand.Armor);
            outMSG.WriteVariableInt32(players[index].MainHand.Type);
            outMSG.WriteVariableInt32(players[index].MainHand.AttackSpeed);
            outMSG.WriteVariableInt32(players[index].MainHand.HealthRestore);
            outMSG.WriteVariableInt32(players[index].MainHand.ManaRestore);
            outMSG.WriteVariableInt32(players[index].MainHand.Strength);
            outMSG.WriteVariableInt32(players[index].MainHand.Agility);
            outMSG.WriteVariableInt32(players[index].MainHand.Intelligence);
            outMSG.WriteVariableInt32(players[index].MainHand.Energy);
            outMSG.WriteVariableInt32(players[index].MainHand.Stamina);
            outMSG.WriteVariableInt32(players[index].MainHand.Value);
            outMSG.WriteVariableInt32(players[index].MainHand.Price);
            outMSG.WriteVariableInt32(players[index].MainHand.Rarity);
            outMSG.WriteVariableInt32(players[index].MainHand.CoolDown);
            outMSG.WriteVariableInt32(players[index].MainHand.AddMaxHealth);
            outMSG.WriteVariableInt32(players[index].MainHand.AddMaxMana);
            outMSG.WriteVariableInt32(players[index].MainHand.BonusXP);
            outMSG.WriteVariableInt32(players[index].MainHand.SpellNum);
            outMSG.Write(players[index].MainHand.Stackable);

            //Secondary weapon
            outMSG.Write(players[index].OffHand.Name);
            outMSG.WriteVariableInt32(players[index].OffHand.Sprite);
            outMSG.WriteVariableInt32(players[index].OffHand.Damage);
            outMSG.WriteVariableInt32(players[index].OffHand.Armor);
            outMSG.WriteVariableInt32(players[index].OffHand.Type);
            outMSG.WriteVariableInt32(players[index].OffHand.AttackSpeed);
            outMSG.WriteVariableInt32(players[index].OffHand.HealthRestore);
            outMSG.WriteVariableInt32(players[index].OffHand.ManaRestore);
            outMSG.WriteVariableInt32(players[index].OffHand.Strength);
            outMSG.WriteVariableInt32(players[index].OffHand.Agility);
            outMSG.WriteVariableInt32(players[index].OffHand.Intelligence);
            outMSG.WriteVariableInt32(players[index].OffHand.Energy);
            outMSG.WriteVariableInt32(players[index].OffHand.Stamina);
            outMSG.WriteVariableInt32(players[index].OffHand.Value);
            outMSG.WriteVariableInt32(players[index].OffHand.Price);
            outMSG.WriteVariableInt32(players[index].OffHand.Rarity);
            outMSG.WriteVariableInt32(players[index].OffHand.CoolDown);
            outMSG.WriteVariableInt32(players[index].OffHand.AddMaxHealth);
            outMSG.WriteVariableInt32(players[index].OffHand.AddMaxMana);
            outMSG.WriteVariableInt32(players[index].OffHand.BonusXP);
            outMSG.WriteVariableInt32(players[index].OffHand.SpellNum);
            outMSG.Write(players[index].OffHand.Stackable);
            SabertoothServer.netServer.SendMessage(outMSG, players[index].Connection, NetDeliveryMethod.ReliableOrdered);
        }

        public static void SendPlayers()
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.Players);
            for (int i = 0; i < 5; i++)
            {
                outMSG.Write(players[i].Name);
                outMSG.WriteVariableInt32(players[i].X);
                outMSG.WriteVariableInt32(players[i].Y);
                outMSG.WriteVariableInt32(players[i].Map);
                outMSG.WriteVariableInt32(players[i].Direction);
                outMSG.WriteVariableInt32(players[i].AimDirection);
                outMSG.WriteVariableInt32(players[i].Sprite);
                outMSG.WriteVariableInt32(players[i].Level);
                outMSG.WriteVariableInt32(players[i].Health);
                outMSG.WriteVariableInt32(players[i].MaxHealth);
                outMSG.WriteVariableInt32(players[i].Mana);
                outMSG.WriteVariableInt32(players[i].MaxMana);
                outMSG.WriteVariableInt32(players[i].Experience);
                outMSG.WriteVariableInt32(players[i].Wallet);
                outMSG.WriteVariableInt32(players[i].Armor);
                outMSG.WriteVariableInt32(players[i].Strength);
                outMSG.WriteVariableInt32(players[i].Agility);
                outMSG.WriteVariableInt32(players[i].Intelligence);
                outMSG.WriteVariableInt32(players[i].Stamina);
                outMSG.WriteVariableInt32(players[i].Energy);
                outMSG.WriteVariableInt32(players[i].LightRadius);
                outMSG.WriteVariableInt32(players[i].PlayDays);
                outMSG.WriteVariableInt32(players[i].PlayHours);
                outMSG.WriteVariableInt32(players[i].PlayMinutes);
                outMSG.WriteVariableInt32(players[i].PlaySeconds);
            }
            SabertoothServer.netServer.SendToAll(outMSG, NetDeliveryMethod.ReliableOrdered);
        }        

        public static void SendChests(NetIncomingMessage incMSG)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.SendChests);
            for (int i = 0; i < 10; i++)
            {
                outMSG.Write(chests[i].Name);
                outMSG.WriteVariableInt32(chests[i].Money);
                outMSG.WriteVariableInt32(chests[i].Experience);
                outMSG.WriteVariableInt32(chests[i].RequiredLevel);
                outMSG.WriteVariableInt32(chests[i].TrapLevel);
                outMSG.WriteVariableInt32(chests[i].Key);
                outMSG.WriteVariableInt32(chests[i].Damage);
                outMSG.WriteVariableInt32(chests[i].NpcSpawn);
                outMSG.WriteVariableInt32(chests[i].SpawnAmount);

                for (int n = 0; n < 10; n++)
                {
                    outMSG.Write(chests[i].ChestItem[n].Name);
                    outMSG.WriteVariableInt32(chests[i].ChestItem[n].ItemNum);
                    outMSG.WriteVariableInt32(chests[i].ChestItem[n].Value);
                }
            }
            SabertoothServer.netServer.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
        }

        public static void SendChestData(NetIncomingMessage incMSG, int index)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.ChestData);
            outMSG.WriteVariableInt32(index);
            outMSG.Write(chests[index].Name);
            outMSG.WriteVariableInt32(chests[index].Money);
            outMSG.WriteVariableInt32(chests[index].Experience);
            outMSG.WriteVariableInt32(chests[index].RequiredLevel);
            outMSG.WriteVariableInt32(chests[index].TrapLevel);
            outMSG.WriteVariableInt32(chests[index].Key);
            outMSG.WriteVariableInt32(chests[index].Damage);
            outMSG.WriteVariableInt32(chests[index].NpcSpawn);
            outMSG.WriteVariableInt32(chests[index].SpawnAmount);

            for (int n = 0; n < MAX_CHEST_ITEMS; n++)
            {
                outMSG.Write(chests[index].ChestItem[n].Name);
                outMSG.WriteVariableInt32(chests[index].ChestItem[n].ItemNum);
                outMSG.WriteVariableInt32(chests[index].ChestItem[n].Value);
            }
            SabertoothServer.netServer.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
        }

        static void SendQuests(NetIncomingMessage incMSG)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.SendQuests);
            for (int i = 0; i < MAX_QUESTS; i++)
            {
                outMSG.Write(quests[i].Name);
                outMSG.Write(quests[i].StartMessage);
                outMSG.Write(quests[i].InProgressMessage);
                outMSG.Write(quests[i].CompleteMessage);
                outMSG.Write(quests[i].Description);
                outMSG.WriteVariableInt32(quests[i].PrerequisiteQuest);
                outMSG.WriteVariableInt32(quests[i].LevelRequired);

                for (int m = 0; m < MAX_QUEST_ITEMS_REQ; m++)
                {
                    outMSG.WriteVariableInt32(quests[i].ItemNum[m]);
                    outMSG.WriteVariableInt32(quests[i].ItemValue[m]);
                }

                for (int n = 0; n < MAX_QUEST_NPCS_REQ; n++)
                {
                    outMSG.WriteVariableInt32(quests[i].NpcNum[n]);
                    outMSG.WriteVariableInt32(quests[i].NpcValue[n]);
                }

                for (int o = 0; o < MAX_QUEST_REWARDS; o++)
                {
                    outMSG.WriteVariableInt32(quests[i].RewardItem[o]);
                    outMSG.WriteVariableInt32(quests[i].RewardValue[o]);
                }

                outMSG.WriteVariableInt32(quests[i].Experience);
                outMSG.WriteVariableInt32(quests[i].Money);
                outMSG.WriteVariableInt32(quests[i].Type);
            }
            SabertoothServer.netServer.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
        }

        static void SendQuestData(NetIncomingMessage incMSG, int index)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.SendQuestData);
            outMSG.WriteVariableInt32(index);
            outMSG.Write(quests[index].Name);
            outMSG.Write(quests[index].StartMessage);
            outMSG.Write(quests[index].InProgressMessage);
            outMSG.Write(quests[index].CompleteMessage);
            outMSG.Write(quests[index].Description);
            outMSG.WriteVariableInt32(quests[index].PrerequisiteQuest);
            outMSG.WriteVariableInt32(quests[index].LevelRequired);

            for (int m = 0; m < MAX_QUEST_ITEMS_REQ; m++)
            {
                outMSG.WriteVariableInt32(quests[index].ItemNum[m]);
                outMSG.WriteVariableInt32(quests[index].ItemValue[m]);
            }

            for (int n = 0; n < MAX_QUEST_NPCS_REQ; n++)
            {
                outMSG.WriteVariableInt32(quests[index].NpcNum[n]);
                outMSG.WriteVariableInt32(quests[index].NpcValue[n]);
            }

            for (int o = 0; o < MAX_QUEST_REWARDS; o++)
            {
                outMSG.WriteVariableInt32(quests[index].RewardItem[o]);
                outMSG.WriteVariableInt32(quests[index].RewardValue[o]);
            }

            outMSG.WriteVariableInt32(quests[index].Experience);
            outMSG.WriteVariableInt32(quests[index].Money);
            outMSG.WriteVariableInt32(quests[index].Type);
            SabertoothServer.netServer.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
        }

        static void SendChats(NetIncomingMessage incMSG)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.SendChats);
            for (int i = 0; i < MAX_CHATS; i++)
            {
                outMSG.Write(chats[i].Name);
                outMSG.Write(chats[i].MainMessage);
                outMSG.Write(chats[i].Option[0]);
                outMSG.Write(chats[i].Option[1]);
                outMSG.Write(chats[i].Option[2]);
                outMSG.Write(chats[i].Option[3]);
                outMSG.WriteVariableInt32(chats[i].NextChat[0]);
                outMSG.WriteVariableInt32(chats[i].NextChat[1]);
                outMSG.WriteVariableInt32(chats[i].NextChat[2]);
                outMSG.WriteVariableInt32(chats[i].NextChat[3]);
                outMSG.WriteVariableInt32(chats[i].ShopNum);
                outMSG.WriteVariableInt32(chats[i].QuestNum);
                outMSG.WriteVariableInt32(chats[i].ItemNum[0]);
                outMSG.WriteVariableInt32(chats[i].ItemNum[1]);
                outMSG.WriteVariableInt32(chats[i].ItemNum[2]);
                outMSG.WriteVariableInt32(chats[i].ItemVal[0]);
                outMSG.WriteVariableInt32(chats[i].ItemVal[1]);
                outMSG.WriteVariableInt32(chats[i].ItemVal[2]);
                outMSG.WriteVariableInt32(chats[i].Money);
                outMSG.WriteVariableInt32(chats[i].Type);
            }
            SabertoothServer.netServer.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
        }

        static void SendChatData(NetIncomingMessage incMSG, int index)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.SendChatData);
            outMSG.WriteVariableInt32(index);
            outMSG.Write(chats[index].Name);
            outMSG.Write(chats[index].MainMessage);
            outMSG.Write(chats[index].Option[0]);
            outMSG.Write(chats[index].Option[1]);
            outMSG.Write(chats[index].Option[2]);
            outMSG.Write(chats[index].Option[3]);
            outMSG.WriteVariableInt32(chats[index].NextChat[0]);
            outMSG.WriteVariableInt32(chats[index].NextChat[1]);
            outMSG.WriteVariableInt32(chats[index].NextChat[2]);
            outMSG.WriteVariableInt32(chats[index].NextChat[3]);
            outMSG.WriteVariableInt32(chats[index].ShopNum);
            outMSG.WriteVariableInt32(chats[index].QuestNum);
            outMSG.WriteVariableInt32(chats[index].ItemNum[0]);
            outMSG.WriteVariableInt32(chats[index].ItemNum[1]);
            outMSG.WriteVariableInt32(chats[index].ItemNum[2]);
            outMSG.WriteVariableInt32(chats[index].ItemVal[0]);
            outMSG.WriteVariableInt32(chats[index].ItemVal[1]);
            outMSG.WriteVariableInt32(chats[index].ItemVal[2]);
            outMSG.WriteVariableInt32(chats[index].Money);
            outMSG.WriteVariableInt32(chats[index].Type);

            SabertoothServer.netServer.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
        }

        static void SendShops(NetIncomingMessage incMSG)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.ShopData);
            for (int i = 0; i < 10; i++)
            {
                outMSG.Write(shops[i].Name);
                for (int n = 0; n < 25; n++)
                {
                    outMSG.Write(shops[i].shopItem[n].Name);
                    outMSG.WriteVariableInt32(shops[i].shopItem[n].ItemNum);
                    outMSG.WriteVariableInt32(shops[i].shopItem[n].Cost);
                }
            }
            SabertoothServer.netServer.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
        }

        static void SendShopItems(NetIncomingMessage incMSG, int shopIndex)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.ShopItemsData);
            outMSG.WriteVariableInt32(shopIndex);
            for (int n = 0; n < 25; n++)
            {
                outMSG.Write(shops[shopIndex].shopItem[n].Name);
                outMSG.WriteVariableInt32(shops[shopIndex].shopItem[n].ItemNum);
                outMSG.WriteVariableInt32(shops[shopIndex].shopItem[n].Cost);
            }
            SabertoothServer.netServer.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
        }

        static void SendShopItemData(NetIncomingMessage incMSG, int shopIndex, int index)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.ShopItemData);
            outMSG.WriteVariableInt32(shopIndex);
            outMSG.WriteVariableInt32(index);
            outMSG.Write(shops[shopIndex].shopItem[index].Name);
            outMSG.WriteVariableInt32(shops[shopIndex].shopItem[index].ItemNum);
            outMSG.WriteVariableInt32(shops[shopIndex].shopItem[index].Cost);

            SabertoothServer.netServer.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
        }

        static void SendItemData(NetIncomingMessage incMSG, int index)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.ItemData);
            outMSG.WriteVariableInt32(index);
            outMSG.Write(items[index].Name);
            outMSG.WriteVariableInt32(items[index].Sprite);
            outMSG.WriteVariableInt32(items[index].Damage);
            outMSG.WriteVariableInt32(items[index].Armor);
            outMSG.WriteVariableInt32(items[index].Type);
            outMSG.WriteVariableInt32(items[index].HealthRestore);
            outMSG.WriteVariableInt32(items[index].ManaRestore);
            outMSG.WriteVariableInt32(items[index].Strength);
            outMSG.WriteVariableInt32(items[index].Agility);
            outMSG.WriteVariableInt32(items[index].Intelligence);
            outMSG.WriteVariableInt32(items[index].Energy);
            outMSG.WriteVariableInt32(items[index].Stamina);
            outMSG.WriteVariableInt32(items[index].Value);
            outMSG.WriteVariableInt32(items[index].Price);
            outMSG.WriteVariableInt32(items[index].Rarity);
            outMSG.WriteVariableInt32(items[index].CoolDown);
            outMSG.WriteVariableInt32(items[index].AddMaxHealth);
            outMSG.WriteVariableInt32(items[index].AddMaxMana);
            outMSG.WriteVariableInt32(items[index].BonusXP);
            outMSG.WriteVariableInt32(items[index].SpellNum);
            outMSG.Write(items[index].Stackable);
            SabertoothServer.netServer.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
        }

        static void SendItems(NetIncomingMessage incMSG)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.Items);
            for (int i = 0; i < 50; i++)
            {
                outMSG.Write(items[i].Name);
                outMSG.WriteVariableInt32(items[i].Sprite);
                outMSG.WriteVariableInt32(items[i].Damage);
                outMSG.WriteVariableInt32(items[i].Armor);
                outMSG.WriteVariableInt32(items[i].Type);
                outMSG.WriteVariableInt32(items[i].HealthRestore);
                outMSG.WriteVariableInt32(items[i].ManaRestore);
                outMSG.WriteVariableInt32(items[i].Strength);
                outMSG.WriteVariableInt32(items[i].Agility);
                outMSG.WriteVariableInt32(items[i].Intelligence);
                outMSG.WriteVariableInt32(items[i].Energy);
                outMSG.WriteVariableInt32(items[i].Stamina);
                outMSG.WriteVariableInt32(items[i].Value);
                outMSG.WriteVariableInt32(items[i].Price);
                outMSG.WriteVariableInt32(items[i].Rarity);
                outMSG.WriteVariableInt32(items[i].CoolDown);
                outMSG.WriteVariableInt32(items[i].AddMaxHealth);
                outMSG.WriteVariableInt32(items[i].AddMaxMana);
                outMSG.WriteVariableInt32(items[i].BonusXP);
                outMSG.WriteVariableInt32(items[i].SpellNum);
                outMSG.Write(items[i].Stackable);
            }
            SabertoothServer.netServer.SendToAll(outMSG, NetDeliveryMethod.ReliableOrdered);
        }

        static void SendNpcs(NetIncomingMessage incMSG)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.Npcs);
            for (int i = 0; i < 10; i++)
            {
                outMSG.Write(npcs[i].Name);
                outMSG.WriteVariableInt32(npcs[i].X);
                outMSG.WriteVariableInt32(npcs[i].Y);
                outMSG.WriteVariableInt32(npcs[i].Direction);
                outMSG.WriteVariableInt32(npcs[i].Sprite);
                outMSG.WriteVariableInt32(npcs[i].Step);
                outMSG.WriteVariableInt32(npcs[i].Owner);
                outMSG.WriteVariableInt32(npcs[i].Behavior);
                outMSG.WriteVariableInt32(npcs[i].SpawnTime);
                outMSG.WriteVariableInt32(npcs[i].Health);
                outMSG.WriteVariableInt32(npcs[i].MaxHealth);
                outMSG.WriteVariableInt32(npcs[i].Damage);
                outMSG.Write(npcs[i].IsSpawned);
            }
            SabertoothServer.netServer.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
        }

        static void SendPoolMapNpcs(NetIncomingMessage incMSG, int map)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.PoolNpcs);
            for (int i = 0; i < 20; i++)
            {
                outMSG.Write(maps[map].r_MapNpc[i].Name);
                outMSG.WriteVariableInt32(maps[map].r_MapNpc[i].X);
                outMSG.WriteVariableInt32(maps[map].r_MapNpc[i].Y);
                outMSG.WriteVariableInt32(maps[map].r_MapNpc[i].Direction);
                outMSG.WriteVariableInt32(maps[map].r_MapNpc[i].Sprite);
                outMSG.WriteVariableInt32(maps[map].r_MapNpc[i].Step);
                outMSG.WriteVariableInt32(maps[map].r_MapNpc[i].Owner);
                outMSG.WriteVariableInt32(maps[map].r_MapNpc[i].Behavior);
                outMSG.WriteVariableInt32(maps[map].r_MapNpc[i].SpawnTime);
                outMSG.WriteVariableInt32(maps[map].r_MapNpc[i].Health);
                outMSG.WriteVariableInt32(maps[map].r_MapNpc[i].MaxHealth);
                outMSG.WriteVariableInt32(maps[map].r_MapNpc[i].Damage);
                outMSG.Write(maps[map].r_MapNpc[i].IsSpawned);
            }
            SabertoothServer.netServer.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
        }

        static void SendMapNpcs(NetIncomingMessage incMSG, int map)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.MapNpc);
            for (int i = 0; i < 10; i++)
            {
                outMSG.Write(maps[map].m_MapNpc[i].Name);
                outMSG.WriteVariableInt32(maps[map].m_MapNpc[i].X);
                outMSG.WriteVariableInt32(maps[map].m_MapNpc[i].Y);
                outMSG.WriteVariableInt32(maps[map].m_MapNpc[i].Direction);
                outMSG.WriteVariableInt32(maps[map].m_MapNpc[i].Sprite);
                outMSG.WriteVariableInt32(maps[map].m_MapNpc[i].Step);
                outMSG.WriteVariableInt32(maps[map].m_MapNpc[i].Owner);
                outMSG.WriteVariableInt32(maps[map].m_MapNpc[i].Behavior);
                outMSG.WriteVariableInt32(maps[map].m_MapNpc[i].SpawnTime);
                outMSG.WriteVariableInt32(maps[map].m_MapNpc[i].Health);
                outMSG.WriteVariableInt32(maps[map].m_MapNpc[i].MaxHealth);
                outMSG.WriteVariableInt32(maps[map].m_MapNpc[i].Damage);
                outMSG.Write(maps[map].m_MapNpc[i].IsSpawned);
            }
            SabertoothServer.netServer.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
        }

        public static void SendPoolNpcData(NetConnection p_Conn, int map, int npcNum)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.PoolNpcData);
            outMSG.WriteVariableInt32(npcNum);
            outMSG.Write(maps[map].r_MapNpc[npcNum].Name);
            outMSG.WriteVariableInt32(maps[map].r_MapNpc[npcNum].X);
            outMSG.WriteVariableInt32(maps[map].r_MapNpc[npcNum].Y);
            outMSG.WriteVariableInt32(maps[map].r_MapNpc[npcNum].Direction);
            outMSG.WriteVariableInt32(maps[map].r_MapNpc[npcNum].Sprite);
            outMSG.WriteVariableInt32(maps[map].r_MapNpc[npcNum].Step);
            outMSG.WriteVariableInt32(maps[map].r_MapNpc[npcNum].Owner);
            outMSG.WriteVariableInt32(maps[map].r_MapNpc[npcNum].Behavior);
            outMSG.WriteVariableInt32(maps[map].r_MapNpc[npcNum].SpawnTime);
            outMSG.WriteVariableInt32(maps[map].r_MapNpc[npcNum].Health);
            outMSG.WriteVariableInt32(maps[map].r_MapNpc[npcNum].MaxHealth);
            outMSG.WriteVariableInt32(maps[map].r_MapNpc[npcNum].Damage);
            outMSG.Write(maps[map].r_MapNpc[npcNum].IsSpawned);

            SabertoothServer.netServer.SendMessage(outMSG, p_Conn, NetDeliveryMethod.ReliableOrdered);
        }

        public static void SendUpdatePoolNpcLoc(NetConnection p_Conn, int map, int npcNum)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.PoolNpcDirecion);
            outMSG.WriteVariableInt32(npcNum);
            outMSG.WriteVariableInt32(maps[map].r_MapNpc[npcNum].X);
            outMSG.WriteVariableInt32(maps[map].r_MapNpc[npcNum].Y);
            outMSG.WriteVariableInt32(maps[map].r_MapNpc[npcNum].Direction);
            outMSG.WriteVariableInt32(maps[map].r_MapNpc[npcNum].Step);
            SabertoothServer.netServer.SendMessage(outMSG, p_Conn, NetDeliveryMethod.ReliableOrdered);
        }

        public static void SendMapNpcData(NetConnection p_Conn, int map, int npcNum)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.NpcData);
            outMSG.WriteVariableInt32(npcNum);
            outMSG.Write(maps[map].m_MapNpc[npcNum].Name);
            outMSG.WriteVariableInt32(maps[map].m_MapNpc[npcNum].X);
            outMSG.WriteVariableInt32(maps[map].m_MapNpc[npcNum].Y);
            outMSG.WriteVariableInt32(maps[map].m_MapNpc[npcNum].Direction);
            outMSG.WriteVariableInt32(maps[map].m_MapNpc[npcNum].Sprite);
            outMSG.WriteVariableInt32(maps[map].m_MapNpc[npcNum].Step);
            outMSG.WriteVariableInt32(maps[map].m_MapNpc[npcNum].Owner);
            outMSG.WriteVariableInt32(maps[map].m_MapNpc[npcNum].Behavior);
            outMSG.WriteVariableInt32(maps[map].m_MapNpc[npcNum].SpawnTime);
            outMSG.WriteVariableInt32(maps[map].m_MapNpc[npcNum].Health);
            outMSG.WriteVariableInt32(maps[map].m_MapNpc[npcNum].MaxHealth);
            outMSG.WriteVariableInt32(maps[map].m_MapNpc[npcNum].Damage);
            outMSG.Write(maps[map].m_MapNpc[npcNum].IsSpawned);

            SabertoothServer.netServer.SendMessage(outMSG, p_Conn, NetDeliveryMethod.ReliableOrdered);
        }

        public static void SendUpdateNpcLoc(NetConnection p_Conn, int map, int npcNum)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.NpcDirection);
            outMSG.WriteVariableInt32(npcNum);
            outMSG.WriteVariableInt32(maps[map].m_MapNpc[npcNum].X);
            outMSG.WriteVariableInt32(maps[map].m_MapNpc[npcNum].Y);
            outMSG.WriteVariableInt32(maps[map].m_MapNpc[npcNum].Direction);
            outMSG.WriteVariableInt32(maps[map].m_MapNpc[npcNum].Step);            

            SabertoothServer.netServer.SendMessage(outMSG, p_Conn, NetDeliveryMethod.ReliableOrdered);
        }

        public static void SendNpcVitalData(NetConnection p_Conn, int map, int npcNum)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.NpcVitals);
            outMSG.WriteVariableInt32(npcNum);
            outMSG.WriteVariableInt32(maps[map].m_MapNpc[npcNum].Health);
            outMSG.Write(maps[map].m_MapNpc[npcNum].IsSpawned);
            outMSG.WriteVariableInt32(maps[map].m_MapNpc[npcNum].X);
            outMSG.WriteVariableInt32(maps[map].m_MapNpc[npcNum].Y);

            SabertoothServer.netServer.SendMessage(outMSG, p_Conn, NetDeliveryMethod.ReliableOrdered);
        }

        public static void SendPoolNpcVitalData(NetConnection p_Conn, int map, int npcNum)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.PoolNpcVitals);
            outMSG.WriteVariableInt32(npcNum);
            outMSG.WriteVariableInt32(maps[map].r_MapNpc[npcNum].Health);
            outMSG.Write(maps[map].r_MapNpc[npcNum].IsSpawned);
            outMSG.WriteVariableInt32(maps[map].r_MapNpc[npcNum].X);
            outMSG.WriteVariableInt32(maps[map].r_MapNpc[npcNum].Y);

            SabertoothServer.netServer.SendMessage(outMSG, p_Conn, NetDeliveryMethod.ReliableOrdered);
        }

        static void SendMapItems(NetIncomingMessage incMSG, int map)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.MapItems);
            for (int i = 0; i < 20; i++)
            {
                outMSG.Write(maps[map].m_MapItem[i].Name);
                outMSG.WriteVariableInt32(maps[map].m_MapItem[i].X);
                outMSG.WriteVariableInt32(maps[map].m_MapItem[i].Y);
                outMSG.WriteVariableInt32(maps[map].m_MapItem[i].Sprite);
                outMSG.WriteVariableInt32(maps[map].m_MapItem[i].Damage);
                outMSG.WriteVariableInt32(maps[map].m_MapItem[i].Armor);
                outMSG.WriteVariableInt32(maps[map].m_MapItem[i].Type);
                outMSG.WriteVariableInt32(maps[map].m_MapItem[i].HealthRestore);
                outMSG.WriteVariableInt32(maps[map].m_MapItem[i].ManaRestore);                
                outMSG.WriteVariableInt32(maps[map].m_MapItem[i].Strength);
                outMSG.WriteVariableInt32(maps[map].m_MapItem[i].Agility);
                outMSG.WriteVariableInt32(maps[map].m_MapItem[i].Intelligence);
                outMSG.WriteVariableInt32(maps[map].m_MapItem[i].Energy);
                outMSG.WriteVariableInt32(maps[map].m_MapItem[i].Stamina);
                outMSG.WriteVariableInt32(maps[map].m_MapItem[i].Value);                
                outMSG.WriteVariableInt32(maps[map].m_MapItem[i].Price);
                outMSG.WriteVariableInt32(maps[map].m_MapItem[i].Rarity);
                outMSG.WriteVariableInt32(maps[map].m_MapItem[i].CoolDown);
                outMSG.WriteVariableInt32(maps[map].m_MapItem[i].AddMaxHealth);
                outMSG.WriteVariableInt32(maps[map].m_MapItem[i].AddMaxMana);
                outMSG.WriteVariableInt32(maps[map].m_MapItem[i].BonusXP);
                outMSG.WriteVariableInt32(maps[map].m_MapItem[i].SpellNum);
                outMSG.Write(maps[map].m_MapItem[i].Stackable);
                outMSG.Write(maps[map].m_MapItem[i].IsSpawned);
            }            

            SabertoothServer.netServer.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
        }

        public static void SendMapItemData(NetConnection p_Conn, int map, int mapSlot)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.MapItemData);
            outMSG.WriteVariableInt32(mapSlot);
            outMSG.Write(maps[map].m_MapItem[mapSlot].Name);
            outMSG.WriteVariableInt32(maps[map].m_MapItem[mapSlot].X);
            outMSG.WriteVariableInt32(maps[map].m_MapItem[mapSlot].Y);
            outMSG.WriteVariableInt32(maps[map].m_MapItem[mapSlot].Sprite);
            outMSG.WriteVariableInt32(maps[map].m_MapItem[mapSlot].Damage);
            outMSG.WriteVariableInt32(maps[map].m_MapItem[mapSlot].Armor);
            outMSG.WriteVariableInt32(maps[map].m_MapItem[mapSlot].Type);
            outMSG.WriteVariableInt32(maps[map].m_MapItem[mapSlot].HealthRestore);
            outMSG.WriteVariableInt32(maps[map].m_MapItem[mapSlot].ManaRestore);
            outMSG.WriteVariableInt32(maps[map].m_MapItem[mapSlot].Strength);
            outMSG.WriteVariableInt32(maps[map].m_MapItem[mapSlot].Agility);
            outMSG.WriteVariableInt32(maps[map].m_MapItem[mapSlot].Intelligence);
            outMSG.WriteVariableInt32(maps[map].m_MapItem[mapSlot].Energy);
            outMSG.WriteVariableInt32(maps[map].m_MapItem[mapSlot].Stamina);
            outMSG.WriteVariableInt32(maps[map].m_MapItem[mapSlot].Value);
            outMSG.WriteVariableInt32(maps[map].m_MapItem[mapSlot].Price);
            outMSG.WriteVariableInt32(maps[map].m_MapItem[mapSlot].Rarity);
            outMSG.WriteVariableInt32(maps[map].m_MapItem[mapSlot].CoolDown);
            outMSG.WriteVariableInt32(maps[map].m_MapItem[mapSlot].AddMaxHealth);
            outMSG.WriteVariableInt32(maps[map].m_MapItem[mapSlot].AddMaxMana);
            outMSG.WriteVariableInt32(maps[map].m_MapItem[mapSlot].BonusXP);
            outMSG.WriteVariableInt32(maps[map].m_MapItem[mapSlot].SpellNum);
            outMSG.Write(maps[map].m_MapItem[mapSlot].Stackable);
            outMSG.Write(maps[map].m_MapItem[mapSlot].IsSpawned);     

            SabertoothServer.netServer.SendMessage(outMSG, p_Conn, NetDeliveryMethod.ReliableOrdered);
        }

        static void SendErrorMessage(string message, string caption, NetIncomingMessage incMSG)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.ErrorMessage);
            outMSG.Write(message);
            outMSG.Write(caption);            
            SabertoothServer.netServer.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
        }

        static void SendActivationRequest(NetIncomingMessage incMSG, int index)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.RequestActivation);
            outMSG.WriteVariableInt32(index);
            SabertoothServer.netServer.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
        }

        public static void SendInstanceMapData(NetIncomingMessage incMSG, Map iMap)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.MapData);
            outMSG.Write(iMap.Name);
            outMSG.WriteVariableInt32(iMap.Revision);
            outMSG.WriteVariableInt32(iMap.TopMap);
            outMSG.WriteVariableInt32(iMap.BottomMap);
            outMSG.WriteVariableInt32(iMap.LeftMap);
            outMSG.WriteVariableInt32(iMap.RightMap);
            outMSG.WriteVariableInt32(iMap.Brightness);
            int maxx = iMap.MaxX;
            int maxy = iMap.MaxY;
            outMSG.WriteVariableInt32(maxx);
            outMSG.WriteVariableInt32(maxy);

            for (int x = 0; x < maxx; x++)
            {
                for (int y = 0; y < maxy; y++)
                {
                    //ground
                    outMSG.WriteVariableInt32(iMap.Ground[x, y].TileX);
                    outMSG.WriteVariableInt32(iMap.Ground[x, y].TileY);
                    outMSG.WriteVariableInt32(iMap.Ground[x, y].TileW);
                    outMSG.WriteVariableInt32(iMap.Ground[x, y].TileH);
                    outMSG.WriteVariableInt32(iMap.Ground[x, y].Tileset);
                    outMSG.WriteVariableInt32(iMap.Ground[x, y].Type);
                    outMSG.WriteVariableInt32(iMap.Ground[x, y].SpawnNum);
                    outMSG.Write(iMap.Ground[x, y].LightRadius);
                    //mask
                    outMSG.WriteVariableInt32(iMap.Mask[x, y].TileX);
                    outMSG.WriteVariableInt32(iMap.Mask[x, y].TileY);
                    outMSG.WriteVariableInt32(iMap.Mask[x, y].TileW);
                    outMSG.WriteVariableInt32(iMap.Mask[x, y].TileH);
                    outMSG.WriteVariableInt32(iMap.Mask[x, y].Tileset);
                    //fringe
                    outMSG.WriteVariableInt32(iMap.Fringe[x, y].TileX);
                    outMSG.WriteVariableInt32(iMap.Fringe[x, y].TileY);
                    outMSG.WriteVariableInt32(iMap.Fringe[x, y].TileW);
                    outMSG.WriteVariableInt32(iMap.Fringe[x, y].TileH);
                    outMSG.WriteVariableInt32(iMap.Fringe[x, y].Tileset);
                    //mask a
                    outMSG.WriteVariableInt32(iMap.MaskA[x, y].TileX);
                    outMSG.WriteVariableInt32(iMap.MaskA[x, y].TileY);
                    outMSG.WriteVariableInt32(iMap.MaskA[x, y].TileW);
                    outMSG.WriteVariableInt32(iMap.MaskA[x, y].TileH);
                    outMSG.WriteVariableInt32(iMap.MaskA[x, y].Tileset);
                    //fringe a
                    outMSG.WriteVariableInt32(iMap.FringeA[x, y].TileX);
                    outMSG.WriteVariableInt32(iMap.FringeA[x, y].TileY);
                    outMSG.WriteVariableInt32(iMap.FringeA[x, y].TileW);
                    outMSG.WriteVariableInt32(iMap.FringeA[x, y].TileH);
                    outMSG.WriteVariableInt32(iMap.FringeA[x, y].Tileset);
                }
            }

            SabertoothServer.netServer.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
        }

        static void SendMapData(NetIncomingMessage incMSG, int map)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.MapData);
            outMSG.WriteVariableInt32(maps[map].Id);
            outMSG.Write(maps[map].Name);
            outMSG.WriteVariableInt32(maps[map].Revision);
            outMSG.WriteVariableInt32(maps[map].TopMap);
            outMSG.WriteVariableInt32(maps[map].BottomMap);
            outMSG.WriteVariableInt32(maps[map].LeftMap);
            outMSG.WriteVariableInt32(maps[map].RightMap);
            outMSG.WriteVariableInt32(maps[map].Brightness);
            int maxx = maps[map].MaxX;
            int maxy = maps[map].MaxY;
            outMSG.WriteVariableInt32(maxx);
            outMSG.WriteVariableInt32(maxy);

            for (int x = 0; x < maxx; x++)
            {
                for (int y = 0; y < maxy; y++)
                {
                    //ground
                    outMSG.WriteVariableInt32(maps[map].Ground[x, y].TileX);
                    outMSG.WriteVariableInt32(maps[map].Ground[x, y].TileY);
                    outMSG.WriteVariableInt32(maps[map].Ground[x, y].TileW);
                    outMSG.WriteVariableInt32(maps[map].Ground[x, y].TileH);
                    outMSG.WriteVariableInt32(maps[map].Ground[x, y].Tileset);
                    outMSG.WriteVariableInt32(maps[map].Ground[x, y].Type);
                    outMSG.WriteVariableInt32(maps[map].Ground[x, y].SpawnNum);
                    outMSG.Write(maps[map].Ground[x, y].LightRadius);
                    //mask
                    outMSG.WriteVariableInt32(maps[map].Mask[x, y].TileX);
                    outMSG.WriteVariableInt32(maps[map].Mask[x, y].TileY);
                    outMSG.WriteVariableInt32(maps[map].Mask[x, y].TileW);
                    outMSG.WriteVariableInt32(maps[map].Mask[x, y].TileH);
                    outMSG.WriteVariableInt32(maps[map].Mask[x, y].Tileset);
                    //fringe
                    outMSG.WriteVariableInt32(maps[map].Fringe[x, y].TileX);
                    outMSG.WriteVariableInt32(maps[map].Fringe[x, y].TileY);
                    outMSG.WriteVariableInt32(maps[map].Fringe[x, y].TileW);
                    outMSG.WriteVariableInt32(maps[map].Fringe[x, y].TileH);
                    outMSG.WriteVariableInt32(maps[map].Fringe[x, y].Tileset);
                    //mask a
                    outMSG.WriteVariableInt32(maps[map].MaskA[x, y].TileX);
                    outMSG.WriteVariableInt32(maps[map].MaskA[x, y].TileY);
                    outMSG.WriteVariableInt32(maps[map].MaskA[x, y].TileW);
                    outMSG.WriteVariableInt32(maps[map].MaskA[x, y].TileH);
                    outMSG.WriteVariableInt32(maps[map].MaskA[x, y].Tileset);
                    //fringe a
                    outMSG.WriteVariableInt32(maps[map].FringeA[x, y].TileX);
                    outMSG.WriteVariableInt32(maps[map].FringeA[x, y].TileY);
                    outMSG.WriteVariableInt32(maps[map].FringeA[x, y].TileW);
                    outMSG.WriteVariableInt32(maps[map].FringeA[x, y].TileH);
                    outMSG.WriteVariableInt32(maps[map].FringeA[x, y].Tileset);
                }
            }
            
            SabertoothServer.netServer.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
        }

        static void SendClientDisconnect(NetIncomingMessage incMSG)
        {
            NetOutgoingMessage outMSG = SabertoothServer.netServer.CreateMessage();
            outMSG.Write((byte)PacketTypes.Shutdown);
            SabertoothServer.netServer.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.Unreliable);
        }
        #endregion

        #region Processing and Checking Voids
        static void ClearSlot(NetConnection conn)
        {
            for (int i = 0; i < MAX_PLAYERS; i++)
            {
                if (players[i] != null && players[i].Connection == conn)
                {
                    players[i] = null;
                    players[i] = new Player();
                    break;
                }
            }
        }

        static void SavePlayers()
        {
            for (int i = 0; i < MAX_PLAYERS; i++)
            {
                if (players[i].Name != null)
                {
                    players[i].SavePlayerToDatabase();
                }
            }
        }

        static void CheckCommand(string msg, int index, NetIncomingMessage incMSG)
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
                        players[index].Map = mapNum;
                        SendPlayerData(incMSG, index);
                        SendMapData(incMSG, mapNum);
                        SendMapNpcs(incMSG, mapNum);
                        SendPoolMapNpcs(incMSG, mapNum);
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
                    players[index].Sprite = spriteNum;
                    SendPlayers();
                    Console.WriteLine("Command:" + msg + " Spritenum: " + spriteNum);
                    return;
                }
            }
        }

        static bool AlreadyLogged(string name)
        {
            if (name == null) return false;

            for (int i = 0; i < 5; i++)
            {
                if (players[i].Name != null)
                {
                    string alname = players[i].Name.ToLower();
                    string alother = name.ToLower();
                    if (alname == alother)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        static int OpenSlot()
        {
            for (int i = 0; i < 5; i++)
            {
                if (players[i].Name == null)
                {
                    return i;
                }
            }
            return 5;
        }

        static int GetPlayerConnection(NetIncomingMessage incMSG)
        {
            for (int i = 0; i < 5; i++)
            {
                if (players[i].Connection == incMSG.SenderConnection)
                {
                    return i;
                }
            }
            return 5;
        }

        static bool CheckPassword(string name, string pass)
        {
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                string command;
                command = "SELECT * FROM Players WHERE Name=@name";
                using (var cmd = new SqlCommand(command, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = name;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string compare = reader[2].ToString();

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

        static bool AccountExist(string name)
        {
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                string command;
                command = "SELECT * FROM Players WHERE Name=@name";
                using (var cmd = new SqlCommand(command, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = name;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader[1].ToString().ToLower() == name.ToLower())
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
        UpdateWeapons,                
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
        PoolNpcVitals,
        ShopData,
        ShopItemData,
        ShopItemsData,
        Interaction,
        OpenShop,
        BuyItem,
        SellItem,
        SendChats,
        SendChatData,
        OpenChat,
        NextChat,
        CloseChat,
        PlayerBank,
        OpenBank,
        DepositItem,
        WithdrawItem,
        ChestData,
        SendChests,
        OpenChest,
        TakeChestItem,
        DateandTime,
        PlayTime,        
        AccountKey,
        RequestActivation,
        CreateBlood,
        ClearBlood,
        PlayerWarp,
        MeleeAttack,
        SendQuests,
        SendQuestData,
        SendQuestList,
        SendHotBar,
        SendInvSwap,
        SendBankSwap,
        UpdateHotBar,
        UseHotBar
    }
}
