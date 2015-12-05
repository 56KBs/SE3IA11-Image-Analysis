using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageCompression.ExtensionMethods
{
    public static class BitmapExtensions
    {
        public static ColorModel.RGB[,] GetRGBPixelArray(this Bitmap bitmap)
        {

            var bitmapHeight = bitmap.Height;
            var bitmapWidth = bitmap.Width;
            var pixelFormat = bitmap.PixelFormat;

            // Make a new colour array
            var colorData = new ColorModel.RGB[bitmapWidth, bitmapHeight];         

            BitmapData bitmapData = bitmap.LockBits(
                new Rectangle(0, 0, bitmapWidth, bitmapHeight),
                ImageLockMode.ReadOnly,
                pixelFormat);

            // Accesses bitmap data in unmanaged code area, direct pointer access for high performance
            unsafe
            {
                // Get a pointer to the first data item
                var dataPointer = (byte*)bitmapData.Scan0.ToPointer();

                // Optimised processing of data
                Parallel.For(0, bitmapHeight, j =>
                {
                    var offsetDataPointer = dataPointer + (j * bitmapWidth);

                    for (var i = 0; i < bitmapWidth; i++)
                    {
                        colorData[i, j] = new ColorModel.RGB(*(offsetDataPointer + 16), *(offsetDataPointer + 8), *offsetDataPointer, ColorModel.RGB.ColorDepth.TwentyFour);

                        offsetDataPointer += 3;
                    }
                });
            }

            bitmap.UnlockBits(bitmapData);

            // Return the colour data
            return colorData;
        }
    }
}
