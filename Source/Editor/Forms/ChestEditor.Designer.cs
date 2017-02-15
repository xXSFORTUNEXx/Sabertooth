namespace Editor.Forms
{
    partial class ChestEditor
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
            this.pnlProperties = new System.Windows.Forms.GroupBox();
            this.scrlSpawnAmount = new System.Windows.Forms.HScrollBar();
            this.lblSpawnAmount = new System.Windows.Forms.Label();
            this.scrlNpcSpawnNum = new System.Windows.Forms.HScrollBar();
            this.lblNpcSpawn = new System.Windows.Forms.Label();
            this.scrlDamage = new System.Windows.Forms.HScrollBar();
            this.lblDamage = new System.Windows.Forms.Label();
            this.scrlKey = new System.Windows.Forms.HScrollBar();
            this.lblKey = new System.Windows.Forms.Label();
            this.scrlTrapLevel = new System.Windows.Forms.HScrollBar();
            this.lblTrapLevel = new System.Windows.Forms.Label();
            this.scrlReqLevel = new System.Windows.Forms.HScrollBar();
            this.lblReqLvl = new System.Windows.Forms.Label();
            this.scrlExp = new System.Windows.Forms.HScrollBar();
            this.lblExperience = new System.Windows.Forms.Label();
            this.scrlMoney = new System.Windows.Forms.HScrollBar();
            this.lblMoney = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlItems = new System.Windows.Forms.GroupBox();
            this.scrlValue = new System.Windows.Forms.HScrollBar();
            this.lblValue = new System.Windows.Forms.Label();
            this.lblContents = new System.Windows.Forms.Label();
            this.cmbItemsToAdd = new System.Windows.Forms.ComboBox();
            this.lblAddItem = new System.Windows.Forms.Label();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.lstItems = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.pnlProperties.SuspendLayout();
            this.pnlItems.SuspendLayout();
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
            // pnlProperties
            // 
            this.pnlProperties.Controls.Add(this.scrlSpawnAmount);
            this.pnlProperties.Controls.Add(this.lblSpawnAmount);
            this.pnlProperties.Controls.Add(this.scrlNpcSpawnNum);
            this.pnlProperties.Controls.Add(this.lblNpcSpawn);
            this.pnlProperties.Controls.Add(this.scrlDamage);
            this.pnlProperties.Controls.Add(this.lblDamage);
            this.pnlProperties.Controls.Add(this.scrlKey);
            this.pnlProperties.Controls.Add(this.lblKey);
            this.pnlProperties.Controls.Add(this.scrlTrapLevel);
            this.pnlProperties.Controls.Add(this.lblTrapLevel);
            this.pnlProperties.Controls.Add(this.scrlReqLevel);
            this.pnlProperties.Controls.Add(this.lblReqLvl);
            this.pnlProperties.Controls.Add(this.scrlExp);
            this.pnlProperties.Controls.Add(this.lblExperience);
            this.pnlProperties.Controls.Add(this.scrlMoney);
            this.pnlProperties.Controls.Add(this.lblMoney);
            this.pnlProperties.Controls.Add(this.txtName);
            this.pnlProperties.Controls.Add(this.label1);
            this.pnlProperties.Location = new System.Drawing.Point(159, 12);
            this.pnlProperties.Name = "pnlProperties";
            this.pnlProperties.Size = new System.Drawing.Size(200, 315);
            this.pnlProperties.TabIndex = 9;
            this.pnlProperties.TabStop = false;
            this.pnlProperties.Text = "Properties";
            this.pnlProperties.Visible = false;
            // 
            // scrlSpawnAmount
            // 
            this.scrlSpawnAmount.Location = new System.Drawing.Point(12, 281);
            this.scrlSpawnAmount.Name = "scrlSpawnAmount";
            this.scrlSpawnAmount.Size = new System.Drawing.Size(171, 17);
            this.scrlSpawnAmount.TabIndex = 17;
            this.scrlSpawnAmount.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlSpawnAmount_Scroll);
            // 
            // lblSpawnAmount
            // 
            this.lblSpawnAmount.AutoSize = true;
            this.lblSpawnAmount.Location = new System.Drawing.Point(9, 268);
            this.lblSpawnAmount.Name = "lblSpawnAmount";
            this.lblSpawnAmount.Size = new System.Drawing.Size(91, 13);
            this.lblSpawnAmount.TabIndex = 16;
            this.lblSpawnAmount.Text = "Spawn Amount: 0";
            // 
            // scrlNpcSpawnNum
            // 
            this.scrlNpcSpawnNum.Location = new System.Drawing.Point(9, 251);
            this.scrlNpcSpawnNum.Name = "scrlNpcSpawnNum";
            this.scrlNpcSpawnNum.Size = new System.Drawing.Size(174, 17);
            this.scrlNpcSpawnNum.TabIndex = 15;
            this.scrlNpcSpawnNum.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlNpcSpawnNum_Scroll);
            // 
            // lblNpcSpawn
            // 
            this.lblNpcSpawn.AutoSize = true;
            this.lblNpcSpawn.Location = new System.Drawing.Point(9, 238);
            this.lblNpcSpawn.Name = "lblNpcSpawn";
            this.lblNpcSpawn.Size = new System.Drawing.Size(115, 13);
            this.lblNpcSpawn.TabIndex = 14;
            this.lblNpcSpawn.Text = "Npc Spawn Number: 0";
            // 
            // scrlDamage
            // 
            this.scrlDamage.Location = new System.Drawing.Point(9, 221);
            this.scrlDamage.Name = "scrlDamage";
            this.scrlDamage.Size = new System.Drawing.Size(174, 17);
            this.scrlDamage.TabIndex = 13;
            this.scrlDamage.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlDamage_Scroll);
            // 
            // lblDamage
            // 
            this.lblDamage.AutoSize = true;
            this.lblDamage.Location = new System.Drawing.Point(9, 208);
            this.lblDamage.Name = "lblDamage";
            this.lblDamage.Size = new System.Drawing.Size(59, 13);
            this.lblDamage.TabIndex = 12;
            this.lblDamage.Text = "Damage: 0";
            // 
            // scrlKey
            // 
            this.scrlKey.Location = new System.Drawing.Point(9, 191);
            this.scrlKey.Name = "scrlKey";
            this.scrlKey.Size = new System.Drawing.Size(174, 17);
            this.scrlKey.TabIndex = 11;
            this.scrlKey.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlKey_Scroll);
            // 
            // lblKey
            // 
            this.lblKey.AutoSize = true;
            this.lblKey.Location = new System.Drawing.Point(9, 178);
            this.lblKey.Name = "lblKey";
            this.lblKey.Size = new System.Drawing.Size(37, 13);
            this.lblKey.TabIndex = 10;
            this.lblKey.Text = "Key: 0";
            // 
            // scrlTrapLevel
            // 
            this.scrlTrapLevel.Location = new System.Drawing.Point(9, 161);
            this.scrlTrapLevel.Name = "scrlTrapLevel";
            this.scrlTrapLevel.Size = new System.Drawing.Size(174, 17);
            this.scrlTrapLevel.TabIndex = 9;
            this.scrlTrapLevel.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlTrapLevel_Scroll);
            // 
            // lblTrapLevel
            // 
            this.lblTrapLevel.AutoSize = true;
            this.lblTrapLevel.Location = new System.Drawing.Point(6, 148);
            this.lblTrapLevel.Name = "lblTrapLevel";
            this.lblTrapLevel.Size = new System.Drawing.Size(70, 13);
            this.lblTrapLevel.TabIndex = 8;
            this.lblTrapLevel.Text = "Trap Level: 0";
            // 
            // scrlReqLevel
            // 
            this.scrlReqLevel.Location = new System.Drawing.Point(9, 131);
            this.scrlReqLevel.Name = "scrlReqLevel";
            this.scrlReqLevel.Size = new System.Drawing.Size(174, 17);
            this.scrlReqLevel.TabIndex = 7;
            this.scrlReqLevel.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlReqLevel_Scroll);
            // 
            // lblReqLvl
            // 
            this.lblReqLvl.AutoSize = true;
            this.lblReqLvl.Location = new System.Drawing.Point(6, 118);
            this.lblReqLvl.Name = "lblReqLvl";
            this.lblReqLvl.Size = new System.Drawing.Size(91, 13);
            this.lblReqLvl.TabIndex = 6;
            this.lblReqLvl.Text = "Required Level: 0";
            // 
            // scrlExp
            // 
            this.scrlExp.Location = new System.Drawing.Point(9, 99);
            this.scrlExp.Maximum = 10000;
            this.scrlExp.Name = "scrlExp";
            this.scrlExp.Size = new System.Drawing.Size(174, 17);
            this.scrlExp.TabIndex = 5;
            this.scrlExp.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlExp_Scroll);
            // 
            // lblExperience
            // 
            this.lblExperience.AutoSize = true;
            this.lblExperience.Location = new System.Drawing.Point(6, 86);
            this.lblExperience.Name = "lblExperience";
            this.lblExperience.Size = new System.Drawing.Size(72, 13);
            this.lblExperience.TabIndex = 4;
            this.lblExperience.Text = "Experience: 0";
            // 
            // scrlMoney
            // 
            this.scrlMoney.Location = new System.Drawing.Point(9, 68);
            this.scrlMoney.Maximum = 15000;
            this.scrlMoney.Name = "scrlMoney";
            this.scrlMoney.Size = new System.Drawing.Size(174, 17);
            this.scrlMoney.TabIndex = 3;
            this.scrlMoney.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlMoney_Scroll);
            // 
            // lblMoney
            // 
            this.lblMoney.AutoSize = true;
            this.lblMoney.Location = new System.Drawing.Point(6, 55);
            this.lblMoney.Name = "lblMoney";
            this.lblMoney.Size = new System.Drawing.Size(51, 13);
            this.lblMoney.TabIndex = 2;
            this.lblMoney.Text = "Money: 0";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(9, 32);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(174, 20);
            this.txtName.TabIndex = 1;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // pnlItems
            // 
            this.pnlItems.Controls.Add(this.scrlValue);
            this.pnlItems.Controls.Add(this.lblValue);
            this.pnlItems.Controls.Add(this.lblContents);
            this.pnlItems.Controls.Add(this.cmbItemsToAdd);
            this.pnlItems.Controls.Add(this.lblAddItem);
            this.pnlItems.Controls.Add(this.btnAddItem);
            this.pnlItems.Controls.Add(this.lstItems);
            this.pnlItems.Location = new System.Drawing.Point(365, 12);
            this.pnlItems.Name = "pnlItems";
            this.pnlItems.Size = new System.Drawing.Size(348, 315);
            this.pnlItems.TabIndex = 10;
            this.pnlItems.TabStop = false;
            this.pnlItems.Text = "Items";
            this.pnlItems.Visible = false;
            // 
            // scrlValue
            // 
            this.scrlValue.LargeChange = 1;
            this.scrlValue.Location = new System.Drawing.Point(179, 68);
            this.scrlValue.Maximum = 10000;
            this.scrlValue.Minimum = 1;
            this.scrlValue.Name = "scrlValue";
            this.scrlValue.Size = new System.Drawing.Size(156, 17);
            this.scrlValue.TabIndex = 20;
            this.scrlValue.Value = 1;
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Location = new System.Drawing.Point(176, 55);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(46, 13);
            this.lblValue.TabIndex = 19;
            this.lblValue.Text = "Value: 1";
            // 
            // lblContents
            // 
            this.lblContents.AutoSize = true;
            this.lblContents.Location = new System.Drawing.Point(6, 16);
            this.lblContents.Name = "lblContents";
            this.lblContents.Size = new System.Drawing.Size(52, 13);
            this.lblContents.TabIndex = 18;
            this.lblContents.Text = "Contents:";
            // 
            // cmbItemsToAdd
            // 
            this.cmbItemsToAdd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbItemsToAdd.FormattingEnabled = true;
            this.cmbItemsToAdd.Location = new System.Drawing.Point(179, 30);
            this.cmbItemsToAdd.MaxDropDownItems = 100;
            this.cmbItemsToAdd.Name = "cmbItemsToAdd";
            this.cmbItemsToAdd.Size = new System.Drawing.Size(156, 21);
            this.cmbItemsToAdd.TabIndex = 17;
            // 
            // lblAddItem
            // 
            this.lblAddItem.AutoSize = true;
            this.lblAddItem.Location = new System.Drawing.Point(176, 14);
            this.lblAddItem.Name = "lblAddItem";
            this.lblAddItem.Size = new System.Drawing.Size(64, 13);
            this.lblAddItem.TabIndex = 16;
            this.lblAddItem.Text = "Item to Add:";
            // 
            // btnAddItem
            // 
            this.btnAddItem.Location = new System.Drawing.Point(260, 88);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(75, 23);
            this.btnAddItem.TabIndex = 15;
            this.btnAddItem.Text = "Add Item";
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // lstItems
            // 
            this.lstItems.FormattingEnabled = true;
            this.lstItems.Location = new System.Drawing.Point(9, 30);
            this.lstItems.Name = "lstItems";
            this.lstItems.Size = new System.Drawing.Size(156, 277);
            this.lstItems.TabIndex = 14;
            this.lstItems.DoubleClick += new System.EventHandler(this.lstItems_DoubleClicked);
            // 
            // ChestEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 402);
            this.Controls.Add(this.pnlItems);
            this.Controls.Add(this.pnlProperties);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ChestEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChestEditor";
            this.groupBox1.ResumeLayout(false);
            this.pnlProperties.ResumeLayout(false);
            this.pnlProperties.PerformLayout();
            this.pnlItems.ResumeLayout(false);
            this.pnlItems.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnNewItem;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ListBox lstIndex;
        private System.Windows.Forms.GroupBox pnlProperties;
        private System.Windows.Forms.HScrollBar scrlReqLevel;
        private System.Windows.Forms.Label lblReqLvl;
        private System.Windows.Forms.HScrollBar scrlExp;
        private System.Windows.Forms.Label lblExperience;
        private System.Windows.Forms.HScrollBar scrlMoney;
        private System.Windows.Forms.Label lblMoney;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDamage;
        private System.Windows.Forms.HScrollBar scrlKey;
        private System.Windows.Forms.Label lblKey;
        private System.Windows.Forms.HScrollBar scrlTrapLevel;
        private System.Windows.Forms.Label lblTrapLevel;
        private System.Windows.Forms.HScrollBar scrlSpawnAmount;
        private System.Windows.Forms.Label lblSpawnAmount;
        private System.Windows.Forms.HScrollBar scrlNpcSpawnNum;
        private System.Windows.Forms.Label lblNpcSpawn;
        private System.Windows.Forms.HScrollBar scrlDamage;
        private System.Windows.Forms.GroupBox pnlItems;
        private System.Windows.Forms.HScrollBar scrlValue;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.Label lblContents;
        private System.Windows.Forms.ComboBox cmbItemsToAdd;
        private System.Windows.Forms.Label lblAddItem;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.ListBox lstItems;
    }
}