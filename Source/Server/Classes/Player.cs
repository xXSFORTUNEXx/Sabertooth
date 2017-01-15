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
    class Player
    {
        #region Main Classes
        public NetConnection Connection;
        public SQLiteConnection s_Database;
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

            mainWeapon = new Item("Assault Rifle", 1, 50, 0, (int)ItemType.RangedWeapon, 150, 1000, 0, 0, 0, 0, 0, 0, 0, 30, 30, (int)AmmoType.AssaultRifle);
            offWeapon = new Item("Knife", 1, 100, 0, (int)ItemType.MeleeWeapon, 500, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, (int)ItemType.None);
            Chest = new Item("Shirt", 1, 0, 0, (int)ItemType.Shirt, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            Legs = new Item("Pants", 1, 0, 0, (int)ItemType.Pants, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            Feet = new Item("Shoes", 1, 0, 0, (int)ItemType.Shoes, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

            for (int i = 0; i < 25; i++)
            {
                Backpack[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
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

            mainWeapon = new Item("Pistol", 1, 50, 0, (int)ItemType.RangedWeapon, 750, 1500, 0, 0, 0, 0, 0, 0, 0, 8, 8, (int)AmmoType.Pistol);
            offWeapon = new Item("Knife", 1, 100, 0, (int)ItemType.MeleeWeapon, 650, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, (int)ItemType.None);
            Chest = new Item("Shirt", 1, 0, 0, (int)ItemType.Shirt, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            Legs = new Item("Pants", 1, 0, 0, (int)ItemType.Pants, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            Feet = new Item("Shoes", 1, 0, 0, (int)ItemType.Shoes, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

            for (int i = 0; i < 25; i++)
            {
                Backpack[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
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
                Backpack[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
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
                Backpack[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            }
        }
        #endregion

        #region Voids
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

        public void VitalLoss(string vital)
        {
            if (vital == "food")
            {
                if (Hunger <= 0)
                {
                    //Basically we start die
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
                    //Basically we start die
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
            }
            else if (Experience > (Level * 1000))
            {
                Level += 1;
                Experience = (Experience - (Level * 1000));
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
            }
            else { return; }
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
                s_Player[index].Backpack[slot] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
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
                        s_Player[index].mainWeapon = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

                        hData.SendWeaponsUpdate(s_Server, s_Player, index);
                        hData.SendPlayerInv(s_Server, s_Player, index);
                        hData.SendPlayerEquipment(s_Server, s_Player, index);
                    }
                    else
                    {
                        hData.SendServerMessage(s_Server, "Inventory is full!");
                        return;
                    }
                    break;

                case (int)EquipSlots.OffWeapon:
                    itemSlot = s_Player[index].FindOpenInvSlot(s_Player[index].Backpack);
                    if (itemSlot < 25)
                    {
                        s_Player[index].Backpack[itemSlot] = s_Player[index].offWeapon;
                        s_Player[index].offWeapon = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                        hData.SendWeaponsUpdate(s_Server, s_Player, index);
                        hData.SendPlayerInv(s_Server, s_Player, index);
                        hData.SendPlayerEquipment(s_Server, s_Player, index);
                    }
                    else
                    {
                        hData.SendServerMessage(s_Server, "Inventory is full!");
                        return;
                    }
                    break;

                case (int)EquipSlots.Chest:
                    itemSlot = s_Player[index].FindOpenInvSlot(s_Player[index].Backpack);
                    if (itemSlot < 25)
                    {
                        s_Player[index].Backpack[itemSlot] = s_Player[index].Chest;
                        s_Player[index].Chest = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                        hData.SendWeaponsUpdate(s_Server, s_Player, index);
                        hData.SendPlayerInv(s_Server, s_Player, index);
                        hData.SendPlayerEquipment(s_Server, s_Player, index);
                    }
                    else
                    {
                        hData.SendServerMessage(s_Server, "Inventory is full!");
                        return;
                    }
                    break;

                case (int)EquipSlots.Legs:
                    itemSlot = s_Player[index].FindOpenInvSlot(s_Player[index].Backpack);
                    if (itemSlot < 25)
                    {
                        s_Player[index].Backpack[itemSlot] = s_Player[index].Legs;
                        s_Player[index].Legs = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                        hData.SendWeaponsUpdate(s_Server, s_Player, index);
                        hData.SendPlayerInv(s_Server, s_Player, index);
                        hData.SendPlayerEquipment(s_Server, s_Player, index);
                    }
                    else
                    {
                        hData.SendServerMessage(s_Server, "Inventory is full!");
                        return;
                    }
                    break;

                case (int)EquipSlots.Feet:
                    itemSlot = s_Player[index].FindOpenInvSlot(s_Player[index].Backpack);
                    if (itemSlot < 25)
                    {
                        s_Player[index].Backpack[itemSlot] = s_Player[index].Feet;
                        s_Player[index].Feet = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                        hData.SendWeaponsUpdate(s_Server, s_Player, index);
                        hData.SendPlayerInv(s_Server, s_Player, index);
                        hData.SendPlayerEquipment(s_Server, s_Player, index);
                    }
                    else
                    {
                        hData.SendServerMessage(s_Server, "Inventory is full!");
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
                    s_Map[mapNum].mapItem[mapSlot].IsSpawned = true;
                    s_Map[mapNum].mapItem[mapSlot].ExpireTick = TickCount;

                    s_Player[index].Backpack[slot] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
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
                    hData.SendServerMessage(s_Server, "All map item slots are filled!");
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
                hData.SendServerMessage(s_Server, "Inventory is full!");
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
            s_Database = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;");
            s_Database.Open();
            string sql;
            SQLiteCommand sql_Command;
            sql = "INSERT INTO `PLAYERS`";
            sql = sql + "(`NAME`,`PASSWORD`,`X`,`Y`,`MAP`,`DIRECTION`,`AIMDIRECTION`,`SPRITE`,`LEVEL`,`POINTS`,`HEALTH`,`MAXHEALTH`,`EXPERIENCE`,`MONEY`,`ARMOR`,";
            sql = sql + "`HUNGER`,`HYDRATION`,`STRENGTH`,`AGILITY`,`ENDURANCE`,`STAMINA`,`PISTOLAMMO`,`ASSAULTAMMO`,`ROCKETAMMO`,`GRENADEAMMO`)";
            sql = sql + " VALUES ";
            sql = sql + "('" + Name + "','" + Pass + "','" + X + "','" + Y + "','" + Map + "','" + Direction + "','" + AimDirection + "','" + Sprite + "','" + Level + "',";
            sql = sql + "'" + Points + "','" + Health + "','" + MaxHealth + "','" + Experience + "','" + Money + "','" + Armor + "','" + Hunger + "','" + Hydration + "','" + Strength + "','" + Agility + "',";
            sql = sql + "'" + Endurance + "','" + Stamina + "','" + PistolAmmo + "','" + AssaultAmmo + "','" + RocketAmmo + "','" + GrenadeAmmo + "')";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Command.ExecuteNonQuery();

            sql = "INSERT INTO `MAINWEAPONS`";
            sql = sql + "(`OWNER`,`NAME`,`CLIP`,`MAXCLIP`,`SPRITE`,`DAMAGE`,`ARMOR`,`TYPE`,`ATTACKSPEED`,`RELOADSPEED`,`HEALTHRESTORE`,`HUNGERRESTORE`,`HYDRATERESTORE`,`STRENGTH`,";
            sql = sql + "`AGILITY`,`ENDURANCE`,`STAMINA`,`AMMOTYPE`, `VALUE`)";
            sql = sql + " VALUES ";
            sql = sql + "('" + Name + "','" + mainWeapon.Name + "','" + mainWeapon.Clip + "','" + mainWeapon.MaxClip + "','" + mainWeapon.Sprite + "','" + mainWeapon.Damage + "','" + mainWeapon.Armor + "',";
            sql = sql + "'" + mainWeapon.Type + "','" + mainWeapon.AttackSpeed + "','" + mainWeapon.ReloadSpeed + "','" + mainWeapon.HealthRestore + "','" + mainWeapon.HungerRestore + "','" + mainWeapon.HydrateRestore + "',";
            sql = sql + "'" + mainWeapon.Strength + "','" + mainWeapon.Agility + "','" + mainWeapon.Endurance + "','" + mainWeapon.Stamina + "','" + mainWeapon.ItemAmmoType + "','" + mainWeapon.Value + "')";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Command.ExecuteNonQuery();

            sql = "INSERT INTO `SECONDARYWEAPONS`";
            sql = sql + "(`OWNER`,`NAME`,`CLIP`,`MAXCLIP`,`SPRITE`,`DAMAGE`,`ARMOR`,`TYPE`,`ATTACKSPEED`,`RELOADSPEED`,`HEALTHRESTORE`,`HUNGERRESTORE`,`HYDRATERESTORE`,`STRENGTH`,";
            sql = sql + "`AGILITY`,`ENDURANCE`,`STAMINA`,`AMMOTYPE`, `VALUE`)";
            sql = sql + " VALUES ";
            sql = sql + "('" + Name + "','" + offWeapon.Name + "','" + offWeapon.Clip + "','" + offWeapon.MaxClip + "','" + offWeapon.Sprite + "','" + offWeapon.Damage + "','" + offWeapon.Armor + "',";
            sql = sql + "'" + offWeapon.Type + "','" + offWeapon.AttackSpeed + "','" + offWeapon.ReloadSpeed + "','" + offWeapon.HealthRestore + "','" + offWeapon.HungerRestore + "','" + offWeapon.HydrateRestore + "',";
            sql = sql + "'" + offWeapon.Strength + "','" + offWeapon.Agility + "','" + offWeapon.Endurance + "','" + offWeapon.Stamina + "','" + offWeapon.ItemAmmoType + "','" + offWeapon.Value + "')";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Command.ExecuteNonQuery();

            sql = "INSERT INTO `EQUIPMENT`";
            sql = sql + "(`OWNER`,`ID`,`NAME`,`SPRITE`,`DAMAGE`,`ARMOR`,`TYPE`,`ATTACKSPEED`,`RELOADSPEED`,`HEALTHRESTORE`,`HUNGERRESTORE`,`HYDRATERESTORE`,";
            sql = sql + "`STRENGTH`,`AGILITY`,`ENDURANCE`,`STAMINA`,`CLIP`,`MAXCLIP`,`AMMOTYPE`, `VALUE`)";
            sql = sql + " VALUES ";
            sql = sql + "('" + Name + "','" + 0 + "','" + Chest.Name + "','" + Chest.Sprite + "','" + Chest.Damage + "','" + Chest.Armor + "','" + Chest.Type + "','" + Chest.AttackSpeed + "','" + Chest.ReloadSpeed + "','" + Chest.HealthRestore + "','" + Chest.HungerRestore + "',";
            sql = sql + "'" + Chest.HydrateRestore + "','" + Chest.Strength + "','" + Chest.Agility + "','" + Chest.Endurance + "','" + Chest.Stamina + "','" + Chest.Clip + "','" + Chest.MaxClip + "','" + Chest.ItemAmmoType + "',";
            sql = sql + "'" + Chest.Value + "');";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Command.ExecuteNonQuery();

            sql = "INSERT INTO `EQUIPMENT`";
            sql = sql + "(`OWNER`,`ID`,`NAME`,`SPRITE`,`DAMAGE`,`ARMOR`,`TYPE`,`ATTACKSPEED`,`RELOADSPEED`,`HEALTHRESTORE`,`HUNGERRESTORE`,`HYDRATERESTORE`,";
            sql = sql + "`STRENGTH`,`AGILITY`,`ENDURANCE`,`STAMINA`,`CLIP`,`MAXCLIP`,`AMMOTYPE`, `VALUE`)";
            sql = sql + " VALUES ";
            sql = sql + "('" + Name + "','" + 1 + "','" + Legs.Name + "','" + Legs.Sprite + "','" + Legs.Damage + "','" + Legs.Armor + "','" + Legs.Type + "','" + Legs.AttackSpeed + "','" + Legs.ReloadSpeed + "','" + Legs.HealthRestore + "','" + Legs.HungerRestore + "',";
            sql = sql + "'" + Legs.HydrateRestore + "','" + Legs.Strength + "','" + Legs.Agility + "','" + Legs.Endurance + "','" + Legs.Stamina + "','" + Legs.Clip + "','" + Legs.MaxClip + "','" + Legs.ItemAmmoType + "',";
            sql = sql + "'" + Legs.Value + "');";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Command.ExecuteNonQuery();

            sql = "INSERT INTO `EQUIPMENT`";
            sql = sql + "(`OWNER`,`ID`,`NAME`,`SPRITE`,`DAMAGE`,`ARMOR`,`TYPE`,`ATTACKSPEED`,`RELOADSPEED`,`HEALTHRESTORE`,`HUNGERRESTORE`,`HYDRATERESTORE`,";
            sql = sql + "`STRENGTH`,`AGILITY`,`ENDURANCE`,`STAMINA`,`CLIP`,`MAXCLIP`,`AMMOTYPE`, `VALUE`)";
            sql = sql + " VALUES ";
            sql = sql + "('" + Name + "','" + 2 + "','" + Feet.Name + "','" + Feet.Sprite + "','" + Feet.Damage + "','" + Feet.Armor + "','" + Feet.Type + "','" + Feet.AttackSpeed + "','" + Feet.ReloadSpeed + "','" + Feet.HealthRestore + "','" + Feet.HungerRestore + "',";
            sql = sql + "'" + Feet.HydrateRestore + "','" + Feet.Strength + "','" + Feet.Agility + "','" + Feet.Endurance + "','" + Feet.Stamina + "','" + Feet.Clip + "','" + Feet.MaxClip + "','" + Feet.ItemAmmoType + "',";
            sql = sql + "'" + Feet.Value + "');";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Command.ExecuteNonQuery();
            s_Database.Close();
        }

        public void SavePlayerToDatabase()
        {
            s_Database = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;");
            s_Database.Open();
            string sql;
            SQLiteCommand sql_Command;
            sql = "UPDATE PLAYERS SET ";
            sql = sql + "NAME = '" + Name + "', PASSWORD = '" + Pass + "', X = '" + X + "', Y = '" + Y + "', MAP = '" + Map + "', DIRECTION = '" + Direction + "', ";
            sql = sql + "AIMDIRECTION = '" + AimDirection + "', SPRITE = '" + Sprite + "', LEVEL = '" + Level + "', POINTS = '" + Points + "', HEALTH = '" + Health + "', MAXHEALTH = '" + MaxHealth + "', EXPERIENCE = '" + Experience + "', ";
            sql = sql + "MONEY = '" + Money + "', ARMOR = '" + Armor + "', HUNGER = '" + Hunger + "', HYDRATION = '" + Hydration + "', STRENGTH = '" + Strength + "', AGILITY = '" + Agility + "', ENDURANCE = '" + Endurance + "', ";
            sql = sql + "STAMINA = '" + Stamina + "', PISTOLAMMO = '" + PistolAmmo + "', ASSAULTAMMO = '" + AssaultAmmo + "', ROCKETAMMO = '" + RocketAmmo + "', GRENADEAMMO = '" + GrenadeAmmo + "' ";
            sql = sql + "WHERE NAME = '" + Name + "';";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Command.ExecuteNonQuery();

            sql = "UPDATE MAINWEAPONS SET ";
            sql = sql + "NAME = '" + mainWeapon.Name + "', CLIP = '" + mainWeapon.Clip + "', MAXCLIP = '" + mainWeapon.MaxClip + "', SPRITE = '" + mainWeapon.Sprite + "', DAMAGE = '" + mainWeapon.Damage + "', ARMOR = '" + mainWeapon.Armor + "', ";
            sql = sql + "TYPE = '" + mainWeapon.Type + "', ATTACKSPEED = '" + mainWeapon.AttackSpeed + "', RELOADSPEED = '" + mainWeapon.ReloadSpeed + "', HEALTHRESTORE = '" + mainWeapon.HealthRestore + "', HUNGERRESTORE = '" + mainWeapon.HungerRestore + "', ";
            sql = sql + "HYDRATERESTORE = '" + mainWeapon.HydrateRestore + "', STRENGTH = '" + mainWeapon.Strength + "', AGILITY = '" + mainWeapon.Agility + "', ENDURANCE = '" + mainWeapon.Endurance + "', STAMINA = '" + mainWeapon.Stamina + "', ";
            sql = sql + "AMMOTYPE = '" + mainWeapon.ItemAmmoType + "',  VALUE = '" + mainWeapon.Value + "' "; 
            sql = sql + "WHERE OWNER = '" + Name + "';";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Command.ExecuteNonQuery();

            sql = "UPDATE SECONDARYWEAPONS SET ";
            sql = sql + "NAME = '" + offWeapon.Name + "', CLIP = '" + offWeapon.Clip + "', MAXCLIP = '" + offWeapon.MaxClip + "', SPRITE = '" + offWeapon.Sprite + "', DAMAGE = '" + offWeapon.Damage + "', ARMOR = '" + offWeapon.Armor + "', ";
            sql = sql + "TYPE = '" + offWeapon.Type + "', ATTACKSPEED = '" + offWeapon.AttackSpeed + "', RELOADSPEED = '" + offWeapon.ReloadSpeed + "', HEALTHRESTORE = '" + offWeapon.HealthRestore + "', HUNGERRESTORE = '" + offWeapon.HungerRestore + "', ";
            sql = sql + "HYDRATERESTORE = '" + offWeapon.HydrateRestore + "', STRENGTH = '" + offWeapon.Strength + "', AGILITY = '" + offWeapon.Agility + "', ENDURANCE = '" + offWeapon.Endurance + "', STAMINA = '" + offWeapon.Stamina + "', ";
            sql = sql + "AMMOTYPE = '" + offWeapon.ItemAmmoType + "',  VALUE = '" + offWeapon.Value + "' ";
            sql = sql + "WHERE OWNER = '" + Name + "';";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Command.ExecuteNonQuery();

            sql = "UPDATE EQUIPMENT SET ";
            sql = sql + "NAME = '" + Chest.Name + "', SPRITE = '" + Chest.Sprite + "', DAMAGE = '" + Chest.Damage + "', ARMOR = '" + Chest.Armor + "', TYPE = '" + Chest.Type + "', ATTACKSPEED = '" + Chest.AttackSpeed + "', ";
            sql = sql + "RELOADSPEED = '" + Chest.ReloadSpeed + "', HEALTHRESTORE = '" + Chest.HealthRestore + "', HUNGERRESTORE = '" + Chest.HungerRestore + "', HYDRATERESTORE = '" + Chest.HydrateRestore + "', ";
            sql = sql + "STRENGTH = '" + Chest.Strength + "', AGILITY = '" + Chest.Agility + "', ENDURANCE = '" + Chest.Endurance + "', STAMINA = '" + Chest.Stamina + "', CLIP = '" + Chest.Clip + "', MAXCLIP = '" + Chest.MaxClip + "', AMMOTYPE = '" + Chest.ItemAmmoType + "', ";
            sql = sql + "VALUE = '" + Chest.Value + "' ";
            sql = sql + "WHERE OWNER = '" + Name + "' AND ID = '0';";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Command.ExecuteNonQuery();

            sql = "UPDATE EQUIPMENT SET ";
            sql = sql + "NAME = '" + Legs.Name + "', SPRITE = '" + Legs.Sprite + "', DAMAGE = '" + Legs.Damage + "', ARMOR = '" + Legs.Armor + "', TYPE = '" + Legs.Type + "', ATTACKSPEED = '" + Legs.AttackSpeed + "', ";
            sql = sql + "RELOADSPEED = '" + Legs.ReloadSpeed + "', HEALTHRESTORE = '" + Legs.HealthRestore + "', HUNGERRESTORE = '" + Legs.HungerRestore + "', HYDRATERESTORE = '" + Legs.HydrateRestore + "', ";
            sql = sql + "STRENGTH = '" + Legs.Strength + "', AGILITY = '" + Legs.Agility + "', ENDURANCE = '" + Legs.Endurance + "', STAMINA = '" + Legs.Stamina + "', CLIP = '" + Legs.Clip + "', MAXCLIP = '" + Legs.MaxClip + "', AMMOTYPE = '" + Legs.ItemAmmoType + "', ";
            sql = sql + "VALUE = '" + Legs.Value + "' ";
            sql = sql + "WHERE OWNER = '" + Name + "' AND ID = '1';";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Command.ExecuteNonQuery();

            sql = "UPDATE EQUIPMENT SET ";
            sql = sql + "NAME = '" + Feet.Name + "', SPRITE = '" + Feet.Sprite + "', DAMAGE = '" + Feet.Damage + "', ARMOR = '" + Feet.Armor + "', TYPE = '" + Feet.Type + "', ATTACKSPEED = '" + Feet.AttackSpeed + "', ";
            sql = sql + "RELOADSPEED = '" + Feet.ReloadSpeed + "', HEALTHRESTORE = '" + Feet.HealthRestore + "', HUNGERRESTORE = '" + Feet.HungerRestore + "', HYDRATERESTORE = '" + Feet.HydrateRestore + "', ";
            sql = sql + "STRENGTH = '" + Feet.Strength + "', AGILITY = '" + Feet.Agility + "', ENDURANCE = '" + Feet.Endurance + "', STAMINA = '" + Feet.Stamina + "', CLIP = '" + Feet.Clip + "', MAXCLIP = '" + Feet.MaxClip + "', AMMOTYPE = '" + Feet.ItemAmmoType + "', ";
            sql = sql + "VALUE = '" + Feet.Value + "' ";
            sql = sql + "WHERE OWNER = '" + Name + "' AND ID = '2';";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Command.ExecuteNonQuery();

            sql = "DELETE FROM INVENTORY WHERE OWNER = '" + Name + "'";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Command.ExecuteNonQuery();

            for (int i = 0; i < 25; i++)
            {
                if (Backpack[i].Name != "None")
                {
                    sql = "INSERT INTO `INVENTORY`";
                    sql = sql + "(`OWNER`,`ID`,`NAME`,`SPRITE`,`DAMAGE`,`ARMOR`,`TYPE`,`ATTACKSPEED`,`RELOADSPEED`,`HEALTHRESTORE`,`HUNGERRESTORE`,`HYDRATERESTORE`,";
                    sql = sql + "`STRENGTH`,`AGILITY`,`ENDURANCE`,`STAMINA`,`CLIP`,`MAXCLIP`,`AMMOTYPE`, `VALUE`)";
                    sql = sql + " VALUES ";
                    sql = sql + "('" + Name + "','" + i + "','" + Backpack[i].Name + "','" + Backpack[i].Sprite + "','" + Backpack[i].Damage + "','" + Backpack[i].Armor + "','" + Backpack[i].Type + "','" + Backpack[i].AttackSpeed + "','" + Backpack[i].ReloadSpeed + "','" + Backpack[i].HealthRestore + "','" + Backpack[i].HungerRestore + "',";
                    sql = sql + "'" + Backpack[i].HydrateRestore + "','" + Backpack[i].Strength + "','" + Backpack[i].Agility + "','" + Backpack[i].Endurance + "','" + Backpack[i].Stamina + "','" + Backpack[i].Clip + "','" + Backpack[i].MaxClip + "','" + Backpack[i].ItemAmmoType + "',";
                    sql = sql + "'" + Backpack[i].Value + "');";
                    sql_Command = new SQLiteCommand(sql, s_Database);
                    sql_Command.ExecuteNonQuery();
                }
            }
            s_Database.Close();
        }

        public void LoadPlayerFromDatabase()
        {
            s_Database = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;");
            s_Database.Open();
            string sql;

            sql = "SELECT * FROM `PLAYERS` WHERE NAME = '" + Name + "'";

            SQLiteCommand sql_Command = new SQLiteCommand(sql, s_Database);
            SQLiteDataReader sql_Reader = sql_Command.ExecuteReader();

            while (sql_Reader.Read())
            {
                Name = sql_Reader["NAME"].ToString();
                Pass = sql_Reader["PASSWORD"].ToString();
                X = ToInt32(sql_Reader["X"].ToString());
                Y = ToInt32(sql_Reader["Y"].ToString());
                Map = ToInt32(sql_Reader["MAP"].ToString());
                Direction = ToInt32(sql_Reader["DIRECTION"].ToString());
                AimDirection = ToInt32(sql_Reader["AIMDIRECTION"].ToString());
                Sprite = ToInt32(sql_Reader["SPRITE"].ToString());
                Level = ToInt32(sql_Reader["LEVEL"].ToString());
                Points = ToInt32(sql_Reader["POINTS"].ToString());
                Health = ToInt32(sql_Reader["HEALTH"].ToString());
                MaxHealth = ToInt32(sql_Reader["MAXHEALTH"].ToString());
                Experience = ToInt32(sql_Reader["EXPERIENCE"].ToString());
                Money = ToInt32(sql_Reader["MONEY"].ToString());
                Armor = ToInt32(sql_Reader["ARMOR"].ToString());
                Hunger = ToInt32(sql_Reader["HUNGER"].ToString());
                Hydration = ToInt32(sql_Reader["HYDRATION"].ToString());
                Strength = ToInt32(sql_Reader["STRENGTH"].ToString());
                Agility = ToInt32(sql_Reader["AGILITY"].ToString());
                Endurance = ToInt32(sql_Reader["ENDURANCE"].ToString());
                Stamina = ToInt32(sql_Reader["STAMINA"].ToString());
                PistolAmmo = ToInt32(sql_Reader["PISTOLAMMO"].ToString());
                AssaultAmmo = ToInt32(sql_Reader["ASSAULTAMMO"].ToString());
                RocketAmmo = ToInt32(sql_Reader["ROCKETAMMO"].ToString());
                GrenadeAmmo = ToInt32(sql_Reader["GRENADEAMMO"].ToString());
            }

            sql = "SELECT * FROM `MAINWEAPONS` WHERE OWNER = '" + Name + "'";

            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Reader = sql_Command.ExecuteReader();

            while (sql_Reader.Read())
            {
                mainWeapon.Name = sql_Reader["NAME"].ToString();
                mainWeapon.Clip = ToInt32(sql_Reader["CLIP"].ToString());
                mainWeapon.MaxClip = ToInt32(sql_Reader["MAXCLIP"].ToString());
                mainWeapon.Sprite = ToInt32(sql_Reader["SPRITE"].ToString());
                mainWeapon.Damage = ToInt32(sql_Reader["DAMAGE"].ToString());
                mainWeapon.Armor = ToInt32(sql_Reader["ARMOR"].ToString());
                mainWeapon.Type = ToInt32(sql_Reader["TYPE"].ToString());
                mainWeapon.AttackSpeed = ToInt32(sql_Reader["ATTACKSPEED"].ToString());
                mainWeapon.ReloadSpeed = ToInt32(sql_Reader["RELOADSPEED"].ToString());
                mainWeapon.HealthRestore = ToInt32(sql_Reader["HEALTHRESTORE"].ToString());
                mainWeapon.HungerRestore = ToInt32(sql_Reader["HUNGERRESTORE"].ToString());
                mainWeapon.HydrateRestore = ToInt32(sql_Reader["HYDRATERESTORE"].ToString());
                mainWeapon.Strength = ToInt32(sql_Reader["STRENGTH"].ToString());
                mainWeapon.Agility = ToInt32(sql_Reader["AGILITY"].ToString());
                mainWeapon.Endurance = ToInt32(sql_Reader["ENDURANCE"].ToString());
                mainWeapon.Stamina = ToInt32(sql_Reader["STAMINA"].ToString());
                mainWeapon.ItemAmmoType = ToInt32(sql_Reader["AMMOTYPE"].ToString());
                mainWeapon.Value = ToInt32(sql_Reader["VALUE"].ToString());
            }

            sql = "SELECT * FROM `SECONDARYWEAPONS` WHERE OWNER = '" + Name + "'";

            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Reader = sql_Command.ExecuteReader();

            while (sql_Reader.Read())
            {
                offWeapon.Name = sql_Reader["NAME"].ToString();
                offWeapon.Clip = ToInt32(sql_Reader["CLIP"].ToString());
                offWeapon.MaxClip = ToInt32(sql_Reader["MAXCLIP"].ToString());
                offWeapon.Sprite = ToInt32(sql_Reader["SPRITE"].ToString());
                offWeapon.Damage = ToInt32(sql_Reader["DAMAGE"].ToString());
                offWeapon.Armor = ToInt32(sql_Reader["ARMOR"].ToString());
                offWeapon.Type = ToInt32(sql_Reader["TYPE"].ToString());
                offWeapon.AttackSpeed = ToInt32(sql_Reader["ATTACKSPEED"].ToString());
                offWeapon.ReloadSpeed = ToInt32(sql_Reader["RELOADSPEED"].ToString());
                offWeapon.HealthRestore = ToInt32(sql_Reader["HEALTHRESTORE"].ToString());
                offWeapon.HungerRestore = ToInt32(sql_Reader["HUNGERRESTORE"].ToString());
                offWeapon.HydrateRestore = ToInt32(sql_Reader["HYDRATERESTORE"].ToString());
                offWeapon.Strength = ToInt32(sql_Reader["STRENGTH"].ToString());
                offWeapon.Agility = ToInt32(sql_Reader["AGILITY"].ToString());
                offWeapon.Endurance = ToInt32(sql_Reader["ENDURANCE"].ToString());
                offWeapon.Stamina = ToInt32(sql_Reader["STAMINA"].ToString());
                offWeapon.ItemAmmoType = ToInt32(sql_Reader["AMMOTYPE"].ToString());
                offWeapon.Value = ToInt32(sql_Reader["VALUE"].ToString());
            }

            sql = "SELECT * FROM `EQUIPMENT` WHERE OWNER = '" + Name + "' AND ID = " + 0 + ";";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Reader = sql_Command.ExecuteReader();
            while (sql_Reader.Read())
            {
                Chest.Name = sql_Reader["NAME"].ToString();
                Chest.Sprite = ToInt32(sql_Reader["SPRITE"].ToString());
                Chest.Damage = ToInt32(sql_Reader["DAMAGE"].ToString());
                Chest.Armor = ToInt32(sql_Reader["ARMOR"].ToString());
                Chest.Type = ToInt32(sql_Reader["TYPE"].ToString());
                Chest.AttackSpeed = ToInt32(sql_Reader["ATTACKSPEED"].ToString());
                Chest.ReloadSpeed = ToInt32(sql_Reader["RELOADSPEED"].ToString());
                Chest.HealthRestore = ToInt32(sql_Reader["HEALTHRESTORE"].ToString());
                Chest.HungerRestore = ToInt32(sql_Reader["HUNGERRESTORE"].ToString());
                Chest.HydrateRestore = ToInt32(sql_Reader["HYDRATERESTORE"].ToString());
                Chest.Strength = ToInt32(sql_Reader["STRENGTH"].ToString());
                Chest.Agility = ToInt32(sql_Reader["AGILITY"].ToString());
                Chest.Endurance = ToInt32(sql_Reader["ENDURANCE"].ToString());
                Chest.Stamina = ToInt32(sql_Reader["STAMINA"].ToString());
                Chest.Clip = ToInt32(sql_Reader["CLIP"].ToString());
                Chest.MaxClip = ToInt32(sql_Reader["MAXCLIP"].ToString());
                Chest.ItemAmmoType = ToInt32(sql_Reader["AMMOTYPE"].ToString());
                Chest.Value = ToInt32(sql_Reader["VALUE"].ToString());
            }

            sql = "SELECT * FROM `EQUIPMENT` WHERE OWNER = '" + Name + "' AND ID = " + 1 + ";";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Reader = sql_Command.ExecuteReader();
            while (sql_Reader.Read())
            {
                Legs.Name = sql_Reader["NAME"].ToString();
                Legs.Sprite = ToInt32(sql_Reader["SPRITE"].ToString());
                Legs.Damage = ToInt32(sql_Reader["DAMAGE"].ToString());
                Legs.Armor = ToInt32(sql_Reader["ARMOR"].ToString());
                Legs.Type = ToInt32(sql_Reader["TYPE"].ToString());
                Legs.AttackSpeed = ToInt32(sql_Reader["ATTACKSPEED"].ToString());
                Legs.ReloadSpeed = ToInt32(sql_Reader["RELOADSPEED"].ToString());
                Legs.HealthRestore = ToInt32(sql_Reader["HEALTHRESTORE"].ToString());
                Legs.HungerRestore = ToInt32(sql_Reader["HUNGERRESTORE"].ToString());
                Legs.HydrateRestore = ToInt32(sql_Reader["HYDRATERESTORE"].ToString());
                Legs.Strength = ToInt32(sql_Reader["STRENGTH"].ToString());
                Legs.Agility = ToInt32(sql_Reader["AGILITY"].ToString());
                Legs.Endurance = ToInt32(sql_Reader["ENDURANCE"].ToString());
                Legs.Stamina = ToInt32(sql_Reader["STAMINA"].ToString());
                Legs.Clip = ToInt32(sql_Reader["CLIP"].ToString());
                Legs.MaxClip = ToInt32(sql_Reader["MAXCLIP"].ToString());
                Legs.ItemAmmoType = ToInt32(sql_Reader["AMMOTYPE"].ToString());
                Legs.Value = ToInt32(sql_Reader["VALUE"].ToString());
            }

            sql = "SELECT * FROM `EQUIPMENT` WHERE OWNER = '" + Name + "' AND ID = " + 2 + ";";
            sql_Command = new SQLiteCommand(sql, s_Database);
            sql_Reader = sql_Command.ExecuteReader();
            while (sql_Reader.Read())
            {
                Feet.Name = sql_Reader["NAME"].ToString();
                Feet.Sprite = ToInt32(sql_Reader["SPRITE"].ToString());
                Feet.Damage = ToInt32(sql_Reader["DAMAGE"].ToString());
                Feet.Armor = ToInt32(sql_Reader["ARMOR"].ToString());
                Feet.Type = ToInt32(sql_Reader["TYPE"].ToString());
                Feet.AttackSpeed = ToInt32(sql_Reader["ATTACKSPEED"].ToString());
                Feet.ReloadSpeed = ToInt32(sql_Reader["RELOADSPEED"].ToString());
                Feet.HealthRestore = ToInt32(sql_Reader["HEALTHRESTORE"].ToString());
                Feet.HungerRestore = ToInt32(sql_Reader["HUNGERRESTORE"].ToString());
                Feet.HydrateRestore = ToInt32(sql_Reader["HYDRATERESTORE"].ToString());
                Feet.Strength = ToInt32(sql_Reader["STRENGTH"].ToString());
                Feet.Agility = ToInt32(sql_Reader["AGILITY"].ToString());
                Feet.Endurance = ToInt32(sql_Reader["ENDURANCE"].ToString());
                Feet.Stamina = ToInt32(sql_Reader["STAMINA"].ToString());
                Feet.Clip = ToInt32(sql_Reader["CLIP"].ToString());
                Feet.MaxClip = ToInt32(sql_Reader["MAXCLIP"].ToString());
                Feet.ItemAmmoType = ToInt32(sql_Reader["AMMOTYPE"].ToString());
                Feet.Value = ToInt32(sql_Reader["VALUE"].ToString());
            }
            sql = "SELECT COUNT(*) FROM `INVENTORY` WHERE OWNER = '" + Name + "'";
            sql_Command = new SQLiteCommand(sql, s_Database);
            int result = int.Parse(sql_Command.ExecuteScalar().ToString());

            if (result > 0)
            {
                for (int i = 0; i < result; i++)
                {
                    sql = "SELECT * FROM `INVENTORY` WHERE OWNER = '" + Name + "' AND ID = " + i + ";";

                    sql_Command = new SQLiteCommand(sql, s_Database);
                    sql_Reader = sql_Command.ExecuteReader();

                    while (sql_Reader.Read())
                    {
                        Backpack[i].Name = sql_Reader["NAME"].ToString();
                        Backpack[i].Sprite = ToInt32(sql_Reader["SPRITE"].ToString());
                        Backpack[i].Damage = ToInt32(sql_Reader["DAMAGE"].ToString());
                        Backpack[i].Armor = ToInt32(sql_Reader["ARMOR"].ToString());
                        Backpack[i].Type = ToInt32(sql_Reader["TYPE"].ToString());
                        Backpack[i].AttackSpeed = ToInt32(sql_Reader["ATTACKSPEED"].ToString());
                        Backpack[i].ReloadSpeed = ToInt32(sql_Reader["RELOADSPEED"].ToString());
                        Backpack[i].HealthRestore = ToInt32(sql_Reader["HEALTHRESTORE"].ToString());
                        Backpack[i].HungerRestore = ToInt32(sql_Reader["HUNGERRESTORE"].ToString());
                        Backpack[i].HydrateRestore = ToInt32(sql_Reader["HYDRATERESTORE"].ToString());
                        Backpack[i].Strength = ToInt32(sql_Reader["STRENGTH"].ToString());
                        Backpack[i].Agility = ToInt32(sql_Reader["AGILITY"].ToString());
                        Backpack[i].Endurance = ToInt32(sql_Reader["ENDURANCE"].ToString());
                        Backpack[i].Stamina = ToInt32(sql_Reader["STAMINA"].ToString());
                        Backpack[i].Clip = ToInt32(sql_Reader["CLIP"].ToString());
                        Backpack[i].MaxClip = ToInt32(sql_Reader["MAXCLIP"].ToString());
                        Backpack[i].ItemAmmoType = ToInt32(sql_Reader["AMMOTYPE"].ToString());
                        Backpack[i].Value = ToInt32(sql_Reader["VALUE"].ToString());
                    }
                }
            }

            s_Database.Close();
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
