using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCompression.ExtensionMethods
{
    public static class BitsExtensions
    {
        public static int ToInt(this ImageCompression.VariableByte.Bits bits)
        {
            switch (bits)
            {
                case VariableByte.Bits.One:
                    return 1;
                case VariableByte.Bits.Two:
                    return 2;
                case VariableByte.Bits.Three:
                    return 3;
                case VariableByte.Bits.Four:
                    return 4;
                case VariableByte.Bits.Five:
                    return 5;
                case VariableByte.Bits.Six:
                    return 6;
                case VariableByte.Bits.Seven:
                    return 7;
                case VariableByte.Bits.Eight:
                    return 8;
                default:
                    throw new ArgumentOutOfRangeException("bits");
            }
        }
    }
}
