namespace Editor.Forms
{
    partial class PlayerEditor
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnNewItem = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lstIndex = new System.Windows.Forms.ListBox();
            this.pnlGeneral = new System.Windows.Forms.GroupBox();
            this.scrlAimDirection = new System.Windows.Forms.HScrollBar();
            this.scrlDirection = new System.Windows.Forms.HScrollBar();
            this.lblAimDirection = new System.Windows.Forms.Label();
            this.lblDirection = new System.Windows.Forms.Label();
            this.scrlMap = new System.Windows.Forms.HScrollBar();
            this.lblMap = new System.Windows.Forms.Label();
            this.scrlY = new System.Windows.Forms.HScrollBar();
            this.scrlX = new System.Windows.Forms.HScrollBar();
            this.lblY = new System.Windows.Forms.Label();
            this.lblX = new System.Windows.Forms.Label();
            this.picSprite = new System.Windows.Forms.PictureBox();
            this.lblSprite = new System.Windows.Forms.Label();
            this.scrlSprite = new System.Windows.Forms.HScrollBar();
            this.chkPassMask = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.pnlStats = new System.Windows.Forms.GroupBox();
            this.txtExp = new System.Windows.Forms.TextBox();
            this.scrlStamina = new System.Windows.Forms.HScrollBar();
            this.lblStamina = new System.Windows.Forms.Label();
            this.scrlEndurance = new System.Windows.Forms.HScrollBar();
            this.lblEndurance = new System.Windows.Forms.Label();
            this.scrlAgility = new System.Windows.Forms.HScrollBar();
            this.lblAgility = new System.Windows.Forms.Label();
            this.scrlStrength = new System.Windows.Forms.HScrollBar();
            this.lblStrength = new System.Windows.Forms.Label();
            this.scrlHydration = new System.Windows.Forms.HScrollBar();
            this.lblHydration = new System.Windows.Forms.Label();
            this.scrlHunger = new System.Windows.Forms.HScrollBar();
            this.lblHunger = new System.Windows.Forms.Label();
            this.scrlArmor = new System.Windows.Forms.HScrollBar();
            this.lblArmor = new System.Windows.Forms.Label();
            this.scrlMoney = new System.Windows.Forms.HScrollBar();
            this.lblMoney = new System.Windows.Forms.Label();
            this.lblExp = new System.Windows.Forms.Label();
            this.scrlMaxHealth = new System.Windows.Forms.HScrollBar();
            this.lblMaxHealth = new System.Windows.Forms.Label();
            this.scrlHealth = new System.Windows.Forms.HScrollBar();
            this.lblHealth = new System.Windows.Forms.Label();
            this.scrlPoints = new System.Windows.Forms.HScrollBar();
            this.lblPoints = new System.Windows.Forms.Label();
            this.scrlLevel = new System.Windows.Forms.HScrollBar();
            this.lblLevel = new System.Windows.Forms.Label();
            this.lblPistol = new System.Windows.Forms.Label();
            this.scrlPistol = new System.Windows.Forms.HScrollBar();
            this.lblAssault = new System.Windows.Forms.Label();
            this.scrlAssault = new System.Windows.Forms.HScrollBar();
            this.lblRocket = new System.Windows.Forms.Label();
            this.scrlRocket = new System.Windows.Forms.HScrollBar();
            this.lblGrenade = new System.Windows.Forms.Label();
            this.scrlGrenade = new System.Windows.Forms.HScrollBar();
            this.pnlAmmo = new System.Windows.Forms.GroupBox();
            this.pnlActivation = new System.Windows.Forms.GroupBox();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grpLastLogged = new System.Windows.Forms.GroupBox();
            this.txtLastLogged = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.pnlGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSprite)).BeginInit();
            this.pnlStats.SuspendLayout();
            this.pnlAmmo.SuspendLayout();
            this.pnlActivation.SuspendLayout();
            this.grpLastLogged.SuspendLayout();
            this.SuspendLayout();
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
            this.groupBox1.TabIndex = 8;
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
            // pnlGeneral
            // 
            this.pnlGeneral.Controls.Add(this.scrlAimDirection);
            this.pnlGeneral.Controls.Add(this.scrlDirection);
            this.pnlGeneral.Controls.Add(this.lblAimDirection);
            this.pnlGeneral.Controls.Add(this.lblDirection);
            this.pnlGeneral.Controls.Add(this.scrlMap);
            this.pnlGeneral.Controls.Add(this.lblMap);
            this.pnlGeneral.Controls.Add(this.scrlY);
            this.pnlGeneral.Controls.Add(this.scrlX);
            this.pnlGeneral.Controls.Add(this.lblY);
            this.pnlGeneral.Controls.Add(this.lblX);
            this.pnlGeneral.Controls.Add(this.picSprite);
            this.pnlGeneral.Controls.Add(this.lblSprite);
            this.pnlGeneral.Controls.Add(this.scrlSprite);
            this.pnlGeneral.Controls.Add(this.chkPassMask);
            this.pnlGeneral.Controls.Add(this.label1);
            this.pnlGeneral.Controls.Add(this.txtPass);
            this.pnlGeneral.Controls.Add(this.txtName);
            this.pnlGeneral.Controls.Add(this.lblName);
            this.pnlGeneral.Location = new System.Drawing.Point(159, 12);
            this.pnlGeneral.Name = "pnlGeneral";
            this.pnlGeneral.Size = new System.Drawing.Size(192, 403);
            this.pnlGeneral.TabIndex = 9;
            this.pnlGeneral.TabStop = false;
            this.pnlGeneral.Text = "General";
            this.pnlGeneral.Visible = false;
            // 
            // scrlAimDirection
            // 
            this.scrlAimDirection.LargeChange = 1;
            this.scrlAimDirection.Location = new System.Drawing.Point(21, 372);
            this.scrlAimDirection.Maximum = 3;
            this.scrlAimDirection.Name = "scrlAimDirection";
            this.scrlAimDirection.Size = new System.Drawing.Size(150, 17);
            this.scrlAimDirection.TabIndex = 25;
            this.scrlAimDirection.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlAimDirection_Scroll);
            // 
            // scrlDirection
            // 
            this.scrlDirection.LargeChange = 1;
            this.scrlDirection.Location = new System.Drawing.Point(18, 330);
            this.scrlDirection.Maximum = 3;
            this.scrlDirection.Name = "scrlDirection";
            this.scrlDirection.Size = new System.Drawing.Size(153, 17);
            this.scrlDirection.TabIndex = 24;
            this.scrlDirection.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlDirection_Scroll);
            // 
            // lblAimDirection
            // 
            this.lblAimDirection.AutoSize = true;
            this.lblAimDirection.Location = new System.Drawing.Point(21, 355);
            this.lblAimDirection.Name = "lblAimDirection";
            this.lblAimDirection.Size = new System.Drawing.Size(81, 13);
            this.lblAimDirection.TabIndex = 23;
            this.lblAimDirection.Text = "Aim Direction: 0";
            // 
            // lblDirection
            // 
            this.lblDirection.AutoSize = true;
            this.lblDirection.Location = new System.Drawing.Point(21, 315);
            this.lblDirection.Name = "lblDirection";
            this.lblDirection.Size = new System.Drawing.Size(61, 13);
            this.lblDirection.TabIndex = 22;
            this.lblDirection.Text = "Direction: 0";
            // 
            // scrlMap
            // 
            this.scrlMap.LargeChange = 1;
            this.scrlMap.Location = new System.Drawing.Point(18, 292);
            this.scrlMap.Maximum = 10;
            this.scrlMap.Minimum = 1;
            this.scrlMap.Name = "scrlMap";
            this.scrlMap.Size = new System.Drawing.Size(153, 17);
            this.scrlMap.TabIndex = 21;
            this.scrlMap.Value = 1;
            this.scrlMap.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlMap_Scroll);
            // 
            // lblMap
            // 
            this.lblMap.AutoSize = true;
            this.lblMap.Location = new System.Drawing.Point(21, 277);
            this.lblMap.Name = "lblMap";
            this.lblMap.Size = new System.Drawing.Size(40, 13);
            this.lblMap.TabIndex = 20;
            this.lblMap.Text = "Map: 1";
            // 
            // scrlY
            // 
            this.scrlY.Location = new System.Drawing.Point(18, 254);
            this.scrlY.Maximum = 50;
            this.scrlY.Minimum = 1;
            this.scrlY.Name = "scrlY";
            this.scrlY.Size = new System.Drawing.Size(153, 17);
            this.scrlY.TabIndex = 19;
            this.scrlY.Value = 1;
            this.scrlY.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlY_Scroll);
            // 
            // scrlX
            // 
            this.scrlX.Location = new System.Drawing.Point(18, 214);
            this.scrlX.Maximum = 50;
            this.scrlX.Minimum = 1;
            this.scrlX.Name = "scrlX";
            this.scrlX.Size = new System.Drawing.Size(153, 17);
            this.scrlX.TabIndex = 18;
            this.scrlX.Value = 1;
            this.scrlX.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlX_Scroll);
            // 
            // lblY
            // 
            this.lblY.AutoSize = true;
            this.lblY.Location = new System.Drawing.Point(18, 239);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(26, 13);
            this.lblY.TabIndex = 17;
            this.lblY.Text = "Y: 1";
            // 
            // lblX
            // 
            this.lblX.AutoSize = true;
            this.lblX.Location = new System.Drawing.Point(18, 197);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(26, 13);
            this.lblX.TabIndex = 16;
            this.lblX.Text = "X: 1";
            // 
            // picSprite
            // 
            this.picSprite.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.picSprite.Location = new System.Drawing.Point(18, 137);
            this.picSprite.Name = "picSprite";
            this.picSprite.Size = new System.Drawing.Size(32, 48);
            this.picSprite.TabIndex = 15;
            this.picSprite.TabStop = false;
            // 
            // lblSprite
            // 
            this.lblSprite.AutoSize = true;
            this.lblSprite.Location = new System.Drawing.Point(56, 153);
            this.lblSprite.Name = "lblSprite";
            this.lblSprite.Size = new System.Drawing.Size(46, 13);
            this.lblSprite.TabIndex = 14;
            this.lblSprite.Text = "Sprite: 1";
            // 
            // scrlSprite
            // 
            this.scrlSprite.LargeChange = 1;
            this.scrlSprite.Location = new System.Drawing.Point(53, 168);
            this.scrlSprite.Maximum = 7;
            this.scrlSprite.Name = "scrlSprite";
            this.scrlSprite.Size = new System.Drawing.Size(118, 17);
            this.scrlSprite.TabIndex = 13;
            this.scrlSprite.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlSprite_Scroll);
            // 
            // chkPassMask
            // 
            this.chkPassMask.AutoSize = true;
            this.chkPassMask.Checked = true;
            this.chkPassMask.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPassMask.Location = new System.Drawing.Point(18, 114);
            this.chkPassMask.Name = "chkPassMask";
            this.chkPassMask.Size = new System.Drawing.Size(101, 17);
            this.chkPassMask.TabIndex = 11;
            this.chkPassMask.Text = "Mask Password";
            this.chkPassMask.UseVisualStyleBackColor = true;
            this.chkPassMask.CheckedChanged += new System.EventHandler(this.chkPassMask_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Password:";
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(18, 87);
            this.txtPass.Name = "txtPass";
            this.txtPass.PasswordChar = '*';
            this.txtPass.Size = new System.Drawing.Size(156, 20);
            this.txtPass.TabIndex = 9;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(18, 41);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(156, 20);
            this.txtName.TabIndex = 8;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(15, 25);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 7;
            this.lblName.Text = "Name:";
            // 
            // pnlStats
            // 
            this.pnlStats.Controls.Add(this.txtExp);
            this.pnlStats.Controls.Add(this.scrlStamina);
            this.pnlStats.Controls.Add(this.lblStamina);
            this.pnlStats.Controls.Add(this.scrlEndurance);
            this.pnlStats.Controls.Add(this.lblEndurance);
            this.pnlStats.Controls.Add(this.scrlAgility);
            this.pnlStats.Controls.Add(this.lblAgility);
            this.pnlStats.Controls.Add(this.scrlStrength);
            this.pnlStats.Controls.Add(this.lblStrength);
            this.pnlStats.Controls.Add(this.scrlHydration);
            this.pnlStats.Controls.Add(this.lblHydration);
            this.pnlStats.Controls.Add(this.scrlHunger);
            this.pnlStats.Controls.Add(this.lblHunger);
            this.pnlStats.Controls.Add(this.scrlArmor);
            this.pnlStats.Controls.Add(this.lblArmor);
            this.pnlStats.Controls.Add(this.scrlMoney);
            this.pnlStats.Controls.Add(this.lblMoney);
            this.pnlStats.Controls.Add(this.lblExp);
            this.pnlStats.Controls.Add(this.scrlMaxHealth);
            this.pnlStats.Controls.Add(this.lblMaxHealth);
            this.pnlStats.Controls.Add(this.scrlHealth);
            this.pnlStats.Controls.Add(this.lblHealth);
            this.pnlStats.Controls.Add(this.scrlPoints);
            this.pnlStats.Controls.Add(this.lblPoints);
            this.pnlStats.Controls.Add(this.scrlLevel);
            this.pnlStats.Controls.Add(this.lblLevel);
            this.pnlStats.Location = new System.Drawing.Point(358, 12);
            this.pnlStats.Name = "pnlStats";
            this.pnlStats.Size = new System.Drawing.Size(200, 617);
            this.pnlStats.TabIndex = 10;
            this.pnlStats.TabStop = false;
            this.pnlStats.Text = "Stats";
            this.pnlStats.Visible = false;
            // 
            // txtExp
            // 
            this.txtExp.Location = new System.Drawing.Point(22, 231);
            this.txtExp.Name = "txtExp";
            this.txtExp.Size = new System.Drawing.Size(156, 20);
            this.txtExp.TabIndex = 26;
            this.txtExp.Text = "0";
            this.txtExp.TextChanged += new System.EventHandler(this.txtExp_TextChanged);
            // 
            // scrlStamina
            // 
            this.scrlStamina.LargeChange = 100;
            this.scrlStamina.Location = new System.Drawing.Point(22, 586);
            this.scrlStamina.Maximum = 100000;
            this.scrlStamina.Name = "scrlStamina";
            this.scrlStamina.Size = new System.Drawing.Size(156, 17);
            this.scrlStamina.TabIndex = 25;
            this.scrlStamina.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlStamina_Scroll);
            // 
            // lblStamina
            // 
            this.lblStamina.AutoSize = true;
            this.lblStamina.Location = new System.Drawing.Point(19, 568);
            this.lblStamina.Name = "lblStamina";
            this.lblStamina.Size = new System.Drawing.Size(57, 13);
            this.lblStamina.TabIndex = 24;
            this.lblStamina.Text = "Stamina: 0";
            // 
            // scrlEndurance
            // 
            this.scrlEndurance.LargeChange = 100;
            this.scrlEndurance.Location = new System.Drawing.Point(22, 544);
            this.scrlEndurance.Maximum = 100000;
            this.scrlEndurance.Name = "scrlEndurance";
            this.scrlEndurance.Size = new System.Drawing.Size(156, 17);
            this.scrlEndurance.TabIndex = 23;
            this.scrlEndurance.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlEndurance_Scroll);
            // 
            // lblEndurance
            // 
            this.lblEndurance.AutoSize = true;
            this.lblEndurance.Location = new System.Drawing.Point(19, 526);
            this.lblEndurance.Name = "lblEndurance";
            this.lblEndurance.Size = new System.Drawing.Size(71, 13);
            this.lblEndurance.TabIndex = 22;
            this.lblEndurance.Text = "Endurance: 0";
            // 
            // scrlAgility
            // 
            this.scrlAgility.LargeChange = 100;
            this.scrlAgility.Location = new System.Drawing.Point(22, 498);
            this.scrlAgility.Maximum = 100000;
            this.scrlAgility.Name = "scrlAgility";
            this.scrlAgility.Size = new System.Drawing.Size(156, 17);
            this.scrlAgility.TabIndex = 21;
            this.scrlAgility.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlAgility_Scroll);
            // 
            // lblAgility
            // 
            this.lblAgility.AutoSize = true;
            this.lblAgility.Location = new System.Drawing.Point(19, 480);
            this.lblAgility.Name = "lblAgility";
            this.lblAgility.Size = new System.Drawing.Size(46, 13);
            this.lblAgility.TabIndex = 20;
            this.lblAgility.Text = "Agility: 0";
            // 
            // scrlStrength
            // 
            this.scrlStrength.LargeChange = 100;
            this.scrlStrength.Location = new System.Drawing.Point(22, 453);
            this.scrlStrength.Maximum = 100000;
            this.scrlStrength.Name = "scrlStrength";
            this.scrlStrength.Size = new System.Drawing.Size(156, 17);
            this.scrlStrength.TabIndex = 19;
            this.scrlStrength.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlStrength_Scroll);
            // 
            // lblStrength
            // 
            this.lblStrength.AutoSize = true;
            this.lblStrength.Location = new System.Drawing.Point(19, 435);
            this.lblStrength.Name = "lblStrength";
            this.lblStrength.Size = new System.Drawing.Size(59, 13);
            this.lblStrength.TabIndex = 18;
            this.lblStrength.Text = "Strength: 0";
            // 
            // scrlHydration
            // 
            this.scrlHydration.Location = new System.Drawing.Point(22, 409);
            this.scrlHydration.Name = "scrlHydration";
            this.scrlHydration.Size = new System.Drawing.Size(156, 17);
            this.scrlHydration.TabIndex = 17;
            this.scrlHydration.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlHydration_Scroll);
            // 
            // lblHydration
            // 
            this.lblHydration.AutoSize = true;
            this.lblHydration.Location = new System.Drawing.Point(19, 391);
            this.lblHydration.Name = "lblHydration";
            this.lblHydration.Size = new System.Drawing.Size(64, 13);
            this.lblHydration.TabIndex = 16;
            this.lblHydration.Text = "Hydration: 0";
            // 
            // scrlHunger
            // 
            this.scrlHunger.Location = new System.Drawing.Point(22, 368);
            this.scrlHunger.Name = "scrlHunger";
            this.scrlHunger.Size = new System.Drawing.Size(156, 17);
            this.scrlHunger.TabIndex = 15;
            this.scrlHunger.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlHunger_Scroll);
            // 
            // lblHunger
            // 
            this.lblHunger.AutoSize = true;
            this.lblHunger.Location = new System.Drawing.Point(19, 350);
            this.lblHunger.Name = "lblHunger";
            this.lblHunger.Size = new System.Drawing.Size(54, 13);
            this.lblHunger.TabIndex = 14;
            this.lblHunger.Text = "Hunger: 0";
            // 
            // scrlArmor
            // 
            this.scrlArmor.LargeChange = 100;
            this.scrlArmor.Location = new System.Drawing.Point(22, 320);
            this.scrlArmor.Maximum = 100000;
            this.scrlArmor.Name = "scrlArmor";
            this.scrlArmor.Size = new System.Drawing.Size(156, 17);
            this.scrlArmor.TabIndex = 13;
            this.scrlArmor.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlArmor_Scroll);
            // 
            // lblArmor
            // 
            this.lblArmor.AutoSize = true;
            this.lblArmor.Location = new System.Drawing.Point(19, 302);
            this.lblArmor.Name = "lblArmor";
            this.lblArmor.Size = new System.Drawing.Size(46, 13);
            this.lblArmor.TabIndex = 12;
            this.lblArmor.Text = "Armor: 0";
            // 
            // scrlMoney
            // 
            this.scrlMoney.LargeChange = 100;
            this.scrlMoney.Location = new System.Drawing.Point(22, 276);
            this.scrlMoney.Maximum = 500000;
            this.scrlMoney.Name = "scrlMoney";
            this.scrlMoney.Size = new System.Drawing.Size(156, 17);
            this.scrlMoney.TabIndex = 11;
            this.scrlMoney.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlMoney_Scroll);
            // 
            // lblMoney
            // 
            this.lblMoney.AutoSize = true;
            this.lblMoney.Location = new System.Drawing.Point(19, 258);
            this.lblMoney.Name = "lblMoney";
            this.lblMoney.Size = new System.Drawing.Size(51, 13);
            this.lblMoney.TabIndex = 10;
            this.lblMoney.Text = "Money: 0";
            // 
            // lblExp
            // 
            this.lblExp.AutoSize = true;
            this.lblExp.Location = new System.Drawing.Point(19, 214);
            this.lblExp.Name = "lblExp";
            this.lblExp.Size = new System.Drawing.Size(63, 13);
            this.lblExp.TabIndex = 8;
            this.lblExp.Text = "Experience:";
            // 
            // scrlMaxHealth
            // 
            this.scrlMaxHealth.LargeChange = 100;
            this.scrlMaxHealth.Location = new System.Drawing.Point(22, 186);
            this.scrlMaxHealth.Maximum = 500000;
            this.scrlMaxHealth.Name = "scrlMaxHealth";
            this.scrlMaxHealth.Size = new System.Drawing.Size(156, 17);
            this.scrlMaxHealth.TabIndex = 7;
            this.scrlMaxHealth.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlMaxHealth_Scroll);
            // 
            // lblMaxHealth
            // 
            this.lblMaxHealth.AutoSize = true;
            this.lblMaxHealth.Location = new System.Drawing.Point(19, 168);
            this.lblMaxHealth.Name = "lblMaxHealth";
            this.lblMaxHealth.Size = new System.Drawing.Size(73, 13);
            this.lblMaxHealth.TabIndex = 6;
            this.lblMaxHealth.Text = "Max Health: 0";
            // 
            // scrlHealth
            // 
            this.scrlHealth.LargeChange = 100;
            this.scrlHealth.Location = new System.Drawing.Point(22, 136);
            this.scrlHealth.Maximum = 500000;
            this.scrlHealth.Name = "scrlHealth";
            this.scrlHealth.Size = new System.Drawing.Size(156, 17);
            this.scrlHealth.TabIndex = 5;
            this.scrlHealth.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlHealth_Scroll);
            // 
            // lblHealth
            // 
            this.lblHealth.AutoSize = true;
            this.lblHealth.Location = new System.Drawing.Point(19, 118);
            this.lblHealth.Name = "lblHealth";
            this.lblHealth.Size = new System.Drawing.Size(50, 13);
            this.lblHealth.TabIndex = 4;
            this.lblHealth.Text = "Health: 0";
            // 
            // scrlPoints
            // 
            this.scrlPoints.LargeChange = 100;
            this.scrlPoints.Location = new System.Drawing.Point(22, 89);
            this.scrlPoints.Maximum = 500000;
            this.scrlPoints.Name = "scrlPoints";
            this.scrlPoints.Size = new System.Drawing.Size(156, 17);
            this.scrlPoints.TabIndex = 3;
            this.scrlPoints.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlPoints_Scroll);
            // 
            // lblPoints
            // 
            this.lblPoints.AutoSize = true;
            this.lblPoints.Location = new System.Drawing.Point(19, 71);
            this.lblPoints.Name = "lblPoints";
            this.lblPoints.Size = new System.Drawing.Size(48, 13);
            this.lblPoints.TabIndex = 2;
            this.lblPoints.Text = "Points: 0";
            // 
            // scrlLevel
            // 
            this.scrlLevel.Location = new System.Drawing.Point(22, 43);
            this.scrlLevel.Maximum = 1000;
            this.scrlLevel.Name = "scrlLevel";
            this.scrlLevel.Size = new System.Drawing.Size(156, 17);
            this.scrlLevel.TabIndex = 1;
            this.scrlLevel.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlLevel_Scroll);
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.Location = new System.Drawing.Point(19, 25);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(45, 13);
            this.lblLevel.TabIndex = 0;
            this.lblLevel.Text = "Level: 1";
            // 
            // lblPistol
            // 
            this.lblPistol.AutoSize = true;
            this.lblPistol.Location = new System.Drawing.Point(16, 23);
            this.lblPistol.Name = "lblPistol";
            this.lblPistol.Size = new System.Drawing.Size(76, 13);
            this.lblPistol.TabIndex = 0;
            this.lblPistol.Text = "Pistol Ammo: 0";
            // 
            // scrlPistol
            // 
            this.scrlPistol.Location = new System.Drawing.Point(19, 40);
            this.scrlPistol.Maximum = 10000;
            this.scrlPistol.Name = "scrlPistol";
            this.scrlPistol.Size = new System.Drawing.Size(158, 17);
            this.scrlPistol.TabIndex = 1;
            this.scrlPistol.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlPistol_Scroll);
            // 
            // lblAssault
            // 
            this.lblAssault.AutoSize = true;
            this.lblAssault.Location = new System.Drawing.Point(16, 70);
            this.lblAssault.Name = "lblAssault";
            this.lblAssault.Size = new System.Drawing.Size(85, 13);
            this.lblAssault.TabIndex = 2;
            this.lblAssault.Text = "Assault Ammo: 0";
            // 
            // scrlAssault
            // 
            this.scrlAssault.Location = new System.Drawing.Point(19, 87);
            this.scrlAssault.Maximum = 10000;
            this.scrlAssault.Name = "scrlAssault";
            this.scrlAssault.Size = new System.Drawing.Size(158, 17);
            this.scrlAssault.TabIndex = 3;
            this.scrlAssault.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlAssault_Scroll);
            // 
            // lblRocket
            // 
            this.lblRocket.AutoSize = true;
            this.lblRocket.Location = new System.Drawing.Point(16, 117);
            this.lblRocket.Name = "lblRocket";
            this.lblRocket.Size = new System.Drawing.Size(86, 13);
            this.lblRocket.TabIndex = 4;
            this.lblRocket.Text = "Rocket Ammo: 0";
            // 
            // scrlRocket
            // 
            this.scrlRocket.Location = new System.Drawing.Point(19, 134);
            this.scrlRocket.Maximum = 10000;
            this.scrlRocket.Name = "scrlRocket";
            this.scrlRocket.Size = new System.Drawing.Size(158, 17);
            this.scrlRocket.TabIndex = 5;
            this.scrlRocket.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlRocket_Scroll);
            // 
            // lblGrenade
            // 
            this.lblGrenade.AutoSize = true;
            this.lblGrenade.Location = new System.Drawing.Point(16, 168);
            this.lblGrenade.Name = "lblGrenade";
            this.lblGrenade.Size = new System.Drawing.Size(92, 13);
            this.lblGrenade.TabIndex = 6;
            this.lblGrenade.Text = "Grenade Ammo: 0";
            // 
            // scrlGrenade
            // 
            this.scrlGrenade.Location = new System.Drawing.Point(19, 185);
            this.scrlGrenade.Maximum = 10000;
            this.scrlGrenade.Name = "scrlGrenade";
            this.scrlGrenade.Size = new System.Drawing.Size(158, 17);
            this.scrlGrenade.TabIndex = 7;
            this.scrlGrenade.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlGrenade_Scroll);
            // 
            // pnlAmmo
            // 
            this.pnlAmmo.Controls.Add(this.scrlGrenade);
            this.pnlAmmo.Controls.Add(this.lblGrenade);
            this.pnlAmmo.Controls.Add(this.scrlRocket);
            this.pnlAmmo.Controls.Add(this.lblRocket);
            this.pnlAmmo.Controls.Add(this.scrlAssault);
            this.pnlAmmo.Controls.Add(this.lblAssault);
            this.pnlAmmo.Controls.Add(this.scrlPistol);
            this.pnlAmmo.Controls.Add(this.lblPistol);
            this.pnlAmmo.Location = new System.Drawing.Point(565, 13);
            this.pnlAmmo.Name = "pnlAmmo";
            this.pnlAmmo.Size = new System.Drawing.Size(196, 226);
            this.pnlAmmo.TabIndex = 11;
            this.pnlAmmo.TabStop = false;
            this.pnlAmmo.Text = "Ammo";
            this.pnlAmmo.Visible = false;
            // 
            // pnlActivation
            // 
            this.pnlActivation.Controls.Add(this.txtStatus);
            this.pnlActivation.Controls.Add(this.label3);
            this.pnlActivation.Controls.Add(this.txtKey);
            this.pnlActivation.Controls.Add(this.label2);
            this.pnlActivation.Location = new System.Drawing.Point(567, 251);
            this.pnlActivation.Name = "pnlActivation";
            this.pnlActivation.Size = new System.Drawing.Size(200, 141);
            this.pnlActivation.TabIndex = 12;
            this.pnlActivation.TabStop = false;
            this.pnlActivation.Text = "Activation";
            this.pnlActivation.Visible = false;
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(20, 91);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(155, 20);
            this.txtStatus.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Status:";
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(20, 38);
            this.txtKey.Name = "txtKey";
            this.txtKey.ReadOnly = true;
            this.txtKey.Size = new System.Drawing.Size(155, 20);
            this.txtKey.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Activation Key:";
            // 
            // grpLastLogged
            // 
            this.grpLastLogged.Controls.Add(this.txtLastLogged);
            this.grpLastLogged.Location = new System.Drawing.Point(567, 403);
            this.grpLastLogged.Name = "grpLastLogged";
            this.grpLastLogged.Size = new System.Drawing.Size(200, 79);
            this.grpLastLogged.TabIndex = 13;
            this.grpLastLogged.TabStop = false;
            this.grpLastLogged.Text = "Last Logged";
            this.grpLastLogged.Visible = false;
            // 
            // txtLastLogged
            // 
            this.txtLastLogged.Location = new System.Drawing.Point(20, 30);
            this.txtLastLogged.Name = "txtLastLogged";
            this.txtLastLogged.ReadOnly = true;
            this.txtLastLogged.Size = new System.Drawing.Size(155, 20);
            this.txtLastLogged.TabIndex = 0;
            // 
            // PlayerEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 646);
            this.Controls.Add(this.grpLastLogged);
            this.Controls.Add(this.pnlActivation);
            this.Controls.Add(this.pnlAmmo);
            this.Controls.Add(this.pnlStats);
            this.Controls.Add(this.pnlGeneral);
            this.Controls.Add(this.groupBox1);
            this.Name = "PlayerEditor";
            this.Text = "PlayerEditor";
            this.groupBox1.ResumeLayout(false);
            this.pnlGeneral.ResumeLayout(false);
            this.pnlGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSprite)).EndInit();
            this.pnlStats.ResumeLayout(false);
            this.pnlStats.PerformLayout();
            this.pnlAmmo.ResumeLayout(false);
            this.pnlAmmo.PerformLayout();
            this.pnlActivation.ResumeLayout(false);
            this.pnlActivation.PerformLayout();
            this.grpLastLogged.ResumeLayout(false);
            this.grpLastLogged.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnNewItem;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ListBox lstIndex;
        private System.Windows.Forms.GroupBox pnlGeneral;
        private System.Windows.Forms.CheckBox chkPassMask;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.PictureBox picSprite;
        private System.Windows.Forms.Label lblSprite;
        private System.Windows.Forms.HScrollBar scrlSprite;
        private System.Windows.Forms.HScrollBar scrlY;
        private System.Windows.Forms.HScrollBar scrlX;
        private System.Windows.Forms.Label lblY;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.HScrollBar scrlMap;
        private System.Windows.Forms.Label lblMap;
        private System.Windows.Forms.HScrollBar scrlAimDirection;
        private System.Windows.Forms.HScrollBar scrlDirection;
        private System.Windows.Forms.Label lblAimDirection;
        private System.Windows.Forms.Label lblDirection;
        private System.Windows.Forms.GroupBox pnlStats;
        private System.Windows.Forms.HScrollBar scrlStamina;
        private System.Windows.Forms.Label lblStamina;
        private System.Windows.Forms.HScrollBar scrlEndurance;
        private System.Windows.Forms.Label lblEndurance;
        private System.Windows.Forms.HScrollBar scrlAgility;
        private System.Windows.Forms.Label lblAgility;
        private System.Windows.Forms.HScrollBar scrlStrength;
        private System.Windows.Forms.Label lblStrength;
        private System.Windows.Forms.HScrollBar scrlHydration;
        private System.Windows.Forms.Label lblHydration;
        private System.Windows.Forms.HScrollBar scrlHunger;
        private System.Windows.Forms.Label lblHunger;
        private System.Windows.Forms.HScrollBar scrlArmor;
        private System.Windows.Forms.Label lblArmor;
        private System.Windows.Forms.HScrollBar scrlMoney;
        private System.Windows.Forms.Label lblMoney;
        private System.Windows.Forms.Label lblExp;
        private System.Windows.Forms.HScrollBar scrlMaxHealth;
        private System.Windows.Forms.Label lblMaxHealth;
        private System.Windows.Forms.HScrollBar scrlHealth;
        private System.Windows.Forms.Label lblHealth;
        private System.Windows.Forms.HScrollBar scrlPoints;
        private System.Windows.Forms.Label lblPoints;
        private System.Windows.Forms.HScrollBar scrlLevel;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.Label lblPistol;
        private System.Windows.Forms.HScrollBar scrlPistol;
        private System.Windows.Forms.Label lblAssault;
        private System.Windows.Forms.HScrollBar scrlAssault;
        private System.Windows.Forms.Label lblRocket;
        private System.Windows.Forms.HScrollBar scrlRocket;
        private System.Windows.Forms.Label lblGrenade;
        private System.Windows.Forms.HScrollBar scrlGrenade;
        private System.Windows.Forms.GroupBox pnlAmmo;
        private System.Windows.Forms.GroupBox pnlActivation;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtExp;
        private System.Windows.Forms.GroupBox grpLastLogged;
        private System.Windows.Forms.TextBox txtLastLogged;
    }
}