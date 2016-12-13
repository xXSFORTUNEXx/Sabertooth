using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace Editor.Forms
{
    public partial class MapEditor : Form
    {
        DrawingSurface e_DrawSurf = new DrawingSurface();

        public MapEditor()
        {
            InitializeComponent();
            Visible = true;
            MapLoop();
        }

        void MapLoop()
        {
            e_DrawSurf.Size = new Size(800, 600);
            Controls.Add(e_DrawSurf);
            e_DrawSurf.Location = new Point(172, 12);
            RenderWindow e_Window = new RenderWindow(e_DrawSurf.Handle);
            
            while (Visible)
            {
                Application.DoEvents();
                e_Window.DispatchEvents();
                e_Window.Clear(SFML.Graphics.Color.Yellow);
                e_Window.Display();
            }
            Visible = false;
        }
    }

    public class DrawingSurface : Control
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            // don't call base.OnPaint(e) to prevent forground painting
            // base.OnPaint(e);
        }
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // don't call base.OnPaintBackground(e) to prevent background painting
            //base.OnPaintBackground(pevent);
        }
    }
}
