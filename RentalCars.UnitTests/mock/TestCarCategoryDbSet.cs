using RentalCars.BusinessCore.models;
using System.Linq;

namespace RentalCars.UnitTests.mock
{
    public class TestCarCategoryDbSet : TestDbSet<CarCategory>
    {
        public override CarCategory Find(params object[] keyValues)
        {
            return this.SingleOrDefault(c => c.Id == (int)keyValues.Single());
        }
    }
}
