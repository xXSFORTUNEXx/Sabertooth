using Lidgren.Network;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using Gwen.Control;
using SFML.Audio;
using static System.Environment;
using System;
using static SabertoothClient.Client;
using AccountKeyGenClass;
using static SabertoothClient.Globals;
using System.IO;

namespace SabertoothClient
{
    public class Player : Drawable
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
        RenderText rText = new RenderText();
        static int spriteTextures = Directory.GetFiles("Resources/Characters/", "*", SearchOption.TopDirectoryOnly).Length;
        VertexArray spritePic = new VertexArray(PrimitiveType.Quads, 4);
        Texture[] c_Sprite = new Texture[spriteTextures];
        Font font = new Font("Resources/Fonts/Arial.ttf");
        Text p_Name = new Text();        
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
        public int LightRadius { get; set; }
        public int PlayDays { get; set; }
        public int PlayHours { get; set; }
        public int PlayMinutes { get; set; }
        public int PlaySeconds { get; set; }
        public string LastLoggedIn { get; set; }
        public string AccountKey { get; set; }
        public string Active { get; set; }
        public int OffsetX { get; set; }
        public int OffsetY { get; set; }
        #endregion

        #region Local Variables
        bool Moved;
        bool Attacking;
        int attackTick;
        int timeTick;
        int pickupTick;
        int equipTick;
        int interactionTick;
        int hotbarTick;
        int directionTick;
        int walkTick;
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
        int oldDirection = -1;
        #endregion

        #region Class Constructors
        public Player(string name, string pass, string email, int x, int y, int direction, int aimdirection, int map, int level, int points, int health, int maxhealth, int mana, int maxmana,
            int exp, int wallet, int armor, int str, int agi, int inte, int sta, int eng, NetConnection conn)
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
            MaxHealth = maxhealth;
            Mana = mana;
            MaxMana = maxmana;
            Experience = exp;
            Wallet = wallet;
            Armor = armor;
            Strength = str;
            Agility = agi;
            Intelligence = inte;
            Stamina = sta;
            Energy = eng;
            Connection = conn;
            LightRadius = 100;
            PlayDays = 0;
            PlayHours = 0;
            PlayMinutes = 0;
            PlaySeconds = 0;
            LastLoggedIn = "00:00:00.000";
            AccountKey = KeyGen.Key(25);
            Active = "N";

            for (int i = 0; i < 25; i++)
            {
                Backpack[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, false, 1);
            }

            for (int i = 0; i < 50; i++)
            {
                Bank[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, false, 1);
            }

            for (int i = 0; i < spriteTextures; i++)
            {
                c_Sprite[i] = new Texture("Resources/Characters/" + (i + 1) + ".png");
            }

            int h = 0;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0);

            p_Name.Font = font;
            p_Name.CharacterSize = 12;
            p_Name.Color = Color.White;
            p_Name.Style = Text.Styles.Bold;
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

            for (int i = 0; i < 25; i++)
            {
                Backpack[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, false, 1);
            }

            for (int i = 0; i < 50; i++)
            {
                Bank[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, false, 1);
            }

            for (int i = 0; i < spriteTextures; i++)
            {
                c_Sprite[i] = new Texture("Resources/Characters/" + (i + 1) + ".png");
            }

            int h = 0;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0);


            p_Name.Font = font;
            p_Name.CharacterSize = 12;
            p_Name.Color = Color.White;
            p_Name.Style = Text.Styles.Bold;
        }

        public Player(string name)
        {
            Name = name;

            for (int i = 0; i < 25; i++)
            {
                Backpack[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, false, 1);
            }

            for (int i = 0; i < 50; i++)
            {
                Bank[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, false, 1);
            }

            for (int i = 0; i < spriteTextures; i++)
            {
                c_Sprite[i] = new Texture("Resources/Characters/" + (i + 1) + ".png");
            }

            int h = 0;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0);

            p_Name.Font = font;
            p_Name.CharacterSize = 12;
            p_Name.Color = Color.White;
            p_Name.Style = Text.Styles.Bold;
        }

        public Player(NetConnection conn)
        {
            Connection = conn;

            for (int i = 0; i < 25; i++)
            {
                Backpack[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, false, 1);
            }

            for (int i = 0; i < 50; i++)
            {
                Bank[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, false, 1);
            }

            for (int i = 0; i < spriteTextures; i++)
            {
                c_Sprite[i] = new Texture("Resources/Characters/" + (i + 1) + ".png");
            }

            int h = 0;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0);

            p_Name.Font = font;
            p_Name.CharacterSize = 12;
            p_Name.Color = Color.White;
            p_Name.Style = Text.Styles.Bold;
        }

        public Player()
        {
            for (int i = 0; i < 25; i++)
            {
                Backpack[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, false, 1);
            }

            for (int i = 0; i < 50; i++)
            {
                Bank[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, false, 1);
            }

            for (int i = 0; i < spriteTextures; i++)
            {
                c_Sprite[i] = new Texture("Resources/Characters/" + (i + 1) + ".png");
            }

            int h = 0;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0); h += 1;
            hotBar[h] = new HotBar(Keyboard.Key.Num0, 0, 0);

            p_Name.Font = font;
            p_Name.CharacterSize = 12;
            p_Name.Color = Color.White;
            p_Name.Style = Text.Styles.Bold;
        }
        #endregion

        #region Controller
        public void CheckControllerMovement()
        {
            /*
            * Button ID's Xbox Controller
            * 0 - A
            * 1 - B
            * 2 - X
            * 3 - Y
            * 4 - LB
            * 5 - RB
            * 6 - Back
            * 7 - Start
            * 8 - Left JoyStick Button
            * 9 - Right JoyStick Button
            * U Axis - Right Stick
            * R Axix - Right Stick
            * Z Axis - Right Trigger
            * X Axis - Left Stick
            * Y Axis - Left Stick
            * D Pad - PovX, PovY
            */
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
            if (TickCount - equipTick < 5000) { return; }
            if (inShop || inChat || inBank) { return; }

            if (Joystick.GetAxisPosition(0, Joystick.Axis.Z) < -25)
            {
                if (MainHand.Name != "None")
                {
                    Attacking = true;
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
                if (OffHand.Name != "None")
                {
                    Attacking = true;
                }
                else
                {
                    gui.AddText("You need a melee weapon equiped to attack!");
                    equipTick = TickCount;
                }
            }

            if (Attacking == true)
            {             
                {
                    if (TickCount - attackTick < OffHand.AttackSpeed) { Attacking = false; return; }
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
        }
        #endregion

        #region Keyboard
        public void CheckMovement()
        {
            if (TickCount - walkTick < 100) { return; }
            if (Moved == true) { Moved = false; return; }
            if (gui.inputChat.HasFocus == true) { return; }
            if (!renderWindow.HasFocus()) { return; }
            if (inShop || inChat || inBank) { return; }
            if (isChangingMaps) { return; }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
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

            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
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

            if (Keyboard.IsKeyPressed(Keyboard.Key.A)) 
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

            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
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
                walkTick = TickCount;
            }

            if (map.Ground[(X + OffsetX), (Y + OffsetY)].Type == (int)TileType.Warp)
            {
                SendPlayerWarp();
            }
        }
        
        public void CheckChangeDirection()
        {
            if (TickCount - directionTick < 500) { return; }
            if (gui.inputChat.HasFocus == true) { return; }
            if (!renderWindow.HasFocus()) { return; }
            if (inShop || inChat || inBank) { return; }

            if (Keyboard.IsKeyPressed(Keyboard.Key.I))
            {
                AimDirection = (int)Directions.Up;
                SendUpdateDirection();
                directionTick = TickCount;
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.K))
            {
                AimDirection = (int)Directions.Down;
                SendUpdateDirection();
                directionTick = TickCount;
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.J))
            {
                AimDirection = (int)Directions.Left;
                SendUpdateDirection();
                directionTick = TickCount;
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.L))
            {
                AimDirection = (int)Directions.Right;
                SendUpdateDirection();
                directionTick = TickCount;
            }
        }

        public void CheckAttack()
        {
            if (TickCount - equipTick < 5000) { return; }
            if (TickCount - attackTick < 25) { return; }
            if (gui.inputChat.HasFocus == true) { return; }
            if (!renderWindow.HasFocus()) { return; }
            if (Attacking == true) { return; }                        
            if (inShop || inChat || inBank) { return; }            

            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                if (MainHand.Name != "None")
                {
                    Attacking = true;
                }
                else
                {
                    gui.AddText("You need a weapon equiped to attack!");
                    equipTick = TickCount;
                    return;
                }
            }

            if (Attacking == true)
            {
                {
                    if (TickCount - attackTick < MainHand.AttackSpeed) { Attacking = false; return; }

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
                                                    Attacking = false;
                                                    attackTick = TickCount;
                                                    return;
                                                }
                                                /*else if (map.m_MapNpc[i].Y == (Y + OffsetY) && map.m_MapNpc[i].X == (X + OffsetX))
                                                {
                                                    SendMeleeAttack(i, 0);
                                                    return;
                                                }*/
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
                                                Attacking = false;
                                                attackTick = TickCount;
                                                return;
                                            }
                                            /*else if (map.r_MapNpc[i].Y == (Y + OffsetY) && map.r_MapNpc[i].X == (X + OffsetX))
                                            {
                                                SendMeleeAttack(i, 1);
                                                return;
                                            }*/
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
                                                    Attacking = false;
                                                    attackTick = TickCount;
                                                    return;
                                                }
                                                /*else if (map.m_MapNpc[i].Y == (Y + OffsetY) && map.m_MapNpc[i].X == (X + OffsetX))
                                                {
                                                    SendMeleeAttack(i, 0);
                                                }*/
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
                                                Attacking = false;
                                                attackTick = TickCount;
                                                return;
                                            }
                                            /*else if (map.r_MapNpc[i].Y == (Y + OffsetY) && map.r_MapNpc[i].X == (X + OffsetX))
                                            {
                                                SendMeleeAttack(i, 1);
                                            }*/
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
                                                    Attacking = false;
                                                    attackTick = TickCount;
                                                    return;
                                                }
                                                /*else if (map.m_MapNpc[i].Y == (Y + OffsetY) && map.m_MapNpc[i].X == (X + OffsetX))
                                                {
                                                    SendMeleeAttack(i, 0);
                                                }*/
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
                                                Attacking = false;
                                                attackTick = TickCount;
                                                return;
                                            }
                                            /*else if (map.r_MapNpc[i].Y == (Y + OffsetY) && map.r_MapNpc[i].X == (X + OffsetX))
                                            {
                                                SendMeleeAttack(i, 1);
                                            }*/
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
                                                    Attacking = false;
                                                    attackTick = TickCount;
                                                    return;
                                                }
                                                /*else if (map.m_MapNpc[i].Y == (Y + OffsetY) && map.m_MapNpc[i].X == (X + OffsetX))
                                                {
                                                    SendMeleeAttack(i, 0);
                                                }*/
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
                                                Attacking = false;
                                                attackTick = TickCount;
                                                return;
                                            }
                                            /*else if (map.r_MapNpc[i].Y == (Y + OffsetY) && map.r_MapNpc[i].X == (X + OffsetX))
                                            {
                                                SendMeleeAttack(i, 1);
                                            }*/
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

        public void CheckItemPickUp()
        {
            if (TickCount - pickupTick < 1000) { return; }
            if (gui.inputChat.HasFocus == true) { return; }
            if (!renderWindow.HasFocus()) { return; }
            if (Attacking == true) { return; }
            if (inShop || inChat || inBank) { return; }            

            if (Keyboard.IsKeyPressed(Keyboard.Key.F))
            {
                SendPickupItem();
                pickupTick = TickCount;
            }
        }

        public void CheckPlayerInteraction()
        {
            if (TickCount - interactionTick < 1000) { return; }
            if (gui.inputChat.HasFocus == true) { return; }
            if (!renderWindow.HasFocus()) { return; }
            if (Attacking == true) { return; }            
            if (inShop || inChat || inBank) { return; }

            if (Keyboard.IsKeyPressed(Keyboard.Key.E))
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

        public void CheckHotBarKeyPress()
        {
            if (TickCount - hotbarTick < 500) { return; }
            if (gui.inputChat.HasFocus == true) { return; }
            if (!renderWindow.HasFocus()) { return; }
            if (inShop || inChat || inBank) { return; }

            if (Keyboard.IsKeyPressed(hotBar[0].HotKey))
            {
                SendUseHotbar(0);
                hotbarTick = TickCount;
            }
            if (Keyboard.IsKeyPressed(hotBar[1].HotKey))
            {
                SendUseHotbar(1);
                hotbarTick = TickCount;
            }
            if (Keyboard.IsKeyPressed(hotBar[2].HotKey))
            {
                SendUseHotbar(2);
                hotbarTick = TickCount;
            }
            if (Keyboard.IsKeyPressed(hotBar[3].HotKey))
            {
                SendUseHotbar(3);
                hotbarTick = TickCount;
            }
            if (Keyboard.IsKeyPressed(hotBar[4].HotKey))
            {
                SendUseHotbar(4);
                hotbarTick = TickCount;
            }
            if (Keyboard.IsKeyPressed(hotBar[5].HotKey))
            {
                SendUseHotbar(5);
                hotbarTick = TickCount;
            }
            if (Keyboard.IsKeyPressed(hotBar[6].HotKey))
            {
                SendUseHotbar(6);
                hotbarTick = TickCount;
            }
            if (Keyboard.IsKeyPressed(hotBar[7].HotKey))
            {
                SendUseHotbar(7);
                hotbarTick = TickCount;
            }
            if (Keyboard.IsKeyPressed(hotBar[8].HotKey))
            {
                SendUseHotbar(8);
                hotbarTick = TickCount;
            }
            if (Keyboard.IsKeyPressed(hotBar[9].HotKey))
            {
                SendUseHotbar(9);
                hotbarTick = TickCount;
            }
        }

        public void CheckPlayerCurrentTypes()
        {
            if (gui.inputChat.HasFocus == true) { return; }
            if (!renderWindow.HasFocus()) { return; }
            if (Attacking == true) { return; }
            if (inShop || inChat || inBank) { return; }


        }
        #endregion

        #region Voids
        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            int x = (X * 32) + (OffsetX * 32);
            int y = (Y * 32) + (OffsetY * 32) - 16;
            int step = (Step * 32);
            int dir = (AimDirection * 48);
            int name_x = (X * 32) + (OffsetX * 32) - (Name.Length / 2);
            int name_y = (Y * 32) + (OffsetY * 32) - 32;
            p_Name.Position = new Vector2f(name_x, name_y);
            p_Name.DisplayedString = Name;
            spritePic[0] = new Vertex(new Vector2f(x, y), new Vector2f(step, dir));
            spritePic[1] = new Vertex(new Vector2f(x + 32, y), new Vector2f(step + 32, dir));
            spritePic[2] = new Vertex(new Vector2f(x + 32, y + 48), new Vector2f(step + 32, dir + 48));
            spritePic[3] = new Vertex(new Vector2f(x, y + 48), new Vector2f(step, dir + 48));
            states.Texture = c_Sprite[Sprite];
            target.Draw(spritePic, states);
            target.Draw(p_Name);
        }

        public void CheckDirection(int curx, int cury)
        {
            if (gui.inputChat.HasFocus == true) { return; }
            if (!renderWindow.HasFocus()) { return; }
            if (inShop || inChat || inBank) { return; }

            int currentY = (cury);
            int currentX = (curx);
            int region = 0;

            int x1 = 256;
            int x2 = 512;
            int x3 = 768;            
            int y1 = 192;
            int y2 = 384;
            int y3 = 576;            

            if (currentX < x1 && currentY < y1)
            {
                AimDirection = (int)Directions.Up;
                if (AimDirection != oldDirection) { SendUpdateDirection(); }
                oldDirection = AimDirection;
                region = 1;
            }

            if (currentX < x2 && currentX > x1 && currentY < y1)
            {
                AimDirection = (int)Directions.Up;
                if (AimDirection != oldDirection) { SendUpdateDirection(); }
                oldDirection = AimDirection;
                region = 2;
            }

            if (currentX > x2 && currentX < x3 && currentY < y1)
            {
                AimDirection = (int)Directions.Up;
                if (AimDirection != oldDirection) { SendUpdateDirection(); }
                oldDirection = AimDirection;
                region = 3;
            }

            if (currentX > x3 && currentY < y1)
            {
                AimDirection = (int)Directions.Up;
                if (AimDirection != oldDirection) { SendUpdateDirection(); }
                oldDirection = AimDirection;
                region = 4;
            }

            if (currentX < x1 && currentY > y1 && currentY < y2)
            {
                AimDirection = (int)Directions.Left;
                if (AimDirection != oldDirection) { SendUpdateDirection(); }
                oldDirection = AimDirection;
                region = 5;
            }

            if (currentX > x1 && currentX < x2 && currentY > y1 && currentY < y2)
            {
                AimDirection = (int)Directions.Left;
                if (AimDirection != oldDirection) { SendUpdateDirection(); }
                oldDirection = AimDirection;
                region = 6;
            }

            if (currentX > x2 && currentX < x3 && currentY > y1 && currentY < y2)
            {
                AimDirection = (int)Directions.Right;
                if (AimDirection != oldDirection) { SendUpdateDirection(); }
                oldDirection = AimDirection;
                region = 7;
            }

            if (currentX > x3 && currentY > y1 && currentY < y2)
            {
                AimDirection = (int)Directions.Right;
                if (AimDirection != oldDirection) { SendUpdateDirection(); }
                oldDirection = AimDirection;
                region = 8;
            }

            if (currentX < x1 && currentY > y2 && currentY < y3)
            {
                AimDirection = (int)Directions.Left;
                if (AimDirection != oldDirection) { SendUpdateDirection(); }
                oldDirection = AimDirection;
                region = 9;
            }

            if (currentX > x1 && currentX < x2 && currentY > y2 && currentY < y3)
            {
                AimDirection = (int)Directions.Left;
                if (AimDirection != oldDirection) { SendUpdateDirection(); }
                oldDirection = AimDirection;
                region = 10;
            }

            if (currentX > x2 && currentX < x3 && currentY > y2 && currentY < y3)
            {
                AimDirection = (int)Directions.Right;
                if (AimDirection != oldDirection) { SendUpdateDirection(); }
                oldDirection = AimDirection;
                region = 11;
            }

            if (currentX > x3 && currentY > y2 && currentY < y3)
            {
                AimDirection = (int)Directions.Right;
                if (AimDirection != oldDirection) { SendUpdateDirection(); }
                oldDirection = AimDirection;
                region = 12;
            }

            if (currentX < x1 && currentY > y3)
            {
                AimDirection = (int)Directions.Down;
                if (AimDirection != oldDirection) { SendUpdateDirection(); }
                oldDirection = AimDirection;
                region = 13;
            }

            if (currentX > x1 && currentX < x2 && currentY > y3)
            {
                AimDirection = (int)Directions.Down;
                if (AimDirection != oldDirection) { SendUpdateDirection(); }
                oldDirection = AimDirection;
                region = 14;
            }

            if  (currentX > x2 && currentX < x3 && currentY > y3)
            {
                AimDirection = (int)Directions.Down;
                if (AimDirection != oldDirection) { SendUpdateDirection(); }
                oldDirection = AimDirection;
                region = 15;
            }

            if (currentX > x3 && currentY > y3)
            {
                AimDirection = (int)Directions.Down;
                if (AimDirection != oldDirection) { SendUpdateDirection(); }
                oldDirection = AimDirection;
                region = 16;
            }

            gui.d_Region.Text = "Mouse Region: " + region;
        }

        void SendUseHotbar(int hotbarslot)
        {
            NetOutgoingMessage outMSG = SabertoothClient.netClient.CreateMessage();
            outMSG.Write((byte)PacketTypes.UseHotBar);
            outMSG.WriteVariableInt32(HandleData.myIndex);
            outMSG.WriteVariableInt32(hotbarslot);
            SabertoothClient.netClient.SendMessage(outMSG, SabertoothClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendMeleeAttack(int npcNum, int type)
        {
            NetOutgoingMessage outMSG = SabertoothClient.netClient.CreateMessage();
            outMSG.Write((byte)PacketTypes.MeleeAttack);
            outMSG.WriteVariableInt32(npcNum);
            outMSG.WriteVariableInt32(HandleData.myIndex);
            outMSG.WriteVariableInt32(type);
            SabertoothClient.netClient.SendMessage(outMSG, SabertoothClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
        }              

        void SendPlayerWarp()
        {
            isChangingMaps = true;            
            gui.CreateLoadingWindow(canvas);
            gui.Ready = false;
            NetOutgoingMessage outMSG = SabertoothClient.netClient.CreateMessage();
            outMSG.Write((byte)PacketTypes.PlayerWarp);
            outMSG.WriteVariableInt32(HandleData.myIndex);
            SabertoothClient.netClient.SendMessage(outMSG, SabertoothClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
        }

        void SendMovementData()
        {
            NetOutgoingMessage outMSG = SabertoothClient.netClient.CreateMessage();
            outMSG.Write((byte)PacketTypes.MoveData);
            outMSG.WriteVariableInt32(HandleData.myIndex);
            outMSG.WriteVariableInt32(X);
            outMSG.WriteVariableInt32(Y);
            outMSG.WriteVariableInt32(Direction);
            outMSG.WriteVariableInt32(AimDirection);
            outMSG.WriteVariableInt32(Step);
            SabertoothClient.netClient.SendMessage(outMSG, SabertoothClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
        }

        void SendUpdateDirection()
        {
            NetOutgoingMessage outMSG = SabertoothClient.netClient.CreateMessage();
            outMSG.Write((byte)PacketTypes.UpdateDirection);
            outMSG.WriteVariableInt32(HandleData.myIndex);
            outMSG.WriteVariableInt32(Direction);
            outMSG.WriteVariableInt32(AimDirection);
            SabertoothClient.netClient.SendMessage(outMSG, SabertoothClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
        }

        void SendPickupItem()
        {
            NetOutgoingMessage outMSG = SabertoothClient.netClient.CreateMessage();
            outMSG.Write((byte)PacketTypes.ItemPickup);
            outMSG.WriteVariableInt32(HandleData.myIndex);
            SabertoothClient.netClient.SendMessage(outMSG, SabertoothClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendSwapInvSlots(int oldslot, int newslot)
        {
            NetOutgoingMessage outMSG = SabertoothClient.netClient.CreateMessage();
            outMSG.Write((byte)PacketTypes.SendInvSwap);
            outMSG.WriteVariableInt32(HandleData.myIndex);
            outMSG.WriteVariableInt32(oldslot);
            outMSG.WriteVariableInt32(newslot);
            SabertoothClient.netClient.SendMessage(outMSG, SabertoothClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendSwapBankSlots(int oldslot, int newslot)
        {
            NetOutgoingMessage outMSG = SabertoothClient.netClient.CreateMessage();
            outMSG.Write((byte)PacketTypes.SendBankSwap);
            outMSG.WriteVariableInt32(HandleData.myIndex);
            outMSG.WriteVariableInt32(oldslot);
            outMSG.WriteVariableInt32(newslot);
            SabertoothClient.netClient.SendMessage(outMSG, SabertoothClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendUpdateHotbar(int slot, int hotbarslot)
        {
            NetOutgoingMessage outMSG = SabertoothClient.netClient.CreateMessage();
            outMSG.Write((byte)PacketTypes.UpdateHotBar);
            outMSG.WriteVariableInt32(HandleData.myIndex);
            outMSG.WriteVariableInt32(slot);
            outMSG.WriteVariableInt32(hotbarslot);
            SabertoothClient.netClient.SendMessage(outMSG, SabertoothClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
        }

        void SendInteraction(int interactionValue, int interactionType)
        {
            NetOutgoingMessage outMSG = SabertoothClient.netClient.CreateMessage();
            outMSG.Write((byte)PacketTypes.Interaction);
            outMSG.WriteVariableInt32(interactionType);
            outMSG.WriteVariableInt32(HandleData.myIndex);
            outMSG.WriteVariableInt32(Map);
            outMSG.WriteVariableInt32(interactionValue);
            SabertoothClient.netClient.SendMessage(outMSG, SabertoothClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendUpdatePlayerTime()
        {
            NetOutgoingMessage outMSG = SabertoothClient.netClient.CreateMessage();
            outMSG.Write((byte)PacketTypes.PlayTime);
            outMSG.WriteVariableInt32(HandleData.myIndex);
            outMSG.WriteVariableInt32(PlayDays);
            outMSG.WriteVariableInt32(PlayHours);
            outMSG.WriteVariableInt32(PlayMinutes);
            outMSG.WriteVariableInt32(PlaySeconds);
            SabertoothClient.netClient.SendMessage(outMSG, SabertoothClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
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

        public int ArmorBonus(bool isBonus)
        {
            int bonus = 0;

            if (!isBonus)
            {
                bonus = Armor;
            }

            bonus += MainHand.Armor;
            bonus += OffHand.Armor;
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

            bonus += MainHand.Strength;
            bonus += OffHand.Strength;
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

            bonus += MainHand.Agility;
            bonus += OffHand.Agility;
            bonus += Chest.Agility;
            bonus += Legs.Agility;
            bonus += Feet.Agility;

            return bonus;
        }

        public int IntelligenceBonus(bool isBonus)
        {
            int bonus = 0;

            if (!isBonus)
            {
                bonus = Intelligence;
            }

            bonus += MainHand.Intelligence;
            bonus += OffHand.Intelligence;
            bonus += Chest.Intelligence;
            bonus += Legs.Intelligence;
            bonus += Feet.Intelligence;

            return bonus;
        }

        public int StaminaBonus(bool isBonus)
        {
            int bonus = 0;

            if (!isBonus)
            {
                bonus = Stamina;
            }

            bonus += MainHand.Stamina;
            bonus += OffHand.Stamina;
            bonus += Chest.Stamina;
            bonus += Legs.Stamina;
            bonus += Feet.Stamina;

            return bonus;
        }

        public int EnergyBonus(bool isBonus)
        {
            int bonus = 0;

            if (!isBonus)
            {
                bonus = Energy;
            }

            bonus += MainHand.Energy;
            bonus += OffHand.Energy;
            bonus += Chest.Energy;
            bonus += Legs.Energy;
            bonus += Feet.Energy;

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
