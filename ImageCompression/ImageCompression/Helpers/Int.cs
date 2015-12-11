using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCompression.Helpers
{
    public static class Int
    {
        /// <summary>
        /// Calculate the bit length of the value
        /// </summary>
        /// <param name="number">Number to calculate the bit length</param>
        /// <returns>Bit length</returns>
        public static int BitLength(int number)
        {
            return (int)Math.Log(number, 2) + 1;
        }
    }
}
