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
            var dataList = new List<Component> { new Component(23), new Component(43), new Component(12), new Component(234), new Component(234), new Component(84), new Component(84), new Component(84) };

            var runLenghted = dataList.GroupRuns(i => i);
        }
    }
}
