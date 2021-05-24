using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Editor.Forms
{
    public partial class ImageSplitter : Form
    {
        Image convImage;
        Image[] splitImage;
        int total;

        public ImageSplitter()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void scrlPreview_Scroll(object sender, ScrollEventArgs e)
        {
            lblPreview.Text = "Preview: " + (scrlPreview.Value);
            picPreview.Image = splitImage[scrlPreview.Value - 1];
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportFile();
        }

        private void ImageSplitter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.O)
            {
                OpenFile();
            }

            if (e.Control && e.KeyCode == Keys.E)
            {
                DialogResult result = MessageBox.Show("Export now?", "Export", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    ExportFile();
                }
            }
        }

        private void OpenFile()
        {
            using (OpenFileDialog openFile = new OpenFileDialog())
            {
                openFile.InitialDirectory = @"C:\Users\sfortune\source\repos\Sabertooth\Images";
                openFile.Filter = "png files (*.png)|*.png";
                openFile.FilterIndex = 2;
                openFile.RestoreDirectory = true;

                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    convImage = Image.FromFile(openFile.FileName);                    
                    picImage.Image = convImage;
                }

                try
                {
                    int xpic = Convert.ToInt32(txtXP.Text);
                    int ypic = Convert.ToInt32(txtYP.Text);
                    int width = (convImage.Width / xpic);
                    int height = (convImage.Height / ypic);
                    total = (width * height);
                    txtXT.Text = width.ToString();
                    txtYT.Text = height.ToString();

                    scrlPreview.Maximum = total;
                    picPreview.Width = xpic;
                    picPreview.Height = ypic;

                    splitImage = new Image[total];
                    int n = 0;

                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            splitImage[n] = new Bitmap(xpic, ypic);
                            var graphics = Graphics.FromImage(splitImage[n]);
                            graphics.DrawImage(convImage, new Rectangle(0, 0, xpic, ypic), new Rectangle((x * xpic), (y * ypic), xpic, ypic), GraphicsUnit.Pixel);
                            graphics.Dispose();
                            n += 1;
                        }
                    }
                    picPreview.Image = splitImage[0];
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error druing opening or converson\n" + e.HResult, "Error", MessageBoxButtons.OK);
                }
            }
        }

        private void ExportFile()
        {
            if (splitImage == null) { ErrorResult(0); return; }
            if (total == 0) { ErrorResult(1); return; }
            if (!Directory.Exists("Exported")) { Directory.CreateDirectory("Exported"); }

            try
            {
                for (int i = 0; i < total; i++)
                {
                    splitImage[i].Save("Exported/" + (i) + ".png");
                }
            }
            catch (Exception h)
            {
                MessageBox.Show("Failed to export!\n" + h.HResult);
            }
            finally
            {
                MessageBox.Show("Export Complete!", "Export");
            }
        }

        private void ErrorResult(int errRslt)
        {
            string message;
            string caption = "Error";
            MessageBoxButtons button = MessageBoxButtons.OK;

            switch (errRslt)
            {
                case 0:                    
                    message = "Split image array is null";
                    break;
                case 1:
                    message = "Total = 0";
                    break;
                default:
                    message = "Error not found!";
                    break;

            }
            MessageBox.Show(message, caption, button);
        }
    }
}
