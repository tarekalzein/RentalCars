using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCars.BusinessCore.models
{
    public class Car
    {
        public string RegNr { get; set; }

        public CarCategory Category { get; set; }

        public int Milage { get; set; }

        public bool IsRented { get; set; }

    }
}
