using RentalCars.BusinessCore.models;
using System.Linq;

namespace RentalCars.UnitTests.mock
{
    public class TestCarDbSet : TestDbSet<Car>
    {
        public override Car Find(params object[] keyValues)
        {
            return this.SingleOrDefault(c => c.RegNr.Equals((string)keyValues.Single()));
        }
    }
}
