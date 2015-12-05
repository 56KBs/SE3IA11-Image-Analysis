using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCompression.Helpers
{
    public static class Int
    {
        public static int BitLength(int number)
        {
            return (int)Math.Log(number, 2) + 1;
        }

        public static int AsBitMask(int length)
        {
            return (1 << length) - 1;
        }
    }
}
