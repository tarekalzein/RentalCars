using RentalCars.BLL;
using RentalCars.BusinessCore.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RentalCars
{
    /// <summary>
    /// Interaction logic for BookingWindow.xaml
    /// </summary>
    public partial class BookingWindow : Window
    {
        BLLHandler Handler;
        Booking Booking;
        public BookingWindow(BLLHandler handler)
        {
            Handler = handler;
            InitializeComponent();


            //GUI Initialization

            endDateTimeLabel.IsEnabled = false;
            endDateDatePicker.IsEnabled = false;
            endHourCbox.IsEnabled = false;
            endMinuteCbox.IsEnabled = false;
            mileageTxtBox.IsEnabled = false;

            ActionButton.Click += createBookingButton_click;
            ActionButton.Content = "Create";

            createComboBoxItems(startHourCbox, startMinuteCbox);
        }

        public BookingWindow(BLLHandler handler, Booking booking)
        {
            InitializeComponent();
            Handler = handler;
            Booking = booking;

            //GUI Initialization
            createComboBoxItems(endHourCbox, endMinuteCbox);

            ActionButton.Click += endBookingButton_click;
            ActionButton.Content = "End booking";
            CarRegNrTxtBox.IsReadOnly = true;
            searchButton.Visibility = Visibility.Hidden;
            customerDatebirthDatepicker.IsEnabled = false;
            startDateDatePicker.IsEnabled = false;
            startHourCbox.IsEditable = false;
            startMinuteCbox.IsEditable = false;

            bookingNrLabel.Content = booking.BookingNr;
            CarRegNrTxtBox.Text = booking.RentedCar.RegNr;
            carCategoryLabel.Content = booking.RentedCar.Category.Category;
            mileageLabel.Content = booking.RentedCar.Milage;
            customerDatebirthDatepicker.SelectedDate = booking.CustomerBirthdate;
            startDateDatePicker.SelectedDate = booking.RentalDateTime.Date;
            startHourCbox.Items.Add(booking.RentalDateTime.Hour.ToString("00"));
            startHourCbox.SelectedIndex = 0;
            startMinuteCbox.Items.Add(booking.RentalDateTime.Minute.ToString("00"));
            startMinuteCbox.SelectedIndex = 0;

        }

        private void endBookingButton_click(object sender, RoutedEventArgs e)
        {
            string err = "";
            if (endDateDatePicker.SelectedDate.HasValue)
            {
                if (endHourCbox.SelectedItem != null)
                {
                    if (endMinuteCbox.SelectedItem != null)
                    {
                        if (String.IsNullOrEmpty(mileageTxtBox.Text))
                        {
                            err = "Please add mileage";
                        }
                                          
                    }
                    else
                    {
                        err = "Please Select minutes";
                    }
                    
                }
                else
                {
                    err = "Please Select hour";
                }                
            }
            else
            {
                err = "Please Select an end date";
            }

            if (String.IsNullOrEmpty(err))
            {
                double price;
                DateTime endRentDateTime = endDateDatePicker.SelectedDate.Value.Date;
                endRentDateTime.AddHours(endHourCbox.SelectedIndex);
                endRentDateTime.AddMinutes(endMinuteCbox.SelectedIndex);
                int mileageOnRentEnd = int.Parse(mileageTxtBox.Text);

                if (Booking.RentalDateTime > endRentDateTime)
                {
                    MessageBox.Show("Rent end date can't be before rental date");
                    return;
                }
                if (Booking.RentedCar.Milage > mileageOnRentEnd)
                {
                    MessageBox.Show("Mileage on rent end can't be smaller than mileage before rent");
                    return;
                }
                //bool result = Handler.EndBooking(Booking.BookingNr, endRentDateTime, mileageOnRentEnd, out price);
                //MessageBox.Show($"Booking nr. {Booking.BookingNr} has ended. \n Tota price= {price}");

                MessageBox.Show("ENDED");
            }
            else
            {
                MessageBox.Show(err);

            }
        }

        private void createBookingButton_click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Create booking button clicked");

        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void createComboBoxItems(ComboBox hours, ComboBox minutes)
        {
            for (int i = 0; i < 24; i++)
            {
                hours.Items.Add(i.ToString("00"));
            }
            for (int i = 0; i < 60; i++)
            {
                minutes.Items.Add(i.ToString("00"));
            }
        }

        private void mileageTxtBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);

        }
    }
}
