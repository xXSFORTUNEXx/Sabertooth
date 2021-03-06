﻿using System;
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
        bool Modified;

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

        private void scrlSprite_Scroll(object sender, ScrollEventArgs e)
        {
            lblSprite.Text = "Sprite: " + (scrlSprite.Value);
            picSprite.Image = Image.FromFile("Resources/Items/" + scrlSprite.Value + ".png");
            e_Item.Sprite = scrlSprite.Value;
            Modified = true;
        }

        private void scrlDamage_Scroll_1(object sender, ScrollEventArgs e)
        {
            lblDamage.Text = "Damage: " + (scrlDamage.Value);
            e_Item.Damage = scrlDamage.Value;
            Modified = true;
        }

        private void scrlArmor_Scroll(object sender, ScrollEventArgs e)
        {
            lblArmor.Text = "Armor: " + (scrlArmor.Value);
            e_Item.Armor = scrlArmor.Value;
            Modified = true;
        }

        private void scrlAttackSpeed_Scroll(object sender, ScrollEventArgs e)
        {
            lblAttackSpeed.Text = "Attack Speed: " + (scrlAttackSpeed.Value);
            e_Item.AttackSpeed = scrlAttackSpeed.Value;
            Modified = true;
        }
        
        private void scrlHealthRestore_Scroll(object sender, ScrollEventArgs e)
        {
            lblHealthRestore.Text = "Health Restore: " + (scrlHealthRestore.Value);
            e_Item.HealthRestore = scrlHealthRestore.Value;
            Modified = true;
        }

        private void scrlManaRestore_Scroll(object sender, ScrollEventArgs e)
        {
            lblManaRestore.Text = "Mana Restore: " + (scrlManaRestore.Value);
            e_Item.ManaRestore = scrlManaRestore.Value;
            Modified = true;
        }

        private void scrlStrength_Scroll(object sender, ScrollEventArgs e)
        {
            lblStrength.Text = "Strength: " + (scrlStrength.Value);
            e_Item.Strength = scrlStrength.Value;
            Modified = true;
        }

        private void scrlAgility_Scroll(object sender, ScrollEventArgs e)
        {
            lblAgility.Text = "Agility: " + (scrlAgility.Value);
            e_Item.Agility = scrlAgility.Value;
            Modified = true;
        }

        private void scrlInt_Scroll(object sender, ScrollEventArgs e)
        {
            lblInt.Text = "Intelligence: " + (scrlInt.Value);
            e_Item.Intelligence = scrlInt.Value;
            Modified = true;
        }

        private void scrlEnergy_Scroll(object sender, ScrollEventArgs e)
        {
            lblEnergy.Text = "Energy: " + (scrlEnergy.Value);
            e_Item.Energy = scrlEnergy.Value;
            Modified = true;
        }

        private void scrlStamina_Scroll(object sender, ScrollEventArgs e)
        {
            lblStamina.Text = "Stamina: " + (scrlStamina.Value);
            e_Item.Stamina = scrlStamina.Value;
            Modified = true;
        }
        
        private void scrlPrice_Scroll(object sender, ScrollEventArgs e)
        {
            lblPrice.Text = "Price: " + scrlPrice.Value;
            e_Item.Price = scrlPrice.Value;
            Modified = true;
        }

        private void scrlCooldown_Scroll(object sender, ScrollEventArgs e)
        {
            lblCoolDown.Text = "Cooldown: " + (scrlCooldown.Value) + "s";
            e_Item.CoolDown = scrlCooldown.Value;
            Modified = true;
        }

        private void scrlSpellNum_Scroll(object sender, ScrollEventArgs e)
        {
            lblSpellNum.Text = "Spell Number: " + (scrlSpellNum.Value);
            e_Item.SpellNum = scrlSpellNum.Value;
            Modified = true;
        }

        private void scrlAddMaxHP_Scroll(object sender, ScrollEventArgs e)
        {
            lblAddMaxHP.Text = "Add Max HP: " + (scrlAddMaxHP.Value);
            e_Item.AddMaxHealth = scrlAddMaxHP.Value;
            Modified = true;
        }

        private void scrlAddMaxMP_Scroll(object sender, ScrollEventArgs e)
        {
            lblAddMaxMP.Text = "Add Max MP: " + (scrlAddMaxMP.Value);
            e_Item.AddMaxMana = scrlAddMaxMP.Value;
            Modified = true;
        }

        private void scrlBonusXP_Scroll(object sender, ScrollEventArgs e)
        {
            lblBonusXP.Text = "Bonus XP: " + (scrlBonusXP.Value);
            e_Item.BonusXP = scrlBonusXP.Value;
            Modified = true;
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
            Modified = true;
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            e_Item.Type = (cmbType.SelectedIndex);
            Modified = true;
            if (cmbType.SelectedIndex == (int)ItemType.Food || cmbType.SelectedIndex == (int)ItemType.Drink || cmbType.SelectedIndex == (int)ItemType.Potion) { pnlConsume.Visible = true; }
            else { pnlConsume.Visible = false; }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            e_Item.Name = txtName.Text;
            Modified = true;
        }


        private void chkStackable_CheckedChanged(object sender, EventArgs e)
        {
            e_Item.Stackable = chkStackable.Checked;
            Modified = true;
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
            Modified = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

        private void lstIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Modified == true)
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
            scrlManaRestore.Value = e_Item.ManaRestore;
            scrlStrength.Value = e_Item.Strength;
            scrlAgility.Value = e_Item.Agility;
            scrlInt.Value = e_Item.Intelligence;
            scrlEnergy.Value = e_Item.Energy;
            scrlStamina.Value = e_Item.Stamina;
            cmbType.SelectedIndex = e_Item.Type;
            scrlPrice.Value = e_Item.Price;
            scrlRarity.Value = e_Item.Rarity;
            scrlCooldown.Value = e_Item.CoolDown;
            scrlAddMaxHP.Value = e_Item.AddMaxHealth;
            scrlAddMaxMP.Value = e_Item.AddMaxMana;
            scrlBonusXP.Value = e_Item.BonusXP;
            scrlSpellNum.Value = e_Item.SpellNum;
            scrlMaxStack.Value = e_Item.MaxStack;
            chkStackable.Checked = e_Item.Stackable;

            lblSprite.Text = "Sprite: " + (scrlSprite.Value);
            picSprite.Image = Image.FromFile("Resources/Items/" + scrlSprite.Value + ".png");                       
            lblDamage.Text = "Damage: " + (scrlDamage.Value);
            lblArmor.Text = "Armor: " + (scrlArmor.Value);
            lblAttackSpeed.Text = "Attack Speed: " + (scrlAttackSpeed.Value);
            lblHealthRestore.Text = "Health Restore: " + (scrlHealthRestore.Value);
            lblManaRestore.Text = "Mana Restore: " + (scrlManaRestore.Value);
            lblStrength.Text = "Strength: " + (scrlStrength.Value);
            lblAgility.Text = "Agility: " + (scrlAgility.Value);
            lblInt.Text = "Intelligence: " + (scrlInt.Value);
            lblEnergy.Text = "Energy: " + (scrlEnergy.Value);
            lblStamina.Text = "Stamina: " + (scrlStamina.Value);
            lblPrice.Text = "Price: " + scrlPrice.Value;
            lblCoolDown.Text = "Cooldown: " + scrlCooldown.Value;
            lblAddMaxHP.Text = "Add Max HP: " + scrlAddMaxHP.Value;
            lblAddMaxMP.Text = "Add Max MP: " + scrlAddMaxMP.Value;
            lblBonusXP.Text = "Bonus XP: " + scrlBonusXP.Value;
            lblSpellNum.Text = "Spell Number: " + scrlSpellNum.Value;
            lblStackSize.Text = "Max Stack: " + scrlMaxStack.Value;
            
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

            Modified = false;
            if (pnlMain.Visible == false) { pnlMain.Visible = true; }
            if (pnlStats.Visible == false) { pnlStats.Visible = true; }
        }

        private void scrlMaxStack_Scroll(object sender, ScrollEventArgs e)
        {
            lblStackSize.Text = "Max Stack: " + (scrlMaxStack.Value);
            e_Item.MaxStack = scrlMaxStack.Value;
            Modified = true;
        }
    }
}
