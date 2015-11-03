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

        public FFT(Bitmap original) : base(original)
        {
            this.original = original;
        }

        protected override Complex Fourier2D(Point UV)
        {
            var fourierValue = Complex.Zero;
            var currentPoint = new Point(-1, -1);

            while (++currentPoint.X < this.width)
            {
                var columnValues = this.ExtractColumn(this.fourierArray, currentPoint.X);
                var columnFourier = this.Fourier1D(columnValues);

                var complexValue = new Complex(0, ((-2 * Math.PI * UV.X * currentPoint.X) / this.width));

                fourierValue += columnValues[UV.Y] * Complex.Exp(complexValue);
            }

            return fourierValue;
        }

        protected Complex[] Fourier1D(Complex[] fourierArray)
        {
            if (fourierArray.Length > 1)
            {
                var fourierLength = fourierArray.Length;
                var fourierHalfLength = fourierLength / 2;

                var evenArray = fourierArray.Where((value, index) => index % 2 == 0).ToArray();
                var oddArray = fourierArray.Where((value, index) => index % 2 != 0).ToArray();

                evenArray = this.Fourier1D(evenArray);
                oddArray = this.Fourier1D(oddArray);

                var currentPoint = new Point(-1, -1);

                while (++currentPoint.X < fourierHalfLength)
                {
                    var complexValue = new Complex(0, (-2 * Math.PI * currentPoint.X) / fourierArray.Length);
                    var t = Complex.Exp(complexValue) * oddArray[currentPoint.X];

                    fourierArray[currentPoint.X] = evenArray[currentPoint.X] + t;
                    fourierArray[currentPoint.X + fourierHalfLength] = evenArray[currentPoint.X] - t;
                }
            }

            return fourierArray;
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
