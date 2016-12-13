using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace Editor.Forms
{
    public partial class MapEditor : Form
    {
        public MapEditor()
        {
            InitializeComponent();
            MapLoop();
        }

        void MapLoop()
        {
            RenderWindow e_Window = new RenderWindow(new VideoMode(800, 600), "Map");
            e_Window.Closed += new EventHandler(onClose);

            while (Visible)
            {
                Application.DoEvents();
                e_Window.DispatchEvents();
                e_Window.Clear(SFML.Graphics.Color.Yellow);
                e_Window.Display();
            }
        }

        static void onClose(object sender, EventArgs e)
        {
            RenderWindow editorWindow = (RenderWindow)sender;
            editorWindow.Close();
        }
    }
}
