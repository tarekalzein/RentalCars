using Microsoft.VisualStudio.TestTools.UnitTesting;
using RentalCars.BLL;
using RentalCars.BusinessCore.models;
using System;
using System.Linq;

namespace RentalCars.UnitTests
{
    [TestClass]
    public class UserCasesTests
    {
        int TEST_PRICE_CORRECT_VALUE = 499;
        BLLHandler bllHandler = new BLLHandler();        

         [TestMethod]
        public void CreateBooking_BookingIsNull_ReturnsFalse()
        {
            int bookingNr;
            Booking booking = null;

            bool result = bllHandler.CreateBooking(booking, out bookingNr);
            Assert.IsFalse(result);
            Assert.AreEqual(-1, bookingNr);
        }

        [TestMethod]
        public void CreateBooking_UseTestCategoryAndCar_ReturnsTrueWithBookingNr()
        {
            //Prepare test data
            int success = CreateTestData();
            CarCategory carCategory = bllHandler.GetAllCarCategories().Where(x => x.Category.ToLower().Equals("testcategory")).First();
            Car car = bllHandler.GetCar("TST123");
            
            //Test and Assert
            if (success != -1 && carCategory != null && car != null)
            {
                Booking booking = new Booking(car, new DateTime(2021, 03, 13, 13, 00, 00), new DateTime(1985, 01, 23));
                int bookingNr;
                bool result = bllHandler.CreateBooking(booking, out bookingNr);               
                Assert.IsTrue(result);
                Assert.IsTrue(bookingNr > 0);

                //Comment out this line to preserve data, or add breakpoint to check DB before deleting.
                RemoveTestData(carCategory, car,booking);
            }
            else
                Assert.Fail();
        }

        [TestMethod]
        public void CreateBooking_CarStatusIsRented_Returnsfalse()
        {            
            //Prepare test data
            int success = CreateTestData();
            CarCategory carCategory = bllHandler.GetAllCarCategories().Where(x => x.Category.ToLower().Equals("testcategory")).First();
            Car car = bllHandler.GetCar("TST123");

            //Test and Assert
            if (success != -1 && carCategory != null && car != null)
            {
                Booking booking = new Booking(car, new DateTime(2021, 03, 13, 13, 00, 00), new DateTime(1985, 01, 23));
                int bookingNr;
                //Create a fresh new booking
                bllHandler.CreateBooking(booking, out bookingNr);
                //Create another booking with same data (Except that car is already rented in database now)
                bool result = bllHandler.CreateBooking(booking, out bookingNr);

                Assert.IsFalse(result);

                //Comment out this line to preserve data, or add breakpoint to check DB before deleting.
                RemoveTestData(carCategory, car, booking);
            }
            else
                Assert.Fail();
        }

        [TestMethod]
        public void EndBooking_EndBookingCreatedWithTestData_ReturnsTrueWithPrice()
        {
            int success = CreateTestData();
            CarCategory carCategory = bllHandler.GetAllCarCategories().Where(x => x.Category.ToLower().Equals("testcategory")).First();
            Car car = bllHandler.GetCar("TST123");

            if (success!= -1 && carCategory != null && car != null)
            {
                Booking booking = new Booking(car, new DateTime(2021, 03, 13, 13, 00, 00), new DateTime(1985, 01, 23));
                int bookingNr;
                bllHandler.CreateBooking(booking, out bookingNr);
                double price;
                Console.WriteLine("Car rent status before ending booking " + car.IsRented);

                //Test data: milageOnRentEnd = 100 ; 
                bool result = bllHandler.EndBooking(bookingNr, new DateTime(2021, 03, 14,13, 00, 00), 100, out price);
                Assert.IsTrue(result);
                Assert.IsTrue(price > 0);
                Assert.IsFalse(car.IsRented);
                Assert.IsFalse(booking.IsActive);
                Assert.AreEqual(100, car.Milage);
                
                //Comment out this line to preserve data, or add breakpoint to check DB before deleting.
                RemoveTestData(carCategory, car, booking);
            }
            else
                Assert.Fail();
        }

        [TestMethod]
        public void CreateAndEndMultipleBookingsOneAfterAnother_TwoBookingsWithTestData_ReturnTrueWithPrice()
        {
             int success = CreateTestData();
            CarCategory carCategory = bllHandler.GetAllCarCategories().Where(x => x.Category.ToLower().Equals("testcategory")).First();
            Car car = bllHandler.GetCar("TST123");

            if (success != -1 && carCategory != null && car != null)
            {
                Booking booking = new Booking(car, new DateTime(2021, 03, 13, 13, 00, 00), new DateTime(1985, 01, 23));
                int bookingNr;
                bllHandler.CreateBooking(booking, out bookingNr);
                double price;

                //Test data: milageOnRentEnd = 100 ; 
                bool result = bllHandler.EndBooking(bookingNr, new DateTime(2021, 03, 14, 13, 00, 00), 100, out price);
                Assert.IsTrue(result);
                Assert.IsTrue(price > 0);
                Assert.IsFalse(car.IsRented);
                Assert.IsFalse(booking.IsActive);
                Assert.AreEqual(100, car.Milage);

                //Add another booking
                Booking newBooking = new Booking(car, new DateTime(2021, 03, 13, 13, 00, 00), new DateTime(1985, 01, 23));
                int newBookingNr;
                bllHandler.CreateBooking(newBooking, out newBookingNr);
                Assert.IsTrue(newBooking.IsActive);
                Assert.IsTrue(newBooking.RentedCar.IsRented);

                result = bllHandler.EndBooking(newBookingNr, new DateTime(2021, 03, 14, 13, 00, 00), 200, out price);
                Assert.IsTrue(result);
                Assert.IsTrue(price > 0);
                Assert.IsFalse(car.IsRented);
                Assert.IsFalse(booking.IsActive);
                Assert.AreEqual(200, car.Milage);

                //Comment out this line to preserve data, or add breakpoint to check DB before deleting.
                bllHandler.RemoveBooking(newBooking);
                RemoveTestData(carCategory, car, booking);
            }
            else
                Assert.Fail();
        }
        
        //return 0 for success, 1 for already exist, -1 on failure
        private int CreateTestData()
        {
            //Test Data
            CarCategory testCategory = new CarCategory("testCategory", 1, 0);
            Car testCar = new Car("TST123", testCategory, 0);

            //Check if test data already exists in DB
            bool testCategoryExists = (bllHandler.GetAllCarCategories().Where(x => x.Category.ToLower().Equals(testCategory.Category.ToLower())).ToList().Count >0);
            bool testCarExists = bllHandler.GetCar(testCar.RegNr) != null;

            if (testCategoryExists || testCarExists)
                return 1;
            try
            {
                if (bllHandler.CreateCarCategory(testCategory) && bllHandler.CreateCar(testCar))
                    return 0;
                else return -1;
            }
            catch
            {
                return -1;
            }
        }

        private void RemoveTestData(CarCategory testCarCategory, Car testCar, Booking booking)
        {
            bllHandler.RemoveBooking(booking);
            bllHandler.RemoveCar(testCar);
            bllHandler.RemoveCarCategory(testCarCategory);
        }
    }
}
