using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Numerics;
using System.IO;

namespace ImageEnhancerLibrary
{
    public abstract class FourierTransform
    {
        protected int[,] pixelArray { get; set; }

        protected Complex[,] fourierArray { get; set; }

        protected Complex fourierPeak { get; set; }

        protected int[,] fourierMagnitudeArray { get; set; }

        protected Color[,] fourierMagnitudeColorArray { get; set; }

        protected int height { get; set; }

        protected int width { get; set; }

        public FourierTransform() { }

        public FourierTransform(Bitmap originalImage)
        {
            this.width = originalImage.Width;
            this.height = originalImage.Height;

            this.BuildPixelArray(originalImage);
            this.BuildFourierArray(originalImage);
        }

        public virtual void Create()
        {
            var fourierArray = new Complex[this.width, this.height];
            var UV = new Point(-1, -1);

            while (++UV.X < this.width)
            {
                while (++UV.Y < this.height)
                {
                    fourierArray[UV.X, UV.Y] = this.Fourier2D(UV);
                }

                UV.Y = -1;
            }

            this.fourierArray = fourierArray;

            this.UpdateFourierMagnitudeArray();
        }

        public void Update()
        {
            this.Create();
        }

        protected abstract Complex Fourier2D(Point UV);

        protected void BuildPixelArray(Bitmap image)
        {
            this.pixelArray = new int[image.Width, image.Height];

            for (var i = 0; i < image.Height; i++)
            {
                for (var j = 0; j < image.Width; j++)
                {
                    this.pixelArray[j, i] = image.GetPixel(j, i).ToGreyscale();
                }
            }
        }

        protected void BuildFourierArray(Bitmap image)
        {
            this.fourierArray = new Complex[image.Width, image.Height];
            this.fourierMagnitudeArray = new int[image.Width, image.Height];
            this.fourierMagnitudeColorArray = new Color[image.Width, image.Height];

            for (var i = 0; i < image.Height; i++)
            {
                for (var j = 0; j < image.Width; j++)
                {
                    this.fourierArray[j, i] = new Complex(this.pixelArray[j, i], 0);
                }
            }
        }

        public Complex[,] GetRawFourierArray()
        {
            return this.fourierArray;
        }

        public void UpdateFourierMagnitudeArray()
        {
            var currentPoint = new Point(-1, -1);

            while (++currentPoint.X < this.width)
            {
                while (++currentPoint.Y < this.height)
                {
                    this.fourierMagnitudeArray[currentPoint.X, currentPoint.Y] = (int) this.fourierArray[currentPoint.X, currentPoint.Y].Magnitude;
                }
            }

            this.fourierPeak = this.CalculateFourierPeak();

            currentPoint = new Point(-1, -1);

            while (++currentPoint.X < this.width)
            {
                while (++currentPoint.Y < this.height)
                {
                    this.fourierMagnitudeColorArray[currentPoint.X, currentPoint.Y] = this.ComplexToColor(this.fourierArray[currentPoint.X, currentPoint.Y]);
                }

                currentPoint.Y = -1;
            }
        }

        private Complex CalculateFourierPeak()
        {
            return this.fourierMagnitudeArray.Cast<int>().Max();
        }

        private Color ComplexToColor(Complex complexValue)
        {
            var colorValue = (int) ((255 / Math.Log(1 + Math.Abs(this.fourierPeak.Magnitude))) * Math.Log(1 + Math.Abs(complexValue.Magnitude)));

            return Color.FromArgb(0, colorValue, colorValue, colorValue);
        }

        public int[,] GetRawFourierMagnitudeArray()
        {
            return this.fourierMagnitudeArray;
        }

        public Bitmap GetFourierMagnitudeBitmap()
        {
            return this.CreateBitmap(this.fourierMagnitudeColorArray);
        }

        public Bitmap GetShiftedFourierMagnitudeBitmap()
        {
            var shiftedMagnitudeArray = this.GetShiftedMagnitudeColorArray();

            return CreateBitmap(shiftedMagnitudeArray);
        }

        private Bitmap CreateBitmap(Color[,] colorArray)
        {
            var magnitudeBitmap = new Bitmap(this.width, this.height);

            var currentPoint = new Point(-1, -1);

            while (++currentPoint.X < this.width)
            {
                while (++currentPoint.Y < this.height)
                {
                    magnitudeBitmap.SetPixel(currentPoint.X, currentPoint.Y, colorArray[currentPoint.X, currentPoint.Y]);
                }

                currentPoint.Y = -1;
            }

            return magnitudeBitmap;
        }

        public Color[,] GetShiftedMagnitudeColorArray()
        {
            var shiftedMagnitudeArray = new Color[this.width, this.height];

            // Quadrant shift

            for (var i = 0; i < this.width; i++)
            {
                for (var j = 0; j < this.height; j++)
                {
                    switch (this.CalculateQuadrant(i, j))
                    {
                        case 1:
                            // Copy to quadrant 4
                            shiftedMagnitudeArray[i + this.width / 2, j + this.height / 2] = this.fourierMagnitudeColorArray[i, j];
                            break;
                        case 2:
                            // Copy to quadrant 3
                            shiftedMagnitudeArray[i - this.width / 2, j + this.height / 2] = this.fourierMagnitudeColorArray[i, j];
                            break;
                        case 3:
                            // Copy to quadrant 2
                            shiftedMagnitudeArray[i + this.width / 2, j - this.height / 2] = this.fourierMagnitudeColorArray[i, j];
                            break;
                        case 4:
                            // Copy to quadrant 4
                            shiftedMagnitudeArray[i - this.width / 2, j - this.height / 2] = this.fourierMagnitudeColorArray[i, j];
                            break;
                    }
                }
            }

            return shiftedMagnitudeArray;
        }

        private int CalculateQuadrant(int x, int y)
        {
            if (x < this.width / 2)
            {
                if (y < this.height / 2)
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
                if (y < this.height / 2)
                {
                    return 2;
                }
                else
                {
                    return 4;
                }
            }
        }

        private byte[] ColorArrayToByteArray(Color[,] inputArray)
        {
            return inputArray.Cast<Color>().Select(x => (byte)x.R).ToArray();
        }
    }
}
