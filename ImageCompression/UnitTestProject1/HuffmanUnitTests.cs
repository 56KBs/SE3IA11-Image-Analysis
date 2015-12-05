using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImageCompression;
using ImageCompression.ColorModel;
using ImageCompression.Encoders;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class HuffmanUnitTests
    {
        [TestMethod]
        public void Huffman_Encoding_Case_1()
        {
            var toEncodeList = new List<RGB>();

            toEncodeList.Add(new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour));
            toEncodeList.Add(new RGB(12, 42, 125, RGB.ColorDepth.TwentyFour));
            toEncodeList.Add(new RGB(34, 68, 1, RGB.ColorDepth.TwentyFour));
            toEncodeList.Add(new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour));
            toEncodeList.Add(new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour));
            toEncodeList.Add(new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour));
            toEncodeList.Add(new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour));
            toEncodeList.Add(new RGB(255, 124, 61, RGB.ColorDepth.TwentyFour));

            var encoded = Huffman<RGB>.Encode(toEncodeList);
        }

        [TestMethod]
        public void Huffman_Encoding_Case_2()
        {
            var toEncodeList = new List<RGB>();

            toEncodeList.Add(new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour));
            toEncodeList.Add(new RGB(12, 42, 125, RGB.ColorDepth.TwentyFour));
            toEncodeList.Add(new RGB(34, 68, 1, RGB.ColorDepth.TwentyFour));
            toEncodeList.Add(new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour));
            toEncodeList.Add(new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour));
            toEncodeList.Add(new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour));
            toEncodeList.Add(new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour));
            toEncodeList.Add(new RGB(34, 68, 1, RGB.ColorDepth.TwentyFour));

            var encoded = Huffman<RGB>.Encode(toEncodeList);
        }
    }
}
