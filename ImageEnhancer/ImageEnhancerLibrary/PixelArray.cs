using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;

namespace ImageEnhancerLibrary
{
    public class PixelArray
    {
        /// <summary>
        /// 2D array of Color pixels
        /// </summary>
        public Color[,] pixels { get; set; }

        /// <summary>
        /// 2D array of complex pixels
        /// </summary>
        public Complex[,] complexPixels { get; set; }

        /// <summary>
        /// Width of the data
        /// </summary>
        public int width
        {
            get
            {
                return this.pixels.GetLength(0);
            }
        }

        /// <summary>
        /// Height of the data
        /// </summary>
        public int height
        {
            get
            {
                return this.pixels.GetLength(1);
            }
        }

        /// <summary>
        /// Create a blank pixel array from dimensions
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public PixelArray(int x, int y)
        {
            // Create empty 2D arrays
            this.pixels = new Color[x, y];
            this.complexPixels = new Complex[x, y];
        }

        /// <summary>
        /// Create a pixel array based on existing 2D colour array
        /// </summary>
        /// <param name="pixelData"></param>
        public PixelArray(Color[,] pixelData)
        {
            // Copy the data as a 2D color array
            this.pixels = pixelData.Clone() as Color[,];
            // Push the colour data into a complex form
            this.PropagateColor();
        }

        /// <summary>
        /// Create a pixel array based on existing 2D complex array
        /// </summary>
        /// <param name="pixelData"></param>
        public PixelArray(Complex[,] pixelData)
        {
            // Copy the data as a 2D complex array
            this.complexPixels = pixelData.Clone() as Complex[,];
            // Push the complex data into the colour array, scaling the data
            this.PropagateComplex(true);
        }

        /// <summary>
        /// Copy the colour data into the complex form
        /// </summary>
        public void PropagateColor()
        {
            // Convert the color pixels into a complex version
            this.complexPixels = this.pixels.ConvertAll2D<Color, Complex>(new Converter<Color, Complex>(value => (Complex)value.ToGreyscale()));
        }

        /// <summary>
        /// Copy the complex data into the colour space
        /// </summary>
        /// <param name="scale">Whether to scale the data according to the peak value</param>
        public void PropagateComplex(bool scale)
        {

            // Build a magnitude array from the complex data
            var magnitudeArray = this.complexPixels.ConvertAll2D<Complex, int>(new Converter<Complex, int>(Converter.ComplexToMagnitude));


            // If we need to scale the data
            if (scale)
            {
                // Get the peak value from the magnitude array
                var peakMagnitude = magnitudeArray.Cast<int>().Max();

                // Set the pixels to the magnitude array converted to colour with the peak magnitude scaling the data
                this.pixels = magnitudeArray.ConvertAll2D<int, Color>(new Converter<int, Color>(value => value.ToColor(peakMagnitude)));
            }
            else
            {
                // Set the pixels as a colour from the magnitude
                this.pixels = magnitudeArray.ConvertAll2D<int, Color>(new Converter<int, Color>(value => value.ToColor()));
            }
        }

        /// <summary>
        /// Pads the Color and Complex arrays with blank values to get to a multiple of 2
        /// </summary>
        public void PadArrays()
        {
            // Calculate the padding
            var paddingValue = this.CalculatePadding();

            // If we need padding
            if (paddingValue > this.height || paddingValue > this.width)
            {
                // Resize both the arrays, padding the data accordingly
                this.pixels = this.pixels.Resize(paddingValue, Color.Black);
                this.complexPixels = this.complexPixels.Resize(paddingValue, Complex.Zero);
            }
        }

        /// <summary>
        /// Calculate the padding needed for FFT (power of 2)
        /// </summary>
        /// <returns>Size of data needed in pixels</returns>
        private int CalculatePadding()
        {
            // Get the longest dimension
            var longestDimension = width >= height ? width : height;

            // Log base 2 the largest dimension to find how many times 2 goes into it, cieling it to get the next highest, pow 2 it for the dimension
            return (int)Math.Pow(2, Math.Ceiling(Math.Log(longestDimension, 2)));
        }

        /// <summary>
        /// Calculate the mean square error
        /// </summary>
        /// <param name="otherArray">Other data array</param>
        /// <returns>Mean square error value</returns>
        public double MeanSquareError(PixelArray otherArray)
        {
            // If the dimensions are different, return -1
            if (this.width != otherArray.width || this.height != otherArray.height)
            {
                return -1;
            }

            // Set up errorSum
            double errorSum = 0;

            // Loop over the array
            for (var i = 0; i < this.width; i++)
            {
                for (var j = 0; j < this.height; j++)
                {
                    // Add to the sum of (our pixel - their pixel)^2
                    errorSum += Math.Pow(this.pixels[i, j].ToGreyscale() - otherArray.pixels[i, j].ToGreyscale(), 2);
                }
            }

            // Divide by the size of our data
            return (errorSum / (this.width * this.height));
        }
    }
}
