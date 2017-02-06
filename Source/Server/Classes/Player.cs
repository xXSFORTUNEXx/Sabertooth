using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;
using System.Xml;
using static System.Environment;
using static System.Convert;
using System.IO;
using System.Threading;
using System.Data.SQLite;

namespace Server.Classes
{
    public class Player
    {
        #region Main Classes
        public NetConnection Connection;
        public Item mainWeapon = new Item();
        public Item offWeapon = new Item();
        public Item[] Backpack = new Item[25];
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

            mainWeapon = new Item("Pistol", 1, 30, 0, (int)ItemType.RangedWeapon, 700, 1500, 0, 0, 0, 0, 0, 0, 0, 8, 8, (int)AmmoType.Pistol, 1, 1, 1);
            offWeapon = new Item("Club", 3, 40, 0, (int)ItemType.MeleeWeapon, 900, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, (int)ItemType.None, 1, 1, 1);
            Chest = new Item("Starter Shirt", 4, 0, 5, (int)ItemType.Shirt, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1);
            Legs = new Item("Starter Pants", 5, 0, 5, (int)ItemType.Pants, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1);
            Feet = new Item("Starter Shoes", 6, 0, 5, (int)ItemType.Shoes, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1);

            for (int i = 0; i < 25; i++)
            {
                Backpack[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1);
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

            mainWeapon = new Item("Pistol", 1, 30, 0, (int)ItemType.RangedWeapon, 700, 1500, 0, 0, 0, 0, 0, 0, 0, 8, 8, (int)AmmoType.Pistol, 1, 1, 1);
            offWeapon = new Item("Club", 3, 40, 0, (int)ItemType.MeleeWeapon, 900, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, (int)ItemType.None, 1, 1, 1);
            Chest = new Item("Starter Shirt", 4, 0, 5, (int)ItemType.Shirt, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1);
            Legs = new Item("Starter Pants", 5, 0, 5, (int)ItemType.Pants, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1);
            Feet = new Item("Starter Shoes", 6, 0, 5, (int)ItemType.Shoes, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1);

            for (int i = 0; i < 25; i++)
            {
                Backpack[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1);
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
                Backpack[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1);
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
                Backpack[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1);
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
                s_Player[index].Backpack[slot] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1);
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
                        s_Player[index].mainWeapon = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1);

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
                        s_Player[index].offWeapon = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1);
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
                        s_Player[index].Chest = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1);
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
                        s_Player[index].Legs = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1);
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
                        s_Player[index].Feet = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1);
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
                    s_Map[mapNum].mapItem[mapSlot].Name = s_Player[index].Backpack[slot].Name;
                    s_Map[mapNum].mapItem[mapSlot].X = s_Player[index].X + 12;
                    s_Map[mapNum].mapItem[mapSlot].Y = s_Player[index].Y + 9;
                    s_Map[mapNum].mapItem[mapSlot].Sprite = s_Player[index].Backpack[slot].Sprite;
                    s_Map[mapNum].mapItem[mapSlot].Damage = s_Player[index].Backpack[slot].Damage;
                    s_Map[mapNum].mapItem[mapSlot].Armor = s_Player[index].Backpack[slot].Armor;
                    s_Map[mapNum].mapItem[mapSlot].Type = s_Player[index].Backpack[slot].Type;
                    s_Map[mapNum].mapItem[mapSlot].AttackSpeed = s_Player[index].Backpack[slot].AttackSpeed;
                    s_Map[mapNum].mapItem[mapSlot].ReloadSpeed = s_Player[index].Backpack[slot].ReloadSpeed;
                    s_Map[mapNum].mapItem[mapSlot].HealthRestore = s_Player[index].Backpack[slot].HealthRestore;
                    s_Map[mapNum].mapItem[mapSlot].HungerRestore = s_Player[index].Backpack[slot].HungerRestore;
                    s_Map[mapNum].mapItem[mapSlot].HydrateRestore = s_Player[index].Backpack[slot].HydrateRestore;
                    s_Map[mapNum].mapItem[mapSlot].Strength = s_Player[index].Backpack[slot].Strength;
                    s_Map[mapNum].mapItem[mapSlot].Agility = s_Player[index].Backpack[slot].Agility;
                    s_Map[mapNum].mapItem[mapSlot].Endurance = s_Player[index].Backpack[slot].Endurance;
                    s_Map[mapNum].mapItem[mapSlot].Stamina = s_Player[index].Backpack[slot].Stamina;
                    s_Map[mapNum].mapItem[mapSlot].Clip = s_Player[index].Backpack[slot].Clip;
                    s_Map[mapNum].mapItem[mapSlot].MaxClip = s_Player[index].Backpack[slot].MaxClip;
                    s_Map[mapNum].mapItem[mapSlot].ItemAmmoType = s_Player[index].Backpack[slot].ItemAmmoType;
                    s_Map[mapNum].mapItem[mapSlot].Value = s_Player[index].Backpack[slot].Value;
                    s_Map[mapNum].mapItem[mapSlot].ProjectileNumber = s_Player[index].Backpack[slot].ProjectileNumber;
                    s_Map[mapNum].mapItem[mapSlot].Price = s_Player[index].Backpack[slot].Price;
                    s_Map[mapNum].mapItem[mapSlot].IsSpawned = true;
                    s_Map[mapNum].mapItem[mapSlot].ExpireTick = TickCount;

                    s_Player[index].Backpack[slot] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1);
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
                if (s_Map.mapItem[c] != null && s_Map.mapItem[c].IsSpawned)
                {
                    if ((X + 12) == s_Map.mapItem[c].X && (Y + 9) == s_Map.mapItem[c].Y)
                    {
                        PickUpItem(s_Server, s_Player, s_Map.mapItem[c], s_Map, index, c);
                        break;
                    }
                }
            }
        }

        void PickUpItem(NetServer s_Server, Player[] s_Player, Item s_Item, Map s_Map, int index, int itemNum)
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

                int TileX = s_Map.mapItem[itemNum].X;
                int TileY = s_Map.mapItem[itemNum].Y;
                s_Map.Ground[TileX, TileY].NeedsSpawnedTick = TickCount;
                s_Map.mapItem[itemNum].Name = "None";
                s_Map.mapItem[itemNum].IsSpawned = false;

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
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
            {
                conn.Open();
                string sql;
                sql = "INSERT INTO `PLAYERS`";
                sql = sql + "(`NAME`,`PASSWORD`,`X`,`Y`,`MAP`,`DIRECTION`,`AIMDIRECTION`,`SPRITE`,`LEVEL`,`POINTS`,`HEALTH`,`MAXHEALTH`,`EXPERIENCE`,`MONEY`,`ARMOR`,";
                sql = sql + "`HUNGER`,`HYDRATION`,`STRENGTH`,`AGILITY`,`ENDURANCE`,`STAMINA`,`PISTOLAMMO`,`ASSAULTAMMO`,`ROCKETAMMO`,`GRENADEAMMO`)";
                sql = sql + " VALUES ";
                sql = sql + "('" + Name + "','" + Pass + "','" + X + "','" + Y + "','" + Map + "','" + Direction + "','" + AimDirection + "','" + Sprite + "','" + Level + "',";
                sql = sql + "'" + Points + "','" + Health + "','" + MaxHealth + "','" + Experience + "','" + Money + "','" + Armor + "','" + Hunger + "','" + Hydration + "','" + Strength + "','" + Agility + "',";
                sql = sql + "'" + Endurance + "','" + Stamina + "','" + PistolAmmo + "','" + AssaultAmmo + "','" + RocketAmmo + "','" + GrenadeAmmo + "')";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                sql = "INSERT INTO `MAINWEAPONS`";
                sql = sql + "(`OWNER`,`NAME`,`CLIP`,`MAXCLIP`,`SPRITE`,`DAMAGE`,`ARMOR`,`TYPE`,`ATTACKSPEED`,`RELOADSPEED`,`HEALTHRESTORE`,`HUNGERRESTORE`,`HYDRATERESTORE`,`STRENGTH`,";
                sql = sql + "`AGILITY`,`ENDURANCE`,`STAMINA`,`AMMOTYPE`, `VALUE`,`PROJ`,`PRICE`)";
                sql = sql + " VALUES ";
                sql = sql + "('" + Name + "','" + mainWeapon.Name + "','" + mainWeapon.Clip + "','" + mainWeapon.MaxClip + "','" + mainWeapon.Sprite + "','" + mainWeapon.Damage + "','" + mainWeapon.Armor + "',";
                sql = sql + "'" + mainWeapon.Type + "','" + mainWeapon.AttackSpeed + "','" + mainWeapon.ReloadSpeed + "','" + mainWeapon.HealthRestore + "','" + mainWeapon.HungerRestore + "','" + mainWeapon.HydrateRestore + "',";
                sql = sql + "'" + mainWeapon.Strength + "','" + mainWeapon.Agility + "','" + mainWeapon.Endurance + "','" + mainWeapon.Stamina + "','" + mainWeapon.ItemAmmoType + "','" + mainWeapon.Value + "','" + mainWeapon.ProjectileNumber + "',";
                sql = sql + "'" + mainWeapon.Price + "');";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                sql = "INSERT INTO `SECONDARYWEAPONS`";
                sql = sql + "(`OWNER`,`NAME`,`CLIP`,`MAXCLIP`,`SPRITE`,`DAMAGE`,`ARMOR`,`TYPE`,`ATTACKSPEED`,`RELOADSPEED`,`HEALTHRESTORE`,`HUNGERRESTORE`,`HYDRATERESTORE`,`STRENGTH`,";
                sql = sql + "`AGILITY`,`ENDURANCE`,`STAMINA`,`AMMOTYPE`, `VALUE`,`PROJ`,`PRICE`)";
                sql = sql + " VALUES ";
                sql = sql + "('" + Name + "','" + offWeapon.Name + "','" + offWeapon.Clip + "','" + offWeapon.MaxClip + "','" + offWeapon.Sprite + "','" + offWeapon.Damage + "','" + offWeapon.Armor + "',";
                sql = sql + "'" + offWeapon.Type + "','" + offWeapon.AttackSpeed + "','" + offWeapon.ReloadSpeed + "','" + offWeapon.HealthRestore + "','" + offWeapon.HungerRestore + "','" + offWeapon.HydrateRestore + "',";
                sql = sql + "'" + offWeapon.Strength + "','" + offWeapon.Agility + "','" + offWeapon.Endurance + "','" + offWeapon.Stamina + "','" + offWeapon.ItemAmmoType + "','" + offWeapon.Value + "','" + offWeapon.ProjectileNumber + "',";
                sql = sql + "'" + offWeapon.Price + "');";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                sql = "INSERT INTO `EQUIPMENT`";
                sql = sql + "(`OWNER`,`ID`,`NAME`,`SPRITE`,`DAMAGE`,`ARMOR`,`TYPE`,`ATTACKSPEED`,`RELOADSPEED`,`HEALTHRESTORE`,`HUNGERRESTORE`,`HYDRATERESTORE`,";
                sql = sql + "`STRENGTH`,`AGILITY`,`ENDURANCE`,`STAMINA`,`CLIP`,`MAXCLIP`,`AMMOTYPE`, `VALUE`,`PROJ`,`PRICE`)";
                sql = sql + " VALUES ";
                sql = sql + "('" + Name + "','" + 0 + "','" + Chest.Name + "','" + Chest.Sprite + "','" + Chest.Damage + "','" + Chest.Armor + "','" + Chest.Type + "','" + Chest.AttackSpeed + "','" + Chest.ReloadSpeed + "','" + Chest.HealthRestore + "','" + Chest.HungerRestore + "',";
                sql = sql + "'" + Chest.HydrateRestore + "','" + Chest.Strength + "','" + Chest.Agility + "','" + Chest.Endurance + "','" + Chest.Stamina + "','" + Chest.Clip + "','" + Chest.MaxClip + "','" + Chest.ItemAmmoType + "',";
                sql = sql + "'" + Chest.Value + "','" + Chest.ProjectileNumber + "','" + Chest.Price + "');";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                sql = "INSERT INTO `EQUIPMENT`";
                sql = sql + "(`OWNER`,`ID`,`NAME`,`SPRITE`,`DAMAGE`,`ARMOR`,`TYPE`,`ATTACKSPEED`,`RELOADSPEED`,`HEALTHRESTORE`,`HUNGERRESTORE`,`HYDRATERESTORE`,";
                sql = sql + "`STRENGTH`,`AGILITY`,`ENDURANCE`,`STAMINA`,`CLIP`,`MAXCLIP`,`AMMOTYPE`, `VALUE`,`PROJ`,`PRICE`)";
                sql = sql + " VALUES ";
                sql = sql + "('" + Name + "','" + 1 + "','" + Legs.Name + "','" + Legs.Sprite + "','" + Legs.Damage + "','" + Legs.Armor + "','" + Legs.Type + "','" + Legs.AttackSpeed + "','" + Legs.ReloadSpeed + "','" + Legs.HealthRestore + "','" + Legs.HungerRestore + "',";
                sql = sql + "'" + Legs.HydrateRestore + "','" + Legs.Strength + "','" + Legs.Agility + "','" + Legs.Endurance + "','" + Legs.Stamina + "','" + Legs.Clip + "','" + Legs.MaxClip + "','" + Legs.ItemAmmoType + "',";
                sql = sql + "'" + Legs.Value + "','" + Legs.ProjectileNumber + "','" + Legs.Price + "');";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                sql = "INSERT INTO `EQUIPMENT`";
                sql = sql + "(`OWNER`,`ID`,`NAME`,`SPRITE`,`DAMAGE`,`ARMOR`,`TYPE`,`ATTACKSPEED`,`RELOADSPEED`,`HEALTHRESTORE`,`HUNGERRESTORE`,`HYDRATERESTORE`,";
                sql = sql + "`STRENGTH`,`AGILITY`,`ENDURANCE`,`STAMINA`,`CLIP`,`MAXCLIP`,`AMMOTYPE`, `VALUE`,`PROJ`,`PRICE`)";
                sql = sql + " VALUES ";
                sql = sql + "('" + Name + "','" + 2 + "','" + Feet.Name + "','" + Feet.Sprite + "','" + Feet.Damage + "','" + Feet.Armor + "','" + Feet.Type + "','" + Feet.AttackSpeed + "','" + Feet.ReloadSpeed + "','" + Feet.HealthRestore + "','" + Feet.HungerRestore + "',";
                sql = sql + "'" + Feet.HydrateRestore + "','" + Feet.Strength + "','" + Feet.Agility + "','" + Feet.Endurance + "','" + Feet.Stamina + "','" + Feet.Clip + "','" + Feet.MaxClip + "','" + Feet.ItemAmmoType + "',";
                sql = sql + "'" + Feet.Value + "','" + Feet.ProjectileNumber + "','" + Feet.Price + "');";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void SavePlayerToDatabase()
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
            {
                conn.Open();
                string sql;

                sql = "UPDATE PLAYERS SET ";
                sql = sql + "NAME = '" + Name + "', PASSWORD = '" + Pass + "', X = '" + X + "', Y = '" + Y + "', MAP = '" + Map + "', DIRECTION = '" + Direction + "', ";
                sql = sql + "AIMDIRECTION = '" + AimDirection + "', SPRITE = '" + Sprite + "', LEVEL = '" + Level + "', POINTS = '" + Points + "', HEALTH = '" + Health + "', MAXHEALTH = '" + MaxHealth + "', EXPERIENCE = '" + Experience + "', ";
                sql = sql + "MONEY = '" + Money + "', ARMOR = '" + Armor + "', HUNGER = '" + Hunger + "', HYDRATION = '" + Hydration + "', STRENGTH = '" + Strength + "', AGILITY = '" + Agility + "', ENDURANCE = '" + Endurance + "', ";
                sql = sql + "STAMINA = '" + Stamina + "', PISTOLAMMO = '" + PistolAmmo + "', ASSAULTAMMO = '" + AssaultAmmo + "', ROCKETAMMO = '" + RocketAmmo + "', GRENADEAMMO = '" + GrenadeAmmo + "' ";
                sql = sql + "WHERE NAME = '" + Name + "';";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                sql = "UPDATE MAINWEAPONS SET ";
                sql = sql + "NAME = '" + mainWeapon.Name + "', CLIP = '" + mainWeapon.Clip + "', MAXCLIP = '" + mainWeapon.MaxClip + "', SPRITE = '" + mainWeapon.Sprite + "', DAMAGE = '" + mainWeapon.Damage + "', ARMOR = '" + mainWeapon.Armor + "', ";
                sql = sql + "TYPE = '" + mainWeapon.Type + "', ATTACKSPEED = '" + mainWeapon.AttackSpeed + "', RELOADSPEED = '" + mainWeapon.ReloadSpeed + "', HEALTHRESTORE = '" + mainWeapon.HealthRestore + "', HUNGERRESTORE = '" + mainWeapon.HungerRestore + "', ";
                sql = sql + "HYDRATERESTORE = '" + mainWeapon.HydrateRestore + "', STRENGTH = '" + mainWeapon.Strength + "', AGILITY = '" + mainWeapon.Agility + "', ENDURANCE = '" + mainWeapon.Endurance + "', STAMINA = '" + mainWeapon.Stamina + "', ";
                sql = sql + "AMMOTYPE = '" + mainWeapon.ItemAmmoType + "',  VALUE = '" + mainWeapon.Value + "', PROJ = '" + mainWeapon.ProjectileNumber + "', PRICE = '" + mainWeapon.Price + "' ";
                sql = sql + "WHERE OWNER = '" + Name + "';";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                sql = "UPDATE SECONDARYWEAPONS SET ";
                sql = sql + "NAME = '" + offWeapon.Name + "', CLIP = '" + offWeapon.Clip + "', MAXCLIP = '" + offWeapon.MaxClip + "', SPRITE = '" + offWeapon.Sprite + "', DAMAGE = '" + offWeapon.Damage + "', ARMOR = '" + offWeapon.Armor + "', ";
                sql = sql + "TYPE = '" + offWeapon.Type + "', ATTACKSPEED = '" + offWeapon.AttackSpeed + "', RELOADSPEED = '" + offWeapon.ReloadSpeed + "', HEALTHRESTORE = '" + offWeapon.HealthRestore + "', HUNGERRESTORE = '" + offWeapon.HungerRestore + "', ";
                sql = sql + "HYDRATERESTORE = '" + offWeapon.HydrateRestore + "', STRENGTH = '" + offWeapon.Strength + "', AGILITY = '" + offWeapon.Agility + "', ENDURANCE = '" + offWeapon.Endurance + "', STAMINA = '" + offWeapon.Stamina + "', ";
                sql = sql + "AMMOTYPE = '" + offWeapon.ItemAmmoType + "',  VALUE = '" + offWeapon.Value + "', PROJ = '" + offWeapon.ProjectileNumber + "', PRICE = '" + offWeapon.Price + "' ";
                sql = sql + "WHERE OWNER = '" + Name + "';";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                sql = "UPDATE EQUIPMENT SET ";
                sql = sql + "NAME = '" + Chest.Name + "', SPRITE = '" + Chest.Sprite + "', DAMAGE = '" + Chest.Damage + "', ARMOR = '" + Chest.Armor + "', TYPE = '" + Chest.Type + "', ATTACKSPEED = '" + Chest.AttackSpeed + "', ";
                sql = sql + "RELOADSPEED = '" + Chest.ReloadSpeed + "', HEALTHRESTORE = '" + Chest.HealthRestore + "', HUNGERRESTORE = '" + Chest.HungerRestore + "', HYDRATERESTORE = '" + Chest.HydrateRestore + "', ";
                sql = sql + "STRENGTH = '" + Chest.Strength + "', AGILITY = '" + Chest.Agility + "', ENDURANCE = '" + Chest.Endurance + "', STAMINA = '" + Chest.Stamina + "', CLIP = '" + Chest.Clip + "', MAXCLIP = '" + Chest.MaxClip + "', AMMOTYPE = '" + Chest.ItemAmmoType + "', ";
                sql = sql + "VALUE = '" + Chest.Value + "', PROJ = '" + Chest.ProjectileNumber + "', PRICE = '" + Chest.Price + "' ";
                sql = sql + "WHERE OWNER = '" + Name + "' AND ID = '0';";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                sql = "UPDATE EQUIPMENT SET ";
                sql = sql + "NAME = '" + Legs.Name + "', SPRITE = '" + Legs.Sprite + "', DAMAGE = '" + Legs.Damage + "', ARMOR = '" + Legs.Armor + "', TYPE = '" + Legs.Type + "', ATTACKSPEED = '" + Legs.AttackSpeed + "', ";
                sql = sql + "RELOADSPEED = '" + Legs.ReloadSpeed + "', HEALTHRESTORE = '" + Legs.HealthRestore + "', HUNGERRESTORE = '" + Legs.HungerRestore + "', HYDRATERESTORE = '" + Legs.HydrateRestore + "', ";
                sql = sql + "STRENGTH = '" + Legs.Strength + "', AGILITY = '" + Legs.Agility + "', ENDURANCE = '" + Legs.Endurance + "', STAMINA = '" + Legs.Stamina + "', CLIP = '" + Legs.Clip + "', MAXCLIP = '" + Legs.MaxClip + "', AMMOTYPE = '" + Legs.ItemAmmoType + "', ";
                sql = sql + "VALUE = '" + Legs.Value + "', PROJ = '" + Legs.ProjectileNumber + "', PRICE = '" + Legs.Price + "' ";
                sql = sql + "WHERE OWNER = '" + Name + "' AND ID = '1';";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                sql = "UPDATE EQUIPMENT SET ";
                sql = sql + "NAME = '" + Feet.Name + "', SPRITE = '" + Feet.Sprite + "', DAMAGE = '" + Feet.Damage + "', ARMOR = '" + Feet.Armor + "', TYPE = '" + Feet.Type + "', ATTACKSPEED = '" + Feet.AttackSpeed + "', ";
                sql = sql + "RELOADSPEED = '" + Feet.ReloadSpeed + "', HEALTHRESTORE = '" + Feet.HealthRestore + "', HUNGERRESTORE = '" + Feet.HungerRestore + "', HYDRATERESTORE = '" + Feet.HydrateRestore + "', ";
                sql = sql + "STRENGTH = '" + Feet.Strength + "', AGILITY = '" + Feet.Agility + "', ENDURANCE = '" + Feet.Endurance + "', STAMINA = '" + Feet.Stamina + "', CLIP = '" + Feet.Clip + "', MAXCLIP = '" + Feet.MaxClip + "', AMMOTYPE = '" + Feet.ItemAmmoType + "', ";
                sql = sql + "VALUE = '" + Feet.Value + "', PROJ = '" + Feet.ProjectileNumber + "', PRICE = '" + Feet.Price + "' ";
                sql = sql + "WHERE OWNER = '" + Name + "' AND ID = '2';";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                sql = "DELETE FROM INVENTORY WHERE OWNER = '" + Name + "'";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                int n = 0;
                for (int i = 0; i < 25; i++)
                {
                    if (Backpack[i].Name != "None")
                    {
                        sql = "INSERT INTO `INVENTORY`";
                        sql = sql + "(`OWNER`,`ID`,`NAME`,`SPRITE`,`DAMAGE`,`ARMOR`,`TYPE`,`ATTACKSPEED`,`RELOADSPEED`,`HEALTHRESTORE`,`HUNGERRESTORE`,`HYDRATERESTORE`,";
                        sql = sql + "`STRENGTH`,`AGILITY`,`ENDURANCE`,`STAMINA`,`CLIP`,`MAXCLIP`,`AMMOTYPE`,`VALUE`,`PROJ`,`PRICE`)";
                        sql = sql + " VALUES ";
                        sql = sql + "('" + Name + "','" + n + "','" + Backpack[i].Name + "','" + Backpack[i].Sprite + "','" + Backpack[i].Damage + "','" + Backpack[i].Armor + "','" + Backpack[i].Type + "','" + Backpack[i].AttackSpeed + "','" + Backpack[i].ReloadSpeed + "','" + Backpack[i].HealthRestore + "','" + Backpack[i].HungerRestore + "',";
                        sql = sql + "'" + Backpack[i].HydrateRestore + "','" + Backpack[i].Strength + "','" + Backpack[i].Agility + "','" + Backpack[i].Endurance + "','" + Backpack[i].Stamina + "','" + Backpack[i].Clip + "','" + Backpack[i].MaxClip + "','" + Backpack[i].ItemAmmoType + "',";
                        sql = sql + "'" + Backpack[i].Value + "','" + Backpack[i].ProjectileNumber + "','" + Backpack[i].Price + "');";
                        n = n + 1;
                        using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        public void LoadPlayerFromDatabase()
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
            {
                conn.Open();
                string sql;

                sql = "SELECT * FROM `PLAYERS` WHERE NAME = '" + Name + "'";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
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
                }

                sql = "SELECT * FROM `MAINWEAPONS` WHERE OWNER = '" + Name + "'";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
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
                        }
                    }
                }

                sql = "SELECT * FROM `SECONDARYWEAPONS` WHERE OWNER = '" + Name + "'";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
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
                        }
                    }
                }

                sql = "SELECT * FROM `EQUIPMENT` WHERE OWNER = '" + Name + "' AND ID = " + 0 + ";";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
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
                        }
                    }
                }

                sql = "SELECT * FROM `EQUIPMENT` WHERE OWNER = '" + Name + "' AND ID = " + 1 + ";";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
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
                        }
                    }
                }

                sql = "SELECT * FROM `EQUIPMENT` WHERE OWNER = '" + Name + "' AND ID = " + 2 + ";";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
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
                        }
                    }
                }

                sql = "SELECT COUNT(*) FROM `INVENTORY` WHERE OWNER = '" + Name + "'";

                object queue;
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    queue = cmd.ExecuteScalar(); 
                }

                int result = ToInt32(queue);

                if (result > 0)
                {
                    for (int i = 0; i < result; i++)
                    {
                        sql = "SELECT * FROM `INVENTORY` WHERE OWNER = '" + Name + "' AND ID = " + i + ";";

                        using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                        {
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
