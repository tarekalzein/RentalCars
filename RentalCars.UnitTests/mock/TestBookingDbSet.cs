using RentalCars.BusinessCore.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
