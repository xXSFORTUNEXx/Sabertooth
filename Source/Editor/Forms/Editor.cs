using System;
using System.Windows.Forms;
using Editor.Forms;
using static System.IO.Directory;
using static System.Windows.Forms.Application;
using static SabertoothServer.Server;

namespace Editor
{
    public partial class Editor : Form
    {
        public Editor()
        {
            InitializeComponent();
            LoadConfiguration();
            CheckSQLConnection();
        }

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            string unlockPass = txtUnlock.Text;
            if (unlockPass == "fortune") { pnlLock.Visible = false; }
            else { lblIncorrect.Visible = true; }
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
            e_ShopEditor.ShowDialog();
        }

        private void btnEditor_Click(object sender, EventArgs e)
        {
            ChatEditor e_ChatEditor = new ChatEditor();
            e_ChatEditor.ShowDialog();
        }

        private void btnChestEditor_Click(object sender, EventArgs e)
        {
            ChestEditor e_ChestEditor = new ChestEditor();
            e_ChestEditor.ShowDialog();
        }

        private void btnPlayerEditor_Click(object sender, EventArgs e)
        {
            PlayerEditor e_PlayerEditor = new PlayerEditor();
            e_PlayerEditor.ShowDialog();
        }
    }
    static class StartUp
    {
        [STAThread]
        static void Main()
        {
            EnableVisualStyles();
            SetCompatibleTextRenderingDefault(false);
            Run(new Editor());
        }
    }

}
