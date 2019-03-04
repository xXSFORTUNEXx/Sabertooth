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
        public Item mainWeapon = new Item();
        public Item offWeapon = new Item();
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
        public int Points { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Hunger { get; set; }
        public int Hydration { get; set; }
        public int Experience { get; set; }
        public int Money { get; set; }
        public int Armor { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Endurance { get; set; }
        public int Stamina { get; set; }
        public int Step { get; set; }        
        public int PistolAmmo { get; set; }
        public int AssaultAmmo { get; set; }
        public int RocketAmmo { get; set; }
        public int GrenadeAmmo { get; set; }
        public int LightRadius { get; set; }
        public int PlayDays { get; set; }
        public int PlayHours { get; set; }
        public int PlayMinutes { get; set; }
        public int PlaySeconds { get; set; }
        public int LifeDay { get; set; }
        public int LifeHour { get; set; }
        public int LifeMinute { get; set; }
        public int LifeSecond { get; set; }
        public int LongestLifeDay { get; set; }
        public int LongestLifeHour { get; set; }
        public int LongestLifeMinute { get; set; }
        public int LongestLifeSecond { get; set; }
        public string LastLoggedIn { get; set; }
        public string AccountKey { get; set; }
        public string Active { get; set; }
        public int Kills { get; set; }
        public int StatsId { get; set; }
        #endregion

        #region Local Variables
        public int hungerTick;
        public int hydrationTick;
        public int timeTick;
        //Instant Variables
        public int iPoints;
        public int iKills;
        #endregion

        #region Class Constructors
        public Player(string name, string pass, string email, int x, int y, int direction, int aimdirection, int map, int level, int points, int health, int maxhealth, int exp, int money, 
                      int armor, int hunger, int hydration, int str, int agi, int end, int sta, int defaultAmmo, NetConnection conn)
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
            Points = points;
            Experience = exp;
            Money = money;
            Armor = armor;
            Hunger = hunger;
            Hydration = hydration;
            Strength = str;
            Agility = agi;
            Endurance = end;
            Stamina = sta;
            Connection = conn;
            MaxHealth = maxhealth;
            Health = MaxHealth;
            hungerTick = TickCount;
            hydrationTick = TickCount;
            timeTick = TickCount;
            PistolAmmo = defaultAmmo;
            AssaultAmmo = defaultAmmo;
            RocketAmmo = 5;
            GrenadeAmmo = 3;
            LightRadius = 100;
            PlayDays = 0;
            PlayHours = 0;
            PlayMinutes = 0;
            PlaySeconds = 0;
            LifeDay = 0;
            LifeHour = 0;
            LifeMinute = 0;
            LifeSecond = 0;
            LongestLifeDay = 0;
            LongestLifeHour = 0;
            LongestLifeMinute = 0;
            LongestLifeSecond = 0;
            LastLoggedIn = "1/1/1000 00:00:00.000";
            AccountKey = KeyGen.Key(25);
            Active = "N";
            Kills = 0;

            mainWeapon = new Item("Pistol", 1, 30, 0, (int)ItemType.RangedWeapon, 700, 1500, 0, 0, 0, 0, 0, 0, 0, 8, 8, (int)AmmoType.Pistol, 1, 1, 1, 0);
            offWeapon = new Item("Club", 3, 40, 0, (int)ItemType.MeleeWeapon, 900, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, (int)ItemType.None, 1, 1, 1, 0);
            Chest = new Item("Starter Shirt", 4, 0, 5, (int)ItemType.Shirt, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
            Legs = new Item("Starter Pants", 5, 0, 5, (int)ItemType.Pants, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
            Feet = new Item("Starter Shoes", 6, 0, 5, (int)ItemType.Shoes, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);

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

        public Player(string name, string pass, string email, int x, int y, int direction, int aimdirection, int map, int level, int points, int health, int maxhealth, int exp, int money,
                      int armor, int hunger, int hydration, int str, int agi, int end, int sta, int defaultAmmo)
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
            Points = points;
            Experience = exp;
            Money = money;
            Armor = armor;
            Hunger = hunger;
            Hydration = hydration;
            hungerTick = TickCount;
            hydrationTick = TickCount;
            timeTick = TickCount;
            Strength = str;
            Agility = agi;
            Endurance = end;
            Stamina = sta;
            MaxHealth = maxhealth;
            Health = MaxHealth;
            PistolAmmo = defaultAmmo;
            AssaultAmmo = defaultAmmo;
            RocketAmmo = 5;
            GrenadeAmmo = 3;
            LightRadius = 100;
            PlayDays = 0;
            PlayHours = 0;
            PlayMinutes = 0;
            PlaySeconds = 0;
            LifeDay = 0;
            LifeHour = 0;
            LifeMinute = 0;
            LifeSecond = 0;
            LongestLifeDay = 0;
            LongestLifeHour = 0;
            LongestLifeMinute = 0;
            LongestLifeSecond = 0;
            LastLoggedIn = "1/1/1000 00:00:00.000";
            AccountKey = KeyGen.Key(25);
            Active = "N";
            Kills = 0;

            mainWeapon = new Item("Pistol", 1, 30, 0, (int)ItemType.RangedWeapon, 700, 1500, 0, 0, 0, 0, 0, 0, 0, 8, 8, (int)AmmoType.Pistol, 1, 1, 1, 0);
            offWeapon = new Item("Club", 3, 40, 0, (int)ItemType.MeleeWeapon, 900, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, (int)ItemType.None, 1, 1, 1, 0);
            Chest = new Item("Starter Shirt", 4, 0, 5, (int)ItemType.Shirt, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
            Legs = new Item("Starter Pants", 5, 0, 5, (int)ItemType.Pants, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
            Feet = new Item("Starter Shoes", 6, 0, 5, (int)ItemType.Shoes, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);

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
            hungerTick = TickCount;
            hydrationTick = TickCount;
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
            string msg;

            if (Hydration == 0)
            {
                Health -= 50;
                msg = "You are dying from dehydration!";
                HandleData.SendServerMessageTo(Connection, msg);
                return;
            }

            if (Hunger == 0)
            {
                Health -= 100;
                msg = "You are dying from starvation!";
                HandleData.SendServerMessageTo(Connection, msg);
                return;
            }

            if (Health < MaxHealth)
            {
                Health += (Stamina * 10);
            }

            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }
        }

        public void VitalLoss(string vital)
        {
            if (vital == "food")
            {
                if (Hunger <= 0)
                {
                    Hunger = 0;
                    Console.WriteLine("We start to die...");
                }
                else
                {
                    Hunger -= 10;
                }
            }

            if (vital == "water")
            {
                if (Hydration <= 0)
                {
                    Hydration = 0;
                    Console.WriteLine("We start to die...");
                }
                else
                {
                    Hydration -= 10;
                }
            }
        }

        public void CheckPlayerLevelUp()
        {
            int exptoLevel = (Level * 1000);
            if (Level == 1000) { Experience = exptoLevel; return; }
            if (Experience >= exptoLevel)
            {
                while (Experience >= exptoLevel)
                {
                    int remainingXp = (Experience - exptoLevel);
                    Level += 1;
                    Experience = remainingXp;
                    Hunger = 100;
                    MaxHealth += (Level * 5) + RND.Next(0, 100);
                    Health = MaxHealth;
                    Strength += RND.Next(1, 3);
                    Agility += RND.Next(1, 3);
                    Endurance += RND.Next(1, 3);
                    Stamina += RND.Next(1, 3);
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
                        if (players[index].mainWeapon.Name == "None")
                        {
                            players[index].mainWeapon = players[index].Backpack[slot];
                        }
                        else
                        {
                            int newSlot = FindOpenInvSlot(Backpack);

                            if (newSlot < 25)
                            {
                                players[index].Backpack[newSlot] = players[index].mainWeapon;
                                players[index].mainWeapon = players[index].Backpack[slot];
                            }
                        }
                        break;

                    case (int)ItemType.MeleeWeapon:
                        if (players[index].offWeapon.Name == "None")
                        {
                            players[index].offWeapon = players[index].Backpack[slot];
                        }
                        else
                        {
                            int newSlot = FindOpenInvSlot(Backpack);

                            if (newSlot < 25)
                            {
                                players[index].Backpack[newSlot] = players[index].offWeapon;
                                players[index].offWeapon = players[index].Backpack[slot];
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
                            players[index].Money += players[index].Backpack[slot].Value;
                        }                        
                        break;

                    case (int)ItemType.Food:
                        if (players[index].Backpack[slot].HungerRestore > 0)
                        {
                            players[index].Hunger += players[index].Backpack[slot].HungerRestore;
                            if (players[index].Hunger > 100) { players[index].Hunger = 100; }
                        }
                        break;

                    case (int)ItemType.Drink:
                        if (players[index].Backpack[slot].HydrateRestore > 0)
                        {
                            players[index].Hydration += players[index].Backpack[slot].HydrateRestore;
                            if (players[index].Hydration > 100) { players[index].Hydration = 100; }
                        }
                        break;

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
                case (int)EquipSlots.MainWeapon:
                    int itemSlot = players[index].FindOpenInvSlot(players[index].Backpack);
                    if (itemSlot < 25)
                    {
                        players[index].Backpack[itemSlot] = players[index].mainWeapon;
                        players[index].mainWeapon = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);

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

                case (int)EquipSlots.OffWeapon:
                    itemSlot = players[index].FindOpenInvSlot(players[index].Backpack);
                    if (itemSlot < 25)
                    {
                        players[index].Backpack[itemSlot] = players[index].offWeapon;
                        players[index].offWeapon = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
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
                    maps[mapNum].m_MapItem[mapSlot].ReloadSpeed = players[index].Backpack[slot].ReloadSpeed;
                    maps[mapNum].m_MapItem[mapSlot].HealthRestore = players[index].Backpack[slot].HealthRestore;
                    maps[mapNum].m_MapItem[mapSlot].HungerRestore = players[index].Backpack[slot].HungerRestore;
                    maps[mapNum].m_MapItem[mapSlot].HydrateRestore = players[index].Backpack[slot].HydrateRestore;
                    maps[mapNum].m_MapItem[mapSlot].Strength = players[index].Backpack[slot].Strength;
                    maps[mapNum].m_MapItem[mapSlot].Agility = players[index].Backpack[slot].Agility;
                    maps[mapNum].m_MapItem[mapSlot].Endurance = players[index].Backpack[slot].Endurance;
                    maps[mapNum].m_MapItem[mapSlot].Stamina = players[index].Backpack[slot].Stamina;
                    maps[mapNum].m_MapItem[mapSlot].Clip = players[index].Backpack[slot].Clip;
                    maps[mapNum].m_MapItem[mapSlot].MaxClip = players[index].Backpack[slot].MaxClip;
                    maps[mapNum].m_MapItem[mapSlot].ItemAmmoType = players[index].Backpack[slot].ItemAmmoType;
                    maps[mapNum].m_MapItem[mapSlot].Value = players[index].Backpack[slot].Value;
                    maps[mapNum].m_MapItem[mapSlot].ProjectileNumber = players[index].Backpack[slot].ProjectileNumber;
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
                Backpack[itemSlot].ReloadSpeed = maps[map].m_MapItem[itemNum].ReloadSpeed;
                Backpack[itemSlot].HealthRestore = maps[map].m_MapItem[itemNum].HealthRestore;
                Backpack[itemSlot].HungerRestore = maps[map].m_MapItem[itemNum].HungerRestore;
                Backpack[itemSlot].HydrateRestore = maps[map].m_MapItem[itemNum].HydrateRestore;
                Backpack[itemSlot].Strength = maps[map].m_MapItem[itemNum].Strength;
                Backpack[itemSlot].Agility = maps[map].m_MapItem[itemNum].Agility;
                Backpack[itemSlot].Endurance = maps[map].m_MapItem[itemNum].Endurance;
                Backpack[itemSlot].Stamina = maps[map].m_MapItem[itemNum].Stamina;
                Backpack[itemSlot].Clip = maps[map].m_MapItem[itemNum].Clip;
                Backpack[itemSlot].MaxClip = maps[map].m_MapItem[itemNum].MaxClip;
                Backpack[itemSlot].ItemAmmoType = maps[map].m_MapItem[itemNum].ItemAmmoType;
                Backpack[itemSlot].Value = maps[map].m_MapItem[itemNum].Value;
                Backpack[itemSlot].ProjectileNumber = maps[map].m_MapItem[itemNum].ProjectileNumber;
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
                    cmd.Parameters.Add(new SqlParameter("@experience", System.Data.DbType.Int32)).Value = Experience;
                    cmd.Parameters.Add(new SqlParameter("@money", System.Data.DbType.Int32)).Value = Money;
                    cmd.Parameters.Add(new SqlParameter("@armor", System.Data.DbType.Int32)).Value = Armor;
                    cmd.Parameters.Add(new SqlParameter("@hunger", System.Data.DbType.Int32)).Value = Hunger;
                    cmd.Parameters.Add(new SqlParameter("@hydration", System.Data.DbType.Int32)).Value = Hydration;
                    cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = Strength;
                    cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = Agility;
                    cmd.Parameters.Add(new SqlParameter("@endurance", System.Data.DbType.Int32)).Value = Endurance;
                    cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = Stamina;
                    cmd.Parameters.Add(new SqlParameter("@pistolammo", System.Data.DbType.Int32)).Value = PistolAmmo;
                    cmd.Parameters.Add(new SqlParameter("@assaultammo", System.Data.DbType.Int32)).Value = AssaultAmmo;
                    cmd.Parameters.Add(new SqlParameter("@rocketammo", System.Data.DbType.Int32)).Value = RocketAmmo;
                    cmd.Parameters.Add(new SqlParameter("@grenadeammo", System.Data.DbType.Int32)).Value = GrenadeAmmo;
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
                    cmd.Parameters.Add(new SqlParameter("@kills", System.Data.DbType.Int32)).Value = Kills;
                    cmd.Parameters.Add(new SqlParameter("@points", System.Data.DbType.Int32)).Value = Points;
                    cmd.Parameters.Add(new SqlParameter("@days", System.Data.DbType.Int32)).Value = PlayDays;
                    cmd.Parameters.Add(new SqlParameter("@hours", System.Data.DbType.Int32)).Value = PlayHours;
                    cmd.Parameters.Add(new SqlParameter("@minutes", System.Data.DbType.Int32)).Value = PlayMinutes;
                    cmd.Parameters.Add(new SqlParameter("@seconds", System.Data.DbType.Int32)).Value = PlaySeconds;
                    cmd.Parameters.Add(new SqlParameter("@ldays", System.Data.DbType.Int32)).Value = LifeDay;
                    cmd.Parameters.Add(new SqlParameter("@lhours", System.Data.DbType.Int32)).Value = LifeHour;
                    cmd.Parameters.Add(new SqlParameter("@lminutes", System.Data.DbType.Int32)).Value = LifeMinute;
                    cmd.Parameters.Add(new SqlParameter("@lseconds", System.Data.DbType.Int32)).Value = LifeSecond;
                    cmd.Parameters.Add(new SqlParameter("@lldays", System.Data.DbType.Int32)).Value = LongestLifeDay;
                    cmd.Parameters.Add(new SqlParameter("@llhours", System.Data.DbType.Int32)).Value = LongestLifeHour;
                    cmd.Parameters.Add(new SqlParameter("@llminutes", System.Data.DbType.Int32)).Value = LongestLifeMinute;
                    cmd.Parameters.Add(new SqlParameter("@llseconds", System.Data.DbType.Int32)).Value = LongestLifeSecond;
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
                    cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = mainWeapon.Name;
                    cmd.Parameters.Add(new SqlParameter("@clip", System.Data.DbType.Int32)).Value = mainWeapon.Clip;
                    cmd.Parameters.Add(new SqlParameter("@maxclip", System.Data.DbType.Int32)).Value = mainWeapon.MaxClip;
                    cmd.Parameters.Add(new SqlParameter("@sprite", System.Data.DbType.Int32)).Value = mainWeapon.Sprite;
                    cmd.Parameters.Add(new SqlParameter("@damage", System.Data.DbType.Int32)).Value = mainWeapon.Damage;
                    cmd.Parameters.Add(new SqlParameter("@armor", System.Data.DbType.Int32)).Value = mainWeapon.Armor;
                    cmd.Parameters.Add(new SqlParameter("@type", System.Data.DbType.Int32)).Value = mainWeapon.Type;
                    cmd.Parameters.Add(new SqlParameter("@attackspeed", System.Data.DbType.Int32)).Value = mainWeapon.AttackSpeed;
                    cmd.Parameters.Add(new SqlParameter("@reloadspeed", System.Data.DbType.Int32)).Value = mainWeapon.ReloadSpeed;
                    cmd.Parameters.Add(new SqlParameter("@healthrestore", System.Data.DbType.Int32)).Value = mainWeapon.HealthRestore;
                    cmd.Parameters.Add(new SqlParameter("@hungerrestore", System.Data.DbType.Int32)).Value = mainWeapon.HungerRestore;
                    cmd.Parameters.Add(new SqlParameter("@hydraterestore", System.Data.DbType.Int32)).Value = mainWeapon.HydrateRestore;
                    cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = mainWeapon.Strength;
                    cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = mainWeapon.Agility;
                    cmd.Parameters.Add(new SqlParameter("@endurance", System.Data.DbType.Int32)).Value = mainWeapon.Endurance;
                    cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = mainWeapon.Stamina;
                    cmd.Parameters.Add(new SqlParameter("@ammotype", System.Data.DbType.Int32)).Value = mainWeapon.ItemAmmoType;
                    cmd.Parameters.Add(new SqlParameter("@value", System.Data.DbType.Int32)).Value = mainWeapon.Value;
                    cmd.Parameters.Add(new SqlParameter("@proj", System.Data.DbType.Int32)).Value = mainWeapon.ProjectileNumber;
                    cmd.Parameters.Add(new SqlParameter("@price", System.Data.DbType.Int32)).Value = mainWeapon.Price;
                    cmd.Parameters.Add(new SqlParameter("@rarity", System.Data.DbType.Int32)).Value = mainWeapon.Rarity;
                    cmd.ExecuteNonQuery();
                }

                script = ReadAllText("SQL Data Scripts/Insert_Secondary_Weapons.sql");
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                    cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = offWeapon.Name;
                    cmd.Parameters.Add(new SqlParameter("@clip", System.Data.DbType.Int32)).Value = offWeapon.Clip;
                    cmd.Parameters.Add(new SqlParameter("@maxclip", System.Data.DbType.Int32)).Value = offWeapon.MaxClip;
                    cmd.Parameters.Add(new SqlParameter("@sprite", System.Data.DbType.Int32)).Value = offWeapon.Sprite;
                    cmd.Parameters.Add(new SqlParameter("@damage", System.Data.DbType.Int32)).Value = offWeapon.Damage;
                    cmd.Parameters.Add(new SqlParameter("@armor", System.Data.DbType.Int32)).Value = offWeapon.Armor;
                    cmd.Parameters.Add(new SqlParameter("@type", System.Data.DbType.Int32)).Value = offWeapon.Type;
                    cmd.Parameters.Add(new SqlParameter("@attackspeed", System.Data.DbType.Int32)).Value = offWeapon.AttackSpeed;
                    cmd.Parameters.Add(new SqlParameter("@reloadspeed", System.Data.DbType.Int32)).Value = offWeapon.ReloadSpeed;
                    cmd.Parameters.Add(new SqlParameter("@healthrestore", System.Data.DbType.Int32)).Value = offWeapon.HealthRestore;
                    cmd.Parameters.Add(new SqlParameter("@hungerrestore", System.Data.DbType.Int32)).Value = offWeapon.HungerRestore;
                    cmd.Parameters.Add(new SqlParameter("@hydraterestore", System.Data.DbType.Int32)).Value = mainWeapon.HydrateRestore;
                    cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = offWeapon.Strength;
                    cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = offWeapon.Agility;
                    cmd.Parameters.Add(new SqlParameter("@endurance", System.Data.DbType.Int32)).Value = offWeapon.Endurance;
                    cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = offWeapon.Stamina;
                    cmd.Parameters.Add(new SqlParameter("@ammotype", System.Data.DbType.Int32)).Value = offWeapon.ItemAmmoType;
                    cmd.Parameters.Add(new SqlParameter("@value", System.Data.DbType.Int32)).Value = offWeapon.Value;
                    cmd.Parameters.Add(new SqlParameter("@proj", System.Data.DbType.Int32)).Value = offWeapon.ProjectileNumber;
                    cmd.Parameters.Add(new SqlParameter("@price", System.Data.DbType.Int32)).Value = offWeapon.Price;
                    cmd.Parameters.Add(new SqlParameter("@rarity", System.Data.DbType.Int32)).Value = offWeapon.Rarity;
                    cmd.ExecuteNonQuery();
                }

                script = ReadAllText("SQL Data Scripts/Insert_Equipment.sql");
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                    //cmd.Parameters.Add(new SqlParameter("@id", System.Data.DbType.Int32)).Value = Chest.Id;
                    cmd.Parameters.Add(new SqlParameter("@slot", System.Data.DbType.Int32)).Value = 0;
                    cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = Chest.Name;
                    cmd.Parameters.Add(new SqlParameter("@clip", System.Data.DbType.Int32)).Value = Chest.Clip;
                    cmd.Parameters.Add(new SqlParameter("@maxclip", System.Data.DbType.Int32)).Value = Chest.MaxClip;
                    cmd.Parameters.Add(new SqlParameter("@sprite", System.Data.DbType.Int32)).Value = Chest.Sprite;
                    cmd.Parameters.Add(new SqlParameter("@damage", System.Data.DbType.Int32)).Value = Chest.Damage;
                    cmd.Parameters.Add(new SqlParameter("@armor", System.Data.DbType.Int32)).Value = Chest.Armor;
                    cmd.Parameters.Add(new SqlParameter("@type", System.Data.DbType.Int32)).Value = Chest.Type;
                    cmd.Parameters.Add(new SqlParameter("@attackspeed", System.Data.DbType.Int32)).Value = Chest.AttackSpeed;
                    cmd.Parameters.Add(new SqlParameter("@reloadspeed", System.Data.DbType.Int32)).Value = Chest.ReloadSpeed;
                    cmd.Parameters.Add(new SqlParameter("@healthrestore", System.Data.DbType.Int32)).Value = Chest.HealthRestore;
                    cmd.Parameters.Add(new SqlParameter("@hungerrestore", System.Data.DbType.Int32)).Value = Chest.HungerRestore;
                    cmd.Parameters.Add(new SqlParameter("@hydraterestore", System.Data.DbType.Int32)).Value = Chest.HydrateRestore;
                    cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = Chest.Strength;
                    cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = Chest.Agility;
                    cmd.Parameters.Add(new SqlParameter("@endurance", System.Data.DbType.Int32)).Value = Chest.Endurance;
                    cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = Chest.Stamina;
                    cmd.Parameters.Add(new SqlParameter("@ammotype", System.Data.DbType.Int32)).Value = Chest.ItemAmmoType;
                    cmd.Parameters.Add(new SqlParameter("@value", System.Data.DbType.Int32)).Value = Chest.Value;
                    cmd.Parameters.Add(new SqlParameter("@proj", System.Data.DbType.Int32)).Value = Chest.ProjectileNumber;
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
                    cmd.Parameters.Add(new SqlParameter("@clip", System.Data.DbType.Int32)).Value = Legs.Clip;
                    cmd.Parameters.Add(new SqlParameter("@maxclip", System.Data.DbType.Int32)).Value = Legs.MaxClip;
                    cmd.Parameters.Add(new SqlParameter("@sprite", System.Data.DbType.Int32)).Value = Legs.Sprite;
                    cmd.Parameters.Add(new SqlParameter("@damage", System.Data.DbType.Int32)).Value = Legs.Damage;
                    cmd.Parameters.Add(new SqlParameter("@armor", System.Data.DbType.Int32)).Value = Legs.Armor;
                    cmd.Parameters.Add(new SqlParameter("@type", System.Data.DbType.Int32)).Value = Legs.Type;
                    cmd.Parameters.Add(new SqlParameter("@attackspeed", System.Data.DbType.Int32)).Value = Legs.AttackSpeed;
                    cmd.Parameters.Add(new SqlParameter("@reloadspeed", System.Data.DbType.Int32)).Value = Legs.ReloadSpeed;
                    cmd.Parameters.Add(new SqlParameter("@healthrestore", System.Data.DbType.Int32)).Value = Legs.HealthRestore;
                    cmd.Parameters.Add(new SqlParameter("@hungerrestore", System.Data.DbType.Int32)).Value = Legs.HungerRestore;
                    cmd.Parameters.Add(new SqlParameter("@hydraterestore", System.Data.DbType.Int32)).Value = Legs.HydrateRestore;
                    cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = Legs.Strength;
                    cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = Legs.Agility;
                    cmd.Parameters.Add(new SqlParameter("@endurance", System.Data.DbType.Int32)).Value = Legs.Endurance;
                    cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = Legs.Stamina;
                    cmd.Parameters.Add(new SqlParameter("@ammotype", System.Data.DbType.Int32)).Value = Legs.ItemAmmoType;
                    cmd.Parameters.Add(new SqlParameter("@value", System.Data.DbType.Int32)).Value = Legs.Value;
                    cmd.Parameters.Add(new SqlParameter("@proj", System.Data.DbType.Int32)).Value = Legs.ProjectileNumber;
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
                    cmd.Parameters.Add(new SqlParameter("@clip", System.Data.DbType.Int32)).Value = Feet.Clip;
                    cmd.Parameters.Add(new SqlParameter("@maxclip", System.Data.DbType.Int32)).Value = Feet.MaxClip;
                    cmd.Parameters.Add(new SqlParameter("@sprite", System.Data.DbType.Int32)).Value = Feet.Sprite;
                    cmd.Parameters.Add(new SqlParameter("@damage", System.Data.DbType.Int32)).Value = Feet.Damage;
                    cmd.Parameters.Add(new SqlParameter("@armor", System.Data.DbType.Int32)).Value = Feet.Armor;
                    cmd.Parameters.Add(new SqlParameter("@type", System.Data.DbType.Int32)).Value = Feet.Type;
                    cmd.Parameters.Add(new SqlParameter("@attackspeed", System.Data.DbType.Int32)).Value = Feet.AttackSpeed;
                    cmd.Parameters.Add(new SqlParameter("@reloadspeed", System.Data.DbType.Int32)).Value = Feet.ReloadSpeed;
                    cmd.Parameters.Add(new SqlParameter("@healthrestore", System.Data.DbType.Int32)).Value = Feet.HealthRestore;
                    cmd.Parameters.Add(new SqlParameter("@hungerrestore", System.Data.DbType.Int32)).Value = Feet.HungerRestore;
                    cmd.Parameters.Add(new SqlParameter("@hydraterestore", System.Data.DbType.Int32)).Value = Feet.HydrateRestore;
                    cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = Feet.Strength;
                    cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = Feet.Agility;
                    cmd.Parameters.Add(new SqlParameter("@endurance", System.Data.DbType.Int32)).Value = Feet.Endurance;
                    cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = Feet.Stamina;
                    cmd.Parameters.Add(new SqlParameter("@ammotype", System.Data.DbType.Int32)).Value = Feet.ItemAmmoType;
                    cmd.Parameters.Add(new SqlParameter("@value", System.Data.DbType.Int32)).Value = Feet.Value;
                    cmd.Parameters.Add(new SqlParameter("@proj", System.Data.DbType.Int32)).Value = Feet.ProjectileNumber;
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
                    cmd.Parameters.Add(new SqlParameter("@experience", System.Data.DbType.Int32)).Value = Experience;
                    cmd.Parameters.Add(new SqlParameter("@money", System.Data.DbType.Int32)).Value = Money;
                    cmd.Parameters.Add(new SqlParameter("@armor", System.Data.DbType.Int32)).Value = Armor;
                    cmd.Parameters.Add(new SqlParameter("@hunger", System.Data.DbType.Int32)).Value = Hunger;
                    cmd.Parameters.Add(new SqlParameter("@hydrate", System.Data.DbType.Int32)).Value = Hydration;
                    cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = Strength;
                    cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = Agility;
                    cmd.Parameters.Add(new SqlParameter("@endurance", System.Data.DbType.Int32)).Value = Endurance;
                    cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = Stamina;
                    cmd.Parameters.Add(new SqlParameter("@pistolammo", System.Data.DbType.Int32)).Value = PistolAmmo;
                    cmd.Parameters.Add(new SqlParameter("@assaultammo", System.Data.DbType.Int32)).Value = AssaultAmmo;
                    cmd.Parameters.Add(new SqlParameter("@rocketammo", System.Data.DbType.Int32)).Value = RocketAmmo;
                    cmd.Parameters.Add(new SqlParameter("@grenadeammo", System.Data.DbType.Int32)).Value = GrenadeAmmo;
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
                    cmd.Parameters.Add(new SqlParameter("@kills", System.Data.DbType.Int32)).Value = Kills;
                    cmd.Parameters.Add(new SqlParameter("@points", System.Data.DbType.Int32)).Value = Points;
                    cmd.Parameters.Add(new SqlParameter("@days", System.Data.DbType.Int32)).Value = PlayDays;
                    cmd.Parameters.Add(new SqlParameter("@hours", System.Data.DbType.Int32)).Value = PlayHours;
                    cmd.Parameters.Add(new SqlParameter("@minutes", System.Data.DbType.Int32)).Value = PlayMinutes;
                    cmd.Parameters.Add(new SqlParameter("@seconds", System.Data.DbType.Int32)).Value = PlaySeconds;
                    cmd.Parameters.Add(new SqlParameter("@ldays", System.Data.DbType.Int32)).Value = LifeDay;
                    cmd.Parameters.Add(new SqlParameter("@lhours", System.Data.DbType.Int32)).Value = LifeHour;
                    cmd.Parameters.Add(new SqlParameter("@lminutes", System.Data.DbType.Int32)).Value = LifeMinute;
                    cmd.Parameters.Add(new SqlParameter("@lseconds", System.Data.DbType.Int32)).Value = LifeSecond;
                    cmd.Parameters.Add(new SqlParameter("@lldays", System.Data.DbType.Int32)).Value = LongestLifeDay;
                    cmd.Parameters.Add(new SqlParameter("@llhours", System.Data.DbType.Int32)).Value = LongestLifeHour;
                    cmd.Parameters.Add(new SqlParameter("@llminutes", System.Data.DbType.Int32)).Value = LongestLifeMinute;
                    cmd.Parameters.Add(new SqlParameter("@llseconds", System.Data.DbType.Int32)).Value = LongestLifeSecond;
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
                    cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = mainWeapon.Name;
                    cmd.Parameters.Add(new SqlParameter("@clip", System.Data.DbType.Int32)).Value = mainWeapon.Clip;
                    cmd.Parameters.Add(new SqlParameter("@maxclip", System.Data.DbType.Int32)).Value = mainWeapon.MaxClip;
                    cmd.Parameters.Add(new SqlParameter("@sprite", System.Data.DbType.Int32)).Value = mainWeapon.Sprite;
                    cmd.Parameters.Add(new SqlParameter("@damage", System.Data.DbType.Int32)).Value = mainWeapon.Damage;
                    cmd.Parameters.Add(new SqlParameter("@armor", System.Data.DbType.Int32)).Value = mainWeapon.Armor;
                    cmd.Parameters.Add(new SqlParameter("@type", System.Data.DbType.Int32)).Value = mainWeapon.Type;
                    cmd.Parameters.Add(new SqlParameter("@attackspeed", System.Data.DbType.Int32)).Value = mainWeapon.AttackSpeed;
                    cmd.Parameters.Add(new SqlParameter("@reloadspeed", System.Data.DbType.Int32)).Value = mainWeapon.ReloadSpeed;
                    cmd.Parameters.Add(new SqlParameter("@healthrestore", System.Data.DbType.Int32)).Value = mainWeapon.HealthRestore;
                    cmd.Parameters.Add(new SqlParameter("@hungerrestore", System.Data.DbType.Int32)).Value = mainWeapon.HungerRestore;
                    cmd.Parameters.Add(new SqlParameter("@hydraterestore", System.Data.DbType.Int32)).Value = mainWeapon.HydrateRestore;
                    cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = mainWeapon.Strength;
                    cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = mainWeapon.Agility;
                    cmd.Parameters.Add(new SqlParameter("@endurance", System.Data.DbType.Int32)).Value = mainWeapon.Endurance;
                    cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = mainWeapon.Stamina;
                    cmd.Parameters.Add(new SqlParameter("@ammotype", System.Data.DbType.Int32)).Value = mainWeapon.ItemAmmoType;
                    cmd.Parameters.Add(new SqlParameter("@value", System.Data.DbType.Int32)).Value = mainWeapon.Value;
                    cmd.Parameters.Add(new SqlParameter("@proj", System.Data.DbType.Int32)).Value = mainWeapon.ProjectileNumber;
                    cmd.Parameters.Add(new SqlParameter("@price", System.Data.DbType.Int32)).Value = mainWeapon.Price;
                    cmd.Parameters.Add(new SqlParameter("@rarity", System.Data.DbType.Int32)).Value = mainWeapon.Rarity;
                    cmd.ExecuteNonQuery();
                }

                script = ReadAllText("SQL Data Scripts/Save_Secondary_Weapons.sql");
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                    cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = offWeapon.Name;
                    cmd.Parameters.Add(new SqlParameter("@clip", System.Data.DbType.Int32)).Value = offWeapon.Clip;
                    cmd.Parameters.Add(new SqlParameter("@maxclip", System.Data.DbType.Int32)).Value = offWeapon.MaxClip;
                    cmd.Parameters.Add(new SqlParameter("@sprite", System.Data.DbType.Int32)).Value = offWeapon.Sprite;
                    cmd.Parameters.Add(new SqlParameter("@damage", System.Data.DbType.Int32)).Value = offWeapon.Damage;
                    cmd.Parameters.Add(new SqlParameter("@armor", System.Data.DbType.Int32)).Value = offWeapon.Armor;
                    cmd.Parameters.Add(new SqlParameter("@type", System.Data.DbType.Int32)).Value = offWeapon.Type;
                    cmd.Parameters.Add(new SqlParameter("@attackspeed", System.Data.DbType.Int32)).Value = offWeapon.AttackSpeed;
                    cmd.Parameters.Add(new SqlParameter("@reloadspeed", System.Data.DbType.Int32)).Value = offWeapon.ReloadSpeed;
                    cmd.Parameters.Add(new SqlParameter("@healthrestore", System.Data.DbType.Int32)).Value = offWeapon.HealthRestore;
                    cmd.Parameters.Add(new SqlParameter("@hungerrestore", System.Data.DbType.Int32)).Value = offWeapon.HungerRestore;
                    cmd.Parameters.Add(new SqlParameter("@hydraterestore", System.Data.DbType.Int32)).Value = mainWeapon.HydrateRestore;
                    cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = offWeapon.Strength;
                    cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = offWeapon.Agility;
                    cmd.Parameters.Add(new SqlParameter("@endurance", System.Data.DbType.Int32)).Value = offWeapon.Endurance;
                    cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = offWeapon.Stamina;
                    cmd.Parameters.Add(new SqlParameter("@ammotype", System.Data.DbType.Int32)).Value = offWeapon.ItemAmmoType;
                    cmd.Parameters.Add(new SqlParameter("@value", System.Data.DbType.Int32)).Value = offWeapon.Value;
                    cmd.Parameters.Add(new SqlParameter("@proj", System.Data.DbType.Int32)).Value = offWeapon.ProjectileNumber;
                    cmd.Parameters.Add(new SqlParameter("@price", System.Data.DbType.Int32)).Value = offWeapon.Price;
                    cmd.Parameters.Add(new SqlParameter("@rarity", System.Data.DbType.Int32)).Value = offWeapon.Rarity;
                    cmd.ExecuteNonQuery();
                }

                script = ReadAllText("SQL Data Scripts/Save_Equipment.sql");
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                    cmd.Parameters.Add(new SqlParameter("@id", System.Data.DbType.Int32)).Value = Chest.Id;
                    cmd.Parameters.Add(new SqlParameter("@slot", System.Data.DbType.Int32)).Value = 0;
                    cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = Chest.Name;
                    cmd.Parameters.Add(new SqlParameter("@clip", System.Data.DbType.Int32)).Value = Chest.Clip;
                    cmd.Parameters.Add(new SqlParameter("@maxclip", System.Data.DbType.Int32)).Value = Chest.MaxClip;
                    cmd.Parameters.Add(new SqlParameter("@sprite", System.Data.DbType.Int32)).Value = Chest.Sprite;
                    cmd.Parameters.Add(new SqlParameter("@damage", System.Data.DbType.Int32)).Value = Chest.Damage;
                    cmd.Parameters.Add(new SqlParameter("@armor", System.Data.DbType.Int32)).Value = Chest.Armor;
                    cmd.Parameters.Add(new SqlParameter("@type", System.Data.DbType.Int32)).Value = Chest.Type;
                    cmd.Parameters.Add(new SqlParameter("@attackspeed", System.Data.DbType.Int32)).Value = Chest.AttackSpeed;
                    cmd.Parameters.Add(new SqlParameter("@reloadspeed", System.Data.DbType.Int32)).Value = Chest.ReloadSpeed;
                    cmd.Parameters.Add(new SqlParameter("@healthrestore", System.Data.DbType.Int32)).Value = Chest.HealthRestore;
                    cmd.Parameters.Add(new SqlParameter("@hungerrestore", System.Data.DbType.Int32)).Value = Chest.HungerRestore;
                    cmd.Parameters.Add(new SqlParameter("@hydraterestore", System.Data.DbType.Int32)).Value = Chest.HydrateRestore;
                    cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = Chest.Strength;
                    cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = Chest.Agility;
                    cmd.Parameters.Add(new SqlParameter("@endurance", System.Data.DbType.Int32)).Value = Chest.Endurance;
                    cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = Chest.Stamina;
                    cmd.Parameters.Add(new SqlParameter("@ammotype", System.Data.DbType.Int32)).Value = Chest.ItemAmmoType;
                    cmd.Parameters.Add(new SqlParameter("@value", System.Data.DbType.Int32)).Value = Chest.Value;
                    cmd.Parameters.Add(new SqlParameter("@proj", System.Data.DbType.Int32)).Value = Chest.ProjectileNumber;
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
                    cmd.Parameters.Add(new SqlParameter("@clip", System.Data.DbType.Int32)).Value = Legs.Clip;
                    cmd.Parameters.Add(new SqlParameter("@maxclip", System.Data.DbType.Int32)).Value = Legs.MaxClip;
                    cmd.Parameters.Add(new SqlParameter("@sprite", System.Data.DbType.Int32)).Value = Legs.Sprite;
                    cmd.Parameters.Add(new SqlParameter("@damage", System.Data.DbType.Int32)).Value = Legs.Damage;
                    cmd.Parameters.Add(new SqlParameter("@armor", System.Data.DbType.Int32)).Value = Legs.Armor;
                    cmd.Parameters.Add(new SqlParameter("@type", System.Data.DbType.Int32)).Value = Legs.Type;
                    cmd.Parameters.Add(new SqlParameter("@attackspeed", System.Data.DbType.Int32)).Value = Legs.AttackSpeed;
                    cmd.Parameters.Add(new SqlParameter("@reloadspeed", System.Data.DbType.Int32)).Value = Legs.ReloadSpeed;
                    cmd.Parameters.Add(new SqlParameter("@healthrestore", System.Data.DbType.Int32)).Value = Legs.HealthRestore;
                    cmd.Parameters.Add(new SqlParameter("@hungerrestore", System.Data.DbType.Int32)).Value = Legs.HungerRestore;
                    cmd.Parameters.Add(new SqlParameter("@hydraterestore", System.Data.DbType.Int32)).Value = Legs.HydrateRestore;
                    cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = Legs.Strength;
                    cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = Legs.Agility;
                    cmd.Parameters.Add(new SqlParameter("@endurance", System.Data.DbType.Int32)).Value = Legs.Endurance;
                    cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = Legs.Stamina;
                    cmd.Parameters.Add(new SqlParameter("@ammotype", System.Data.DbType.Int32)).Value = Legs.ItemAmmoType;
                    cmd.Parameters.Add(new SqlParameter("@value", System.Data.DbType.Int32)).Value = Legs.Value;
                    cmd.Parameters.Add(new SqlParameter("@proj", System.Data.DbType.Int32)).Value = Legs.ProjectileNumber;
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
                    cmd.Parameters.Add(new SqlParameter("@clip", System.Data.DbType.Int32)).Value = Feet.Clip;
                    cmd.Parameters.Add(new SqlParameter("@maxclip", System.Data.DbType.Int32)).Value = Feet.MaxClip;
                    cmd.Parameters.Add(new SqlParameter("@sprite", System.Data.DbType.Int32)).Value = Feet.Sprite;
                    cmd.Parameters.Add(new SqlParameter("@damage", System.Data.DbType.Int32)).Value = Feet.Damage;
                    cmd.Parameters.Add(new SqlParameter("@armor", System.Data.DbType.Int32)).Value = Feet.Armor;
                    cmd.Parameters.Add(new SqlParameter("@type", System.Data.DbType.Int32)).Value = Feet.Type;
                    cmd.Parameters.Add(new SqlParameter("@attackspeed", System.Data.DbType.Int32)).Value = Feet.AttackSpeed;
                    cmd.Parameters.Add(new SqlParameter("@reloadspeed", System.Data.DbType.Int32)).Value = Feet.ReloadSpeed;
                    cmd.Parameters.Add(new SqlParameter("@healthrestore", System.Data.DbType.Int32)).Value = Feet.HealthRestore;
                    cmd.Parameters.Add(new SqlParameter("@hungerrestore", System.Data.DbType.Int32)).Value = Feet.HungerRestore;
                    cmd.Parameters.Add(new SqlParameter("@hydraterestore", System.Data.DbType.Int32)).Value = Feet.HydrateRestore;
                    cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = Feet.Strength;
                    cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = Feet.Agility;
                    cmd.Parameters.Add(new SqlParameter("@endurance", System.Data.DbType.Int32)).Value = Feet.Endurance;
                    cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = Feet.Stamina;
                    cmd.Parameters.Add(new SqlParameter("@ammotype", System.Data.DbType.Int32)).Value = Feet.ItemAmmoType;
                    cmd.Parameters.Add(new SqlParameter("@value", System.Data.DbType.Int32)).Value = Feet.Value;
                    cmd.Parameters.Add(new SqlParameter("@proj", System.Data.DbType.Int32)).Value = Feet.ProjectileNumber;
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
                            cmd.Parameters.Add(new SqlParameter("@reloadspeed", System.Data.DbType.Int32)).Value = Backpack[i].ReloadSpeed;
                            cmd.Parameters.Add(new SqlParameter("@healthrestore", System.Data.DbType.Int32)).Value = Backpack[i].HealthRestore;
                            cmd.Parameters.Add(new SqlParameter("@hungerrestore", System.Data.DbType.Int32)).Value = Backpack[i].HungerRestore;
                            cmd.Parameters.Add(new SqlParameter("@hydraterestore", System.Data.DbType.Int32)).Value = Backpack[i].HydrateRestore;
                            cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = Backpack[i].Strength;
                            cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = Backpack[i].Agility;
                            cmd.Parameters.Add(new SqlParameter("@endurance", System.Data.DbType.Int32)).Value = Backpack[i].Endurance;
                            cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = Backpack[i].Stamina;
                            cmd.Parameters.Add(new SqlParameter("@clip", System.Data.DbType.Int32)).Value = Backpack[i].Clip;
                            cmd.Parameters.Add(new SqlParameter("@maxclip", System.Data.DbType.Int32)).Value = Backpack[i].MaxClip;
                            cmd.Parameters.Add(new SqlParameter("@ammotype", System.Data.DbType.Int32)).Value = Backpack[i].ItemAmmoType;
                            cmd.Parameters.Add(new SqlParameter("@value", System.Data.DbType.Int32)).Value = Backpack[i].Value;
                            cmd.Parameters.Add(new SqlParameter("@proj", System.Data.DbType.Int32)).Value = Backpack[i].ProjectileNumber;
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
                            cmd.Parameters.Add(new SqlParameter("@reloadspeed", System.Data.DbType.Int32)).Value = Bank[i].ReloadSpeed;
                            cmd.Parameters.Add(new SqlParameter("@healthrestore", System.Data.DbType.Int32)).Value = Bank[i].HealthRestore;
                            cmd.Parameters.Add(new SqlParameter("@hungerrestore", System.Data.DbType.Int32)).Value = Bank[i].HungerRestore;
                            cmd.Parameters.Add(new SqlParameter("@hydraterestore", System.Data.DbType.Int32)).Value = Bank[i].HydrateRestore;
                            cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = Bank[i].Strength;
                            cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = Bank[i].Agility;
                            cmd.Parameters.Add(new SqlParameter("@endurance", System.Data.DbType.Int32)).Value = Bank[i].Endurance;
                            cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = Bank[i].Stamina;
                            cmd.Parameters.Add(new SqlParameter("@clip", System.Data.DbType.Int32)).Value = Bank[i].Clip;
                            cmd.Parameters.Add(new SqlParameter("@maxclip", System.Data.DbType.Int32)).Value = Bank[i].MaxClip;
                            cmd.Parameters.Add(new SqlParameter("@ammotype", System.Data.DbType.Int32)).Value = Bank[i].ItemAmmoType;
                            cmd.Parameters.Add(new SqlParameter("@value", System.Data.DbType.Int32)).Value = Bank[i].Value;
                            cmd.Parameters.Add(new SqlParameter("@proj", System.Data.DbType.Int32)).Value = Bank[i].ProjectileNumber;
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
                            Experience = ToInt32(reader[i]); i += 1;
                            Money = ToInt32(reader[i]); i += 1;
                            Armor = ToInt32(reader[i]); i += 1;
                            Hunger = ToInt32(reader[i]); i += 1;
                            Hydration = ToInt32(reader[i]); i += 1;
                            Strength = ToInt32(reader[i]); i += 1;
                            Agility = ToInt32(reader[i]); i += 1;
                            Endurance = ToInt32(reader[i]); i += 1;
                            Stamina = ToInt32(reader[i]); i += 1;
                            PistolAmmo = ToInt32(reader[i]); i += 1;
                            AssaultAmmo = ToInt32(reader[i]); i += 1;
                            RocketAmmo = ToInt32(reader[i]); i += 1;
                            GrenadeAmmo = ToInt32(reader[i]); i += 1;
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
                            Kills = ToInt32(reader[i]); i += 1;
                            Points = ToInt32(reader[i]); i += 1;
                            PlayDays = ToInt32(reader[i]); i += 1;
                            PlayHours = ToInt32(reader[i]); i += 1;
                            PlayMinutes = ToInt32(reader[i]); i += 1;
                            PlaySeconds = ToInt32(reader[i]); i += 1;
                            LifeDay = ToInt32(reader[i]); i += 1;
                            LifeHour = ToInt32(reader[i]); i += 1;
                            LifeMinute = ToInt32(reader[i]); i += 1;
                            LifeSecond = ToInt32(reader[i]); i += 1;
                            LongestLifeDay = ToInt32(reader[i]); i += 1;
                            LongestLifeHour = ToInt32(reader[i]); i += 1;
                            LongestLifeMinute = ToInt32(reader[i]); i += 1;
                            LongestLifeSecond = ToInt32(reader[i]); i += 1;
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
                            mainWeapon.Id = ToInt32(reader[i]); i = 2;
                            mainWeapon.Name = reader[i].ToString(); i += 1;
                            mainWeapon.Clip = ToInt32(reader[i]); i += 1;
                            mainWeapon.MaxClip = ToInt32(reader[i]); i += 1;
                            mainWeapon.Sprite = ToInt32(reader[i]); i += 1;
                            mainWeapon.Damage = ToInt32(reader[i]); i += 1;
                            mainWeapon.Armor = ToInt32(reader[i]); i += 1;
                            mainWeapon.Type = ToInt32(reader[i]); i += 1;
                            mainWeapon.AttackSpeed = ToInt32(reader[i]); i += 1;
                            mainWeapon.ReloadSpeed = ToInt32(reader[i]); i += 1;
                            mainWeapon.HealthRestore = ToInt32(reader[i]); i += 1;
                            mainWeapon.HungerRestore = ToInt32(reader[i]); i += 1;
                            mainWeapon.HydrateRestore = ToInt32(reader[i]); i += 1;
                            mainWeapon.Strength = ToInt32(reader[i]); i += 1;
                            mainWeapon.Agility = ToInt32(reader[i]); i += 1;
                            mainWeapon.Endurance = ToInt32(reader[i]); i += 1;
                            mainWeapon.Stamina = ToInt32(reader[i]); i += 1;
                            mainWeapon.ItemAmmoType = ToInt32(reader[i]); i += 1;
                            mainWeapon.Value = ToInt32(reader[i]); i += 1;
                            mainWeapon.ProjectileNumber = ToInt32(reader[i]); i += 1;
                            mainWeapon.Price = ToInt32(reader[i]); i += 1;
                            mainWeapon.Rarity = ToInt32(reader[i]);
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
                            offWeapon.Id = ToInt32(reader[i]); i = 2;
                            offWeapon.Name = reader[i].ToString(); i += 1;
                            offWeapon.Clip = ToInt32(reader[i]); i += 1;
                            offWeapon.MaxClip = ToInt32(reader[i]); i += 1;
                            offWeapon.Sprite = ToInt32(reader[i]); i += 1;
                            offWeapon.Damage = ToInt32(reader[i]); i += 1;
                            offWeapon.Armor = ToInt32(reader[i]); i += 1;
                            offWeapon.Type = ToInt32(reader[i]); i += 1;
                            offWeapon.AttackSpeed = ToInt32(reader[i]); i += 1;
                            offWeapon.ReloadSpeed = ToInt32(reader[i]); i += 1;
                            offWeapon.HealthRestore = ToInt32(reader[i]); i += 1;
                            offWeapon.HungerRestore = ToInt32(reader[i]); i += 1;
                            offWeapon.HydrateRestore = ToInt32(reader[i]); i += 1;
                            offWeapon.Strength = ToInt32(reader[i]); i += 1;
                            offWeapon.Agility = ToInt32(reader[i]); i += 1;
                            offWeapon.Endurance = ToInt32(reader[i]); i += 1;
                            offWeapon.Stamina = ToInt32(reader[i]); i += 1;
                            offWeapon.ItemAmmoType = ToInt32(reader[i]); i += 1;
                            offWeapon.Value = ToInt32(reader[i]); i += 1;
                            offWeapon.ProjectileNumber = ToInt32(reader[i]); i += 1;
                            offWeapon.Price = ToInt32(reader[i]); i += 1;
                            offWeapon.Rarity = ToInt32(reader[i]);
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
                            Chest.Clip = ToInt32(reader[i]); i += 1;
                            Chest.MaxClip = ToInt32(reader[i]); i += 1;
                            Chest.Sprite = ToInt32(reader[i]); i += 1;
                            Chest.Damage = ToInt32(reader[i]); i += 1;
                            Chest.Armor = ToInt32(reader[i]); i += 1;
                            Chest.Type = ToInt32(reader[i]); i += 1;
                            Chest.AttackSpeed = ToInt32(reader[i]); i += 1;
                            Chest.ReloadSpeed = ToInt32(reader[i]); i += 1;
                            Chest.HealthRestore = ToInt32(reader[i]); i += 1;
                            Chest.HungerRestore = ToInt32(reader[i]); i += 1;
                            Chest.HydrateRestore = ToInt32(reader[i]); i += 1;
                            Chest.Strength = ToInt32(reader[i]); i += 1;
                            Chest.Agility = ToInt32(reader[i]); i += 1;
                            Chest.Endurance = ToInt32(reader[i]); i += 1;
                            Chest.Stamina = ToInt32(reader[i]); i += 1;
                            Chest.ItemAmmoType = ToInt32(reader[i]); i += 1;
                            Chest.Value = ToInt32(reader[i]); i += 1;
                            Chest.ProjectileNumber = ToInt32(reader[i]); i += 1;
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
                            Legs.Clip = ToInt32(reader[i]); i += 1;
                            Legs.MaxClip = ToInt32(reader[i]); i += 1;
                            Legs.Sprite = ToInt32(reader[i]); i += 1;
                            Legs.Damage = ToInt32(reader[i]); i += 1;
                            Legs.Armor = ToInt32(reader[i]); i += 1;
                            Legs.Type = ToInt32(reader[i]); i += 1;
                            Legs.AttackSpeed = ToInt32(reader[i]); i += 1;
                            Legs.ReloadSpeed = ToInt32(reader[i]); i += 1;
                            Legs.HealthRestore = ToInt32(reader[i]); i += 1;
                            Legs.HungerRestore = ToInt32(reader[i]); i += 1;
                            Legs.HydrateRestore = ToInt32(reader[i]); i += 1;
                            Legs.Strength = ToInt32(reader[i]); i += 1;
                            Legs.Agility = ToInt32(reader[i]); i += 1;
                            Legs.Endurance = ToInt32(reader[i]); i += 1;
                            Legs.Stamina = ToInt32(reader[i]); i += 1;
                            Legs.ItemAmmoType = ToInt32(reader[i]); i += 1;
                            Legs.Value = ToInt32(reader[i]); i += 1;
                            Legs.ProjectileNumber = ToInt32(reader[i]); i += 1;
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
                            Feet.Clip = ToInt32(reader[i]); i += 1;
                            Feet.MaxClip = ToInt32(reader[i]); i += 1;
                            Feet.Sprite = ToInt32(reader[i]); i += 1;
                            Feet.Damage = ToInt32(reader[i]); i += 1;
                            Feet.Armor = ToInt32(reader[i]); i += 1;
                            Feet.Type = ToInt32(reader[i]); i += 1;
                            Feet.AttackSpeed = ToInt32(reader[i]); i += 1;
                            Feet.ReloadSpeed = ToInt32(reader[i]); i += 1;
                            Feet.HealthRestore = ToInt32(reader[i]); i += 1;
                            Feet.HungerRestore = ToInt32(reader[i]); i += 1;
                            Feet.HydrateRestore = ToInt32(reader[i]); i += 1;
                            Feet.Strength = ToInt32(reader[i]); i += 1;
                            Feet.Agility = ToInt32(reader[i]); i += 1;
                            Feet.Endurance = ToInt32(reader[i]); i += 1;
                            Feet.Stamina = ToInt32(reader[i]); i += 1;
                            Feet.ItemAmmoType = ToInt32(reader[i]); i += 1;
                            Feet.Value = ToInt32(reader[i]); i += 1;
                            Feet.ProjectileNumber = ToInt32(reader[i]); i += 1;
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
                                    Backpack[i].Clip = ToInt32(reader[n]); n += 1;
                                    Backpack[i].MaxClip = ToInt32(reader[n]); n += 1;
                                    Backpack[i].Sprite = ToInt32(reader[n]); n += 1;
                                    Backpack[i].Damage = ToInt32(reader[n]); n += 1;
                                    Backpack[i].Armor = ToInt32(reader[n]); n += 1;
                                    Backpack[i].Type = ToInt32(reader[n]); n += 1;
                                    Backpack[i].AttackSpeed = ToInt32(reader[n]); n += 1;
                                    Backpack[i].ReloadSpeed = ToInt32(reader[n]); n += 1;
                                    Backpack[i].HealthRestore = ToInt32(reader[n]); n += 1;
                                    Backpack[i].HungerRestore = ToInt32(reader[n]); n += 1;
                                    Backpack[i].HydrateRestore = ToInt32(reader[n]); n += 1;
                                    Backpack[i].Strength = ToInt32(reader[n]); n += 1;
                                    Backpack[i].Agility = ToInt32(reader[n]); n += 1;
                                    Backpack[i].Endurance = ToInt32(reader[n]); n += 1;
                                    Backpack[i].Stamina = ToInt32(reader[n]); n += 1;
                                    Backpack[i].ItemAmmoType = ToInt32(reader[n]); n += 1;
                                    Backpack[i].Value = ToInt32(reader[n]); n += 1;
                                    Backpack[i].ProjectileNumber = ToInt32(reader[n]); n += 1;
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
                                    Bank[i].Clip = ToInt32(reader[n]); n += 1;
                                    Bank[i].MaxClip = ToInt32(reader[n]); n += 1;
                                    Bank[i].Sprite = ToInt32(reader[n]); n += 1;
                                    Bank[i].Damage = ToInt32(reader[n]); n += 1;
                                    Bank[i].Armor = ToInt32(reader[n]); n += 1;
                                    Bank[i].Type = ToInt32(reader[n]); n += 1;
                                    Bank[i].AttackSpeed = ToInt32(reader[n]); n += 1;
                                    Bank[i].ReloadSpeed = ToInt32(reader[n]); n += 1;
                                    Bank[i].HealthRestore = ToInt32(reader[n]); n += 1;
                                    Bank[i].HungerRestore = ToInt32(reader[n]); n += 1;
                                    Bank[i].HydrateRestore = ToInt32(reader[n]); n += 1;
                                    Bank[i].Strength = ToInt32(reader[n]); n += 1;
                                    Bank[i].Agility = ToInt32(reader[n]); n += 1;
                                    Bank[i].Endurance = ToInt32(reader[n]); n += 1;
                                    Bank[i].Stamina = ToInt32(reader[n]); n += 1;
                                    Bank[i].ItemAmmoType = ToInt32(reader[n]); n += 1;
                                    Bank[i].Value = ToInt32(reader[n]); n += 1;
                                    Bank[i].ProjectileNumber = ToInt32(reader[n]); n += 1;
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
        MainWeapon,
        OffWeapon,
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
