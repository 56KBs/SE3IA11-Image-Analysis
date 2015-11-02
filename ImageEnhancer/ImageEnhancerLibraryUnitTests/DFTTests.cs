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
            var tester = new DFT(new Bitmap(@"D:\GitHub\SE3IA11-Image-Enhancement\ImageEnhancer\TestImage\TestImage.bmp"));              
        }

        [TestMethod]
        public void TestFFT()
        {
            var tester = new FFT(new Bitmap(@"D:\GitHub\SE3IA11-Image-Enhancement\ImageEnhancer\TestImage\TestImage.bmp"));
        }
    }
}
