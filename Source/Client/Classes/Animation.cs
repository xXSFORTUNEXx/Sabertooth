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
using static System.Environment;

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


        public Animation()
        {

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
        }

        public virtual void Draw(RenderTarget target, RenderStates states) { }
    }
}
