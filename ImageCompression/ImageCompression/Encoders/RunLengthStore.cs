using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageCompression.ExtensionMethods;

namespace ImageCompression.Encoders
{
    public class RunLengthStore<T> : Interfaces.IEncodable
        where T : Interfaces.IEncodable
    {
        /// <summary>
        /// Data to store
        /// </summary>
        public T data { get; private set; }

        /// <summary>
        /// Length of the run
        /// </summary>
        public int length { get; private set; }

        /// <summary>
        /// Pattern that represents this data
        /// </summary>
        public byte[] bytePattern { get; }

        public RunLengthStore(T data, int length)
        {
            // Set up variables
            this.data = data;
            this.length = length;
            this.bytePattern = ((byte)8).Merge(this.data.bytePattern);
        }

        public override string ToString()
        {
            if (length > 0)
            {
                return data.ToString() + ";" + length.ToString();
            }
            else
            {
                return data.ToString();
            }
        }

        /// <summary>
        /// Get the object as a byte array
        /// </summary>
        /// <returns></returns>
        public byte[] ToByteArray()
        {
            var dataBytes = data.ToByteArray();

            var returnByteArray = new byte[dataBytes.Length + 1];

            // Copy array into another
            dataBytes.CopyTo(returnByteArray, 0);
            returnByteArray[dataBytes.Length] = (byte)this.length;

            return returnByteArray;
        }

        /// <summary>
        /// Get an RGB object from a string
        /// </summary>
        /// <param name="data">Stringified RGB data</param>
        /// <returns>RGB object</returns>
        public static RunLengthStore<ColorModel.RGB> FromString(string data)
        {
            var splitData = data.Split(';');

            return new RunLengthStore<ColorModel.RGB>(ColorModel.RGB.FromString(splitData[0], ColorModel.RGB.ColorDepth.TwentyFour), int.Parse(splitData[1]));
        }
    }
}
