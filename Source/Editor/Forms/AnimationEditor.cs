using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SabertoothServer;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using static System.Convert;

namespace Editor.Forms
{
    public partial class AnimationEditor : Form
    {
        Animation e_Animation = new Animation();
        Image[] finalAnimation;
        int tmrFrameCount = 0;
        int SelectedIndex;
        bool Modified;

        public AnimationEditor()
        {
            InitializeComponent();
            picSprite.Image = Image.FromFile("Resources/Animation/1.png");
            scrlSprite.Maximum = Directory.GetFiles("Resources/Animation/", "*", SearchOption.TopDirectoryOnly).Length;
            LoadAnimationList();
        }

        private void LoadAnimationList()
        {
            string connection = "Data Source=" + Server.sqlServer + ";Initial Catalog=" + Server.sqlDatabase + ";Integrated Security=True";
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                string command = "SELECT COUNT(*) FROM Animation";
                using (SqlCommand cmd = new SqlCommand(command, sql))
                {
                    object count = cmd.ExecuteScalar();
                    int result = ToInt32(count);
                    lstIndex.Items.Clear();
                    for (int i = 0; i < result; i++)
                    {
                        e_Animation.LoadAnimationNameFromDatabase(i + 1);
                        lstIndex.Items.Add(e_Animation.Name);
                    }
                }
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            e_Animation.Name = txtName.Text;
            Modified = true;
        }

        private void scrlFCH_Scroll(object sender, ScrollEventArgs e)
        {
            lblFCH.Text = "Frame Count Horizontal: " + (scrlFCH.Value);
            e_Animation.FrameCountH = scrlFCH.Value;
            Modified = true;
        }

        private void scrlFCV_Scroll(object sender, ScrollEventArgs e)
        {
            lblFCV.Text = "Frame Count Vertical: " + (scrlFCV.Value);
            e_Animation.FrameCountV = scrlFCV.Value;
            Modified = true;
        }

        private void scrlSprite_Scroll(object sender, ScrollEventArgs e)
        {
            lblSprite.Text = "Sprite Number: " + (scrlSprite.Value);
            picSprite.Image = Image.FromFile("Resources/Animation/" + scrlSprite.Value + ".png");
            e_Animation.SpriteNumber = scrlSprite.Value;
            Modified = true;
        }

        private void scrlFC_Scroll(object sender, ScrollEventArgs e)
        {
            lblFC.Text = "Frame Count: " + (scrlFC.Value);
            e_Animation.FrameCount = scrlFC.Value;
            Modified = true;
        }

        private void scrlFD_Scroll(object sender, ScrollEventArgs e)
        {
            lblFD.Text = "Frame Duration: " + (scrlFD.Value);
            e_Animation.FrameDuration = scrlFD.Value;
            Modified = true;
        }

        private void scrlLPC_Scroll(object sender, ScrollEventArgs e)
        {
            lblLPC.Text = "Loop Count: " + (scrlLPC.Value);
            e_Animation.LoopCount = scrlLPC.Value;
            Modified = true;
        }

        private void chkRBT_CheckedChanged(object sender, EventArgs e)
        {
            e_Animation.RenderBelowTarget = chkRBT.Checked;
            Modified = true;
        }

        private void btnNewItem_Click(object sender, EventArgs e)
        {
            if (Modified == true)
            {
                string w_Message = "Are you sure you want to create a new animation? All unsaved progress will be lost.";
                string w_Caption = "Modified data";
                MessageBoxButtons w_Buttons = MessageBoxButtons.YesNo;
                DialogResult w_Result;
                w_Result = MessageBox.Show(w_Message, w_Caption, w_Buttons);
                if (w_Result == DialogResult.No) { return; }
            }

            e_Animation.CreateAnimationInDatabase();
            lstIndex.Items.Add(e_Animation.Name);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            e_Animation.SaveAnimationToDatabase(SelectedIndex);
            LoadAnimationList();
            Modified = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

        private void lstIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Modified == true)
            {
                string w_Message = "Are you sure you want to load a different animation? All unsaved progress will be lost.";
                string w_Caption = "Modified data";
                MessageBoxButtons w_Buttons = MessageBoxButtons.YesNo;
                DialogResult w_Result;
                w_Result = MessageBox.Show(w_Message, w_Caption, w_Buttons);
                if (w_Result == DialogResult.No) { return; }
            }

            tmrAnimation.Enabled = false;
            tmrFrameCount = 0;
            SelectedIndex = (lstIndex.SelectedIndex + 1);
            if (SelectedIndex == 0) { return; }
            e_Animation.LoadAnimationFromDatabase(SelectedIndex);
            txtName.Text = e_Animation.Name;
            scrlSprite.Value = e_Animation.SpriteNumber;
            scrlFCH.Value = e_Animation.FrameCountH;
            scrlFCV.Value = e_Animation.FrameCountV;
            scrlFC.Value = e_Animation.FrameCount;
            scrlFD.Value = e_Animation.FrameDuration;
            scrlLPC.Value = e_Animation.LoopCount;
            chkRBT.Checked = e_Animation.RenderBelowTarget;
            lblSprite.Text = "Sprite Number: " + (scrlSprite.Value);
            picSprite.Image = Image.FromFile("Resources/Animation/" + scrlSprite.Value + ".png");
            lblFCH.Text = "Frame Count Horizontal: " + (scrlFCH.Value);
            lblFCV.Text = "Frame Count Vertical: " + (scrlFCV.Value);
            lblFC.Text = "Frame Count: " + (scrlFC.Value);
            lblFD.Text = "Frame Duration: " + (scrlFD.Value);
            lblLPC.Text = "Loop Count: " + (scrlLPC.Value);
            Modified = false;
            pnlMain.Visible = true;
        }

        private void btnViewFull_Click(object sender, EventArgs e)
        {
            Image formBKG = Image.FromFile("Resources/Animation/" + scrlSprite.Value + ".png");
            Form viewFull = new Form();
            PictureBox picBox = new PictureBox();

            picBox.Image = formBKG;
            picBox.Width = formBKG.Width;
            picBox.Height = formBKG.Height;
            picBox.BackColor = Color.Black;
            
            viewFull.Controls.Add(picBox);
            viewFull.StartPosition = FormStartPosition.WindowsDefaultLocation;
            viewFull.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            viewFull.Text = "Resources/Animation/" + scrlSprite.Value + ".png - W: " + formBKG.Width + " H: " + formBKG.Height;
            viewFull.BackColor = Color.Black;
            viewFull.AutoSize = true;
            viewFull.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            viewFull.Show();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (tmrAnimation.Enabled) { tmrAnimation.Enabled = false; return; }
            bool err = false;

            try
            {
                Image anim = Image.FromFile("Resources/Animation/" + scrlSprite.Value + ".png");
                finalAnimation = new Image[scrlFC.Value];
                int currentCount = 0;
                int xpic = anim.Width / scrlFCV.Value;
                int ypic = anim.Height / scrlFCH.Value;
                picPreview.Width = xpic;
                picPreview.Height = ypic;
                lblAnimSize.Text = "Animation Size: " + xpic + "x" + ypic;

                for (int i = 0; i < scrlFCH.Value; i++)
                {
                    for (int j = 0; j < scrlFCV.Value; j++)
                    {
                        finalAnimation[currentCount] = new Bitmap(xpic, ypic);
                        var graphics = Graphics.FromImage(finalAnimation[currentCount]);
                        graphics.DrawImage(anim, new Rectangle(0, 0, xpic, ypic), new Rectangle((j * xpic), (i * ypic), xpic, ypic), GraphicsUnit.Pixel);
                        graphics.Dispose();
                        currentCount += 1;
                    }
                }                
            }
            catch (Exception er)
            {
                MessageBox.Show("Error when showing preview:\nThe most common error is that the frame Count doesnt match (h*v).\n" + er.Message, "Error", MessageBoxButtons.OK);
                err = true;
            }
            finally
            {
                if (!err)
                {                    
                    tmrAnimation.Interval = scrlFD.Value;
                    tmrAnimation.Enabled = true;
                }
            }
        }

        private void tmrAnimation_Tick(object sender, EventArgs e)
        {
            if (tmrFrameCount >= scrlFC.Value) { tmrFrameCount = 0; }

            picPreview.Image = finalAnimation[tmrFrameCount];
            tmrFrameCount += 1;
        }
    }
}
