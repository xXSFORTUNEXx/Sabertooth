using System;
using System.Windows.Forms;
using Editor.Forms;
using static System.IO.Directory;
using static System.Windows.Forms.Application;
using static SabertoothServer.Server;
using System.Data.SqlClient;
using System.Data;

namespace Editor
{
    public partial class Editor : Form
    {
        

        public Editor()
        {
            InitializeComponent();
            LoadConfiguration();
            CheckSQLConnection();

            string script = "SELECT Name from SYS.views";
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            SqlConnection c = new SqlConnection(connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(script, c);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
            DataTable ds = new DataTable();
            dataAdapter.Fill(ds);
            lstViews.DisplayMember = "Name";
            lstViews.DataSource = ds;
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
            e_ItemEditor.Show();
        }

        private void btnNpcEditor_Click(object sender, EventArgs e)
        {
            NpcEditor e_NpcEditor = new NpcEditor();
            e_NpcEditor.Show();
        }

        private void btnMapEditor_Click(object sender, EventArgs e)
        {
            MapEditor e_MapEditor = new MapEditor();
        }

        private void btnProjectileEditor_Click(object sender, EventArgs e)
        {
            ProjectileEditor e_ProjEditor = new ProjectileEditor();
            e_ProjEditor.Show();
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

        private void btnChestEditor_Click(object sender, EventArgs e)
        {
            ChestEditor e_ChestEditor = new ChestEditor();
            e_ChestEditor.Show();
        }

        private void btnPlayerEditor_Click(object sender, EventArgs e)
        {
            PlayerEditor e_PlayerEditor = new PlayerEditor();
            e_PlayerEditor.Show();
        }

        private void btnQuestEditor_Click(object sender, EventArgs e)
        {
            QuestEditor e_QuestEditor = new QuestEditor();
            e_QuestEditor.Show();
        }

        private void lstViews_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtViewGrid.DataSource = null;
            dtViewGrid.Rows.Clear();
            string view = lstViews.GetItemText(lstViews.SelectedItem);
            string script = "SELECT * from " + view;
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            SqlConnection c = new SqlConnection(connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(script, c);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
            DataTable ds = new DataTable();
            dataAdapter.Fill(ds);
            dtViewGrid.ReadOnly = true;
            dtViewGrid.DataSource = ds;
            dtViewGrid.AutoResizeColumns();
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
