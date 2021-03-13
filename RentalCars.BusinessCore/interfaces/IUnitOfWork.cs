using System;

namespace RentalCars.BusinessCore.interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICarRepository Cars { get; }

        ICarCategoriesRepository CarCategories { get; }

        IBookingRepository Bookings { get; }

        int Complete();

    }
}
