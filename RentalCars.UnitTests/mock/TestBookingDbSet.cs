using RentalCars.BusinessCore.models;
using System.Linq;

namespace RentalCars.UnitTests.mock
{
    public class TestBookingDbSet : TestDbSet<Booking>
    {
        public override Booking Find(params object[] keyValues)
        {
            return this.SingleOrDefault(b => b.BookingNr == (int)keyValues.Single());
        }
    }
}
