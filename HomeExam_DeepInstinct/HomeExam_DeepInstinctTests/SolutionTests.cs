using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeExam_DeepInstinct;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace HomeExam_DeepInstinct.Tests
{
    [TestClass()]
    public class SolutionTests
    {
        private Solution m_solution = new Solution();

        [TestMethod()]
        public void RunChallengeTest1()
        {
            //arrange
            string path1 = @"../../data/activityList.txt";
            string expected = "Guard #10 is most likely to be asleep in 00:24\r\n";
            //act
            Console.Clear();
            string actual = checkOneFile(path1);
            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RunChallengeTest2()
        {
            //arrange
            string path2 = @"../../data/activityList2.txt";
            string expected2 = "Guard #99 is most likely to be asleep in 00:45\r\n";
            //act
            Console.Clear();
            string actual = checkOneFile(path2);
            //assert
            Assert.AreEqual(expected2, actual);
        }

        private string checkOneFile(string i_Path)
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.Clear();
                Console.SetOut(sw);
                m_solution.RunChallenge(i_Path);
                return sw.ToString();
            }
        }

        [TestMethod]
        public void extractDateAndTimeTest()
        {
            //arrange
            PrivateObject obj = new PrivateObject(m_solution);
            string inputString = "[2019-01-04 13:57] The quick brown fox jumps over the lazy dog";
            DateTime expected = new DateTime(2019, 01, 04, 13, 57, 00);
            //act
            var retVal = obj.Invoke("extractDateAndTime", inputString);
            //assert
            Assert.AreEqual(expected, retVal);
        }

        [TestMethod]
        public void extractInfoFromOneLineTest()
        {
            //arrange
            PrivateObject obj = new PrivateObject(m_solution);
            string[] strArr = { "The", "quick", "brown", "fox", "jumps", "over", "the", "lazy", "dog" };
            string inputString = "The quick brown fox jumps over the lazy dog";
            List<string> expected = new List<string>(strArr);
            //act
            var retVal = obj.Invoke("extractInfoFromOneLine", inputString);
            var actual = retVal as List<string>;
            //assert
            CollectionAssert.AreEqual(expected, actual);
        }
    }    
}