using RentalCars.BusinessCore;
using RentalCars.BusinessCore.models;
using RentalCars.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RentalCars
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TestManager manager;
        UnitOfWork unitOfWork = new UnitOfWork(new RentalCarsDbContext());
        public MainWindow()
        {
            InitializeComponent();
            manager = new TestManager();
            //ActiveRentals_datagrid.ItemsSource = unitOfWork.Bookings.GetAll();
            Booking booking = new Booking(unitOfWork.Cars.GetAll().ToList()[0],DateTime.Now,new DateTime(1985,01,23));
            unitOfWork.Bookings.Add(booking);

        }

        private void NewBooking_ButtonClick(object sender, RoutedEventArgs e)
        {
            BookingWindow bookingWindow = new BookingWindow();
            bookingWindow.Show();
        }
    }
}
