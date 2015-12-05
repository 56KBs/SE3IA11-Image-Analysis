using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using ImageCompression;
using ImageCompression.Helpers;

namespace ImageCompression.ColorModel
{
    public class Component
    {
        private byte data { get; set; }

        private int bits { get; set; }

        public Component(byte data)
        {
            this.data = data;
            this.bits = 8;
        }

        public Component(byte data, int bits)
        {
            this.bits = bits;

            // If the data is bigger than the bits we want, downscale it to fit
            if (Helpers.Int.BitLength((int)data) > this.bits)
            {
                this.data = this.Convert(data, 8, bits);
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

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Component component = obj as Component;
            if ((System.Object)component == null)
            {
                return false;
            }

            return this.ToString() == component.ToString();
        }

        public override string ToString()
        {
            return (data & Helpers.Int.AsBitMask(bits)).ToString();
        }

        public VariableByte ToByte()
        {
            return new VariableByte(data, bits);
        }

        public byte ToFullByte()
        {
            return this.data;
        }

        public override int GetHashCode()
        {
            return data & (byte)bits;
        }

        public static explicit operator Complex(Component v)
        {
            return new Complex(double.Parse(v.ToString()), 0);
        }
    }
}
