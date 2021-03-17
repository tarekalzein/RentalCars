namespace RentalCars.DataAccess.Migrations
{
    using RentalCars.BusinessCore.models;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<RentalCars.DataAccess.RentalCarsDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RentalCars.DataAccess.RentalCarsDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            context.Cars.AddOrUpdate(x => x.RegNr,
                new Car("TTT123", new CarCategory("Compact", 1, 0), 0));

            context.Cars.AddOrUpdate(x => x.RegNr,
                new Car("AAA123", new CarCategory("Premium", 1.2, 1), 2000));

            context.Cars.AddOrUpdate(x => x.RegNr,
                new Car("BBB123", new CarCategory("Minivan", 1.7, 1.5), 2000));

        }
    }
}
