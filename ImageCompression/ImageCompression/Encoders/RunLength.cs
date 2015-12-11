using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageCompression.ColorModel;

namespace ImageCompression.Encoders
{
    public static class RunLength
    {
        /// <summary>
        /// Encode via Run Length
        /// </summary>
        /// <typeparam name="T">Type of data</typeparam>
        /// <param name="rawData">Data to encode</param>
        /// <returns>List of RunLength data</returns>
        public static List<Interfaces.IEncodable> Encode<T>(List<T> rawData) where T : Interfaces.IEncodable
        {
            var encodedData = new List<Interfaces.IEncodable>();

            var iterator = 0;

            // Iterate over all the items
            while (iterator < rawData.Count)
            {
                var currentDataItem = rawData[iterator];
                var runLength = 0;

                // If the iterator is not at the end, attempt to find a run length
                if (iterator < rawData.Count - 1)
                {
                    // While there is a match, increment the match length
                    while (rawData[iterator].Equals(rawData[++iterator]))
                    {
                        runLength++;
                        
                        if (iterator == rawData.Count - 1)
                        {
                            iterator++;
                            break;
                        }
                    }
                }
                else
                {
                    iterator++;
                }

                // Add the data to the list
                encodedData.Add(new RunLengthStore<T>(currentDataItem, runLength));
            }

            return encodedData;
        }

        /// <summary>
        /// Encode as array
        /// </summary>
        /// <typeparam name="T">IEncodable</typeparam>
        /// <param name="rawData">Data array to encode</param>
        /// <returns>Array of RunLength data</returns>
        public static Interfaces.IEncodable[] Encode<T>(T[] rawData) where T : Interfaces.IEncodable
        {
            // Cast to list
            var rawDataAs1DList = rawData.Cast<T>().ToList();

            // Return data in array form
            return RunLength.Encode(rawDataAs1DList).ToArray();
        }

        /// <summary>
        /// Encode a multidimensional array
        /// </summary>
        /// <typeparam name="T">IEncodable</typeparam>
        /// <param name="rawData">Data array to encode</param>
        /// <returns>Array of RunLength data</returns>
        public static Interfaces.IEncodable[] Encode<T>(T[,] rawData) where T : Interfaces.IEncodable
        {
            // Cast to a list
            var rawDataAs1DList = rawData.Cast<T>().ToList();

            // Return data in array form
            return RunLength.Encode(rawDataAs1DList).ToArray();
        }
        
        /// <summary>
        /// Decode the runlength data
        /// </summary>
        /// <param name="encodedData"></param>
        /// <returns></returns>
        public static List<ColorModel.RGB> Decode(string encodedData)
        {
            // Split the string
            var encodedList = encodedData.Split(',').ToList();

            var rawData = new List<ColorModel.RGB>();

            var iterator = -1;

            // Iterate over the encoded list data
            while (++iterator < encodedList.Count)
            {
                // If has a run attached
                if (encodedList[iterator].Contains(';'))
                {
                    // Decode the data
                    var runLengthStore = RunLengthStore<ColorModel.RGB>.FromString(encodedList[iterator]);

                    for (var i = 0; i <= runLengthStore.length; i++)
                    {
                        rawData.Add(runLengthStore.data);
                    }
                }
                else
                {
                    rawData.Add(ColorModel.RGB.FromString(encodedList[iterator], RGB.ColorDepth.TwentyFour));
                }
            }

            return rawData;
        }
    }
}
