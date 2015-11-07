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
        public static byte ToGreyscale(this Color color)
        {
            return (byte) ((color.R + color.G + color.B) / 3);
        }

        public static bool NearEquals(this Complex first, Complex second)
        {          
            var accuracy = 0.0000001;

            return (Math.Abs(first.Real - second.Real) <= accuracy) & (Math.Abs(first.Imaginary - second.Imaginary) <= accuracy);
        }
        
        /// <summary>
        /// Generates a multidimensional color array from the pixels in a bitmap
        /// </summary>
        /// <param name="bitmap">A bitmap image</param>
        /// <returns>A multidimensional color array</returns>
        public static Color[,] GetPixelArray(this Bitmap bitmap)
        {
            var colorData = new Color[bitmap.Width, bitmap.Height];

            for (var i = 0; i < bitmap.Width; i++)
            {
                for (var j = 0; j < bitmap.Height; j++)
                {
                    colorData[i, j] = bitmap.GetPixel(i, j);
                }
            }

            return colorData;
        }

        public static Color ToColor(this Complex complex, int peakValue)
        {
            var colorValue = (int)((255 / Math.Log(1 + Math.Abs(peakValue))) * Math.Log(1 + Math.Abs(complex.Magnitude)));

            return Color.FromArgb(0, colorValue, colorValue, colorValue);
        }

        public static Color ToColor(this int magnitude)
        {
            return Color.FromArgb(0, magnitude, magnitude, magnitude);
        }

        public static Color ToColor(this int magnitude, int peakValue)
        {
            var colorValue = (int)((255 / Math.Log(1 + Math.Abs(peakValue))) * Math.Log(1 + Math.Abs(magnitude)));

            return Color.FromArgb(0, colorValue, colorValue, colorValue);
        }
    }

    public static class ArrayExtensions
    {
        public static TOutput[,] ConvertAll2D<TInput, TOutput>(this TInput[,] inputArray, Converter<TInput, TOutput> converter)
        {
            var width = inputArray.GetLength(0);
            var height = inputArray.GetLength(1);

            TOutput[,] convertedArray = new TOutput[width, height];

            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    convertedArray[i, j] = converter((TInput)inputArray.GetValue(i, j));
                }
            }

            return convertedArray;
        }

        public static T[,] SetColumn<T>(this T[,] inputArray, T[] columnArray, int column)
        {
            var width = inputArray.GetLength(0);
            var height = inputArray.GetLength(1);

            for (var j = 0; j < height; j++)
            {
                inputArray[column, j] = columnArray[j];
            }

            return inputArray;
        }

        public static T[,] SetRow<T>(this T[,] inputArray, T[] rowArray, int row)
        {
            var width = inputArray.GetLength(0);
            var height = inputArray.GetLength(1);

            for (var j = 0; j < height; j++)
            {
                inputArray[j, row] = rowArray[j];
            }

            return inputArray;
        }

        public static T[,] Resize<T>(this T[,] inputArray, int xSize, int ySize, T defaultValue)
        {
            var resizedArray = new T[xSize, ySize];
            var inputWidth = inputArray.GetLength(0);
            var inputHeight = inputArray.GetLength(1);

            for (var i = 0; i < xSize; i++)
            {
                for (var j = 0; j < ySize; j++)
                {
                    if (i >= inputWidth || j >= inputHeight)
                    {
                        resizedArray[i, j] = defaultValue;
                    }
                    else
                    {
                        resizedArray[i, j] = inputArray[i, j];
                    }
                }
            }

            return resizedArray;
        }

        public static T[,] Resize<T>(this T[,] inputArray, int size, T defaultValue)
        {
            var resizedArray = new T[size, size];
            var inputWidth = inputArray.GetLength(0);
            var inputHeight = inputArray.GetLength(1);
            
            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    if (i >= inputWidth || j >= inputHeight)
                    {
                        resizedArray[i, j] = defaultValue;
                    }
                    else
                    {
                        resizedArray[i, j] = inputArray[i, j];
                    }
                }
            }

            return resizedArray;
        }

        public static T[][] Split<T>(this T[] inputArray)
        {
            var arrayLength = inputArray.Length;
            var arrayHalfLength = arrayLength / 2;

            var splitArray = new T[2][];

            splitArray[0] = new T[arrayHalfLength];
            splitArray[1] = new T[arrayHalfLength];

            if (arrayLength % 2 != 0)
            {
                splitArray[0] = new T[arrayHalfLength + 1];
            }

            var firstCounter = 0;
            var secondCounter = 0;

            for (var i = 0; i < arrayLength; i++)
            {
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

            return splitArray;
        }

        public static T[] GetColumn<T>(this T[,] inputArray, int column)
        {
            var columnData = new T[inputArray.GetLength(1)];

            for (var y = 0; y < columnData.Length; y++)
            {
                columnData[y] = inputArray[column, y];
            }

            return columnData;
        }

        public static T[] GetRow<T>(this T[,] inputArray, int row)
        {
            var rowData = new T[inputArray.GetLength(0)];

            for (var x = 0; x < rowData.Length; x++)
            {
                rowData[x] = inputArray[x, row];
            }

            return rowData;
        }

        public static T[] Flatten<T>(this T[,] inputArray)
        {
            var inputLength = inputArray.Length;
            var flatArray = new T[inputLength];
            
            var indexCounter = 0;

            for (var i = 0; i < inputArray.GetLength(0); i++)
            {
                for (var j = 0; j < inputArray.GetLength(1); j++)
                {
                    flatArray[indexCounter] = inputArray[i, j];
                    indexCounter++;
                }
            }

            return flatArray;
        }
    }
}