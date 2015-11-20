﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ImageCompression.Encoders;
using ImageCompression.ColorModel;
using ImageCompression.Interfaces;

namespace UnitTestProject1
{
    [TestClass]
    public class LZ77UnitTest
    {
        [TestMethod]
        public void LZ77_Encoding_Case_1()
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

            var encoded = LZ77.Encode(toEncodeList.ToArray(), 255, 255);

            var expected = new List<ImageCompression.Interfaces.IEncodable>();

            expected.Add(new LZ77StoreShort<RGB>(new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour)));
            expected.Add(new LZ77StoreShort<RGB>(new RGB(12, 42, 125, RGB.ColorDepth.TwentyFour)));
            expected.Add(new LZ77StoreShort<RGB>(new RGB(34, 68, 1, RGB.ColorDepth.TwentyFour)));
            expected.Add(new LZ77StoreLong<RGB>(3, 1));
            expected.Add(new LZ77StoreLong<RGB>(1, 3));
            expected.Add(new LZ77StoreShort<RGB>(new RGB(255, 124, 61, RGB.ColorDepth.TwentyFour)));

            var expectedArray = expected.ToArray();

            CollectionAssert.AreEqual(expectedArray, encoded);
        }

        [TestMethod]
        public void LZ77_Decoding_Case_1()
        {
            var toDecodeList = new List<LZ77Store>();

            toDecodeList.Add(new LZ77StoreShort<RGB>(new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour)));
            toDecodeList.Add(new LZ77StoreShort<RGB>(new RGB(12, 42, 125, RGB.ColorDepth.TwentyFour)));
            toDecodeList.Add(new LZ77StoreShort<RGB>(new RGB(34, 68, 1, RGB.ColorDepth.TwentyFour)));
            toDecodeList.Add(new LZ77StoreLong<RGB>(3, 1));
            toDecodeList.Add(new LZ77StoreLong<RGB>(1, 3));
            toDecodeList.Add(new LZ77StoreShort<RGB>(new RGB(255, 124, 61, RGB.ColorDepth.TwentyFour)));

            var decoded = LZ77.Decode<RGB>(toDecodeList.ToArray());

            var expectedList = new List<RGB>();

            expectedList.Add(new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour));
            expectedList.Add(new RGB(12, 42, 125, RGB.ColorDepth.TwentyFour));
            expectedList.Add(new RGB(34, 68, 1, RGB.ColorDepth.TwentyFour));
            expectedList.Add(new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour));
            expectedList.Add(new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour));
            expectedList.Add(new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour));
            expectedList.Add(new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour));
            expectedList.Add(new RGB(255, 124, 61, RGB.ColorDepth.TwentyFour));

            var expectedArray = expectedList.ToArray();

            CollectionAssert.AreEqual(expectedList, decoded);
        }

        [TestMethod]
        public void LZ77_Encoding_Case_2()
        {
            var toEncodeList = new List<RGB>();

            toEncodeList.Add(new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour));
            toEncodeList.Add(new RGB(12, 42, 125, RGB.ColorDepth.TwentyFour));
            toEncodeList.Add(new RGB(34, 68, 1, RGB.ColorDepth.TwentyFour));
            toEncodeList.Add(new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour));
            toEncodeList.Add(new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour));
            toEncodeList.Add(new RGB(255, 124, 61, RGB.ColorDepth.TwentyFour));
            toEncodeList.Add(new RGB(12, 45, 31, RGB.ColorDepth.TwentyFour));
            toEncodeList.Add(new RGB(95, 84, 168, RGB.ColorDepth.TwentyFour));

            var encoded = LZ77.Encode(toEncodeList.ToArray(), 255, 255);

            var expected = new List<ImageCompression.Interfaces.IEncodable>();

            expected.Add(new LZ77StoreShort<RGB>(new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour)));
            expected.Add(new LZ77StoreShort<RGB>(new RGB(12, 42, 125, RGB.ColorDepth.TwentyFour)));
            expected.Add(new LZ77StoreShort<RGB>(new RGB(34, 68, 1, RGB.ColorDepth.TwentyFour)));
            expected.Add(new LZ77StoreLong<RGB>(3, 1));
            expected.Add(new LZ77StoreLong<RGB>(1, 1));
            expected.Add(new LZ77StoreShort<RGB>(new RGB(255, 124, 61, RGB.ColorDepth.TwentyFour)));
            expected.Add(new LZ77StoreShort<RGB>(new RGB(12, 45, 31, RGB.ColorDepth.TwentyFour)));
            expected.Add(new LZ77StoreShort<RGB>(new RGB(95, 84, 168, RGB.ColorDepth.TwentyFour)));

            var expectedArray = expected.ToArray();

            CollectionAssert.AreEqual(expectedArray, encoded);
        }

        [TestMethod]
        public void LZ77_Decoding_Case_2()
        {
            var toDecodeList = new List<LZ77Store>();

            toDecodeList.Add(new LZ77StoreShort<RGB>(new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour)));
            toDecodeList.Add(new LZ77StoreShort<RGB>(new RGB(12, 42, 125, RGB.ColorDepth.TwentyFour)));
            toDecodeList.Add(new LZ77StoreShort<RGB>(new RGB(34, 68, 1, RGB.ColorDepth.TwentyFour)));
            toDecodeList.Add(new LZ77StoreLong<RGB>(3, 1));
            toDecodeList.Add(new LZ77StoreLong<RGB>(1, 1));
            toDecodeList.Add(new LZ77StoreShort<RGB>(new RGB(255, 124, 61, RGB.ColorDepth.TwentyFour)));
            toDecodeList.Add(new LZ77StoreShort<RGB>(new RGB(12, 45, 31, RGB.ColorDepth.TwentyFour)));
            toDecodeList.Add(new LZ77StoreShort<RGB>(new RGB(95, 84, 168, RGB.ColorDepth.TwentyFour)));

            var decoded = LZ77.Decode<RGB>(toDecodeList.ToArray());

            var expectedList = new List<RGB>();

            expectedList.Add(new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour));
            expectedList.Add(new RGB(12, 42, 125, RGB.ColorDepth.TwentyFour));
            expectedList.Add(new RGB(34, 68, 1, RGB.ColorDepth.TwentyFour));
            expectedList.Add(new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour));
            expectedList.Add(new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour));
            expectedList.Add(new RGB(255, 124, 61, RGB.ColorDepth.TwentyFour));
            expectedList.Add(new RGB(12, 45, 31, RGB.ColorDepth.TwentyFour));
            expectedList.Add(new RGB(95, 84, 168, RGB.ColorDepth.TwentyFour));

            var expectedArray = expectedList.ToArray();

            CollectionAssert.AreEqual(expectedList, decoded);
        }


        [TestMethod]
        public void LZ77_Encoding_Case_3()
        {
            var toEncodeList = new List<RGB>();

            toEncodeList.Add(new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour));
            toEncodeList.Add(new RGB(12, 42, 125, RGB.ColorDepth.TwentyFour));
            toEncodeList.Add(new RGB(34, 68, 1, RGB.ColorDepth.TwentyFour));
            toEncodeList.Add(new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour));
            toEncodeList.Add(new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour));
            toEncodeList.Add(new RGB(12, 42, 125, RGB.ColorDepth.TwentyFour));
            toEncodeList.Add(new RGB(34, 68, 1, RGB.ColorDepth.TwentyFour));
            toEncodeList.Add(new RGB(255, 124, 61, RGB.ColorDepth.TwentyFour));
            toEncodeList.Add(new RGB(12, 45, 31, RGB.ColorDepth.TwentyFour));
            toEncodeList.Add(new RGB(95, 84, 168, RGB.ColorDepth.TwentyFour));

            var encoded = LZ77.Encode(toEncodeList.ToArray(), 255, 255);

            var expected = new List<ImageCompression.Interfaces.IEncodable>();

            expected.Add(new LZ77StoreShort<RGB>(new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour)));
            expected.Add(new LZ77StoreShort<RGB>(new RGB(12, 42, 125, RGB.ColorDepth.TwentyFour)));
            expected.Add(new LZ77StoreShort<RGB>(new RGB(34, 68, 1, RGB.ColorDepth.TwentyFour)));
            expected.Add(new LZ77StoreLong<RGB>(3, 1));
            expected.Add(new LZ77StoreLong<RGB>(4, 3));
            expected.Add(new LZ77StoreShort<RGB>(new RGB(255, 124, 61, RGB.ColorDepth.TwentyFour)));
            expected.Add(new LZ77StoreShort<RGB>(new RGB(12, 45, 31, RGB.ColorDepth.TwentyFour)));
            expected.Add(new LZ77StoreShort<RGB>(new RGB(95, 84, 168, RGB.ColorDepth.TwentyFour)));

            var expectedArray = expected.ToArray();

            CollectionAssert.AreEqual(expectedArray, encoded);
        }

        [TestMethod]
        public void LZ77_Decoding_Case_3()
        {
            var toDecodeList = new List<LZ77Store>();

            toDecodeList.Add(new LZ77StoreShort<RGB>(new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour)));
            toDecodeList.Add(new LZ77StoreShort<RGB>(new RGB(12, 42, 125, RGB.ColorDepth.TwentyFour)));
            toDecodeList.Add(new LZ77StoreShort<RGB>(new RGB(34, 68, 1, RGB.ColorDepth.TwentyFour)));
            toDecodeList.Add(new LZ77StoreLong<RGB>(3, 1));
            toDecodeList.Add(new LZ77StoreLong<RGB>(4, 3));
            toDecodeList.Add(new LZ77StoreShort<RGB>(new RGB(255, 124, 61, RGB.ColorDepth.TwentyFour)));
            toDecodeList.Add(new LZ77StoreShort<RGB>(new RGB(12, 45, 31, RGB.ColorDepth.TwentyFour)));
            toDecodeList.Add(new LZ77StoreShort<RGB>(new RGB(95, 84, 168, RGB.ColorDepth.TwentyFour)));

            var decoded = LZ77.Decode<RGB>(toDecodeList.ToArray());

            var expectedList = new List<RGB>();

            expectedList.Add(new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour));
            expectedList.Add(new RGB(12, 42, 125, RGB.ColorDepth.TwentyFour));
            expectedList.Add(new RGB(34, 68, 1, RGB.ColorDepth.TwentyFour));
            expectedList.Add(new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour));
            expectedList.Add(new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour));
            expectedList.Add(new RGB(12, 42, 125, RGB.ColorDepth.TwentyFour));
            expectedList.Add(new RGB(34, 68, 1, RGB.ColorDepth.TwentyFour));
            expectedList.Add(new RGB(255, 124, 61, RGB.ColorDepth.TwentyFour));
            expectedList.Add(new RGB(12, 45, 31, RGB.ColorDepth.TwentyFour));
            expectedList.Add(new RGB(95, 84, 168, RGB.ColorDepth.TwentyFour));

            var expectedArray = expectedList.ToArray();

            CollectionAssert.AreEqual(expectedList, decoded);
        }

        [TestMethod]
        public void LZ77_Encoding_Case_4()
        {
            var toEncodeList = new List<RGB>();

            toEncodeList.Add(new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour));
            toEncodeList.Add(new RGB(12, 42, 125, RGB.ColorDepth.TwentyFour));
            toEncodeList.Add(new RGB(34, 68, 1, RGB.ColorDepth.TwentyFour));
            toEncodeList.Add(new RGB(255, 124, 61, RGB.ColorDepth.TwentyFour));
            toEncodeList.Add(new RGB(12, 45, 31, RGB.ColorDepth.TwentyFour));
            toEncodeList.Add(new RGB(95, 84, 168, RGB.ColorDepth.TwentyFour));

            var encoded = LZ77.Encode(toEncodeList.ToArray(), 255, 255);

            var expected = new List<ImageCompression.Interfaces.IEncodable>();

            expected.Add(new LZ77StoreShort<RGB>(new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour)));
            expected.Add(new LZ77StoreShort<RGB>(new RGB(12, 42, 125, RGB.ColorDepth.TwentyFour)));
            expected.Add(new LZ77StoreShort<RGB>(new RGB(34, 68, 1, RGB.ColorDepth.TwentyFour)));
            expected.Add(new LZ77StoreShort<RGB>(new RGB(255, 124, 61, RGB.ColorDepth.TwentyFour)));
            expected.Add(new LZ77StoreShort<RGB>(new RGB(12, 45, 31, RGB.ColorDepth.TwentyFour)));
            expected.Add(new LZ77StoreShort<RGB>(new RGB(95, 84, 168, RGB.ColorDepth.TwentyFour)));

            var expectedArray = expected.ToArray();

            CollectionAssert.AreEqual(expectedArray, encoded);
        }
    }
}