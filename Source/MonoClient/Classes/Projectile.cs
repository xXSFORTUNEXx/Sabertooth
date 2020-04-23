using Lidgren.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using static Mono_Client.Client;
using static Mono_Client.Globals;
using System.IO;

namespace Mono_Client
{
    public class Projectile
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
        Random RND = new Random();

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

        public void CheckMovment(int slot)
        {
            if (Moved == true) { Moved = false; return; }
            if (map.m_MapProj[slot] == null) { return; }

            if (RangeCounter > Range) { Direction = (int)Directions.Down; Moved = false; SendClearProjectile(slot); return; }

            switch (map.m_MapProj[slot].Direction)
            {
                case (int)Directions.Down:
                    if (Y < map.MaxY - 1)
                    {
                        if (map.Ground[X, (Y + 1)].Type == (int)TileType.Blocked)
                        {
                            Direction = (int)Directions.Down;
                            Moved = false;
                            SendClearProjectile(slot);
                            return;
                        }
                        for (int i = 0; i < 10; i++)
                        {
                            if (map.m_MapNpc[i].IsSpawned)
                            {
                                if (map.m_MapNpc[i].X == X && map.m_MapNpc[i].Y == (Y + 1))
                                {
                                    if (map.m_MapNpc[i].Behavior == (int)BehaviorType.Friendly || map.m_MapNpc[i].Behavior == (int)BehaviorType.Passive)
                                    {
                                        Direction = (int)Directions.Down;
                                        Moved = false;
                                        SendClearProjectile(slot);
                                        return;
                                    }
                                    Direction = (int)Directions.Down;
                                    Moved = false;
                                    SendProjectileAttackNpc(slot, i, 0);
                                    return;
                                }
                            }
                        }
                        for (int c = 0; c < 20; c++)
                        {
                            if (map.r_MapNpc[c].IsSpawned)
                            {
                                if (map.r_MapNpc[c].X == X && map.r_MapNpc[c].Y == (Y + 1))
                                {
                                    Direction = (int)Directions.Down;
                                    Moved = false;
                                    SendProjectileAttackNpc(slot, c, 1);
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
                        SendClearProjectile(slot);
                        return;
                    }
                    break;

                case (int)Directions.Left:
                    if (X > 1)
                    {
                        if (map.Ground[(X - 1), Y].Type == (int)TileType.Blocked)
                        {
                            Direction = (int)Directions.Left;
                            Moved = false;
                            SendClearProjectile(slot);
                            return;
                        }
                        for (int i = 0; i < 10; i++)
                        {
                            if (map.m_MapNpc[i].IsSpawned)
                            {
                                if (map.m_MapNpc[i].X == (X - 1) && map.m_MapNpc[i].Y == Y)
                                {
                                    if (map.m_MapNpc[i].Behavior == (int)BehaviorType.Friendly || map.m_MapNpc[i].Behavior == (int)BehaviorType.Passive)
                                    {
                                        Direction = (int)Directions.Down;
                                        Moved = false;
                                        SendClearProjectile(slot);
                                        return;
                                    }
                                    Direction = (int)Directions.Left;
                                    Moved = false;
                                    SendProjectileAttackNpc(slot, i, 0);
                                    return;
                                }
                            }
                        }
                        for (int c = 0; c < 20; c++)
                        {
                            if (map.r_MapNpc[c].IsSpawned)
                            {
                                if (map.r_MapNpc[c].X == (X - 1) && map.r_MapNpc[c].Y == Y)
                                {
                                    Direction = (int)Directions.Left;
                                    Moved = false;
                                    SendProjectileAttackNpc(slot, c, 1);
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
                        SendClearProjectile(slot);
                        return;
                    }
                    break;

                case (int)Directions.Right:
                    if (X < map.MaxX - 1)
                    {
                        if (map.Ground[(X + 1), Y].Type == (int)TileType.Blocked)
                        {
                            Direction = (int)Directions.Right;
                            Moved = false;
                            SendClearProjectile(slot);
                            return;
                        }
                        for (int i = 0; i < 10; i++)
                        {
                            if (map.m_MapNpc[i].IsSpawned)
                            {
                                if (map.m_MapNpc[i].X == (X + 1) && map.m_MapNpc[i].Y == Y)
                                {
                                    if (map.m_MapNpc[i].Behavior == (int)BehaviorType.Friendly || map.m_MapNpc[i].Behavior == (int)BehaviorType.Passive)
                                    {
                                        Direction = (int)Directions.Down;
                                        Moved = false;
                                        SendClearProjectile(slot);
                                        return;
                                    }
                                    Direction = (int)Directions.Right;
                                    Moved = false;
                                    SendProjectileAttackNpc(slot, i, 0);
                                    return;
                                }
                            }
                        }
                        for (int c = 0; c < 20; c++)
                        {
                            if (map.r_MapNpc[c].IsSpawned)
                            {
                                if (map.r_MapNpc[c].X == (X + 1) && map.r_MapNpc[c].Y == Y)
                                {
                                    Direction = (int)Directions.Right;
                                    Moved = false;
                                    SendProjectileAttackNpc(slot, c, 1);
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
                        SendClearProjectile(slot);
                        return;
                    }
                    break;

                case (int)Directions.Up:
                    if (Y > 1)
                    {
                        if (map.Ground[X, (Y - 1)].Type == (int)TileType.Blocked)
                        {
                            Direction = (int)Directions.Up;
                            Moved = false;
                            SendClearProjectile(slot);
                            return;
                        }
                        for (int i = 0; i < 10; i++)
                        {
                            if (map.m_MapNpc[i].IsSpawned)
                            {
                                if (map.m_MapNpc[i].X == X && map.m_MapNpc[i].Y == (Y - 1))
                                {
                                    if (map.m_MapNpc[i].Behavior == (int)BehaviorType.Friendly || map.m_MapNpc[i].Behavior == (int)BehaviorType.Passive)
                                    {
                                        Direction = (int)Directions.Down;
                                        Moved = false;
                                        SendClearProjectile(slot);
                                        return;
                                    }
                                    Direction = (int)Directions.Up;
                                    Moved = false;
                                    SendProjectileAttackNpc(slot, i, 0);
                                    return;
                                }
                            }
                        }
                        for (int c = 0; c < 20; c++)
                        {
                            if (map.r_MapNpc[c].IsSpawned)
                            {
                                if (map.r_MapNpc[c].X == X && map.r_MapNpc[c].Y == (Y - 1))
                                {                                    
                                    Direction = (int)Directions.Up;
                                    Moved = false;
                                    SendProjectileAttackNpc(slot, c, 1);
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
                        SendClearProjectile(slot);
                        return;
                    }
                    break;
            }
            if (Moved == true)
            {
                Moved = false;
            }
        }

        void SendClearProjectile(int slot)
        {
            NetOutgoingMessage outMSG = MonoClient.netClient.CreateMessage();
            outMSG.Write((byte)PacketTypes.ClearProj);
            outMSG.WriteVariableInt32(slot);
            outMSG.WriteVariableInt32(Owner);
            MonoClient.netClient.SendMessage(outMSG, MonoClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
            map.m_MapProj[slot] = null;
        }

        void SendProjectileAttackNpc(int slot, int npcNum, int spawntype)
        {
            NetOutgoingMessage outMSG = MonoClient.netClient.CreateMessage();
            outMSG.Write((byte)PacketTypes.AttackNpcProj);
            outMSG.WriteVariableInt32(slot);
            outMSG.WriteVariableInt32(npcNum);
            outMSG.WriteVariableInt32(Owner);
            outMSG.WriteVariableInt32(spawntype);
            MonoClient.netClient.SendMessage(outMSG, MonoClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
            map.m_MapProj[slot] = null;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            int rndX = RND.Next(0, 10);
            int rndY = RND.Next(0, 10);

            int x = (X * 32) + rndX;
            int y = (Y * 32) + rndY;
            int dir = (Direction * 32);

            int range = 0;
            if (Sprite >= maxprojSprites) { range = 1; }
            spriteBatch.Draw(proj_Texture[Sprite - range], new Rectangle((x * 32), (y * 32), 32, 32), new Rectangle(0, dir, 32, 32), Color.White);
        }
    }

    enum ProjType
    {
        Bullet,
        Thrown,
        Explosive
    }
}