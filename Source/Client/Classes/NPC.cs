using SFML.Graphics;
using SFML.System;

namespace Client.Classes
{
    class Npc
    {
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Direction { get; set; }
        public int Sprite { get; set; }
        public int Step { get; set; }
        public int Owner { get; set; }
        public int Behavior { get; set; }
        public int SpawnTime { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Damage { get; set; }
        public int DesX { get; set; }
        public int DesY { get; set; }
        public int Exp { get; set; }
        public int Money { get; set; }
        public bool isSpawned;

        Sprite c_Sprite = new Sprite();

        public Npc() { }

        public Npc(string name, int x, int y, int direction, int sprite, int step, int owner, int behavior, int spawnTime, int health, int maxHealth, int damage, int desx, int desy, int exp, int money)
        {
            Name = name;
            X = x;
            Y = y;
            Direction = direction;
            Sprite = sprite;
            Step = step;
            Owner = owner;
            Behavior = behavior;
            SpawnTime = spawnTime;
            Health = health;
            MaxHealth = maxHealth;
            Damage = damage;
            DesX = desx;
            DesY = desy;
            Exp = exp;
            Money = money;
        }

        public Npc(int x, int y)
        {
            Name = "Default";
            X = x;
            Y = y;
            Direction = (int)Directions.Down;
            Sprite = 0;
            Step = 0;
            Owner = 0;
            Behavior = (int)BehaviorType.Friendly;
            SpawnTime = 5000;
            Health = 100;
            MaxHealth = 100;
            Damage = 10;
            DesX = 0;
            DesY = 0;
            Exp = 0;
            Money = 0;
        }

        public void DrawNpc(RenderWindow c_Window, Texture c_Texture)
        {
            c_Sprite.Texture = c_Texture;
            c_Sprite.TextureRect = new IntRect((Step * 32), (Direction * 48), 32, 48);
            c_Sprite.Position = new Vector2f((X * 32), ((Y * 32) - 16));

            c_Window.Draw(c_Sprite);
        }

        public enum BehaviorType
        {
            Friendly,
            Passive,
            Aggressive,
            ToLocation
        }
    }
}
