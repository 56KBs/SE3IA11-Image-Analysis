using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageCompression.ExtensionMethods;
using System.Collections;

namespace ImageCompression.ExtensionMethods
{
    public static class ByteExtensions
    {
        /// <summary>
        /// Convert a byte from one byte length to another
        /// </summary>
        /// <param name="b">original byte</param>
        /// <param name="originalBits">original byte length</param>
        /// <param name="newBits">new byte length</param>
        /// <returns>Converted byte</returns>
        public static byte ConvertBitLength(this byte b, int originalBits, int newBits)
        {
            // If no changes required, return the original
            if (originalBits == newBits)
            {
                return b;
            }
            else
            {
                // If the new bits are longer, left shift the data be the required amount
                if (originalBits < newBits)
                {
                    return (byte)(b << (newBits - originalBits));
                }
                // If the new bits are shorter, right shift the data to first the required size
                else
                {
                    return (byte)(b >> (originalBits - newBits));
                }
            }
        }

        /// <summary>
        /// Returns the byte at the specified bits
        /// </summary>
        /// <param name="b">Original byte</param>
        /// <param name="count">Amount of end bytes to copy</param>
        /// <returns>Bytes at the end of the specified data</returns>
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

        /// <summary>
        /// Returns the bits between two points
        /// </summary>
        /// <param name="b">Original byte</param>
        /// <param name="startPosition">Starting position to copy from</param>
        /// <param name="count">Amount of data to copy</param>
        /// <returns>Returns the bits between two points</returns>
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
            var bitMask = (byte)(1 << (8 - startPosition));
            bitMask = (byte)(bitMask - (byte)(1 << (8 - startPosition - count)));

            return (byte)((b & bitMask) >> (8 - startPosition - count));
        }

        /// <summary>
        /// Get a single bit from the byte
        /// </summary>
        /// <param name="b">Original byte</param>
        /// <param name="startPosition">Position to copy from</param>
        /// <returns>A boolean value of the bits</returns>
        public static bool GetSingleBit(this byte b, int startPosition)
        {
            // Get the offset bits with a length of 1
            var byteVersion = b.GetOffsetBits(startPosition, 1);

            return Convert.ToBoolean(byteVersion);
        }

        /// <summary>
        /// Push bits onto a byte
        /// </summary>
        /// <param name="b">Original byte</param>
        /// <param name="v">Byte to push on</param>
        /// <param name="byteBitsRemaining">Length remaining for the byte that is empty</param>
        /// <param name="variableLength">Length of the v byte</param>
        /// <returns></returns>
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

        /// <summary>
        /// Push a full byte
        /// </summary>
        /// <param name="b">Original byte</param>
        /// <param name="v">Byte to push</param>
        /// <returns></returns>
        public static byte PushBit(this byte b, bool v)
        {
            return (byte)((byte)(b << 1) | Convert.ToByte(v));
        }

        /// <summary>
        /// Pads a byte with blank data
        /// </summary>
        /// <param name="b">Original byte</param>
        /// <param name="byteBitsRemaining">Bits remaining in the original byte</param>
        /// <returns>Padded data byte</returns>
        public static byte Pad(this byte b, ref int byteBitsRemaining)
        {
            // Pad the byte so the data is shifted correctly for reading
            b = (byte)(b << byteBitsRemaining);

            byteBitsRemaining -= byteBitsRemaining;

            return b;
        }

        /// <summary>
        /// Pads a byte with blank data
        /// </summary>
        /// <param name="b">Original byte</param>
        /// <param name="byteBitsRemaining">Bits remaining in the original byte</param>
        /// <returns>Padded data byte</returns>
        public static byte Pad(this byte b, int byteBitsRemaining)
        {
            // Pad the byte so the data is shifted correctly for reading
            return (byte)(b << byteBitsRemaining);
        }

        /// <summary>
        /// Check if a flag is set in the byte value
        /// </summary>
        /// <param name="b">Original byte containing the flag</param>
        /// <param name="flag">Flag value</param>
        /// <returns></returns>
        public static bool FlagIsSet(this byte b, byte flag)
        {
            return (b & flag) == flag;
        }

        /// <summary>
        /// Upsample a less than full byte into a full byte
        /// </summary>
        /// <param name="b">Less than full byte</param>
        /// <param name="originalBitLength">Length that the less than full byte is</param>
        /// <returns></returns>
        public static byte Upsample(this byte b, int originalBitLength)
        {
            // If it is a full byte, just return the full byte;
            if (originalBitLength == 8)
            {
                return b;
            }
            else
            {
                // Left shift the data to pad the 1's over
                return (byte)(b << (8 - originalBitLength));
            }
        }
    }
}
