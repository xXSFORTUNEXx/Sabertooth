using Lidgren.Network;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using Gwen.Control;
using static System.Environment;
using System;

namespace Client.Classes
{
    class Player : Drawable
    {
        #region Main Classes
        public NetConnection Connection;
        public Item mainWeapon = new Item();
        public Item offWeapon = new Item();
        public Item[] Backpack = new Item[25];
        public Item Chest = new Item();
        public Item Legs = new Item();
        public Item Feet = new Item();
        RenderText rText = new RenderText();
        const int spriteTextures = 8;
        VertexArray spritePic = new VertexArray(PrimitiveType.Quads, 4);
        Texture[] c_Sprite = new Texture[spriteTextures];
        Font font = new Font("Resources/Fonts/Arial.ttf");
        Text p_Name = new Text();
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
        public int interactionTick;
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
            for (int i = 0; i < spriteTextures; i++)
            {
                c_Sprite[i] = new Texture("Resources/Characters/" + (i + 1) + ".png");
            }
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
            offsetX = 12;
            offsetY = 9;
            for (int i = 0; i < spriteTextures; i++)
            {
                c_Sprite[i] = new Texture("Resources/Characters/" + (i + 1) + ".png");
            }
            p_Name.Font = font;
            p_Name.CharacterSize = 12;
            p_Name.Color = Color.White;
            p_Name.Style = Text.Styles.Bold;
        }

        public Player(string name)
        {
            Name = name;
            for (int i = 0; i < spriteTextures; i++)
            {
                c_Sprite[i] = new Texture("Resources/Characters/" + (i + 1) + ".png");
            }
            p_Name.Font = font;
            p_Name.CharacterSize = 12;
            p_Name.Color = Color.White;
            p_Name.Style = Text.Styles.Bold;
        }

        public Player(NetConnection conn)
        {
            Connection = conn;
            for (int i = 0; i < spriteTextures; i++)
            {
                c_Sprite[i] = new Texture("Resources/Characters/" + (i + 1) + ".png");
            }
            p_Name.Font = font;
            p_Name.CharacterSize = 12;
            p_Name.Color = Color.White;
            p_Name.Style = Text.Styles.Bold;
        }

        public Player()
        {
            for (int i = 0; i < 25; i++)
            {
                Backpack[i] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1);
            }
            for (int i = 0; i < spriteTextures; i++)
            {
                c_Sprite[i] = new Texture("Resources/Characters/" + (i + 1) + ".png");
            }
            p_Name.Font = font;
            p_Name.CharacterSize = 12;
            p_Name.Color = Color.White;
            p_Name.Style = Text.Styles.Bold;
        }
        #endregion

        #region Voids
        public virtual void Draw(RenderTarget target, RenderStates states)
        {            
            int x = (X * 32) + (offsetX * 32);
            int y = (Y * 32) + (offsetY * 32) - 16;
            int step = (Step * 32);
            int dir = (AimDirection * 48);
            int name_x = (X * 32) + (offsetX * 32) - (Name.Length / 2);
            int name_y = (Y * 32) + (offsetY * 32) - 32;
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

        public void CheckControllerMovement(NetClient c_Client, RenderWindow c_Window, Map c_Map, GUI c_GUI, int index)
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
            if (c_GUI.inputChat.HasFocus == true) { return; }
            if (!c_Window.HasFocus()) { return; }

            float deadZone = 35;
            float x = SnaptoZero(Joystick.GetAxisPosition(0, Joystick.Axis.X), deadZone);
            float y = SnaptoZero(Joystick.GetAxisPosition(0, Joystick.Axis.Y), deadZone);

            c_GUI.d_Controller.Text = "X: " + x + " / Y: " + y;

            if (x > deadZone)
            {
                if (X < 49 - offsetX)
                {
                    if (c_Map.Ground[(X + offsetX) + 1, (Y + offsetY)].type == (int)TileType.Blocked)
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

            if (x < -deadZone)
            {
                if (X > 1 - offsetX)
                {
                    if (c_Map.Ground[(X + offsetX) - 1, (Y + offsetY)].type == (int)TileType.Blocked)
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

            if (y > deadZone)
            {
                if (Y < 49 - offsetY)
                {
                    if (c_Map.Ground[(X + offsetX), (Y + offsetY) + 1].type == (int)TileType.Blocked)
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

            if (y < -deadZone)
            {
                if (Y > 1 - offsetY)
                {
                    if (c_Map.Ground[(X + offsetX), (Y + offsetY) - 1].type == (int)TileType.Blocked)
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

            if (Moved == true)
            {
                Step += 1;
                if (Step == 4) { Step = 0; }
                Moved = false;
                SendMovementData(c_Client, index);
            }
        }

        public void CheckControllerChangeDirection(NetClient c_Client, GUI c_GUI, RenderWindow c_Window, int index)
        {
            if (!Joystick.IsConnected(0)) { return; }
            if (c_GUI.inputChat.HasFocus == true) { return; }
            if (!c_Window.HasFocus()) { return; }

            float deadZone = 35;
            float u = SnaptoZero(Joystick.GetAxisPosition(0, Joystick.Axis.U), deadZone);
            float r = SnaptoZero(Joystick.GetAxisPosition(0, Joystick.Axis.R), deadZone);

            c_GUI.d_ConDir.Text = "Direction: " + AimDirection;

            if (u > deadZone)
            {
                AimDirection = (int)Directions.Right;
                SendUpdateDirection(c_Client, index);
            }

            if (u < -deadZone)
            {
                AimDirection = (int)Directions.Left;
                SendUpdateDirection(c_Client, index);
            }

            if (r > deadZone)
            {
                AimDirection = (int)Directions.Down;
                SendUpdateDirection(c_Client, index);
            }

            if (r < -deadZone)
            {
                AimDirection = (int)Directions.Up;
                SendUpdateDirection(c_Client, index);
            }
        }

        public void CheckControllerAttack(NetClient c_Client, GUI c_GUI, RenderWindow c_Window, int index)
        {
            if (!Joystick.IsConnected(0)) { return; }
            if (c_GUI.inputChat.HasFocus == true) { return; }
            if (!c_Window.HasFocus()) { return; }
            if (Attacking == true) { return; }
            if (TickCount - reloadTick < mainWeapon.ReloadSpeed) { return; }
            if (TickCount - equipTick < 5000) { return; }

            if (Joystick.GetAxisPosition(0, Joystick.Axis.Z) > 25)
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

        public void CheckControllerReload(NetClient c_Client, int index)
        {
            if (!Joystick.IsConnected(0)) { return; }
            if (Joystick.IsButtonPressed(0, 2))
            {
                if (mainWeapon.Clip == mainWeapon.maxClip) { return; }
                Reload();
                reloadTick = TickCount;
                SendUpdateClip(c_Client, index);
                SendUpdateAmmo(c_Client, index);
            }
        }

        public void CheckControllerItemPickUp(NetClient c_Client, GUI c_GUI, RenderWindow c_Window, int index)
        {
            if (!Joystick.IsConnected(0)) { return; }
            if (c_GUI.inputChat.HasFocus == true) { return; }
            if (!c_Window.HasFocus()) { return; }
            if (Attacking == true) { return; }

            if (Joystick.IsButtonPressed(0, 0))
            {
                SendPickupItem(c_Client, index);
                PickupTick = TickCount;
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

        public void CheckMovement(NetClient c_Client, int index, RenderWindow c_Window, Map m_Map, GUI c_GUI)
        {
            if (Moved == true) { Moved = false; return; }
            if (c_GUI.inputChat.HasFocus == true) { return; }
            if (!c_Window.HasFocus()) { return; }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                if (Y > 1 - offsetY)
                {
                    if (m_Map.Ground[(X + offsetX), (Y + offsetY) - 1].type == (int)TileType.Blocked)
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
                    if (m_Map.Ground[(X + offsetX), (Y + offsetY) + 1].type == (int)TileType.Blocked)
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
                    if (m_Map.Ground[(X + offsetX) - 1, (Y + offsetY)].type == (int)TileType.Blocked)
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
                    if (m_Map.Ground[(X + offsetX) + 1, (Y + offsetY)].type == (int)TileType.Blocked) 
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
        //current
        public void CheckPlayerInteraction(NetClient c_Client, GUI c_GUI, RenderWindow c_Window, Map m_Map, int index)
        {
            if (c_GUI.inputChat.HasFocus == true) { return; }
            if (!c_Window.HasFocus()) { return; }
            if (Attacking == true) { return; }
            if (TickCount - interactionTick < 1000) { return; }

            if (Keyboard.IsKeyPressed(Keyboard.Key.E))
            {
                switch (AimDirection)
                {
                    case (int)Directions.Up:
                        if (Y > 2 - offsetY)
                        {
                            for (int i = 0; i < 10; i++)
                            {
                                if (m_Map.m_MapNpc[i].IsSpawned && m_Map.m_MapNpc[i].Behavior == (int)BehaviorType.ShopOwner)
                                {
                                    if (m_Map.m_MapNpc[i].Y + 1 == (Y + offsetY) && m_Map.m_MapNpc[i].X == (X + offsetX))
                                    {
                                        c_GUI.outputChat.AddRow("Npc Interaction! NPC: " + i);
                                    }
                                }
                            }
                        }
                        break;

                    case (int)Directions.Down:
                        if (Y < 48 - offsetY)
                        {
                            for (int i = 0; i < 10; i++)
                            {
                                if (m_Map.m_MapNpc[i].IsSpawned && m_Map.m_MapNpc[i].Behavior == (int)BehaviorType.ShopOwner)
                                {
                                    if (m_Map.m_MapNpc[i].Y - 1 == (Y + offsetY) && m_Map.m_MapNpc[i].X == (X + offsetX))
                                    {
                                        c_GUI.outputChat.AddRow("Npc Interaction! NPC: " + i);
                                    }
                                }
                            }
                        }
                        break;

                    case (int)Directions.Left:
                        if (X > 2 - offsetX)
                        {
                            for (int i = 0; i < 10; i++)
                            {
                                if (m_Map.m_MapNpc[i].IsSpawned && m_Map.m_MapNpc[i].Behavior == (int)BehaviorType.ShopOwner)
                                {
                                    if (m_Map.m_MapNpc[i].X + 1 == (X + offsetX) && m_Map.m_MapNpc[i].Y == (Y + offsetY))
                                    {
                                        c_GUI.outputChat.AddRow("Npc Interaction! NPC: " + i);
                                    }
                                }
                            }
                        }
                        break;

                    case (int)Directions.Right:
                        if (X < 48 - offsetX)
                        {
                            for (int i = 0; i < 10; i++)
                            {
                                if (m_Map.m_MapNpc[i].IsSpawned && m_Map.m_MapNpc[i].Behavior == (int)BehaviorType.ShopOwner)
                                {
                                    if (m_Map.m_MapNpc[i].X - 1 == (X + offsetX) && m_Map.m_MapNpc[i].Y == (Y + offsetY))
                                    {
                                        c_GUI.outputChat.AddRow("Npc Interaction! NPC: " + i);
                                    }
                                }
                            }
                        }
                        break;
                }
                interactionTick = TickCount;
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
            NetOutgoingMessage outMSG = c_Client.CreateMessage(2);
            outMSG.Write((byte)PacketTypes.RangedAttack);
            outMSG.WriteVariableInt32(index);
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
