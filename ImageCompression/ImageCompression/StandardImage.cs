using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ImageCompression.ImageData;
using ImageCompression.ExtensionMethods;

namespace ImageCompression
{
    public class StandardImage
    {
        /// <summary>
        /// The bitmap image loaded
        /// </summary>
        private Bitmap image { get; set; }

        /// <summary>
        /// Pixel matrix for the image
        /// </summary>
        public PixelMatrix data { get; private set; }

        public StandardImage(Bitmap image)
        {
            this.image = image;
            // Read a the pixel data
            this.data = new PixelMatrix(image.GetRGBPixelArray());
        }

        public StandardImage(ColorModel.RGB[,] data)
        {
            // Set the pixel matrix
            this.data = new PixelMatrix(data);
        }

        /// <summary>
        /// Get the bitmap stored
        /// </summary>
        /// <returns>Bitmap</returns>
        public Bitmap GetBitmap()
        {
            return this.image;
        }

        /// <summary>
        /// Get the pixel matrix from the data at a specified bit depth
        /// </summary>
        /// <param name="depth">Bit depth to return the data as</param>
        /// <returns>Multidimensional RGB data</returns>
        public ColorModel.RGB[,] GetPixelMatrix(ColorModel.RGB.ColorDepth depth)
        {
            // Copy the data
            var copiedData = this.GetDataCopy();

            // If the bits match what we have, return the copied data
            if (this.data.data[0, 0].bits == depth)
            {
                return copiedData;
            }
            else
            {
                // Return a converted version
                return copiedData.ConvertAll2D(new Converter<ColorModel.RGB, ColorModel.RGB>(x => x.ToDepth(depth)));
            }
        }

        /// <summary>
        /// Dispose of the image
        /// </summary>
        public void Dispose()
        {
            this.image.Dispose();
        }

        /// <summary>
        /// Get a copy of the RGB data
        /// </summary>
        /// <returns>Multidimensional RGB array</returns>
        public ColorModel.RGB[,] GetDataCopy()
        {
            var copied = new ColorModel.RGB[this.data.data.GetLength(0), this.data.data.GetLength(1)];

            // Optimised processing of data
            Parallel.For(0, this.data.data.GetLength(0), i =>
            {
                for (var j = 0; j < this.data.data.GetLength(1); j++)
                {
                    copied[i, j] = this.data.data[i, j];
                }
            });

            return copied;
        }

        /// <summary>
        /// Calculate the MSE with another multidimensional array
        /// </summary>
        /// <param name="comparison">Array to compare with</param>
        /// <returns>The MSE value</returns>
        public double MeanSquaredError(ColorModel.RGB[,] comparison)
        {
            // If the dimensions are different, return -1
            if (this.data.width != comparison.GetLength(0) || this.data.height != comparison.GetLength(1))
            {
                return -1;
            }

            // Set up errorSum
            double errorSum = 0;

            // Loop over the array
            for (var i = 0; i < this.data.width; i++)
            {
                for (var j = 0; j < this.data.height; j++)
                {
                    // Add to the sum of (our pixel - their pixel)^2
                    var thisSum = this.data.data[i, j].R + this.data.data[i, j].G + this.data.data[i, j].B;
                    var otherSum = comparison[i, j].R + comparison[i, j].G + comparison[i, j].B;

                    errorSum += Math.Pow(thisSum - otherSum, 2);
                }
            }

            // Divide by the size of our data
            return (errorSum / (this.data.width * this.data.height));
        }
    }
}
