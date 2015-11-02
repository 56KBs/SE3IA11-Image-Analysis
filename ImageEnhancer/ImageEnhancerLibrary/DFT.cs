using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Numerics;

namespace ImageEnhancerLibrary
{
    public class DFT
    {
        public Bitmap original { get; set; }

        // Stored as x,y
        private int[,] pixelArray { get; set; }

        private Complex[,] fourierArray { get; set; }

        private int height { get; set; }

        private int width { get; set; }

        public DFT(Bitmap original)
        {
            this.original = original;
            this.height = this.original.Height;
            this.width = this.original.Width;
            this.pixelArray = new int[this.width, this.height];
            this.BuildPixelArray();
            this.fourierArray = this.Run2DDFT();
        }

        private void BuildPixelArray()
        {
            for (var i = 0; i < original.Height; i++)
            {
                for (var j = 0; j < original.Width; j++)
                {
                    this.pixelArray[j,i] = original.GetPixel(j, i).ToGreyscale();
                }
            }
        }

        private Complex Calculate1DDFT(Point UV, int[] samples)
        {
            // Runs on an 'vertical' array
            var sampleCount = samples.Length;
            var returnValue = Complex.Zero;
            var currentPoint = new Point(-1, -1);
            
            while (++currentPoint.Y < sampleCount)
            {
                var complexPower = new Complex(0, ((-2 * Math.PI * UV.Y * currentPoint.Y) / sampleCount));
                returnValue += samples[currentPoint.Y] * Complex.Exp(complexPower);
            }

            return returnValue;
        }

        private Complex Calculate2DDFT(Point UV)
        {
            var returnValue = Complex.Zero;
            var currentPoint = new Point(-1, -1);

            while (++currentPoint.X < this.width)
            {
                var listY = this.ExtractColumn(this.pixelArray, currentPoint.X);
                var resultY = this.Calculate1DDFT(UV, listY);

                var complexPower = new Complex(0, ((-2 * Math.PI * UV.X * currentPoint.X) / this.width));
                returnValue += resultY * Complex.Exp(complexPower);
            }

            return returnValue;
        }

        public Complex[,] Run2DDFT()
        {
            // In place method
            var DFTArray = new Complex[this.width, this.height];
            var UV = new Point(-1, -1);

            while (++UV.X < this.width)
            {
                while (++UV.Y < this.height)
                {
                    DFTArray[UV.X, UV.Y] = this.Calculate2DDFT(UV);
                }

                UV.Y = -1;
            }

            return DFTArray;
        }

        private int[] ExtractColumn(int[,] samples, int x)
        {
            var columnData = new int[this.height];

            for (int y = 0; y < this.height; y++)
            {
                columnData[y] = samples[x, y];
            }

            return columnData;
        }
    }
}
