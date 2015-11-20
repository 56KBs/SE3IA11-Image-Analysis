﻿using System;
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
            var component = new Component(255, VariableByte.Bits.Eight);
            Assert.AreEqual("255", component.ToString());

            component = new Component(0, VariableByte.Bits.Eight);
            Assert.AreEqual("0", component.ToString());

            component = new Component(255);
            Assert.AreEqual("255", component.ToString());

            component = new Component(127);
            Assert.AreEqual("127", component.ToString());
        }

        [TestMethod]
        public void Component_Constructor_DifferingBitSizes()
        {
            var component = new Component(127, VariableByte.Bits.Seven);
            Assert.AreEqual("127", component.ToString());

            component = new Component(12, VariableByte.Bits.Five);
            Assert.AreEqual("12", component.ToString());

            component = new Component(3, VariableByte.Bits.Three);
            Assert.AreEqual("3", component.ToString());

            component = new Component(30, VariableByte.Bits.Six);
            Assert.AreEqual("30", component.ToString());
        }

        [TestMethod]
        public void Component_Constructor_BitResizing()
        {
            var component = new Component(255, VariableByte.Bits.Four);
            Assert.AreEqual("15", component.ToString());

            component = new Component(63, VariableByte.Bits.Two);
            Assert.AreEqual("0", component.ToString());
        }

        [TestMethod]
        public void Component_Equality()
        {
            var first = new Component(127, VariableByte.Bits.Eight);
            var second = new Component(127, VariableByte.Bits.Seven);

            Assert.IsTrue(first.Equals(second));

            var third = new Component(120, VariableByte.Bits.Eight);
            var fourth = new Component(120, VariableByte.Bits.Eight);

            Assert.IsTrue(third.Equals(fourth));
        }

        

        [TestMethod]
        public void ComponentTests2()
        {
            var test = new Component(255, VariableByte.Bits.Eight);

            var test2 = new Component(255, VariableByte.Bits.Four);

            var testing = Color.FromArgb(0, 255, 123, 43).ToString();

            var colourTest = new RGB(255, 255, 255, RGB.ColorDepth.TwentyFour);

            var stringlol = colourTest.ToString();
        }
    }
}