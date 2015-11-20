using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImageCompression.Helpers;

namespace UnitTestProject1
{
    [TestClass]
    public class HelpersIntUnitTests
    {
        [TestMethod]
        public void Int_BitLength()
        {
            Assert.AreEqual(2, Int.BitLength(2));
        }
    }
}
