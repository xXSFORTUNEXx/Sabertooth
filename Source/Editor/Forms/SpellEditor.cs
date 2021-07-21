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
    public partial class SpellEditor : Form
    {
        Spell e_Spell = new Spell();
        int SelectedIndex;
        bool Modified;

        public SpellEditor()
        {
            InitializeComponent();
            picIcon.Image = Image.FromFile("Resources/Icons/1.png");
            scrlIcon.Maximum = Directory.GetFiles("Resources/Icons/", "*", SearchOption.TopDirectoryOnly).Length;
            LoadSpellList();
        }

        private void LoadSpellList()
        {
            string connection = "Data Source=" + Server.sqlServer + ";Initial Catalog=" + Server.sqlDatabase + ";Integrated Security=True";
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                string command = "SELECT COUNT(*) FROM Spells";
                using (SqlCommand cmd = new SqlCommand(command, sql))
                {
                    object count = cmd.ExecuteScalar();
                    int result = ToInt32(count);
                    lstIndex.Items.Clear();
                    for (int i = 0; i < result; i++)
                    {
                        e_Spell.LoadNameFromDatabase(i + 1);
                        lstIndex.Items.Add(e_Spell.Name);
                    }
                }
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            e_Spell.Name = txtName.Text;
            Modified = true;
        }

        private void scrlIcon_Scroll(object sender, ScrollEventArgs e)
        {
            lblIcon.Text = "Icon: " + scrlIcon.Value;
            picIcon.Image = Image.FromFile("Resources/Icons/" + scrlIcon.Value + ".png");
            e_Spell.Icon = scrlIcon.Value;
            Modified = true;
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            e_Spell.SpellType = cmbType.SelectedIndex;
            Modified = true;
        }

        private void scrlLevelReq_Scroll(object sender, ScrollEventArgs e)
        {
            lblLevelReq.Text = "Level: " + scrlLevelReq.Value;
            e_Spell.Level = scrlLevelReq.Value;
            Modified = true;
        }

        private void scrlCharges_Scroll(object sender, ScrollEventArgs e)
        {
            lblCharges.Text = "Chargers: " + scrlCharges.Value;
            e_Spell.Charges = scrlCharges.Value;
            Modified = true;
        }

        private void scrlRange_Scroll(object sender, ScrollEventArgs e)
        {
            lblRange.Text = "Range: " + scrlRange.Value;
            e_Spell.Range = scrlRange.Value;
            Modified = true;
        }

        private void chkAOE_CheckedChanged(object sender, EventArgs e)
        {
            e_Spell.AOE = chkAOE.Checked;
            Modified = true;
        }

        private void scrlDistance_Scroll(object sender, ScrollEventArgs e)
        {
            lblDistance.Text = "Distance: " + scrlDistance.Value;
            e_Spell.Distance = scrlDistance.Value;
            Modified = true;
        }

        private void scrlAnimation_Scroll(object sender, ScrollEventArgs e)
        {
            lblAnimation.Text = "Animation: " + scrlAnimation.Value;
            e_Spell.Animation = scrlAnimation.Value;
            Modified = true;
        }

        private void scrlVital_Scroll(object sender, ScrollEventArgs e)
        {
            lblVital.Text = "Vital: " + scrlVital.Value;
            e_Spell.Vital = scrlVital.Value;
            Modified = true;
        }

        private void scrlHPCost_Scroll(object sender, ScrollEventArgs e)
        {
            lblHPCost.Text = "HP Cost: " + scrlHPCost.Value;
            e_Spell.HealthCost = scrlHPCost.Value;
            Modified = true;
        }

        private void scrlMPCost_Scroll(object sender, ScrollEventArgs e)
        {
            lblMPCost.Text = "MP Cost: " + scrlMPCost.Value;
            e_Spell.ManaCost = scrlMPCost.Value;
            Modified = true;
        }

        private void scrlCoolDown_Scroll(object sender, ScrollEventArgs e)
        {
            if (scrlCoolDown.Value == 0) { lblCoolDown.Text = "Cool Down: Instant"; }
            else { lblCoolDown.Text = "Cool Down: " + scrlCoolDown.Value + "ms"; }            
            e_Spell.CoolDown = scrlCoolDown.Value;
            Modified = true;
        }

        private void scrlCastTime_Scroll(object sender, ScrollEventArgs e)
        {
                        
            if (scrlCastTime.Value == 0) { lblCastTime.Text = "Cast Time: Instant"; }
            else { lblCastTime.Text = "Cast Time: " + scrlCastTime.Value + "ms"; }
            e_Spell.CastTime = scrlCastTime.Value;
            Modified = true;
        }

        private void scrlTotalTicks_Scroll(object sender, ScrollEventArgs e)
        {
            lblTotalTicks.Text = "Total Ticks: " + scrlTotalTicks.Value;
            e_Spell.TotalTick = scrlTotalTicks.Value;
            Modified = true;
        }

        private void scrlTickInt_Scroll(object sender, ScrollEventArgs e)
        {
            lblTickInt.Text = "Tick Interval: " + scrlTickInt.Value + "ms";
            e_Spell.TickInterval = scrlTickInt.Value;
            Modified = true;
        }

        private void lstIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Modified == true)
            {
                string w_Message = "Are you sure you want to load a different spell? All unsaved progress will be lost.";
                string w_Caption = "Unsaved data";
                MessageBoxButtons w_Buttons = MessageBoxButtons.YesNo;
                DialogResult w_Result;
                w_Result = MessageBox.Show(w_Message, w_Caption, w_Buttons);
                if (w_Result == DialogResult.No) { return; }
            }

            SelectedIndex = (lstIndex.SelectedIndex + 1);
            if (SelectedIndex == 0) { return; }
            e_Spell.LoadSpellFromDatabase(SelectedIndex);

            txtName.Text = e_Spell.Name;
            scrlIcon.Value = e_Spell.Icon;
            cmbType.SelectedIndex = e_Spell.SpellType;
            scrlLevelReq.Value = e_Spell.Level;
            scrlCharges.Value = e_Spell.Charges;
            scrlRange.Value = e_Spell.Range;
            chkAOE.Checked = e_Spell.AOE;
            scrlDistance.Value = e_Spell.Distance;
            scrlAnimation.Value = e_Spell.Animation;
            scrlVital.Value = e_Spell.Vital;
            scrlHPCost.Value = e_Spell.HealthCost;
            scrlMPCost.Value = e_Spell.ManaCost;
            scrlCoolDown.Value = e_Spell.CoolDown;
            scrlCastTime.Value = e_Spell.CastTime;
            scrlTotalTicks.Value = e_Spell.TotalTick;
            scrlTickInt.Value = e_Spell.TickInterval;

            lblIcon.Text = "Icon: " + scrlIcon.Value;
            picIcon.Image = Image.FromFile("Resources/Icons/" + scrlIcon.Value + ".png");
            lblLevelReq.Text = "Level: " + scrlLevelReq.Value;
            lblCharges.Text = "Charges: " + scrlCharges.Value;
            lblRange.Text = "Range: " + scrlRange.Value;
            lblDistance.Text = "Distance: " + scrlDistance.Value;
            lblAnimation.Text = "Animation: " + scrlAnimation.Value;
            lblVital.Text = "Vital: " + scrlVital.Value;
            lblHPCost.Text = "HP Cost: " + scrlHPCost.Value;
            lblMPCost.Text = "MP Cost: " + scrlMPCost.Value;
            if (scrlCoolDown.Value == 0) { lblCoolDown.Text = "Cool Down: Instant"; }
            else { lblCoolDown.Text = "Cool Down: " + scrlCoolDown.Value + "ms"; }
            if (scrlCastTime.Value == 0) { lblCastTime.Text = "Cast Time: Instant"; }
            else { lblCastTime.Text = "Cast Time: " + scrlCastTime.Value + "ms"; }
            lblTotalTicks.Text = "Total Tickets: " + scrlTotalTicks.Value;
            lblTickInt.Text = "Tick Interval " + scrlTickInt.Value + "ms";
        }

        private void btnNewItem_Click(object sender, EventArgs e)
        {
            e_Spell.CreateSpellInDatabase();
            lstIndex.Items.Add(e_Spell.Name);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            e_Spell.SaveSpellInDatabase(SelectedIndex);
            LoadSpellList();
            Modified = false;
        }
    }
}
