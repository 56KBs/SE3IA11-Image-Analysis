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
    }
}
