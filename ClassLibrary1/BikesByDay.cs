using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseVeronicaMassera
{
    /// <summary>
    /// the bikesByDays is an object that has datefrom, dateto and quantity of bicycles for that period of days,
    /// the fringes are not superimposed, in case of being the last fringe and it is valid the dateto it is null
    /// </summary>
    public class BikesByDay
    {
        public int BikeCount { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
    
}
