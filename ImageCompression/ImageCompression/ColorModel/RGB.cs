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
    public class RGB : Interfaces.IEncodable
    {
        /// <summary>
        /// The colour depths allowed for RGB colour space
        /// </summary>
        public enum ColorDepth : int
        {
            Eight = 8,
            Fifteen = 15,
            Eighteen = 18,
            TwentyFour = 24
        }

        /// <summary>
        /// Enum of the RGB colour channels
        /// </summary>
        public enum Channels : int
        {
            R,
            G,
            B
        };

        /// <summary>
        /// Byte value of red
        /// </summary>
        public byte R { get; set; }

        /// <summary>
        /// Byte value of green
        /// </summary>
        public byte G { get; set; }

        /// <summary>
        /// Byte value of blue
        /// </summary>
        public byte B { get; set; }

        /// <summary>
        /// Depth of the RGB pixel
        /// </summary>
        public ColorDepth bits { get; private set; }

        public byte[] bytePattern { get; }

        public RGB(byte R, byte G, byte B, ColorDepth bitDepth)
        {
            this.R = this.MakeByte(Channels.R, bitDepth, R);
            this.G = this.MakeByte(Channels.G, bitDepth, G);
            this.B = this.MakeByte(Channels.B, bitDepth, B);

            this.bits = bitDepth;
            this.bytePattern = Helpers.BytePattern.GenerateArrayFromDepth(bitDepth);
        }

        public RGB(byte R, int originalRLength, byte G, int originalGLength, byte B, int originalBLength, ColorDepth bitDepth)
        {
            this.R = this.MakeByte(Channels.R, bitDepth, R, originalRLength);
            this.G = this.MakeByte(Channels.G, bitDepth, G, originalGLength);
            this.B = this.MakeByte(Channels.B, bitDepth, B, originalBLength);

            this.bits = bitDepth;
            this.bytePattern = Helpers.BytePattern.GenerateArrayFromDepth(bitDepth);
        }

        private byte MakeByte(Channels channel, ColorDepth bitDepth, byte value, int originalLength)
        {
            if (bitDepth == ColorDepth.Eight)
            {
                if (channel == Channels.B)
                {
                    return value.ConvertBitLength(originalLength, 2);
                }
                else
                {
                    return value.ConvertBitLength(originalLength, 3);
                }
            }
            else
            {
                var componentBits = (int)bitDepth / 3;

                return value.ConvertBitLength(originalLength, componentBits);
            }
        }

        private byte MakeByte(Channels channel, ColorDepth bitDepth, byte value)
        {
            if (bitDepth == ColorDepth.Eight)
            {
                if (channel == Channels.B)
                {
                    return value.ConvertBitLength(8, 2);
                }
                else
                {
                    return value.ConvertBitLength(8, 3);
                }
            }
            else
            {
                var componentBits = (int)bitDepth / 3;

                return value.ConvertBitLength(8, componentBits);
            }
        }

        public RGB ToDepth(ColorDepth bitDepth)
        {
            if (this.bits == bitDepth)
            {
                return this;
            }
            else
            {
                if ((byte)this.bits < (byte)bitDepth)
                {
                    if (this.bits == ColorDepth.Eight)
                    {
                        return new RGB(this.R.Upsample(3), this.G.Upsample(3), this.B.Upsample(2), bitDepth);
                    }
                    else
                    {
                        var componentBits = (int)this.bits / 3;

                        return new RGB(this.R.Upsample(componentBits), this.G.Upsample(componentBits), this.B.Upsample(componentBits), bitDepth);
                    }
                }
                else
                {
                    return new RGB(this.R, this.G, this.B, bitDepth);
                }
            }
        }

        public Color ToColor()
        {
            return Color.FromArgb(0, this.R, this.G, this.B);
        }

        public byte SelectChannel(Channels channel)
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
            return this.R.ToString() + "," + this.G.ToString() + "," + this.B.ToString();
            //return String.Concat(R, ",", G, ",", B);
        }

        public byte[] ToByteArray()
        {
            return new byte[] { R, G, B };
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

            return (this.R == rgb.R) && (this.G == rgb.G) && (this.B == rgb.B);
        }

        public static bool operator ==(RGB lhs, RGB rhs)
        {
            if (Object.ReferenceEquals(lhs, null))
            {
                if (Object.ReferenceEquals(rhs, null))
                {
                    return true;
                }

                return false;
            }

            return lhs.Equals(rhs);
        }

        public static bool operator !=(RGB lhs, RGB rhs)
        {
            return !(lhs == rhs);
        }

        public override int GetHashCode()
        {
            return this.R ^ this.G ^ this.B;
        }
    }
}
