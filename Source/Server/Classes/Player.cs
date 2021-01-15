using Lidgren.Network;
using System;
using System.Data.SqlClient;
using static System.Convert;
using static System.Environment;
using static SabertoothServer.Server;
using AccountKeyGenClass;
using static SabertoothServer.Globals;
using static System.IO.File;

namespace SabertoothServer
{
    public class Player
    {
        #region Main Classes
        public NetConnection Connection;
        public Item MainHand = new Item();
        public Item OffHand = new Item();
        public Item[] Backpack = new Item[MAX_INV_SLOTS];
        public Item[] Bank = new Item[MAX_BANK_SLOTS];
        public Item Chest = new Item();
        public Item Legs = new Item();
        public Item Feet = new Item();
        public int[] QuestList = new int[MAX_PLAYER_QUEST_LIST];
        public int[] QuestStatus = new int[MAX_PLAYER_QUEST_LIST];
        Random RND = new Random();
        #endregion

        #region Stats
        public int Id { get; set; }
        public string Name { get; set; }
        public string Pass { get; set; }
        public string EmailAddress { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Map { get; set; }
        public int Direction { get; set; }
        public int AimDirection { get; set; }
        public int Sprite { get; set; }
        public int Level { get; set; }        
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Mana { get; set; }
        public int MaxMana { get; set; }
        public int Experience { get; set; }
        public int Wallet { get; set; }
        public int Armor { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }        
        public int Stamina { get; set; }
        public int Energy { get; set; }
        public int Step { get; set; }        
        public int LightRadius { get; set; }
        public int PlayDays { get; set; }
        public int PlayHours { get; set; }
        public int PlayMinutes { get; set; }
        public int PlaySeconds { get; set; }
        public string LastLoggedIn { get; set; }
        public string AccountKey { get; set; }
        public string Active { get; set; }
        public int StatsId { get; set; }
        #endregion

        #region Local Variables
        public int timeTick;
        #endregion

        #region Class Constructors
        public Player(string name, string pass, string email, int x, int y, int direction, int aimdirection, int map, int level, int health, int maxhealth, int mana, int maxmana, int exp, int wallet,
            int armor, int str, int agi, int inte, int sta, int eng, int sprite, NetConnection conn)
        {
            Name = name;
            Pass = pass;
            EmailAddress = email;
            X = x;
            Y = y;
            Map = map;
            Direction = direction;
            AimDirection = aimdirection;
            Level = level;
            Experience = exp;
            Wallet = wallet;
            Armor = armor;
            Strength = str;
            Agility = agi;
            Intelligence = inte;
            Stamina = sta;
            Energy = eng;
            Connection = conn;
            MaxHealth = maxhealth;
            Health = MaxHealth;
            Mana = mana;
            MaxMana = maxmana;
            Sprite = sprite;
            timeTick = TickCount;
            LightRadius = 100;
            PlayDays = 0;
            PlayHours = 0;
            PlayMinutes = 0;
            PlaySeconds = 0;
            LastLoggedIn = "1/1/1000 00:00:00.000";
            AccountKey = KeyGen.Key(25);
            Active = "N";

            MainHand = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
            OffHand = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
            Chest = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
            Legs = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
            Feet = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);

            for (int i = 0; i < MAX_INV_SLOTS; i++)
            {
                Backpack[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
            }

            for (int i = 0; i < MAX_BANK_SLOTS; i++)
            {
                Bank[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
            }

            for (int i = 0; i < MAX_PLAYER_QUEST_LIST; i++)
            {
                QuestList[i] = 0;
                QuestStatus[i] = 0;
            }
        }

        public Player(string name, string pass, string email, int x, int y, int direction, int aimdirection, int map, int level, int health, int maxhealth, int mana, int maxmana,
            int exp, int money, int armor, int str, int agi, int inte, int sta, int eng, int sprite)
        {
            Name = name;
            Pass = pass;
            EmailAddress = email;
            X = x;
            Y = y;
            Map = map;
            Direction = direction;
            AimDirection = aimdirection;
            Level = level;
            Experience = exp;
            Wallet = money;
            Armor = armor;
            Health = health;
            MaxHealth = maxhealth;
            Mana = mana;
            MaxMana = maxmana;
            timeTick = TickCount;
            Strength = str;
            Agility = agi;
            Intelligence = eng;
            Stamina = sta;
            Energy = eng;                        
            Sprite = sprite;
            LightRadius = 100;
            PlayDays = 0;
            PlayHours = 0;
            PlayMinutes = 0;
            PlaySeconds = 0;
            LastLoggedIn = "1/1/1000 00:00:00.000";
            AccountKey = KeyGen.Key(25);
            Active = "N";

            MainHand = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
            OffHand = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
            Chest = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
            Legs = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
            Feet = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);

            for (int i = 0; i < MAX_INV_SLOTS; i++)
            {
                Backpack[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
            }

            for (int i = 0; i < MAX_BANK_SLOTS; i++)
            {
                Bank[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
            }

            for (int i = 0; i < MAX_PLAYER_QUEST_LIST; i++)
            {
                QuestList[i] = 0;
                QuestStatus[i] = 0;
            }
        }

        public Player(string name, string pass, NetConnection conn)
        {
            Name = name;
            Pass = pass;
            Connection = conn;
            timeTick = TickCount;

            for (int i = 0; i < MAX_INV_SLOTS; i++)
            {
                Backpack[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
            }

            for (int i = 0; i < MAX_BANK_SLOTS; i++)
            {
                Bank[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
            }

            for (int i = 0; i < MAX_PLAYER_QUEST_LIST; i++)
            {
                QuestList[i] = 0;
                QuestStatus[i] = 0;
            }
        }

        public Player(NetConnection conn)
        {
            Connection = conn;
        }

        public Player()
        {
            for (int i = 0; i < MAX_INV_SLOTS; i++)
            {
                Backpack[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
            }

            for (int i = 0; i < MAX_BANK_SLOTS; i++)
            {
                Bank[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
            }

            for (int i = 0; i < MAX_PLAYER_QUEST_LIST; i++)
            {
                QuestList[i] = 0;
                QuestStatus[i] = 0;
            }
        }
        #endregion

        #region Methods
        public void UpdateLastLogged()
        {
            LastLoggedIn = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff");
        }

        public void RegenHealth()
        {            
            if (Health < MaxHealth)
            {
                Health += (Stamina * 10);
            }

            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }
        }

        public void RegainMana()
        {
            if (Mana < MaxMana)
            {
                Mana += (Energy * 10);
            }

            if (Mana > MaxMana)
            {
                Mana = MaxMana;
            }
        }

        public void CheckPlayerLevelUp()
        {
            int exptoLevel = (Level * 450);
            if (Level == MAX_LEVEL) { Experience = exptoLevel; return; }
            if (Experience >= exptoLevel)
            {
                while (Experience >= exptoLevel)
                {
                    int remainingXp = (Experience - exptoLevel);
                    Level += 1;
                    Experience = remainingXp;
                    MaxHealth += (Stamina * 5);
                    Health = MaxHealth;
                    MaxMana += (Energy * 5);
                    Mana = MaxMana;
                    Strength += RND.Next(1, 3);
                    Agility += RND.Next(1, 3);
                    Intelligence += RND.Next(1, 3);
                    Stamina += RND.Next(1, 3);
                    Energy += RND.Next(1, 3);
                }
            }
        }

        public void DepositItem(int index, int slot)
        {
            if (players[index].Backpack[slot].Name != "None")
            {
                int newSlot = FindOpenBankSlot(players[index].Bank);
                if (newSlot < 50)
                {
                    players[index].Bank[newSlot] = players[index].Backpack[slot];
                    players[index].Backpack[slot] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1);
                    HandleData.SendPlayerBank(index);
                    HandleData.SendPlayerInv(index);
                }
                else
                {
                    HandleData.SendServerMessageTo(Connection, "You bank is full!");
                    return;
                }
            }
        }

        public void WithdrawItem(int index, int slot)
        {
            if (players[index].Bank[slot].Name != "None")
            {
                int newSlot = FindOpenInvSlot(players[index].Backpack);
                if (newSlot < 25)
                {
                    players[index].Backpack[newSlot] = players[index].Bank[slot];
                    players[index].Bank[slot] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1);
                    HandleData.SendPlayerBank(index);
                    HandleData.SendPlayerInv(index);
                }
                else
                {
                    HandleData.SendServerMessageTo(Connection, "You inventory is full!");
                    return;
                }
            }
        }

        public void EquipItem(int index, int slot)
        {
            if (players[index].Backpack[slot].Name != "None")
            {              
                switch (players[index].Backpack[slot].Type)
                {                    
                    case (int)ItemType.RangedWeapon:
                        if (players[index].MainHand.Name == "None")
                        {
                            players[index].MainHand = players[index].Backpack[slot];
                        }
                        else
                        {
                            int newSlot = FindOpenInvSlot(Backpack);

                            if (newSlot < 25)
                            {
                                players[index].Backpack[newSlot] = players[index].MainHand;
                                players[index].MainHand = players[index].Backpack[slot];
                            }
                        }
                        break;

                    case (int)ItemType.MeleeWeapon:
                        if (players[index].OffHand.Name == "None")
                        {
                            players[index].OffHand = players[index].Backpack[slot];
                        }
                        else
                        {
                            int newSlot = FindOpenInvSlot(Backpack);

                            if (newSlot < 25)
                            {
                                players[index].Backpack[newSlot] = players[index].OffHand;
                                players[index].OffHand = players[index].Backpack[slot];
                            }
                        }
                        break;

                    case (int)ItemType.Shirt:
                        if (players[index].Chest.Name == "None")
                        {
                            players[index].Chest = players[index].Backpack[slot];
                        }
                        else
                        {
                            int newSlot = FindOpenInvSlot(Backpack);

                            if (newSlot < 25)
                            {
                                players[index].Backpack[newSlot] = players[index].Chest;
                                players[index].Chest = players[index].Backpack[slot];
                            }
                        }
                        break;

                    case (int)ItemType.Pants:
                        if (players[index].Legs.Name == "None")
                        {
                            players[index].Legs = players[index].Backpack[slot];
                        }
                        else
                        {
                            int newSlot = FindOpenInvSlot(Backpack);

                            if (newSlot < 25)
                            {
                                players[index].Backpack[newSlot] = players[index].Legs;
                                players[index].Legs = players[index].Backpack[slot];
                            }
                        }
                        break;

                    case (int)ItemType.Shoes:
                        if (players[index].Feet.Name == "None")
                        {
                            players[index].Feet = players[index].Backpack[slot];
                        }
                        else
                        {
                            int newSlot = FindOpenInvSlot(Backpack);

                            if (newSlot < 25)
                            {
                                players[index].Backpack[newSlot] = players[index].Feet;
                                players[index].Feet = players[index].Backpack[slot];
                            }
                        }
                        break;

                    case (int)ItemType.Currency:
                        if (players[index].Backpack[slot].Value > 0)
                        {
                            players[index].Wallet += players[index].Backpack[slot].Value;
                        }                        
                        break;

                    //food and drink need stuff to be done

                    case (int)ItemType.FirstAid:
                        if (players[index].Backpack[slot].HealthRestore > 0)
                        {
                            players[index].Health += players[index].Backpack[slot].HealthRestore;
                            if (players[index].Health > players[index].MaxHealth) { players[index].Health = players[index].MaxHealth; }
                        }
                        break;
                }
                players[index].Backpack[slot] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
                HandleData.SendWeaponsUpdate(index);
                HandleData.SendPlayerInv(index);
                HandleData.SendUpdatePlayerStats(index);
                HandleData.SendPlayerEquipment(index);
            }
        }

        public void UnequipItem(int index, int equip)
        {
            switch (equip)
            {
                case (int)EquipSlots.MainHand:
                    int itemSlot = players[index].FindOpenInvSlot(players[index].Backpack);
                    if (itemSlot < 25)
                    {
                        players[index].Backpack[itemSlot] = players[index].MainHand;
                        players[index].MainHand = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);

                        HandleData.SendWeaponsUpdate(index);
                        HandleData.SendPlayerInv(index);
                        HandleData.SendPlayerEquipment(index);
                    }
                    else
                    {
                        HandleData.SendServerMessageTo(Connection, "Inventory is full!");
                        return;
                    }
                    break;

                case (int)EquipSlots.OffHand:
                    itemSlot = players[index].FindOpenInvSlot(players[index].Backpack);
                    if (itemSlot < 25)
                    {
                        players[index].Backpack[itemSlot] = players[index].OffHand;
                        players[index].OffHand = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
                        HandleData.SendWeaponsUpdate(index);
                        HandleData.SendPlayerInv(index);
                        HandleData.SendPlayerEquipment(index);
                    }
                    else
                    {
                        HandleData.SendServerMessageTo(Connection, "Inventory is full!");
                        return;
                    }
                    break;

                case (int)EquipSlots.Chest:
                    itemSlot = players[index].FindOpenInvSlot(players[index].Backpack);
                    if (itemSlot < 25)
                    {
                        players[index].Backpack[itemSlot] = players[index].Chest;
                        players[index].Chest = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
                        HandleData.SendWeaponsUpdate(index);
                        HandleData.SendPlayerInv(index);
                        HandleData.SendPlayerEquipment(index);
                    }
                    else
                    {
                        HandleData.SendServerMessageTo(Connection,"Inventory is full!");
                        return;
                    }
                    break;

                case (int)EquipSlots.Legs:
                    itemSlot = players[index].FindOpenInvSlot(players[index].Backpack);
                    if (itemSlot < 25)
                    {
                        players[index].Backpack[itemSlot] = players[index].Legs;
                        players[index].Legs = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
                        HandleData.SendWeaponsUpdate(index);
                        HandleData.SendPlayerInv(index);
                        HandleData.SendPlayerEquipment(index);
                    }
                    else
                    {
                        HandleData.SendServerMessageTo(Connection,"Inventory is full!");
                        return;
                    }
                    break;

                case (int)EquipSlots.Feet:
                    itemSlot = players[index].FindOpenInvSlot(players[index].Backpack);
                    if (itemSlot < 25)
                    {
                        players[index].Backpack[itemSlot] = players[index].Feet;
                        players[index].Feet = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
                        HandleData.SendWeaponsUpdate(index);
                        HandleData.SendPlayerInv(index);
                        HandleData.SendPlayerEquipment(index);
                    }
                    else
                    {
                        HandleData.SendServerMessageTo(Connection,"Inventory is full!");
                        return;
                    }
                    break;
            }
        }

        public void DropItem(int index, int slot, int mapNum)
        {
            if (players[index].Backpack[slot].Name != "None")
            {
                int mapSlot = FindOpenMapItemSlot(maps[mapNum]);
                if (mapSlot < MAX_MAP_ITEMS)
                {
                    maps[mapNum].m_MapItem[mapSlot].Name = players[index].Backpack[slot].Name;
                    maps[mapNum].m_MapItem[mapSlot].X = players[index].X + OFFSET_X;
                    maps[mapNum].m_MapItem[mapSlot].Y = players[index].Y + OFFSET_Y;
                    maps[mapNum].m_MapItem[mapSlot].Sprite = players[index].Backpack[slot].Sprite;
                    maps[mapNum].m_MapItem[mapSlot].Damage = players[index].Backpack[slot].Damage;
                    maps[mapNum].m_MapItem[mapSlot].Armor = players[index].Backpack[slot].Armor;
                    maps[mapNum].m_MapItem[mapSlot].Type = players[index].Backpack[slot].Type;
                    maps[mapNum].m_MapItem[mapSlot].AttackSpeed = players[index].Backpack[slot].AttackSpeed;
                    maps[mapNum].m_MapItem[mapSlot].HealthRestore = players[index].Backpack[slot].HealthRestore;
                    maps[mapNum].m_MapItem[mapSlot].Strength = players[index].Backpack[slot].Strength;
                    maps[mapNum].m_MapItem[mapSlot].Agility = players[index].Backpack[slot].Agility;
                    maps[mapNum].m_MapItem[mapSlot].Endurance = players[index].Backpack[slot].Endurance;
                    maps[mapNum].m_MapItem[mapSlot].Stamina = players[index].Backpack[slot].Stamina;
                    maps[mapNum].m_MapItem[mapSlot].Value = players[index].Backpack[slot].Value;
                    maps[mapNum].m_MapItem[mapSlot].Price = players[index].Backpack[slot].Price;
                    maps[mapNum].m_MapItem[mapSlot].Rarity = players[index].Backpack[slot].Rarity;
                    maps[mapNum].m_MapItem[mapSlot].IsSpawned = true;
                    maps[mapNum].m_MapItem[mapSlot].ExpireTick = TickCount;

                    players[index].Backpack[slot] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
                    HandleData.SendPlayerInv(index);

                    for (int p = 0; p < MAX_PLAYERS; p++)
                    {
                        if (players[p].Connection != null && mapNum == players[p].Map)
                        {
                            HandleData.SendMapItemData(players[p].Connection, mapNum, mapSlot);
                        }
                    }
                }
                else
                {
                    HandleData.SendServerMessageTo(Connection, "All map item slots are filled!");
                }
            }
        }

        public void CheckPickup(int index, int map)
        {
            for (int c = 0; c < MAX_MAP_ITEMS; c++)
            {
                if (maps[map].m_MapItem[c] != null && maps[map].m_MapItem[c].IsSpawned)
                {
                    if ((X + OFFSET_X) == maps[map].m_MapItem[c].X && (Y + OFFSET_Y) == maps[map].m_MapItem[c].Y)
                    {
                        PickUpItem(map, index, c);
                        break;
                    }
                }
            }
        }

        void PickUpItem(int map, int index, int itemNum)
        {
            int itemSlot = FindOpenInvSlot(Backpack);

            if (itemSlot < MAX_INV_SLOTS)
            {
                Backpack[itemSlot].Name = maps[map].m_MapItem[itemNum].Name;
                Backpack[itemSlot].Sprite = maps[map].m_MapItem[itemNum].Sprite;
                Backpack[itemSlot].Damage = maps[map].m_MapItem[itemNum].Damage;
                Backpack[itemSlot].Armor = maps[map].m_MapItem[itemNum].Armor;
                Backpack[itemSlot].Type = maps[map].m_MapItem[itemNum].Type;
                Backpack[itemSlot].AttackSpeed = maps[map].m_MapItem[itemNum].AttackSpeed;
                Backpack[itemSlot].HealthRestore = maps[map].m_MapItem[itemNum].HealthRestore;
                Backpack[itemSlot].Strength = maps[map].m_MapItem[itemNum].Strength;
                Backpack[itemSlot].Agility = maps[map].m_MapItem[itemNum].Agility;
                Backpack[itemSlot].Endurance = maps[map].m_MapItem[itemNum].Endurance;
                Backpack[itemSlot].Stamina = maps[map].m_MapItem[itemNum].Stamina;
                Backpack[itemSlot].Value = maps[map].m_MapItem[itemNum].Value;
                Backpack[itemSlot].Price = maps[map].m_MapItem[itemNum].Price;
                Backpack[itemSlot].Rarity = maps[map].m_MapItem[itemNum].Rarity;

                int TileX = maps[map].m_MapItem[itemNum].X;
                int TileY = maps[map].m_MapItem[itemNum].Y;
                maps[map].Ground[TileX, TileY].NeedsSpawnedTick = TickCount;
                maps[map].m_MapItem[itemNum].Name = "None";
                maps[map].m_MapItem[itemNum].IsSpawned = false;

                for (int p = 0; p < MAX_PLAYERS; p++)
                {
                    if (players[p].Connection != null && Map == players[p].Map)
                    {
                        HandleData.SendMapItemData(players[p].Connection, map, itemNum);
                    }
                }
                HandleData.SendPlayerInv(index);
            }
            else
            {
                HandleData.SendServerMessageTo(Connection,"Inventory is full!");
                return;
            }
        }

        public int FindOpenInvSlot(Item[] s_Backpack)
        {
            for (int i = 0; i < MAX_INV_SLOTS; i++)
            {
                if (s_Backpack[i].Name == "None")
                {
                    return i;
                }
            }
            return MAX_INV_SLOTS;
        }

        public int FindOpenBankSlot(Item[] s_Bank)
        {
            for (int i = 0; i < MAX_BANK_SLOTS; i++)
            {
                if (s_Bank[i].Name == "None")
                {
                    return i;
                }
            }
            return MAX_BANK_SLOTS;
        }

        public int FindOpenQuestListSlot()
        {
            for (int i = 0; i < MAX_PLAYER_QUEST_LIST; i++)
            {
                if (QuestList[i] == 0)
                {
                    return i;
                }
            }
            return MAX_PLAYER_QUEST_LIST;
        }

        public bool CheckPlayerHasQuest(int questNum)
        {
            for (int i = 0; i < MAX_PLAYER_QUEST_LIST; i++)
            {
                if (QuestList[i] == questNum)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Database
        public void CreatePlayerInDatabase()
        {
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            string script = ReadAllText("SQL Data Scripts/Insert_Player.sql");
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = Name;
                    cmd.Parameters.Add(new SqlParameter("@password", System.Data.DbType.String)).Value = Pass;
                    cmd.Parameters.Add(new SqlParameter("@email", System.Data.DbType.String)).Value = EmailAddress;
                    cmd.Parameters.Add(new SqlParameter("@x", System.Data.DbType.Int32)).Value = X;
                    cmd.Parameters.Add(new SqlParameter("@y", System.Data.DbType.Int32)).Value = Y;
                    cmd.Parameters.Add(new SqlParameter("@map", System.Data.DbType.Int32)).Value = Map;
                    cmd.Parameters.Add(new SqlParameter("@direction", System.Data.DbType.Int32)).Value = Direction;
                    cmd.Parameters.Add(new SqlParameter("@aimdirection", System.Data.DbType.Int32)).Value = AimDirection;
                    cmd.Parameters.Add(new SqlParameter("@sprite", System.Data.DbType.Int32)).Value = Sprite;
                    cmd.Parameters.Add(new SqlParameter("@level", System.Data.DbType.Int32)).Value = Level;                    
                    cmd.Parameters.Add(new SqlParameter("@health", System.Data.DbType.Int32)).Value = Health;
                    cmd.Parameters.Add(new SqlParameter("@maxhealth", System.Data.DbType.Int32)).Value = MaxHealth;
                    cmd.Parameters.Add(new SqlParameter("@mana", System.Data.DbType.Int32)).Value = Mana;
                    cmd.Parameters.Add(new SqlParameter("@maxmana", System.Data.DbType.Int32)).Value = MaxMana;
                    cmd.Parameters.Add(new SqlParameter("@experience", System.Data.DbType.Int32)).Value = Experience;
                    cmd.Parameters.Add(new SqlParameter("@wallet", System.Data.DbType.Int32)).Value = Wallet;
                    cmd.Parameters.Add(new SqlParameter("@armor", System.Data.DbType.Int32)).Value = Armor;
                    cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = Strength;
                    cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = Agility;
                    cmd.Parameters.Add(new SqlParameter("@intelligence", System.Data.DbType.Int32)).Value = Intelligence;
                    cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = Stamina;
                    cmd.Parameters.Add(new SqlParameter("@energy", System.Data.DbType.Int32)).Value = Energy;
                    cmd.Parameters.Add(new SqlParameter("@lightradius", System.Data.DbType.Int32)).Value = LightRadius;
                    cmd.Parameters.Add(new SqlParameter("@lastlogged", System.Data.DbType.Int32)).Value = LastLoggedIn;
                    cmd.Parameters.Add(new SqlParameter("@accountkey", System.Data.DbType.String)).Value = AccountKey;
                    cmd.Parameters.Add(new SqlParameter("@active", System.Data.DbType.String)).Value = Active;
                    cmd.ExecuteNonQuery();
                }

                script = ReadAllText("SQL Data Scripts/Insert_Stats.sql");
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = Name;
                    cmd.Parameters.Add(new SqlParameter("@days", System.Data.DbType.Int32)).Value = PlayDays;
                    cmd.Parameters.Add(new SqlParameter("@hours", System.Data.DbType.Int32)).Value = PlayHours;
                    cmd.Parameters.Add(new SqlParameter("@minutes", System.Data.DbType.Int32)).Value = PlayMinutes;
                    cmd.Parameters.Add(new SqlParameter("@seconds", System.Data.DbType.Int32)).Value = PlaySeconds;
                    cmd.ExecuteNonQuery();
                }

                script = ReadAllText("SQL Data Scripts/Insert_Quest_List.sql");
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = Name;
                    cmd.Parameters.Add(new SqlParameter("@quest1", System.Data.DbType.Int32)).Value = QuestList[0];
                    cmd.Parameters.Add(new SqlParameter("@quest1status", System.Data.DbType.Int32)).Value = QuestStatus[0];
                    cmd.Parameters.Add(new SqlParameter("@quest2", System.Data.DbType.Int32)).Value = QuestList[1];
                    cmd.Parameters.Add(new SqlParameter("@quest2status", System.Data.DbType.Int32)).Value = QuestStatus[1];
                    cmd.Parameters.Add(new SqlParameter("@quest3", System.Data.DbType.Int32)).Value = QuestList[2];
                    cmd.Parameters.Add(new SqlParameter("@quest3status", System.Data.DbType.Int32)).Value = QuestStatus[2];
                    cmd.Parameters.Add(new SqlParameter("@quest4", System.Data.DbType.Int32)).Value = QuestList[3];
                    cmd.Parameters.Add(new SqlParameter("@quest4status", System.Data.DbType.Int32)).Value = QuestStatus[3];
                    cmd.Parameters.Add(new SqlParameter("@quest5", System.Data.DbType.Int32)).Value = QuestList[4];
                    cmd.Parameters.Add(new SqlParameter("@quest5status", System.Data.DbType.Int32)).Value = QuestStatus[4];
                    cmd.Parameters.Add(new SqlParameter("@quest6", System.Data.DbType.Int32)).Value = QuestList[5];
                    cmd.Parameters.Add(new SqlParameter("@quest6status", System.Data.DbType.Int32)).Value = QuestStatus[5];
                    cmd.Parameters.Add(new SqlParameter("@quest7", System.Data.DbType.Int32)).Value = QuestList[6];
                    cmd.Parameters.Add(new SqlParameter("@quest7status", System.Data.DbType.Int32)).Value = QuestStatus[6];
                    cmd.Parameters.Add(new SqlParameter("@quest8", System.Data.DbType.Int32)).Value = QuestList[7];
                    cmd.Parameters.Add(new SqlParameter("@quest8status", System.Data.DbType.Int32)).Value = QuestStatus[7];
                    cmd.Parameters.Add(new SqlParameter("@quest9", System.Data.DbType.Int32)).Value = QuestList[8];
                    cmd.Parameters.Add(new SqlParameter("@quest9status", System.Data.DbType.Int32)).Value = QuestStatus[8];
                    cmd.Parameters.Add(new SqlParameter("@quest10", System.Data.DbType.Int32)).Value = QuestList[9];
                    cmd.Parameters.Add(new SqlParameter("@quest10status", System.Data.DbType.Int32)).Value = QuestStatus[9];
                    cmd.ExecuteNonQuery();
                }

                script = ReadAllText("SQL Data Scripts/Insert_Main_Weapons.sql");
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                    cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = MainHand.Name;
                    cmd.Parameters.Add(new SqlParameter("@sprite", System.Data.DbType.Int32)).Value = MainHand.Sprite;
                    cmd.Parameters.Add(new SqlParameter("@damage", System.Data.DbType.Int32)).Value = MainHand.Damage;
                    cmd.Parameters.Add(new SqlParameter("@armor", System.Data.DbType.Int32)).Value = MainHand.Armor;
                    cmd.Parameters.Add(new SqlParameter("@type", System.Data.DbType.Int32)).Value = MainHand.Type;
                    cmd.Parameters.Add(new SqlParameter("@attackspeed", System.Data.DbType.Int32)).Value = MainHand.AttackSpeed;
                    cmd.Parameters.Add(new SqlParameter("@healthrestore", System.Data.DbType.Int32)).Value = MainHand.HealthRestore;
                    cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = MainHand.Strength;
                    cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = MainHand.Agility;
                    cmd.Parameters.Add(new SqlParameter("@endurance", System.Data.DbType.Int32)).Value = MainHand.Endurance;
                    cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = MainHand.Stamina;
                    cmd.Parameters.Add(new SqlParameter("@value", System.Data.DbType.Int32)).Value = MainHand.Value;
                    cmd.Parameters.Add(new SqlParameter("@price", System.Data.DbType.Int32)).Value = MainHand.Price;
                    cmd.Parameters.Add(new SqlParameter("@rarity", System.Data.DbType.Int32)).Value = MainHand.Rarity;
                    cmd.ExecuteNonQuery();
                }

                script = ReadAllText("SQL Data Scripts/Insert_Secondary_Weapons.sql");
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                    cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = OffHand.Name;
                    cmd.Parameters.Add(new SqlParameter("@sprite", System.Data.DbType.Int32)).Value = OffHand.Sprite;
                    cmd.Parameters.Add(new SqlParameter("@damage", System.Data.DbType.Int32)).Value = OffHand.Damage;
                    cmd.Parameters.Add(new SqlParameter("@armor", System.Data.DbType.Int32)).Value = OffHand.Armor;
                    cmd.Parameters.Add(new SqlParameter("@type", System.Data.DbType.Int32)).Value = OffHand.Type;
                    cmd.Parameters.Add(new SqlParameter("@attackspeed", System.Data.DbType.Int32)).Value = OffHand.AttackSpeed;
                    cmd.Parameters.Add(new SqlParameter("@healthrestore", System.Data.DbType.Int32)).Value = OffHand.HealthRestore;
                    cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = OffHand.Strength;
                    cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = OffHand.Agility;
                    cmd.Parameters.Add(new SqlParameter("@endurance", System.Data.DbType.Int32)).Value = OffHand.Endurance;
                    cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = OffHand.Stamina;
                    cmd.Parameters.Add(new SqlParameter("@value", System.Data.DbType.Int32)).Value = OffHand.Value;
                    cmd.Parameters.Add(new SqlParameter("@price", System.Data.DbType.Int32)).Value = OffHand.Price;
                    cmd.Parameters.Add(new SqlParameter("@rarity", System.Data.DbType.Int32)).Value = OffHand.Rarity;
                    cmd.ExecuteNonQuery();
                }

                script = ReadAllText("SQL Data Scripts/Insert_Equipment.sql");
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                    //cmd.Parameters.Add(new SqlParameter("@id", System.Data.DbType.Int32)).Value = Chest.Id;
                    cmd.Parameters.Add(new SqlParameter("@slot", System.Data.DbType.Int32)).Value = 0;
                    cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = Chest.Name;
                    cmd.Parameters.Add(new SqlParameter("@sprite", System.Data.DbType.Int32)).Value = Chest.Sprite;
                    cmd.Parameters.Add(new SqlParameter("@damage", System.Data.DbType.Int32)).Value = Chest.Damage;
                    cmd.Parameters.Add(new SqlParameter("@armor", System.Data.DbType.Int32)).Value = Chest.Armor;
                    cmd.Parameters.Add(new SqlParameter("@type", System.Data.DbType.Int32)).Value = Chest.Type;
                    cmd.Parameters.Add(new SqlParameter("@attackspeed", System.Data.DbType.Int32)).Value = Chest.AttackSpeed;
                    cmd.Parameters.Add(new SqlParameter("@healthrestore", System.Data.DbType.Int32)).Value = Chest.HealthRestore;
                    cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = Chest.Strength;
                    cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = Chest.Agility;
                    cmd.Parameters.Add(new SqlParameter("@endurance", System.Data.DbType.Int32)).Value = Chest.Endurance;
                    cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = Chest.Stamina;
                    cmd.Parameters.Add(new SqlParameter("@value", System.Data.DbType.Int32)).Value = Chest.Value;
                    cmd.Parameters.Add(new SqlParameter("@price", System.Data.DbType.Int32)).Value = Chest.Price;
                    cmd.Parameters.Add(new SqlParameter("@rarity", System.Data.DbType.Int32)).Value = Chest.Rarity;
                    cmd.ExecuteNonQuery();
                }

                script = ReadAllText("SQL Data Scripts/Insert_Equipment.sql");
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                    //cmd.Parameters.Add(new SqlParameter("@id", System.Data.DbType.Int32)).Value = Legs.Id;
                    cmd.Parameters.Add(new SqlParameter("@slot", System.Data.DbType.Int32)).Value = 1;
                    cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = Legs.Name;
                    cmd.Parameters.Add(new SqlParameter("@sprite", System.Data.DbType.Int32)).Value = Legs.Sprite;
                    cmd.Parameters.Add(new SqlParameter("@damage", System.Data.DbType.Int32)).Value = Legs.Damage;
                    cmd.Parameters.Add(new SqlParameter("@armor", System.Data.DbType.Int32)).Value = Legs.Armor;
                    cmd.Parameters.Add(new SqlParameter("@type", System.Data.DbType.Int32)).Value = Legs.Type;
                    cmd.Parameters.Add(new SqlParameter("@attackspeed", System.Data.DbType.Int32)).Value = Legs.AttackSpeed;
                    cmd.Parameters.Add(new SqlParameter("@healthrestore", System.Data.DbType.Int32)).Value = Legs.HealthRestore;
                    cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = Legs.Strength;
                    cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = Legs.Agility;
                    cmd.Parameters.Add(new SqlParameter("@endurance", System.Data.DbType.Int32)).Value = Legs.Endurance;
                    cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = Legs.Stamina;
                    cmd.Parameters.Add(new SqlParameter("@value", System.Data.DbType.Int32)).Value = Legs.Value;
                    cmd.Parameters.Add(new SqlParameter("@price", System.Data.DbType.Int32)).Value = Legs.Price;
                    cmd.Parameters.Add(new SqlParameter("@rarity", System.Data.DbType.Int32)).Value = Legs.Rarity;
                    cmd.ExecuteNonQuery();
                }

                script = ReadAllText("SQL Data Scripts/Insert_Equipment.sql");
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                    //cmd.Parameters.Add(new SqlParameter("@id", System.Data.DbType.Int32)).Value = Feet.Id;
                    cmd.Parameters.Add(new SqlParameter("@slot", System.Data.DbType.Int32)).Value = 2;
                    cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = Feet.Name;
                    cmd.Parameters.Add(new SqlParameter("@sprite", System.Data.DbType.Int32)).Value = Feet.Sprite;
                    cmd.Parameters.Add(new SqlParameter("@damage", System.Data.DbType.Int32)).Value = Feet.Damage;
                    cmd.Parameters.Add(new SqlParameter("@armor", System.Data.DbType.Int32)).Value = Feet.Armor;
                    cmd.Parameters.Add(new SqlParameter("@type", System.Data.DbType.Int32)).Value = Feet.Type;
                    cmd.Parameters.Add(new SqlParameter("@attackspeed", System.Data.DbType.Int32)).Value = Feet.AttackSpeed;
                    cmd.Parameters.Add(new SqlParameter("@healthrestore", System.Data.DbType.Int32)).Value = Feet.HealthRestore;
                    cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = Feet.Strength;
                    cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = Feet.Agility;
                    cmd.Parameters.Add(new SqlParameter("@endurance", System.Data.DbType.Int32)).Value = Feet.Endurance;
                    cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = Feet.Stamina;
                    cmd.Parameters.Add(new SqlParameter("@value", System.Data.DbType.Int32)).Value = Feet.Value;
                    cmd.Parameters.Add(new SqlParameter("@price", System.Data.DbType.Int32)).Value = Feet.Price;
                    cmd.Parameters.Add(new SqlParameter("@rarity", System.Data.DbType.Int32)).Value = Feet.Rarity;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateAccountStatusInDatabase()
        {
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                string command;
                command = "UPDATE Players SET Active = 'Y' WHERE ID = @id";
                using (var cmd = new SqlCommand(command, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("id", System.Data.SqlDbType.Int)).Value = Id;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void SavePlayerToDatabase()
        {
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            string script = ReadAllText("SQL Data Scripts/Save_Player.sql");
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("id", System.Data.SqlDbType.Int)).Value = Id;
                    cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = Name;
                    cmd.Parameters.Add(new SqlParameter("@password", System.Data.DbType.String)).Value = Pass;
                    cmd.Parameters.Add(new SqlParameter("@email", System.Data.DbType.String)).Value = EmailAddress;
                    cmd.Parameters.Add(new SqlParameter("@x", System.Data.DbType.Int32)).Value = X;
                    cmd.Parameters.Add(new SqlParameter("@y", System.Data.DbType.Int32)).Value = Y;
                    cmd.Parameters.Add(new SqlParameter("@map", System.Data.DbType.Int32)).Value = Map;
                    cmd.Parameters.Add(new SqlParameter("@direction", System.Data.DbType.Int32)).Value = Direction;
                    cmd.Parameters.Add(new SqlParameter("@aimdirection", System.Data.DbType.Int32)).Value = AimDirection;
                    cmd.Parameters.Add(new SqlParameter("@sprite", System.Data.DbType.Int32)).Value = Sprite;
                    cmd.Parameters.Add(new SqlParameter("@level", System.Data.DbType.Int32)).Value = Level;                    
                    cmd.Parameters.Add(new SqlParameter("@health", System.Data.DbType.Int32)).Value = Health;
                    cmd.Parameters.Add(new SqlParameter("@maxhealth", System.Data.DbType.Int32)).Value = MaxHealth;
                    cmd.Parameters.Add(new SqlParameter("@mana", System.Data.DbType.Int32)).Value = Mana;
                    cmd.Parameters.Add(new SqlParameter("@maxmana", System.Data.DbType.Int32)).Value = MaxMana;
                    cmd.Parameters.Add(new SqlParameter("@experience", System.Data.DbType.Int32)).Value = Experience;
                    cmd.Parameters.Add(new SqlParameter("@wallet", System.Data.DbType.Int32)).Value = Wallet;
                    cmd.Parameters.Add(new SqlParameter("@armor", System.Data.DbType.Int32)).Value = Armor;
                    cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = Strength;
                    cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = Agility;
                    cmd.Parameters.Add(new SqlParameter("@intelligence", System.Data.DbType.Int32)).Value = Intelligence;
                    cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = Stamina;
                    cmd.Parameters.Add(new SqlParameter("@energy", System.Data.DbType.Int32)).Value = Energy;
                    cmd.Parameters.Add(new SqlParameter("@lightradius", System.Data.DbType.Int32)).Value = LightRadius;
                    cmd.Parameters.Add(new SqlParameter("@lastlogged", System.Data.DbType.String)).Value = LastLoggedIn;
                    cmd.Parameters.Add(new SqlParameter("@accountkey", System.Data.DbType.String)).Value = AccountKey;
                    cmd.Parameters.Add(new SqlParameter("@active", System.Data.DbType.String)).Value = Active;
                    cmd.ExecuteNonQuery();
                }

                script = ReadAllText("SQL Data Scripts/Save_Stats.sql");
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("id", System.Data.SqlDbType.Int)).Value = StatsId;
                    cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = Name;
                    cmd.Parameters.Add(new SqlParameter("@days", System.Data.DbType.Int32)).Value = PlayDays;
                    cmd.Parameters.Add(new SqlParameter("@hours", System.Data.DbType.Int32)).Value = PlayHours;
                    cmd.Parameters.Add(new SqlParameter("@minutes", System.Data.DbType.Int32)).Value = PlayMinutes;
                    cmd.Parameters.Add(new SqlParameter("@seconds", System.Data.DbType.Int32)).Value = PlaySeconds;
                    cmd.ExecuteNonQuery();
                }

                script = ReadAllText("SQL Data Scripts/Save_Quest_List.sql");
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = Name;
                    cmd.Parameters.Add(new SqlParameter("@quest1", System.Data.DbType.Int32)).Value = QuestList[0];
                    cmd.Parameters.Add(new SqlParameter("@quest1status", System.Data.DbType.Int32)).Value = QuestStatus[0];
                    cmd.Parameters.Add(new SqlParameter("@quest2", System.Data.DbType.Int32)).Value = QuestList[1];
                    cmd.Parameters.Add(new SqlParameter("@quest2status", System.Data.DbType.Int32)).Value = QuestStatus[1];
                    cmd.Parameters.Add(new SqlParameter("@quest3", System.Data.DbType.Int32)).Value = QuestList[2];
                    cmd.Parameters.Add(new SqlParameter("@quest3status", System.Data.DbType.Int32)).Value = QuestStatus[2];
                    cmd.Parameters.Add(new SqlParameter("@quest4", System.Data.DbType.Int32)).Value = QuestList[3];
                    cmd.Parameters.Add(new SqlParameter("@quest4status", System.Data.DbType.Int32)).Value = QuestStatus[3];
                    cmd.Parameters.Add(new SqlParameter("@quest5", System.Data.DbType.Int32)).Value = QuestList[4];
                    cmd.Parameters.Add(new SqlParameter("@quest5status", System.Data.DbType.Int32)).Value = QuestStatus[4];
                    cmd.Parameters.Add(new SqlParameter("@quest6", System.Data.DbType.Int32)).Value = QuestList[5];
                    cmd.Parameters.Add(new SqlParameter("@quest6status", System.Data.DbType.Int32)).Value = QuestStatus[5];
                    cmd.Parameters.Add(new SqlParameter("@quest7", System.Data.DbType.Int32)).Value = QuestList[6];
                    cmd.Parameters.Add(new SqlParameter("@quest7status", System.Data.DbType.Int32)).Value = QuestStatus[6];
                    cmd.Parameters.Add(new SqlParameter("@quest8", System.Data.DbType.Int32)).Value = QuestList[7];
                    cmd.Parameters.Add(new SqlParameter("@quest8status", System.Data.DbType.Int32)).Value = QuestStatus[7];
                    cmd.Parameters.Add(new SqlParameter("@quest9", System.Data.DbType.Int32)).Value = QuestList[8];
                    cmd.Parameters.Add(new SqlParameter("@quest9status", System.Data.DbType.Int32)).Value = QuestStatus[8];
                    cmd.Parameters.Add(new SqlParameter("@quest10", System.Data.DbType.Int32)).Value = QuestList[9];
                    cmd.Parameters.Add(new SqlParameter("@quest10status", System.Data.DbType.Int32)).Value = QuestStatus[9];
                    cmd.ExecuteNonQuery();
                }

                script = ReadAllText("SQL Data Scripts/Save_Main_Weapons.sql");
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                    cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = MainHand.Name;
                    cmd.Parameters.Add(new SqlParameter("@sprite", System.Data.DbType.Int32)).Value = MainHand.Sprite;
                    cmd.Parameters.Add(new SqlParameter("@damage", System.Data.DbType.Int32)).Value = MainHand.Damage;
                    cmd.Parameters.Add(new SqlParameter("@armor", System.Data.DbType.Int32)).Value = MainHand.Armor;
                    cmd.Parameters.Add(new SqlParameter("@type", System.Data.DbType.Int32)).Value = MainHand.Type;
                    cmd.Parameters.Add(new SqlParameter("@attackspeed", System.Data.DbType.Int32)).Value = MainHand.AttackSpeed;
                    cmd.Parameters.Add(new SqlParameter("@healthrestore", System.Data.DbType.Int32)).Value = MainHand.HealthRestore;
                    cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = MainHand.Strength;
                    cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = MainHand.Agility;
                    cmd.Parameters.Add(new SqlParameter("@endurance", System.Data.DbType.Int32)).Value = MainHand.Endurance;
                    cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = MainHand.Stamina;
                    cmd.Parameters.Add(new SqlParameter("@value", System.Data.DbType.Int32)).Value = MainHand.Value;
                    cmd.Parameters.Add(new SqlParameter("@price", System.Data.DbType.Int32)).Value = MainHand.Price;
                    cmd.Parameters.Add(new SqlParameter("@rarity", System.Data.DbType.Int32)).Value = MainHand.Rarity;
                    cmd.ExecuteNonQuery();
                }

                script = ReadAllText("SQL Data Scripts/Save_Secondary_Weapons.sql");
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                    cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = OffHand.Name;
                    cmd.Parameters.Add(new SqlParameter("@sprite", System.Data.DbType.Int32)).Value = OffHand.Sprite;
                    cmd.Parameters.Add(new SqlParameter("@damage", System.Data.DbType.Int32)).Value = OffHand.Damage;
                    cmd.Parameters.Add(new SqlParameter("@armor", System.Data.DbType.Int32)).Value = OffHand.Armor;
                    cmd.Parameters.Add(new SqlParameter("@type", System.Data.DbType.Int32)).Value = OffHand.Type;
                    cmd.Parameters.Add(new SqlParameter("@attackspeed", System.Data.DbType.Int32)).Value = OffHand.AttackSpeed;
                    cmd.Parameters.Add(new SqlParameter("@healthrestore", System.Data.DbType.Int32)).Value = OffHand.HealthRestore;
                    cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = OffHand.Strength;
                    cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = OffHand.Agility;
                    cmd.Parameters.Add(new SqlParameter("@endurance", System.Data.DbType.Int32)).Value = OffHand.Endurance;
                    cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = OffHand.Stamina;
                    cmd.Parameters.Add(new SqlParameter("@value", System.Data.DbType.Int32)).Value = OffHand.Value;
                    cmd.Parameters.Add(new SqlParameter("@price", System.Data.DbType.Int32)).Value = OffHand.Price;
                    cmd.Parameters.Add(new SqlParameter("@rarity", System.Data.DbType.Int32)).Value = OffHand.Rarity;
                    cmd.ExecuteNonQuery();
                }

                script = ReadAllText("SQL Data Scripts/Save_Equipment.sql");
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                    cmd.Parameters.Add(new SqlParameter("@id", System.Data.DbType.Int32)).Value = Chest.Id;
                    cmd.Parameters.Add(new SqlParameter("@slot", System.Data.DbType.Int32)).Value = 0;
                    cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = Chest.Name;
                    cmd.Parameters.Add(new SqlParameter("@sprite", System.Data.DbType.Int32)).Value = Chest.Sprite;
                    cmd.Parameters.Add(new SqlParameter("@damage", System.Data.DbType.Int32)).Value = Chest.Damage;
                    cmd.Parameters.Add(new SqlParameter("@armor", System.Data.DbType.Int32)).Value = Chest.Armor;
                    cmd.Parameters.Add(new SqlParameter("@type", System.Data.DbType.Int32)).Value = Chest.Type;
                    cmd.Parameters.Add(new SqlParameter("@attackspeed", System.Data.DbType.Int32)).Value = Chest.AttackSpeed;
                    cmd.Parameters.Add(new SqlParameter("@healthrestore", System.Data.DbType.Int32)).Value = Chest.HealthRestore;
                    cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = Chest.Strength;
                    cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = Chest.Agility;
                    cmd.Parameters.Add(new SqlParameter("@endurance", System.Data.DbType.Int32)).Value = Chest.Endurance;
                    cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = Chest.Stamina;
                    cmd.Parameters.Add(new SqlParameter("@value", System.Data.DbType.Int32)).Value = Chest.Value;
                    cmd.Parameters.Add(new SqlParameter("@price", System.Data.DbType.Int32)).Value = Chest.Price;
                    cmd.Parameters.Add(new SqlParameter("@rarity", System.Data.DbType.Int32)).Value = Chest.Rarity;
                    cmd.ExecuteNonQuery();
                }

                script = ReadAllText("SQL Data Scripts/Save_Equipment.sql");
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                    cmd.Parameters.Add(new SqlParameter("@id", System.Data.DbType.Int32)).Value = Legs.Id;
                    cmd.Parameters.Add(new SqlParameter("@slot", System.Data.DbType.Int32)).Value = 1;
                    cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = Legs.Name;
                    cmd.Parameters.Add(new SqlParameter("@sprite", System.Data.DbType.Int32)).Value = Legs.Sprite;
                    cmd.Parameters.Add(new SqlParameter("@damage", System.Data.DbType.Int32)).Value = Legs.Damage;
                    cmd.Parameters.Add(new SqlParameter("@armor", System.Data.DbType.Int32)).Value = Legs.Armor;
                    cmd.Parameters.Add(new SqlParameter("@type", System.Data.DbType.Int32)).Value = Legs.Type;
                    cmd.Parameters.Add(new SqlParameter("@attackspeed", System.Data.DbType.Int32)).Value = Legs.AttackSpeed;
                    cmd.Parameters.Add(new SqlParameter("@healthrestore", System.Data.DbType.Int32)).Value = Legs.HealthRestore;
                    cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = Legs.Strength;
                    cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = Legs.Agility;
                    cmd.Parameters.Add(new SqlParameter("@endurance", System.Data.DbType.Int32)).Value = Legs.Endurance;
                    cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = Legs.Stamina;
                    cmd.Parameters.Add(new SqlParameter("@value", System.Data.DbType.Int32)).Value = Legs.Value;
                    cmd.Parameters.Add(new SqlParameter("@price", System.Data.DbType.Int32)).Value = Legs.Price;
                    cmd.Parameters.Add(new SqlParameter("@rarity", System.Data.DbType.Int32)).Value = Legs.Rarity;
                    cmd.ExecuteNonQuery();
                }

                script = ReadAllText("SQL Data Scripts/Save_Equipment.sql");
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                    cmd.Parameters.Add(new SqlParameter("@id", System.Data.DbType.Int32)).Value = Feet.Id;
                    cmd.Parameters.Add(new SqlParameter("@slot", System.Data.DbType.Int32)).Value = 2;
                    cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = Feet.Name;
                    cmd.Parameters.Add(new SqlParameter("@sprite", System.Data.DbType.Int32)).Value = Feet.Sprite;
                    cmd.Parameters.Add(new SqlParameter("@damage", System.Data.DbType.Int32)).Value = Feet.Damage;
                    cmd.Parameters.Add(new SqlParameter("@armor", System.Data.DbType.Int32)).Value = Feet.Armor;
                    cmd.Parameters.Add(new SqlParameter("@type", System.Data.DbType.Int32)).Value = Feet.Type;
                    cmd.Parameters.Add(new SqlParameter("@attackspeed", System.Data.DbType.Int32)).Value = Feet.AttackSpeed;
                    cmd.Parameters.Add(new SqlParameter("@healthrestore", System.Data.DbType.Int32)).Value = Feet.HealthRestore;
                    cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = Feet.Strength;
                    cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = Feet.Agility;
                    cmd.Parameters.Add(new SqlParameter("@endurance", System.Data.DbType.Int32)).Value = Feet.Endurance;
                    cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = Feet.Stamina;
                    cmd.Parameters.Add(new SqlParameter("@value", System.Data.DbType.Int32)).Value = Feet.Value;
                    cmd.Parameters.Add(new SqlParameter("@price", System.Data.DbType.Int32)).Value = Feet.Price;
                    cmd.Parameters.Add(new SqlParameter("@rarity", System.Data.DbType.Int32)).Value = Feet.Rarity;
                    cmd.ExecuteNonQuery();
                }

                script = "DELETE FROM Inventory WHERE Owner=@owner;";
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                    cmd.ExecuteNonQuery();
                }

                int n = 0;
                for (int i = 0; i < MAX_INV_SLOTS; i++)
                {
                    if (Backpack[i].Name != "None")
                    {
                        script = ReadAllText("SQL Data Scripts/Save_Inventory.sql");
                        using (var cmd = new SqlCommand(script, sql))
                        {
                            cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                            cmd.Parameters.Add(new SqlParameter("@slot", System.Data.DbType.Int32)).Value = n;
                            cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = Backpack[i].Name;
                            cmd.Parameters.Add(new SqlParameter("@sprite", System.Data.DbType.Int32)).Value = Backpack[i].Sprite;
                            cmd.Parameters.Add(new SqlParameter("@damage", System.Data.DbType.Int32)).Value = Backpack[i].Damage;
                            cmd.Parameters.Add(new SqlParameter("@armor", System.Data.DbType.Int32)).Value = Backpack[i].Armor;
                            cmd.Parameters.Add(new SqlParameter("@type", System.Data.DbType.Int32)).Value = Backpack[i].Type;
                            cmd.Parameters.Add(new SqlParameter("@attackspeed", System.Data.DbType.Int32)).Value = Backpack[i].AttackSpeed;
                            cmd.Parameters.Add(new SqlParameter("@healthrestore", System.Data.DbType.Int32)).Value = Backpack[i].HealthRestore;
                            cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = Backpack[i].Strength;
                            cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = Backpack[i].Agility;
                            cmd.Parameters.Add(new SqlParameter("@endurance", System.Data.DbType.Int32)).Value = Backpack[i].Endurance;
                            cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = Backpack[i].Stamina;
                            cmd.Parameters.Add(new SqlParameter("@value", System.Data.DbType.Int32)).Value = Backpack[i].Value;
                            cmd.Parameters.Add(new SqlParameter("@price", System.Data.DbType.Int32)).Value = Backpack[i].Price;
                            cmd.Parameters.Add(new SqlParameter("@rarity", System.Data.DbType.Int32)).Value = Backpack[i].Rarity;
                            cmd.ExecuteNonQuery();
                        }
                        n = n + 1;
                    }
                }

                script = "DELETE FROM Bank WHERE Owner=@owner";
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                    cmd.ExecuteNonQuery();
                }
                    
                int m = 0;
                for (int i = 0; i < MAX_BANK_SLOTS; i++)
                {
                    if (Bank[i].Name != "None")
                    {
                        script = ReadAllText("SQL Data Scripts/Save_Bank.sql");
                        using (var cmd = new SqlCommand(script, sql))
                        {
                            cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                            cmd.Parameters.Add(new SqlParameter("@slot", System.Data.DbType.Int32)).Value = m;
                            cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = Bank[i].Name;
                            cmd.Parameters.Add(new SqlParameter("@sprite", System.Data.DbType.Int32)).Value = Bank[i].Sprite;
                            cmd.Parameters.Add(new SqlParameter("@damage", System.Data.DbType.Int32)).Value = Bank[i].Damage;
                            cmd.Parameters.Add(new SqlParameter("@armor", System.Data.DbType.Int32)).Value = Bank[i].Armor;
                            cmd.Parameters.Add(new SqlParameter("@type", System.Data.DbType.Int32)).Value = Bank[i].Type;
                            cmd.Parameters.Add(new SqlParameter("@attackspeed", System.Data.DbType.Int32)).Value = Bank[i].AttackSpeed;
                            cmd.Parameters.Add(new SqlParameter("@healthrestore", System.Data.DbType.Int32)).Value = Bank[i].HealthRestore;
                            cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = Bank[i].Strength;
                            cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = Bank[i].Agility;
                            cmd.Parameters.Add(new SqlParameter("@endurance", System.Data.DbType.Int32)).Value = Bank[i].Endurance;
                            cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = Bank[i].Stamina;
                            cmd.Parameters.Add(new SqlParameter("@value", System.Data.DbType.Int32)).Value = Bank[i].Value;
                            cmd.Parameters.Add(new SqlParameter("@price", System.Data.DbType.Int32)).Value = Bank[i].Price;
                            cmd.Parameters.Add(new SqlParameter("@rarity", System.Data.DbType.Int32)).Value = Bank[i].Rarity;
                            cmd.ExecuteNonQuery();
                        }
                        m = m + 1;
                    }
                }                    
            }
        }

        public void LoadPlayerFromDatabase()
        {
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            string script = ReadAllText("SQL Data Scripts/Load_Player.sql");
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                int result;
                int i;
                int n;
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = Name;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            i = 0;
                            Id = ToInt32(reader[i]); i += 1;
                            Name = reader[i].ToString(); i += 1;
                            Pass = reader[i].ToString(); i += 1;
                            EmailAddress = reader[i].ToString(); i += 1;
                            X = ToInt32(reader[i]); i += 1;
                            Y = ToInt32(reader[i]); i += 1;
                            Map = ToInt32(reader[i]); i += 1;
                            Direction = ToInt32(reader[i]); i += 1;
                            AimDirection = ToInt32(reader[i]); i += 1;
                            Sprite = ToInt32(reader[i]); i += 1;
                            Level = ToInt32(reader[i]); i += 1;
                            Health = ToInt32(reader[i]); i += 1;
                            MaxHealth = ToInt32(reader[i]); i += 1;
                            Mana = ToInt32(reader[i]); i += 1;
                            MaxMana = ToInt32(reader[i]); i += 1;
                            Experience = ToInt32(reader[i]); i += 1;
                            Wallet = ToInt32(reader[i]); i += 1;
                            Armor = ToInt32(reader[i]); i += 1;
                            Strength = ToInt32(reader[i]); i += 1;
                            Agility = ToInt32(reader[i]); i += 1;
                            Intelligence = ToInt32(reader[i]); i += 1;
                            Stamina = ToInt32(reader[i]); i += 1;
                            Energy = ToInt32(reader[i]); i += 1;
                            LightRadius = ToInt32(reader[i]); i += 1;
                            LastLoggedIn = reader[i].ToString(); i += 1;
                            AccountKey = reader[i].ToString(); i += 1;
                            Active = reader[i].ToString();
                        }
                    }
                }

                script = ReadAllText("SQL Data Scripts/Load_Stats.sql");
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = Name;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            i = 0;
                            StatsId = ToInt32(reader[i]); i = 2;
                            PlayDays = ToInt32(reader[i]); i += 1;
                            PlayHours = ToInt32(reader[i]); i += 1;
                            PlayMinutes = ToInt32(reader[i]); i += 1;
                            PlaySeconds = ToInt32(reader[i]); i += 1;
                        }
                    }
                }

                script = ReadAllText("SQL Data Scripts/Load_Quest_List.sql");
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = Name;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            QuestList[0] = ToInt32(reader[2]);
                            QuestStatus[0] = ToInt32(reader[3]);
                            QuestList[1] = ToInt32(reader[4]);
                            QuestStatus[1] = ToInt32(reader[5]);
                            QuestList[2] = ToInt32(reader[6]);
                            QuestStatus[2] = ToInt32(reader[7]);
                            QuestList[3] = ToInt32(reader[8]);
                            QuestStatus[3] = ToInt32(reader[9]);
                            QuestList[4] = ToInt32(reader[10]);
                            QuestStatus[4] = ToInt32(reader[11]);
                            QuestList[5] = ToInt32(reader[12]);
                            QuestStatus[5] = ToInt32(reader[13]);
                            QuestList[6] = ToInt32(reader[14]);
                            QuestStatus[6] = ToInt32(reader[15]);
                            QuestList[7] = ToInt32(reader[16]);
                            QuestStatus[7] = ToInt32(reader[17]);
                            QuestList[8] = ToInt32(reader[18]);
                            QuestStatus[8] = ToInt32(reader[19]);
                            QuestList[9] = ToInt32(reader[20]);
                            QuestStatus[9] = ToInt32(reader[21]);
                        }
                    }
                }

                script = ReadAllText("SQL Data Scripts/Load_Main_Weapons.sql");
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            i = 0;
                            MainHand.Id = ToInt32(reader[i]); i = 2;
                            MainHand.Name = reader[i].ToString(); i += 1;
                            MainHand.Sprite = ToInt32(reader[i]); i += 1;
                            MainHand.Damage = ToInt32(reader[i]); i += 1;
                            MainHand.Armor = ToInt32(reader[i]); i += 1;
                            MainHand.Type = ToInt32(reader[i]); i += 1;
                            MainHand.AttackSpeed = ToInt32(reader[i]); i += 1;
                            MainHand.HealthRestore = ToInt32(reader[i]); i += 1;
                            MainHand.Strength = ToInt32(reader[i]); i += 1;
                            MainHand.Agility = ToInt32(reader[i]); i += 1;
                            MainHand.Endurance = ToInt32(reader[i]); i += 1;
                            MainHand.Stamina = ToInt32(reader[i]); i += 1;
                            MainHand.Value = ToInt32(reader[i]); i += 1;
                            MainHand.Price = ToInt32(reader[i]); i += 1;
                            MainHand.Rarity = ToInt32(reader[i]);
                        }
                    }
                }

                script = ReadAllText("SQL Data Scripts/Load_Secondary_Weapons.sql");
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            i = 0;
                            OffHand.Id = ToInt32(reader[i]); i = 2;
                            OffHand.Name = reader[i].ToString(); i += 1;
                            OffHand.Sprite = ToInt32(reader[i]); i += 1;
                            OffHand.Damage = ToInt32(reader[i]); i += 1;
                            OffHand.Armor = ToInt32(reader[i]); i += 1;
                            OffHand.Type = ToInt32(reader[i]); i += 1;
                            OffHand.AttackSpeed = ToInt32(reader[i]); i += 1;
                            OffHand.HealthRestore = ToInt32(reader[i]); i += 1;
                            OffHand.Strength = ToInt32(reader[i]); i += 1;
                            OffHand.Agility = ToInt32(reader[i]); i += 1;
                            OffHand.Endurance = ToInt32(reader[i]); i += 1;
                            OffHand.Stamina = ToInt32(reader[i]); i += 1;
                            OffHand.Value = ToInt32(reader[i]); i += 1;
                            OffHand.Price = ToInt32(reader[i]); i += 1;
                            OffHand.Rarity = ToInt32(reader[i]);
                        }
                    }
                }

                script = ReadAllText("SQL Data Scripts/Load_Equipment.sql");
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                    cmd.Parameters.Add(new SqlParameter("@slot", System.Data.DbType.Int32)).Value = 0;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            i = 0;
                            Chest.Id = ToInt32(reader[i]); i = 3;
                            Chest.Name = reader[i].ToString(); i += 1;
                            Chest.Sprite = ToInt32(reader[i]); i += 1;
                            Chest.Damage = ToInt32(reader[i]); i += 1;
                            Chest.Armor = ToInt32(reader[i]); i += 1;
                            Chest.Type = ToInt32(reader[i]); i += 1;
                            Chest.AttackSpeed = ToInt32(reader[i]); i += 1;
                            Chest.HealthRestore = ToInt32(reader[i]); i += 1;
                            Chest.Strength = ToInt32(reader[i]); i += 1;
                            Chest.Agility = ToInt32(reader[i]); i += 1;
                            Chest.Endurance = ToInt32(reader[i]); i += 1;
                            Chest.Stamina = ToInt32(reader[i]); i += 1;
                            Chest.Value = ToInt32(reader[i]); i += 1;
                            Chest.Price = ToInt32(reader[i]); i += 1;
                            Chest.Rarity = ToInt32(reader[i]);
                        }
                    }
                }

                script = ReadAllText("SQL Data Scripts/Load_Equipment.sql");
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                    cmd.Parameters.Add(new SqlParameter("@slot", System.Data.DbType.Int32)).Value = 1;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            i = 0;
                            Legs.Id = ToInt32(reader[i]); i = 3;
                            Legs.Name = reader[i].ToString(); i += 1;
                            Legs.Sprite = ToInt32(reader[i]); i += 1;
                            Legs.Damage = ToInt32(reader[i]); i += 1;
                            Legs.Armor = ToInt32(reader[i]); i += 1;
                            Legs.Type = ToInt32(reader[i]); i += 1;
                            Legs.AttackSpeed = ToInt32(reader[i]); i += 1;
                            Legs.HealthRestore = ToInt32(reader[i]); i += 1;
                            Legs.Strength = ToInt32(reader[i]); i += 1;
                            Legs.Agility = ToInt32(reader[i]); i += 1;
                            Legs.Endurance = ToInt32(reader[i]); i += 1;
                            Legs.Stamina = ToInt32(reader[i]); i += 1;
                            Legs.Value = ToInt32(reader[i]); i += 1;
                            Legs.Price = ToInt32(reader[i]); i += 1;
                            Legs.Rarity = ToInt32(reader[i]);
                        }
                    }
                }

                script = ReadAllText("SQL Data Scripts/Load_Equipment.sql");
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                    cmd.Parameters.Add(new SqlParameter("@slot", System.Data.DbType.Int32)).Value = 2;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            i = 0;
                            Feet.Id = ToInt32(reader[i]); i = 3;
                            Feet.Name = reader[i].ToString(); i += 1;
                            Feet.Sprite = ToInt32(reader[i]); i += 1;
                            Feet.Damage = ToInt32(reader[i]); i += 1;
                            Feet.Armor = ToInt32(reader[i]); i += 1;
                            Feet.Type = ToInt32(reader[i]); i += 1;
                            Feet.AttackSpeed = ToInt32(reader[i]); i += 1;
                            Feet.HealthRestore = ToInt32(reader[i]); i += 1;
                            Feet.Strength = ToInt32(reader[i]); i += 1;
                            Feet.Agility = ToInt32(reader[i]); i += 1;
                            Feet.Endurance = ToInt32(reader[i]); i += 1;
                            Feet.Stamina = ToInt32(reader[i]); i += 1;
                            Feet.Value = ToInt32(reader[i]); i += 1;
                            Feet.Price = ToInt32(reader[i]); i += 1;
                            Feet.Rarity = ToInt32(reader[i]);
                        }
                    }
                }

                script = ReadAllText("SQL Data Scripts/Inventory_Count.sql");
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                    object queue = cmd.ExecuteScalar();
                    result = ToInt32(queue);
                }

                if (result > 0)
                {
                    for (i = 0; i < result; i++)
                    {
                        script = ReadAllText("SQL Data Scripts/Load_Inventory.sql");
                        using (var cmd = new SqlCommand(script, sql))
                        {
                            cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                            cmd.Parameters.Add(new SqlParameter("@slot", System.Data.DbType.Int32)).Value = i;

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    n = 0;
                                    Backpack[i].Id = ToInt32(reader[n]); n = 3;
                                    Backpack[i].Name = reader[n].ToString(); n += 1;
                                    Backpack[i].Sprite = ToInt32(reader[n]); n += 1;
                                    Backpack[i].Damage = ToInt32(reader[n]); n += 1;
                                    Backpack[i].Armor = ToInt32(reader[n]); n += 1;
                                    Backpack[i].Type = ToInt32(reader[n]); n += 1;
                                    Backpack[i].AttackSpeed = ToInt32(reader[n]); n += 1;                                    
                                    Backpack[i].HealthRestore = ToInt32(reader[n]); n += 1;
                                    Backpack[i].Strength = ToInt32(reader[n]); n += 1;
                                    Backpack[i].Agility = ToInt32(reader[n]); n += 1;
                                    Backpack[i].Endurance = ToInt32(reader[n]); n += 1;
                                    Backpack[i].Stamina = ToInt32(reader[n]); n += 1;
                                    Backpack[i].Value = ToInt32(reader[n]); n += 1;
                                    Backpack[i].Price = ToInt32(reader[n]); n += 1;
                                    Backpack[i].Rarity = ToInt32(reader[n]);
                                }
                            }
                        }
                    }
                }

                script = ReadAllText("SQL Data Scripts/Bank_Count.sql");
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                    object queue = cmd.ExecuteScalar();
                    result = ToInt32(queue);
                }

                if (result > 0)
                {
                    for (i = 0; i < result; i++)
                    {
                        script = ReadAllText("SQL Data Scripts/Load_Bank.sql");
                        using (var cmd = new SqlCommand(script, sql))
                        {
                            cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                            cmd.Parameters.Add(new SqlParameter("@slot", System.Data.DbType.Int32)).Value = i;

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    n = 0;
                                    Bank[i].Id = ToInt32(reader[n]); n = 3;
                                    Bank[i].Name = reader[n].ToString(); n += 1;
                                    Bank[i].Sprite = ToInt32(reader[n]); n += 1;
                                    Bank[i].Damage = ToInt32(reader[n]); n += 1;
                                    Bank[i].Armor = ToInt32(reader[n]); n += 1;
                                    Bank[i].Type = ToInt32(reader[n]); n += 1;
                                    Bank[i].AttackSpeed = ToInt32(reader[n]); n += 1;
                                    Bank[i].HealthRestore = ToInt32(reader[n]); n += 1;
                                    Bank[i].Strength = ToInt32(reader[n]); n += 1;
                                    Bank[i].Agility = ToInt32(reader[n]); n += 1;
                                    Bank[i].Endurance = ToInt32(reader[n]); n += 1;
                                    Bank[i].Stamina = ToInt32(reader[n]); n += 1;
                                    Bank[i].Value = ToInt32(reader[n]); n += 1;
                                    Bank[i].Price = ToInt32(reader[n]); n += 1;
                                    Bank[i].Rarity = ToInt32(reader[n]);
                                }
                            }
                        }
                    }
                }
            }
        }

        public void LoadPlayerNameFromDatabase(int id)
        {
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                string command;
                command = "SELECT Name FROM Players WHERE ID=@id";
                using (var cmd = new SqlCommand(command, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@id", System.Data.DbType.Int32)).Value = id;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Name = reader[0].ToString();
                        }
                    }
                }
            }
        }

        public void LoadPlayerIDFromDatabase(string name)
        {
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                string command;
                command = "SELECT ID FROM Players WHERE Name=@name";
                using (var cmd = new SqlCommand(command, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = name;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Id = ToInt32(reader[0]);
                        }
                    }
                }
            }
        }

        public bool IsAccountActive()
        {
            string result;
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                string command;
                command = "SELECT Active FROM Players WHERE ID = @id";
                using (var cmd = new SqlCommand(command, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("id", System.Data.SqlDbType.Int)).Value = Id;
                    result = cmd.ExecuteScalar().ToString();
                }
            }

            if (result == "Y") { return true; }
            else { return false; }
        }
        #endregion
    }

    public enum EquipSlots : int
    {
        MainHand,
        OffHand,
        Chest,
        Legs,
        Feet
    }

    public enum Directions : int
    {
        Down,
        Left,
        Right,
        Up,
        DownLeft,
        DownRight,
        UpLeft,
        UpRight
    }
}
