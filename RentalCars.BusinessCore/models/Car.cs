using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCars.BusinessCore.models
{
    public abstract class Car
    {
        public string RegNr { get; set; }

        public int Milage { get; set; }

        public bool IsRented { get; set; }
    }
}
