using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImageCompression;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class VariableBytePackerUnitTests
    {
        [TestMethod]
        public void BytePacker_Pack_InSingleByte()
        {
            var data = new List<VariableByte[]>();

            data.Add(
                new VariableByte[]
                {
                    new VariableByte(1, VariableByte.Bits.One),
                    new VariableByte(12, VariableByte.Bits.Four)
                }
            );

            var packedData = ImageCompression.Helpers.BytePacker.Pack(data);

            var result = new byte[] { 0x1C };

            CollectionAssert.AreEqual(result, packedData);
        }

        [TestMethod]
        public void BytePacker_Pack_TwoByteOverflow()
        {
            var data = new List<VariableByte[]>();

            data.Add(
                new VariableByte[]
                {
                    new VariableByte(5, VariableByte.Bits.Three),
                    new VariableByte(45, VariableByte.Bits.Six)
                }
            );

            var packedData = ImageCompression.Helpers.BytePacker.Pack(data);

            var result = new byte[] { 0xB6, 0x01 };

            CollectionAssert.AreEqual(result, packedData);
        }

        [TestMethod]
        public void BytePacker_Pack_InFullBytes()
        {
            var data = new List<VariableByte[]>();

            data.Add(
                new VariableByte[]
                {
                    new VariableByte(255, VariableByte.Bits.Eight),
                    new VariableByte(45, VariableByte.Bits.Eight),
                    new VariableByte(64, VariableByte.Bits.Eight)
                }
            );

            var packedData = ImageCompression.Helpers.BytePacker.Pack(data);

            var result = new byte[] { 255, 45, 64 };

            CollectionAssert.AreEqual(result, packedData);
        }

        [TestMethod]
        public void BytePacker_Pack_Test_1()
        {
            var lz77data = new List<ImageCompression.Encoders.LZ77Store>();

            lz77data.Add(
                new ImageCompression.Encoders.LZ77StoreShort<ImageCompression.ColorModel.RGB>(
                    new ImageCompression.ColorModel.RGB(
                        0,
                        0,
                        0,
                        ImageCompression.ColorModel.RGB.ColorDepth.TwentyFour
                    )
                )
            );

            lz77data.Add(
                new ImageCompression.Encoders.LZ77StoreLong<ImageCompression.ColorModel.RGB>(
                    1,
                    16
                )
            );

            var data = lz77data.ConvertAll(new Converter<ImageCompression.Interfaces.IEncodable, VariableByte[]>(x => x.ToByteArray()));

            var packedData = ImageCompression.Helpers.BytePacker.Pack(data);
        }
    }
}
