using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace ImageEnhancerLibrary
{
    static class Converter
    {
        /// <summary>
        /// Converts a complex number into it's magnitude
        /// </summary>
        /// <param name="complexValue">Complex data</param>
        /// <returns>Magnitude of data</returns>
        public static int ComplexToMagnitude(Complex complexValue)
        {
            return (int)complexValue.Magnitude;
        }
    }
}
