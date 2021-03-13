using RentalCars.BusinessCore.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCars.BusinessCore
{
     public class TestManager
    {
        ObservableCollection<Car> Cars;
        ObservableCollection<Booking> Bookings;
        ObservableCollection<CarCategory> CarCategories;

        public TestManager()
        {
            Cars = new ObservableCollection<Car>();
            Bookings = new ObservableCollection<Booking>();
            CarCategories = new ObservableCollection<CarCategory>();

            SeedTestData();
        }

        private void SeedTestData()
        {
            //CarCategories.Add(new CarCategory { Category = "Compact", BaseRentalMultiplier = 1, KilometerPriceMultiplier = 1 });
            //CarCategories.Add(new CarCategory { Category = "Premium", BaseRentalMultiplier = 1.2, KilometerPriceMultiplier = 1 });
            //CarCategories.Add(new CarCategory { Category = "Minivan", BaseRentalMultiplier = 1.7, KilometerPriceMultiplier = 1.5 });

            //Cars.Add(new Car { RegNr = "AAA123", Category = CarCategories[0], IsRented = false, Milage = 5000 });
            //Cars.Add(new Car { RegNr = "BBB123", Category = CarCategories[1], IsRented = true, Milage = 15000 });
            //Cars.Add(new Car { RegNr = "CCC123", Category = CarCategories[2], IsRented = false, Milage = 7000 });
            //Cars.Add(new Car { RegNr = "DDD123", Category = CarCategories[0], IsRented = true, Milage = 9000 });
            //Cars.Add(new Car { RegNr = "EEE123", Category = CarCategories[1], IsRented = false, Milage = 500 });
            //Cars.Add(new Car { RegNr = "FFF123", Category = CarCategories[2], IsRented = true, Milage = 20000 });

            //Bookings.Add(new Booking { BookingNr = 0, CustomerBirthdate = new DateTime(1985, 01, 23), IsActive = true,RentalDateTime = new DateTime(2021,03,10,15,05,00),RentedCar = Cars[1] });
            //Bookings.Add(new Booking { BookingNr = 1, CustomerBirthdate = new DateTime(1985, 01, 23), IsActive = false, RentalDateTime = new DateTime(2021, 03, 05, 01, 16, 10), RentedCar = Cars[0],ReturnDateTime = new DateTime(2021, 03, 06, 01, 00, 00) });
            //Bookings.Add(new Booking { BookingNr = 2, CustomerBirthdate = new DateTime(1987, 01, 02), IsActive = true, RentalDateTime = new DateTime(2021, 03, 01, 15, 00, 00), RentedCar = Cars[3] });
        }

        public List<Booking> GetActiveBookings()
        {
            return Bookings.Where(x => x.IsActive = true).ToList();
        }

        

    }
}
//TODO: Constructor for Car
//TODO: Constructor for category
//TODO: Consutructor for Booking