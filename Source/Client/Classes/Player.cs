using Lidgren.Network;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using Gwen.Control;

namespace Client.Classes
{
    class Player
    {
        public string Name { get; set; }    //define player name
        public string Pass { get; set; }    //define player password
        public NetConnection Connection;    //create network connection
        RenderText c_Text = new RenderText();
        public int X { get; set; }  //define x
        public int Y { get; set; }  //define y
        public int Map { get; set; }    //define map
        public int Direction { get; set; }  //define direction
        public int Sprite { get; set; } //define player sprite
        public int Step;    //the step at which the player is
        public int maxHealth;

        public int Level { get; set; }
        public int Health { get; set; }
        public int Hunger { get; set; }
        public int Hydration { get; set; }
        public int Experience { get; set; }
        public int Money { get; set; }
        public int Armor { get; set; }

        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Endurance { get; set; }
        public int Stamina { get; set; }

        public bool Moved;  //if they have moved
        public int offsetX; //offset for center screen
        public int offsetY; //offset for center screen
        public int tempX;   //temp x that is saved for movement over packets
        public int tempY;   //temp y that is saved for movement over packets
        public int tempDir; //temp direction that is saved for movement over packets
        public int tempStep;    //temp step that is saved for movement over packets
        Sprite c_Sprite = new Sprite();    //define a sprite for which the above texture with be reference from

        public Player(string name, string pass, int x, int y, int direction, int map, int level, int health, int exp, int money, int armor, int hunger, int hydration, int str, int agi, int end, int sta, NetConnection conn)    //main player contructor if we have all the details
        {
            Name = name;
            Pass = pass;
            X = x;
            Y = y;
            Map = map;
            offsetX = 12;
            offsetY = 9;
            Direction = direction;
            Level = level;
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
            c_Sprite.TextureRect = new IntRect((Step * 32), (Direction * 48), 32, 48); //define what are we want to draw from the texture using a intrect (rectangle)
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

        void SendMovementData(NetClient c_Client, int index)   //packet for sending data to the server
        {
            NetOutgoingMessage outMSG = c_Client.CreateMessage();  //create the message we will use to write out data going out to the server
            outMSG.Write((byte)PacketTypes.MoveData);   //packet header name
            outMSG.Write(index);    //current user's index
            outMSG.Write(X);    //write the x of the current index
            outMSG.Write(Y);    //write the y of the current index
            outMSG.Write(Direction);    //write the direction of the current index
            outMSG.Write(Step); //write the step of the current index
            c_Client.SendMessage(outMSG, c_Client.ServerConnection, NetDeliveryMethod.ReliableOrdered);   //send the packet to the server in reliable order so its not jumbled when the server gets it
        }

        void SendUpdateDirection(NetClient c_Client, int index)    //packet for updateing the direction
        {
            NetOutgoingMessage outMSG = c_Client.CreateMessage();  //create the message we will use to wirte out data going to the server
            outMSG.Write((byte)PacketTypes.UpdateDirection);    //packet header name
            outMSG.Write(index);    //current clients index
            outMSG.Write(Direction);    //current index direction
            c_Client.SendMessage(outMSG, c_Client.ServerConnection, NetDeliveryMethod.ReliableOrdered);   //send the packet in reliable order
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
}
