using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace ImageCompression.ColorModel
{
    public class Component
    {
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

        private byte data { get; set; }

        private Bits bits { get; set; }

        public Component(byte data)
        {
            this.data = data;
            this.bits = Bits.Eight;
        }

        public Component(byte data, Bits bits)
        {
            this.bits = bits;

            // If the data is bigger than the bits we want, downscale it to fit
            if ((data ^ (byte)this.bits) > (byte)this.bits)
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
                return (byte)((int)data << (Helpers.Int.BinaryLog((int)newBits + 1) - Helpers.Int.BinaryLog((int)originalBits + 1)));
            }
            else
            {
                return (byte)((int)data >> (Helpers.Int.BinaryLog((int)originalBits + 1) - Helpers.Int.BinaryLog((int)newBits + 1)));
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

        public byte ToByte()
        {
            return (byte)(data & (byte)bits);
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
