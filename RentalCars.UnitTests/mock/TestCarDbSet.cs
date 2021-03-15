using RentalCars.BusinessCore.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
