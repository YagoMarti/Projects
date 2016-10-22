using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary2;
using ClassLibrary2.TopCoder_Medium;

namespace ClassLibrary2UnitTest.TopCoder_Medium
{
    class AirlinerSeatsTests
    {
        [TestClass]
        public class mostAisleSeatsTests
        {
            AirlinerSeats airlinerSeats = new AirlinerSeats();
            [TestMethod]
            public void mostAisleSeats_Test0()
            {
                int width = 6;
                int seats = 3;
                string[] result = airlinerSeats.mostAisleSeats(width, seats);

                string[] expectedResult = new string[] { "..SS.S" };
                CollectionAssert.AreEqual(result, expectedResult);
            }
            [TestMethod]
            public void mostAisleSeats_Test1()
            {
                int width = 6;
                int seats = 4;
                string[] result = airlinerSeats.mostAisleSeats(width, seats);

                string[] expectedResult = new string[] { "S.SS.S" };
                CollectionAssert.AreEqual(result, expectedResult);
            }
            [TestMethod]
            public void mostAisleSeats_Test2()
            {
                int width = 12;
                int seats = 10;
                string[] result = airlinerSeats.mostAisleSeats(width, seats);

                string[] expectedResult = new string[] { "S.SS.SSSSSSS" };
                CollectionAssert.AreEqual(result, expectedResult);
            }
            [TestMethod]
            public void mostAisleSeats_Test3()
            {
                int width = 11;
                int seats = 7;
                string[] result = airlinerSeats.mostAisleSeats(width, seats);

                string[] expectedResult = new string[] { ".SS.SS.SS.S" };
                CollectionAssert.AreEqual(result, expectedResult);
            }
            [TestMethod]
            public void mostAisleSeats_Test4()
            {
                int width = 52;
                int seats = 52;
                string[] result = airlinerSeats.mostAisleSeats(width, seats);

                string[] expectedResult = new string[] { "SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS", "SS" };
                CollectionAssert.AreEqual(result, expectedResult);
            }
            [TestMethod]
            public void mostAisleSeats_Test5()
            {
                int width = 200;
                int seats = 2;
                string[] result = airlinerSeats.mostAisleSeats(width, seats);

                string[] expectedResult = new string[] { "..................................................", "..................................................", "..................................................", "...............................................S.S" };
                CollectionAssert.AreEqual(result, expectedResult);
            }
        }
    }
}
