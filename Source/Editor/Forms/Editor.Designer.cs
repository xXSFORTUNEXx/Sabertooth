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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dtViewGrid = new System.Windows.Forms.DataGridView();
            this.pnlEditorsViews = new System.Windows.Forms.Panel();
            this.btnCloseWindow = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lstViews = new System.Windows.Forms.ListBox();
            this.mnuFileMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showHideViewWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showHideEditorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chestEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.npcEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shopEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chatEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.questEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playerEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlEditors = new System.Windows.Forms.Panel();
            this.btnCloseEditors = new System.Windows.Forms.Button();
            this.btnQuestEditor = new System.Windows.Forms.Button();
            this.btnPlayerEditor = new System.Windows.Forms.Button();
            this.btnShopEditor = new System.Windows.Forms.Button();
            this.btnEditor = new System.Windows.Forms.Button();
            this.btnChestEditor = new System.Windows.Forms.Button();
            this.btnItemEditor = new System.Windows.Forms.Button();
            this.btnNpcEditor = new System.Windows.Forms.Button();
            this.btnMapEditor = new System.Windows.Forms.Button();
            this.btnIMGSplit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtViewGrid)).BeginInit();
            this.pnlEditorsViews.SuspendLayout();
            this.mnuFileMenu.SuspendLayout();
            this.pnlEditors.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtViewGrid
            // 
            this.dtViewGrid.AllowUserToDeleteRows = false;
            this.dtViewGrid.AllowUserToOrderColumns = true;
            this.dtViewGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtViewGrid.Location = new System.Drawing.Point(0, 24);
            this.dtViewGrid.Name = "dtViewGrid";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtViewGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtViewGrid.Size = new System.Drawing.Size(1154, 685);
            this.dtViewGrid.TabIndex = 11;
            // 
            // pnlEditorsViews
            // 
            this.pnlEditorsViews.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlEditorsViews.Controls.Add(this.btnCloseWindow);
            this.pnlEditorsViews.Controls.Add(this.label1);
            this.pnlEditorsViews.Controls.Add(this.lstViews);
            this.pnlEditorsViews.Location = new System.Drawing.Point(12, 343);
            this.pnlEditorsViews.Name = "pnlEditorsViews";
            this.pnlEditorsViews.Size = new System.Drawing.Size(207, 354);
            this.pnlEditorsViews.TabIndex = 14;
            // 
            // btnCloseWindow
            // 
            this.btnCloseWindow.Location = new System.Drawing.Point(74, 320);
            this.btnCloseWindow.Name = "btnCloseWindow";
            this.btnCloseWindow.Size = new System.Drawing.Size(117, 23);
            this.btnCloseWindow.TabIndex = 25;
            this.btnCloseWindow.Text = "Close Window";
            this.btnCloseWindow.UseVisualStyleBackColor = true;
            this.btnCloseWindow.Click += new System.EventHandler(this.btnCloseWindow_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Views:";
            // 
            // lstViews
            // 
            this.lstViews.FormattingEnabled = true;
            this.lstViews.Location = new System.Drawing.Point(15, 24);
            this.lstViews.Name = "lstViews";
            this.lstViews.Size = new System.Drawing.Size(176, 290);
            this.lstViews.TabIndex = 14;
            this.lstViews.SelectedIndexChanged += new System.EventHandler(this.lstViews_SelectedIndexChanged);
            // 
            // mnuFileMenu
            // 
            this.mnuFileMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.editorsToolStripMenuItem});
            this.mnuFileMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuFileMenu.Name = "mnuFileMenu";
            this.mnuFileMenu.Size = new System.Drawing.Size(1154, 24);
            this.mnuFileMenu.TabIndex = 15;
            this.mnuFileMenu.Text = "File Menu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showHideViewWindowToolStripMenuItem,
            this.showHideEditorsToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // showHideViewWindowToolStripMenuItem
            // 
            this.showHideViewWindowToolStripMenuItem.Checked = true;
            this.showHideViewWindowToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showHideViewWindowToolStripMenuItem.Name = "showHideViewWindowToolStripMenuItem";
            this.showHideViewWindowToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.showHideViewWindowToolStripMenuItem.Text = "Show/Hide Views";
            this.showHideViewWindowToolStripMenuItem.Click += new System.EventHandler(this.showHideViewWindowToolStripMenuItem_Click);
            // 
            // showHideEditorsToolStripMenuItem
            // 
            this.showHideEditorsToolStripMenuItem.Checked = true;
            this.showHideEditorsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showHideEditorsToolStripMenuItem.Name = "showHideEditorsToolStripMenuItem";
            this.showHideEditorsToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.showHideEditorsToolStripMenuItem.Text = "Show/Hide Editors";
            this.showHideEditorsToolStripMenuItem.Click += new System.EventHandler(this.showHideEditorsToolStripMenuItem_Click);
            // 
            // editorsToolStripMenuItem
            // 
            this.editorsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mapEditorToolStripMenuItem,
            this.chestEditorToolStripMenuItem,
            this.itemEditorToolStripMenuItem,
            this.npcEditorToolStripMenuItem,
            this.shopEditorToolStripMenuItem,
            this.chatEditorToolStripMenuItem,
            this.questEditorToolStripMenuItem,
            this.playerEditorToolStripMenuItem});
            this.editorsToolStripMenuItem.Name = "editorsToolStripMenuItem";
            this.editorsToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.editorsToolStripMenuItem.Text = "Editors";
            // 
            // mapEditorToolStripMenuItem
            // 
            this.mapEditorToolStripMenuItem.Name = "mapEditorToolStripMenuItem";
            this.mapEditorToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.mapEditorToolStripMenuItem.Text = "Map Editor";
            this.mapEditorToolStripMenuItem.Click += new System.EventHandler(this.btnMapEditor_Click);
            // 
            // chestEditorToolStripMenuItem
            // 
            this.chestEditorToolStripMenuItem.Name = "chestEditorToolStripMenuItem";
            this.chestEditorToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.chestEditorToolStripMenuItem.Text = "Chest Editor";
            this.chestEditorToolStripMenuItem.Click += new System.EventHandler(this.btnChestEditor_Click);
            // 
            // itemEditorToolStripMenuItem
            // 
            this.itemEditorToolStripMenuItem.Name = "itemEditorToolStripMenuItem";
            this.itemEditorToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.itemEditorToolStripMenuItem.Text = "Item Editor";
            this.itemEditorToolStripMenuItem.Click += new System.EventHandler(this.btnItemEditor_Click);
            // 
            // npcEditorToolStripMenuItem
            // 
            this.npcEditorToolStripMenuItem.Name = "npcEditorToolStripMenuItem";
            this.npcEditorToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.npcEditorToolStripMenuItem.Text = "Npc Editor";
            this.npcEditorToolStripMenuItem.Click += new System.EventHandler(this.btnNpcEditor_Click);
            // 
            // shopEditorToolStripMenuItem
            // 
            this.shopEditorToolStripMenuItem.Name = "shopEditorToolStripMenuItem";
            this.shopEditorToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.shopEditorToolStripMenuItem.Text = "Shop Editor";
            this.shopEditorToolStripMenuItem.Click += new System.EventHandler(this.btnShopEditor_Click);
            // 
            // chatEditorToolStripMenuItem
            // 
            this.chatEditorToolStripMenuItem.Name = "chatEditorToolStripMenuItem";
            this.chatEditorToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.chatEditorToolStripMenuItem.Text = "Chat Editor";
            this.chatEditorToolStripMenuItem.Click += new System.EventHandler(this.btnChatEditor_Click);
            // 
            // questEditorToolStripMenuItem
            // 
            this.questEditorToolStripMenuItem.Name = "questEditorToolStripMenuItem";
            this.questEditorToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.questEditorToolStripMenuItem.Text = "Quest Editor";
            this.questEditorToolStripMenuItem.Click += new System.EventHandler(this.btnQuestEditor_Click);
            // 
            // playerEditorToolStripMenuItem
            // 
            this.playerEditorToolStripMenuItem.Name = "playerEditorToolStripMenuItem";
            this.playerEditorToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.playerEditorToolStripMenuItem.Text = "Player Editor";
            this.playerEditorToolStripMenuItem.Click += new System.EventHandler(this.btnPlayerEditor_Click);
            // 
            // pnlEditors
            // 
            this.pnlEditors.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlEditors.Controls.Add(this.btnIMGSplit);
            this.pnlEditors.Controls.Add(this.btnCloseEditors);
            this.pnlEditors.Controls.Add(this.btnQuestEditor);
            this.pnlEditors.Controls.Add(this.btnPlayerEditor);
            this.pnlEditors.Controls.Add(this.btnShopEditor);
            this.pnlEditors.Controls.Add(this.btnEditor);
            this.pnlEditors.Controls.Add(this.btnChestEditor);
            this.pnlEditors.Controls.Add(this.btnItemEditor);
            this.pnlEditors.Controls.Add(this.btnNpcEditor);
            this.pnlEditors.Controls.Add(this.btnMapEditor);
            this.pnlEditors.Location = new System.Drawing.Point(225, 343);
            this.pnlEditors.Name = "pnlEditors";
            this.pnlEditors.Size = new System.Drawing.Size(151, 354);
            this.pnlEditors.TabIndex = 16;
            // 
            // btnCloseEditors
            // 
            this.btnCloseEditors.Location = new System.Drawing.Point(15, 320);
            this.btnCloseEditors.Name = "btnCloseEditors";
            this.btnCloseEditors.Size = new System.Drawing.Size(118, 23);
            this.btnCloseEditors.TabIndex = 34;
            this.btnCloseEditors.Text = "Close Window";
            this.btnCloseEditors.UseVisualStyleBackColor = true;
            this.btnCloseEditors.Click += new System.EventHandler(this.btnCloseEditors_Click);
            // 
            // btnQuestEditor
            // 
            this.btnQuestEditor.Location = new System.Drawing.Point(15, 191);
            this.btnQuestEditor.Name = "btnQuestEditor";
            this.btnQuestEditor.Size = new System.Drawing.Size(118, 23);
            this.btnQuestEditor.TabIndex = 33;
            this.btnQuestEditor.Text = "Quest Editor";
            this.btnQuestEditor.UseVisualStyleBackColor = true;
            this.btnQuestEditor.Click += new System.EventHandler(this.btnQuestEditor_Click);
            // 
            // btnPlayerEditor
            // 
            this.btnPlayerEditor.Location = new System.Drawing.Point(15, 220);
            this.btnPlayerEditor.Name = "btnPlayerEditor";
            this.btnPlayerEditor.Size = new System.Drawing.Size(118, 23);
            this.btnPlayerEditor.TabIndex = 32;
            this.btnPlayerEditor.Text = "Player Editor";
            this.btnPlayerEditor.UseVisualStyleBackColor = true;
            this.btnPlayerEditor.Click += new System.EventHandler(this.btnPlayerEditor_Click);
            // 
            // btnShopEditor
            // 
            this.btnShopEditor.Location = new System.Drawing.Point(15, 132);
            this.btnShopEditor.Name = "btnShopEditor";
            this.btnShopEditor.Size = new System.Drawing.Size(119, 23);
            this.btnShopEditor.TabIndex = 31;
            this.btnShopEditor.Text = "Shop Editor";
            this.btnShopEditor.UseVisualStyleBackColor = true;
            this.btnShopEditor.Click += new System.EventHandler(this.btnShopEditor_Click);
            // 
            // btnEditor
            // 
            this.btnEditor.Location = new System.Drawing.Point(15, 162);
            this.btnEditor.Name = "btnEditor";
            this.btnEditor.Size = new System.Drawing.Size(119, 23);
            this.btnEditor.TabIndex = 30;
            this.btnEditor.Text = "Chat Editor";
            this.btnEditor.UseVisualStyleBackColor = true;
            this.btnEditor.Click += new System.EventHandler(this.btnChatEditor_Click);
            // 
            // btnChestEditor
            // 
            this.btnChestEditor.Location = new System.Drawing.Point(15, 43);
            this.btnChestEditor.Name = "btnChestEditor";
            this.btnChestEditor.Size = new System.Drawing.Size(119, 23);
            this.btnChestEditor.TabIndex = 29;
            this.btnChestEditor.Text = "Chest Editor";
            this.btnChestEditor.UseVisualStyleBackColor = true;
            this.btnChestEditor.Click += new System.EventHandler(this.btnChestEditor_Click);
            // 
            // btnItemEditor
            // 
            this.btnItemEditor.Location = new System.Drawing.Point(15, 74);
            this.btnItemEditor.Name = "btnItemEditor";
            this.btnItemEditor.Size = new System.Drawing.Size(119, 23);
            this.btnItemEditor.TabIndex = 27;
            this.btnItemEditor.Text = "Item Editor";
            this.btnItemEditor.UseVisualStyleBackColor = true;
            this.btnItemEditor.Click += new System.EventHandler(this.btnItemEditor_Click);
            // 
            // btnNpcEditor
            // 
            this.btnNpcEditor.Location = new System.Drawing.Point(15, 103);
            this.btnNpcEditor.Name = "btnNpcEditor";
            this.btnNpcEditor.Size = new System.Drawing.Size(119, 23);
            this.btnNpcEditor.TabIndex = 26;
            this.btnNpcEditor.Text = "NPC Editor";
            this.btnNpcEditor.UseVisualStyleBackColor = true;
            this.btnNpcEditor.Click += new System.EventHandler(this.btnNpcEditor_Click);
            // 
            // btnMapEditor
            // 
            this.btnMapEditor.Location = new System.Drawing.Point(15, 14);
            this.btnMapEditor.Name = "btnMapEditor";
            this.btnMapEditor.Size = new System.Drawing.Size(119, 23);
            this.btnMapEditor.TabIndex = 25;
            this.btnMapEditor.Text = "Map Editor";
            this.btnMapEditor.UseVisualStyleBackColor = true;
            this.btnMapEditor.Click += new System.EventHandler(this.btnMapEditor_Click);
            // 
            // btnIMGSplit
            // 
            this.btnIMGSplit.Location = new System.Drawing.Point(15, 250);
            this.btnIMGSplit.Name = "btnIMGSplit";
            this.btnIMGSplit.Size = new System.Drawing.Size(118, 23);
            this.btnIMGSplit.TabIndex = 35;
            this.btnIMGSplit.Text = "Image Splitter";
            this.btnIMGSplit.UseVisualStyleBackColor = true;
            this.btnIMGSplit.Click += new System.EventHandler(this.btnIMGSplit_Click);
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1154, 709);
            this.Controls.Add(this.pnlEditors);
            this.Controls.Add(this.pnlEditorsViews);
            this.Controls.Add(this.dtViewGrid);
            this.Controls.Add(this.mnuFileMenu);
            this.MainMenuStrip = this.mnuFileMenu;
            this.Name = "Editor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Editor";
            ((System.ComponentModel.ISupportInitialize)(this.dtViewGrid)).EndInit();
            this.pnlEditorsViews.ResumeLayout(false);
            this.pnlEditorsViews.PerformLayout();
            this.mnuFileMenu.ResumeLayout(false);
            this.mnuFileMenu.PerformLayout();
            this.pnlEditors.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dtViewGrid;
        private System.Windows.Forms.Panel pnlEditorsViews;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstViews;
        private System.Windows.Forms.MenuStrip mnuFileMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editorsToolStripMenuItem;
        private System.Windows.Forms.Button btnCloseWindow;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showHideViewWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showHideEditorsToolStripMenuItem;
        private System.Windows.Forms.Panel pnlEditors;
        private System.Windows.Forms.Button btnQuestEditor;
        private System.Windows.Forms.Button btnPlayerEditor;
        private System.Windows.Forms.Button btnShopEditor;
        private System.Windows.Forms.Button btnEditor;
        private System.Windows.Forms.Button btnChestEditor;
        private System.Windows.Forms.Button btnItemEditor;
        private System.Windows.Forms.Button btnNpcEditor;
        private System.Windows.Forms.Button btnMapEditor;
        private System.Windows.Forms.Button btnCloseEditors;
        private System.Windows.Forms.ToolStripMenuItem mapEditorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chestEditorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem itemEditorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem npcEditorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shopEditorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chatEditorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem questEditorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playerEditorToolStripMenuItem;
        private System.Windows.Forms.Button btnIMGSplit;
    }
}

