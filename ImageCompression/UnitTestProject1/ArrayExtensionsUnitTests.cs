using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using ImageCompression;
using ImageCompression.ColorModel;
using ImageCompression.ExtensionMethods;

namespace UnitTestProject1
{
    [TestClass]
    public class ArrayExtensionsUnitTests
    {
        RGB[,] NormalPixels = new Bitmap(@"D:\GitHub\SE3IA11-Image-Analysis\ImageCompression\TestImage\eng_LO_uncompressed.png").GetRGBPixelArray();

        [TestMethod]
        public void ConvertAll2D_Normal_Time()
        {
            var conversion = NormalPixels.ConvertAll2D(new Converter<RGB, RGB>(x => new RGB(x.R.ToFullByte(), x.G.ToFullByte(), x.B.ToFullByte(), RGB.ColorDepth.TwentyFour)));
        }
    }
}
