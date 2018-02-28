using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExerciseVeronicaMassera;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseVeronicaMassera.Tests
{
    [TestClass()]
    public class PeriodTypeTests
    {
        [TestMethod()]
        public void CalculateDateToTest()
        {
            DateTime from = new DateTime(2018, 1, 1);
            int periodCount = 3;
            PeriodType periodType = new PeriodType { Type = Period.Hour };

            // Invocation
            DateTime res = periodType.CalculateDateTo(from,periodCount);


            // Assertions
            Assert.AreEqual(res, from.AddHours(1 * periodCount));
            
        }

        [TestMethod()]
        public void CalculateDateTo_Error_Test()
        {
            DateTime from = new DateTime(2018, 1, 1);
            int periodCount = 3;
            PeriodType periodType = new PeriodType { Type = Period.Hour };

            // Invocation
            DateTime res = periodType.CalculateDateTo(from, periodCount);


            // Assertions
            Assert.AreNotEqual(res, from.AddHours(1 * 5* periodCount));

        }
    }
}