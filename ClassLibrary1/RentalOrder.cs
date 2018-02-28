using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseVeronicaMassera
{
    public class RentalOrder {
        public double Price { get; set; }
        public List<Rental> Rents { get; set; }
        public bool IsDeleted { get; set; }
        public Guid Id { get; set; }
        public bool IsFamilyRental() {
            return Rents.Count >= 3 && Rents.Count <= 5;
        }
        public void SetPrice() {
            double price = 0;
            if (Rents.Count > 0)
            {
                price = Rents.Select(x => x.Price).Sum();
                price = ((IsFamilyRental()) ? (price * 0.7) : price);
            }
            Price = price;
        }
        public void RemoveTmpFlag()
        {
            foreach (Rental rent in Rents)
            {
                rent.IsTmp = false;
            }

        }
        public void Delete()
        {
            //TODO: Transaction
            foreach (Rental rent in Rents) {
                rent.IsDeleted = true;
            }
            IsDeleted = true;

        }

       
    }
}
