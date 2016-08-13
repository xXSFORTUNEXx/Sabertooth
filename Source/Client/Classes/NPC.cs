using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using Lidgren.Network;

namespace Client.Classes
{
    class NPC
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

        //Only for live applications, no editors
        public bool isSpawned;

        Sprite svrSprite = new Sprite();

        public NPC() { }

        public NPC(string name, int x, int y, int direction, int sprite, int step, int owner, int behavior, int spawnTime)
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

        public NPC(int x, int y)
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

        public void DrawNpc(RenderWindow svrWindow, Texture svrTexture)
        {
            svrSprite.Texture = svrTexture;
            svrSprite.TextureRect = new IntRect((Step * 32), (Direction * 32), 32, 48);
            svrSprite.Position = new Vector2f((X * 32), ((Y * 32) - 16));

            svrWindow.Draw(svrSprite);
        }

        public enum BehaviorType
        {
            Friendly,
            Passive,
            Aggressive
        }
    }
}
