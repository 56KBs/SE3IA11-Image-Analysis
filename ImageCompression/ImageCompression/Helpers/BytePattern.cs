using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageCompression.ColorModel;

namespace ImageCompression.Helpers
{
    public static class BytePattern
    {

        public static byte[] GenerateArrayFromDepth(RGB.ColorDepth depth)
        {
            var pattern = new byte[3];

            switch (depth)
            {
                case RGB.ColorDepth.Eight:
                    pattern = new byte[] { 3, 3, 2 };
                    break;
                case RGB.ColorDepth.Fifteen:
                    pattern = new byte[] { 5, 5, 5 };
                    break;
                case RGB.ColorDepth.Eighteen:
                    pattern = new byte[] { 6, 6, 6 };
                    break;
                case RGB.ColorDepth.TwentyFour:
                    pattern = new byte[] { 8, 8, 8 };
                    break;
            }

            return pattern;
        }

        public static List<byte> GenerateFromDepth(RGB.ColorDepth depth)
        {
            return GenerateArrayFromDepth(depth).ToList();
        }
    }
}
