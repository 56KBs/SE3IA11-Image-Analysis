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
        public static int ComplexToMagnitude(Complex complexValue)
        {
            return (int)complexValue.Magnitude;
        }
    }
}
