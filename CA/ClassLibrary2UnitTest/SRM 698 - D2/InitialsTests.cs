using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary2.SRM_698___R1;

namespace ClassLibrary2UnitTest.SRM_698___D2
{
    class InitialsTests
    {
        [TestClass]
        public class getInitialsTests
        {
            Initials initials = new Initials();
            [TestMethod]
            public void getInitials_Test0()
            {
                String name = "john fitzgerald kennedy";
                String result = initials.getInitials(name);

                String expectedResult = "jfk";
                Assert.AreEqual(result, expectedResult);
            }
            [TestMethod]
            public void getInitials_Test1()
            {
                String name = "single";
                String result = initials.getInitials(name);

                String expectedResult = "s";
                Assert.AreEqual(result, expectedResult);
            }
            [TestMethod]
            public void getInitials_Test2()
            {
                String name = "looks good to me";
                String result = initials.getInitials(name);

                String expectedResult = "lgtm";
                Assert.AreEqual(result, expectedResult);
            }
            [TestMethod]
            public void getInitials_Test3()
            {
                String name = "single round match";
                String result = initials.getInitials(name);

                String expectedResult = "srm";
                Assert.AreEqual(result, expectedResult);
            }
            [TestMethod]
            public void getInitials_Test4()
            {
                String name = "a aa aaa aa a bbb bb b bb bbb";
                String result = initials.getInitials(name);

                String expectedResult = "aaaaabbbbb";
                Assert.AreEqual(result, expectedResult);
            }
        }
    }
}
