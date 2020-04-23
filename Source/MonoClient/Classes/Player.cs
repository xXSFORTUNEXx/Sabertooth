using Lidgren.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using static System.Environment;
using System;
using static Mono_Client.Client;
using AccountKeyGenClass;
using static Mono_Client.Globals;
using System.IO;

namespace Mono_Client
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
        public int OffsetX { get; set; }
        public int OffsetY { get; set; }
        public int Kills { get; set; }
        #endregion

        #region Local Variables
        bool Moved;
        bool Attacking;
        int attackTick;
        int timeTick;
        int lifeTick;
        int pickupTick;
        int equipTick;
        int interactionTick;
        public int reloadTick;
        public int tempX;
        public int tempY;
        public int tempDir;
        public int tempaimDir;
        public int tempStep;
        public int Step;
        public bool inShop;
        public int shopNum;
        public bool inChat;
        public int chatNum;
        public bool inBank;
        public int chestNum;
        public bool inChest;
        public bool isChangingMaps;
        #endregion

        #region Class Constructors
        public Player(string name, string pass, string email, int x, int y, int direction, int aimdirection, int map, int level, int points, int health, int exp, int money, int armor, int hunger, 
                      int hydration, int str, int agi, int end, int sta, int defaultAmmo, NetConnection conn)
        {
            Name = name;
            Pass = pass;
            EmailAddress = email;
            X = x;
            Y = y;
            Map = map;
            if (SCREEN_WIDTH == 1024 && SCREEN_HEIGHT == 768)
            {
                OffsetX = 16;
                OffsetY = 11;
            }
            else
            {
                OffsetX = 12;
                OffsetY = 9;
            }
            Direction = direction;
            AimDirection = aimdirection;
            Level = level;
            Points = points;
            Health = health;
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
            Kills = 0;
        }

        public Player(string name, string pass, NetConnection conn)
        {
            Name = name;
            Pass = pass;
            Connection = conn;
            if (SCREEN_WIDTH == 1024 && SCREEN_HEIGHT == 768)
            {
                OffsetX = 16;
                OffsetY = 11;
            }
            else
            {
                OffsetX = 12;
                OffsetY = 9;
            }
        }

        public Player(string name)
        {
            Name = name;
        }

        public Player(NetConnection conn)
        {
            Connection = conn;
        }

        public Player()
        {
            for (int i = 0; i < 25; i++)
            {
                Backpack[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0);
            }
            for (int i = 0; i < 50; i++)
            {
                Bank[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1);
            }
        }
        #endregion

        #region Voids
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            int x = (X * 32) + (OffsetX * 32);
            int y = (Y * 32) + (OffsetY * 32) - 16;
            int step = (Step * 32);
            int dir = (AimDirection * 48);
            spriteBatch.Draw(c_Sprite[Sprite], new Rectangle((x * 32), (y * 32), 32, 48), new Rectangle(step, dir, 32, 48), Color.White);
        }

        #region Controller
        /*
        public void CheckControllerMovement()
        {
            if (!Joystick.IsConnected(0)) { return; }
            if (Moved == true) { Moved = false; return; }
            if (gui.inputChat.HasFocus == true) { return; }
            if (!renderWindow.HasFocus()) { return; }
            if (inShop|| inChat || inBank) { return; }
            if (isChangingMaps) { return; }

            float deadZone = 35;
            float x = SnaptoZero(Joystick.GetAxisPosition(0, Joystick.Axis.X), deadZone);
            float y = SnaptoZero(Joystick.GetAxisPosition(0, Joystick.Axis.Y), deadZone);

            gui.d_Controller.Text = "X: " + x + " / Y: " + y;

            if (x > deadZone)
            {
                if (X < (map.MaxX - 1) - OffsetX)
                {
                    if (map.Ground[(X + OffsetX) + 1, (Y + OffsetY)].Type == (int)TileType.Blocked || map.Ground[(X + OffsetX) + 1, (Y + OffsetY)].Type == (int)TileType.Chest)
                    {
                        Direction = (int)Directions.Right;
                        Moved = false;
                        SendUpdateDirection();
                        return;
                    }
                    X += 1;
                    Direction = (int)Directions.Right;
                    Moved = true;
                }
            }

            if (x < -deadZone)
            {
                if (X > 0 - OffsetX)
                {
                    if (map.Ground[(X + OffsetX) - 1, (Y + OffsetY)].Type == (int)TileType.Blocked || map.Ground[(X + OffsetX) - 1, (Y + OffsetY)].Type == (int)TileType.Chest)
                    {
                        Direction = (int)Directions.Left;
                        Moved = false;
                        SendUpdateDirection();
                        return;
                    }
                    X -= 1;
                    Direction = (int)Directions.Left;
                    Moved = true;
                }
            }

            if (y > deadZone)
            {
                if (Y < (map.MaxY - 1) - OffsetY)
                {
                    if (map.Ground[(X + OffsetX), (Y + OffsetY) + 1].Type == (int)TileType.Blocked || map.Ground[(X + OffsetX), (Y + OffsetY) + 1].Type == (int)TileType.Chest)
                    {
                        Direction = (int)Directions.Down;
                        Moved = false;
                        SendUpdateDirection();
                        return;
                    }
                    Y += 1;
                    Direction = (int)Directions.Down;
                    Moved = true;
                }
            }

            if (y < -deadZone)
            {
                if (Y > 0 - OffsetY)
                {
                    if (map.Ground[(X + OffsetX), (Y + OffsetY) - 1].Type == (int)TileType.Blocked || map.Ground[(X + OffsetX), (Y + OffsetY) - 1].Type == (int)TileType.Chest)
                    {
                        Direction = (int)Directions.Up;
                        Moved = false;
                        SendUpdateDirection();
                        return;
                    }
                    Y -= 1;
                    Direction = (int)Directions.Up;
                    Moved = true;
                }
            }

            if (Moved == true)
            {
                Step += 1;
                if (Step == 4) { Step = 0; }
                Moved = false;
                SendMovementData();
            }

            if (map.Ground[(X + OffsetX), (Y + OffsetY)].Type == (int)TileType.Warp)
            {
                SendPlayerWarp();
            }
        }

        public void CheckControllerChangeDirection()
        {
            if (!Joystick.IsConnected(0)) { return; }
            if (gui.inputChat.HasFocus == true) { return; }
            if (!renderWindow.HasFocus()) { return; }
            if (inShop || inChat || inBank) { return; }

            float deadZone = 35;
            float u = SnaptoZero(Joystick.GetAxisPosition(0, Joystick.Axis.U), deadZone);
            float r = SnaptoZero(Joystick.GetAxisPosition(0, Joystick.Axis.R), deadZone);

            gui.d_ConDir.Text = "Direction: " + AimDirection;

            if (u > deadZone)
            {
                AimDirection = (int)Directions.Right;
                SendUpdateDirection();
            }

            if (u < -deadZone)
            {
                AimDirection = (int)Directions.Left;
                SendUpdateDirection();
            }

            if (r > deadZone)
            {
                AimDirection = (int)Directions.Down;
                SendUpdateDirection();
            }

            if (r < -deadZone)
            {
                AimDirection = (int)Directions.Up;
                SendUpdateDirection();
            }
        }

        public void CheckControllerAttack()
        {
            if (!Joystick.IsConnected(0)) { return; }
            if (gui.inputChat.HasFocus == true) { return; }
            if (!renderWindow.HasFocus()) { return; }
            if (Attacking == true) { return; }
            if (TickCount - reloadTick < mainWeapon.ReloadSpeed) { return; }
            if (TickCount - equipTick < 5000) { return; }
            if (inShop || inChat || inBank) { return; }

            bool isGun = false;

            if (Joystick.GetAxisPosition(0, Joystick.Axis.Z) < -25)
            {
                if (mainWeapon.Name != "None")
                {
                    Attacking = true;
                    isGun = true;
                }
                else
                {
                    gui.AddText("You need a weapon equiped to attack!");
                    equipTick = TickCount;
                    return;
                }
            }

            if (Joystick.IsButtonPressed(0, 1))
            {
                if (offWeapon.Name != "None")
                {
                    Attacking = true;
                    isGun = false;
                }
                else
                {
                    gui.AddText("You need a melee weapon equiped to attack!");
                    equipTick = TickCount;
                }
            }

            if (Attacking == true)
            {
                if (isGun)
                {
                    switch (mainWeapon.ItemAmmoType)
                    {
                        case (int)AmmoType.Pistol:
                            if (mainWeapon.Clip == 0 && PistolAmmo == 0) { Attacking = false; return; }
                            break;
                        case (int)AmmoType.AssaultRifle:
                            if (mainWeapon.Clip == 0 && AssaultAmmo == 0) { Attacking = false; return; }
                            break;
                        case (int)AmmoType.Rocket:
                            if (mainWeapon.Clip == 0 && RocketAmmo == 0) { Attacking = false; return; }
                            break;
                        case (int)AmmoType.Grenade:
                            if (mainWeapon.Clip == 0 && GrenadeAmmo == 0) { Attacking = false; return; }
                            break;
                    }
                    if (TickCount - attackTick < mainWeapon.AttackSpeed) { Attacking = false; return; }
                    CreateBulletSound();
                    RemoveBulletFromClip();
                    Attacking = false;
                    attackTick = TickCount;
                }
                else
                {
                    if (TickCount - attackTick < offWeapon.AttackSpeed) { Attacking = false; return; }
                    switch (AimDirection)
                    {
                        case (int)Directions.Up:
                            if (Y > 2 - OffsetY)
                            {
                                for (int i = 0; i < MAX_MAP_POOL_NPCS; i++)
                                {
                                    if (i < MAX_MAP_NPCS)
                                    {
                                        if (map.m_MapNpc[i].IsSpawned)
                                        {
                                            if (map.m_MapNpc[i].Behavior == (int)BehaviorType.Aggressive)
                                            {
                                                if (map.m_MapNpc[i].Y + 1 == (Y + OffsetY) && map.m_MapNpc[i].X == (X + OffsetX))
                                                {
                                                    SendMeleeAttack(i, 0);
                                                }
                                                else if (map.m_MapNpc[i].Y == (Y + OffsetY) && map.m_MapNpc[i].X == (X + OffsetX))
                                                {
                                                    SendMeleeAttack(i, 0);
                                                }
                                            }
                                        }
                                    }
                                    if (map.r_MapNpc[i].IsSpawned)
                                    {
                                        if (map.r_MapNpc[i].Behavior == (int)BehaviorType.Aggressive)
                                        {
                                            if (map.r_MapNpc[i].Y + 1 == (Y + OffsetY) && map.r_MapNpc[i].X == (X + OffsetX))
                                            {
                                                SendMeleeAttack(i, 1);
                                            }
                                            else if (map.r_MapNpc[i].Y == (Y + OffsetY) && map.r_MapNpc[i].X == (X + OffsetX))
                                            {
                                                SendMeleeAttack(i, 1);
                                            }
                                        }
                                    }
                                }
                            }
                            break;

                        case (int)Directions.Down:
                            if (Y < (map.MaxY - 2) - OffsetY)
                            {
                                for (int i = 0; i < MAX_MAP_POOL_NPCS; i++)
                                {
                                    if (i < MAX_MAP_NPCS)
                                    {
                                        if (map.m_MapNpc[i].IsSpawned)
                                        {
                                            if (map.m_MapNpc[i].Behavior == (int)BehaviorType.Aggressive)
                                            {
                                                if (map.m_MapNpc[i].Y - 1 == (Y + OffsetY) && map.m_MapNpc[i].X == (X + OffsetX))
                                                {
                                                    SendMeleeAttack(i, 0);
                                                }
                                                else if (map.m_MapNpc[i].Y == (Y + OffsetY) && map.m_MapNpc[i].X == (X + OffsetX))
                                                {
                                                    SendMeleeAttack(i, 0);
                                                }
                                            }
                                        }
                                    }
                                    if (map.r_MapNpc[i].IsSpawned)
                                    {
                                        if (map.r_MapNpc[i].Behavior == (int)BehaviorType.Aggressive)
                                        {
                                            if (map.r_MapNpc[i].Y - 1 == (Y + OffsetY) && map.r_MapNpc[i].X == (X + OffsetX))
                                            {
                                                SendMeleeAttack(i, 1);
                                            }
                                            else if (map.r_MapNpc[i].Y == (Y + OffsetY) && map.r_MapNpc[i].X == (X + OffsetX))
                                            {
                                                SendMeleeAttack(i, 1);
                                            }
                                        }
                                    }
                                }
                            }
                            break;

                        case (int)Directions.Left:
                            if (X > 2 - OffsetX)
                            {
                                for (int i = 0; i < MAX_MAP_POOL_NPCS; i++)
                                {
                                    if (i < MAX_MAP_NPCS)
                                    {
                                        if (map.m_MapNpc[i].IsSpawned)
                                        {
                                            if (map.m_MapNpc[i].Behavior == (int)BehaviorType.Aggressive)
                                            {
                                                if (map.m_MapNpc[i].X + 1 == (X + OffsetX) && map.m_MapNpc[i].Y == (Y + OffsetY))
                                                {
                                                    SendMeleeAttack(i, 0);
                                                }
                                                else if (map.m_MapNpc[i].Y == (Y + OffsetY) && map.m_MapNpc[i].X == (X + OffsetX))
                                                {
                                                    SendMeleeAttack(i, 0);
                                                }
                                            }
                                        }
                                    }
                                    if (map.r_MapNpc[i].IsSpawned)
                                    {
                                        if (map.r_MapNpc[i].Behavior == (int)BehaviorType.Aggressive)
                                        {
                                            if (map.r_MapNpc[i].X + 1 == (X + OffsetX) && map.r_MapNpc[i].Y == (Y + OffsetY))
                                            {
                                                SendMeleeAttack(i, 1);
                                            }
                                            else if (map.r_MapNpc[i].Y == (Y + OffsetY) && map.r_MapNpc[i].X == (X + OffsetX))
                                            {
                                                SendMeleeAttack(i, 1);
                                            }
                                        }
                                    }
                                }
                            }
                            break;

                        case (int)Directions.Right:
                            if (X < (map.MaxX - 2) - OffsetX)
                            {
                                for (int i = 0; i < MAX_MAP_POOL_NPCS; i++)
                                {
                                    if (i < MAX_MAP_NPCS)
                                    {
                                        if (map.m_MapNpc[i].IsSpawned)
                                        {
                                            if (map.m_MapNpc[i].Behavior == (int)BehaviorType.Aggressive)
                                            {
                                                if (map.m_MapNpc[i].X - 1 == (X + OffsetX) && map.m_MapNpc[i].Y == (Y + OffsetY))
                                                {
                                                    SendMeleeAttack(i, 0);
                                                }
                                                else if (map.m_MapNpc[i].Y == (Y + OffsetY) && map.m_MapNpc[i].X == (X + OffsetX))
                                                {
                                                    SendMeleeAttack(i, 0);
                                                }
                                            }
                                        }
                                    }
                                    if (map.r_MapNpc[i].IsSpawned)
                                    {
                                        if (map.r_MapNpc[i].Behavior == (int)BehaviorType.Aggressive)
                                        {
                                            if (map.r_MapNpc[i].X - 1 == (X + OffsetX) && map.r_MapNpc[i].Y == (Y + OffsetY))
                                            {
                                                SendMeleeAttack(i, 1);
                                            }
                                            else if (map.r_MapNpc[i].Y == (Y + OffsetY) && map.r_MapNpc[i].X == (X + OffsetX))
                                            {
                                                SendMeleeAttack(i, 1);
                                            }
                                        }
                                    }
                                }

                            }
                            break;
                    }
                    Attacking = false;
                    attackTick = TickCount;
                }
            }
        }

        public void CheckControllerReload()
        {
            if (inShop || inChat || inBank) { return; }

            if (!Joystick.IsConnected(0)) { return; }
            if (Joystick.IsButtonPressed(0, 2))
            {
                if (mainWeapon.Clip == mainWeapon.MaxClip) { return; }
                CreateReloadSound();
                Reload();
                reloadTick = TickCount;
                SendUpdateClip();
                SendUpdateAmmo();
            }
        }

        public void CheckControllerItemPickUp()
        {
            if (inShop || inChat || inBank) { return; }

            if (!Joystick.IsConnected(0)) { return; }
            if (gui.inputChat.HasFocus == true) { return; }
            if (!renderWindow.HasFocus()) { return; }
            if (Attacking == true) { return; }
            if (inShop || inChat) { return; }

            if (Joystick.IsButtonPressed(0, 0))
            {
                SendPickupItem();
                pickupTick = TickCount;
            }
        }

        public void CheckControllerButtonPress()
        {
            if (!Joystick.IsConnected(0)) { return; }
            if (gui.inputChat.HasFocus == true) { return; }
            if (!renderWindow.HasFocus()) { return; }
            if (Attacking == true) { return; }            
            if (inShop || inChat || inBank) { return; }

            if (Joystick.IsButtonPressed(0, 6))
            {
                if (gui.chatWindow != null)
                {
                    if (gui.inputChat.HasFocus) { return; }

                    if (gui.chatWindow.IsVisible)
                    {
                        gui.chatWindow.Hide();
                    }
                    else
                    {
                        gui.chatWindow.Show();
                    }
                }
            }

            if (Joystick.IsButtonPressed(0, 7))
            {
                if (gui.menuWindow != null)
                {
                    if (gui.inputChat.HasFocus) { return; }

                    if (gui.menuWindow.IsVisible)
                    {
                        gui.menuWindow.Hide();
                    }
                    else
                    {
                        gui.menuWindow.Show();
                        gui.charTab.Focus();
                    }
                }
            }
        }

        public void CheckControllerPlayerInteraction()
        {
            if (!Joystick.IsConnected(0)) { return; }
            if (gui.inputChat.HasFocus == true) { return; }
            if (!renderWindow.HasFocus()) { return; }
            if (Attacking == true) { return; }
            if (TickCount - interactionTick < 1000) { return; }
            if (inShop || inChat || inBank) { return; }

            if (Joystick.IsButtonPressed(0, 3))
            {
                switch (AimDirection)
                {
                    case (int)Directions.Up:
                        if (Y > 2 - OffsetY)
                        {
                            for (int i = 0; i < 10; i++)
                            {
                                if (map.m_MapNpc[i].IsSpawned)
                                {
                                    if (map.m_MapNpc[i].Behavior == (int)BehaviorType.ShopOwner || map.m_MapNpc[i].Behavior == (int)BehaviorType.Friendly || map.m_MapNpc[i].Behavior == (int)BehaviorType.Passive)
                                    {
                                        if (map.m_MapNpc[i].Y + 1 == (Y + OffsetY) && map.m_MapNpc[i].X == (X + OffsetX))
                                        {
                                            SendInteraction(i, 0);
                                        }
                                    }
                                }
                            }

                            if (map.Ground[(X + OffsetX), (Y + OffsetY) - 1].Type == (int)TileType.Chest)
                            {
                                SendInteraction(map.Ground[(X + OffsetX), (Y + OffsetY) - 1].ChestNum, 1);
                            }
                        }
                        break;

                    case (int)Directions.Down:
                        if (Y < 48 - OffsetY)
                        {
                            for (int i = 0; i < 10; i++)
                            {
                                if (map.m_MapNpc[i].IsSpawned)
                                {
                                    if (map.m_MapNpc[i].Behavior == (int)BehaviorType.ShopOwner || map.m_MapNpc[i].Behavior == (int)BehaviorType.Friendly || map.m_MapNpc[i].Behavior == (int)BehaviorType.Passive)
                                    {
                                        if (map.m_MapNpc[i].Y - 1 == (Y + OffsetY) && map.m_MapNpc[i].X == (X + OffsetX))
                                        {
                                            SendInteraction(i, 0);
                                        }
                                    }
                                }
                            }

                            if (map.Ground[(X + OffsetX), (Y + OffsetY) + 1].Type == (int)TileType.Chest)
                            {
                                SendInteraction(map.Ground[(X + OffsetX), (Y + OffsetY) + 1].ChestNum, 1);
                            }
                        }
                        break;

                    case (int)Directions.Left:
                        if (X > 2 - OffsetX)
                        {
                            for (int i = 0; i < 10; i++)
                            {
                                if (map.m_MapNpc[i].IsSpawned)
                                {
                                    if (map.m_MapNpc[i].Behavior == (int)BehaviorType.ShopOwner || map.m_MapNpc[i].Behavior == (int)BehaviorType.Friendly || map.m_MapNpc[i].Behavior == (int)BehaviorType.Passive)
                                    {
                                        if (map.m_MapNpc[i].X + 1 == (X + OffsetX) && map.m_MapNpc[i].Y == (Y + OffsetY))
                                        {
                                            SendInteraction(i, 0);
                                        }
                                    }
                                }
                            }

                            if (map.Ground[(X + OffsetX) - 1, (Y + OffsetY)].Type == (int)TileType.Chest)
                            {
                                SendInteraction(map.Ground[(X + OffsetX) - 1, (Y + OffsetY)].ChestNum, 1);
                            }
                        }
                        break;

                    case (int)Directions.Right:
                        if (X < 48 - OffsetX)
                        {
                            for (int i = 0; i < 10; i++)
                            {
                                if (map.m_MapNpc[i].IsSpawned)
                                {
                                    if (map.m_MapNpc[i].Behavior == (int)BehaviorType.ShopOwner || map.m_MapNpc[i].Behavior == (int)BehaviorType.Friendly || map.m_MapNpc[i].Behavior == (int)BehaviorType.Passive)
                                    {
                                        if (map.m_MapNpc[i].X - 1 == (X + OffsetX) && map.m_MapNpc[i].Y == (Y + OffsetY))
                                        {
                                            SendInteraction(i, 0);
                                        }
                                    }
                                }
                            }

                            if (map.Ground[(X + OffsetX) + 1, (Y + OffsetY)].Type == (int)TileType.Chest)
                            {
                                SendInteraction(map.Ground[(X + OffsetX) + 1, (Y + OffsetY)].ChestNum, 1);
                            }
                        }
                        break;
                }
                interactionTick = TickCount;
            }
        }

        float SnaptoZero(float value, float threshold)
        {
            if (Math.Abs(value) < threshold)
            {
                return 0;
            }
            return value;
        }*/
        #endregion

        #region Keyboard
        public void CheckMovement()
        {
            if (Moved == true) { Moved = false; return; }
            //if (gui.inputChat.HasFocus == true) { return; }
            if (inShop || inChat || inBank) { return; }
            if (isChangingMaps) { return; }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                if (Y > 0 - OffsetY)
                {
                    if (map.Ground[(X + OffsetX), (Y + OffsetY) - 1].Type == (int)TileType.Blocked || map.Ground[(X + OffsetX), (Y + OffsetY) - 1].Type == (int)TileType.Chest)
                    {
                        Direction = (int)Directions.Up;
                        Moved = false;
                        SendUpdateDirection();
                        return;
                    }
                    Y -= 1;
                    Direction = (int)Directions.Up; 
                    Moved = true;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                if (Y < (map.MaxY - 1) - OffsetY)  
                {
                    if (map.Ground[(X + OffsetX), (Y + OffsetY) + 1].Type == (int)TileType.Blocked || map.Ground[(X + OffsetX), (Y + OffsetY) + 1].Type == (int)TileType.Chest)
                    {
                        Direction = (int)Directions.Down;
                        Moved = false; 
                        SendUpdateDirection();
                        return; 
                    }
                    Y += 1;
                    Direction = (int)Directions.Down;
                    Moved = true;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A)) 
            {
                if (X > 0 - OffsetX)
                {
                    if (map.Ground[(X + OffsetX) - 1, (Y + OffsetY)].Type == (int)TileType.Blocked || map.Ground[(X + OffsetX) - 1, (Y + OffsetY)].Type == (int)TileType.Chest)
                    {
                        Direction = (int)Directions.Left;
                        Moved = false; 
                        SendUpdateDirection();
                        return;
                    }
                    X -= 1;
                    Direction = (int)Directions.Left;
                    Moved = true;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                if (X < (map.MaxX - 1) - OffsetX) 
                {
                    if (map.Ground[(X + OffsetX) + 1, (Y + OffsetY)].Type == (int)TileType.Blocked || map.Ground[(X + OffsetX) + 1, (Y + OffsetY)].Type == (int)TileType.Chest) 
                    {
                        Direction = (int)Directions.Right;
                        Moved = false;
                        SendUpdateDirection();
                        return;
                    }
                    X += 1;
                    Direction = (int)Directions.Right;
                    Moved = true;
                }
            }

            if (Moved == true)
            {
                Step += 1;
                if (Step == 4) { Step = 0; }
                Moved = false;
                SendMovementData();
            }

            if (map.Ground[(X + OffsetX), (Y + OffsetY)].Type == (int)TileType.Warp)
            {
                SendPlayerWarp();
            }
        }
        
        public void CheckChangeDirection()
        {
            //if (gui.inputChat.HasFocus == true) { return; }
            if (inShop || inChat || inBank) { return; }

            if (Keyboard.GetState().IsKeyDown(Keys.I))
            {
                AimDirection = (int)Directions.Up;
                SendUpdateDirection();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.K))
            {
                AimDirection = (int)Directions.Down;
                SendUpdateDirection();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.J))
            {
                AimDirection = (int)Directions.Left;
                SendUpdateDirection();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.L))
            {
                AimDirection = (int)Directions.Right;
                SendUpdateDirection();
            }
        }

        public void CheckAttack()
        {
            //if (gui.inputChat.HasFocus == true) { return; }
            if (Attacking == true) { return; }
            if (TickCount - reloadTick < mainWeapon.ReloadSpeed) { return; }
            if (TickCount - equipTick < 5000) { return; }
            if (inShop || inChat || inBank) { return; }

            bool isGun = false;

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (mainWeapon.Name != "None")
                {
                    Attacking = true;
                    isGun = true;
                }
                else
                {
                    gui.AddText("You need a weapon equiped to attack!");
                    equipTick = TickCount;
                    return;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.F))
            {
                if (offWeapon.Name != "None")
                {
                    Attacking = true;
                    isGun = false;
                }
                else
                {
                    gui.AddText("You need a melee weapon equiped to attack!");
                    equipTick = TickCount;
                }
            }

            if (Attacking == true)
            {
                if (isGun)
                {
                    switch (mainWeapon.ItemAmmoType)
                    {
                        case (int)AmmoType.Pistol:
                            if (mainWeapon.Clip == 0 && PistolAmmo == 0) { Attacking = false; return; }
                            break;
                        case (int)AmmoType.AssaultRifle:
                            if (mainWeapon.Clip == 0 && AssaultAmmo == 0) { Attacking = false; return; }
                            break;
                        case (int)AmmoType.Rocket:
                            if (mainWeapon.Clip == 0 && RocketAmmo == 0) { Attacking = false; return; }
                            break;
                        case (int)AmmoType.Grenade:
                            if (mainWeapon.Clip == 0 && GrenadeAmmo == 0) { Attacking = false; return; }
                            break;
                    }
                    if (TickCount - attackTick < mainWeapon.AttackSpeed) { Attacking = false; return; }
                    CreateBulletSound();
                    RemoveBulletFromClip();
                    Attacking = false;
                    attackTick = TickCount;
                }
                else
                {
                    if (TickCount - attackTick < offWeapon.AttackSpeed) { Attacking = false; return; }
                    switch (AimDirection)
                    {
                        case (int)Directions.Up:
                            if (Y > 2 - OffsetY)
                            {
                                for (int i = 0; i < MAX_MAP_POOL_NPCS; i++)
                                {
                                    if (i < MAX_MAP_NPCS)
                                    {
                                        if (map.m_MapNpc[i].IsSpawned)
                                        {
                                            if (map.m_MapNpc[i].Behavior == (int)BehaviorType.Aggressive)
                                            {
                                                if (map.m_MapNpc[i].Y + 1 == (Y + OffsetY) && map.m_MapNpc[i].X == (X + OffsetX))
                                                {
                                                    SendMeleeAttack(i, 0);
                                                }
                                                else if (map.m_MapNpc[i].Y == (Y + OffsetY) && map.m_MapNpc[i].X == (X + OffsetX))
                                                {
                                                    SendMeleeAttack(i, 0);
                                                }
                                            }
                                        }
                                    }
                                    if (map.r_MapNpc[i].IsSpawned)
                                    {
                                        if (map.r_MapNpc[i].Behavior == (int)BehaviorType.Aggressive)
                                        {
                                            if (map.r_MapNpc[i].Y + 1 == (Y + OffsetY) && map.r_MapNpc[i].X == (X + OffsetX))
                                            {
                                                SendMeleeAttack(i, 1);
                                            }
                                            else if (map.r_MapNpc[i].Y == (Y + OffsetY) && map.r_MapNpc[i].X == (X + OffsetX))
                                            {
                                                SendMeleeAttack(i, 1);
                                            }
                                        }
                                    }
                                }
                            }
                            break;

                        case (int)Directions.Down:
                            if (Y < (map.MaxY - 2) - OffsetY)
                            {
                                for (int i = 0; i < MAX_MAP_POOL_NPCS; i++)
                                {
                                    if (i < MAX_MAP_NPCS)
                                    {
                                        if (map.m_MapNpc[i].IsSpawned)
                                        {
                                            if (map.m_MapNpc[i].Behavior == (int)BehaviorType.Aggressive)
                                            {
                                                if (map.m_MapNpc[i].Y - 1 == (Y + OffsetY) && map.m_MapNpc[i].X == (X + OffsetX))
                                                {
                                                    SendMeleeAttack(i, 0);
                                                }
                                                else if (map.m_MapNpc[i].Y == (Y + OffsetY) && map.m_MapNpc[i].X == (X + OffsetX))
                                                {
                                                    SendMeleeAttack(i, 0);
                                                }
                                            }
                                        }
                                    }
                                    if (map.r_MapNpc[i].IsSpawned)
                                    {
                                        if (map.r_MapNpc[i].Behavior == (int)BehaviorType.Aggressive)
                                        {
                                            if (map.r_MapNpc[i].Y - 1 == (Y + OffsetY) && map.r_MapNpc[i].X == (X + OffsetX))
                                            {
                                                SendMeleeAttack(i, 1);
                                            }
                                            else if (map.r_MapNpc[i].Y == (Y + OffsetY) && map.r_MapNpc[i].X == (X + OffsetX))
                                            {
                                                SendMeleeAttack(i, 1);
                                            }
                                        }
                                    }
                                }
                            }
                            break;

                        case (int)Directions.Left:
                            if (X > 2 - OffsetX)
                            {
                                for (int i = 0; i < MAX_MAP_POOL_NPCS; i++)
                                {
                                    if (i < MAX_MAP_NPCS)
                                    {
                                        if (map.m_MapNpc[i].IsSpawned)
                                        {
                                            if (map.m_MapNpc[i].Behavior == (int)BehaviorType.Aggressive)
                                            {
                                                if (map.m_MapNpc[i].X + 1 == (X + OffsetX) && map.m_MapNpc[i].Y == (Y + OffsetY))
                                                {
                                                    SendMeleeAttack(i, 0);
                                                }
                                                else if (map.m_MapNpc[i].Y == (Y + OffsetY) && map.m_MapNpc[i].X == (X + OffsetX))
                                                {
                                                    SendMeleeAttack(i, 0);
                                                }
                                            }
                                        }
                                    }
                                    if (map.r_MapNpc[i].IsSpawned)
                                    {
                                        if (map.r_MapNpc[i].Behavior == (int)BehaviorType.Aggressive)
                                        {
                                            if (map.r_MapNpc[i].X + 1 == (X + OffsetX) && map.r_MapNpc[i].Y == (Y + OffsetY))
                                            {
                                                SendMeleeAttack(i, 1);
                                            }
                                            else if (map.r_MapNpc[i].Y == (Y + OffsetY) && map.r_MapNpc[i].X == (X + OffsetX))
                                            {
                                                SendMeleeAttack(i, 1);
                                            }
                                        }
                                    }
                                }
                            }
                            break;

                        case (int)Directions.Right:
                            if (X < (map.MaxX - 2) - OffsetX)
                            {
                                for (int i = 0; i < MAX_MAP_POOL_NPCS; i++)
                                {
                                    if (i < MAX_MAP_NPCS)
                                    {
                                        if (map.m_MapNpc[i].IsSpawned)
                                        {
                                            if (map.m_MapNpc[i].Behavior == (int)BehaviorType.Aggressive)
                                            {
                                                if (map.m_MapNpc[i].X - 1 == (X + OffsetX) && map.m_MapNpc[i].Y == (Y + OffsetY))
                                                {
                                                    SendMeleeAttack(i, 0);
                                                }
                                                else if (map.m_MapNpc[i].Y == (Y + OffsetY) && map.m_MapNpc[i].X == (X + OffsetX))
                                                {
                                                    SendMeleeAttack(i, 0);
                                                }
                                            }
                                        }
                                    }
                                    if (map.r_MapNpc[i].IsSpawned)
                                    {
                                        if (map.r_MapNpc[i].Behavior == (int)BehaviorType.Aggressive)
                                        {
                                            if (map.r_MapNpc[i].X - 1 == (X + OffsetX) && map.r_MapNpc[i].Y == (Y + OffsetY))
                                            {
                                                SendMeleeAttack(i, 1);
                                            }
                                            else if (map.r_MapNpc[i].Y == (Y + OffsetY) && map.r_MapNpc[i].X == (X + OffsetX))
                                            {
                                                SendMeleeAttack(i, 1);
                                            }
                                        }
                                    }
                                }

                            }
                            break;
                    }
                    Attacking = false;
                    attackTick = TickCount;
                }
            }
        }

        public void CheckReload()
        {
            if (inShop || inChat || inBank) { return; }

            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                if (mainWeapon.Clip == mainWeapon.MaxClip) { return; }
                CreateReloadSound();
                Reload();
                reloadTick = TickCount;
                SendUpdateClip();
                SendUpdateAmmo();
            }
        }

        public void CheckItemPickUp()
        {
            //if (gui.inputChat.HasFocus == true) { return; }
            if (Attacking == true) { return; }
            if (inShop || inChat || inBank) { return; }

            if (Keyboard.GetState().IsKeyDown(Keys.O))
            {
                SendPickupItem();
                pickupTick = TickCount;
            }
        }

        public void CheckPlayerInteraction()
        {
            //if (gui.inputChat.HasFocus == true) { return; }
            if (Attacking == true) { return; }
            if (TickCount - interactionTick < 1000) { return; }
            if (inShop || inChat || inBank) { return; }

            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                switch (AimDirection)
                {
                    case (int)Directions.Up:
                        if (Y > 2 - OffsetY)
                        {
                            for (int i = 0; i < MAX_MAP_NPCS; i++)
                            {
                                if (map.m_MapNpc[i].IsSpawned)
                                {
                                    if (map.m_MapNpc[i].Behavior == (int)BehaviorType.ShopOwner || map.m_MapNpc[i].Behavior == (int)BehaviorType.Friendly || map.m_MapNpc[i].Behavior == (int)BehaviorType.Passive)
                                    {
                                        if (map.m_MapNpc[i].Y + 1 == (Y + OffsetY) && map.m_MapNpc[i].X == (X + OffsetX))
                                        {
                                            SendInteraction(i, 0);
                                        }
                                    }
                                }
                            }

                            if (map.Ground[(X + OffsetX), (Y + OffsetY) - 1].Type == (int)TileType.Chest)
                            {
                                SendInteraction(map.Ground[(X + OffsetX), (Y + OffsetY) - 1].ChestNum, 1);
                            }
                        }
                        break;

                    case (int)Directions.Down:
                        if (Y < (map.MaxY - 2) - OffsetY)
                        {
                            for (int i = 0; i < MAX_MAP_NPCS; i++)
                            {
                                if (map.m_MapNpc[i].IsSpawned)
                                {
                                    if (map.m_MapNpc[i].Behavior == (int)BehaviorType.ShopOwner || map.m_MapNpc[i].Behavior == (int)BehaviorType.Friendly || map.m_MapNpc[i].Behavior == (int)BehaviorType.Passive)
                                    {
                                        if (map.m_MapNpc[i].Y - 1 == (Y + OffsetY) && map.m_MapNpc[i].X == (X + OffsetX))
                                        {
                                            SendInteraction(i, 0);
                                        }
                                    }
                                }
                            }

                            if (map.Ground[(X + OffsetX), (Y + OffsetY) + 1].Type == (int)TileType.Chest)
                            {
                                SendInteraction(map.Ground[(X + OffsetX), (Y + OffsetY) + 1].ChestNum, 1);
                            }
                        }
                        break;

                    case (int)Directions.Left:
                        if (X > 2 - OffsetX)
                        {
                            for (int i = 0; i < MAX_MAP_NPCS; i++)
                            {
                                if (map.m_MapNpc[i].IsSpawned)
                                {
                                    if (map.m_MapNpc[i].Behavior == (int)BehaviorType.ShopOwner || map.m_MapNpc[i].Behavior == (int)BehaviorType.Friendly || map.m_MapNpc[i].Behavior == (int)BehaviorType.Passive)
                                    {
                                        if (map.m_MapNpc[i].X + 1 == (X + OffsetX) && map.m_MapNpc[i].Y == (Y + OffsetY))
                                        {
                                            SendInteraction(i, 0);
                                        }
                                    }
                                }
                            }

                            if (map.Ground[(X + OffsetX) - 1, (Y + OffsetY)].Type == (int)TileType.Chest)
                            {
                                SendInteraction(map.Ground[(X + OffsetX) - 1, (Y + OffsetY)].ChestNum, 1);
                            }
                        }
                        break;

                    case (int)Directions.Right:
                        if (X < (map.MaxX - 2) - OffsetX)
                        {
                            for (int i = 0; i < MAX_MAP_NPCS; i++)
                            {
                                if (map.m_MapNpc[i].IsSpawned)
                                {
                                    if (map.m_MapNpc[i].Behavior == (int)BehaviorType.ShopOwner || map.m_MapNpc[i].Behavior == (int)BehaviorType.Friendly || map.m_MapNpc[i].Behavior == (int)BehaviorType.Passive)
                                    {
                                        if (map.m_MapNpc[i].X - 1 == (X + OffsetX) && map.m_MapNpc[i].Y == (Y + OffsetY))
                                        {
                                            SendInteraction(i, 0);
                                        }
                                    }
                                }
                            }

                            if (map.Ground[(X + OffsetX) + 1, (Y + OffsetY)].Type == (int)TileType.Chest)
                            {
                                SendInteraction(map.Ground[(X + OffsetX) + 1, (Y + OffsetY)].ChestNum, 1);
                            }
                        }
                        break;
                }
                interactionTick = TickCount;
            }
        }

        public void CheckPlayerCurrentTypes()
        {
            //if (gui.inputChat.HasFocus == true) { return; }
            if (Attacking == true) { return; }
            if (inShop || inChat || inBank) { return; }


        }
        #endregion

        public void SendMeleeAttack(int npcNum, int type)
        {
            NetOutgoingMessage outMSG = MonoClient.netClient.CreateMessage();
            outMSG.Write((byte)PacketTypes.MeleeAttack);
            outMSG.WriteVariableInt32(npcNum);
            outMSG.WriteVariableInt32(HandleData.myIndex);
            outMSG.WriteVariableInt32(type);
            MonoClient.netClient.SendMessage(outMSG, MonoClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
        }

        public void CreateBulletSound()
        {
            //SoundBuffer soundBuffer = new SoundBuffer("Resources/Sounds/M4A1Shot.wav");
            //gunShot = new Sound(soundBuffer);
            //gunShot.Play();
        }

        public void CreateReloadSound()
        {
            //SoundBuffer soundBuffer = new SoundBuffer("Resources/Sounds/M4A1Reload.wav");
            //gunReload = new Sound(soundBuffer);
            //gunReload.Play();
        }

        public void RemoveBulletFromClip()
        {
            if (mainWeapon.Clip > 0)
            {
                SendCreateBullet();
                mainWeapon.Clip -= 1;
                SendUpdateClip();
            }
            else
            {
                CreateReloadSound();
                Reload();
                reloadTick = TickCount;
                SendUpdateClip();
                SendUpdateAmmo();
            }
        }

        public void Reload()
        {
            switch (mainWeapon.ItemAmmoType)
            {
                case (int)AmmoType.Pistol:
                    if (PistolAmmo == 0) { break; }
                    if (PistolAmmo > mainWeapon.MaxClip)
                    {
                        if (mainWeapon.Clip > 0)
                        {
                            int leftOver = mainWeapon.MaxClip - mainWeapon.Clip;
                            mainWeapon.Clip = mainWeapon.MaxClip;
                            PistolAmmo -= leftOver;
                        }
                        else
                        {
                            mainWeapon.Clip = mainWeapon.MaxClip;
                            PistolAmmo -= mainWeapon.MaxClip;
                        }
                    }
                    else
                    {
                        mainWeapon.Clip = PistolAmmo;
                        PistolAmmo = 0;
                    }
                    break;

                case (int)AmmoType.AssaultRifle:
                    if (AssaultAmmo == 0) { break; }

                    if (AssaultAmmo > mainWeapon.MaxClip)
                    {
                        if (mainWeapon.Clip > 0)
                        {
                            int leftOver = mainWeapon.MaxClip - mainWeapon.Clip;
                            mainWeapon.Clip = mainWeapon.MaxClip;
                            AssaultAmmo -= leftOver;                         
                        }
                        else
                        {
                            mainWeapon.Clip = mainWeapon.MaxClip;
                            AssaultAmmo -= mainWeapon.MaxClip;
                        }
                    }
                    else
                    {
                        mainWeapon.Clip = AssaultAmmo;
                        AssaultAmmo = 0;
                    }
                    break;

                case (int)AmmoType.Rocket:
                    if (RocketAmmo == 0) { break; }
                    if (RocketAmmo > mainWeapon.MaxClip)
                    {
                        if (mainWeapon.Clip > 0)
                        {
                            int leftOver = mainWeapon.MaxClip - mainWeapon.Clip;
                            mainWeapon.Clip = mainWeapon.MaxClip;
                            RocketAmmo -= leftOver;
                        }
                        else
                        {
                            mainWeapon.Clip = mainWeapon.MaxClip;
                            RocketAmmo -= mainWeapon.MaxClip;
                        }
                    }
                    else
                    {
                        mainWeapon.Clip = RocketAmmo;
                        RocketAmmo = 0;
                    }
                    break;
                case (int)AmmoType.Grenade:
                    if (GrenadeAmmo == 0) { break; }
                    if (GrenadeAmmo > mainWeapon.MaxClip)
                    {
                        if (mainWeapon.Clip > 0)
                        {
                            int leftOver = mainWeapon.MaxClip - mainWeapon.Clip;
                            mainWeapon.Clip = mainWeapon.MaxClip;
                            GrenadeAmmo -= leftOver;
                        }
                        else
                        {
                            mainWeapon.Clip = mainWeapon.MaxClip;
                            GrenadeAmmo -= mainWeapon.MaxClip;
                        }
                    }
                    else
                    {
                        mainWeapon.Clip = GrenadeAmmo;
                        GrenadeAmmo = 0;
                    }
                    break;
            }            
        }

        public void SendUpdateClip()
        {
            NetOutgoingMessage outMSG = MonoClient.netClient.CreateMessage();
            outMSG.Write((byte)PacketTypes.UpdateClip);
            outMSG.WriteVariableInt32(HandleData.myIndex);
            outMSG.WriteVariableInt32(mainWeapon.Clip);
            outMSG.WriteVariableInt32(offWeapon.Clip);
            MonoClient.netClient.SendMessage(outMSG, MonoClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
        }

        void SendUpdateAmmo()
        {
            NetOutgoingMessage outMSG = MonoClient.netClient.CreateMessage();
            outMSG.Write((byte)PacketTypes.UpdateAmmo);
            outMSG.WriteVariableInt32(HandleData.myIndex);
            outMSG.WriteVariableInt32(PistolAmmo);
            outMSG.WriteVariableInt32(AssaultAmmo);
            outMSG.WriteVariableInt32(RocketAmmo);
            outMSG.WriteVariableInt32(GrenadeAmmo);
            MonoClient.netClient.SendMessage(outMSG, MonoClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
        }

        void SendCreateBullet()
        {
            NetOutgoingMessage outMSG = MonoClient.netClient.CreateMessage();
            outMSG.Write((byte)PacketTypes.RangedAttack);
            outMSG.WriteVariableInt32(HandleData.myIndex);
            MonoClient.netClient.SendMessage(outMSG, MonoClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
        }

        void SendPlayerWarp()
        {
            isChangingMaps = true;            
            gui.CreateLoadingWindow(canvas);
            gui.Ready = false;
            NetOutgoingMessage outMSG = MonoClient.netClient.CreateMessage();
            outMSG.Write((byte)PacketTypes.PlayerWarp);
            outMSG.WriteVariableInt32(HandleData.myIndex);
            MonoClient.netClient.SendMessage(outMSG, MonoClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
        }

        void SendMovementData()
        {
            NetOutgoingMessage outMSG = MonoClient.netClient.CreateMessage();
            outMSG.Write((byte)PacketTypes.MoveData);
            outMSG.WriteVariableInt32(HandleData.myIndex);
            outMSG.WriteVariableInt32(X);
            outMSG.WriteVariableInt32(Y);
            outMSG.WriteVariableInt32(Direction);
            outMSG.WriteVariableInt32(AimDirection);
            outMSG.WriteVariableInt32(Step);
            MonoClient.netClient.SendMessage(outMSG, MonoClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
        }

        void SendUpdateDirection()
        {
            NetOutgoingMessage outMSG = MonoClient.netClient.CreateMessage();
            outMSG.Write((byte)PacketTypes.UpdateDirection);
            outMSG.WriteVariableInt32(HandleData.myIndex);
            outMSG.WriteVariableInt32(Direction);
            outMSG.WriteVariableInt32(AimDirection);
            MonoClient.netClient.SendMessage(outMSG, MonoClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
        }

        void SendPickupItem()
        {
            NetOutgoingMessage outMSG = MonoClient.netClient.CreateMessage();
            outMSG.Write((byte)PacketTypes.ItemPickup);
            outMSG.WriteVariableInt32(HandleData.myIndex);
            MonoClient.netClient.SendMessage(outMSG, MonoClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
        }

        void SendInteraction(int interactionValue, int interactionType)
        {
            NetOutgoingMessage outMSG = MonoClient.netClient.CreateMessage();
            outMSG.Write((byte)PacketTypes.Interaction);
            outMSG.WriteVariableInt32(interactionType);
            outMSG.WriteVariableInt32(HandleData.myIndex);
            outMSG.WriteVariableInt32(Map);
            outMSG.WriteVariableInt32(interactionValue);
            MonoClient.netClient.SendMessage(outMSG, MonoClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendUpdatePlayerTime()
        {
            NetOutgoingMessage outMSG = MonoClient.netClient.CreateMessage();
            outMSG.Write((byte)PacketTypes.PlayTime);
            outMSG.WriteVariableInt32(HandleData.myIndex);
            outMSG.WriteVariableInt32(PlayDays);
            outMSG.WriteVariableInt32(PlayHours);
            outMSG.WriteVariableInt32(PlayMinutes);
            outMSG.WriteVariableInt32(PlaySeconds);
            MonoClient.netClient.SendMessage(outMSG, MonoClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendUpdateLifeTime()
        {
            NetOutgoingMessage outMSG = MonoClient.netClient.CreateMessage();
            outMSG.Write((byte)PacketTypes.LifeTime);
            outMSG.WriteVariableInt32(HandleData.myIndex);
            outMSG.WriteVariableInt32(LifeDay);
            outMSG.WriteVariableInt32(LifeHour);
            outMSG.WriteVariableInt32(LifeMinute);
            outMSG.WriteVariableInt32(LifeSecond);
            MonoClient.netClient.SendMessage(outMSG, MonoClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
        }

        public void UpdatePlayerTime()
        {
            if (TickCount - timeTick >= 1000)
            {
                if (PlaySeconds < 59)
                {
                    PlaySeconds += 1;
                }
                else
                {
                    PlaySeconds = 0;
                    PlayMinutes += 1;
                }

                if (PlayMinutes >= 60)
                {
                    PlayMinutes = 0;
                    PlayHours += 1;
                }

                if (PlayHours == 24)
                {
                    PlayHours = 0;
                    PlayDays += 1;
                }
                timeTick = TickCount;
            }
        }

        public void UpdateLifeTime()
        {
            if (TickCount - lifeTick >= 1000)
            {
                if (LifeSecond < 59)
                {
                    LifeSecond += 1;
                }
                else
                {
                    LifeSecond = 0;
                    LifeMinute += 1;
                }
                if (LifeMinute >= 60)
                {
                    LifeMinute = 0;
                    LifeHour += 1;
                }
                if (LifeHour == 24)
                {
                    LifeHour = 0;
                    LifeDay += 1;
                }
                lifeTick = TickCount;
            }
        }

        public int ArmorBonus(bool isBonus)
        {
            int bonus = 0;

            if (!isBonus)
            {
                bonus = Armor;
            }

            bonus += mainWeapon.Armor;
            bonus += offWeapon.Armor;
            bonus += Chest.Armor;
            bonus += Legs.Armor;
            bonus += Feet.Armor;

            return bonus;
        }

        public int StrengthBonus(bool isBonus)
        {
            int bonus = 0;

            if (!isBonus)
            {
                bonus = Strength;
            }

            bonus += mainWeapon.Strength;
            bonus += offWeapon.Strength;
            bonus += Chest.Strength;
            bonus += Legs.Strength;
            bonus += Feet.Strength;

            return bonus;
        }

        public int AgilityBonus(bool isBonus)
        {
            int bonus = 0;

            if (!isBonus)
            {
                bonus = Agility;
            }

            bonus += mainWeapon.Agility;
            bonus += offWeapon.Agility;
            bonus += Chest.Agility;
            bonus += Legs.Agility;
            bonus += Feet.Agility;

            return bonus;
        }

        public int EnduranceBonus(bool isBonus)
        {
            int bonus = 0;

            if (!isBonus)
            {
                bonus = Endurance;
            }

            bonus += mainWeapon.Endurance;
            bonus += offWeapon.Endurance;
            bonus += Chest.Endurance;
            bonus += Legs.Endurance;
            bonus += Feet.Endurance;

            return bonus;
        }

        public int StaminaBonus(bool isBonus)
        {
            int bonus = 0;

            if (!isBonus)
            {
                bonus = Stamina;
            }

            bonus += mainWeapon.Stamina;
            bonus += offWeapon.Stamina;
            bonus += Chest.Stamina;
            bonus += Legs.Stamina;
            bonus += Feet.Stamina;

            return bonus;
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

        public int GetPlayerQuestSlot(int questNum)
        {
            for (int i = 0; i < MAX_PLAYER_QUEST_LIST; i++)
            {
                if (QuestList[i] == questNum)
                {
                    return i;
                }
            }
            return MAX_PLAYER_QUEST_LIST;
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
