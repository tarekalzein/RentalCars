using RentalCars.BusinessCore.models;
using System;

namespace RentalCars.events
{
    public class BookingEventInfo : EventArgs
    {
        public Booking Booking { get; set; }
    }
}
