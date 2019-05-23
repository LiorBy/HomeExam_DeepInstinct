using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeExam_DeepInstinct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeExam_DeepInstinct.Tests
{
    [TestClass()]
    public class GuardTests
    {
        private Guard m_Guard = new Guard();  

        [TestMethod()]
        public void UpdateTotalSleepTest()
        {
            //arrange
            DateTime startTime = new DateTime(2019, 01, 04, 13, 57, 00);
            DateTime endTime = new DateTime(2019, 01, 04, 14, 56, 00);
            Guard expectedGuard = new Guard();
            expectedGuard.TotalSleep = 59;
            //act
            m_Guard.UpdateTotalSleep(startTime, endTime);
            int actual = m_Guard.TotalSleep;
            //assert
            Assert.AreEqual(expectedGuard.TotalSleep, actual);
        }

        [TestMethod()]
        public void UpdateAllHuoresOfSleepTest()
        {
            //arrange
            DateTime startTime = new DateTime(2019, 01, 04, 13, 57, 00);
            DateTime endTime = new DateTime(2019, 01, 04, 13, 59, 00);

            DateTime minuteOfSleep1 = new DateTime(2019, 01, 04, 13, 57, 00);
            DateTime minuteOfSleep2 = new DateTime(2019, 01, 04, 13, 58, 00);
            DateTime minuteOfSleep3 = new DateTime(2019, 01, 04, 13, 59, 00);

            Guard expectedGuard = new Guard();
            expectedGuard.MostLikelyOfSleep.Add(minuteOfSleep1.ToString("HH:mm"), 1);
            expectedGuard.MostLikelyOfSleep.Add(minuteOfSleep2.ToString("HH:mm"), 1);
            expectedGuard.MostLikelyOfSleep.Add(minuteOfSleep3.ToString("HH:mm"), 1);
            //act
            m_Guard.UpdateAllHuoresOfSleep(startTime, endTime);
            //assert
            CollectionAssert.AreEqual(expectedGuard.MostLikelyOfSleep, m_Guard.MostLikelyOfSleep);
        }
    }
}