namespace Editor.Forms
{
    partial class ShopEditor
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
            this.txtCustomCost = new System.Windows.Forms.TextBox();
            this.lblCost = new System.Windows.Forms.Label();
            this.cmbItemsToAdd = new System.Windows.Forms.ComboBox();
            this.lblAddItem = new System.Windows.Forms.Label();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.lblItemList = new System.Windows.Forms.Label();
            this.lstItemsSold = new System.Windows.Forms.ListBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblShopName = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.pnlMain.SuspendLayout();
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
            this.pnlMain.Controls.Add(this.txtCustomCost);
            this.pnlMain.Controls.Add(this.lblCost);
            this.pnlMain.Controls.Add(this.cmbItemsToAdd);
            this.pnlMain.Controls.Add(this.lblAddItem);
            this.pnlMain.Controls.Add(this.btnAddItem);
            this.pnlMain.Controls.Add(this.lblItemList);
            this.pnlMain.Controls.Add(this.lstItemsSold);
            this.pnlMain.Controls.Add(this.txtName);
            this.pnlMain.Controls.Add(this.lblShopName);
            this.pnlMain.Location = new System.Drawing.Point(159, 12);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(372, 286);
            this.pnlMain.TabIndex = 9;
            this.pnlMain.TabStop = false;
            this.pnlMain.Text = "Properties";
            this.pnlMain.Visible = false;
            // 
            // txtCustomCost
            // 
            this.txtCustomCost.Location = new System.Drawing.Point(194, 110);
            this.txtCustomCost.Name = "txtCustomCost";
            this.txtCustomCost.Size = new System.Drawing.Size(156, 20);
            this.txtCustomCost.TabIndex = 15;
            this.txtCustomCost.Text = "1";
            // 
            // lblCost
            // 
            this.lblCost.AutoSize = true;
            this.lblCost.Location = new System.Drawing.Point(191, 94);
            this.lblCost.Name = "lblCost";
            this.lblCost.Size = new System.Drawing.Size(66, 13);
            this.lblCost.TabIndex = 14;
            this.lblCost.Text = "Custom Cost";
            // 
            // cmbItemsToAdd
            // 
            this.cmbItemsToAdd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbItemsToAdd.FormattingEnabled = true;
            this.cmbItemsToAdd.Location = new System.Drawing.Point(194, 34);
            this.cmbItemsToAdd.MaxDropDownItems = 100;
            this.cmbItemsToAdd.Name = "cmbItemsToAdd";
            this.cmbItemsToAdd.Size = new System.Drawing.Size(156, 21);
            this.cmbItemsToAdd.TabIndex = 13;
            // 
            // lblAddItem
            // 
            this.lblAddItem.AutoSize = true;
            this.lblAddItem.Location = new System.Drawing.Point(191, 19);
            this.lblAddItem.Name = "lblAddItem";
            this.lblAddItem.Size = new System.Drawing.Size(64, 13);
            this.lblAddItem.TabIndex = 12;
            this.lblAddItem.Text = "Item to Add:";
            // 
            // btnAddItem
            // 
            this.btnAddItem.Location = new System.Drawing.Point(275, 61);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(75, 23);
            this.btnAddItem.TabIndex = 11;
            this.btnAddItem.Text = "Add Item";
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // lblItemList
            // 
            this.lblItemList.AutoSize = true;
            this.lblItemList.Location = new System.Drawing.Point(17, 58);
            this.lblItemList.Name = "lblItemList";
            this.lblItemList.Size = new System.Drawing.Size(35, 13);
            this.lblItemList.TabIndex = 10;
            this.lblItemList.Text = "Stock";
            // 
            // lstItemsSold
            // 
            this.lstItemsSold.FormattingEnabled = true;
            this.lstItemsSold.Location = new System.Drawing.Point(20, 74);
            this.lstItemsSold.Name = "lstItemsSold";
            this.lstItemsSold.Size = new System.Drawing.Size(156, 199);
            this.lstItemsSold.TabIndex = 9;
            this.lstItemsSold.DoubleClick += new System.EventHandler(this.lstItemsSold_DoubleClicked);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(20, 35);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(156, 20);
            this.txtName.TabIndex = 8;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // lblShopName
            // 
            this.lblShopName.AutoSize = true;
            this.lblShopName.Location = new System.Drawing.Point(17, 19);
            this.lblShopName.Name = "lblShopName";
            this.lblShopName.Size = new System.Drawing.Size(66, 13);
            this.lblShopName.TabIndex = 7;
            this.lblShopName.Text = "Shop Name:";
            // 
            // ShopEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 405);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.groupBox1);
            this.Name = "ShopEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ShopEditor";
            this.groupBox1.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
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
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblShopName;
        private System.Windows.Forms.Label lblCost;
        private System.Windows.Forms.Label lblAddItem;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.Label lblItemList;
        private System.Windows.Forms.ListBox lstItemsSold;
        private System.Windows.Forms.TextBox txtCustomCost;
        private System.Windows.Forms.ComboBox cmbItemsToAdd;
    }
}