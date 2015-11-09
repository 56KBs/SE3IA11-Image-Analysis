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
        /// <summary>
        /// Fourier transform the data given 
        /// </summary>
        /// <param name="data">Data to transform</param>
        /// <returns>Pixel array of fourier transformed data</returns>
        public static PixelArray Transform(PixelArray data)
        {
            // Run FFT2D
            return Fourier.FFT2D(data);
        }

        /// <summary>
        /// Inverse transform the data given
        /// </summary>
        /// <param name="data">Data to inverse transform</param>
        /// <returns>Inverse transformed data</returns>
        public static PixelArray InverseTransform(PixelArray data)
        {
            // Make a new pixelarray from our own data
            var inverseData = new PixelArray(data.complexPixels);

            // Convert the complex pixels via conjugation (negate the imaginary)
            inverseData.complexPixels = inverseData.complexPixels.ConvertAll2D<Complex, Complex>(new Converter<Complex, Complex>(value => Complex.Conjugate(value)));
            // Run FFT on the data
            inverseData = Fourier.FFT2D(inverseData);
            // Conjugate the complex pixels again
            inverseData.complexPixels = inverseData.complexPixels.ConvertAll2D<Complex, Complex>(new Converter<Complex, Complex>(value => Complex.Conjugate(value)));

            // Get the length of the data
            var length = inverseData.complexPixels.Length;
            // Divide the complex pixel value by the length of the data to scale correctly
            inverseData.complexPixels = inverseData.complexPixels.ConvertAll2D<Complex, Complex>(new Converter<Complex, Complex>(value => (value/length)));

            // Push the complex data into the colour array but don't scale the result
            inverseData.PropagateComplex(false);

            // Return the inversed data
            return inverseData;
        }

        /// <summary>
        /// Run a 2D FFT
        /// </summary>
        /// <param name="data">Data to FFT</param>
        /// <returns>FFT version of the data</returns>
        private static PixelArray FFT2D(PixelArray data)
        {
            // Build a new temporary pixel array and pad accordingly
            var temporaryData = new PixelArray(data.complexPixels);
            temporaryData.PadArrays();

            // Build pixel array of the FFT data
            var FFTData = new PixelArray(temporaryData.width, temporaryData.height);
            // Build 2D complex for the columns
            var columnFourier = new Complex[temporaryData.width, temporaryData.height];

            // Run 1D FFT on each column
            for (var i = 0; i < temporaryData.width; i++)
            {
                columnFourier.SetColumn(Fourier.FFT1D(temporaryData.complexPixels.GetColumn(i)), i);
            }

            // Run 1D FFT on each row from the column FFT data just generated
            for (var j = 0; j < temporaryData.height; j++)
            {
                FFTData.complexPixels.SetRow(Fourier.FFT1D(columnFourier.GetRow(j)), j);
            }

            // Push the complex data into the colour array and scale for viewing
            FFTData.PropagateComplex(true);

            // Return the FFT data
            return FFTData;
        }

        /// <summary>
        /// 1D FFT on array
        /// </summary>
        /// <param name="data">Array of complex data to FFT</param>
        /// <returns>Value of FFT</returns>
        private static Complex[] FFT1D(Complex[] data)
        {
            // Get the length
            var complexLength = data.Length;

            // If the length is 1 just return the data
            if (complexLength <= 1)
            {
                return data;

            }

            // Get half the length
            var complexHalfLength = complexLength / 2;

            // Split the array in 2
            var splitArray = data.Split();

            // Fourier transform each of the split array and store as even and odd
            var evenArray = Fourier.FFT1D(splitArray[0]);
            var oddArray = Fourier.FFT1D(splitArray[1]);

            // Loop over half the length of the data
            for (var i = 0; i < complexHalfLength; i++)
            {
                // Calculate the 'twiddle factor'
                var complexValue = new Complex(0, ((-2 * Math.PI * i) / complexLength));
                var t = Complex.Exp(complexValue) * oddArray[i];

                // Add the data correctly
                data[i] = evenArray[i] + t;
                data[i + complexHalfLength] = evenArray[i] - t;
            }

            // Return the data
            return data;
        }
    }
}
