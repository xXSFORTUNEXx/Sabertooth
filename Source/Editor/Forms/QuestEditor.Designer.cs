namespace Editor.Forms
{
    partial class QuestEditor
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
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCompleteMessage = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtInprogressMessage = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtStartMessage = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlReq = new System.Windows.Forms.GroupBox();
            this.scrlLevelRequired = new System.Windows.Forms.HScrollBar();
            this.lblLevelReq = new System.Windows.Forms.Label();
            this.scrlPreQuest = new System.Windows.Forms.HScrollBar();
            this.lblPreReq = new System.Windows.Forms.Label();
            this.pnlReqItems = new System.Windows.Forms.GroupBox();
            this.scrlItemValue = new System.Windows.Forms.HScrollBar();
            this.scrlItemNum = new System.Windows.Forms.HScrollBar();
            this.lblItemValue = new System.Windows.Forms.Label();
            this.lblItemNum = new System.Windows.Forms.Label();
            this.scrlItem = new System.Windows.Forms.HScrollBar();
            this.lblItem = new System.Windows.Forms.Label();
            this.pnlReqNpcs = new System.Windows.Forms.GroupBox();
            this.scrlNpcValue = new System.Windows.Forms.HScrollBar();
            this.scrlNpcNum = new System.Windows.Forms.HScrollBar();
            this.lblNpcValue = new System.Windows.Forms.Label();
            this.lblNpcNum = new System.Windows.Forms.Label();
            this.scrlNpc = new System.Windows.Forms.HScrollBar();
            this.lblNpc = new System.Windows.Forms.Label();
            this.pnlRewards = new System.Windows.Forms.GroupBox();
            this.scrlMoney = new System.Windows.Forms.HScrollBar();
            this.lblMoney = new System.Windows.Forms.Label();
            this.scrlExperience = new System.Windows.Forms.HScrollBar();
            this.lblExperience = new System.Windows.Forms.Label();
            this.scrlRewardValue = new System.Windows.Forms.HScrollBar();
            this.scrlRewardNum = new System.Windows.Forms.HScrollBar();
            this.lblRewardValue = new System.Windows.Forms.Label();
            this.lblRewardNum = new System.Windows.Forms.Label();
            this.scrlReward = new System.Windows.Forms.HScrollBar();
            this.lblReward = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.pnlProperties.SuspendLayout();
            this.pnlReq.SuspendLayout();
            this.pnlReqItems.SuspendLayout();
            this.pnlReqNpcs.SuspendLayout();
            this.pnlRewards.SuspendLayout();
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
            // pnlProperties
            // 
            this.pnlProperties.Controls.Add(this.cmbType);
            this.pnlProperties.Controls.Add(this.label6);
            this.pnlProperties.Controls.Add(this.txtCompleteMessage);
            this.pnlProperties.Controls.Add(this.label4);
            this.pnlProperties.Controls.Add(this.txtInprogressMessage);
            this.pnlProperties.Controls.Add(this.label3);
            this.pnlProperties.Controls.Add(this.txtStartMessage);
            this.pnlProperties.Controls.Add(this.label2);
            this.pnlProperties.Controls.Add(this.txtName);
            this.pnlProperties.Controls.Add(this.label1);
            this.pnlProperties.Location = new System.Drawing.Point(160, 13);
            this.pnlProperties.Name = "pnlProperties";
            this.pnlProperties.Size = new System.Drawing.Size(253, 491);
            this.pnlProperties.TabIndex = 10;
            this.pnlProperties.TabStop = false;
            this.pnlProperties.Text = "Properties";
            this.pnlProperties.Visible = false;
            // 
            // cmbType
            // 
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            "None",
            "Talk to NPC",
            "Kill NPC",
            "Get Item For NPC"});
            this.cmbType.Location = new System.Drawing.Point(23, 454);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(209, 21);
            this.cmbType.TabIndex = 17;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 437);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Quest Type:";
            // 
            // txtCompleteMessage
            // 
            this.txtCompleteMessage.Location = new System.Drawing.Point(23, 334);
            this.txtCompleteMessage.Name = "txtCompleteMessage";
            this.txtCompleteMessage.Size = new System.Drawing.Size(209, 96);
            this.txtCompleteMessage.TabIndex = 7;
            this.txtCompleteMessage.Text = "";
            this.txtCompleteMessage.TextChanged += new System.EventHandler(this.txtCompleteMessage_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 317);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Complete Message:";
            // 
            // txtInprogressMessage
            // 
            this.txtInprogressMessage.Location = new System.Drawing.Point(23, 214);
            this.txtInprogressMessage.Name = "txtInprogressMessage";
            this.txtInprogressMessage.Size = new System.Drawing.Size(209, 96);
            this.txtInprogressMessage.TabIndex = 5;
            this.txtInprogressMessage.Text = "";
            this.txtInprogressMessage.TextChanged += new System.EventHandler(this.txtInprogressMessage_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 193);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "InProgress Message:";
            // 
            // txtStartMessage
            // 
            this.txtStartMessage.Location = new System.Drawing.Point(23, 94);
            this.txtStartMessage.Name = "txtStartMessage";
            this.txtStartMessage.Size = new System.Drawing.Size(209, 96);
            this.txtStartMessage.TabIndex = 3;
            this.txtStartMessage.Text = "";
            this.txtStartMessage.TextChanged += new System.EventHandler(this.txtStartMessage_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Start Message:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(23, 50);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(209, 20);
            this.txtName.TabIndex = 1;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // pnlReq
            // 
            this.pnlReq.Controls.Add(this.scrlLevelRequired);
            this.pnlReq.Controls.Add(this.lblLevelReq);
            this.pnlReq.Controls.Add(this.scrlPreQuest);
            this.pnlReq.Controls.Add(this.lblPreReq);
            this.pnlReq.Location = new System.Drawing.Point(419, 16);
            this.pnlReq.Name = "pnlReq";
            this.pnlReq.Size = new System.Drawing.Size(185, 132);
            this.pnlReq.TabIndex = 11;
            this.pnlReq.TabStop = false;
            this.pnlReq.Text = "Requirements";
            this.pnlReq.Visible = false;
            // 
            // scrlLevelRequired
            // 
            this.scrlLevelRequired.Location = new System.Drawing.Point(23, 95);
            this.scrlLevelRequired.Name = "scrlLevelRequired";
            this.scrlLevelRequired.Size = new System.Drawing.Size(137, 17);
            this.scrlLevelRequired.TabIndex = 13;
            this.scrlLevelRequired.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlLevelRequired_Scroll);
            // 
            // lblLevelReq
            // 
            this.lblLevelReq.AutoSize = true;
            this.lblLevelReq.Location = new System.Drawing.Point(23, 78);
            this.lblLevelReq.Name = "lblLevelReq";
            this.lblLevelReq.Size = new System.Drawing.Size(91, 13);
            this.lblLevelReq.TabIndex = 12;
            this.lblLevelReq.Text = "Level Required: 1";
            // 
            // scrlPreQuest
            // 
            this.scrlPreQuest.Location = new System.Drawing.Point(23, 52);
            this.scrlPreQuest.Name = "scrlPreQuest";
            this.scrlPreQuest.Size = new System.Drawing.Size(137, 17);
            this.scrlPreQuest.TabIndex = 11;
            this.scrlPreQuest.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlPreQuest_Scroll);
            // 
            // lblPreReq
            // 
            this.lblPreReq.AutoSize = true;
            this.lblPreReq.Location = new System.Drawing.Point(20, 33);
            this.lblPreReq.Name = "lblPreReq";
            this.lblPreReq.Size = new System.Drawing.Size(116, 13);
            this.lblPreReq.TabIndex = 10;
            this.lblPreReq.Text = "Prereq Quest: 0 - None";
            // 
            // pnlReqItems
            // 
            this.pnlReqItems.Controls.Add(this.scrlItemValue);
            this.pnlReqItems.Controls.Add(this.scrlItemNum);
            this.pnlReqItems.Controls.Add(this.lblItemValue);
            this.pnlReqItems.Controls.Add(this.lblItemNum);
            this.pnlReqItems.Controls.Add(this.scrlItem);
            this.pnlReqItems.Controls.Add(this.lblItem);
            this.pnlReqItems.Location = new System.Drawing.Point(419, 154);
            this.pnlReqItems.Name = "pnlReqItems";
            this.pnlReqItems.Size = new System.Drawing.Size(185, 173);
            this.pnlReqItems.TabIndex = 12;
            this.pnlReqItems.TabStop = false;
            this.pnlReqItems.Text = "Required Items";
            this.pnlReqItems.Visible = false;
            // 
            // scrlItemValue
            // 
            this.scrlItemValue.Location = new System.Drawing.Point(20, 141);
            this.scrlItemValue.Name = "scrlItemValue";
            this.scrlItemValue.Size = new System.Drawing.Size(140, 17);
            this.scrlItemValue.TabIndex = 21;
            this.scrlItemValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlItemValue_Scroll);
            // 
            // scrlItemNum
            // 
            this.scrlItemNum.Location = new System.Drawing.Point(20, 91);
            this.scrlItemNum.Name = "scrlItemNum";
            this.scrlItemNum.Size = new System.Drawing.Size(140, 17);
            this.scrlItemNum.TabIndex = 20;
            this.scrlItemNum.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlItemNum_Scroll);
            // 
            // lblItemValue
            // 
            this.lblItemValue.AutoSize = true;
            this.lblItemValue.Location = new System.Drawing.Point(17, 120);
            this.lblItemValue.Name = "lblItemValue";
            this.lblItemValue.Size = new System.Drawing.Size(46, 13);
            this.lblItemValue.TabIndex = 19;
            this.lblItemValue.Text = "Value: 1";
            // 
            // lblItemNum
            // 
            this.lblItemNum.AutoSize = true;
            this.lblItemNum.Location = new System.Drawing.Point(17, 68);
            this.lblItemNum.Name = "lblItemNum";
            this.lblItemNum.Size = new System.Drawing.Size(96, 13);
            this.lblItemNum.TabIndex = 18;
            this.lblItemNum.Text = "Item Num: 0 -None";
            // 
            // scrlItem
            // 
            this.scrlItem.LargeChange = 1;
            this.scrlItem.Location = new System.Drawing.Point(20, 42);
            this.scrlItem.Minimum = 1;
            this.scrlItem.Name = "scrlItem";
            this.scrlItem.Size = new System.Drawing.Size(140, 17);
            this.scrlItem.TabIndex = 17;
            this.scrlItem.Value = 1;
            this.scrlItem.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlItem_Scroll);
            // 
            // lblItem
            // 
            this.lblItem.AutoSize = true;
            this.lblItem.Location = new System.Drawing.Point(17, 26);
            this.lblItem.Name = "lblItem";
            this.lblItem.Size = new System.Drawing.Size(39, 13);
            this.lblItem.TabIndex = 16;
            this.lblItem.Text = "Item: 1";
            // 
            // pnlReqNpcs
            // 
            this.pnlReqNpcs.Controls.Add(this.scrlNpcValue);
            this.pnlReqNpcs.Controls.Add(this.scrlNpcNum);
            this.pnlReqNpcs.Controls.Add(this.lblNpcValue);
            this.pnlReqNpcs.Controls.Add(this.lblNpcNum);
            this.pnlReqNpcs.Controls.Add(this.scrlNpc);
            this.pnlReqNpcs.Controls.Add(this.lblNpc);
            this.pnlReqNpcs.Location = new System.Drawing.Point(419, 331);
            this.pnlReqNpcs.Name = "pnlReqNpcs";
            this.pnlReqNpcs.Size = new System.Drawing.Size(185, 173);
            this.pnlReqNpcs.TabIndex = 13;
            this.pnlReqNpcs.TabStop = false;
            this.pnlReqNpcs.Text = "Required Npcs";
            this.pnlReqNpcs.Visible = false;
            // 
            // scrlNpcValue
            // 
            this.scrlNpcValue.Location = new System.Drawing.Point(20, 141);
            this.scrlNpcValue.Name = "scrlNpcValue";
            this.scrlNpcValue.Size = new System.Drawing.Size(140, 17);
            this.scrlNpcValue.TabIndex = 21;
            this.scrlNpcValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlNpcValue_Scroll);
            // 
            // scrlNpcNum
            // 
            this.scrlNpcNum.Location = new System.Drawing.Point(20, 91);
            this.scrlNpcNum.Name = "scrlNpcNum";
            this.scrlNpcNum.Size = new System.Drawing.Size(140, 17);
            this.scrlNpcNum.TabIndex = 20;
            this.scrlNpcNum.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlNpcNum_Scroll);
            // 
            // lblNpcValue
            // 
            this.lblNpcValue.AutoSize = true;
            this.lblNpcValue.Location = new System.Drawing.Point(17, 120);
            this.lblNpcValue.Name = "lblNpcValue";
            this.lblNpcValue.Size = new System.Drawing.Size(46, 13);
            this.lblNpcValue.TabIndex = 19;
            this.lblNpcValue.Text = "Value: 1";
            // 
            // lblNpcNum
            // 
            this.lblNpcNum.AutoSize = true;
            this.lblNpcNum.Location = new System.Drawing.Point(17, 68);
            this.lblNpcNum.Name = "lblNpcNum";
            this.lblNpcNum.Size = new System.Drawing.Size(99, 13);
            this.lblNpcNum.TabIndex = 18;
            this.lblNpcNum.Text = "Npc Num: 0 - None";
            // 
            // scrlNpc
            // 
            this.scrlNpc.LargeChange = 1;
            this.scrlNpc.Location = new System.Drawing.Point(20, 42);
            this.scrlNpc.Minimum = 1;
            this.scrlNpc.Name = "scrlNpc";
            this.scrlNpc.Size = new System.Drawing.Size(140, 17);
            this.scrlNpc.TabIndex = 17;
            this.scrlNpc.Value = 1;
            this.scrlNpc.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlNpc_Scroll);
            // 
            // lblNpc
            // 
            this.lblNpc.AutoSize = true;
            this.lblNpc.Location = new System.Drawing.Point(17, 26);
            this.lblNpc.Name = "lblNpc";
            this.lblNpc.Size = new System.Drawing.Size(39, 13);
            this.lblNpc.TabIndex = 16;
            this.lblNpc.Text = "Npc: 1";
            // 
            // pnlRewards
            // 
            this.pnlRewards.Controls.Add(this.scrlMoney);
            this.pnlRewards.Controls.Add(this.lblMoney);
            this.pnlRewards.Controls.Add(this.scrlExperience);
            this.pnlRewards.Controls.Add(this.lblExperience);
            this.pnlRewards.Controls.Add(this.scrlRewardValue);
            this.pnlRewards.Controls.Add(this.scrlRewardNum);
            this.pnlRewards.Controls.Add(this.lblRewardValue);
            this.pnlRewards.Controls.Add(this.lblRewardNum);
            this.pnlRewards.Controls.Add(this.scrlReward);
            this.pnlRewards.Controls.Add(this.lblReward);
            this.pnlRewards.Location = new System.Drawing.Point(610, 16);
            this.pnlRewards.Name = "pnlRewards";
            this.pnlRewards.Size = new System.Drawing.Size(185, 311);
            this.pnlRewards.TabIndex = 14;
            this.pnlRewards.TabStop = false;
            this.pnlRewards.Text = "Rewards";
            this.pnlRewards.Visible = false;
            // 
            // scrlMoney
            // 
            this.scrlMoney.Location = new System.Drawing.Point(20, 253);
            this.scrlMoney.Maximum = 100000;
            this.scrlMoney.Name = "scrlMoney";
            this.scrlMoney.Size = new System.Drawing.Size(140, 17);
            this.scrlMoney.TabIndex = 25;
            this.scrlMoney.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlMoney_Scroll);
            // 
            // lblMoney
            // 
            this.lblMoney.AutoSize = true;
            this.lblMoney.Location = new System.Drawing.Point(20, 229);
            this.lblMoney.Name = "lblMoney";
            this.lblMoney.Size = new System.Drawing.Size(51, 13);
            this.lblMoney.TabIndex = 24;
            this.lblMoney.Text = "Money: 0";
            // 
            // scrlExperience
            // 
            this.scrlExperience.LargeChange = 100;
            this.scrlExperience.Location = new System.Drawing.Point(20, 196);
            this.scrlExperience.Maximum = 100000;
            this.scrlExperience.Name = "scrlExperience";
            this.scrlExperience.Size = new System.Drawing.Size(140, 17);
            this.scrlExperience.TabIndex = 23;
            this.scrlExperience.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlExperience_Scroll);
            // 
            // lblExperience
            // 
            this.lblExperience.AutoSize = true;
            this.lblExperience.Location = new System.Drawing.Point(17, 174);
            this.lblExperience.Name = "lblExperience";
            this.lblExperience.Size = new System.Drawing.Size(72, 13);
            this.lblExperience.TabIndex = 22;
            this.lblExperience.Text = "Experience: 0";
            // 
            // scrlRewardValue
            // 
            this.scrlRewardValue.Location = new System.Drawing.Point(20, 141);
            this.scrlRewardValue.Name = "scrlRewardValue";
            this.scrlRewardValue.Size = new System.Drawing.Size(140, 17);
            this.scrlRewardValue.TabIndex = 21;
            this.scrlRewardValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlRewardValue_Scroll);
            // 
            // scrlRewardNum
            // 
            this.scrlRewardNum.Location = new System.Drawing.Point(20, 91);
            this.scrlRewardNum.Name = "scrlRewardNum";
            this.scrlRewardNum.Size = new System.Drawing.Size(140, 17);
            this.scrlRewardNum.TabIndex = 20;
            this.scrlRewardNum.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlRewardNum_Scroll);
            // 
            // lblRewardValue
            // 
            this.lblRewardValue.AutoSize = true;
            this.lblRewardValue.Location = new System.Drawing.Point(17, 120);
            this.lblRewardValue.Name = "lblRewardValue";
            this.lblRewardValue.Size = new System.Drawing.Size(46, 13);
            this.lblRewardValue.TabIndex = 19;
            this.lblRewardValue.Text = "Value: 1";
            // 
            // lblRewardNum
            // 
            this.lblRewardNum.AutoSize = true;
            this.lblRewardNum.Location = new System.Drawing.Point(17, 68);
            this.lblRewardNum.Name = "lblRewardNum";
            this.lblRewardNum.Size = new System.Drawing.Size(116, 13);
            this.lblRewardNum.TabIndex = 18;
            this.lblRewardNum.Text = "Reward Num: 0 - None";
            // 
            // scrlReward
            // 
            this.scrlReward.LargeChange = 1;
            this.scrlReward.Location = new System.Drawing.Point(20, 42);
            this.scrlReward.Minimum = 1;
            this.scrlReward.Name = "scrlReward";
            this.scrlReward.Size = new System.Drawing.Size(140, 17);
            this.scrlReward.TabIndex = 17;
            this.scrlReward.Value = 1;
            this.scrlReward.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlReward_Scroll);
            // 
            // lblReward
            // 
            this.lblReward.AutoSize = true;
            this.lblReward.Location = new System.Drawing.Point(17, 26);
            this.lblReward.Name = "lblReward";
            this.lblReward.Size = new System.Drawing.Size(56, 13);
            this.lblReward.TabIndex = 16;
            this.lblReward.Text = "Reward: 1";
            // 
            // QuestEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 519);
            this.Controls.Add(this.pnlRewards);
            this.Controls.Add(this.pnlReqNpcs);
            this.Controls.Add(this.pnlReqItems);
            this.Controls.Add(this.pnlReq);
            this.Controls.Add(this.pnlProperties);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "QuestEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quest Editor";
            this.groupBox1.ResumeLayout(false);
            this.pnlProperties.ResumeLayout(false);
            this.pnlProperties.PerformLayout();
            this.pnlReq.ResumeLayout(false);
            this.pnlReq.PerformLayout();
            this.pnlReqItems.ResumeLayout(false);
            this.pnlReqItems.PerformLayout();
            this.pnlReqNpcs.ResumeLayout(false);
            this.pnlReqNpcs.PerformLayout();
            this.pnlRewards.ResumeLayout(false);
            this.pnlRewards.PerformLayout();
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
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox txtCompleteMessage;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox txtInprogressMessage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox txtStartMessage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox pnlReq;
        private System.Windows.Forms.HScrollBar scrlPreQuest;
        private System.Windows.Forms.Label lblPreReq;
        private System.Windows.Forms.HScrollBar scrlLevelRequired;
        private System.Windows.Forms.Label lblLevelReq;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox pnlReqItems;
        private System.Windows.Forms.HScrollBar scrlItemValue;
        private System.Windows.Forms.HScrollBar scrlItemNum;
        private System.Windows.Forms.Label lblItemValue;
        private System.Windows.Forms.Label lblItemNum;
        private System.Windows.Forms.HScrollBar scrlItem;
        private System.Windows.Forms.Label lblItem;
        private System.Windows.Forms.GroupBox pnlReqNpcs;
        private System.Windows.Forms.HScrollBar scrlNpcValue;
        private System.Windows.Forms.HScrollBar scrlNpcNum;
        private System.Windows.Forms.Label lblNpcValue;
        private System.Windows.Forms.Label lblNpcNum;
        private System.Windows.Forms.HScrollBar scrlNpc;
        private System.Windows.Forms.Label lblNpc;
        private System.Windows.Forms.GroupBox pnlRewards;
        private System.Windows.Forms.HScrollBar scrlExperience;
        private System.Windows.Forms.Label lblExperience;
        private System.Windows.Forms.HScrollBar scrlRewardValue;
        private System.Windows.Forms.HScrollBar scrlRewardNum;
        private System.Windows.Forms.Label lblRewardValue;
        private System.Windows.Forms.Label lblRewardNum;
        private System.Windows.Forms.HScrollBar scrlReward;
        private System.Windows.Forms.Label lblReward;
        private System.Windows.Forms.HScrollBar scrlMoney;
        private System.Windows.Forms.Label lblMoney;
    }
}