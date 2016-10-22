using ClassLibrary2.SRM_699___D2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2UnitTest.SRM_699___D2
{
    class LastDigitTests
    {
        [TestClass]
        public class lastDigitTest
        {
            LastDigit lastDigitsTest = new LastDigit();
            [TestMethod]
            public void lastDigit_Test0()
            {
                long S = 509;
                long result = lastDigitsTest.findX(S);

                int expectedResult = 564;
                Assert.AreEqual(result, expectedResult);
            }
        }
    }
}
