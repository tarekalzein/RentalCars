using RentalCars.BusinessCore.models;
using System.Data.Entity;

namespace RentalCars.DataAccess
{
    public class RentalCarsDbContext : DbContext
    {
        public RentalCarsDbContext()
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        public virtual DbSet<Car> Cars { get; set; }

        public virtual DbSet<Booking> Bookings { get; set; }

        public virtual DbSet<CarCategory> CarCategories { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
