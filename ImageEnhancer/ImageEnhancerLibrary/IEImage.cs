using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Numerics;

namespace ImageEnhancerLibrary
{
    public class IEImage
    {
        /// <summary>
        /// Bitmap that this was constructed with
        /// </summary>
        private Bitmap image { get; set; }

        /// <summary>
        /// The data from the image
        /// </summary>
        private PixelArray imageData { get; set; }

        /// <summary>
        /// Width of the image
        /// </summary>
        public int width
        {
            get
            {
                return this.imageData.pixels.GetLength(0);
            }
        }

        /// <summary>
        /// Height of the time
        /// </summary>
        public int height
        {
            get
            {
                return this.imageData.pixels.GetLength(1);
            }
        }

        /// <summary>
        /// Build an IEImage from a bitmap
        /// </summary>
        /// <param name="image">Image to build from</param>
        public IEImage(Bitmap image)
        {
            this.image = image;

            // Create the image data from the pixel data of the image
            this.imageData = new PixelArray(image.GetPixelArray());
        }

        /// <summary>
        /// Create IEImage from a pixelarray
        /// </summary>
        /// <param name="data">PixelArray data</param>
        public IEImage(PixelArray data)
        {
            this.imageData = data;
            // Set the image based on the bitmap generated
            this.image = this.GetBitmap();
        }

        /// <summary>
        /// Get a bitmap from our data
        /// </summary>
        /// <returns>Bitmap</returns>
        public Bitmap GetBitmap()
        {
            // Return a bitmap from our pixels
            return this.CreateBitmap(imageData.pixels);
        }

        /// <summary>
        /// Get a shifted bitmap from our data
        /// </summary>
        /// <returns>Bitmap</returns>
        public Bitmap GetShiftedBitmap()
        {
            // Return a bitmap from the shifted data
            return this.CreateBitmap(this.ShiftArray(this.imageData.pixels));
        }

        /// <summary>
        /// Create a bitmap
        /// </summary>
        /// <param name="pixelData">Data to make a bitmap with</param>
        /// <returns>A bitmap</returns>
        private Bitmap CreateBitmap(Color[,] pixelData)
        {
            // Get the size of the data
            var width = pixelData.GetLength(0);
            var height = pixelData.GetLength(1);

            // Create a new bitmap
            var bitmap = new Bitmap(width, height);

            // Loop over the data, set each bitmap pixel to our pixeldata value
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    bitmap.SetPixel(i, j, pixelData[i, j]);
                }
            }

            // Return the bitmap
            return bitmap;
        }

        /// <summary>
        /// Get the image data
        /// </summary>
        /// <returns>PixelArray of image data</returns>
        public PixelArray GetFullPixelData()
        {
            return this.imageData;
        }

        /// <summary>
        /// Get the colour pixel data only
        /// </summary>
        /// <returns>2D color array</returns>
        public Color[,] GetColorPixelData()
        {
            return this.imageData.pixels;
        }

        /// <summary>
        /// Set the pixel data of this IEImage
        /// </summary>
        /// <param name="data">Input data to set</param>
        public void SetPixelData(PixelArray data)
        {
            this.imageData = data;
        }

        /// <summary>
        /// Shift the array given
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="inputArray">Input array to shift</param>
        /// <returns>Shifted version of the array</returns>
        private T[,] ShiftArray<T>(T[,] inputArray)
        {
            // Get the dimensions and set up the new shifted array
            var width = inputArray.GetLength(0);
            var height = inputArray.GetLength(1);
            var shiftedArray = new T[width, height];

            // Loop over the data
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    // Based on the quadrant we are in, copy it to the relevant one
                    switch (this.CalculateQuadrant(i, j))
                    {
                        case 1:
                            // Copy to quadrant 4
                            shiftedArray[i + width / 2, j + height / 2] = inputArray[i, j];
                            break;
                        case 2:
                            // Copy to quadrant 3
                            shiftedArray[i - width / 2, j + height / 2] = inputArray[i, j];
                            break;
                        case 3:
                            // Copy to quadrant 2
                            shiftedArray[i + width / 2, j - height / 2] = inputArray[i, j];
                            break;
                        case 4:
                            // Copy to quadrant 4
                            shiftedArray[i - width / 2, j - height / 2] = inputArray[i, j];
                            break;
                    }
                }
            }

            // Return the data
            return shiftedArray;
        }

        /// <summary>
        /// Calculate the quadrant this data is in
        /// </summary>
        /// <param name="i">X value</param>
        /// <param name="j">Y value</param>
        /// <returns>Quadrant number</returns>
        private int CalculateQuadrant(int i, int j)
        {
            // If the data is on the left side
            if (i < this.imageData.width / 2)
            {
                // Data is in the top left
                if (j < this.imageData.height / 2)
                {
                    return 1;
                }
                else // Data is in the bottom left
                {
                    return 3;
                }
            }
            else
            {
                if (j < this.imageData.height / 2)
                {
                    // Data is in the top right
                    return 2;
                }
                else
                {
                    // Data is in the bottom right
                    return 4;
                }
            }
        }
    }
}
