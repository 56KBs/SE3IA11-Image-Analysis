﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ImageCompression.ExtensionMethods;

namespace ImageCompression.ColorModel
{
    public class RGB : Interfaces.IEncodable
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

        public VariableByte R { get; set; }

        public VariableByte G { get; set; }

        public VariableByte B { get; set; }

        public ColorDepth bits { get; private set; }

        public RGB(byte R, byte G, byte B, ColorDepth bitDepth)
        {
            this.R = this.MakeVariableByte(Channels.R, bitDepth, R);
            this.G = this.MakeVariableByte(Channels.G, bitDepth, G);
            this.B = this.MakeVariableByte(Channels.B, bitDepth, B);

            this.bits = bitDepth;
        }

        public RGB(Component R, Component G, Component B, ColorDepth bitDepth)
        {
            this.R = this.MakeVariableByte(Channels.R, bitDepth, R.ToFullByte());
            this.G = this.MakeVariableByte(Channels.G, bitDepth, G.ToFullByte());
            this.B = this.MakeVariableByte(Channels.B, bitDepth, B.ToFullByte());

            this.bits = bitDepth;
        }

        private VariableByte MakeVariableByte(Channels channel, ColorDepth bitDepth, byte value)
        {
            if (bitDepth == ColorDepth.Eight)
            {
                if (channel == Channels.B)
                {
                    return new VariableByte(value, VariableByte.Bits.Two);
                }
                else
                {
                    return new VariableByte(value, VariableByte.Bits.Three);
                }
            }
            else
            {
                // Get the component values as hex
                var componentBitsInt = (byte)(Math.Pow(2, (int)bitDepth / 3) - 1);

                var componentBits = (VariableByte.Bits)componentBitsInt;
                return new VariableByte(value, componentBits);
            }
        }

        public RGB ToDepth(ColorDepth bitDepth)
        {
            return new RGB(this.R.ToFullByte(), this.G.ToFullByte(), this.B.ToFullByte(), bitDepth);
        }

        public VariableByte SelectChannel(Channels channel)
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

        public VariableByte[] ToByteArray()
        {
            return new VariableByte[] { R, G, B };
        }

        public byte[] ToFullByteArray()
        {
            return new byte[] { R.ToFullByte(), G.ToFullByte(), B.ToFullByte() };
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