using RentalCars.BLL;
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
using System.Windows.Shapes;

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
