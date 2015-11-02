using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Numerics;

namespace ImageEnhancerLibrary
{
    public static class ExtensionMethods
    {
        public static byte ToGreyscale(this Color color)
        {
            return (byte) ((color.R + color.G + color.B) / 3);
        }

        public static bool NearEquals(this Complex first, Complex second)
        {          
            var accuracy = 0.0000001;

            return (Math.Abs(first.Real - second.Real) <= accuracy) & (Math.Abs(first.Imaginary - second.Imaginary) <= accuracy);
        }
    }
}
