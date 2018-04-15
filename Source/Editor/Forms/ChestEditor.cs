using Sabertooth;
using System;
using System.Data.SQLite;
using System.Windows.Forms;
using static System.Convert;

namespace Editor.Forms
{
    public partial class ChestEditor : Form
    {
        Chest e_Chest = new Chest();
        Item e_Item = new Item();
        int SelectedIndex;
        bool UnModSave;

        public ChestEditor()
        {
            InitializeComponent();
            LoadItemList();
            LoadItemCombo();
        }

        private void LoadItemList()
        {
            using (var conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
            {
                using (var cmd = new SQLiteCommand(conn))
                {
                    conn.Open();
                    cmd.CommandText = "SELECT COUNT(*) FROM CHESTS";
                    object count = cmd.ExecuteScalar();
                    int result = ToInt32(count);
                    lstIndex.Items.Clear();
                    for (int i = 0; i < result; i++)
                    {
                        e_Chest.LoadChestNameFromDatabase(i + 1);
                        lstIndex.Items.Add(e_Chest.Name);
                    }
                }
            }
        }

        private void LoadItemCombo()
        {
            cmbItemsToAdd.Items.Add("None");
            using (var conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
            {
                using (var cmd = new SQLiteCommand(conn))
                {
                    conn.Open();
                    cmd.CommandText = "SELECT COUNT(*) FROM ITEMS";
                    object count = cmd.ExecuteScalar();
                    int result = ToInt32(count);
                    for (int i = 0; i < result; i++)
                    {
                        e_Item.LoadNameFromDatabase(i + 1);
                        cmbItemsToAdd.Items.Add((i + 1) + ": " + e_Item.Name);
                    }
                }
            }
        }

        private void LoadChestItemList()
        {
            if (e_Chest == null) { return; }

            lstItems.Items.Clear();
            for (int i = 0; i < 10; i++)
            {
                if (e_Chest.ChestItem[i].Name != "None")
                {
                    lstItems.Items.Add(e_Chest.ChestItem[i].Name + " Value: " + e_Chest.ChestItem[i].Value);
                }
            }
        }

        private int FindOpenChestItemSlot()
        {
            for (int i = 0; i < 10; i++)
            {
                if (e_Chest.ChestItem[i].Name == "None")
                {
                    return i;
                }
            }
            return 10;
        }

        private void RestructureChestItemArray()
        {
            for (int i = 0; i < 10; i++)
            {
                if (e_Chest.ChestItem[i].Name == "None")
                {
                    if (i < 10)
                    {
                        if (e_Chest.ChestItem[i].Name != "None")
                        {
                            e_Chest.ChestItem[i] = e_Chest.ChestItem[i + 1];
                            e_Chest.ChestItem[i] = new ChestItem("None", 0, 1);
                        }
                    }
                }
            }
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
            e_Chest.LoadChestFromDatabase(SelectedIndex);
            txtName.Text = e_Chest.Name;
            scrlMoney.Value = e_Chest.Money;
            scrlExp.Value = e_Chest.Experience;
            scrlReqLevel.Value = e_Chest.RequiredLevel;
            scrlTrapLevel.Value = e_Chest.TrapLevel;
            scrlKey.Value = e_Chest.Key;
            scrlDamage.Value = e_Chest.Damage;
            scrlNpcSpawnNum.Value = e_Chest.NpcSpawn;
            scrlSpawnAmount.Value = e_Chest.SpawnAmount;
            lblMoney.Text = "Money: " + scrlMoney.Value;
            lblExperience.Text = "Experience: " + scrlExp.Value;
            lblReqLvl.Text = "Required Level: " + scrlReqLevel.Value;
            lblTrapLevel.Text = "Trap Level: " + scrlTrapLevel.Value;
            lblKey.Text = "Key: " + scrlKey.Value;
            lblDamage.Text = "Damage: " + scrlDamage.Value;
            lblNpcSpawn.Text = "Npc Spawn Number: " + scrlNpcSpawnNum.Value;
            lblSpawnAmount.Text = "Spawn Amount: " + scrlSpawnAmount.Value;
            LoadChestItemList();
            UnModSave = false;
            if (!pnlProperties.Visible) { pnlProperties.Visible = true; }
            if (!pnlItems.Visible) { pnlItems.Visible = true; }
        }

        private void lstItems_DoubleClicked(object sender, EventArgs e)
        {
            int slot = (lstItems.SelectedIndex);
            if (e_Chest.ChestItem[slot].Name == "None") { return; }
            e_Chest.ChestItem[slot].Name = "None";
            e_Chest.ChestItem[slot].ItemNum = 0;
            e_Chest.ChestItem[slot].Value = 1;
            RestructureChestItemArray();
            LoadChestItemList();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            e_Chest.SaveChestInDatabase(SelectedIndex);
            LoadItemList();
            UnModSave = false;
        }

        private void btnNewItem_Click(object sender, EventArgs e)
        {
            e_Chest.CreateChestInDatabase();
            lstIndex.Items.Add(e_Chest.Name);
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (cmbItemsToAdd.Text == "None") { return; }
            int slot = FindOpenChestItemSlot();

            if (slot == 10) { return; }
            e_Chest.ChestItem[slot].Name = cmbItemsToAdd.Text;
            e_Chest.ChestItem[slot].ItemNum = cmbItemsToAdd.SelectedIndex;
            e_Chest.ChestItem[slot].Value = scrlValue.Value;
            e_Chest.SaveChestInDatabase(SelectedIndex);
            LoadChestItemList();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

            e_Chest.Name = txtName.Text;
        }

        private void scrlMoney_Scroll(object sender, ScrollEventArgs e)
        {
            lblMoney.Text = "Money: " + scrlMoney.Value;
            e_Chest.Money = scrlMoney.Value;
        }

        private void scrlExp_Scroll(object sender, ScrollEventArgs e)
        {
            lblExperience.Text = "Experience: " + scrlExp.Value;
            e_Chest.Experience = scrlExp.Value;
        }

        private void scrlReqLevel_Scroll(object sender, ScrollEventArgs e)
        {
            lblReqLvl.Text = "Required Level: " + scrlReqLevel.Value;
            e_Chest.RequiredLevel = scrlReqLevel.Value;
        }

        private void scrlTrapLevel_Scroll(object sender, ScrollEventArgs e)
        {
            lblTrapLevel.Text = "Trap Level: " + scrlTrapLevel.Value;
            e_Chest.TrapLevel = scrlTrapLevel.Value;
        }

        private void scrlKey_Scroll(object sender, ScrollEventArgs e)
        {
            lblKey.Text = "Key: " + scrlKey.Value;
            e_Chest.Key = scrlKey.Value;
        }

        private void scrlDamage_Scroll(object sender, ScrollEventArgs e)
        {
            lblDamage.Text = "Damage: " + scrlDamage.Value;
            e_Chest.Damage = scrlDamage.Value;
        }

        private void scrlNpcSpawnNum_Scroll(object sender, ScrollEventArgs e)
        {
            lblNpcSpawn.Text = "Npc Spawn Number: " + scrlNpcSpawnNum.Value;
            e_Chest.NpcSpawn = scrlNpcSpawnNum.Value;
        }

        private void scrlSpawnAmount_Scroll(object sender, ScrollEventArgs e)
        {
            lblSpawnAmount.Text = "Spawn Amount: " + scrlSpawnAmount.Value;
            e_Chest.SpawnAmount = scrlSpawnAmount.Value;
        }
    }
}
