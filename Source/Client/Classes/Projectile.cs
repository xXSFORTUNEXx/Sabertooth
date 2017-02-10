using Lidgren.Network;
using SFML.Graphics;
using SFML.System;
using System;

namespace Client.Classes
{
    class Projectile : Drawable
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
        public int RangeCounter;
        const int maxprojSprites = 2;
        VertexArray spritePic = new VertexArray(PrimitiveType.Quads, 4);
        Texture[] proj_Texture = new Texture[maxprojSprites];
        Random RND = new Random();

        public Projectile()
        {
            for (int i = 0; i < maxprojSprites; i++)
            {
                proj_Texture[i] = new Texture("Resources/Projectiles/" + (i + 1) + ".png");
            }
        }

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

        public void CheckMovment(NetClient c_Client, RenderWindow c_Window, Map c_MoveMap, int slot)
        {
            if (Moved == true) { Moved = false; return; }
            if (c_MoveMap.m_MapProj[slot] == null) { return; }

            if (RangeCounter > Range) { Direction = (int)Directions.Down; Moved = false; SendClearProjectile(c_Client, c_MoveMap, slot); return; }

            switch (c_MoveMap.m_MapProj[slot].Direction)
            {
                case (int)Directions.Down:
                    if (Y < 49)
                    {
                        if (c_MoveMap.Ground[X, (Y + 1)].Type == (int)TileType.Blocked)
                        {
                            Direction = (int)Directions.Down;
                            Moved = false;
                            SendClearProjectile(c_Client, c_MoveMap, slot);
                            return;
                        }
                        for (int i = 0; i < 10; i++)
                        {
                            if (c_MoveMap.m_MapNpc[i].IsSpawned)
                            {
                                if (c_MoveMap.m_MapNpc[i].X == X && c_MoveMap.m_MapNpc[i].Y == (Y + 1))
                                {
                                    if (c_MoveMap.m_MapNpc[i].Behavior == (int)BehaviorType.Friendly || c_MoveMap.m_MapNpc[i].Behavior == (int)BehaviorType.Passive)
                                    {
                                        Direction = (int)Directions.Down;
                                        Moved = false;
                                        SendClearProjectile(c_Client, c_MoveMap, slot);
                                        return;
                                    }
                                    Direction = (int)Directions.Down;
                                    Moved = false;
                                    SendProjectileAttackNpc(c_Client, c_MoveMap, slot, i, 0);
                                    return;
                                }
                            }
                        }
                        for (int c = 0; c < 20; c++)
                        {
                            if (c_MoveMap.r_MapNpc[c].IsSpawned)
                            {
                                if (c_MoveMap.r_MapNpc[c].X == X && c_MoveMap.r_MapNpc[c].Y == (Y + 1))
                                {
                                    Direction = (int)Directions.Down;
                                    Moved = false;
                                    SendProjectileAttackNpc(c_Client, c_MoveMap, slot, c, 1);
                                    return;
                                }
                            }
                        }
                        Y += 1;
                        RangeCounter += 1;
                        Direction = (int)Directions.Down;
                        Moved = true;
                    }
                    else
                    {
                        Direction = (int)Directions.Down;
                        Moved = false;
                        SendClearProjectile(c_Client, c_MoveMap, slot);
                        return;
                    }
                    break;

                case (int)Directions.Left:
                    if (X > 1)
                    {
                        if (c_MoveMap.Ground[(X - 1), Y].Type == (int)TileType.Blocked)
                        {
                            Direction = (int)Directions.Left;
                            Moved = false;
                            SendClearProjectile(c_Client, c_MoveMap, slot);
                            return;
                        }
                        for (int i = 0; i < 10; i++)
                        {
                            if (c_MoveMap.m_MapNpc[i].IsSpawned)
                            {
                                if (c_MoveMap.m_MapNpc[i].X == (X - 1) && c_MoveMap.m_MapNpc[i].Y == Y)
                                {
                                    if (c_MoveMap.m_MapNpc[i].Behavior == (int)BehaviorType.Friendly || c_MoveMap.m_MapNpc[i].Behavior == (int)BehaviorType.Passive)
                                    {
                                        Direction = (int)Directions.Down;
                                        Moved = false;
                                        SendClearProjectile(c_Client, c_MoveMap, slot);
                                        return;
                                    }
                                    Direction = (int)Directions.Left;
                                    Moved = false;
                                    SendProjectileAttackNpc(c_Client, c_MoveMap, slot, i, 0);
                                    return;
                                }
                            }
                        }
                        for (int c = 0; c < 20; c++)
                        {
                            if (c_MoveMap.r_MapNpc[c].IsSpawned)
                            {
                                if (c_MoveMap.r_MapNpc[c].X == (X - 1) && c_MoveMap.r_MapNpc[c].Y == Y)
                                {
                                    Direction = (int)Directions.Left;
                                    Moved = false;
                                    SendProjectileAttackNpc(c_Client, c_MoveMap, slot, c, 1);
                                    return;
                                }
                            }
                        }
                        X -= 1;
                        RangeCounter += 1;
                        Direction = (int)Directions.Left;
                        Moved = true;
                    }
                    else
                    {
                        Direction = (int)Directions.Down;
                        Moved = false;
                        SendClearProjectile(c_Client, c_MoveMap, slot);
                        return;
                    }
                    break;

                case (int)Directions.Right:
                    if (X < 49)
                    {
                        if (c_MoveMap.Ground[(X + 1), Y].Type == (int)TileType.Blocked)
                        {
                            Direction = (int)Directions.Right;
                            Moved = false;
                            SendClearProjectile(c_Client, c_MoveMap, slot);
                            return;
                        }
                        for (int i = 0; i < 10; i++)
                        {
                            if (c_MoveMap.m_MapNpc[i].IsSpawned)
                            {
                                if (c_MoveMap.m_MapNpc[i].X == (X + 1) && c_MoveMap.m_MapNpc[i].Y == Y)
                                {
                                    if (c_MoveMap.m_MapNpc[i].Behavior == (int)BehaviorType.Friendly || c_MoveMap.m_MapNpc[i].Behavior == (int)BehaviorType.Passive)
                                    {
                                        Direction = (int)Directions.Down;
                                        Moved = false;
                                        SendClearProjectile(c_Client, c_MoveMap, slot);
                                        return;
                                    }
                                    Direction = (int)Directions.Right;
                                    Moved = false;
                                    SendProjectileAttackNpc(c_Client, c_MoveMap, slot, i, 0);
                                    return;
                                }
                            }
                        }
                        for (int c = 0; c < 20; c++)
                        {
                            if (c_MoveMap.r_MapNpc[c].IsSpawned)
                            {
                                if (c_MoveMap.r_MapNpc[c].X == (X + 1) && c_MoveMap.r_MapNpc[c].Y == Y)
                                {
                                    Direction = (int)Directions.Right;
                                    Moved = false;
                                    SendProjectileAttackNpc(c_Client, c_MoveMap, slot, c, 1);
                                    return;
                                }
                            }
                        }
                        X += 1;
                        RangeCounter += 1;
                        Direction = (int)Directions.Right;
                        Moved = true;
                    }
                    else
                    {
                        Direction = (int)Directions.Down;
                        Moved = false;
                        SendClearProjectile(c_Client, c_MoveMap, slot);
                        return;
                    }
                    break;

                case (int)Directions.Up:
                    if (Y > 1)
                    {
                        if (c_MoveMap.Ground[X, (Y - 1)].Type == (int)TileType.Blocked)
                        {
                            Direction = (int)Directions.Up;
                            Moved = false;
                            SendClearProjectile(c_Client, c_MoveMap, slot);
                            return;
                        }
                        for (int i = 0; i < 10; i++)
                        {
                            if (c_MoveMap.m_MapNpc[i].IsSpawned)
                            {
                                if (c_MoveMap.m_MapNpc[i].X == X && c_MoveMap.m_MapNpc[i].Y == (Y - 1))
                                {
                                    if (c_MoveMap.m_MapNpc[i].Behavior == (int)BehaviorType.Friendly || c_MoveMap.m_MapNpc[i].Behavior == (int)BehaviorType.Passive)
                                    {
                                        Direction = (int)Directions.Down;
                                        Moved = false;
                                        SendClearProjectile(c_Client, c_MoveMap, slot);
                                        return;
                                    }
                                    Direction = (int)Directions.Up;
                                    Moved = false;
                                    SendProjectileAttackNpc(c_Client, c_MoveMap, slot, i, 0);
                                    return;
                                }
                            }
                        }
                        for (int c = 0; c < 20; c++)
                        {
                            if (c_MoveMap.r_MapNpc[c].IsSpawned)
                            {
                                if (c_MoveMap.r_MapNpc[c].X == X && c_MoveMap.r_MapNpc[c].Y == (Y - 1))
                                {                                    
                                    Direction = (int)Directions.Up;
                                    Moved = false;
                                    SendProjectileAttackNpc(c_Client, c_MoveMap, slot, c, 1);
                                    return;
                                }
                            }
                        }
                        Y -= 1;
                        Direction = (int)Directions.Up;
                        Moved = true;
                    }
                    else
                    {
                        Direction = (int)Directions.Down;
                        Moved = false;
                        SendClearProjectile(c_Client, c_MoveMap, slot);
                        return;
                    }
                    break;
            }
            if (Moved == true)
            {
                Moved = false;
            }
        }

        void SendClearProjectile(NetClient c_Client, Map c_Map, int slot)
        {
            NetOutgoingMessage outMSG = c_Client.CreateMessage();
            outMSG.Write((byte)PacketTypes.ClearProj);
            outMSG.WriteVariableInt32(slot);
            outMSG.WriteVariableInt32(Owner);
            c_Client.SendMessage(outMSG, c_Client.ServerConnection, NetDeliveryMethod.ReliableSequenced, 2);
            c_Map.m_MapProj[slot] = null;
        }

        void SendProjectileAttackNpc(NetClient c_Client, Map c_Map, int slot, int npcNum, int spawntype)
        {
            NetOutgoingMessage outMSG = c_Client.CreateMessage();
            outMSG.Write((byte)PacketTypes.AttackNpcProj);
            outMSG.WriteVariableInt32(slot);
            outMSG.WriteVariableInt32(npcNum);
            outMSG.WriteVariableInt32(Owner);
            outMSG.WriteVariableInt32(spawntype);
            c_Client.SendMessage(outMSG, c_Client.ServerConnection, NetDeliveryMethod.ReliableSequenced, 1);
            c_Map.m_MapProj[slot] = null;
        }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            int rndX = RND.Next(0, 10);
            int rndY = RND.Next(0, 10);

            int x = (X * 32) + rndX;
            int y = (Y * 32) + rndY;
            int dir = (Direction * 32);
            spritePic[0] = new Vertex(new Vector2f(x, y), new Vector2f(dir, 0));
            spritePic[1] = new Vertex(new Vector2f(x + 32, y), new Vector2f(dir + 32, 0));
            spritePic[2] = new Vertex(new Vector2f(x + 32, y + 32), new Vector2f(dir + 32, 32));
            spritePic[3] = new Vertex(new Vector2f(x, y + 32), new Vector2f(dir, 32));

            states.Texture = proj_Texture[Sprite - 1];
            target.Draw(spritePic, states);
        }
    }

    enum ProjType
    {
        Bullet,
        Thrown,
        Explosive
    }
}