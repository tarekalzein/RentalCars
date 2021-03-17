using RentalCars.BusinessCore.interfaces;
using RentalCars.BusinessCore.models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RentalCars.DataAccess.repositories
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        public BookingRepository(RentalCarsDbContext context) : base(context)
        {

        }

        public IEnumerable<Booking> GetAllBookings() => (Context as RentalCarsDbContext).Bookings.Include(x => x.RentedCar).Include(x => x.RentedCar.Category);

        public Booking GetBooking(int id) => (Context as RentalCarsDbContext).Bookings.Include(c => c.RentedCar).Include(c => c.RentedCar.Category).SingleOrDefault(x => x.BookingNr == id);
    }
}
