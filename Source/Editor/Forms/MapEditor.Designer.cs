﻿namespace Editor.Forms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapEditor));
            this.tabTools = new System.Windows.Forms.TabControl();
            this.tabLayer = new System.Windows.Forms.TabPage();
            this.pnlOptions = new System.Windows.Forms.Panel();
            this.chkHScroll = new System.Windows.Forms.CheckBox();
            this.radScroll = new System.Windows.Forms.RadioButton();
            this.radZoom = new System.Windows.Forms.RadioButton();
            this.chkNpc = new System.Windows.Forms.CheckBox();
            this.chkGrid = new System.Windows.Forms.CheckBox();
            this.radFringe = new System.Windows.Forms.RadioButton();
            this.radFringe2 = new System.Windows.Forms.RadioButton();
            this.radMask2 = new System.Windows.Forms.RadioButton();
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
            this.radMask = new System.Windows.Forms.RadioButton();
            this.radGround = new System.Windows.Forms.RadioButton();
            this.tabTypes = new System.Windows.Forms.TabPage();
            this.pnlChest = new System.Windows.Forms.Panel();
            this.scrlChest = new System.Windows.Forms.HScrollBar();
            this.lblChest = new System.Windows.Forms.Label();
            this.radChest = new System.Windows.Forms.RadioButton();
            this.pnlMapItem = new System.Windows.Forms.Panel();
            this.scrlItemAmount = new System.Windows.Forms.HScrollBar();
            this.lblItemAmount = new System.Windows.Forms.Label();
            this.scrlItemNum = new System.Windows.Forms.HScrollBar();
            this.lblItemNum = new System.Windows.Forms.Label();
            this.radMapItem = new System.Windows.Forms.RadioButton();
            this.radNpcAvoid = new System.Windows.Forms.RadioButton();
            this.radSpawnPool = new System.Windows.Forms.RadioButton();
            this.pnlNpcSpawn = new System.Windows.Forms.Panel();
            this.scrlSpawnAmount = new System.Windows.Forms.HScrollBar();
            this.lblSpawnAmount = new System.Windows.Forms.Label();
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
            this.tabLight = new System.Windows.Forms.TabPage();
            this.scrlIntensity = new System.Windows.Forms.HScrollBar();
            this.lblIntensity = new System.Windows.Forms.Label();
            this.chkNight = new System.Windows.Forms.CheckBox();
            this.tabHelp = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.picMap = new System.Windows.Forms.PictureBox();
            this.pnlMapNpcs = new System.Windows.Forms.Panel();
            this.cmbNpc10 = new System.Windows.Forms.ComboBox();
            this.cmbNpc9 = new System.Windows.Forms.ComboBox();
            this.cmbNpc8 = new System.Windows.Forms.ComboBox();
            this.cmbNpc7 = new System.Windows.Forms.ComboBox();
            this.cmbNpc6 = new System.Windows.Forms.ComboBox();
            this.cmbNpc5 = new System.Windows.Forms.ComboBox();
            this.cmbNpc4 = new System.Windows.Forms.ComboBox();
            this.cmbNpc3 = new System.Windows.Forms.ComboBox();
            this.cmbNpc2 = new System.Windows.Forms.ComboBox();
            this.cmbNpc1 = new System.Windows.Forms.ComboBox();
            this.btnCloseNpcs = new System.Windows.Forms.Button();
            this.lblNpcs = new System.Windows.Forms.Label();
            this.scrlViewX = new System.Windows.Forms.HScrollBar();
            this.scrlViewY = new System.Windows.Forms.VScrollBar();
            this.treeMaps = new System.Windows.Forms.TreeView();
            this.mapProperties = new System.Windows.Forms.PropertyGrid();
            this.tosMenu = new System.Windows.Forms.ToolStrip();
            this.btnNewMap = new System.Windows.Forms.ToolStripButton();
            this.btnSaveMap = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnMapNpcs = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnFillMap = new System.Windows.Forms.ToolStripButton();
            this.btnDeleteLayer = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDebug = new System.Windows.Forms.ToolStripButton();
            this.btnHelp = new System.Windows.Forms.ToolStripButton();
            this.tabTools.SuspendLayout();
            this.tabLayer.SuspendLayout();
            this.pnlOptions.SuspendLayout();
            this.pnlDebug.SuspendLayout();
            this.tabTypes.SuspendLayout();
            this.pnlChest.SuspendLayout();
            this.pnlMapItem.SuspendLayout();
            this.pnlNpcSpawn.SuspendLayout();
            this.tabTiles.SuspendLayout();
            this.pnlTile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTileset)).BeginInit();
            this.tabLight.SuspendLayout();
            this.tabHelp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).BeginInit();
            this.pnlMapNpcs.SuspendLayout();
            this.tosMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabTools
            // 
            this.tabTools.Controls.Add(this.tabLayer);
            this.tabTools.Controls.Add(this.tabTypes);
            this.tabTools.Controls.Add(this.tabTiles);
            this.tabTools.Controls.Add(this.tabLight);
            this.tabTools.Controls.Add(this.tabHelp);
            this.tabTools.Location = new System.Drawing.Point(12, 36);
            this.tabTools.Name = "tabTools";
            this.tabTools.SelectedIndex = 0;
            this.tabTools.Size = new System.Drawing.Size(307, 615);
            this.tabTools.TabIndex = 1;
            // 
            // tabLayer
            // 
            this.tabLayer.Controls.Add(this.pnlOptions);
            this.tabLayer.Controls.Add(this.chkNpc);
            this.tabLayer.Controls.Add(this.chkGrid);
            this.tabLayer.Controls.Add(this.radFringe);
            this.tabLayer.Controls.Add(this.radFringe2);
            this.tabLayer.Controls.Add(this.radMask2);
            this.tabLayer.Controls.Add(this.pnlDebug);
            this.tabLayer.Controls.Add(this.radMask);
            this.tabLayer.Controls.Add(this.radGround);
            this.tabLayer.Location = new System.Drawing.Point(4, 22);
            this.tabLayer.Name = "tabLayer";
            this.tabLayer.Padding = new System.Windows.Forms.Padding(3);
            this.tabLayer.Size = new System.Drawing.Size(299, 589);
            this.tabLayer.TabIndex = 0;
            this.tabLayer.Text = "Layer";
            this.tabLayer.UseVisualStyleBackColor = true;
            // 
            // pnlOptions
            // 
            this.pnlOptions.Controls.Add(this.chkHScroll);
            this.pnlOptions.Controls.Add(this.radScroll);
            this.pnlOptions.Controls.Add(this.radZoom);
            this.pnlOptions.Location = new System.Drawing.Point(195, 22);
            this.pnlOptions.Name = "pnlOptions";
            this.pnlOptions.Size = new System.Drawing.Size(86, 109);
            this.pnlOptions.TabIndex = 15;
            // 
            // chkHScroll
            // 
            this.chkHScroll.AutoSize = true;
            this.chkHScroll.Location = new System.Drawing.Point(12, 69);
            this.chkHScroll.Name = "chkHScroll";
            this.chkHScroll.Size = new System.Drawing.Size(63, 17);
            this.chkHScroll.TabIndex = 2;
            this.chkHScroll.Text = "H Scroll";
            this.chkHScroll.UseVisualStyleBackColor = true;
            this.chkHScroll.CheckedChanged += new System.EventHandler(this.chkHScroll_CheckedChanged);
            // 
            // radScroll
            // 
            this.radScroll.AutoSize = true;
            this.radScroll.Location = new System.Drawing.Point(12, 37);
            this.radScroll.Name = "radScroll";
            this.radScroll.Size = new System.Drawing.Size(51, 17);
            this.radScroll.TabIndex = 1;
            this.radScroll.Text = "Scroll";
            this.radScroll.UseVisualStyleBackColor = true;
            this.radScroll.CheckedChanged += new System.EventHandler(this.radScroll_CheckedChanged);
            // 
            // radZoom
            // 
            this.radZoom.AutoSize = true;
            this.radZoom.Checked = true;
            this.radZoom.Location = new System.Drawing.Point(12, 13);
            this.radZoom.Name = "radZoom";
            this.radZoom.Size = new System.Drawing.Size(52, 17);
            this.radZoom.TabIndex = 0;
            this.radZoom.TabStop = true;
            this.radZoom.Text = "Zoom";
            this.radZoom.UseVisualStyleBackColor = true;
            this.radZoom.CheckedChanged += new System.EventHandler(this.radZoom_CheckedChanged);
            // 
            // chkNpc
            // 
            this.chkNpc.AutoSize = true;
            this.chkNpc.Location = new System.Drawing.Point(19, 171);
            this.chkNpc.Name = "chkNpc";
            this.chkNpc.Size = new System.Drawing.Size(79, 17);
            this.chkNpc.TabIndex = 14;
            this.chkNpc.Text = "Show npcs";
            this.chkNpc.UseVisualStyleBackColor = true;
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
            this.pnlDebug.Location = new System.Drawing.Point(36, 293);
            this.pnlDebug.Name = "pnlDebug";
            this.pnlDebug.Size = new System.Drawing.Size(228, 259);
            this.pnlDebug.TabIndex = 13;
            this.pnlDebug.Visible = false;
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
            this.tabTypes.Controls.Add(this.pnlChest);
            this.tabTypes.Controls.Add(this.radChest);
            this.tabTypes.Controls.Add(this.pnlMapItem);
            this.tabTypes.Controls.Add(this.radMapItem);
            this.tabTypes.Controls.Add(this.radNpcAvoid);
            this.tabTypes.Controls.Add(this.radSpawnPool);
            this.tabTypes.Controls.Add(this.pnlNpcSpawn);
            this.tabTypes.Controls.Add(this.radSpawnNpc);
            this.tabTypes.Controls.Add(this.radBlocked);
            this.tabTypes.Controls.Add(this.radNone);
            this.tabTypes.Location = new System.Drawing.Point(4, 22);
            this.tabTypes.Name = "tabTypes";
            this.tabTypes.Padding = new System.Windows.Forms.Padding(3);
            this.tabTypes.Size = new System.Drawing.Size(299, 589);
            this.tabTypes.TabIndex = 1;
            this.tabTypes.Text = "Types";
            this.tabTypes.UseVisualStyleBackColor = true;
            // 
            // pnlChest
            // 
            this.pnlChest.Controls.Add(this.scrlChest);
            this.pnlChest.Controls.Add(this.lblChest);
            this.pnlChest.Location = new System.Drawing.Point(126, 267);
            this.pnlChest.Name = "pnlChest";
            this.pnlChest.Size = new System.Drawing.Size(167, 69);
            this.pnlChest.TabIndex = 9;
            this.pnlChest.Visible = false;
            // 
            // scrlChest
            // 
            this.scrlChest.LargeChange = 1;
            this.scrlChest.Location = new System.Drawing.Point(20, 26);
            this.scrlChest.Maximum = 10;
            this.scrlChest.Minimum = 1;
            this.scrlChest.Name = "scrlChest";
            this.scrlChest.Size = new System.Drawing.Size(123, 17);
            this.scrlChest.TabIndex = 1;
            this.scrlChest.Value = 1;
            this.scrlChest.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlChest_Scroll);
            // 
            // lblChest
            // 
            this.lblChest.AutoSize = true;
            this.lblChest.Location = new System.Drawing.Point(17, 13);
            this.lblChest.Name = "lblChest";
            this.lblChest.Size = new System.Drawing.Size(46, 13);
            this.lblChest.TabIndex = 0;
            this.lblChest.Text = "Chest: 1";
            // 
            // radChest
            // 
            this.radChest.AutoSize = true;
            this.radChest.Location = new System.Drawing.Point(20, 159);
            this.radChest.Name = "radChest";
            this.radChest.Size = new System.Drawing.Size(52, 17);
            this.radChest.TabIndex = 8;
            this.radChest.TabStop = true;
            this.radChest.Text = "Chest";
            this.radChest.UseVisualStyleBackColor = true;
            this.radChest.CheckedChanged += new System.EventHandler(this.radChest_CheckedChanged);
            // 
            // pnlMapItem
            // 
            this.pnlMapItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMapItem.Controls.Add(this.scrlItemAmount);
            this.pnlMapItem.Controls.Add(this.lblItemAmount);
            this.pnlMapItem.Controls.Add(this.scrlItemNum);
            this.pnlMapItem.Controls.Add(this.lblItemNum);
            this.pnlMapItem.Location = new System.Drawing.Point(126, 136);
            this.pnlMapItem.Name = "pnlMapItem";
            this.pnlMapItem.Size = new System.Drawing.Size(167, 129);
            this.pnlMapItem.TabIndex = 6;
            this.pnlMapItem.Visible = false;
            // 
            // scrlItemAmount
            // 
            this.scrlItemAmount.LargeChange = 100;
            this.scrlItemAmount.Location = new System.Drawing.Point(19, 90);
            this.scrlItemAmount.Maximum = 5000;
            this.scrlItemAmount.Name = "scrlItemAmount";
            this.scrlItemAmount.Size = new System.Drawing.Size(123, 17);
            this.scrlItemAmount.TabIndex = 3;
            this.scrlItemAmount.Value = 1;
            this.scrlItemAmount.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlItemAmount_Scroll);
            // 
            // lblItemAmount
            // 
            this.lblItemAmount.AutoSize = true;
            this.lblItemAmount.Location = new System.Drawing.Point(16, 71);
            this.lblItemAmount.Name = "lblItemAmount";
            this.lblItemAmount.Size = new System.Drawing.Size(55, 13);
            this.lblItemAmount.TabIndex = 2;
            this.lblItemAmount.Text = "Amount: 1";
            // 
            // scrlItemNum
            // 
            this.scrlItemNum.LargeChange = 1;
            this.scrlItemNum.Location = new System.Drawing.Point(19, 37);
            this.scrlItemNum.Maximum = 50;
            this.scrlItemNum.Minimum = 1;
            this.scrlItemNum.Name = "scrlItemNum";
            this.scrlItemNum.Size = new System.Drawing.Size(123, 17);
            this.scrlItemNum.TabIndex = 1;
            this.scrlItemNum.Value = 1;
            this.scrlItemNum.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlItemNum_Scroll);
            // 
            // lblItemNum
            // 
            this.lblItemNum.AutoSize = true;
            this.lblItemNum.Location = new System.Drawing.Point(16, 16);
            this.lblItemNum.Name = "lblItemNum";
            this.lblItemNum.Size = new System.Drawing.Size(79, 13);
            this.lblItemNum.TabIndex = 0;
            this.lblItemNum.Text = "Item Number: 1";
            // 
            // radMapItem
            // 
            this.radMapItem.AutoSize = true;
            this.radMapItem.Location = new System.Drawing.Point(19, 136);
            this.radMapItem.Name = "radMapItem";
            this.radMapItem.Size = new System.Drawing.Size(45, 17);
            this.radMapItem.TabIndex = 7;
            this.radMapItem.TabStop = true;
            this.radMapItem.Text = "Item";
            this.radMapItem.UseVisualStyleBackColor = true;
            this.radMapItem.CheckedChanged += new System.EventHandler(this.radMapItem_CheckedChanged);
            // 
            // radNpcAvoid
            // 
            this.radNpcAvoid.AutoSize = true;
            this.radNpcAvoid.Location = new System.Drawing.Point(20, 68);
            this.radNpcAvoid.Name = "radNpcAvoid";
            this.radNpcAvoid.Size = new System.Drawing.Size(75, 17);
            this.radNpcAvoid.TabIndex = 5;
            this.radNpcAvoid.TabStop = true;
            this.radNpcAvoid.Text = "Npc Avoid";
            this.radNpcAvoid.UseVisualStyleBackColor = true;
            this.radNpcAvoid.CheckedChanged += new System.EventHandler(this.radNpcAvoid_CheckedChanged);
            // 
            // radSpawnPool
            // 
            this.radSpawnPool.AutoSize = true;
            this.radSpawnPool.Location = new System.Drawing.Point(19, 113);
            this.radSpawnPool.Name = "radSpawnPool";
            this.radSpawnPool.Size = new System.Drawing.Size(82, 17);
            this.radSpawnPool.TabIndex = 4;
            this.radSpawnPool.TabStop = true;
            this.radSpawnPool.Text = "Spawn Pool";
            this.radSpawnPool.UseVisualStyleBackColor = true;
            this.radSpawnPool.CheckedChanged += new System.EventHandler(this.radSpawnPool_CheckedChanged);
            // 
            // pnlNpcSpawn
            // 
            this.pnlNpcSpawn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlNpcSpawn.Controls.Add(this.scrlSpawnAmount);
            this.pnlNpcSpawn.Controls.Add(this.lblSpawnAmount);
            this.pnlNpcSpawn.Controls.Add(this.scrlNpcNum);
            this.pnlNpcSpawn.Controls.Add(this.lblNpcSpawn);
            this.pnlNpcSpawn.Location = new System.Drawing.Point(126, 6);
            this.pnlNpcSpawn.Name = "pnlNpcSpawn";
            this.pnlNpcSpawn.Size = new System.Drawing.Size(167, 129);
            this.pnlNpcSpawn.TabIndex = 3;
            this.pnlNpcSpawn.Visible = false;
            // 
            // scrlSpawnAmount
            // 
            this.scrlSpawnAmount.Enabled = false;
            this.scrlSpawnAmount.LargeChange = 1;
            this.scrlSpawnAmount.Location = new System.Drawing.Point(19, 90);
            this.scrlSpawnAmount.Maximum = 20;
            this.scrlSpawnAmount.Name = "scrlSpawnAmount";
            this.scrlSpawnAmount.Size = new System.Drawing.Size(123, 17);
            this.scrlSpawnAmount.TabIndex = 3;
            this.scrlSpawnAmount.Value = 1;
            this.scrlSpawnAmount.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlSpawnAmount_Scroll);
            // 
            // lblSpawnAmount
            // 
            this.lblSpawnAmount.AutoSize = true;
            this.lblSpawnAmount.Location = new System.Drawing.Point(16, 71);
            this.lblSpawnAmount.Name = "lblSpawnAmount";
            this.lblSpawnAmount.Size = new System.Drawing.Size(55, 13);
            this.lblSpawnAmount.TabIndex = 2;
            this.lblSpawnAmount.Text = "Amount: 1";
            // 
            // scrlNpcNum
            // 
            this.scrlNpcNum.LargeChange = 1;
            this.scrlNpcNum.Location = new System.Drawing.Point(19, 37);
            this.scrlNpcNum.Maximum = 50;
            this.scrlNpcNum.Minimum = 1;
            this.scrlNpcNum.Name = "scrlNpcNum";
            this.scrlNpcNum.Size = new System.Drawing.Size(123, 17);
            this.scrlNpcNum.TabIndex = 1;
            this.scrlNpcNum.Value = 1;
            this.scrlNpcNum.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlNpcNum_Scroll);
            // 
            // lblNpcSpawn
            // 
            this.lblNpcSpawn.AutoSize = true;
            this.lblNpcSpawn.Location = new System.Drawing.Point(16, 16);
            this.lblNpcSpawn.Name = "lblNpcSpawn";
            this.lblNpcSpawn.Size = new System.Drawing.Size(79, 13);
            this.lblNpcSpawn.TabIndex = 0;
            this.lblNpcSpawn.Text = "Npc Number: 1";
            // 
            // radSpawnNpc
            // 
            this.radSpawnNpc.AutoSize = true;
            this.radSpawnNpc.Location = new System.Drawing.Point(19, 91);
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
            this.tabTiles.Size = new System.Drawing.Size(299, 589);
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
            this.pnlTile.Size = new System.Drawing.Size(293, 527);
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
            // tabLight
            // 
            this.tabLight.Controls.Add(this.scrlIntensity);
            this.tabLight.Controls.Add(this.lblIntensity);
            this.tabLight.Controls.Add(this.chkNight);
            this.tabLight.Location = new System.Drawing.Point(4, 22);
            this.tabLight.Name = "tabLight";
            this.tabLight.Size = new System.Drawing.Size(299, 589);
            this.tabLight.TabIndex = 4;
            this.tabLight.Text = "Light";
            this.tabLight.UseVisualStyleBackColor = true;
            // 
            // scrlIntensity
            // 
            this.scrlIntensity.LargeChange = 20;
            this.scrlIntensity.Location = new System.Drawing.Point(23, 57);
            this.scrlIntensity.Maximum = 400;
            this.scrlIntensity.Name = "scrlIntensity";
            this.scrlIntensity.Size = new System.Drawing.Size(141, 17);
            this.scrlIntensity.TabIndex = 2;
            this.scrlIntensity.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlIntensity_Scroll);
            // 
            // lblIntensity
            // 
            this.lblIntensity.AutoSize = true;
            this.lblIntensity.Location = new System.Drawing.Point(20, 44);
            this.lblIntensity.Name = "lblIntensity";
            this.lblIntensity.Size = new System.Drawing.Size(58, 13);
            this.lblIntensity.TabIndex = 1;
            this.lblIntensity.Text = "Intensity: 0";
            // 
            // chkNight
            // 
            this.chkNight.AutoSize = true;
            this.chkNight.Location = new System.Drawing.Point(23, 24);
            this.chkNight.Name = "chkNight";
            this.chkNight.Size = new System.Drawing.Size(51, 17);
            this.chkNight.TabIndex = 0;
            this.chkNight.Text = "Night";
            this.chkNight.UseVisualStyleBackColor = true;
            // 
            // tabHelp
            // 
            this.tabHelp.Controls.Add(this.label1);
            this.tabHelp.Location = new System.Drawing.Point(4, 22);
            this.tabHelp.Name = "tabHelp";
            this.tabHelp.Padding = new System.Windows.Forms.Padding(3);
            this.tabHelp.Size = new System.Drawing.Size(299, 589);
            this.tabHelp.TabIndex = 3;
            this.tabHelp.Text = "Help";
            this.tabHelp.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(273, 495);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
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
            // pnlMapNpcs
            // 
            this.pnlMapNpcs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMapNpcs.Controls.Add(this.cmbNpc10);
            this.pnlMapNpcs.Controls.Add(this.cmbNpc9);
            this.pnlMapNpcs.Controls.Add(this.cmbNpc8);
            this.pnlMapNpcs.Controls.Add(this.cmbNpc7);
            this.pnlMapNpcs.Controls.Add(this.cmbNpc6);
            this.pnlMapNpcs.Controls.Add(this.cmbNpc5);
            this.pnlMapNpcs.Controls.Add(this.cmbNpc4);
            this.pnlMapNpcs.Controls.Add(this.cmbNpc3);
            this.pnlMapNpcs.Controls.Add(this.cmbNpc2);
            this.pnlMapNpcs.Controls.Add(this.cmbNpc1);
            this.pnlMapNpcs.Controls.Add(this.btnCloseNpcs);
            this.pnlMapNpcs.Controls.Add(this.lblNpcs);
            this.pnlMapNpcs.Location = new System.Drawing.Point(593, 133);
            this.pnlMapNpcs.Name = "pnlMapNpcs";
            this.pnlMapNpcs.Size = new System.Drawing.Size(281, 345);
            this.pnlMapNpcs.TabIndex = 15;
            this.pnlMapNpcs.Visible = false;
            // 
            // cmbNpc10
            // 
            this.cmbNpc10.FormattingEnabled = true;
            this.cmbNpc10.Items.AddRange(new object[] {
            "None"});
            this.cmbNpc10.Location = new System.Drawing.Point(20, 282);
            this.cmbNpc10.Name = "cmbNpc10";
            this.cmbNpc10.Size = new System.Drawing.Size(242, 21);
            this.cmbNpc10.TabIndex = 11;
            this.cmbNpc10.SelectedIndexChanged += new System.EventHandler(this.cmbNpc10_SelectedIndexChanged);
            // 
            // cmbNpc9
            // 
            this.cmbNpc9.FormattingEnabled = true;
            this.cmbNpc9.Items.AddRange(new object[] {
            "None"});
            this.cmbNpc9.Location = new System.Drawing.Point(20, 255);
            this.cmbNpc9.Name = "cmbNpc9";
            this.cmbNpc9.Size = new System.Drawing.Size(242, 21);
            this.cmbNpc9.TabIndex = 10;
            this.cmbNpc9.SelectedIndexChanged += new System.EventHandler(this.cmbNpc9_SelectedIndexChanged);
            // 
            // cmbNpc8
            // 
            this.cmbNpc8.FormattingEnabled = true;
            this.cmbNpc8.Items.AddRange(new object[] {
            "None"});
            this.cmbNpc8.Location = new System.Drawing.Point(20, 228);
            this.cmbNpc8.Name = "cmbNpc8";
            this.cmbNpc8.Size = new System.Drawing.Size(242, 21);
            this.cmbNpc8.TabIndex = 9;
            this.cmbNpc8.SelectedIndexChanged += new System.EventHandler(this.cmbNpc8_SelectedIndexChanged);
            // 
            // cmbNpc7
            // 
            this.cmbNpc7.FormattingEnabled = true;
            this.cmbNpc7.Items.AddRange(new object[] {
            "None"});
            this.cmbNpc7.Location = new System.Drawing.Point(20, 201);
            this.cmbNpc7.Name = "cmbNpc7";
            this.cmbNpc7.Size = new System.Drawing.Size(242, 21);
            this.cmbNpc7.TabIndex = 8;
            this.cmbNpc7.SelectedIndexChanged += new System.EventHandler(this.cmbNpc7_SelectedIndexChanged);
            // 
            // cmbNpc6
            // 
            this.cmbNpc6.FormattingEnabled = true;
            this.cmbNpc6.Items.AddRange(new object[] {
            "None"});
            this.cmbNpc6.Location = new System.Drawing.Point(20, 174);
            this.cmbNpc6.Name = "cmbNpc6";
            this.cmbNpc6.Size = new System.Drawing.Size(242, 21);
            this.cmbNpc6.TabIndex = 7;
            this.cmbNpc6.SelectedIndexChanged += new System.EventHandler(this.cmbNpc6_SelectedIndexChanged);
            // 
            // cmbNpc5
            // 
            this.cmbNpc5.FormattingEnabled = true;
            this.cmbNpc5.Items.AddRange(new object[] {
            "None"});
            this.cmbNpc5.Location = new System.Drawing.Point(20, 147);
            this.cmbNpc5.Name = "cmbNpc5";
            this.cmbNpc5.Size = new System.Drawing.Size(242, 21);
            this.cmbNpc5.TabIndex = 6;
            this.cmbNpc5.SelectedIndexChanged += new System.EventHandler(this.cmbNpc5_SelectedIndexChanged);
            // 
            // cmbNpc4
            // 
            this.cmbNpc4.FormattingEnabled = true;
            this.cmbNpc4.Items.AddRange(new object[] {
            "None"});
            this.cmbNpc4.Location = new System.Drawing.Point(20, 120);
            this.cmbNpc4.Name = "cmbNpc4";
            this.cmbNpc4.Size = new System.Drawing.Size(242, 21);
            this.cmbNpc4.TabIndex = 5;
            this.cmbNpc4.SelectedIndexChanged += new System.EventHandler(this.cmbNpc4_SelectedIndexChanged);
            // 
            // cmbNpc3
            // 
            this.cmbNpc3.FormattingEnabled = true;
            this.cmbNpc3.Items.AddRange(new object[] {
            "None"});
            this.cmbNpc3.Location = new System.Drawing.Point(20, 91);
            this.cmbNpc3.Name = "cmbNpc3";
            this.cmbNpc3.Size = new System.Drawing.Size(242, 21);
            this.cmbNpc3.TabIndex = 4;
            this.cmbNpc3.SelectedIndexChanged += new System.EventHandler(this.cmbNpc3_SelectedIndexChanged);
            // 
            // cmbNpc2
            // 
            this.cmbNpc2.FormattingEnabled = true;
            this.cmbNpc2.Items.AddRange(new object[] {
            "None"});
            this.cmbNpc2.Location = new System.Drawing.Point(20, 64);
            this.cmbNpc2.Name = "cmbNpc2";
            this.cmbNpc2.Size = new System.Drawing.Size(242, 21);
            this.cmbNpc2.TabIndex = 3;
            this.cmbNpc2.SelectedIndexChanged += new System.EventHandler(this.cmbNpc2_SelectedIndexChanged);
            // 
            // cmbNpc1
            // 
            this.cmbNpc1.FormattingEnabled = true;
            this.cmbNpc1.Items.AddRange(new object[] {
            "None"});
            this.cmbNpc1.Location = new System.Drawing.Point(20, 37);
            this.cmbNpc1.Name = "cmbNpc1";
            this.cmbNpc1.Size = new System.Drawing.Size(242, 21);
            this.cmbNpc1.TabIndex = 2;
            this.cmbNpc1.SelectedIndexChanged += new System.EventHandler(this.cmbNpc1_SelectedIndexChanged);
            // 
            // btnCloseNpcs
            // 
            this.btnCloseNpcs.Location = new System.Drawing.Point(187, 309);
            this.btnCloseNpcs.Name = "btnCloseNpcs";
            this.btnCloseNpcs.Size = new System.Drawing.Size(75, 23);
            this.btnCloseNpcs.TabIndex = 1;
            this.btnCloseNpcs.Text = "Close";
            this.btnCloseNpcs.UseVisualStyleBackColor = true;
            this.btnCloseNpcs.Click += new System.EventHandler(this.btnCloseNpcs_Click);
            // 
            // lblNpcs
            // 
            this.lblNpcs.AutoSize = true;
            this.lblNpcs.Location = new System.Drawing.Point(17, 19);
            this.lblNpcs.Name = "lblNpcs";
            this.lblNpcs.Size = new System.Drawing.Size(68, 13);
            this.lblNpcs.TabIndex = 0;
            this.lblNpcs.Text = "Select Npcs:";
            // 
            // scrlViewX
            // 
            this.scrlViewX.LargeChange = 1;
            this.scrlViewX.Location = new System.Drawing.Point(321, 634);
            this.scrlViewX.Maximum = 25;
            this.scrlViewX.Name = "scrlViewX";
            this.scrlViewX.Size = new System.Drawing.Size(800, 17);
            this.scrlViewX.TabIndex = 16;
            this.scrlViewX.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlViewX_Scroll);
            // 
            // scrlViewY
            // 
            this.scrlViewY.LargeChange = 1;
            this.scrlViewY.Location = new System.Drawing.Point(1124, 31);
            this.scrlViewY.Maximum = 31;
            this.scrlViewY.Name = "scrlViewY";
            this.scrlViewY.Size = new System.Drawing.Size(17, 600);
            this.scrlViewY.TabIndex = 17;
            this.scrlViewY.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlViewY_Scroll);
            // 
            // treeMaps
            // 
            this.treeMaps.Location = new System.Drawing.Point(1144, 36);
            this.treeMaps.Name = "treeMaps";
            this.treeMaps.Size = new System.Drawing.Size(241, 414);
            this.treeMaps.TabIndex = 18;
            this.treeMaps.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeMaps_AfterSelect);
            // 
            // mapProperties
            // 
            this.mapProperties.Location = new System.Drawing.Point(1144, 456);
            this.mapProperties.Name = "mapProperties";
            this.mapProperties.Size = new System.Drawing.Size(241, 195);
            this.mapProperties.TabIndex = 19;
            // 
            // tosMenu
            // 
            this.tosMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNewMap,
            this.btnSaveMap,
            this.toolStripSeparator,
            this.btnMapNpcs,
            this.toolStripSeparator1,
            this.btnFillMap,
            this.btnDeleteLayer,
            this.toolStripSeparator2,
            this.btnDebug,
            this.btnHelp});
            this.tosMenu.Location = new System.Drawing.Point(0, 0);
            this.tosMenu.Name = "tosMenu";
            this.tosMenu.Size = new System.Drawing.Size(1397, 25);
            this.tosMenu.TabIndex = 20;
            this.tosMenu.Text = "toolStrip1";
            // 
            // btnNewMap
            // 
            this.btnNewMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNewMap.Image = ((System.Drawing.Image)(resources.GetObject("btnNewMap.Image")));
            this.btnNewMap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewMap.Name = "btnNewMap";
            this.btnNewMap.Size = new System.Drawing.Size(23, 22);
            this.btnNewMap.Text = "New Map";
            this.btnNewMap.Click += new System.EventHandler(this.btnNewMap_Click);
            // 
            // btnSaveMap
            // 
            this.btnSaveMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveMap.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveMap.Image")));
            this.btnSaveMap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveMap.Name = "btnSaveMap";
            this.btnSaveMap.Size = new System.Drawing.Size(23, 22);
            this.btnSaveMap.Text = "Save Map";
            this.btnSaveMap.Click += new System.EventHandler(this.saveToolStripButton_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // btnMapNpcs
            // 
            this.btnMapNpcs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMapNpcs.Image = ((System.Drawing.Image)(resources.GetObject("btnMapNpcs.Image")));
            this.btnMapNpcs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMapNpcs.Name = "btnMapNpcs";
            this.btnMapNpcs.Size = new System.Drawing.Size(23, 22);
            this.btnMapNpcs.Text = "Map Npcs";
            this.btnMapNpcs.Click += new System.EventHandler(this.btnMapNpcs_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnFillMap
            // 
            this.btnFillMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFillMap.Image = ((System.Drawing.Image)(resources.GetObject("btnFillMap.Image")));
            this.btnFillMap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFillMap.Name = "btnFillMap";
            this.btnFillMap.Size = new System.Drawing.Size(23, 22);
            this.btnFillMap.Text = "Fill Current Layer";
            this.btnFillMap.Click += new System.EventHandler(this.btnFillMap_Click);
            // 
            // btnDeleteLayer
            // 
            this.btnDeleteLayer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDeleteLayer.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteLayer.Image")));
            this.btnDeleteLayer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteLayer.Name = "btnDeleteLayer";
            this.btnDeleteLayer.Size = new System.Drawing.Size(23, 22);
            this.btnDeleteLayer.Text = "Delete Layer";
            this.btnDeleteLayer.Click += new System.EventHandler(this.btnDeleteLayer_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnDebug
            // 
            this.btnDebug.CheckOnClick = true;
            this.btnDebug.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDebug.Image = ((System.Drawing.Image)(resources.GetObject("btnDebug.Image")));
            this.btnDebug.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDebug.Name = "btnDebug";
            this.btnDebug.Size = new System.Drawing.Size(23, 22);
            this.btnDebug.Text = "Debug Window";
            this.btnDebug.Click += new System.EventHandler(this.btnDebug_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnHelp.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp.Image")));
            this.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(23, 22);
            this.btnHelp.Text = "Help";
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // MapEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1397, 663);
            this.Controls.Add(this.tosMenu);
            this.Controls.Add(this.mapProperties);
            this.Controls.Add(this.treeMaps);
            this.Controls.Add(this.scrlViewY);
            this.Controls.Add(this.scrlViewX);
            this.Controls.Add(this.pnlMapNpcs);
            this.Controls.Add(this.picMap);
            this.Controls.Add(this.tabTools);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.HelpButton = true;
            this.Name = "MapEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Map Editor";
            this.tabTools.ResumeLayout(false);
            this.tabLayer.ResumeLayout(false);
            this.tabLayer.PerformLayout();
            this.pnlOptions.ResumeLayout(false);
            this.pnlOptions.PerformLayout();
            this.pnlDebug.ResumeLayout(false);
            this.pnlDebug.PerformLayout();
            this.tabTypes.ResumeLayout(false);
            this.tabTypes.PerformLayout();
            this.pnlChest.ResumeLayout(false);
            this.pnlChest.PerformLayout();
            this.pnlMapItem.ResumeLayout(false);
            this.pnlMapItem.PerformLayout();
            this.pnlNpcSpawn.ResumeLayout(false);
            this.pnlNpcSpawn.PerformLayout();
            this.tabTiles.ResumeLayout(false);
            this.tabTiles.PerformLayout();
            this.pnlTile.ResumeLayout(false);
            this.pnlTile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTileset)).EndInit();
            this.tabLight.ResumeLayout(false);
            this.tabLight.PerformLayout();
            this.tabHelp.ResumeLayout(false);
            this.tabHelp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).EndInit();
            this.pnlMapNpcs.ResumeLayout(false);
            this.pnlMapNpcs.PerformLayout();
            this.tosMenu.ResumeLayout(false);
            this.tosMenu.PerformLayout();
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
        private System.Windows.Forms.Panel pnlMapNpcs;
        private System.Windows.Forms.ComboBox cmbNpc10;
        private System.Windows.Forms.ComboBox cmbNpc9;
        private System.Windows.Forms.ComboBox cmbNpc8;
        private System.Windows.Forms.ComboBox cmbNpc7;
        private System.Windows.Forms.ComboBox cmbNpc6;
        private System.Windows.Forms.ComboBox cmbNpc5;
        private System.Windows.Forms.ComboBox cmbNpc4;
        private System.Windows.Forms.ComboBox cmbNpc3;
        private System.Windows.Forms.ComboBox cmbNpc2;
        private System.Windows.Forms.ComboBox cmbNpc1;
        private System.Windows.Forms.Button btnCloseNpcs;
        private System.Windows.Forms.Label lblNpcs;
        private System.Windows.Forms.HScrollBar scrlViewX;
        private System.Windows.Forms.VScrollBar scrlViewY;
        private System.Windows.Forms.CheckBox chkNpc;
        private System.Windows.Forms.RadioButton radSpawnPool;
        private System.Windows.Forms.HScrollBar scrlSpawnAmount;
        private System.Windows.Forms.Label lblSpawnAmount;
        private System.Windows.Forms.RadioButton radNpcAvoid;
        private System.Windows.Forms.Panel pnlMapItem;
        private System.Windows.Forms.HScrollBar scrlItemAmount;
        private System.Windows.Forms.Label lblItemAmount;
        private System.Windows.Forms.HScrollBar scrlItemNum;
        private System.Windows.Forms.Label lblItemNum;
        private System.Windows.Forms.RadioButton radMapItem;
        private System.Windows.Forms.TreeView treeMaps;
        private System.Windows.Forms.PropertyGrid mapProperties;
        private System.Windows.Forms.ToolStrip tosMenu;
        private System.Windows.Forms.ToolStripButton btnNewMap;
        private System.Windows.Forms.ToolStripButton btnSaveMap;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnMapNpcs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnFillMap;
        private System.Windows.Forms.ToolStripButton btnDebug;
        private System.Windows.Forms.TabPage tabHelp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnHelp;
        private System.Windows.Forms.ToolStripButton btnDeleteLayer;
        private System.Windows.Forms.Panel pnlChest;
        private System.Windows.Forms.HScrollBar scrlChest;
        private System.Windows.Forms.Label lblChest;
        private System.Windows.Forms.RadioButton radChest;
        private System.Windows.Forms.TabPage tabLight;
        private System.Windows.Forms.CheckBox chkNight;
        private System.Windows.Forms.HScrollBar scrlIntensity;
        private System.Windows.Forms.Label lblIntensity;
        private System.Windows.Forms.Panel pnlOptions;
        private System.Windows.Forms.RadioButton radScroll;
        private System.Windows.Forms.RadioButton radZoom;
        private System.Windows.Forms.CheckBox chkHScroll;
    }
}