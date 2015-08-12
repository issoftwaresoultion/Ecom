using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ecommerce;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Utility.GetConvertedPrice(100, "Naira", "");
            Utility.GetConvertedPrice(100, "Kwanza", "");
            Utility.GetConvertedPrice(100, "Cedi", "");
        }
    }
}
