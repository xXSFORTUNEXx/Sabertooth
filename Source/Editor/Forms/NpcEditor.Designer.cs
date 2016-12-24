namespace Editor.Forms
{
    partial class NpcEditor
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
            this.pnlMain = new System.Windows.Forms.GroupBox();
            this.scrlDamage = new System.Windows.Forms.HScrollBar();
            this.lblDamage = new System.Windows.Forms.Label();
            this.scrlMaxHealth = new System.Windows.Forms.HScrollBar();
            this.lblMaxHealth = new System.Windows.Forms.Label();
            this.scrlHealth = new System.Windows.Forms.HScrollBar();
            this.lblHealth = new System.Windows.Forms.Label();
            this.scrlSpawnTime = new System.Windows.Forms.HScrollBar();
            this.lblSpawnTime = new System.Windows.Forms.Label();
            this.scrlOwner = new System.Windows.Forms.HScrollBar();
            this.lblOwner = new System.Windows.Forms.Label();
            this.scrlStep = new System.Windows.Forms.HScrollBar();
            this.lblStep = new System.Windows.Forms.Label();
            this.scrlDirection = new System.Windows.Forms.HScrollBar();
            this.lblDirection = new System.Windows.Forms.Label();
            this.cmbBehavior = new System.Windows.Forms.ComboBox();
            this.lblBehavior = new System.Windows.Forms.Label();
            this.scrlY = new System.Windows.Forms.HScrollBar();
            this.lblY = new System.Windows.Forms.Label();
            this.scrlX = new System.Windows.Forms.HScrollBar();
            this.lblX = new System.Windows.Forms.Label();
            this.picSprite = new System.Windows.Forms.PictureBox();
            this.lblSprite = new System.Windows.Forms.Label();
            this.scrlSprite = new System.Windows.Forms.HScrollBar();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSprite)).BeginInit();
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
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.scrlDamage);
            this.pnlMain.Controls.Add(this.lblDamage);
            this.pnlMain.Controls.Add(this.scrlMaxHealth);
            this.pnlMain.Controls.Add(this.lblMaxHealth);
            this.pnlMain.Controls.Add(this.scrlHealth);
            this.pnlMain.Controls.Add(this.lblHealth);
            this.pnlMain.Controls.Add(this.scrlSpawnTime);
            this.pnlMain.Controls.Add(this.lblSpawnTime);
            this.pnlMain.Controls.Add(this.scrlOwner);
            this.pnlMain.Controls.Add(this.lblOwner);
            this.pnlMain.Controls.Add(this.scrlStep);
            this.pnlMain.Controls.Add(this.lblStep);
            this.pnlMain.Controls.Add(this.scrlDirection);
            this.pnlMain.Controls.Add(this.lblDirection);
            this.pnlMain.Controls.Add(this.cmbBehavior);
            this.pnlMain.Controls.Add(this.lblBehavior);
            this.pnlMain.Controls.Add(this.scrlY);
            this.pnlMain.Controls.Add(this.lblY);
            this.pnlMain.Controls.Add(this.scrlX);
            this.pnlMain.Controls.Add(this.lblX);
            this.pnlMain.Controls.Add(this.picSprite);
            this.pnlMain.Controls.Add(this.lblSprite);
            this.pnlMain.Controls.Add(this.scrlSprite);
            this.pnlMain.Controls.Add(this.txtName);
            this.pnlMain.Controls.Add(this.lblName);
            this.pnlMain.Location = new System.Drawing.Point(159, 12);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(393, 380);
            this.pnlMain.TabIndex = 9;
            this.pnlMain.TabStop = false;
            this.pnlMain.Text = "Properties";
            this.pnlMain.Visible = false;
            // 
            // scrlDamage
            // 
            this.scrlDamage.Location = new System.Drawing.Point(215, 142);
            this.scrlDamage.Name = "scrlDamage";
            this.scrlDamage.Size = new System.Drawing.Size(156, 17);
            this.scrlDamage.TabIndex = 31;
            this.scrlDamage.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlDamage_Scroll);
            // 
            // lblDamage
            // 
            this.lblDamage.AutoSize = true;
            this.lblDamage.Location = new System.Drawing.Point(212, 129);
            this.lblDamage.Name = "lblDamage";
            this.lblDamage.Size = new System.Drawing.Size(59, 13);
            this.lblDamage.TabIndex = 30;
            this.lblDamage.Text = "Damage: 0";
            // 
            // scrlMaxHealth
            // 
            this.scrlMaxHealth.Location = new System.Drawing.Point(215, 112);
            this.scrlMaxHealth.Name = "scrlMaxHealth";
            this.scrlMaxHealth.Size = new System.Drawing.Size(156, 17);
            this.scrlMaxHealth.TabIndex = 29;
            this.scrlMaxHealth.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlMaxHealth_Scroll);
            // 
            // lblMaxHealth
            // 
            this.lblMaxHealth.AutoSize = true;
            this.lblMaxHealth.Location = new System.Drawing.Point(212, 99);
            this.lblMaxHealth.Name = "lblMaxHealth";
            this.lblMaxHealth.Size = new System.Drawing.Size(73, 13);
            this.lblMaxHealth.TabIndex = 28;
            this.lblMaxHealth.Text = "Max Health: 0";
            // 
            // scrlHealth
            // 
            this.scrlHealth.Location = new System.Drawing.Point(215, 82);
            this.scrlHealth.Name = "scrlHealth";
            this.scrlHealth.Size = new System.Drawing.Size(156, 17);
            this.scrlHealth.TabIndex = 27;
            this.scrlHealth.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlHealth_Scroll);
            // 
            // lblHealth
            // 
            this.lblHealth.AutoSize = true;
            this.lblHealth.Location = new System.Drawing.Point(212, 69);
            this.lblHealth.Name = "lblHealth";
            this.lblHealth.Size = new System.Drawing.Size(50, 13);
            this.lblHealth.TabIndex = 26;
            this.lblHealth.Text = "Health: 0";
            // 
            // scrlSpawnTime
            // 
            this.scrlSpawnTime.Location = new System.Drawing.Point(17, 287);
            this.scrlSpawnTime.Name = "scrlSpawnTime";
            this.scrlSpawnTime.Size = new System.Drawing.Size(156, 17);
            this.scrlSpawnTime.TabIndex = 25;
            this.scrlSpawnTime.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlSpawnTime_Scroll);
            // 
            // lblSpawnTime
            // 
            this.lblSpawnTime.AutoSize = true;
            this.lblSpawnTime.Location = new System.Drawing.Point(17, 274);
            this.lblSpawnTime.Name = "lblSpawnTime";
            this.lblSpawnTime.Size = new System.Drawing.Size(78, 13);
            this.lblSpawnTime.TabIndex = 24;
            this.lblSpawnTime.Text = "Spawn Time: 0";
            // 
            // scrlOwner
            // 
            this.scrlOwner.Location = new System.Drawing.Point(17, 257);
            this.scrlOwner.Name = "scrlOwner";
            this.scrlOwner.Size = new System.Drawing.Size(156, 17);
            this.scrlOwner.TabIndex = 23;
            this.scrlOwner.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlOwner_Scroll);
            // 
            // lblOwner
            // 
            this.lblOwner.AutoSize = true;
            this.lblOwner.Location = new System.Drawing.Point(17, 244);
            this.lblOwner.Name = "lblOwner";
            this.lblOwner.Size = new System.Drawing.Size(50, 13);
            this.lblOwner.TabIndex = 22;
            this.lblOwner.Text = "Owner: 0";
            // 
            // scrlStep
            // 
            this.scrlStep.LargeChange = 6;
            this.scrlStep.Location = new System.Drawing.Point(17, 223);
            this.scrlStep.Maximum = 5;
            this.scrlStep.Name = "scrlStep";
            this.scrlStep.Size = new System.Drawing.Size(156, 17);
            this.scrlStep.TabIndex = 21;
            this.scrlStep.Value = 1;
            this.scrlStep.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlStep_Scroll);
            // 
            // lblStep
            // 
            this.lblStep.AutoSize = true;
            this.lblStep.Location = new System.Drawing.Point(17, 210);
            this.lblStep.Name = "lblStep";
            this.lblStep.Size = new System.Drawing.Size(41, 13);
            this.lblStep.TabIndex = 20;
            this.lblStep.Text = "Step: 0";
            // 
            // scrlDirection
            // 
            this.scrlDirection.LargeChange = 4;
            this.scrlDirection.Location = new System.Drawing.Point(17, 193);
            this.scrlDirection.Maximum = 3;
            this.scrlDirection.Name = "scrlDirection";
            this.scrlDirection.Size = new System.Drawing.Size(156, 17);
            this.scrlDirection.TabIndex = 19;
            this.scrlDirection.Value = 1;
            this.scrlDirection.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlDirection_Scroll);
            // 
            // lblDirection
            // 
            this.lblDirection.AutoSize = true;
            this.lblDirection.Location = new System.Drawing.Point(17, 180);
            this.lblDirection.Name = "lblDirection";
            this.lblDirection.Size = new System.Drawing.Size(61, 13);
            this.lblDirection.TabIndex = 18;
            this.lblDirection.Text = "Direction: 0";
            // 
            // cmbBehavior
            // 
            this.cmbBehavior.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBehavior.FormattingEnabled = true;
            this.cmbBehavior.Items.AddRange(new object[] {
            "Friendly",
            "Passive",
            "Aggressive",
            "ToLocation"});
            this.cmbBehavior.Location = new System.Drawing.Point(215, 42);
            this.cmbBehavior.Name = "cmbBehavior";
            this.cmbBehavior.Size = new System.Drawing.Size(156, 21);
            this.cmbBehavior.TabIndex = 15;
            this.cmbBehavior.SelectedIndexChanged += new System.EventHandler(this.cmbBehavior_SelectedIndexChanged);
            // 
            // lblBehavior
            // 
            this.lblBehavior.AutoSize = true;
            this.lblBehavior.Location = new System.Drawing.Point(212, 27);
            this.lblBehavior.Name = "lblBehavior";
            this.lblBehavior.Size = new System.Drawing.Size(52, 13);
            this.lblBehavior.TabIndex = 14;
            this.lblBehavior.Text = "Behavior:";
            // 
            // scrlY
            // 
            this.scrlY.Location = new System.Drawing.Point(17, 163);
            this.scrlY.Name = "scrlY";
            this.scrlY.Size = new System.Drawing.Size(156, 17);
            this.scrlY.TabIndex = 13;
            this.scrlY.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlY_Scroll);
            // 
            // lblY
            // 
            this.lblY.AutoSize = true;
            this.lblY.Location = new System.Drawing.Point(17, 150);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(26, 13);
            this.lblY.TabIndex = 12;
            this.lblY.Text = "Y: 0";
            // 
            // scrlX
            // 
            this.scrlX.Location = new System.Drawing.Point(17, 133);
            this.scrlX.Name = "scrlX";
            this.scrlX.Size = new System.Drawing.Size(156, 17);
            this.scrlX.TabIndex = 11;
            this.scrlX.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlX_Scroll);
            // 
            // lblX
            // 
            this.lblX.AutoSize = true;
            this.lblX.Location = new System.Drawing.Point(17, 120);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(26, 13);
            this.lblX.TabIndex = 10;
            this.lblX.Text = "X: 0";
            // 
            // picSprite
            // 
            this.picSprite.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.picSprite.Location = new System.Drawing.Point(23, 69);
            this.picSprite.Name = "picSprite";
            this.picSprite.Size = new System.Drawing.Size(32, 48);
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
            this.scrlSprite.Location = new System.Drawing.Point(58, 84);
            this.scrlSprite.Maximum = 204;
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
            // NpcEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 409);
            this.ControlBox = false;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.groupBox1);
            this.Name = "NpcEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Npc Editor";
            this.groupBox1.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSprite)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnNewItem;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ListBox lstIndex;
        private System.Windows.Forms.GroupBox pnlMain;
        private System.Windows.Forms.HScrollBar scrlDamage;
        private System.Windows.Forms.Label lblDamage;
        private System.Windows.Forms.HScrollBar scrlMaxHealth;
        private System.Windows.Forms.Label lblMaxHealth;
        private System.Windows.Forms.HScrollBar scrlHealth;
        private System.Windows.Forms.Label lblHealth;
        private System.Windows.Forms.HScrollBar scrlSpawnTime;
        private System.Windows.Forms.Label lblSpawnTime;
        private System.Windows.Forms.HScrollBar scrlOwner;
        private System.Windows.Forms.Label lblOwner;
        private System.Windows.Forms.HScrollBar scrlStep;
        private System.Windows.Forms.Label lblStep;
        private System.Windows.Forms.HScrollBar scrlDirection;
        private System.Windows.Forms.Label lblDirection;
        private System.Windows.Forms.ComboBox cmbBehavior;
        private System.Windows.Forms.Label lblBehavior;
        private System.Windows.Forms.HScrollBar scrlY;
        private System.Windows.Forms.Label lblY;
        private System.Windows.Forms.HScrollBar scrlX;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.PictureBox picSprite;
        private System.Windows.Forms.Label lblSprite;
        private System.Windows.Forms.HScrollBar scrlSprite;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
    }
}