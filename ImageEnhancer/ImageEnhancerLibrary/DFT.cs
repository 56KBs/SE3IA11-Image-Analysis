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

        private int[][] pixelArray { get; set; }

        private List<List<Complex>> fourierArray { get; set; }

        private int height { get; set; }

        private int width { get; set; }

        public DFT(Bitmap original)
        {
            this.original = original;
            this.height = this.original.Height;
            this.width = this.original.Width;
            this.pixelArray = new int[this.height][this.width];
            this.BuildComplexArray();
            this.fourierArray = this.Run2DDFT();
        }

        private void BuildComplexArray()
        {
            for (var i = 0; i <= original.Height; i++)
            {
                for (var j = 0; j <= original.Width; j++)
                {
                    this.pixelArray[j][i] = original.GetPixel(j, i).ToGreyscale();
                }
            }
        }

        public Complex CalculateDFT(Point UV, List<int> samples)
        {
            var sampleCount = samples.Count;
            var returnValue = Complex.Zero;

            for (var y = 0; y < samples.Count; y++)
            {
                returnValue += samples[y] * Complex.Exp(
                    new Complex(
                        0,
                        ((-2 * Math.PI * UV.Y * y) / samples.Count))
                    );
            }

            return returnValue;
        }

        public List<List<Complex>> Run2DDFT()
        {
            var DFTArray = new List<List<Complex>>();

            for (var x = 0; x < this.width; x++)
            {
                for (var y = 0; y < this.height; y++)
                {
                    var listY = this.ExtractColumn(this.pixelArray, x);

                    var resultY = this.CalculateDFT(new Point(x, y), listY);

                    DFTArray[x][y] = 0;
                }
            }

            return DFTArray;
        }

        private List<int> ExtractColumn(List<List<int>> samples, int column)
        {
            var columnData = new List<int>();

            for (int y = 0; y < samples.Count; y++)
            {
                columnData.Add(samples[y][column]);
            }

            return columnData;
        }
    }
}
