using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCompression.Helpers
{
    static class Int
    {
        public static int BinaryLog(int number)
        {
            return (int)Math.Log(number, 2);
        }
    }
}
