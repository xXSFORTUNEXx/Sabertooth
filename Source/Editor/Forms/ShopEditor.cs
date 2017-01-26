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
using static System.Convert;

namespace Editor.Forms
{
    public partial class ShopEditor : Form
    {
        Shop e_Shop = new Shop();
        int SelectedIndex;
        bool UnModSave;

        public ShopEditor()
        {
            InitializeComponent();
        }

        private void LoadItemList()
        {

            using (SQLiteConnection conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
            {
                conn.Open();
                string sql;

                sql = "SELECT COUNT(*) FROM SHOPS";

                object queue;
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    queue = cmd.ExecuteScalar();
                }
                int result = ToInt32(queue);
                lstIndex.Items.Clear();
                for (int i = 0; i < result; i++)
                {
                    e_Shop.LoadShopNameFromDatabase(i + 1);
                    lstIndex.Items.Add(e_Shop.Name);
                }
            }
        }

        private void lstIndex_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
