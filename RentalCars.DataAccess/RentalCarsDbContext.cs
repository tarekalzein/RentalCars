using RentalCars.BusinessCore.models;
using System.Data.Entity;

namespace RentalCars.DataAccess
{
    public class RentalCarsDbContext : DbContext
    {
        public RentalCarsDbContext()
        {

        }
        public DbSet<Car> Cars { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<CarCategory> CarCategories { get; set; }

        public DbSet<Customer> Customers { get; set; }
    }
}
