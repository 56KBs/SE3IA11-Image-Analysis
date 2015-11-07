using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace ImageEnhancerLibrary
{
    public static class Fourier
    {
        public static PixelArray Transform(PixelArray data)
        {
            return Fourier.FFT2D(data);
        }

        public static PixelArray InverseTransform(PixelArray data)
        {
            var inverseData = new PixelArray(data.complexPixels);

            inverseData.complexPixels = inverseData.complexPixels.ConvertAll2D<Complex, Complex>(new Converter<Complex, Complex>(value => Complex.Conjugate(value)));
            inverseData = Fourier.FFT2D(inverseData);
            inverseData.complexPixels = inverseData.complexPixels.ConvertAll2D<Complex, Complex>(new Converter<Complex, Complex>(value => Complex.Conjugate(value)));

            var length = inverseData.complexPixels.Length;
            inverseData.complexPixels = inverseData.complexPixels.ConvertAll2D<Complex, Complex>(new Converter<Complex, Complex>(value => (value/length)));

            inverseData.PropagateComplex(false);

            return inverseData;
        }

        private static PixelArray FFT2D(PixelArray data)
        {
            var temporaryData = new PixelArray(data.complexPixels);
            temporaryData.PadArrays();

            var FFTData = new PixelArray(temporaryData.width, temporaryData.height);
            var columnFourier = new Complex[temporaryData.width, temporaryData.height];

            for (var i = 0; i < temporaryData.width; i++)
            {
                columnFourier.SetColumn(Fourier.FFT1D(temporaryData.complexPixels.GetColumn(i)), i);
            }

            for (var j = 0; j < temporaryData.height; j++)
            {
                FFTData.complexPixels.SetRow(Fourier.FFT1D(columnFourier.GetRow(j)), j);
            }

            FFTData.PropagateComplex(true);

            return FFTData;
        }

        private static Complex[] FFT1D(Complex[] data)
        {
            var complexLength = data.Length;

            if (complexLength <= 1)
            {
                return data;

            }

            var complexHalfLength = complexLength / 2;

            var splitArray = data.Split();

            var evenArray = Fourier.FFT1D(splitArray[0]);
            var oddArray = Fourier.FFT1D(splitArray[1]);

            for (var i = 0; i < complexHalfLength; i++)
            {
                var complexValue = new Complex(0, ((-2 * Math.PI * i) / complexLength));
                var t = Complex.Exp(complexValue) * oddArray[i];

                data[i] = evenArray[i] + t;
                data[i + complexHalfLength] = evenArray[i] - t;
            }

            return data;
        }
    }
}
