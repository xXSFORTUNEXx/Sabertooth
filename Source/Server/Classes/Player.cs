using Lidgren.Network;
using System;
using System.Data.SQLite;
using System.Data.SqlClient;
using static System.Convert;
using static System.Environment;
using static SabertoothServer.Server;
using AccountKeyGenClass;
using static SabertoothServer.Globals;

namespace SabertoothServer
{
    public class Player
    {
        #region Main Classes
        public NetConnection Connection;
        public Item mainWeapon = new Item();
        public Item offWeapon = new Item();
        public Item[] Backpack = new Item[25];
        public Item[] Bank = new Item[50];
        public Item Chest = new Item();
        public Item Legs = new Item();
        public Item Feet = new Item();
        Random RND = new Random();
        public int Id { get; set; }
        #endregion

        #region Stats
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
        #endregion

        #region Local Variables
        public int hungerTick;
        public int hydrationTick;
        public int timeTick;
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
            LastLoggedIn = "00:00:00.000";
            AccountKey = KeyGen.Key(25);
            Active = "N";

            mainWeapon = new Item("Pistol", 1, 30, 0, (int)ItemType.RangedWeapon, 700, 1500, 0, 0, 0, 0, 0, 0, 0, 8, 8, (int)AmmoType.Pistol, 1, 1, 1, 0);
            offWeapon = new Item("Club", 3, 40, 0, (int)ItemType.MeleeWeapon, 900, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, (int)ItemType.None, 1, 1, 1, 0);
            Chest = new Item("Starter Shirt", 4, 0, 5, (int)ItemType.Shirt, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
            Legs = new Item("Starter Pants", 5, 0, 5, (int)ItemType.Pants, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
            Feet = new Item("Starter Shoes", 6, 0, 5, (int)ItemType.Shoes, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);

            for (int i = 0; i < 25; i++)
            {
                Backpack[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
            }

            for (int i = 0; i < 50; i++)
            {
                Bank[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
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
            LastLoggedIn = "00:00:00.000";
            AccountKey = KeyGen.Key(25);
            Active = "N";

            mainWeapon = new Item("Pistol", 1, 30, 0, (int)ItemType.RangedWeapon, 700, 1500, 0, 0, 0, 0, 0, 0, 0, 8, 8, (int)AmmoType.Pistol, 1, 1, 1, 0);
            offWeapon = new Item("Club", 3, 40, 0, (int)ItemType.MeleeWeapon, 900, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, (int)ItemType.None, 1, 1, 1, 0);
            Chest = new Item("Starter Shirt", 4, 0, 5, (int)ItemType.Shirt, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
            Legs = new Item("Starter Pants", 5, 0, 5, (int)ItemType.Pants, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
            Feet = new Item("Starter Shoes", 6, 0, 5, (int)ItemType.Shoes, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);

            for (int i = 0; i < 25; i++)
            {
                Backpack[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
            }

            for (int i = 0; i < 50; i++)
            {
                Bank[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
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

            for (int i = 0; i < 25; i++)
            {
                Backpack[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
            }

            for (int i = 0; i < 50; i++)
            {
                Bank[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
            }
        }

        public Player(NetConnection conn)
        {
            Connection = conn;
        }

        public Player()
        {
            for (int i = 0; i < 25; i++)
            {
                Backpack[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
            }

            for (int i = 0; i < 50; i++)
            {
                Bank[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
            }
        }
        #endregion

        #region Methods
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
                if (mapSlot < 20)
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

                    for (int p = 0; p < 5; p++)
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
            for (int c = 0; c < 20; c++)
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

            if (itemSlot < 25)
            {
                Backpack[itemSlot].Name = items[itemNum].Name;
                Backpack[itemSlot].Sprite = items[itemNum].Sprite;
                Backpack[itemSlot].Damage = items[itemNum].Damage;
                Backpack[itemSlot].Armor = items[itemNum].Armor;
                Backpack[itemSlot].Type = items[itemNum].Type;
                Backpack[itemSlot].AttackSpeed = items[itemNum].AttackSpeed;
                Backpack[itemSlot].ReloadSpeed = items[itemNum].ReloadSpeed;
                Backpack[itemSlot].HealthRestore = items[itemNum].HealthRestore;
                Backpack[itemSlot].HungerRestore = items[itemNum].HungerRestore;
                Backpack[itemSlot].HydrateRestore = items[itemNum].HydrateRestore;
                Backpack[itemSlot].Strength = items[itemNum].Strength;
                Backpack[itemSlot].Agility = items[itemNum].Agility;
                Backpack[itemSlot].Endurance = items[itemNum].Endurance;
                Backpack[itemSlot].Stamina = items[itemNum].Stamina;
                Backpack[itemSlot].Clip = items[itemNum].Clip;
                Backpack[itemSlot].MaxClip = items[itemNum].MaxClip;
                Backpack[itemSlot].ItemAmmoType = items[itemNum].ItemAmmoType;
                Backpack[itemSlot].Value = items[itemNum].Value;
                Backpack[itemSlot].ProjectileNumber = items[itemNum].ProjectileNumber;
                Backpack[itemSlot].Price = items[itemNum].Price;
                Backpack[itemSlot].Rarity = items[itemNum].Rarity;

                int TileX = maps[map].m_MapItem[itemNum].X;
                int TileY = maps[map].m_MapItem[itemNum].Y;
                maps[map].Ground[TileX, TileY].NeedsSpawnedTick = TickCount;
                maps[map].m_MapItem[itemNum].Name = "None";
                maps[map].m_MapItem[itemNum].IsSpawned = false;

                for (int p = 0; p < 5; p++)
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
            for (int i = 0; i < 25; i++)
            {
                if (s_Backpack[i].Name == "None")
                {
                    return i;
                }
            }
            return 25;
        }

        public int FindOpenBankSlot(Item[] s_Bank)
        {
            for (int i = 0; i < 50; i++)
            {
                if (s_Bank[i].Name == "None")
                {
                    return i;
                }
            }
            return 50;
        }
        #endregion

        #region Database
        public void CreatePlayerInDatabase()
        {
            if (DBType == SQL_DATABASE_REMOTE.ToString())
            {
                string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
                using (var sql = new SqlConnection(connection))
                {
                    sql.Open();
                    string command;
                    command = "INSERT INTO PLAYERS ";
                    command += "(NAME,PASSWORD,EMAILADDRESS,X,Y,MAP,DIRECTION,AIMDIRECTION,SPRITE,LEVEL,POINTS,HEALTH,MAXHEALTH,EXPERIENCE,MONEY,ARMOR,HUNGER,HYDRATION,STRENGTH,AGILITY,ENDURANCE,STAMINA,PISTOLAMMO,ASSAULTAMMO,ROCKETAMMO,GRENADEAMMO,LIGHTRADIUS,";
                    command += "DAYS,HOURS,MINUTES,SECONDS,LDAYS,LHOURS,LMINUTES,LSECONDS,LLDAYS,LLHOURS,LLMINUTES,LLSECONDS,LASTLOGGED,ACCOUNTKEY,ACTIVE) VALUES ";
                    command += "(@name,@password,@email,@x,@y,@map,@direction,@aimdirection,@sprite,@level,@points,@health,@maxhealth,@experience,@money,@armor,@hunger,@hydration,@strength,@agility,@endurance,@stamina,";
                    command += "@pistolammo,@assaultammo,@rocketammo,@grenadeammo,@lightradius,@days,@hours,@minutes,@seconds,@ldays,@lhours,@lminutes,@lseconds,@lldays,@llhours,@llminutes,@llseconds,@lastlogged,@accountkey,@active);";
                    using (var cmd = new SqlCommand(command, sql))
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
                        cmd.Parameters.Add(new SqlParameter("@points", System.Data.DbType.Int32)).Value = Points;
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
                        cmd.Parameters.Add(new SqlParameter("@lastlogged", System.Data.DbType.String)).Value = LastLoggedIn;
                        cmd.Parameters.Add(new SqlParameter("@accountkey", System.Data.DbType.String)).Value = AccountKey;
                        cmd.Parameters.Add(new SqlParameter("@active", System.Data.DbType.String)).Value = Active;
                        cmd.ExecuteNonQuery();
                    }

                    command = "INSERT INTO MAINWEAPONS ";
                    command += "(OWNER,NAME,CLIP,MAXCLIP,SPRITE,DAMAGE,ARMOR,TYPE,ATTACKSPEED,RELOADSPEED,HEALTHRESTORE,HUNGERRESTORE,HYDRATERESTORE,STRENGTH,AGILITY,ENDURANCE,STAMINA,AMMOTYPE,VALUE,PROJ,PRICE,RARITY) VALUES ";
                    command += "(@owner,@name,@clip,@maxclip,@sprite,@damage,@armor,@type,@attackspeed,@reloadspeed,@healthrestore,@hungerrestore,@hydraterestore,@strength,@agility,@endurance,@stamina,@ammotype,@value,@proj,@price,@rarity)";
                    using (var cmd = new SqlCommand(command, sql))
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

                    command = "INSERT INTO SECONDARYWEAPONS ";
                    command += "(OWNER,NAME,CLIP,MAXCLIP,SPRITE,DAMAGE,ARMOR,TYPE,ATTACKSPEED,RELOADSPEED,HEALTHRESTORE,HUNGERRESTORE,HYDRATERESTORE,STRENGTH,AGILITY,ENDURANCE,STAMINA,AMMOTYPE,VALUE,PROJ,PRICE,RARITY) VALUES ";
                    command += "(@owner,@name,@clip,@maxclip,@sprite,@damage,@armor,@type,@attackspeed,@reloadspeed,@healthrestore,@hungerrestore,@hydraterestore,@strength,@agility,@endurance,@stamina,@ammotype,@value,@proj,@price,@rarity)";
                    using (var cmd = new SqlCommand(command, sql))
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

                    command = "INSERT INTO EQUIPMENT ";
                    command += "(OWNER,SLOT,NAME,CLIP,MAXCLIP,SPRITE,DAMAGE,ARMOR,TYPE,ATTACKSPEED,RELOADSPEED,HEALTHRESTORE,HUNGERRESTORE,HYDRATERESTORE,STRENGTH,AGILITY,ENDURANCE,STAMINA,AMMOTYPE,VALUE,PROJ,PRICE,RARITY) VALUES ";
                    command += "(@owner,@id,@name,@clip,@maxclip,@sprite,@damage,@armor,@type,@attackspeed,@reloadspeed,@healthrestore,@hungerrestore,@hydraterestore,@strength,@agility,@endurance,@stamina,@ammotype,@value,@proj,@price,@rarity)";
                    using (var cmd = new SqlCommand(command, sql))
                    {
                        cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                        cmd.Parameters.Add(new SqlParameter("@id", System.Data.DbType.Int32)).Value = 0;
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

                    command = "INSERT INTO EQUIPMENT ";
                    command += "(OWNER,SLOT,NAME,CLIP,MAXCLIP,SPRITE,DAMAGE,ARMOR,TYPE,ATTACKSPEED,RELOADSPEED,HEALTHRESTORE,HUNGERRESTORE,HYDRATERESTORE,STRENGTH,AGILITY,ENDURANCE,STAMINA,AMMOTYPE,VALUE,PROJ,PRICE,RARITY) VALUES ";
                    command += "(@owner,@id,@name,@clip,@maxclip,@sprite,@damage,@armor,@type,@attackspeed,@reloadspeed,@healthrestore,@hungerrestore,@hydraterestore,@strength,@agility,@endurance,@stamina,@ammotype,@value,@proj,@price,@rarity)";
                    using (var cmd = new SqlCommand(command, sql))
                    {
                        cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                        cmd.Parameters.Add(new SqlParameter("@id", System.Data.DbType.Int32)).Value = 1;
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

                    command = "INSERT INTO EQUIPMENT ";
                    command += "(OWNER,SLOT,NAME,CLIP,MAXCLIP,SPRITE,DAMAGE,ARMOR,TYPE,ATTACKSPEED,RELOADSPEED,HEALTHRESTORE,HUNGERRESTORE,HYDRATERESTORE,STRENGTH,AGILITY,ENDURANCE,STAMINA,AMMOTYPE,VALUE,PROJ,PRICE,RARITY) VALUES ";
                    command += "(@owner,@id,@name,@clip,@maxclip,@sprite,@damage,@armor,@type,@attackspeed,@reloadspeed,@healthrestore,@hungerrestore,@hydraterestore,@strength,@agility,@endurance,@stamina,@ammotype,@value,@proj,@price,@rarity)";
                    using (var cmd = new SqlCommand(command, sql))
                    {
                        cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                        cmd.Parameters.Add(new SqlParameter("@id", System.Data.DbType.Int32)).Value = 2;
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
            else
            {
                using (var conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
                {
                    using (var cmd = new SQLiteCommand(conn))
                    {
                        conn.Open();
                        string command;
                        command = "INSERT INTO PLAYERS";
                        command = command + "(NAME,PASSWORD,EMAILADDRESS,X,Y,MAP,DIRECTION,AIMDIRECTION,SPRITE,LEVEL,POINTS,HEALTH,MAXHEALTH,EXPERIENCE,MONEY,ARMOR,HUNGER,HYDRATION,STRENGTH,AGILITY,ENDURANCE,STAMINA,PISTOLAMMO,ASSAULTAMMO,ROCKETAMMO,GRENADEAMMO,LIGHTRADIUS,";
                        command = command + "DAYS,HOURS,MINUTES,SECONDS,LDAYS,LHOURS,LMINUTES,LSECONDS,LLDAYS,LLHOURS,LLMINUTES,LLSECONDS,LASTLOGGED,ACCOUNTKEY,ACTIVE)";
                        command = command + " VALUES ";
                        command = command + "(@name,@password,@email,@x,@y,@map,@direction,@aimdirection,@sprite,@level,@points,@health,@maxhealth,@experience,@money,@armor,@hunger,@hydration,@strength,@agility,@endurance,@stamina,";
                        command = command + "@pistolammo,@assaultammo,@rocketammo,@grenadeammo,@lightradius,@days,@hours,@minutes,@seconds,@ldays,@lhours,@lminutes,@lseconds,@lldays,@llhours,@llminutes,@llseconds,@lastlogged,@accountket,@active);";
                        cmd.CommandText = command;
                        cmd.Parameters.Add("@name", System.Data.DbType.String).Value = Name;
                        cmd.Parameters.Add("@password", System.Data.DbType.String).Value = Pass;
                        cmd.Parameters.Add("@email", System.Data.DbType.String).Value = EmailAddress;
                        cmd.Parameters.Add("@x", System.Data.DbType.Int32).Value = X;
                        cmd.Parameters.Add("@y", System.Data.DbType.Int32).Value = Y;
                        cmd.Parameters.Add("@map", System.Data.DbType.Int32).Value = Map;
                        cmd.Parameters.Add("@direction", System.Data.DbType.Int32).Value = Direction;
                        cmd.Parameters.Add("@aimdirection", System.Data.DbType.Int32).Value = AimDirection;
                        cmd.Parameters.Add("@sprite", System.Data.DbType.Int32).Value = Sprite;
                        cmd.Parameters.Add("@level", System.Data.DbType.Int32).Value = Level;
                        cmd.Parameters.Add("@points", System.Data.DbType.Int32).Value = Points;
                        cmd.Parameters.Add("@health", System.Data.DbType.Int32).Value = Health;
                        cmd.Parameters.Add("@maxhealth", System.Data.DbType.Int32).Value = MaxHealth;
                        cmd.Parameters.Add("@experience", System.Data.DbType.Int32).Value = Experience;
                        cmd.Parameters.Add("@money", System.Data.DbType.Int32).Value = Money;
                        cmd.Parameters.Add("@armor", System.Data.DbType.Int32).Value = Armor;
                        cmd.Parameters.Add("@hunger", System.Data.DbType.Int32).Value = Hunger;
                        cmd.Parameters.Add("@hydration", System.Data.DbType.Int32).Value = Hydration;
                        cmd.Parameters.Add("@strength", System.Data.DbType.Int32).Value = Strength;
                        cmd.Parameters.Add("@agility", System.Data.DbType.Int32).Value = Agility;
                        cmd.Parameters.Add("@endurance", System.Data.DbType.Int32).Value = Endurance;
                        cmd.Parameters.Add("@stamina", System.Data.DbType.Int32).Value = Stamina;
                        cmd.Parameters.Add("@pistolammo", System.Data.DbType.Int32).Value = PistolAmmo;
                        cmd.Parameters.Add("@assaultammo", System.Data.DbType.Int32).Value = AssaultAmmo;
                        cmd.Parameters.Add("@rocketammo", System.Data.DbType.Int32).Value = RocketAmmo;
                        cmd.Parameters.Add("@grenadeammo", System.Data.DbType.Int32).Value = GrenadeAmmo;
                        cmd.Parameters.Add("@lightradius", System.Data.DbType.Int32).Value = LightRadius;
                        cmd.Parameters.Add("@days", System.Data.DbType.Int32).Value = PlayDays;
                        cmd.Parameters.Add("@hours", System.Data.DbType.Int32).Value = PlayHours;
                        cmd.Parameters.Add("@minutes", System.Data.DbType.Int32).Value = PlayMinutes;
                        cmd.Parameters.Add("@seconds", System.Data.DbType.Int32).Value = PlaySeconds;
                        cmd.Parameters.Add("@ldays", System.Data.DbType.Int32).Value = LifeDay;
                        cmd.Parameters.Add("@lhours", System.Data.DbType.Int32).Value = LifeHour;
                        cmd.Parameters.Add("@lminutes", System.Data.DbType.Int32).Value = LifeMinute;
                        cmd.Parameters.Add("@lseconds", System.Data.DbType.Int32).Value = LifeSecond;
                        cmd.Parameters.Add("@lldays", System.Data.DbType.Int32).Value = LongestLifeDay;
                        cmd.Parameters.Add("@llhours", System.Data.DbType.Int32).Value = LongestLifeHour;
                        cmd.Parameters.Add("@llminutes", System.Data.DbType.Int32).Value = LongestLifeMinute;
                        cmd.Parameters.Add("@llseconds", System.Data.DbType.Int32).Value = LongestLifeSecond;
                        cmd.Parameters.Add("@lastlogged", System.Data.DbType.String).Value = LastLoggedIn;
                        cmd.Parameters.Add("@accountkey", System.Data.DbType.String).Value = AccountKey;
                        cmd.Parameters.Add("@active", System.Data.DbType.String).Value = Active;
                        cmd.ExecuteNonQuery();

                        command = "INSERT INTO MAINWEAPONS";
                        command = command + "(OWNER,NAME,CLIP,MAXCLIP,SPRITE,DAMAGE,ARMOR,TYPE,ATTACKSPEED,RELOADSPEED,HEALTHRESTORE,HUNGERRESTORE,HYDRATERESTORE,STRENGTH,AGILITY,ENDURANCE,STAMINA,AMMOTYPE,VALUE,PROJ,PRICE,RARITY)";
                        command = command + " VALUES ";
                        command = command + "(@owner,@name,@clip,@maxclip,@sprite,@damage,@armor,@type,@attackspeed,@reloadspeed,@healthrestore,@hungerrestore,@hydraterestore,@strength,@agility,@endurance,@stamina,@ammotype,@value,@proj,@price,@rarity);";
                        cmd.CommandText = command;
                        cmd.Parameters.Add("@owner", System.Data.DbType.String).Value = Name;
                        cmd.Parameters.Add("@name", System.Data.DbType.String).Value = mainWeapon.Name;
                        cmd.Parameters.Add("@clip", System.Data.DbType.Int32).Value = mainWeapon.Clip;
                        cmd.Parameters.Add("@maxclip", System.Data.DbType.Int32).Value = mainWeapon.MaxClip;
                        cmd.Parameters.Add("@sprite", System.Data.DbType.Int32).Value = mainWeapon.Sprite;
                        cmd.Parameters.Add("@damage", System.Data.DbType.Int32).Value = mainWeapon.Damage;
                        cmd.Parameters.Add("@armor", System.Data.DbType.Int32).Value = mainWeapon.Armor;
                        cmd.Parameters.Add("@type", System.Data.DbType.Int32).Value = mainWeapon.Type;
                        cmd.Parameters.Add("@attackspeed", System.Data.DbType.Int32).Value = mainWeapon.AttackSpeed;
                        cmd.Parameters.Add("@reloadspeed", System.Data.DbType.Int32).Value = mainWeapon.ReloadSpeed;
                        cmd.Parameters.Add("@healthrestore", System.Data.DbType.Int32).Value = mainWeapon.HealthRestore;
                        cmd.Parameters.Add("@hungerrestore", System.Data.DbType.Int32).Value = mainWeapon.HungerRestore;
                        cmd.Parameters.Add("@hydraterestore", System.Data.DbType.Int32).Value = mainWeapon.HydrateRestore;
                        cmd.Parameters.Add("@strength", System.Data.DbType.Int32).Value = mainWeapon.Strength;
                        cmd.Parameters.Add("@agility", System.Data.DbType.Int32).Value = mainWeapon.Agility;
                        cmd.Parameters.Add("@endurance", System.Data.DbType.Int32).Value = mainWeapon.Endurance;
                        cmd.Parameters.Add("@stamina", System.Data.DbType.Int32).Value = mainWeapon.Stamina;
                        cmd.Parameters.Add("@ammotype", System.Data.DbType.Int32).Value = mainWeapon.ItemAmmoType;
                        cmd.Parameters.Add("@value", System.Data.DbType.Int32).Value = mainWeapon.Value;
                        cmd.Parameters.Add("@proj", System.Data.DbType.Int32).Value = mainWeapon.ProjectileNumber;
                        cmd.Parameters.Add("@price", System.Data.DbType.Int32).Value = mainWeapon.Price;
                        cmd.Parameters.Add("@rarity", System.Data.DbType.Int32).Value = mainWeapon.Rarity;
                        cmd.ExecuteNonQuery();

                        command = "INSERT INTO SECONDARYWEAPONS";
                        command = command + "(OWNER,NAME,CLIP,MAXCLIP,SPRITE,DAMAGE,ARMOR,TYPE,ATTACKSPEED,RELOADSPEED,HEALTHRESTORE,HUNGERRESTORE,HYDRATERESTORE,STRENGTH,AGILITY,ENDURANCE,STAMINA,AMMOTYPE,VALUE,PROJ,PRICE,RARITY)";
                        command = command + " VALUES ";
                        command = command + "(@owner,@name,@clip,@maxclip,@sprite,@damage,@armor,@type,@attackspeed,@reloadspeed,@healthrestore,@hungerrestore,@hydraterestore,@strength,@agility,@endurance,@stamina,@ammotype,@value,@proj,@price,@rarity);";
                        cmd.CommandText = command;
                        cmd.Parameters.Add("@owner", System.Data.DbType.String).Value = Name;
                        cmd.Parameters.Add("@name", System.Data.DbType.String).Value = offWeapon.Name;
                        cmd.Parameters.Add("@clip", System.Data.DbType.Int32).Value = offWeapon.Clip;
                        cmd.Parameters.Add("@maxclip", System.Data.DbType.Int32).Value = offWeapon.MaxClip;
                        cmd.Parameters.Add("@sprite", System.Data.DbType.Int32).Value = offWeapon.Sprite;
                        cmd.Parameters.Add("@damage", System.Data.DbType.Int32).Value = offWeapon.Damage;
                        cmd.Parameters.Add("@armor", System.Data.DbType.Int32).Value = offWeapon.Armor;
                        cmd.Parameters.Add("@type", System.Data.DbType.Int32).Value = offWeapon.Type;
                        cmd.Parameters.Add("@attackspeed", System.Data.DbType.Int32).Value = offWeapon.AttackSpeed;
                        cmd.Parameters.Add("@reloadspeed", System.Data.DbType.Int32).Value = offWeapon.ReloadSpeed;
                        cmd.Parameters.Add("@healthrestore", System.Data.DbType.Int32).Value = offWeapon.HealthRestore;
                        cmd.Parameters.Add("@hungerrestore", System.Data.DbType.Int32).Value = offWeapon.HungerRestore;
                        cmd.Parameters.Add("@hydraterestore", System.Data.DbType.Int32).Value = mainWeapon.HydrateRestore;
                        cmd.Parameters.Add("@strength", System.Data.DbType.Int32).Value = offWeapon.Strength;
                        cmd.Parameters.Add("@agility", System.Data.DbType.Int32).Value = offWeapon.Agility;
                        cmd.Parameters.Add("@endurance", System.Data.DbType.Int32).Value = offWeapon.Endurance;
                        cmd.Parameters.Add("@stamina", System.Data.DbType.Int32).Value = offWeapon.Stamina;
                        cmd.Parameters.Add("@ammotype", System.Data.DbType.Int32).Value = offWeapon.ItemAmmoType;
                        cmd.Parameters.Add("@value", System.Data.DbType.Int32).Value = offWeapon.Value;
                        cmd.Parameters.Add("@proj", System.Data.DbType.Int32).Value = offWeapon.ProjectileNumber;
                        cmd.Parameters.Add("@price", System.Data.DbType.Int32).Value = offWeapon.Price;
                        cmd.Parameters.Add("@rarity", System.Data.DbType.Int32).Value = offWeapon.Rarity;
                        cmd.ExecuteNonQuery();

                        command = "INSERT INTO EQUIPMENT";
                        command = command + "(OWNER,ID,NAME,CLIP,MAXCLIP,SPRITE,DAMAGE,ARMOR,TYPE,ATTACKSPEED,RELOADSPEED,HEALTHRESTORE,HUNGERRESTORE,HYDRATERESTORE,STRENGTH,AGILITY,ENDURANCE,STAMINA,AMMOTYPE,VALUE,PROJ,PRICE,RARITY)";
                        command = command + " VALUES ";
                        command = command + "(@owner,@id,@name,@clip,@maxclip,@sprite,@damage,@armor,@type,@attackspeed,@reloadspeed,@healthrestore,@hungerrestore,@hydraterestore,@strength,@agility,@endurance,@stamina,@ammotype,@value,@proj,@price,@rarity);";
                        cmd.CommandText = command;
                        cmd.Parameters.Add("@owner", System.Data.DbType.String).Value = Name;
                        cmd.Parameters.Add("@id", System.Data.DbType.Int32).Value = 0;
                        cmd.Parameters.Add("@name", System.Data.DbType.String).Value = Chest.Name;
                        cmd.Parameters.Add("@clip", System.Data.DbType.Int32).Value = Chest.Clip;
                        cmd.Parameters.Add("@maxclip", System.Data.DbType.Int32).Value = Chest.MaxClip;
                        cmd.Parameters.Add("@sprite", System.Data.DbType.Int32).Value = Chest.Sprite;
                        cmd.Parameters.Add("@damage", System.Data.DbType.Int32).Value = Chest.Damage;
                        cmd.Parameters.Add("@armor", System.Data.DbType.Int32).Value = Chest.Armor;
                        cmd.Parameters.Add("@type", System.Data.DbType.Int32).Value = Chest.Type;
                        cmd.Parameters.Add("@attackspeed", System.Data.DbType.Int32).Value = Chest.AttackSpeed;
                        cmd.Parameters.Add("@reloadspeed", System.Data.DbType.Int32).Value = Chest.ReloadSpeed;
                        cmd.Parameters.Add("@healthrestore", System.Data.DbType.Int32).Value = Chest.HealthRestore;
                        cmd.Parameters.Add("@hungerrestore", System.Data.DbType.Int32).Value = Chest.HungerRestore;
                        cmd.Parameters.Add("@hydraterestore", System.Data.DbType.Int32).Value = Chest.HydrateRestore;
                        cmd.Parameters.Add("@strength", System.Data.DbType.Int32).Value = Chest.Strength;
                        cmd.Parameters.Add("@agility", System.Data.DbType.Int32).Value = Chest.Agility;
                        cmd.Parameters.Add("@endurance", System.Data.DbType.Int32).Value = Chest.Endurance;
                        cmd.Parameters.Add("@stamina", System.Data.DbType.Int32).Value = Chest.Stamina;
                        cmd.Parameters.Add("@ammotype", System.Data.DbType.Int32).Value = Chest.ItemAmmoType;
                        cmd.Parameters.Add("@value", System.Data.DbType.Int32).Value = Chest.Value;
                        cmd.Parameters.Add("@proj", System.Data.DbType.Int32).Value = Chest.ProjectileNumber;
                        cmd.Parameters.Add("@price", System.Data.DbType.Int32).Value = Chest.Price;
                        cmd.Parameters.Add("@rarity", System.Data.DbType.Int32).Value = Chest.Rarity;
                        cmd.ExecuteNonQuery();

                        command = "INSERT INTO EQUIPMENT";
                        command = command + "(OWNER,ID,NAME,CLIP,MAXCLIP,SPRITE,DAMAGE,ARMOR,TYPE,ATTACKSPEED,RELOADSPEED,HEALTHRESTORE,HUNGERRESTORE,HYDRATERESTORE,STRENGTH,AGILITY,ENDURANCE,STAMINA,AMMOTYPE,VALUE,PROJ,PRICE,RARITY)";
                        command = command + " VALUES ";
                        command = command + "(@owner,@id,@name,@clip,@maxclip,@sprite,@damage,@armor,@type,@attackspeed,@reloadspeed,@healthrestore,@hungerrestore,@hydraterestore,@strength,@agility,@endurance,@stamina,@ammotype,@value,@proj,@price,@rarity);";
                        cmd.CommandText = command;
                        cmd.Parameters.Add("@owner", System.Data.DbType.String).Value = Name;
                        cmd.Parameters.Add("@id", System.Data.DbType.Int32).Value = 1;
                        cmd.Parameters.Add("@name", System.Data.DbType.String).Value = Legs.Name;
                        cmd.Parameters.Add("@clip", System.Data.DbType.Int32).Value = Legs.Clip;
                        cmd.Parameters.Add("@maxclip", System.Data.DbType.Int32).Value = Legs.MaxClip;
                        cmd.Parameters.Add("@sprite", System.Data.DbType.Int32).Value = Legs.Sprite;
                        cmd.Parameters.Add("@damage", System.Data.DbType.Int32).Value = Legs.Damage;
                        cmd.Parameters.Add("@armor", System.Data.DbType.Int32).Value = Legs.Armor;
                        cmd.Parameters.Add("@type", System.Data.DbType.Int32).Value = Legs.Type;
                        cmd.Parameters.Add("@attackspeed", System.Data.DbType.Int32).Value = Legs.AttackSpeed;
                        cmd.Parameters.Add("@reloadspeed", System.Data.DbType.Int32).Value = Legs.ReloadSpeed;
                        cmd.Parameters.Add("@healthrestore", System.Data.DbType.Int32).Value = Legs.HealthRestore;
                        cmd.Parameters.Add("@hungerrestore", System.Data.DbType.Int32).Value = Legs.HungerRestore;
                        cmd.Parameters.Add("@hydraterestore", System.Data.DbType.Int32).Value = Legs.HydrateRestore;
                        cmd.Parameters.Add("@strength", System.Data.DbType.Int32).Value = Legs.Strength;
                        cmd.Parameters.Add("@agility", System.Data.DbType.Int32).Value = Legs.Agility;
                        cmd.Parameters.Add("@endurance", System.Data.DbType.Int32).Value = Legs.Endurance;
                        cmd.Parameters.Add("@stamina", System.Data.DbType.Int32).Value = Legs.Stamina;
                        cmd.Parameters.Add("@ammotype", System.Data.DbType.Int32).Value = Legs.ItemAmmoType;
                        cmd.Parameters.Add("@value", System.Data.DbType.Int32).Value = Legs.Value;
                        cmd.Parameters.Add("@proj", System.Data.DbType.Int32).Value = Legs.ProjectileNumber;
                        cmd.Parameters.Add("@price", System.Data.DbType.Int32).Value = Legs.Price;
                        cmd.Parameters.Add("@rarity", System.Data.DbType.Int32).Value = Legs.Rarity;
                        cmd.ExecuteNonQuery();

                        command = "INSERT INTO EQUIPMENT";
                        command = command + "(OWNER,ID,NAME,CLIP,MAXCLIP,SPRITE,DAMAGE,ARMOR,TYPE,ATTACKSPEED,RELOADSPEED,HEALTHRESTORE,HUNGERRESTORE,HYDRATERESTORE,STRENGTH,AGILITY,ENDURANCE,STAMINA,AMMOTYPE,VALUE,PROJ,PRICE,RARITY)";
                        command = command + " VALUES ";
                        command = command + "(@owner,@id,@name,@clip,@maxclip,@sprite,@damage,@armor,@type,@attackspeed,@reloadspeed,@healthrestore,@hungerrestore,@hydraterestore,@strength,@agility,@endurance,@stamina,@ammotype,@value,@proj,@price,@rarity);";
                        cmd.CommandText = command;
                        cmd.Parameters.Add("@owner", System.Data.DbType.String).Value = Name;
                        cmd.Parameters.Add("@id", System.Data.DbType.Int32).Value = 2;
                        cmd.Parameters.Add("@name", System.Data.DbType.String).Value = Feet.Name;
                        cmd.Parameters.Add("@clip", System.Data.DbType.Int32).Value = Feet.Clip;
                        cmd.Parameters.Add("@maxclip", System.Data.DbType.Int32).Value = Feet.MaxClip;
                        cmd.Parameters.Add("@sprite", System.Data.DbType.Int32).Value = Feet.Sprite;
                        cmd.Parameters.Add("@damage", System.Data.DbType.Int32).Value = Feet.Damage;
                        cmd.Parameters.Add("@armor", System.Data.DbType.Int32).Value = Feet.Armor;
                        cmd.Parameters.Add("@type", System.Data.DbType.Int32).Value = Feet.Type;
                        cmd.Parameters.Add("@attackspeed", System.Data.DbType.Int32).Value = Feet.AttackSpeed;
                        cmd.Parameters.Add("@reloadspeed", System.Data.DbType.Int32).Value = Feet.ReloadSpeed;
                        cmd.Parameters.Add("@healthrestore", System.Data.DbType.Int32).Value = Feet.HealthRestore;
                        cmd.Parameters.Add("@hungerrestore", System.Data.DbType.Int32).Value = Feet.HungerRestore;
                        cmd.Parameters.Add("@hydraterestore", System.Data.DbType.Int32).Value = Feet.HydrateRestore;
                        cmd.Parameters.Add("@strength", System.Data.DbType.Int32).Value = Feet.Strength;
                        cmd.Parameters.Add("@agility", System.Data.DbType.Int32).Value = Feet.Agility;
                        cmd.Parameters.Add("@endurance", System.Data.DbType.Int32).Value = Feet.Endurance;
                        cmd.Parameters.Add("@stamina", System.Data.DbType.Int32).Value = Feet.Stamina;
                        cmd.Parameters.Add("@ammotype", System.Data.DbType.Int32).Value = Feet.ItemAmmoType;
                        cmd.Parameters.Add("@value", System.Data.DbType.Int32).Value = Feet.Value;
                        cmd.Parameters.Add("@proj", System.Data.DbType.Int32).Value = Feet.ProjectileNumber;
                        cmd.Parameters.Add("@price", System.Data.DbType.Int32).Value = Feet.Price;
                        cmd.Parameters.Add("@rarity", System.Data.DbType.Int32).Value = Feet.Rarity;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void UpdateAccountStatusInDatabase()
        {
            if (DBType == SQL_DATABASE_REMOTE.ToString())
            {
                string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
                using (var sql = new SqlConnection(connection))
                {
                    sql.Open();
                    string command;
                    command = "UPDATE PLAYERS SET ACTIVE = 'Y' WHERE ID = @id";
                    using (var cmd = new SqlCommand(command, sql))
                    {
                        cmd.Parameters.Add(new SqlParameter("id", System.Data.SqlDbType.Int)).Value = Id;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            else
            {
                using (var conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
                {
                    using (var cmd = new SQLiteCommand(conn))
                    {
                        conn.Open();
                        string command;
                        command = "UPDATE PLAYERS SET ACTIVE = 'Y' WHERE NAME = @name";
                        cmd.CommandText = command;
                        cmd.Parameters.Add("@name", System.Data.DbType.String).Value = Name;
                    }
                }
            }
        }

        public void SavePlayerToDatabase()
        {
            if (DBType == SQL_DATABASE_REMOTE.ToString())
            {
                string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
                using (var sql = new SqlConnection(connection))
                {
                    sql.Open();
                    string command;
                    command = "UPDATE PLAYERS SET ";
                    command += "NAME = @name, PASSWORD = @password, EMAILADDRESS = @email, X = @x, Y = @y, MAP = @map, DIRECTION = @direction, AIMDIRECTION = @aimdirection, SPRITE = @sprite, LEVEL = @level, POINTS = @points, HEALTH = @health, MAXHEALTH = @maxhealth, ";
                    command += "EXPERIENCE = @experience, MONEY = @money, ARMOR = @armor, HUNGER = @hunger, HYDRATION = @hydrate, STRENGTH = @strength, AGILITY = @agility, ENDURANCE = @endurance, STAMINA = @stamina, ";
                    command += "PISTOLAMMO = @pistolammo, ASSAULTAMMO = @assaultammo, ROCKETAMMO = @rocketammo, GRENADEAMMO = @grenadeammo, LIGHTRADIUS = @lightradius, ";
                    command += "DAYS = @days, HOURS = @hours, MINUTES = @minutes, SECONDS = @seconds, LDAYS = @ldays, LHOURS = @lhours, LMINUTES = @lminutes, LSECONDS = @lseconds, ";
                    command += "LLDAYS = @lldays, LLHOURS = @llhours, LLMINUTES = @llminutes, LLSECONDS = @llseconds, LASTLOGGED = @lastlogged, ACCOUNTKEY = @accountkey, ACTIVE = @active WHERE ID = @id";
                    using (var cmd = new SqlCommand(command, sql))
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
                        cmd.Parameters.Add(new SqlParameter("@points", System.Data.DbType.Int32)).Value = Points;
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
                        cmd.Parameters.Add(new SqlParameter("@lastlogged", System.Data.DbType.String)).Value = LastLoggedIn;
                        cmd.Parameters.Add(new SqlParameter("@accountkey", System.Data.DbType.String)).Value = AccountKey;
                        cmd.Parameters.Add(new SqlParameter("@active", System.Data.DbType.String)).Value = Active;
                        cmd.ExecuteNonQuery();
                    }

                    command = "UPDATE MAINWEAPONS SET ";
                    command += "NAME = @name, CLIP = @clip, MAXCLIP = @maxclip, SPRITE = @sprite, DAMAGE = @damage, ARMOR = @armor, TYPE = @type, ATTACKSPEED = @attackspeed, RELOADSPEED = @reloadspeed, ";
                    command += "HEALTHRESTORE = @healthrestore, HUNGERRESTORE = @hungerrestore, HYDRATERESTORE = @hydraterestore, STRENGTH = @strength, AGILITY = @agility, ENDURANCE = @endurance, STAMINA = @stamina, AMMOTYPE = @ammotype, ";
                    command += "VALUE = @value, PROJ = @proj, PRICE = @price, RARITY = @rarity WHERE OWNER = @owner";
                    using (var cmd = new SqlCommand(command, sql))
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

                    command = "UPDATE SECONDARYWEAPONS SET ";
                    command += "NAME = @name, CLIP = @clip, MAXCLIP = @maxclip, SPRITE = @sprite, DAMAGE = @damage, ARMOR = @armor, TYPE = @type, ATTACKSPEED = @attackspeed, RELOADSPEED = @reloadspeed, ";
                    command += "HEALTHRESTORE = @healthrestore, HUNGERRESTORE = @hungerrestore, HYDRATERESTORE = @hydraterestore, STRENGTH = @strength, AGILITY = @agility, ENDURANCE = @endurance, STAMINA = @stamina, AMMOTYPE = @ammotype, ";
                    command += "VALUE = @value, PROJ = @proj, PRICE = @price, RARITY = @rarity WHERE OWNER = @owner";
                    using (var cmd = new SqlCommand(command, sql))
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

                    command = "UPDATE EQUIPMENT SET ";
                    command += "NAME = @name, CLIP = @clip, MAXCLIP = @maxclip, SPRITE = @sprite, DAMAGE = @damage, ARMOR = @armor, TYPE = @type, ATTACKSPEED = @attackspeed, RELOADSPEED = @reloadspeed, ";
                    command += "HEALTHRESTORE = @healthrestore, HUNGERRESTORE = @hungerrestore, HYDRATERESTORE = @hydraterestore, STRENGTH = @strength, AGILITY = @agility, ENDURANCE = @endurance, STAMINA = @stamina, AMMOTYPE = @ammotype, ";
                    command += "VALUE = @value, PROJ = @proj, PRICE = @price, RARITY = @rarity WHERE OWNER = @owner AND SLOT = @id";
                    using (var cmd = new SqlCommand(command, sql))
                    {
                        cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                        cmd.Parameters.Add(new SqlParameter("@id", System.Data.DbType.Int32)).Value = 0;
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

                    command = "UPDATE EQUIPMENT SET ";
                    command += "NAME = @name, CLIP = @clip, MAXCLIP = @maxclip, SPRITE = @sprite, DAMAGE = @damage, ARMOR = @armor, TYPE = @type, ATTACKSPEED = @attackspeed, RELOADSPEED = @reloadspeed, ";
                    command += "HEALTHRESTORE = @healthrestore, HUNGERRESTORE = @hungerrestore, HYDRATERESTORE = @hydraterestore, STRENGTH = @strength, AGILITY = @agility, ENDURANCE = @endurance, STAMINA = @stamina, AMMOTYPE = @ammotype, ";
                    command += "VALUE = @value, PROJ = @proj, PRICE = @price, RARITY = @rarity WHERE OWNER = @owner AND SLOT = @id";
                    using (var cmd = new SqlCommand(command, sql))
                    {
                        cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                        cmd.Parameters.Add(new SqlParameter("@id", System.Data.DbType.Int32)).Value = 1;
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

                    command = "UPDATE EQUIPMENT SET ";
                    command += "NAME = @name, CLIP = @clip, MAXCLIP = @maxclip, SPRITE = @sprite, DAMAGE = @damage, ARMOR = @armor, TYPE = @type, ATTACKSPEED = @attackspeed, RELOADSPEED = @reloadspeed, ";
                    command += "HEALTHRESTORE = @healthrestore, HUNGERRESTORE = @hungerrestore, HYDRATERESTORE = @hydraterestore, STRENGTH = @strength, AGILITY = @agility, ENDURANCE = @endurance, STAMINA = @stamina, AMMOTYPE = @ammotype, ";
                    command += "VALUE = @value, PROJ = @proj, PRICE = @price, RARITY = @rarity WHERE OWNER = @owner AND SLOT = @id";
                    using (var cmd = new SqlCommand(command, sql))
                    {
                        cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                        cmd.Parameters.Add(new SqlParameter("@id", System.Data.DbType.Int32)).Value = 2;
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

                    command = "DELETE FROM INVENTORY WHERE OWNER=@owner;";
                    using (var cmd = new SqlCommand(command, sql))
                    {
                        cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                        cmd.ExecuteNonQuery();
                    }

                    int n = 0;
                    for (int i = 0; i < 25; i++)
                    {
                        if (Backpack[i].Name != "None")
                        {
                            command = "INSERT INTO INVENTORY ";
                            command += "(OWNER,SLOT,NAME,SPRITE,DAMAGE,ARMOR,TYPE,ATTACKSPEED,RELOADSPEED,HEALTHRESTORE,HUNGERRESTORE,HYDRATERESTORE,STRENGTH,AGILITY,ENDURANCE,STAMINA,CLIP,MAXCLIP,AMMOTYPE,VALUE,PROJ,PRICE,RARITY) VALUES ";
                            command += "(@owner,@slot,@name,@sprite,@damage,@armor,@type,@attackspeed,@reloadspeed,@healthrestore,@hungerrestore,@hydraterestore,@strength,@agility,@endurance,@stamina,@clip,@maxclip,@ammotype,@value,@proj,@price,@rarity)";
                            using (var cmd = new SqlCommand(command, sql))
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

                    command = "DELETE FROM BANK WHERE OWNER=@owner";
                    using (var cmd = new SqlCommand(command, sql))
                    {
                        cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                        cmd.ExecuteNonQuery();
                    }
                    
                    int m = 0;
                    for (int i = 0; i < 50; i++)
                    {
                        if (Bank[i].Name != "None")
                        {
                            command = "INSERT INTO BANK ";
                            command += "(OWNER,SLOT,NAME,SPRITE,DAMAGE,ARMOR,TYPE,ATTACKSPEED,RELOADSPEED,HEALTHRESTORE,HUNGERRESTORE,HYDRATERESTORE,STRENGTH,AGILITY,ENDURANCE,STAMINA,CLIP,MAXCLIP,AMMOTYPE,VALUE,PROJ,PRICE,RARITY) VALUES ";
                            command += "(@owner,@slot,@name,@sprite,@damage,@armor,@type,@attackspeed,@reloadspeed,@healthrestore,@hungerrestore,@hydraterestore,@strength,@agility,@endurance,@stamina,@clip,@maxclip,@ammotype,@value,@proj,@price,@rarity)";
                            using (var cmd = new SqlCommand(command, sql))
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
            else
            {
                using (var conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
                {
                    using (var cmd = new SQLiteCommand(conn))
                    {
                        conn.Open();
                        string command;
                        command = "UPDATE PLAYERS SET ";
                        command = command + "NAME = @name, PASSWORD = @password, EMAILADDRESS = @email, X = @x, Y = @y, MAP = @map, DIRECTION = @direction, AIMDIRECTION = @aimdirection, SPRITE = @sprite, LEVEL = @level, POINTS = @points, HEALTH = @health, MAXHEALTH = @maxhealth, ";
                        command = command + "EXPERIENCE = @experience, MONEY = @money, ARMOR = @armor, HUNGER = @hunger, HYDRATION = @hydrate, STRENGTH = @strength, AGILITY = @agility, ENDURANCE = @endurance, STAMINA = @stamina, ";
                        command = command + "PISTOLAMMO = @pistolammo, ASSAULTAMMO = @assaultammo, ROCKETAMMO = @rocketammo, GRENADEAMMO = @grenadeammo, LIGHTRADIUS = @lightradius, ";
                        command = command + "DAYS = @days, HOURS = @hours, MINUTES = @minutes, SECONDS = @seconds, LDAYS = @ldays, LHOURS = @lhours, LMINUTES = @lminutes, LSECONDS = @lseconds, ";
                        command = command + "LLDAYS = @lldays, LLHOURS = @llhours, LLMINUTES = @llminutes, LLSECONDS = @llseconds, LASTLOGGED = @lastlogged, ACCOUNTKEY = @accountkey, ACTIVE = @active WHERE NAME = '" + Name + "';";
                        cmd.CommandText = command;
                        cmd.Parameters.Add("@name", System.Data.DbType.String).Value = Name;
                        cmd.Parameters.Add("@password", System.Data.DbType.String).Value = Pass;
                        cmd.Parameters.Add("@email", System.Data.DbType.String).Value = EmailAddress;
                        cmd.Parameters.Add("@x", System.Data.DbType.Int32).Value = X;
                        cmd.Parameters.Add("@y", System.Data.DbType.Int32).Value = Y;
                        cmd.Parameters.Add("@map", System.Data.DbType.Int32).Value = Map;
                        cmd.Parameters.Add("@direction", System.Data.DbType.Int32).Value = Direction;
                        cmd.Parameters.Add("@aimdirection", System.Data.DbType.Int32).Value = AimDirection;
                        cmd.Parameters.Add("@sprite", System.Data.DbType.Int32).Value = Sprite;
                        cmd.Parameters.Add("@level", System.Data.DbType.Int32).Value = Level;
                        cmd.Parameters.Add("@points", System.Data.DbType.Int32).Value = Points;
                        cmd.Parameters.Add("@health", System.Data.DbType.Int32).Value = Health;
                        cmd.Parameters.Add("@maxhealth", System.Data.DbType.Int32).Value = MaxHealth;
                        cmd.Parameters.Add("@experience", System.Data.DbType.Int32).Value = Experience;
                        cmd.Parameters.Add("@money", System.Data.DbType.Int32).Value = Money;
                        cmd.Parameters.Add("@armor", System.Data.DbType.Int32).Value = Armor;
                        cmd.Parameters.Add("@hunger", System.Data.DbType.Int32).Value = Hunger;
                        cmd.Parameters.Add("@hydrate", System.Data.DbType.Int32).Value = Hydration;
                        cmd.Parameters.Add("@strength", System.Data.DbType.Int32).Value = Strength;
                        cmd.Parameters.Add("@agility", System.Data.DbType.Int32).Value = Agility;
                        cmd.Parameters.Add("@endurance", System.Data.DbType.Int32).Value = Endurance;
                        cmd.Parameters.Add("@stamina", System.Data.DbType.Int32).Value = Stamina;
                        cmd.Parameters.Add("@pistolammo", System.Data.DbType.Int32).Value = PistolAmmo;
                        cmd.Parameters.Add("@assaultammo", System.Data.DbType.Int32).Value = AssaultAmmo;
                        cmd.Parameters.Add("@rocketammo", System.Data.DbType.Int32).Value = RocketAmmo;
                        cmd.Parameters.Add("@grenadeammo", System.Data.DbType.Int32).Value = GrenadeAmmo;
                        cmd.Parameters.Add("@lightradius", System.Data.DbType.Int32).Value = LightRadius;
                        cmd.Parameters.Add("@days", System.Data.DbType.Int32).Value = PlayDays;
                        cmd.Parameters.Add("@hours", System.Data.DbType.Int32).Value = PlayHours;
                        cmd.Parameters.Add("@minutes", System.Data.DbType.Int32).Value = PlayMinutes;
                        cmd.Parameters.Add("@seconds", System.Data.DbType.Int32).Value = PlaySeconds;
                        cmd.Parameters.Add("@ldays", System.Data.DbType.Int32).Value = LifeDay;
                        cmd.Parameters.Add("@lhours", System.Data.DbType.Int32).Value = LifeHour;
                        cmd.Parameters.Add("@lminutes", System.Data.DbType.Int32).Value = LifeMinute;
                        cmd.Parameters.Add("@lseconds", System.Data.DbType.Int32).Value = LifeSecond;
                        cmd.Parameters.Add("@lldays", System.Data.DbType.Int32).Value = LongestLifeDay;
                        cmd.Parameters.Add("@llhours", System.Data.DbType.Int32).Value = LongestLifeHour;
                        cmd.Parameters.Add("@llminutes", System.Data.DbType.Int32).Value = LongestLifeMinute;
                        cmd.Parameters.Add("@llseconds", System.Data.DbType.Int32).Value = LongestLifeSecond;
                        cmd.Parameters.Add("@lastlogged", System.Data.DbType.String).Value = LastLoggedIn;
                        cmd.Parameters.Add("@accountkey", System.Data.DbType.String).Value = AccountKey;
                        cmd.Parameters.Add("@active", System.Data.DbType.String).Value = Active;
                        cmd.ExecuteNonQuery();

                        command = "UPDATE MAINWEAPONS SET ";
                        command = command + "NAME = @name, CLIP = @clip, MAXCLIP = @maxclip, SPRITE = @sprite, DAMAGE = @damage, ARMOR = @armor, TYPE = @type, ATTACKSPEED = @attackspeed, RELOADSPEED = @reloadspeed, ";
                        command = command + "HEALTHRESTORE = @healthrestore, HUNGERRESTORE = @hungerrestore, HYDRATERESTORE = @hydraterestore, STRENGTH = @strength, AGILITY = @agility, ENDURANCE = @endurance, STAMINA = @stamina, AMMOTYPE = @ammotype, ";
                        command = command + "VALUE = @value, PROJ = @proj, PRICE = @price, RARITY = @rarity WHERE OWNER = '" + Name + "';";
                        cmd.CommandText = command;
                        cmd.Parameters.Add("@owner", System.Data.DbType.String).Value = Name;
                        cmd.Parameters.Add("@name", System.Data.DbType.String).Value = mainWeapon.Name;
                        cmd.Parameters.Add("@clip", System.Data.DbType.Int32).Value = mainWeapon.Clip;
                        cmd.Parameters.Add("@maxclip", System.Data.DbType.Int32).Value = mainWeapon.MaxClip;
                        cmd.Parameters.Add("@sprite", System.Data.DbType.Int32).Value = mainWeapon.Sprite;
                        cmd.Parameters.Add("@damage", System.Data.DbType.Int32).Value = mainWeapon.Damage;
                        cmd.Parameters.Add("@armor", System.Data.DbType.Int32).Value = mainWeapon.Armor;
                        cmd.Parameters.Add("@type", System.Data.DbType.Int32).Value = mainWeapon.Type;
                        cmd.Parameters.Add("@attackspeed", System.Data.DbType.Int32).Value = mainWeapon.AttackSpeed;
                        cmd.Parameters.Add("@reloadspeed", System.Data.DbType.Int32).Value = mainWeapon.ReloadSpeed;
                        cmd.Parameters.Add("@healthrestore", System.Data.DbType.Int32).Value = mainWeapon.HealthRestore;
                        cmd.Parameters.Add("@hungerrestore", System.Data.DbType.Int32).Value = mainWeapon.HungerRestore;
                        cmd.Parameters.Add("@hydraterestore", System.Data.DbType.Int32).Value = mainWeapon.HydrateRestore;
                        cmd.Parameters.Add("@strength", System.Data.DbType.Int32).Value = mainWeapon.Strength;
                        cmd.Parameters.Add("@agility", System.Data.DbType.Int32).Value = mainWeapon.Agility;
                        cmd.Parameters.Add("@endurance", System.Data.DbType.Int32).Value = mainWeapon.Endurance;
                        cmd.Parameters.Add("@stamina", System.Data.DbType.Int32).Value = mainWeapon.Stamina;
                        cmd.Parameters.Add("@ammotype", System.Data.DbType.Int32).Value = mainWeapon.ItemAmmoType;
                        cmd.Parameters.Add("@value", System.Data.DbType.Int32).Value = mainWeapon.Value;
                        cmd.Parameters.Add("@proj", System.Data.DbType.Int32).Value = mainWeapon.ProjectileNumber;
                        cmd.Parameters.Add("@price", System.Data.DbType.Int32).Value = mainWeapon.Price;
                        cmd.Parameters.Add("@rarity", System.Data.DbType.Int32).Value = mainWeapon.Rarity;
                        cmd.ExecuteNonQuery();

                        command = "UPDATE SECONDARYWEAPONS SET ";
                        command = command + "NAME = @name, CLIP = @clip, MAXCLIP = @maxclip, SPRITE = @sprite, DAMAGE = @damage, ARMOR = @armor, TYPE = @type, ATTACKSPEED = @attackspeed, RELOADSPEED = @reloadspeed, ";
                        command = command + "HEALTHRESTORE = @healthrestore, HUNGERRESTORE = @hungerrestore, HYDRATERESTORE = @hydraterestore, STRENGTH = @strength, AGILITY = @agility, ENDURANCE = @endurance, STAMINA = @stamina, AMMOTYPE = @ammotype, ";
                        command = command + "VALUE = @value, PROJ = @proj, PRICE = @price, RARITY = @rarity WHERE OWNER = '" + Name + "';";
                        cmd.CommandText = command;
                        cmd.Parameters.Add("@owner", System.Data.DbType.String).Value = Name;
                        cmd.Parameters.Add("@name", System.Data.DbType.String).Value = offWeapon.Name;
                        cmd.Parameters.Add("@clip", System.Data.DbType.Int32).Value = offWeapon.Clip;
                        cmd.Parameters.Add("@maxclip", System.Data.DbType.Int32).Value = offWeapon.MaxClip;
                        cmd.Parameters.Add("@sprite", System.Data.DbType.Int32).Value = offWeapon.Sprite;
                        cmd.Parameters.Add("@damage", System.Data.DbType.Int32).Value = offWeapon.Damage;
                        cmd.Parameters.Add("@armor", System.Data.DbType.Int32).Value = offWeapon.Armor;
                        cmd.Parameters.Add("@type", System.Data.DbType.Int32).Value = offWeapon.Type;
                        cmd.Parameters.Add("@attackspeed", System.Data.DbType.Int32).Value = offWeapon.AttackSpeed;
                        cmd.Parameters.Add("@reloadspeed", System.Data.DbType.Int32).Value = offWeapon.ReloadSpeed;
                        cmd.Parameters.Add("@healthrestore", System.Data.DbType.Int32).Value = offWeapon.HealthRestore;
                        cmd.Parameters.Add("@hungerrestore", System.Data.DbType.Int32).Value = offWeapon.HungerRestore;
                        cmd.Parameters.Add("@hydraterestore", System.Data.DbType.Int32).Value = mainWeapon.HydrateRestore;
                        cmd.Parameters.Add("@strength", System.Data.DbType.Int32).Value = offWeapon.Strength;
                        cmd.Parameters.Add("@agility", System.Data.DbType.Int32).Value = offWeapon.Agility;
                        cmd.Parameters.Add("@endurance", System.Data.DbType.Int32).Value = offWeapon.Endurance;
                        cmd.Parameters.Add("@stamina", System.Data.DbType.Int32).Value = offWeapon.Stamina;
                        cmd.Parameters.Add("@ammotype", System.Data.DbType.Int32).Value = offWeapon.ItemAmmoType;
                        cmd.Parameters.Add("@value", System.Data.DbType.Int32).Value = offWeapon.Value;
                        cmd.Parameters.Add("@proj", System.Data.DbType.Int32).Value = offWeapon.ProjectileNumber;
                        cmd.Parameters.Add("@price", System.Data.DbType.Int32).Value = offWeapon.Price;
                        cmd.Parameters.Add("@rarity", System.Data.DbType.Int32).Value = offWeapon.Rarity;
                        cmd.ExecuteNonQuery();

                        command = "UPDATE EQUIPMENT SET ";
                        command = command + "NAME = @name, CLIP = @clip, MAXCLIP = @maxclip, SPRITE = @sprite, DAMAGE = @damage, ARMOR = @armor, TYPE = @type, ATTACKSPEED = @attackspeed, RELOADSPEED = @reloadspeed, ";
                        command = command + "HEALTHRESTORE = @healthrestore, HUNGERRESTORE = @hungerrestore, HYDRATERESTORE = @hydraterestore, STRENGTH = @strength, AGILITY = @agility, ENDURANCE = @endurance, STAMINA = @stamina, AMMOTYPE = @ammotype, ";
                        command = command + "VALUE = @value, PROJ = @proj, PRICE = @price, RARITY = @rarity WHERE OWNER = '" + Name + "' AND ID = 0;";
                        cmd.CommandText = command;
                        cmd.Parameters.Add("@name", System.Data.DbType.String).Value = Chest.Name;
                        cmd.Parameters.Add("@clip", System.Data.DbType.Int32).Value = Chest.Clip;
                        cmd.Parameters.Add("@maxclip", System.Data.DbType.Int32).Value = Chest.MaxClip;
                        cmd.Parameters.Add("@sprite", System.Data.DbType.Int32).Value = Chest.Sprite;
                        cmd.Parameters.Add("@damage", System.Data.DbType.Int32).Value = Chest.Damage;
                        cmd.Parameters.Add("@armor", System.Data.DbType.Int32).Value = Chest.Armor;
                        cmd.Parameters.Add("@type", System.Data.DbType.Int32).Value = Chest.Type;
                        cmd.Parameters.Add("@attackspeed", System.Data.DbType.Int32).Value = Chest.AttackSpeed;
                        cmd.Parameters.Add("@reloadspeed", System.Data.DbType.Int32).Value = Chest.ReloadSpeed;
                        cmd.Parameters.Add("@healthrestore", System.Data.DbType.Int32).Value = Chest.HealthRestore;
                        cmd.Parameters.Add("@hungerrestore", System.Data.DbType.Int32).Value = Chest.HungerRestore;
                        cmd.Parameters.Add("@hydraterestore", System.Data.DbType.Int32).Value = Chest.HydrateRestore;
                        cmd.Parameters.Add("@strength", System.Data.DbType.Int32).Value = Chest.Strength;
                        cmd.Parameters.Add("@agility", System.Data.DbType.Int32).Value = Chest.Agility;
                        cmd.Parameters.Add("@endurance", System.Data.DbType.Int32).Value = Chest.Endurance;
                        cmd.Parameters.Add("@stamina", System.Data.DbType.Int32).Value = Chest.Stamina;
                        cmd.Parameters.Add("@ammotype", System.Data.DbType.Int32).Value = Chest.ItemAmmoType;
                        cmd.Parameters.Add("@value", System.Data.DbType.Int32).Value = Chest.Value;
                        cmd.Parameters.Add("@proj", System.Data.DbType.Int32).Value = Chest.ProjectileNumber;
                        cmd.Parameters.Add("@price", System.Data.DbType.Int32).Value = Chest.Price;
                        cmd.Parameters.Add("@rarity", System.Data.DbType.Int32).Value = Chest.Rarity;
                        cmd.ExecuteNonQuery();

                        command = "UPDATE EQUIPMENT SET ";
                        command = command + "NAME = @name, CLIP = @clip, MAXCLIP = @maxclip, SPRITE = @sprite, DAMAGE = @damage, ARMOR = @armor, TYPE = @type, ATTACKSPEED = @attackspeed, RELOADSPEED = @reloadspeed, ";
                        command = command + "HEALTHRESTORE = @healthrestore, HUNGERRESTORE = @hungerrestore, HYDRATERESTORE = @hydraterestore, STRENGTH = @strength, AGILITY = @agility, ENDURANCE = @endurance, STAMINA = @stamina, AMMOTYPE = @ammotype, ";
                        command = command + "VALUE = @value, PROJ = @proj, PRICE = @price, RARITY = @rarity WHERE OWNER = '" + Name + "' AND ID = 1;";
                        cmd.CommandText = command;
                        cmd.Parameters.Add("@name", System.Data.DbType.String).Value = Legs.Name;
                        cmd.Parameters.Add("@clip", System.Data.DbType.Int32).Value = Legs.Clip;
                        cmd.Parameters.Add("@maxclip", System.Data.DbType.Int32).Value = Legs.MaxClip;
                        cmd.Parameters.Add("@sprite", System.Data.DbType.Int32).Value = Legs.Sprite;
                        cmd.Parameters.Add("@damage", System.Data.DbType.Int32).Value = Legs.Damage;
                        cmd.Parameters.Add("@armor", System.Data.DbType.Int32).Value = Legs.Armor;
                        cmd.Parameters.Add("@type", System.Data.DbType.Int32).Value = Legs.Type;
                        cmd.Parameters.Add("@attackspeed", System.Data.DbType.Int32).Value = Legs.AttackSpeed;
                        cmd.Parameters.Add("@reloadspeed", System.Data.DbType.Int32).Value = Legs.ReloadSpeed;
                        cmd.Parameters.Add("@healthrestore", System.Data.DbType.Int32).Value = Legs.HealthRestore;
                        cmd.Parameters.Add("@hungerrestore", System.Data.DbType.Int32).Value = Legs.HungerRestore;
                        cmd.Parameters.Add("@hydraterestore", System.Data.DbType.Int32).Value = Legs.HydrateRestore;
                        cmd.Parameters.Add("@strength", System.Data.DbType.Int32).Value = Legs.Strength;
                        cmd.Parameters.Add("@agility", System.Data.DbType.Int32).Value = Legs.Agility;
                        cmd.Parameters.Add("@endurance", System.Data.DbType.Int32).Value = Legs.Endurance;
                        cmd.Parameters.Add("@stamina", System.Data.DbType.Int32).Value = Legs.Stamina;
                        cmd.Parameters.Add("@ammotype", System.Data.DbType.Int32).Value = Legs.ItemAmmoType;
                        cmd.Parameters.Add("@value", System.Data.DbType.Int32).Value = Legs.Value;
                        cmd.Parameters.Add("@proj", System.Data.DbType.Int32).Value = Legs.ProjectileNumber;
                        cmd.Parameters.Add("@price", System.Data.DbType.Int32).Value = Legs.Price;
                        cmd.Parameters.Add("@rarity", System.Data.DbType.Int32).Value = Legs.Rarity;
                        cmd.ExecuteNonQuery();

                        command = "UPDATE EQUIPMENT SET ";
                        command = command + "NAME = @name, CLIP = @clip, MAXCLIP = @maxclip, SPRITE = @sprite, DAMAGE = @damage, ARMOR = @armor, TYPE = @type, ATTACKSPEED = @attackspeed, RELOADSPEED = @reloadspeed, ";
                        command = command + "HEALTHRESTORE = @healthrestore, HUNGERRESTORE = @hungerrestore, HYDRATERESTORE = @hydraterestore, STRENGTH = @strength, AGILITY = @agility, ENDURANCE = @endurance, STAMINA = @stamina, AMMOTYPE = @ammotype, ";
                        command = command + "VALUE = @value, PROJ = @proj, PRICE = @price, RARITY = @rarity WHERE OWNER = '" + Name + "' AND ID = 2;";
                        cmd.CommandText = command;
                        cmd.Parameters.Add("@name", System.Data.DbType.String).Value = Feet.Name;
                        cmd.Parameters.Add("@clip", System.Data.DbType.Int32).Value = Feet.Clip;
                        cmd.Parameters.Add("@maxclip", System.Data.DbType.Int32).Value = Feet.MaxClip;
                        cmd.Parameters.Add("@sprite", System.Data.DbType.Int32).Value = Feet.Sprite;
                        cmd.Parameters.Add("@damage", System.Data.DbType.Int32).Value = Feet.Damage;
                        cmd.Parameters.Add("@armor", System.Data.DbType.Int32).Value = Feet.Armor;
                        cmd.Parameters.Add("@type", System.Data.DbType.Int32).Value = Feet.Type;
                        cmd.Parameters.Add("@attackspeed", System.Data.DbType.Int32).Value = Feet.AttackSpeed;
                        cmd.Parameters.Add("@reloadspeed", System.Data.DbType.Int32).Value = Feet.ReloadSpeed;
                        cmd.Parameters.Add("@healthrestore", System.Data.DbType.Int32).Value = Feet.HealthRestore;
                        cmd.Parameters.Add("@hungerrestore", System.Data.DbType.Int32).Value = Feet.HungerRestore;
                        cmd.Parameters.Add("@hydraterestore", System.Data.DbType.Int32).Value = Feet.HydrateRestore;
                        cmd.Parameters.Add("@strength", System.Data.DbType.Int32).Value = Feet.Strength;
                        cmd.Parameters.Add("@agility", System.Data.DbType.Int32).Value = Feet.Agility;
                        cmd.Parameters.Add("@endurance", System.Data.DbType.Int32).Value = Feet.Endurance;
                        cmd.Parameters.Add("@stamina", System.Data.DbType.Int32).Value = Feet.Stamina;
                        cmd.Parameters.Add("@ammotype", System.Data.DbType.Int32).Value = Feet.ItemAmmoType;
                        cmd.Parameters.Add("@value", System.Data.DbType.Int32).Value = Feet.Value;
                        cmd.Parameters.Add("@proj", System.Data.DbType.Int32).Value = Feet.ProjectileNumber;
                        cmd.Parameters.Add("@price", System.Data.DbType.Int32).Value = Feet.Price;
                        cmd.Parameters.Add("@rarity", System.Data.DbType.Int32).Value = Feet.Rarity;
                        cmd.ExecuteNonQuery();

                        command = "DELETE FROM INVENTORY WHERE OWNER = '" + Name + "';";
                        cmd.CommandText = command;
                        cmd.ExecuteNonQuery();

                        int n = 0;
                        for (int i = 0; i < 25; i++)
                        {
                            if (Backpack[i].Name != "None")
                            {
                                command = "INSERT INTO INVENTORY";
                                command = command + "(OWNER,ID,NAME,SPRITE,DAMAGE,ARMOR,TYPE,ATTACKSPEED,RELOADSPEED,HEALTHRESTORE,HUNGERRESTORE,HYDRATERESTORE,STRENGTH,AGILITY,ENDURANCE,STAMINA,CLIP,MAXCLIP,AMMOTYPE,VALUE,PROJ,PRICE,RARITY)";
                                command = command + " VALUES ";
                                command = command + "(@owner,@id,@name,@sprite,@damage,@armor,@type,@attackspeed,@reloadspeed,@healthrestore,@hungerrestore,@hydraterestore,@strength,@agility,@endurance,@stamina,@clip,@maxclip,@ammotype,@value,@proj,@price,@rarity);";
                                cmd.CommandText = command;
                                cmd.Parameters.Add("@owner", System.Data.DbType.String).Value = Name;
                                cmd.Parameters.Add("@id", System.Data.DbType.Int32).Value = n;
                                cmd.Parameters.Add("@name", System.Data.DbType.String).Value = Backpack[i].Name;
                                cmd.Parameters.Add("@sprite", System.Data.DbType.Int32).Value = Backpack[i].Sprite;
                                cmd.Parameters.Add("@damage", System.Data.DbType.Int32).Value = Backpack[i].Damage;
                                cmd.Parameters.Add("@armor", System.Data.DbType.Int32).Value = Backpack[i].Armor;
                                cmd.Parameters.Add("@type", System.Data.DbType.Int32).Value = Backpack[i].Type;
                                cmd.Parameters.Add("@attackspeed", System.Data.DbType.Int32).Value = Backpack[i].AttackSpeed;
                                cmd.Parameters.Add("@reloadspeed", System.Data.DbType.Int32).Value = Backpack[i].ReloadSpeed;
                                cmd.Parameters.Add("@healthrestore", System.Data.DbType.Int32).Value = Backpack[i].HealthRestore;
                                cmd.Parameters.Add("@hungerrestore", System.Data.DbType.Int32).Value = Backpack[i].HungerRestore;
                                cmd.Parameters.Add("@hydraterestore", System.Data.DbType.Int32).Value = Backpack[i].HydrateRestore;
                                cmd.Parameters.Add("@strength", System.Data.DbType.Int32).Value = Backpack[i].Strength;
                                cmd.Parameters.Add("@agility", System.Data.DbType.Int32).Value = Backpack[i].Agility;
                                cmd.Parameters.Add("@endurance", System.Data.DbType.Int32).Value = Backpack[i].Endurance;
                                cmd.Parameters.Add("@stamina", System.Data.DbType.Int32).Value = Backpack[i].Stamina;
                                cmd.Parameters.Add("@clip", System.Data.DbType.Int32).Value = Backpack[i].Clip;
                                cmd.Parameters.Add("@maxclip", System.Data.DbType.Int32).Value = Backpack[i].MaxClip;
                                cmd.Parameters.Add("@ammotype", System.Data.DbType.Int32).Value = Backpack[i].ItemAmmoType;
                                cmd.Parameters.Add("@value", System.Data.DbType.Int32).Value = Backpack[i].Value;
                                cmd.Parameters.Add("@proj", System.Data.DbType.Int32).Value = Backpack[i].ProjectileNumber;
                                cmd.Parameters.Add("@price", System.Data.DbType.Int32).Value = Backpack[i].Price;
                                cmd.Parameters.Add("@rarity", System.Data.DbType.Int32).Value = Backpack[i].Rarity;
                                n = n + 1;
                                cmd.ExecuteNonQuery();
                            }
                        }

                        command = "DELETE FROM BANK WHERE OWNER = '" + Name + "';";
                        cmd.CommandText = command;
                        cmd.ExecuteNonQuery();

                        int m = 0;
                        for (int i = 0; i < 50; i++)
                        {
                            if (Bank[i].Name != "None")
                            {
                                command = "INSERT INTO BANK";
                                command = command + "(OWNER,ID,NAME,SPRITE,DAMAGE,ARMOR,TYPE,ATTACKSPEED,RELOADSPEED,HEALTHRESTORE,HUNGERRESTORE,HYDRATERESTORE,STRENGTH,AGILITY,ENDURANCE,STAMINA,CLIP,MAXCLIP,AMMOTYPE,VALUE,PROJ,PRICE,RARITY)";
                                command = command + " VALUES ";
                                command = command + "(@owner,@id,@name,@sprite,@damage,@armor,@type,@attackspeed,@reloadspeed,@healthrestore,@hungerrestore,@hydraterestore,@strength,@agility,@endurance,@stamina,@clip,@maxclip,@ammotype,@value,@proj,@price,@rarity);";
                                cmd.CommandText = command;
                                cmd.Parameters.Add("@owner", System.Data.DbType.String).Value = Name;
                                cmd.Parameters.Add("@id", System.Data.DbType.Int32).Value = m;
                                cmd.Parameters.Add("@name", System.Data.DbType.String).Value = Bank[i].Name;
                                cmd.Parameters.Add("@sprite", System.Data.DbType.Int32).Value = Bank[i].Sprite;
                                cmd.Parameters.Add("@damage", System.Data.DbType.Int32).Value = Bank[i].Damage;
                                cmd.Parameters.Add("@armor", System.Data.DbType.Int32).Value = Bank[i].Armor;
                                cmd.Parameters.Add("@type", System.Data.DbType.Int32).Value = Bank[i].Type;
                                cmd.Parameters.Add("@attackspeed", System.Data.DbType.Int32).Value = Bank[i].AttackSpeed;
                                cmd.Parameters.Add("@reloadspeed", System.Data.DbType.Int32).Value = Bank[i].ReloadSpeed;
                                cmd.Parameters.Add("@healthrestore", System.Data.DbType.Int32).Value = Bank[i].HealthRestore;
                                cmd.Parameters.Add("@hungerrestore", System.Data.DbType.Int32).Value = Bank[i].HungerRestore;
                                cmd.Parameters.Add("@hydraterestore", System.Data.DbType.Int32).Value = Bank[i].HydrateRestore;
                                cmd.Parameters.Add("@strength", System.Data.DbType.Int32).Value = Bank[i].Strength;
                                cmd.Parameters.Add("@agility", System.Data.DbType.Int32).Value = Bank[i].Agility;
                                cmd.Parameters.Add("@endurance", System.Data.DbType.Int32).Value = Bank[i].Endurance;
                                cmd.Parameters.Add("@stamina", System.Data.DbType.Int32).Value = Bank[i].Stamina;
                                cmd.Parameters.Add("@clip", System.Data.DbType.Int32).Value = Bank[i].Clip;
                                cmd.Parameters.Add("@maxclip", System.Data.DbType.Int32).Value = Bank[i].MaxClip;
                                cmd.Parameters.Add("@ammotype", System.Data.DbType.Int32).Value = Bank[i].ItemAmmoType;
                                cmd.Parameters.Add("@value", System.Data.DbType.Int32).Value = Bank[i].Value;
                                cmd.Parameters.Add("@proj", System.Data.DbType.Int32).Value = Bank[i].ProjectileNumber;
                                cmd.Parameters.Add("@price", System.Data.DbType.Int32).Value = Bank[i].Price;
                                cmd.Parameters.Add("@rarity", System.Data.DbType.Int32).Value = Bank[i].Rarity;
                                m = m + 1;
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
        }

        public void LoadPlayerFromDatabase()
        {
            if (DBType == SQL_DATABASE_REMOTE.ToString())
            {
                string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
                using (var sql = new SqlConnection(connection))
                {
                    sql.Open();
                    string command;
                    int result;
                    command = "SELECT * FROM PLAYERS WHERE NAME=@name";
                    using (var cmd = new SqlCommand(command, sql))
                    {
                        cmd.Parameters.Add(new SqlParameter("@name", System.Data.DbType.String)).Value = Name;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Id = ToInt32(reader[0]);
                                Name = reader[1].ToString();
                                Pass = reader[2].ToString();
                                EmailAddress = reader[3].ToString();
                                X = ToInt32(reader[4]);
                                Y = ToInt32(reader[5]);
                                Map = ToInt32(reader[6]);
                                Direction = ToInt32(reader[7]);
                                AimDirection = ToInt32(reader[8]);
                                Sprite = ToInt32(reader[9]);
                                Level = ToInt32(reader[10]);
                                Points = ToInt32(reader[11]);
                                Health = ToInt32(reader[12]);
                                MaxHealth = ToInt32(reader[13]);
                                Experience = ToInt32(reader[14]);
                                Money = ToInt32(reader[15]);
                                Armor = ToInt32(reader[16]);
                                Hunger = ToInt32(reader[17]);
                                Hydration = ToInt32(reader[18]);
                                Strength = ToInt32(reader[19]);
                                Agility = ToInt32(reader[20]);
                                Endurance = ToInt32(reader[21]);
                                Stamina = ToInt32(reader[22]);
                                PistolAmmo = ToInt32(reader[23]);
                                AssaultAmmo = ToInt32(reader[24]);
                                RocketAmmo = ToInt32(reader[25]);
                                GrenadeAmmo = ToInt32(reader[26]);
                                LightRadius = ToInt32(reader[27]);
                                PlayDays = ToInt32(reader[28]);
                                PlayHours = ToInt32(reader[29]);
                                PlayMinutes = ToInt32(reader[30]);
                                PlaySeconds = ToInt32(reader[31]);
                                LifeDay = ToInt32(reader[32]);
                                LifeHour = ToInt32(reader[33]);
                                LifeMinute = ToInt32(reader[34]);
                                LifeSecond = ToInt32(reader[35]);
                                LongestLifeDay = ToInt32(reader[36]);
                                LongestLifeHour = ToInt32(reader[37]);
                                LongestLifeMinute = ToInt32(reader[38]);
                                LongestLifeSecond = ToInt32(reader[39]);
                                LastLoggedIn = reader[40].ToString();
                                AccountKey = reader[41].ToString();
                                Active = reader[42].ToString();
                            }
                        }
                    }

                    command = "SELECT * FROM MAINWEAPONS WHERE OWNER=@owner";
                    using (var cmd = new SqlCommand(command, sql))
                    {
                        cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                mainWeapon.Id = ToInt32(reader[0]);
                                mainWeapon.Name = reader[2].ToString();
                                mainWeapon.Clip = ToInt32(reader[3]);
                                mainWeapon.MaxClip = ToInt32(reader[4]);
                                mainWeapon.Sprite = ToInt32(reader[5]);
                                mainWeapon.Damage = ToInt32(reader[6]);
                                mainWeapon.Armor = ToInt32(reader[7]);
                                mainWeapon.Type = ToInt32(reader[8]);
                                mainWeapon.AttackSpeed = ToInt32(reader[9]);
                                mainWeapon.ReloadSpeed = ToInt32(reader[10]);
                                mainWeapon.HealthRestore = ToInt32(reader[11]);
                                mainWeapon.HungerRestore = ToInt32(reader[12]);
                                mainWeapon.HydrateRestore = ToInt32(reader[13]);
                                mainWeapon.Strength = ToInt32(reader[14]);
                                mainWeapon.Agility = ToInt32(reader[15]);
                                mainWeapon.Endurance = ToInt32(reader[16]);
                                mainWeapon.Stamina = ToInt32(reader[17]);
                                mainWeapon.ItemAmmoType = ToInt32(reader[18]);
                                mainWeapon.Value = ToInt32(reader[19]);
                                mainWeapon.ProjectileNumber = ToInt32(reader[20]);
                                mainWeapon.Price = ToInt32(reader[21]);
                                mainWeapon.Rarity = ToInt32(reader[22]);
                            }
                        }
                    }

                    command = "SELECT * FROM SECONDARYWEAPONS WHERE OWNER=@owner";
                    using (var cmd = new SqlCommand(command, sql))
                    {
                        cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                offWeapon.Id = ToInt32(reader[0]);
                                offWeapon.Name = reader[2].ToString();
                                offWeapon.Clip = ToInt32(reader[3]);
                                offWeapon.MaxClip = ToInt32(reader[4]);
                                offWeapon.Sprite = ToInt32(reader[5]);
                                offWeapon.Damage = ToInt32(reader[6]);
                                offWeapon.Armor = ToInt32(reader[7]);
                                offWeapon.Type = ToInt32(reader[8]);
                                offWeapon.AttackSpeed = ToInt32(reader[9]);
                                offWeapon.ReloadSpeed = ToInt32(reader[10]);
                                offWeapon.HealthRestore = ToInt32(reader[11]);
                                offWeapon.HungerRestore = ToInt32(reader[12]);
                                offWeapon.HydrateRestore = ToInt32(reader[13]);
                                offWeapon.Strength = ToInt32(reader[14]);
                                offWeapon.Agility = ToInt32(reader[15]);
                                offWeapon.Endurance = ToInt32(reader[16]);
                                offWeapon.Stamina = ToInt32(reader[17]);
                                offWeapon.ItemAmmoType = ToInt32(reader[18]);
                                offWeapon.Value = ToInt32(reader[19]);
                                offWeapon.ProjectileNumber = ToInt32(reader[20]);
                                offWeapon.Price = ToInt32(reader[21]);
                                offWeapon.Rarity = ToInt32(reader[22]);
                            }
                        }
                    }

                    command = "SELECT * FROM EQUIPMENT WHERE OWNER=@owner AND SLOT=@slot";
                    using (var cmd = new SqlCommand(command, sql))
                    {
                        cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                        cmd.Parameters.Add(new SqlParameter("@slot", System.Data.DbType.Int32)).Value = 0;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Chest.Id = ToInt32(reader[0]);
                                Chest.Name = reader[3].ToString();
                                Chest.Sprite = ToInt32(reader[4]);
                                Chest.Damage = ToInt32(reader[5]);
                                Chest.Armor = ToInt32(reader[6]);
                                Chest.Type = ToInt32(reader[7]);
                                Chest.AttackSpeed = ToInt32(reader[8]);
                                Chest.ReloadSpeed = ToInt32(reader[9]);
                                Chest.HealthRestore = ToInt32(reader[10]);
                                Chest.HungerRestore = ToInt32(reader[11]);
                                Chest.HydrateRestore = ToInt32(reader[12]);
                                Chest.Strength = ToInt32(reader[13]);
                                Chest.Agility = ToInt32(reader[14]);
                                Chest.Endurance = ToInt32(reader[15]);
                                Chest.Stamina = ToInt32(reader[16]);
                                Chest.Clip = ToInt32(reader[17]);
                                Chest.MaxClip = ToInt32(reader[18]);
                                Chest.ItemAmmoType = ToInt32(reader[19]);
                                Chest.Value = ToInt32(reader[20]);
                                Chest.ProjectileNumber = ToInt32(reader[21]);
                                Chest.Price = ToInt32(reader[22]);
                                Chest.Rarity = ToInt32(reader[23]);
                            }
                        }
                    }

                    command = "SELECT * FROM EQUIPMENT WHERE OWNER=@owner AND SLOT=@slot";
                    using (var cmd = new SqlCommand(command, sql))
                    {
                        cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                        cmd.Parameters.Add(new SqlParameter("@slot", System.Data.DbType.Int32)).Value = 1;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Legs.Id = ToInt32(reader[0]);
                                Legs.Name = reader[3].ToString();
                                Legs.Sprite = ToInt32(reader[4]);
                                Legs.Damage = ToInt32(reader[5]);
                                Legs.Armor = ToInt32(reader[6]);
                                Legs.Type = ToInt32(reader[7]);
                                Legs.AttackSpeed = ToInt32(reader[8]);
                                Legs.ReloadSpeed = ToInt32(reader[9]);
                                Legs.HealthRestore = ToInt32(reader[10]);
                                Legs.HungerRestore = ToInt32(reader[11]);
                                Legs.HydrateRestore = ToInt32(reader[12]);
                                Legs.Strength = ToInt32(reader[13]);
                                Legs.Agility = ToInt32(reader[14]);
                                Legs.Endurance = ToInt32(reader[15]);
                                Legs.Stamina = ToInt32(reader[16]);
                                Legs.Clip = ToInt32(reader[17]);
                                Legs.MaxClip = ToInt32(reader[18]);
                                Legs.ItemAmmoType = ToInt32(reader[19]);
                                Legs.Value = ToInt32(reader[20]);
                                Legs.ProjectileNumber = ToInt32(reader[21]);
                                Legs.Price = ToInt32(reader[22]);
                                Legs.Rarity = ToInt32(reader[23]);
                            }
                        }
                    }

                    command = "SELECT * FROM EQUIPMENT WHERE OWNER=@owner AND SLOT=@slot";
                    using (var cmd = new SqlCommand(command, sql))
                    {
                        cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                        cmd.Parameters.Add(new SqlParameter("@slot", System.Data.DbType.Int32)).Value = 2;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Feet.Id = ToInt32(reader[0]);
                                Feet.Name = reader[3].ToString();
                                Feet.Sprite = ToInt32(reader[4]);
                                Feet.Damage = ToInt32(reader[5]);
                                Feet.Armor = ToInt32(reader[6]);
                                Feet.Type = ToInt32(reader[7]);
                                Feet.AttackSpeed = ToInt32(reader[8]);
                                Feet.ReloadSpeed = ToInt32(reader[9]);
                                Feet.HealthRestore = ToInt32(reader[10]);
                                Feet.HungerRestore = ToInt32(reader[11]);
                                Feet.HydrateRestore = ToInt32(reader[12]);
                                Feet.Strength = ToInt32(reader[13]);
                                Feet.Agility = ToInt32(reader[14]);
                                Feet.Endurance = ToInt32(reader[15]);
                                Feet.Stamina = ToInt32(reader[16]);
                                Feet.Clip = ToInt32(reader[17]);
                                Feet.MaxClip = ToInt32(reader[18]);
                                Feet.ItemAmmoType = ToInt32(reader[19]);
                                Feet.Value = ToInt32(reader[20]);
                                Feet.ProjectileNumber = ToInt32(reader[21]);
                                Feet.Price = ToInt32(reader[22]);
                                Feet.Rarity = ToInt32(reader[23]);
                            }
                        }
                    }

                    command = "SELECT COUNT(*) FROM INVENTORY WHERE OWNER=@owner";
                    using (var cmd = new SqlCommand(command, sql))
                    {
                        cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                        object queue = cmd.ExecuteScalar();
                        result = ToInt32(queue);
                    }

                    if (result > 0)
                    {
                        for (int i = 0; i < result; i++)
                        {
                            command = "SELECT * FROM INVENTORY WHERE OWNER=@owner AND SLOT=@slot";
                            using (var cmd = new SqlCommand(command, sql))
                            {
                                cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                                cmd.Parameters.Add(new SqlParameter("@slot", System.Data.DbType.Int32)).Value = i;

                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        Backpack[i].Id = ToInt32(reader[0]);
                                        Backpack[i].Name = reader[3].ToString();
                                        Backpack[i].Sprite = ToInt32(reader[4]);
                                        Backpack[i].Damage = ToInt32(reader[5]);
                                        Backpack[i].Armor = ToInt32(reader[6]);
                                        Backpack[i].Type = ToInt32(reader[7]);
                                        Backpack[i].AttackSpeed = ToInt32(reader[8]);
                                        Backpack[i].ReloadSpeed = ToInt32(reader[9]);
                                        Backpack[i].HealthRestore = ToInt32(reader[10]);
                                        Backpack[i].HungerRestore = ToInt32(reader[11]);
                                        Backpack[i].HydrateRestore = ToInt32(reader[12]);
                                        Backpack[i].Strength = ToInt32(reader[13]);
                                        Backpack[i].Agility = ToInt32(reader[14]);
                                        Backpack[i].Endurance = ToInt32(reader[15]);
                                        Backpack[i].Stamina = ToInt32(reader[16]);
                                        Backpack[i].Clip = ToInt32(reader[17]);
                                        Backpack[i].MaxClip = ToInt32(reader[18]);
                                        Backpack[i].ItemAmmoType = ToInt32(reader[19]);
                                        Backpack[i].Value = ToInt32(reader[20]);
                                        Backpack[i].ProjectileNumber = ToInt32(reader[21]);
                                        Backpack[i].Price = ToInt32(reader[22]);
                                        Backpack[i].Rarity = ToInt32(reader[23]);
                                    }
                                }
                            }
                        }
                    }

                    command = "SELECT COUNT(*) FROM BANK WHERE OWNER=@owner";
                    using (var cmd = new SqlCommand(command, sql))
                    {
                        cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                        object queue = cmd.ExecuteScalar();
                        result = ToInt32(queue);
                    }

                    if (result > 0)
                    {
                        for (int i = 0; i < result; i++)
                        {
                            command = "SELECT * FROM BANK WHERE OWNER=@owner AND SLOT=@slot";
                            using (var cmd = new SqlCommand(command, sql))
                            {
                                cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                                cmd.Parameters.Add(new SqlParameter("@slot", System.Data.DbType.Int32)).Value = i;

                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        Bank[i].Id = ToInt32(reader[0]);
                                        Bank[i].Name = reader[3].ToString();
                                        Bank[i].Sprite = ToInt32(reader[4]);
                                        Bank[i].Damage = ToInt32(reader[5]);
                                        Bank[i].Armor = ToInt32(reader[6]);
                                        Bank[i].Type = ToInt32(reader[7]);
                                        Bank[i].AttackSpeed = ToInt32(reader[8]);
                                        Bank[i].ReloadSpeed = ToInt32(reader[9]);
                                        Bank[i].HealthRestore = ToInt32(reader[10]);
                                        Bank[i].HungerRestore = ToInt32(reader[11]);
                                        Bank[i].HydrateRestore = ToInt32(reader[12]);
                                        Bank[i].Strength = ToInt32(reader[13]);
                                        Bank[i].Agility = ToInt32(reader[14]);
                                        Bank[i].Endurance = ToInt32(reader[15]);
                                        Bank[i].Stamina = ToInt32(reader[16]);
                                        Bank[i].Clip = ToInt32(reader[17]);
                                        Bank[i].MaxClip = ToInt32(reader[18]);
                                        Bank[i].ItemAmmoType = ToInt32(reader[19]);
                                        Bank[i].Value = ToInt32(reader[20]);
                                        Bank[i].ProjectileNumber = ToInt32(reader[21]);
                                        Bank[i].Price = ToInt32(reader[22]);
                                        Bank[i].Rarity = ToInt32(reader[23]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                using (var conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
                {
                    using (var cmd = new SQLiteCommand(conn))
                    {
                        conn.Open();
                        string command;

                        command = "SELECT * FROM PLAYERS WHERE NAME = '" + Name + "'";
                        cmd.CommandText = command;
                        using (SQLiteDataReader read = cmd.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                Name = read["NAME"].ToString();
                                Pass = read["PASSWORD"].ToString();
                                EmailAddress = read["EMAILADDRESS"].ToString();
                                X = ToInt32(read["X"].ToString());
                                Y = ToInt32(read["Y"].ToString());
                                Map = ToInt32(read["MAP"].ToString());
                                Direction = ToInt32(read["DIRECTION"].ToString());
                                AimDirection = ToInt32(read["AIMDIRECTION"].ToString());
                                Sprite = ToInt32(read["SPRITE"].ToString());
                                Level = ToInt32(read["LEVEL"].ToString());
                                Points = ToInt32(read["POINTS"].ToString());
                                Health = ToInt32(read["HEALTH"].ToString());
                                MaxHealth = ToInt32(read["MAXHEALTH"].ToString());
                                Experience = ToInt32(read["EXPERIENCE"].ToString());
                                Money = ToInt32(read["MONEY"].ToString());
                                Armor = ToInt32(read["ARMOR"].ToString());
                                Hunger = ToInt32(read["HUNGER"].ToString());
                                Hydration = ToInt32(read["HYDRATION"].ToString());
                                Strength = ToInt32(read["STRENGTH"].ToString());
                                Agility = ToInt32(read["AGILITY"].ToString());
                                Endurance = ToInt32(read["ENDURANCE"].ToString());
                                Stamina = ToInt32(read["STAMINA"].ToString());
                                PistolAmmo = ToInt32(read["PISTOLAMMO"].ToString());
                                AssaultAmmo = ToInt32(read["ASSAULTAMMO"].ToString());
                                RocketAmmo = ToInt32(read["ROCKETAMMO"].ToString());
                                GrenadeAmmo = ToInt32(read["GRENADEAMMO"].ToString());
                                LightRadius = ToInt32(read["LIGHTRADIUS"].ToString());
                                PlayDays = ToInt32(read["DAYS"].ToString());
                                PlayHours = ToInt32(read["HOURS"].ToString());
                                PlayMinutes = ToInt32(read["MINUTES"].ToString());
                                PlaySeconds = ToInt32(read["SECONDS"].ToString());
                                LifeDay = ToInt32(read["LDAYS"].ToString());
                                LifeHour = ToInt32(read["LHOURS"].ToString());
                                LifeMinute = ToInt32(read["LMINUTES"].ToString());
                                LifeSecond = ToInt32(read["LSECONDS"].ToString());
                                LongestLifeDay = ToInt32(read["LLDAYS"].ToString());
                                LongestLifeHour = ToInt32(read["LLHOURS"].ToString());
                                LongestLifeMinute = ToInt32(read["LLMINUTES"].ToString());
                                LongestLifeSecond = ToInt32(read["LLSECONDS"].ToString());
                                LastLoggedIn = read["LASTLOGGED"].ToString();
                                AccountKey = read["ACCOUNTKEY"].ToString();
                                Active = read["ACTIVE"].ToString();
                            }
                        }

                        command = "SELECT * FROM MAINWEAPONS WHERE OWNER = '" + Name + "'";
                        cmd.CommandText = command;
                        using (SQLiteDataReader read = cmd.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                mainWeapon.Name = read["NAME"].ToString();
                                mainWeapon.Clip = ToInt32(read["CLIP"].ToString());
                                mainWeapon.MaxClip = ToInt32(read["MAXCLIP"].ToString());
                                mainWeapon.Sprite = ToInt32(read["SPRITE"].ToString());
                                mainWeapon.Damage = ToInt32(read["DAMAGE"].ToString());
                                mainWeapon.Armor = ToInt32(read["ARMOR"].ToString());
                                mainWeapon.Type = ToInt32(read["TYPE"].ToString());
                                mainWeapon.AttackSpeed = ToInt32(read["ATTACKSPEED"].ToString());
                                mainWeapon.ReloadSpeed = ToInt32(read["RELOADSPEED"].ToString());
                                mainWeapon.HealthRestore = ToInt32(read["HEALTHRESTORE"].ToString());
                                mainWeapon.HungerRestore = ToInt32(read["HUNGERRESTORE"].ToString());
                                mainWeapon.HydrateRestore = ToInt32(read["HYDRATERESTORE"].ToString());
                                mainWeapon.Strength = ToInt32(read["STRENGTH"].ToString());
                                mainWeapon.Agility = ToInt32(read["AGILITY"].ToString());
                                mainWeapon.Endurance = ToInt32(read["ENDURANCE"].ToString());
                                mainWeapon.Stamina = ToInt32(read["STAMINA"].ToString());
                                mainWeapon.ItemAmmoType = ToInt32(read["AMMOTYPE"].ToString());
                                mainWeapon.Value = ToInt32(read["VALUE"].ToString());
                                mainWeapon.ProjectileNumber = ToInt32(read["PROJ"].ToString());
                                mainWeapon.Price = ToInt32(read["PRICE"].ToString());
                                mainWeapon.Rarity = ToInt32(read["RARITY"].ToString());
                            }
                        }

                        command = "SELECT * FROM SECONDARYWEAPONS WHERE OWNER = '" + Name + "'";
                        cmd.CommandText = command;
                        using (SQLiteDataReader read = cmd.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                offWeapon.Name = read["NAME"].ToString();
                                offWeapon.Clip = ToInt32(read["CLIP"].ToString());
                                offWeapon.MaxClip = ToInt32(read["MAXCLIP"].ToString());
                                offWeapon.Sprite = ToInt32(read["SPRITE"].ToString());
                                offWeapon.Damage = ToInt32(read["DAMAGE"].ToString());
                                offWeapon.Armor = ToInt32(read["ARMOR"].ToString());
                                offWeapon.Type = ToInt32(read["TYPE"].ToString());
                                offWeapon.AttackSpeed = ToInt32(read["ATTACKSPEED"].ToString());
                                offWeapon.ReloadSpeed = ToInt32(read["RELOADSPEED"].ToString());
                                offWeapon.HealthRestore = ToInt32(read["HEALTHRESTORE"].ToString());
                                offWeapon.HungerRestore = ToInt32(read["HUNGERRESTORE"].ToString());
                                offWeapon.HydrateRestore = ToInt32(read["HYDRATERESTORE"].ToString());
                                offWeapon.Strength = ToInt32(read["STRENGTH"].ToString());
                                offWeapon.Agility = ToInt32(read["AGILITY"].ToString());
                                offWeapon.Endurance = ToInt32(read["ENDURANCE"].ToString());
                                offWeapon.Stamina = ToInt32(read["STAMINA"].ToString());
                                offWeapon.ItemAmmoType = ToInt32(read["AMMOTYPE"].ToString());
                                offWeapon.Value = ToInt32(read["VALUE"].ToString());
                                offWeapon.ProjectileNumber = ToInt32(read["PROJ"].ToString());
                                offWeapon.Price = ToInt32(read["PRICE"].ToString());
                                offWeapon.Rarity = ToInt32(read["RARITY"].ToString());
                            }
                        }

                        command = "SELECT * FROM EQUIPMENT WHERE OWNER = '" + Name + "' AND ID = " + 0 + ";";
                        cmd.CommandText = command;
                        using (SQLiteDataReader read = cmd.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                Chest.Name = read["NAME"].ToString();
                                Chest.Sprite = ToInt32(read["SPRITE"].ToString());
                                Chest.Damage = ToInt32(read["DAMAGE"].ToString());
                                Chest.Armor = ToInt32(read["ARMOR"].ToString());
                                Chest.Type = ToInt32(read["TYPE"].ToString());
                                Chest.AttackSpeed = ToInt32(read["ATTACKSPEED"].ToString());
                                Chest.ReloadSpeed = ToInt32(read["RELOADSPEED"].ToString());
                                Chest.HealthRestore = ToInt32(read["HEALTHRESTORE"].ToString());
                                Chest.HungerRestore = ToInt32(read["HUNGERRESTORE"].ToString());
                                Chest.HydrateRestore = ToInt32(read["HYDRATERESTORE"].ToString());
                                Chest.Strength = ToInt32(read["STRENGTH"].ToString());
                                Chest.Agility = ToInt32(read["AGILITY"].ToString());
                                Chest.Endurance = ToInt32(read["ENDURANCE"].ToString());
                                Chest.Stamina = ToInt32(read["STAMINA"].ToString());
                                Chest.Clip = ToInt32(read["CLIP"].ToString());
                                Chest.MaxClip = ToInt32(read["MAXCLIP"].ToString());
                                Chest.ItemAmmoType = ToInt32(read["AMMOTYPE"].ToString());
                                Chest.Value = ToInt32(read["VALUE"].ToString());
                                Chest.ProjectileNumber = ToInt32(read["PROJ"].ToString());
                                Chest.Price = ToInt32(read["PRICE"].ToString());
                                Chest.Rarity = ToInt32(read["RARITY"].ToString());
                            }
                        }

                        command = "SELECT * FROM EQUIPMENT WHERE OWNER = '" + Name + "' AND ID = " + 1 + ";";
                        cmd.CommandText = command;
                        using (SQLiteDataReader read = cmd.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                Legs.Name = read["NAME"].ToString();
                                Legs.Sprite = ToInt32(read["SPRITE"].ToString());
                                Legs.Damage = ToInt32(read["DAMAGE"].ToString());
                                Legs.Armor = ToInt32(read["ARMOR"].ToString());
                                Legs.Type = ToInt32(read["TYPE"].ToString());
                                Legs.AttackSpeed = ToInt32(read["ATTACKSPEED"].ToString());
                                Legs.ReloadSpeed = ToInt32(read["RELOADSPEED"].ToString());
                                Legs.HealthRestore = ToInt32(read["HEALTHRESTORE"].ToString());
                                Legs.HungerRestore = ToInt32(read["HUNGERRESTORE"].ToString());
                                Legs.HydrateRestore = ToInt32(read["HYDRATERESTORE"].ToString());
                                Legs.Strength = ToInt32(read["STRENGTH"].ToString());
                                Legs.Agility = ToInt32(read["AGILITY"].ToString());
                                Legs.Endurance = ToInt32(read["ENDURANCE"].ToString());
                                Legs.Stamina = ToInt32(read["STAMINA"].ToString());
                                Legs.Clip = ToInt32(read["CLIP"].ToString());
                                Legs.MaxClip = ToInt32(read["MAXCLIP"].ToString());
                                Legs.ItemAmmoType = ToInt32(read["AMMOTYPE"].ToString());
                                Legs.Value = ToInt32(read["VALUE"].ToString());
                                Legs.ProjectileNumber = ToInt32(read["PROJ"].ToString());
                                Legs.Price = ToInt32(read["PRICE"].ToString());
                                Legs.Rarity = ToInt32(read["RARITY"].ToString());
                            }
                        }

                        command = "SELECT * FROM EQUIPMENT WHERE OWNER = '" + Name + "' AND ID = " + 2 + ";";
                        cmd.CommandText = command;
                        using (SQLiteDataReader read = cmd.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                Feet.Name = read["NAME"].ToString();
                                Feet.Sprite = ToInt32(read["SPRITE"].ToString());
                                Feet.Damage = ToInt32(read["DAMAGE"].ToString());
                                Feet.Armor = ToInt32(read["ARMOR"].ToString());
                                Feet.Type = ToInt32(read["TYPE"].ToString());
                                Feet.AttackSpeed = ToInt32(read["ATTACKSPEED"].ToString());
                                Feet.ReloadSpeed = ToInt32(read["RELOADSPEED"].ToString());
                                Feet.HealthRestore = ToInt32(read["HEALTHRESTORE"].ToString());
                                Feet.HungerRestore = ToInt32(read["HUNGERRESTORE"].ToString());
                                Feet.HydrateRestore = ToInt32(read["HYDRATERESTORE"].ToString());
                                Feet.Strength = ToInt32(read["STRENGTH"].ToString());
                                Feet.Agility = ToInt32(read["AGILITY"].ToString());
                                Feet.Endurance = ToInt32(read["ENDURANCE"].ToString());
                                Feet.Stamina = ToInt32(read["STAMINA"].ToString());
                                Feet.Clip = ToInt32(read["CLIP"].ToString());
                                Feet.MaxClip = ToInt32(read["MAXCLIP"].ToString());
                                Feet.ItemAmmoType = ToInt32(read["AMMOTYPE"].ToString());
                                Feet.Value = ToInt32(read["VALUE"].ToString());
                                Feet.ProjectileNumber = ToInt32(read["PROJ"].ToString());
                                Feet.Price = ToInt32(read["PRICE"].ToString());
                                Feet.Rarity = ToInt32(read["RARITY"].ToString());
                            }
                        }

                        command = "SELECT COUNT(*) FROM INVENTORY WHERE OWNER = '" + Name + "'";
                        cmd.CommandText = command;
                        object queue = cmd.ExecuteScalar();
                        int result = ToInt32(queue);

                        if (result > 0)
                        {
                            for (int i = 0; i < result; i++)
                            {
                                command = "SELECT * FROM INVENTORY WHERE OWNER = '" + Name + "' AND ID = " + i + ";";
                                cmd.CommandText = command;
                                using (SQLiteDataReader read = cmd.ExecuteReader())
                                {
                                    while (read.Read())
                                    {
                                        Backpack[i].Name = read["NAME"].ToString();
                                        Backpack[i].Sprite = ToInt32(read["SPRITE"].ToString());
                                        Backpack[i].Damage = ToInt32(read["DAMAGE"].ToString());
                                        Backpack[i].Armor = ToInt32(read["ARMOR"].ToString());
                                        Backpack[i].Type = ToInt32(read["TYPE"].ToString());
                                        Backpack[i].AttackSpeed = ToInt32(read["ATTACKSPEED"].ToString());
                                        Backpack[i].ReloadSpeed = ToInt32(read["RELOADSPEED"].ToString());
                                        Backpack[i].HealthRestore = ToInt32(read["HEALTHRESTORE"].ToString());
                                        Backpack[i].HungerRestore = ToInt32(read["HUNGERRESTORE"].ToString());
                                        Backpack[i].HydrateRestore = ToInt32(read["HYDRATERESTORE"].ToString());
                                        Backpack[i].Strength = ToInt32(read["STRENGTH"].ToString());
                                        Backpack[i].Agility = ToInt32(read["AGILITY"].ToString());
                                        Backpack[i].Endurance = ToInt32(read["ENDURANCE"].ToString());
                                        Backpack[i].Stamina = ToInt32(read["STAMINA"].ToString());
                                        Backpack[i].Clip = ToInt32(read["CLIP"].ToString());
                                        Backpack[i].MaxClip = ToInt32(read["MAXCLIP"].ToString());
                                        Backpack[i].ItemAmmoType = ToInt32(read["AMMOTYPE"].ToString());
                                        Backpack[i].Value = ToInt32(read["VALUE"].ToString());
                                        Backpack[i].ProjectileNumber = ToInt32(read["PROJ"].ToString());
                                        Backpack[i].Price = ToInt32(read["PRICE"].ToString());
                                        Backpack[i].Rarity = ToInt32(read["RARITY"].ToString());
                                    }
                                }
                            }
                        }

                        command = "SELECT COUNT(*) FROM BANK WHERE OWNER = '" + Name + "'";
                        cmd.CommandText = command;
                        queue = cmd.ExecuteScalar();
                        result = ToInt32(queue);

                        if (result > 0)
                        {
                            for (int i = 0; i < 50; i++)
                            {
                                command = "SELECT * FROM BANK WHERE OWNER = '" + Name + "' AND ID = " + i + ";";
                                cmd.CommandText = command;
                                using (SQLiteDataReader read = cmd.ExecuteReader())
                                {
                                    while (read.Read())
                                    {
                                        Bank[i].Name = read["NAME"].ToString();
                                        Bank[i].Sprite = ToInt32(read["SPRITE"].ToString());
                                        Bank[i].Damage = ToInt32(read["DAMAGE"].ToString());
                                        Bank[i].Armor = ToInt32(read["ARMOR"].ToString());
                                        Bank[i].Type = ToInt32(read["TYPE"].ToString());
                                        Bank[i].AttackSpeed = ToInt32(read["ATTACKSPEED"].ToString());
                                        Bank[i].ReloadSpeed = ToInt32(read["RELOADSPEED"].ToString());
                                        Bank[i].HealthRestore = ToInt32(read["HEALTHRESTORE"].ToString());
                                        Bank[i].HungerRestore = ToInt32(read["HUNGERRESTORE"].ToString());
                                        Bank[i].HydrateRestore = ToInt32(read["HYDRATERESTORE"].ToString());
                                        Bank[i].Strength = ToInt32(read["STRENGTH"].ToString());
                                        Bank[i].Agility = ToInt32(read["AGILITY"].ToString());
                                        Bank[i].Endurance = ToInt32(read["ENDURANCE"].ToString());
                                        Bank[i].Stamina = ToInt32(read["STAMINA"].ToString());
                                        Bank[i].Clip = ToInt32(read["CLIP"].ToString());
                                        Bank[i].MaxClip = ToInt32(read["MAXCLIP"].ToString());
                                        Bank[i].ItemAmmoType = ToInt32(read["AMMOTYPE"].ToString());
                                        Bank[i].Value = ToInt32(read["VALUE"].ToString());
                                        Bank[i].ProjectileNumber = ToInt32(read["PROJ"].ToString());
                                        Bank[i].Price = ToInt32(read["PRICE"].ToString());
                                        Bank[i].Rarity = ToInt32(read["RARITY"].ToString());
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void LoadPlayerNameFromDatabase(int id)
        {
            if (DBType == SQL_DATABASE_REMOTE.ToString())
            {
                string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
                using (var sql = new SqlConnection(connection))
                {
                    sql.Open();
                    string command;
                    command = "SELECT * FROM PLAYERS WHERE ID=@id";
                    using (var cmd = new SqlCommand(command, sql))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", System.Data.DbType.Int32)).Value = id;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Name = reader[1].ToString();
                            }
                        }
                    }
                }
            }
            else
            {
                using (var conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
                {
                    using (var cmd = new SQLiteCommand(conn))
                    {
                        conn.Open();
                        string command;

                        command = "SELECT * FROM PLAYERS WHERE rowid = " + id;
                        cmd.CommandText = command;
                        using (SQLiteDataReader read = cmd.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                Name = read["NAME"].ToString();
                            }
                        }
                    }
                }
            }
        }

        public void LoadPlayerIDFromDatabase(string name)
        {
            if (DBType == SQL_DATABASE_REMOTE.ToString())
            {
                string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
                using (var sql = new SqlConnection(connection))
                {
                    sql.Open();
                    string command;
                    command = "SELECT * FROM PLAYERS WHERE NAME=@name";
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
        }

        public bool IsAccountActive()
        {
            string result;
            if (DBType == SQL_DATABASE_REMOTE.ToString())
            {
                string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
                using (var sql = new SqlConnection(connection))
                {
                    sql.Open();
                    string command;
                    command = "SELECT ACTIVE FROM PLAYERS WHERE ID = @id";
                    using (var cmd = new SqlCommand(command, sql))
                    {
                        cmd.Parameters.Add(new SqlParameter("id", System.Data.SqlDbType.Int)).Value = Id;
                        result = cmd.ExecuteScalar().ToString();
                    }
                }
            }
            else
            {
                using (var conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
                {
                    using (var cmd = new SQLiteCommand(conn))
                    {
                        conn.Open();
                        string command;
                        command = "SELECT ACTIVE FROM PLAYERS WHERE NAME = @name";
                        cmd.CommandText = command;
                        cmd.Parameters.Add("@name", System.Data.DbType.String).Value = Name;
                        result = cmd.ExecuteScalar().ToString();
                    }
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
