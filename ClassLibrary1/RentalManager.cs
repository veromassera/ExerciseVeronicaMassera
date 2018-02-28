using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseVeronicaMassera
{
    public class RentalManager
    {
        public List<RentalOrder> rentalOrders = new List<RentalOrder>();
        public List<BikesByDay> bikesByDays = new List<BikesByDay>(); //Represents bike stock in consecutive non-overlapping periods

        public RentalManager()
        {
            /*
             * Collections must be initialized from the persistence layer, if any
             */

        }
        public bool RentOrder(List<ParameterOfRent> paramsRentOrders, out RentalOrder rentalOrder)
        {
            bool successRentOrder = SimulateRentOrder(paramsRentOrders, out rentalOrder);
            if (successRentOrder)
            {
                //The rent order was successful. Remove the tmp flag and calculate price (promotion).
                rentalOrder.SetPrice();
                rentalOrder.RemoveTmpFlag(); 
                //TODO: verificar que se agregó el rental order a la lista.
            }
            return successRentOrder;
        }

        private bool SimulateRentOrder(List<ParameterOfRent> paramsRents, out RentalOrder rentalOrder)
        {
            rentalOrder = new RentalOrder();
            rentalOrder.Id = Guid.NewGuid();
            rentalOrder.IsDeleted = false;
            rentalOrder.Rents = new List<Rental>();
            rentalOrders.Add(rentalOrder); //Va modificando la cantidad de alquileres que tengo, o sea la disponibilidad

            bool successRentalOrder = true;
            foreach(ParameterOfRent paramsRent in paramsRents)
            {
                Rental rental;
                bool successRental = SimulateRent(rentalOrder.Id, paramsRent, out rental);
                successRentalOrder = successRentalOrder && successRental;
                if (!successRentalOrder)
                {
                    break;
                }
                else {
                    rentalOrder.Rents.Add(rental); //TODO: comentar que se agrega a la coleccion para seguir calculando la disponibilidad
                }
            }
            if (!successRentalOrder)
            {
                // elimino la RentalOrder que se habia agregado a la coleccion
                rentalOrders.Remove(rentalOrder);
                rentalOrder = null;
            }
            return successRentalOrder;

        }

        private bool SimulateRent(Guid tmpId, ParameterOfRent paramsRent, out Rental rental) {
            bool successRent;
            rental = new Rental
            {
                IdRentalOrder = tmpId,
                BikeCount = paramsRent.BikeCount,
                Period = paramsRent.Period,
                PeriodCount = paramsRent.PeriodCount,
                DateFrom = new DateTime(paramsRent.From.Year, paramsRent.From.Month, paramsRent.From.Day, paramsRent.From.Hour, paramsRent.From.Minute, paramsRent.From.Second), //I leave precision in seconds,I eliminate milliseconds
                IsDeleted = false,
                IsTmp = true
            };
            if (IsValidRent(rental)) {
                rental.CalculatePrice();
                successRent = true;
            }
            else
            {
                successRent = false;
                rental = null;
            }

            return successRent;
        }


        private bool IsValidRent(Rental rental)
        {
            var isValidCount = IsValidCount(rental);
            var isValidAvailability = IsValidAvailability(rental);
            return isValidCount && isValidAvailability;
        }

        private bool IsValidCount(Rental rental)
        {
            if (rental == null)
                return false;
            if (rental.BikeCount < 1) {
                throw new ArgumentException("At least one bicycle must be rented", "BikeCount");
            }
            else if (rental.PeriodCount < 1)
            {

                throw new ArgumentException("At least one period must be rented", "PeriodCount");
            }
            else
            {
                return true;
            }
        }

        private bool IsValidAvailability(Rental rental)
        {
            bool isValid = true;
            rental.CalculateDateTo();
            DateTime currDate = new DateTime(rental.DateFrom.Year, rental.DateFrom.Month, rental.DateFrom.Day); //Format: Year, month, day without hours  
            DateTime lastDate = new DateTime(rental.DateTo.Year, rental.DateTo.Month, rental.DateTo.Day); //Format: Year, month, day without hours  
            while (isValid && currDate <= lastDate) {

                var allBikesByDay = GetAllBikesByDay(currDate);
                #region Setting periodFromSearch, periodToSearch
                DateTime periodFromSearch;
                DateTime periodToSearch;
                if (rental.DateFrom < currDate)
                {
                    periodFromSearch = currDate;
                }
                else {
                    periodFromSearch = rental.DateFrom;
                }

                if (rental.DateTo > currDate.AddDays(1))
                {
                    periodToSearch = currDate.AddDays(1).AddSeconds(-1); // ultima horario del dia 23:59:59
                }
                else
                {
                    periodToSearch = rental.DateTo;
                }
                #endregion Setting periodFromSearch, periodToSearch
                var busyBikesByPeriod = GetBusyBikesByPeriod(periodFromSearch, periodToSearch);
                if ((allBikesByDay - busyBikesByPeriod) < rental.BikeCount) {
                    //Comment If in the time zone of the day I do not have available bicycles, I return that the query is invalid
                    isValid = false;
                    break;
                }
                currDate = currDate.AddDays(1);
            }

            return isValid;
        }

        private int GetBusyBikesByPeriod(DateTime periodFromSearch, DateTime periodToSearch)
        {
            int res = 0;
            var rentOrdersActive = rentalOrders.Where(x => x.IsDeleted == false).ToList();
            foreach (RentalOrder rentalOrder in rentOrdersActive)
            {
                int bikesByRentOrder = rentalOrder.Rents.Where(x => (x.IsDeleted == false) && (
                  (periodFromSearch <= x.DateFrom && x.DateFrom <= periodToSearch) ||
                  (periodFromSearch <= x.DateTo && x.DateTo <= periodToSearch)
                )).Select(x => x.BikeCount).Sum();
                res += bikesByRentOrder;
            }
            return res;
        }

        private int GetAllBikesByDay(DateTime currDate)
        {
            /*
             * the bikesByDays is an object that has datefrom, dateto and quantity of bicycles for that period of days, 
             * the fringes are not superimposed, in case of being the last fringe and it is valid the dateto it is null
             */
            BikesByDay bikesByDay;
            bikesByDay = bikesByDays.Where(x => (!x.DateTo.HasValue) && x.DateFrom <= currDate).FirstOrDefault();
            if (bikesByDay != null)
            {
                return bikesByDay.BikeCount;
            }
            else
            {
                bikesByDay = bikesByDays.Where(x => x.DateTo.HasValue && x.DateTo.Value >= currDate && x.DateFrom <= currDate).FirstOrDefault();
                if (bikesByDay != null)
                {
                    return bikesByDay.BikeCount;
                }
                else
                {
                    return 0;
                }
            }
        }

        public void SetBikesByDay(DateTime date, int bikeCount)
        {

            if (bikesByDays.Count != 0)
            {
                var last = bikesByDays.ElementAt(bikesByDays.Count-1);
                if(last.DateFrom >= date)
                {
                    throw new Exception("The date must be greater than the last entered date");
                }
                last.DateTo = date.AddDays(-1);
            }
            bikesByDays.Add(new BikesByDay { BikeCount = bikeCount, DateFrom = date, DateTo = null });

        }


    }
}
