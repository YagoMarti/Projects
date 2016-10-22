using ClassLibrary2.SRM_697___D2;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClassLibrary2UnitTest.SRM_697___D2
{
    class TriangleMakingTests
    {
        [TestClass]
        public class maxPerimeterTests
        {
            TriangleMaking triangleMaking = new TriangleMaking();
            [TestMethod]
            public void getInitials_Test0()
            {
                int[] sides = new int[] { 1, 2, 3 };
                int result = triangleMaking.maxPerimeter(sides[0], sides[1], sides[2]);

                int expectedResult = 5;
                Assert.AreEqual(result, expectedResult);
            }
            [TestMethod]
            public void getInitials_Test1()
            {
                int[] sides = new int[] { 2,2,2 };
                int result = triangleMaking.maxPerimeter(sides[0], sides[1], sides[2]);

                int expectedResult = 6;
                Assert.AreEqual(result, expectedResult);
            }
            [TestMethod]
            public void getInitials_Test2()
            {
                int[] sides = new int[] { 1, 100, 1};
                int result = triangleMaking.maxPerimeter(sides[0], sides[1], sides[2]);

                int expectedResult = 3;
                Assert.AreEqual(result, expectedResult);
            }
            [TestMethod]
            public void getInitials_Test3()
            {
                int[] sides = new int[] { 41, 64, 16};
                int result = triangleMaking.maxPerimeter(sides[0], sides[1], sides[2]);

                int expectedResult = 113;
                Assert.AreEqual(result, expectedResult);
            }
        }
    }
}
