using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCompression
{
    [Flags]
    public enum CompressionFlags
    {
        EightBit = 0x01,
        FifteenBit = 0x03,
        EighteenBit = 0x07,
        TwentyFourBit = 0x0F,
        RunLength = 0x10,
        LZ77 = 0x20
    }
}
