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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapEditor));
            this.tabTools = new System.Windows.Forms.TabControl();
            this.tabLayer = new System.Windows.Forms.TabPage();
            this.pnlTile = new System.Windows.Forms.Panel();
            this.picTileset = new System.Windows.Forms.PictureBox();
            this.cmbTileset = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRestruct = new System.Windows.Forms.Button();
            this.txtMaxY = new System.Windows.Forms.TextBox();
            this.txtMaxX = new System.Windows.Forms.TextBox();
            this.lblMaxY = new System.Windows.Forms.Label();
            this.lblMaxX = new System.Windows.Forms.Label();
            this.pnlOptions = new System.Windows.Forms.Panel();
            this.chkHScroll = new System.Windows.Forms.CheckBox();
            this.radScroll = new System.Windows.Forms.RadioButton();
            this.radZoom = new System.Windows.Forms.RadioButton();
            this.chkNpc = new System.Windows.Forms.CheckBox();
            this.chkGrid = new System.Windows.Forms.CheckBox();
            this.radFringe = new System.Windows.Forms.RadioButton();
            this.radFringe2 = new System.Windows.Forms.RadioButton();
            this.radMask2 = new System.Windows.Forms.RadioButton();
            this.radMask = new System.Windows.Forms.RadioButton();
            this.radGround = new System.Windows.Forms.RadioButton();
            this.tabTypes = new System.Windows.Forms.TabPage();
            this.pnlWarp = new System.Windows.Forms.Panel();
            this.scrlMapY = new System.Windows.Forms.HScrollBar();
            this.scrlMapX = new System.Windows.Forms.HScrollBar();
            this.scrlMapNum = new System.Windows.Forms.HScrollBar();
            this.lblMapY = new System.Windows.Forms.Label();
            this.lblMapX = new System.Windows.Forms.Label();
            this.lblMapNum = new System.Windows.Forms.Label();
            this.radWarp = new System.Windows.Forms.RadioButton();
            this.pnlChest = new System.Windows.Forms.Panel();
            this.scrlChest = new System.Windows.Forms.HScrollBar();
            this.lblChest = new System.Windows.Forms.Label();
            this.pnlMapItem = new System.Windows.Forms.Panel();
            this.picItem = new System.Windows.Forms.PictureBox();
            this.scrlItemAmount = new System.Windows.Forms.HScrollBar();
            this.lblItemAmount = new System.Windows.Forms.Label();
            this.scrlItemNum = new System.Windows.Forms.HScrollBar();
            this.lblItemNum = new System.Windows.Forms.Label();
            this.radChest = new System.Windows.Forms.RadioButton();
            this.radMapItem = new System.Windows.Forms.RadioButton();
            this.radNpcAvoid = new System.Windows.Forms.RadioButton();
            this.radSpawnPool = new System.Windows.Forms.RadioButton();
            this.radSpawnNpc = new System.Windows.Forms.RadioButton();
            this.radBlocked = new System.Windows.Forms.RadioButton();
            this.radNone = new System.Windows.Forms.RadioButton();
            this.pnlNpcSpawn = new System.Windows.Forms.Panel();
            this.picSprite = new System.Windows.Forms.PictureBox();
            this.scrlSpawnAmount = new System.Windows.Forms.HScrollBar();
            this.lblSpawnAmount = new System.Windows.Forms.Label();
            this.scrlNpcNum = new System.Windows.Forms.HScrollBar();
            this.lblNpcSpawn = new System.Windows.Forms.Label();
            this.tabLight = new System.Windows.Forms.TabPage();
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
            this.lblNpcs = new System.Windows.Forms.Label();
            this.scrlIntensity = new System.Windows.Forms.HScrollBar();
            this.lblIntensity = new System.Windows.Forms.Label();
            this.chkNight = new System.Windows.Forms.CheckBox();
            this.picMap = new System.Windows.Forms.PictureBox();
            this.scrlViewX = new System.Windows.Forms.HScrollBar();
            this.scrlViewY = new System.Windows.Forms.VScrollBar();
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
            this.treeMaps = new System.Windows.Forms.TreeView();
            this.mapProperties = new System.Windows.Forms.PropertyGrid();
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
            this.tabTools.SuspendLayout();
            this.tabLayer.SuspendLayout();
            this.pnlTile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTileset)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnlOptions.SuspendLayout();
            this.tabTypes.SuspendLayout();
            this.pnlWarp.SuspendLayout();
            this.pnlChest.SuspendLayout();
            this.pnlMapItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picItem)).BeginInit();
            this.pnlNpcSpawn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSprite)).BeginInit();
            this.tabLight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).BeginInit();
            this.tosMenu.SuspendLayout();
            this.pnlDebug.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabTools
            // 
            this.tabTools.Controls.Add(this.tabLayer);
            this.tabTools.Controls.Add(this.tabTypes);
            this.tabTools.Controls.Add(this.tabLight);
            this.tabTools.Location = new System.Drawing.Point(11, 31);
            this.tabTools.Name = "tabTools";
            this.tabTools.SelectedIndex = 0;
            this.tabTools.Size = new System.Drawing.Size(322, 726);
            this.tabTools.TabIndex = 1;
            // 
            // tabLayer
            // 
            this.tabLayer.Controls.Add(this.pnlTile);
            this.tabLayer.Controls.Add(this.cmbTileset);
            this.tabLayer.Controls.Add(this.panel1);
            this.tabLayer.Controls.Add(this.pnlOptions);
            this.tabLayer.Controls.Add(this.chkNpc);
            this.tabLayer.Controls.Add(this.chkGrid);
            this.tabLayer.Controls.Add(this.radFringe);
            this.tabLayer.Controls.Add(this.radFringe2);
            this.tabLayer.Controls.Add(this.radMask2);
            this.tabLayer.Controls.Add(this.radMask);
            this.tabLayer.Controls.Add(this.radGround);
            this.tabLayer.Location = new System.Drawing.Point(4, 22);
            this.tabLayer.Name = "tabLayer";
            this.tabLayer.Padding = new System.Windows.Forms.Padding(3);
            this.tabLayer.Size = new System.Drawing.Size(314, 700);
            this.tabLayer.TabIndex = 0;
            this.tabLayer.Text = "Layer";
            this.tabLayer.UseVisualStyleBackColor = true;
            // 
            // pnlTile
            // 
            this.pnlTile.AutoScroll = true;
            this.pnlTile.Controls.Add(this.picTileset);
            this.pnlTile.Location = new System.Drawing.Point(11, 169);
            this.pnlTile.Name = "pnlTile";
            this.pnlTile.Size = new System.Drawing.Size(293, 527);
            this.pnlTile.TabIndex = 26;
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
            // cmbTileset
            // 
            this.cmbTileset.FormattingEnabled = true;
            this.cmbTileset.Location = new System.Drawing.Point(11, 142);
            this.cmbTileset.Name = "cmbTileset";
            this.cmbTileset.Size = new System.Drawing.Size(293, 21);
            this.cmbTileset.TabIndex = 24;
            this.cmbTileset.SelectedIndexChanged += new System.EventHandler(this.cmbTileset_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnRestruct);
            this.panel1.Controls.Add(this.txtMaxY);
            this.panel1.Controls.Add(this.txtMaxX);
            this.panel1.Controls.Add(this.lblMaxY);
            this.panel1.Controls.Add(this.lblMaxX);
            this.panel1.Location = new System.Drawing.Point(111, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(106, 123);
            this.panel1.TabIndex = 16;
            // 
            // btnRestruct
            // 
            this.btnRestruct.Location = new System.Drawing.Point(14, 91);
            this.btnRestruct.Name = "btnRestruct";
            this.btnRestruct.Size = new System.Drawing.Size(75, 23);
            this.btnRestruct.TabIndex = 4;
            this.btnRestruct.Text = "Restructure";
            this.btnRestruct.UseVisualStyleBackColor = true;
            this.btnRestruct.Click += new System.EventHandler(this.btnRestruct_Click);
            // 
            // txtMaxY
            // 
            this.txtMaxY.Location = new System.Drawing.Point(14, 66);
            this.txtMaxY.Name = "txtMaxY";
            this.txtMaxY.Size = new System.Drawing.Size(75, 20);
            this.txtMaxY.TabIndex = 3;
            // 
            // txtMaxX
            // 
            this.txtMaxX.Location = new System.Drawing.Point(14, 24);
            this.txtMaxX.Name = "txtMaxX";
            this.txtMaxX.Size = new System.Drawing.Size(75, 20);
            this.txtMaxX.TabIndex = 2;
            // 
            // lblMaxY
            // 
            this.lblMaxY.AutoSize = true;
            this.lblMaxY.Location = new System.Drawing.Point(11, 50);
            this.lblMaxY.Name = "lblMaxY";
            this.lblMaxY.Size = new System.Drawing.Size(40, 13);
            this.lblMaxY.TabIndex = 1;
            this.lblMaxY.Text = "Max Y:";
            // 
            // lblMaxX
            // 
            this.lblMaxX.AutoSize = true;
            this.lblMaxX.Location = new System.Drawing.Point(11, 8);
            this.lblMaxX.Name = "lblMaxX";
            this.lblMaxX.Size = new System.Drawing.Size(40, 13);
            this.lblMaxX.TabIndex = 0;
            this.lblMaxX.Text = "Max X:";
            // 
            // pnlOptions
            // 
            this.pnlOptions.Controls.Add(this.chkHScroll);
            this.pnlOptions.Controls.Add(this.radScroll);
            this.pnlOptions.Controls.Add(this.radZoom);
            this.pnlOptions.Location = new System.Drawing.Point(223, 3);
            this.pnlOptions.Name = "pnlOptions";
            this.pnlOptions.Size = new System.Drawing.Size(81, 76);
            this.pnlOptions.TabIndex = 15;
            // 
            // chkHScroll
            // 
            this.chkHScroll.AutoSize = true;
            this.chkHScroll.Location = new System.Drawing.Point(11, 32);
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
            this.radScroll.Checked = true;
            this.radScroll.Location = new System.Drawing.Point(11, 9);
            this.radScroll.Name = "radScroll";
            this.radScroll.Size = new System.Drawing.Size(51, 17);
            this.radScroll.TabIndex = 1;
            this.radScroll.TabStop = true;
            this.radScroll.Text = "Scroll";
            this.radScroll.UseVisualStyleBackColor = true;
            this.radScroll.CheckedChanged += new System.EventHandler(this.radScroll_CheckedChanged);
            // 
            // radZoom
            // 
            this.radZoom.AutoSize = true;
            this.radZoom.Location = new System.Drawing.Point(11, 55);
            this.radZoom.Name = "radZoom";
            this.radZoom.Size = new System.Drawing.Size(52, 17);
            this.radZoom.TabIndex = 0;
            this.radZoom.Text = "Zoom";
            this.radZoom.UseVisualStyleBackColor = true;
            this.radZoom.CheckedChanged += new System.EventHandler(this.radZoom_CheckedChanged);
            // 
            // chkNpc
            // 
            this.chkNpc.AutoSize = true;
            this.chkNpc.Location = new System.Drawing.Point(225, 85);
            this.chkNpc.Name = "chkNpc";
            this.chkNpc.Size = new System.Drawing.Size(79, 17);
            this.chkNpc.TabIndex = 14;
            this.chkNpc.Text = "Show npcs";
            this.chkNpc.UseVisualStyleBackColor = true;
            // 
            // chkGrid
            // 
            this.chkGrid.AutoSize = true;
            this.chkGrid.Location = new System.Drawing.Point(225, 108);
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
            this.tabTypes.Controls.Add(this.pnlWarp);
            this.tabTypes.Controls.Add(this.radWarp);
            this.tabTypes.Controls.Add(this.pnlChest);
            this.tabTypes.Controls.Add(this.pnlMapItem);
            this.tabTypes.Controls.Add(this.radChest);
            this.tabTypes.Controls.Add(this.radMapItem);
            this.tabTypes.Controls.Add(this.radNpcAvoid);
            this.tabTypes.Controls.Add(this.radSpawnPool);
            this.tabTypes.Controls.Add(this.radSpawnNpc);
            this.tabTypes.Controls.Add(this.radBlocked);
            this.tabTypes.Controls.Add(this.radNone);
            this.tabTypes.Controls.Add(this.pnlNpcSpawn);
            this.tabTypes.Location = new System.Drawing.Point(4, 22);
            this.tabTypes.Name = "tabTypes";
            this.tabTypes.Padding = new System.Windows.Forms.Padding(3);
            this.tabTypes.Size = new System.Drawing.Size(314, 769);
            this.tabTypes.TabIndex = 1;
            this.tabTypes.Text = "Types";
            this.tabTypes.UseVisualStyleBackColor = true;
            // 
            // pnlWarp
            // 
            this.pnlWarp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlWarp.Controls.Add(this.scrlMapY);
            this.pnlWarp.Controls.Add(this.scrlMapX);
            this.pnlWarp.Controls.Add(this.scrlMapNum);
            this.pnlWarp.Controls.Add(this.lblMapY);
            this.pnlWarp.Controls.Add(this.lblMapX);
            this.pnlWarp.Controls.Add(this.lblMapNum);
            this.pnlWarp.Location = new System.Drawing.Point(126, 391);
            this.pnlWarp.Name = "pnlWarp";
            this.pnlWarp.Size = new System.Drawing.Size(167, 192);
            this.pnlWarp.TabIndex = 11;
            this.pnlWarp.Visible = false;
            // 
            // scrlMapY
            // 
            this.scrlMapY.Location = new System.Drawing.Point(22, 137);
            this.scrlMapY.Maximum = 50;
            this.scrlMapY.Name = "scrlMapY";
            this.scrlMapY.Size = new System.Drawing.Size(120, 17);
            this.scrlMapY.TabIndex = 5;
            this.scrlMapY.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlMapY_Scroll);
            // 
            // scrlMapX
            // 
            this.scrlMapX.Location = new System.Drawing.Point(22, 92);
            this.scrlMapX.Maximum = 50;
            this.scrlMapX.Name = "scrlMapX";
            this.scrlMapX.Size = new System.Drawing.Size(120, 17);
            this.scrlMapX.TabIndex = 4;
            this.scrlMapX.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlMapX_Scroll);
            // 
            // scrlMapNum
            // 
            this.scrlMapNum.LargeChange = 1;
            this.scrlMapNum.Location = new System.Drawing.Point(22, 48);
            this.scrlMapNum.Minimum = 1;
            this.scrlMapNum.Name = "scrlMapNum";
            this.scrlMapNum.Size = new System.Drawing.Size(121, 17);
            this.scrlMapNum.TabIndex = 3;
            this.scrlMapNum.Value = 1;
            this.scrlMapNum.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlMapNum_Scroll);
            // 
            // lblMapY
            // 
            this.lblMapY.AutoSize = true;
            this.lblMapY.Location = new System.Drawing.Point(19, 120);
            this.lblMapY.Name = "lblMapY";
            this.lblMapY.Size = new System.Drawing.Size(26, 13);
            this.lblMapY.TabIndex = 2;
            this.lblMapY.Text = "Y: 0";
            // 
            // lblMapX
            // 
            this.lblMapX.AutoSize = true;
            this.lblMapX.Location = new System.Drawing.Point(19, 75);
            this.lblMapX.Name = "lblMapX";
            this.lblMapX.Size = new System.Drawing.Size(26, 13);
            this.lblMapX.TabIndex = 1;
            this.lblMapX.Text = "X: 0";
            // 
            // lblMapNum
            // 
            this.lblMapNum.AutoSize = true;
            this.lblMapNum.Location = new System.Drawing.Point(19, 29);
            this.lblMapNum.Name = "lblMapNum";
            this.lblMapNum.Size = new System.Drawing.Size(75, 13);
            this.lblMapNum.TabIndex = 0;
            this.lblMapNum.Text = "Map: 0 - None";
            // 
            // radWarp
            // 
            this.radWarp.AutoSize = true;
            this.radWarp.Location = new System.Drawing.Point(20, 182);
            this.radWarp.Name = "radWarp";
            this.radWarp.Size = new System.Drawing.Size(51, 17);
            this.radWarp.TabIndex = 10;
            this.radWarp.TabStop = true;
            this.radWarp.Text = "Warp";
            this.radWarp.UseVisualStyleBackColor = true;
            this.radWarp.CheckedChanged += new System.EventHandler(this.radWarp_CheckedChanged);
            // 
            // pnlChest
            // 
            this.pnlChest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlChest.Controls.Add(this.scrlChest);
            this.pnlChest.Controls.Add(this.lblChest);
            this.pnlChest.Location = new System.Drawing.Point(126, 317);
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
            this.lblChest.Size = new System.Drawing.Size(81, 13);
            this.lblChest.TabIndex = 0;
            this.lblChest.Text = "Chest: 0 - None";
            // 
            // pnlMapItem
            // 
            this.pnlMapItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMapItem.Controls.Add(this.picItem);
            this.pnlMapItem.Controls.Add(this.scrlItemAmount);
            this.pnlMapItem.Controls.Add(this.lblItemAmount);
            this.pnlMapItem.Controls.Add(this.scrlItemNum);
            this.pnlMapItem.Controls.Add(this.lblItemNum);
            this.pnlMapItem.Location = new System.Drawing.Point(126, 182);
            this.pnlMapItem.Name = "pnlMapItem";
            this.pnlMapItem.Size = new System.Drawing.Size(167, 129);
            this.pnlMapItem.TabIndex = 6;
            this.pnlMapItem.Visible = false;
            // 
            // picItem
            // 
            this.picItem.BackColor = System.Drawing.Color.Black;
            this.picItem.Location = new System.Drawing.Point(19, 12);
            this.picItem.Name = "picItem";
            this.picItem.Size = new System.Drawing.Size(32, 32);
            this.picItem.TabIndex = 4;
            this.picItem.TabStop = false;
            // 
            // scrlItemAmount
            // 
            this.scrlItemAmount.LargeChange = 100;
            this.scrlItemAmount.Location = new System.Drawing.Point(19, 99);
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
            this.lblItemAmount.Location = new System.Drawing.Point(16, 86);
            this.lblItemAmount.Name = "lblItemAmount";
            this.lblItemAmount.Size = new System.Drawing.Size(55, 13);
            this.lblItemAmount.TabIndex = 2;
            this.lblItemAmount.Text = "Amount: 1";
            // 
            // scrlItemNum
            // 
            this.scrlItemNum.LargeChange = 1;
            this.scrlItemNum.Location = new System.Drawing.Point(19, 63);
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
            this.lblItemNum.Location = new System.Drawing.Point(16, 47);
            this.lblItemNum.Name = "lblItemNum";
            this.lblItemNum.Size = new System.Drawing.Size(74, 13);
            this.lblItemNum.TabIndex = 0;
            this.lblItemNum.Text = "Item: 0 - None";
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
            // pnlNpcSpawn
            // 
            this.pnlNpcSpawn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlNpcSpawn.Controls.Add(this.picSprite);
            this.pnlNpcSpawn.Controls.Add(this.scrlSpawnAmount);
            this.pnlNpcSpawn.Controls.Add(this.lblSpawnAmount);
            this.pnlNpcSpawn.Controls.Add(this.scrlNpcNum);
            this.pnlNpcSpawn.Controls.Add(this.lblNpcSpawn);
            this.pnlNpcSpawn.Location = new System.Drawing.Point(126, 11);
            this.pnlNpcSpawn.Name = "pnlNpcSpawn";
            this.pnlNpcSpawn.Size = new System.Drawing.Size(167, 165);
            this.pnlNpcSpawn.TabIndex = 3;
            this.pnlNpcSpawn.Visible = false;
            // 
            // picSprite
            // 
            this.picSprite.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.picSprite.Location = new System.Drawing.Point(19, 8);
            this.picSprite.Name = "picSprite";
            this.picSprite.Size = new System.Drawing.Size(32, 48);
            this.picSprite.TabIndex = 10;
            this.picSprite.TabStop = false;
            // 
            // scrlSpawnAmount
            // 
            this.scrlSpawnAmount.Enabled = false;
            this.scrlSpawnAmount.LargeChange = 1;
            this.scrlSpawnAmount.Location = new System.Drawing.Point(19, 125);
            this.scrlSpawnAmount.Maximum = 20;
            this.scrlSpawnAmount.Minimum = 1;
            this.scrlSpawnAmount.Name = "scrlSpawnAmount";
            this.scrlSpawnAmount.Size = new System.Drawing.Size(123, 17);
            this.scrlSpawnAmount.TabIndex = 3;
            this.scrlSpawnAmount.Value = 1;
            this.scrlSpawnAmount.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlSpawnAmount_Scroll);
            // 
            // lblSpawnAmount
            // 
            this.lblSpawnAmount.AutoSize = true;
            this.lblSpawnAmount.Location = new System.Drawing.Point(16, 112);
            this.lblSpawnAmount.Name = "lblSpawnAmount";
            this.lblSpawnAmount.Size = new System.Drawing.Size(55, 13);
            this.lblSpawnAmount.TabIndex = 2;
            this.lblSpawnAmount.Text = "Amount: 1";
            // 
            // scrlNpcNum
            // 
            this.scrlNpcNum.LargeChange = 1;
            this.scrlNpcNum.Location = new System.Drawing.Point(19, 84);
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
            this.lblNpcSpawn.Location = new System.Drawing.Point(16, 67);
            this.lblNpcSpawn.Name = "lblNpcSpawn";
            this.lblNpcSpawn.Size = new System.Drawing.Size(74, 13);
            this.lblNpcSpawn.TabIndex = 0;
            this.lblNpcSpawn.Text = "Npc: 0 - None";
            // 
            // tabLight
            // 
            this.tabLight.Controls.Add(this.pnlDebug);
            this.tabLight.Controls.Add(this.cmbNpc10);
            this.tabLight.Controls.Add(this.cmbNpc9);
            this.tabLight.Controls.Add(this.cmbNpc8);
            this.tabLight.Controls.Add(this.cmbNpc7);
            this.tabLight.Controls.Add(this.cmbNpc6);
            this.tabLight.Controls.Add(this.cmbNpc5);
            this.tabLight.Controls.Add(this.cmbNpc4);
            this.tabLight.Controls.Add(this.cmbNpc3);
            this.tabLight.Controls.Add(this.cmbNpc2);
            this.tabLight.Controls.Add(this.cmbNpc1);
            this.tabLight.Controls.Add(this.lblNpcs);
            this.tabLight.Controls.Add(this.scrlIntensity);
            this.tabLight.Controls.Add(this.lblIntensity);
            this.tabLight.Controls.Add(this.chkNight);
            this.tabLight.Location = new System.Drawing.Point(4, 22);
            this.tabLight.Name = "tabLight";
            this.tabLight.Size = new System.Drawing.Size(314, 700);
            this.tabLight.TabIndex = 4;
            this.tabLight.Text = "Light/NPCs";
            this.tabLight.UseVisualStyleBackColor = true;
            // 
            // cmbNpc10
            // 
            this.cmbNpc10.FormattingEnabled = true;
            this.cmbNpc10.Items.AddRange(new object[] {
            "None"});
            this.cmbNpc10.Location = new System.Drawing.Point(23, 360);
            this.cmbNpc10.Name = "cmbNpc10";
            this.cmbNpc10.Size = new System.Drawing.Size(252, 21);
            this.cmbNpc10.TabIndex = 22;
            this.cmbNpc10.SelectedIndexChanged += new System.EventHandler(this.cmbNpc10_SelectedIndexChanged);
            // 
            // cmbNpc9
            // 
            this.cmbNpc9.FormattingEnabled = true;
            this.cmbNpc9.Items.AddRange(new object[] {
            "None"});
            this.cmbNpc9.Location = new System.Drawing.Point(23, 333);
            this.cmbNpc9.Name = "cmbNpc9";
            this.cmbNpc9.Size = new System.Drawing.Size(252, 21);
            this.cmbNpc9.TabIndex = 21;
            this.cmbNpc9.SelectedIndexChanged += new System.EventHandler(this.cmbNpc9_SelectedIndexChanged);
            // 
            // cmbNpc8
            // 
            this.cmbNpc8.FormattingEnabled = true;
            this.cmbNpc8.Items.AddRange(new object[] {
            "None"});
            this.cmbNpc8.Location = new System.Drawing.Point(23, 306);
            this.cmbNpc8.Name = "cmbNpc8";
            this.cmbNpc8.Size = new System.Drawing.Size(252, 21);
            this.cmbNpc8.TabIndex = 20;
            this.cmbNpc8.SelectedIndexChanged += new System.EventHandler(this.cmbNpc8_SelectedIndexChanged);
            // 
            // cmbNpc7
            // 
            this.cmbNpc7.FormattingEnabled = true;
            this.cmbNpc7.Items.AddRange(new object[] {
            "None"});
            this.cmbNpc7.Location = new System.Drawing.Point(23, 279);
            this.cmbNpc7.Name = "cmbNpc7";
            this.cmbNpc7.Size = new System.Drawing.Size(252, 21);
            this.cmbNpc7.TabIndex = 19;
            this.cmbNpc7.SelectedIndexChanged += new System.EventHandler(this.cmbNpc7_SelectedIndexChanged);
            // 
            // cmbNpc6
            // 
            this.cmbNpc6.FormattingEnabled = true;
            this.cmbNpc6.Items.AddRange(new object[] {
            "None"});
            this.cmbNpc6.Location = new System.Drawing.Point(23, 252);
            this.cmbNpc6.Name = "cmbNpc6";
            this.cmbNpc6.Size = new System.Drawing.Size(252, 21);
            this.cmbNpc6.TabIndex = 18;
            this.cmbNpc6.SelectedIndexChanged += new System.EventHandler(this.cmbNpc6_SelectedIndexChanged);
            // 
            // cmbNpc5
            // 
            this.cmbNpc5.FormattingEnabled = true;
            this.cmbNpc5.Items.AddRange(new object[] {
            "None"});
            this.cmbNpc5.Location = new System.Drawing.Point(23, 225);
            this.cmbNpc5.Name = "cmbNpc5";
            this.cmbNpc5.Size = new System.Drawing.Size(252, 21);
            this.cmbNpc5.TabIndex = 17;
            this.cmbNpc5.SelectedIndexChanged += new System.EventHandler(this.cmbNpc5_SelectedIndexChanged);
            // 
            // cmbNpc4
            // 
            this.cmbNpc4.FormattingEnabled = true;
            this.cmbNpc4.Items.AddRange(new object[] {
            "None"});
            this.cmbNpc4.Location = new System.Drawing.Point(23, 198);
            this.cmbNpc4.Name = "cmbNpc4";
            this.cmbNpc4.Size = new System.Drawing.Size(252, 21);
            this.cmbNpc4.TabIndex = 16;
            this.cmbNpc4.SelectedIndexChanged += new System.EventHandler(this.cmbNpc4_SelectedIndexChanged);
            // 
            // cmbNpc3
            // 
            this.cmbNpc3.FormattingEnabled = true;
            this.cmbNpc3.Items.AddRange(new object[] {
            "None"});
            this.cmbNpc3.Location = new System.Drawing.Point(23, 169);
            this.cmbNpc3.Name = "cmbNpc3";
            this.cmbNpc3.Size = new System.Drawing.Size(252, 21);
            this.cmbNpc3.TabIndex = 15;
            this.cmbNpc3.SelectedIndexChanged += new System.EventHandler(this.cmbNpc3_SelectedIndexChanged);
            // 
            // cmbNpc2
            // 
            this.cmbNpc2.FormattingEnabled = true;
            this.cmbNpc2.Items.AddRange(new object[] {
            "None"});
            this.cmbNpc2.Location = new System.Drawing.Point(23, 142);
            this.cmbNpc2.Name = "cmbNpc2";
            this.cmbNpc2.Size = new System.Drawing.Size(252, 21);
            this.cmbNpc2.TabIndex = 14;
            this.cmbNpc2.SelectedIndexChanged += new System.EventHandler(this.cmbNpc2_SelectedIndexChanged);
            // 
            // cmbNpc1
            // 
            this.cmbNpc1.FormattingEnabled = true;
            this.cmbNpc1.Items.AddRange(new object[] {
            "None"});
            this.cmbNpc1.Location = new System.Drawing.Point(23, 115);
            this.cmbNpc1.Name = "cmbNpc1";
            this.cmbNpc1.Size = new System.Drawing.Size(252, 21);
            this.cmbNpc1.TabIndex = 13;
            this.cmbNpc1.SelectedIndexChanged += new System.EventHandler(this.cmbNpc1_SelectedIndexChanged);
            // 
            // lblNpcs
            // 
            this.lblNpcs.AutoSize = true;
            this.lblNpcs.Location = new System.Drawing.Point(20, 97);
            this.lblNpcs.Name = "lblNpcs";
            this.lblNpcs.Size = new System.Drawing.Size(68, 13);
            this.lblNpcs.TabIndex = 12;
            this.lblNpcs.Text = "Select Npcs:";
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
            // picMap
            // 
            this.picMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picMap.Location = new System.Drawing.Point(335, 51);
            this.picMap.Name = "picMap";
            this.picMap.Size = new System.Drawing.Size(800, 608);
            this.picMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picMap.TabIndex = 14;
            this.picMap.TabStop = false;
            // 
            // scrlViewX
            // 
            this.scrlViewX.LargeChange = 1;
            this.scrlViewX.Location = new System.Drawing.Point(336, 662);
            this.scrlViewX.Maximum = 25;
            this.scrlViewX.Name = "scrlViewX";
            this.scrlViewX.Size = new System.Drawing.Size(800, 17);
            this.scrlViewX.TabIndex = 16;
            this.scrlViewX.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlViewX_Scroll);
            // 
            // scrlViewY
            // 
            this.scrlViewY.LargeChange = 1;
            this.scrlViewY.Location = new System.Drawing.Point(1138, 51);
            this.scrlViewY.Maximum = 31;
            this.scrlViewY.Name = "scrlViewY";
            this.scrlViewY.Size = new System.Drawing.Size(17, 608);
            this.scrlViewY.TabIndex = 17;
            this.scrlViewY.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlViewY_Scroll);
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
            this.tosMenu.Size = new System.Drawing.Size(1404, 25);
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
            // treeMaps
            // 
            this.treeMaps.Location = new System.Drawing.Point(1166, 31);
            this.treeMaps.Name = "treeMaps";
            this.treeMaps.Size = new System.Drawing.Size(228, 257);
            this.treeMaps.TabIndex = 23;
            this.treeMaps.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeMaps_AfterSelect);
            // 
            // mapProperties
            // 
            this.mapProperties.Location = new System.Drawing.Point(1166, 294);
            this.mapProperties.Name = "mapProperties";
            this.mapProperties.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.mapProperties.Size = new System.Drawing.Size(228, 459);
            this.mapProperties.TabIndex = 24;
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
            this.pnlDebug.Location = new System.Drawing.Point(87, 402);
            this.pnlDebug.Name = "pnlDebug";
            this.pnlDebug.Size = new System.Drawing.Size(124, 153);
            this.pnlDebug.TabIndex = 24;
            this.pnlDebug.Visible = false;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(8, 130);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(63, 13);
            this.lblType.TabIndex = 21;
            this.lblType.Text = "Type: None";
            // 
            // lblLayer
            // 
            this.lblLayer.AutoSize = true;
            this.lblLayer.Location = new System.Drawing.Point(8, 117);
            this.lblLayer.Name = "lblLayer";
            this.lblLayer.Size = new System.Drawing.Size(74, 13);
            this.lblLayer.TabIndex = 20;
            this.lblLayer.Text = "Layer: Ground";
            // 
            // lblViewX
            // 
            this.lblViewX.AutoSize = true;
            this.lblViewX.Location = new System.Drawing.Point(8, 91);
            this.lblViewX.Name = "lblViewX";
            this.lblViewX.Size = new System.Drawing.Size(52, 13);
            this.lblViewX.TabIndex = 19;
            this.lblViewX.Text = "View X: 0";
            // 
            // lblViewY
            // 
            this.lblViewY.AutoSize = true;
            this.lblViewY.Location = new System.Drawing.Point(8, 104);
            this.lblViewY.Name = "lblViewY";
            this.lblViewY.Size = new System.Drawing.Size(52, 13);
            this.lblViewY.TabIndex = 18;
            this.lblViewY.Text = "View Y: 0";
            // 
            // lblSelectW
            // 
            this.lblSelectW.AutoSize = true;
            this.lblSelectW.Location = new System.Drawing.Point(8, 52);
            this.lblSelectW.Name = "lblSelectW";
            this.lblSelectW.Size = new System.Drawing.Size(77, 13);
            this.lblSelectW.TabIndex = 17;
            this.lblSelectW.Text = "SelectTileW: 0";
            // 
            // lblSelectH
            // 
            this.lblSelectH.AutoSize = true;
            this.lblSelectH.Location = new System.Drawing.Point(8, 65);
            this.lblSelectH.Name = "lblSelectH";
            this.lblSelectH.Size = new System.Drawing.Size(74, 13);
            this.lblSelectH.TabIndex = 16;
            this.lblSelectH.Text = "SelectTileH: 0";
            // 
            // lblButtonDown
            // 
            this.lblButtonDown.AutoSize = true;
            this.lblButtonDown.Location = new System.Drawing.Point(8, 78);
            this.lblButtonDown.Name = "lblButtonDown";
            this.lblButtonDown.Size = new System.Drawing.Size(81, 13);
            this.lblButtonDown.TabIndex = 15;
            this.lblButtonDown.Text = "Button Down: ?";
            // 
            // lblSelectY
            // 
            this.lblSelectY.AutoSize = true;
            this.lblSelectY.Location = new System.Drawing.Point(8, 26);
            this.lblSelectY.Name = "lblSelectY";
            this.lblSelectY.Size = new System.Drawing.Size(73, 13);
            this.lblSelectY.TabIndex = 14;
            this.lblSelectY.Text = "SelectTileY: 0";
            // 
            // lblSelectX
            // 
            this.lblSelectX.AutoSize = true;
            this.lblSelectX.Location = new System.Drawing.Point(8, 39);
            this.lblSelectX.Name = "lblSelectX";
            this.lblSelectX.Size = new System.Drawing.Size(73, 13);
            this.lblSelectX.TabIndex = 13;
            this.lblSelectX.Text = "SelectTileX: 0";
            // 
            // lblMouseLoc
            // 
            this.lblMouseLoc.AutoSize = true;
            this.lblMouseLoc.Location = new System.Drawing.Point(8, 13);
            this.lblMouseLoc.Name = "lblMouseLoc";
            this.lblMouseLoc.Size = new System.Drawing.Size(92, 13);
            this.lblMouseLoc.TabIndex = 12;
            this.lblMouseLoc.Text = "Mouse - X: 0, Y: 0";
            // 
            // MapEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1404, 765);
            this.Controls.Add(this.mapProperties);
            this.Controls.Add(this.treeMaps);
            this.Controls.Add(this.tosMenu);
            this.Controls.Add(this.scrlViewY);
            this.Controls.Add(this.scrlViewX);
            this.Controls.Add(this.picMap);
            this.Controls.Add(this.tabTools);
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MapEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Map Editor";
            this.tabTools.ResumeLayout(false);
            this.tabLayer.ResumeLayout(false);
            this.tabLayer.PerformLayout();
            this.pnlTile.ResumeLayout(false);
            this.pnlTile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTileset)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlOptions.ResumeLayout(false);
            this.pnlOptions.PerformLayout();
            this.tabTypes.ResumeLayout(false);
            this.tabTypes.PerformLayout();
            this.pnlWarp.ResumeLayout(false);
            this.pnlWarp.PerformLayout();
            this.pnlChest.ResumeLayout(false);
            this.pnlChest.PerformLayout();
            this.pnlMapItem.ResumeLayout(false);
            this.pnlMapItem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picItem)).EndInit();
            this.pnlNpcSpawn.ResumeLayout(false);
            this.pnlNpcSpawn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSprite)).EndInit();
            this.tabLight.ResumeLayout(false);
            this.tabLight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).EndInit();
            this.tosMenu.ResumeLayout(false);
            this.tosMenu.PerformLayout();
            this.pnlDebug.ResumeLayout(false);
            this.pnlDebug.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TabControl tabTools;
        private System.Windows.Forms.TabPage tabLayer;
        private System.Windows.Forms.TabPage tabTypes;
        private System.Windows.Forms.RadioButton radFringe;
        private System.Windows.Forms.RadioButton radFringe2;
        private System.Windows.Forms.RadioButton radMask2;
        private System.Windows.Forms.RadioButton radMask;
        private System.Windows.Forms.RadioButton radGround;
        private System.Windows.Forms.RadioButton radSpawnNpc;
        private System.Windows.Forms.RadioButton radBlocked;
        private System.Windows.Forms.RadioButton radNone;
        private System.Windows.Forms.CheckBox chkGrid;
        private System.Windows.Forms.Panel pnlNpcSpawn;
        private System.Windows.Forms.HScrollBar scrlNpcNum;
        private System.Windows.Forms.Label lblNpcSpawn;
        private System.Windows.Forms.PictureBox picMap;
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
        private System.Windows.Forms.ToolStrip tosMenu;
        private System.Windows.Forms.ToolStripButton btnNewMap;
        private System.Windows.Forms.ToolStripButton btnSaveMap;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnMapNpcs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnFillMap;
        private System.Windows.Forms.ToolStripButton btnDebug;
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
        private System.Windows.Forms.PictureBox picItem;
        private System.Windows.Forms.PictureBox picSprite;
        private System.Windows.Forms.Panel pnlWarp;
        private System.Windows.Forms.Label lblMapY;
        private System.Windows.Forms.Label lblMapX;
        private System.Windows.Forms.Label lblMapNum;
        private System.Windows.Forms.RadioButton radWarp;
        private System.Windows.Forms.HScrollBar scrlMapY;
        private System.Windows.Forms.HScrollBar scrlMapX;
        private System.Windows.Forms.HScrollBar scrlMapNum;
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
        private System.Windows.Forms.Label lblNpcs;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRestruct;
        private System.Windows.Forms.TextBox txtMaxY;
        private System.Windows.Forms.TextBox txtMaxX;
        private System.Windows.Forms.Label lblMaxY;
        private System.Windows.Forms.Label lblMaxX;
        private System.Windows.Forms.TreeView treeMaps;
        private System.Windows.Forms.PropertyGrid mapProperties;
        private System.Windows.Forms.Panel pnlTile;
        private System.Windows.Forms.PictureBox picTileset;
        private System.Windows.Forms.ComboBox cmbTileset;
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
    }
}