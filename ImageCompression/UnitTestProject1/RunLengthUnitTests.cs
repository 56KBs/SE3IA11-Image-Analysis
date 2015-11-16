using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImageCompression;
using ImageCompression.ColorModel;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ImageCompressionUnitTests
{
    [TestClass]
    public class RunLengthUnitTests
    {
        /*[TestMethod]
        public void RunLength_Encoding_Components()
        {
            var dataList = new List<Component> { new Component(23), new Component(43), new Component(12), new Component(234), new Component(234), new Component(84), new Component(84), new Component(84) };

            var result = ImageCompression.Encoders.RunLength.Encode(dataList);

            var stringBuilder = new StringBuilder();
            result.ForEach(x => stringBuilder.Append(x.ToString() + ","));
            var stringified = stringBuilder.ToString().Substring(0, stringBuilder.Length - 1);

            Assert.AreEqual("23,43,12,234;1,84;2", stringified);
        }

        [TestMethod]
        public void RunLength_Decoding_Components()
        {
            var stringToDecode = "23,43,12,234;1,84;2";

            var decoded = ImageCompression.Encoders.RunLength.Decode(stringToDecode);

            var convertedDecode = decoded.ConvertAll(new Converter<string, Component>(element => new Component((byte)int.Parse(element))));

            var dataList = new List<Component> { new Component(23), new Component(43), new Component(12), new Component(234), new Component(234), new Component(84), new Component(84), new Component(84) };

            CollectionAssert.AreEqual(dataList, convertedDecode);
        }

        [TestMethod]
        public void RunLength_Decoding_Autocast_Components()
        {
            var stringToDecode = "23,43,12,234;1,84;2";

            var decoded = ImageCompression.Encoders.RunLength.Decode(stringToDecode);

            var dataList = new List<Component> { new Component(23), new Component(43), new Component(12), new Component(234), new Component(234), new Component(84), new Component(84), new Component(84) };

            CollectionAssert.AreEqual(dataList, decoded);
        }*/
    }
}
