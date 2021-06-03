namespace Editor.Forms
{
    partial class ImageSplitter
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
            this.picImage = new System.Windows.Forms.PictureBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.pnlDim = new System.Windows.Forms.Panel();
            this.txtYT = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtXT = new System.Windows.Forms.TextBox();
            this.txtYP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtXP = new System.Windows.Forms.TextBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.lblPreview = new System.Windows.Forms.Label();
            this.scrlPreview = new System.Windows.Forms.HScrollBar();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).BeginInit();
            this.pnlDim.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // picImage
            // 
            this.picImage.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.picImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picImage.Location = new System.Drawing.Point(12, 140);
            this.picImage.Name = "picImage";
            this.picImage.Size = new System.Drawing.Size(545, 492);
            this.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picImage.TabIndex = 0;
            this.picImage.TabStop = false;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(211, 41);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(95, 23);
            this.btnExport.TabIndex = 2;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // pnlDim
            // 
            this.pnlDim.Controls.Add(this.txtYT);
            this.pnlDim.Controls.Add(this.label1);
            this.pnlDim.Controls.Add(this.label2);
            this.pnlDim.Controls.Add(this.txtXT);
            this.pnlDim.Controls.Add(this.txtYP);
            this.pnlDim.Controls.Add(this.label3);
            this.pnlDim.Controls.Add(this.label4);
            this.pnlDim.Controls.Add(this.txtXP);
            this.pnlDim.Location = new System.Drawing.Point(12, 12);
            this.pnlDim.Name = "pnlDim";
            this.pnlDim.Size = new System.Drawing.Size(193, 122);
            this.pnlDim.TabIndex = 7;
            // 
            // txtYT
            // 
            this.txtYT.Location = new System.Drawing.Point(109, 69);
            this.txtYT.Name = "txtYT";
            this.txtYT.Size = new System.Drawing.Size(62, 20);
            this.txtYT.TabIndex = 14;
            this.txtYT.Text = "1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(109, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Y (tiles):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(110, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "X (tiles):";
            // 
            // txtXT
            // 
            this.txtXT.Location = new System.Drawing.Point(109, 26);
            this.txtXT.Name = "txtXT";
            this.txtXT.Size = new System.Drawing.Size(62, 20);
            this.txtXT.TabIndex = 11;
            this.txtXT.Text = "1";
            // 
            // txtYP
            // 
            this.txtYP.Location = new System.Drawing.Point(17, 69);
            this.txtYP.Name = "txtYP";
            this.txtYP.Size = new System.Drawing.Size(62, 20);
            this.txtYP.TabIndex = 10;
            this.txtYP.Text = "32";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Y (pixels):";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "X (pixels):";
            // 
            // txtXP
            // 
            this.txtXP.Location = new System.Drawing.Point(17, 26);
            this.txtXP.Name = "txtXP";
            this.txtXP.Size = new System.Drawing.Size(62, 20);
            this.txtXP.TabIndex = 7;
            this.txtXP.Text = "32";
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(211, 12);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(95, 23);
            this.btnOpen.TabIndex = 8;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // picPreview
            // 
            this.picPreview.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.picPreview.Location = new System.Drawing.Point(325, 12);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(32, 32);
            this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picPreview.TabIndex = 9;
            this.picPreview.TabStop = false;
            // 
            // lblPreview
            // 
            this.lblPreview.AutoSize = true;
            this.lblPreview.Location = new System.Drawing.Point(211, 67);
            this.lblPreview.Name = "lblPreview";
            this.lblPreview.Size = new System.Drawing.Size(57, 13);
            this.lblPreview.TabIndex = 10;
            this.lblPreview.Text = "Preview: 1";
            // 
            // scrlPreview
            // 
            this.scrlPreview.Location = new System.Drawing.Point(211, 84);
            this.scrlPreview.Minimum = 1;
            this.scrlPreview.Name = "scrlPreview";
            this.scrlPreview.Size = new System.Drawing.Size(99, 17);
            this.scrlPreview.TabIndex = 11;
            this.scrlPreview.Value = 1;
            this.scrlPreview.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrlPreview_Scroll);
            // 
            // ImageSplitter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 644);
            this.Controls.Add(this.picPreview);
            this.Controls.Add(this.scrlPreview);
            this.Controls.Add(this.lblPreview);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.pnlDim);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.picImage);
            this.Name = "ImageSplitter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Image Splitter";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ImageSplitter_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).EndInit();
            this.pnlDim.ResumeLayout(false);
            this.pnlDim.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picImage;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Panel pnlDim;
        private System.Windows.Forms.TextBox txtYP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtXP;
        private System.Windows.Forms.TextBox txtYT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtXT;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.Label lblPreview;
        private System.Windows.Forms.HScrollBar scrlPreview;
    }
}