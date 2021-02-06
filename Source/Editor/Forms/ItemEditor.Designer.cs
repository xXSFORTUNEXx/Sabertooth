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
            this.scrlSpellNum = new System.Windows.Forms.HScrollBar();
            this.lblSpellNum = new System.Windows.Forms.Label();
            this.chkStackable = new System.Windows.Forms.CheckBox();
            this.scrlRarity = new System.Windows.Forms.HScrollBar();
            this.lblRarity = new System.Windows.Forms.Label();
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
            this.pnlConsume = new System.Windows.Forms.GroupBox();
            this.scrlBonusXP = new System.Windows.Forms.HScrollBar();
            this.lblBonusXP = new System.Windows.Forms.Label();
            this.scrlAddMaxMP = new System.Windows.Forms.HScrollBar();
            this.lblAddMaxMP = new System.Windows.Forms.Label();
            this.scrlAddMaxHP = new System.Windows.Forms.HScrollBar();
            this.lblAddMaxHP = new System.Windows.Forms.Label();
            this.scrlCooldown = new System.Windows.Forms.HScrollBar();
            this.lblCoolDown = new System.Windows.Forms.Label();
            this.scrlManaRestore = new System.Windows.Forms.HScrollBar();
            this.lblManaRestore = new System.Windows.Forms.Label();
            this.scrlHealthRestore = new System.Windows.Forms.HScrollBar();
            this.lblHealthRestore = new System.Windows.Forms.Label();
            this.pnlStats = new System.Windows.Forms.GroupBox();
            this.scrlEnergy = new System.Windows.Forms.HScrollBar();
            this.lblEnergy = new System.Windows.Forms.Label();
            this.scrlStamina = new System.Windows.Forms.HScrollBar();
            this.lblStamina = new System.Windows.Forms.Label();
            this.scrlInt = new System.Windows.Forms.HScrollBar();
            this.lblInt = new System.Windows.Forms.Label();
            this.scrlAgility = new System.Windows.Forms.HScrollBar();
            this.lblAgility = new System.Windows.Forms.Label();
            this.scrlStrength = new System.Windows.Forms.HScrollBar();
            this.lblStrength = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSprite)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.pnlConsume.SuspendLayout();
            this.pnlStats.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.scrlSpellNum);
            this.pnlMain.Controls.Add(this.lblSpellNum);
            this.pnlMain.Controls.Add(this.chkStackable);
            this.pnlMain.Controls.Add(this.scrlRarity);
            this.pnlMain.Controls.Add(this.lblRarity);
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
            this.pnlMain.Size = new System.Drawing.Size(197, 409);
            this.pnlMain.TabIndex = 5;
            this.pnlMain.TabStop = false;
            this.pnlMain.Text = "Properties";
            this.pnlMain.Visible = false;
            // 
            // scrlSpellNum
            // 
            this.scrlSpellNum.Location = new System.Drawing.Point(21, 365);
            this.scrlSpellNum.Name = "scrlSpellNum";
            this.scrlSpellNum.Size = new System.Drawing.Size(150, 17);
            this.scrlSpellNum.TabIndex = 26;
            this.scrlSpellNum.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlSpellNum_Scroll);
            // 
            // lblSpellNum
            // 
            this.lblSpellNum.AutoSize = true;
            this.lblSpellNum.Location = new System.Drawing.Point(17, 350);
            this.lblSpellNum.Name = "lblSpellNum";
            this.lblSpellNum.Size = new System.Drawing.Size(82, 13);
            this.lblSpellNum.TabIndex = 25;
            this.lblSpellNum.Text = "Spell Number: 0";
            // 
            // chkStackable
            // 
            this.chkStackable.AutoSize = true;
            this.chkStackable.Location = new System.Drawing.Point(20, 321);
            this.chkStackable.Name = "chkStackable";
            this.chkStackable.Size = new System.Drawing.Size(74, 17);
            this.chkStackable.TabIndex = 24;
            this.chkStackable.Text = "Stackable";
            this.chkStackable.UseVisualStyleBackColor = true;
            this.chkStackable.CheckedChanged += new System.EventHandler(this.chkStackable_CheckedChanged);
            // 
            // scrlRarity
            // 
            this.scrlRarity.LargeChange = 1;
            this.scrlRarity.Location = new System.Drawing.Point(17, 251);
            this.scrlRarity.Maximum = 5;
            this.scrlRarity.Name = "scrlRarity";
            this.scrlRarity.Size = new System.Drawing.Size(156, 17);
            this.scrlRarity.TabIndex = 23;
            this.scrlRarity.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlRarity_Scroll);
            // 
            // lblRarity
            // 
            this.lblRarity.AutoSize = true;
            this.lblRarity.Location = new System.Drawing.Point(17, 237);
            this.lblRarity.Name = "lblRarity";
            this.lblRarity.Size = new System.Drawing.Size(88, 13);
            this.lblRarity.TabIndex = 22;
            this.lblRarity.Text = "Rarity: 0 - Normal";
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
            "Potion",
            "Shirt",
            "Pants",
            "Shoes",
            "Book",
            "Other"});
            this.cmbType.Location = new System.Drawing.Point(20, 290);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(153, 21);
            this.cmbType.TabIndex = 15;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(17, 273);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(34, 13);
            this.lblType.TabIndex = 14;
            this.lblType.Text = "Type:";
            // 
            // scrlArmor
            // 
            this.scrlArmor.Location = new System.Drawing.Point(17, 152);
            this.scrlArmor.Maximum = 2500;
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
            this.scrlDamage.Maximum = 2500;
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
            // pnlConsume
            // 
            this.pnlConsume.Controls.Add(this.scrlBonusXP);
            this.pnlConsume.Controls.Add(this.lblBonusXP);
            this.pnlConsume.Controls.Add(this.scrlAddMaxMP);
            this.pnlConsume.Controls.Add(this.lblAddMaxMP);
            this.pnlConsume.Controls.Add(this.scrlAddMaxHP);
            this.pnlConsume.Controls.Add(this.lblAddMaxHP);
            this.pnlConsume.Controls.Add(this.scrlCooldown);
            this.pnlConsume.Controls.Add(this.lblCoolDown);
            this.pnlConsume.Controls.Add(this.scrlManaRestore);
            this.pnlConsume.Controls.Add(this.lblManaRestore);
            this.pnlConsume.Controls.Add(this.scrlHealthRestore);
            this.pnlConsume.Controls.Add(this.lblHealthRestore);
            this.pnlConsume.Location = new System.Drawing.Point(362, 200);
            this.pnlConsume.Name = "pnlConsume";
            this.pnlConsume.Size = new System.Drawing.Size(200, 238);
            this.pnlConsume.TabIndex = 9;
            this.pnlConsume.TabStop = false;
            this.pnlConsume.Text = "Consumable";
            this.pnlConsume.Visible = false;
            // 
            // scrlBonusXP
            // 
            this.scrlBonusXP.Location = new System.Drawing.Point(24, 204);
            this.scrlBonusXP.Maximum = 10000;
            this.scrlBonusXP.Name = "scrlBonusXP";
            this.scrlBonusXP.Size = new System.Drawing.Size(150, 17);
            this.scrlBonusXP.TabIndex = 39;
            this.scrlBonusXP.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlBonusXP_Scroll);
            // 
            // lblBonusXP
            // 
            this.lblBonusXP.AutoSize = true;
            this.lblBonusXP.Location = new System.Drawing.Point(24, 187);
            this.lblBonusXP.Name = "lblBonusXP";
            this.lblBonusXP.Size = new System.Drawing.Size(66, 13);
            this.lblBonusXP.TabIndex = 38;
            this.lblBonusXP.Text = "Bonus XP: 0";
            // 
            // scrlAddMaxMP
            // 
            this.scrlAddMaxMP.Location = new System.Drawing.Point(24, 170);
            this.scrlAddMaxMP.Maximum = 1000;
            this.scrlAddMaxMP.Name = "scrlAddMaxMP";
            this.scrlAddMaxMP.Size = new System.Drawing.Size(150, 17);
            this.scrlAddMaxMP.TabIndex = 37;
            this.scrlAddMaxMP.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlAddMaxMP_Scroll);
            // 
            // lblAddMaxMP
            // 
            this.lblAddMaxMP.AutoSize = true;
            this.lblAddMaxMP.Location = new System.Drawing.Point(24, 153);
            this.lblAddMaxMP.Name = "lblAddMaxMP";
            this.lblAddMaxMP.Size = new System.Drawing.Size(80, 13);
            this.lblAddMaxMP.TabIndex = 36;
            this.lblAddMaxMP.Text = "Add Max MP: 0";
            // 
            // scrlAddMaxHP
            // 
            this.scrlAddMaxHP.Location = new System.Drawing.Point(24, 136);
            this.scrlAddMaxHP.Maximum = 1000;
            this.scrlAddMaxHP.Name = "scrlAddMaxHP";
            this.scrlAddMaxHP.Size = new System.Drawing.Size(150, 17);
            this.scrlAddMaxHP.TabIndex = 35;
            this.scrlAddMaxHP.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlAddMaxHP_Scroll);
            // 
            // lblAddMaxHP
            // 
            this.lblAddMaxHP.AutoSize = true;
            this.lblAddMaxHP.Location = new System.Drawing.Point(24, 119);
            this.lblAddMaxHP.Name = "lblAddMaxHP";
            this.lblAddMaxHP.Size = new System.Drawing.Size(79, 13);
            this.lblAddMaxHP.TabIndex = 34;
            this.lblAddMaxHP.Text = "Add Max HP: 0";
            // 
            // scrlCooldown
            // 
            this.scrlCooldown.Location = new System.Drawing.Point(24, 98);
            this.scrlCooldown.Maximum = 1000;
            this.scrlCooldown.Name = "scrlCooldown";
            this.scrlCooldown.Size = new System.Drawing.Size(150, 17);
            this.scrlCooldown.TabIndex = 33;
            this.scrlCooldown.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlCooldown_Scroll);
            // 
            // lblCoolDown
            // 
            this.lblCoolDown.AutoSize = true;
            this.lblCoolDown.Location = new System.Drawing.Point(21, 85);
            this.lblCoolDown.Name = "lblCoolDown";
            this.lblCoolDown.Size = new System.Drawing.Size(71, 13);
            this.lblCoolDown.TabIndex = 32;
            this.lblCoolDown.Text = "Cooldown: 0s";
            // 
            // scrlManaRestore
            // 
            this.scrlManaRestore.Location = new System.Drawing.Point(21, 65);
            this.scrlManaRestore.Maximum = 1000;
            this.scrlManaRestore.Name = "scrlManaRestore";
            this.scrlManaRestore.Size = new System.Drawing.Size(153, 17);
            this.scrlManaRestore.TabIndex = 31;
            this.scrlManaRestore.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlManaRestore_Scroll);
            // 
            // lblManaRestore
            // 
            this.lblManaRestore.AutoSize = true;
            this.lblManaRestore.Location = new System.Drawing.Point(21, 52);
            this.lblManaRestore.Name = "lblManaRestore";
            this.lblManaRestore.Size = new System.Drawing.Size(86, 13);
            this.lblManaRestore.TabIndex = 30;
            this.lblManaRestore.Text = "Mana Restore: 0";
            // 
            // scrlHealthRestore
            // 
            this.scrlHealthRestore.Location = new System.Drawing.Point(21, 35);
            this.scrlHealthRestore.Maximum = 1000;
            this.scrlHealthRestore.Name = "scrlHealthRestore";
            this.scrlHealthRestore.Size = new System.Drawing.Size(156, 17);
            this.scrlHealthRestore.TabIndex = 29;
            this.scrlHealthRestore.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlHealthRestore_Scroll);
            // 
            // lblHealthRestore
            // 
            this.lblHealthRestore.AutoSize = true;
            this.lblHealthRestore.Location = new System.Drawing.Point(21, 22);
            this.lblHealthRestore.Name = "lblHealthRestore";
            this.lblHealthRestore.Size = new System.Drawing.Size(90, 13);
            this.lblHealthRestore.TabIndex = 28;
            this.lblHealthRestore.Text = "Health Restore: 0";
            // 
            // pnlStats
            // 
            this.pnlStats.Controls.Add(this.scrlEnergy);
            this.pnlStats.Controls.Add(this.lblEnergy);
            this.pnlStats.Controls.Add(this.scrlStamina);
            this.pnlStats.Controls.Add(this.lblStamina);
            this.pnlStats.Controls.Add(this.scrlInt);
            this.pnlStats.Controls.Add(this.lblInt);
            this.pnlStats.Controls.Add(this.scrlAgility);
            this.pnlStats.Controls.Add(this.lblAgility);
            this.pnlStats.Controls.Add(this.scrlStrength);
            this.pnlStats.Controls.Add(this.lblStrength);
            this.pnlStats.Location = new System.Drawing.Point(362, 12);
            this.pnlStats.Name = "pnlStats";
            this.pnlStats.Size = new System.Drawing.Size(200, 182);
            this.pnlStats.TabIndex = 10;
            this.pnlStats.TabStop = false;
            this.pnlStats.Text = "Stats";
            this.pnlStats.Visible = false;
            // 
            // scrlEnergy
            // 
            this.scrlEnergy.Location = new System.Drawing.Point(24, 128);
            this.scrlEnergy.Maximum = 1000;
            this.scrlEnergy.Name = "scrlEnergy";
            this.scrlEnergy.Size = new System.Drawing.Size(156, 17);
            this.scrlEnergy.TabIndex = 45;
            this.scrlEnergy.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlEnergy_Scroll);
            // 
            // lblEnergy
            // 
            this.lblEnergy.AutoSize = true;
            this.lblEnergy.Location = new System.Drawing.Point(21, 111);
            this.lblEnergy.Name = "lblEnergy";
            this.lblEnergy.Size = new System.Drawing.Size(52, 13);
            this.lblEnergy.TabIndex = 44;
            this.lblEnergy.Text = "Energy: 0";
            // 
            // scrlStamina
            // 
            this.scrlStamina.Location = new System.Drawing.Point(24, 158);
            this.scrlStamina.Maximum = 1000;
            this.scrlStamina.Name = "scrlStamina";
            this.scrlStamina.Size = new System.Drawing.Size(156, 17);
            this.scrlStamina.TabIndex = 43;
            this.scrlStamina.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlStamina_Scroll);
            // 
            // lblStamina
            // 
            this.lblStamina.AutoSize = true;
            this.lblStamina.Location = new System.Drawing.Point(21, 145);
            this.lblStamina.Name = "lblStamina";
            this.lblStamina.Size = new System.Drawing.Size(57, 13);
            this.lblStamina.TabIndex = 42;
            this.lblStamina.Text = "Stamina: 0";
            // 
            // scrlInt
            // 
            this.scrlInt.Location = new System.Drawing.Point(24, 94);
            this.scrlInt.Maximum = 1000;
            this.scrlInt.Name = "scrlInt";
            this.scrlInt.Size = new System.Drawing.Size(156, 17);
            this.scrlInt.TabIndex = 41;
            this.scrlInt.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlInt_Scroll);
            // 
            // lblInt
            // 
            this.lblInt.AutoSize = true;
            this.lblInt.Location = new System.Drawing.Point(21, 81);
            this.lblInt.Name = "lblInt";
            this.lblInt.Size = new System.Drawing.Size(73, 13);
            this.lblInt.TabIndex = 40;
            this.lblInt.Text = "Intelligence: 0";
            // 
            // scrlAgility
            // 
            this.scrlAgility.Location = new System.Drawing.Point(24, 64);
            this.scrlAgility.Maximum = 1000;
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
            this.scrlStrength.Maximum = 1000;
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
            this.ClientSize = new System.Drawing.Size(575, 453);
            this.Controls.Add(this.pnlStats);
            this.Controls.Add(this.pnlConsume);
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
        private System.Windows.Forms.GroupBox pnlConsume;
        private System.Windows.Forms.HScrollBar scrlHealthRestore;
        private System.Windows.Forms.Label lblHealthRestore;
        private System.Windows.Forms.GroupBox pnlStats;
        private System.Windows.Forms.HScrollBar scrlStamina;
        private System.Windows.Forms.Label lblStamina;
        private System.Windows.Forms.HScrollBar scrlInt;
        private System.Windows.Forms.Label lblInt;
        private System.Windows.Forms.HScrollBar scrlAgility;
        private System.Windows.Forms.Label lblAgility;
        private System.Windows.Forms.HScrollBar scrlStrength;
        private System.Windows.Forms.Label lblStrength;
        private System.Windows.Forms.HScrollBar scrlPrice;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.HScrollBar scrlRarity;
        private System.Windows.Forms.Label lblRarity;
        private System.Windows.Forms.HScrollBar scrlEnergy;
        private System.Windows.Forms.Label lblEnergy;
        private System.Windows.Forms.HScrollBar scrlManaRestore;
        private System.Windows.Forms.Label lblManaRestore;
        private System.Windows.Forms.HScrollBar scrlCooldown;
        private System.Windows.Forms.Label lblCoolDown;
        private System.Windows.Forms.CheckBox chkStackable;
        private System.Windows.Forms.HScrollBar scrlSpellNum;
        private System.Windows.Forms.Label lblSpellNum;
        private System.Windows.Forms.HScrollBar scrlBonusXP;
        private System.Windows.Forms.Label lblBonusXP;
        private System.Windows.Forms.HScrollBar scrlAddMaxMP;
        private System.Windows.Forms.Label lblAddMaxMP;
        private System.Windows.Forms.HScrollBar scrlAddMaxHP;
        private System.Windows.Forms.Label lblAddMaxHP;
    }
}