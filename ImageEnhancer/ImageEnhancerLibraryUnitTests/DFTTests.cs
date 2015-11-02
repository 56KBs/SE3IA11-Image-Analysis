using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImageEnhancerLibrary;
using System.Numerics;
using System.Collections.Generic;
using System.Drawing;

namespace ImageEnhancerLibraryUnitTests
{
    [TestClass]
    public class DFTTests
    {
        [TestMethod]
        public void TestDFT()
        {
            var tester = new DFT(new Bitmap(@"C:\Users\Matt\Pictures\TestImage.bmp"));

            tester.Run2DDFT();

            
        }
    }
}
