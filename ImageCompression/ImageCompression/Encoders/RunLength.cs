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
        public static List<Interfaces.IEncodable> Encode<T>(List<T> rawData) where T : Interfaces.IEncodable
        {
            var encodedData = new List<Interfaces.IEncodable>();

            var iterator = 0;

            while (iterator < rawData.Count)
            {
                var currentDataItem = rawData[iterator];
                var runLength = 0;

                if (iterator < rawData.Count - 1)
                {
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

                encodedData.Add(new RunLengthStore<T>(currentDataItem, runLength));
            }

            return encodedData;
        }

        public static Interfaces.IEncodable[] Encode<T>(T[] rawData) where T : Interfaces.IEncodable
        {
            var rawDataAs1DList = rawData.Cast<T>().ToList();

            return RunLength.Encode(rawDataAs1DList).ToArray();
        }

        public static Interfaces.IEncodable[] Encode<T>(T[,] rawData) where T : Interfaces.IEncodable
        {
            var rawDataAs1DList = rawData.Cast<T>().ToList();

            return RunLength.Encode(rawDataAs1DList).ToArray();
        }

        public static List<ColorModel.RGB> Decode(string encodedData)
        {
            var encodedList = encodedData.Split(',').ToList();

            var rawData = new List<ColorModel.RGB>();

            var iterator = -1;

            while (++iterator < encodedList.Count)
            {
                // If has a run attached
                if (encodedList[iterator].Contains(';'))
                {
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

        /*public static List<T> Decode<T>(string encodedData, Converter<string, T> converter) where T : ColorModel.RGB
        {
            return RunLength.Decode(encodedData).ConvertAll(converter);
        }*/
    }
}
