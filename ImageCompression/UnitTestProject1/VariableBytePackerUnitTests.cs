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
                    new VariableByte(1, VariableByte.Bits.One), // 1
                    new VariableByte(12, VariableByte.Bits.Four) // 1100
                }
            );

            var packedData = ImageCompression.Helpers.BytePacker.Pack(data); // Expect: 11100 -> Padded to 11100000

            var result = new byte[] { 0xE0 };

            CollectionAssert.AreEqual(result, packedData);
        }

        [TestMethod]
        public void BytePacker_Pack_TwoByteOverflow()
        {
            var data = new List<VariableByte[]>();

            data.Add(
                new VariableByte[]
                {
                    new VariableByte(5, VariableByte.Bits.Three), // 101
                    new VariableByte(45, VariableByte.Bits.Six) // 101101
                }
            );

            var packedData = ImageCompression.Helpers.BytePacker.Pack(data); // Expect 10110110 1 -> Padded to 10110110 10000000

            var result = new byte[] { 0xB6, 0x80 };

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
