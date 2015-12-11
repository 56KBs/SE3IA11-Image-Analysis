using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ImageCompression.ExtensionMethods;
using System.Collections;
using System.Diagnostics.Contracts;

namespace ImageCompression
{
    public class BitReader : BinaryReader
    {
        /// <summary>
        /// Store the current bit index of the byte
        /// </summary>
        private byte currentBitIndex { get; set; }

        public BitReader(Stream stream) : base(stream)
        {
            this.currentBitIndex = 0;
        }

        /// <summary>
        /// Read a boolean value from the byte
        /// </summary>
        /// <returns>Boolean value</returns>
        public override bool ReadBoolean()
        {
            // Read the bytes
            var readByte = base.ReadByte();
            var returnBoolean = false;

            // If zero, return false
            if (readByte == 0)
            {
                returnBoolean = false;
            }
            else
            {
                // Get the single bit at a given index
                returnBoolean = readByte.GetSingleBit(this.currentBitIndex);
            }

            // If the current bit index is at the end of byte, reset it
            if (this.currentBitIndex == 7)
            {
                this.currentBitIndex = 0;
            }
            else
            {
                // Move the bit index on and move the position back
                this.currentBitIndex++;
                base.BaseStream.Position--;
            }

            return returnBoolean;
        }

        /// <summary>
        /// Read a small amount of data from the byte
        /// </summary>
        /// <param name="bitLength"></param>
        /// <returns></returns>
        public byte ReadSmallBits(int bitLength)
        {
            if (bitLength > 8)
            {
                throw new ArgumentOutOfRangeException("bitLength");
            }

            var returnBit = (byte)0;

            // Read boolean values and push the bits onto our return bit
            for (var i = 0; i < bitLength; i++)
            {
                var bitValue = this.ReadBoolean();
                returnBit = returnBit.PushBit(bitValue);
            }

            return returnBit;
        }

        /// <summary>
        /// Read a full byte
        /// </summary>
        /// <returns>Byte</returns>
        public override byte ReadByte()
        {
            return this.ReadSmallBits(8);
        }
    }
}
