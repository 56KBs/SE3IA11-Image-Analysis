using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Numerics;

namespace ImageEnhancerLibrary
{
    public class FFT : FourierTransform
    {
        public Bitmap original { get; set; }

        private Complex[][] fourierArrayY { get; set; }

        public FFT(Bitmap original) : base(original)
        {
            this.original = original;
        }

        public override void Create()
        {
            var fourierArray = new Complex[this.width, this.height];
            var UV = new Point(-1, -1);

            this.BuildFourierArrayY();

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

        protected override Complex Fourier2D(Point UV)
        {
            var fourierValue = Complex.Zero;
            var currentPoint = new Point(-1, -1);

            while (++currentPoint.X < this.width)
            {
                var complexValue = new Complex(0, ((-2 * Math.PI * UV.X * currentPoint.X) / this.width));

                fourierValue += fourierArrayY[currentPoint.X][UV.Y] * Complex.Exp(complexValue);
            }

            return fourierValue;
        }

        protected Complex[] Fourier1D(Complex[] fourierArray)
        {
            if (fourierArray.Length > 1)
            {
                var fourierLength = fourierArray.Length;
                var fourierHalfLength = fourierLength / 2;

                var splitFourierArray = this.splitArray(fourierArray);

                var evenArray = splitFourierArray[0];
                var oddArray = splitFourierArray[1];

                evenArray = this.Fourier1D(evenArray);
                oddArray = this.Fourier1D(oddArray);

                var x = -1;

                while (++x < fourierHalfLength)
                {
                    var complexValue = new Complex(0, (-2 * Math.PI * x) / fourierArray.Length);
                    var t = Complex.Exp(complexValue) * oddArray[x];

                    fourierArray[x] = evenArray[x] + t;
                    fourierArray[x + fourierHalfLength] = evenArray[x] - t;
                }
            }

            return fourierArray;
        }

        private void BuildFourierArrayY()
        {
            fourierArrayY = new Complex[this.width][];

            var currentPoint = new Point(-1, -1);
            while (++currentPoint.X < this.width)
            {
                fourierArrayY[currentPoint.X] = this.Fourier1D(this.ExtractColumn(this.fourierArray, currentPoint.X));
            }
        }

        private Complex[][] splitArray(Complex[] fourierArray)
        {
            var fourierLength = fourierArray.Length;
            var fourierHalfLength = fourierLength / 2;

            var splitArray = new Complex[2][];

            if (fourierLength % 2 != 0)
            {
                splitArray[0] = new Complex[fourierHalfLength + 1];
            }
            else
            {
                splitArray[0] = new Complex[fourierHalfLength];
            } 

            splitArray[1] = new Complex[fourierHalfLength];

            var evenCounter = 0;
            var oddCounter = 0;

            for (var i = 0; i < fourierLength; i++)
            {
                if (i % 2 == 0)
                {
                    splitArray[0][evenCounter] = fourierArray[i];
                    evenCounter++;
                }
                else
                {
                    splitArray[1][oddCounter] = fourierArray[i];
                    oddCounter++;
                }
            }

            return splitArray;
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
