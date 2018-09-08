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
using System.Data.SQLite;
using SabertoothServer;
using static System.Convert;
using static SabertoothServer.Globals;

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
            scrlSprite.Maximum = 7;
            LoadPlayerList();
        }

        private void LoadPlayerList()
        {
            if (Server.DBType == SQL_DATABASE_REMOTE.ToString())
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
            else
            {
                using (SQLiteConnection conn = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;"))
                {
                    conn.Open();
                    string sql;

                    sql = "SELECT COUNT(*) FROM PLAYERS";

                    object queue;
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        queue = cmd.ExecuteScalar();
                    }
                    int result = ToInt32(queue);
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
            scrlPoints.Value = (e_Player.Points);
            scrlHealth.Value = (e_Player.Health);
            scrlMaxHealth.Value = (e_Player.MaxHealth);
            txtExp.Text = (e_Player.Experience.ToString());
            scrlMoney.Value = (e_Player.Money);
            scrlArmor.Value = (e_Player.Armor);
            scrlHunger.Value = (e_Player.Hunger);
            scrlHydration.Value = (e_Player.Hydration);
            scrlStrength.Value = (e_Player.Strength);
            scrlAgility.Value = (e_Player.Agility);
            scrlEndurance.Value = (e_Player.Endurance);
            scrlStamina.Value = (e_Player.Stamina);
            //Ammo Scroll Bars
            scrlPistol.Value = (e_Player.PistolAmmo);
            scrlAssault.Value = (e_Player.AssaultAmmo);
            scrlRocket.Value = (e_Player.RocketAmmo);
            scrlGrenade.Value = (e_Player.GrenadeAmmo);
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
            picSprite.Image = Image.FromFile("Resources/Characters/" + (e_Player.Sprite + 1) + ".png");
            //Stats Labels
            lblLevel.Text = "Level: " + (e_Player.Level);
            lblPoints.Text = "Points: " + (e_Player.Points);
            lblHealth.Text = "Health: " + (e_Player.Health);
            lblMaxHealth.Text = "Max Health: " + (e_Player.MaxHealth);
            lblMoney.Text = "Money: " + (e_Player.Money);
            lblArmor.Text = "Armor: " + (e_Player.Armor);
            lblHunger.Text = "Hunger: " + (e_Player.Hunger);
            lblHydration.Text = "Hydration: " + (e_Player.Hydration);
            lblStrength.Text = "Strength: " + (e_Player.Strength);
            lblAgility.Text = "Agility: " + (e_Player.Agility);
            lblEndurance.Text = "Endurance: " + (e_Player.Endurance);
            lblStamina.Text = "Stamina: " + (e_Player.Stamina);
            //Ammo Labels
            lblPistol.Text = "Pistol Ammo: " + (e_Player.PistolAmmo);
            lblAssault.Text = "Assault Ammo: " + (e_Player.AssaultAmmo);
            lblRocket.Text = "Rocket Ammo: " + (e_Player.RocketAmmo);
            lblGrenade.Text = "Grenade Ammo: " + (e_Player.GrenadeAmmo);

            if (pnlGeneral.Visible == false) { pnlGeneral.Visible = true; }
            if (pnlStats.Visible == false) { pnlStats.Visible = true; }
            if (pnlAmmo.Visible == false) { pnlAmmo.Visible = true; }
            if (lstIndex.Enabled == false) { lstIndex.Enabled = true; }
            if (pnlActivation.Visible == false) { pnlActivation.Visible = true; }
            txtName.Enabled = false;
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
            if (txtName.Enabled == true)
            {
                e_Player.Name = txtName.Text;
                e_Player.Pass = txtPass.Text;
                e_Player.CreatePlayerInDatabase();
                txtName.Enabled = false;
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
            txtName.Enabled = true;
            chkPassMask.Checked = false;

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
            scrlPoints.Value = (e_Player.Points);
            scrlHealth.Value = (e_Player.Health);
            scrlMaxHealth.Value = (e_Player.MaxHealth);
            txtExp.Text = (e_Player.Experience.ToString());
            scrlMoney.Value = (e_Player.Money);
            scrlArmor.Value = (e_Player.Armor);
            scrlHunger.Value = (e_Player.Hunger);
            scrlHydration.Value = (e_Player.Hydration);
            scrlStrength.Value = (e_Player.Strength);
            scrlAgility.Value = (e_Player.Agility);
            scrlEndurance.Value = (e_Player.Endurance);
            scrlStamina.Value = (e_Player.Stamina);
            //Ammo Scroll Bars
            scrlPistol.Value = (e_Player.PistolAmmo);
            scrlAssault.Value = (e_Player.AssaultAmmo);
            scrlRocket.Value = (e_Player.RocketAmmo);
            scrlGrenade.Value = (e_Player.GrenadeAmmo);
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
            lblPoints.Text = "Points: " + (e_Player.Points);
            lblHealth.Text = "Health: " + (e_Player.Health);
            lblMaxHealth.Text = "Max Health: " + (e_Player.MaxHealth);
            lblMoney.Text = "Money: " + (e_Player.Money);
            lblArmor.Text = "Armor: " + (e_Player.Armor);
            lblHunger.Text = "Hunger: " + (e_Player.Hunger);
            lblHydration.Text = "Hydration: " + (e_Player.Hydration);
            lblStrength.Text = "Strength: " + (e_Player.Strength);
            lblAgility.Text = "Agility: " + (e_Player.Agility);
            lblEndurance.Text = "Endurance: " + (e_Player.Endurance);
            lblStamina.Text = "Stamina: " + (e_Player.Stamina);
            //Ammo Labels
            lblPistol.Text = "Pistol Ammo: " + (e_Player.PistolAmmo);
            lblAssault.Text = "Assault Ammo: " + (e_Player.AssaultAmmo);
            lblRocket.Text = "Rocket Ammo: " + (e_Player.RocketAmmo);
            lblGrenade.Text = "Grenade Ammo: " + (e_Player.GrenadeAmmo);

            pnlStats.Visible = true;
            pnlAmmo.Visible = true;
            pnlGeneral.Visible = true;
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

        private void scrlPoints_Scroll(object sender, ScrollEventArgs e)
        {
            lblPoints.Text = "Points: " + (scrlPoints.Value);
            e_Player.Points = (scrlPoints.Value);
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
            e_Player.Money = (scrlMoney.Value);
        }

        private void scrlArmor_Scroll(object sender, ScrollEventArgs e)
        {
            lblArmor.Text = "Armor: " + (scrlArmor.Value);
            e_Player.Armor = (scrlArmor.Value);
        }

        private void scrlHunger_Scroll(object sender, ScrollEventArgs e)
        {
            lblHunger.Text = "Hunger: " + (scrlHunger.Value);
            e_Player.Hunger = (scrlHunger.Value);
        }

        private void scrlHydration_Scroll(object sender, ScrollEventArgs e)
        {
            lblHydration.Text = "Hydration: " + (scrlHydration.Value);
            e_Player.Hydration = (scrlHydration.Value);
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
            e_Player.Endurance = (scrlEndurance.Value);
        }

        private void scrlStamina_Scroll(object sender, ScrollEventArgs e)
        {
            lblStamina.Text = "Stamina: " + (scrlStamina.Value);
            e_Player.Stamina = (scrlStamina.Value);
        }

        private void scrlPistol_Scroll(object sender, ScrollEventArgs e)
        {
            lblPistol.Text = "Pistol Ammo: " + (scrlPistol.Value);
            e_Player.PistolAmmo = (scrlPistol.Value);
        }

        private void scrlAssault_Scroll(object sender, ScrollEventArgs e)
        {
            lblAssault.Text = "Assault Ammo: " + (scrlAssault.Value);
            e_Player.AssaultAmmo = (scrlAssault.Value);
        }

        private void scrlRocket_Scroll(object sender, ScrollEventArgs e)
        {
            lblRocket.Text = "Rocket Ammo: " + (scrlRocket.Value);
            e_Player.RocketAmmo = (scrlRocket.Value);
        }

        private void scrlGrenade_Scroll(object sender, ScrollEventArgs e)
        {
            lblGrenade.Text = "Grenade Ammo: " + (scrlGrenade.Value);
            e_Player.GrenadeAmmo = (scrlGrenade.Value);
        }

        private void txtExp_TextChanged(object sender, EventArgs e)
        {
            e_Player.Experience = ToInt32(txtExp.Text);
        }
    }
}
