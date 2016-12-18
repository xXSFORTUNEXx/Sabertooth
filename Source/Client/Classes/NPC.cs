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
        public int maxHealth { get; set; }
        public int Damage { get; set; }
        //Only for live applications, no editors
        public bool isSpawned;

        Sprite c_Sprite = new Sprite();

        public Npc() { }

        public Npc(string name, int x, int y, int direction, int sprite, int step, int owner, int behavior, int spawnTime)
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
            Aggressive
        }
    }
}
