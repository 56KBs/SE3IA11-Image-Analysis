using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCompression.Encoders
{
    public class RunLengthStore<T> : Interfaces.ILZ77able where T : ColorModel.RGB
    {
        public T data { get; private set; }

        public int length { get; private set; }

        public RunLengthStore(T data, int length)
        {
            this.data = data;
            this.length = length;
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

        public byte[] ToByte()
        {
            var dataBytes = data.ToByteArray();

            var returnByteArray = new byte[dataBytes.Length + 2];

            dataBytes.CopyTo(returnByteArray, 0);
            returnByteArray[dataBytes.Length] = 0x002C;
            returnByteArray[dataBytes.Length + 1] = (byte)this.length;

            return returnByteArray;
        }

        public static RunLengthStore<ColorModel.RGB> FromString(string data)
        {
            var splitData = data.Split(';');

            return new RunLengthStore<ColorModel.RGB>(ColorModel.RGB.FromString(splitData[0], ColorModel.RGB.ColorDepth.TwentyFour), int.Parse(splitData[1]));
        }
    }
}
