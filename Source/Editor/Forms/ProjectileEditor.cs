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
using Server.Classes;

namespace Editor.Forms
{
    public partial class ProjectileEditor : Form
    {
        Projectile e_Proj = new Projectile();
        int SelectedIndex;
        bool UnModSave;
        
        public ProjectileEditor()
        {
            InitializeComponent();
            picSprite.Image = Image.FromFile("Resources/Projectiles/1.png");
            scrlSprite.Maximum = 2;
            LoadProjList();
        }

        public void LoadProjList()
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
            {
                conn.Open();
                string sql;

                sql = "SELECT COUNT(*) FROM PROJECTILES";

                object queue;
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    queue = cmd.ExecuteScalar();
                }
                int result = ToInt32(queue);
                lstIndex.Items.Clear();
                for (int i = 0; i < result; i++)
                {
                    e_Proj.LoadNameFromDatabase(i + 1);
                    lstIndex.Items.Add(e_Proj.Name);
                }
            }
        }

        private void btnNewItem_Click(object sender, EventArgs e)
        {
            e_Proj.CreateProjectileInDatabase();
            lstIndex.Items.Add(e_Proj.Name);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            e_Proj.SaveProjectileToDatabase(SelectedIndex);
            LoadProjList();
            UnModSave = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

        private void scrlSprite_Scroll(object sender, ScrollEventArgs e)
        {
            lblSprite.Text = "Sprite: " + (scrlSprite.Value);
            picSprite.Image = Image.FromFile("Resources/Projectiles/" + scrlSprite.Value + ".png");
            e_Proj.Sprite = scrlSprite.Value;
            UnModSave = true;
        }

        private void scrlDamage_Scroll(object sender, ScrollEventArgs e)
        {
            lblDamage.Text = "Damage: " + (scrlDamage.Value);
            e_Proj.Damage = scrlDamage.Value;
            UnModSave = true;
        }

        private void scrlRange_Scroll(object sender, ScrollEventArgs e)
        {
            lblRange.Text = "Range: " + (scrlRange.Value);
            e_Proj.Range = scrlRange.Value;
            UnModSave = true;
        }

        private void scrlSpeed_Scroll(object sender, ScrollEventArgs e)
        {
            lblSpeed.Text = "Speed: " + scrlSpeed.Value;
            e_Proj.Speed = scrlSpeed.Value;
            UnModSave = true;
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            e_Proj.Type = cmbType.SelectedIndex;
            UnModSave = true;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            e_Proj.Name = txtName.Text;
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
            e_Proj.LoadProjectileFromDatabase(SelectedIndex);
            txtName.Text = e_Proj.Name;
            scrlSprite.Value = e_Proj.Sprite;
            scrlDamage.Value = e_Proj.Damage;
            scrlRange.Value = e_Proj.Range;
            scrlSpeed.Value = e_Proj.Speed;
            cmbType.SelectedIndex = e_Proj.Type;
            picSprite.Image = Image.FromFile("Resources/Projectiles/" + scrlSprite.Value + ".png");
            lblSprite.Text = "Sprite: " + scrlSprite.Value;
            lblDamage.Text = "Damage: " + scrlDamage.Value;
            lblRange.Text = "Range: " + scrlRange.Value;
            lblSpeed.Text = "Speed: " + scrlSpeed.Value;
            UnModSave = false;
            if (pnlMain.Visible == false) { pnlMain.Visible = true; }
        }
    }
}
