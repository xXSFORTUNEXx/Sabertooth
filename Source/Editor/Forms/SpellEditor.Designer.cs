namespace Editor.Forms
{
    partial class SpellEditor
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.scrlMPCost = new System.Windows.Forms.HScrollBar();
            this.lblMPCost = new System.Windows.Forms.Label();
            this.scrlHPCost = new System.Windows.Forms.HScrollBar();
            this.lblHPCost = new System.Windows.Forms.Label();
            this.scrlVital = new System.Windows.Forms.HScrollBar();
            this.lblVital = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.scrlTickInt = new System.Windows.Forms.HScrollBar();
            this.lblTickInt = new System.Windows.Forms.Label();
            this.scrlTotalTicks = new System.Windows.Forms.HScrollBar();
            this.lblTotalTicks = new System.Windows.Forms.Label();
            this.scrlCastTime = new System.Windows.Forms.HScrollBar();
            this.lblCastTime = new System.Windows.Forms.Label();
            this.scrlCoolDown = new System.Windows.Forms.HScrollBar();
            this.lblCoolDown = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.picProj = new System.Windows.Forms.PictureBox();
            this.lblProj = new System.Windows.Forms.Label();
            this.scrlProj = new System.Windows.Forms.HScrollBar();
            this.chkProj = new System.Windows.Forms.CheckBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.scrlIcon = new System.Windows.Forms.HScrollBar();
            this.lblIcon = new System.Windows.Forms.Label();
            this.picIcon = new System.Windows.Forms.PictureBox();
            this.lblType = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.lblCharges = new System.Windows.Forms.Label();
            this.scrlCharges = new System.Windows.Forms.HScrollBar();
            this.lblRange = new System.Windows.Forms.Label();
            this.scrlRange = new System.Windows.Forms.HScrollBar();
            this.lblLevelReq = new System.Windows.Forms.Label();
            this.scrlLevelReq = new System.Windows.Forms.HScrollBar();
            this.chkAOE = new System.Windows.Forms.CheckBox();
            this.lblDistance = new System.Windows.Forms.Label();
            this.scrlDistance = new System.Windows.Forms.HScrollBar();
            this.pnlMain = new System.Windows.Forms.GroupBox();
            this.chkSelfCast = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.chkRenderOnTarget = new System.Windows.Forms.CheckBox();
            this.picAnimation = new System.Windows.Forms.PictureBox();
            this.scrlAnimation = new System.Windows.Forms.HScrollBar();
            this.lblAnimation = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProj)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAnimation)).BeginInit();
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.scrlMPCost);
            this.groupBox2.Controls.Add(this.lblMPCost);
            this.groupBox2.Controls.Add(this.scrlHPCost);
            this.groupBox2.Controls.Add(this.lblHPCost);
            this.groupBox2.Controls.Add(this.scrlVital);
            this.groupBox2.Controls.Add(this.lblVital);
            this.groupBox2.Location = new System.Drawing.Point(362, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(202, 131);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Stats";
            // 
            // scrlMPCost
            // 
            this.scrlMPCost.LargeChange = 100;
            this.scrlMPCost.Location = new System.Drawing.Point(24, 97);
            this.scrlMPCost.Maximum = 5000;
            this.scrlMPCost.Name = "scrlMPCost";
            this.scrlMPCost.Size = new System.Drawing.Size(156, 17);
            this.scrlMPCost.TabIndex = 25;
            this.scrlMPCost.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlMPCost_Scroll);
            // 
            // lblMPCost
            // 
            this.lblMPCost.AutoSize = true;
            this.lblMPCost.Location = new System.Drawing.Point(23, 84);
            this.lblMPCost.Name = "lblMPCost";
            this.lblMPCost.Size = new System.Drawing.Size(59, 13);
            this.lblMPCost.TabIndex = 24;
            this.lblMPCost.Text = "MP Cost: 0";
            // 
            // scrlHPCost
            // 
            this.scrlHPCost.LargeChange = 100;
            this.scrlHPCost.Location = new System.Drawing.Point(23, 67);
            this.scrlHPCost.Maximum = 2500;
            this.scrlHPCost.Name = "scrlHPCost";
            this.scrlHPCost.Size = new System.Drawing.Size(156, 17);
            this.scrlHPCost.TabIndex = 23;
            this.scrlHPCost.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlHPCost_Scroll);
            // 
            // lblHPCost
            // 
            this.lblHPCost.AutoSize = true;
            this.lblHPCost.Location = new System.Drawing.Point(23, 54);
            this.lblHPCost.Name = "lblHPCost";
            this.lblHPCost.Size = new System.Drawing.Size(58, 13);
            this.lblHPCost.TabIndex = 22;
            this.lblHPCost.Text = "HP Cost: 0";
            // 
            // scrlVital
            // 
            this.scrlVital.LargeChange = 100;
            this.scrlVital.Location = new System.Drawing.Point(23, 37);
            this.scrlVital.Maximum = 2500;
            this.scrlVital.Name = "scrlVital";
            this.scrlVital.Size = new System.Drawing.Size(156, 17);
            this.scrlVital.TabIndex = 21;
            this.scrlVital.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlVital_Scroll);
            // 
            // lblVital
            // 
            this.lblVital.AutoSize = true;
            this.lblVital.Location = new System.Drawing.Point(23, 24);
            this.lblVital.Name = "lblVital";
            this.lblVital.Size = new System.Drawing.Size(39, 13);
            this.lblVital.TabIndex = 20;
            this.lblVital.Text = "Vital: 0";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.scrlTickInt);
            this.groupBox3.Controls.Add(this.lblTickInt);
            this.groupBox3.Controls.Add(this.scrlTotalTicks);
            this.groupBox3.Controls.Add(this.lblTotalTicks);
            this.groupBox3.Controls.Add(this.scrlCastTime);
            this.groupBox3.Controls.Add(this.lblCastTime);
            this.groupBox3.Controls.Add(this.scrlCoolDown);
            this.groupBox3.Controls.Add(this.lblCoolDown);
            this.groupBox3.Location = new System.Drawing.Point(363, 151);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 165);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Events";
            // 
            // scrlTickInt
            // 
            this.scrlTickInt.LargeChange = 500;
            this.scrlTickInt.Location = new System.Drawing.Point(23, 135);
            this.scrlTickInt.Maximum = 25000;
            this.scrlTickInt.Minimum = 50;
            this.scrlTickInt.Name = "scrlTickInt";
            this.scrlTickInt.Size = new System.Drawing.Size(156, 17);
            this.scrlTickInt.SmallChange = 50;
            this.scrlTickInt.TabIndex = 37;
            this.scrlTickInt.Value = 50;
            this.scrlTickInt.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlTickInt_Scroll);
            // 
            // lblTickInt
            // 
            this.lblTickInt.AutoSize = true;
            this.lblTickInt.Location = new System.Drawing.Point(23, 121);
            this.lblTickInt.Name = "lblTickInt";
            this.lblTickInt.Size = new System.Drawing.Size(97, 13);
            this.lblTickInt.TabIndex = 36;
            this.lblTickInt.Text = "Tick Interval: 50ms";
            // 
            // scrlTotalTicks
            // 
            this.scrlTotalTicks.LargeChange = 1;
            this.scrlTotalTicks.Location = new System.Drawing.Point(22, 102);
            this.scrlTotalTicks.Maximum = 5;
            this.scrlTotalTicks.Name = "scrlTotalTicks";
            this.scrlTotalTicks.Size = new System.Drawing.Size(156, 17);
            this.scrlTotalTicks.TabIndex = 35;
            this.scrlTotalTicks.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlTotalTicks_Scroll);
            // 
            // lblTotalTicks
            // 
            this.lblTotalTicks.AutoSize = true;
            this.lblTotalTicks.Location = new System.Drawing.Point(22, 88);
            this.lblTotalTicks.Name = "lblTotalTicks";
            this.lblTotalTicks.Size = new System.Drawing.Size(72, 13);
            this.lblTotalTicks.TabIndex = 34;
            this.lblTotalTicks.Text = "Total Ticks: 0";
            // 
            // scrlCastTime
            // 
            this.scrlCastTime.LargeChange = 500;
            this.scrlCastTime.Location = new System.Drawing.Point(22, 68);
            this.scrlCastTime.Maximum = 10000;
            this.scrlCastTime.Name = "scrlCastTime";
            this.scrlCastTime.Size = new System.Drawing.Size(156, 17);
            this.scrlCastTime.SmallChange = 50;
            this.scrlCastTime.TabIndex = 33;
            this.scrlCastTime.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlCastTime_Scroll);
            // 
            // lblCastTime
            // 
            this.lblCastTime.AutoSize = true;
            this.lblCastTime.Location = new System.Drawing.Point(22, 54);
            this.lblCastTime.Name = "lblCastTime";
            this.lblCastTime.Size = new System.Drawing.Size(92, 13);
            this.lblCastTime.TabIndex = 32;
            this.lblCastTime.Text = "Cast Time: Instant";
            // 
            // scrlCoolDown
            // 
            this.scrlCoolDown.LargeChange = 500;
            this.scrlCoolDown.Location = new System.Drawing.Point(22, 32);
            this.scrlCoolDown.Maximum = 50000;
            this.scrlCoolDown.Name = "scrlCoolDown";
            this.scrlCoolDown.Size = new System.Drawing.Size(156, 17);
            this.scrlCoolDown.SmallChange = 50;
            this.scrlCoolDown.TabIndex = 31;
            this.scrlCoolDown.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlCoolDown_Scroll);
            // 
            // lblCoolDown
            // 
            this.lblCoolDown.AutoSize = true;
            this.lblCoolDown.Location = new System.Drawing.Point(22, 18);
            this.lblCoolDown.Name = "lblCoolDown";
            this.lblCoolDown.Size = new System.Drawing.Size(97, 13);
            this.lblCoolDown.TabIndex = 30;
            this.lblCoolDown.Text = "Cool Down: Instant";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.picProj);
            this.groupBox4.Controls.Add(this.lblProj);
            this.groupBox4.Controls.Add(this.scrlProj);
            this.groupBox4.Controls.Add(this.chkProj);
            this.groupBox4.Location = new System.Drawing.Point(362, 322);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 100);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Projectile";
            // 
            // picProj
            // 
            this.picProj.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.picProj.Location = new System.Drawing.Point(24, 45);
            this.picProj.Name = "picProj";
            this.picProj.Size = new System.Drawing.Size(32, 32);
            this.picProj.TabIndex = 43;
            this.picProj.TabStop = false;
            // 
            // lblProj
            // 
            this.lblProj.AutoSize = true;
            this.lblProj.Location = new System.Drawing.Point(62, 45);
            this.lblProj.Name = "lblProj";
            this.lblProj.Size = new System.Drawing.Size(46, 13);
            this.lblProj.TabIndex = 42;
            this.lblProj.Text = "Sprite: 1";
            // 
            // scrlProj
            // 
            this.scrlProj.LargeChange = 1;
            this.scrlProj.Location = new System.Drawing.Point(59, 60);
            this.scrlProj.Maximum = 5;
            this.scrlProj.Minimum = 1;
            this.scrlProj.Name = "scrlProj";
            this.scrlProj.Size = new System.Drawing.Size(118, 17);
            this.scrlProj.TabIndex = 41;
            this.scrlProj.Value = 1;
            this.scrlProj.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlProj_Scroll);
            // 
            // chkProj
            // 
            this.chkProj.AutoSize = true;
            this.chkProj.Location = new System.Drawing.Point(12, 19);
            this.chkProj.Name = "chkProj";
            this.chkProj.Size = new System.Drawing.Size(69, 17);
            this.chkProj.TabIndex = 40;
            this.chkProj.Text = "Projectile";
            this.chkProj.UseVisualStyleBackColor = true;
            this.chkProj.CheckedChanged += new System.EventHandler(this.chkProj_CheckedChanged);
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
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(20, 43);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(156, 20);
            this.txtName.TabIndex = 6;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // scrlIcon
            // 
            this.scrlIcon.LargeChange = 1;
            this.scrlIcon.Location = new System.Drawing.Point(58, 84);
            this.scrlIcon.Maximum = 4;
            this.scrlIcon.Minimum = 1;
            this.scrlIcon.Name = "scrlIcon";
            this.scrlIcon.Size = new System.Drawing.Size(118, 17);
            this.scrlIcon.TabIndex = 7;
            this.scrlIcon.Value = 1;
            this.scrlIcon.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlIcon_Scroll);
            // 
            // lblIcon
            // 
            this.lblIcon.AutoSize = true;
            this.lblIcon.Location = new System.Drawing.Point(61, 69);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(40, 13);
            this.lblIcon.TabIndex = 8;
            this.lblIcon.Text = "Icon: 0";
            // 
            // picIcon
            // 
            this.picIcon.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.picIcon.Location = new System.Drawing.Point(23, 69);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(32, 32);
            this.picIcon.TabIndex = 9;
            this.picIcon.TabStop = false;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(21, 104);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(34, 13);
            this.lblType.TabIndex = 14;
            this.lblType.Text = "Type:";
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            "None",
            "Damage",
            "Heal",
            "Buff",
            "Debuff",
            "Dash",
            "Shield"});
            this.cmbType.Location = new System.Drawing.Point(24, 120);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(153, 21);
            this.cmbType.TabIndex = 15;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // lblCharges
            // 
            this.lblCharges.AutoSize = true;
            this.lblCharges.Location = new System.Drawing.Point(21, 188);
            this.lblCharges.Name = "lblCharges";
            this.lblCharges.Size = new System.Drawing.Size(58, 13);
            this.lblCharges.TabIndex = 24;
            this.lblCharges.Text = "Charges: 0";
            // 
            // scrlCharges
            // 
            this.scrlCharges.Location = new System.Drawing.Point(21, 202);
            this.scrlCharges.Maximum = 200;
            this.scrlCharges.Name = "scrlCharges";
            this.scrlCharges.Size = new System.Drawing.Size(156, 17);
            this.scrlCharges.TabIndex = 25;
            this.scrlCharges.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlCharges_Scroll);
            // 
            // lblRange
            // 
            this.lblRange.AutoSize = true;
            this.lblRange.Location = new System.Drawing.Point(21, 224);
            this.lblRange.Name = "lblRange";
            this.lblRange.Size = new System.Drawing.Size(51, 13);
            this.lblRange.TabIndex = 30;
            this.lblRange.Text = "Range: 0";
            // 
            // scrlRange
            // 
            this.scrlRange.LargeChange = 5;
            this.scrlRange.Location = new System.Drawing.Point(21, 238);
            this.scrlRange.Maximum = 50;
            this.scrlRange.Name = "scrlRange";
            this.scrlRange.Size = new System.Drawing.Size(156, 17);
            this.scrlRange.TabIndex = 31;
            this.scrlRange.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlRange_Scroll);
            // 
            // lblLevelReq
            // 
            this.lblLevelReq.AutoSize = true;
            this.lblLevelReq.Location = new System.Drawing.Point(21, 154);
            this.lblLevelReq.Name = "lblLevelReq";
            this.lblLevelReq.Size = new System.Drawing.Size(45, 13);
            this.lblLevelReq.TabIndex = 34;
            this.lblLevelReq.Text = "Level: 0";
            // 
            // scrlLevelReq
            // 
            this.scrlLevelReq.Location = new System.Drawing.Point(21, 168);
            this.scrlLevelReq.Maximum = 99;
            this.scrlLevelReq.Name = "scrlLevelReq";
            this.scrlLevelReq.Size = new System.Drawing.Size(156, 17);
            this.scrlLevelReq.TabIndex = 35;
            this.scrlLevelReq.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlLevelReq_Scroll);
            // 
            // chkAOE
            // 
            this.chkAOE.AutoSize = true;
            this.chkAOE.Location = new System.Drawing.Point(24, 286);
            this.chkAOE.Name = "chkAOE";
            this.chkAOE.Size = new System.Drawing.Size(91, 17);
            this.chkAOE.TabIndex = 36;
            this.chkAOE.Text = "Area of Effect";
            this.chkAOE.UseVisualStyleBackColor = true;
            this.chkAOE.CheckedChanged += new System.EventHandler(this.chkAOE_CheckedChanged);
            // 
            // lblDistance
            // 
            this.lblDistance.AutoSize = true;
            this.lblDistance.Location = new System.Drawing.Point(21, 306);
            this.lblDistance.Name = "lblDistance";
            this.lblDistance.Size = new System.Drawing.Size(61, 13);
            this.lblDistance.TabIndex = 37;
            this.lblDistance.Text = "Distance: 0";
            // 
            // scrlDistance
            // 
            this.scrlDistance.LargeChange = 5;
            this.scrlDistance.Location = new System.Drawing.Point(21, 323);
            this.scrlDistance.Maximum = 50;
            this.scrlDistance.Name = "scrlDistance";
            this.scrlDistance.Size = new System.Drawing.Size(156, 17);
            this.scrlDistance.TabIndex = 38;
            this.scrlDistance.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlDistance_Scroll);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.chkSelfCast);
            this.pnlMain.Controls.Add(this.scrlDistance);
            this.pnlMain.Controls.Add(this.lblDistance);
            this.pnlMain.Controls.Add(this.chkAOE);
            this.pnlMain.Controls.Add(this.scrlLevelReq);
            this.pnlMain.Controls.Add(this.lblLevelReq);
            this.pnlMain.Controls.Add(this.scrlRange);
            this.pnlMain.Controls.Add(this.lblRange);
            this.pnlMain.Controls.Add(this.scrlCharges);
            this.pnlMain.Controls.Add(this.lblCharges);
            this.pnlMain.Controls.Add(this.cmbType);
            this.pnlMain.Controls.Add(this.lblType);
            this.pnlMain.Controls.Add(this.picIcon);
            this.pnlMain.Controls.Add(this.lblIcon);
            this.pnlMain.Controls.Add(this.scrlIcon);
            this.pnlMain.Controls.Add(this.txtName);
            this.pnlMain.Controls.Add(this.lblName);
            this.pnlMain.Location = new System.Drawing.Point(159, 15);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(197, 407);
            this.pnlMain.TabIndex = 9;
            this.pnlMain.TabStop = false;
            this.pnlMain.Text = "Properties";
            // 
            // chkSelfCast
            // 
            this.chkSelfCast.AutoSize = true;
            this.chkSelfCast.Location = new System.Drawing.Point(24, 263);
            this.chkSelfCast.Name = "chkSelfCast";
            this.chkSelfCast.Size = new System.Drawing.Size(68, 17);
            this.chkSelfCast.TabIndex = 39;
            this.chkSelfCast.Text = "Self Cast";
            this.chkSelfCast.UseVisualStyleBackColor = true;
            this.chkSelfCast.CheckedChanged += new System.EventHandler(this.chkSelfCast_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.chkRenderOnTarget);
            this.groupBox5.Controls.Add(this.picAnimation);
            this.groupBox5.Controls.Add(this.scrlAnimation);
            this.groupBox5.Controls.Add(this.lblAnimation);
            this.groupBox5.Location = new System.Drawing.Point(569, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(200, 273);
            this.groupBox5.TabIndex = 13;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Animation";
            // 
            // chkRenderOnTarget
            // 
            this.chkRenderOnTarget.AutoSize = true;
            this.chkRenderOnTarget.Location = new System.Drawing.Point(19, 58);
            this.chkRenderOnTarget.Name = "chkRenderOnTarget";
            this.chkRenderOnTarget.Size = new System.Drawing.Size(112, 17);
            this.chkRenderOnTarget.TabIndex = 45;
            this.chkRenderOnTarget.Text = "Render On Target";
            this.chkRenderOnTarget.UseVisualStyleBackColor = true;
            this.chkRenderOnTarget.CheckedChanged += new System.EventHandler(this.chkRenderOnTarget_CheckedChanged);
            // 
            // picAnimation
            // 
            this.picAnimation.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.picAnimation.Location = new System.Drawing.Point(6, 81);
            this.picAnimation.Name = "picAnimation";
            this.picAnimation.Size = new System.Drawing.Size(188, 186);
            this.picAnimation.TabIndex = 44;
            this.picAnimation.TabStop = false;
            // 
            // scrlAnimation
            // 
            this.scrlAnimation.LargeChange = 1;
            this.scrlAnimation.Location = new System.Drawing.Point(16, 38);
            this.scrlAnimation.Maximum = 50;
            this.scrlAnimation.Name = "scrlAnimation";
            this.scrlAnimation.Size = new System.Drawing.Size(156, 17);
            this.scrlAnimation.TabIndex = 35;
            this.scrlAnimation.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlAnimation_Scroll);
            // 
            // lblAnimation
            // 
            this.lblAnimation.AutoSize = true;
            this.lblAnimation.Location = new System.Drawing.Point(16, 22);
            this.lblAnimation.Name = "lblAnimation";
            this.lblAnimation.Size = new System.Drawing.Size(65, 13);
            this.lblAnimation.TabIndex = 34;
            this.lblAnimation.Text = "Animation: 0";
            // 
            // SpellEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 440);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SpellEditor";
            this.Text = "SpellEditor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProj)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAnimation)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnNewItem;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ListBox lstIndex;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.HScrollBar scrlMPCost;
        private System.Windows.Forms.Label lblMPCost;
        private System.Windows.Forms.HScrollBar scrlHPCost;
        private System.Windows.Forms.Label lblHPCost;
        private System.Windows.Forms.HScrollBar scrlVital;
        private System.Windows.Forms.Label lblVital;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.HScrollBar scrlTickInt;
        private System.Windows.Forms.Label lblTickInt;
        private System.Windows.Forms.HScrollBar scrlTotalTicks;
        private System.Windows.Forms.Label lblTotalTicks;
        private System.Windows.Forms.HScrollBar scrlCastTime;
        private System.Windows.Forms.Label lblCastTime;
        private System.Windows.Forms.HScrollBar scrlCoolDown;
        private System.Windows.Forms.Label lblCoolDown;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.PictureBox picProj;
        private System.Windows.Forms.Label lblProj;
        private System.Windows.Forms.HScrollBar scrlProj;
        private System.Windows.Forms.CheckBox chkProj;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.HScrollBar scrlIcon;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.PictureBox picIcon;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label lblCharges;
        private System.Windows.Forms.HScrollBar scrlCharges;
        private System.Windows.Forms.Label lblRange;
        private System.Windows.Forms.HScrollBar scrlRange;
        private System.Windows.Forms.Label lblLevelReq;
        private System.Windows.Forms.HScrollBar scrlLevelReq;
        private System.Windows.Forms.CheckBox chkAOE;
        private System.Windows.Forms.Label lblDistance;
        private System.Windows.Forms.HScrollBar scrlDistance;
        private System.Windows.Forms.GroupBox pnlMain;
        private System.Windows.Forms.CheckBox chkSelfCast;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.PictureBox picAnimation;
        private System.Windows.Forms.HScrollBar scrlAnimation;
        private System.Windows.Forms.Label lblAnimation;
        private System.Windows.Forms.CheckBox chkRenderOnTarget;
    }
}