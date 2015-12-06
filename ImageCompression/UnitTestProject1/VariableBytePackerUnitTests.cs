using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImageCompression;
using System.Collections.Generic;
using ImageCompression.ColorModel;

namespace UnitTestProject1
{
    [TestClass]
    public class VariableBytePackerUnitTests
    {
        [TestMethod]
        public void BytePacker_Pack_RGB_1()
        {
            var data = new List<ImageCompression.Interfaces.IEncodable>();

            data.Add(new RGB(7, 3, 5, 3, 2, 2, RGB.ColorDepth.Eight));

            var packedData = ImageCompression.Helpers.BytePacker.Pack(data); // Expect: 11100 -> Padded to 11100000

            var result = new byte[] { 246 };

            CollectionAssert.AreEqual(result, packedData);
        }

        [TestMethod]
        public void BytePacker_Pack_LZ77_1()
        {
            var data = new List<ImageCompression.Interfaces.IEncodable>();

            data.Add(
                new ImageCompression.Encoders.LZ77StoreShort<ImageCompression.ColorModel.RGB>(
                    new ImageCompression.ColorModel.RGB(
                        0,
                        0,
                        0,
                        ImageCompression.ColorModel.RGB.ColorDepth.TwentyFour
                    )
                )
            );

            data.Add(
                new ImageCompression.Encoders.LZ77StoreLong<ImageCompression.ColorModel.RGB>(
                    1,
                    16
                )
            );
            
            var packedData = ImageCompression.Helpers.BytePacker.Pack(data);

            var result = new byte[] { 128, 0, 0, 0, 68, 0 };

            CollectionAssert.AreEqual(result, packedData);
        }
    }
}
