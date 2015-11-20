using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ImageCompression
{
    public class VariableByte
    {
        private byte data { get; set; }

        public Bits bitLength { get; private set; }

        public static VariableByte Zero
        {
            get { return new VariableByte(0, Bits.One); }
        }

        public static VariableByte One
        {
            get { return new VariableByte(1, Bits.One); }
        }

        [Flags]
        public enum Bits
        {
            One = 0x01,
            Two = 0x03,
            Three = 0x07,
            Four = 0x0F,
            Five = 0x1F,
            Six = 0x3F,
            Seven = 0x7F,
            Eight = 0xFF
        }

        public VariableByte(byte data, Bits bits)
        {
            this.bitLength = bits;

            // If the data is bigger than the bits we want, downscale it to fit
            if ((data ^ (byte)this.bitLength) > (byte)this.bitLength)
            {
                this.data = this.Convert(data, Bits.Eight, bits);
            }
            else
            {
                this.data = data;
            }
        }

        private byte Convert(byte data, Bits originalBits, Bits newBits)
        {
            if (originalBits < newBits)
            {
                return (byte)((int)data << (Helpers.Int.BitLength((int)newBits) - Helpers.Int.BitLength((int)originalBits)));
            }
            else
            {
                return (byte)((int)data >> (Helpers.Int.BitLength((int)originalBits) - Helpers.Int.BitLength((int)newBits)));
            }
        }

        public static explicit operator VariableByte(int v)
        {
            return new VariableByte((byte)v, (Bits)Enum.Parse(typeof(Bits), Helpers.Int.BitLength(v).ToString()));
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
            return (data & (byte)bitLength).ToString();
        }

        public byte ToFullByte()
        {
            return (byte)(data & (byte)bitLength);
        }

        public override int GetHashCode()
        {
            return data & (byte)bitLength;
        }
    }
}
