using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCars.BusinessCore.models
{
    class Booking
    {
        public int BookingNr { get; set; }
        public Car RentedCar { get; set; }
        public DateTime RentalDateTime { get; set; }
        public DateTime ReturnDateTime { get; set; }
        public bool IsActive { get; set; }

    }
}
