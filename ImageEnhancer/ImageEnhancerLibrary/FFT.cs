using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Numerics;

namespace ImageEnhancerLibrary
{
    public class FFT
    {
        public Bitmap original { get; set; }

        // Stored as x,y
        private int[,] pixelArray { get; set; }

        public Complex[,] fourierArray { get; set; }

        private int height { get; set; }

        private int width { get; set; }

        public FFT(Bitmap original)
        {
            this.original = original;
            this.height = this.original.Height;
            this.width = this.original.Width;
            this.pixelArray = new int[this.width, this.height];
            this.BuildPixelArray();
            this.fourierArray = new Complex[this.width, this.height];
            this.BuildFourierArray();
            this.fourierArray = this.Run2DFFT();
        }

        private void BuildPixelArray()
        {
            for (var i = 0; i < original.Height; i++)
            {
                for (var j = 0; j < original.Width; j++)
                {
                    this.pixelArray[j, i] = original.GetPixel(j, i).ToGreyscale();
                }
            }
        }

        private void BuildFourierArray()
        {
            for (var i = 0; i < original.Height; i++)
            {
                for (var j = 0; j < original.Width; j++)
                {
                    this.fourierArray[j, i] = new Complex(this.pixelArray[j, i], 0);
                }
            }
        }

        public Complex[,] Run2DFFT()
        {
            // In place method
            var DFTArray = new Complex[this.width, this.height];
            var UV = new Point(-1, -1);

            while (++UV.X < this.width)
            {
                while (++UV.Y < this.height)
                {
                    DFTArray[UV.X, UV.Y] = this.Calculate2DFFT(UV);
                }

                UV.Y = -1;
            }

            return DFTArray;
        }

        private Complex[] Calculate1DFFT(Complex[] fourierArray)
        {
            if (fourierArray.Length == 1)
            {
                return fourierArray;
            }
            else
            {
                Complex[] evenFourierArray = fourierArray.Where((value, index) => index % 2 == 0).ToArray();
                Complex[] oddFourierArray = fourierArray.Where((value, index) => index % 2 != 0).ToArray();

                evenFourierArray = Calculate1DFFT(evenFourierArray);
                oddFourierArray = Calculate1DFFT(oddFourierArray);

                var currentPoint = new Point(-1, -1);

                while (++currentPoint.X < (fourierArray.Length / 2))
                {
                    var complexPower = new Complex(0, (-2 * Math.PI * currentPoint.X) / fourierArray.Length);
                    var t = Complex.Exp(complexPower) * oddFourierArray[currentPoint.X];
                    fourierArray[currentPoint.X] = evenFourierArray[currentPoint.X] + t;
                    fourierArray[currentPoint.X + (fourierArray.Length / 2)] = evenFourierArray[currentPoint.X] - t;
                }
            }

            return fourierArray;
        }

        private Complex Calculate2DFFT(Point UV)
        {
            var returnValue = Complex.Zero;
            var currentPoint = new Point(-1, -1);

            while (++currentPoint.X < this.width)
            {
                var listY = this.ExtractColumn(this.fourierArray, currentPoint.X);
                var resultY = this.Calculate1DFFT(listY);

                var complexPower = new Complex(0, ((-2 * Math.PI * UV.X * currentPoint.X) / this.width));

                returnValue += resultY[UV.Y] * Complex.Exp(complexPower);
            }

            return returnValue;
        }

        private Complex[] ExtractColumn(Complex[,] samples, int x)
        {
            var columnData = new Complex[this.height];

            for (int y = 0; y < this.height; y++)
            {
                columnData[y] = samples[x, y];
            }

            return columnData;
        }
    }
}
