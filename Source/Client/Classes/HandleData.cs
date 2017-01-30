using Gwen.Control;
using Lidgren.Network;
using System;
using static System.Convert;
using static System.Environment;

namespace Client.Classes
{
    class HandleData
    {
        public string s_IPAddress;
        public string s_Port;
        public int c_Index;
        public string c_Version = "1.0";

        public void DataMessage(NetClient c_Client, Canvas c_Canvas, GUI c_GUI, Player[] c_Player, Map c_Map, 
            ClientConfig c_Config, Npc[] c_Npc, Item[] c_Item, Projectile[] c_Proj)
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
                                HandleCreateProjectile(incMSG, c_Player, c_Map, c_Proj);
                                break;

                            case (byte)PacketTypes.ClearProj:
                                HandleClearProjectile(incMSG, c_Player, c_Map);
                                break;

                            case (byte)PacketTypes.UpdateWeapons:
                                HandleWeaponsUpdate(incMSG, c_Client, c_Player, c_Index);
                                break;

                            case (byte)PacketTypes.UpdatePlayerStats:
                                HandleUpdatePlayerStats(incMSG, c_Client, c_Player, c_Index);
                                break;

                            case (byte)PacketTypes.PoolNpcs:
                                HandlePoolNpcs(incMSG, c_Map);
                                break;

                            case (byte)PacketTypes.PoolNpcData:
                                HandlePoolNpcData(incMSG, c_Map);
                                break;

                            case (byte)PacketTypes.PlayerInv:
                                HandlePlayerInv(incMSG, c_Player, c_Index);
                                break;

                            case (byte)PacketTypes.MapItems:
                                HandleMapItems(incMSG, c_Map);
                                break;

                            case (byte)PacketTypes.MapItemData:
                                 HandleMapItemData(incMSG, c_Map);
                                break;

                            case (byte)PacketTypes.PlayerEquip:
                                HandlePlayerEquipment(incMSG, c_Player, c_Index);
                                break;

                            case (byte)PacketTypes.NpcDirection:
                                HandleNpcDirection(incMSG, c_Map);
                                break;

                            case (byte)PacketTypes.PoolNpcDirecion:
                                HandleNpcPoolDirection(incMSG, c_Map);
                                break;

                            case (byte)PacketTypes.NpcVitals:
                                HandleNpcVitals(incMSG, c_Map);
                                break;

                            case (byte)PacketTypes.PoolNpcVitals:
                                HandlePoolNpcVitals(incMSG, c_Map);
                                break;

                        }
                        break;
                }
                #if DEBUG
                Console.WriteLine("INCMSG Size: " + incMSG.LengthBytes + " btyes, " + incMSG.LengthBits + " bits, " + incMSG.DeliveryMethod.ToString());
                #endif
        }
            c_Client.Recycle(incMSG);
        }

        #region Handle Incoming Data
        void HandleServerShutdown(NetClient c_Client, Canvas c_Canvas)
        {
            c_Canvas.Dispose();
            c_Client.Shutdown("Disconnect");
            Exit(0);
        }

        void HandlePlayerEquipment(NetIncomingMessage incMSG, Player[] c_Player, int index)
        {
            c_Player[index].Chest.Name = incMSG.ReadString();
            c_Player[index].Chest.Sprite = incMSG.ReadVariableInt32();
            c_Player[index].Chest.Damage = incMSG.ReadVariableInt32();
            c_Player[index].Chest.Armor = incMSG.ReadVariableInt32();
            c_Player[index].Chest.Type = incMSG.ReadVariableInt32();
            c_Player[index].Chest.AttackSpeed = incMSG.ReadVariableInt32();
            c_Player[index].Chest.ReloadSpeed = incMSG.ReadVariableInt32();
            c_Player[index].Chest.HealthRestore = incMSG.ReadVariableInt32();
            c_Player[index].Chest.HungerRestore = incMSG.ReadVariableInt32();
            c_Player[index].Chest.HydrateRestore = incMSG.ReadVariableInt32();
            c_Player[index].Chest.Strength = incMSG.ReadVariableInt32();
            c_Player[index].Chest.Agility = incMSG.ReadVariableInt32();
            c_Player[index].Chest.Endurance = incMSG.ReadVariableInt32();
            c_Player[index].Chest.Stamina = incMSG.ReadVariableInt32();
            c_Player[index].Chest.Clip = incMSG.ReadVariableInt32();
            c_Player[index].Chest.maxClip = incMSG.ReadVariableInt32();
            c_Player[index].Chest.ammoType = incMSG.ReadVariableInt32();
            c_Player[index].Chest.Value = incMSG.ReadVariableInt32();
            c_Player[index].Chest.ProjectileNumber = incMSG.ReadVariableInt32();

            c_Player[index].Legs.Name = incMSG.ReadString();
            c_Player[index].Legs.Sprite = incMSG.ReadVariableInt32();
            c_Player[index].Legs.Damage = incMSG.ReadVariableInt32();
            c_Player[index].Legs.Armor = incMSG.ReadVariableInt32();
            c_Player[index].Legs.Type = incMSG.ReadVariableInt32();
            c_Player[index].Legs.AttackSpeed = incMSG.ReadVariableInt32();
            c_Player[index].Legs.ReloadSpeed = incMSG.ReadVariableInt32();
            c_Player[index].Legs.HealthRestore = incMSG.ReadVariableInt32();
            c_Player[index].Legs.HungerRestore = incMSG.ReadVariableInt32();
            c_Player[index].Legs.HydrateRestore = incMSG.ReadVariableInt32();
            c_Player[index].Legs.Strength = incMSG.ReadVariableInt32();
            c_Player[index].Legs.Agility = incMSG.ReadVariableInt32();
            c_Player[index].Legs.Endurance = incMSG.ReadVariableInt32();
            c_Player[index].Legs.Stamina = incMSG.ReadVariableInt32();
            c_Player[index].Legs.Clip = incMSG.ReadVariableInt32();
            c_Player[index].Legs.maxClip = incMSG.ReadVariableInt32();
            c_Player[index].Legs.ammoType = incMSG.ReadVariableInt32();
            c_Player[index].Legs.Value = incMSG.ReadVariableInt32();
            c_Player[index].Legs.ProjectileNumber = incMSG.ReadVariableInt32();

            c_Player[index].Feet.Name = incMSG.ReadString();
            c_Player[index].Feet.Sprite = incMSG.ReadVariableInt32();
            c_Player[index].Feet.Damage = incMSG.ReadVariableInt32();
            c_Player[index].Feet.Armor = incMSG.ReadVariableInt32();
            c_Player[index].Feet.Type = incMSG.ReadVariableInt32();
            c_Player[index].Feet.AttackSpeed = incMSG.ReadVariableInt32();
            c_Player[index].Feet.ReloadSpeed = incMSG.ReadVariableInt32();
            c_Player[index].Feet.HealthRestore = incMSG.ReadVariableInt32();
            c_Player[index].Feet.HungerRestore = incMSG.ReadVariableInt32();
            c_Player[index].Feet.HydrateRestore = incMSG.ReadVariableInt32();
            c_Player[index].Feet.Strength = incMSG.ReadVariableInt32();
            c_Player[index].Feet.Agility = incMSG.ReadVariableInt32();
            c_Player[index].Feet.Endurance = incMSG.ReadVariableInt32();
            c_Player[index].Feet.Stamina = incMSG.ReadVariableInt32();
            c_Player[index].Feet.Clip = incMSG.ReadVariableInt32();
            c_Player[index].Feet.maxClip = incMSG.ReadVariableInt32();
            c_Player[index].Feet.ammoType = incMSG.ReadVariableInt32();
            c_Player[index].Feet.Value = incMSG.ReadVariableInt32();
            c_Player[index].Feet.ProjectileNumber = incMSG.ReadVariableInt32();
        }

        void HandlePlayerInv(NetIncomingMessage incMSG, Player[] c_Player, int index)
        {
            for (int i = 0; i < 25; i++)
            {
                if (c_Player[index].Backpack[i] != null)
                {
                    c_Player[index].Backpack[i].Name = incMSG.ReadString();
                    c_Player[index].Backpack[i].Sprite = incMSG.ReadVariableInt32();
                    c_Player[index].Backpack[i].Damage = incMSG.ReadVariableInt32();
                    c_Player[index].Backpack[i].Armor = incMSG.ReadVariableInt32();
                    c_Player[index].Backpack[i].Type = incMSG.ReadVariableInt32();
                    c_Player[index].Backpack[i].AttackSpeed = incMSG.ReadVariableInt32();
                    c_Player[index].Backpack[i].ReloadSpeed = incMSG.ReadVariableInt32();
                    c_Player[index].Backpack[i].HealthRestore = incMSG.ReadVariableInt32();
                    c_Player[index].Backpack[i].HungerRestore = incMSG.ReadVariableInt32();
                    c_Player[index].Backpack[i].HydrateRestore = incMSG.ReadVariableInt32();
                    c_Player[index].Backpack[i].Strength = incMSG.ReadVariableInt32();
                    c_Player[index].Backpack[i].Agility = incMSG.ReadVariableInt32();
                    c_Player[index].Backpack[i].Endurance = incMSG.ReadVariableInt32();
                    c_Player[index].Backpack[i].Stamina = incMSG.ReadVariableInt32();
                    c_Player[index].Backpack[i].Clip = incMSG.ReadVariableInt32();
                    c_Player[index].Backpack[i].maxClip = incMSG.ReadVariableInt32();
                    c_Player[index].Backpack[i].ammoType = incMSG.ReadVariableInt32();
                    c_Player[index].Backpack[i].Value = incMSG.ReadVariableInt32();
                    c_Player[index].Backpack[i].ProjectileNumber = incMSG.ReadVariableInt32();
                }
            }
        }

        void HandleClearProjectile(NetIncomingMessage incMSG, Player[] c_Player, Map c_Map)
        {
            string mapName = incMSG.ReadString();
            if (mapName != c_Map.Name) { return; }

            int slot = incMSG.ReadVariableInt32();
            if (c_Map.mapProj[slot] != null) { return; }
            c_Map.mapProj[slot] = null;
        }

        void HandleCreateProjectile(NetIncomingMessage incMSG, Player[] c_Player, Map c_Map, Projectile[] c_Proj)
        {
            int slot = incMSG.ReadVariableInt32();
            int proj = incMSG.ReadVariableInt32();

            c_Map.mapProj[slot] = new MapProj();

            c_Map.mapProj[slot].X = incMSG.ReadVariableInt32();
            c_Map.mapProj[slot].Y = incMSG.ReadVariableInt32();
            c_Map.mapProj[slot].Direction = incMSG.ReadVariableInt32();
            c_Map.mapProj[slot].Name = c_Proj[proj].Name;
            c_Map.mapProj[slot].Speed = c_Proj[proj].Speed;
            c_Map.mapProj[slot].Type = c_Proj[proj].Type;
            c_Map.mapProj[slot].Sprite = c_Proj[proj].Sprite;
            c_Map.mapProj[slot].Range = c_Proj[proj].Range;
        }

        void HandleUpdateAmmo(NetIncomingMessage incMSG, Player[] c_Player)
        {
            int index = incMSG.ReadVariableInt32();

            c_Player[index].mainWeapon.Clip = incMSG.ReadVariableInt32();
            c_Player[index].PistolAmmo = incMSG.ReadVariableInt32();
            c_Player[index].AssaultAmmo = incMSG.ReadVariableInt32();
            c_Player[index].RocketAmmo = incMSG.ReadVariableInt32();
            c_Player[index].GrenadeAmmo = incMSG.ReadVariableInt32();
        }

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
                    c_Item[i].Value = incMSG.ReadVariableInt32();
                    c_Item[i].ProjectileNumber = incMSG.ReadVariableInt32();
                }
            }
        }

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
            c_Item[index].Value = incMSG.ReadVariableInt32();
            c_Item[index].ProjectileNumber = incMSG.ReadVariableInt32();
        }

        void HandleNpcs(NetIncomingMessage incMSG, Npc[] c_Npc)
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
                    c_Npc[i].MaxHealth = incMSG.ReadVariableInt32();
                    c_Npc[i].Damage = incMSG.ReadVariableInt32();
                    c_Npc[i].IsSpawned = incMSG.ReadBoolean();
                }
            }
        }

        void HandlePoolNpcs(NetIncomingMessage incMSG, Map c_Map)
        {
            for (int i = 0; i < 20; i++)
            {
                if (c_Map.r_MapNpc[i] != null)
                {
                    c_Map.r_MapNpc[i].Name = incMSG.ReadString();
                    c_Map.r_MapNpc[i].X = incMSG.ReadVariableInt32();
                    c_Map.r_MapNpc[i].Y = incMSG.ReadVariableInt32();
                    c_Map.r_MapNpc[i].Direction = incMSG.ReadVariableInt32();
                    c_Map.r_MapNpc[i].Sprite = incMSG.ReadVariableInt32();
                    c_Map.r_MapNpc[i].Step = incMSG.ReadVariableInt32();
                    c_Map.r_MapNpc[i].Owner = incMSG.ReadVariableInt32();
                    c_Map.r_MapNpc[i].Behavior = incMSG.ReadVariableInt32();
                    c_Map.r_MapNpc[i].SpawnTime = incMSG.ReadVariableInt32();
                    c_Map.r_MapNpc[i].Health = incMSG.ReadVariableInt32();
                    c_Map.r_MapNpc[i].MaxHealth = incMSG.ReadVariableInt32();
                    c_Map.r_MapNpc[i].Damage = incMSG.ReadVariableInt32();
                    c_Map.r_MapNpc[i].IsSpawned = incMSG.ReadBoolean();
                }
            }
        }

        void HandleMapItems(NetIncomingMessage incMSG, Map c_Map)
        {
            for (int i = 0; i < 20; i++)
            {
                c_Map.mapItem[i].Name = incMSG.ReadString();
                c_Map.mapItem[i].X = incMSG.ReadVariableInt32();
                c_Map.mapItem[i].Y = incMSG.ReadVariableInt32();
                c_Map.mapItem[i].Sprite = incMSG.ReadVariableInt32();
                c_Map.mapItem[i].Damage = incMSG.ReadVariableInt32();
                c_Map.mapItem[i].Armor = incMSG.ReadVariableInt32();
                c_Map.mapItem[i].Type = incMSG.ReadVariableInt32();
                c_Map.mapItem[i].HealthRestore = incMSG.ReadVariableInt32();
                c_Map.mapItem[i].HungerRestore = incMSG.ReadVariableInt32();
                c_Map.mapItem[i].HydrateRestore = incMSG.ReadVariableInt32();
                c_Map.mapItem[i].Strength = incMSG.ReadVariableInt32();
                c_Map.mapItem[i].Agility = incMSG.ReadVariableInt32();
                c_Map.mapItem[i].Endurance = incMSG.ReadVariableInt32();
                c_Map.mapItem[i].Stamina = incMSG.ReadVariableInt32();
                c_Map.mapItem[i].Clip = incMSG.ReadVariableInt32();
                c_Map.mapItem[i].maxClip = incMSG.ReadVariableInt32();
                c_Map.mapItem[i].ammoType = incMSG.ReadVariableInt32();
                c_Map.mapItem[i].Value = incMSG.ReadVariableInt32();
                c_Map.mapItem[i].ProjectileNumber = incMSG.ReadVariableInt32();
                c_Map.mapItem[i].IsSpawned = incMSG.ReadBoolean();
            }
        }

        void HandleMapItemData(NetIncomingMessage incMSG, Map c_Map)
        {
            int itemNum = incMSG.ReadVariableInt32();

            c_Map.mapItem[itemNum].Name = incMSG.ReadString();
            c_Map.mapItem[itemNum].X = incMSG.ReadVariableInt32();
            c_Map.mapItem[itemNum].Y = incMSG.ReadVariableInt32();
            c_Map.mapItem[itemNum].Sprite = incMSG.ReadVariableInt32();
            c_Map.mapItem[itemNum].Damage = incMSG.ReadVariableInt32();
            c_Map.mapItem[itemNum].Armor = incMSG.ReadVariableInt32();
            c_Map.mapItem[itemNum].Type = incMSG.ReadVariableInt32();
            c_Map.mapItem[itemNum].HealthRestore = incMSG.ReadVariableInt32();
            c_Map.mapItem[itemNum].HungerRestore = incMSG.ReadVariableInt32();
            c_Map.mapItem[itemNum].HydrateRestore = incMSG.ReadVariableInt32();
            c_Map.mapItem[itemNum].Strength = incMSG.ReadVariableInt32();
            c_Map.mapItem[itemNum].Agility = incMSG.ReadVariableInt32();
            c_Map.mapItem[itemNum].Endurance = incMSG.ReadVariableInt32();
            c_Map.mapItem[itemNum].Stamina = incMSG.ReadVariableInt32();
            c_Map.mapItem[itemNum].Clip = incMSG.ReadVariableInt32();
            c_Map.mapItem[itemNum].maxClip = incMSG.ReadVariableInt32();
            c_Map.mapItem[itemNum].ammoType = incMSG.ReadVariableInt32();
            c_Map.mapItem[itemNum].Value = incMSG.ReadVariableInt32();
            c_Map.mapItem[itemNum].ProjectileNumber = incMSG.ReadVariableInt32();
            c_Map.mapItem[itemNum].IsSpawned = incMSG.ReadBoolean();
        }

        void HandleMapNpcs(NetIncomingMessage incMSG, Map c_Map)
        {
            for (int i = 0; i < 10; i++)
            {
                if (c_Map.m_MapNpc[i] != null)
                {
                    c_Map.m_MapNpc[i].Name = incMSG.ReadString();
                    c_Map.m_MapNpc[i].X = incMSG.ReadVariableInt32();
                    c_Map.m_MapNpc[i].Y = incMSG.ReadVariableInt32();
                    c_Map.m_MapNpc[i].Direction = incMSG.ReadVariableInt32();
                    c_Map.m_MapNpc[i].Sprite = incMSG.ReadVariableInt32();
                    c_Map.m_MapNpc[i].Step = incMSG.ReadVariableInt32();
                    c_Map.m_MapNpc[i].Owner = incMSG.ReadVariableInt32();
                    c_Map.m_MapNpc[i].Behavior = incMSG.ReadVariableInt32();
                    c_Map.m_MapNpc[i].SpawnTime = incMSG.ReadVariableInt32();
                    c_Map.m_MapNpc[i].Health = incMSG.ReadVariableInt32();
                    c_Map.m_MapNpc[i].MaxHealth = incMSG.ReadVariableInt32();
                    c_Map.m_MapNpc[i].Damage = incMSG.ReadVariableInt32();
                    c_Map.m_MapNpc[i].IsSpawned = incMSG.ReadBoolean();
                }
            }
        }

        void HandlePoolNpcData(NetIncomingMessage incMSG, Map c_Map)
        {
            int npcNum = incMSG.ReadVariableInt32();

            c_Map.r_MapNpc[npcNum].Name = incMSG.ReadString();
            c_Map.r_MapNpc[npcNum].X = incMSG.ReadVariableInt32();
            c_Map.r_MapNpc[npcNum].Y = incMSG.ReadVariableInt32();
            c_Map.r_MapNpc[npcNum].Direction = incMSG.ReadVariableInt32();
            c_Map.r_MapNpc[npcNum].Sprite = incMSG.ReadVariableInt32();
            c_Map.r_MapNpc[npcNum].Step = incMSG.ReadVariableInt32();
            c_Map.r_MapNpc[npcNum].Owner = incMSG.ReadVariableInt32();
            c_Map.r_MapNpc[npcNum].Behavior = incMSG.ReadVariableInt32();
            c_Map.r_MapNpc[npcNum].SpawnTime = incMSG.ReadVariableInt32();
            c_Map.r_MapNpc[npcNum].Health = incMSG.ReadVariableInt32();
            c_Map.r_MapNpc[npcNum].MaxHealth = incMSG.ReadVariableInt32();
            c_Map.r_MapNpc[npcNum].Damage = incMSG.ReadVariableInt32();
            c_Map.r_MapNpc[npcNum].IsSpawned = incMSG.ReadBoolean();
        }

        void HandleNpcDirection(NetIncomingMessage incMSG, Map c_Map)
        {
            int index = incMSG.ReadVariableInt32();

            c_Map.m_MapNpc[index].X = incMSG.ReadVariableInt32();
            c_Map.m_MapNpc[index].Y = incMSG.ReadVariableInt32();
            c_Map.m_MapNpc[index].Direction = incMSG.ReadVariableInt32();
            c_Map.m_MapNpc[index].Step = incMSG.ReadVariableInt32();
        }

        void HandleNpcPoolDirection(NetIncomingMessage incMSG, Map c_Map)
        {
            int index = incMSG.ReadVariableInt32();

            c_Map.r_MapNpc[index].X = incMSG.ReadVariableInt32();
            c_Map.r_MapNpc[index].Y = incMSG.ReadVariableInt32();
            c_Map.r_MapNpc[index].Direction = incMSG.ReadVariableInt32();
            c_Map.r_MapNpc[index].Step = incMSG.ReadVariableInt32();
        }

        void HandleNpcVitals(NetIncomingMessage incMSG, Map c_Map)
        {
            int index = incMSG.ReadVariableInt32();

            c_Map.m_MapNpc[index].Health = incMSG.ReadVariableInt32();
            c_Map.m_MapNpc[index].IsSpawned = incMSG.ReadBoolean();
        }

        void HandlePoolNpcVitals(NetIncomingMessage incMSG, Map c_Map)
        {
            int index = incMSG.ReadVariableInt32();

            c_Map.r_MapNpc[index].Health = incMSG.ReadVariableInt32();
            c_Map.r_MapNpc[index].IsSpawned = incMSG.ReadBoolean();
        }

        void HandleNpcData(NetIncomingMessage incMSG, Map c_Map)
        {
            int npcNum = incMSG.ReadVariableInt32();

            c_Map.m_MapNpc[npcNum].Name = incMSG.ReadString();
            c_Map.m_MapNpc[npcNum].X = incMSG.ReadVariableInt32();
            c_Map.m_MapNpc[npcNum].Y = incMSG.ReadVariableInt32();
            c_Map.m_MapNpc[npcNum].Direction = incMSG.ReadVariableInt32();
            c_Map.m_MapNpc[npcNum].Sprite = incMSG.ReadVariableInt32();
            c_Map.m_MapNpc[npcNum].Step = incMSG.ReadVariableInt32();
            c_Map.m_MapNpc[npcNum].Owner = incMSG.ReadVariableInt32();
            c_Map.m_MapNpc[npcNum].Behavior = incMSG.ReadVariableInt32();
            c_Map.m_MapNpc[npcNum].SpawnTime = incMSG.ReadVariableInt32();
            c_Map.m_MapNpc[npcNum].Health = incMSG.ReadVariableInt32();
            c_Map.m_MapNpc[npcNum].MaxHealth = incMSG.ReadVariableInt32();
            c_Map.m_MapNpc[npcNum].Damage = incMSG.ReadVariableInt32();
            c_Map.m_MapNpc[npcNum].IsSpawned = incMSG.ReadBoolean();
        }

        void HandleUpdateDirectionData(NetIncomingMessage incMSG, Player[] c_Player, int clientIndex)
        {
            int index = incMSG.ReadVariableInt32();
            int direction = incMSG.ReadVariableInt32();
            int aimdirection = incMSG.ReadVariableInt32();

            c_Player[index].Direction = direction;
            c_Player[index].AimDirection = aimdirection;
        }

        void HandleUpdateMoveData(NetIncomingMessage incMSG, Player[] c_Player, int clientIndex)
        {
            int index = incMSG.ReadVariableInt32();
            int x = incMSG.ReadVariableInt32();
            int y = incMSG.ReadVariableInt32();
            int direction = incMSG.ReadVariableInt32();
            int aimdirection = incMSG.ReadVariableInt32();
            int step = incMSG.ReadVariableInt32();

            c_Player[index].tempX = x;
            c_Player[index].tempY = y;
            c_Player[index].tempDir = direction;
            c_Player[index].tempaimDir = aimdirection;
            c_Player[index].tempStep = step;
        }

        void HandleDiscoveryResponse(NetIncomingMessage incMSG, NetClient c_Client)
        {
            Console.WriteLine("Found Server: " + incMSG.ReadString() + " @ " + incMSG.SenderEndPoint);
            NetOutgoingMessage outMSG = c_Client.CreateMessage();
            outMSG.Write((byte)PacketTypes.Connection);
            outMSG.Write("sabertooth");
            c_Client.Connect(s_IPAddress, ToInt32(s_Port), outMSG);
        }

        void HandleConnectionData(NetIncomingMessage incMSG, NetClient c_Client)
        {
            if (c_Client.ServerConnection != null) { return; }
            Console.WriteLine("Connected to server!");        }

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
            if (!c_GUI.chatWindow.IsVisible) { c_GUI.chatWindow.Show(); }
            c_GUI.outputChat.ScrollToBottom();
            c_GUI.outputChat.UnselectAll();
        }

        void HandleLoginData(NetIncomingMessage incMSG, NetClient c_Client, Canvas c_Canvas, GUI c_GUI)
        {
            Console.WriteLine("Login successful! IP: " + incMSG.SenderConnection);
            c_Canvas.DeleteAllChildren();
            c_GUI.CreateLoadingWindow(c_Canvas);
        }

        void HandleVitalData(NetIncomingMessage incMSG, Player[] c_Player)
        {
            int index = incMSG.ReadVariableInt32();
            string vitalName = incMSG.ReadString();
            int vital = incMSG.ReadVariableInt32();

            if (vitalName == "food") { c_Player[index].Hunger = vital; }
            if (vitalName == "water") { c_Player[index].Hydration = vital; }
        }

        void HandleHealthData(NetIncomingMessage incMSG, Player[] c_Player)
        {
            int index = incMSG.ReadVariableInt32();
            int health = incMSG.ReadVariableInt32();

            c_Player[index].Health = health;
        }

        void HandleUpdatePlayerStats(NetIncomingMessage incMSG, NetClient c_Client, Player[] c_Player, int index)
        {
            c_Player[index].Level = incMSG.ReadVariableInt32();
            c_Player[index].Points = incMSG.ReadVariableInt32();
            c_Player[index].Health = incMSG.ReadVariableInt32();
            c_Player[index].MaxHealth = incMSG.ReadVariableInt32();
            c_Player[index].Hunger = incMSG.ReadVariableInt32();
            c_Player[index].Hydration = incMSG.ReadVariableInt32();
            c_Player[index].Experience = incMSG.ReadVariableInt32();
            c_Player[index].Money = incMSG.ReadVariableInt32();
            c_Player[index].Armor = incMSG.ReadVariableInt32();
            c_Player[index].Strength = incMSG.ReadVariableInt32();
            c_Player[index].Agility = incMSG.ReadVariableInt32();
            c_Player[index].Endurance = incMSG.ReadVariableInt32();
            c_Player[index].Stamina = incMSG.ReadVariableInt32();
            c_Player[index].PistolAmmo = incMSG.ReadVariableInt32();
            c_Player[index].AssaultAmmo = incMSG.ReadVariableInt32();
            c_Player[index].RocketAmmo = incMSG.ReadVariableInt32();
            c_Player[index].GrenadeAmmo = incMSG.ReadVariableInt32();
        }

        void HandlePlayerData(NetIncomingMessage incMSG, NetClient c_Client, Player[] c_Player, int index)
        {            
            c_Player[index].Name = incMSG.ReadString();
            c_Player[index].X = incMSG.ReadVariableInt32();
            c_Player[index].Y = incMSG.ReadVariableInt32();
            c_Player[index].Map = incMSG.ReadVariableInt32();
            c_Player[index].Direction = incMSG.ReadVariableInt32();
            c_Player[index].AimDirection = incMSG.ReadVariableInt32();
            c_Player[index].Sprite = incMSG.ReadVariableInt32();
            c_Player[index].Level = incMSG.ReadVariableInt32();
            c_Player[index].Points = incMSG.ReadVariableInt32();
            c_Player[index].Health = incMSG.ReadVariableInt32();
            c_Player[index].MaxHealth = incMSG.ReadVariableInt32();
            c_Player[index].Hunger = incMSG.ReadVariableInt32();
            c_Player[index].Hydration = incMSG.ReadVariableInt32();
            c_Player[index].Experience = incMSG.ReadVariableInt32();
            c_Player[index].Money = incMSG.ReadVariableInt32();
            c_Player[index].Armor = incMSG.ReadVariableInt32();
            c_Player[index].Strength = incMSG.ReadVariableInt32();
            c_Player[index].Agility = incMSG.ReadVariableInt32();
            c_Player[index].Endurance = incMSG.ReadVariableInt32();
            c_Player[index].Stamina = incMSG.ReadVariableInt32();
            c_Player[index].PistolAmmo = incMSG.ReadVariableInt32();
            c_Player[index].AssaultAmmo = incMSG.ReadVariableInt32();
            c_Player[index].RocketAmmo = incMSG.ReadVariableInt32();
            c_Player[index].GrenadeAmmo = incMSG.ReadVariableInt32();
            c_Player[index].offsetX = 12;
            c_Player[index].offsetY = 9;

            //Main Weapon
            c_Player[index].mainWeapon.Name = incMSG.ReadString();
            c_Player[index].mainWeapon.Clip = incMSG.ReadVariableInt32();
            c_Player[index].mainWeapon.maxClip = incMSG.ReadVariableInt32();
            c_Player[index].mainWeapon.Sprite = incMSG.ReadVariableInt32();
            c_Player[index].mainWeapon.Damage = incMSG.ReadVariableInt32();
            c_Player[index].mainWeapon.Armor = incMSG.ReadVariableInt32();
            c_Player[index].mainWeapon.Type = incMSG.ReadVariableInt32();
            c_Player[index].mainWeapon.AttackSpeed = incMSG.ReadVariableInt32();
            c_Player[index].mainWeapon.ReloadSpeed = incMSG.ReadVariableInt32();
            c_Player[index].mainWeapon.HealthRestore = incMSG.ReadVariableInt32();
            c_Player[index].mainWeapon.HungerRestore = incMSG.ReadVariableInt32();
            c_Player[index].mainWeapon.HydrateRestore = incMSG.ReadVariableInt32();
            c_Player[index].mainWeapon.Strength = incMSG.ReadVariableInt32();
            c_Player[index].mainWeapon.Agility = incMSG.ReadVariableInt32();
            c_Player[index].mainWeapon.Endurance = incMSG.ReadVariableInt32();
            c_Player[index].mainWeapon.Stamina = incMSG.ReadVariableInt32();
            c_Player[index].mainWeapon.ammoType = incMSG.ReadVariableInt32();
            c_Player[index].mainWeapon.Value = incMSG.ReadVariableInt32();
            c_Player[index].mainWeapon.ProjectileNumber = incMSG.ReadVariableInt32();

            //Secondary Weapon
            c_Player[index].offWeapon.Name = incMSG.ReadString();
            c_Player[index].offWeapon.Clip = incMSG.ReadVariableInt32();
            c_Player[index].offWeapon.maxClip = incMSG.ReadVariableInt32();
            c_Player[index].offWeapon.Sprite = incMSG.ReadVariableInt32();
            c_Player[index].offWeapon.Damage = incMSG.ReadVariableInt32();
            c_Player[index].offWeapon.Armor = incMSG.ReadVariableInt32();
            c_Player[index].offWeapon.Type = incMSG.ReadVariableInt32();
            c_Player[index].offWeapon.AttackSpeed = incMSG.ReadVariableInt32();
            c_Player[index].offWeapon.ReloadSpeed = incMSG.ReadVariableInt32();
            c_Player[index].offWeapon.HealthRestore = incMSG.ReadVariableInt32();
            c_Player[index].offWeapon.HungerRestore = incMSG.ReadVariableInt32();
            c_Player[index].offWeapon.HydrateRestore = incMSG.ReadVariableInt32();
            c_Player[index].offWeapon.Strength = incMSG.ReadVariableInt32();
            c_Player[index].offWeapon.Agility = incMSG.ReadVariableInt32();
            c_Player[index].offWeapon.Endurance = incMSG.ReadVariableInt32();
            c_Player[index].offWeapon.Stamina = incMSG.ReadVariableInt32();
            c_Player[index].offWeapon.ammoType = incMSG.ReadVariableInt32();
            c_Player[index].offWeapon.Value = incMSG.ReadVariableInt32();
            c_Player[index].offWeapon.ProjectileNumber = incMSG.ReadVariableInt32();
        }

        void HandleWeaponsUpdate(NetIncomingMessage incMSG, NetClient c_Client, Player[] c_Player, int index)
        {
            //Main Weapon
            c_Player[index].mainWeapon.Name = incMSG.ReadString();
            c_Player[index].mainWeapon.Clip = incMSG.ReadVariableInt32();
            c_Player[index].mainWeapon.maxClip = incMSG.ReadVariableInt32();
            c_Player[index].mainWeapon.Sprite = incMSG.ReadVariableInt32();
            c_Player[index].mainWeapon.Damage = incMSG.ReadVariableInt32();
            c_Player[index].mainWeapon.Armor = incMSG.ReadVariableInt32();
            c_Player[index].mainWeapon.Type = incMSG.ReadVariableInt32();
            c_Player[index].mainWeapon.AttackSpeed = incMSG.ReadVariableInt32();
            c_Player[index].mainWeapon.ReloadSpeed = incMSG.ReadVariableInt32();
            c_Player[index].mainWeapon.HealthRestore = incMSG.ReadVariableInt32();
            c_Player[index].mainWeapon.HungerRestore = incMSG.ReadVariableInt32();
            c_Player[index].mainWeapon.HydrateRestore = incMSG.ReadVariableInt32();
            c_Player[index].mainWeapon.Strength = incMSG.ReadVariableInt32();
            c_Player[index].mainWeapon.Agility = incMSG.ReadVariableInt32();
            c_Player[index].mainWeapon.Endurance = incMSG.ReadVariableInt32();
            c_Player[index].mainWeapon.Stamina = incMSG.ReadVariableInt32();
            c_Player[index].mainWeapon.ammoType = incMSG.ReadVariableInt32();
            c_Player[index].mainWeapon.Value = incMSG.ReadVariableInt32();
            c_Player[index].mainWeapon.ProjectileNumber = incMSG.ReadVariableInt32();

            //Secondary Weapon
            c_Player[index].offWeapon.Name = incMSG.ReadString();
            c_Player[index].offWeapon.Clip = incMSG.ReadVariableInt32();
            c_Player[index].offWeapon.maxClip = incMSG.ReadVariableInt32();
            c_Player[index].offWeapon.Sprite = incMSG.ReadVariableInt32();
            c_Player[index].offWeapon.Damage = incMSG.ReadVariableInt32();
            c_Player[index].offWeapon.Armor = incMSG.ReadVariableInt32();
            c_Player[index].offWeapon.Type = incMSG.ReadVariableInt32();
            c_Player[index].offWeapon.AttackSpeed = incMSG.ReadVariableInt32();
            c_Player[index].offWeapon.ReloadSpeed = incMSG.ReadVariableInt32();
            c_Player[index].offWeapon.HealthRestore = incMSG.ReadVariableInt32();
            c_Player[index].offWeapon.HungerRestore = incMSG.ReadVariableInt32();
            c_Player[index].offWeapon.HydrateRestore = incMSG.ReadVariableInt32();
            c_Player[index].offWeapon.Strength = incMSG.ReadVariableInt32();
            c_Player[index].offWeapon.Agility = incMSG.ReadVariableInt32();
            c_Player[index].offWeapon.Endurance = incMSG.ReadVariableInt32();
            c_Player[index].offWeapon.Stamina = incMSG.ReadVariableInt32();
            c_Player[index].offWeapon.ammoType = incMSG.ReadVariableInt32();
            c_Player[index].offWeapon.Value = incMSG.ReadVariableInt32();
            c_Player[index].offWeapon.ProjectileNumber = incMSG.ReadVariableInt32();
        }

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
                c_Player[i].Points = incMSG.ReadVariableInt32();
                c_Player[i].Health = incMSG.ReadVariableInt32();
                c_Player[i].MaxHealth = incMSG.ReadVariableInt32();
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
        }

        void HandleErrorMessage(NetIncomingMessage incMSG, NetClient c_Client, Canvas c_Canvas)
        {
            string msg = incMSG.ReadString();
            string caption = incMSG.ReadString();
            MessageBox msgBox = new MessageBox(c_Canvas, msg, caption);
            msgBox.Position(Gwen.Pos.Center);
        }

        void HandleMapData(NetClient c_Client, NetIncomingMessage incMSG, Map c_Map, GUI c_GUI, Canvas c_Canvas)
        {
            c_Map.Name = incMSG.ReadString();

            for (int i = 0; i < 10; i++)
            {
                c_Map.m_MapNpc[i] = new MapNpc();
            }

            for (int i = 0; i < 20; i++)
            {
                c_Map.r_MapNpc[i] = new MapNpc();
                c_Map.mapItem[i] = new MapItem();
            }

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
                    c_Map.Ground[x, y].SpawnNum = incMSG.ReadVariableInt32();
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
            c_Map.t_Map.Load(c_Map);
            c_Map.p_Map.Load(c_Map);
            LoadMainGUI(c_GUI, c_Canvas);
        }
        #endregion

        void LoadMainGUI(GUI c_GUI, Canvas c_Canvas)
        {
            c_Canvas.DeleteAllChildren();
            c_GUI.CreateDebugWindow(c_Canvas);
            c_GUI.d_Window.Hide();
            c_GUI.CreateMenuWindow(c_Canvas);
            c_GUI.menuWindow.Hide();
            c_GUI.CreateChatWindow(c_Canvas);
            c_GUI.chatWindow.Hide();
            c_GUI.AddText("Welcome to Sabertooth!");
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
        PoolNpcVitals
    }
}
