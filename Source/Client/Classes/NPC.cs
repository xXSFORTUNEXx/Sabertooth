using SFML.Graphics;
using SFML.System;
using System;

namespace Client.Classes
{
    class Npc : Drawable
    {
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Range { get; set; }
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
        public bool IsSpawned { get; set; }
        const int spriteTextures = 8;
        Texture[] c_Sprite = new Texture[spriteTextures];
        VertexArray spritePic = new VertexArray(PrimitiveType.Quads, 4);
        VertexArray healthBar = new VertexArray(PrimitiveType.Quads, 4);
        float barLength;

        public Npc()
        {
            for (int i = 0; i < spriteTextures; i++)
            {
                c_Sprite[i] = new Texture("Resources/Characters/" + (i + 1) + ".png");
            }
        }

        public Npc(string name, int x, int y, int direction, int sprite, int step, int owner, int behavior, int spawnTime, int health, int maxHealth, int damage, int desx, int desy, int exp, int money, int range)
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
            Range = range;
            for (int i = 0; i < spriteTextures; i++)
            {
                c_Sprite[i] = new Texture("Resources/Characters/" + (i + 1) + ".png");
            }
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
            Range = 0;
            for (int i = 0; i < spriteTextures; i++)
            {
                c_Sprite[i] = new Texture("Resources/Characters/" + (i + 1) + ".png");
            }
        }

        public virtual void Draw(RenderTarget target, RenderStates state)
        {
            int x = (X * 32);
            int y = (Y * 32) - 16;
            int step = (Step * 32);
            int dir = (Direction * 48);
            spritePic[0] = new Vertex(new Vector2f(x, y), new Vector2f(step, dir));
            spritePic[1] = new Vertex(new Vector2f(x + 32, y), new Vector2f(step + 32, dir));
            spritePic[2] = new Vertex(new Vector2f(x + 32, y + 48), new Vector2f(step + 32, dir + 48));
            spritePic[3] = new Vertex(new Vector2f(x, y + 48), new Vector2f(step, dir + 48));

            barLength = ((float)Health / MaxHealth) * 35;

            x = (X * 32);
            y = (Y * 32) - 20;
            healthBar[0] = new Vertex(new Vector2f(x, y), Color.Red);
            healthBar[1] = new Vertex(new Vector2f(barLength + x, y), Color.Red);
            healthBar[2] = new Vertex(new Vector2f(barLength + x, y + 5), Color.Red);
            healthBar[3] = new Vertex(new Vector2f(x, y + 5), Color.Red);

            state.Texture = c_Sprite[Sprite - 1];
            target.Draw(spritePic, state);
            target.Draw(healthBar);
        }
    }

    public enum BehaviorType
    {
        Friendly,
        Passive,
        Aggressive,
        ToLocation
    }
}
