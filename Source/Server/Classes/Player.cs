using Lidgren.Network;
using System;
using System.Data.SqlClient;
using static System.Convert;
using static System.Environment;
using static SabertoothServer.Server;
using AccountKeyGenClass;
using static SabertoothServer.Globals;
using static System.IO.File;
using SFML.Window;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

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
        public HotBar[] hotBar = new HotBar[MAX_PLAYER_HOTBAR];
        public SpellBook[] SpellBook = new SpellBook[MAX_PLAYER_SPELLBOOK];
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

        //Volatile Variables
        public int timeTick;
        public int Target;        

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

            MainHand = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, false, 1);
            OffHand = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, false, 1);
            Chest = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, false, 1);
            Legs = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, false, 1);
            Feet = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, false, 1);

            for (int i = 0; i < MAX_INV_SLOTS; i++)
            {
                Backpack[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, false, 1);
            }

            for (int i = 0; i < MAX_BANK_SLOTS; i++)
            {
                Bank[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, false, 1);
            }

            for (int i = 0; i < MAX_PLAYER_QUEST_LIST; i++)
            {
                QuestList[i] = 0;
                QuestStatus[i] = 0;
            }

            int h = 0;
            hotBar[h] = new HotBar(Keyboard.Key.Num1, -1, -1); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num2, -1, -1); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num3, -1, -1); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num4, -1, -1); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num5, -1, -1); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num6, -1, -1); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num7, -1, -1); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num8, -1, -1); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num9, -1, -1); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, -1, -1);

            for (int s = 0; s < MAX_PLAYER_SPELLBOOK; s++)
            {
                SpellBook[s] = new SpellBook(-1);                
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

            MainHand = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, false, 1);
            OffHand = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, false, 1);
            Chest = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, false, 1);
            Legs = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, false, 1);
            Feet = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, false, 1);

            for (int i = 0; i < MAX_INV_SLOTS; i++)
            {
                Backpack[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, false, 1);
            }

            for (int i = 0; i < MAX_BANK_SLOTS; i++)
            {
                Bank[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, false, 1);
            }

            for (int i = 0; i < MAX_PLAYER_QUEST_LIST; i++)
            {
                QuestList[i] = 0;
                QuestStatus[i] = 0;
            }

            int h = 0;
            hotBar[h] = new HotBar(Keyboard.Key.Num1, -1, -1); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num2, -1, -1); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num3, -1, -1); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num4, -1, -1); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num5, -1, -1); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num6, -1, -1); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num7, -1, -1); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num8, -1, -1); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num9, -1, -1); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, -1, -1);

            for (int s = 0; s < MAX_PLAYER_SPELLBOOK; s++)
            {
                SpellBook[s] = new SpellBook(-1);
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
                Backpack[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, false, 1);
            }

            for (int i = 0; i < MAX_BANK_SLOTS; i++)
            {
                Bank[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, false, 1);
            }

            for (int i = 0; i < MAX_PLAYER_QUEST_LIST; i++)
            {
                QuestList[i] = 0;
                QuestStatus[i] = 0;
            }

            int h = 0;
            hotBar[h] = new HotBar(Keyboard.Key.Num1, -1, -1); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num2, -1, -1); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num3, -1, -1); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num4, -1, -1); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num5, -1, -1); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num6, -1, -1); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num7, -1, -1); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num8, -1, -1); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num9, -1, -1); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, -1, -1);

            for (int s = 0; s < MAX_PLAYER_SPELLBOOK; s++)
            {
                SpellBook[s] = new SpellBook(-1);
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
                Backpack[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, false, 1);
            }

            for (int i = 0; i < MAX_BANK_SLOTS; i++)
            {
                Bank[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, false, 1);
            }

            for (int i = 0; i < MAX_PLAYER_QUEST_LIST; i++)
            {
                QuestList[i] = 0;
                QuestStatus[i] = 0;
            }

            int h = 0;
            hotBar[h] = new HotBar(Keyboard.Key.Num1, -1, -1); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num2, -1, -1); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num3, -1, -1); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num4, -1, -1); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num5, -1, -1); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num6, -1, -1); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num7, -1, -1); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num8, -1, -1); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num9, -1, -1); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, -1, -1);

            for (int s = 0; s < MAX_PLAYER_SPELLBOOK; s++)
            {
                SpellBook[s] = new SpellBook(-1);
            }
        }
        #endregion

        #region Methods
        //updating the last logged in so we can keep track of the last time they logged in for some reason (perhaps create a table of login timestamps for tracking??)
        public void UpdateLastLogged()
        {
            LastLoggedIn = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff");
        }

        //Regain health if weeeee need it
        public void RegenHealth()
        {            
            if (Health < MaxHealth)
            {
                Health += (Stamina * 5);
            }

            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }
        }

        //Regain mana if we need it
        public void RegainMana()
        {
            if (Mana < MaxMana)
            {
                Mana += (Energy * 5);
            }

            if (Mana > MaxMana)
            {
                Mana = MaxMana;
            }
        }

        //Check to see if the plater leveled up
        public void CheckPlayerLevelUp()
        {
            int exptoLevel = (Level * 450); //set the bar to 450 times whatever level you are
            if (Level == MAX_LEVEL) { Experience = exptoLevel; return; }    //if you are max level then we just set xp bar to max and go to bed
            if (Experience >= exptoLevel)   //if you are over the xp to level
            {
                while (Experience >= exptoLevel)    //as long as we are over a few levels it will loop instead of just 1 instance
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

        public void DepositItem(int index, int slot, int value)
        {
            if (players[index].Backpack[slot].Name != "None")
            {
                if (players[index].Backpack[slot].Stackable)
                {
                    int existSlot = FindSameBankItem(players[index].Bank, players[index].Backpack[slot]);

                    if (existSlot < MAX_BANK_SLOTS)
                    {
                        if (players[index].Backpack[slot].Value > value)
                        {
                            players[index].Backpack[slot].Value -= value;
                            players[index].Bank[existSlot].Value += value;
                            HandleData.SendPlayerBank(index);
                            HandleData.SendPlayerInv(index);
                        }
                        else
                        {
                            players[index].Bank[existSlot].Value += value;
                            players[index].Backpack[slot] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, false, 1);
                            HandleData.SendPlayerBank(index);
                            HandleData.SendPlayerInv(index);
                        }
                    }
                    else
                    {
                        int newSlot = FindOpenBankSlot(players[index].Bank);

                        if (newSlot < MAX_BANK_SLOTS)
                        {
                            if (players[index].Backpack[slot].Value > value)
                            {
                                players[index].Bank[newSlot].Name = players[index].Backpack[slot].Name;
                                players[index].Bank[newSlot].Sprite = players[index].Backpack[slot].Sprite;
                                players[index].Bank[newSlot].Damage = players[index].Backpack[slot].Damage;
                                players[index].Bank[newSlot].Armor = players[index].Backpack[slot].Armor;
                                players[index].Bank[newSlot].Type = players[index].Backpack[slot].Type;
                                players[index].Bank[newSlot].AttackSpeed = players[index].Backpack[slot].AttackSpeed;
                                players[index].Bank[newSlot].HealthRestore = players[index].Backpack[slot].HealthRestore;
                                players[index].Bank[newSlot].ManaRestore = players[index].Backpack[slot].ManaRestore;
                                players[index].Bank[newSlot].Strength = players[index].Backpack[slot].Strength;
                                players[index].Bank[newSlot].Agility = players[index].Backpack[slot].Agility;
                                players[index].Bank[newSlot].Intelligence = players[index].Backpack[slot].Intelligence;
                                players[index].Bank[newSlot].Energy = players[index].Backpack[slot].Energy;
                                players[index].Bank[newSlot].Stamina = players[index].Backpack[slot].Stamina;
                                players[index].Bank[newSlot].Value = value;
                                players[index].Bank[newSlot].Price = players[index].Backpack[slot].Price;
                                players[index].Bank[newSlot].Rarity = players[index].Backpack[slot].Rarity;
                                players[index].Bank[newSlot].CoolDown = players[index].Backpack[slot].CoolDown;
                                players[index].Bank[newSlot].AddMaxHealth = players[index].Backpack[slot].AddMaxHealth;
                                players[index].Bank[newSlot].AddMaxMana = players[index].Backpack[slot].AddMaxMana;
                                players[index].Bank[newSlot].BonusXP = players[index].Backpack[slot].BonusXP;
                                players[index].Bank[newSlot].SpellNum = players[index].Backpack[slot].SpellNum;
                                players[index].Bank[newSlot].Stackable = players[index].Backpack[slot].Stackable;
                                players[index].Bank[newSlot].MaxStack = players[index].Backpack[slot].MaxStack;

                                players[index].Backpack[slot].Value -= value;
                                HandleData.SendPlayerBank(index);
                                HandleData.SendPlayerInv(index);
                            }
                            else
                            {
                                players[index].Bank[newSlot] = players[index].Backpack[slot];
                                players[index].Backpack[slot] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, false, 1);
                                HandleData.SendPlayerBank(index);
                                HandleData.SendPlayerInv(index);
                            }
                        }
                        else
                        {
                            HandleData.SendServerMessageTo(Connection, "You bank is full!");
                        }
                    }
                }
                else
                {
                    int newSlot = FindOpenBankSlot(players[index].Bank);

                    if (newSlot < MAX_BANK_SLOTS)
                    {
                        players[index].Bank[newSlot] = players[index].Backpack[slot];
                        players[index].Backpack[slot] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, false, 1);
                        HandleData.SendPlayerBank(index);
                        HandleData.SendPlayerInv(index);
                    }
                    else
                    {
                        HandleData.SendServerMessageTo(Connection, "You bank is full!");
                    }
                }
            }
        }

        public void WithdrawItem(int index, int slot, int value)
        {
            if (players[index].Bank[slot].Name != "None")
            {
                if (players[index].Bank[slot].Stackable)
                {
                    int existSlot = FindSameInvItem(players[index].Backpack, players[index].Bank[slot]);

                    if (existSlot < MAX_INV_SLOTS)
                    {
                        if (players[index].Bank[slot].Value > value)
                        {
                            players[index].Bank[slot].Value -= value;
                            players[index].Backpack[existSlot].Value += value;
                            HandleData.SendPlayerBank(index);
                            HandleData.SendPlayerInv(index);
                        }
                        else
                        {
                            players[index].Backpack[existSlot].Value += value;
                            players[index].Bank[slot] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, false, 1);
                            HandleData.SendPlayerBank(index);
                            HandleData.SendPlayerInv(index);
                        }
                    }
                    else
                    {
                        int newSlot = FindOpenInvSlot(players[index].Backpack);

                        if (newSlot < MAX_INV_SLOTS)
                        {
                            if (players[index].Bank[slot].Value > value)
                            {
                                players[index].Backpack[newSlot].Name = players[index].Bank[slot].Name;
                                players[index].Backpack[newSlot].Sprite = players[index].Bank[slot].Sprite;
                                players[index].Backpack[newSlot].Damage = players[index].Bank[slot].Damage;
                                players[index].Backpack[newSlot].Armor = players[index].Bank[slot].Armor;
                                players[index].Backpack[newSlot].Type = players[index].Bank[slot].Type;
                                players[index].Backpack[newSlot].AttackSpeed = players[index].Bank[slot].AttackSpeed;
                                players[index].Backpack[newSlot].HealthRestore = players[index].Bank[slot].HealthRestore;
                                players[index].Backpack[newSlot].ManaRestore = players[index].Bank[slot].ManaRestore;
                                players[index].Backpack[newSlot].Strength = players[index].Bank[slot].Strength;
                                players[index].Backpack[newSlot].Agility = players[index].Bank[slot].Agility;
                                players[index].Backpack[newSlot].Intelligence = players[index].Bank[slot].Intelligence;
                                players[index].Backpack[newSlot].Energy = players[index].Bank[slot].Energy;
                                players[index].Backpack[newSlot].Stamina = players[index].Bank[slot].Stamina;
                                players[index].Backpack[newSlot].Value = value;
                                players[index].Backpack[newSlot].Price = players[index].Bank[slot].Price;
                                players[index].Backpack[newSlot].Rarity = players[index].Bank[slot].Rarity;
                                players[index].Backpack[newSlot].CoolDown = players[index].Bank[slot].CoolDown;
                                players[index].Backpack[newSlot].AddMaxHealth = players[index].Bank[slot].AddMaxHealth;
                                players[index].Backpack[newSlot].AddMaxMana = players[index].Bank[slot].AddMaxMana;
                                players[index].Backpack[newSlot].BonusXP = players[index].Bank[slot].BonusXP;
                                players[index].Backpack[newSlot].SpellNum = players[index].Bank[slot].SpellNum;
                                players[index].Backpack[newSlot].Stackable = players[index].Bank[slot].Stackable;
                                players[index].Backpack[newSlot].MaxStack = players[index].Bank[slot].MaxStack;

                                players[index].Bank[slot].Value -= value;
                                HandleData.SendPlayerBank(index);
                                HandleData.SendPlayerInv(index);
                            }
                            else
                            {
                                players[index].Backpack[newSlot] = players[index].Bank[slot];
                                players[index].Bank[slot] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, false, 1);
                                HandleData.SendPlayerBank(index);
                                HandleData.SendPlayerInv(index);
                            }
                        }
                        else
                        {
                            HandleData.SendServerMessageTo(Connection, "You inventory is full!");
                        }
                    }
                }
                else
                {
                    int newSlot = FindOpenInvSlot(players[index].Backpack);

                    if (newSlot < MAX_INV_SLOTS)
                    {
                        players[index].Backpack[newSlot] = players[index].Bank[slot];
                        players[index].Bank[slot] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, false, 1);
                        HandleData.SendPlayerBank(index);
                        HandleData.SendPlayerInv(index);
                    }
                    else
                    {
                        HandleData.SendServerMessageTo(Connection, "You inventory is full!");
                    }
                }
            }
        }

        public void UseSpell(int index, int slot, int hotbarslot)
        {
            if (players[index].SpellBook[slot].SpellNumber > -1)
            {
                Spell spell = spells[players[index].SpellBook[slot].SpellNumber];

                switch (spell.SpellType)
                {
                    case (int)SpellType.Dash:
                        if (players[index].Mana >= spell.ManaCost) { HandleData.SendServerMessageTo(players[index].Connection, "You dont have enough mana to cast this spell!"); return; }

                        int newX = 0;
                        int newY = 0;
                        switch (players[index].AimDirection)
                        {
                            case (int)Directions.Down:
                                newY = players[index].Y + spell.Range;
                                players[index].Mana -= spell.ManaCost;
                                break;

                            case (int)Directions.Up:
                                newY = players[index].Y - spell.Range;
                                players[index].Mana -= spell.ManaCost;
                                break;

                            case (int)Directions.Left:
                                newX = players[index].X - spell.Range;
                                players[index].Mana -= spell.ManaCost;
                                break;

                            case (int)Directions.Right:
                                newX = players[index].X + spell.Range;
                                players[index].Mana -= spell.ManaCost;
                                break;
                        }

                        HandleData.SendServerMessageTo(players[index].Connection, "CHARGE!!!");
                        HandleData.SendUpdatePlayerStats(index);

                        int map = players[index].Map;
                        for (int i = 0; i < MAX_PLAYERS; i++)
                        {
                            if (players[i].Connection != null && players[i].Map == map)
                            {
                                HandleData.SendUpdateMovementData(players[i].Connection, index, newX, newY, players[index].Direction, players[index].AimDirection, players[index].Step);
                            }
                        }
                        break;
                }
            }
        }

        public void UseItem(int index, int slot, int hotbarslot)
        {
            //really just seems like this is hotbar stuff
            if (players[index].Backpack[slot].Name != "None")
            {
                bool removeItem = false;

                switch (players[index].Backpack[slot].Type)
                {
                    case (int)ItemType.Food:
                    case (int)ItemType.Drink:
                    case (int)ItemType.Potion:

                        //Check to see if the potion restores HP
                        if (players[index].Backpack[slot].HealthRestore > 0)
                        {
                            //check if the item is on cooldown
                            if (TickCount - players[index].Backpack[slot].cooldownTick < (players[index].Backpack[slot].CoolDown * A_MILLISECOND)) { return; }

                            //If we already have max hp let not let the player waste it
                            if (players[index].Health == players[index].MaxHealth)
                            {
                                HandleData.SendServerMessageTo(players[index].Connection, "You already have max health!");
                                return;
                            }

                            //if we have less that max hp then we will use the pot and heal
                            if (players[index].Health < players[index].MaxHealth)
                            {
                                players[index].Health += players[index].Backpack[slot].HealthRestore;
                                if (players[index].Health > players[index].MaxHealth) { players[index].Health = players[index].MaxHealth; } //just incase we over heal
                                players[index].Backpack[slot].cooldownTick = TickCount;
                                players[index].Backpack[slot].OnCoolDown = true;
                                HandleData.SendItemCoolDownUpdate(players[index].Connection, index, slot, true);
                            }
                        }

                        //Check to see if the potion restores MP
                        if (players[index].Backpack[slot].ManaRestore > 0)
                        {
                            //check if the item is on cooldown
                            if (TickCount - players[index].Backpack[slot].cooldownTick < (players[index].Backpack[slot].CoolDown * A_MILLISECOND)) { return; }

                            //If we already have max mp let not let the player waste it
                            if (players[index].Mana == players[index].MaxMana)
                            {
                                HandleData.SendServerMessageTo(players[index].Connection, "You already have max mana!");
                                return;
                            }

                            //if we have less that max mp then we will use the pot and gen
                            if (players[index].Mana < players[index].MaxMana)
                            {
                                players[index].Mana += players[index].Backpack[slot].ManaRestore;
                                if (players[index].Mana > players[index].MaxMana) { players[index].Mana = players[index].MaxMana; } //just incase we over gen
                                players[index].Backpack[slot].cooldownTick = TickCount;
                                players[index].Backpack[slot].OnCoolDown = true;
                                HandleData.SendItemCoolDownUpdate(players[index].Connection, index, slot, true);
                            }
                        }

                        //Check to see if it stacks
                        if (players[index].Backpack[slot].Stackable)
                        {
                            //is it greater than 1 and if so then we...
                            if (players[index].Backpack[slot].Value > 1)
                            {
                                players[index].Backpack[slot].Value -= 1;   //...remove it
                            }
                            else
                            {
                                removeItem = true;  //if there isnt any remaining we will remove it for gud
                            }
                        }
                        break;

                    default:
                        HandleData.SendServerMessageTo(players[index].Connection, "Item cannot be used on hotbar!");
                        players[index].hotBar[hotbarslot].InvNumber = -1;
                        HandleData.SendPlayerHotBar(index);
                        return;
                }

                //removes any used up items
                if (removeItem)
                {
                    players[index].Backpack[slot] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, false, 1);
                    players[index].hotBar[hotbarslot].InvNumber = -1;
                }

                //update players with new information
                HandleData.SendPlayerHotBar(index);
                HandleData.SendPlayerInv(index);
                HandleData.SendUpdatePlayerStats(index);
            }
        }

        //this void is where we also use consumables and such from the inventory but also equip gear
        public void EquipItem(int index, int slot)
        {
            bool removeItem = false;

            if (players[index].Backpack[slot].Name != "None")
            {              
                switch (players[index].Backpack[slot].Type)
                {                    
                    case (int)ItemType.MainHand:
                        if (players[index].MainHand.Name == "None")
                        {
                            players[index].MainHand = players[index].Backpack[slot];
                        }
                        else
                        {
                            int newSlot = FindOpenInvSlot(Backpack);

                            if (newSlot < MAX_INV_SLOTS)
                            {
                                players[index].Backpack[newSlot] = players[index].MainHand;
                                players[index].MainHand = players[index].Backpack[slot];
                            }
                        }
                        removeItem = true;
                        break;

                    case (int)ItemType.OffHand:
                        if (players[index].OffHand.Name == "None")
                        {
                            players[index].OffHand = players[index].Backpack[slot];
                        }
                        else
                        {
                            int newSlot = FindOpenInvSlot(Backpack);

                            if (newSlot < MAX_INV_SLOTS)
                            {
                                players[index].Backpack[newSlot] = players[index].OffHand;
                                players[index].OffHand = players[index].Backpack[slot];
                            }
                        }
                        removeItem = true;
                        break;

                    case (int)ItemType.Shirt:
                        if (players[index].Chest.Name == "None")
                        {
                            players[index].Chest = players[index].Backpack[slot];
                        }
                        else
                        {
                            int newSlot = FindOpenInvSlot(Backpack);

                            if (newSlot < MAX_INV_SLOTS)
                            {
                                players[index].Backpack[newSlot] = players[index].Chest;
                                players[index].Chest = players[index].Backpack[slot];
                            }
                        }
                        removeItem = true;
                        break;

                    case (int)ItemType.Pants:
                        if (players[index].Legs.Name == "None")
                        {
                            players[index].Legs = players[index].Backpack[slot];
                        }
                        else
                        {
                            int newSlot = FindOpenInvSlot(Backpack);

                            if (newSlot < MAX_INV_SLOTS)
                            {
                                players[index].Backpack[newSlot] = players[index].Legs;
                                players[index].Legs = players[index].Backpack[slot];
                            }
                        }
                        removeItem = true;
                        break;

                    case (int)ItemType.Shoes:
                        if (players[index].Feet.Name == "None")
                        {
                            players[index].Feet = players[index].Backpack[slot];
                        }
                        else
                        {
                            int newSlot = FindOpenInvSlot(Backpack);

                            if (newSlot < MAX_INV_SLOTS)
                            {
                                players[index].Backpack[newSlot] = players[index].Feet;
                                players[index].Feet = players[index].Backpack[slot];
                            }
                        }
                        removeItem = true;
                        break;

                    //take care of on use stuff too
                    case (int)ItemType.Currency:
                        if (players[index].Backpack[slot].Value > 0)
                        {
                            players[index].Wallet += players[index].Backpack[slot].Value;
                            removeItem = true;
                        }
                        break;

                    case (int)ItemType.Book:
                        int spellNum = players[index].Backpack[slot].SpellNum - 1;
                        int spellSlot = FindOpenSpellSlot();

                        if (spellSlot < MAX_PLAYER_SPELLBOOK)
                        {
                            if (!CheckIfPlayerHasSpell(spellNum))
                            {
                                players[index].SpellBook[spellSlot].SpellNumber = spellNum;
                                HandleData.SendServerMessageTo(players[index].Connection, "You learned " + spells[spellNum].Name + "!");
                                removeItem = true;
                            }
                            else
                            {
                                HandleData.SendServerMessageTo(players[index].Connection, "You already know this spell!");
                                return;
                            }
                        }
                        else
                        {
                            HandleData.SendServerMessageTo(players[index].Connection, "You cannot learn anymore spells!");
                            return;
                        }

                        break;

                    case (int)ItemType.Food:
                    case (int)ItemType.Drink:
                    case (int)ItemType.Potion:

                        //Check to see if the potion restores HP
                        if (players[index].Backpack[slot].HealthRestore > 0)
                        {
                            //check if the item is on cooldown
                            if (TickCount - players[index].Backpack[slot].cooldownTick < (players[index].Backpack[slot].CoolDown * 1000)) { return; }

                            //If we already have max hp let not let the player waste it
                            if (players[index].Health == players[index].MaxHealth)
                            {
                                HandleData.SendServerMessageTo(players[index].Connection, "You already have max health!");
                                return;
                            }

                            //if we have less that max hp then we will use the pot and heal
                            if (players[index].Health < players[index].MaxHealth)
                            {
                                players[index].Health += players[index].Backpack[slot].HealthRestore;
                                if (players[index].Health > players[index].MaxHealth) { players[index].Health = players[index].MaxHealth; } //just incase we over heal
                                players[index].Backpack[slot].cooldownTick = TickCount;
                                players[index].Backpack[slot].OnCoolDown = true;
                                HandleData.SendItemCoolDownUpdate(players[index].Connection, index, slot, true);
                            }                                                        
                        }

                        //Check to see if the potion restores MP
                        if (players[index].Backpack[slot].ManaRestore > 0)
                        {
                            //check if the item is on cooldown
                            if (TickCount - players[index].Backpack[slot].cooldownTick < (players[index].Backpack[slot].CoolDown * 1000)) { return; }

                            //If we already have max mp let not let the player waste it
                            if (players[index].Mana == players[index].MaxMana)
                            {
                                HandleData.SendServerMessageTo(players[index].Connection, "You already have max mana!");
                                return;
                            }

                            //if we have less that max mp then we will use the pot and gen
                            if (players[index].Mana < players[index].MaxMana)
                            {
                                players[index].Mana += players[index].Backpack[slot].ManaRestore;
                                if (players[index].Mana > players[index].MaxMana) { players[index].Mana = players[index].MaxMana; } //just incase we over gen
                                players[index].Backpack[slot].cooldownTick = TickCount;
                                players[index].Backpack[slot].OnCoolDown = true;
                                HandleData.SendItemCoolDownUpdate(players[index].Connection, index, slot, true);
                            }
                        }

                        //Check to see if it stacks
                        if (players[index].Backpack[slot].Stackable)
                        {
                            //is it greater than 1 and if so then we...
                            if (players[index].Backpack[slot].Value > 1)
                            {
                                players[index].Backpack[slot].Value -= 1;   //...remove it
                            }
                            else
                            {
                                removeItem = true;  //if there isnt any remaining we will remove it for gud
                            }
                        }
                        break;

                }

                //removes any used up items
                if (removeItem)
                {
                    players[index].Backpack[slot] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, false, 1);
                }

                //lets update the player with results
                HandleData.SendWeaponsUpdate(index);
                HandleData.SendPlayerInv(index);
                HandleData.SendUpdatePlayerStats(index);
                HandleData.SendPlayerEquipment(index);
                HandleData.SendPlayerSpellBook(index);
            }
        }

        int FindOpenSpellSlot()
        {
            for (int i = 0; i < MAX_PLAYER_SPELLBOOK; i++)
            {
                if (SpellBook[i].SpellNumber == -1)
                {
                    return i;
                }
            }
            return MAX_PLAYER_SPELLBOOK;
        }

        bool CheckIfPlayerHasSpell(int spellNum)
        {
            for (int i = 0; i < MAX_PLAYER_SPELLBOOK; i++)
            {
                if (SpellBook[i].SpellNumber > -1)
                {
                    if (SpellBook[i].SpellNumber == spellNum)
                    {
                        return true;
                    }
                }
            }
            return false;
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
                        players[index].MainHand = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, false, 1);

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
                        players[index].OffHand = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, false, 1);
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
                        players[index].Chest = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, false, 1);
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
                        players[index].Legs = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, false, 1);
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
                        players[index].Feet = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, false, 1);
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

        public int FindSameInvItem(Item[] s_Backpack, Item s_Item)
        {
            for (int i = 0; i < MAX_INV_SLOTS; i++)
            {                
                if (s_Backpack[i].Name == s_Item.Name)
                {
                    return i;
                }
            }
            return MAX_INV_SLOTS;
        }

        int FindSameBankItem(Item[] s_Bank, Item s_Item)
        {
            for (int i = 0; i < MAX_BANK_SLOTS; i++)
            {
                if (s_Bank[i].Name == s_Item.Name)
                {
                    return i;
                }
            }
            return MAX_BANK_SLOTS;
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
        private byte[] ToByteArray(object source)
        {
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, source);
                return stream.ToArray();
            }
        }

        private static object ByteArrayToObject(byte[] arrBytes)
        {
            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = binForm.Deserialize(memStream);
                return obj;
            }
        }

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

                LoadPlayerIDFromDatabase(Name);

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
                    cmd.Parameters.Add(new SqlParameter("@manarestore", System.Data.DbType.Int32)).Value = MainHand.ManaRestore;
                    cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = MainHand.Strength;
                    cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = MainHand.Agility;
                    cmd.Parameters.Add(new SqlParameter("@intelligence", System.Data.DbType.Int32)).Value = MainHand.Intelligence;
                    cmd.Parameters.Add(new SqlParameter("@energy", System.Data.DbType.Int32)).Value = MainHand.Energy;
                    cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = MainHand.Stamina;
                    cmd.Parameters.Add(new SqlParameter("@value", System.Data.DbType.Int32)).Value = MainHand.Value;
                    cmd.Parameters.Add(new SqlParameter("@price", System.Data.DbType.Int32)).Value = MainHand.Price;
                    cmd.Parameters.Add(new SqlParameter("@rarity", System.Data.DbType.Int32)).Value = MainHand.Rarity;
                    cmd.Parameters.Add(new SqlParameter("@cooldown", System.Data.DbType.Int32)).Value = MainHand.CoolDown;
                    cmd.Parameters.Add(new SqlParameter("@addmaxhp", System.Data.DbType.Int32)).Value = MainHand.AddMaxHealth;
                    cmd.Parameters.Add(new SqlParameter("@addmaxmp", System.Data.DbType.Int32)).Value = MainHand.AddMaxMana;
                    cmd.Parameters.Add(new SqlParameter("@bonusxp", System.Data.DbType.Int32)).Value = MainHand.BonusXP;
                    cmd.Parameters.Add(new SqlParameter("@spellnum", System.Data.DbType.Int32)).Value = MainHand.SpellNum;
                    cmd.Parameters.Add(new SqlParameter("@stack", System.Data.DbType.Boolean)).Value = MainHand.Stackable;
                    cmd.Parameters.Add(new SqlParameter("@maxstack", System.Data.DbType.Int32)).Value = MainHand.MaxStack;
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
                    cmd.Parameters.Add(new SqlParameter("@manarestore", System.Data.DbType.Int32)).Value = OffHand.ManaRestore;
                    cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = OffHand.Strength;
                    cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = OffHand.Agility;
                    cmd.Parameters.Add(new SqlParameter("@intelligence", System.Data.DbType.Int32)).Value = OffHand.Intelligence;
                    cmd.Parameters.Add(new SqlParameter("@energy", System.Data.DbType.Int32)).Value = OffHand.Energy;
                    cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = OffHand.Stamina;
                    cmd.Parameters.Add(new SqlParameter("@value", System.Data.DbType.Int32)).Value = OffHand.Value;
                    cmd.Parameters.Add(new SqlParameter("@price", System.Data.DbType.Int32)).Value = OffHand.Price;
                    cmd.Parameters.Add(new SqlParameter("@rarity", System.Data.DbType.Int32)).Value = OffHand.Rarity;
                    cmd.Parameters.Add(new SqlParameter("@cooldown", System.Data.DbType.Int32)).Value = OffHand.CoolDown;
                    cmd.Parameters.Add(new SqlParameter("@addmaxhp", System.Data.DbType.Int32)).Value = OffHand.AddMaxHealth;
                    cmd.Parameters.Add(new SqlParameter("@addmaxmp", System.Data.DbType.Int32)).Value = OffHand.AddMaxMana;
                    cmd.Parameters.Add(new SqlParameter("@bonusxp", System.Data.DbType.Int32)).Value = OffHand.BonusXP;
                    cmd.Parameters.Add(new SqlParameter("@spellnum", System.Data.DbType.Int32)).Value = OffHand.SpellNum;
                    cmd.Parameters.Add(new SqlParameter("@stack", System.Data.DbType.Boolean)).Value = OffHand.Stackable;
                    cmd.Parameters.Add(new SqlParameter("@maxstack", System.Data.DbType.Int32)).Value = OffHand.MaxStack;
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
                    cmd.Parameters.Add(new SqlParameter("@manarestore", System.Data.DbType.Int32)).Value = Chest.ManaRestore;
                    cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = Chest.Strength;
                    cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = Chest.Agility;
                    cmd.Parameters.Add(new SqlParameter("@intelligence", System.Data.DbType.Int32)).Value = Chest.Intelligence;
                    cmd.Parameters.Add(new SqlParameter("@energy", System.Data.DbType.Int32)).Value = Chest.Energy;
                    cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = Chest.Stamina;
                    cmd.Parameters.Add(new SqlParameter("@value", System.Data.DbType.Int32)).Value = Chest.Value;
                    cmd.Parameters.Add(new SqlParameter("@price", System.Data.DbType.Int32)).Value = Chest.Price;
                    cmd.Parameters.Add(new SqlParameter("@rarity", System.Data.DbType.Int32)).Value = Chest.Rarity;
                    cmd.Parameters.Add(new SqlParameter("@cooldown", System.Data.DbType.Int32)).Value = Chest.CoolDown;
                    cmd.Parameters.Add(new SqlParameter("@addmaxhp", System.Data.DbType.Int32)).Value = Chest.AddMaxHealth;
                    cmd.Parameters.Add(new SqlParameter("@addmaxmp", System.Data.DbType.Int32)).Value = Chest.AddMaxMana;
                    cmd.Parameters.Add(new SqlParameter("@bonusxp", System.Data.DbType.Int32)).Value = Chest.BonusXP;
                    cmd.Parameters.Add(new SqlParameter("@spellnum", System.Data.DbType.Int32)).Value = Chest.SpellNum;
                    cmd.Parameters.Add(new SqlParameter("@stack", System.Data.DbType.Boolean)).Value = Chest.Stackable;
                    cmd.Parameters.Add(new SqlParameter("@maxstack", System.Data.DbType.Int32)).Value = Chest.MaxStack;
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
                    cmd.Parameters.Add(new SqlParameter("@manarestore", System.Data.DbType.Int32)).Value = Legs.ManaRestore;
                    cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = Legs.Strength;
                    cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = Legs.Agility;
                    cmd.Parameters.Add(new SqlParameter("@intelligence", System.Data.DbType.Int32)).Value = Legs.Intelligence;
                    cmd.Parameters.Add(new SqlParameter("@energy", System.Data.DbType.Int32)).Value = Legs.Energy;
                    cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = Legs.Stamina;
                    cmd.Parameters.Add(new SqlParameter("@value", System.Data.DbType.Int32)).Value = Legs.Value;
                    cmd.Parameters.Add(new SqlParameter("@price", System.Data.DbType.Int32)).Value = Legs.Price;
                    cmd.Parameters.Add(new SqlParameter("@rarity", System.Data.DbType.Int32)).Value = Legs.Rarity;
                    cmd.Parameters.Add(new SqlParameter("@cooldown", System.Data.DbType.Int32)).Value = Legs.CoolDown;
                    cmd.Parameters.Add(new SqlParameter("@addmaxhp", System.Data.DbType.Int32)).Value = Legs.AddMaxHealth;
                    cmd.Parameters.Add(new SqlParameter("@addmaxmp", System.Data.DbType.Int32)).Value = Legs.AddMaxMana;
                    cmd.Parameters.Add(new SqlParameter("@bonusxp", System.Data.DbType.Int32)).Value = Legs.BonusXP;
                    cmd.Parameters.Add(new SqlParameter("@spellnum", System.Data.DbType.Int32)).Value = Legs.SpellNum;
                    cmd.Parameters.Add(new SqlParameter("@stack", System.Data.DbType.Boolean)).Value = Legs.Stackable;
                    cmd.Parameters.Add(new SqlParameter("@maxstack", System.Data.DbType.Int32)).Value = Legs.MaxStack;
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
                    cmd.Parameters.Add(new SqlParameter("@manarestore", System.Data.DbType.Int32)).Value = Feet.ManaRestore;
                    cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = Feet.Strength;
                    cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = Feet.Agility;
                    cmd.Parameters.Add(new SqlParameter("@intelligence", System.Data.DbType.Int32)).Value = Feet.Intelligence;
                    cmd.Parameters.Add(new SqlParameter("@energy", System.Data.DbType.Int32)).Value = Feet.Energy;
                    cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = Feet.Stamina;
                    cmd.Parameters.Add(new SqlParameter("@value", System.Data.DbType.Int32)).Value = Feet.Value;
                    cmd.Parameters.Add(new SqlParameter("@price", System.Data.DbType.Int32)).Value = Feet.Price;
                    cmd.Parameters.Add(new SqlParameter("@rarity", System.Data.DbType.Int32)).Value = Feet.Rarity;
                    cmd.Parameters.Add(new SqlParameter("@cooldown", System.Data.DbType.Int32)).Value = Feet.CoolDown;
                    cmd.Parameters.Add(new SqlParameter("@addmaxhp", System.Data.DbType.Int32)).Value = Feet.AddMaxHealth;
                    cmd.Parameters.Add(new SqlParameter("@addmaxmp", System.Data.DbType.Int32)).Value = Feet.AddMaxMana;
                    cmd.Parameters.Add(new SqlParameter("@bonusxp", System.Data.DbType.Int32)).Value = Feet.BonusXP;
                    cmd.Parameters.Add(new SqlParameter("@spellnum", System.Data.DbType.Int32)).Value = Feet.SpellNum;
                    cmd.Parameters.Add(new SqlParameter("@stack", System.Data.DbType.Boolean)).Value = Feet.Stackable;
                    cmd.Parameters.Add(new SqlParameter("@maxstack", System.Data.DbType.Int32)).Value = Feet.MaxStack;
                    cmd.ExecuteNonQuery();
                }

                script = ReadAllText("SQL Data Scripts/Insert_Hotbar.sql");
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@playerid", System.Data.SqlDbType.Int)).Value = Id;    //set the players id, we want to set this before the LooP ;)
                    for (int i = 0; i < MAX_PLAYER_HOTBAR; i++)
                    {
                        {
                            //create each hotkey for the query
                            byte[] hotKey = ToByteArray(hotBar[i].HotKey);
                            cmd.Parameters.Add(new SqlParameter("@hotkey_" + (i + 1), System.Data.SqlDbType.VarBinary)).Value = hotKey;
                            cmd.Parameters.Add(new SqlParameter("@spellnum_" + (i + 1), System.Data.SqlDbType.Int)).Value = hotBar[i].SpellNumber;
                            cmd.Parameters.Add(new SqlParameter("@invnum_" + (i + 1), System.Data.SqlDbType.Int)).Value = hotBar[i].InvNumber;                            
                        }
                    }
                    //execute after its made
                    cmd.ExecuteNonQuery();
                }

                script = ReadAllText("SQL Data Scripts/Insert_Player_Spell.sql");
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@playerid", System.Data.SqlDbType.Int)).Value = Id;
                    for (int i = 0; i < MAX_PLAYER_SPELLBOOK; i++)
                    {
                        {                        
                            cmd.Parameters.Add(new SqlParameter("@spellnumber_" + (i + 1), System.Data.SqlDbType.Int)).Value = SpellBook[i].SpellNumber;                                                       
                        }
                    }
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
                    cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = Id;
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
                    cmd.Parameters.Add(new SqlParameter("@manarestore", System.Data.DbType.Int32)).Value = MainHand.ManaRestore;
                    cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = MainHand.Strength;
                    cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = MainHand.Agility;
                    cmd.Parameters.Add(new SqlParameter("@intelligence", System.Data.DbType.Int32)).Value = MainHand.Intelligence;
                    cmd.Parameters.Add(new SqlParameter("@energy", System.Data.DbType.Int32)).Value = MainHand.Energy;
                    cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = MainHand.Stamina;
                    cmd.Parameters.Add(new SqlParameter("@value", System.Data.DbType.Int32)).Value = MainHand.Value;
                    cmd.Parameters.Add(new SqlParameter("@price", System.Data.DbType.Int32)).Value = MainHand.Price;
                    cmd.Parameters.Add(new SqlParameter("@rarity", System.Data.DbType.Int32)).Value = MainHand.Rarity;
                    cmd.Parameters.Add(new SqlParameter("@cooldown", System.Data.DbType.Int32)).Value = MainHand.CoolDown;
                    cmd.Parameters.Add(new SqlParameter("@addmaxhp", System.Data.DbType.Int32)).Value = MainHand.AddMaxHealth;
                    cmd.Parameters.Add(new SqlParameter("@addmaxmp", System.Data.DbType.Int32)).Value = MainHand.AddMaxMana;
                    cmd.Parameters.Add(new SqlParameter("@bonusxp", System.Data.DbType.Int32)).Value = MainHand.BonusXP;
                    cmd.Parameters.Add(new SqlParameter("@spellnum", System.Data.DbType.Int32)).Value = MainHand.SpellNum;
                    cmd.Parameters.Add(new SqlParameter("@stack", System.Data.DbType.Boolean)).Value = MainHand.Stackable;
                    cmd.Parameters.Add(new SqlParameter("@maxstack", System.Data.DbType.Int32)).Value = MainHand.MaxStack;
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
                    cmd.Parameters.Add(new SqlParameter("@manarestore", System.Data.DbType.Int32)).Value = OffHand.ManaRestore;
                    cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = OffHand.Strength;
                    cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = OffHand.Agility;
                    cmd.Parameters.Add(new SqlParameter("@intelligence", System.Data.DbType.Int32)).Value = OffHand.Intelligence;
                    cmd.Parameters.Add(new SqlParameter("@energy", System.Data.DbType.Int32)).Value = OffHand.Energy;
                    cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = OffHand.Stamina;
                    cmd.Parameters.Add(new SqlParameter("@value", System.Data.DbType.Int32)).Value = OffHand.Value;
                    cmd.Parameters.Add(new SqlParameter("@price", System.Data.DbType.Int32)).Value = OffHand.Price;
                    cmd.Parameters.Add(new SqlParameter("@rarity", System.Data.DbType.Int32)).Value = OffHand.Rarity;
                    cmd.Parameters.Add(new SqlParameter("@cooldown", System.Data.DbType.Int32)).Value = OffHand.CoolDown;
                    cmd.Parameters.Add(new SqlParameter("@addmaxhp", System.Data.DbType.Int32)).Value = OffHand.AddMaxHealth;
                    cmd.Parameters.Add(new SqlParameter("@addmaxmp", System.Data.DbType.Int32)).Value = OffHand.AddMaxMana;
                    cmd.Parameters.Add(new SqlParameter("@bonusxp", System.Data.DbType.Int32)).Value = OffHand.BonusXP;
                    cmd.Parameters.Add(new SqlParameter("@spellnum", System.Data.DbType.Int32)).Value = OffHand.SpellNum;
                    cmd.Parameters.Add(new SqlParameter("@stack", System.Data.DbType.Boolean)).Value = OffHand.Stackable;
                    cmd.Parameters.Add(new SqlParameter("@maxstack", System.Data.DbType.Int32)).Value = OffHand.MaxStack;
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
                    cmd.Parameters.Add(new SqlParameter("@manarestore", System.Data.DbType.Int32)).Value = Chest.ManaRestore;
                    cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = Chest.Strength;
                    cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = Chest.Agility;
                    cmd.Parameters.Add(new SqlParameter("@intelligence", System.Data.DbType.Int32)).Value = Chest.Intelligence;
                    cmd.Parameters.Add(new SqlParameter("@energy", System.Data.DbType.Int32)).Value = Chest.Energy;
                    cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = Chest.Stamina;
                    cmd.Parameters.Add(new SqlParameter("@value", System.Data.DbType.Int32)).Value = Chest.Value;
                    cmd.Parameters.Add(new SqlParameter("@price", System.Data.DbType.Int32)).Value = Chest.Price;
                    cmd.Parameters.Add(new SqlParameter("@rarity", System.Data.DbType.Int32)).Value = Chest.Rarity;
                    cmd.Parameters.Add(new SqlParameter("@cooldown", System.Data.DbType.Int32)).Value = Chest.CoolDown;
                    cmd.Parameters.Add(new SqlParameter("@addmaxhp", System.Data.DbType.Int32)).Value = Chest.AddMaxHealth;
                    cmd.Parameters.Add(new SqlParameter("@addmaxmp", System.Data.DbType.Int32)).Value = Chest.AddMaxMana;
                    cmd.Parameters.Add(new SqlParameter("@bonusxp", System.Data.DbType.Int32)).Value = Chest.BonusXP;
                    cmd.Parameters.Add(new SqlParameter("@spellnum", System.Data.DbType.Int32)).Value = Chest.SpellNum;
                    cmd.Parameters.Add(new SqlParameter("@stack", System.Data.DbType.Boolean)).Value = Chest.Stackable;
                    cmd.Parameters.Add(new SqlParameter("@maxstack", System.Data.DbType.Int32)).Value = Chest.MaxStack;
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
                    cmd.Parameters.Add(new SqlParameter("@manarestore", System.Data.DbType.Int32)).Value = Legs.ManaRestore;
                    cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = Legs.Strength;
                    cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = Legs.Agility;
                    cmd.Parameters.Add(new SqlParameter("@intelligence", System.Data.DbType.Int32)).Value = Legs.Intelligence;
                    cmd.Parameters.Add(new SqlParameter("@energy", System.Data.DbType.Int32)).Value = Legs.Energy;
                    cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = Legs.Stamina;
                    cmd.Parameters.Add(new SqlParameter("@value", System.Data.DbType.Int32)).Value = Legs.Value;
                    cmd.Parameters.Add(new SqlParameter("@price", System.Data.DbType.Int32)).Value = Legs.Price;
                    cmd.Parameters.Add(new SqlParameter("@rarity", System.Data.DbType.Int32)).Value = Legs.Rarity;
                    cmd.Parameters.Add(new SqlParameter("@cooldown", System.Data.DbType.Int32)).Value = Legs.CoolDown;
                    cmd.Parameters.Add(new SqlParameter("@addmaxhp", System.Data.DbType.Int32)).Value = Legs.AddMaxHealth;
                    cmd.Parameters.Add(new SqlParameter("@addmaxmp", System.Data.DbType.Int32)).Value = Legs.AddMaxMana;
                    cmd.Parameters.Add(new SqlParameter("@bonusxp", System.Data.DbType.Int32)).Value = Legs.BonusXP;
                    cmd.Parameters.Add(new SqlParameter("@spellnum", System.Data.DbType.Int32)).Value = Legs.SpellNum;
                    cmd.Parameters.Add(new SqlParameter("@stack", System.Data.DbType.Boolean)).Value = Legs.Stackable;
                    cmd.Parameters.Add(new SqlParameter("@maxstack", System.Data.DbType.Int32)).Value = Legs.MaxStack;
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
                    cmd.Parameters.Add(new SqlParameter("@manarestore", System.Data.DbType.Int32)).Value = Feet.ManaRestore;
                    cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = Feet.Strength;
                    cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = Feet.Agility;
                    cmd.Parameters.Add(new SqlParameter("@intelligence", System.Data.DbType.Int32)).Value = Feet.Intelligence;
                    cmd.Parameters.Add(new SqlParameter("@energy", System.Data.DbType.Int32)).Value = Feet.Energy;
                    cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = Feet.Stamina;
                    cmd.Parameters.Add(new SqlParameter("@value", System.Data.DbType.Int32)).Value = Feet.Value;
                    cmd.Parameters.Add(new SqlParameter("@price", System.Data.DbType.Int32)).Value = Feet.Price;
                    cmd.Parameters.Add(new SqlParameter("@rarity", System.Data.DbType.Int32)).Value = Feet.Rarity;
                    cmd.Parameters.Add(new SqlParameter("@cooldown", System.Data.DbType.Int32)).Value = Feet.CoolDown;
                    cmd.Parameters.Add(new SqlParameter("@addmaxhp", System.Data.DbType.Int32)).Value = Feet.AddMaxHealth;
                    cmd.Parameters.Add(new SqlParameter("@addmaxmp", System.Data.DbType.Int32)).Value = Feet.AddMaxMana;
                    cmd.Parameters.Add(new SqlParameter("@bonusxp", System.Data.DbType.Int32)).Value = Feet.BonusXP;
                    cmd.Parameters.Add(new SqlParameter("@spellnum", System.Data.DbType.Int32)).Value = Feet.SpellNum;
                    cmd.Parameters.Add(new SqlParameter("@stack", System.Data.DbType.Boolean)).Value = Feet.Stackable;
                    cmd.Parameters.Add(new SqlParameter("@maxstack", System.Data.DbType.Int32)).Value = Feet.MaxStack;
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
                            cmd.Parameters.Add(new SqlParameter("@manarestore", System.Data.DbType.Int32)).Value = Backpack[i].ManaRestore;
                            cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = Backpack[i].Strength;
                            cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = Backpack[i].Agility;
                            cmd.Parameters.Add(new SqlParameter("@intelligence", System.Data.DbType.Int32)).Value = Backpack[i].Intelligence;
                            cmd.Parameters.Add(new SqlParameter("@energy", System.Data.DbType.Int32)).Value = Backpack[i].Energy;
                            cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = Backpack[i].Stamina;
                            cmd.Parameters.Add(new SqlParameter("@value", System.Data.DbType.Int32)).Value = Backpack[i].Value;
                            cmd.Parameters.Add(new SqlParameter("@price", System.Data.DbType.Int32)).Value = Backpack[i].Price;
                            cmd.Parameters.Add(new SqlParameter("@rarity", System.Data.DbType.Int32)).Value = Backpack[i].Rarity;
                            cmd.Parameters.Add(new SqlParameter("@cooldown", System.Data.DbType.Int32)).Value = Backpack[i].CoolDown;
                            cmd.Parameters.Add(new SqlParameter("@addmaxhp", System.Data.DbType.Int32)).Value = Backpack[i].AddMaxHealth;
                            cmd.Parameters.Add(new SqlParameter("@addmaxmp", System.Data.DbType.Int32)).Value = Backpack[i].AddMaxMana;
                            cmd.Parameters.Add(new SqlParameter("@bonusxp", System.Data.DbType.Int32)).Value = Backpack[i].BonusXP;
                            cmd.Parameters.Add(new SqlParameter("@spellnum", System.Data.DbType.Int32)).Value = Backpack[i].SpellNum;
                            cmd.Parameters.Add(new SqlParameter("@stack", System.Data.DbType.Boolean)).Value = Backpack[i].Stackable;
                            cmd.Parameters.Add(new SqlParameter("@maxstack", System.Data.DbType.Int32)).Value = Backpack[i].MaxStack;
                            cmd.ExecuteNonQuery();
                        }                        
                    }
                    n = n + 1;
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
                            cmd.Parameters.Add(new SqlParameter("@manarestore", System.Data.DbType.Int32)).Value = Bank[i].ManaRestore;
                            cmd.Parameters.Add(new SqlParameter("@strength", System.Data.DbType.Int32)).Value = Bank[i].Strength;
                            cmd.Parameters.Add(new SqlParameter("@agility", System.Data.DbType.Int32)).Value = Bank[i].Agility;
                            cmd.Parameters.Add(new SqlParameter("@intelligence", System.Data.DbType.Int32)).Value = Bank[i].Intelligence;
                            cmd.Parameters.Add(new SqlParameter("@energy", System.Data.DbType.Int32)).Value = Bank[i].Energy;
                            cmd.Parameters.Add(new SqlParameter("@stamina", System.Data.DbType.Int32)).Value = Bank[i].Stamina;
                            cmd.Parameters.Add(new SqlParameter("@value", System.Data.DbType.Int32)).Value = Bank[i].Value;
                            cmd.Parameters.Add(new SqlParameter("@price", System.Data.DbType.Int32)).Value = Bank[i].Price;
                            cmd.Parameters.Add(new SqlParameter("@rarity", System.Data.DbType.Int32)).Value = Bank[i].Rarity;
                            cmd.Parameters.Add(new SqlParameter("@cooldown", System.Data.DbType.Int32)).Value = Bank[i].CoolDown;
                            cmd.Parameters.Add(new SqlParameter("@addmaxhp", System.Data.DbType.Int32)).Value = Bank[i].AddMaxHealth;
                            cmd.Parameters.Add(new SqlParameter("@addmaxmp", System.Data.DbType.Int32)).Value = Bank[i].AddMaxMana;
                            cmd.Parameters.Add(new SqlParameter("@bonusxp", System.Data.DbType.Int32)).Value = Bank[i].BonusXP;
                            cmd.Parameters.Add(new SqlParameter("@spellnum", System.Data.DbType.Int32)).Value = Bank[i].SpellNum;
                            cmd.Parameters.Add(new SqlParameter("@stack", System.Data.DbType.Boolean)).Value = Bank[i].Stackable;
                            cmd.Parameters.Add(new SqlParameter("@maxstack", System.Data.DbType.Int32)).Value = Bank[i].MaxStack;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    m = m + 1;
                }

                
                script = ReadAllText("SQL Data Scripts/Save_Hotbar.sql");
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@playerid", System.Data.SqlDbType.Int)).Value = Id;    //set the id before the LOOP
                    for (int i = 0; i < MAX_PLAYER_HOTBAR; i++)
                    {
                        byte[] hotKey = ToByteArray(hotBar[i].HotKey);                                                
                        cmd.Parameters.Add(new SqlParameter("@hotkey_" + (i + 1), System.Data.SqlDbType.VarBinary)).Value = hotKey;
                        cmd.Parameters.Add(new SqlParameter("@spellnum_" + (i + 1), System.Data.SqlDbType.Int)).Value = hotBar[i].SpellNumber;
                        cmd.Parameters.Add(new SqlParameter("@invnum_" + (i + 1), System.Data.SqlDbType.Int)).Value = hotBar[i].InvNumber;                        
                    }
                    cmd.ExecuteNonQuery();
                }

                script = ReadAllText("SQL Data Scripts/Save_Player_Spell.sql");
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@playerid", System.Data.SqlDbType.Int)).Value = Id;    //set the id before the LOOP
                    for (int i = 0; i < MAX_PLAYER_SPELLBOOK; i++)
                    {
                        cmd.Parameters.Add(new SqlParameter("@spellnumber_" + (i + 1), System.Data.SqlDbType.Int)).Value = SpellBook[i].SpellNumber;
                    }
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void LoadPlayerFromDatabase()
        {
            //Setup the connection string
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";

            //Open a connection and start loading data BBY
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                int result;
                int i;
                int n;
                int slot;
                int bankslot;

                //Load the basic information on the player
                string script = ReadAllText("SQL Data Scripts/Load_Player.sql");
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

                //Load Stats
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

                //Load Quest List
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

                //Load Main Weapon
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
                            MainHand.ManaRestore = ToInt32(reader[i]); i += 1;
                            MainHand.Strength = ToInt32(reader[i]); i += 1;
                            MainHand.Agility = ToInt32(reader[i]); i += 1;
                            MainHand.Intelligence = ToInt32(reader[i]); i += 1;
                            MainHand.Energy = ToInt32(reader[i]); i += 1;
                            MainHand.Stamina = ToInt32(reader[i]); i += 1;
                            MainHand.Value = ToInt32(reader[i]); i += 1;
                            MainHand.Price = ToInt32(reader[i]); i += 1;
                            MainHand.Rarity = ToInt32(reader[i]); i += 1;
                            MainHand.CoolDown = ToInt32(reader[i]); i += 1;
                            MainHand.AddMaxHealth = ToInt32(reader[i]); i += 1;
                            MainHand.AddMaxMana = ToInt32(reader[i]); i += 1;
                            MainHand.BonusXP = ToInt32(reader[i]); i += 1;
                            MainHand.SpellNum = ToInt32(reader[i]); i += 1;
                            MainHand.Stackable = ToBoolean(reader[i]); i += 1;
                            MainHand.MaxStack = ToInt32(reader[i]);
                        }
                    }
                }

                //Load Secondary Weapon
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
                            OffHand.ManaRestore = ToInt32(reader[i]); i += 1;
                            OffHand.Strength = ToInt32(reader[i]); i += 1;
                            OffHand.Agility = ToInt32(reader[i]); i += 1;
                            OffHand.Intelligence = ToInt32(reader[i]); i += 1;
                            OffHand.Energy = ToInt32(reader[i]); i += 1;
                            OffHand.Stamina = ToInt32(reader[i]); i += 1;
                            OffHand.Value = ToInt32(reader[i]); i += 1;
                            OffHand.Price = ToInt32(reader[i]); i += 1;
                            OffHand.Rarity = ToInt32(reader[i]); i += 1;
                            OffHand.CoolDown = ToInt32(reader[i]); i += 1;
                            OffHand.AddMaxHealth = ToInt32(reader[i]); i += 1;
                            OffHand.AddMaxMana = ToInt32(reader[i]); i += 1;
                            OffHand.BonusXP = ToInt32(reader[i]); i += 1;
                            OffHand.SpellNum = ToInt32(reader[i]); i += 1;
                            OffHand.Stackable = ToBoolean(reader[i]); i += 1;
                            OffHand.MaxStack = ToInt32(reader[i]);
                        }
                    }
                }

                //Load Chest Equipment
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
                            Chest.ManaRestore = ToInt32(reader[i]); i += 1;
                            Chest.Strength = ToInt32(reader[i]); i += 1;
                            Chest.Agility = ToInt32(reader[i]); i += 1;
                            Chest.Intelligence = ToInt32(reader[i]); i += 1;
                            Chest.Energy = ToInt32(reader[i]); i += 1;
                            Chest.Stamina = ToInt32(reader[i]); i += 1;
                            Chest.Value = ToInt32(reader[i]); i += 1;
                            Chest.Price = ToInt32(reader[i]); i += 1;
                            Chest.Rarity = ToInt32(reader[i]); i += 1;
                            Chest.CoolDown = ToInt32(reader[i]); i += 1;
                            Chest.AddMaxHealth = ToInt32(reader[i]); i += 1;
                            Chest.AddMaxMana = ToInt32(reader[i]); i += 1;
                            Chest.BonusXP = ToInt32(reader[i]); i += 1;
                            Chest.SpellNum = ToInt32(reader[i]); i += 1;
                            Chest.Stackable = ToBoolean(reader[i]); i += 1;
                            Chest.MaxStack = ToInt32(reader[i]);
                        }
                    }
                }

                //Load Legs Equipment
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
                            Legs.ManaRestore = ToInt32(reader[i]); i += 1;
                            Legs.Strength = ToInt32(reader[i]); i += 1;
                            Legs.Agility = ToInt32(reader[i]); i += 1;
                            Legs.Intelligence = ToInt32(reader[i]); i += 1;
                            Legs.Energy = ToInt32(reader[i]); i += 1;
                            Legs.Stamina = ToInt32(reader[i]); i += 1;
                            Legs.Value = ToInt32(reader[i]); i += 1;
                            Legs.Price = ToInt32(reader[i]); i += 1;
                            Legs.Rarity = ToInt32(reader[i]); i += 1;
                            Legs.CoolDown = ToInt32(reader[i]); i += 1;
                            Legs.AddMaxHealth = ToInt32(reader[i]); i += 1;
                            Legs.AddMaxMana = ToInt32(reader[i]); i += 1;
                            Legs.BonusXP = ToInt32(reader[i]); i += 1;
                            Legs.SpellNum = ToInt32(reader[i]); i += 1;
                            Legs.Stackable = ToBoolean(reader[i]); i += 1;
                            Legs.MaxStack = ToInt32(reader[i]);
                        }
                    }
                }
                
                //Load Feet Equipment
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
                            Feet.ManaRestore = ToInt32(reader[i]); i += 1;
                            Feet.Strength = ToInt32(reader[i]); i += 1;
                            Feet.Agility = ToInt32(reader[i]); i += 1;
                            Feet.Intelligence = ToInt32(reader[i]); i += 1;
                            Feet.Energy = ToInt32(reader[i]); i += 1;
                            Feet.Stamina = ToInt32(reader[i]); i += 1;
                            Feet.Value = ToInt32(reader[i]); i += 1;
                            Feet.Price = ToInt32(reader[i]); i += 1;
                            Feet.Rarity = ToInt32(reader[i]); i += 1;
                            Feet.CoolDown = ToInt32(reader[i]); i += 1;
                            Feet.AddMaxHealth = ToInt32(reader[i]); i += 1;
                            Feet.AddMaxMana = ToInt32(reader[i]); i += 1;
                            Feet.BonusXP = ToInt32(reader[i]); i += 1;
                            Feet.SpellNum = ToInt32(reader[i]); i += 1;
                            Feet.Stackable = ToBoolean(reader[i]); i += 1;
                            Feet.MaxStack = ToInt32(reader[i]);
                        }
                    }
                }

                //Check how many inventory slots they have an item in
                script = ReadAllText("SQL Data Scripts/Inventory_Count.sql");
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                    object queue = cmd.ExecuteScalar();
                    result = ToInt32(queue);
                }

                //Use the result from above to load their inventory
                if (result > 0)
                {
                    for (i = 0; i < result; i++)
                    {
                        script = ReadAllText("SQL Data Scripts/Load_Inventory.sql");
                        using (var cmd = new SqlCommand(script, sql))
                        {
                            cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                            //cmd.Parameters.Add(new SqlParameter("@slot", System.Data.DbType.Int32)).Value = i;

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    n = 0;
                                    slot = ToInt32(reader[2]);
                                    Backpack[slot].Id = ToInt32(reader[n]); n = 3;
                                    Backpack[slot].Name = reader[n].ToString(); n += 1;
                                    Backpack[slot].Sprite = ToInt32(reader[n]); n += 1;
                                    Backpack[slot].Damage = ToInt32(reader[n]); n += 1;
                                    Backpack[slot].Armor = ToInt32(reader[n]); n += 1;
                                    Backpack[slot].Type = ToInt32(reader[n]); n += 1;
                                    Backpack[slot].AttackSpeed = ToInt32(reader[n]); n += 1;                                    
                                    Backpack[slot].HealthRestore = ToInt32(reader[n]); n += 1;
                                    Backpack[slot].ManaRestore = ToInt32(reader[n]); n += 1;
                                    Backpack[slot].Strength = ToInt32(reader[n]); n += 1;
                                    Backpack[slot].Agility = ToInt32(reader[n]); n += 1;
                                    Backpack[slot].Intelligence = ToInt32(reader[n]); n += 1;
                                    Backpack[slot].Energy = ToInt32(reader[n]); n += 1;
                                    Backpack[slot].Stamina = ToInt32(reader[n]); n += 1;
                                    Backpack[slot].Value = ToInt32(reader[n]); n += 1;
                                    Backpack[slot].Price = ToInt32(reader[n]); n += 1;
                                    Backpack[slot].Rarity = ToInt32(reader[n]); n += 1;
                                    Backpack[slot].CoolDown = ToInt32(reader[n]); n += 1;
                                    Backpack[slot].AddMaxHealth = ToInt32(reader[n]); n += 1;
                                    Backpack[slot].AddMaxMana = ToInt32(reader[n]); n += 1;
                                    Backpack[slot].BonusXP = ToInt32(reader[n]); n += 1;
                                    Backpack[slot].SpellNum = ToInt32(reader[n]); n += 1;
                                    Backpack[slot].Stackable = ToBoolean(reader[n]); n += 1;
                                    Backpack[slot].MaxStack = ToInt32(reader[n]); 
                                }
                            }
                        }
                    }
                }

                //Cheeck how many bank slots they have an item in
                script = ReadAllText("SQL Data Scripts/Bank_Count.sql");
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                    object queue = cmd.ExecuteScalar();
                    result = ToInt32(queue);
                }

                //Use the result from above to load their bank
                if (result > 0)
                {
                    for (i = 0; i < result; i++)
                    {
                        script = ReadAllText("SQL Data Scripts/Load_Bank.sql");
                        using (var cmd = new SqlCommand(script, sql))
                        {
                            cmd.Parameters.Add(new SqlParameter("@owner", System.Data.DbType.String)).Value = Name;
                            //cmd.Parameters.Add(new SqlParameter("@slot", System.Data.DbType.Int32)).Value = i;

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    n = 0;
                                    bankslot = ToInt32(reader[2]);
                                    Bank[bankslot].Id = ToInt32(reader[n]); n = 3;
                                    Bank[bankslot].Name = reader[n].ToString(); n += 1;
                                    Bank[bankslot].Sprite = ToInt32(reader[n]); n += 1;
                                    Bank[bankslot].Damage = ToInt32(reader[n]); n += 1;
                                    Bank[bankslot].Armor = ToInt32(reader[n]); n += 1;
                                    Bank[bankslot].Type = ToInt32(reader[n]); n += 1;
                                    Bank[bankslot].AttackSpeed = ToInt32(reader[n]); n += 1;
                                    Bank[bankslot].HealthRestore = ToInt32(reader[n]); n += 1;
                                    Bank[bankslot].ManaRestore = ToInt32(reader[n]); n += 1;
                                    Bank[bankslot].Strength = ToInt32(reader[n]); n += 1;
                                    Bank[bankslot].Agility = ToInt32(reader[n]); n += 1;
                                    Bank[bankslot].Intelligence = ToInt32(reader[n]); n += 1;
                                    Bank[bankslot].Energy = ToInt32(reader[n]); n += 1;
                                    Bank[bankslot].Stamina = ToInt32(reader[n]); n += 1;
                                    Bank[bankslot].Value = ToInt32(reader[n]); n += 1;
                                    Bank[bankslot].Price = ToInt32(reader[n]); n += 1;
                                    Bank[bankslot].Rarity = ToInt32(reader[n]); n += 1;
                                    Bank[bankslot].CoolDown = ToInt32(reader[n]); n += 1;
                                    Bank[bankslot].AddMaxHealth = ToInt32(reader[n]); n += 1;
                                    Bank[bankslot].AddMaxMana = ToInt32(reader[n]); n += 1;
                                    Bank[bankslot].BonusXP = ToInt32(reader[n]); n += 1;
                                    Bank[bankslot].SpellNum = ToInt32(reader[n]); n += 1;
                                    Bank[bankslot].Stackable = ToBoolean(reader[n]); n += 1;
                                    Bank[bankslot].MaxStack = ToInt32(reader[n]);
                                }
                            }
                        }
                    }
                }

                //Load the hotbar
                script = ReadAllText("SQL Data Scripts/Load_Hotbar.sql");
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@playerid", System.Data.DbType.String)).Value = Id;
                    n = 0;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {                            
                            for (i = 0; i < MAX_PLAYER_HOTBAR; i++)
                            {
                                byte[] buffer;
                                object load;

                                buffer = (byte[])reader[n];
                                load = ByteArrayToObject(buffer);
                                hotBar[i].HotKey = (Keyboard.Key)load; n += 1;
                                hotBar[i].SpellNumber = ToInt32(reader[n]); n += 1;
                                hotBar[i].InvNumber = ToInt32(reader[n]); n += 1;
                            }
                        }
                    }
                }

                script = ReadAllText("SQL Data Scripts/Load_Player_Spell.sql");
                using (var cmd = new SqlCommand(script, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@playerid", System.Data.DbType.String)).Value = Id;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for (i = 0; i < MAX_PLAYER_SPELLBOOK; i++)
                            {
                                SpellBook[i].SpellNumber = ToInt32(reader[i]);
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

    [Serializable()]
    public class HotBar
    {
        public Keyboard.Key HotKey;
        public int SpellNumber { get; set; }
        public int InvNumber { get; set; }

        public HotBar(Keyboard.Key key, int spellnum, int invnum)
        {
            HotKey = key;
            SpellNumber = spellnum;
            InvNumber = invnum;
        }
    }

    public class SpellBook
    {
        public int SpellNumber { get; set; }

        public SpellBook() { }

        public SpellBook(int spellNum)
        {
            SpellNumber = spellNum;
        }
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
