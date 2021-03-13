using RentalCars.BusinessCore.interfaces;
using RentalCars.BusinessCore.models;

namespace RentalCars.DataAccess.repositories
{
    public class CarRepository : Repository<Car>, ICarRepository
    {
        public CarRepository(RentalCarsDbContext context) : base(context)
        {
        }
    }
}
