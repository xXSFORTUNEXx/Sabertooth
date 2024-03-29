﻿using SabertoothServer;
using System;
using System.Windows.Forms;
using static System.Convert;
using System.Data.SqlClient;
using static SabertoothServer.Globals;

namespace Editor.Forms
{
    public partial class ShopEditor : Form
    {
        Shop e_Shop = new Shop();
        Item s_Item = new Item();
        int SelectedIndex;
        bool UnModSave;

        public ShopEditor()
        {
            InitializeComponent();
            LoadItemList();
            LoadItemCombo();
        }

        private void LoadItemList()
        {
            string connection = "Data Source=" + Server.sqlServer + ";Initial Catalog=" + Server.sqlDatabase + ";Integrated Security=True";
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                string command = "SELECT COUNT(*) FROM SHOPS";
                using (SqlCommand cmd = new SqlCommand(command, sql))
                {
                    object count = cmd.ExecuteScalar();
                    int result = ToInt32(count);
                    lstIndex.Items.Clear();
                    for (int i = 0; i < result; i++)
                    {
                        e_Shop.LoadShopNameFromDatabase(i + 1);
                        lstIndex.Items.Add(e_Shop.Name);
                    }
                }
            }
        }

        private void LoadItemCombo()
        {
            cmbItemsToAdd.Items.Add("None");
            string connection = "Data Source=" + Server.sqlServer + ";Initial Catalog=" + Server.sqlDatabase + ";Integrated Security=True";
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                string command = "SELECT COUNT(*) FROM ITEMS";
                using (SqlCommand cmd = new SqlCommand(command, sql))
                {
                    object count = cmd.ExecuteScalar();
                    int result = ToInt32(count);
                    for (int i = 0; i < result; i++)
                    {
                        s_Item.LoadNameFromDatabase(i + 1);
                        cmbItemsToAdd.Items.Add((i + 1) + ": " + s_Item.Name);
                    }
                }
            }
        }

        private void LoadShopItemList()
        {
            if (e_Shop == null) { return; }

            lstItemsSold.Items.Clear();
            for (int i = 0; i < 25; i++)
            {
                if (e_Shop.shopItem[i].Name != "None")
                {
                    if (e_Shop.shopItem[i].Cost > 1) 
                    { lstItemsSold.Items.Add(e_Shop.shopItem[i].Name + " Price: " + e_Shop.shopItem[i].Cost); }
                    else { lstItemsSold.Items.Add(e_Shop.shopItem[i].Name); }
                }
            }
        }

        private int FindOpenShopItemSlot()
        {
            for (int i = 0; i < 25; i++)
            {
                if (e_Shop.shopItem[i].Name == "None")
                {
                    return i;
                }
            }
            return 25;
        }
        
        private void RestructureShopItemArray()
        {
            for (int i = 0; i < 25; i++)
            {
                if (e_Shop.shopItem[i].Name == "None")
                {
                    if (i < 24)
                    {
                        if (e_Shop.shopItem[i + 1].Name != "None")
                        {
                            e_Shop.shopItem[i] = e_Shop.shopItem[i + 1];
                            e_Shop.shopItem[i + 1] = new ShopItem("None", 1, 0);
                        }
                    }
                }
            }
        }

        private void btnNewItem_Click(object sender, EventArgs e)
        {
            e_Shop.CreateShopInDatabase();
            lstIndex.Items.Add(e_Shop.Name);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            e_Shop.SaveShopInDatabase(SelectedIndex);
            LoadItemList();
            UnModSave = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (btnAddItem.Text == "None") { return; }
            int slot = FindOpenShopItemSlot();

            if (slot == 25) { return; }
            string name;
            if (cmbItemsToAdd.SelectedIndex > 10) { name = cmbItemsToAdd.Text.Substring(4); }
            else { name = cmbItemsToAdd.Text.Substring(3); }

            e_Shop.shopItem[slot].Name = name;
            e_Shop.shopItem[slot].ItemNum = cmbItemsToAdd.SelectedIndex;
            if (txtCustomCost.Text == null) { e_Shop.shopItem[slot].Cost = 1; }
            else { e_Shop.shopItem[slot].Cost = ToInt32(txtCustomCost.Text.Trim()); }
            e_Shop.SaveShopInDatabase(SelectedIndex);
            LoadShopItemList();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            e_Shop.Name = txtName.Text;
            UnModSave = true;
        }

        private void lstIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UnModSave == true)
            {
                string w_Message = "Are you sure you want to load a different item? All unsaved progress will be lost.";
                string w_Caption = "Unsaved data";
                MessageBoxButtons w_Buttons = MessageBoxButtons.YesNo;
                DialogResult w_Result;
                w_Result = MessageBox.Show(w_Message, w_Caption, w_Buttons);
                if (w_Result == DialogResult.No) { return; }
            }

            SelectedIndex = (lstIndex.SelectedIndex + 1);
            if (SelectedIndex == 0) { return; }
            e_Shop.LoadShopFromDatabase(SelectedIndex);
            txtName.Text = e_Shop.Name;
            LoadShopItemList();
            UnModSave = false;
            if (pnlMain.Visible == false) { pnlMain.Visible = true; }
        }

        private void lstItemsSold_DoubleClicked(object sender, EventArgs e)
        {
            int slot = (lstItemsSold.SelectedIndex);
            if (e_Shop.shopItem[slot].Name == "None") { return; }
            e_Shop.shopItem[slot].Name = "None";
            e_Shop.shopItem[slot].ItemNum = 0;
            e_Shop.shopItem[slot].Cost = 1;
            RestructureShopItemArray();
            e_Shop.SaveShopInDatabase(SelectedIndex);
            LoadShopItemList();
        }
    }
}
