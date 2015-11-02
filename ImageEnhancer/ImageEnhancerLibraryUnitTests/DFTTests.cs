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

            var actualResult = tester.fourierArray;

            var expectedResult = new Complex[8, 8];

            expectedResult[0, 0] = new Complex(1320, 0);
            expectedResult[0, 1] = new Complex(-504.852813742386, -724.264068711929);
            expectedResult[0, 2] = new Complex(120, 120);
            expectedResult[0, 3] = new Complex(-335.147186257614, -124.264068711928);
            expectedResult[0, 4] = new Complex(120, 0);
            expectedResult[0, 5] = new Complex(-335.147186257614, 124.264068711929);
            expectedResult[0, 6] = new Complex(120, -120);
            expectedResult[0, 7] = new Complex(-504.852813742386, 724.264068711929);
            expectedResult[1, 0] = new Complex(-791.126983722081, -711.126983722081);
            expectedResult[1, 1] = new Complex(-90.7106781186548, 713.553390593274);
            expectedResult[1, 2] = new Complex(9.2370555648813E-14, -136.568542494924);
            expectedResult[1, 3] = new Complex(134.142135623731, 255.56349186104);
            expectedResult[1, 4] = new Complex(-68.2842712474619, -68.2842712474619);
            expectedResult[1, 5] = new Complex(267.279220613579, 105.857864376269);
            expectedResult[1, 6] = new Complex(-136.568542494924, -7.105427357601E-15);
            expectedResult[1, 7] = new Complex(685.269119345812, -158.994949366117);
            expectedResult[2, 0] = new Complex(79.9999999999999, 440);
            expectedResult[2, 1] = new Complex(221.42135623731, -216.568542494924);
            expectedResult[2, 2] = new Complex(-40, 40);
            expectedResult[2, 3] = new Complex(21.4213562373096, -120);
            expectedResult[2, 4] = new Complex(-7.105427357601E-15, 40);
            expectedResult[2, 5] = new Complex(-61.4213562373095, -103.431457505076);
            expectedResult[2, 6] = new Complex(40, 40);
            expectedResult[2, 7] = new Complex(-261.42135623731, -120);
            expectedResult[3, 0] = new Complex(-168.873016277919, 88.873016277919);
            expectedResult[3, 1] = new Complex(105.857864376269, 55.5634918610405);
            expectedResult[3, 2] = new Complex(-23.4314575050762, -7.105427357601E-15);
            expectedResult[3, 3] = new Complex(50.7106781186547, -6.44660940672623);
            expectedResult[3, 4] = new Complex(-11.7157287525381, 11.7157287525381);
            expectedResult[3, 5] = new Complex(34.7308806541882, -38.9949493661166);
            expectedResult[3, 6] = new Complex(-7.105427357601E-15, 23.4314575050762);
            expectedResult[3, 7] = new Complex(12.7207793864215, -134.142135623731);
            expectedResult[4, 0] = new Complex(440, 1.71444889535133E-13);
            expectedResult[4, 1] = new Complex(-168.284271247462, -241.42135623731);
            expectedResult[4, 2] = new Complex(40, 40);
            expectedResult[4, 3] = new Complex(-111.715728752538, -41.4213562373095);
            expectedResult[4, 4] = new Complex(40, 1.46952762458685E-14);
            expectedResult[4, 5] = new Complex(-111.715728752538, 41.4213562373095);
            expectedResult[4, 6] = new Complex(40, -40);
            expectedResult[4, 7] = new Complex(-168.284271247462, 241.421356237309);
            expectedResult[5, 0] = new Complex(-168.873016277919, -88.873016277919);
            expectedResult[5, 1] = new Complex(12.7207793864216, 134.142135623731);
            expectedResult[5, 2] = new Complex(-3.5527136788005E-14, -23.4314575050762);
            expectedResult[5, 3] = new Complex(34.7308806541882, 38.9949493661167);
            expectedResult[5, 4] = new Complex(-11.7157287525381, -11.7157287525381);
            expectedResult[5, 5] = new Complex(50.7106781186548, 6.44660940672618);
            expectedResult[5, 6] = new Complex(-23.4314575050762, 3.5527136788005E-14);
            expectedResult[5, 7] = new Complex(105.857864376269, -55.5634918610407);
            expectedResult[6, 0] = new Complex(80.0000000000002, -440);
            expectedResult[6, 1] = new Complex(-261.421356237309, 120);
            expectedResult[6, 2] = new Complex(40, -40);
            expectedResult[6, 3] = new Complex(-61.4213562373095, 103.431457505076);
            expectedResult[6, 4] = new Complex(2.1316282072803E-14, -40);
            expectedResult[6, 5] = new Complex(21.4213562373094, 120);
            expectedResult[6, 6] = new Complex(-40, -40);
            expectedResult[6, 7] = new Complex(221.421356237309, 216.568542494924);
            expectedResult[7, 0] = new Complex(-791.126983722081, 711.12698372208);
            expectedResult[7, 1] = new Complex(685.269119345812, 158.994949366117);
            expectedResult[7, 2] = new Complex(-136.568542494924, -3.5527136788005E-14);
            expectedResult[7, 3] = new Complex(267.279220613579, -105.857864376269);
            expectedResult[7, 4] = new Complex(-68.2842712474619, 68.2842712474619);
            expectedResult[7, 5] = new Complex(134.142135623731, -255.56349186104);
            expectedResult[7, 6] = new Complex(-3.5527136788005E-14, 136.568542494924);
            expectedResult[7, 7] = new Complex(-90.7106781186545, -713.553390593274);

            var difference = 0.0000001;

            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 8; j++)
                {
                    Assert.IsTrue(expectedResult[j, i].NearEquals(actualResult[j, i]));
                }
            }
        }

        [TestMethod]
        public void TestFFT()
        {
            var tester = new FFT(new Bitmap(@"D:\GitHub\SE3IA11-Image-Enhancement\ImageEnhancer\TestImage\TestImage.bmp"));

            var actualResult = tester.fourierArray;

            var expectedResult = new Complex[8,8];

            expectedResult[0, 0] = new Complex(1320, 0);
            expectedResult[0, 1] = new Complex(-504.852813742386, -724.264068711929);
            expectedResult[0, 2] = new Complex(120, 120);
            expectedResult[0, 3] = new Complex(-335.147186257614, -124.264068711928);
            expectedResult[0, 4] = new Complex(120, 0);
            expectedResult[0, 5] = new Complex(-335.147186257614, 124.264068711929);
            expectedResult[0, 6] = new Complex(120, -120);
            expectedResult[0, 7] = new Complex(-504.852813742386, 724.264068711929);
            expectedResult[1, 0] = new Complex(-791.126983722081, -711.126983722081);
            expectedResult[1, 1] = new Complex(-90.7106781186548, 713.553390593274);
            expectedResult[1, 2] = new Complex(9.2370555648813E-14, -136.568542494924);
            expectedResult[1, 3] = new Complex(134.142135623731, 255.56349186104);
            expectedResult[1, 4] = new Complex(-68.2842712474619, -68.2842712474619);
            expectedResult[1, 5] = new Complex(267.279220613579, 105.857864376269);
            expectedResult[1, 6] = new Complex(-136.568542494924, -7.105427357601E-15);
            expectedResult[1, 7] = new Complex(685.269119345812, -158.994949366117);
            expectedResult[2, 0] = new Complex(79.9999999999999, 440);
            expectedResult[2, 1] = new Complex(221.42135623731, -216.568542494924);
            expectedResult[2, 2] = new Complex(-40, 40);
            expectedResult[2, 3] = new Complex(21.4213562373096, -120);
            expectedResult[2, 4] = new Complex(-7.105427357601E-15, 40);
            expectedResult[2, 5] = new Complex(-61.4213562373095, -103.431457505076);
            expectedResult[2, 6] = new Complex(40, 40);
            expectedResult[2, 7] = new Complex(-261.42135623731, -120);
            expectedResult[3, 0] = new Complex(-168.873016277919, 88.873016277919);
            expectedResult[3, 1] = new Complex(105.857864376269, 55.5634918610405);
            expectedResult[3, 2] = new Complex(-23.4314575050762, -7.105427357601E-15);
            expectedResult[3, 3] = new Complex(50.7106781186547, -6.44660940672623);
            expectedResult[3, 4] = new Complex(-11.7157287525381, 11.7157287525381);
            expectedResult[3, 5] = new Complex(34.7308806541882, -38.9949493661166);
            expectedResult[3, 6] = new Complex(-7.105427357601E-15, 23.4314575050762);
            expectedResult[3, 7] = new Complex(12.7207793864215, -134.142135623731);
            expectedResult[4, 0] = new Complex(440, 1.71444889535133E-13);
            expectedResult[4, 1] = new Complex(-168.284271247462, -241.42135623731);
            expectedResult[4, 2] = new Complex(40, 40);
            expectedResult[4, 3] = new Complex(-111.715728752538, -41.4213562373095);
            expectedResult[4, 4] = new Complex(40, 1.46952762458685E-14);
            expectedResult[4, 5] = new Complex(-111.715728752538, 41.4213562373095);
            expectedResult[4, 6] = new Complex(40, -40);
            expectedResult[4, 7] = new Complex(-168.284271247462, 241.421356237309);
            expectedResult[5, 0] = new Complex(-168.873016277919, -88.873016277919);
            expectedResult[5, 1] = new Complex(12.7207793864216, 134.142135623731);
            expectedResult[5, 2] = new Complex(-3.5527136788005E-14, -23.4314575050762);
            expectedResult[5, 3] = new Complex(34.7308806541882, 38.9949493661167);
            expectedResult[5, 4] = new Complex(-11.7157287525381, -11.7157287525381);
            expectedResult[5, 5] = new Complex(50.7106781186548, 6.44660940672618);
            expectedResult[5, 6] = new Complex(-23.4314575050762, 3.5527136788005E-14);
            expectedResult[5, 7] = new Complex(105.857864376269, -55.5634918610407);
            expectedResult[6, 0] = new Complex(80.0000000000002, -440);
            expectedResult[6, 1] = new Complex(-261.421356237309, 120);
            expectedResult[6, 2] = new Complex(40, -40);
            expectedResult[6, 3] = new Complex(-61.4213562373095, 103.431457505076);
            expectedResult[6, 4] = new Complex(2.1316282072803E-14, -40);
            expectedResult[6, 5] = new Complex(21.4213562373094, 120);
            expectedResult[6, 6] = new Complex(-40, -40);
            expectedResult[6, 7] = new Complex(221.421356237309, 216.568542494924);
            expectedResult[7, 0] = new Complex(-791.126983722081, 711.12698372208);
            expectedResult[7, 1] = new Complex(685.269119345812, 158.994949366117);
            expectedResult[7, 2] = new Complex(-136.568542494924, -3.5527136788005E-14);
            expectedResult[7, 3] = new Complex(267.279220613579, -105.857864376269);
            expectedResult[7, 4] = new Complex(-68.2842712474619, 68.2842712474619);
            expectedResult[7, 5] = new Complex(134.142135623731, -255.56349186104);
            expectedResult[7, 6] = new Complex(-3.5527136788005E-14, 136.568542494924);
            expectedResult[7, 7] = new Complex(-90.7106781186545, -713.553390593274);

            var difference = 0.0000001;

            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 8; j++)
                {
                    Assert.IsTrue(expectedResult[j, i].NearEquals(actualResult[j, i]));
                }
            }
        }
    }
}
