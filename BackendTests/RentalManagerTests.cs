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
    public class RentalManagerTests
    {
        [TestMethod()]
        public void RentOrderTest()
        {
            //Setup
            RentalManager rm = new RentalManager();
            rm.SetBikesByDay(new DateTime(2018, 1, 1), 10);
            rm.SetBikesByDay(new DateTime(2018, 5, 1), 20);
            rm.SetBikesByDay(new DateTime(2018, 10, 1), 30);

            List<ParameterOfRent> paramsRents = new List<ParameterOfRent>();
            paramsRents.Add(
                new ParameterOfRent
                {
                    BikeCount = 5,
                    From = new DateTime(2018, 1, 1, 13, 0, 0),
                    Period = new PeriodType { Type = Period.Hour },
                    PeriodCount = 3
                }); //Rent for 3 hours of 5 bike from 13hs
            paramsRents.Add(
                new ParameterOfRent
                {
                    BikeCount = 5,
                    From = new DateTime(2018, 1, 1, 13, 0, 0),
                    Period = new PeriodType { Type = Period.Hour },
                    PeriodCount = 3
                }); //Other rent for 3 hours of 5 bike from 13hs
            RentalOrder rentalOrder; 
            bool res = rm.RentOrder(paramsRents, out rentalOrder);
            Assert.AreEqual(res, true && rentalOrder.Price == 150);
        }
        [TestMethod()]
        public void RentOrder_Error_Test()
        {
            //Setup
            RentalManager rm = new RentalManager();
            rm.SetBikesByDay(new DateTime(2018, 1, 1), 10);
            rm.SetBikesByDay(new DateTime(2018, 5, 1), 20);
            rm.SetBikesByDay(new DateTime(2018, 10, 1), 30);

            List<ParameterOfRent> paramsRents = new List<ParameterOfRent>();
            paramsRents.Add(
                new ParameterOfRent
                {
                    BikeCount = 5,
                    From = new DateTime(2018, 1, 1, 13, 0, 0),
                    Period = new PeriodType { Type = Period.Hour },
                    PeriodCount = 3
                }); //Rent for 3 hours of 5 bike from 13hs
            paramsRents.Add(
                new ParameterOfRent
                {
                    BikeCount = 5,
                    From = new DateTime(2018, 1, 1, 13, 0, 0),
                    Period = new PeriodType { Type = Period.Hour },
                    PeriodCount = 3
                }); //Other rent for 3 hours of 5 bike from 13hs
            paramsRents.Add(
               new ParameterOfRent
               {
                   BikeCount = 5,
                   From = new DateTime(2018, 1, 1, 13, 0, 0),
                   Period = new PeriodType { Type = Period.Hour },
                   PeriodCount = 3
               }); //Other rent for 3 hours of 5 bike from 13hs
            RentalOrder rentalOrder;
            bool res = rm.RentOrder(paramsRents, out rentalOrder);
            //It should not be possible to rent since there are only 10 bike for that day
            Assert.AreEqual(res, false);
        }
        [TestMethod()]
        public void RentOrder_FamilyRental_Test()
        {
            //Setup
            RentalManager rm = new RentalManager();
            rm.SetBikesByDay(new DateTime(2018, 1, 1), 10);
            rm.SetBikesByDay(new DateTime(2018, 5, 1), 20);
            rm.SetBikesByDay(new DateTime(2018, 10, 1), 30);

            List<ParameterOfRent> paramsRents = new List<ParameterOfRent>();
            paramsRents.Add(
                new ParameterOfRent
                {
                    BikeCount = 5,
                    From = new DateTime(2018, 11, 1, 13, 0, 0),
                    Period = new PeriodType { Type = Period.Hour },
                    PeriodCount = 3
                }); //Rent for 3 hours of 5 bike from 13hs
            paramsRents.Add(
                new ParameterOfRent
                {
                    BikeCount = 5,
                    From = new DateTime(2018, 11, 1, 13, 0, 0),
                    Period = new PeriodType { Type = Period.Hour },
                    PeriodCount = 3
                }); //Other rent for 3 hours of 5 bike from 13hs
            paramsRents.Add(
                new ParameterOfRent
                {
                    BikeCount = 5,
                    From = new DateTime(2018, 11, 1, 13, 0, 0),
                    Period = new PeriodType { Type = Period.Hour },
                    PeriodCount = 3
                }); //Other rent for 3 hours of 5 bike from 13hs
            
            // Invocation
            RentalOrder rentalOrder;
            bool res = rm.RentOrder(paramsRents, out rentalOrder);
            
            // Assertions
            Assert.AreEqual(res, true && rentalOrder.Price == (75*3*0.70) && rentalOrder.IsFamilyRental());
        }
        [TestMethod()]
        public void SetBikesByDayTest()
        {
            //Setup
            RentalManager rm = new RentalManager();
            rm.SetBikesByDay(new DateTime(2018, 1, 1), 10);
            rm.SetBikesByDay(new DateTime(2018, 5, 1), 20);
            rm.SetBikesByDay(new DateTime(2018, 10, 1), 30);

            // Invocation
            bool ok, ok_1, ok_2, ok_3;
            ok_1 = rm.bikesByDays.ElementAt(0).DateFrom == new DateTime(2018, 1, 1)
                    && rm.bikesByDays.ElementAt(0).DateTo == new DateTime(2018, 4, 30)
                    && rm.bikesByDays.ElementAt(0).BikeCount == 10;
            ok_2 = rm.bikesByDays.ElementAt(1).DateFrom == new DateTime(2018, 5, 1)
                    && rm.bikesByDays.ElementAt(1).DateTo == new DateTime(2018, 9, 30)
                    && rm.bikesByDays.ElementAt(1).BikeCount == 20;
            ok_3 = rm.bikesByDays.ElementAt(2).DateFrom == new DateTime(2018, 10, 1)
                    && rm.bikesByDays.ElementAt(2).DateTo == null
                    && rm.bikesByDays.ElementAt(2).BikeCount == 30;
            ok = ok_1 && ok_2 && ok_3;
            
            // Assertions
            Assert.AreEqual(ok, true);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void SetBikesByDay_Error_Test()
        {
            RentalManager rm = new RentalManager();
            rm.SetBikesByDay(new DateTime(2018, 1, 1), 10);
            rm.SetBikesByDay(new DateTime(2018, 5, 1), 20);
            rm.SetBikesByDay(new DateTime(2018, 3, 1), 30);
        }
    }
}