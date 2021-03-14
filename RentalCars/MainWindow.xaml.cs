using RentalCars.BLL;
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
        BLLHandler handler;

        public MainWindow()
        {
            InitializeComponent();
            handler = new BLLHandler();
            CarCategory test = new CarCategory("Premium", 1, 1);
            if (handler.CreateCarCategory(test))
                MessageBox.Show("Done");
            else
                MessageBox.Show("FAILED");
        }

        private void NewBooking_ButtonClick(object sender, RoutedEventArgs e)
        {
            BookingWindow bookingWindow = new BookingWindow();
            bookingWindow.Show();
        }
    }
}
