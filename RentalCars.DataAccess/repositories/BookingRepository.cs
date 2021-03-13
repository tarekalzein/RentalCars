using RentalCars.BusinessCore.interfaces;
using RentalCars.BusinessCore.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCars.DataAccess.repositories
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        public BookingRepository(RentalCarsDbContext context) : base(context)
        {
        }

        public override void Add(Booking entity)
        {
            //base.Add(entity);
            Console.WriteLine("called from overriden");
        }

    }
}
