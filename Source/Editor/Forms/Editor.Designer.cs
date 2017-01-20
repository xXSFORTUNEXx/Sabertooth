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
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.pnlLock.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlLock
            // 
            this.pnlLock.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pnlLock.Controls.Add(this.lblIncorrect);
            this.pnlLock.Controls.Add(this.txtUnlock);
            this.pnlLock.Controls.Add(this.btnUnlock);
            this.pnlLock.Location = new System.Drawing.Point(12, 14);
            this.pnlLock.Name = "pnlLock";
            this.pnlLock.Size = new System.Drawing.Size(119, 226);
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
            this.btnNpcEditor.Location = new System.Drawing.Point(12, 41);
            this.btnNpcEditor.Name = "btnNpcEditor";
            this.btnNpcEditor.Size = new System.Drawing.Size(119, 23);
            this.btnNpcEditor.TabIndex = 2;
            this.btnNpcEditor.Text = "NPC Editor";
            this.btnNpcEditor.UseVisualStyleBackColor = true;
            this.btnNpcEditor.Click += new System.EventHandler(this.btnNpcEditor_Click);
            // 
            // btnItemEditor
            // 
            this.btnItemEditor.Location = new System.Drawing.Point(12, 70);
            this.btnItemEditor.Name = "btnItemEditor";
            this.btnItemEditor.Size = new System.Drawing.Size(119, 23);
            this.btnItemEditor.TabIndex = 3;
            this.btnItemEditor.Text = "Item Editor";
            this.btnItemEditor.UseVisualStyleBackColor = true;
            this.btnItemEditor.Click += new System.EventHandler(this.btnItemEditor_Click);
            // 
            // btnProjectileEditor
            // 
            this.btnProjectileEditor.Location = new System.Drawing.Point(12, 99);
            this.btnProjectileEditor.Name = "btnProjectileEditor";
            this.btnProjectileEditor.Size = new System.Drawing.Size(119, 23);
            this.btnProjectileEditor.TabIndex = 4;
            this.btnProjectileEditor.Text = "Projectile Editor";
            this.btnProjectileEditor.UseVisualStyleBackColor = true;
            this.btnProjectileEditor.Click += new System.EventHandler(this.btnProjectileEditor_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(12, 128);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(119, 23);
            this.button5.TabIndex = 5;
            this.button5.Text = "Mission Editor";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(12, 157);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(119, 23);
            this.button6.TabIndex = 6;
            this.button6.Text = "Skill Editor";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(12, 186);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(119, 23);
            this.button7.TabIndex = 7;
            this.button7.Text = "Event Editor";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(12, 215);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(119, 23);
            this.button8.TabIndex = 8;
            this.button8.Text = "Shop Editor";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(144, 252);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.btnProjectileEditor);
            this.Controls.Add(this.btnItemEditor);
            this.Controls.Add(this.btnNpcEditor);
            this.Controls.Add(this.btnMapEditor);
            this.Controls.Add(this.pnlLock);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Editor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Editor";
            this.pnlLock.ResumeLayout(false);
            this.pnlLock.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
    }
}

