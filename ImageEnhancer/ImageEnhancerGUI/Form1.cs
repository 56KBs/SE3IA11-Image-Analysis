using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageEnhancerLibrary;
using System.Drawing.Imaging;
using System.Numerics;

namespace ImageEnhancerGUI
{
    public partial class Form1 : Form
    {
        private IEImage image { get; set; }

        private IEImage transformedImage { get; set; }

        private Graphics drawingPanel;
        private int? X = null;
        private int? Y = null;
        private bool painting = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void loadImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Title = "Select a image to load";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                this.image = new IEImage(new Bitmap(fileDialog.FileName));

                panel1.BackgroundImage = image.GetBitmap();
            }
        }

        private void loadedImage1_Click(object sender, EventArgs e)
        {

        }

        private void loadImage_Click_1(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Title = "Select a image to load";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                image = new IEImage(new Bitmap(fileDialog.FileName));

                imageBoxOriginal.Image = image.GetBitmap();
            }
        }

        private void fourierTransform_Click(object sender, EventArgs e)
        {
            this.transformedImage = new IEImage(Fourier.Transform(this.image.GetFullPixelData()));

            if (panel1.BackgroundImage != null)
            {
                panel1.BackgroundImage.Dispose();
            }

            transformedImage.GetShiftedBitmap().Save(@"D:\GitHub\SE3IA11-Image-Enhancement\ImageEnhancer\TestImage\Output\FFTShifted.bmp", ImageFormat.Bmp);

            panel1.BackgroundImage = new Bitmap(@"D:\GitHub\SE3IA11-Image-Enhancement\ImageEnhancer\TestImage\Output\FFTShifted.bmp"); ;

            tabControl1.SelectTab(1);

            panel1.Size = new Size(panel1.BackgroundImage.Width, panel1.BackgroundImage.Height);
            this.drawingPanel = panel1.CreateGraphics();
        }
    
        private void panel1_Draw_MouseDown(object sender, EventArgs e)
        {
            this.painting = true;
        }

        private void panel1_Draw_MouseUp(object sender, EventArgs e)
        {
            this.painting = false;
            this.X = null;
            this.Y = null;
        }

        private void panel1_Draw_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.painting)
            {
                Pen userBrush = new Pen(Color.Black, 5);
                Point startPoint = new Point();

                startPoint.X = this.X ?? e.X;
                this.X = e.X;

                startPoint.Y = this.Y ?? e.Y;
                this.Y = e.Y;

                Point endPoint = new Point(e.X, e.Y);

                drawingPanel.DrawLine(userBrush, startPoint, endPoint);
            }
        }

        private void saveUnshiftedTransform_Click(object sender, EventArgs e)
        {
            var saveDialog = new SaveFileDialog();

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                this.transformedImage.GetBitmap().Save(saveDialog.FileName, ImageFormat.Bmp);
            }
        }

        private void removePeriodicNoise_Click(object sender, EventArgs e)
        {
            var noiseReduced = new IEImage(Filters.RemovePeriodicNoise(this.transformedImage.GetFullPixelData(), Filters.HighPassFourierFilter(this.transformedImage.GetFullPixelData(), 80)));

            noiseReduced.GetShiftedBitmap().Save(@"D:\GitHub\SE3IA11-Image-Enhancement\ImageEnhancer\TestImage\Output\FFTShiftedNoiseReduced.bmp", ImageFormat.Bmp);

            var inversedPixelData = Fourier.InverseTransform(this.transformedImage.GetFullPixelData());
            inversedPixelData.complexPixels = inversedPixelData.complexPixels.Resize(this.image.width, this.image.height, Complex.Zero);
            inversedPixelData.PropagateComplex(false);

            var inversed = new IEImage(inversedPixelData);

            inversed.GetBitmap().Save(@"D:\GitHub\SE3IA11-Image-Enhancement\ImageEnhancer\TestImage\Output\ShiftedNoiseReduced.bmp", ImageFormat.Bmp);
        }

        private void inverseTransform_Click(object sender, EventArgs e)
        {

            //var noiseReduced = new IEImage(Filters.RemovePeriodicNoise(this.transformedImage.GetFullPixelData()));
            var inversedPixelData = Fourier.InverseTransform(this.transformedImage.GetFullPixelData());
            inversedPixelData.complexPixels = inversedPixelData.complexPixels.Resize(this.image.width, this.image.height, Complex.Zero);
            inversedPixelData.PropagateComplex(false);

            var inversed = new IEImage(inversedPixelData);

            inversed.GetBitmap().Save(@"D:\GitHub\SE3IA11-Image-Enhancement\ImageEnhancer\TestImage\Output\ShiftedNoiseReduced.bmp", ImageFormat.Bmp);
        }
    }
}
