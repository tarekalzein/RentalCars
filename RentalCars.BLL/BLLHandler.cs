using RentalCars.BusinessCore.models;
using RentalCars.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RentalCars.BLL
{
    public class BLLHandler
    {
        private readonly UnitOfWork unitOfWork;

        public BLLHandler()
        {
            unitOfWork = new UnitOfWork(new RentalCarsDbContext());
        }

        public List<Booking> GetAllBookings() => unitOfWork.Bookings.GetAll().ToList();

        public List<Booking> GetActiveBookings() => unitOfWork.Bookings.GetAll().Where(x => x.IsActive = true).ToList();

        public Booking GetBooking(int id) => unitOfWork.Bookings.Get(id);

        public bool CreateBooking(Booking booking, out int bookingNr)
        {
            //Check no conflict occred and car rent status hasn't change by another user (optimistic concurrency...sort of).
            //1- Check if booking info is correct
            //2- check if car is not rented
            //3- save changes
            if (booking == null || booking.RentedCar.IsRented == true)
            {
                bookingNr = -1;
                return false;
            }
            else
            {
                try
                {
                    unitOfWork.Bookings.Add(booking);
                    Car car = unitOfWork.Cars.Get(booking.BookingNr);
                    car.IsRented = true;
                    unitOfWork.Cars.Update(car.RegNr, car);
                    unitOfWork.Complete();
                    bookingNr = booking.BookingNr;
                    return true;
                }
                catch
                {
                    bookingNr = -1;
                    return false;
                }
            }
        }

        public List<Car> GetAllCars() => unitOfWork.Cars.GetAll().ToList();

        public List<Car> GetAvailableCars() => unitOfWork.Cars.GetAll().Where(x => x.IsRented = false).ToList();

        public List<Car> GetRentedCars() => unitOfWork.Cars.GetAll().Where(x => x.IsRented = true).ToList();

        public Car GetCar(string regID) => unitOfWork.Cars.Get(regID);

        public bool CreateCar(Car car)
        {
            if (car == null)
                return false;
            try
            {
                unitOfWork.Cars.Add(car);
                unitOfWork.Complete();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<CarCategory> GetAllCarCategories() => unitOfWork.CarCategories.GetAll().ToList();

        public CarCategory GetCarCategory(int id) => unitOfWork.CarCategories.Get(id);

        public bool CreateCarCategory(CarCategory carCategory)
        {
            if (carCategory == null)
                return false;

            if (unitOfWork.CarCategories.GetAll().Where(x => x.Category.ToLower() == carCategory.Category.ToLower()).ToList().Count == 0)
            {
                try
                {
                    unitOfWork.CarCategories.Add(carCategory);
                    unitOfWork.Complete();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
                return false;
        }

        public double CalculateRentPrice(TimeSpan ts, CarCategory carCategory, int milage)
        {            
                int days = ts.Days;
                int hours = ts.Hours;
                int minutes = ts.Minutes;
                if (minutes > 0)
                    minutes += 1;
                if (hours > 0)
                    days += 1;

                double price = (Constants.BaseDayRental * days * carCategory.BaseRentalMultiplier)
                    + (Constants.KilometerPrice * milage * carCategory.KilometerPriceMultiplier);
                return price;            
        }
        //public bool EndBooking(Booking booking,int milage, double price)
        public bool EndBooking(int bookingNr, DateTime rentalEndDateTime, int milageOnRentEnd, out double price)
        {
            Booking booking = unitOfWork.Bookings.Get(bookingNr);
            if (booking == null || booking.IsActive != true || booking.RentalDateTime > rentalEndDateTime)
            {
                price = -1;
                return false;
            }
            unitOfWork.Bookings.Update(booking.BookingNr, booking);
            Car car = booking.RentedCar;
            car.IsRented = false;
            unitOfWork.Cars.Update(car.RegNr, car);
            unitOfWork.Complete();
            TimeSpan? t = booking.ReturnDateTime - booking.RentalDateTime;
            if (t.HasValue)
            {
                price = CalculateRentPrice(t.Value, booking.RentedCar.Category,milageOnRentEnd);
                return true;
            }
            else
            {
                price = -1;
                return false;
            }
        }
        public void CancelBooking()
        {

        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

    }
}
