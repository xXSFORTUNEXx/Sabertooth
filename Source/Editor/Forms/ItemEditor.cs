﻿using System;
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
    public partial class ItemEditor : Form
    {
        SQLiteConnection e_Database;
        Item e_Item = new Item();
        int SelectedIndex;
        bool UnModSave;

        public ItemEditor()
        {
            InitializeComponent();
            picSprite.Image = Image.FromFile("Resources/Items/1.png");
            LoadItemList();
        }

        private void LoadItemList()
        {
            e_Database = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;");
            e_Database.Open();
            string sql;

            sql = "SELECT COUNT(*) FROM ITEMS";

            SQLiteCommand sql_Command = new SQLiteCommand(sql, e_Database);
            int result = int.Parse(sql_Command.ExecuteScalar().ToString());
            e_Database.Close();
            lstIndex.Items.Clear();
            for (int i = 0; i < result; i++)
            {
                e_Item.LoadNameFromDatabase(i + 1);
                lstIndex.Items.Add(e_Item.Name);
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
            e_Item.LoadItemFromDatabase(SelectedIndex);
            txtName.Text = e_Item.Name;
            scrlSprite.Value = e_Item.Sprite;
            scrlDamage.Value = e_Item.Damage;
            scrlArmor.Value = e_Item.Armor;
            scrlAttackSpeed.Value = e_Item.AttackSpeed;
            scrlReloadSpeed.Value = e_Item.ReloadSpeed;
            scrlHealthRestore.Value = e_Item.HealthRestore;
            scrlHungerRestore.Value = e_Item.HungerRestore;
            scrlHydrateRestore.Value = e_Item.HydrateRestore;
            scrlStrength.Value = e_Item.Strength;
            scrlAgility.Value = e_Item.Agility;
            scrlEndurance.Value = e_Item.Endurance;
            scrlStamina.Value = e_Item.Stamina;
            scrlClip.Value = e_Item.Clip;
            scrlMaxClip.Value = e_Item.MaxClip;
            cmbType.SelectedIndex = e_Item.Type;
            cmbAmmoType.SelectedIndex = e_Item.ItemAmmoType;
            lblSprite.Text = "Sprite: " + (scrlSprite.Value);
            picSprite.Image = Image.FromFile("Resources/Items/" + scrlSprite.Value + ".png");
            lblDamage.Text = "Damage: " + (scrlDamage.Value);
            lblArmor.Text = "Armor: " + (scrlArmor.Value);
            lblAttackSpeed.Text = "Attack Speed: : " + (scrlAttackSpeed.Value);
            lblReloadSpeed.Text = "Reload Speed: " + (scrlReloadSpeed.Value);
            lblHealthRestore.Text = "Health Restore: " + (scrlHealthRestore.Value);
            lblHungerRestore.Text = "Hunger Restore: " + (scrlHungerRestore.Value);
            lblHydrateRestore.Text = "Hydrate Restore: " + (scrlHydrateRestore.Value);
            lblStrength.Text = "Strength: " + (scrlStrength.Value);
            lblAgility.Text = "Agility: " + (scrlAgility.Value);
            lblEndurance.Text = "Endurance: " + (scrlEndurance.Value);
            lblStamina.Text = "Stamina: " + (scrlStamina.Value);
            lblClip.Text = "Clip: " + (scrlClip.Value);
            lblMaxClip.Text = "Max Clip: " + (scrlMaxClip.Value);
            UnModSave = false;
            if (pnlMain.Visible == false) { pnlMain.Visible = true; }
        }

        private void btnNewItem_Click(object sender, EventArgs e)
        {
            e_Item.CreateItemInDatabase();
            lstIndex.Items.Add(e_Item.Name);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            e_Item.SaveItemToDatabase(SelectedIndex);
            LoadItemList();
            UnModSave = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            e_Item.DeleteItemFromDatabase(SelectedIndex);
            LoadItemList();
        }

        private void scrlSprite_Scroll(object sender, ScrollEventArgs e)
        {
            lblSprite.Text = "Sprite: " + (scrlSprite.Value);
            picSprite.Image = Image.FromFile("Resources/Items/" + scrlSprite.Value + ".png");
            e_Item.Sprite = scrlSprite.Value;
            UnModSave = true;
        }

        private void scrlDamage_Scroll(object sender, ScrollEventArgs e)
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
            lblAttackSpeed.Text = "Attack Speed: : " + (scrlAttackSpeed.Value);
            e_Item.AttackSpeed = scrlAttackSpeed.Value;
            UnModSave = true;
        }

        private void scrlReloadSpeed_Scroll(object sender, ScrollEventArgs e)
        {
            lblReloadSpeed.Text = "Reload Speed: " + (scrlReloadSpeed.Value);
            e_Item.ReloadSpeed = scrlReloadSpeed.Value;
            UnModSave = true;
        }

        private void scrlHealthRestore_Scroll(object sender, ScrollEventArgs e)
        {
            lblHealthRestore.Text = "Health Restore: " + (scrlHealthRestore.Value);
            e_Item.HealthRestore = scrlHealthRestore.Value;
            UnModSave = true;
        }

        private void scrlHungerRestore_Scroll(object sender, ScrollEventArgs e)
        {
            lblHungerRestore.Text = "Hunger Restore: " + (scrlHungerRestore.Value);
            e_Item.HungerRestore = scrlHungerRestore.Value;
            UnModSave = true;
        }

        private void scrlHydrateRestore_Scroll(object sender, ScrollEventArgs e)
        {
            lblHydrateRestore.Text = "Hydrate Restore: " + (scrlHydrateRestore.Value);
            e_Item.HydrateRestore = scrlHydrateRestore.Value;
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

        private void scrlClip_Scroll(object sender, ScrollEventArgs e)
        {
            lblClip.Text = "Clip: " + (scrlClip.Value);
            e_Item.Clip = scrlClip.Value;
            UnModSave = true;
        }

        private void scrlMaxClip_Scroll(object sender, ScrollEventArgs e)
        {
            lblMaxClip.Text = "Max Clip: " + (scrlMaxClip.Value);
            e_Item.MaxClip = scrlMaxClip.Value;
            UnModSave = true;
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            e_Item.Type = (cmbType.SelectedIndex);
            UnModSave = true;
        }

        private void cmbAmmoType_SelectedIndexChanged(object sender, EventArgs e)
        {
            e_Item.ItemAmmoType = (cmbAmmoType.SelectedIndex);
            UnModSave = true;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            e_Item.Name = txtName.Text;
            UnModSave = true;
        }
    }
}
