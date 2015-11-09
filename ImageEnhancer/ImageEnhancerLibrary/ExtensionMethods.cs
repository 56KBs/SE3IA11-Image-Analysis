using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Numerics;

namespace ImageEnhancerLibrary
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Return a greyscale byte version of the color
        /// </summary>
        /// <param name="color">The colour you wish to greyscale</param>
        /// <returns>Byte containing the greyscale value</returns>
        public static byte ToGreyscale(this Color color)
        {
            return (byte) ((color.R + color.G + color.B) / 3);
        }

        /// <summary>
        /// Compare complex numbers within a 'near' amount of each other (Avoids issues with accuracy)
        /// </summary>
        /// <param name="first">First complex number</param>
        /// <param name="second">Second complex number</param>
        /// <returns>Boolean based on equality</returns>
        public static bool NearEquals(this Complex first, Complex second)
        {          
            // Set an accuracy we'll accept
            var accuracy = 0.0000001;

            // Calculate whether both the real and imaginary are within the accuary required and return true/false
            return (Math.Abs(first.Real - second.Real) <= accuracy) & (Math.Abs(first.Imaginary - second.Imaginary) <= accuracy);
        }
        
        /// <summary>
        /// Generates a multidimensional color array from the pixels in a bitmap
        /// </summary>
        /// <param name="bitmap">A bitmap image</param>
        /// <returns>A multidimensional color array</returns>
        public static Color[,] GetPixelArray(this Bitmap bitmap)
        {
            // Make a new colour array
            var colorData = new Color[bitmap.Width, bitmap.Height];

            // Fill the array with the bitmap data
            for (var i = 0; i < bitmap.Width; i++)
            {
                for (var j = 0; j < bitmap.Height; j++)
                {
                    colorData[i, j] = bitmap.GetPixel(i, j);
                }
            }

            // Return the colour data
            return colorData;
        }

        /// <summary>
        /// Converts a magnitude into a colour value
        /// </summary>
        /// <param name="magnitude">Magnitude value</param>
        /// <returns>A colour base don the magnitude</returns>
        public static Color ToColor(this int magnitude)
        {
            // If the magnitude is over the limit of the colour spectrum, set it to 255
            if (magnitude > 255)
            {
                return Color.FromArgb(0, 255, 255, 255);
            }
            else
            {
                // Make and return a colour based on our magnitude value
                return Color.FromArgb(0, magnitude, magnitude, magnitude);
            }
        }

        /// <summary>
        /// Get a colour from the magnitude whilst also scaling the colour based on the peak value
        /// </summary>
        /// <param name="magnitude">Magnitude value to create colour</param>
        /// <param name="peakValue">The peak value of the data</param>
        /// <returns>A scaled colour based on the magnitude</returns>
        public static Color ToColor(this int magnitude, int peakValue)
        {
            // Scale the colour based on a log formula, scaling based on the peakValue to ensure even distribution + visibility
            var colorValue = (int)((255 / Math.Log(1 + Math.Abs(peakValue))) * Math.Log(1 + Math.Abs(magnitude)));

            // Return the colour
            return Color.FromArgb(0, colorValue, colorValue, colorValue);
        }
    }

    public static class ArrayExtensions
    {
        /// <summary>
        /// Convert a 2D array from one type to another
        /// </summary>
        /// <typeparam name="TInput">Input type</typeparam>
        /// <typeparam name="TOutput">Output type</typeparam>
        /// <param name="inputArray">2D input array</param>
        /// <param name="converter">Conversion method</param>
        /// <returns>The converted array</returns>
        public static TOutput[,] ConvertAll2D<TInput, TOutput>(this TInput[,] inputArray, Converter<TInput, TOutput> converter)
        {
            // Get dimensions
            var width = inputArray.GetLength(0);
            var height = inputArray.GetLength(1);

            // Make new 2D array
            TOutput[,] convertedArray = new TOutput[width, height];

            // Add data into array
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    // Convert the data using the conversion method supplied, ensuring to cast the value to specified type
                    convertedArray[i, j] = converter((TInput)inputArray.GetValue(i, j));
                }
            }

            // Return the array
            return convertedArray;
        }

        /// <summary>
        /// Set a column of a 2D array
        /// </summary>
        /// <typeparam name="T">Type of array</typeparam>
        /// <param name="inputArray">Data to insert into</param>
        /// <param name="columnArray">Data to insert</param>
        /// <param name="column">Which column to set</param>
        /// <returns>The array with the data inserted</returns>
        public static T[,] SetColumn<T>(this T[,] inputArray, T[] columnArray, int column)
        {
            // Get dimensions
            var width = inputArray.GetLength(0);
            var height = inputArray.GetLength(1);

            for (var j = 0; j < height; j++)
            {
                // Add the item from the columnArray into the inputArray based on the column provided
                inputArray[column, j] = columnArray[j];
            }

            // Return the data
            return inputArray;
        }

        /// <summary>
        /// Set a row of a 2D array
        /// </summary>
        /// <typeparam name="T">Type of array</typeparam>
        /// <param name="inputArray">Data to insert into</param>
        /// <param name="rowArray">Data to insert</param>
        /// <param name="row">Row to be set</param>
        /// <returns>The array with the data inserted</returns>
        public static T[,] SetRow<T>(this T[,] inputArray, T[] rowArray, int row)
        {
            // Get dimensions
            var width = inputArray.GetLength(0);
            var height = inputArray.GetLength(1);

            for (var j = 0; j < height; j++)
            {
                // Add the item from the rowArray into the inputArray based on the row provided
                inputArray[j, row] = rowArray[j];
            }

            return inputArray;
        }

        /// <summary>
        /// Resizes an array based on the sizes provided
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="inputArray">Array to resize</param>
        /// <param name="xSize">New x dimension</param>
        /// <param name="ySize">New y dimension</param>
        /// <param name="defaultValue">Default value to pad if larger</param>
        /// <returns></returns>
        public static T[,] Resize<T>(this T[,] inputArray, int xSize, int ySize, T defaultValue)
        {
            // Make a new resized array and get the input array dimensions
            var resizedArray = new T[xSize, ySize];
            var inputWidth = inputArray.GetLength(0);
            var inputHeight = inputArray.GetLength(1);
        
            // Loop over the new size
            for (var i = 0; i < xSize; i++)
            {
                for (var j = 0; j < ySize; j++)
                {
                    // If we're outside the bounds of the old array, pad with the defaultValue
                    if (i >= inputWidth || j >= inputHeight)
                    {
                        resizedArray[i, j] = defaultValue;
                    }
                    else
                    {
                        // Add the normal data into our resized array
                        resizedArray[i, j] = inputArray[i, j];
                    }
                }
            }

            // Return the resized array
            return resizedArray;
        }

        /// <summary>
        /// Resizes an array based on a single size
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="inputArray">Array to resize</param>
        /// <param name="size">Size of dimension in both dimensions</param>
        /// <param name="defaultValue">Default value to pad</param>
        /// <returns>The resized 2D array</returns>
        public static T[,] Resize<T>(this T[,] inputArray, int size, T defaultValue)
        {
            // Make a new resized array and get the input array dimensions
            var resizedArray = new T[size, size];
            var inputWidth = inputArray.GetLength(0);
            var inputHeight = inputArray.GetLength(1);

            // Loop over the new size
            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    // If we're outside the bounds of the old array, pad with the defaultValue
                    if (i >= inputWidth || j >= inputHeight)
                    {
                        resizedArray[i, j] = defaultValue;
                    }
                    else
                    {
                        // Add the normal data into our resized array
                        resizedArray[i, j] = inputArray[i, j];
                    }
                }
            }

            return resizedArray;
        }

        /// <summary>
        /// Split an array into two arrays
        /// </summary>
        /// <typeparam name="T">Type of array</typeparam>
        /// <param name="inputArray">Array to split</param>
        /// <returns>An array containing the split arrays</returns>
        public static T[][] Split<T>(this T[] inputArray)
        {
            // Get the length and half the length
            var arrayLength = inputArray.Length;
            var arrayHalfLength = arrayLength / 2;

            // Make a jagged array of size 2 (splitting in 2)
            var splitArray = new T[2][];

            // Set the sizes to half the length of the input array
            splitArray[0] = new T[arrayHalfLength];
            splitArray[1] = new T[arrayHalfLength];

            // If the input array is odd sized, add one to the size of the even array
            if (arrayLength % 2 != 0)
            {
                splitArray[0] = new T[arrayHalfLength + 1];
            }

            // Set up index counters
            var firstCounter = 0;
            var secondCounter = 0;

            // Loop over the input array
            for (var i = 0; i < arrayLength; i++)
            {
                // If even index, add to the even array and increase counter, else do it for the odd array
                if (i % 2 == 0)
                {
                    splitArray[0][firstCounter] = inputArray[i];
                    firstCounter++;
                }
                else
                {
                    splitArray[1][secondCounter] = inputArray[i];
                    secondCounter++;
                }
            }

            // Return the split array
            return splitArray;
        }

        /// <summary>
        /// Get a column from a multidimensional array
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="inputArray">2D array to select from</param>
        /// <param name="column">Column to select</param>
        /// <returns>Column of data</returns>
        public static T[] GetColumn<T>(this T[,] inputArray, int column)
        {
            // Set up the column
            var columnData = new T[inputArray.GetLength(1)];

            // Loop over the column specified and add to the columnData array
            for (var y = 0; y < columnData.Length; y++)
            {
                columnData[y] = inputArray[column, y];
            }

            // Return the data
            return columnData;
        }

        /// <summary>
        /// Get a row from a multidimensional array
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="inputArray">2D array to select from</param>
        /// <param name="row">Row to select</param>
        /// <returns>Row of data</returns>
        public static T[] GetRow<T>(this T[,] inputArray, int row)
        {
            // Set up the row data
            var rowData = new T[inputArray.GetLength(0)];

            // Loop over the row specified and add to the rowData array
            for (var x = 0; x < rowData.Length; x++)
            {
                rowData[x] = inputArray[x, row];
            }

            // Return the data
            return rowData;
        }

        /// <summary>
        /// Flattens a 2D array into a 1D array
        /// </summary>
        /// <typeparam name="T">Type of array</typeparam>
        /// <param name="inputArray">Array to flatten</param>
        /// <returns>1D form of the 2D array</returns>
        public static T[] Flatten<T>(this T[,] inputArray)
        {
            // Get the length and set up an array based on it
            var inputLength = inputArray.Length;
            var flatArray = new T[inputLength];
            
            // Keep an index counter for the 1D array
            var indexCounter = 0;

            // Loop over the 2D array
            for (var i = 0; i < inputArray.GetLength(0); i++)
            {
                for (var j = 0; j < inputArray.GetLength(1); j++)
                {
                    // Add each item to the flat array, increase index counter
                    flatArray[indexCounter] = inputArray[i, j];
                    indexCounter++;
                }
            }

            // Return data
            return flatArray;
        }
    }
}