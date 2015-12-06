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

            // NEED TO CALCULATE BYTES PER PIXEL AS SOME FORMATS HALF ALPHA CHANNEL!
            var bytesPerPixel = bitmap.BytesPerPixel();

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
                    // Use the stride to take the padding into account at the end of rows
                    var offsetDataPointer = dataPointer + (j * bitmapData.Stride);

                    for (var i = 0; i < bitmapWidth; i++)
                    {
                        if (*offsetDataPointer == 255 || *(offsetDataPointer + 1) == 255 || *(offsetDataPointer + 2) == 255)
                        {
                            var debuggerplz = 0;
                        }

                        colorData[i, j] = new ColorModel.RGB(*(offsetDataPointer + 2), *(offsetDataPointer + 1), *offsetDataPointer, ColorModel.RGB.ColorDepth.TwentyFour);

                        offsetDataPointer += bytesPerPixel;
                    }
                });
            }

            bitmap.UnlockBits(bitmapData);

            // Return the colour data
            return colorData;
        }

        public static byte BytesPerPixel(this Bitmap bitmap)
        {
            if (bitmap.PixelFormat == PixelFormat.Canonical ||
                bitmap.PixelFormat == PixelFormat.Format32bppArgb ||
                bitmap.PixelFormat == PixelFormat.Format32bppPArgb ||
                bitmap.PixelFormat == PixelFormat.Format32bppRgb)
            {
                return 32 / 8;
            }
            else if (bitmap.PixelFormat == PixelFormat.Format24bppRgb)
            {
                return 24 /8;
            }
            else if (bitmap.PixelFormat == PixelFormat.Format16bppArgb1555 ||
                     bitmap.PixelFormat == PixelFormat.Format16bppGrayScale ||
                     bitmap.PixelFormat == PixelFormat.Format16bppRgb555 ||
                     bitmap.PixelFormat == PixelFormat.Format16bppRgb565)
            {
                return 16 / 8;
            }
            else if (bitmap.PixelFormat == PixelFormat.Format8bppIndexed)
            {
                return 8 /8;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Unhandled bitmap pixelformat");
            }
        }
    }
}
