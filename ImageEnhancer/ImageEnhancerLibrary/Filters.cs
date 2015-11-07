using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;

namespace ImageEnhancerLibrary
{
    public static class Filters
    {
        public static PixelArray RemovePeriodicNoise(PixelArray imageData)
        {
            // Array must be in non-shifted mode, purely for ease of use
            var width = imageData.width;
            var height = imageData.height;

            var noiseReduced = imageData;

            var castPixels = imageData.pixels.ConvertAll2D<Color, int>(new Converter<Color, int>(value => (int)value.ToGreyscale())).Flatten();

            var pixelMaximum = castPixels.Max();
            var pixelMean = castPixels.Average();

            var pixelThreshold = (int)(pixelMaximum + pixelMean) / 2;

            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    // If current pixel is over the threshold value, its a spike
                    if (imageData.pixels[j, i].ToGreyscale() >= pixelThreshold)
                    {
                        // Remove the value
                        noiseReduced.complexPixels = Filters.NotchFilter(imageData.complexPixels, j, i, 2, Complex.Zero);
                    }
                }
            }

            noiseReduced.PropagateComplex(true);

            return noiseReduced;
        }

        public static PixelArray RemovePeriodicNoise(PixelArray imageData, PixelArray maskedImageData)
        {
            // Array must be in non-shifted mode, purely for ease of use
            var width = imageData.width;
            var height = imageData.height;

            var noiseReduced = new PixelArray(imageData.complexPixels);

            var castPixels = maskedImageData.pixels.ConvertAll2D<Color, int>(new Converter<Color, int>(value => (int)value.ToGreyscale())).Flatten();

            var pixelMaximum = castPixels.Max();
            var pixelMean = castPixels.Average();

            var pixelThreshold = (int)(pixelMaximum + pixelMean) / 2;

            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    // If current pixel is over the threshold value, its a spike
                    if (maskedImageData.pixels[j, i].ToGreyscale() >= pixelThreshold)
                    {
                        // Remove the value
                        noiseReduced.complexPixels = Filters.NotchFilter(imageData.complexPixels, j, i, 2, Complex.Zero);
                    }
                }
            }

            noiseReduced.PropagateComplex(true);

            return noiseReduced;
        }

        public static T[,] NotchFilter<T>(T[,] inputArray, int x, int y, int radius, T notchedValue)
        {
            var notchedData = inputArray;

            if (radius == 0)
            {
                return notchedData;
            }
            else if (radius == 1)
            {
                notchedData[x, y] = notchedValue;
            }
            else
            {
                var xMin = (x - radius) < 0 ? 0 : (x - radius);
                var xMax = (x + radius) >= notchedData.GetLength(0) ? notchedData.GetLength(0) : (x + radius + 1);

                var yMin = (y - radius) < 0 ? 0 : (y - radius);
                var yMax = (y + radius) >= notchedData.GetLength(1) ? notchedData.GetLength(1) : (y + radius + 1);

                for (var i = xMin; i < xMax; i++)
                {
                    for (var j = yMin; j < yMax; j++)
                    {
                        notchedData[i, j] = notchedValue;
                    }
                }
            }

            return notchedData;
        }

        public static PixelArray HighPassFourierFilter(PixelArray imageData, int radius)
        {
            var width = imageData.width;
            var height = imageData.height;

            var highPassedData = new PixelArray(imageData.complexPixels);

            highPassedData.complexPixels = Filters.HighPassFourierFilter(highPassedData.complexPixels, radius, Complex.Zero);

            highPassedData.PropagateComplex(true);

            return highPassedData;
        }

        public static T[,] HighPassFourierFilter<T>(T[,] inputArray, int radius, T insideValue)
        {
            var highPassedData = inputArray;

            if (radius == 0)
            {
                return highPassedData;
            }
            else
            {
                highPassedData = Filters.HighPassFourierFilter(highPassedData, 0, 0, radius, insideValue);
                highPassedData = Filters.HighPassFourierFilter(highPassedData, highPassedData.GetLength(0)-1, 0, radius, insideValue);
                highPassedData = Filters.HighPassFourierFilter(highPassedData, 0, highPassedData.GetLength(1)-1, radius, insideValue);
                highPassedData = Filters.HighPassFourierFilter(highPassedData, highPassedData.GetLength(0)-1, highPassedData.GetLength(1)-1, radius, insideValue);
            }

            return highPassedData;
        }

        public static T[,] HighPassFourierFilter<T>(T[,] inputArray, int x, int y, int radius, T insideValue)
        {
            var highPassedData = inputArray;

            if (radius == 0)
            {
                return highPassedData;
            }
            else
            {
                var xMin = (x - radius) < 0 ? 0 : (x - radius);
                var xMax = (x + radius) >= highPassedData.GetLength(0) ? highPassedData.GetLength(0) : (x + radius + 1);

                var yMin = (y - radius) < 0 ? 0 : (y - radius);
                var yMax = (y + radius) >= highPassedData.GetLength(1) ? highPassedData.GetLength(1) : (y + radius + 1);

                for (var i = xMin; i < xMax; i++)
                {
                    for (var j = yMin; j < yMax; j++)
                    {
                        if (Filters.IsWithinRadius(i, j, x, y, radius))
                        {
                            highPassedData[i, j] = insideValue;
                        }
                    }
                }
            }

            return highPassedData;
        }

        public static T[,] LowPassFourierFilter<T>(T[,] inputArray, int x, int y, int radius, T outsideValue)
        {
            var lowPassedData = inputArray;

            if (radius == 0)
            {
                return lowPassedData;
            }
            else
            {
                var xMin = (x - radius) < 0 ? 0 : (x - radius);
                var xMax = (x + radius) >= lowPassedData.GetLength(0) ? lowPassedData.GetLength(0) : (x + radius + 1);

                var yMin = (y - radius) < 0 ? 0 : (y - radius);
                var yMax = (y + radius) >= lowPassedData.GetLength(1) ? lowPassedData.GetLength(1) : (y + radius + 1);

                for (var i = xMin; i < xMax; i++)
                {
                    for (var j = yMin; j < yMax; j++)
                    {
                        if (!Filters.IsWithinRadius(i, j, x, y, radius))
                        {
                            lowPassedData[i, j] = outsideValue;
                        }
                    }
                }
            }

            return lowPassedData;
        }

        private static bool IsWithinRadius(int centerX, int centerY, int x, int y, int radius)
        {
            return Math.Sqrt(Math.Abs(centerX - x) + Math.Abs(centerY - y)) <= radius;
        }

        public static T[,] MedianFilter<T>(T[,] inputArray, int windowSize)
        {
            var medianData = inputArray;

            if (windowSize == 0)
            {
                return medianData;
            }
            else
            {

            }

            return medianData;
        }
    }
}
