using RentalCars.BusinessCore.models;
using RentalCars.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace RentalCars.BLL
{
    public class BLLHandler
    {
        private readonly UnitOfWork unitOfWork;

        public BLLHandler()
        {
            unitOfWork = new UnitOfWork(new RentalCarsDbContext());

        }

        public List<Booking> GetAllBookings() => unitOfWork.Bookings.GetAllBookings().ToList();

        public List<Booking> GetActiveBookings() => unitOfWork.Bookings.GetAllBookings().Where(x => x.IsActive == true).ToList();

        public Booking GetBooking(int id) => unitOfWork.Bookings.GetBooking(id);

        public bool CreateBooking(Booking booking, out int bookingNr)
        {
            //Check no conflict occred and car rent status hasn't change by another user (optimistic concurrency...sort of).
            //1- Check if booking info is correct
            //2- check if car is not rented
            //3- save changes
            //4- return true for success and booking nr.
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
                    unitOfWork.Complete();

                    Car car = unitOfWork.Cars.Get(booking.RentedCar.RegNr);
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
            //Implement better feedback on why creation failed (error in db, duplicate, car status change error etc.)
        }

        public void UpdateBooking(Booking booking)
        {
            unitOfWork.Bookings.Update(booking.BookingNr, booking);
        }

        public List<Car> GetAllCars() => unitOfWork.Cars.GetAll().ToList();

        public List<Car> GetAvailableCars() => unitOfWork.Cars.GetAll().Where(x => x.IsRented = false).ToList();

        //public Car FindCarByRegNr(string regNr) => unitOfWork.Cars.GetAll().Where(x=>x.RegNr.ToUpper().Equals(regNr.ToUpper())).FirstOrDefault();
        public Car FindCarByRegNr(string regNr) => unitOfWork.Cars.GetCar( regNr);


        public List<Car> GetRentedCars() => unitOfWork.Cars.GetAll().Where(x => x.IsRented = true).ToList();

        public Car GetCar(string regID) => unitOfWork.Cars.GetCar(regID);

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
            catch(Exception e)
            {
                return false;
            }
        }

        public void RemoveCar(Car car)
        {
            unitOfWork.Cars.Remove(car);
            unitOfWork.Complete();
        }

        public List<CarCategory> GetAllCarCategories() => unitOfWork.CarCategories.GetAll().ToList();

        public CarCategory GetCarCategory(int id) => unitOfWork.CarCategories.Get(id);

        public bool CreateCarCategory(CarCategory carCategory)
        {
            if (carCategory == null)
                return false;

            if (unitOfWork.CarCategories.GetAll().Where(x => x.Category.ToLower() == carCategory.Category.ToLower()).ToList().Count == 0)
            //int index = unitOfWork.CarCategories.GetAll().ToList().FindIndex(c => c.Category.ToLower().Equals(carCategory.Category.ToLower()));
            //if (index <0)
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

        public void RemoveCarCategory(CarCategory category) {
            unitOfWork.CarCategories.Remove(category);
            unitOfWork.Complete();
        } 

        public double CalculateRentPrice(TimeSpan ts, CarCategory carCategory, int milage)
        {
            int days = (int)Math.Ceiling(ts.TotalDays);
                double price = (Constants.BaseDayRental * days * carCategory.BaseRentalMultiplier)
                    + (Constants.KilometerPrice * milage * carCategory.KilometerPriceMultiplier);
                return price;            
        }

        //public bool EndBooking(Booking booking,int milage, double price)
        public bool EndBooking(int bookingNr, DateTime rentalEndDateTime, int milageOnRentEnd, out double price)
        {
            //TODO: add new property to booking> end milage?
            Booking booking = unitOfWork.Bookings.Get(bookingNr);
            if (booking == null || booking.IsActive == false || booking.RentalDateTime > rentalEndDateTime)
            {
                price = -1;
                return false;
            }
            //update booking info
            booking.ReturnDateTime = rentalEndDateTime;
            booking.IsActive = false;
            unitOfWork.Bookings.Update(booking.BookingNr, booking);
            
            //Call the calculation function
            TimeSpan? t = booking.ReturnDateTime - booking.RentalDateTime;
            if (t.HasValue)
            {
                price = CalculateRentPrice(t.Value, booking.RentedCar.Category,milageOnRentEnd);

                //Update car information.
                Car car = booking.RentedCar;
                car.IsRented = false;
                car.Milage = milageOnRentEnd;
                unitOfWork.Cars.Update(car.RegNr, car);
                unitOfWork.Complete();
                return true;
            }
            else
            {
                price = -1;
                return false;
            }
        }

        public void RemoveBooking(Booking booking)
        {
            unitOfWork.Bookings.Remove(booking);
            unitOfWork.Complete();
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

    }
}
