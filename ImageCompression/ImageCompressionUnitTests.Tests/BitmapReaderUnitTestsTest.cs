// <copyright file="BitmapReaderUnitTestsTest.cs">Copyright ©  2015</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestProject1;

namespace UnitTestProject1.Tests
{
    [TestClass]
    [PexClass(typeof(BitmapReaderUnitTests))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class BitmapReaderUnitTestsTest
    {

        [PexMethod(MaxBranches = 20000)]
        public void BitmapRead_Large_Image_Time_Unsafe([PexAssumeUnderTest]BitmapReaderUnitTests target)
        {
            target.BitmapRead_Large_Image_Time_Unsafe();
            // TODO: add assertions to method BitmapReaderUnitTestsTest.BitmapRead_Large_Image_Time_Unsafe(BitmapReaderUnitTests)
        }
    }
}
