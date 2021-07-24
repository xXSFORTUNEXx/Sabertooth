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
        public NetConnection Connection;
        public Item MainHand = new Item();
        public Item OffHand = new Item();
        public Item[] Backpack = new Item[MAX_INV_SLOTS];
        public Item[] Bank = new Item[MAX_BANK_SLOTS];
        public Item Chest = new Item();
        public Item Legs = new Item();
        public Item Feet = new Item();
        public HotBar[] hotBar = new HotBar[MAX_PLAYER_HOTBAR];
        public SpellBook[] spellBook = new SpellBook[MAX_PLAYER_SPELLBOOK];

        RenderText rText = new RenderText();
        VertexArray spritePic = new VertexArray(PrimitiveType.Quads, 4);
        static int spriteTextures = Directory.GetFiles("Resources/Characters/", "*", SearchOption.TopDirectoryOnly).Length;
        Texture[] c_Sprite = new Texture[spriteTextures];
        Font font = new Font("Resources/Fonts/Arial.ttf");
        Text p_Name = new Text();
        Texture targetTexture = new Texture("Resources/Target.png");
        VertexArray targetPic = new VertexArray(PrimitiveType.Quads, 4);
        public DisplayText[] displayText = new DisplayText[MAX_DISPLAY_TEXT];

        VertexArray castBar = new VertexArray(PrimitiveType.Quads, 4);
        float castbarLength;

        public int[] QuestList = new int[MAX_PLAYER_QUEST_LIST];
        public int[] QuestStatus = new int[MAX_PLAYER_QUEST_LIST];

        public string Name { get; set; }
        public string Pass { get; set; }
        public string EmailAddress { get; set; }
        public string LastLoggedIn { get; set; }
        public string AccountKey { get; set; }
        public string Active { get; set; }

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
        public int OffsetX { get; set; }
        public int OffsetY { get; set; }

        //Volatile Variables
        bool Moved;
        public bool Attacking;
        public bool CastSpell;
        public bool CastingSpell;
        public bool FailedCast;
        public int CurrentSpell = -1;
        public int SpellBookSlot = -1;
        public int castTick;
        public int gcdTick;
        public bool inChat;
        public bool inChest;
        public bool isChangingMaps;
        public bool inShop;
        public bool inBank;
        public int Target = -1;
        public int TargetType;
        double lastTarget;
        int attackTick;
        int timeTick;
        int hotbarTick;
        int targetTick;
        int directionTick;
        int walkTick;
        int oldDirection = -1;
        public int tempX;
        public int tempY;
        public int tempDir;
        public int tempaimDir;
        public int tempStep;
        public int Step;
        public int shopNum;
        public int chatNum;
        public int chestNum;

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

            for (int s = 0; s < MAX_PLAYER_SPELLBOOK; s++)
            {
                spellBook[s] = new SpellBook(-1, false);
            }

            p_Name.Font = font;
            p_Name.CharacterSize = 12;
            p_Name.Color = Color.White;
            p_Name.Style = Text.Styles.Bold;

            for (int i = 0; i < MAX_DISPLAY_TEXT; i++)
            {
                displayText[i] = new DisplayText();
            }
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

            for (int s = 0; s < MAX_PLAYER_SPELLBOOK; s++)
            {
                spellBook[s] = new SpellBook(-1, false);
            }

            p_Name.Font = font;
            p_Name.CharacterSize = 12;
            p_Name.Color = Color.White;
            p_Name.Style = Text.Styles.Bold;

            for (int i = 0; i < MAX_DISPLAY_TEXT; i++)
            {
                displayText[i] = new DisplayText();
            }
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

            for (int s = 0; s < MAX_PLAYER_SPELLBOOK; s++)
            {
                spellBook[s] = new SpellBook(-1, false);
            }

            p_Name.Font = font;
            p_Name.CharacterSize = 12;
            p_Name.Color = Color.White;
            p_Name.Style = Text.Styles.Bold;

            for (int i = 0; i < MAX_DISPLAY_TEXT; i++)
            {
                displayText[i] = new DisplayText();
            }
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

            for (int s = 0; s < MAX_PLAYER_SPELLBOOK; s++)
            {
                spellBook[s] = new SpellBook(-1, false);
            }

            p_Name.Font = font;
            p_Name.CharacterSize = 12;
            p_Name.Color = Color.White;
            p_Name.Style = Text.Styles.Bold;

            for (int i = 0; i < MAX_DISPLAY_TEXT; i++)
            {
                displayText[i] = new DisplayText();
            }
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

            for (int s = 0; s < MAX_PLAYER_SPELLBOOK; s++)
            {
                spellBook[s] = new SpellBook(-1, false);
            }

            p_Name.Font = font;
            p_Name.CharacterSize = 12;
            p_Name.Color = Color.White;
            p_Name.Style = Text.Styles.Bold;

            for (int i = 0; i < MAX_DISPLAY_TEXT; i++)
            {
                displayText[i] = new DisplayText();
            }
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
                FailedCast = true;
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

        public void MeleeCombatLoop(MapNpc mapNpc)
        {
            if (mapNpc == null) { return; }
            if (CastSpell || CastingSpell) { return; }
            if (MainHand.Name == "None") { Attacking = false; return; }    //no weapon

            if (Attacking)
            {
                if (TickCount - attackTick > MainHand.AttackSpeed)
                {
                    int pX = 0;
                    int pY = 0;
                    int nX = 0;
                    int nY = 0;
                    double dX = 0;
                    double dY = 0;
                    double dFinal = 0;
                    double dPoint = 0;

                    pX = X + OFFSET_X;
                    pY = Y + OFFSET_Y;
                    nX = mapNpc.X;
                    nY = mapNpc.Y;
                    dX = pX - nX;
                    dY = pY - nY;
                    dFinal = dX * dX + dY * dY;
                    dPoint = Math.Sqrt(dFinal);

                    if (dPoint < 3)
                    {
                        if (!CheckFacingTarget(mapNpc))
                        {
                            int openDT = HandleData.FindOpenPlayerDisplayText(HandleData.myIndex);
                            displayText[openDT].CreateDisplayText(0, nX, nY, (int)DisplayTextMsg.Warning, "FRD");
                            attackTick = TickCount;
                            return;
                        }                        
                        SendAttack(Target, 0, 0);
                        attackTick = TickCount;
                    }
                    else
                    {
                        int openDT = HandleData.FindOpenPlayerDisplayText(HandleData.myIndex);
                        displayText[openDT].CreateDisplayText(0, nX, nY, (int)DisplayTextMsg.Warning, "OOR");
                        attackTick = TickCount;
                    }
                }
            }
        }

        public void SpellCastLoop(Spell spell)
        {
            int disSlot;
            int pX = 0;
            int pY = 0;
            int nX = 0;
            int nY = 0;
            double dX = 0;
            double dY = 0;
            double dFinal = 0;
            double dPoint = 0;

            if (CastSpell)
            {                
                if (TickCount - gcdTick < GLOBAL_COOL_DOWN) { CastSpell = false; return; }                
                if (spellBook[SpellBookSlot].OnCoolDown) { gui.AddText("Spell on cooldown!"); CastSpell = false; return; }                
                if (Mana < spell.ManaCost) { gui.AddText("Not enough mana!"); CastSpell = false; return; }

                if (!CastingSpell)
                {
                    switch (spell.SpellType)
                    {
                        case (int)SpellType.Damage:
                            if (Target > -1)
                            {
                                pX = X + OFFSET_X;
                                pY = Y + OFFSET_Y;
                                nX = map.m_MapNpc[Target].X;
                                nY = map.m_MapNpc[Target].Y;
                                dX = pX - nX;
                                dY = pY - nY;
                                dFinal = dX * dX + dY * dY;
                                dPoint = Math.Sqrt(dFinal);

                                if (dPoint < spell.Range)
                                {
                                    if (!CheckFacingTarget(map.m_MapNpc[Target]))
                                    {
                                        disSlot = HandleData.FindOpenPlayerDisplayText(HandleData.myIndex);
                                        displayText[disSlot].CreateDisplayText(0, pX, pY, (int)DisplayTextMsg.Warning, "FRD");
                                        FailedCast = true;
                                    }
                                    else
                                    {
                                        CastSpell = false;
                                        CastingSpell = true;
                                        gcdTick = TickCount;
                                        castTick = TickCount;
                                    }
                                }
                                else
                                {
                                    disSlot = HandleData.FindOpenPlayerDisplayText(HandleData.myIndex);
                                    displayText[disSlot].CreateDisplayText(0, pX, pY, (int)DisplayTextMsg.Warning, "OOR");
                                    FailedCast = true;
                                }
                            }
                            else
                            {
                                disSlot = HandleData.FindOpenPlayerDisplayText(HandleData.myIndex);
                                displayText[disSlot].CreateDisplayText(0, pX, pY, (int)DisplayTextMsg.Warning, "NTS");
                                FailedCast = true;
                            }
                            break;

                        case (int)SpellType.Heal:
                            CastSpell = false;
                            CastingSpell = true;
                            gcdTick = TickCount;
                            castTick = TickCount;
                            break;

                        case (int)SpellType.Dash:
                            int newX = 0;
                            int newY = 0;
                            switch (AimDirection)
                            {
                                case (int)Directions.Down:
                                    newY = Y + spell.Range;
                                    Y = newY;
                                    break;

                                case (int)Directions.Up:
                                    newY = Y - spell.Range;
                                    Y = newY;
                                    break;

                                case (int)Directions.Left:
                                    newX = X - spell.Range;
                                    X = newX;
                                    break;

                                case (int)Directions.Right:
                                    newX = X + spell.Range;
                                    X = newX;
                                    break;
                            }

                            Mana -= spell.ManaCost;
                            SendUpdatedMana();
                            SendMovementData();
                            CastSpell = false;
                            gcdTick = TickCount;
                            break;
                    }
                }
            }

            if (FailedCast)
            {
                CastSpell = false;
                CastingSpell = false;
                FailedCast = false;
                castTick = 0;
            }

            if (CastingSpell)
            {
                if (TickCount - castTick > spell.CastTime)
                {
                    switch (spell.SpellType)
                    {
                        case (int)SpellType.Damage:
                            Mana -= spell.ManaCost;
                            SendUpdatedMana();

                            CastingSpell = false;
                            CastSpell = false;

                            spellBook[SpellBookSlot].SetupSpellAnimation(map.m_MapNpc[Target].X, map.m_MapNpc[Target].Y);
                            spellBook[SpellBookSlot].OnCoolDown = true;
                            spellBook[SpellBookSlot].cooldownTick = TickCount;
                            SendAttack(Target, spellBook[SpellBookSlot].SpellNumber, 1);
                            castTick = 0;
                            break;

                        case (int)SpellType.Heal:                            
                            Mana -= spell.ManaCost;
                            SendUpdatedMana();                           
                            if (Health + spell.Vital > MaxHealth) { Health = MaxHealth; }
                            else { Health += spell.Vital; }
                            SendUpdateHealth();

                            disSlot = FindOpenNpcDisplayText();
                            displayText[disSlot].CreateDisplayText(spell.Vital, (X + OffsetX), (Y + OffsetY), (int)DisplayTextMsg.Healing, "+");
                            spellBook[SpellBookSlot].SetupSpellAnimation((X + OffsetX), (Y + OffsetY));

                            CastingSpell = false;
                            CastSpell = false;
                            spellBook[SpellBookSlot].OnCoolDown = true;
                            spellBook[SpellBookSlot].cooldownTick = TickCount;
                            castTick = 0;                            
                            break;
                    }
                }
                else
                {
                    switch(spell.SpellType)
                    {
                        case (int)SpellType.Damage:
                            pX = X + OFFSET_X;
                            pY = Y + OFFSET_Y;
                            nX = map.m_MapNpc[Target].X;
                            nY = map.m_MapNpc[Target].Y;
                            dX = pX - nX;
                            dY = pY - nY;
                            dFinal = dX * dX + dY * dY;
                            dPoint = Math.Sqrt(dFinal);

                            if (dPoint > spell.Range)
                            {
                                disSlot = HandleData.FindOpenPlayerDisplayText(HandleData.myIndex);
                                displayText[disSlot].CreateDisplayText(0, nX, nY, (int)DisplayTextMsg.Warning, "MOR");
                                FailedCast = true;
                            }

                            if (!CheckFacingTarget(map.m_MapNpc[Target]))
                            {
                                disSlot = HandleData.FindOpenPlayerDisplayText(HandleData.myIndex);
                                displayText[disSlot].CreateDisplayText(0, pX, pY, (int)DisplayTextMsg.Warning, "FRD");
                                FailedCast = true;
                            }
                            break;
                    }
                }
            }

            if (TickCount - gcdTick > GLOBAL_COOL_DOWN)
            {
                for (int s = 0; s < MAX_PLAYER_SPELLBOOK; s++)
                {
                    if (spellBook[s].OnCoolDown)
                    {
                        if (TickCount - spellBook[s].cooldownTick > spells[spellBook[s].SpellNumber].CoolDown)
                        {
                            spellBook[s].OnCoolDown = false;
                            spellBook[s].cooldownTick = 0;
                        }
                    }
                }
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

        public void CheckForTabTarget()
        {
            if (TickCount - targetTick < 500) { return; }
            if (gui.inputChat.HasFocus == true) { return; }
            if (!renderWindow.HasFocus()) { return; }
            if (inShop || inChat || inBank) { return; }
            if (isChangingMaps) { return; }

            int pX = 0;
            int pY = 0;
            double dX = 0;
            double dY = 0;
            double dFinal = 0;
            double dPoint = 0;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Tab))
            {
                Logging.WriteMessageLog("TABBING - START -----");

                //check for closest target normal
                for (int n = 0; n < MAX_MAP_NPCS; n++)
                {
                    //find out if this property is populated :D
                    if (map.m_MapNpc[n].IsSpawned)
                    {
                        pX = X + OFFSET_X;
                        pY = Y + OFFSET_Y;
                        dX = pX - map.m_MapNpc[n].X;
                        dY = pY - map.m_MapNpc[n].Y;
                        dFinal = dX * dX + dY * dY;
                        dPoint = Math.Sqrt(dFinal);

                        if (dPoint < lastTarget)
                        {
                            Target = n;             
                        }

                        lastTarget = dPoint;
                    }
                }
                targetTick = TickCount;
                Logging.WriteMessageLog("TABBING - END -----");
            }
        }
        #endregion

        #region Voids
        int FindOpenNpcDisplayText()
        {
            for (int i = 0; i < MAX_DISPLAY_TEXT; i++)
            {
                if (displayText[i].displayText.DisplayedString == "EMPTY")
                {
                    return i;
                }
            }

            return MAX_DISPLAY_TEXT;
        }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            int x = (X * PIC_X) + (OffsetX * PIC_X);
            int y = (Y * PIC_Y) + (OffsetY * PIC_Y) - 16;
            int step = (Step * SPRITE_SIZE_X);
            int dir = (AimDirection * SPRITE_SIZE_Y);
            int name_x = (X * PIC_X) + (OffsetX * PIC_X) - (Name.Length / 2);
            int name_y = (Y * PIC_Y) + (OffsetY * PIC_Y) - PIC_Y;

            p_Name.Position = new Vector2f(name_x, name_y);
            p_Name.DisplayedString = Name;

            spritePic[0] = new Vertex(new Vector2f(x, y), new Vector2f(step, dir));
            spritePic[1] = new Vertex(new Vector2f(x + 32, y), new Vector2f(step + 32, dir));
            spritePic[2] = new Vertex(new Vector2f(x + 32, y + 48), new Vector2f(step + 32, dir + 48));
            spritePic[3] = new Vertex(new Vector2f(x, y + 48), new Vector2f(step, dir + 48));

            states.Texture = c_Sprite[Sprite];
            target.Draw(spritePic, states);
            target.Draw(p_Name);

            if (Target > -1)
            {
                x = map.m_MapNpc[Target].X;
                y = map.m_MapNpc[Target].Y;
                targetPic[0] = new Vertex(new Vector2f((x * PIC_X), (y * PIC_Y)), new Vector2f(0, 0));
                targetPic[1] = new Vertex(new Vector2f((x * PIC_X) + PIC_X, (y * PIC_Y)), new Vector2f(PIC_X, 0));
                targetPic[2] = new Vertex(new Vector2f((x * PIC_X) + PIC_X, (y * PIC_Y) + PIC_Y), new Vector2f(PIC_X, PIC_Y));
                targetPic[3] = new Vertex(new Vector2f((x * PIC_X), (y * PIC_X) + PIC_Y), new Vector2f(0, PIC_Y));

                states.Texture = targetTexture;
                target.Draw(targetPic, states);
            }

            if (CastingSpell)
            {
                castbarLength = ((float)(TickCount - castTick) / spells[spellBook[SpellBookSlot].SpellNumber].CastTime) * 35;

                x = ((X * PIC_X) + (OffsetX * PIC_X));
                y = ((Y * PIC_Y) + (OffsetY * PIC_Y)) + 48;
                castBar[0] = new Vertex(new Vector2f(x, y), Color.Cyan);
                castBar[1] = new Vertex(new Vector2f(castbarLength + x, y), Color.Cyan);
                castBar[2] = new Vertex(new Vector2f(castbarLength + x, y + 5), Color.Cyan);
                castBar[3] = new Vertex(new Vector2f(x, y + 5), Color.Cyan);

                target.Draw(castBar);
            }
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

        public void SendAttack(int npcNum, int spellNum, int attackType)
        {
            NetOutgoingMessage outMSG = SabertoothClient.netClient.CreateMessage();
            outMSG.Write((byte)PacketTypes.PlayerAttack);
            outMSG.WriteVariableInt32(npcNum);
            outMSG.WriteVariableInt32(spellNum);
            outMSG.WriteVariableInt32(attackType);
            outMSG.WriteVariableInt32(HandleData.myIndex);
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

        public void SendUpdateHotbar(int slot, int hotbarslot, string barType = "")
        {
            NetOutgoingMessage outMSG = SabertoothClient.netClient.CreateMessage();
            outMSG.Write((byte)PacketTypes.UpdateHotBar);
            outMSG.WriteVariableInt32(HandleData.myIndex);
            outMSG.WriteVariableInt32(slot);
            outMSG.WriteVariableInt32(hotbarslot);
            outMSG.Write(barType);
            SabertoothClient.netClient.SendMessage(outMSG, SabertoothClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendInteraction(int interactionValue, int interactionType)
        {
            NetOutgoingMessage outMSG = SabertoothClient.netClient.CreateMessage();
            outMSG.Write((byte)PacketTypes.Interaction);
            outMSG.WriteVariableInt32(interactionType);
            outMSG.WriteVariableInt32(HandleData.myIndex);
            outMSG.WriteVariableInt32(Map);
            outMSG.WriteVariableInt32(interactionValue);
            SabertoothClient.netClient.SendMessage(outMSG, SabertoothClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendUpdatedMana()
        {
            NetOutgoingMessage outMSG = SabertoothClient.netClient.CreateMessage();
            outMSG.Write((byte)PacketTypes.ManaData);
            outMSG.WriteVariableInt32(HandleData.myIndex);
            outMSG.WriteVariableInt32(Mana);
            SabertoothClient.netClient.SendMessage(outMSG, SabertoothClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendUpdateHealth()
        {
            NetOutgoingMessage outMSG = SabertoothClient.netClient.CreateMessage();
            outMSG.Write((byte)PacketTypes.HealthData);
            outMSG.WriteVariableInt32(HandleData.myIndex);
            outMSG.WriteVariableInt32(Health);
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

        public bool CheckFacingTarget(MapNpc mapNpc)
        {
            int pX = X + OFFSET_X;  //players x location
            int pY = Y + OFFSET_Y;  //players y location
            int nX = mapNpc.X;  //npcs x location
            int nY = mapNpc.Y;  //npcs y location

            switch (AimDirection)   //we are checking the players direction which is aim direction
            {
                case (int)Directions.Up:
                    if (pY > nY)
                    {
                        return true;
                    }
                    break;

                case (int)Directions.Down:
                    if (pY < nY)
                    {
                        return true;
                    }
                    break;

                case (int)Directions.Left:
                    if (pX > nX)
                    {
                        return true;
                    }
                    break;

                case (int)Directions.Right:
                    if (pX < nX)
                    {
                        return true;
                    }
                    break;
            }
            return false;
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

    public class SpellBook
    {
        public int SpellNumber { get; set; }
        public bool OnCoolDown { get; set; }
        public int cooldownTick;
        public SpellAnimation spellAnim = new SpellAnimation();

        public SpellBook() { }

        public SpellBook(int spellNum, bool oncd)
        {
            SpellNumber = spellNum;
            OnCoolDown = oncd;
        }

        public void SetupSpellAnimation(int x, int y)
        {
            int animnum = spells[SpellNumber].Anim - 1;
            spellAnim.Name = animations[animnum].Name;
            spellAnim.X = x;
            spellAnim.Y = y;
            spellAnim.SpriteNumber = animations[animnum].SpriteNumber;
            spellAnim.FrameCount = animations[animnum].FrameCount;
            spellAnim.FrameCountH = animations[animnum].FrameCountH;
            spellAnim.FrameCountV = animations[animnum].FrameCountV;
            spellAnim.FrameDuration = animations[animnum].FrameDuration;
            spellAnim.LoopCount = animations[animnum].LoopCount;
            spellAnim.RenderBelowTarget = animations[animnum].RenderBelowTarget;
            spellAnim.ConfigAnimation();
        }
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

    public enum TargetType : int
    {
        Npc,
        Player
    }
}
