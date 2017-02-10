using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Editor.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Data.SQLite;
using SFML.Graphics;
using SFML.Window;
using static System.Convert;
using static System.IO.Directory;

namespace Editor
{
    public partial class Editor : Form
    {
        public Editor()
        {
            InitializeComponent();
            CheckDirectories();
        }

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            string unlockPass = txtUnlock.Text;
            if (unlockPass == "fortune") { pnlLock.Visible = false; }
            else { lblIncorrect.Visible = true; }
        }

        private void CheckDirectories()
        {
            if (!Exists("Database"))
            {
                CreateDirectory("Database");
                Server.Classes.StartUp.CreateDatabase();
            }
        }

        private void btnItemEditor_Click(object sender, EventArgs e)
        {
            ItemEditor e_ItemEditor = new ItemEditor();
            e_ItemEditor.ShowDialog();
        }

        private void btnNpcEditor_Click(object sender, EventArgs e)
        {
            NpcEditor e_NpcEditor = new NpcEditor();
            e_NpcEditor.ShowDialog();
        }

        private void btnMapEditor_Click(object sender, EventArgs e)
        {
            MapEditor e_MapEditor = new MapEditor();
        }

        private void btnProjectileEditor_Click(object sender, EventArgs e)
        {
            ProjectileEditor e_ProjEditor = new ProjectileEditor();
            e_ProjEditor.ShowDialog();
        }

        private void btnShopEditor_Click(object sender, EventArgs e)
        {
            ShopEditor e_ShopEditor = new ShopEditor();
            e_ShopEditor.Show();
        }

        private void btnEditor_Click(object sender, EventArgs e)
        {
            ChatEditor e_ChatEditor = new ChatEditor();
            e_ChatEditor.Show();
        }
    }
}
