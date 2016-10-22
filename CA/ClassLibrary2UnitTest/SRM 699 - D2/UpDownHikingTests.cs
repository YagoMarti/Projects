using ClassLibrary2.SRM_699___D2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2UnitTest.SRM_699___D2
{
    class UpDownHikingTests
    {
        [TestClass]
        public class isPossibleTests
        {
            UpDownHiking UpDownHikingTest = new UpDownHiking();
            [TestMethod]
            public void UpDownHiking_Test0()
            {
                int[] hikingVars = new int[] { 3, 7, 10 };
                int result = UpDownHikingTest.maxHeight(hikingVars[0],hikingVars[1],hikingVars[2]) ;

                int expectedResult = 10;
                Assert.AreEqual(result, expectedResult);
            }
            [TestMethod]
            public void UpDownHiking_Test1()
            {
                int[] hikingVars = new int[] { 5, 40, 30 };
                int result = UpDownHikingTest.maxHeight(hikingVars[0], hikingVars[1], hikingVars[2]);

                int expectedResult = 80;
                Assert.AreEqual(result, expectedResult);
            }
            [TestMethod]
            public void UpDownHiking_Test2()
            {
                int[] hikingVars = new int[] { 2, 50, 1 };
                int result = UpDownHikingTest.maxHeight(hikingVars[0], hikingVars[1], hikingVars[2]);

                int expectedResult = 1;
                Assert.AreEqual(result, expectedResult);
            }
            [TestMethod]
            public void UpDownHiking_Test3()
            {
                int[] hikingVars = new int[] { 3, 42, 42 };
                int result = UpDownHikingTest.maxHeight(hikingVars[0], hikingVars[1], hikingVars[2]);

                int expectedResult = 42;
                Assert.AreEqual(result, expectedResult);
            }
            [TestMethod]
            public void UpDownHiking_Test4()
            {
                int[] hikingVars = new int[] { 20, 7, 9 };
                int result = UpDownHikingTest.maxHeight(hikingVars[0], hikingVars[1], hikingVars[2]);

                int expectedResult = 77;
                Assert.AreEqual(result, expectedResult);
            }
        }
    }
}
