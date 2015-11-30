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
        private byte currentBitIndex { get; set; }

        public BitReader(Stream stream) : base(stream)
        {
            this.currentBitIndex = 0;
        }

        public override bool ReadBoolean()
        {
            var readByte = base.ReadByte();
            var returnBoolean = false;

            if (readByte == 0)
            {
                returnBoolean = false;
            }
            else
            {
                returnBoolean = readByte.GetSingleBit(this.currentBitIndex);
            }

            if (this.currentBitIndex == 7)
            {
                this.currentBitIndex = 0;
            }
            else
            {
                this.currentBitIndex++;
                base.BaseStream.Position--;
            }

            return returnBoolean;
        }

        public byte ReadSmallBits(int bitLength)
        {
            if (bitLength > 8)
            {
                throw new ArgumentOutOfRangeException("bitLength");
            }

            var returnBit = (byte)0;

            for (var i = 0; i < bitLength; i++)
            {
                var bitValue = this.ReadBoolean();
                returnBit = returnBit.PushBit(bitValue);
            }

            return returnBit;
        }

        public byte[] ReadBits(int bitLength)
        {
            var returnBits = new byte[bitLength / 8];

            var currentIndex = 0;
            
            for (var i = 0; i < bitLength; i++)
            {
                returnBits[currentIndex] = returnBits[currentIndex].PushBit(this.ReadBoolean());

                // If at the end of a current bit, push the indexer on
                if (i % 7 == 0 && i != 0)
                {
                    currentIndex++;
                }
            }

            return returnBits;
        }

        public override byte ReadByte()
        {
            return this.ReadSmallBits(8);
        }
    }
}
