using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using ImageCompression;

namespace ImageCompression.ColorModel
{
    public class Component
    {
        private byte data { get; set; }

        private VariableByte.Bits bits { get; set; }

        public Component(byte data)
        {
            this.data = data;
            this.bits = VariableByte.Bits.Eight;
        }

        public Component(byte data, VariableByte.Bits bits)
        {
            this.bits = bits;

            // If the data is bigger than the bits we want, downscale it to fit
            if ((data ^ (byte)this.bits) > (byte)this.bits)
            {
                this.data = this.Convert(data, VariableByte.Bits.Eight, bits);
            }
            else
            {
                this.data = data;
            }
        }

        private byte Convert(byte data, VariableByte.Bits originalBits, VariableByte.Bits newBits)
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
            return (data & (byte)bits).ToString();
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
