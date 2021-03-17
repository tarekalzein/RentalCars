using RentalCars.BusinessCore.models;
using RentalCars.DataAccess;
using System.Data.Entity;

namespace RentalCars.UnitTests.mock
{
    public class TestRentalAppContext : RentalCarsDbContext
    {
        public TestRentalAppContext()
        {
            this.CarCategories = new TestCarCategoryDbSet();

        }

        public DbSet<CarCategory> CarCategories { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public int SaveChanges() => 0;

        public void MarkAsModified(CarCategory cc) { }
        public void MarkAsModified(Car c) { }
        public void MarkAsModified(Booking b) { }

        public void Dispose() { }

    }
}
