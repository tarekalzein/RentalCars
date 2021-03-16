using RentalCars.BusinessCore.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCars.BusinessCore.interfaces
{
    public interface ICarRepository : IRepository<Car>
    {
        Car GetCar(string regNr);
    }
}
