using ClassLibrary2.SRM_697___D2;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClassLibrary2UnitTest.SRM_697___D2
{
    class DivisibleSetDiv2Tests
    {
        [TestClass]
        public class isPossibleTests
        {
            DivisibleSetDiv2 DivisibleSetDiv2Test = new DivisibleSetDiv2();
            [TestMethod]
            public void getInitials_Test0()
            {
                int[] powers = new int[] { 1, 2, 3 };
                string result = DivisibleSetDiv2Test.isPossible(powers);

                string expectedResult = "Possible";
                Assert.AreEqual(result, expectedResult);
            }
        }
    }
}
