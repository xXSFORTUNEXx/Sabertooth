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
            this.picMap = new System.Windows.Forms.PictureBox();
            this.tabTools = new System.Windows.Forms.TabControl();
            this.tabLayer = new System.Windows.Forms.TabPage();
            this.tabTypes = new System.Windows.Forms.TabPage();
            this.tabTiles = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).BeginInit();
            this.tabTools.SuspendLayout();
            this.SuspendLayout();
            // 
            // picMap
            // 
            this.picMap.Location = new System.Drawing.Point(294, 12);
            this.picMap.Name = "picMap";
            this.picMap.Size = new System.Drawing.Size(800, 600);
            this.picMap.TabIndex = 0;
            this.picMap.TabStop = false;
            // 
            // tabTools
            // 
            this.tabTools.Controls.Add(this.tabLayer);
            this.tabTools.Controls.Add(this.tabTypes);
            this.tabTools.Controls.Add(this.tabTiles);
            this.tabTools.Location = new System.Drawing.Point(12, 12);
            this.tabTools.Name = "tabTools";
            this.tabTools.SelectedIndex = 0;
            this.tabTools.Size = new System.Drawing.Size(276, 599);
            this.tabTools.TabIndex = 1;
            // 
            // tabLayer
            // 
            this.tabLayer.Location = new System.Drawing.Point(4, 22);
            this.tabLayer.Name = "tabLayer";
            this.tabLayer.Padding = new System.Windows.Forms.Padding(3);
            this.tabLayer.Size = new System.Drawing.Size(268, 573);
            this.tabLayer.TabIndex = 0;
            this.tabLayer.Text = "Layer";
            this.tabLayer.UseVisualStyleBackColor = true;
            // 
            // tabTypes
            // 
            this.tabTypes.Location = new System.Drawing.Point(4, 22);
            this.tabTypes.Name = "tabTypes";
            this.tabTypes.Padding = new System.Windows.Forms.Padding(3);
            this.tabTypes.Size = new System.Drawing.Size(268, 573);
            this.tabTypes.TabIndex = 1;
            this.tabTypes.Text = "Types";
            this.tabTypes.UseVisualStyleBackColor = true;
            // 
            // tabTiles
            // 
            this.tabTiles.Location = new System.Drawing.Point(4, 22);
            this.tabTiles.Name = "tabTiles";
            this.tabTiles.Size = new System.Drawing.Size(268, 573);
            this.tabTiles.TabIndex = 2;
            this.tabTiles.Text = "Tiles";
            this.tabTiles.UseVisualStyleBackColor = true;
            // 
            // MapEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1106, 623);
            this.Controls.Add(this.tabTools);
            this.Controls.Add(this.picMap);
            this.Name = "MapEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Map Editor";
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).EndInit();
            this.tabTools.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picMap;
        private System.Windows.Forms.TabControl tabTools;
        private System.Windows.Forms.TabPage tabLayer;
        private System.Windows.Forms.TabPage tabTypes;
        private System.Windows.Forms.TabPage tabTiles;
    }
}