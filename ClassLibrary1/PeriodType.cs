using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseVeronicaMassera
{
    
    public class PeriodType
    {
        public Period Type { get; set; }
        public DateTime CalculateDateTo(DateTime from, int periodCount) {
            switch (this.Type)
            {
                case Period.Hour:
                    return from.AddHours(1 * periodCount);
                case Period.Day:
                    return from.AddDays(1 * periodCount);
                case Period.Week:
                    return from.AddDays(7 * periodCount);
                default:
                    throw new Exception(String.Format("Period '{0}' is invalid ", this.Type.ToString()));
            }
        }
        public double GetPrice()
        {
            double price = 0;
            switch (this.Type)
            {
                case Period.Hour:
                    price = 5;
                    break;
                case Period.Day:
                    price = 20;
                    break;
                case Period.Week:
                    price = 60;
                    break;
                default:
                    throw new Exception(String.Format("Period '{0}' is invalid ", this.Type.ToString()));
                    
            }
            return price;
        }


    }
    
}
