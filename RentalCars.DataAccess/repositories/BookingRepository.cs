using RentalCars.BusinessCore.interfaces;
using RentalCars.BusinessCore.models;

namespace RentalCars.DataAccess.repositories
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        public BookingRepository(RentalCarsDbContext context) : base(context)
        {
        }

    }
}
