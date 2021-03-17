using RentalCars.BusinessCore.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCars.events
{
    public class BookingEventInfo :EventArgs
    {
        public Booking Booking { get; set; }
    }
}
