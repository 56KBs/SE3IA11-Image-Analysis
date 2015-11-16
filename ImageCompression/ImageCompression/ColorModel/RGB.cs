using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ImageCompression.ExtensionMethods;

namespace ImageCompression.ColorModel
{
    public class RGB
    {
        public enum ColorDepth : int
        {
            Eight = 8,
            Fifteen = 15,
            Eighteen = 18,
            TwentyFour = 24
        }

        public enum Channels : int
        {
            R,
            G,
            B
        };

        public Component R { get; set; }

        public Component G { get; set; }

        public Component B { get; set; }

        public ColorDepth bits { get; private set; }

        public RGB(byte R, byte G, byte B, ColorDepth bitDepth)
        {
            this.R = this.MakeComponent(Channels.R, bitDepth, R);
            this.G = this.MakeComponent(Channels.G, bitDepth, G);
            this.B = this.MakeComponent(Channels.B, bitDepth, B);

            this.bits = bitDepth;
        }

        public RGB(Component R, Component G, Component B, ColorDepth bitDepth)
        {
            this.R = this.MakeComponent(Channels.R, bitDepth, R.ToByte());
            this.G = this.MakeComponent(Channels.G, bitDepth, G.ToByte());
            this.B = this.MakeComponent(Channels.B, bitDepth, B.ToByte());

            this.bits = bitDepth;
        }

        private Component MakeComponent(Channels channel, ColorDepth bitDepth, byte value)
        {
            if (bitDepth == ColorDepth.Eight)
            {
                if (channel == Channels.B)
                {
                    return new Component(value, Component.Bits.Two);
                }
                else
                {
                    return new Component(value, Component.Bits.Three);
                }
            }
            else
            {
                // Get the component values as hex
                var componentBitsInt = (byte)(Math.Pow(2, (int)bitDepth / 3) - 1);

                var componentBits = (Component.Bits)componentBitsInt;
                return new Component(value, componentBits);
            }
        }

        public RGB ToDepth(ColorDepth bitDepth)
        {
            return new RGB(this.R, this.G, this.B, bitDepth);
        }

        public Component SelectChannel(Channels channel)
        {
            switch (channel)
            {
                case Channels.R:
                    return this.R;
                case Channels.G:
                    return this.G;
                case Channels.B:
                    return this.B;
                default:
                    throw new Exception("Channel does not exist");
            }
        }

        public static RGB FromRGB(byte R, byte G, byte B, ColorDepth bitDepth = ColorDepth.TwentyFour)
        {
            return new RGB(R, G, B, bitDepth);
        }

        public static RGB FromColor(System.Drawing.Color color, ColorDepth bitDepth = ColorDepth.TwentyFour)
        {
            return new RGB(color.R, color.G, color.B, bitDepth);
        }

        public static RGB FromString(string data, ColorDepth colorDepth)
        {
            var splitData = data.Split(',');

            return new RGB(Byte.Parse(splitData[0]), Byte.Parse(splitData[1]), Byte.Parse(splitData[2]), colorDepth);
        }

        public override String ToString()
        {
            return String.Concat(R, ",", G, ",", B);
        }

        public byte[] ToByteArray()
        {
            return new byte[] { R.ToByte(), G.ToByte(), G.ToByte() };
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            RGB rgb = obj as RGB;
            if ((System.Object)rgb == null)
            {
                return false;
            }

            return this.ToString() == rgb.ToString();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
