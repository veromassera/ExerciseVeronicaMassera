using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseVeronicaMassera
{
    public class Rental
    {
        public Guid IdRentalOrder { get; set; } //TODO: Comentar redundancia?
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int BikeCount { get; set; } //TODO: Comment: Se puede alquilar una o muchas bicicletas, por uno (o muchos) periodos de tiempo (hour, day, week) Ej: 3 bicicletas por 2 horas 
        public PeriodType Period { get; set; }
        public int PeriodCount { get; set; } //TODO: Comment: Se puede alquilar una (o muchas) bicicletas, por uno o muchos periodos de tiempo (hour, day, week) Ej: 3 bicicletas por 2 horas 
        public double Price { get; set; }
        public bool IsTmp { get; set; } //TODO: Comment: marca de objeto generado por la simulacion en caso de no se exitosa, la simulación se deben eliminar
        public bool IsDeleted { get; set; } //logical erasing


        public void CalculatePrice()
        {
            Price = PeriodCount * Period.GetPrice() * BikeCount;
        }

        public void CalculateDateTo()
        {
            DateTo =  Period.CalculateDateTo(DateFrom, PeriodCount);
        }
    }
}
