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
            LoadViews();
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

        private void btnShopEditor_Click(object sender, EventArgs e)
        {
            ShopEditor e_ShopEditor = new ShopEditor();
            e_ShopEditor.Show();
        }

        private void btnChatEditor_Click(object sender, EventArgs e)
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

        private void btnIMGSplit_Click(object sender, EventArgs e)
        {
            ImageSplitter imgSplit = new ImageSplitter();
            imgSplit.Show();
        }

        private void lstViews_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtViewGrid.DataSource = null;
            dtViewGrid.Rows.Clear();
            string view = lstViews.GetItemText(lstViews.SelectedItem);
            string script = "SELECT * FROM " + view;
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

        private void btnCloseWindow_Click(object sender, EventArgs e)
        {
            if (pnlEditorsViews.Visible)
            {
                showHideViewWindowToolStripMenuItem.Checked = false;
                pnlEditorsViews.Visible = false;
            }
        }

        private void showHideViewWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (showHideViewWindowToolStripMenuItem.Checked)
            {
                showHideViewWindowToolStripMenuItem.Checked = false;
                pnlEditorsViews.Visible = false;
            }
            else
            {
                showHideViewWindowToolStripMenuItem.Checked = true;
                pnlEditorsViews.Visible = true;
            }
        }

        private void btnCloseEditors_Click(object sender, EventArgs e)
        {
            if (pnlEditors.Visible)
            {
                showHideEditorsToolStripMenuItem.Checked = false;
                pnlEditors.Visible = false;
            }
        }

        private void showHideEditorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (showHideEditorsToolStripMenuItem.Checked)
            {
                showHideEditorsToolStripMenuItem.Checked = false;
                pnlEditors.Visible = false;
            }
            else
            {
                showHideEditorsToolStripMenuItem.Checked = true;
                pnlEditors.Visible = true;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void LoadViews()
        {
            string script = "SELECT Name FROM SYS.views ORDER BY Name ASC";
            string connection = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";Integrated Security=True";
            SqlConnection c = new SqlConnection(connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(script, c);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
            DataTable ds = new DataTable();
            dataAdapter.Fill(ds);
            lstViews.DisplayMember = "Name";
            lstViews.DataSource = ds;
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
