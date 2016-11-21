using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using Lidgren.Network;

namespace Client.Classes
{
    class Projectile
    {
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Direction { get; set; }
        public int Damage { get; set; }
        public int Range { get; set; }
        public int Sprite { get; set; }
        public int Owner { get; set; }
        public int Type { get; set; }
        public int Speed { get; set; }
        public bool Moved;

        Sprite c_Sprite = new Sprite();
        Texture proj_Texture = new Texture("Resources/Projectiles/1.png");

        public Projectile() { }

        public Projectile(string name, int x, int y, int direction)
        {
            Name = name;
            X = x;
            Y = y;
            Direction = direction;
        }

        public Projectile(string name, int damage, int range, int sprite, int owner, int type, int speed)
        {
            Name = name;
            Damage = damage;
            Range = range;
            Sprite = sprite;
            Owner = owner;
            Type = type;
            Speed = speed;
        }

        public void DrawProjectile(RenderWindow c_Window)
        {
            c_Sprite.Texture = proj_Texture;
            c_Sprite.TextureRect = new IntRect((Direction * 32), 0, 32, 32);
            c_Sprite.Position = new Vector2f((X * 32), (Y * 32));

            c_Window.Draw(c_Sprite);
        }

        public void CheckMovment(NetClient c_Client, RenderWindow c_Window, Map c_MoveMap, int slot)
        {
            if (Moved == true) { Moved = false; return; }
            if (c_MoveMap.mapProj[slot] == null) { return; }
            switch (c_MoveMap.mapProj[slot].Direction)
            {
                case (int)Directions.Down:
                    if (Y < 49)
                    {
                        if (c_MoveMap.Ground[X, (Y + 1)].type == (int)TileType.Blocked)
                        {
                            Direction = (int)Directions.Down;
                            Moved = false;
                            SendUpdateProj(c_Client, slot, false);
                            return;
                        }
                        Y += 1;
                        Direction = (int)Directions.Down;
                        Moved = true;
                    }
                    else
                    {
                        Direction = (int)Directions.Down;
                        Moved = false;
                        SendUpdateProj(c_Client, slot, false);
                        return;
                    }
                    break;

                case (int)Directions.Left:
                    if (X > 1)
                    {
                        if (c_MoveMap.Ground[(X + 1), Y].type == (int)TileType.Blocked)
                        {
                            Direction = (int)Directions.Left;
                            Moved = false;
                            SendUpdateProj(c_Client, slot, false);
                            return;
                        }
                        X -= 1;
                        Direction = (int)Directions.Left;
                        Moved = true;
                    }
                    else
                    {
                        Direction = (int)Directions.Down;
                        Moved = false;
                        SendUpdateProj(c_Client, slot, false);
                        return;
                    }
                    break;

                case (int)Directions.Right:
                    if (X < 49)
                    {
                        if (c_MoveMap.Ground[(X + 1), Y].type == (int)TileType.Blocked)
                        {
                            Direction = (int)Directions.Right;
                            Moved = false;
                            SendUpdateProj(c_Client, slot, false);
                            return;
                        }
                        X += 1;
                        Direction = (int)Directions.Right;
                        Moved = true;
                    }
                    else
                    {
                        Direction = (int)Directions.Down;
                        Moved = false;
                        SendUpdateProj(c_Client, slot, false);
                        return;
                    }
                    break;

                case (int)Directions.Up:
                    if (Y > 1)
                    {
                        if (c_MoveMap.Ground[X, (Y + 1)].type == (int)TileType.Blocked)
                        {
                            Direction = (int)Directions.Up;
                            Moved = false;
                            SendUpdateProj(c_Client, slot, false);
                            return;
                        }
                        Y -= 1;
                        Direction = (int)Directions.Up;
                        Moved = true;
                    }
                    else
                    {
                        Direction = (int)Directions.Down;
                        Moved = false;
                        SendUpdateProj(c_Client, slot, false);
                        return;
                    }
                    break;
            }
            if (Moved == true)
            {
                Moved = false;
                SendUpdateProj(c_Client, slot, true);
            }
        }

        void SendUpdateProj(NetClient c_Client, int slot, bool valid)
        {
            NetOutgoingMessage outMSG = c_Client.CreateMessage();
            outMSG.Write((byte)PacketTypes.UpdateProj);
            outMSG.WriteVariableInt32(slot);
            outMSG.Write(valid);
            outMSG.WriteVariableInt32(Owner);
            outMSG.WriteVariableInt32(X);
            outMSG.WriteVariableInt32(Y);
            outMSG.WriteVariableInt32(Direction);

            c_Client.SendMessage(outMSG, c_Client.ServerConnection, NetDeliveryMethod.ReliableSequenced, 5);
        }
    }

    enum ProjType
    {
        Bullet,
        Thrown,
        Explosive
    }
}