using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageCompression.ExtensionMethods;

namespace ImageCompression.ExtensionMethods
{
    public static class ByteExtensions
    {
        public static byte GetFirstBits(this byte b, int count)
        {
            switch (count)
            {
                case 0:
                    return (byte)(b & 0x00);
                case 1:
                    return (byte)(b & 0x80);
                case 2:
                    return (byte)(b & 0xC0);
                case 3:
                    return (byte)(b & 0xE0);
                case 4:
                    return (byte)(b & 0xF0);
                case 5:
                    return (byte)(b & 0xF8);
                case 6:
                    return (byte)(b & 0xFC);
                case 7:
                    return (byte)(b & 0xFE);
                case 8:
                    return (byte)(b & 0xFF);
                default:
                    throw new ArgumentOutOfRangeException("count");
            }
        }

        public static byte GetLastBits(this byte b, int count)
        {
            switch (count)
            {
                case 0:
                    return (byte)(b & 0x00);
                case 1:
                    return (byte)(b & 0x01);
                case 2:
                    return (byte)(b & 0x03);
                case 3:
                    return (byte)(b & 0x07);
                case 4:
                    return (byte)(b & 0x0F);
                case 5:
                    return (byte)(b & 0x1F);
                case 6:
                    return (byte)(b & 0x3F);
                case 7:
                    return (byte)(b & 0x7F);
                case 8:
                    return (byte)(b & 0xFF);
                default:
                    throw new ArgumentOutOfRangeException("count");
            }
        }

        public static byte GetOffsetBits(this byte b, int startPosition, int count)
        {
            /* Example for thoughts
             *
             * Byte: 01011011
             * Start: 2
             * Count: 4
             * 
             * Expected: 00000110
             *
             * How?
             * Byte >> (8 - count)
             * Result.LastBits(start + 1)
             */

            // Make a mask for this specific offset request
            var mask = "";

            for (var i = 1; i <= 8; i++)
            {
                if (i >= startPosition && i < startPosition + count)
                {
                    mask += "1";
                }
                else
                {
                    mask += "0";
                }
            }

            var bitMask = (byte)int.Parse(mask);

            return (byte)((byte)(b & bitMask) >> (8 - startPosition - count));
        }

        public static byte PushBits(this byte b, byte v, ref int byteBitsRemaining, int variableLength)
        {
            /* Example for thoughts
             *
             * Byte: 01101000
             * Push: 00000001   (01 as variable byte)
             * Bits remaining: 3
             * Length: 2
             *
             * Expected: 01101010
             *
             * How?
             * Push << (bytesBitsRemaining - variableLength)    -> Push << 1     -> 010
             * Bits remaining = bits remaining - variable length
             * Byte | Push
             */

            // Recalculate the bits remaining
            var newByteBitsRemaining = byteBitsRemaining - variableLength;

            // If the data is a byte and we have a full byte empty, push it on
            if (byteBitsRemaining == 8 && variableLength == 8)
            {
                byteBitsRemaining -= variableLength;

                return (byte)(b | v);
            }
            // Pad the data and push our data onto it
            else
            {
                byteBitsRemaining -= variableLength;

                return (byte)(b.Pad(variableLength) | v);
            }
        }

        public static byte Pad(this byte b, ref int byteBitsRemaining)
        {
            // Pad the byte so the data is shifted correctly for reading
            b = (byte)(b << byteBitsRemaining);

            byteBitsRemaining -= byteBitsRemaining;

            return b;
        }

        public static byte Pad(this byte b, int byteBitsRemaining)
        {
            // Pad the byte so the data is shifted correctly for reading
            return (byte)(b << byteBitsRemaining);
        }
    }
}
