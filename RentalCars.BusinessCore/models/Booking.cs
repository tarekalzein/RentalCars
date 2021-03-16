using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCars.BusinessCore.models
{
    public class Booking
    {
        [Key]
        public int BookingNr { get; set; }

        public virtual Car RentedCar { get; set; }

        public DateTime RentalDateTime { get; set; }

        public DateTime? ReturnDateTime { get; set; }

        public bool IsActive { get; set; }

        public DateTime CustomerBirthdate { get; set; }

        //Functionality can extended to save list of customers.
        //public Customer Customer { get; set; }
        private Booking()
        {

        }
        public Booking(Car rentedCar, DateTime rentalDateTime, DateTime customerBirthdate)
        {
            RentedCar = rentedCar;
            RentalDateTime = rentalDateTime;
            ReturnDateTime = null;
            IsActive = true;
            CustomerBirthdate = customerBirthdate;
        }


    }
}
