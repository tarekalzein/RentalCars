using RentalCars.BusinessCore.models;
using System;
using System.Collections.Generic;

namespace RentalCars
{
    public class Dummy
    {
        List<CarCategory> CarCategories;
        List<Car> Cars;
        List<Booking> Bookings;
        public Dummy()
        {
            CarCategories = new List<CarCategory>();
            Cars = new List<Car>();
            Bookings = new List<Booking>();
            CreateMockData();

        }

        private void CreateMockData()
        {
            CarCategories.Add(new CarCategory("Compact", 1, 0));
            CarCategories.Add(new CarCategory("Premium", 1.2, 1));
            CarCategories.Add(new CarCategory("Minivan", 1.7, 1.5));

            Cars.Add(new Car("Test1", CarCategories[0], 0));
            Cars.Add(new Car("Test2", CarCategories[1], 15000));
            Cars.Add(new Car("Test3", CarCategories[2], 2000));

            Bookings.Add(new Booking(Cars[0], DateTime.Now, new DateTime(1985, 01, 23)) { BookingNr = 1 });
            Bookings.Add(new Booking(Cars[1], new DateTime(2021, 03, 10, 15, 00, 00), new DateTime(1985, 01, 23)) { BookingNr = 2 });
            Bookings.Add(new Booking(Cars[2], new DateTime(2021, 03, 2, 08, 00, 00), new DateTime(1980, 01, 01)) { BookingNr = 3 });
        }

        public List<Booking> GetDummyBookings() => Bookings;
    }
}
