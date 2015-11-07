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
        public Color[,] pixels { get; set; }

        public Complex[,] complexPixels { get; set; }

        public int height { get; set; }

        public int width { get; set; }

        public PixelArray(Bitmap image)
        {
            this.width = image.Width;
            this.height = image.Height;
            this.BitmapToArrays(image);
        }

        public PixelArray(int x, int y)
        {
            this.width = x;
            this.height = y;
            this.pixels = new Color[this.width, this.height];
            this.complexPixels = new Complex[this.width, this.height];
        }

        public PixelArray(Color[,] pixelData)
        {
            this.width = pixelData.GetLength(0);
            this.height = pixelData.GetLength(1);
            this.pixels = pixelData.Clone() as Color[,];
            this.PropagateColor();
        }

        public PixelArray(Complex[,] pixelData)
        {
            this.width = pixelData.GetLength(0);
            this.height = pixelData.GetLength(1);
            this.complexPixels = pixelData.Clone() as Complex[,];
            this.PropagateComplex(true);
        }

        public void PropagateColor()
        {
            var width = this.pixels.GetLength(0);
            var height = this.pixels.GetLength(1);

            this.complexPixels = this.pixels.ConvertAll2D<Color, Complex>(new Converter<Color, Complex>(value => (Complex)value.ToGreyscale()));
        }

        public void PropagateComplex(bool scale)
        {
            var width = this.complexPixels.GetLength(0);
            var height = this.complexPixels.GetLength(1);

            var magnitudeArray = this.complexPixels.ConvertAll2D<Complex, int>(new Converter<Complex, int>(Converter.ComplexToMagnitude));


            if (scale)
            {
                var peakMagnitude = magnitudeArray.Cast<int>().Max();

                this.pixels = magnitudeArray.ConvertAll2D<int, Color>(new Converter<int, Color>(value => value.ToColor(peakMagnitude)));
            }
            else
            {
                this.pixels = magnitudeArray.ConvertAll2D<int, Color>(new Converter<int, Color>(value => value.ToColor()));
            }
        }

        private void BitmapToArrays(Bitmap bitmap)
        {
            this.pixels = new Color[bitmap.Width, bitmap.Height];
            this.complexPixels = new Complex[bitmap.Width, bitmap.Height];

            for (var i = 0; i < bitmap.Width; i++)
            {
                for (var j = 0; j < bitmap.Height; j++)
                {
                    this.pixels[i, j] = bitmap.GetPixel(i, j);
                    this.complexPixels[i, j] = (Complex)bitmap.GetPixel(i, j).ToGreyscale();
                }
            }
        }

        /// <summary>
        /// Pads the Color and Complex arrays with blank values to get to a multiple of 2
        /// </summary>
        public void PadArrays()
        {
            var paddingValue = this.CalculatePadding();

            if (paddingValue > this.height || paddingValue > this.width)
            {
                this.height = paddingValue;
                this.width = paddingValue;
                this.pixels = this.pixels.Resize(paddingValue, Color.Black);
                this.complexPixels = this.complexPixels.Resize(paddingValue, Complex.Zero);
            }
        }

        private int CalculatePadding()
        {
            var longestDimension = width >= height ? width : height;
            return (int)Math.Pow(2, Math.Ceiling(Math.Log(longestDimension, 2)));
        }
    }
}
