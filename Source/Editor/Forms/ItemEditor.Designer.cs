namespace Editor.Forms
{
    partial class ItemEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlMain = new System.Windows.Forms.GroupBox();
            this.scrlPrice = new System.Windows.Forms.HScrollBar();
            this.lblPrice = new System.Windows.Forms.Label();
            this.scrlAttackSpeed = new System.Windows.Forms.HScrollBar();
            this.lblAttackSpeed = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.scrlArmor = new System.Windows.Forms.HScrollBar();
            this.lblArmor = new System.Windows.Forms.Label();
            this.scrlDamage = new System.Windows.Forms.HScrollBar();
            this.lblDamage = new System.Windows.Forms.Label();
            this.picSprite = new System.Windows.Forms.PictureBox();
            this.lblSprite = new System.Windows.Forms.Label();
            this.scrlSprite = new System.Windows.Forms.HScrollBar();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnNewItem = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lstIndex = new System.Windows.Forms.ListBox();
            this.pnlRanged = new System.Windows.Forms.GroupBox();
            this.scrlReloadSpeed = new System.Windows.Forms.HScrollBar();
            this.lblReloadSpeed = new System.Windows.Forms.Label();
            this.cmbAmmoType = new System.Windows.Forms.ComboBox();
            this.lblAmmoType = new System.Windows.Forms.Label();
            this.picProj = new System.Windows.Forms.PictureBox();
            this.scrlProjNum = new System.Windows.Forms.HScrollBar();
            this.lblProjNum = new System.Windows.Forms.Label();
            this.scrlMaxClip = new System.Windows.Forms.HScrollBar();
            this.lblMaxClip = new System.Windows.Forms.Label();
            this.scrlClip = new System.Windows.Forms.HScrollBar();
            this.lblClip = new System.Windows.Forms.Label();
            this.pnlConsume = new System.Windows.Forms.GroupBox();
            this.scrlHydrateRestore = new System.Windows.Forms.HScrollBar();
            this.lblHydrateRestore = new System.Windows.Forms.Label();
            this.scrlHungerRestore = new System.Windows.Forms.HScrollBar();
            this.lblHungerRestore = new System.Windows.Forms.Label();
            this.scrlHealthRestore = new System.Windows.Forms.HScrollBar();
            this.lblHealthRestore = new System.Windows.Forms.Label();
            this.pnlStats = new System.Windows.Forms.GroupBox();
            this.scrlStamina = new System.Windows.Forms.HScrollBar();
            this.lblStamina = new System.Windows.Forms.Label();
            this.scrlEndurance = new System.Windows.Forms.HScrollBar();
            this.lblEndurance = new System.Windows.Forms.Label();
            this.scrlAgility = new System.Windows.Forms.HScrollBar();
            this.lblAgility = new System.Windows.Forms.Label();
            this.scrlStrength = new System.Windows.Forms.HScrollBar();
            this.lblStrength = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSprite)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.pnlRanged.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProj)).BeginInit();
            this.pnlConsume.SuspendLayout();
            this.pnlStats.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.scrlPrice);
            this.pnlMain.Controls.Add(this.lblPrice);
            this.pnlMain.Controls.Add(this.scrlAttackSpeed);
            this.pnlMain.Controls.Add(this.lblAttackSpeed);
            this.pnlMain.Controls.Add(this.cmbType);
            this.pnlMain.Controls.Add(this.lblType);
            this.pnlMain.Controls.Add(this.scrlArmor);
            this.pnlMain.Controls.Add(this.lblArmor);
            this.pnlMain.Controls.Add(this.scrlDamage);
            this.pnlMain.Controls.Add(this.lblDamage);
            this.pnlMain.Controls.Add(this.picSprite);
            this.pnlMain.Controls.Add(this.lblSprite);
            this.pnlMain.Controls.Add(this.scrlSprite);
            this.pnlMain.Controls.Add(this.txtName);
            this.pnlMain.Controls.Add(this.lblName);
            this.pnlMain.Location = new System.Drawing.Point(159, 12);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(197, 380);
            this.pnlMain.TabIndex = 5;
            this.pnlMain.TabStop = false;
            this.pnlMain.Text = "Properties";
            this.pnlMain.Visible = false;
            // 
            // scrlPrice
            // 
            this.scrlPrice.LargeChange = 1;
            this.scrlPrice.Location = new System.Drawing.Point(17, 215);
            this.scrlPrice.Maximum = 50000;
            this.scrlPrice.Name = "scrlPrice";
            this.scrlPrice.Size = new System.Drawing.Size(156, 17);
            this.scrlPrice.TabIndex = 21;
            this.scrlPrice.Value = 1;
            this.scrlPrice.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlPrice_Scroll);
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(17, 201);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(43, 13);
            this.lblPrice.TabIndex = 20;
            this.lblPrice.Text = "Price: 1";
            // 
            // scrlAttackSpeed
            // 
            this.scrlAttackSpeed.LargeChange = 100;
            this.scrlAttackSpeed.Location = new System.Drawing.Point(17, 182);
            this.scrlAttackSpeed.Maximum = 5000;
            this.scrlAttackSpeed.Name = "scrlAttackSpeed";
            this.scrlAttackSpeed.Size = new System.Drawing.Size(156, 17);
            this.scrlAttackSpeed.SmallChange = 100;
            this.scrlAttackSpeed.TabIndex = 19;
            this.scrlAttackSpeed.Value = 1;
            this.scrlAttackSpeed.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlAttackSpeed_Scroll);
            // 
            // lblAttackSpeed
            // 
            this.lblAttackSpeed.AutoSize = true;
            this.lblAttackSpeed.Location = new System.Drawing.Point(17, 169);
            this.lblAttackSpeed.Name = "lblAttackSpeed";
            this.lblAttackSpeed.Size = new System.Drawing.Size(84, 13);
            this.lblAttackSpeed.TabIndex = 18;
            this.lblAttackSpeed.Text = "Attack Speed: 0";
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            "None",
            "MeleeWeapon",
            "RangedWeapon",
            "Currency",
            "Food",
            "Drink",
            "FirstAid",
            "Shirt",
            "Pants",
            "Shoes",
            "Other"});
            this.cmbType.Location = new System.Drawing.Point(17, 259);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(156, 21);
            this.cmbType.TabIndex = 15;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(17, 242);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(34, 13);
            this.lblType.TabIndex = 14;
            this.lblType.Text = "Type:";
            // 
            // scrlArmor
            // 
            this.scrlArmor.Location = new System.Drawing.Point(17, 152);
            this.scrlArmor.Name = "scrlArmor";
            this.scrlArmor.Size = new System.Drawing.Size(156, 17);
            this.scrlArmor.TabIndex = 13;
            this.scrlArmor.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlArmor_Scroll);
            // 
            // lblArmor
            // 
            this.lblArmor.AutoSize = true;
            this.lblArmor.Location = new System.Drawing.Point(17, 139);
            this.lblArmor.Name = "lblArmor";
            this.lblArmor.Size = new System.Drawing.Size(46, 13);
            this.lblArmor.TabIndex = 12;
            this.lblArmor.Text = "Armor: 0";
            // 
            // scrlDamage
            // 
            this.scrlDamage.Location = new System.Drawing.Point(17, 122);
            this.scrlDamage.Name = "scrlDamage";
            this.scrlDamage.Size = new System.Drawing.Size(156, 17);
            this.scrlDamage.TabIndex = 11;
            this.scrlDamage.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlDamage_Scroll_1);
            // 
            // lblDamage
            // 
            this.lblDamage.AutoSize = true;
            this.lblDamage.Location = new System.Drawing.Point(17, 109);
            this.lblDamage.Name = "lblDamage";
            this.lblDamage.Size = new System.Drawing.Size(59, 13);
            this.lblDamage.TabIndex = 10;
            this.lblDamage.Text = "Damage: 0";
            // 
            // picSprite
            // 
            this.picSprite.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.picSprite.Location = new System.Drawing.Point(23, 69);
            this.picSprite.Name = "picSprite";
            this.picSprite.Size = new System.Drawing.Size(32, 32);
            this.picSprite.TabIndex = 9;
            this.picSprite.TabStop = false;
            // 
            // lblSprite
            // 
            this.lblSprite.AutoSize = true;
            this.lblSprite.Location = new System.Drawing.Point(61, 69);
            this.lblSprite.Name = "lblSprite";
            this.lblSprite.Size = new System.Drawing.Size(46, 13);
            this.lblSprite.TabIndex = 8;
            this.lblSprite.Text = "Sprite: 0";
            // 
            // scrlSprite
            // 
            this.scrlSprite.LargeChange = 1;
            this.scrlSprite.Location = new System.Drawing.Point(58, 84);
            this.scrlSprite.Maximum = 4;
            this.scrlSprite.Minimum = 1;
            this.scrlSprite.Name = "scrlSprite";
            this.scrlSprite.Size = new System.Drawing.Size(118, 17);
            this.scrlSprite.TabIndex = 7;
            this.scrlSprite.Value = 1;
            this.scrlSprite.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlSprite_Scroll);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(20, 43);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(156, 20);
            this.txtName.TabIndex = 6;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(17, 27);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 5;
            this.lblName.Text = "Name:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.btnNewItem);
            this.groupBox1.Controls.Add(this.btnExit);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.lstIndex);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(141, 380);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Index";
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(6, 321);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(129, 23);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnNewItem
            // 
            this.btnNewItem.Location = new System.Drawing.Point(6, 263);
            this.btnNewItem.Name = "btnNewItem";
            this.btnNewItem.Size = new System.Drawing.Size(129, 23);
            this.btnNewItem.TabIndex = 6;
            this.btnNewItem.Text = "New";
            this.btnNewItem.UseVisualStyleBackColor = true;
            this.btnNewItem.Click += new System.EventHandler(this.btnNewItem_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(6, 350);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(129, 23);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(6, 292);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(129, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lstIndex
            // 
            this.lstIndex.FormattingEnabled = true;
            this.lstIndex.Location = new System.Drawing.Point(6, 19);
            this.lstIndex.Name = "lstIndex";
            this.lstIndex.Size = new System.Drawing.Size(129, 238);
            this.lstIndex.TabIndex = 3;
            this.lstIndex.SelectedIndexChanged += new System.EventHandler(this.lstIndex_SelectedIndexChanged);
            // 
            // pnlRanged
            // 
            this.pnlRanged.Controls.Add(this.scrlReloadSpeed);
            this.pnlRanged.Controls.Add(this.lblReloadSpeed);
            this.pnlRanged.Controls.Add(this.cmbAmmoType);
            this.pnlRanged.Controls.Add(this.lblAmmoType);
            this.pnlRanged.Controls.Add(this.picProj);
            this.pnlRanged.Controls.Add(this.scrlProjNum);
            this.pnlRanged.Controls.Add(this.lblProjNum);
            this.pnlRanged.Controls.Add(this.scrlMaxClip);
            this.pnlRanged.Controls.Add(this.lblMaxClip);
            this.pnlRanged.Controls.Add(this.scrlClip);
            this.pnlRanged.Controls.Add(this.lblClip);
            this.pnlRanged.Location = new System.Drawing.Point(568, 13);
            this.pnlRanged.Name = "pnlRanged";
            this.pnlRanged.Size = new System.Drawing.Size(184, 256);
            this.pnlRanged.TabIndex = 8;
            this.pnlRanged.TabStop = false;
            this.pnlRanged.Text = "Ranged Properties";
            this.pnlRanged.Visible = false;
            // 
            // scrlReloadSpeed
            // 
            this.scrlReloadSpeed.LargeChange = 100;
            this.scrlReloadSpeed.Location = new System.Drawing.Point(15, 138);
            this.scrlReloadSpeed.Maximum = 5000;
            this.scrlReloadSpeed.Name = "scrlReloadSpeed";
            this.scrlReloadSpeed.Size = new System.Drawing.Size(156, 17);
            this.scrlReloadSpeed.SmallChange = 100;
            this.scrlReloadSpeed.TabIndex = 52;
            this.scrlReloadSpeed.Value = 1;
            this.scrlReloadSpeed.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlReloadSpeed_Scroll);
            // 
            // lblReloadSpeed
            // 
            this.lblReloadSpeed.AutoSize = true;
            this.lblReloadSpeed.Location = new System.Drawing.Point(12, 126);
            this.lblReloadSpeed.Name = "lblReloadSpeed";
            this.lblReloadSpeed.Size = new System.Drawing.Size(87, 13);
            this.lblReloadSpeed.TabIndex = 51;
            this.lblReloadSpeed.Text = "Reload Speed: 0";
            // 
            // cmbAmmoType
            // 
            this.cmbAmmoType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAmmoType.FormattingEnabled = true;
            this.cmbAmmoType.Items.AddRange(new object[] {
            "None",
            "Pistol",
            "Assault Rifle",
            "Rocket",
            "Grenade"});
            this.cmbAmmoType.Location = new System.Drawing.Point(15, 32);
            this.cmbAmmoType.Name = "cmbAmmoType";
            this.cmbAmmoType.Size = new System.Drawing.Size(156, 21);
            this.cmbAmmoType.TabIndex = 50;
            this.cmbAmmoType.SelectedIndexChanged += new System.EventHandler(this.cmbAmmoType_SelectedIndexChanged);
            // 
            // lblAmmoType
            // 
            this.lblAmmoType.AutoSize = true;
            this.lblAmmoType.Location = new System.Drawing.Point(12, 18);
            this.lblAmmoType.Name = "lblAmmoType";
            this.lblAmmoType.Size = new System.Drawing.Size(66, 13);
            this.lblAmmoType.TabIndex = 49;
            this.lblAmmoType.Text = "Ammo Type:";
            // 
            // picProj
            // 
            this.picProj.BackColor = System.Drawing.Color.Black;
            this.picProj.Location = new System.Drawing.Point(15, 203);
            this.picProj.Name = "picProj";
            this.picProj.Size = new System.Drawing.Size(32, 32);
            this.picProj.TabIndex = 48;
            this.picProj.TabStop = false;
            // 
            // scrlProjNum
            // 
            this.scrlProjNum.LargeChange = 1;
            this.scrlProjNum.Location = new System.Drawing.Point(15, 175);
            this.scrlProjNum.Minimum = 1;
            this.scrlProjNum.Name = "scrlProjNum";
            this.scrlProjNum.Size = new System.Drawing.Size(154, 17);
            this.scrlProjNum.TabIndex = 47;
            this.scrlProjNum.Value = 1;
            this.scrlProjNum.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlProjNum_Scroll);
            // 
            // lblProjNum
            // 
            this.lblProjNum.AutoSize = true;
            this.lblProjNum.Location = new System.Drawing.Point(12, 160);
            this.lblProjNum.Name = "lblProjNum";
            this.lblProjNum.Size = new System.Drawing.Size(62, 13);
            this.lblProjNum.TabIndex = 46;
            this.lblProjNum.Text = "Projectile: 0";
            // 
            // scrlMaxClip
            // 
            this.scrlMaxClip.Location = new System.Drawing.Point(15, 104);
            this.scrlMaxClip.Name = "scrlMaxClip";
            this.scrlMaxClip.Size = new System.Drawing.Size(156, 17);
            this.scrlMaxClip.TabIndex = 45;
            this.scrlMaxClip.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlMaxClip_Scroll);
            // 
            // lblMaxClip
            // 
            this.lblMaxClip.AutoSize = true;
            this.lblMaxClip.Location = new System.Drawing.Point(12, 89);
            this.lblMaxClip.Name = "lblMaxClip";
            this.lblMaxClip.Size = new System.Drawing.Size(59, 13);
            this.lblMaxClip.TabIndex = 44;
            this.lblMaxClip.Text = "Max Clip: 0";
            // 
            // scrlClip
            // 
            this.scrlClip.Location = new System.Drawing.Point(15, 69);
            this.scrlClip.Name = "scrlClip";
            this.scrlClip.Size = new System.Drawing.Size(156, 17);
            this.scrlClip.TabIndex = 43;
            this.scrlClip.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlClip_Scroll);
            // 
            // lblClip
            // 
            this.lblClip.AutoSize = true;
            this.lblClip.Location = new System.Drawing.Point(12, 56);
            this.lblClip.Name = "lblClip";
            this.lblClip.Size = new System.Drawing.Size(36, 13);
            this.lblClip.TabIndex = 42;
            this.lblClip.Text = "Clip: 0";
            // 
            // pnlConsume
            // 
            this.pnlConsume.Controls.Add(this.scrlHydrateRestore);
            this.pnlConsume.Controls.Add(this.lblHydrateRestore);
            this.pnlConsume.Controls.Add(this.scrlHungerRestore);
            this.pnlConsume.Controls.Add(this.lblHungerRestore);
            this.pnlConsume.Controls.Add(this.scrlHealthRestore);
            this.pnlConsume.Controls.Add(this.lblHealthRestore);
            this.pnlConsume.Location = new System.Drawing.Point(362, 188);
            this.pnlConsume.Name = "pnlConsume";
            this.pnlConsume.Size = new System.Drawing.Size(200, 204);
            this.pnlConsume.TabIndex = 9;
            this.pnlConsume.TabStop = false;
            this.pnlConsume.Text = "Consumable";
            this.pnlConsume.Visible = false;
            // 
            // scrlHydrateRestore
            // 
            this.scrlHydrateRestore.Location = new System.Drawing.Point(23, 96);
            this.scrlHydrateRestore.Name = "scrlHydrateRestore";
            this.scrlHydrateRestore.Size = new System.Drawing.Size(156, 17);
            this.scrlHydrateRestore.TabIndex = 33;
            this.scrlHydrateRestore.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlHydrateRestore_Scroll);
            // 
            // lblHydrateRestore
            // 
            this.lblHydrateRestore.AutoSize = true;
            this.lblHydrateRestore.Location = new System.Drawing.Point(23, 83);
            this.lblHydrateRestore.Name = "lblHydrateRestore";
            this.lblHydrateRestore.Size = new System.Drawing.Size(96, 13);
            this.lblHydrateRestore.TabIndex = 32;
            this.lblHydrateRestore.Text = "Hydrate Restore: 0";
            // 
            // scrlHungerRestore
            // 
            this.scrlHungerRestore.Location = new System.Drawing.Point(23, 66);
            this.scrlHungerRestore.Name = "scrlHungerRestore";
            this.scrlHungerRestore.Size = new System.Drawing.Size(156, 17);
            this.scrlHungerRestore.TabIndex = 31;
            this.scrlHungerRestore.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlHungerRestore_Scroll);
            // 
            // lblHungerRestore
            // 
            this.lblHungerRestore.AutoSize = true;
            this.lblHungerRestore.Location = new System.Drawing.Point(23, 53);
            this.lblHungerRestore.Name = "lblHungerRestore";
            this.lblHungerRestore.Size = new System.Drawing.Size(94, 13);
            this.lblHungerRestore.TabIndex = 30;
            this.lblHungerRestore.Text = "Hunger Restore: 0";
            // 
            // scrlHealthRestore
            // 
            this.scrlHealthRestore.Location = new System.Drawing.Point(23, 36);
            this.scrlHealthRestore.Name = "scrlHealthRestore";
            this.scrlHealthRestore.Size = new System.Drawing.Size(156, 17);
            this.scrlHealthRestore.TabIndex = 29;
            this.scrlHealthRestore.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlHealthRestore_Scroll);
            // 
            // lblHealthRestore
            // 
            this.lblHealthRestore.AutoSize = true;
            this.lblHealthRestore.Location = new System.Drawing.Point(23, 23);
            this.lblHealthRestore.Name = "lblHealthRestore";
            this.lblHealthRestore.Size = new System.Drawing.Size(90, 13);
            this.lblHealthRestore.TabIndex = 28;
            this.lblHealthRestore.Text = "Health Restore: 0";
            // 
            // pnlStats
            // 
            this.pnlStats.Controls.Add(this.scrlStamina);
            this.pnlStats.Controls.Add(this.lblStamina);
            this.pnlStats.Controls.Add(this.scrlEndurance);
            this.pnlStats.Controls.Add(this.lblEndurance);
            this.pnlStats.Controls.Add(this.scrlAgility);
            this.pnlStats.Controls.Add(this.lblAgility);
            this.pnlStats.Controls.Add(this.scrlStrength);
            this.pnlStats.Controls.Add(this.lblStrength);
            this.pnlStats.Location = new System.Drawing.Point(362, 12);
            this.pnlStats.Name = "pnlStats";
            this.pnlStats.Size = new System.Drawing.Size(200, 162);
            this.pnlStats.TabIndex = 10;
            this.pnlStats.TabStop = false;
            this.pnlStats.Text = "Stats";
            this.pnlStats.Visible = false;
            // 
            // scrlStamina
            // 
            this.scrlStamina.Location = new System.Drawing.Point(24, 124);
            this.scrlStamina.Name = "scrlStamina";
            this.scrlStamina.Size = new System.Drawing.Size(156, 17);
            this.scrlStamina.TabIndex = 43;
            this.scrlStamina.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlStamina_Scroll);
            // 
            // lblStamina
            // 
            this.lblStamina.AutoSize = true;
            this.lblStamina.Location = new System.Drawing.Point(21, 111);
            this.lblStamina.Name = "lblStamina";
            this.lblStamina.Size = new System.Drawing.Size(57, 13);
            this.lblStamina.TabIndex = 42;
            this.lblStamina.Text = "Stamina: 0";
            // 
            // scrlEndurance
            // 
            this.scrlEndurance.Location = new System.Drawing.Point(24, 94);
            this.scrlEndurance.Name = "scrlEndurance";
            this.scrlEndurance.Size = new System.Drawing.Size(156, 17);
            this.scrlEndurance.TabIndex = 41;
            this.scrlEndurance.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlEndurance_Scroll);
            // 
            // lblEndurance
            // 
            this.lblEndurance.AutoSize = true;
            this.lblEndurance.Location = new System.Drawing.Point(21, 81);
            this.lblEndurance.Name = "lblEndurance";
            this.lblEndurance.Size = new System.Drawing.Size(71, 13);
            this.lblEndurance.TabIndex = 40;
            this.lblEndurance.Text = "Endurance: 0";
            // 
            // scrlAgility
            // 
            this.scrlAgility.Location = new System.Drawing.Point(24, 64);
            this.scrlAgility.Name = "scrlAgility";
            this.scrlAgility.Size = new System.Drawing.Size(156, 17);
            this.scrlAgility.TabIndex = 39;
            this.scrlAgility.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlAgility_Scroll);
            // 
            // lblAgility
            // 
            this.lblAgility.AutoSize = true;
            this.lblAgility.Location = new System.Drawing.Point(21, 51);
            this.lblAgility.Name = "lblAgility";
            this.lblAgility.Size = new System.Drawing.Size(46, 13);
            this.lblAgility.TabIndex = 38;
            this.lblAgility.Text = "Agility: 0";
            // 
            // scrlStrength
            // 
            this.scrlStrength.Location = new System.Drawing.Point(24, 34);
            this.scrlStrength.Name = "scrlStrength";
            this.scrlStrength.Size = new System.Drawing.Size(156, 17);
            this.scrlStrength.TabIndex = 37;
            this.scrlStrength.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlStrength_Scroll);
            // 
            // lblStrength
            // 
            this.lblStrength.AutoSize = true;
            this.lblStrength.Location = new System.Drawing.Point(21, 21);
            this.lblStrength.Name = "lblStrength";
            this.lblStrength.Size = new System.Drawing.Size(59, 13);
            this.lblStrength.TabIndex = 36;
            this.lblStrength.Text = "Strength: 0";
            // 
            // ItemEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 401);
            this.Controls.Add(this.pnlStats);
            this.Controls.Add(this.pnlConsume);
            this.Controls.Add(this.pnlRanged);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ItemEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Item Editor";
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSprite)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.pnlRanged.ResumeLayout(false);
            this.pnlRanged.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProj)).EndInit();
            this.pnlConsume.ResumeLayout(false);
            this.pnlConsume.PerformLayout();
            this.pnlStats.ResumeLayout(false);
            this.pnlStats.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox pnlMain;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ListBox lstIndex;
        private System.Windows.Forms.Button btnNewItem;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.HScrollBar scrlArmor;
        private System.Windows.Forms.Label lblArmor;
        private System.Windows.Forms.HScrollBar scrlDamage;
        private System.Windows.Forms.Label lblDamage;
        private System.Windows.Forms.PictureBox picSprite;
        private System.Windows.Forms.Label lblSprite;
        private System.Windows.Forms.HScrollBar scrlSprite;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.HScrollBar scrlAttackSpeed;
        private System.Windows.Forms.Label lblAttackSpeed;
        private System.Windows.Forms.GroupBox pnlRanged;
        private System.Windows.Forms.HScrollBar scrlReloadSpeed;
        private System.Windows.Forms.Label lblReloadSpeed;
        private System.Windows.Forms.ComboBox cmbAmmoType;
        private System.Windows.Forms.Label lblAmmoType;
        private System.Windows.Forms.PictureBox picProj;
        private System.Windows.Forms.HScrollBar scrlProjNum;
        private System.Windows.Forms.Label lblProjNum;
        private System.Windows.Forms.HScrollBar scrlMaxClip;
        private System.Windows.Forms.Label lblMaxClip;
        private System.Windows.Forms.HScrollBar scrlClip;
        private System.Windows.Forms.Label lblClip;
        private System.Windows.Forms.GroupBox pnlConsume;
        private System.Windows.Forms.HScrollBar scrlHydrateRestore;
        private System.Windows.Forms.Label lblHydrateRestore;
        private System.Windows.Forms.HScrollBar scrlHungerRestore;
        private System.Windows.Forms.Label lblHungerRestore;
        private System.Windows.Forms.HScrollBar scrlHealthRestore;
        private System.Windows.Forms.Label lblHealthRestore;
        private System.Windows.Forms.GroupBox pnlStats;
        private System.Windows.Forms.HScrollBar scrlStamina;
        private System.Windows.Forms.Label lblStamina;
        private System.Windows.Forms.HScrollBar scrlEndurance;
        private System.Windows.Forms.Label lblEndurance;
        private System.Windows.Forms.HScrollBar scrlAgility;
        private System.Windows.Forms.Label lblAgility;
        private System.Windows.Forms.HScrollBar scrlStrength;
        private System.Windows.Forms.Label lblStrength;
        private System.Windows.Forms.HScrollBar scrlPrice;
        private System.Windows.Forms.Label lblPrice;
    }
}