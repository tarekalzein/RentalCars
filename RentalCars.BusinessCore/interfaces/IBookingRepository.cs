using RentalCars.BusinessCore.models;
using System.Collections.Generic;

namespace RentalCars.BusinessCore.interfaces
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Booking GetBooking(int id);
        IEnumerable<Booking> GetAllBookings();
    }


}
