﻿using Lidgren.Network;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using Gwen.Control;
using static System.Environment;

namespace Client.Classes
{
    class Player
    {
        #region Main Classes
        public NetConnection Connection;
        RenderText c_Text = new RenderText();
        public Item mainWeapon = new Item();
        public Item offWeapon = new Item();
        public Item[] Backpack = new Item[25];
        public Item Chest = new Item();
        public Item Legs = new Item();
        public Item Feet = new Item();
        Sprite c_Sprite = new Sprite();
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
        public int PistolAmmo { get; set; }
        public int AssaultAmmo { get; set; }
        public int RocketAmmo { get; set; }
        public int GrenadeAmmo { get; set; }
        #endregion

        #region Local Variables
        public bool Moved;
        public bool Attacking;
        public int reloadTick;
        public int attackTick;
        public int offsetX;
        public int offsetY;
        public int tempX;
        public int tempY;
        public int tempDir;
        public int tempaimDir;
        public int tempStep;
        public int Step;
        public int PickupTick;
        public int equipTick;
        #endregion

        #region Class Constructors
        public Player(string name, string pass, int x, int y, int direction, int aimdirection, int map, int level, int points, int health, int exp, int money, int armor, int hunger, 
                      int hydration, int str, int agi, int end, int sta, int defaultAmmo, NetConnection conn)
        {
            Name = name;
            Pass = pass;
            X = x;
            Y = y;
            Map = map;
            offsetX = 12;
            offsetY = 9;
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
        }

        public Player(string name, string pass, NetConnection conn)
        {
            Name = name;
            Pass = pass;
            Connection = conn;
            offsetX = 12;
            offsetY = 9;
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
                Backpack[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            }
        }
        #endregion

        #region Voids
        public void DrawPlayer(RenderWindow c_Window, Texture c_Texture)
        {
            c_Sprite.Texture = c_Texture;
            c_Sprite.TextureRect = new IntRect((Step * 32), (AimDirection * 48), 32, 48);
            c_Sprite.Position = new Vector2f(((X * 32) + (offsetX * 32)), (((Y * 32) + (offsetY * 32) - 16)));

            c_Window.Draw(c_Sprite);
        }

        public void DrawPlayerName(RenderWindow c_Window)
        {
            c_Text.DrawText(c_Window, Name, new Vector2f((X * 32) + ((offsetX * 32) - Name.Length / 2), (Y * 32) + (offsetY * 32) - 32), 12, Color.White);
        }

        public void CheckMovement(NetClient c_Client, int index, RenderWindow c_Window, Map movementMap, GUI c_GUI)
        {
            if (Moved == true) { Moved = false; return; }
            if (c_GUI.inputChat.HasFocus == true) { return; }
            if (!c_Window.HasFocus()) { return; }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                if (Y > 1 - offsetY)
                {
                    if (movementMap.Ground[(X + offsetX), (Y + offsetY) - 1].type == (int)TileType.Blocked)
                    {
                        Direction = (int)Directions.Up;
                        Moved = false;
                        SendUpdateDirection(c_Client, index);
                        return;
                    }
                    Y -= 1;
                    Direction = (int)Directions.Up; 
                    Moved = true; 
                }
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                if (Y < 49 - offsetY)  
                {
                    if (movementMap.Ground[(X + offsetX), (Y + offsetY) + 1].type == (int)TileType.Blocked)
                    {
                        Direction = (int)Directions.Down;
                        Moved = false; 
                        SendUpdateDirection(c_Client, index);
                        return; 
                    }
                    Y += 1;
                    Direction = (int)Directions.Down;
                    Moved = true;
                }
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A)) 
            {
                if (X > 1 - offsetX)
                {
                    if (movementMap.Ground[(X + offsetX) - 1, (Y + offsetY)].type == (int)TileType.Blocked)
                    {
                        Direction = (int)Directions.Left;
                        Moved = false; 
                        SendUpdateDirection(c_Client, index);
                        return;
                    }
                    X -= 1;
                    Direction = (int)Directions.Left;
                    Moved = true;
                }
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                if (X < 49 - offsetX) 
                {
                    if (movementMap.Ground[(X + offsetX) + 1, (Y + offsetY)].type == (int)TileType.Blocked) 
                    {
                        Direction = (int)Directions.Right;
                        Moved = false;
                        SendUpdateDirection(c_Client, index);
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
                SendMovementData(c_Client, index);
            }
        }

        public void CheckItemPickUp(NetClient c_Client, GUI c_GUI, RenderWindow c_Window, int index)
        {
            if (c_GUI.inputChat.HasFocus == true) { return; }
            if (!c_Window.HasFocus()) { return; }
            if (Attacking == true) { return; }

            if (Keyboard.IsKeyPressed(Keyboard.Key.O))
            {
                SendPickupItem(c_Client, index);
                PickupTick = TickCount;
            }
        }
                
        public void CheckChangeDirection(NetClient c_Client, GUI c_GUI, RenderWindow c_Window, int index)
        {
            if (c_GUI.inputChat.HasFocus == true) { return; }
            if (!c_Window.HasFocus()) { return; }

            if (Keyboard.IsKeyPressed(Keyboard.Key.I))
            {
                AimDirection = (int)Directions.Up;
                SendUpdateDirection(c_Client, index);
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.K))
            {
                AimDirection = (int)Directions.Down;
                SendUpdateDirection(c_Client, index);
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.J))
            {
                AimDirection = (int)Directions.Left;
                SendUpdateDirection(c_Client, index);
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.L))
            {
                AimDirection = (int)Directions.Right;
                SendUpdateDirection(c_Client, index);
            }
        }

        public void CheckReload(NetClient c_Client, int index)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.R))
            {
                if (mainWeapon.Clip == mainWeapon.maxClip) { return; }
                Reload();
                reloadTick = TickCount;
                SendUpdateClip(c_Client, index);
                SendUpdateAmmo(c_Client, index);
            }
        }

        public void CheckAttack(NetClient c_Client, GUI c_GUI, RenderWindow c_Window, int index)
        {
            if (c_GUI.inputChat.HasFocus == true) { return; }
            if (!c_Window.HasFocus()) { return; }
            if (Attacking == true) { return; }
            if (TickCount - reloadTick < mainWeapon.ReloadSpeed) { return; }
            if (TickCount - equipTick < 5000) { return; }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                if (mainWeapon.Name != "None")
                {
                    Attacking = true;
                }
                else
                {
                    c_GUI.AddText("You need a weapon equiped to attack!");
                    equipTick = TickCount;
                    return;
                }
            }

            if (Attacking == true)
            {             
                switch (mainWeapon.ammoType)
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
                SendCreateBullet(c_Client, index);
                RemoveBulletFromClip(c_Client, index);
                Attacking = false;
                attackTick = TickCount;
            }
        }

        public void RemoveBulletFromClip(NetClient c_Client, int index)
        {
            if (mainWeapon.Clip > 0)
            {
                mainWeapon.Clip -= 1;
                SendUpdateClip(c_Client, index);
            }

            if (mainWeapon.Clip == 0)
            {
                Reload();
                reloadTick = TickCount;
                SendUpdateClip(c_Client, index);
                SendUpdateAmmo(c_Client, index);
            }
        }

        public void Reload()
        {
            switch (mainWeapon.ammoType)
            {
                case (int)AmmoType.Pistol:
                    if (PistolAmmo > mainWeapon.maxClip)
                    {
                        if (mainWeapon.Clip > 0)
                        {
                            int leftOver = mainWeapon.maxClip - mainWeapon.Clip;
                            mainWeapon.Clip = mainWeapon.maxClip;
                            PistolAmmo -= leftOver;
                        }
                        else
                        {
                            mainWeapon.Clip = mainWeapon.maxClip;
                            PistolAmmo -= mainWeapon.maxClip;
                        }
                    }
                    else
                    {
                        mainWeapon.Clip = PistolAmmo;
                        PistolAmmo = 0;
                    }
                    break;
                case (int)AmmoType.AssaultRifle:
                    if (AssaultAmmo > mainWeapon.maxClip)
                    {
                        if (mainWeapon.Clip > 0)
                        {
                            int leftOver = mainWeapon.maxClip - mainWeapon.Clip;
                            mainWeapon.Clip = mainWeapon.maxClip;
                            AssaultAmmo -= leftOver;
                        }
                        else
                        {
                            mainWeapon.Clip = mainWeapon.maxClip;
                            AssaultAmmo -= mainWeapon.maxClip;
                        }
                    }
                    else
                    {
                        mainWeapon.Clip = AssaultAmmo;
                        AssaultAmmo = 0;
                    }
                    break;
                case (int)AmmoType.Rocket:
                    if (RocketAmmo > mainWeapon.maxClip)
                    {
                        if (mainWeapon.Clip > 0)
                        {
                            int leftOver = mainWeapon.maxClip - mainWeapon.Clip;
                            mainWeapon.Clip = mainWeapon.maxClip;
                            RocketAmmo -= leftOver;
                        }
                        else
                        {
                            mainWeapon.Clip = mainWeapon.maxClip;
                            RocketAmmo -= mainWeapon.maxClip;
                        }
                    }
                    else
                    {
                        mainWeapon.Clip = RocketAmmo;
                        RocketAmmo = 0;
                    }
                    break;
                case (int)AmmoType.Grenade:
                    if (GrenadeAmmo > mainWeapon.maxClip)
                    {
                        if (mainWeapon.Clip > 0)
                        {
                            int leftOver = mainWeapon.maxClip - mainWeapon.Clip;
                            mainWeapon.Clip = mainWeapon.maxClip;
                            GrenadeAmmo -= leftOver;
                        }
                        else
                        {
                            mainWeapon.Clip = mainWeapon.maxClip;
                            GrenadeAmmo -= mainWeapon.maxClip;
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

        public void SendUpdateClip(NetClient c_Client, int index)
        {
            NetOutgoingMessage outMSG = c_Client.CreateMessage();
            outMSG.Write((byte)PacketTypes.UpdateClip);
            outMSG.WriteVariableInt32(index);
            outMSG.WriteVariableInt32(mainWeapon.Clip);
            outMSG.WriteVariableInt32(offWeapon.Clip);
            c_Client.SendMessage(outMSG, c_Client.ServerConnection, NetDeliveryMethod.ReliableOrdered);
        }

        void SendUpdateAmmo(NetClient c_Client, int index)
        {
            NetOutgoingMessage outMSG = c_Client.CreateMessage();
            outMSG.Write((byte)PacketTypes.UpdateAmmo);
            outMSG.WriteVariableInt32(index);
            outMSG.WriteVariableInt32(PistolAmmo);
            outMSG.WriteVariableInt32(AssaultAmmo);
            outMSG.WriteVariableInt32(RocketAmmo);
            outMSG.WriteVariableInt32(GrenadeAmmo);
            c_Client.SendMessage(outMSG, c_Client.ServerConnection, NetDeliveryMethod.ReliableOrdered);
        }

        void SendCreateBullet(NetClient c_Client, int index)
        {
            NetOutgoingMessage outMSG = c_Client.CreateMessage();
            outMSG.Write((byte)PacketTypes.RangedAttack);
            outMSG.WriteVariableInt32(index);
            outMSG.WriteVariableInt32(Direction);
            outMSG.WriteVariableInt32(AimDirection);
            c_Client.SendMessage(outMSG, c_Client.ServerConnection, NetDeliveryMethod.ReliableOrdered);
        }

        void SendMovementData(NetClient c_Client, int index)
        {
            NetOutgoingMessage outMSG = c_Client.CreateMessage();
            outMSG.Write((byte)PacketTypes.MoveData);
            outMSG.WriteVariableInt32(index);
            outMSG.WriteVariableInt32(X);
            outMSG.WriteVariableInt32(Y);
            outMSG.WriteVariableInt32(Direction);
            outMSG.WriteVariableInt32(AimDirection);
            outMSG.WriteVariableInt32(Step);
            c_Client.SendMessage(outMSG, c_Client.ServerConnection, NetDeliveryMethod.ReliableOrdered);
        }

        void SendUpdateDirection(NetClient c_Client, int index)
        {
            NetOutgoingMessage outMSG = c_Client.CreateMessage();
            outMSG.Write((byte)PacketTypes.UpdateDirection);
            outMSG.WriteVariableInt32(index);
            outMSG.WriteVariableInt32(Direction);
            outMSG.WriteVariableInt32(AimDirection);
            c_Client.SendMessage(outMSG, c_Client.ServerConnection, NetDeliveryMethod.ReliableOrdered);
        }

        void SendPickupItem(NetClient c_Client, int index)
        {
            NetOutgoingMessage outMSG = c_Client.CreateMessage();
            outMSG.Write((byte)PacketTypes.ItemPickup);
            outMSG.WriteVariableInt32(index);
            c_Client.SendMessage(outMSG, c_Client.ServerConnection, NetDeliveryMethod.ReliableOrdered);
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
