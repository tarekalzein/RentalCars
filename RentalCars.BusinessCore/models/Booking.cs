using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCars.BusinessCore.models
{
    public class Booking
    {
        [Key]
        public int BookingNr { get; set; }

        public Car RentedCar { get; set; }

        public DateTime RentalDateTime { get; set; }

        public DateTime ReturnDateTime { get; set; }

        public bool IsActive { get; set; }

        public DateTime CustomerBirthdate { get; set; }

        //Functionality can extended to save list of customers.
        //public Customer Customer { get; set; }

    }
}
