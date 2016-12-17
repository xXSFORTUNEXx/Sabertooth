namespace Editor.Forms
{
    partial class MapEditor
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
            this.tabTools = new System.Windows.Forms.TabControl();
            this.tabLayer = new System.Windows.Forms.TabPage();
            this.chkGrid = new System.Windows.Forms.CheckBox();
            this.radFringe = new System.Windows.Forms.RadioButton();
            this.radFringe2 = new System.Windows.Forms.RadioButton();
            this.radMask2 = new System.Windows.Forms.RadioButton();
            this.radMask = new System.Windows.Forms.RadioButton();
            this.radGround = new System.Windows.Forms.RadioButton();
            this.tabTypes = new System.Windows.Forms.TabPage();
            this.pnlNpcSpawn = new System.Windows.Forms.Panel();
            this.scrlNpcNum = new System.Windows.Forms.HScrollBar();
            this.lblNpcSpawn = new System.Windows.Forms.Label();
            this.radSpawnNpc = new System.Windows.Forms.RadioButton();
            this.radBlocked = new System.Windows.Forms.RadioButton();
            this.radNone = new System.Windows.Forms.RadioButton();
            this.tabTiles = new System.Windows.Forms.TabPage();
            this.pnlTile = new System.Windows.Forms.Panel();
            this.picTileset = new System.Windows.Forms.PictureBox();
            this.lblTileset = new System.Windows.Forms.Label();
            this.cmbTileset = new System.Windows.Forms.ComboBox();
            this.mnuFile = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlDebug = new System.Windows.Forms.Panel();
            this.lblType = new System.Windows.Forms.Label();
            this.lblLayer = new System.Windows.Forms.Label();
            this.lblViewX = new System.Windows.Forms.Label();
            this.lblViewY = new System.Windows.Forms.Label();
            this.lblSelectW = new System.Windows.Forms.Label();
            this.lblSelectH = new System.Windows.Forms.Label();
            this.lblButtonDown = new System.Windows.Forms.Label();
            this.lblSelectY = new System.Windows.Forms.Label();
            this.lblSelectX = new System.Windows.Forms.Label();
            this.lblMouseLoc = new System.Windows.Forms.Label();
            this.picMap = new System.Windows.Forms.PictureBox();
            this.tabTools.SuspendLayout();
            this.tabLayer.SuspendLayout();
            this.tabTypes.SuspendLayout();
            this.pnlNpcSpawn.SuspendLayout();
            this.tabTiles.SuspendLayout();
            this.pnlTile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTileset)).BeginInit();
            this.mnuFile.SuspendLayout();
            this.pnlDebug.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).BeginInit();
            this.SuspendLayout();
            // 
            // tabTools
            // 
            this.tabTools.Controls.Add(this.tabLayer);
            this.tabTools.Controls.Add(this.tabTypes);
            this.tabTools.Controls.Add(this.tabTiles);
            this.tabTools.Location = new System.Drawing.Point(12, 36);
            this.tabTools.Name = "tabTools";
            this.tabTools.SelectedIndex = 0;
            this.tabTools.Size = new System.Drawing.Size(307, 599);
            this.tabTools.TabIndex = 1;
            // 
            // tabLayer
            // 
            this.tabLayer.Controls.Add(this.chkGrid);
            this.tabLayer.Controls.Add(this.radFringe);
            this.tabLayer.Controls.Add(this.radFringe2);
            this.tabLayer.Controls.Add(this.radMask2);
            this.tabLayer.Controls.Add(this.radMask);
            this.tabLayer.Controls.Add(this.radGround);
            this.tabLayer.Location = new System.Drawing.Point(4, 22);
            this.tabLayer.Name = "tabLayer";
            this.tabLayer.Padding = new System.Windows.Forms.Padding(3);
            this.tabLayer.Size = new System.Drawing.Size(299, 573);
            this.tabLayer.TabIndex = 0;
            this.tabLayer.Text = "Layer";
            this.tabLayer.UseVisualStyleBackColor = true;
            // 
            // chkGrid
            // 
            this.chkGrid.AutoSize = true;
            this.chkGrid.Location = new System.Drawing.Point(19, 148);
            this.chkGrid.Name = "chkGrid";
            this.chkGrid.Size = new System.Drawing.Size(45, 17);
            this.chkGrid.TabIndex = 5;
            this.chkGrid.Text = "Grid";
            this.chkGrid.UseVisualStyleBackColor = true;
            // 
            // radFringe
            // 
            this.radFringe.AutoSize = true;
            this.radFringe.Location = new System.Drawing.Point(19, 91);
            this.radFringe.Name = "radFringe";
            this.radFringe.Size = new System.Drawing.Size(54, 17);
            this.radFringe.TabIndex = 4;
            this.radFringe.TabStop = true;
            this.radFringe.Text = "Fringe";
            this.radFringe.UseVisualStyleBackColor = true;
            this.radFringe.CheckedChanged += new System.EventHandler(this.radFringe_CheckedChanged);
            // 
            // radFringe2
            // 
            this.radFringe2.AutoSize = true;
            this.radFringe2.Location = new System.Drawing.Point(19, 114);
            this.radFringe2.Name = "radFringe2";
            this.radFringe2.Size = new System.Drawing.Size(63, 17);
            this.radFringe2.TabIndex = 3;
            this.radFringe2.TabStop = true;
            this.radFringe2.Text = "Fringe 2";
            this.radFringe2.UseVisualStyleBackColor = true;
            this.radFringe2.CheckedChanged += new System.EventHandler(this.radFringe2_CheckedChanged);
            // 
            // radMask2
            // 
            this.radMask2.AutoSize = true;
            this.radMask2.Location = new System.Drawing.Point(19, 68);
            this.radMask2.Name = "radMask2";
            this.radMask2.Size = new System.Drawing.Size(60, 17);
            this.radMask2.TabIndex = 2;
            this.radMask2.TabStop = true;
            this.radMask2.Text = "Mask 2";
            this.radMask2.UseVisualStyleBackColor = true;
            this.radMask2.CheckedChanged += new System.EventHandler(this.radMask2_CheckedChanged);
            // 
            // radMask
            // 
            this.radMask.AutoSize = true;
            this.radMask.Location = new System.Drawing.Point(19, 45);
            this.radMask.Name = "radMask";
            this.radMask.Size = new System.Drawing.Size(51, 17);
            this.radMask.TabIndex = 1;
            this.radMask.TabStop = true;
            this.radMask.Text = "Mask\r\n";
            this.radMask.UseVisualStyleBackColor = true;
            this.radMask.CheckedChanged += new System.EventHandler(this.radMask_CheckedChanged);
            // 
            // radGround
            // 
            this.radGround.AutoSize = true;
            this.radGround.Checked = true;
            this.radGround.Location = new System.Drawing.Point(19, 22);
            this.radGround.Name = "radGround";
            this.radGround.Size = new System.Drawing.Size(60, 17);
            this.radGround.TabIndex = 0;
            this.radGround.TabStop = true;
            this.radGround.Text = "Ground";
            this.radGround.UseVisualStyleBackColor = true;
            this.radGround.CheckedChanged += new System.EventHandler(this.radGround_CheckedChanged);
            // 
            // tabTypes
            // 
            this.tabTypes.Controls.Add(this.pnlNpcSpawn);
            this.tabTypes.Controls.Add(this.radSpawnNpc);
            this.tabTypes.Controls.Add(this.radBlocked);
            this.tabTypes.Controls.Add(this.radNone);
            this.tabTypes.Location = new System.Drawing.Point(4, 22);
            this.tabTypes.Name = "tabTypes";
            this.tabTypes.Padding = new System.Windows.Forms.Padding(3);
            this.tabTypes.Size = new System.Drawing.Size(299, 573);
            this.tabTypes.TabIndex = 1;
            this.tabTypes.Text = "Types";
            this.tabTypes.UseVisualStyleBackColor = true;
            // 
            // pnlNpcSpawn
            // 
            this.pnlNpcSpawn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlNpcSpawn.Controls.Add(this.scrlNpcNum);
            this.pnlNpcSpawn.Controls.Add(this.lblNpcSpawn);
            this.pnlNpcSpawn.Location = new System.Drawing.Point(20, 92);
            this.pnlNpcSpawn.Name = "pnlNpcSpawn";
            this.pnlNpcSpawn.Size = new System.Drawing.Size(200, 80);
            this.pnlNpcSpawn.TabIndex = 3;
            this.pnlNpcSpawn.Visible = false;
            // 
            // scrlNpcNum
            // 
            this.scrlNpcNum.Location = new System.Drawing.Point(19, 37);
            this.scrlNpcNum.Maximum = 50;
            this.scrlNpcNum.Minimum = 1;
            this.scrlNpcNum.Name = "scrlNpcNum";
            this.scrlNpcNum.Size = new System.Drawing.Size(161, 17);
            this.scrlNpcNum.TabIndex = 1;
            this.scrlNpcNum.Value = 1;
            this.scrlNpcNum.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlNpcNum_Scroll);
            // 
            // lblNpcSpawn
            // 
            this.lblNpcSpawn.AutoSize = true;
            this.lblNpcSpawn.Location = new System.Drawing.Point(16, 16);
            this.lblNpcSpawn.Name = "lblNpcSpawn";
            this.lblNpcSpawn.Size = new System.Drawing.Size(81, 13);
            this.lblNpcSpawn.TabIndex = 0;
            this.lblNpcSpawn.Text = "NPC Number: 1";
            // 
            // radSpawnNpc
            // 
            this.radSpawnNpc.AutoSize = true;
            this.radSpawnNpc.Location = new System.Drawing.Point(20, 69);
            this.radSpawnNpc.Name = "radSpawnNpc";
            this.radSpawnNpc.Size = new System.Drawing.Size(81, 17);
            this.radSpawnNpc.TabIndex = 2;
            this.radSpawnNpc.TabStop = true;
            this.radSpawnNpc.Text = "Spawn Npc";
            this.radSpawnNpc.UseVisualStyleBackColor = true;
            this.radSpawnNpc.CheckedChanged += new System.EventHandler(this.radSpawnNpc_CheckedChanged);
            // 
            // radBlocked
            // 
            this.radBlocked.AutoSize = true;
            this.radBlocked.Location = new System.Drawing.Point(20, 46);
            this.radBlocked.Name = "radBlocked";
            this.radBlocked.Size = new System.Drawing.Size(64, 17);
            this.radBlocked.TabIndex = 1;
            this.radBlocked.Text = "Blocked";
            this.radBlocked.UseVisualStyleBackColor = true;
            this.radBlocked.CheckedChanged += new System.EventHandler(this.radBlocked_CheckedChanged);
            // 
            // radNone
            // 
            this.radNone.AutoSize = true;
            this.radNone.Checked = true;
            this.radNone.Location = new System.Drawing.Point(20, 23);
            this.radNone.Name = "radNone";
            this.radNone.Size = new System.Drawing.Size(51, 17);
            this.radNone.TabIndex = 0;
            this.radNone.TabStop = true;
            this.radNone.Text = "None";
            this.radNone.UseVisualStyleBackColor = true;
            this.radNone.CheckedChanged += new System.EventHandler(this.radNone_CheckedChanged);
            // 
            // tabTiles
            // 
            this.tabTiles.Controls.Add(this.pnlTile);
            this.tabTiles.Controls.Add(this.lblTileset);
            this.tabTiles.Controls.Add(this.cmbTileset);
            this.tabTiles.Location = new System.Drawing.Point(4, 22);
            this.tabTiles.Name = "tabTiles";
            this.tabTiles.Size = new System.Drawing.Size(299, 573);
            this.tabTiles.TabIndex = 2;
            this.tabTiles.Text = "Tiles";
            this.tabTiles.UseVisualStyleBackColor = true;
            // 
            // pnlTile
            // 
            this.pnlTile.AutoScroll = true;
            this.pnlTile.Controls.Add(this.picTileset);
            this.pnlTile.Location = new System.Drawing.Point(3, 59);
            this.pnlTile.Name = "pnlTile";
            this.pnlTile.Size = new System.Drawing.Size(293, 511);
            this.pnlTile.TabIndex = 4;
            // 
            // picTileset
            // 
            this.picTileset.Location = new System.Drawing.Point(3, 5);
            this.picTileset.Name = "picTileset";
            this.picTileset.Size = new System.Drawing.Size(100, 50);
            this.picTileset.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picTileset.TabIndex = 0;
            this.picTileset.TabStop = false;
            // 
            // lblTileset
            // 
            this.lblTileset.AutoSize = true;
            this.lblTileset.Location = new System.Drawing.Point(3, 16);
            this.lblTileset.Name = "lblTileset";
            this.lblTileset.Size = new System.Drawing.Size(41, 13);
            this.lblTileset.TabIndex = 3;
            this.lblTileset.Text = "Tileset:";
            // 
            // cmbTileset
            // 
            this.cmbTileset.FormattingEnabled = true;
            this.cmbTileset.Location = new System.Drawing.Point(3, 32);
            this.cmbTileset.Name = "cmbTileset";
            this.cmbTileset.Size = new System.Drawing.Size(293, 21);
            this.cmbTileset.TabIndex = 2;
            this.cmbTileset.SelectedIndexChanged += new System.EventHandler(this.cmbTileset_SelectedIndexChanged);
            // 
            // mnuFile
            // 
            this.mnuFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.mnuFile.Location = new System.Drawing.Point(0, 0);
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(1375, 24);
            this.mnuFile.TabIndex = 12;
            this.mnuFile.Text = "File";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOpen,
            this.mnuSave,
            this.mnuExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // mnuOpen
            // 
            this.mnuOpen.Name = "mnuOpen";
            this.mnuOpen.Size = new System.Drawing.Size(152, 22);
            this.mnuOpen.Text = "Open";
            this.mnuOpen.Click += new System.EventHandler(this.mnuOpen_Click);
            // 
            // mnuSave
            // 
            this.mnuSave.Name = "mnuSave";
            this.mnuSave.Size = new System.Drawing.Size(152, 22);
            this.mnuSave.Text = "Save";
            this.mnuSave.Click += new System.EventHandler(this.mnuSave_Click);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(152, 22);
            this.mnuExit.Text = "Exit";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // pnlDebug
            // 
            this.pnlDebug.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDebug.Controls.Add(this.lblType);
            this.pnlDebug.Controls.Add(this.lblLayer);
            this.pnlDebug.Controls.Add(this.lblViewX);
            this.pnlDebug.Controls.Add(this.lblViewY);
            this.pnlDebug.Controls.Add(this.lblSelectW);
            this.pnlDebug.Controls.Add(this.lblSelectH);
            this.pnlDebug.Controls.Add(this.lblButtonDown);
            this.pnlDebug.Controls.Add(this.lblSelectY);
            this.pnlDebug.Controls.Add(this.lblSelectX);
            this.pnlDebug.Controls.Add(this.lblMouseLoc);
            this.pnlDebug.Location = new System.Drawing.Point(1131, 31);
            this.pnlDebug.Name = "pnlDebug";
            this.pnlDebug.Size = new System.Drawing.Size(228, 259);
            this.pnlDebug.TabIndex = 13;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(16, 218);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(63, 13);
            this.lblType.TabIndex = 21;
            this.lblType.Text = "Type: None";
            // 
            // lblLayer
            // 
            this.lblLayer.AutoSize = true;
            this.lblLayer.Location = new System.Drawing.Point(16, 203);
            this.lblLayer.Name = "lblLayer";
            this.lblLayer.Size = new System.Drawing.Size(74, 13);
            this.lblLayer.TabIndex = 20;
            this.lblLayer.Text = "Layer: Ground";
            // 
            // lblViewX
            // 
            this.lblViewX.AutoSize = true;
            this.lblViewX.Location = new System.Drawing.Point(16, 160);
            this.lblViewX.Name = "lblViewX";
            this.lblViewX.Size = new System.Drawing.Size(52, 13);
            this.lblViewX.TabIndex = 19;
            this.lblViewX.Text = "View X: 0";
            // 
            // lblViewY
            // 
            this.lblViewY.AutoSize = true;
            this.lblViewY.Location = new System.Drawing.Point(16, 176);
            this.lblViewY.Name = "lblViewY";
            this.lblViewY.Size = new System.Drawing.Size(52, 13);
            this.lblViewY.TabIndex = 18;
            this.lblViewY.Text = "View Y: 0";
            // 
            // lblSelectW
            // 
            this.lblSelectW.AutoSize = true;
            this.lblSelectW.Location = new System.Drawing.Point(16, 85);
            this.lblSelectW.Name = "lblSelectW";
            this.lblSelectW.Size = new System.Drawing.Size(77, 13);
            this.lblSelectW.TabIndex = 17;
            this.lblSelectW.Text = "SelectTileW: 0";
            // 
            // lblSelectH
            // 
            this.lblSelectH.AutoSize = true;
            this.lblSelectH.Location = new System.Drawing.Point(16, 101);
            this.lblSelectH.Name = "lblSelectH";
            this.lblSelectH.Size = new System.Drawing.Size(74, 13);
            this.lblSelectH.TabIndex = 16;
            this.lblSelectH.Text = "SelectTileH: 0";
            // 
            // lblButtonDown
            // 
            this.lblButtonDown.AutoSize = true;
            this.lblButtonDown.Location = new System.Drawing.Point(16, 135);
            this.lblButtonDown.Name = "lblButtonDown";
            this.lblButtonDown.Size = new System.Drawing.Size(81, 13);
            this.lblButtonDown.TabIndex = 15;
            this.lblButtonDown.Text = "Button Down: ?";
            // 
            // lblSelectY
            // 
            this.lblSelectY.AutoSize = true;
            this.lblSelectY.Location = new System.Drawing.Point(16, 40);
            this.lblSelectY.Name = "lblSelectY";
            this.lblSelectY.Size = new System.Drawing.Size(73, 13);
            this.lblSelectY.TabIndex = 14;
            this.lblSelectY.Text = "SelectTileY: 0";
            // 
            // lblSelectX
            // 
            this.lblSelectX.AutoSize = true;
            this.lblSelectX.Location = new System.Drawing.Point(16, 56);
            this.lblSelectX.Name = "lblSelectX";
            this.lblSelectX.Size = new System.Drawing.Size(73, 13);
            this.lblSelectX.TabIndex = 13;
            this.lblSelectX.Text = "SelectTileX: 0";
            // 
            // lblMouseLoc
            // 
            this.lblMouseLoc.AutoSize = true;
            this.lblMouseLoc.Location = new System.Drawing.Point(16, 13);
            this.lblMouseLoc.Name = "lblMouseLoc";
            this.lblMouseLoc.Size = new System.Drawing.Size(92, 13);
            this.lblMouseLoc.TabIndex = 12;
            this.lblMouseLoc.Text = "Mouse - X: 0, Y: 0";
            // 
            // picMap
            // 
            this.picMap.Location = new System.Drawing.Point(321, 31);
            this.picMap.Name = "picMap";
            this.picMap.Size = new System.Drawing.Size(800, 600);
            this.picMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picMap.TabIndex = 14;
            this.picMap.TabStop = false;
            // 
            // MapEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1375, 649);
            this.Controls.Add(this.picMap);
            this.Controls.Add(this.pnlDebug);
            this.Controls.Add(this.tabTools);
            this.Controls.Add(this.mnuFile);
            this.MainMenuStrip = this.mnuFile;
            this.Name = "MapEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Map Editor";
            this.tabTools.ResumeLayout(false);
            this.tabLayer.ResumeLayout(false);
            this.tabLayer.PerformLayout();
            this.tabTypes.ResumeLayout(false);
            this.tabTypes.PerformLayout();
            this.pnlNpcSpawn.ResumeLayout(false);
            this.pnlNpcSpawn.PerformLayout();
            this.tabTiles.ResumeLayout(false);
            this.tabTiles.PerformLayout();
            this.pnlTile.ResumeLayout(false);
            this.pnlTile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTileset)).EndInit();
            this.mnuFile.ResumeLayout(false);
            this.mnuFile.PerformLayout();
            this.pnlDebug.ResumeLayout(false);
            this.pnlDebug.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TabControl tabTools;
        private System.Windows.Forms.TabPage tabLayer;
        private System.Windows.Forms.TabPage tabTypes;
        private System.Windows.Forms.TabPage tabTiles;
        private System.Windows.Forms.RadioButton radFringe;
        private System.Windows.Forms.RadioButton radFringe2;
        private System.Windows.Forms.RadioButton radMask2;
        private System.Windows.Forms.RadioButton radMask;
        private System.Windows.Forms.RadioButton radGround;
        private System.Windows.Forms.RadioButton radSpawnNpc;
        private System.Windows.Forms.RadioButton radBlocked;
        private System.Windows.Forms.RadioButton radNone;
        private System.Windows.Forms.Label lblTileset;
        private System.Windows.Forms.ComboBox cmbTileset;
        private System.Windows.Forms.CheckBox chkGrid;
        private System.Windows.Forms.MenuStrip mnuFile;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuOpen;
        private System.Windows.Forms.ToolStripMenuItem mnuSave;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.Panel pnlTile;
        private System.Windows.Forms.PictureBox picTileset;
        private System.Windows.Forms.Panel pnlDebug;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblLayer;
        private System.Windows.Forms.Label lblViewX;
        private System.Windows.Forms.Label lblViewY;
        private System.Windows.Forms.Label lblSelectW;
        private System.Windows.Forms.Label lblSelectH;
        private System.Windows.Forms.Label lblButtonDown;
        private System.Windows.Forms.Label lblSelectY;
        private System.Windows.Forms.Label lblSelectX;
        private System.Windows.Forms.Label lblMouseLoc;
        private System.Windows.Forms.Panel pnlNpcSpawn;
        private System.Windows.Forms.HScrollBar scrlNpcNum;
        private System.Windows.Forms.Label lblNpcSpawn;
        private System.Windows.Forms.PictureBox picMap;
    }
}