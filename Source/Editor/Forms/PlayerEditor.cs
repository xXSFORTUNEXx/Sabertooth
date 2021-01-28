using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using SabertoothServer;
using static System.Convert;
using static SabertoothServer.Globals;
using System.IO;

namespace Editor.Forms
{
    public partial class PlayerEditor : Form
    {
        Player e_Player = new Player();
        static int OffsetX = 12;
        static int OffsetY = 9;
        int SelectedIndex;
        bool UnModSave;

        public PlayerEditor()
        {
            InitializeComponent();
            picSprite.Image = Image.FromFile("Resources/Characters/1.png");
            scrlSprite.Maximum = Directory.GetFiles("Resources/Characters/", "*", SearchOption.TopDirectoryOnly).Length;
            LoadPlayerList();
        }

        private void LoadPlayerList()
        {
            string connection = "Data Source=" + Server.sqlServer + ";Initial Catalog=" + Server.sqlDatabase + ";Integrated Security=True";
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                string command = "SELECT COUNT(*) FROM PLAYERS";
                using (SqlCommand cmd = new SqlCommand(command, sql))
                {
                    object count = cmd.ExecuteScalar();
                    int result = ToInt32(count);
                    lstIndex.Items.Clear();
                    for (int i = 0; i < result; i++)
                    {
                        e_Player.LoadPlayerNameFromDatabase(i + 1);
                        lstIndex.Items.Add(e_Player.Name);
                    }
                }
            }
        }

        private void lstIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedIndex = (lstIndex.SelectedIndex + 1);
            if (SelectedIndex == 0) { return; }
            e_Player.LoadPlayerNameFromDatabase(SelectedIndex);
            e_Player.LoadPlayerFromDatabase();
            //General Textbox/Scroll Bars
            txtName.Text = e_Player.Name;
            txtPass.Text = e_Player.Pass;
            scrlSprite.Value = (e_Player.Sprite + 1);
            scrlMap.Value = (e_Player.Map + 1);
            scrlX.Value = (e_Player.X + OffsetX);
            scrlY.Value = (e_Player.Y + OffsetY);
            scrlDirection.Value = e_Player.Direction;
            scrlAimDirection.Value = e_Player.AimDirection;
            //Stats Scroll Bars
            scrlLevel.Value = (e_Player.Level);            
            scrlHealth.Value = (e_Player.Health);
            scrlMaxHealth.Value = (e_Player.MaxHealth);
            txtExp.Text = (e_Player.Experience.ToString());
            scrlMoney.Value = (e_Player.Wallet);
            scrlArmor.Value = (e_Player.Armor);
            scrlStrength.Value = (e_Player.Strength);
            scrlAgility.Value = (e_Player.Agility);
            scrlEndurance.Value = (e_Player.Intelligence);
            scrlStamina.Value = (e_Player.Stamina);
            //General Labels
            lblSprite.Text = "Sprite: " + (e_Player.Sprite + 1);
            lblMap.Text = "Map: " + (e_Player.Map + 1);
            lblX.Text = "X: " + (e_Player.X + OffsetX);
            lblY.Text = "Y: " + (e_Player.Y + OffsetY);
            switch (e_Player.Direction)
            {
                case (int)Directions.Down:
                    lblDirection.Text = "Direction: 0 - Down";
                    break;
                case (int)Directions.Left:
                    lblDirection.Text = "Direction: 1 - Left";
                    break;
                case (int)Directions.Right:
                    lblDirection.Text = "Direction: 2 - Right";
                    break;
                case (int)Directions.Up:
                    lblDirection.Text = "Direction: 3 - Up";
                    break;
            }
            switch (e_Player.AimDirection)
            {
                case (int)Directions.Down:
                    lblAimDirection.Text = "Aim Direction: 0 - Down";
                    break;
                case (int)Directions.Left:
                    lblAimDirection.Text = "Aim Direction: 1 - Left";
                    break;
                case (int)Directions.Right:
                    lblAimDirection.Text = "Aim Direction: 2 - Right";
                    break;
                case (int)Directions.Up:
                    lblAimDirection.Text = "Aim Direction: 3 - Up";
                    break;
            }
            txtKey.Text = (e_Player.AccountKey);
            txtStatus.Text = (e_Player.Active);
            txtLastLogged.Text = e_Player.LastLoggedIn;
            picSprite.Image = Image.FromFile("Resources/Characters/" + (e_Player.Sprite + 1) + ".png");
            //Stats Labels
            lblLevel.Text = "Level: " + (e_Player.Level);            
            lblHealth.Text = "Health: " + (e_Player.Health);
            lblMaxHealth.Text = "Max Health: " + (e_Player.MaxHealth);
            lblMoney.Text = "Money: " + (e_Player.Wallet);
            lblArmor.Text = "Armor: " + (e_Player.Armor);
            lblStrength.Text = "Strength: " + (e_Player.Strength);
            lblAgility.Text = "Agility: " + (e_Player.Agility);
            lblEndurance.Text = "Endurance: " + (e_Player.Intelligence);
            lblStamina.Text = "Stamina: " + (e_Player.Stamina);

            if (pnlGeneral.Visible == false) { pnlGeneral.Visible = true; }
            if (pnlStats.Visible == false) { pnlStats.Visible = true; }
            if (lstIndex.Enabled == false) { lstIndex.Enabled = true; }
            if (pnlActivation.Visible == false) { pnlActivation.Visible = true; }
            if (grpLastLogged.Visible == false) { grpLastLogged.Visible = true; }
            txtName.ReadOnly = true;
        }

        private void scrlSprite_Scroll(object sender, ScrollEventArgs e)
        {
            lblSprite.Text = "Sprite: " + (scrlSprite.Value + 1);
            picSprite.Image = Image.FromFile("Resources/Characters/" + (scrlSprite.Value + 1) + ".png");
            e_Player.Sprite = scrlSprite.Value;
        }

        private void chkPassMask_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPassMask.Checked == true)
            {
                txtPass.PasswordChar = '*';
            }
            else
            {
                txtPass.PasswordChar = char.MinValue;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.ReadOnly == false)
            {
                e_Player.Name = txtName.Text;
                e_Player.Pass = txtPass.Text;
                e_Player.CreatePlayerInDatabase();
                txtName.ReadOnly = true;
                chkPassMask.Checked = true;
                lstIndex.Enabled = true;
            }
            e_Player.SavePlayerToDatabase();
            LoadPlayerList();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

        private void scrlX_Scroll(object sender, ScrollEventArgs e)
        {
            lblX.Text = "X: " + (scrlX.Value);
            e_Player.X = (scrlX.Value - OffsetX);
        }

        private void scrlY_Scroll(object sender, ScrollEventArgs e)
        {
            lblY.Text = "Y: " + (scrlY.Value);
            e_Player.Y = (scrlY.Value - OffsetY);
        }

        private void btnNewItem_Click(object sender, EventArgs e)
        {
            e_Player = new Player("Default", "Password", "Email", PLAYER_START_X, PLAYER_START_Y, 0, 0, 0, 1, 100, 100, 100, 0, 100, 10, 100, 100, 1, 1, 1, 1, 1000);
            //e_Player.CreatePlayerInDatabase();
            //e_Player.LoadPlayerIDFromDatabase(e_Player.Name);
            txtName.ReadOnly = false;
            chkPassMask.Checked = false;
            txtKey.Text = (e_Player.AccountKey);
            txtStatus.Text = (e_Player.Active);
            txtLastLogged.Text = e_Player.LastLoggedIn;
            //General Textbox/Scroll Bars
            txtName.Text = e_Player.Name;
            txtPass.Text = e_Player.Pass;
            scrlSprite.Value = (e_Player.Sprite + 1);
            scrlMap.Value = (e_Player.Map + 1);
            scrlX.Value = (e_Player.X + OffsetX);
            scrlY.Value = (e_Player.Y + OffsetY);
            scrlDirection.Value = e_Player.Direction;
            scrlAimDirection.Value = e_Player.AimDirection;
            //Stats Scroll Bars
            scrlLevel.Value = (e_Player.Level);            
            scrlHealth.Value = (e_Player.Health);
            scrlMaxHealth.Value = (e_Player.MaxHealth);
            txtExp.Text = (e_Player.Experience.ToString());
            scrlMoney.Value = (e_Player.Wallet);
            scrlArmor.Value = (e_Player.Armor);
            scrlStrength.Value = (e_Player.Strength);
            scrlAgility.Value = (e_Player.Agility);
            scrlEndurance.Value = (e_Player.Intelligence);
            scrlStamina.Value = (e_Player.Stamina);
            //General Labels
            lblSprite.Text = "Sprite: " + (e_Player.Sprite + 1);
            lblMap.Text = "Map: " + (e_Player.Map + 1);
            lblX.Text = "X: " + (e_Player.X + OffsetX);
            lblY.Text = "Y: " + (e_Player.Y + OffsetY);
            switch (e_Player.Direction)
            {
                case (int)Directions.Down:
                    lblDirection.Text = "Direction: 0 - Down";
                    break;
                case (int)Directions.Left:
                    lblDirection.Text = "Direction: 1 - Left";
                    break;
                case (int)Directions.Right:
                    lblDirection.Text = "Direction: 2 - Right";
                    break;
                case (int)Directions.Up:
                    lblDirection.Text = "Direction: 3 - Up";
                    break;
            }
            switch (e_Player.AimDirection)
            {
                case (int)Directions.Down:
                    lblAimDirection.Text = "Aim Direction: 0 - Down";
                    break;
                case (int)Directions.Left:
                    lblAimDirection.Text = "Aim Direction: 1 - Left";
                    break;
                case (int)Directions.Right:
                    lblAimDirection.Text = "Aim Direction: 2 - Right";
                    break;
                case (int)Directions.Up:
                    lblAimDirection.Text = "Aim Direction: 3 - Up";
                    break;
            }
            picSprite.Image = Image.FromFile("Resources/Characters/" + (e_Player.Sprite + 1) + ".png");
            //Stats Labels
            lblLevel.Text = "Level: " + (e_Player.Level);            
            lblHealth.Text = "Health: " + (e_Player.Health);
            lblMaxHealth.Text = "Max Health: " + (e_Player.MaxHealth);
            lblMoney.Text = "Money: " + (e_Player.Wallet);
            lblArmor.Text = "Armor: " + (e_Player.Armor);
            lblStrength.Text = "Strength: " + (e_Player.Strength);
            lblAgility.Text = "Agility: " + (e_Player.Agility);
            lblEndurance.Text = "Endurance: " + (e_Player.Intelligence);
            lblStamina.Text = "Stamina: " + (e_Player.Stamina);

            pnlStats.Visible = true;            
            pnlGeneral.Visible = true;
            grpLastLogged.Visible = true;
            pnlActivation.Visible = true;
            lstIndex.Enabled = false;
            lstIndex.Items.Add(e_Player.Name);
        }

        private void scrlMap_Scroll(object sender, ScrollEventArgs e)
        {
            lblMap.Text = "Map: " + (scrlMap.Value);
            e_Player.Map = (scrlMap.Value - 1);
        }

        private void scrlDirection_Scroll(object sender, ScrollEventArgs e)
        {
            switch (scrlDirection.Value)
            {
                case (int)Directions.Down:
                    e_Player.Direction = (int)Directions.Down;
                    lblDirection.Text = "Direction: 0 - Down";
                    break;
                case (int)Directions.Left:
                    e_Player.Direction = (int)Directions.Left;
                    lblDirection.Text = "Direction: 1 - Left";
                    break;
                case (int)Directions.Right:
                    e_Player.Direction = (int)Directions.Right;
                    lblDirection.Text = "Direction: 2 - Right";
                    break;
                case (int)Directions.Up:
                    e_Player.Direction = (int)Directions.Up;
                    lblDirection.Text = "Direction: 3 - Up";
                    break;
            }
        }

        private void scrlAimDirection_Scroll(object sender, ScrollEventArgs e)
        {
            switch (scrlAimDirection.Value)
            {
                case (int)Directions.Down:
                    e_Player.AimDirection = (int)Directions.Down;
                    lblAimDirection.Text = "Aim Direction: 0 - Down";
                    break;
                case (int)Directions.Left:
                    e_Player.AimDirection = (int)Directions.Left;
                    lblAimDirection.Text = "Aim Direction: 1 - Left";
                    break;
                case (int)Directions.Right:
                    e_Player.AimDirection = (int)Directions.Right;
                    lblAimDirection.Text = "Aim Direction: 2 - Right";
                    break;
                case (int)Directions.Up:
                    e_Player.AimDirection = (int)Directions.Up;
                    lblAimDirection.Text = "Aim Direction: 3 - Up";
                    break;
            }
        }

        private void scrlLevel_Scroll(object sender, ScrollEventArgs e)
        {
            lblLevel.Text = "Level: " + (scrlLevel.Value);
            e_Player.Level = (scrlLevel.Value);
        }

        private void scrlHealth_Scroll(object sender, ScrollEventArgs e)
        {
            lblHealth.Text = "Health: " + (scrlHealth.Value);
            e_Player.Health = (scrlHealth.Value);
        }

        private void scrlMaxHealth_Scroll(object sender, ScrollEventArgs e)
        {
            lblMaxHealth.Text = "Max Health: " + (scrlMaxHealth.Value);
            e_Player.MaxHealth = (scrlMaxHealth.Value);
        }

        private void scrlMoney_Scroll(object sender, ScrollEventArgs e)
        {
            lblMoney.Text = "Money: " + (scrlMoney.Value);
            e_Player.Wallet = (scrlMoney.Value);
        }

        private void scrlArmor_Scroll(object sender, ScrollEventArgs e)
        {
            lblArmor.Text = "Armor: " + (scrlArmor.Value);
            e_Player.Armor = (scrlArmor.Value);
        }        

        private void scrlStrength_Scroll(object sender, ScrollEventArgs e)
        {
            lblStrength.Text = "Strength: " + (scrlStrength.Value);
            e_Player.Strength = (scrlStrength.Value);
        }

        private void scrlAgility_Scroll(object sender, ScrollEventArgs e)
        {
            lblAgility.Text = "Agility: " + (scrlAgility.Value);
            e_Player.Agility = (scrlAgility.Value);
        }

        private void scrlEndurance_Scroll(object sender, ScrollEventArgs e)
        {
            lblEndurance.Text = "Endurance: " + (scrlEndurance.Value);
            e_Player.Intelligence = (scrlEndurance.Value);
        }

        private void scrlStamina_Scroll(object sender, ScrollEventArgs e)
        {
            lblStamina.Text = "Stamina: " + (scrlStamina.Value);
            e_Player.Stamina = (scrlStamina.Value);
        }        

        private void txtExp_TextChanged(object sender, EventArgs e)
        {
            e_Player.Experience = ToInt32(txtExp.Text);
        }
    }
}
