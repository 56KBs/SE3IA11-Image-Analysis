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
        EightBit = 1,
        FifteenBit = 2,
        EighteenBit = 4,
        TwentyFourBit = 16,
        RunLength = 32,
        LZ77 = 64,
        Huffman = 128
    }
}
