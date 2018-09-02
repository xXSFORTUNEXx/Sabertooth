#undef DEBUG
using Gwen.Control;
using Lidgren.Network;
using System;
using static System.Convert;
using static System.Environment;
using static SabertoothClient.Client;
using static SabertoothClient.Globals;

namespace SabertoothClient
{
    public static class HandleData
    {
        public static int myIndex;
        public static int tempIndex;

        public static void HandleDataMessage()
        {
            NetIncomingMessage incMSG;

            if ((incMSG = SabertoothClient.netClient.ReadMessage()) != null)
            {
                switch (incMSG.MessageType)
                {
                    case NetIncomingMessageType.DiscoveryResponse:
                        HandleDiscoveryResponse(incMSG);
                        break;

                    case NetIncomingMessageType.Data:
                        switch (incMSG.ReadByte())
                        {
                            case (byte)PacketTypes.Connection:
                                HandleConnectionData(incMSG);
                                break;

                            case (byte)PacketTypes.ErrorMessage:
                                HandleErrorMessage(incMSG);
                                break;

                            case (byte)PacketTypes.Login:
                                HandleLoginData(incMSG);
                                break;

                            case (byte)PacketTypes.PlayerData:
                                myIndex = incMSG.ReadInt32();
                                HandlePlayerData(incMSG);
                                break;

                            case (byte)PacketTypes.ChatMessage:
                                HandleChatMessage(incMSG);
                                break;

                            case (byte)PacketTypes.MapData:
                                HandleMapData(incMSG);
                                break;

                            case (byte)PacketTypes.Players:
                                HandlePlayers(incMSG);
                                break;

                            case (byte)PacketTypes.UpdateMoveData:
                                HandleUpdateMoveData(incMSG);
                                break;

                            case (byte)PacketTypes.DirData:
                                HandleUpdateDirectionData(incMSG);
                                break;

                            case (byte)PacketTypes.Npcs:
                                HandleNpcs(incMSG);
                                break;

                            case (byte)PacketTypes.MapNpc:
                                HandleMapNpcs(incMSG);
                                break;

                            case (byte)PacketTypes.NpcData:
                                if (map.Name != null)
                                {
                                    HandleNpcData(incMSG);
                                }
                                break;

                            case (byte)PacketTypes.HealthData:
                                HandleHealthData(incMSG);
                                break;

                            case (byte)PacketTypes.VitalLoss:
                                HandleVitalData(incMSG);
                                break;

                            case (byte)PacketTypes.ItemData:
                                HandleItemData(incMSG);
                                break;

                            case (byte)PacketTypes.Items:
                                HandleItems(incMSG);
                                break;

                            case (byte)PacketTypes.Shutdown:
                                HandleServerShutdown();
                                break;

                            case (byte)PacketTypes.ProjData:
                                HandleProjData(incMSG);
                                break;

                            case (byte)PacketTypes.Projectiles:
                                HandleProjectiles(incMSG);
                                break;

                            case (byte)PacketTypes.UpdateAmmo:
                                HandleUpdateAmmo(incMSG);
                                break;

                            case (byte)PacketTypes.CreateProj:
                                HandleCreateProjectile(incMSG);
                                break;

                            case (byte)PacketTypes.ClearProj:
                                HandleClearProjectile(incMSG);
                                break;

                            case (byte)PacketTypes.UpdateWeapons:
                                HandleWeaponsUpdate(incMSG);
                                break;

                            case (byte)PacketTypes.UpdatePlayerStats:
                                HandleUpdatePlayerStats(incMSG);
                                break;

                            case (byte)PacketTypes.PoolNpcs:
                                HandlePoolNpcs(incMSG);
                                break;

                            case (byte)PacketTypes.PoolNpcData:
                                HandlePoolNpcData(incMSG);
                                break;

                            case (byte)PacketTypes.PlayerInv:
                                HandlePlayerInv(incMSG);
                                break;

                            case (byte)PacketTypes.MapItems:
                                HandleMapItems(incMSG);
                                break;

                            case (byte)PacketTypes.MapItemData:
                                HandleMapItemData(incMSG);
                                break;

                            case (byte)PacketTypes.PlayerEquip:
                                HandlePlayerEquipment(incMSG);
                                break;

                            case (byte)PacketTypes.NpcDirection:
                                HandleNpcDirection(incMSG);
                                break;

                            case (byte)PacketTypes.PoolNpcDirecion:
                                HandleNpcPoolDirection(incMSG);
                                break;

                            case (byte)PacketTypes.NpcVitals:
                                HandleNpcVitals(incMSG);
                                break;

                            case (byte)PacketTypes.PoolNpcVitals:
                                HandlePoolNpcVitals(incMSG);
                                break;

                            case (byte)PacketTypes.ShopData:
                                HandleShops(incMSG);
                                break;

                            case (byte)PacketTypes.ShopItemsData:
                                HandleShopItems(incMSG);
                                break;

                            case (byte)PacketTypes.ShopItemData:
                                HandleShopItemData(incMSG);
                                break;

                            case (byte)PacketTypes.OpenShop:
                                HandleOpenShop(incMSG);
                                break;

                            case (byte)PacketTypes.SendChats:
                                HandleChats(incMSG);
                                break;

                            case (byte)PacketTypes.SendChatData:
                                HandleChatData(incMSG);
                                break;

                            case (byte)PacketTypes.OpenChat:
                                HandleOpenChat(incMSG);
                                break;

                            case (byte)PacketTypes.NextChat:
                                HandleNextChat(incMSG);
                                break;

                            case (byte)PacketTypes.CloseChat:
                                HandleCloseChat(incMSG);
                                break;

                            case (byte)PacketTypes.PlayerBank:
                                HandlePlayerBank(incMSG);
                                break;

                            case (byte)PacketTypes.OpenBank:
                                HandleOpenBank(incMSG);
                                break;

                            case (byte)PacketTypes.SendChests:
                                HandleChests(incMSG);
                                break;

                            case (byte)PacketTypes.ChestData:
                                HandleSendChest(incMSG);
                                break;

                            case (byte)PacketTypes.OpenChest:
                                HandleOpenChest(incMSG);
                                break;

                            case (byte)PacketTypes.DateandTime:
                                HandleDateAndTime(incMSG);
                                break;

                            case (byte)PacketTypes.RequestActivation:
                                HandleActivationRequest(incMSG);
                                break;
                        }
                        break;
                }
                #if DEBUG
                Console.WriteLine("INCMSG Size: " + incMSG.LengthBytes + " btyes, " + incMSG.LengthBits + " bits, " + incMSG.DeliveryMethod.ToString());
                #endif
        }
            SabertoothClient.netClient.Recycle(incMSG);
        }

        #region Handle Incoming Data
        static void HandleActivationRequest(NetIncomingMessage incMSG)
        {
            tempIndex = incMSG.ReadVariableInt32();
            gui.CreateActivateWindow(canvas);
        }
        static void HandleDateAndTime(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();
            int year = incMSG.ReadVariableInt32();
            int month = incMSG.ReadVariableInt32();
            int day = incMSG.ReadVariableInt32();
            int hour = incMSG.ReadVariableInt32();
            int minute = incMSG.ReadVariableInt32();
            int second = incMSG.ReadVariableInt32();

            worldTime.g_Year = year;
            worldTime.g_Month = month;
            worldTime.g_DayOfWeek = day;
            worldTime.g_Hour = hour;
            worldTime.g_Minute = minute;
            worldTime.g_Second = second;
            worldTime.updateTime = true;
        }

        static void HandleOpenChest(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();

            if (!players[myIndex].inChest)
            {
                players[myIndex].chestNum = index;
                players[myIndex].inChest = true;
                gui.CreateChestWindow(canvas);
                gui.menuWindow.Show();
                gui.packTab.Press(gui.menuTabs);
                gui.charTab.Hide();
                gui.equipTab.Hide();
                gui.skillsTab.Hide();
                gui.missionTab.Hide();
                gui.optionsTab.Hide();
            }
        }

        static void HandleServerShutdown()
        {
            canvas.Dispose();
            SabertoothClient.netClient.Shutdown("Disconnect");
            Exit(0);
        }

        static void HandleCloseChat(NetIncomingMessage incMSG)
        {
            gui.npcChatWindow.Close();
            players[myIndex].inChat = false;
            players[myIndex].chatNum = 0;
        }

        static void HandleNextChat(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();

            players[myIndex].chatNum = index;
            players[myIndex].inChat = true;
            gui.UpdateNpcChatWindow(index - 1);
        }

        static void HandleOpenChat(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();

            if (!players[myIndex].inChat)
            {
                players[myIndex].chatNum = index;
                players[myIndex].inChat = true;
                gui.CreatNpcChatWindow(canvas, index - 1);
            }          
        }

        static void HandleChests(NetIncomingMessage incMSG)
        {
            for (int i = 0; i < 10; i++)
            {
                chests[i].Name = incMSG.ReadString();
                chests[i].Money = incMSG.ReadVariableInt32();
                chests[i].Experience = incMSG.ReadVariableInt32();
                chests[i].RequiredLevel = incMSG.ReadVariableInt32();
                chests[i].TrapLevel = incMSG.ReadVariableInt32();
                chests[i].Key = incMSG.ReadVariableInt32();
                chests[i].Damage = incMSG.ReadVariableInt32();
                chests[i].NpcSpawn = incMSG.ReadVariableInt32();
                chests[i].SpawnAmount = incMSG.ReadVariableInt32();

                for (int n = 0; n < 10; n++)
                {
                    chests[i].ChestItem[n].Name = incMSG.ReadString();
                    chests[i].ChestItem[n].ItemNum = incMSG.ReadVariableInt32();
                    chests[i].ChestItem[n].Value = incMSG.ReadVariableInt32();
                }
            }
        }

        static void HandleSendChest(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();

            chests[index].Name = incMSG.ReadString();
            chests[index].Money = incMSG.ReadVariableInt32();
            chests[index].Experience = incMSG.ReadVariableInt32();
            chests[index].RequiredLevel = incMSG.ReadVariableInt32();
            chests[index].TrapLevel = incMSG.ReadVariableInt32();
            chests[index].Key = incMSG.ReadVariableInt32();
            chests[index].Damage = incMSG.ReadVariableInt32();
            chests[index].NpcSpawn = incMSG.ReadVariableInt32();
            chests[index].SpawnAmount = incMSG.ReadVariableInt32();

            for (int n = 0; n < 10; n++)
            {
                chests[index].ChestItem[n].Name = incMSG.ReadString();
                chests[index].ChestItem[n].ItemNum = incMSG.ReadVariableInt32();
                chests[index].ChestItem[n].Value = incMSG.ReadVariableInt32();
            }
        }

        static void HandleChats(NetIncomingMessage incMSG)
        {
            for (int i = 0; i < 15; i++)
            {
                chats[i].Name = incMSG.ReadString();
                chats[i].MainMessage = incMSG.ReadString();
                chats[i].Option[0] = incMSG.ReadString();
                chats[i].Option[1] = incMSG.ReadString();
                chats[i].Option[2] = incMSG.ReadString();
                chats[i].Option[3] = incMSG.ReadString();
                chats[i].NextChat[0] = incMSG.ReadVariableInt32();
                chats[i].NextChat[1] = incMSG.ReadVariableInt32();
                chats[i].NextChat[2] = incMSG.ReadVariableInt32();
                chats[i].NextChat[3] = incMSG.ReadVariableInt32();
                chats[i].ShopNum = incMSG.ReadVariableInt32();
                chats[i].MissionNum = incMSG.ReadVariableInt32();
                chats[i].ItemNum[0] = incMSG.ReadVariableInt32();
                chats[i].ItemNum[1] = incMSG.ReadVariableInt32();
                chats[i].ItemNum[2] = incMSG.ReadVariableInt32();
                chats[i].ItemVal[0] = incMSG.ReadVariableInt32();
                chats[i].ItemVal[1] = incMSG.ReadVariableInt32();
                chats[i].ItemVal[2] = incMSG.ReadVariableInt32();
                chats[i].Money = incMSG.ReadVariableInt32();
                chats[i].Type = incMSG.ReadVariableInt32();
            }
        }

        static void HandleChatData(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();

            chats[index].Name = incMSG.ReadString();
            chats[index].MainMessage = incMSG.ReadString();
            chats[index].Option[0] = incMSG.ReadString();
            chats[index].Option[1] = incMSG.ReadString();
            chats[index].Option[2] = incMSG.ReadString();
            chats[index].Option[3] = incMSG.ReadString();
            chats[index].NextChat[0] = incMSG.ReadVariableInt32();
            chats[index].NextChat[1] = incMSG.ReadVariableInt32();
            chats[index].NextChat[2] = incMSG.ReadVariableInt32();
            chats[index].NextChat[3] = incMSG.ReadVariableInt32();
            chats[index].ShopNum = incMSG.ReadVariableInt32();
            chats[index].MissionNum = incMSG.ReadVariableInt32();
            chats[index].ItemNum[0] = incMSG.ReadVariableInt32();
            chats[index].ItemNum[1] = incMSG.ReadVariableInt32();
            chats[index].ItemNum[2] = incMSG.ReadVariableInt32();
            chats[index].ItemVal[0] = incMSG.ReadVariableInt32();
            chats[index].ItemVal[1] = incMSG.ReadVariableInt32();
            chats[index].ItemVal[2] = incMSG.ReadVariableInt32();
            chats[index].Money = incMSG.ReadVariableInt32();
            chats[index].Type = incMSG.ReadVariableInt32();
        }

        static void HandleOpenShop(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();

            if (!players[myIndex].inShop)
            {
                players[myIndex].shopNum = index;
                players[myIndex].inShop = true;
                gui.CreateShopWindow(canvas);
                gui.menuWindow.Show();
                gui.packTab.Press(gui.menuTabs);
                gui.charTab.Hide();
                gui.equipTab.Hide();
                gui.skillsTab.Hide();
                gui.missionTab.Hide();
                gui.optionsTab.Hide();
            }
        }

        static void HandleOpenBank(NetIncomingMessage incMSG)
        {
            if (!players[myIndex].inBank)
            {
                players[myIndex].inBank = true;
                gui.CreateBankWindow(canvas);
                gui.menuWindow.Show();
                gui.packTab.Press(gui.menuTabs);
                gui.charTab.Hide();
                gui.equipTab.Hide();
                gui.skillsTab.Hide();
                gui.missionTab.Hide();
                gui.optionsTab.Hide();
            }
        }

        static void HandleShops(NetIncomingMessage incMSG)
        {
            for (int i = 0; i < 10; i++)
            {
                shops[i].Name = incMSG.ReadString();
                for (int n = 0; n < 25; n++)
                {
                    shops[i].shopItem[n].Name = incMSG.ReadString();
                    shops[i].shopItem[n].ItemNum = incMSG.ReadVariableInt32();
                    shops[i].shopItem[n].Cost = incMSG.ReadVariableInt32();
                }
            }
        }

        static void HandleShopItems(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();

            for (int n = 0; n < 25; n++)
            {
                shops[index].shopItem[n].Name = incMSG.ReadString();
                shops[index].shopItem[n].ItemNum = incMSG.ReadVariableInt32();
                shops[index].shopItem[n].Cost = incMSG.ReadVariableInt32();
            }
        }

        static void HandleShopItemData(NetIncomingMessage incMSG)
        {
            int shopIndex = incMSG.ReadVariableInt32();
            int index = incMSG.ReadVariableInt32();

            shops[shopIndex].shopItem[index].Name = incMSG.ReadString();
            shops[shopIndex].shopItem[index].ItemNum = incMSG.ReadVariableInt32();
            shops[shopIndex].shopItem[index].Cost = incMSG.ReadVariableInt32();
        }

        static void HandlePlayerEquipment(NetIncomingMessage incMSG)
        {
            players[myIndex].Chest.Name = incMSG.ReadString();
            players[myIndex].Chest.Sprite = incMSG.ReadVariableInt32();
            players[myIndex].Chest.Damage = incMSG.ReadVariableInt32();
            players[myIndex].Chest.Armor = incMSG.ReadVariableInt32();
            players[myIndex].Chest.Type = incMSG.ReadVariableInt32();
            players[myIndex].Chest.AttackSpeed = incMSG.ReadVariableInt32();
            players[myIndex].Chest.ReloadSpeed = incMSG.ReadVariableInt32();
            players[myIndex].Chest.HealthRestore = incMSG.ReadVariableInt32();
            players[myIndex].Chest.HungerRestore = incMSG.ReadVariableInt32();
            players[myIndex].Chest.HydrateRestore = incMSG.ReadVariableInt32();
            players[myIndex].Chest.Strength = incMSG.ReadVariableInt32();
            players[myIndex].Chest.Agility = incMSG.ReadVariableInt32();
            players[myIndex].Chest.Endurance = incMSG.ReadVariableInt32();
            players[myIndex].Chest.Stamina = incMSG.ReadVariableInt32();
            players[myIndex].Chest.Clip = incMSG.ReadVariableInt32();
            players[myIndex].Chest.MaxClip = incMSG.ReadVariableInt32();
            players[myIndex].Chest.ItemAmmoType = incMSG.ReadVariableInt32();
            players[myIndex].Chest.Value = incMSG.ReadVariableInt32();
            players[myIndex].Chest.ProjectileNumber = incMSG.ReadVariableInt32();
            players[myIndex].Chest.Price= incMSG.ReadVariableInt32();
            players[myIndex].Chest.Rarity = incMSG.ReadVariableInt32();

            players[myIndex].Legs.Name = incMSG.ReadString();
            players[myIndex].Legs.Sprite = incMSG.ReadVariableInt32();
            players[myIndex].Legs.Damage = incMSG.ReadVariableInt32();
            players[myIndex].Legs.Armor = incMSG.ReadVariableInt32();
            players[myIndex].Legs.Type = incMSG.ReadVariableInt32();
            players[myIndex].Legs.AttackSpeed = incMSG.ReadVariableInt32();
            players[myIndex].Legs.ReloadSpeed = incMSG.ReadVariableInt32();
            players[myIndex].Legs.HealthRestore = incMSG.ReadVariableInt32();
            players[myIndex].Legs.HungerRestore = incMSG.ReadVariableInt32();
            players[myIndex].Legs.HydrateRestore = incMSG.ReadVariableInt32();
            players[myIndex].Legs.Strength = incMSG.ReadVariableInt32();
            players[myIndex].Legs.Agility = incMSG.ReadVariableInt32();
            players[myIndex].Legs.Endurance = incMSG.ReadVariableInt32();
            players[myIndex].Legs.Stamina = incMSG.ReadVariableInt32();
            players[myIndex].Legs.Clip = incMSG.ReadVariableInt32();
            players[myIndex].Legs.MaxClip = incMSG.ReadVariableInt32();
            players[myIndex].Legs.ItemAmmoType = incMSG.ReadVariableInt32();
            players[myIndex].Legs.Value = incMSG.ReadVariableInt32();
            players[myIndex].Legs.ProjectileNumber = incMSG.ReadVariableInt32();
            players[myIndex].Legs.Price = incMSG.ReadVariableInt32();
            players[myIndex].Legs.Rarity = incMSG.ReadVariableInt32();

            players[myIndex].Feet.Name = incMSG.ReadString();
            players[myIndex].Feet.Sprite = incMSG.ReadVariableInt32();
            players[myIndex].Feet.Damage = incMSG.ReadVariableInt32();
            players[myIndex].Feet.Armor = incMSG.ReadVariableInt32();
            players[myIndex].Feet.Type = incMSG.ReadVariableInt32();
            players[myIndex].Feet.AttackSpeed = incMSG.ReadVariableInt32();
            players[myIndex].Feet.ReloadSpeed = incMSG.ReadVariableInt32();
            players[myIndex].Feet.HealthRestore = incMSG.ReadVariableInt32();
            players[myIndex].Feet.HungerRestore = incMSG.ReadVariableInt32();
            players[myIndex].Feet.HydrateRestore = incMSG.ReadVariableInt32();
            players[myIndex].Feet.Strength = incMSG.ReadVariableInt32();
            players[myIndex].Feet.Agility = incMSG.ReadVariableInt32();
            players[myIndex].Feet.Endurance = incMSG.ReadVariableInt32();
            players[myIndex].Feet.Stamina = incMSG.ReadVariableInt32();
            players[myIndex].Feet.Clip = incMSG.ReadVariableInt32();
            players[myIndex].Feet.MaxClip = incMSG.ReadVariableInt32();
            players[myIndex].Feet.ItemAmmoType = incMSG.ReadVariableInt32();
            players[myIndex].Feet.Value = incMSG.ReadVariableInt32();
            players[myIndex].Feet.ProjectileNumber = incMSG.ReadVariableInt32();
            players[myIndex].Feet.Price = incMSG.ReadVariableInt32();
            players[myIndex].Feet.Rarity = incMSG.ReadVariableInt32();
        }

        static void HandlePlayerInv(NetIncomingMessage incMSG)
        {
            for (int i = 0; i < 25; i++)
            {
                if (players[myIndex].Backpack[i] != null)
                {
                    players[myIndex].Backpack[i].Name = incMSG.ReadString();
                    players[myIndex].Backpack[i].Sprite = incMSG.ReadVariableInt32();
                    players[myIndex].Backpack[i].Damage = incMSG.ReadVariableInt32();
                    players[myIndex].Backpack[i].Armor = incMSG.ReadVariableInt32();
                    players[myIndex].Backpack[i].Type = incMSG.ReadVariableInt32();
                    players[myIndex].Backpack[i].AttackSpeed = incMSG.ReadVariableInt32();
                    players[myIndex].Backpack[i].ReloadSpeed = incMSG.ReadVariableInt32();
                    players[myIndex].Backpack[i].HealthRestore = incMSG.ReadVariableInt32();
                    players[myIndex].Backpack[i].HungerRestore = incMSG.ReadVariableInt32();
                    players[myIndex].Backpack[i].HydrateRestore = incMSG.ReadVariableInt32();
                    players[myIndex].Backpack[i].Strength = incMSG.ReadVariableInt32();
                    players[myIndex].Backpack[i].Agility = incMSG.ReadVariableInt32();
                    players[myIndex].Backpack[i].Endurance = incMSG.ReadVariableInt32();
                    players[myIndex].Backpack[i].Stamina = incMSG.ReadVariableInt32();
                    players[myIndex].Backpack[i].Clip = incMSG.ReadVariableInt32();
                    players[myIndex].Backpack[i].MaxClip = incMSG.ReadVariableInt32();
                    players[myIndex].Backpack[i].ItemAmmoType = incMSG.ReadVariableInt32();
                    players[myIndex].Backpack[i].Value = incMSG.ReadVariableInt32();
                    players[myIndex].Backpack[i].ProjectileNumber = incMSG.ReadVariableInt32();
                    players[myIndex].Backpack[i].Price = incMSG.ReadVariableInt32();
                    players[myIndex].Backpack[i].Rarity = incMSG.ReadVariableInt32();
                }
            }
        }

        static void HandlePlayerBank(NetIncomingMessage incMSG)
        {
            for (int i = 0; i < 50; i++)
            {
                if (players[myIndex].Bank[i] != null)
                {
                    players[myIndex].Bank[i].Name = incMSG.ReadString();
                    players[myIndex].Bank[i].Sprite = incMSG.ReadVariableInt32();
                    players[myIndex].Bank[i].Damage = incMSG.ReadVariableInt32();
                    players[myIndex].Bank[i].Armor = incMSG.ReadVariableInt32();
                    players[myIndex].Bank[i].Type = incMSG.ReadVariableInt32();
                    players[myIndex].Bank[i].AttackSpeed = incMSG.ReadVariableInt32();
                    players[myIndex].Bank[i].ReloadSpeed = incMSG.ReadVariableInt32();
                    players[myIndex].Bank[i].HealthRestore = incMSG.ReadVariableInt32();
                    players[myIndex].Bank[i].HungerRestore = incMSG.ReadVariableInt32();
                    players[myIndex].Bank[i].HydrateRestore = incMSG.ReadVariableInt32();
                    players[myIndex].Bank[i].Strength = incMSG.ReadVariableInt32();
                    players[myIndex].Bank[i].Agility = incMSG.ReadVariableInt32();
                    players[myIndex].Bank[i].Endurance = incMSG.ReadVariableInt32();
                    players[myIndex].Bank[i].Stamina = incMSG.ReadVariableInt32();
                    players[myIndex].Bank[i].Clip = incMSG.ReadVariableInt32();
                    players[myIndex].Bank[i].MaxClip = incMSG.ReadVariableInt32();
                    players[myIndex].Bank[i].ItemAmmoType = incMSG.ReadVariableInt32();
                    players[myIndex].Bank[i].Value = incMSG.ReadVariableInt32();
                    players[myIndex].Bank[i].ProjectileNumber = incMSG.ReadVariableInt32();
                    players[myIndex].Bank[i].Price = incMSG.ReadVariableInt32();
                    players[myIndex].Bank[i].Rarity = incMSG.ReadVariableInt32();
                }
            }
        }

        static void HandleClearProjectile(NetIncomingMessage incMSG)
        {
            string mapName = incMSG.ReadString();
            if (mapName != map.Name) { return; }

            int slot = incMSG.ReadVariableInt32();
            if (map.m_MapProj[slot] != null) { return; }
            map.m_MapProj[slot] = null;
        }

        static void HandleCreateProjectile(NetIncomingMessage incMSG)
        {
            int slot = incMSG.ReadVariableInt32();
            int proj = incMSG.ReadVariableInt32();

            map.m_MapProj[slot] = new MapProj();

            map.m_MapProj[slot].X = incMSG.ReadVariableInt32();
            map.m_MapProj[slot].Y = incMSG.ReadVariableInt32();
            map.m_MapProj[slot].Direction = incMSG.ReadVariableInt32();
            map.m_MapProj[slot].Name = projectiles[proj].Name;
            map.m_MapProj[slot].Speed = projectiles[proj].Speed;
            map.m_MapProj[slot].Type = projectiles[proj].Type;
            map.m_MapProj[slot].Sprite = projectiles[proj].Sprite;
            map.m_MapProj[slot].Range = projectiles[proj].Range;
        }

        static void HandleUpdateAmmo(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();

            players[myIndex].mainWeapon.Clip = incMSG.ReadVariableInt32();
            players[myIndex].PistolAmmo = incMSG.ReadVariableInt32();
            players[myIndex].AssaultAmmo = incMSG.ReadVariableInt32();
            players[myIndex].RocketAmmo = incMSG.ReadVariableInt32();
            players[myIndex].GrenadeAmmo = incMSG.ReadVariableInt32();
        }

        static void HandleProjectiles(NetIncomingMessage incMSG)
        {
            for (int i = 0; i < MAX_PROJECTILES; i++)
            {
                if (projectiles[i] != null)
                {
                    projectiles[i].Name = incMSG.ReadString();
                    projectiles[i].Damage = incMSG.ReadVariableInt32();
                    projectiles[i].Range = incMSG.ReadVariableInt32();
                    projectiles[i].Sprite = incMSG.ReadVariableInt32();
                    projectiles[i].Type = incMSG.ReadVariableInt32();
                    projectiles[i].Speed = incMSG.ReadVariableInt32();
                }
            }
        }

        static void HandleProjData(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();

            projectiles[index].Name = incMSG.ReadString();
            projectiles[index].Damage = incMSG.ReadVariableInt32();
            projectiles[index].Range = incMSG.ReadVariableInt32();
            projectiles[index].Sprite = incMSG.ReadVariableInt32();
            projectiles[index].Type = incMSG.ReadVariableInt32();
            projectiles[index].Speed = incMSG.ReadVariableInt32();
        }

        static void HandleItems(NetIncomingMessage incMSG)
        {
            for (int i = 0; i < MAX_ITEMS; i++)
            {
                if (items[i] != null)
                {
                    items[i].Name = incMSG.ReadString();
                    items[i].Sprite = incMSG.ReadVariableInt32();
                    items[i].Damage = incMSG.ReadVariableInt32();
                    items[i].Armor = incMSG.ReadVariableInt32();
                    items[i].Type = incMSG.ReadVariableInt32();
                    items[i].HealthRestore = incMSG.ReadVariableInt32();
                    items[i].HungerRestore = incMSG.ReadVariableInt32();
                    items[i].HydrateRestore = incMSG.ReadVariableInt32();
                    items[i].Strength = incMSG.ReadVariableInt32();
                    items[i].Agility = incMSG.ReadVariableInt32();
                    items[i].Endurance = incMSG.ReadVariableInt32();
                    items[i].Stamina = incMSG.ReadVariableInt32();
                    items[i].Clip = incMSG.ReadVariableInt32();
                    items[i].MaxClip = incMSG.ReadVariableInt32();
                    items[i].ItemAmmoType = incMSG.ReadVariableInt32();
                    items[i].Value = incMSG.ReadVariableInt32();
                    items[i].ProjectileNumber = incMSG.ReadVariableInt32();
                    items[i].Price = incMSG.ReadVariableInt32();
                    items[i].Rarity = incMSG.ReadVariableInt32();
                }
            }
        }

        static void HandleItemData(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();

            items[index].Name = incMSG.ReadString();
            items[index].Sprite = incMSG.ReadVariableInt32();
            items[index].Damage = incMSG.ReadVariableInt32();
            items[index].Armor = incMSG.ReadVariableInt32();
            items[index].Type = incMSG.ReadVariableInt32();
            items[index].HealthRestore = incMSG.ReadVariableInt32();
            items[index].HungerRestore = incMSG.ReadVariableInt32();
            items[index].HydrateRestore = incMSG.ReadVariableInt32();
            items[index].Strength = incMSG.ReadVariableInt32();
            items[index].Agility = incMSG.ReadVariableInt32();
            items[index].Endurance = incMSG.ReadVariableInt32();
            items[index].Stamina = incMSG.ReadVariableInt32();
            items[index].Clip = incMSG.ReadVariableInt32();
            items[index].MaxClip = incMSG.ReadVariableInt32();
            items[index].ItemAmmoType = incMSG.ReadVariableInt32();
            items[index].Value = incMSG.ReadVariableInt32();
            items[index].ProjectileNumber = incMSG.ReadVariableInt32();
            items[index].Price = incMSG.ReadVariableInt32();
            items[index].Rarity = incMSG.ReadVariableInt32();
        }

        static void HandleNpcs(NetIncomingMessage incMSG)
        {
            for (int i = 0; i < MAX_NPCS; i++)
            {
                if (npcs[i] != null)
                {
                    npcs[i].Name = incMSG.ReadString();
                    npcs[i].X = incMSG.ReadVariableInt32();
                    npcs[i].Y = incMSG.ReadVariableInt32();
                    npcs[i].Direction = incMSG.ReadVariableInt32();
                    npcs[i].Sprite = incMSG.ReadVariableInt32();
                    npcs[i].Step = incMSG.ReadVariableInt32();
                    npcs[i].Owner = incMSG.ReadVariableInt32();
                    npcs[i].Behavior = incMSG.ReadVariableInt32();
                    npcs[i].SpawnTime = incMSG.ReadVariableInt32();
                    npcs[i].Health = incMSG.ReadVariableInt32();
                    npcs[i].MaxHealth = incMSG.ReadVariableInt32();
                    npcs[i].Damage = incMSG.ReadVariableInt32();
                    npcs[i].IsSpawned = incMSG.ReadBoolean();
                }
            }
        }

        static void HandlePoolNpcs(NetIncomingMessage incMSG)
        {
            for (int i = 0; i < MAX_MAP_POOL_NPCS; i++)
            {
                if (map.r_MapNpc[i] != null)
                {
                    map.r_MapNpc[i].Name = incMSG.ReadString();
                    map.r_MapNpc[i].X = incMSG.ReadVariableInt32();
                    map.r_MapNpc[i].Y = incMSG.ReadVariableInt32();
                    map.r_MapNpc[i].Direction = incMSG.ReadVariableInt32();
                    map.r_MapNpc[i].Sprite = incMSG.ReadVariableInt32();
                    map.r_MapNpc[i].Step = incMSG.ReadVariableInt32();
                    map.r_MapNpc[i].Owner = incMSG.ReadVariableInt32();
                    map.r_MapNpc[i].Behavior = incMSG.ReadVariableInt32();
                    map.r_MapNpc[i].SpawnTime = incMSG.ReadVariableInt32();
                    map.r_MapNpc[i].Health = incMSG.ReadVariableInt32();
                    map.r_MapNpc[i].MaxHealth = incMSG.ReadVariableInt32();
                    map.r_MapNpc[i].Damage = incMSG.ReadVariableInt32();
                    map.r_MapNpc[i].IsSpawned = incMSG.ReadBoolean();
                }
            }
        }

        static void HandleMapItems(NetIncomingMessage incMSG)
        {
            for (int i = 0; i < MAX_MAP_ITEMS; i++)
            {
                map.m_MapItem[i].Name = incMSG.ReadString();
                map.m_MapItem[i].X = incMSG.ReadVariableInt32();
                map.m_MapItem[i].Y = incMSG.ReadVariableInt32();
                map.m_MapItem[i].Sprite = incMSG.ReadVariableInt32();
                map.m_MapItem[i].Damage = incMSG.ReadVariableInt32();
                map.m_MapItem[i].Armor = incMSG.ReadVariableInt32();
                map.m_MapItem[i].Type = incMSG.ReadVariableInt32();
                map.m_MapItem[i].HealthRestore = incMSG.ReadVariableInt32();
                map.m_MapItem[i].HungerRestore = incMSG.ReadVariableInt32();
                map.m_MapItem[i].HydrateRestore = incMSG.ReadVariableInt32();
                map.m_MapItem[i].Strength = incMSG.ReadVariableInt32();
                map.m_MapItem[i].Agility = incMSG.ReadVariableInt32();
                map.m_MapItem[i].Endurance = incMSG.ReadVariableInt32();
                map.m_MapItem[i].Stamina = incMSG.ReadVariableInt32();
                map.m_MapItem[i].Clip = incMSG.ReadVariableInt32();
                map.m_MapItem[i].MaxClip = incMSG.ReadVariableInt32();
                map.m_MapItem[i].ItemAmmoType = incMSG.ReadVariableInt32();
                map.m_MapItem[i].Value = incMSG.ReadVariableInt32();
                map.m_MapItem[i].ProjectileNumber = incMSG.ReadVariableInt32();
                map.m_MapItem[i].Price = incMSG.ReadVariableInt32();
                map.m_MapItem[i].Rarity = incMSG.ReadVariableInt32();
                map.m_MapItem[i].IsSpawned = incMSG.ReadBoolean();
            }
            LoadMainGUI();
            gui.Ready = true;
        }

        static void HandleMapItemData(NetIncomingMessage incMSG)
        {
            int itemNum = incMSG.ReadVariableInt32();

            map.m_MapItem[itemNum].Name = incMSG.ReadString();
            map.m_MapItem[itemNum].X = incMSG.ReadVariableInt32();
            map.m_MapItem[itemNum].Y = incMSG.ReadVariableInt32();
            map.m_MapItem[itemNum].Sprite = incMSG.ReadVariableInt32();
            map.m_MapItem[itemNum].Damage = incMSG.ReadVariableInt32();
            map.m_MapItem[itemNum].Armor = incMSG.ReadVariableInt32();
            map.m_MapItem[itemNum].Type = incMSG.ReadVariableInt32();
            map.m_MapItem[itemNum].HealthRestore = incMSG.ReadVariableInt32();
            map.m_MapItem[itemNum].HungerRestore = incMSG.ReadVariableInt32();
            map.m_MapItem[itemNum].HydrateRestore = incMSG.ReadVariableInt32();
            map.m_MapItem[itemNum].Strength = incMSG.ReadVariableInt32();
            map.m_MapItem[itemNum].Agility = incMSG.ReadVariableInt32();
            map.m_MapItem[itemNum].Endurance = incMSG.ReadVariableInt32();
            map.m_MapItem[itemNum].Stamina = incMSG.ReadVariableInt32();
            map.m_MapItem[itemNum].Clip = incMSG.ReadVariableInt32();
            map.m_MapItem[itemNum].MaxClip = incMSG.ReadVariableInt32();
            map.m_MapItem[itemNum].ItemAmmoType = incMSG.ReadVariableInt32();
            map.m_MapItem[itemNum].Value = incMSG.ReadVariableInt32();
            map.m_MapItem[itemNum].ProjectileNumber = incMSG.ReadVariableInt32();
            map.m_MapItem[itemNum].Price = incMSG.ReadVariableInt32();
            map.m_MapItem[itemNum].Rarity = incMSG.ReadVariableInt32();
            map.m_MapItem[itemNum].IsSpawned = incMSG.ReadBoolean();
        }

        static void HandleMapNpcs(NetIncomingMessage incMSG)
        {
            for (int i = 0; i < MAX_MAP_NPCS; i++)
            {
                if (map.m_MapNpc[i] != null)
                {
                    map.m_MapNpc[i].Name = incMSG.ReadString();
                    map.m_MapNpc[i].X = incMSG.ReadVariableInt32();
                    map.m_MapNpc[i].Y = incMSG.ReadVariableInt32();
                    map.m_MapNpc[i].Direction = incMSG.ReadVariableInt32();
                    map.m_MapNpc[i].Sprite = incMSG.ReadVariableInt32();
                    map.m_MapNpc[i].Step = incMSG.ReadVariableInt32();
                    map.m_MapNpc[i].Owner = incMSG.ReadVariableInt32();
                    map.m_MapNpc[i].Behavior = incMSG.ReadVariableInt32();
                    map.m_MapNpc[i].SpawnTime = incMSG.ReadVariableInt32();
                    map.m_MapNpc[i].Health = incMSG.ReadVariableInt32();
                    map.m_MapNpc[i].MaxHealth = incMSG.ReadVariableInt32();
                    map.m_MapNpc[i].Damage = incMSG.ReadVariableInt32();
                    map.m_MapNpc[i].IsSpawned = incMSG.ReadBoolean();
                }
            }
        }

        static void HandlePoolNpcData(NetIncomingMessage incMSG)
        {
            int npcNum = incMSG.ReadVariableInt32();

            map.r_MapNpc[npcNum].Name = incMSG.ReadString();
            map.r_MapNpc[npcNum].X = incMSG.ReadVariableInt32();
            map.r_MapNpc[npcNum].Y = incMSG.ReadVariableInt32();
            map.r_MapNpc[npcNum].Direction = incMSG.ReadVariableInt32();
            map.r_MapNpc[npcNum].Sprite = incMSG.ReadVariableInt32();
            map.r_MapNpc[npcNum].Step = incMSG.ReadVariableInt32();
            map.r_MapNpc[npcNum].Owner = incMSG.ReadVariableInt32();
            map.r_MapNpc[npcNum].Behavior = incMSG.ReadVariableInt32();
            map.r_MapNpc[npcNum].SpawnTime = incMSG.ReadVariableInt32();
            map.r_MapNpc[npcNum].Health = incMSG.ReadVariableInt32();
            map.r_MapNpc[npcNum].MaxHealth = incMSG.ReadVariableInt32();
            map.r_MapNpc[npcNum].Damage = incMSG.ReadVariableInt32();
            map.r_MapNpc[npcNum].IsSpawned = incMSG.ReadBoolean();
        }

        static void HandleNpcDirection(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();

            map.m_MapNpc[index].X = incMSG.ReadVariableInt32();
            map.m_MapNpc[index].Y = incMSG.ReadVariableInt32();
            map.m_MapNpc[index].Direction = incMSG.ReadVariableInt32();
            map.m_MapNpc[index].Step = incMSG.ReadVariableInt32();
        }

        static void HandleNpcPoolDirection(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();

            map.r_MapNpc[index].X = incMSG.ReadVariableInt32();
            map.r_MapNpc[index].Y = incMSG.ReadVariableInt32();
            map.r_MapNpc[index].Direction = incMSG.ReadVariableInt32();
            map.r_MapNpc[index].Step = incMSG.ReadVariableInt32();
        }

        static void HandleNpcVitals(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();

            map.m_MapNpc[index].Health = incMSG.ReadVariableInt32();
            map.m_MapNpc[index].IsSpawned = incMSG.ReadBoolean();
        }

        static void HandlePoolNpcVitals(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();

            map.r_MapNpc[index].Health = incMSG.ReadVariableInt32();
            map.r_MapNpc[index].IsSpawned = incMSG.ReadBoolean();
        }

        static void HandleNpcData(NetIncomingMessage incMSG)
        {
            int npcNum = incMSG.ReadVariableInt32();

            map.m_MapNpc[npcNum].Name = incMSG.ReadString();
            map.m_MapNpc[npcNum].X = incMSG.ReadVariableInt32();
            map.m_MapNpc[npcNum].Y = incMSG.ReadVariableInt32();
            map.m_MapNpc[npcNum].Direction = incMSG.ReadVariableInt32();
            map.m_MapNpc[npcNum].Sprite = incMSG.ReadVariableInt32();
            map.m_MapNpc[npcNum].Step = incMSG.ReadVariableInt32();
            map.m_MapNpc[npcNum].Owner = incMSG.ReadVariableInt32();
            map.m_MapNpc[npcNum].Behavior = incMSG.ReadVariableInt32();
            map.m_MapNpc[npcNum].SpawnTime = incMSG.ReadVariableInt32();
            map.m_MapNpc[npcNum].Health = incMSG.ReadVariableInt32();
            map.m_MapNpc[npcNum].MaxHealth = incMSG.ReadVariableInt32();
            map.m_MapNpc[npcNum].Damage = incMSG.ReadVariableInt32();
            map.m_MapNpc[npcNum].IsSpawned = incMSG.ReadBoolean();
        }

        static void HandleUpdateDirectionData(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();
            int direction = incMSG.ReadVariableInt32();
            int aimdirection = incMSG.ReadVariableInt32();

            players[index].Direction = direction;
            players[index].AimDirection = aimdirection;
        }

        static void HandleUpdateMoveData(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();
            int x = incMSG.ReadVariableInt32();
            int y = incMSG.ReadVariableInt32();
            int direction = incMSG.ReadVariableInt32();
            int aimdirection = incMSG.ReadVariableInt32();
            int step = incMSG.ReadVariableInt32();

            players[index].tempX = x;
            players[index].tempY = y;
            players[index].tempDir = direction;
            players[index].tempaimDir = aimdirection;
            players[index].tempStep = step;
        }

        static void HandleDiscoveryResponse(NetIncomingMessage incMSG)
        {
            Console.WriteLine("Found Server: " + incMSG.ReadString() + " @ " + incMSG.SenderEndPoint);
            NetOutgoingMessage outMSG = SabertoothClient.netClient.CreateMessage();
            outMSG.Write((byte)PacketTypes.Connection);
            outMSG.Write("sabertooth");
            SabertoothClient.netClient.Connect(IPAddress, ToInt32(Port), outMSG);
        }

        static void HandleConnectionData(NetIncomingMessage incMSG)
        {
            if (SabertoothClient.netClient.ServerConnection != null) { return; }
            Console.WriteLine("Connected to server!");        }

        static void HandleChatMessage(NetIncomingMessage incMSG)
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
                gui.outputChat.AddRow(splitMsg[0]);
                gui.outputChat.AddRow(splitMsg[1]);
            }
            else
            {
                gui.outputChat.AddRow(msg);
            }
            if (!gui.chatWindow.IsVisible) { gui.chatWindow.Show(); }
            gui.outputChat.ScrollToBottom();
            gui.outputChat.UnselectAll();
        }

        static void HandleLoginData(NetIncomingMessage incMSG)
        {
            Console.WriteLine("Login successful! IP: " + incMSG.SenderConnection);
            canvas.DeleteAllChildren();
            gui.CreateLoadingWindow(canvas);
            gui.Ready = false;
        }

        static void HandleVitalData(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();
            string vitalName = incMSG.ReadString();
            int vital = incMSG.ReadVariableInt32();

            if (vitalName == "food") { players[myIndex].Hunger = vital; }
            if (vitalName == "water") { players[myIndex].Hydration = vital; }
        }

        static void HandleHealthData(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();
            int health = incMSG.ReadVariableInt32();

            players[myIndex].Health = health;
        }

        static void HandleUpdatePlayerStats(NetIncomingMessage incMSG)
        {
            players[myIndex].Level = incMSG.ReadVariableInt32();
            players[myIndex].Points = incMSG.ReadVariableInt32();
            players[myIndex].Health = incMSG.ReadVariableInt32();
            players[myIndex].MaxHealth = incMSG.ReadVariableInt32();
            players[myIndex].Hunger = incMSG.ReadVariableInt32();
            players[myIndex].Hydration = incMSG.ReadVariableInt32();
            players[myIndex].Experience = incMSG.ReadVariableInt32();
            players[myIndex].Money = incMSG.ReadVariableInt32();
            players[myIndex].Armor = incMSG.ReadVariableInt32();
            players[myIndex].Strength = incMSG.ReadVariableInt32();
            players[myIndex].Agility = incMSG.ReadVariableInt32();
            players[myIndex].Endurance = incMSG.ReadVariableInt32();
            players[myIndex].Stamina = incMSG.ReadVariableInt32();
            players[myIndex].PistolAmmo = incMSG.ReadVariableInt32();
            players[myIndex].AssaultAmmo = incMSG.ReadVariableInt32();
            players[myIndex].RocketAmmo = incMSG.ReadVariableInt32();
            players[myIndex].GrenadeAmmo = incMSG.ReadVariableInt32();
            players[myIndex].LightRadius = incMSG.ReadVariableInt32();
        }

        static void HandlePlayerData(NetIncomingMessage incMSG)
        {            
            players[myIndex].Name = incMSG.ReadString();
            players[myIndex].X = incMSG.ReadVariableInt32();
            players[myIndex].Y = incMSG.ReadVariableInt32();
            players[myIndex].Map = incMSG.ReadVariableInt32();
            players[myIndex].Direction = incMSG.ReadVariableInt32();
            players[myIndex].AimDirection = incMSG.ReadVariableInt32();
            players[myIndex].Sprite = incMSG.ReadVariableInt32();
            players[myIndex].Level = incMSG.ReadVariableInt32();
            players[myIndex].Points = incMSG.ReadVariableInt32();
            players[myIndex].Health = incMSG.ReadVariableInt32();
            players[myIndex].MaxHealth = incMSG.ReadVariableInt32();
            players[myIndex].Hunger = incMSG.ReadVariableInt32();
            players[myIndex].Hydration = incMSG.ReadVariableInt32();
            players[myIndex].Experience = incMSG.ReadVariableInt32();
            players[myIndex].Money = incMSG.ReadVariableInt32();
            players[myIndex].Armor = incMSG.ReadVariableInt32();
            players[myIndex].Strength = incMSG.ReadVariableInt32();
            players[myIndex].Agility = incMSG.ReadVariableInt32();
            players[myIndex].Endurance = incMSG.ReadVariableInt32();
            players[myIndex].Stamina = incMSG.ReadVariableInt32();
            players[myIndex].PistolAmmo = incMSG.ReadVariableInt32();
            players[myIndex].AssaultAmmo = incMSG.ReadVariableInt32();
            players[myIndex].RocketAmmo = incMSG.ReadVariableInt32();
            players[myIndex].GrenadeAmmo = incMSG.ReadVariableInt32();
            players[myIndex].LightRadius = incMSG.ReadVariableInt32();
            players[myIndex].PlayDays = incMSG.ReadVariableInt32();
            players[myIndex].PlayHours = incMSG.ReadVariableInt32();
            players[myIndex].PlayMinutes = incMSG.ReadVariableInt32();
            players[myIndex].PlaySeconds = incMSG.ReadVariableInt32();
            players[myIndex].LifeDay = incMSG.ReadVariableInt32();
            players[myIndex].LifeHour = incMSG.ReadVariableInt32();
            players[myIndex].LifeMinute = incMSG.ReadVariableInt32();
            players[myIndex].LifeSecond = incMSG.ReadVariableInt32();
            players[myIndex].LongestLifeDay = incMSG.ReadVariableInt32();
            players[myIndex].LongestLifeHour = incMSG.ReadVariableInt32();
            players[myIndex].LongestLifeMinute = incMSG.ReadVariableInt32();
            players[myIndex].LongestLifeSecond = incMSG.ReadVariableInt32();
            if (SCREEN_WIDTH == 1024 && SCREEN_HEIGHT == 768)
            {
                players[myIndex].OffsetX = 16;
                players[myIndex].OffsetY = 11;
            }
            else
            {
                players[myIndex].OffsetX = 12;
                players[myIndex].OffsetY = 9;
            }

            //Main Weapon
            players[myIndex].mainWeapon.Name = incMSG.ReadString();
            players[myIndex].mainWeapon.Clip = incMSG.ReadVariableInt32();
            players[myIndex].mainWeapon.MaxClip = incMSG.ReadVariableInt32();
            players[myIndex].mainWeapon.Sprite = incMSG.ReadVariableInt32();
            players[myIndex].mainWeapon.Damage = incMSG.ReadVariableInt32();
            players[myIndex].mainWeapon.Armor = incMSG.ReadVariableInt32();
            players[myIndex].mainWeapon.Type = incMSG.ReadVariableInt32();
            players[myIndex].mainWeapon.AttackSpeed = incMSG.ReadVariableInt32();
            players[myIndex].mainWeapon.ReloadSpeed = incMSG.ReadVariableInt32();
            players[myIndex].mainWeapon.HealthRestore = incMSG.ReadVariableInt32();
            players[myIndex].mainWeapon.HungerRestore = incMSG.ReadVariableInt32();
            players[myIndex].mainWeapon.HydrateRestore = incMSG.ReadVariableInt32();
            players[myIndex].mainWeapon.Strength = incMSG.ReadVariableInt32();
            players[myIndex].mainWeapon.Agility = incMSG.ReadVariableInt32();
            players[myIndex].mainWeapon.Endurance = incMSG.ReadVariableInt32();
            players[myIndex].mainWeapon.Stamina = incMSG.ReadVariableInt32();
            players[myIndex].mainWeapon.ItemAmmoType = incMSG.ReadVariableInt32();
            players[myIndex].mainWeapon.Value = incMSG.ReadVariableInt32();
            players[myIndex].mainWeapon.ProjectileNumber = incMSG.ReadVariableInt32();
            players[myIndex].mainWeapon.Price = incMSG.ReadVariableInt32();
            players[myIndex].mainWeapon.Rarity = incMSG.ReadVariableInt32();

            //Secondary Weapon
            players[myIndex].offWeapon.Name = incMSG.ReadString();
            players[myIndex].offWeapon.Clip = incMSG.ReadVariableInt32();
            players[myIndex].offWeapon.MaxClip = incMSG.ReadVariableInt32();
            players[myIndex].offWeapon.Sprite = incMSG.ReadVariableInt32();
            players[myIndex].offWeapon.Damage = incMSG.ReadVariableInt32();
            players[myIndex].offWeapon.Armor = incMSG.ReadVariableInt32();
            players[myIndex].offWeapon.Type = incMSG.ReadVariableInt32();
            players[myIndex].offWeapon.AttackSpeed = incMSG.ReadVariableInt32();
            players[myIndex].offWeapon.ReloadSpeed = incMSG.ReadVariableInt32();
            players[myIndex].offWeapon.HealthRestore = incMSG.ReadVariableInt32();
            players[myIndex].offWeapon.HungerRestore = incMSG.ReadVariableInt32();
            players[myIndex].offWeapon.HydrateRestore = incMSG.ReadVariableInt32();
            players[myIndex].offWeapon.Strength = incMSG.ReadVariableInt32();
            players[myIndex].offWeapon.Agility = incMSG.ReadVariableInt32();
            players[myIndex].offWeapon.Endurance = incMSG.ReadVariableInt32();
            players[myIndex].offWeapon.Stamina = incMSG.ReadVariableInt32();
            players[myIndex].offWeapon.ItemAmmoType = incMSG.ReadVariableInt32();
            players[myIndex].offWeapon.Value = incMSG.ReadVariableInt32();
            players[myIndex].offWeapon.ProjectileNumber = incMSG.ReadVariableInt32();
            players[myIndex].offWeapon.Price = incMSG.ReadVariableInt32();
            players[myIndex].offWeapon.Rarity = incMSG.ReadVariableInt32();
        }

        static void HandleWeaponsUpdate(NetIncomingMessage incMSG)
        {
            //Main Weapon
            players[myIndex].mainWeapon.Name = incMSG.ReadString();
            players[myIndex].mainWeapon.Clip = incMSG.ReadVariableInt32();
            players[myIndex].mainWeapon.MaxClip = incMSG.ReadVariableInt32();
            players[myIndex].mainWeapon.Sprite = incMSG.ReadVariableInt32();
            players[myIndex].mainWeapon.Damage = incMSG.ReadVariableInt32();
            players[myIndex].mainWeapon.Armor = incMSG.ReadVariableInt32();
            players[myIndex].mainWeapon.Type = incMSG.ReadVariableInt32();
            players[myIndex].mainWeapon.AttackSpeed = incMSG.ReadVariableInt32();
            players[myIndex].mainWeapon.ReloadSpeed = incMSG.ReadVariableInt32();
            players[myIndex].mainWeapon.HealthRestore = incMSG.ReadVariableInt32();
            players[myIndex].mainWeapon.HungerRestore = incMSG.ReadVariableInt32();
            players[myIndex].mainWeapon.HydrateRestore = incMSG.ReadVariableInt32();
            players[myIndex].mainWeapon.Strength = incMSG.ReadVariableInt32();
            players[myIndex].mainWeapon.Agility = incMSG.ReadVariableInt32();
            players[myIndex].mainWeapon.Endurance = incMSG.ReadVariableInt32();
            players[myIndex].mainWeapon.Stamina = incMSG.ReadVariableInt32();
            players[myIndex].mainWeapon.ItemAmmoType = incMSG.ReadVariableInt32();
            players[myIndex].mainWeapon.Value = incMSG.ReadVariableInt32();
            players[myIndex].mainWeapon.ProjectileNumber = incMSG.ReadVariableInt32();
            players[myIndex].mainWeapon.Price = incMSG.ReadVariableInt32();
            players[myIndex].mainWeapon.Rarity = incMSG.ReadVariableInt32();

            //Secondary Weapon
            players[myIndex].offWeapon.Name = incMSG.ReadString();
            players[myIndex].offWeapon.Clip = incMSG.ReadVariableInt32();
            players[myIndex].offWeapon.MaxClip = incMSG.ReadVariableInt32();
            players[myIndex].offWeapon.Sprite = incMSG.ReadVariableInt32();
            players[myIndex].offWeapon.Damage = incMSG.ReadVariableInt32();
            players[myIndex].offWeapon.Armor = incMSG.ReadVariableInt32();
            players[myIndex].offWeapon.Type = incMSG.ReadVariableInt32();
            players[myIndex].offWeapon.AttackSpeed = incMSG.ReadVariableInt32();
            players[myIndex].offWeapon.ReloadSpeed = incMSG.ReadVariableInt32();
            players[myIndex].offWeapon.HealthRestore = incMSG.ReadVariableInt32();
            players[myIndex].offWeapon.HungerRestore = incMSG.ReadVariableInt32();
            players[myIndex].offWeapon.HydrateRestore = incMSG.ReadVariableInt32();
            players[myIndex].offWeapon.Strength = incMSG.ReadVariableInt32();
            players[myIndex].offWeapon.Agility = incMSG.ReadVariableInt32();
            players[myIndex].offWeapon.Endurance = incMSG.ReadVariableInt32();
            players[myIndex].offWeapon.Stamina = incMSG.ReadVariableInt32();
            players[myIndex].offWeapon.ItemAmmoType = incMSG.ReadVariableInt32();
            players[myIndex].offWeapon.Value = incMSG.ReadVariableInt32();
            players[myIndex].offWeapon.ProjectileNumber = incMSG.ReadVariableInt32();
            players[myIndex].offWeapon.Price = incMSG.ReadVariableInt32();
            players[myIndex].offWeapon.Rarity = incMSG.ReadVariableInt32();
        }

        static void HandlePlayers(NetIncomingMessage incMSG)
        {
            for (int i = 0; i < MAX_PLAYERS; i++)
            {
                players[i].Name = incMSG.ReadString();
                players[i].X = incMSG.ReadVariableInt32();
                players[i].Y = incMSG.ReadVariableInt32();
                players[i].Map = incMSG.ReadVariableInt32();
                players[i].Direction = incMSG.ReadVariableInt32();
                players[i].AimDirection = incMSG.ReadVariableInt32();
                players[i].Sprite = incMSG.ReadVariableInt32();
                players[i].Level = incMSG.ReadVariableInt32();
                players[i].Points = incMSG.ReadVariableInt32();
                players[i].Health = incMSG.ReadVariableInt32();
                players[i].MaxHealth = incMSG.ReadVariableInt32();
                players[i].Hunger = incMSG.ReadVariableInt32();
                players[i].Hydration = incMSG.ReadVariableInt32();
                players[i].Experience = incMSG.ReadVariableInt32();
                players[i].Money = incMSG.ReadVariableInt32();
                players[i].Armor = incMSG.ReadVariableInt32();
                players[i].Strength = incMSG.ReadVariableInt32();
                players[i].Agility = incMSG.ReadVariableInt32();
                players[i].Endurance = incMSG.ReadVariableInt32();
                players[i].Stamina = incMSG.ReadVariableInt32();
                players[i].PistolAmmo = incMSG.ReadVariableInt32();
                players[i].AssaultAmmo = incMSG.ReadVariableInt32();
                players[i].RocketAmmo = incMSG.ReadVariableInt32();
                players[i].GrenadeAmmo = incMSG.ReadVariableInt32();
                players[i].LightRadius = incMSG.ReadVariableInt32();
                players[i].PlayDays = incMSG.ReadVariableInt32();
                players[i].PlayHours = incMSG.ReadVariableInt32();
                players[i].PlayMinutes = incMSG.ReadVariableInt32();
                players[i].PlaySeconds = incMSG.ReadVariableInt32();
                players[i].LifeDay = incMSG.ReadVariableInt32();
                players[i].LifeHour = incMSG.ReadVariableInt32();
                players[i].LifeMinute = incMSG.ReadVariableInt32();
                players[i].LifeSecond = incMSG.ReadVariableInt32();
                players[i].LongestLifeDay = incMSG.ReadVariableInt32();
                players[i].LongestLifeHour = incMSG.ReadVariableInt32();
                players[i].LongestLifeMinute = incMSG.ReadVariableInt32();
                players[i].LongestLifeSecond = incMSG.ReadVariableInt32();
                if (SCREEN_WIDTH == 1024 && SCREEN_HEIGHT == 768)
                {
                    players[i].OffsetX = 16;
                    players[i].OffsetY = 11;
                }
                else
                {
                    players[i].OffsetX = 12;
                    players[i].OffsetY = 9;
                }
            }
        }

        static void HandleErrorMessage(NetIncomingMessage incMSG)
        {
            string msg = incMSG.ReadString();
            string caption = incMSG.ReadString();
            MessageBox msgBox = new MessageBox(canvas, msg, caption);
            msgBox.MakeModal();
        }

        static void HandleMapData(NetIncomingMessage incMSG)
        {
            map.Name = incMSG.ReadString();
            map.Revision = incMSG.ReadVariableInt32();
            map.TopMap = incMSG.ReadVariableInt32();
            map.BottomMap = incMSG.ReadVariableInt32();
            map.LeftMap = incMSG.ReadVariableInt32();
            map.RightMap = incMSG.ReadVariableInt32();
            map.Brightness = incMSG.ReadVariableInt32();

            for (int i = 0; i < 10; i++)
            {
                map.m_MapNpc[i] = new MapNpc();
            }

            for (int i = 0; i < 20; i++)
            {
                map.r_MapNpc[i] = new MapNpc();
                map.m_MapItem[i] = new MapItem();
            }

            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    map.Ground[x, y] = new Tile();
                    map.Mask[x, y] = new Tile();
                    map.Fringe[x, y] = new Tile();
                    map.MaskA[x, y] = new Tile();
                    map.FringeA[x, y] = new Tile();

                    //ground
                    map.Ground[x, y].TileX = incMSG.ReadVariableInt32();
                    map.Ground[x, y].TileY = incMSG.ReadVariableInt32();
                    map.Ground[x, y].TileW = incMSG.ReadVariableInt32();
                    map.Ground[x, y].TileH = incMSG.ReadVariableInt32();
                    map.Ground[x, y].Tileset = incMSG.ReadVariableInt32();
                    map.Ground[x, y].Type = incMSG.ReadVariableInt32();
                    map.Ground[x, y].SpawnNum = incMSG.ReadVariableInt32();
                    map.Ground[x, y].LightRadius = incMSG.ReadDouble();
                    //mask
                    map.Mask[x, y].TileX = incMSG.ReadVariableInt32();
                    map.Mask[x, y].TileY = incMSG.ReadVariableInt32();
                    map.Mask[x, y].TileW = incMSG.ReadVariableInt32();
                    map.Mask[x, y].TileH = incMSG.ReadVariableInt32();
                    map.Mask[x, y].Tileset = incMSG.ReadVariableInt32();
                    //fringe
                    map.Fringe[x, y].TileX = incMSG.ReadVariableInt32();
                    map.Fringe[x, y].TileY = incMSG.ReadVariableInt32();
                    map.Fringe[x, y].TileW = incMSG.ReadVariableInt32();
                    map.Fringe[x, y].TileH = incMSG.ReadVariableInt32();
                    map.Fringe[x, y].Tileset = incMSG.ReadVariableInt32();
                    //mask a
                    map.MaskA[x, y].TileX = incMSG.ReadVariableInt32();
                    map.MaskA[x, y].TileY = incMSG.ReadVariableInt32();
                    map.MaskA[x, y].TileW = incMSG.ReadVariableInt32();
                    map.MaskA[x, y].TileH = incMSG.ReadVariableInt32();
                    map.MaskA[x, y].Tileset = incMSG.ReadVariableInt32();
                    //fringe a
                    map.FringeA[x, y].TileX = incMSG.ReadVariableInt32();
                    map.FringeA[x, y].TileY = incMSG.ReadVariableInt32();
                    map.FringeA[x, y].TileW = incMSG.ReadVariableInt32();
                    map.FringeA[x, y].TileH = incMSG.ReadVariableInt32();
                    map.FringeA[x, y].Tileset = incMSG.ReadVariableInt32();
                }
            }
            map.MapDatabaseCache(players[myIndex].Map + 1);
        }
        #endregion

        static void LoadMainGUI()
        {
            canvas.DeleteAllChildren();
            gui.CreateDebugWindow(canvas);
            gui.d_Window.Hide();
            gui.CreateMenuWindow(canvas);
            gui.menuWindow.Hide();
            gui.CreateChatWindow(canvas);
            gui.chatWindow.Hide();
            gui.AddText("Welcome to Sabertooth!");

        }
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
        LifeTime,
        AccountKey,
        RequestActivation
    }
}
