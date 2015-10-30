using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ImageEnhancerLibrary
{
    public static class ExtensionMethods
    {
        public static byte ToGreyscale(this Color color)
        {
            return (byte) ((color.R + color.G + color.B) / 3);
        }
    }
}
