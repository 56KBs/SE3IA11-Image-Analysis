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
        /// <summary>
        /// Remove periodic noise from a FFT image, using a masked data version to ensure specific data is not removed
        /// </summary>
        /// <param name="imageData">FFT image data</param>
        /// <param name="maskedImageData">FFT image data that is masked with zero</param>
        /// <returns>An FFT with peaks removed</returns>
        public static PixelArray RemovePeriodicNoise(PixelArray imageData, PixelArray maskedImageData)
        {
            // Array must be in non-shifted mode, purely for ease of use

            // Get dimensions
            var width = imageData.width;
            var height = imageData.height;

            // Set up noiseReduced pixel array based on the input data
            var noiseReduced = new PixelArray(imageData.complexPixels);

            // Cast the data to greyscale int's
            var castPixels = maskedImageData.pixels.ConvertAll2D<Color, int>(new Converter<Color, int>(value => (int)value.ToGreyscale())).Flatten();

            // Get the max and the average data
            var pixelMaximum = castPixels.Max();
            var pixelMean = castPixels.Average();

            // Calculate the threshold
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

            // Push the new complex data into the Color array
            noiseReduced.PropagateComplex(true);

            // Return the data
            return noiseReduced;
        }

        /// <summary>
        /// Set data at a specific place based on inputs
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="inputArray">Input array to notch</param>
        /// <param name="x">X point to notch at</param>
        /// <param name="y">Y point to notch at</param>
        /// <param name="radius">Radius of the notch</param>
        /// <param name="notchedValue">The value to change the data to</param>
        /// <returns>A notched version of the input array</returns>
        public static T[,] NotchFilter<T>(T[,] inputArray, int x, int y, int radius, T notchedValue)
        {
            // Copy the input array
            var notchedData = inputArray;

            // If size is 0 just return the array
            if (radius == 0)
            {
                return notchedData;
            }
            else if (radius == 1)
            {
                // Notch the single value
                notchedData[x, y] = notchedValue;
            }
            else
            {
                // Calculate the max and min X,Y values, ensuring not to go out of bounds
                var xMin = (x - radius) < 0 ? 0 : (x - radius);
                var xMax = (x + radius) >= notchedData.GetLength(0) ? notchedData.GetLength(0) : (x + radius + 1);

                var yMin = (y - radius) < 0 ? 0 : (y - radius);
                var yMax = (y + radius) >= notchedData.GetLength(1) ? notchedData.GetLength(1) : (y + radius + 1);

                for (var i = xMin; i < xMax; i++)
                {
                    for (var j = yMin; j < yMax; j++)
                    {
                        // Notch the data with the specific value
                        notchedData[i, j] = notchedValue;
                    }
                }
            }

            return notchedData;
        }

        /// <summary>
        /// Runs a high pass filter of the data at a specific radius
        /// </summary>
        /// <param name="imageData">Data to filter</param>
        /// <param name="radius">Radius of the high pass filter</param>
        /// <returns>High passed version of the data</returns>
        public static PixelArray HighPassFourierFilter(PixelArray imageData, int radius)
        {
            // Get dimensions
            var width = imageData.width;
            var height = imageData.height;

            // Make a new pixel array based on current data
            var highPassedData = new PixelArray(imageData.complexPixels);

            // Call the other method to complete filter
            highPassedData.complexPixels = Filters.HighPassFourierFilter(highPassedData.complexPixels, radius, Complex.Zero);

            // Push the complex data into the color array
            highPassedData.PropagateComplex(true);

            return highPassedData;
        }

        /// <summary>
        /// Run a high pass filter
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="inputArray">Input array to high pass</param>
        /// <param name="radius">Radius of the high pass filter</param>
        /// <param name="insideValue">The value to set inside the filter</param>
        /// <returns></returns>
        public static T[,] HighPassFourierFilter<T>(T[,] inputArray, int radius, T insideValue)
        {
            // Copy the input array
            var highPassedData = inputArray;

            // If radius is 0 just return the original
            if (radius == 0)
            {
                return highPassedData;
            }
            else
            {
                // High pass the data based on the four corners of the FFT (Non shifted FFT)
                highPassedData = Filters.HighPassFourierFilter(highPassedData, 0, 0, radius, insideValue);
                highPassedData = Filters.HighPassFourierFilter(highPassedData, highPassedData.GetLength(0)-1, 0, radius, insideValue);
                highPassedData = Filters.HighPassFourierFilter(highPassedData, 0, highPassedData.GetLength(1)-1, radius, insideValue);
                highPassedData = Filters.HighPassFourierFilter(highPassedData, highPassedData.GetLength(0)-1, highPassedData.GetLength(1)-1, radius, insideValue);
            }

            // Return the data
            return highPassedData;
        }

        /// <summary>
        /// Runs a high pass fourier filter
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="inputArray">Input data</param>
        /// <param name="x">X point to run at</param>
        /// <param name="y">Y point to run at</param>
        /// <param name="radius">Radius of the filter</param>
        /// <param name="insideValue">Data to set inside the filter</param>
        /// <returns></returns>
        public static T[,] HighPassFourierFilter<T>(T[,] inputArray, int x, int y, int radius, T insideValue)
        {
            // Copy the input array
            var highPassedData = inputArray;

            // If radius 0 just return the original
            if (radius == 0)
            {
                return highPassedData;
            }
            else
            {
                // Set up X,Y maximum minimum values, ensuring not to go out of bounds
                var xMin = (x - radius) < 0 ? 0 : (x - radius);
                var xMax = (x + radius) >= highPassedData.GetLength(0) ? highPassedData.GetLength(0) : (x + radius + 1);

                var yMin = (y - radius) < 0 ? 0 : (y - radius);
                var yMax = (y + radius) >= highPassedData.GetLength(1) ? highPassedData.GetLength(1) : (y + radius + 1);

                // Run over this XY square
                for (var i = xMin; i < xMax; i++)
                {
                    for (var j = yMin; j < yMax; j++)
                    {
                        // If the data is in the circle radius of the filter, set the value accordingly
                        if (Filters.IsWithinRadius(i, j, x, y, radius))
                        {
                            highPassedData[i, j] = insideValue;
                        }
                    }
                }
            }

            // Return the data
            return highPassedData;
        }

        /// <summary>
        /// Runs a low pass filter
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="inputArray">Input data</param>
        /// <param name="x">X point to run at</param>
        /// <param name="y">Y point to run at</param>
        /// <param name="radius">Radius of the filter</param>
        /// <param name="outsideValue">Data to set outside the filter</param>
        /// <returns>Low passed data</returns>
        public static T[,] LowPassFourierFilter<T>(T[,] inputArray, int x, int y, int radius, T outsideValue)
        {
            var lowPassedData = inputArray;

            // If radius 0 just return the data
            if (radius == 0)
            {
                return lowPassedData;
            }
            else
            {
                // Set up XY max min ensuring not to go out of bounds
                var xMin = (x - radius) < 0 ? 0 : (x - radius);
                var xMax = (x + radius) >= lowPassedData.GetLength(0) ? lowPassedData.GetLength(0) : (x + radius + 1);

                var yMin = (y - radius) < 0 ? 0 : (y - radius);
                var yMax = (y + radius) >= lowPassedData.GetLength(1) ? lowPassedData.GetLength(1) : (y + radius + 1);

                // Loop over the XY square
                for (var i = xMin; i < xMax; i++)
                {
                    for (var j = yMin; j < yMax; j++)
                    {
                        // Check if the data is outside our specified circle, if it is set the outsideValue
                        if (!Filters.IsWithinRadius(i, j, x, y, radius))
                        {
                            lowPassedData[i, j] = outsideValue;
                        }
                    }
                }
            }

            // Return the data
            return lowPassedData;
        }

        /// <summary>
        /// Calculate if a point is within the radius specified of another point
        /// </summary>
        /// <param name="centerX">Center X point</param>
        /// <param name="centerY">Center Y point</param>
        /// <param name="x">X Point comparing to</param>
        /// <param name="y">Y point comparing to</param>
        /// <param name="radius">Radius to compare</param>
        /// <returns>Boolean true if within the radius</returns>
        private static bool IsWithinRadius(int centerX, int centerY, int x, int y, int radius)
        {
            // Calculate the distance between the points, if its less or equal to the radius return true
            return Math.Sqrt(Math.Abs(centerX - x) + Math.Abs(centerY - y)) <= radius;
        }
        
        /// <summary>
        /// Run a median filter
        /// </summary>
        /// <param name="imageData">Input data</param>
        /// <param name="radius">Radius of the median filter</param>
        /// <returns>Median filtered data</returns>
        public static PixelArray MedianFilter(PixelArray imageData, int radius)
        {
            // Get the dimensions
            var width = imageData.width;
            var height = imageData.height;

            // Set up a new data
            var medianFilteredData = new PixelArray(imageData.pixels);

            // Filter the data
            medianFilteredData.pixels = Filters.MedianFilter(medianFilteredData.pixels, radius);

            // Push the color data into the complex space
            medianFilteredData.PropagateColor();

            // Return the data
            return medianFilteredData;
        }

        /// <summary>
        /// Runs a median filter on a colour 2D array
        /// </summary>
        /// <param name="inputArray">2D colour array</param>
        /// <param name="windowSize">Size of the window to run against</param>
        /// <returns>2D median filtered array</returns>
        public static Color[,] MedianFilter(Color[,] inputArray, int windowSize)
        {
            // Get dimensions and set up new data
            var width = inputArray.GetLength(0);
            var height = inputArray.GetLength(1);
            var medianData = inputArray;

            // If no window size, return original data
            if (windowSize == 0)
            {
                return medianData;
            }
            else
            {
                // Loop over the data
                for (var i = 0; i < width; i++)
                {
                    for (var j = 0; j < height; j++)
                    {
                        // Set up the window, ensuring not to go out of bounds
                        var xMin = (i - windowSize) < 0 ? 0 : (i - windowSize);
                        var xMax = (i + windowSize) >= medianData.GetLength(0) ? medianData.GetLength(0) : (i + windowSize + 1);

                        var yMin = (j - windowSize) < 0 ? 0 : (j - windowSize);
                        var yMax = (j + windowSize) >= medianData.GetLength(1) ? medianData.GetLength(1) : (j + windowSize + 1);

                        // Create a list for storing the contents of the window
                        var medianList = new List<Color>();
                        var medianValue = Color.Black;

                        // Add all the window data to the list
                        for (var wX = xMin; wX < xMax; wX++)
                        {
                            for (var wY = yMin; wY < yMax; wY++)
                            {
                                medianList.Add(medianData[wX, wY]);
                            }
                        }

                        // Order the data by size
                        medianList = medianList.OrderBy(value => value.ToGreyscale()).ToList();

                        // If there is an even size
                        if (medianList.Count % 2 == 0)
                        {
                            // Get the average of the two center data points and set the median value to this colour
                            var halfSize = (int)(medianList.Count / 2);
                            var medianInt =  (medianList[halfSize].ToGreyscale() + medianList[halfSize + 1].ToGreyscale()) / 2;
                            medianValue = Color.FromArgb(0, medianInt, medianInt, medianInt);
                        }
                        else
                        {
                            // Get the middle point and set the median value to this colour
                            var halfSize = (int)Math.Ceiling((double)(medianList.Count / 2));
                            medianValue = medianList[halfSize];
                        }

                        // Set the current data point to the median value
                        medianData[i, j] = medianValue;
                    }
                }
            }


            // Return the data
            return medianData;
        }
    }
}
