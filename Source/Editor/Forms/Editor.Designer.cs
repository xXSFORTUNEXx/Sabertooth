namespace Editor
{
    partial class Editor
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
            this.pnlLock = new System.Windows.Forms.Panel();
            this.lblIncorrect = new System.Windows.Forms.Label();
            this.txtUnlock = new System.Windows.Forms.TextBox();
            this.btnUnlock = new System.Windows.Forms.Button();
            this.btnMapEditor = new System.Windows.Forms.Button();
            this.btnNpcEditor = new System.Windows.Forms.Button();
            this.btnItemEditor = new System.Windows.Forms.Button();
            this.btnProjectileEditor = new System.Windows.Forms.Button();
            this.btnChestEditor = new System.Windows.Forms.Button();
            this.btnEditor = new System.Windows.Forms.Button();
            this.btnShopEditor = new System.Windows.Forms.Button();
            this.btnPlayerEditor = new System.Windows.Forms.Button();
            this.btnQuestEditor = new System.Windows.Forms.Button();
            this.dtViewGrid = new System.Windows.Forms.DataGridView();
            this.lstViews = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlLock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtViewGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlLock
            // 
            this.pnlLock.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pnlLock.Controls.Add(this.lblIncorrect);
            this.pnlLock.Controls.Add(this.txtUnlock);
            this.pnlLock.Controls.Add(this.btnUnlock);
            this.pnlLock.Location = new System.Drawing.Point(11, 643);
            this.pnlLock.Name = "pnlLock";
            this.pnlLock.Size = new System.Drawing.Size(119, 91);
            this.pnlLock.TabIndex = 0;
            this.pnlLock.Visible = false;
            // 
            // lblIncorrect
            // 
            this.lblIncorrect.AutoSize = true;
            this.lblIncorrect.ForeColor = System.Drawing.Color.Red;
            this.lblIncorrect.Location = new System.Drawing.Point(12, 158);
            this.lblIncorrect.Name = "lblIncorrect";
            this.lblIncorrect.Size = new System.Drawing.Size(97, 13);
            this.lblIncorrect.TabIndex = 2;
            this.lblIncorrect.Text = "Password incorrect";
            this.lblIncorrect.Visible = false;
            // 
            // txtUnlock
            // 
            this.txtUnlock.Location = new System.Drawing.Point(10, 89);
            this.txtUnlock.Name = "txtUnlock";
            this.txtUnlock.PasswordChar = '*';
            this.txtUnlock.Size = new System.Drawing.Size(99, 20);
            this.txtUnlock.TabIndex = 1;
            // 
            // btnUnlock
            // 
            this.btnUnlock.Location = new System.Drawing.Point(23, 125);
            this.btnUnlock.Name = "btnUnlock";
            this.btnUnlock.Size = new System.Drawing.Size(75, 23);
            this.btnUnlock.TabIndex = 0;
            this.btnUnlock.Text = "Unlock";
            this.btnUnlock.UseVisualStyleBackColor = true;
            this.btnUnlock.Click += new System.EventHandler(this.btnUnlock_Click);
            // 
            // btnMapEditor
            // 
            this.btnMapEditor.Location = new System.Drawing.Point(12, 12);
            this.btnMapEditor.Name = "btnMapEditor";
            this.btnMapEditor.Size = new System.Drawing.Size(119, 23);
            this.btnMapEditor.TabIndex = 1;
            this.btnMapEditor.Text = "Map Editor";
            this.btnMapEditor.UseVisualStyleBackColor = true;
            this.btnMapEditor.Click += new System.EventHandler(this.btnMapEditor_Click);
            // 
            // btnNpcEditor
            // 
            this.btnNpcEditor.Location = new System.Drawing.Point(12, 101);
            this.btnNpcEditor.Name = "btnNpcEditor";
            this.btnNpcEditor.Size = new System.Drawing.Size(119, 23);
            this.btnNpcEditor.TabIndex = 2;
            this.btnNpcEditor.Text = "NPC Editor";
            this.btnNpcEditor.UseVisualStyleBackColor = true;
            this.btnNpcEditor.Click += new System.EventHandler(this.btnNpcEditor_Click);
            // 
            // btnItemEditor
            // 
            this.btnItemEditor.Location = new System.Drawing.Point(12, 72);
            this.btnItemEditor.Name = "btnItemEditor";
            this.btnItemEditor.Size = new System.Drawing.Size(119, 23);
            this.btnItemEditor.TabIndex = 3;
            this.btnItemEditor.Text = "Item Editor";
            this.btnItemEditor.UseVisualStyleBackColor = true;
            this.btnItemEditor.Click += new System.EventHandler(this.btnItemEditor_Click);
            // 
            // btnProjectileEditor
            // 
            this.btnProjectileEditor.Location = new System.Drawing.Point(12, 218);
            this.btnProjectileEditor.Name = "btnProjectileEditor";
            this.btnProjectileEditor.Size = new System.Drawing.Size(118, 23);
            this.btnProjectileEditor.TabIndex = 4;
            this.btnProjectileEditor.Text = "Projectile Editor";
            this.btnProjectileEditor.UseVisualStyleBackColor = true;
            this.btnProjectileEditor.Click += new System.EventHandler(this.btnProjectileEditor_Click);
            // 
            // btnChestEditor
            // 
            this.btnChestEditor.Location = new System.Drawing.Point(12, 41);
            this.btnChestEditor.Name = "btnChestEditor";
            this.btnChestEditor.Size = new System.Drawing.Size(119, 23);
            this.btnChestEditor.TabIndex = 5;
            this.btnChestEditor.Text = "Chest Editor";
            this.btnChestEditor.UseVisualStyleBackColor = true;
            this.btnChestEditor.Click += new System.EventHandler(this.btnChestEditor_Click);
            // 
            // btnEditor
            // 
            this.btnEditor.Location = new System.Drawing.Point(12, 160);
            this.btnEditor.Name = "btnEditor";
            this.btnEditor.Size = new System.Drawing.Size(119, 23);
            this.btnEditor.TabIndex = 7;
            this.btnEditor.Text = "Chat Editor";
            this.btnEditor.UseVisualStyleBackColor = true;
            this.btnEditor.Click += new System.EventHandler(this.btnEditor_Click);
            // 
            // btnShopEditor
            // 
            this.btnShopEditor.Location = new System.Drawing.Point(12, 130);
            this.btnShopEditor.Name = "btnShopEditor";
            this.btnShopEditor.Size = new System.Drawing.Size(119, 23);
            this.btnShopEditor.TabIndex = 8;
            this.btnShopEditor.Text = "Shop Editor";
            this.btnShopEditor.UseVisualStyleBackColor = true;
            this.btnShopEditor.Click += new System.EventHandler(this.btnShopEditor_Click);
            // 
            // btnPlayerEditor
            // 
            this.btnPlayerEditor.Location = new System.Drawing.Point(12, 247);
            this.btnPlayerEditor.Name = "btnPlayerEditor";
            this.btnPlayerEditor.Size = new System.Drawing.Size(118, 23);
            this.btnPlayerEditor.TabIndex = 9;
            this.btnPlayerEditor.Text = "Player Editor";
            this.btnPlayerEditor.UseVisualStyleBackColor = true;
            this.btnPlayerEditor.Click += new System.EventHandler(this.btnPlayerEditor_Click);
            // 
            // btnQuestEditor
            // 
            this.btnQuestEditor.Location = new System.Drawing.Point(12, 189);
            this.btnQuestEditor.Name = "btnQuestEditor";
            this.btnQuestEditor.Size = new System.Drawing.Size(118, 23);
            this.btnQuestEditor.TabIndex = 10;
            this.btnQuestEditor.Text = "Quest Editor";
            this.btnQuestEditor.UseVisualStyleBackColor = true;
            this.btnQuestEditor.Click += new System.EventHandler(this.btnQuestEditor_Click);
            // 
            // dtViewGrid
            // 
            this.dtViewGrid.AllowUserToAddRows = false;
            this.dtViewGrid.AllowUserToDeleteRows = false;
            this.dtViewGrid.AllowUserToOrderColumns = true;
            this.dtViewGrid.Location = new System.Drawing.Point(138, 12);
            this.dtViewGrid.Name = "dtViewGrid";
            this.dtViewGrid.ReadOnly = true;
            this.dtViewGrid.Size = new System.Drawing.Size(1347, 779);
            this.dtViewGrid.TabIndex = 11;
            // 
            // lstViews
            // 
            this.lstViews.FormattingEnabled = true;
            this.lstViews.Location = new System.Drawing.Point(12, 301);
            this.lstViews.Name = "lstViews";
            this.lstViews.Size = new System.Drawing.Size(118, 264);
            this.lstViews.TabIndex = 12;
            this.lstViews.SelectedIndexChanged += new System.EventHandler(this.lstViews_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 282);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Views:";
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1497, 806);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstViews);
            this.Controls.Add(this.dtViewGrid);
            this.Controls.Add(this.btnQuestEditor);
            this.Controls.Add(this.btnPlayerEditor);
            this.Controls.Add(this.btnShopEditor);
            this.Controls.Add(this.btnEditor);
            this.Controls.Add(this.btnChestEditor);
            this.Controls.Add(this.btnProjectileEditor);
            this.Controls.Add(this.btnItemEditor);
            this.Controls.Add(this.btnNpcEditor);
            this.Controls.Add(this.btnMapEditor);
            this.Controls.Add(this.pnlLock);
            this.Name = "Editor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Editor";
            this.pnlLock.ResumeLayout(false);
            this.pnlLock.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtViewGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlLock;
        private System.Windows.Forms.TextBox txtUnlock;
        private System.Windows.Forms.Button btnUnlock;
        private System.Windows.Forms.Label lblIncorrect;
        private System.Windows.Forms.Button btnMapEditor;
        private System.Windows.Forms.Button btnNpcEditor;
        private System.Windows.Forms.Button btnItemEditor;
        private System.Windows.Forms.Button btnProjectileEditor;
        private System.Windows.Forms.Button btnChestEditor;
        private System.Windows.Forms.Button btnEditor;
        private System.Windows.Forms.Button btnShopEditor;
        private System.Windows.Forms.Button btnPlayerEditor;
        private System.Windows.Forms.Button btnQuestEditor;
        private System.Windows.Forms.DataGridView dtViewGrid;
        private System.Windows.Forms.ListBox lstViews;
        private System.Windows.Forms.Label label1;
    }
}

