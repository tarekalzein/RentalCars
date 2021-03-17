using RentalCars.BusinessCore.interfaces;
using RentalCars.BusinessCore.models;

namespace RentalCars.DataAccess.repositories
{
    public class CarCategoryRepository : Repository<CarCategory>, ICarCategoriesRepository
    {
        public CarCategoryRepository(RentalCarsDbContext context) : base(context)
        {
        }
    }

}
