using Gwen.Control;
using Lidgren.Network;
using System;
using static System.Convert;
using static System.Environment;
using static SabertoothClient.Client;
using static SabertoothClient.Globals;
using System.Threading;
using SFML.Window;

namespace SabertoothClient
{
    public static class HandleData
    {
        public static int myIndex;
        public static int tempIndex;
        static Random RND = new Random();

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

                            case (byte)PacketTypes.ManaData:
                                HandleManaData(incMSG);
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

                            case (byte)PacketTypes.UpdateWeapons:
                                HandleWeaponsUpdate(incMSG);
                                break;

                            case (byte)PacketTypes.UpdatePlayerStats:
                                HandleUpdatePlayerStats(incMSG);
                                break;

                            case (byte)PacketTypes.PlayerInv:
                                HandlePlayerInv(incMSG);
                                break;

                            case (byte)PacketTypes.PlayerEquip:
                                HandlePlayerEquipment(incMSG);
                                break;

                            case (byte)PacketTypes.NpcDirection:
                                HandleNpcDirection(incMSG);
                                break;

                            case (byte)PacketTypes.NpcVitals:
                                HandleNpcVitals(incMSG);
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

                            case (byte)PacketTypes.CreateBlood:
                                HandleCreateBlood(incMSG);
                                break;

                            case (byte)PacketTypes.SendQuests:
                                HandleQuests(incMSG);
                                break;

                            case (byte)PacketTypes.SendQuestData:
                                HandleQuestData(incMSG);
                                break;

                            case (byte)PacketTypes.SendQuestList:
                                HandlePlayerQuestList(incMSG);
                                break;

                            case (byte)PacketTypes.SendHotBar:
                                HandlePlayerHotBar(incMSG);
                                break;

                            case (byte)PacketTypes.ItemCoolDown:
                                HandleItemCoolDownUpdate(incMSG);
                                break;

                            case (byte)PacketTypes.AnimationData:
                                HandleAnimationData(incMSG);
                                break;

                            case (byte)PacketTypes.AnimationsData:
                                HandleAnimationsData(incMSG);
                                break;

                            case (byte)PacketTypes.SendSpellBook:
                                HandleSpellBook(incMSG);
                                break;

                            case (byte)PacketTypes.SpellData:
                                HandleSpellData(incMSG);
                                break;

                            case (byte)PacketTypes.SpellsData:
                                HandleSpellsData(incMSG);
                                break;

                            case (byte)PacketTypes.CastSpell:
                                HandlePlayerCastSpell(incMSG);
                                break;
                        }
                        break;

                    case NetIncomingMessageType.StatusChanged:
                        HandleStatusChange(incMSG);
                        break;
                }                
                //Logging.WriteLog("INCMSG Size: " + incMSG.LengthBytes + " btyes, " + incMSG.LengthBits + " bits, " + incMSG.DeliveryMethod.ToString(), "Networking");
        }
            SabertoothClient.netClient.Recycle(incMSG);
        }

        #region Handle Incoming Data
        static void HandlePlayerCastSpell(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();
            int spellnum = incMSG.ReadVariableInt32();
            int slot = incMSG.ReadVariableInt32();
            
            if (players[index].CastingSpell) { return; }

            players[index].CastSpell = true;
            players[index].CurrentSpell = spellnum;
            players[index].SpellBookSlot = slot;
        }

        static void HandleItemCoolDownUpdate(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();
            int invNum = incMSG.ReadVariableInt32();
            bool status = incMSG.ReadBoolean();

            if (index != myIndex) { return; }
            players[index].Backpack[invNum].OnCoolDown = status;
            if (status == true) { players[index].Backpack[invNum].cooldownTick = TickCount; }
            else { players[index].Backpack[invNum].cooldownTick = 0; }
        }

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
                gui.spellsTab.Hide();
                gui.questTab.Hide();
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
                gui.CreateNpcChatWindow(canvas, index - 1);
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

        static void HandleQuests(NetIncomingMessage incMSG)
        {
            for (int i = 0; i < MAX_QUESTS; i++)
            {
                quests[i].Name = incMSG.ReadString();
                quests[i].StartMessage = incMSG.ReadString();
                quests[i].InProgressMessage = incMSG.ReadString();
                quests[i].CompleteMessage = incMSG.ReadString();
                quests[i].Description = incMSG.ReadString();
                quests[i].PrerequisiteQuest = incMSG.ReadVariableInt32();
                quests[i].LevelRequired = incMSG.ReadVariableInt32();

                for (int m = 0; m < MAX_QUEST_ITEMS_REQ; m++)
                {
                    quests[i].ItemNum[m] = incMSG.ReadVariableInt32();
                    quests[i].ItemValue[m] = incMSG.ReadVariableInt32();
                }

                for (int n = 0; n < MAX_QUEST_NPCS_REQ; n++)
                {
                    quests[i].NpcNum[n] = incMSG.ReadVariableInt32();
                    quests[i].NpcValue[n] = incMSG.ReadVariableInt32();
                }

                for (int o = 0; o < MAX_QUEST_REWARDS; o++)
                {
                    quests[i].RewardItem[o] = incMSG.ReadVariableInt32();
                    quests[i].RewardValue[o] = incMSG.ReadVariableInt32();
                }

                quests[i].Experience = incMSG.ReadVariableInt32();
                quests[i].Money = incMSG.ReadVariableInt32();
                quests[i].Type = incMSG.ReadVariableInt32();
            }
        }

        static void HandleQuestData(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();

            quests[index].Name = incMSG.ReadString();
            quests[index].StartMessage = incMSG.ReadString();
            quests[index].InProgressMessage = incMSG.ReadString();
            quests[index].CompleteMessage = incMSG.ReadString();
            quests[index].Description = incMSG.ReadString();
            quests[index].PrerequisiteQuest = incMSG.ReadVariableInt32();
            quests[index].LevelRequired = incMSG.ReadVariableInt32();

            for (int m = 0; m < MAX_QUEST_ITEMS_REQ; m++)
            {
                quests[index].ItemNum[m] = incMSG.ReadVariableInt32();
                quests[index].ItemValue[m] = incMSG.ReadVariableInt32();
            }

            for (int n = 0; n < MAX_QUEST_NPCS_REQ; n++)
            {
                quests[index].NpcNum[n] = incMSG.ReadVariableInt32();
                quests[index].NpcValue[n] = incMSG.ReadVariableInt32();
            }

            for (int o = 0; o < MAX_QUEST_REWARDS; o++)
            {
                quests[index].RewardItem[o] = incMSG.ReadVariableInt32();
                quests[index].RewardValue[o] = incMSG.ReadVariableInt32();
            }

            quests[index].Experience = incMSG.ReadVariableInt32();
            quests[index].Money = incMSG.ReadVariableInt32();
            quests[index].Type = incMSG.ReadVariableInt32();
        }

        static void HandleChats(NetIncomingMessage incMSG)
        {
            for (int i = 0; i < MAX_CHATS; i++)
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
                chats[i].QuestNum = incMSG.ReadVariableInt32();
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
            chats[index].QuestNum = incMSG.ReadVariableInt32();
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
                gui.spellsTab.Hide();
                gui.questTab.Hide();
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
                gui.spellsTab.Hide();
                gui.questTab.Hide();
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
            players[myIndex].Chest.HealthRestore = incMSG.ReadVariableInt32();
            players[myIndex].Chest.ManaRestore = incMSG.ReadVariableInt32();
            players[myIndex].Chest.Strength = incMSG.ReadVariableInt32();
            players[myIndex].Chest.Agility = incMSG.ReadVariableInt32();
            players[myIndex].Chest.Intelligence = incMSG.ReadVariableInt32();
            players[myIndex].Chest.Energy = incMSG.ReadVariableInt32();
            players[myIndex].Chest.Stamina = incMSG.ReadVariableInt32();
            players[myIndex].Chest.Value = incMSG.ReadVariableInt32();
            players[myIndex].Chest.Price= incMSG.ReadVariableInt32();
            players[myIndex].Chest.Rarity = incMSG.ReadVariableInt32();
            players[myIndex].Chest.CoolDown = incMSG.ReadVariableInt32();
            players[myIndex].Chest.AddMaxHealth = incMSG.ReadVariableInt32();
            players[myIndex].Chest.AddMaxMana = incMSG.ReadVariableInt32();
            players[myIndex].Chest.BonusXP = incMSG.ReadVariableInt32();
            players[myIndex].Chest.SpellNum = incMSG.ReadVariableInt32();
            players[myIndex].Chest.Stackable = incMSG.ReadBoolean();
            players[myIndex].Chest.MaxStack = incMSG.ReadVariableInt32();

            players[myIndex].Legs.Name = incMSG.ReadString();
            players[myIndex].Legs.Sprite = incMSG.ReadVariableInt32();
            players[myIndex].Legs.Damage = incMSG.ReadVariableInt32();
            players[myIndex].Legs.Armor = incMSG.ReadVariableInt32();
            players[myIndex].Legs.Type = incMSG.ReadVariableInt32();
            players[myIndex].Legs.AttackSpeed = incMSG.ReadVariableInt32();
            players[myIndex].Legs.HealthRestore = incMSG.ReadVariableInt32();
            players[myIndex].Legs.ManaRestore = incMSG.ReadVariableInt32();
            players[myIndex].Legs.Strength = incMSG.ReadVariableInt32();
            players[myIndex].Legs.Agility = incMSG.ReadVariableInt32();
            players[myIndex].Legs.Intelligence = incMSG.ReadVariableInt32();
            players[myIndex].Legs.Energy = incMSG.ReadVariableInt32();
            players[myIndex].Legs.Stamina = incMSG.ReadVariableInt32();
            players[myIndex].Legs.Value = incMSG.ReadVariableInt32();
            players[myIndex].Legs.Price = incMSG.ReadVariableInt32();
            players[myIndex].Legs.Rarity = incMSG.ReadVariableInt32();
            players[myIndex].Legs.CoolDown = incMSG.ReadVariableInt32();
            players[myIndex].Legs.AddMaxHealth = incMSG.ReadVariableInt32();
            players[myIndex].Legs.AddMaxMana = incMSG.ReadVariableInt32();
            players[myIndex].Legs.BonusXP = incMSG.ReadVariableInt32();
            players[myIndex].Legs.SpellNum = incMSG.ReadVariableInt32();
            players[myIndex].Legs.Stackable = incMSG.ReadBoolean();
            players[myIndex].Legs.MaxStack = incMSG.ReadVariableInt32();

            players[myIndex].Feet.Name = incMSG.ReadString();
            players[myIndex].Feet.Sprite = incMSG.ReadVariableInt32();
            players[myIndex].Feet.Damage = incMSG.ReadVariableInt32();
            players[myIndex].Feet.Armor = incMSG.ReadVariableInt32();
            players[myIndex].Feet.Type = incMSG.ReadVariableInt32();
            players[myIndex].Feet.AttackSpeed = incMSG.ReadVariableInt32();
            players[myIndex].Feet.HealthRestore = incMSG.ReadVariableInt32();
            players[myIndex].Feet.ManaRestore = incMSG.ReadVariableInt32();
            players[myIndex].Feet.Strength = incMSG.ReadVariableInt32();
            players[myIndex].Feet.Agility = incMSG.ReadVariableInt32();
            players[myIndex].Feet.Intelligence = incMSG.ReadVariableInt32();
            players[myIndex].Feet.Energy = incMSG.ReadVariableInt32();
            players[myIndex].Feet.Stamina = incMSG.ReadVariableInt32();
            players[myIndex].Feet.Value = incMSG.ReadVariableInt32();
            players[myIndex].Feet.Price = incMSG.ReadVariableInt32();
            players[myIndex].Feet.Rarity = incMSG.ReadVariableInt32();
            players[myIndex].Feet.CoolDown = incMSG.ReadVariableInt32();
            players[myIndex].Feet.AddMaxHealth = incMSG.ReadVariableInt32();
            players[myIndex].Feet.AddMaxMana = incMSG.ReadVariableInt32();
            players[myIndex].Feet.BonusXP = incMSG.ReadVariableInt32();
            players[myIndex].Feet.SpellNum = incMSG.ReadVariableInt32();
            players[myIndex].Feet.Stackable = incMSG.ReadBoolean();
            players[myIndex].Feet.MaxStack = incMSG.ReadVariableInt32();
        }

        static void HandlePlayerQuestList(NetIncomingMessage incMSG)
        {
            if (gui.questList != null)
            {
                gui.questList.Clear();
                for (int i = 0; i < MAX_PLAYER_QUEST_LIST; i++)
                {
                    players[myIndex].QuestList[i] = incMSG.ReadVariableInt32();
                    players[myIndex].QuestStatus[i] = incMSG.ReadVariableInt32();

                    if (players[myIndex].QuestList[i] == 0)
                    {
                        gui.questList.AddRow((i + 1) + ": None");
                    }
                    else
                    {
                        if (quests[players[myIndex].QuestList[i] - 1].Name != null)
                        {
                            gui.questList.AddRow((i + 1) + ": " + quests[players[HandleData.myIndex].QuestList[i] - 1].Name);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < MAX_PLAYER_QUEST_LIST; i++)
                {
                    players[myIndex].QuestList[i] = incMSG.ReadVariableInt32();
                    players[myIndex].QuestStatus[i] = incMSG.ReadVariableInt32();
                }
            }
        }

        static void HandlePlayerInv(NetIncomingMessage incMSG)
        {
            for (int i = 0; i < MAX_INV_SLOTS; i++)
            {
                if (players[myIndex].Backpack[i] != null)
                {
                    players[myIndex].Backpack[i].Name = incMSG.ReadString();
                    players[myIndex].Backpack[i].Sprite = incMSG.ReadVariableInt32();
                    players[myIndex].Backpack[i].Damage = incMSG.ReadVariableInt32();
                    players[myIndex].Backpack[i].Armor = incMSG.ReadVariableInt32();
                    players[myIndex].Backpack[i].Type = incMSG.ReadVariableInt32();
                    players[myIndex].Backpack[i].AttackSpeed = incMSG.ReadVariableInt32();
                    players[myIndex].Backpack[i].HealthRestore = incMSG.ReadVariableInt32();
                    players[myIndex].Backpack[i].ManaRestore = incMSG.ReadVariableInt32();
                    players[myIndex].Backpack[i].Strength = incMSG.ReadVariableInt32();
                    players[myIndex].Backpack[i].Agility = incMSG.ReadVariableInt32();
                    players[myIndex].Backpack[i].Intelligence = incMSG.ReadVariableInt32();
                    players[myIndex].Backpack[i].Energy = incMSG.ReadVariableInt32();
                    players[myIndex].Backpack[i].Stamina = incMSG.ReadVariableInt32();
                    players[myIndex].Backpack[i].Value = incMSG.ReadVariableInt32();
                    players[myIndex].Backpack[i].Price = incMSG.ReadVariableInt32();
                    players[myIndex].Backpack[i].Rarity = incMSG.ReadVariableInt32();
                    players[myIndex].Backpack[i].CoolDown = incMSG.ReadVariableInt32();
                    players[myIndex].Backpack[i].AddMaxHealth = incMSG.ReadVariableInt32();
                    players[myIndex].Backpack[i].AddMaxMana = incMSG.ReadVariableInt32();
                    players[myIndex].Backpack[i].BonusXP = incMSG.ReadVariableInt32();
                    players[myIndex].Backpack[i].SpellNum = incMSG.ReadVariableInt32();
                    players[myIndex].Backpack[i].Stackable = incMSG.ReadBoolean();
                    players[myIndex].Backpack[i].MaxStack = incMSG.ReadVariableInt32();
                }
            }
        }

        static void HandlePlayerBank(NetIncomingMessage incMSG)
        {
            for (int i = 0; i < MAX_BANK_SLOTS; i++)
            {
                if (players[myIndex].Bank[i] != null)
                {
                    players[myIndex].Bank[i].Name = incMSG.ReadString();
                    players[myIndex].Bank[i].Sprite = incMSG.ReadVariableInt32();
                    players[myIndex].Bank[i].Damage = incMSG.ReadVariableInt32();
                    players[myIndex].Bank[i].Armor = incMSG.ReadVariableInt32();
                    players[myIndex].Bank[i].Type = incMSG.ReadVariableInt32();
                    players[myIndex].Bank[i].AttackSpeed = incMSG.ReadVariableInt32();
                    players[myIndex].Bank[i].HealthRestore = incMSG.ReadVariableInt32();
                    players[myIndex].Bank[i].ManaRestore = incMSG.ReadVariableInt32();
                    players[myIndex].Bank[i].Strength = incMSG.ReadVariableInt32();
                    players[myIndex].Bank[i].Agility = incMSG.ReadVariableInt32();
                    players[myIndex].Bank[i].Intelligence = incMSG.ReadVariableInt32();
                    players[myIndex].Bank[i].Energy = incMSG.ReadVariableInt32();
                    players[myIndex].Bank[i].Stamina = incMSG.ReadVariableInt32();
                    players[myIndex].Bank[i].Value = incMSG.ReadVariableInt32();
                    players[myIndex].Bank[i].Price = incMSG.ReadVariableInt32();
                    players[myIndex].Bank[i].Rarity = incMSG.ReadVariableInt32();
                    players[myIndex].Bank[i].CoolDown = incMSG.ReadVariableInt32();
                    players[myIndex].Bank[i].AddMaxHealth = incMSG.ReadVariableInt32();
                    players[myIndex].Bank[i].AddMaxMana = incMSG.ReadVariableInt32();
                    players[myIndex].Bank[i].BonusXP = incMSG.ReadVariableInt32();
                    players[myIndex].Bank[i].SpellNum = incMSG.ReadVariableInt32();
                    players[myIndex].Bank[i].Stackable = incMSG.ReadBoolean();
                    players[myIndex].Bank[i].MaxStack = incMSG.ReadVariableInt32();
                }
            }
        }

        static void HandlePlayerHotBar(NetIncomingMessage incMSG)
        {
            for (int i = 0; i < MAX_PLAYER_HOTBAR; i++)
            {
                string key = incMSG.ReadString();
                int spell = incMSG.ReadVariableInt32();
                int inv = incMSG.ReadVariableInt32();

                players[myIndex].hotBar[i].HotKey = (Keyboard.Key)Enum.Parse(typeof(Keyboard.Key), key, true);
                players[myIndex].hotBar[i].SpellNumber = spell;
                players[myIndex].hotBar[i].InvNumber = inv;
            }
        }

        static void HandleSpellBook(NetIncomingMessage incMSG)
        {
            for (int i = 0; i < MAX_PLAYER_SPELLBOOK; i++)
            {
                int spell = incMSG.ReadVariableInt32();

                players[myIndex].spellBook[i].SpellNumber = spell;
            }
        }

        static void HandleCreateBlood(NetIncomingMessage incMSG)
        {
            int slot = incMSG.ReadVariableInt32();
            int x = incMSG.ReadVariableInt32();
            int y = incMSG.ReadVariableInt32();

            map.m_BloodSplats[slot] = new BloodSplat();

            map.m_BloodSplats[slot].X = x;
            map.m_BloodSplats[slot].Y = y;
            map.m_BloodSplats[slot].TexX = RND.Next(0, 5);
            map.m_BloodSplats[slot].TexY = RND.Next(0, 4);
        }

        static void HandleSpellData(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();

            spells[index].Name = incMSG.ReadString();
            spells[index].Level = incMSG.ReadVariableInt32();
            spells[index].Icon = incMSG.ReadVariableInt32();
            spells[index].Vital = incMSG.ReadVariableInt32();
            spells[index].HealthCost = incMSG.ReadVariableInt32();
            spells[index].ManaCost = incMSG.ReadVariableInt32();
            spells[index].CoolDown = incMSG.ReadVariableInt32();
            spells[index].CastTime = incMSG.ReadVariableInt32();
            spells[index].Charges = incMSG.ReadVariableInt32();
            spells[index].TotalTick = incMSG.ReadVariableInt32();
            spells[index].TickInterval = incMSG.ReadVariableInt32();
            spells[index].SpellType = incMSG.ReadVariableInt32();
            spells[index].Range = incMSG.ReadVariableInt32();
            spells[index].Animation = incMSG.ReadVariableInt32();
            spells[index].AOE = incMSG.ReadBoolean();
            spells[index].Distance = incMSG.ReadVariableInt32();
        }

        static void HandleSpellsData(NetIncomingMessage incMSG)
        {
            for (int i = 0; i < MAX_SPELLS; i++)
            {
                if (spells[i] != null)
                {
                    spells[i].Name = incMSG.ReadString();

                    spells[i].Level = incMSG.ReadVariableInt32();
                    spells[i].Icon = incMSG.ReadVariableInt32();
                    spells[i].Vital = incMSG.ReadVariableInt32();
                    spells[i].HealthCost = incMSG.ReadVariableInt32();
                    spells[i].ManaCost = incMSG.ReadVariableInt32();
                    spells[i].CoolDown = incMSG.ReadVariableInt32();
                    spells[i].CastTime = incMSG.ReadVariableInt32();
                    spells[i].Charges = incMSG.ReadVariableInt32();
                    spells[i].TotalTick = incMSG.ReadVariableInt32();
                    spells[i].TickInterval = incMSG.ReadVariableInt32();
                    spells[i].SpellType = incMSG.ReadVariableInt32();
                    spells[i].Range = incMSG.ReadVariableInt32();
                    spells[i].Animation = incMSG.ReadVariableInt32();
                    spells[i].AOE = incMSG.ReadBoolean();
                    spells[i].Distance = incMSG.ReadVariableInt32();
                }
            }
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
                    items[i].ManaRestore = incMSG.ReadVariableInt32();
                    items[i].Strength = incMSG.ReadVariableInt32();
                    items[i].Agility = incMSG.ReadVariableInt32();
                    items[i].Intelligence = incMSG.ReadVariableInt32();
                    items[i].Energy = incMSG.ReadVariableInt32();
                    items[i].Stamina = incMSG.ReadVariableInt32();
                    items[i].Value = incMSG.ReadVariableInt32();
                    items[i].Price = incMSG.ReadVariableInt32();
                    items[i].Rarity = incMSG.ReadVariableInt32();
                    items[i].CoolDown = incMSG.ReadVariableInt32();
                    items[i].AddMaxHealth = incMSG.ReadVariableInt32();
                    items[i].AddMaxMana = incMSG.ReadVariableInt32();
                    items[i].BonusXP = incMSG.ReadVariableInt32();
                    items[i].SpellNum = incMSG.ReadVariableInt32();

                    items[i].Stackable = incMSG.ReadBoolean();

                    items[i].MaxStack = incMSG.ReadVariableInt32();
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
            items[index].ManaRestore = incMSG.ReadVariableInt32();
            items[index].Strength = incMSG.ReadVariableInt32();
            items[index].Agility = incMSG.ReadVariableInt32();
            items[index].Intelligence = incMSG.ReadVariableInt32();
            items[index].Energy = incMSG.ReadVariableInt32();
            items[index].Stamina = incMSG.ReadVariableInt32();
            items[index].Value = incMSG.ReadVariableInt32();
            items[index].Price = incMSG.ReadVariableInt32();
            items[index].Rarity = incMSG.ReadVariableInt32();
            items[index].CoolDown = incMSG.ReadVariableInt32();
            items[index].AddMaxHealth = incMSG.ReadVariableInt32();
            items[index].AddMaxMana = incMSG.ReadVariableInt32();
            items[index].BonusXP = incMSG.ReadVariableInt32();
            items[index].SpellNum = incMSG.ReadVariableInt32();
            items[index].Stackable = incMSG.ReadBoolean();
            items[index].MaxStack = incMSG.ReadVariableInt32();
        }

        static void HandleAnimationsData(NetIncomingMessage incMSG)
        {
            for (int i = 0; i < MAX_ANIMATIONS; i++)
            {
                if (animations[i] != null)
                {
                    animations[i].Name = incMSG.ReadString();

                    animations[i].SpriteNumber = incMSG.ReadVariableInt32();
                    animations[i].FrameCountH = incMSG.ReadVariableInt32();
                    animations[i].FrameCountV = incMSG.ReadVariableInt32();
                    animations[i].FrameCount = incMSG.ReadVariableInt32();
                    animations[i].FrameDuration = incMSG.ReadVariableInt32();
                    animations[i].LoopCount = incMSG.ReadVariableInt32();
                    animations[i].RenderBelowTarget = incMSG.ReadBoolean();
                }
            }
        }

        static void HandleAnimationData(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();

            animations[index].Name = incMSG.ReadString();

            animations[index].SpriteNumber = incMSG.ReadVariableInt32();
            animations[index].FrameCountH = incMSG.ReadVariableInt32();
            animations[index].FrameCountV = incMSG.ReadVariableInt32();
            animations[index].FrameCount = incMSG.ReadVariableInt32();
            animations[index].FrameDuration = incMSG.ReadVariableInt32();
            animations[index].LoopCount = incMSG.ReadVariableInt32();
            animations[index].RenderBelowTarget = incMSG.ReadBoolean();
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

            LoadMainGUI();
            gui.Ready = true;
        }

        static void HandleNpcDirection(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();

            map.m_MapNpc[index].X = incMSG.ReadVariableInt32();
            map.m_MapNpc[index].Y = incMSG.ReadVariableInt32();
            map.m_MapNpc[index].Direction = incMSG.ReadVariableInt32();
            map.m_MapNpc[index].Step = incMSG.ReadVariableInt32();
        }

        static void HandleNpcVitals(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();
            int health = incMSG.ReadVariableInt32();
            bool isSpanwed = incMSG.ReadBoolean();
            int x = incMSG.ReadVariableInt32();
            int y = incMSG.ReadVariableInt32();
            int damage = incMSG.ReadVariableInt32();
            int target = players[myIndex].Target;
            int openDT = FindOpenNpcDisplayText(index);

            map.m_MapNpc[index].dText[openDT].CreateDisplayText(damage, x, y, (int)DisplayTextMsg.Damage, "-");

            map.m_MapNpc[index].Health = health;
            map.m_MapNpc[index].IsSpawned = isSpanwed;
            map.m_MapNpc[index].X = x;
            map.m_MapNpc[index].Y = y;
            
            if (!isSpanwed)
            {
                if (index == target)
                {
                    players[myIndex].Target = -1;
                    players[myIndex].Attacking = false;
                }
            }            
        }

        static int FindOpenNpcDisplayText(int index)
        {
            for (int i = 0; i < MAX_DISPLAY_TEXT; i++)
            {
                if (map.m_MapNpc[index].dText[i].displayText.DisplayedString == "EMPTY")
                {
                    return i;
                }
            }

            return MAX_DISPLAY_TEXT;
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
            SabertoothClient.netClient.Connect(SabertoothClient.IPAddress, ToInt32(SabertoothClient.Port), outMSG);
        }

        static void HandleConnectionData(NetIncomingMessage incMSG)
        {
            if (SabertoothClient.netClient.ServerConnection != null) { return; }
            Console.WriteLine("Connected to server!");
        }

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
            //if (!gui.chatWindow.IsVisible) { gui.chatWindow.Show(); }
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

        static void HandleHealthData(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();
            int health = incMSG.ReadVariableInt32();
            int vital = incMSG.ReadVariableInt32();
            int x = players[index].X + OFFSET_X;
            int y = players[index].Y + OFFSET_Y;

            players[index].Health = health;

            if (vital > 0)
            {
                int openTD = FindOpenPlayerDisplayText(index);
                players[index].displayText[openTD].CreateDisplayText(vital, x, y, (int)DisplayTextMsg.Damage, "-");
            }
        }

        public static int FindOpenPlayerDisplayText(int index)
        {
            for (int i = 0; i < MAX_DISPLAY_TEXT; i++)
            {
                if (players[index].displayText[i].displayText.DisplayedString == "EMPTY")
                {
                    return i;
                }
            }

            return MAX_DISPLAY_TEXT;
        }

        static void HandleManaData(NetIncomingMessage incMSG)
        {
            int index = incMSG.ReadVariableInt32();
            int mana = incMSG.ReadVariableInt32();

            players[index].Mana = mana;
        }

        static void HandleUpdatePlayerStats(NetIncomingMessage incMSG)
        {
            players[myIndex].Level = incMSG.ReadVariableInt32();            
            players[myIndex].Health = incMSG.ReadVariableInt32();
            players[myIndex].MaxHealth = incMSG.ReadVariableInt32();
            players[myIndex].Mana = incMSG.ReadVariableInt32();
            players[myIndex].MaxMana = incMSG.ReadVariableInt32();
            players[myIndex].Experience = incMSG.ReadVariableInt32();
            players[myIndex].Wallet = incMSG.ReadVariableInt32();
            players[myIndex].Armor = incMSG.ReadVariableInt32();
            players[myIndex].Strength = incMSG.ReadVariableInt32();
            players[myIndex].Agility = incMSG.ReadVariableInt32();
            players[myIndex].Intelligence = incMSG.ReadVariableInt32();
            players[myIndex].Stamina = incMSG.ReadVariableInt32();
            players[myIndex].Energy = incMSG.ReadVariableInt32();
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
            players[myIndex].Health = incMSG.ReadVariableInt32();
            players[myIndex].MaxHealth = incMSG.ReadVariableInt32();
            players[myIndex].Mana = incMSG.ReadVariableInt32();
            players[myIndex].MaxMana = incMSG.ReadVariableInt32();
            players[myIndex].Experience = incMSG.ReadVariableInt32();
            players[myIndex].Wallet = incMSG.ReadVariableInt32();
            players[myIndex].Armor = incMSG.ReadVariableInt32();
            players[myIndex].Strength = incMSG.ReadVariableInt32();
            players[myIndex].Agility = incMSG.ReadVariableInt32();
            players[myIndex].Intelligence = incMSG.ReadVariableInt32();
            players[myIndex].Stamina = incMSG.ReadVariableInt32();
            players[myIndex].Energy = incMSG.ReadVariableInt32();
            players[myIndex].LightRadius = incMSG.ReadVariableInt32();
            players[myIndex].PlayDays = incMSG.ReadVariableInt32();
            players[myIndex].PlayHours = incMSG.ReadVariableInt32();
            players[myIndex].PlayMinutes = incMSG.ReadVariableInt32();
            players[myIndex].PlaySeconds = incMSG.ReadVariableInt32();
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
            players[myIndex].MainHand.Name = incMSG.ReadString();
            players[myIndex].MainHand.Sprite = incMSG.ReadVariableInt32();
            players[myIndex].MainHand.Damage = incMSG.ReadVariableInt32();
            players[myIndex].MainHand.Armor = incMSG.ReadVariableInt32();
            players[myIndex].MainHand.Type = incMSG.ReadVariableInt32();
            players[myIndex].MainHand.AttackSpeed = incMSG.ReadVariableInt32();
            players[myIndex].MainHand.HealthRestore = incMSG.ReadVariableInt32();
            players[myIndex].MainHand.ManaRestore = incMSG.ReadVariableInt32();
            players[myIndex].MainHand.Strength = incMSG.ReadVariableInt32();
            players[myIndex].MainHand.Agility = incMSG.ReadVariableInt32();
            players[myIndex].MainHand.Intelligence = incMSG.ReadVariableInt32();
            players[myIndex].MainHand.Energy = incMSG.ReadVariableInt32();
            players[myIndex].MainHand.Stamina = incMSG.ReadVariableInt32();
            players[myIndex].MainHand.Value = incMSG.ReadVariableInt32();
            players[myIndex].MainHand.Price = incMSG.ReadVariableInt32();
            players[myIndex].MainHand.Rarity = incMSG.ReadVariableInt32();
            players[myIndex].MainHand.CoolDown = incMSG.ReadVariableInt32();
            players[myIndex].MainHand.AddMaxHealth = incMSG.ReadVariableInt32();
            players[myIndex].MainHand.AddMaxMana = incMSG.ReadVariableInt32();
            players[myIndex].MainHand.BonusXP = incMSG.ReadVariableInt32();
            players[myIndex].MainHand.SpellNum = incMSG.ReadVariableInt32();
            players[myIndex].MainHand.Stackable = incMSG.ReadBoolean();
            players[myIndex].MainHand.MaxStack = incMSG.ReadVariableInt32();

            //Secondary Weapon
            players[myIndex].OffHand.Name = incMSG.ReadString();
            players[myIndex].OffHand.Sprite = incMSG.ReadVariableInt32();
            players[myIndex].OffHand.Damage = incMSG.ReadVariableInt32();
            players[myIndex].OffHand.Armor = incMSG.ReadVariableInt32();
            players[myIndex].OffHand.Type = incMSG.ReadVariableInt32();
            players[myIndex].OffHand.AttackSpeed = incMSG.ReadVariableInt32();
            players[myIndex].OffHand.HealthRestore = incMSG.ReadVariableInt32();
            players[myIndex].OffHand.ManaRestore = incMSG.ReadVariableInt32();
            players[myIndex].OffHand.Strength = incMSG.ReadVariableInt32();
            players[myIndex].OffHand.Agility = incMSG.ReadVariableInt32();
            players[myIndex].OffHand.Intelligence = incMSG.ReadVariableInt32();
            players[myIndex].OffHand.Energy = incMSG.ReadVariableInt32();
            players[myIndex].OffHand.Stamina = incMSG.ReadVariableInt32();
            players[myIndex].OffHand.Value = incMSG.ReadVariableInt32();
            players[myIndex].OffHand.Price = incMSG.ReadVariableInt32();
            players[myIndex].OffHand.Rarity = incMSG.ReadVariableInt32();
            players[myIndex].OffHand.CoolDown = incMSG.ReadVariableInt32();
            players[myIndex].OffHand.AddMaxHealth = incMSG.ReadVariableInt32();
            players[myIndex].OffHand.AddMaxMana = incMSG.ReadVariableInt32();
            players[myIndex].OffHand.BonusXP = incMSG.ReadVariableInt32();
            players[myIndex].OffHand.SpellNum = incMSG.ReadVariableInt32();
            players[myIndex].OffHand.Stackable = incMSG.ReadBoolean();
            players[myIndex].OffHand.MaxStack = incMSG.ReadVariableInt32();
        }

        static void HandleWeaponsUpdate(NetIncomingMessage incMSG)
        {
            //Main Weapon
            players[myIndex].MainHand.Name = incMSG.ReadString();
            players[myIndex].MainHand.Sprite = incMSG.ReadVariableInt32();
            players[myIndex].MainHand.Damage = incMSG.ReadVariableInt32();
            players[myIndex].MainHand.Armor = incMSG.ReadVariableInt32();
            players[myIndex].MainHand.Type = incMSG.ReadVariableInt32();
            players[myIndex].MainHand.AttackSpeed = incMSG.ReadVariableInt32();
            players[myIndex].MainHand.HealthRestore = incMSG.ReadVariableInt32();
            players[myIndex].MainHand.ManaRestore = incMSG.ReadVariableInt32();
            players[myIndex].MainHand.Strength = incMSG.ReadVariableInt32();
            players[myIndex].MainHand.Agility = incMSG.ReadVariableInt32();
            players[myIndex].MainHand.Intelligence = incMSG.ReadVariableInt32();
            players[myIndex].MainHand.Energy = incMSG.ReadVariableInt32();
            players[myIndex].MainHand.Stamina = incMSG.ReadVariableInt32();
            players[myIndex].MainHand.Value = incMSG.ReadVariableInt32();
            players[myIndex].MainHand.Price = incMSG.ReadVariableInt32();
            players[myIndex].MainHand.Rarity = incMSG.ReadVariableInt32();
            players[myIndex].MainHand.CoolDown = incMSG.ReadVariableInt32();
            players[myIndex].MainHand.AddMaxHealth = incMSG.ReadVariableInt32();
            players[myIndex].MainHand.AddMaxMana = incMSG.ReadVariableInt32();
            players[myIndex].MainHand.BonusXP = incMSG.ReadVariableInt32();
            players[myIndex].MainHand.SpellNum = incMSG.ReadVariableInt32();
            players[myIndex].MainHand.Stackable = incMSG.ReadBoolean();
            players[myIndex].MainHand.MaxStack = incMSG.ReadVariableInt32();

            //Secondary Weapon
            players[myIndex].OffHand.Name = incMSG.ReadString();
            players[myIndex].OffHand.Sprite = incMSG.ReadVariableInt32();
            players[myIndex].OffHand.Damage = incMSG.ReadVariableInt32();
            players[myIndex].OffHand.Armor = incMSG.ReadVariableInt32();
            players[myIndex].OffHand.Type = incMSG.ReadVariableInt32();
            players[myIndex].OffHand.AttackSpeed = incMSG.ReadVariableInt32();
            players[myIndex].OffHand.HealthRestore = incMSG.ReadVariableInt32();
            players[myIndex].OffHand.ManaRestore = incMSG.ReadVariableInt32();
            players[myIndex].OffHand.Strength = incMSG.ReadVariableInt32();
            players[myIndex].OffHand.Agility = incMSG.ReadVariableInt32();
            players[myIndex].OffHand.Intelligence = incMSG.ReadVariableInt32();
            players[myIndex].OffHand.Energy = incMSG.ReadVariableInt32();
            players[myIndex].OffHand.Stamina = incMSG.ReadVariableInt32();
            players[myIndex].OffHand.Value = incMSG.ReadVariableInt32();
            players[myIndex].OffHand.Price = incMSG.ReadVariableInt32();
            players[myIndex].OffHand.Rarity = incMSG.ReadVariableInt32();
            players[myIndex].OffHand.CoolDown = incMSG.ReadVariableInt32();
            players[myIndex].OffHand.AddMaxHealth = incMSG.ReadVariableInt32();
            players[myIndex].OffHand.AddMaxMana = incMSG.ReadVariableInt32();
            players[myIndex].OffHand.BonusXP = incMSG.ReadVariableInt32();
            players[myIndex].OffHand.SpellNum = incMSG.ReadVariableInt32();
            players[myIndex].OffHand.Stackable = incMSG.ReadBoolean();
            players[myIndex].OffHand.MaxStack = incMSG.ReadVariableInt32();
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
                players[i].Health = incMSG.ReadVariableInt32();
                players[i].MaxHealth = incMSG.ReadVariableInt32();
                players[i].Mana = incMSG.ReadVariableInt32();
                players[i].MaxMana = incMSG.ReadVariableInt32();
                players[i].Experience = incMSG.ReadVariableInt32();
                players[i].Wallet = incMSG.ReadVariableInt32();
                players[i].Armor = incMSG.ReadVariableInt32();
                players[i].Strength = incMSG.ReadVariableInt32();
                players[i].Agility = incMSG.ReadVariableInt32();
                players[i].Intelligence = incMSG.ReadVariableInt32();
                players[i].Stamina = incMSG.ReadVariableInt32();
                players[i].Energy = incMSG.ReadVariableInt32();
                players[i].LightRadius = incMSG.ReadVariableInt32();
                players[i].PlayDays = incMSG.ReadVariableInt32();
                players[i].PlayHours = incMSG.ReadVariableInt32();
                players[i].PlayMinutes = incMSG.ReadVariableInt32();
                players[i].PlaySeconds = incMSG.ReadVariableInt32();
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
            msgBox.Show();

            if (caption == "Update")
            {
                LaunchUpdateClient();
            }
        }

        static void HandleMapData(NetIncomingMessage incMSG)
        {
            map.Id = incMSG.ReadVariableInt32();
            map.Name = incMSG.ReadString();
            map.Revision = incMSG.ReadVariableInt32();
            map.TopMap = incMSG.ReadVariableInt32();
            map.BottomMap = incMSG.ReadVariableInt32();
            map.LeftMap = incMSG.ReadVariableInt32();
            map.RightMap = incMSG.ReadVariableInt32();
            map.Brightness = incMSG.ReadVariableInt32();
            int maxx = incMSG.ReadVariableInt32();
            int maxy = incMSG.ReadVariableInt32();
            map.MaxX = maxx;
            map.MaxY = maxy;

            for (int i = 0; i < MAX_MAP_NPCS; i++)
            {
                map.m_MapNpc[i] = new MapNpc();
            }

            for (int i = 0; i < MAX_MAP_ANIMATIONS; i++)
            {
                map.m_Animation[i] = new MapAnimation();
            }

            map.Ground = new Tile[maxx, maxy];
            map.Mask = new Tile[maxx, maxy];
            map.MaskA = new Tile[maxx, maxy];
            map.Fringe = new Tile[maxx, maxy];
            map.FringeA = new Tile[maxx, maxy];

            for (int x = 0; x < maxx; x++)
            {
                for (int y = 0; y < maxy; y++)
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

            SetupMapAnimations();   //setup the map animations

            //map.MapDatabaseCache(map.Id);
            players[myIndex].isChangingMaps = false;
        }

        static void HandleStatusChange(NetIncomingMessage incMSG)
        {
            Logging.WriteMessageLog(incMSG.SenderConnection.ToString() + " status changed. " + incMSG.SenderConnection.Status);
        }
        #endregion

        #region Other Voids
        static void LoadMainGUI()
        {
            canvas.DeleteAllChildren();
            gui.CreateDebugWindow(canvas);
            gui.d_Window.Hide();
            gui.CreateMenuWindow(canvas);
            gui.menuWindow.Hide();
            gui.CreateChatWindow(canvas);
            gui.chatWindow.Hide();
            gui.CreateHotBarWindow(canvas);
            renderWindow.SetMouseCursorVisible(false);
            gui.AddText("Welcome to Sabertooth!");
        }

        static void SetupMapAnimations()
        {
            for (int x = 0; x < map.MaxX; x++)
            {
                for (int y = 0; y < map.MaxY; y++)
                {
                    if (map.Ground[x, y].Type == (int)TileType.Animation)
                    {
                        int animNum = map.Ground[x, y].SpawnNum - 1;
                        int index = FindOpenMapAnimationSlot();

                        map.m_Animation[index].Name = animations[animNum].Name;
                        map.m_Animation[index].X = x;
                        map.m_Animation[index].Y = y;
                        map.m_Animation[index].SpriteNumber = animations[animNum].SpriteNumber;
                        map.m_Animation[index].FrameCount = animations[animNum].FrameCount;
                        map.m_Animation[index].FrameCountH = animations[animNum].FrameCountH;
                        map.m_Animation[index].FrameCountV = animations[animNum].FrameCountV;
                        map.m_Animation[index].FrameDuration = animations[animNum].FrameDuration;                        
                        map.m_Animation[index].LoopCount = animations[animNum].LoopCount;
                        map.m_Animation[index].RenderBelowTarget = animations[animNum].RenderBelowTarget;
                        map.m_Animation[index].ConfigAnimation();
                    }
                }
            }
        }

        static int FindOpenMapAnimationSlot()
        {
            for (int i = 0; i < MAX_MAP_ANIMATIONS; i++)
            {
                if (map.m_Animation[i].Name == null)
                {
                    return i;
                }
            }
            return MAX_MAP_ANIMATIONS - 1;
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
        ManaData,
        VitalLoss,
        ItemData,
        Items,
        Shutdown,
        Attack,
        UpdateWeapons,
        UpdatePlayerStats,
        PlayerInv,
        UnequipItem,
        EquipItem,
        DropItem,
        PlayerEquip,
        NpcDirection,
        NpcVitals,
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
        PlayerAttack,
        SendQuests,
        SendQuestData,
        SendQuestList,
        SendHotBar,
        SendInvSwap,
        SendBankSwap,
        UpdateHotBar,
        UseHotBar,
        ItemCoolDown,
        AnimationData,
        AnimationsData,
        PlayerTarget,
        SendSpellBook,
        SpellData,
        SpellsData,
        ForgetSpell,
        CastSpell
    }
}
