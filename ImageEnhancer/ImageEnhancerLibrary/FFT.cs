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

        private Complex[,] fourierArray { get; set; }

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

            var test = new Complex[8];
            test[0] = 0;
            test[1] = 0;
            test[2] = 400;
            test[3] = 440;
            test[4] = 480;
            test[5] = 0;
            test[6] = 0;
            test[7] = 0;


            var FFTArray = this.Calculate1DFFT(test);

            return new Complex[1,1];
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

                //fourierArray = this.JoinComplexArray(evenFourierArray, oddFourierArray);

                var currentPoint = new Point(-1, -1);

                while (++currentPoint.X < (fourierArray.Length / 2))
                {
                    var complexPower = new Complex(0, (-2 * Math.PI * currentPoint.X) / fourierArray.Length);
                    var t = Complex.Exp(complexPower) * oddFourierArray[currentPoint.X];
                    fourierArray[currentPoint.X] = evenFourierArray[currentPoint.X] + t;
                    fourierArray[currentPoint.X + (fourierArray.Length / 2)] = evenFourierArray[currentPoint.X] - t;
                }

                /*while (++currentPoint.X < (fourierArray.Length / 2))
                {
                    var t = fourierArray[currentPoint.X];
                    var complexConstant = Complex.Exp((-2 * Math.PI * currentPoint.X) / fourierArray.Length) * fourierArray[currentPoint.X + (fourierArray.Length/2)];
                    fourierArray[currentPoint.X] = t + complexConstant;
                    fourierArray[currentPoint.X + (fourierArray.Length / 2)] = t - complexConstant;
                }*/
            }

            return fourierArray;
        }

        private Complex[] JoinComplexArray(Complex[] first, Complex[] second)
        {
            var returnArray = new Complex[first.Length + second.Length];

            for (var i = 0; i < first.Length; i++)
            {
                returnArray[i] = first[i];
                returnArray[i + 1] = second[i];
            }

            return returnArray;
        }
    }
}
