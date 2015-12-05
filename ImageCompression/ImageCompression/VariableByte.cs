using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ImageCompression.Helpers;

namespace ImageCompression
{
    public class VariableByte
    {
        private byte data { get; set; }

        public int bitLength { get; private set; }

        public static VariableByte Zero
        {
            get { return new VariableByte(0, 1); }
        }

        public static VariableByte One
        {
            get { return new VariableByte(1, 1); }
        }

        public VariableByte(byte data, int bits)
        {
            this.bitLength = bits;

            // If the data is bigger than the bits we want, downscale it to fit
            if (Helpers.Int.BitLength((int)data) > this.bitLength)
            {
                this.data = this.Convert(data, Helpers.Int.BitLength((int)data), bits);
            }
            else
            {
                this.data = data;
            }
        }

        public VariableByte(byte data, int fullBits, int bits)
        {
            this.bitLength = bits;

            // If the data is bigger than the bits we want, downscale it to fit
            if (fullBits > this.bitLength)
            {
                this.data = this.Convert(data, fullBits, bits);
            }
            else
            {
                this.data = data;
            }
        }

        private byte Convert(byte data, int originalBits, int newBits)
        {
            if (originalBits < newBits)
            {
                return (byte)(data << (newBits - originalBits));
            }
            else
            {
                return (byte)(data >> (originalBits - newBits));
            }
        }

        public static explicit operator VariableByte(int v)
        {
            return new VariableByte((byte)v, Helpers.Int.BitLength(v));
        }

        public static explicit operator Complex(VariableByte v)
        {
            return new Complex(double.Parse(v.ToString()), 0);
        }

        public static explicit operator byte(VariableByte v)
        {
            return v.ToFullByte();
        }

        public static explicit operator VariableByte(byte v)
        {
            return (VariableByte)(int)v;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            VariableByte custombits = obj as VariableByte;
            if ((System.Object)custombits == null)
            {
                return false;
            }

            return this.ToString() == custombits.ToString();
        }

        public override string ToString()
        {
            return (data & Helpers.Int.AsBitMask(bitLength)).ToString();
        }

        public byte ToFullByte()
        {
            return (byte)(data & Helpers.Int.AsBitMask(bitLength));
        }

        public override int GetHashCode()
        {
            return data & Helpers.Int.AsBitMask(bitLength);
        }
    }
}
