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
        private Bitmap image { get; set; }

        private PixelArray imageData { get; set; }

        public int width
        {
            get
            {
                return this.imageData.pixels.GetLength(0);
            }
        }

        public int height
        {
            get
            {
                return this.imageData.pixels.GetLength(1);
            }
        }

        public IEImage(Bitmap image)
        {
            this.image = image;

            this.imageData = new PixelArray(image.GetPixelArray());
        }

        public IEImage(PixelArray data)
        {
            this.imageData = data;
            this.image = this.GetBitmap();
        }

        public Bitmap GetBitmap()
        {
            return this.CreateBitmap(imageData.pixels);
        }

        public Bitmap GetShiftedBitmap()
        {
            return this.CreateBitmap(this.ShiftArray(this.imageData.pixels));
        }

        private Bitmap CreateBitmap(Color[,] pixelData)
        {
            var width = pixelData.GetLength(0);
            var height = pixelData.GetLength(1);

            var bitmap = new Bitmap(width, height);

            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    bitmap.SetPixel(i, j, pixelData[i, j]);
                }
            }

            return bitmap;
        }

        public PixelArray GetFullPixelData()
        {
            return this.imageData;
        }

        public Color[,] GetColorPixelData()
        {
            return this.imageData.pixels;
        }

        public Complex[,] GetComplexPixelData()
        {
            return this.imageData.complexPixels;
        }

        private T[,] ShiftArray<T>(T[,] inputArray)
        {
            var width = inputArray.GetLength(0);
            var height = inputArray.GetLength(1);
            var shiftedArray = new T[width, height];

            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
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

            return shiftedArray;
        }

        private int CalculateQuadrant(int i, int j)
        {
            if (i < this.imageData.width / 2)
            {
                if (j < this.imageData.height / 2)
                {
                    return 1;
                }
                else
                {
                    return 3;
                }
            }
            else
            {
                if (j < this.imageData.height / 2)
                {
                    return 2;
                }
                else
                {
                    return 4;
                }
            }
        }
    }
}
