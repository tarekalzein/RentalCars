using RentalCars.BusinessCore.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCars.BusinessCore.interfaces
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Booking GetBooking(int id);
        IEnumerable<Booking> GetAllBookings();
    }

   
}
