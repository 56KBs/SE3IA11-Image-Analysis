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

        private List<List<Complex>> complexArray { get; set; }

        private List<List<Complex>> fourierArray { get; set; }

        public void FFT(Bitmap original)
        {
            this.original = original;
            this.complexArray = new List<List<Complex>>();
        }

        private void BuildComplexArray()
        {
            for (var i = 0; i < original.Width; i++)
            {
                for (var j = 0; j < original.Height; j++)
                {
                    this.complexArray[i][j] = new Complex((double) original.GetPixel(i, j).ToGreyscale(), 0);
                }
            }
        }

        private void RunFFT(List<List<Complex>> complexArray)
        {
            if (complexArray.coun)
        }
    }
}
