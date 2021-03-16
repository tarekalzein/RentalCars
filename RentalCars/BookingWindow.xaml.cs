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
        Car car;
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
                endRentDateTime.AddHours(double.Parse(endHourCbox.Text));
                endRentDateTime.AddMinutes(double.Parse(endMinuteCbox.Text));
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
                bool result = Handler.EndBooking(Booking.BookingNr, endRentDateTime, mileageOnRentEnd, out price);
                MessageBox.Show($"Booking nr. {Booking.BookingNr} has ended. \n Tota price= {price}");
            }
            else
            {
                MessageBox.Show(err);

            }
        }

        private void createBookingButton_click(object sender, RoutedEventArgs e)
        {
            string err = "";
            if (car != null)
            {               
                if(startDateDatePicker.SelectedDate.HasValue)
                {
                    if(startHourCbox.SelectedItem != null && startMinuteCbox.SelectedItem != null)
                    {
                        if (customerDatebirthDatepicker.SelectedDate == null)
                            err = "Please select customer birthdate";
                    }
                    else
                    {
                        err = "Please select start time for this rental";
                    }
                }
                else
                {
                    err = "Please select a start date";
                }                
            }
            else
            {
                err="Can't get this car";
            }
            if (String.IsNullOrEmpty(err))
            {
                DateTime startRentDateTime = startDateDatePicker.SelectedDate.Value.Date;
                startRentDateTime.AddHours(double.Parse(startHourCbox.Text));
                startRentDateTime.AddMinutes(double.Parse(startMinuteCbox.Text));
                Booking booking = new Booking(car, startRentDateTime, customerDatebirthDatepicker.SelectedDate.Value.Date);
                int bookingNr;
                bool result = Handler.CreateBooking(booking, out bookingNr);
                if (result)
                {
                    bookingNrLabel.Content = bookingNr.ToString();
                    MessageBox.Show($"Booking created successfully. Booking Number is: {bookingNr}");
                }
                else
                    MessageBox.Show("Error creating the booking. Check with System administrator");
            }
            else
            {
                MessageBox.Show(err);
            }

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

        private void CarRegNrTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Return)
            {
                SearchCar();
            }
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchCar();
        }

        private void SearchCar()
        {
            if (!String.IsNullOrEmpty(CarRegNrTxtBox.Text))
            {
               Car c = Handler.FindCarByRegNr(CarRegNrTxtBox.Text);
                if (c != null)
                {
                    
                    if (!c.IsRented)
                    {
                        carRegNrLabel.Content = c.RegNr;
                        carCategoryLabel.Content = c.Category.Category;
                        mileageLabel.Content = c.Milage;
                        car = c;
                    }
                    else
                    {
                        MessageBox.Show("The Selected car is not available");
                    }
                }
                else
                {
                    MessageBox.Show("Car not found");
                    car = null;
                }
            }
        }
    }
}
