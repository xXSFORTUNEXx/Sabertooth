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
                            SendClearProjectile(c_Client, c_MoveMap, slot);
                            return;
                        }
                        for (int i = 0; i < 10; i++)
                        {
                            if (c_MoveMap.m_MapNpc[i].IsSpawned)
                            {
                                if (c_MoveMap.m_MapNpc[i].X == X && c_MoveMap.m_MapNpc[i].Y == (Y + 1))
                                {
                                    if (c_MoveMap.m_MapNpc[i].Behavior == (int)Npc.BehaviorType.Friendly || c_MoveMap.m_MapNpc[i].Behavior == (int)Npc.BehaviorType.Passive)
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
                        if (c_MoveMap.Ground[(X - 1), Y].type == (int)TileType.Blocked)
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
                                    if (c_MoveMap.m_MapNpc[i].Behavior == (int)Npc.BehaviorType.Friendly || c_MoveMap.m_MapNpc[i].Behavior == (int)Npc.BehaviorType.Passive)
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
                        if (c_MoveMap.Ground[(X + 1), Y].type == (int)TileType.Blocked)
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
                                    if (c_MoveMap.m_MapNpc[i].Behavior == (int)Npc.BehaviorType.Friendly || c_MoveMap.m_MapNpc[i].Behavior == (int)Npc.BehaviorType.Passive)
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
                        if (c_MoveMap.Ground[X, (Y - 1)].type == (int)TileType.Blocked)
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
                                    if (c_MoveMap.m_MapNpc[i].Behavior == (int)Npc.BehaviorType.Friendly || c_MoveMap.m_MapNpc[i].Behavior == (int)Npc.BehaviorType.Passive)
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
            c_Client.SendMessage(outMSG, c_Client.ServerConnection, NetDeliveryMethod.ReliableSequenced, 13);
            c_Map.mapProj[slot] = null;
        }

        void SendProjectileAttackNpc(NetClient c_Client, Map c_Map, int slot, int npcNum, int spawntype)
        {
            NetOutgoingMessage outMSG = c_Client.CreateMessage();
            outMSG.Write((byte)PacketTypes.AttackNpcProj);
            outMSG.WriteVariableInt32(slot);
            outMSG.WriteVariableInt32(npcNum);
            outMSG.WriteVariableInt32(Owner);
            outMSG.WriteVariableInt32(spawntype);
            c_Client.SendMessage(outMSG, c_Client.ServerConnection, NetDeliveryMethod.ReliableSequenced, 9);
            c_Map.mapProj[slot] = null;
        }
    }

    enum ProjType
    {
        Bullet,
        Thrown,
        Explosive
    }
}