using System;
using System.ComponentModel.DataAnnotations;

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
