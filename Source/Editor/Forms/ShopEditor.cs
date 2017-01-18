using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Editor.Classes;
using System.Data.SQLite;

namespace Editor.Forms
{
    public partial class ShopEditor : Form
    {
        SQLiteConnection e_Database;
        Shop e_Shop = new Shop();
        int SelectedIndex;
        bool UnModSave;

        public ShopEditor()
        {
            InitializeComponent();
        }

        private void LoadItemList()
        {
            e_Database = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;");
            e_Database.Open();
            string sql;

            sql = "SELECT COUNT(*) FROM SHOPS";

            SQLiteCommand sql_Command = new SQLiteCommand(sql, e_Database);
            int result = int.Parse(sql_Command.ExecuteScalar().ToString());
            e_Database.Close();
            lstIndex.Items.Clear();
            for (int i = 0; i < result; i++)
            {
                e_Shop.LoadShopNameFromDatabase(i + 1);
                lstIndex.Items.Add(e_Shop.Name);
            }
        }

        private void lstIndex_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
