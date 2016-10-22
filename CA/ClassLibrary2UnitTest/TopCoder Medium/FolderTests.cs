using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary2;
using ClassLibrary2.TopCoder_Medium;

namespace ClassLibrary2UnitTest
{
    public class FolderTests
    {
        [TestClass]
        public class FolderSizeTests
        {
            FolderSize folder = new FolderSize();
            [TestMethod]
            public void calculateWaste_Test0()
            {
                 string[] files = new string[]{"0 55", "0 47", "1 86"};
                 int folders = 3;
                 int clusterSize = 50;
                 int[] rets = folder.calculateWaste(files,folders,clusterSize);

                int[] expectedResult =  new int[] { 48, 14, 0 };
                 CollectionAssert.AreEqual(rets,expectedResult);
            }
            [TestMethod]
            public void calculateWaste_Test1()
            {
                string[] files = new string[] { "0 123", "2 456", "4 789", "6 012", "8 345" };
                int folders = 10;
                int clusterSize = 98;
                int[] rets = folder.calculateWaste(files, folders, clusterSize);

                int[] expectedResult = new int[] { 73, 0, 34, 0, 93, 0, 86, 0, 47, 0 };
                CollectionAssert.AreEqual(rets, expectedResult);
            }
            [TestMethod]
            public void calculateWaste_Test2()
            {
                string[] files = new string[] {  };
                int folders = 5;
                int clusterSize = 100;
                int[] rets = folder.calculateWaste(files, folders, clusterSize);

                int[] expectedResult = new int[] { 0, 0, 0, 0, 0 };
                CollectionAssert.AreEqual(rets, expectedResult);
            }
            [TestMethod]
            public void calculateWaste_Test3()
            {
                string[] files = new string[] { "0 93842", "1 493784", "2 43212", "3 99327", "4 456209", "5 947243", "6 59348", "7 58237", "8 5834", "9 492384", "0 58342", "3 538432", "6 1432", "9 453983", "2 4321", "4 583729", "6 6974", "8 9864", "4 43211", "8 38437" };
                int folders = 10;
                int clusterSize = 22485;
                int[] rets = folder.calculateWaste(files, folders, clusterSize);

                int[] expectedResult = new int[] { 27696, 886, 19922, 14306, 18616, 19612, 44671, 9218, 35805, 20488 };
                CollectionAssert.AreEqual(rets, expectedResult);
            }

        }
    }
}
