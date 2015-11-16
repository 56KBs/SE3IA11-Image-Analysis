using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ImageCompression.Encoders;
using ImageCompression.ColorModel;

namespace UnitTestProject1
{
    [TestClass]
    public class LZ77UnitTest
    {
        [TestMethod]
        public void LZ77_Encoding()
        {
            var toEncodeList = new List<ImageCompression.ColorModel.RGB>();

            toEncodeList.Add(new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour));
            toEncodeList.Add(new RGB(12, 42, 125, RGB.ColorDepth.TwentyFour));
            toEncodeList.Add(new RGB(34, 68, 1, RGB.ColorDepth.TwentyFour));
            toEncodeList.Add(new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour));
            toEncodeList.Add(new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour));
            toEncodeList.Add(new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour));
            toEncodeList.Add(new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour));
            toEncodeList.Add(new RGB(255, 124, 61, RGB.ColorDepth.TwentyFour));

            var encoded = LZ77.Encode(toEncodeList.ToArray());
        }
    }
}
