using RentalCars.BusinessCore.interfaces;
using RentalCars.DataAccess.repositories;

namespace RentalCars.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RentalCarsDbContext Context;

        public UnitOfWork(RentalCarsDbContext context)
        {
            Context = context;
            Cars = new CarRepository(Context);
            CarCategories = new CarCategoryRepository(Context);
            Bookings = new BookingRepository(Context);

        }

        public ICarRepository Cars { get; private set; }

        public ICarCategoriesRepository CarCategories { get; private set; }

        public IBookingRepository Bookings { get; private set; }

        public int Complete()
        {
            return Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
