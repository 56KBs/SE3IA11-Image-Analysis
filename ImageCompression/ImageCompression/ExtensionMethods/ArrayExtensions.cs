using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCompression.ExtensionMethods
{
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
            Parallel.For(0, width, i =>
            {
                for (var j = 0; j < height; j++)
                {
                    // Convert the data using the conversion method supplied, ensuring to cast the value to specified type
                    convertedArray[i, j] = converter(inputArray[i, j]);
                }
            });

            // Return the array
            return convertedArray;
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
        
        /// <summary>
        /// Merge an array into a single item value
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="first">Single T data item</param>
        /// <param name="second">Array of T to append</param>
        /// <returns>Array of type T</returns>
        public static T[] Merge<T>(this T first, T[] second)
        {
            // Copy the array into an array with a single item longer in length
            T[] mergedArray = new T[1 + second.Length];

            mergedArray[0] = first;

            Array.Copy(second, 0, mergedArray, 1, second.Length);

            return mergedArray;
        }
    }
}
