using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Numerics;

namespace ImageEnhancerLibrary
{
    public class DFT : FourierTransform
    {
        public Bitmap original { get; set; }


        public DFT(Bitmap original) : base(original)
        {
            this.original = original;
        }

        protected Complex Fourier1D(Complex[] fourierArray, Point UV)
        {
            var sampleCount = fourierArray.Length;
            var returnValue = Complex.Zero;
            var currentPoint = new Point(-1, -1);

            while (++currentPoint.Y < sampleCount)
            {
                var complexPower = new Complex(0, ((-2 * Math.PI * UV.Y * currentPoint.Y) / sampleCount));
                returnValue += fourierArray[currentPoint.Y] * Complex.Exp(complexPower);
            }

            return returnValue;
        }

        protected override Complex Fourier2D(Point UV)
        {
            var fourierValue = Complex.Zero;
            var currentPoint = new Point(-1, -1);

            while (++currentPoint.X < this.width)
            {
                var columnValues = this.ExtractColumn(this.fourierArray, currentPoint.X);
                var columnFourier = this.Fourier1D(columnValues, UV);

                var complexPower = new Complex(0, ((-2 * Math.PI * UV.X * currentPoint.X) / this.width));
                fourierValue += columnFourier * Complex.Exp(complexPower);
            }

            return fourierValue;
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
