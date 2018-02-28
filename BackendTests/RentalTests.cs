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
    public class RentalTests
    {
        [TestMethod()]
        public void CalculatePriceTest()
        {
            // Setup
            Rental rental = new Rental {
                BikeCount = 5,
                Period = new PeriodType { Type = Period.Hour },
                PeriodCount = 3
            };

            // Invocation
            rental.CalculatePrice();

            // Assertions
            Assert.AreEqual(rental.Price, 3 * 5 * rental.Period.GetPrice());
        }

        [TestMethod()]
        public void CalculateDateToTest()
        {
            // Setup
            PeriodType period = new PeriodType { Type = Period.Week };
            DateTime dateFrom = new DateTime(2018, 1, 1);
            int periodCount = 3;
            // Invocation
            var res = period.CalculateDateTo(dateFrom, periodCount);
            // Assertions
            Assert.AreEqual(res, dateFrom.AddDays(7 * periodCount));
        }
    }
}