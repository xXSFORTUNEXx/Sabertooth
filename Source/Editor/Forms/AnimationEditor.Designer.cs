namespace Editor.Forms
{
    partial class AnimationEditor
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnNewItem = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lstIndex = new System.Windows.Forms.ListBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.scrlSprite = new System.Windows.Forms.HScrollBar();
            this.lblSprite = new System.Windows.Forms.Label();
            this.picSprite = new System.Windows.Forms.PictureBox();
            this.btnViewFull = new System.Windows.Forms.Button();
            this.lblFCH = new System.Windows.Forms.Label();
            this.scrlFCH = new System.Windows.Forms.HScrollBar();
            this.lblFCV = new System.Windows.Forms.Label();
            this.scrlFCV = new System.Windows.Forms.HScrollBar();
            this.lblFC = new System.Windows.Forms.Label();
            this.scrlFC = new System.Windows.Forms.HScrollBar();
            this.lblFD = new System.Windows.Forms.Label();
            this.scrlFD = new System.Windows.Forms.HScrollBar();
            this.lblLPC = new System.Windows.Forms.Label();
            this.scrlLPC = new System.Windows.Forms.HScrollBar();
            this.pnlMain = new System.Windows.Forms.GroupBox();
            this.chkLoop = new System.Windows.Forms.CheckBox();
            this.lblAnimSize = new System.Windows.Forms.Label();
            this.btnPreview = new System.Windows.Forms.Button();
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.chkRBT = new System.Windows.Forms.CheckBox();
            this.tmrAnimation = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSprite)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
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
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(18, 32);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 10;
            this.lblName.Text = "Name:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(21, 49);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(156, 20);
            this.txtName.TabIndex = 11;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // scrlSprite
            // 
            this.scrlSprite.LargeChange = 1;
            this.scrlSprite.Location = new System.Drawing.Point(193, 179);
            this.scrlSprite.Maximum = 4;
            this.scrlSprite.Minimum = 1;
            this.scrlSprite.Name = "scrlSprite";
            this.scrlSprite.Size = new System.Drawing.Size(128, 17);
            this.scrlSprite.TabIndex = 12;
            this.scrlSprite.Value = 1;
            this.scrlSprite.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlSprite_Scroll);
            // 
            // lblSprite
            // 
            this.lblSprite.AutoSize = true;
            this.lblSprite.Location = new System.Drawing.Point(190, 162);
            this.lblSprite.Name = "lblSprite";
            this.lblSprite.Size = new System.Drawing.Size(86, 13);
            this.lblSprite.TabIndex = 13;
            this.lblSprite.Text = "Sprite Number: 0";
            // 
            // picSprite
            // 
            this.picSprite.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.picSprite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picSprite.Location = new System.Drawing.Point(193, 32);
            this.picSprite.Name = "picSprite";
            this.picSprite.Size = new System.Drawing.Size(567, 128);
            this.picSprite.TabIndex = 14;
            this.picSprite.TabStop = false;
            // 
            // btnViewFull
            // 
            this.btnViewFull.Location = new System.Drawing.Point(193, 203);
            this.btnViewFull.Name = "btnViewFull";
            this.btnViewFull.Size = new System.Drawing.Size(75, 23);
            this.btnViewFull.TabIndex = 15;
            this.btnViewFull.Text = "View Full Image";
            this.btnViewFull.UseVisualStyleBackColor = true;
            this.btnViewFull.Click += new System.EventHandler(this.btnViewFull_Click);
            // 
            // lblFCH
            // 
            this.lblFCH.AutoSize = true;
            this.lblFCH.Location = new System.Drawing.Point(18, 75);
            this.lblFCH.Name = "lblFCH";
            this.lblFCH.Size = new System.Drawing.Size(129, 13);
            this.lblFCH.TabIndex = 16;
            this.lblFCH.Text = "Frame Count Horizontal: 1";
            // 
            // scrlFCH
            // 
            this.scrlFCH.LargeChange = 1;
            this.scrlFCH.Location = new System.Drawing.Point(21, 92);
            this.scrlFCH.Maximum = 30;
            this.scrlFCH.Minimum = 1;
            this.scrlFCH.Name = "scrlFCH";
            this.scrlFCH.Size = new System.Drawing.Size(153, 17);
            this.scrlFCH.TabIndex = 17;
            this.scrlFCH.Value = 1;
            this.scrlFCH.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlFCH_Scroll);
            // 
            // lblFCV
            // 
            this.lblFCV.AutoSize = true;
            this.lblFCV.Location = new System.Drawing.Point(18, 119);
            this.lblFCV.Name = "lblFCV";
            this.lblFCV.Size = new System.Drawing.Size(117, 13);
            this.lblFCV.TabIndex = 18;
            this.lblFCV.Text = "Frame Count Vertical: 1";
            // 
            // scrlFCV
            // 
            this.scrlFCV.LargeChange = 1;
            this.scrlFCV.Location = new System.Drawing.Point(21, 136);
            this.scrlFCV.Maximum = 30;
            this.scrlFCV.Minimum = 1;
            this.scrlFCV.Name = "scrlFCV";
            this.scrlFCV.Size = new System.Drawing.Size(153, 17);
            this.scrlFCV.TabIndex = 19;
            this.scrlFCV.Value = 1;
            this.scrlFCV.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlFCV_Scroll);
            // 
            // lblFC
            // 
            this.lblFC.AutoSize = true;
            this.lblFC.Location = new System.Drawing.Point(18, 161);
            this.lblFC.Name = "lblFC";
            this.lblFC.Size = new System.Drawing.Size(70, 13);
            this.lblFC.TabIndex = 20;
            this.lblFC.Text = "Frame Count:";
            // 
            // scrlFC
            // 
            this.scrlFC.Location = new System.Drawing.Point(21, 178);
            this.scrlFC.Minimum = 1;
            this.scrlFC.Name = "scrlFC";
            this.scrlFC.Size = new System.Drawing.Size(153, 17);
            this.scrlFC.TabIndex = 21;
            this.scrlFC.Value = 1;
            this.scrlFC.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlFC_Scroll);
            // 
            // lblFD
            // 
            this.lblFD.AutoSize = true;
            this.lblFD.Location = new System.Drawing.Point(18, 203);
            this.lblFD.Name = "lblFD";
            this.lblFD.Size = new System.Drawing.Size(116, 13);
            this.lblFD.TabIndex = 22;
            this.lblFD.Text = "Frame Duration: 100ms";
            // 
            // scrlFD
            // 
            this.scrlFD.LargeChange = 1000;
            this.scrlFD.Location = new System.Drawing.Point(21, 220);
            this.scrlFD.Maximum = 5000;
            this.scrlFD.Minimum = 25;
            this.scrlFD.Name = "scrlFD";
            this.scrlFD.Size = new System.Drawing.Size(153, 17);
            this.scrlFD.SmallChange = 25;
            this.scrlFD.TabIndex = 23;
            this.scrlFD.Value = 100;
            this.scrlFD.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlFD_Scroll);
            // 
            // lblLPC
            // 
            this.lblLPC.AutoSize = true;
            this.lblLPC.Location = new System.Drawing.Point(18, 244);
            this.lblLPC.Name = "lblLPC";
            this.lblLPC.Size = new System.Drawing.Size(74, 13);
            this.lblLPC.TabIndex = 24;
            this.lblLPC.Text = "Loop Count: 0";
            // 
            // scrlLPC
            // 
            this.scrlLPC.Location = new System.Drawing.Point(21, 261);
            this.scrlLPC.Name = "scrlLPC";
            this.scrlLPC.Size = new System.Drawing.Size(153, 17);
            this.scrlLPC.TabIndex = 25;
            this.scrlLPC.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlLPC_Scroll);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.chkLoop);
            this.pnlMain.Controls.Add(this.lblAnimSize);
            this.pnlMain.Controls.Add(this.btnPreview);
            this.pnlMain.Controls.Add(this.picPreview);
            this.pnlMain.Controls.Add(this.chkRBT);
            this.pnlMain.Controls.Add(this.scrlLPC);
            this.pnlMain.Controls.Add(this.lblLPC);
            this.pnlMain.Controls.Add(this.scrlFD);
            this.pnlMain.Controls.Add(this.lblFD);
            this.pnlMain.Controls.Add(this.scrlFC);
            this.pnlMain.Controls.Add(this.lblFC);
            this.pnlMain.Controls.Add(this.scrlFCV);
            this.pnlMain.Controls.Add(this.lblFCV);
            this.pnlMain.Controls.Add(this.scrlFCH);
            this.pnlMain.Controls.Add(this.lblFCH);
            this.pnlMain.Controls.Add(this.btnViewFull);
            this.pnlMain.Controls.Add(this.picSprite);
            this.pnlMain.Controls.Add(this.lblSprite);
            this.pnlMain.Controls.Add(this.scrlSprite);
            this.pnlMain.Controls.Add(this.txtName);
            this.pnlMain.Controls.Add(this.lblName);
            this.pnlMain.Location = new System.Drawing.Point(159, 12);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(780, 637);
            this.pnlMain.TabIndex = 9;
            this.pnlMain.TabStop = false;
            this.pnlMain.Text = "Properties";
            this.pnlMain.Visible = false;
            // 
            // chkLoop
            // 
            this.chkLoop.AutoSize = true;
            this.chkLoop.Checked = true;
            this.chkLoop.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLoop.Location = new System.Drawing.Point(21, 315);
            this.chkLoop.Name = "chkLoop";
            this.chkLoop.Size = new System.Drawing.Size(91, 17);
            this.chkLoop.TabIndex = 30;
            this.chkLoop.Text = "Loop Preview";
            this.chkLoop.UseVisualStyleBackColor = true;
            // 
            // lblAnimSize
            // 
            this.lblAnimSize.AutoSize = true;
            this.lblAnimSize.Location = new System.Drawing.Point(21, 357);
            this.lblAnimSize.Name = "lblAnimSize";
            this.lblAnimSize.Size = new System.Drawing.Size(99, 26);
            this.lblAnimSize.TabIndex = 29;
            this.lblAnimSize.Text = "Animation Size: 0x0\r\nOriginal Size: 0x0";
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(193, 292);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(75, 23);
            this.btnPreview.TabIndex = 28;
            this.btnPreview.Text = "Preview";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // picPreview
            // 
            this.picPreview.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.picPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPreview.Location = new System.Drawing.Point(193, 321);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(567, 298);
            this.picPreview.TabIndex = 27;
            this.picPreview.TabStop = false;
            // 
            // chkRBT
            // 
            this.chkRBT.AutoSize = true;
            this.chkRBT.Location = new System.Drawing.Point(21, 292);
            this.chkRBT.Name = "chkRBT";
            this.chkRBT.Size = new System.Drawing.Size(127, 17);
            this.chkRBT.TabIndex = 26;
            this.chkRBT.Text = "Render Below Target";
            this.chkRBT.UseVisualStyleBackColor = true;
            this.chkRBT.CheckedChanged += new System.EventHandler(this.chkRBT_CheckedChanged);
            // 
            // tmrAnimation
            // 
            this.tmrAnimation.Tick += new System.EventHandler(this.tmrAnimation_Tick);
            // 
            // AnimationEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 661);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.groupBox1);
            this.Name = "AnimationEditor";
            this.Text = "AnimationEditor";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picSprite)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnNewItem;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ListBox lstIndex;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.HScrollBar scrlSprite;
        private System.Windows.Forms.Label lblSprite;
        private System.Windows.Forms.PictureBox picSprite;
        private System.Windows.Forms.Button btnViewFull;
        private System.Windows.Forms.Label lblFCH;
        private System.Windows.Forms.HScrollBar scrlFCH;
        private System.Windows.Forms.Label lblFCV;
        private System.Windows.Forms.HScrollBar scrlFCV;
        private System.Windows.Forms.Label lblFC;
        private System.Windows.Forms.HScrollBar scrlFC;
        private System.Windows.Forms.Label lblFD;
        private System.Windows.Forms.HScrollBar scrlFD;
        private System.Windows.Forms.Label lblLPC;
        private System.Windows.Forms.HScrollBar scrlLPC;
        private System.Windows.Forms.GroupBox pnlMain;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.CheckBox chkRBT;
        private System.Windows.Forms.Timer tmrAnimation;
        private System.Windows.Forms.Label lblAnimSize;
        private System.Windows.Forms.CheckBox chkLoop;
    }
}