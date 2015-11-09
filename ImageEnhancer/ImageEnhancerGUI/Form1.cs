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
using System.Drawing.Drawing2D;

namespace ImageEnhancerGUI
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// The original image
        /// </summary>
        private IEImage image { get; set; }

        /// <summary>
        /// The image shown as the user edits the image
        /// </summary>
        private IEImage workingImage { get; set; }

        /// <summary>
        /// The preview for colourised data
        /// </summary>
        private IEImage previewedImage { get; set; }

        /// <summary>
        /// Flag whether the data is in the fourier form
        /// </summary>
        private bool workingImageFourier { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Set to non fourier by default
            this.workingImageFourier = false;
        }

        private void loadImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create a new file dialog
            var fileDialog = new OpenFileDialog();
            fileDialog.Title = "Select a image to load";

            // Show the dialog
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                // Load the image into the IEImages required
                this.image = new IEImage(new Bitmap(fileDialog.FileName));
                this.workingImage = new IEImage(new Bitmap(fileDialog.FileName));

                // Set the image box
                imageBoxOriginal.Image = new Bitmap(this.image.GetBitmap());
                tabControl1.SelectTab(0);
            }
        }

        private void forwardFFTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Set the working image data to the fourier transformed data
            this.workingImage.SetPixelData(Fourier.Transform(this.workingImage.GetFullPixelData()));
            this.workingImageFourier = true;

            // Remove the image in the box if it exists
            if (imageBoxWorking.Image != null)
            {
                imageBoxWorking.Image.Dispose();
            }

            // Save the fourier transform and load it into the imagebox
            this.workingImage.GetBitmap().Save(@"D:\GitHub\SE3IA11-Image-Enhancement\ImageEnhancer\TestImage\Output\tmp.bmp", ImageFormat.Bmp);
            imageBoxWorking.Image = new Bitmap(@"D:\GitHub\SE3IA11-Image-Enhancement\ImageEnhancer\TestImage\Output\tmp.bmp");

            // Show the tab for the transform
            tabControl1.SelectTab(1);
        }

        private void inverseFFTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Run the inverse transform on the workingImage data
            var inversedPixelArray = Fourier.InverseTransform(this.workingImage.GetFullPixelData());
            // Resize the data
            inversedPixelArray.complexPixels = inversedPixelArray.complexPixels.Resize(this.image.width, this.image.height, Complex.Zero);
            // Push the complex to colours without scaling
            inversedPixelArray.PropagateComplex(false);

            // Set the working image data to the inverse form
            this.workingImage.SetPixelData(inversedPixelArray);
            
            // Flag no longer in fourier form
            this.workingImageFourier = false;

            // Remove the image in the box if it exists
            if (imageBoxWorking.Image != null)
            {
                imageBoxWorking.Image.Dispose();
            }

            // Save the fourier transform and load it into the imagebox
            this.workingImage.GetBitmap().Save(@"D:\GitHub\SE3IA11-Image-Enhancement\ImageEnhancer\TestImage\Output\tmp.bmp", ImageFormat.Bmp);
            imageBoxWorking.Image = new Bitmap(@"D:\GitHub\SE3IA11-Image-Enhancement\ImageEnhancer\TestImage\Output\tmp.bmp");

            // Show the tab for the transform
            tabControl1.SelectTab(1);
        }

        private void periodicNoiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Remove the periodic noise using the image data and a high passed version of the data to mask the low frequencies from being removed
            this.workingImage = new IEImage(Filters.RemovePeriodicNoise(this.workingImage.GetFullPixelData(), Filters.HighPassFourierFilter(this.workingImage.GetFullPixelData(), 80)));

            // Inverse fourier transform the data
            var inversedPixelData = Fourier.InverseTransform(this.workingImage.GetFullPixelData());
            inversedPixelData.complexPixels = inversedPixelData.complexPixels.Resize(this.image.width, this.image.height, Complex.Zero);
            inversedPixelData.PropagateComplex(false);

            // Set the data to our noise removed version
            this.workingImage = new IEImage(inversedPixelData);

            // Remove the image in the box if it exists
            if (imageBoxWorking.Image != null)
            {
                imageBoxWorking.Image.Dispose();
            }

            // Save the image and load it into the imagebox
            this.workingImage.GetBitmap().Save(@"D:\GitHub\SE3IA11-Image-Enhancement\ImageEnhancer\TestImage\Output\tmp.bmp", ImageFormat.Bmp);
            imageBoxWorking.Image = new Bitmap(@"D:\GitHub\SE3IA11-Image-Enhancement\ImageEnhancer\TestImage\Output\tmp.bmp");
        }

        private void medianFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Run a median filter with radius 1 and create a new IEImage from it
            this.workingImage = new IEImage(Filters.MedianFilter(this.workingImage.GetFullPixelData(), 1));
            
            // Remove the image in the box if it exists
            if (imageBoxWorking.Image != null)
            {
                imageBoxWorking.Image.Dispose();
            }

            // Save the image and load it into the imagebox
            this.workingImage.GetBitmap().Save(@"D:\GitHub\SE3IA11-Image-Enhancement\ImageEnhancer\TestImage\Output\tmp.bmp", ImageFormat.Bmp);
            imageBoxWorking.Image = new Bitmap(@"D:\GitHub\SE3IA11-Image-Enhancement\ImageEnhancer\TestImage\Output\tmp.bmp");
        }

        private void mSEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create a new file dialog
            var fileDialog = new OpenFileDialog();
            fileDialog.Title = "Select an image to compare with";

            // Show the dialog
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                // Load the comparison image
                var comparisonImage = new IEImage(new Bitmap(fileDialog.FileName));

                // Calculate the mean square error of the working image + the comparison image
                var meanSquareError = this.workingImage.GetFullPixelData().MeanSquareError(comparisonImage.GetFullPixelData());

                // Show the mean square error
                MessageBox.Show("Mean Square Error: " + meanSquareError, "Mean Square Error");
            }
        }

        // Set up a nullable colour array of size 256
        private Color?[] assignedColors = new Color?[256];

        private void imageBoxWorking_MouseDown(object sender, MouseEventArgs e)
        {
            // If the image is empty, just return
            if (this.workingImage == null)
            {
                return;
            } // If the click is outside the image bounds, just return
            else if (e.X >= this.workingImage.width || e.Y >= this.workingImage.height)
            {
                return;
            }

            // Create a new colour dialog
            var colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                // Add the colour picked to the assignedColors array based on the selected pixel grey value
                this.assignedColors[this.workingImage.GetColorPixelData()[e.X, e.Y].ToGreyscale()] = colorDialog.Color;
            }
        }


        private void colouriseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Generates a color bitmap to work off
            Tuple<int, Color> previousValue = null;
            Tuple<int, Color> endValue = new Tuple<int,Color>(255, Color.White);

            // Flag that a gradient isn't required
            bool gradientRequired = false;

            // Store gradient data
            var gradientData = new Color[256];

            // Loop over the assignedColors array
            for (var i = 0; i < assignedColors.Length; i++)
            {
                // If this color is not assigned
                if (!assignedColors[i].HasValue)
                {
                    // If we haven't had a value previously, default to black
                    if (previousValue == null)
                    {
                        previousValue = new Tuple<int, Color>(i, Color.Black);
                        gradientRequired = true;
                    }
                    else
                    {
                        // We need a gradient due to a gap
                        gradientRequired = true;
                    }

                    // If we are the last item in the list, force a gradient from previousValue to endValue
                    if (i + 1 == assignedColors.Length)
                    {
                        // Calculate the length of the gradient
                        var gradientLength = i - previousValue.Item1;
                        var position = 0;

                        // Loop over the length of values we need to generate a gradient for
                        for (var j = previousValue.Item1; j < i; j++)
                        {
                            // If data exists, just skip it
                            if (j == previousValue.Item1 && previousValue.Item1 != 0)
                            {
                                continue;
                            }

                            // Set the gradientData at this position to the gradient value generated, also increment the position of the gradient
                            gradientData[j] = this.MakeGradient(previousValue.Item2, endValue.Item2, gradientLength, position++);
                        }

                        // Set the very last item in the list to the default last value
                        gradientData[i] = endValue.Item2;
                    }
                }
                else // Color exists in the assignedColor array
                {
                    // If a gradient is required
                    if (gradientRequired)
                    {
                        // Calculate the length of the gradient
                        var gradientLength = i - previousValue.Item1;
                        var position = 0;

                        // Loop over the length of values we need to generate a gradient for
                        for (var j = previousValue.Item1; j <= i; j++)
                        {
                            // If data exists here, set it to the gradientdata array
                            if (j == previousValue.Item1 && previousValue.Item1 != 0)
                            {
                                gradientData[j] = previousValue.Item2;
                            }
                            else
                            {
                                // Set the gradientData at this position to the gradient value generated, also increment the position of the gradient
                                gradientData[j] = this.MakeGradient(previousValue.Item2, assignedColors[i].Value, gradientLength, position++);
                            }
                        }

                        previousValue = new Tuple<int, Color>(i, assignedColors[i].Value);
                        gradientRequired = false;
                    }
                }
            }


            // Create a new pixel array based on the working image data
            var colorised = new PixelArray(this.workingImage.GetColorPixelData());

            // Loop over the pixel data
            for (var i = 0; i < this.workingImage.width; i++)
            {
                for (var j = 0; j < this.workingImage.height; j++)
                {
                    // Get the grey level of this pixel
                    int greylevel = colorised.pixels[i, j].ToGreyscale();

                    // Set the current pixel based on the corresponding gradient value
                    colorised.pixels[i, j] = (Color)gradientData[greylevel];
                }
            }

            // Create a new IEImage from the colorised image
            this.previewedImage = new IEImage(colorised);

            // Remove the image in the imagebox if it exists
            if (imageBoxWorking.Image != null)
            {
                imageBoxWorking.Image.Dispose();
            }

            // Save and display the image in the imagebox
            this.previewedImage.GetBitmap().Save(@"D:\GitHub\SE3IA11-Image-Enhancement\ImageEnhancer\TestImage\Output\tmp.bmp", ImageFormat.Bmp);
            imageBoxWorking.Image = new Bitmap(@"D:\GitHub\SE3IA11-Image-Enhancement\ImageEnhancer\TestImage\Output\tmp.bmp"); 
        }

        private Color MakeGradient(Color start, Color end, int length, int position)
        {
            // Calculate the stepped value based on the start, end, length and current position in the length
            var steppedR = Math.Abs(start.R - ((Math.Abs(end.R - start.R) * position) / length));
            var steppedG = Math.Abs(start.G - ((Math.Abs(end.G - start.G) * position) / length));
            var steppedB = Math.Abs(start.B - ((Math.Abs(end.B - start.B) * position) / length));

            // Return the stepped colour
            return Color.FromArgb(steppedR, steppedG, steppedB);
        }
    }
}
