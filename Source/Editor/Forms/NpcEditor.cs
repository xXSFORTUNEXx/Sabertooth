using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using SabertoothServer;
using static System.Convert;
using System.Data.SqlClient;
using static SabertoothServer.Globals;

namespace Editor.Forms
{
    public partial class NpcEditor : Form
    {
        Npc e_Npc = new Npc();
        int SelectedIndex;
        bool UnModSave;

        public NpcEditor()
        {
            InitializeComponent();
            picSprite.Image = Image.FromFile("Resources/Characters/1.png");
            scrlSprite.Maximum = 8;
            LoadNpcList();
        }

        private void LoadNpcList()
        {
            if (Server.DBType == SQL_DATABASE_REMOTE.ToString())
            {
                string connection = "Data Source=" + Server.sqlServer + ";Initial Catalog=" + Server.sqlDatabase + ";Integrated Security=True";
                using (var sql = new SqlConnection(connection))
                {
                    sql.Open();
                    string command = "SELECT COUNT(*) FROM NPCS";
                    using (SqlCommand cmd = new SqlCommand(command, sql))
                    {
                        object count = cmd.ExecuteScalar();
                        int result = ToInt32(count);
                        lstIndex.Items.Clear();
                        for (int i = 0; i < result; i++)
                        {
                            e_Npc.LoadNpcNameFromDatabase(i + 1);
                            lstIndex.Items.Add(e_Npc.Name);
                        }
                    }
                }
            }
            else
            {
                using (SQLiteConnection conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
                {
                    conn.Open();
                    string sql;

                    sql = "SELECT COUNT(*) FROM NPCS";

                    object queue;
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        queue = cmd.ExecuteScalar();
                    }
                    int result = ToInt32(queue);
                    lstIndex.Items.Clear();
                    for (int i = 0; i < result; i++)
                    {
                        e_Npc.LoadNpcNameFromDatabase(i + 1);
                        lstIndex.Items.Add(e_Npc.Name);
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
            e_Npc.LoadNpcFromDatabase(SelectedIndex);
            txtName.Text = e_Npc.Name;
            scrlX.Value = e_Npc.DesX;
            scrlY.Value = e_Npc.DesY;
            scrlDirection.Value = e_Npc.Direction;
            scrlSprite.Value = e_Npc.Sprite;
            scrlStep.Value = e_Npc.Step;
            scrlOwner.Value = e_Npc.Owner;
            cmbBehavior.SelectedIndex = e_Npc.Behavior;
            scrlSpawnTime.Value = e_Npc.SpawnTime;
            scrlHealth.Value = e_Npc.Health;
            scrlMaxHealth.Value = e_Npc.MaxHealth;
            scrlDamage.Value = e_Npc.Damage;
            scrlExp.Value = e_Npc.Exp;
            scrlMoney.Value = e_Npc.Money;
            scrlRange.Value = e_Npc.Range;
            scrlShopNum.Value = e_Npc.ShopNum;
            scrlChatNum.Value = e_Npc.ChatNum;
            scrlMoveSpeed.Value = e_Npc.Speed;
            lblX.Text = "X: " + (scrlX.Value);
            lblY.Text = "Y: " + (scrlY.Value);
            lblDirection.Text = "Direction: " + (scrlDirection.Value);
            lblSprite.Text = "Sprite: " + (scrlSprite.Value);
            picSprite.Image = Image.FromFile("Resources/Characters/" + scrlSprite.Value + ".png");
            lblStep.Text = "Step: " + (scrlStep.Value);
            lblOwner.Text = "Owner: " + (scrlOwner.Value);
            lblSpawnTime.Text = "Spawn Time: " + (scrlSpawnTime.Value);
            lblHealth.Text = "Health: " + (scrlHealth.Value);
            lblMaxHealth.Text = "Max Health: " + (scrlMaxHealth.Value);
            lblDamage.Text = "Damage: " + (scrlDamage.Value);
            lblExp.Text = "Experience: " + scrlExp.Value;
            lblMoney.Text = "Money: " + scrlMoney.Value;
            lblRange.Text = "Range: " + scrlRange.Value;
            lblShopNum.Text = "Shop: " + scrlShopNum.Value;
            lblChatNum.Text = "Chat Number: " + scrlChatNum.Value;
            lblMoveSpeed.Text = "Move Speed: " + scrlMoveSpeed.Value;
            UnModSave = false;
            if (pnlMain.Visible == false) { pnlMain.Visible = true; }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

        private void btnNewItem_Click(object sender, EventArgs e)
        {
            e_Npc.CreateNpcInDatabase();
            lstIndex.Items.Add(e_Npc.Name);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            e_Npc.SaveNpcToDatabase(SelectedIndex);
            LoadNpcList();
            UnModSave = false;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            e_Npc.Name = txtName.Text;
            UnModSave = true;
        }

        private void scrlSprite_Scroll(object sender, ScrollEventArgs e)
        {
            lblSprite.Text = "Sprite: " + (scrlSprite.Value);
            picSprite.Image = Image.FromFile("Resources/Characters/" + scrlSprite.Value + ".png");
            e_Npc.Sprite = scrlSprite.Value;
        }

        private void scrlX_Scroll(object sender, ScrollEventArgs e)
        {
            lblX.Text = "X: " + (scrlX.Value);
            e_Npc.DesX = scrlX.Value;
        }

        private void scrlY_Scroll(object sender, ScrollEventArgs e)
        {
            lblY.Text = "Y: " + (scrlY.Value);
            e_Npc.DesY = scrlY.Value;
        }

        private void scrlDirection_Scroll(object sender, ScrollEventArgs e)
        {
            lblDirection.Text = "Direction: " + (scrlDirection.Value);
            e_Npc.Direction = scrlDirection.Value;
        }

        private void scrlStep_Scroll(object sender, ScrollEventArgs e)
        {
            lblStep.Text = "Step: " + (scrlStep.Value);
            e_Npc.Step = scrlStep.Value;
        }

        private void scrlOwner_Scroll(object sender, ScrollEventArgs e)
        {
            lblOwner.Text = "Owner: " + (scrlOwner.Value);
            e_Npc.Owner = scrlOwner.Value;
        }

        private void scrlSpawnTime_Scroll(object sender, ScrollEventArgs e)
        {
            lblSpawnTime.Text = "Spawn Time: " + (scrlSpawnTime.Value);
            e_Npc.SpawnTime = scrlSpawnTime.Value;
        }

        private void scrlHealth_Scroll(object sender, ScrollEventArgs e)
        {
            lblHealth.Text = "Health: " + (scrlHealth.Value);
            e_Npc.Health = scrlHealth.Value;
        }

        private void scrlMaxHealth_Scroll(object sender, ScrollEventArgs e)
        {
            lblMaxHealth.Text = "Max Health: " + (scrlMaxHealth.Value);
            e_Npc.MaxHealth = scrlMaxHealth.Value;
        }

        private void scrlDamage_Scroll(object sender, ScrollEventArgs e)
        {
            lblDamage.Text = "Damage: " + (scrlDamage.Value);
            e_Npc.Damage = scrlDamage.Value;
        }

        private void cmbBehavior_SelectedIndexChanged(object sender, EventArgs e)
        {
            e_Npc.Behavior = cmbBehavior.SelectedIndex;
        }

        private void scrlExp_Scroll(object sender, ScrollEventArgs e)
        {
            lblExp.Text = "Experience: " + scrlExp.Value;
            e_Npc.Exp = scrlExp.Value;
        }

        private void scrlMoney_Scroll(object sender, ScrollEventArgs e)
        {
            lblMoney.Text = "Money: " + scrlMoney.Value;
            e_Npc.Money = scrlMoney.Value;
        }

        private void scrlRange_Scroll(object sender, ScrollEventArgs e)
        {
            lblRange.Text = "Range: " + scrlRange.Value;
            e_Npc.Range = scrlRange.Value;
        }

        private void scrlShopNum_Scroll(object sender, ScrollEventArgs e)
        {
            lblShopNum.Text = "Shop: " + scrlShopNum.Value;
            e_Npc.ShopNum = scrlShopNum.Value;
        }

        private void scrlChatNum_Scroll(object sender, ScrollEventArgs e)
        {
            lblChatNum.Text = "Chat Number: " + scrlChatNum.Value;
            e_Npc.ChatNum = scrlChatNum.Value;
        }

        private void scrlMoveSpeed_Scroll(object sender, ScrollEventArgs e)
        {
            lblMoveSpeed.Text = "Move Speed: " + scrlMoveSpeed.Value;
            e_Npc.Speed = scrlMoveSpeed.Value;
        }
    }
}
