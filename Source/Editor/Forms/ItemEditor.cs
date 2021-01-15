using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Convert;
using SabertoothServer;
using System.Data.SqlClient;
using static SabertoothServer.Globals;
using System.IO;

namespace Editor.Forms
{
    public partial class ItemEditor : Form
    {
        Item e_Item = new Item();        
        int SelectedIndex;
        bool UnModSave;

        public ItemEditor()
        {
            InitializeComponent();
            picSprite.Image = Image.FromFile("Resources/Items/1.png"); 
            scrlSprite.Maximum = Directory.GetFiles("Resources/Items/", "*", SearchOption.TopDirectoryOnly).Length;            
            LoadItemList();
        }

        private void LoadItemList()
        {
            string connection = "Data Source=" + Server.sqlServer + ";Initial Catalog=" + Server.sqlDatabase + ";Integrated Security=True";
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                string command = "SELECT COUNT(*) FROM Items";
                using (SqlCommand cmd = new SqlCommand(command, sql))
                {
                    object count = cmd.ExecuteScalar();
                    int result = ToInt32(count);
                    lstIndex.Items.Clear();
                    for (int i = 0; i < result; i++)
                    {
                        e_Item.LoadNameFromDatabase(i + 1);
                        lstIndex.Items.Add(e_Item.Name);
                    }
                }
            }
        }

        private int LoadProjectileCount()
        {
            string connection = "Data Source=" + Server.sqlServer + ";Initial Catalog=" + Server.sqlDatabase + ";Integrated Security=True";
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                string command = "SELECT COUNT(*) FROM Projectiles";
                using (SqlCommand cmd = new SqlCommand(command, sql))
                {
                    object count = cmd.ExecuteScalar();
                    return ToInt32(count);
                }
            }
        }

        private void scrlSprite_Scroll(object sender, ScrollEventArgs e)
        {
            lblSprite.Text = "Sprite: " + (scrlSprite.Value);
            picSprite.Image = Image.FromFile("Resources/Items/" + scrlSprite.Value + ".png");
            e_Item.Sprite = scrlSprite.Value;
            UnModSave = true;
        }

        private void scrlDamage_Scroll_1(object sender, ScrollEventArgs e)
        {
            lblDamage.Text = "Damage: " + (scrlDamage.Value);
            e_Item.Damage = scrlDamage.Value;
            UnModSave = true;
        }

        private void scrlArmor_Scroll(object sender, ScrollEventArgs e)
        {
            lblArmor.Text = "Armor: " + (scrlArmor.Value);
            e_Item.Armor = scrlArmor.Value;
            UnModSave = true;
        }

        private void scrlAttackSpeed_Scroll(object sender, ScrollEventArgs e)
        {
            lblAttackSpeed.Text = "Attack Speed: " + (scrlAttackSpeed.Value);
            e_Item.AttackSpeed = scrlAttackSpeed.Value;
            UnModSave = true;
        }
        
        private void scrlHealthRestore_Scroll(object sender, ScrollEventArgs e)
        {
            lblHealthRestore.Text = "Health Restore: " + (scrlHealthRestore.Value);
            e_Item.HealthRestore = scrlHealthRestore.Value;
            UnModSave = true;
        }        

        private void scrlStrength_Scroll(object sender, ScrollEventArgs e)
        {
            lblStrength.Text = "Strength: " + (scrlStrength.Value);
            e_Item.Strength = scrlStrength.Value;
            UnModSave = true;
        }

        private void scrlAgility_Scroll(object sender, ScrollEventArgs e)
        {
            lblAgility.Text = "Agility: " + (scrlAgility.Value);
            e_Item.Agility = scrlAgility.Value;
            UnModSave = true;
        }

        private void scrlEndurance_Scroll(object sender, ScrollEventArgs e)
        {
            lblEndurance.Text = "Endurance: " + (scrlEndurance.Value);
            e_Item.Endurance = scrlEndurance.Value;
            UnModSave = true;
        }

        private void scrlStamina_Scroll(object sender, ScrollEventArgs e)
        {
            lblStamina.Text = "Stamina: " + (scrlStamina.Value);
            e_Item.Stamina = scrlStamina.Value;
            UnModSave = true;
        }
        
        private void scrlPrice_Scroll(object sender, ScrollEventArgs e)
        {
            lblPrice.Text = "Price: " + scrlPrice.Value;
            e_Item.Price = scrlPrice.Value;
            UnModSave = true;
        }

        private void scrlRarity_Scroll(object sender, ScrollEventArgs e)
        {
            switch (scrlRarity.Value)
            {
                case (int)Rarity.Normal:
                    lblRarity.Text = "Rarity: 0 - Normal";
                    break;
                case (int)Rarity.Uncommon:
                    lblRarity.Text = "Rarity: 1 - Uncommon";
                    break;
                case (int)Rarity.Rare:
                    lblRarity.Text = "Rarity: 2 - Rare";
                    break;
                case (int)Rarity.UltraRare:
                    lblRarity.Text = "Rarity: 3 - UltraRare";
                    break;
                case (int)Rarity.Legendary:
                    lblRarity.Text = "Rarity: 4 - Legendary";
                    break;
                case (int)Rarity.Admin:
                    lblRarity.Text = "Rarity: 5 - Admin";
                    break;
            }
            e_Item.Rarity = scrlRarity.Value;
            UnModSave = true;
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            e_Item.Type = (cmbType.SelectedIndex);
            UnModSave = true;
            if (cmbType.SelectedIndex == (int)ItemType.Food || cmbType.SelectedIndex == (int)ItemType.Drink || cmbType.SelectedIndex == (int)ItemType.FirstAid) { pnlConsume.Visible = true; }
            else { pnlConsume.Visible = false; }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            e_Item.Name = txtName.Text;
            UnModSave = true;
        }

        private void btnNewItem_Click(object sender, EventArgs e)
        {
            e_Item.CreateItemInDatabase();
            lstIndex.Items.Add(e_Item.Name);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //e_Item.DeleteItemFromDatabase(SelectedIndex);
            LoadItemList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            e_Item.SaveItemToDatabase(SelectedIndex);
            LoadItemList();
            UnModSave = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Visible = false;
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
            e_Item.LoadItemFromDatabase(SelectedIndex);            
            txtName.Text = e_Item.Name;
            scrlSprite.Value = e_Item.Sprite;
            scrlDamage.Value = e_Item.Damage;
            scrlArmor.Value = e_Item.Armor;
            scrlAttackSpeed.Value = e_Item.AttackSpeed;
            scrlHealthRestore.Value = e_Item.HealthRestore;
            scrlStrength.Value = e_Item.Strength;
            scrlAgility.Value = e_Item.Agility;
            scrlEndurance.Value = e_Item.Endurance;
            scrlStamina.Value = e_Item.Stamina;
            cmbType.SelectedIndex = e_Item.Type;
            scrlPrice.Value = e_Item.Price;
            scrlRarity.Value = e_Item.Rarity;
            lblSprite.Text = "Sprite: " + (scrlSprite.Value);
            picSprite.Image = Image.FromFile("Resources/Items/" + scrlSprite.Value + ".png");                       
            lblDamage.Text = "Damage: " + (scrlDamage.Value);
            lblArmor.Text = "Armor: " + (scrlArmor.Value);
            lblAttackSpeed.Text = "Attack Speed: " + (scrlAttackSpeed.Value);
            lblHealthRestore.Text = "Health Restore: " + (scrlHealthRestore.Value);
            lblStrength.Text = "Strength: " + (scrlStrength.Value);
            lblAgility.Text = "Agility: " + (scrlAgility.Value);
            lblEndurance.Text = "Endurance: " + (scrlEndurance.Value);
            lblStamina.Text = "Stamina: " + (scrlStamina.Value);
            lblPrice.Text = "Price: " + scrlPrice.Value;
            switch (scrlRarity.Value)
            {
                case (int)Rarity.Normal:
                    lblRarity.Text = "Rarity: 0 - Normal";
                    break;
                case (int)Rarity.Uncommon:
                    lblRarity.Text = "Rarity: 1 - Uncommon";
                    break;
                case (int)Rarity.Rare:
                    lblRarity.Text = "Rarity: 2 - Rare";
                    break;
                case (int)Rarity.UltraRare:
                    lblRarity.Text = "Rarity: 3 - UltraRare";
                    break;
                case (int)Rarity.Legendary:
                    lblRarity.Text = "Rarity: 4 - Legendary";
                    break;
                case (int)Rarity.Admin:
                    lblRarity.Text = "Rarity: 5 - Admin";
                    break;
            }
            UnModSave = false;
            if (pnlMain.Visible == false) { pnlMain.Visible = true; }
            if (pnlStats.Visible == false) { pnlStats.Visible = true; }
        }
    }
}
