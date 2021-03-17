using RentalCars.BusinessCore.models;

namespace RentalCars.BusinessCore.interfaces
{
    public interface ICarRepository : IRepository<Car>
    {
        Car GetCar(string regNr);
    }
}
