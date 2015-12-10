using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImageCompression.ExtensionMethods;

namespace UnitTestProject1
{
    [TestClass]
    public class FlagUnitTests
    {
        [TestMethod]
        public void FlagIsSetTest()
        {
            byte flag = (byte)ImageCompression.CompressionFlags.EightBit;

            Assert.IsTrue(flag.FlagIsSet((byte)ImageCompression.CompressionFlags.EightBit));

            flag = (byte)ImageCompression.CompressionFlags.EighteenBit;

            Assert.IsFalse(flag.FlagIsSet((byte)ImageCompression.CompressionFlags.EightBit));
        }
    }
}
