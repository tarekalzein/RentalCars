using RentalCars.BLL;
using RentalCars.BusinessCore.models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RentalCars
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BLLHandler handler;

        public MainWindow()
        {
            InitializeComponent();
           

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            handler = new BLLHandler();
            ActiveRentals_datagrid.ItemsSource = handler.GetActiveBookings();
            
            //Dummy dummy = new Dummy();
            //ActiveRentals_datagrid.ItemsSource = dummy.GetDummyBookings();
        }

        private void NewBooking_ButtonClick(object sender, RoutedEventArgs e)
        {
            BookingWindow bookingWindow = new BookingWindow(handler);
            bookingWindow.Show();
        }

        private void ActiveRentals_datagrid_SelectionChanged()
        {

        }

        private void ActiveRentals_datagrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            if(  (dg.SelectedItem as Booking ) != null)
            {
                BookingWindow booking = new BookingWindow(handler, dg.SelectedItem as Booking);
                booking.Show();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            //Dispose the DB connection.
            handler.Dispose();
        }

        private void CategoriesButton_Click(object sender, RoutedEventArgs e)
        {
            CarCategoriesWindow carCategoriesWindow = new CarCategoriesWindow(handler);
            carCategoriesWindow.Show();
        }

        private void CarManagerButton_Click(object sender, RoutedEventArgs e)
        {
            CarManagerWindow carManager = new CarManagerWindow(handler);
            carManager.Show();
        }

        private void AllBookingsButton_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
