using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImageCompression.ColorModel;

namespace UnitTestProject1
{
    [TestClass]
    public class RGBUnitTests
    {
        [TestMethod]
        public void RGB_Constructor_Default()
        {
            var RGBValue = new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour);
            Assert.AreEqual("255,255,255", RGBValue.ToString());

            RGBValue = new RGB(12, 42, 125, RGB.ColorDepth.TwentyFour);
            Assert.AreEqual("12,42,125", RGBValue.ToString());

            RGBValue = new RGB(34, 68, 1, RGB.ColorDepth.TwentyFour);
            Assert.AreEqual("34,68,1", RGBValue.ToString());
        }

        [TestMethod]
        public void RGB_Constructor_Converting()
        {
            var RGBValue = new RGB(255, 255, 255, RGB.ColorDepth.Eight);
            Assert.AreEqual("7,7,3", RGBValue.ToString());

            RGBValue = new RGB(255, 255, 255, RGB.ColorDepth.Eighteen);
            Assert.AreEqual("63,63,63", RGBValue.ToString());

            RGBValue = new RGB(124, 61, 255, RGB.ColorDepth.Eighteen);
            Assert.AreEqual("31,61,63", RGBValue.ToString());

            RGBValue = new RGB(124, 61, 255, RGB.ColorDepth.Fifteen);
            Assert.AreEqual("15,7,31", RGBValue.ToString());
        }

        [TestMethod]
        public void RGB_ToDepth()
        {
            var RGBValue = new RGB(255, 124, 61, RGB.ColorDepth.TwentyFour);
            var RGBValueConverted = RGBValue.ToDepth(RGB.ColorDepth.Eight);
            Assert.AreEqual("7,3,0", RGBValueConverted.ToString());

            RGBValueConverted = RGBValue.ToDepth(RGB.ColorDepth.Eighteen);
            Assert.AreEqual("63,31,61", RGBValueConverted.ToString());

            RGBValueConverted = RGBValue.ToDepth(RGB.ColorDepth.Fifteen);
            Assert.AreEqual("31,15,7", RGBValueConverted.ToString());
        }
    }
}
