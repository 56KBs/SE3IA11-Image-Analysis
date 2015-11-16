using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageCompression;
using System.IO;
using ImageCompression.Encoders;
using ImageCompression.ColorModel;

namespace ImageCompressionGUI
{
    public partial class Form1 : Form
    {
        private StandardImage originalImage { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.originalImageTab.AutoScroll = true;
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog();

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                if (Path.GetExtension(fileDialog.FileName) == ".miac")
                {

                }
                else
                {
                    this.originalImage = new StandardImage(new Bitmap(fileDialog.FileName));

                    var originalPixelFormat = this.originalImage.GetBitmap().PixelFormat;
                    if (originalPixelFormat == System.Drawing.Imaging.PixelFormat.Format16bppArgb1555 ||
                        originalPixelFormat == System.Drawing.Imaging.PixelFormat.Format16bppRgb555 ||
                        originalPixelFormat == System.Drawing.Imaging.PixelFormat.Format16bppRgb565 ||
                        originalPixelFormat == System.Drawing.Imaging.PixelFormat.Format16bppGrayScale)
                    {
                        this.bitDepthComboBox.SelectedIndex = 1;
                    }
                    else if (originalPixelFormat == System.Drawing.Imaging.PixelFormat.Format24bppRgb ||
                             originalPixelFormat == System.Drawing.Imaging.PixelFormat.Canonical)
                    {
                        this.bitDepthComboBox.SelectedIndex = 3;
                    }
                    else if (originalPixelFormat == System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
                    {
                        this.bitDepthComboBox.SelectedIndex = 0;
                    }
                    else
                    {
                        this.bitDepthComboBox.SelectedIndex = 3;
                    }
                }

                if (this.originalPictureBox.Image != null)
                {
                    this.originalPictureBox.Image.Dispose();
                }

                this.originalPictureBox.Image = this.originalImage.GetBitmap();
                this.originalSizeLabelBytes.Text = new FileInfo(fileDialog.FileName).Length.ToString();
            }
        }

        private void colourDepthToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void recompressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var singleDimensionArray = RunLength.Encode(this.originalImage.GetPixelMatrix());

            var byteArray = singleDimensionArray.ConvertAll<byte[]>(new Converter<RunLengthStore<RGB>, byte[]>(value => value.ToByte()));

            var byteArray1D = byteArray.SelectMany(x => x).ToArray();

            using (var byteWriter = new FileStream(@"D:\GitHub\SE3IA11-Image-Analysis\ImageCompression\TestImage\Output\file.miac", FileMode.Create, FileAccess.Write))
            {
                byteArray.ForEach(x => byteWriter.Write(x, 0, x.Length));
            }
        }
    }
}
