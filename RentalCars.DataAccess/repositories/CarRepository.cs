using RentalCars.BusinessCore.interfaces;
using RentalCars.BusinessCore.models;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace RentalCars.DataAccess.repositories
{
    public class CarRepository : Repository<Car>, ICarRepository
    {
        public CarRepository(RentalCarsDbContext context) : base(context)
        {           
        }

        public Car GetCar(string regNr) => RentalCarsDbContext.Cars.Include(car => car.Category).SingleOrDefault(car => car.RegNr.ToUpper().Equals(regNr.ToUpper()));

        public RentalCarsDbContext RentalCarsDbContext
        {
            get { return Context as RentalCarsDbContext; }
        }

       
    }
}
