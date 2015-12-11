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
            Bitmap workingBitmap = null;

            // Convert 8bppIndexed into 24bppRGB
            // http://stackoverflow.com/questions/2016406/converting-bitmap-pixelformats-in-c-sharp
            if (bitmap.PixelFormat == PixelFormat.Format8bppIndexed)
            {
                var newBitmap = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format24bppRgb);
                using (Graphics gr = Graphics.FromImage(newBitmap))
                {
                    gr.DrawImage(bitmap, new Rectangle(0, 0, newBitmap.Width, newBitmap.Height));
                }

                workingBitmap = newBitmap;
            }
            else
            {
                workingBitmap = bitmap;
            }

            // Calculate information about the bitmap
            var bitmapHeight = workingBitmap.Height;
            var bitmapWidth = workingBitmap.Width;
            var pixelFormat = workingBitmap.PixelFormat;

            // Get the bytes per pixel
            var bytesPerPixel = workingBitmap.BytesPerPixel();

            // Make a new colour array
            var colorData = new ColorModel.RGB[bitmapWidth, bitmapHeight];         

            // Lock the data into memory
            BitmapData bitmapData = workingBitmap.LockBits(
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
                        colorData[i, j] = new ColorModel.RGB(*(offsetDataPointer + 2), *(offsetDataPointer + 1), *offsetDataPointer, ColorModel.RGB.ColorDepth.TwentyFour);

                        offsetDataPointer += bytesPerPixel;
                    }
                });
            }

            // Unlock the data
            workingBitmap.UnlockBits(bitmapData);

            // Return the colour data
            return colorData;
        }

        /// <summary>
        /// Calculate the number of bytes used per pixel in the bitmap
        /// </summary>
        /// <param name="bitmap">Bitmap to read from</param>
        /// <returns>Byte containing the size of the data</returns>
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
                return 8 / 8;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Unhandled bitmap pixelformat");
            }
        }
    }
}
