using Lidgren.Network;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using Gwen.Control;
using static System.Environment;

namespace Client.Classes
{
    class Player
    {
        public NetConnection Connection;
        RenderText c_Text = new RenderText();
        public Item mainWeapon = new Item();
        public Item offWeapon = new Item();
        Sprite c_Sprite = new Sprite();

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

        public Player(string name, string pass, NetConnection conn) //player contructor if we are missing a few key setails
        {
            Name = name;
            Pass = pass;
            Connection = conn;
            offsetX = 12;
            offsetY = 9;
        }

        public Player(string name)  //for creating a player array with just the name defined
        {
            Name = name;
        }

        public Player(NetConnection conn)   //for creating a player array with just the server connection defined
        {
            Connection = conn;
        }

        public Player() { }     //An emply player contructor, this is only needed to setup a blank player array for information to be stored appon login

        public void DrawPlayer(RenderWindow c_Window, Texture c_Texture)  // draws the player to the screen
        {
            c_Sprite.Texture = c_Texture; //set the sprites texture
            c_Sprite.TextureRect = new IntRect((Step * 32), (AimDirection * 48), 32, 48); //define what are we want to draw from the texture using a intrect (rectangle)
            c_Sprite.Position = new Vector2f(((X * 32) + (offsetX * 32)), (((Y * 32) + (offsetY * 32) - 16))); //Define the actual location on the screen

            c_Window.Draw(c_Sprite);  //draw the player to the define window using the above sprite array
        }

        public void DrawPlayerName(RenderWindow c_Window)
        {
            c_Text.DrawText(c_Window, Name, new Vector2f((X * 32) + ((offsetX * 32) - Name.Length / 2), (Y * 32) + (offsetY * 32) - 32), 12, Color.White);
        }

        public void CheckMovement(NetClient c_Client, int index, RenderWindow c_Window, Map movementMap, GUI c_GUI)   //Check for player movement
        {
            if (Moved == true) { Moved = false; return; }   //check and see if they are already moving and if so we return and exit the void
            if (c_GUI.inputChat.HasFocus == true) { return; }
            if (!c_Window.HasFocus()) { return; }  //we need to make sure that when a button is pressed our game window has focus otherwise we dont want to process the button press

            if (Keyboard.IsKeyPressed(Keyboard.Key.W))  //check for w
            {
                if (Y > 1 - offsetY)    //make sure we are not out of bounds of the screen
                {
                    if (movementMap.Ground[(X + offsetX), (Y + offsetY) - 1].type == (int)TileType.Blocked) //check for a blocked tile
                    {
                        Direction = (int)Directions.Up; //set the direction to up
                        Moved = false;  //we cant move but we can still change direction
                        SendUpdateDirection(c_Client, index);  //send the update direction packet
                        return; //exit this bitch
                    }
                    Y -= 1; //move the player up
                    Direction = (int)Directions.Up; //change their direction to up
                    Moved = true;   //register that we have moved
                }
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S))  //check for s
            {
                if (Y < 49 - offsetY)   //make sure we are not out of bounds of the screen
                {
                    if (movementMap.Ground[(X + offsetX), (Y + offsetY) + 1].type == (int)TileType.Blocked) //check for a blocked tile
                    {
                        Direction = (int)Directions.Down; //set the direction to down
                        Moved = false;  //we cant move but we can still change direction
                        SendUpdateDirection(c_Client, index);  //send the update direction packet
                        return; //exit this bitch
                    }
                    Y += 1; //move the player down
                    Direction = (int)Directions.Down;   //change their direction to up
                    Moved = true;   //register that we have moved
                }
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A))  //check for a
            {
                if (X > 1 - offsetX)    //make sure we are not out of bounds of the screen
                {
                    if (movementMap.Ground[(X + offsetX) - 1, (Y + offsetY)].type == (int)TileType.Blocked) //check for a blocked tile
                    {
                        Direction = (int)Directions.Left; //set the direction to left
                        Moved = false;  //we cant move but we can still change direction
                        SendUpdateDirection(c_Client, index);  //send the update direction packet
                        return; //exit this bitch
                    }
                    X -= 1; //move player to the left
                    Direction = (int)Directions.Left;   //set player direction to left
                    Moved = true;   //register that we have moved
                }
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.D))  //check for d
            {
                if (X < 49 - offsetX)   //make sure we are not out of bounds of the screen
                {
                    if (movementMap.Ground[(X + offsetX) + 1, (Y + offsetY)].type == (int)TileType.Blocked) //check for a blocked tile
                    {
                        Direction = (int)Directions.Right; //set the direction to right
                        Moved = false;  //we cant move but we can still change direction
                        SendUpdateDirection(c_Client, index);  //send the update direction packet
                        return; //exit this bitch
                    }
                    X += 1; //move player to the right
                    Direction = (int)Directions.Right;  //set player direction to right
                    Moved = true;   //register that we have moved
                }
            }

            if (Moved == true)  //check and see if we moved
            {
                Step += 1;  //add a step if so
                if (Step == 4) { Step = 0; }    //if we have reached the make amount of steps then we need to start over
                Moved = false;  //we moved so let make sure it knows we can move again
                SendMovementData(c_Client, index); //send movement data to the server
            }
        }

        public void CheckAttack(NetClient c_Client, GUI c_GUI, RenderWindow c_Window, int index)
        {
            if (Attacking == true) { return; }
            if (c_GUI.inputChat.HasFocus == true) { return; }
            if (!c_Window.HasFocus()) { return; }
            if (TickCount - reloadTick < mainWeapon.ReloadSpeed) { return; }
            //Range Attack
            //Direction Up
            if (Keyboard.IsKeyPressed(Keyboard.Key.I))
            {
                AimDirection = (int)Directions.Up;
                Attacking = true;
            }
            //Direction Down
            if (Keyboard.IsKeyPressed(Keyboard.Key.K))
            {
                AimDirection = (int)Directions.Down;
                Attacking = true;
            }
            //Direction Left
            if (Keyboard.IsKeyPressed(Keyboard.Key.J))
            {
                AimDirection = (int)Directions.Left;
                Attacking = true;
            }
            //Direction Right
            if (Keyboard.IsKeyPressed(Keyboard.Key.L))
            {
                AimDirection = (int)Directions.Right;
                Attacking = true;
            }

            if (Attacking == true)
            {             
                switch (mainWeapon.ammoType)
                {
                    case (int)AmmoType.Pistol:
                        if (PistolAmmo == 0) { Attacking = false; SendUpdateDirection(c_Client, index); return; }
                        break;
                    case (int)AmmoType.AssaultRifle:
                        if (AssaultAmmo == 0) { Attacking = false; SendUpdateDirection(c_Client, index); return; }
                        break;
                    case (int)AmmoType.Rocket:
                        if (RocketAmmo == 0) { Attacking = false; SendUpdateDirection(c_Client, index); return; }
                        break;
                    case (int)AmmoType.Grenade:
                        if (GrenadeAmmo == 0) { Attacking = false; SendUpdateDirection(c_Client, index); return; }
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
                        mainWeapon.Clip = mainWeapon.maxClip;
                        PistolAmmo -= mainWeapon.maxClip;
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
                        mainWeapon.Clip = mainWeapon.maxClip;
                        AssaultAmmo -= mainWeapon.maxClip;
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
                        mainWeapon.Clip = mainWeapon.maxClip;
                        RocketAmmo -= mainWeapon.maxClip;
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
                        mainWeapon.Clip = mainWeapon.maxClip;
                        GrenadeAmmo -= mainWeapon.maxClip;
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
            c_Client.SendMessage(outMSG, c_Client.ServerConnection, NetDeliveryMethod.ReliableSequenced, 10);
        }

        void SendCreateBullet(NetClient c_Client, int index)
        {
            NetOutgoingMessage outMSG = c_Client.CreateMessage();
            outMSG.Write((byte)PacketTypes.RangedAttack);
            outMSG.WriteVariableInt32(index);
            outMSG.WriteVariableInt32(Direction);
            outMSG.WriteVariableInt32(AimDirection);
            c_Client.SendMessage(outMSG, c_Client.ServerConnection, NetDeliveryMethod.ReliableSequenced, 11);
        }

        void SendMovementData(NetClient c_Client, int index)   //packet for sending data to the server
        {
            NetOutgoingMessage outMSG = c_Client.CreateMessage();  //create the message we will use to write out data going out to the server
            outMSG.Write((byte)PacketTypes.MoveData);   //packet header name
            outMSG.WriteVariableInt32(index);    //current user's index
            outMSG.WriteVariableInt32(X);    //write the x of the current index
            outMSG.WriteVariableInt32(Y);    //write the y of the current index
            outMSG.WriteVariableInt32(Direction);    //write the direction of the current index
            outMSG.WriteVariableInt32(AimDirection);
            outMSG.WriteVariableInt32(Step); //write the step of the current index
            c_Client.SendMessage(outMSG, c_Client.ServerConnection, NetDeliveryMethod.ReliableSequenced, 1);   //send the packet to the server in reliable order so its not jumbled when the server gets it
        }

        void SendUpdateDirection(NetClient c_Client, int index)    //packet for updateing the direction
        {
            NetOutgoingMessage outMSG = c_Client.CreateMessage();  //create the message we will use to wirte out data going to the server
            outMSG.Write((byte)PacketTypes.UpdateDirection);    //packet header name
            outMSG.WriteVariableInt32(index);    //current clients index
            outMSG.WriteVariableInt32(Direction);    //current index direction
            outMSG.WriteVariableInt32(AimDirection);
            c_Client.SendMessage(outMSG, c_Client.ServerConnection, NetDeliveryMethod.ReliableSequenced, 2);   //send the packet in reliable order
        }
    }

    public enum Directions  //enumeration for directions
    {
        Down,   //down is 0
        Left,   //left is 1
        Right,  //right is 2
        Up,  //up is 3
        DownLeft,
        DownRight,
        UpLeft,
        UpRight
    }

    //Channels
}
