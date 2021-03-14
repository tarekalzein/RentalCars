using Microsoft.VisualStudio.TestTools.UnitTesting;
using RentalCars.BLL;
using RentalCars.BusinessCore.models;
using System;

namespace RentalCars.UnitTests
{
    [TestClass]
    public class BLLHandlerTestsCalculateRentPrice
    {
        BLLHandler bllHandler = new BLLHandler();

        [TestMethod]
        public void CalculateRentPrice_CarIsCompactLessThanDayRent_ReturnsPrice()
        {
            //Compact Price = baseDayRental * numberOfDays
            //price = 499 * 1;
            DateTime d1 = new DateTime(2021, 03, 20, 13, 00, 00);
            DateTime d2 = new DateTime(2021, 03, 20, 08, 00, 00);
            TimeSpan t = d1 - d2;

            CarCategory category = new CarCategory("Compact_test", 1, 0);
            Assert.AreEqual(499, bllHandler.CalculateRentPrice(t, category, 10));
        }

        [TestMethod]
        public void CalculateRentPrice_CarIsCompactOneDayRent_ReturnsPrice()
        {
            //Compact Price = baseDayRental * numberOfDays
            //price = 499 * 1;
            DateTime d1 = new DateTime(2021, 03, 20, 13, 00, 00);
            DateTime d2 = new DateTime(2021, 03, 19, 13, 00, 00);
            TimeSpan t = d1 - d2;

            CarCategory category = new CarCategory("Compact_test", 1, 0);
            Assert.AreEqual(499,bllHandler.CalculateRentPrice(t, category, 10));
        }
        [TestMethod]
        public void CalculateRentPrice_CarIsCompactOneDayWithFewHoursRent_ReturnsPrice()
        {
            //Compact Price = baseDayRental * numberOfDays
            //price = 499 * 2;
            DateTime d1 = new DateTime(2021, 03, 20, 13, 00, 00);
            DateTime d2 = new DateTime(2021, 03, 19, 11, 00, 00);
            TimeSpan t = d1 - d2;

            CarCategory category = new CarCategory("Compact_test", 1, 0);
            Assert.AreEqual(499*2, bllHandler.CalculateRentPrice(t, category, 10));
        }
        [TestMethod]
        public void CalculateRentPrice_CarIsPremiumOneDayRent_ReturnsPrice()
        {
            //Premium Price = baseDayRental * numberOfDays * 1.2 + kilometerPrice* numberOfKilometers
            double expected = (BLL.Constants.BaseDayRental * 1 * 1.2) + BLL.Constants.KilometerPrice * 10;

            DateTime d1 = new DateTime(2021, 03, 20, 13, 00, 00);
            DateTime d2 = new DateTime(2021, 03, 19, 13, 00, 00);
            TimeSpan t = d1 - d2;

            CarCategory category = new CarCategory("premium_test", 1.2, 1);
            Assert.AreEqual(expected, bllHandler.CalculateRentPrice(t, category, 10));
        }
        [TestMethod]
        public void CalculateRentPrice_CarIsPremiumOneDayWithFewHoursRent_ReturnsPrice()
        {
            //Premium Price = baseDayRental * numberOfDays * 1.2 + kilometerPrice* numberOfKilometers
            double expected = (BLL.Constants.BaseDayRental * 2 * 1.2) + BLL.Constants.KilometerPrice * 10;

            DateTime d1 = new DateTime(2021, 03, 20, 18, 00, 00);
            DateTime d2 = new DateTime(2021, 03, 19, 06, 00, 00);
            TimeSpan t = d1 - d2;

            CarCategory category = new CarCategory("premium_test", 1.2, 1);
            Assert.AreEqual(expected, bllHandler.CalculateRentPrice(t, category, 10));
        }
        [TestMethod]
        public void CalculateRentPrice_CarIsMinivanOneDayRent_ReturnsPrice()
        {
            //Premium Price = baseDayRental * numberOfDays * 1.2 + kilometerPrice* numberOfKilometers
            double expected = (BLL.Constants.BaseDayRental * 1 * 1.7) + BLL.Constants.KilometerPrice * 10 * 1.5;

            DateTime d1 = new DateTime(2021, 03, 20, 13, 00, 00);
            DateTime d2 = new DateTime(2021, 03, 19, 13, 00, 00);
            TimeSpan t = d1 - d2;

            CarCategory category = new CarCategory("Minivan", 1.7, 1.5);
            Assert.AreEqual(expected, bllHandler.CalculateRentPrice(t, category, 10));
        }


    }
}
