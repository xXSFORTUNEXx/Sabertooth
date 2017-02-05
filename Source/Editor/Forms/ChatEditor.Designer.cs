namespace Editor.Forms
{
    partial class ChatEditor
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMainMessage = new System.Windows.Forms.RichTextBox();
            this.lblShopNum = new System.Windows.Forms.Label();
            this.scrlShopNum = new System.Windows.Forms.HScrollBar();
            this.lblMissionNum = new System.Windows.Forms.Label();
            this.scrlMissionNum = new System.Windows.Forms.HScrollBar();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.lblMoney = new System.Windows.Forms.Label();
            this.scrlMoney = new System.Windows.Forms.HScrollBar();
            this.lblItem = new System.Windows.Forms.Label();
            this.scrlItem = new System.Windows.Forms.HScrollBar();
            this.lblItemNum = new System.Windows.Forms.Label();
            this.scrlItemNum = new System.Windows.Forms.HScrollBar();
            this.lblItemVal = new System.Windows.Forms.Label();
            this.scrlValue = new System.Windows.Forms.HScrollBar();
            this.pnlOptions = new System.Windows.Forms.GroupBox();
            this.scrlNextChatD = new System.Windows.Forms.HScrollBar();
            this.lblNextChatD = new System.Windows.Forms.Label();
            this.scrlNextChatC = new System.Windows.Forms.HScrollBar();
            this.lblNextChatC = new System.Windows.Forms.Label();
            this.scrlNextChatB = new System.Windows.Forms.HScrollBar();
            this.lblNextChatB = new System.Windows.Forms.Label();
            this.txtOptD = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtOptC = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtOptB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.scrlNextChatA = new System.Windows.Forms.HScrollBar();
            this.lblNextChatA = new System.Windows.Forms.Label();
            this.txtOptA = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.pnlOptions.SuspendLayout();
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
            this.groupBox1.TabIndex = 9;
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(19, 35);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(213, 20);
            this.txtName.TabIndex = 1;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Main Message:";
            // 
            // txtMainMessage
            // 
            this.txtMainMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMainMessage.Location = new System.Drawing.Point(19, 74);
            this.txtMainMessage.Name = "txtMainMessage";
            this.txtMainMessage.Size = new System.Drawing.Size(213, 96);
            this.txtMainMessage.TabIndex = 3;
            this.txtMainMessage.Text = "";
            this.txtMainMessage.TextChanged += new System.EventHandler(this.txtMainMessage_TextChanged);
            // 
            // lblShopNum
            // 
            this.lblShopNum.AutoSize = true;
            this.lblShopNum.Location = new System.Drawing.Point(16, 216);
            this.lblShopNum.Name = "lblShopNum";
            this.lblShopNum.Size = new System.Drawing.Size(84, 13);
            this.lblShopNum.TabIndex = 14;
            this.lblShopNum.Text = "Shop Number: 0";
            // 
            // scrlShopNum
            // 
            this.scrlShopNum.Location = new System.Drawing.Point(19, 232);
            this.scrlShopNum.Name = "scrlShopNum";
            this.scrlShopNum.Size = new System.Drawing.Size(213, 17);
            this.scrlShopNum.TabIndex = 15;
            this.scrlShopNum.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlShopNum_Scroll);
            // 
            // lblMissionNum
            // 
            this.lblMissionNum.AutoSize = true;
            this.lblMissionNum.Location = new System.Drawing.Point(16, 251);
            this.lblMissionNum.Name = "lblMissionNum";
            this.lblMissionNum.Size = new System.Drawing.Size(94, 13);
            this.lblMissionNum.TabIndex = 16;
            this.lblMissionNum.Text = "Mission Number: 0";
            // 
            // scrlMissionNum
            // 
            this.scrlMissionNum.Location = new System.Drawing.Point(19, 266);
            this.scrlMissionNum.Name = "scrlMissionNum";
            this.scrlMissionNum.Size = new System.Drawing.Size(213, 17);
            this.scrlMissionNum.TabIndex = 17;
            this.scrlMissionNum.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlMissionNum_Scroll);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 173);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Chat Type:";
            // 
            // cmbType
            // 
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            "None",
            "Open Shop",
            "Open Bank",
            "Reward",
            "Mission"});
            this.cmbType.Location = new System.Drawing.Point(19, 189);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(213, 21);
            this.cmbType.TabIndex = 19;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // lblMoney
            // 
            this.lblMoney.AutoSize = true;
            this.lblMoney.Location = new System.Drawing.Point(16, 288);
            this.lblMoney.Name = "lblMoney";
            this.lblMoney.Size = new System.Drawing.Size(51, 13);
            this.lblMoney.TabIndex = 20;
            this.lblMoney.Text = "Money: 0";
            // 
            // scrlMoney
            // 
            this.scrlMoney.Location = new System.Drawing.Point(19, 301);
            this.scrlMoney.Name = "scrlMoney";
            this.scrlMoney.Size = new System.Drawing.Size(213, 17);
            this.scrlMoney.TabIndex = 21;
            this.scrlMoney.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlMoney_Scroll);
            // 
            // lblItem
            // 
            this.lblItem.AutoSize = true;
            this.lblItem.Location = new System.Drawing.Point(16, 335);
            this.lblItem.Name = "lblItem";
            this.lblItem.Size = new System.Drawing.Size(39, 13);
            this.lblItem.TabIndex = 22;
            this.lblItem.Text = "Item: 1";
            // 
            // scrlItem
            // 
            this.scrlItem.LargeChange = 1;
            this.scrlItem.Location = new System.Drawing.Point(20, 351);
            this.scrlItem.Maximum = 3;
            this.scrlItem.Minimum = 1;
            this.scrlItem.Name = "scrlItem";
            this.scrlItem.Size = new System.Drawing.Size(212, 17);
            this.scrlItem.TabIndex = 23;
            this.scrlItem.Value = 1;
            this.scrlItem.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlItem_Scroll);
            // 
            // lblItemNum
            // 
            this.lblItemNum.AutoSize = true;
            this.lblItemNum.Location = new System.Drawing.Point(16, 374);
            this.lblItemNum.Name = "lblItemNum";
            this.lblItemNum.Size = new System.Drawing.Size(79, 13);
            this.lblItemNum.TabIndex = 24;
            this.lblItemNum.Text = "Item Number: 0";
            // 
            // scrlItemNum
            // 
            this.scrlItemNum.Location = new System.Drawing.Point(19, 390);
            this.scrlItemNum.Name = "scrlItemNum";
            this.scrlItemNum.Size = new System.Drawing.Size(213, 17);
            this.scrlItemNum.TabIndex = 25;
            this.scrlItemNum.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlItemNum_Scroll);
            // 
            // lblItemVal
            // 
            this.lblItemVal.AutoSize = true;
            this.lblItemVal.Location = new System.Drawing.Point(20, 412);
            this.lblItemVal.Name = "lblItemVal";
            this.lblItemVal.Size = new System.Drawing.Size(46, 13);
            this.lblItemVal.TabIndex = 26;
            this.lblItemVal.Text = "Value: 1";
            // 
            // scrlValue
            // 
            this.scrlValue.LargeChange = 1;
            this.scrlValue.Location = new System.Drawing.Point(19, 429);
            this.scrlValue.Minimum = 1;
            this.scrlValue.Name = "scrlValue";
            this.scrlValue.Size = new System.Drawing.Size(213, 17);
            this.scrlValue.TabIndex = 27;
            this.scrlValue.Value = 1;
            this.scrlValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlValue_Scroll);
            // 
            // pnlOptions
            // 
            this.pnlOptions.Controls.Add(this.label8);
            this.pnlOptions.Controls.Add(this.scrlNextChatD);
            this.pnlOptions.Controls.Add(this.lblNextChatD);
            this.pnlOptions.Controls.Add(this.scrlNextChatC);
            this.pnlOptions.Controls.Add(this.lblNextChatC);
            this.pnlOptions.Controls.Add(this.scrlNextChatB);
            this.pnlOptions.Controls.Add(this.lblNextChatB);
            this.pnlOptions.Controls.Add(this.txtOptD);
            this.pnlOptions.Controls.Add(this.label6);
            this.pnlOptions.Controls.Add(this.txtOptC);
            this.pnlOptions.Controls.Add(this.label5);
            this.pnlOptions.Controls.Add(this.txtOptB);
            this.pnlOptions.Controls.Add(this.label4);
            this.pnlOptions.Controls.Add(this.scrlNextChatA);
            this.pnlOptions.Controls.Add(this.lblNextChatA);
            this.pnlOptions.Controls.Add(this.txtOptA);
            this.pnlOptions.Controls.Add(this.label3);
            this.pnlOptions.Location = new System.Drawing.Point(259, 17);
            this.pnlOptions.Name = "pnlOptions";
            this.pnlOptions.Size = new System.Drawing.Size(248, 441);
            this.pnlOptions.TabIndex = 28;
            this.pnlOptions.TabStop = false;
            this.pnlOptions.Text = "Chat Options";
            // 
            // scrlNextChatD
            // 
            this.scrlNextChatD.Location = new System.Drawing.Point(18, 327);
            this.scrlNextChatD.Name = "scrlNextChatD";
            this.scrlNextChatD.Size = new System.Drawing.Size(210, 17);
            this.scrlNextChatD.TabIndex = 27;
            this.scrlNextChatD.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlNextChatD_Scroll);
            // 
            // lblNextChatD
            // 
            this.lblNextChatD.AutoSize = true;
            this.lblNextChatD.Location = new System.Drawing.Point(15, 311);
            this.lblNextChatD.Name = "lblNextChatD";
            this.lblNextChatD.Size = new System.Drawing.Size(66, 13);
            this.lblNextChatD.TabIndex = 26;
            this.lblNextChatD.Text = "Next Chat: 0";
            // 
            // scrlNextChatC
            // 
            this.scrlNextChatC.Location = new System.Drawing.Point(17, 246);
            this.scrlNextChatC.Name = "scrlNextChatC";
            this.scrlNextChatC.Size = new System.Drawing.Size(210, 17);
            this.scrlNextChatC.TabIndex = 25;
            this.scrlNextChatC.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlNextChatC_Scroll);
            // 
            // lblNextChatC
            // 
            this.lblNextChatC.AutoSize = true;
            this.lblNextChatC.Location = new System.Drawing.Point(14, 230);
            this.lblNextChatC.Name = "lblNextChatC";
            this.lblNextChatC.Size = new System.Drawing.Size(66, 13);
            this.lblNextChatC.TabIndex = 24;
            this.lblNextChatC.Text = "Next Chat: 0";
            // 
            // scrlNextChatB
            // 
            this.scrlNextChatB.Location = new System.Drawing.Point(20, 169);
            this.scrlNextChatB.Name = "scrlNextChatB";
            this.scrlNextChatB.Size = new System.Drawing.Size(210, 17);
            this.scrlNextChatB.TabIndex = 23;
            this.scrlNextChatB.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlNextChatB_Scroll);
            // 
            // lblNextChatB
            // 
            this.lblNextChatB.AutoSize = true;
            this.lblNextChatB.Location = new System.Drawing.Point(17, 153);
            this.lblNextChatB.Name = "lblNextChatB";
            this.lblNextChatB.Size = new System.Drawing.Size(66, 13);
            this.lblNextChatB.TabIndex = 22;
            this.lblNextChatB.Text = "Next Chat: 0";
            // 
            // txtOptD
            // 
            this.txtOptD.Location = new System.Drawing.Point(17, 285);
            this.txtOptD.Name = "txtOptD";
            this.txtOptD.Size = new System.Drawing.Size(213, 20);
            this.txtOptD.TabIndex = 21;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 267);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Option D:";
            // 
            // txtOptC
            // 
            this.txtOptC.Location = new System.Drawing.Point(17, 207);
            this.txtOptC.Name = "txtOptC";
            this.txtOptC.Size = new System.Drawing.Size(213, 20);
            this.txtOptC.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 191);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Option C:";
            // 
            // txtOptB
            // 
            this.txtOptB.Location = new System.Drawing.Point(17, 130);
            this.txtOptB.Name = "txtOptB";
            this.txtOptB.Size = new System.Drawing.Size(213, 20);
            this.txtOptB.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Option B:";
            // 
            // scrlNextChatA
            // 
            this.scrlNextChatA.Location = new System.Drawing.Point(20, 86);
            this.scrlNextChatA.Name = "scrlNextChatA";
            this.scrlNextChatA.Size = new System.Drawing.Size(210, 17);
            this.scrlNextChatA.TabIndex = 15;
            this.scrlNextChatA.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlNextChatA_Scroll);
            // 
            // lblNextChatA
            // 
            this.lblNextChatA.AutoSize = true;
            this.lblNextChatA.Location = new System.Drawing.Point(17, 70);
            this.lblNextChatA.Name = "lblNextChatA";
            this.lblNextChatA.Size = new System.Drawing.Size(66, 13);
            this.lblNextChatA.TabIndex = 14;
            this.lblNextChatA.Text = "Next Chat: 0";
            // 
            // txtOptA
            // 
            this.txtOptA.Location = new System.Drawing.Point(17, 47);
            this.txtOptA.Name = "txtOptA";
            this.txtOptA.Size = new System.Drawing.Size(213, 20);
            this.txtOptA.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Option A:";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlOptions);
            this.pnlMain.Controls.Add(this.scrlValue);
            this.pnlMain.Controls.Add(this.lblItemVal);
            this.pnlMain.Controls.Add(this.scrlItemNum);
            this.pnlMain.Controls.Add(this.lblItemNum);
            this.pnlMain.Controls.Add(this.scrlItem);
            this.pnlMain.Controls.Add(this.lblItem);
            this.pnlMain.Controls.Add(this.scrlMoney);
            this.pnlMain.Controls.Add(this.lblMoney);
            this.pnlMain.Controls.Add(this.cmbType);
            this.pnlMain.Controls.Add(this.label7);
            this.pnlMain.Controls.Add(this.scrlMissionNum);
            this.pnlMain.Controls.Add(this.lblMissionNum);
            this.pnlMain.Controls.Add(this.scrlShopNum);
            this.pnlMain.Controls.Add(this.lblShopNum);
            this.pnlMain.Controls.Add(this.txtMainMessage);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.txtName);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Location = new System.Drawing.Point(159, 12);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(526, 466);
            this.pnlMain.TabIndex = 10;
            this.pnlMain.TabStop = false;
            this.pnlMain.Text = "Properties";
            this.pnlMain.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 371);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(228, 52);
            this.label8.TabIndex = 28;
            this.label8.Text = "To assign an option to mission or shop use the \r\nfollowing keywords: \r\n\r\n\"Accept " +
    "Mission\" or \"Shop\"";
            // 
            // ChatEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 490);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.groupBox1);
            this.Name = "ChatEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChatEditor";
            this.groupBox1.ResumeLayout(false);
            this.pnlOptions.ResumeLayout(false);
            this.pnlOptions.PerformLayout();
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox txtMainMessage;
        private System.Windows.Forms.Label lblShopNum;
        private System.Windows.Forms.HScrollBar scrlShopNum;
        private System.Windows.Forms.Label lblMissionNum;
        private System.Windows.Forms.HScrollBar scrlMissionNum;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label lblMoney;
        private System.Windows.Forms.HScrollBar scrlMoney;
        private System.Windows.Forms.Label lblItem;
        private System.Windows.Forms.HScrollBar scrlItem;
        private System.Windows.Forms.Label lblItemNum;
        private System.Windows.Forms.HScrollBar scrlItemNum;
        private System.Windows.Forms.Label lblItemVal;
        private System.Windows.Forms.HScrollBar scrlValue;
        private System.Windows.Forms.GroupBox pnlOptions;
        private System.Windows.Forms.HScrollBar scrlNextChatA;
        private System.Windows.Forms.Label lblNextChatA;
        private System.Windows.Forms.TextBox txtOptA;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox pnlMain;
        private System.Windows.Forms.HScrollBar scrlNextChatD;
        private System.Windows.Forms.Label lblNextChatD;
        private System.Windows.Forms.HScrollBar scrlNextChatC;
        private System.Windows.Forms.Label lblNextChatC;
        private System.Windows.Forms.HScrollBar scrlNextChatB;
        private System.Windows.Forms.Label lblNextChatB;
        private System.Windows.Forms.TextBox txtOptD;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtOptC;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtOptB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
    }
}