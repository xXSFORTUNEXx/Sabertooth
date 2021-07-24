using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SabertoothClient.Globals;
using SFML.Graphics;
using static SabertoothClient.Client;
using SFML.Window;
using SFML.System;
using System.IO;
using static System.Environment;

namespace SabertoothClient
{
    public class Spell
    {
        public string Name { get; set; }

        public int ID { get; set; }
        public int Level { get; set; }
        public int Icon { get; set; }
        public int Vital { get; set; } //was orig damage but vital makes more since cause it can be damage or health for hots/dots
        public int HealthCost { get; set; } //this is if a spell cost health instead of mana to cast (not sure about this yet)
        public int ManaCost { get; set; }   //probably the norm resource for casting spells/buffs
        public int CoolDown { get; set; }   //time it takes before the spell can be casted again
        public int CastTime { get; set; }   //time it takes to cast the spell
        public int Charges { get; set; }    //you can learn a spell with charges and restore the charges at a well or something
        public int TotalTick { get; set; } //Total amount of time for a hot/dot
        public int TickInterval { get; set; }   //how often the dot/hot should tick damage/heal
        public int SpellType { get; set; }
        public int Range { get; set; }
        public int Anim { get; set; }        
        public bool AOE { get; set; }   //area of effect
        public int Distance { get; set; }
        public bool Projectile { get; set; }
        public int Sprite { get; set; }
        public bool SelfCast { get; set; }

        public Spell() { }

        public Spell(string name,
            int icon, int level, int vital, int hpcost, int mpcost, int cooldown, int casttime, int charges, int totaltick, int tickint, int spelltype, 
            int range, bool aoe, int distance, bool proj, int sprite, bool selfcast, int anim)
        {
            Name = name;

            Level = level;
            Icon = icon;
            Vital = vital;
            HealthCost = hpcost;
            ManaCost = mpcost;
            CoolDown = cooldown;
            CastTime = casttime;
            Charges = charges;
            TotalTick = totaltick;
            TickInterval = tickint;
            SpellType = spelltype;
            Range = range;
            Anim = anim;
            AOE = aoe;
            Distance = distance;
            Projectile = proj;
            Sprite = sprite;
            SelfCast = selfcast;
        }
    }

    public class SpellAnimation : Animation
    {
        static int animTextures = Directory.GetFiles("Resources/Animation/", "*", SearchOption.TopDirectoryOnly).Length;   //count the textures

        VertexArray animPic = new VertexArray(PrimitiveType.Quads, 4);
        Texture[] animSprite = new Texture[animTextures];
        Texture[] finalTexture;
        public int currentFrame;
        int animTick;
        public int X { get; set; }
        public int Y { get; set; }

        public SpellAnimation()
        {
            for (int i = 0; i < animTextures; i++)
            {
                animSprite[i] = new Texture("Resources/Animation/" + (i + 1) + ".png");
            }
            currentFrame = 0;
        }

        public void ConfigAnimation()
        {
            try
            {
                Texture anim = animSprite[SpriteNumber - 1];
                finalTexture = new Texture[FrameCount];
                int currentCount = 0;
                int xpic = (int)anim.Size.X / FrameCountV;
                int ypic = (int)anim.Size.Y / FrameCountH;

                for (int i = 0; i < FrameCountH; i++)
                {
                    for (int j = 0; j < FrameCountV; j++)
                    {
                        Image animImg = anim.CopyToImage();
                        finalTexture[currentCount] = new Texture(animImg, new IntRect((j * xpic), (i * ypic), xpic, ypic));
                        currentCount += 1;
                    }
                }
            }
            catch
            {
                Logging.WriteMessageLog("Error loading texture");
            }
        }

        void CheckCurrentFrame()
        {
            if (TickCount - animTick > FrameDuration)
            {
                currentFrame += 1;

                if (currentFrame >= FrameCount)
                {
                    currentFrame = 0;
                    Name = "NONE";
                    return;
                }                
                animTick = TickCount;
            }
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            if (SpriteNumber > 0)
            {
                int x = (X * PIC_X);   //16
                int y = (Y * PIC_Y) - 32;  //96

                CheckCurrentFrame();

                int xpic = (int)finalTexture[currentFrame].Size.X;
                int ypic = (int)finalTexture[currentFrame].Size.Y;

                animPic[0] = new Vertex(new Vector2f(x, y), new Vector2f(0, 0));
                animPic[1] = new Vertex(new Vector2f(x + xpic, y), new Vector2f(xpic, 0));
                animPic[2] = new Vertex(new Vector2f(x + xpic, y + ypic), new Vector2f(xpic, ypic));
                animPic[3] = new Vertex(new Vector2f(x, y + ypic), new Vector2f(0, ypic));

                states.Texture = finalTexture[currentFrame];
                target.Draw(animPic, states);
            }
        }
    }

    public enum SpellType : int
    {
        None,
        Damage,
        Heal,
        Buff,
        Debuff,
        Dash,
        Shield
    }
}
