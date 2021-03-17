using RentalCars.BLL;
using RentalCars.BusinessCore.models;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace RentalCars
{
    /// <summary>
    /// Interaction logic for CarCategoriesWindow.xaml
    /// </summary>
    public partial class CarCategoriesWindow : Window
    {
        BLLHandler Handler;
        ObservableCollection<CarCategory> categories;
        double baseMultiplier;
        double kmMultiplier;
        public CarCategoriesWindow(BLLHandler handler)
        {
            Handler = handler;
            InitializeComponent();
            categories = new ObservableCollection<CarCategory>(handler.GetAllCarCategories());
            Categories_datagrid.ItemsSource = categories;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (VerifyInput())
            {
                CarCategory c = new CarCategory(
                        categoryNameTxtbox.Text,
                        baseMultiplier,
                        kmMultiplier);
                if (Handler.CreateCarCategory(c))
                    categories.Add(c);
            }
            else
                MessageBox.Show("Please check your input");
        }

        private bool VerifyInput()
        {
            if (!String.IsNullOrWhiteSpace(categoryNameTxtbox.Text)
                &&
                double.TryParse(baseMultiplierTxtbox.Text, out baseMultiplier)
                &&
                double.TryParse(kmMultiplierTxtbox.Text, out kmMultiplier))
            {
                return true;
            }
            else return false;
        }

    }

}
