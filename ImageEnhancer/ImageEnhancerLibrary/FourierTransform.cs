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

        public void Create()
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
            this.CalculateFourierPeak();
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
            }
        }

        private Complex CalculateFourierPeak()
        {
            return this.fourierMagnitudeArray.Cast<int>().Max();
        }

        private Color ComplexToColor(Complex complexValue)
        {
            var colorValue = (int) ((255 / Math.Log(1 + Math.Abs(this.fourierPeak.Magnitude))) * Math.Log(1 + Math.Abs(complexValue.Magnitude)));

            return Color.FromArgb(colorValue, colorValue, colorValue);
        }

        public int[,] GetRawFourierMagnitudeArray()
        {
            return this.fourierMagnitudeArray;
        }

        public Bitmap GetFourierMagnitudeBitmap()
        {
            Bitmap image;

            using (var stream = new MemoryStream(this.ColorArrayToByteArray(this.fourierMagnitudeColorArray)))
            {
                image = new Bitmap(stream);
            }

            return image;
        }

        private byte[] ColorArrayToByteArray(Color[,] inputArray)
        {
            return inputArray.Cast<int>().Select(x => (byte)x).ToArray();
        }
    }
}
