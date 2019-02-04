using Lidgren.Network;
using SFML.Graphics;
using SFML.System;
using System;
using static SabertoothClient.Client;
using static SabertoothClient.Globals;
using System.IO;

namespace SabertoothClient
{
    public class Projectile : Drawable
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
        static int maxprojSprites = Directory.GetFiles("Resources/Projectiles/", "*", SearchOption.TopDirectoryOnly).Length;
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

        public void CheckMovment(int slot)
        {
            if (Moved == true) { Moved = false; return; }
            if (map.m_MapProj[slot] == null) { return; }

            if (RangeCounter > Range) { Direction = (int)Directions.Down; Moved = false; SendClearProjectile(slot); return; }

            switch (map.m_MapProj[slot].Direction)
            {
                case (int)Directions.Down:
                    if (Y < 49)
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
                    if (X < 49)
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
            NetOutgoingMessage outMSG = SabertoothClient.netClient.CreateMessage();
            outMSG.Write((byte)PacketTypes.ClearProj);
            outMSG.WriteVariableInt32(slot);
            outMSG.WriteVariableInt32(Owner);
            SabertoothClient.netClient.SendMessage(outMSG, SabertoothClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
            map.m_MapProj[slot] = null;
        }

        void SendProjectileAttackNpc(int slot, int npcNum, int spawntype)
        {
            NetOutgoingMessage outMSG = SabertoothClient.netClient.CreateMessage();
            outMSG.Write((byte)PacketTypes.AttackNpcProj);
            outMSG.WriteVariableInt32(slot);
            outMSG.WriteVariableInt32(npcNum);
            outMSG.WriteVariableInt32(Owner);
            outMSG.WriteVariableInt32(spawntype);
            SabertoothClient.netClient.SendMessage(outMSG, SabertoothClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
            map.m_MapProj[slot] = null;
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

            int range = 0;
            if (Sprite >= maxprojSprites) { range = 1; }
            states.Texture = proj_Texture[Sprite - range];
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