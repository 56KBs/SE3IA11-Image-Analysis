using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ImageCompression.ExtensionMethods
{
    public static class BitmapExtensions
    {
        public static ColorModel.RGB[,] GetRGBPixelArray(this Bitmap bitmap)
        {
            // Make a new colour array
            var colorData = new ColorModel.RGB[bitmap.Width, bitmap.Height];

            // Fill the array with the bitmap data
            for (var i = 0; i < bitmap.Width; i++)
            {
                for (var j = 0; j < bitmap.Height; j++)
                {
                    colorData[i, j] = ColorModel.RGB.FromColor(bitmap.GetPixel(i, j));
                }
            }

            // Return the colour data
            return colorData;
        }
    }
}
