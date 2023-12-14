using CarDbLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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

namespace CarPoolManager
{
    /// <summary>
    /// Interaction logic for CarDetails.xaml
    /// </summary>
    public partial class CarDetails : Window
    {

        public Car Car { get; set; }
        public CarDetails()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public void FillGrdView()
        {
            if (Car == null) return;

            List<Object> list = new List<Object>();

            var carDetails = new
            {
                Id = Car.Id,
                Make = Car.Make,
                Model = Car.Model,
                YearOfManufacture = Car.YearOfManufacture,
                Color = Car.Color,
                NumberPlate = Car.NumberPlate,
                Milage = Car.Milage,
            };

            list.Add(carDetails);
            grdCarDetails.ItemsSource = list;
            //grdCarDetails.Items.Add(carDetails);
        }

        public void FillLstBox()
        {
            lstBookings.ItemsSource = Car.Bookings.ToList();
        }
    }
}
