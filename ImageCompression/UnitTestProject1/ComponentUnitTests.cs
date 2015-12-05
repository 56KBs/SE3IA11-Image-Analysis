using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImageCompression;
using ImageCompression.ColorModel;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Drawing;

namespace ImageCompressionUnitTests
{
    [TestClass]
    public class ComponentUnitTests
    {
        [TestMethod]
        public void Component_Constructor_Default()
        {
            var component = new Component(255, 8);
            Assert.AreEqual("255", component.ToString());

            component = new Component(0, 8);
            Assert.AreEqual("0", component.ToString());

            component = new Component(255);
            Assert.AreEqual("255", component.ToString());

            component = new Component(127);
            Assert.AreEqual("127", component.ToString());
        }

        [TestMethod]
        public void Component_Constructor_DifferingBitSizes()
        {
            var component = new Component(127, 7);
            Assert.AreEqual("127", component.ToString());

            component = new Component(12, 5);
            Assert.AreEqual("12", component.ToString());

            component = new Component(3, 3);
            Assert.AreEqual("3", component.ToString());

            component = new Component(30, 6);
            Assert.AreEqual("30", component.ToString());
        }

        [TestMethod]
        public void Component_Constructor_BitResizing()
        {
            var component = new Component(255, 4);
            Assert.AreEqual("15", component.ToString());

            component = new Component(63, 2);
            Assert.AreEqual("0", component.ToString());
        }

        [TestMethod]
        public void Component_Equality()
        {
            var first = new Component(127, 8);
            var second = new Component(127, 7);

            Assert.IsTrue(first.Equals(second));

            var third = new Component(120, 8);
            var fourth = new Component(120, 8);

            Assert.IsTrue(third.Equals(fourth));
        }

        

        [TestMethod]
        public void ComponentTests2()
        {
            var test = new Component(255, 8);

            var test2 = new Component(255, 4);

            var testing = Color.FromArgb(0, 255, 123, 43).ToString();

            var colourTest = new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour);

            var stringlol = colourTest.ToString();
        }
    }
}
