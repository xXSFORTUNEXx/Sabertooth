using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SabertoothClient.Globals;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System.IO;

namespace SabertoothClient
{
    public class Animation : Drawable
    {
        public string Name { get; set; }    //name of animation

        public int SpriteNumber { get; set; }   //the graphic number for the animation
        public int FrameCountH { get; set; }    //Horizontal frame count
        public int FrameCountV { get; set; }    //Verticle frame count
        public int FrameCount { get; set; } //total frame count
        public int FrameDuration { get; set; }  //the duration of each framm in miliseconds
        public int LoopCount { get; set; }  //how many times the full frame count will loop

        public bool RenderBelowTarget { get; set; } //set the render below, right now they will default to above the target below the first fringe layer     

        static int animTextures = Directory.GetFiles("Resources/Animations/", "*", SearchOption.TopDirectoryOnly).Length;   //count the textures

        VertexArray animPic = new VertexArray(PrimitiveType.Quads, 4);  //setup the vertex array for later
        Texture[] animSprite = new Texture[animTextures];   //create textures for all the sprites loaded

        //Client side stuff
        public int X { get; set; }  //Animations X location
        public int Y { get; set; }  //animations Y location

        public Animation()
        {
            for (int i = 0; i < animTextures; i++)
            {
                animSprite[i] = new Texture("Resources/Animation/" + (i + 1) + ".png");
            }
        }

        public Animation(string name,
                int spritenumber, int fcounth, int fcountv, int fcount, int fduration, int loopcount,
                bool rbelowtarget)
        {
            Name = name;

            SpriteNumber = spritenumber;
            FrameCountH = fcounth;
            FrameCountV = fcountv;
            FrameCount = fcount;
            FrameDuration = fduration;
            LoopCount = loopcount;

            RenderBelowTarget = rbelowtarget;

            for (int i = 0; i < animTextures; i++)
            {
                animSprite[i] = new Texture("Resources/Animations/" + (i + 1) + ".png");
            }
        }

        public virtual void Draw(RenderTarget target, RenderStates state)
        {

        }
    }
}
