using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCompression.ImageIO
{
    [Flags]
    public enum Flags
    {
        EightBit = 1,
        FifteenBit = 2,
        EighteenBit = 4,
        TwentyfourBit = 8,
        RunlengthEncoded = 16,
        HuffmanEncoded = 32
    }
}
