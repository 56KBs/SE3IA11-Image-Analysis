using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImageCompression;
using ImageCompression.ColorModel;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ImageCompression.ExtensionMethods;

namespace ImageCompressionUnitTests
{
    [TestClass]
    public class RunLengthUnitTests
    {
        [TestMethod]
        public void RunLength_Encoding_Linq()
        {
            var dataList = new List<byte> { 23, 43, 12, 234, 234, 84, 84, 84 };

            var runLenghted = dataList.GroupRuns(i => i);
        }
    }
}
