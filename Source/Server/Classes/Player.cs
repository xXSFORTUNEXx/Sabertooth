using Lidgren.Network;
using System;
using System.Data.SQLite;
using static System.Convert;
using static System.Environment;

namespace Server.Classes
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
        #endregion

        #region Stats
        public string Name { get; set; }
        public string Pass { get; set; }
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
        #endregion

        #region Local Variables
        public int hungerTick;
        public int hydrationTick;
        #endregion

        #region Class Constructors
        public Player(string name, string pass, int x, int y, int direction, int aimdirection, int map, int level, int points, int health, int maxhealth, int exp, int money, 
                      int armor, int hunger, int hydration, int str, int agi, int end, int sta, int defaultAmmo, NetConnection conn)
        {
            Name = name;
            Pass = pass;
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
            PistolAmmo = defaultAmmo;
            AssaultAmmo = defaultAmmo;
            RocketAmmo = 5;
            GrenadeAmmo = 3;

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

        public Player(string name, string pass, int x, int y, int direction, int aimdirection, int map, int level, int points, int health, int maxhealth, int exp, int money,
                      int armor, int hunger, int hydration, int str, int agi, int end, int sta, int defaultAmmo)
        {
            Name = name;
            Pass = pass;
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
            MaxHealth = maxhealth;
            Health = MaxHealth;
            PistolAmmo = defaultAmmo;
            AssaultAmmo = defaultAmmo;
            RocketAmmo = 5;
            GrenadeAmmo = 3;

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
        public void RegenHealth(NetServer s_Server)
        {
            HandleData hData = new HandleData();
            string msg;

            if (Hydration == 0)
            {
                Health -= 50;
                msg = "You are dying from dehydration!";
                hData.SendServerMessageTo(Connection, s_Server, msg);
                return;
            }

            if (Hunger == 0)
            {
                Health -= 100;
                msg = "You are dying from starvation!";
                hData.SendServerMessageTo(Connection, s_Server, msg);
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

        public bool CheckPlayerLevelUp()
        {
            if (Experience == (Level * 1000))
            {
                Level += 1;
                Experience = 0;
                Points += 100;
                Hunger = 100;
                Hydration = 100;
                MaxHealth += (Level * 5) + RND.Next(0, 100);
                Health = MaxHealth;
                Money += 50;
                Strength += RND.Next(1, 3);
                Agility += RND.Next(1, 3);
                Endurance += RND.Next(1, 3);
                Stamina += RND.Next(1, 3);
                return true;              
            }
            else if (Experience > (Level * 1000))
            {
                int remaining = Experience - (Level * 1000);
                Level += 1;
                Experience = remaining;
                Points += 100;
                Hunger = 100;
                Hydration = 100;
                MaxHealth += (Level * 5) + RND.Next(0, 100);
                Health = MaxHealth;
                Money += 50;
                Strength += RND.Next(1, 3);
                Agility += RND.Next(1, 3);
                Endurance += RND.Next(1, 3);
                Stamina += RND.Next(1, 3);
                return true;
            }
            return false;
        }

        public void EquipItem(NetServer s_Server, Player[] s_Player, int index, int slot)
        {
            if (s_Player[index].Backpack[slot].Name != "None")
            {
                HandleData hData = new HandleData();                
                switch (s_Player[index].Backpack[slot].Type)
                {                    
                    case (int)ItemType.RangedWeapon:
                        if (s_Player[index].mainWeapon.Name == "None")
                        {
                            s_Player[index].mainWeapon = s_Player[index].Backpack[slot];
                        }
                        else
                        {
                            int newSlot = FindOpenInvSlot(Backpack);

                            if (newSlot < 25)
                            {
                                s_Player[index].Backpack[newSlot] = s_Player[index].mainWeapon;
                                s_Player[index].mainWeapon = s_Player[index].Backpack[slot];
                            }
                        }
                        break;

                    case (int)ItemType.MeleeWeapon:
                        if (s_Player[index].offWeapon.Name == "None")
                        {
                            s_Player[index].offWeapon = s_Player[index].Backpack[slot];
                        }
                        else
                        {
                            int newSlot = FindOpenInvSlot(Backpack);

                            if (newSlot < 25)
                            {
                                s_Player[index].Backpack[newSlot] = s_Player[index].offWeapon;
                                s_Player[index].offWeapon = s_Player[index].Backpack[slot];
                            }
                        }
                        break;

                    case (int)ItemType.Shirt:
                        if (s_Player[index].Chest.Name == "None")
                        {
                            s_Player[index].Chest = s_Player[index].Backpack[slot];
                        }
                        else
                        {
                            int newSlot = FindOpenInvSlot(Backpack);

                            if (newSlot < 25)
                            {
                                s_Player[index].Backpack[newSlot] = s_Player[index].Chest;
                                s_Player[index].Chest = s_Player[index].Backpack[slot];
                            }
                        }
                        break;

                    case (int)ItemType.Pants:
                        if (s_Player[index].Legs.Name == "None")
                        {
                            s_Player[index].Legs = s_Player[index].Backpack[slot];
                        }
                        else
                        {
                            int newSlot = FindOpenInvSlot(Backpack);

                            if (newSlot < 25)
                            {
                                s_Player[index].Backpack[newSlot] = s_Player[index].Legs;
                                s_Player[index].Legs = s_Player[index].Backpack[slot];
                            }
                        }
                        break;

                    case (int)ItemType.Shoes:
                        if (s_Player[index].Feet.Name == "None")
                        {
                            s_Player[index].Feet = s_Player[index].Backpack[slot];
                        }
                        else
                        {
                            int newSlot = FindOpenInvSlot(Backpack);

                            if (newSlot < 25)
                            {
                                s_Player[index].Backpack[newSlot] = s_Player[index].Feet;
                                s_Player[index].Feet = s_Player[index].Backpack[slot];
                            }
                        }
                        break;

                    case (int)ItemType.Currency:
                        if (s_Player[index].Backpack[slot].Value > 0)
                        {
                            s_Player[index].Money += s_Player[index].Backpack[slot].Value;
                        }                        
                        break;

                    case (int)ItemType.Food:
                        if (s_Player[index].Backpack[slot].HungerRestore > 0)
                        {
                            s_Player[index].Hunger += s_Player[index].Backpack[slot].HungerRestore;
                            if (s_Player[index].Hunger > 100) { s_Player[index].Hunger = 100; }
                        }
                        break;

                    case (int)ItemType.Drink:
                        if (s_Player[index].Backpack[slot].HydrateRestore > 0)
                        {
                            s_Player[index].Hydration += s_Player[index].Backpack[slot].HydrateRestore;
                            if (s_Player[index].Hydration > 100) { s_Player[index].Hydration = 100; }
                        }
                        break;

                    case (int)ItemType.FirstAid:
                        if (s_Player[index].Backpack[slot].HealthRestore > 0)
                        {
                            s_Player[index].Health += s_Player[index].Backpack[slot].HealthRestore;
                            if (s_Player[index].Health > s_Player[index].MaxHealth) { s_Player[index].Health = s_Player[index].MaxHealth; }
                        }
                        break;
                }
                s_Player[index].Backpack[slot] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
                hData.SendWeaponsUpdate(s_Server, s_Player, index);
                hData.SendPlayerInv(s_Server, s_Player, index);
                hData.SendUpdatePlayerStats(s_Server, s_Player, index);
                hData.SendPlayerEquipment(s_Server, s_Player, index);
            }
        }

        public void UnequipItem(NetServer s_Server, Player[] s_Player, int index, int equip)
        {
            HandleData hData = new HandleData();

            switch (equip)
            {
                case (int)EquipSlots.MainWeapon:
                    int itemSlot = s_Player[index].FindOpenInvSlot(s_Player[index].Backpack);
                    if (itemSlot < 25)
                    {
                        s_Player[index].Backpack[itemSlot] = s_Player[index].mainWeapon;
                        s_Player[index].mainWeapon = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);

                        hData.SendWeaponsUpdate(s_Server, s_Player, index);
                        hData.SendPlayerInv(s_Server, s_Player, index);
                        hData.SendPlayerEquipment(s_Server, s_Player, index);
                    }
                    else
                    {
                        hData.SendServerMessageTo(Connection, s_Server, "Inventory is full!");
                        return;
                    }
                    break;

                case (int)EquipSlots.OffWeapon:
                    itemSlot = s_Player[index].FindOpenInvSlot(s_Player[index].Backpack);
                    if (itemSlot < 25)
                    {
                        s_Player[index].Backpack[itemSlot] = s_Player[index].offWeapon;
                        s_Player[index].offWeapon = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
                        hData.SendWeaponsUpdate(s_Server, s_Player, index);
                        hData.SendPlayerInv(s_Server, s_Player, index);
                        hData.SendPlayerEquipment(s_Server, s_Player, index);
                    }
                    else
                    {
                        hData.SendServerMessageTo(Connection, s_Server, "Inventory is full!");
                        return;
                    }
                    break;

                case (int)EquipSlots.Chest:
                    itemSlot = s_Player[index].FindOpenInvSlot(s_Player[index].Backpack);
                    if (itemSlot < 25)
                    {
                        s_Player[index].Backpack[itemSlot] = s_Player[index].Chest;
                        s_Player[index].Chest = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
                        hData.SendWeaponsUpdate(s_Server, s_Player, index);
                        hData.SendPlayerInv(s_Server, s_Player, index);
                        hData.SendPlayerEquipment(s_Server, s_Player, index);
                    }
                    else
                    {
                        hData.SendServerMessageTo(Connection, s_Server, "Inventory is full!");
                        return;
                    }
                    break;

                case (int)EquipSlots.Legs:
                    itemSlot = s_Player[index].FindOpenInvSlot(s_Player[index].Backpack);
                    if (itemSlot < 25)
                    {
                        s_Player[index].Backpack[itemSlot] = s_Player[index].Legs;
                        s_Player[index].Legs = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
                        hData.SendWeaponsUpdate(s_Server, s_Player, index);
                        hData.SendPlayerInv(s_Server, s_Player, index);
                        hData.SendPlayerEquipment(s_Server, s_Player, index);
                    }
                    else
                    {
                        hData.SendServerMessageTo(Connection, s_Server, "Inventory is full!");
                        return;
                    }
                    break;

                case (int)EquipSlots.Feet:
                    itemSlot = s_Player[index].FindOpenInvSlot(s_Player[index].Backpack);
                    if (itemSlot < 25)
                    {
                        s_Player[index].Backpack[itemSlot] = s_Player[index].Feet;
                        s_Player[index].Feet = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
                        hData.SendWeaponsUpdate(s_Server, s_Player, index);
                        hData.SendPlayerInv(s_Server, s_Player, index);
                        hData.SendPlayerEquipment(s_Server, s_Player, index);
                    }
                    else
                    {
                        hData.SendServerMessageTo(Connection, s_Server, "Inventory is full!");
                        return;
                    }
                    break;
            }
        }

        public void DropItem(NetServer s_Server, Map[] s_Map, Player[] s_Player, int index, int slot, int mapNum)
        {
            if (s_Player[index].Backpack[slot].Name != "None")
            {
                Server server = new Server();
                HandleData hData = new HandleData();

                int mapSlot = server.FindOpenMapItemSlot(s_Map[mapNum]);
                if (mapSlot < 20)
                {
                    s_Map[mapNum].m_MapItem[mapSlot].Name = s_Player[index].Backpack[slot].Name;
                    s_Map[mapNum].m_MapItem[mapSlot].X = s_Player[index].X + 12;
                    s_Map[mapNum].m_MapItem[mapSlot].Y = s_Player[index].Y + 9;
                    s_Map[mapNum].m_MapItem[mapSlot].Sprite = s_Player[index].Backpack[slot].Sprite;
                    s_Map[mapNum].m_MapItem[mapSlot].Damage = s_Player[index].Backpack[slot].Damage;
                    s_Map[mapNum].m_MapItem[mapSlot].Armor = s_Player[index].Backpack[slot].Armor;
                    s_Map[mapNum].m_MapItem[mapSlot].Type = s_Player[index].Backpack[slot].Type;
                    s_Map[mapNum].m_MapItem[mapSlot].AttackSpeed = s_Player[index].Backpack[slot].AttackSpeed;
                    s_Map[mapNum].m_MapItem[mapSlot].ReloadSpeed = s_Player[index].Backpack[slot].ReloadSpeed;
                    s_Map[mapNum].m_MapItem[mapSlot].HealthRestore = s_Player[index].Backpack[slot].HealthRestore;
                    s_Map[mapNum].m_MapItem[mapSlot].HungerRestore = s_Player[index].Backpack[slot].HungerRestore;
                    s_Map[mapNum].m_MapItem[mapSlot].HydrateRestore = s_Player[index].Backpack[slot].HydrateRestore;
                    s_Map[mapNum].m_MapItem[mapSlot].Strength = s_Player[index].Backpack[slot].Strength;
                    s_Map[mapNum].m_MapItem[mapSlot].Agility = s_Player[index].Backpack[slot].Agility;
                    s_Map[mapNum].m_MapItem[mapSlot].Endurance = s_Player[index].Backpack[slot].Endurance;
                    s_Map[mapNum].m_MapItem[mapSlot].Stamina = s_Player[index].Backpack[slot].Stamina;
                    s_Map[mapNum].m_MapItem[mapSlot].Clip = s_Player[index].Backpack[slot].Clip;
                    s_Map[mapNum].m_MapItem[mapSlot].MaxClip = s_Player[index].Backpack[slot].MaxClip;
                    s_Map[mapNum].m_MapItem[mapSlot].ItemAmmoType = s_Player[index].Backpack[slot].ItemAmmoType;
                    s_Map[mapNum].m_MapItem[mapSlot].Value = s_Player[index].Backpack[slot].Value;
                    s_Map[mapNum].m_MapItem[mapSlot].ProjectileNumber = s_Player[index].Backpack[slot].ProjectileNumber;
                    s_Map[mapNum].m_MapItem[mapSlot].Price = s_Player[index].Backpack[slot].Price;
                    s_Map[mapNum].m_MapItem[mapSlot].Rarity = s_Player[index].Backpack[slot].Rarity;
                    s_Map[mapNum].m_MapItem[mapSlot].IsSpawned = true;
                    s_Map[mapNum].m_MapItem[mapSlot].ExpireTick = TickCount;

                    s_Player[index].Backpack[slot] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0);
                    hData.SendPlayerInv(s_Server, s_Player, index);

                    for (int p = 0; p < 5; p++)
                    {
                        if (s_Player[p].Connection != null && mapNum == s_Player[p].Map)
                        {
                            hData.SendMapItemData(s_Server, s_Player[p].Connection, s_Map[mapNum], mapSlot);
                        }
                    }
                }
                else
                {
                    hData.SendServerMessageTo(Connection, s_Server, "All map item slots are filled!");
                }
            }
        }

        public void CheckPickup(NetServer s_Server, Map s_Map, Player[] s_Player, Item[] s_Item, int index)
        {
            for (int c = 0; c < 20; c++)
            {
                if (s_Map.m_MapItem[c] != null && s_Map.m_MapItem[c].IsSpawned)
                {
                    if ((X + 12) == s_Map.m_MapItem[c].X && (Y + 9) == s_Map.m_MapItem[c].Y)
                    {
                        PickUpItem(s_Server, s_Player, s_Map.m_MapItem[c], s_Map, index, c);
                        break;
                    }
                }
            }
        }

        void PickUpItem(NetServer s_Server, Player[] s_Player, MapItem s_Item, Map s_Map, int index, int itemNum)
        {
            HandleData hData = new HandleData();
            int itemSlot = FindOpenInvSlot(Backpack);

            if (itemSlot < 25)
            {
                Backpack[itemSlot].Name = s_Item.Name;
                Backpack[itemSlot].Sprite = s_Item.Sprite;
                Backpack[itemSlot].Damage = s_Item.Damage;
                Backpack[itemSlot].Armor = s_Item.Armor;
                Backpack[itemSlot].Type = s_Item.Type;
                Backpack[itemSlot].AttackSpeed = s_Item.AttackSpeed;
                Backpack[itemSlot].ReloadSpeed = s_Item.ReloadSpeed;
                Backpack[itemSlot].HealthRestore = s_Item.HealthRestore;
                Backpack[itemSlot].HungerRestore = s_Item.HungerRestore;
                Backpack[itemSlot].HydrateRestore = s_Item.HydrateRestore;
                Backpack[itemSlot].Strength = s_Item.Strength;
                Backpack[itemSlot].Agility = s_Item.Agility;
                Backpack[itemSlot].Endurance = s_Item.Endurance;
                Backpack[itemSlot].Stamina = s_Item.Stamina;
                Backpack[itemSlot].Clip = s_Item.Clip;
                Backpack[itemSlot].MaxClip = s_Item.MaxClip;
                Backpack[itemSlot].ItemAmmoType = s_Item.ItemAmmoType;
                Backpack[itemSlot].Value = s_Item.Value;
                Backpack[itemSlot].ProjectileNumber = s_Item.ProjectileNumber;
                Backpack[itemSlot].Price = s_Item.Price;
                Backpack[itemSlot].Rarity = s_Item.Price;

                int TileX = s_Map.m_MapItem[itemNum].X;
                int TileY = s_Map.m_MapItem[itemNum].Y;
                s_Map.Ground[TileX, TileY].NeedsSpawnedTick = TickCount;
                s_Map.m_MapItem[itemNum].Name = "None";
                s_Map.m_MapItem[itemNum].IsSpawned = false;

                for (int p = 0; p < 5; p++)
                {
                    if (s_Player[p].Connection != null && Map == s_Player[p].Map)
                    {
                        hData.SendMapItemData(s_Server, s_Player[p].Connection, s_Map, itemNum);
                    }
                }
                hData.SendPlayerInv(s_Server, s_Player, index);
            }
            else
            {
                hData.SendServerMessageTo(Connection, s_Server, "Inventory is full!");
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
        #endregion

        #region Database
        public void CreatePlayerInDatabase()
        {
            using (var conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
            {
                using (var cmd = new SQLiteCommand(conn))
                {
                    conn.Open();
                    string command;
                    command = "INSERT INTO PLAYERS";
                    command = command + "(NAME,PASSWORD,X,Y,MAP,DIRECTION,AIMDIRECTION,SPRITE,LEVEL,POINTS,HEALTH,MAXHEALTH,EXPERIENCE,MONEY,ARMOR,HUNGER,HYDRATION,STRENGTH,AGILITY,ENDURANCE,STAMINA,PISTOLAMMO,ASSAULTAMMO,ROCKETAMMO,GRENADEAMMO)";
                    command = command + " VALUES ";
                    command = command + "(@name,@password,@x,@y,@map,@direction,@aimdirection,sprite,@level,@points,@health,@maxhealth,@experience,@money,@armor,@hunger,@hydration,@strength,@agility,@endurance,@stamina,@endurance,@stamina,";
                    command = command + "@pistolammo,@assaultammo,@rocketammo,@grenadeammo);";
                    cmd.CommandText = command;
                    cmd.Parameters.Add("@name", System.Data.DbType.String).Value = Name;
                    cmd.Parameters.Add("@password", System.Data.DbType.String).Value = Pass;
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
                    cmd.Parameters.Add("@hunger", System.Data.DbType.Int32).Value = hungerTick;
                    cmd.Parameters.Add("@hydrate", System.Data.DbType.Int32).Value = Hydration;
                    cmd.Parameters.Add("@strength", System.Data.DbType.Int32).Value = Strength;
                    cmd.Parameters.Add("@agility", System.Data.DbType.Int32).Value = Agility;
                    cmd.Parameters.Add("@endurance", System.Data.DbType.Int32).Value = Endurance;
                    cmd.Parameters.Add("@stamina", System.Data.DbType.Int32).Value = Stamina;
                    cmd.Parameters.Add("@pistolammo", System.Data.DbType.Int32).Value = PistolAmmo;
                    cmd.Parameters.Add("@assaultammo", System.Data.DbType.Int32).Value = AssaultAmmo;
                    cmd.Parameters.Add("@rocketammo", System.Data.DbType.Int32).Value = RocketAmmo;
                    cmd.Parameters.Add("@grenadeammo", System.Data.DbType.Int32).Value = GrenadeAmmo;
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

        public void SavePlayerToDatabase()
        {
            using (var conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
            {
                using (var cmd = new SQLiteCommand(conn))
                {
                    conn.Open();
                    string command;
                    command = "UPDATE PLAYERS SET ";
                    command = command + "NAME = @name, PASSWORD = @password, X = @x, Y = @y, MAP = @map, DIRECTION = @direction, AIMDIRECTION = @aimdirection, SPRITE = @sprite, LEVEL = @level, POINTS = @points, HEALTH = @health, MAXHEALTH = @maxhealth, ";
                    command = command + "EXPERIENCE = @experience, MONEY = @money, ARMOR = @armor, HUNGER = @hunger, HYDRATION = @hydrate, STRENGTH = @strength, AGILITY = @agility, ENDURANCE = @endurance, STAMINA = @stamina, ";
                    command = command + "PISTOLAMMO = @pistolammo, ASSAULTAMMO = @assaultammo, ROCKETAMMO = @rocketammo, GRENADEAMMO = @grenadeammo WHERE NAME = '" + Name + "';";
                    cmd.CommandText = command;
                    cmd.Parameters.Add("@name", System.Data.DbType.String).Value = Name;
                    cmd.Parameters.Add("@password", System.Data.DbType.String).Value = Pass;
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
                            cmd.Parameters.Add("@value", System.Data.DbType.Int32).Value = Backpack[i].Value;
                            cmd.Parameters.Add("@proj", System.Data.DbType.Int32).Value = Backpack[i].ProjectileNumber;
                            cmd.Parameters.Add("@price", System.Data.DbType.Int32).Value = Backpack[i].Price;
                            cmd.Parameters.Add("@rarity", System.Data.DbType.Int32).Value = Backpack[i].Rarity;
                            n = n + 1;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        public void LoadPlayerFromDatabase()
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
                }
            }
        }
        #endregion
    }

    public enum EquipSlots
    {
        MainWeapon,
        OffWeapon,
        Chest,
        Legs,
        Feet
    }

    public enum Directions
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
