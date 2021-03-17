using RentalCars.BLL;
using System.Windows;

namespace RentalCars
{
    /// <summary>
    /// Interaction logic for CarManagerWindow.xaml
    /// </summary>
    public partial class CarManagerWindow : Window
    {
        BLLHandler Handler;
        public CarManagerWindow(BLLHandler handler)
        {
            Handler = handler;
            InitializeComponent();
            CarsDataGrid.ItemsSource = handler.GetAllCars();
        }
    }
}
