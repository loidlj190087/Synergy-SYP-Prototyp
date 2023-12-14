using System.IO;
using System.Windows;
using CarDbLib;
namespace CarPoolManager;

public partial class MainWindow : Window
{
    DatabaseContext _db;
    public MainWindow() => InitializeComponent();

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        _db = new DatabaseContext();
        //_db.Database.EnsureDeleted();
        _db.Database.EnsureCreated();
        cboMakes.ItemsSource = _db.Cars.Select(x => x.Make).Distinct().OrderBy(x => x).ToList();
        cboMakes.SelectedIndex = 0;
    }


    #region SeedDatabase
    private void ReadCSV_Cars()
    {
        var lines = File.ReadAllLines(@".\csv\cars.csv").Skip(1);

        foreach (var line in lines)
        {
            var parts = line.Split(',');

            var car = new Car()
            {
                Make = parts[1],
                Model = parts[2],
                YearOfManufacture = parts[3],
                Color = parts[4],
                NumberPlate = parts[5],
                Milage = int.Parse(parts[6]),
            };

            _db.Cars.Add(car);
            _db.SaveChanges();
        }
    }

    private void ReadCSV_Bookings()
    {
        var lines = File.ReadAllLines(@".\csv\bookings.csv").Skip(1);

        foreach (var line in lines)
        {
            var parts = line.Split(",");


            var booking = new Booking()
            {
                StartDate = DateTime.ParseExact(parts[0].Trim(), "M/d/yyyy", null),
                EndDate = DateTime.ParseExact(parts[1].Trim(), "M/d/yyyy", null),
                Description = parts[2],
                CarId = int.Parse(parts[3]),
            };

            _db.Bookings.Add(booking); _db.SaveChanges();
        }

    }
    #endregion

    private void cboMakes_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        cboModels.ItemsSource = _db.Cars.Where(x => x.Make == cboMakes.SelectedItem).Select(x => x.Model).Distinct().OrderBy(x => x).ToList();
        cboModels.SelectedIndex = 0;
        if (txtNumberPlate.Text.Trim().Length > 0)
        {
            lstCars.ItemsSource = _db.Cars.Where(x => x.Make == cboMakes.SelectedItem)
                .Where(x => x.Model == cboModels.SelectedItem)
                .Where(x => x.NumberPlate.Contains(txtNumberPlate.Text.Trim()))
                .Distinct().OrderBy(x => x).ToList();
        }
        else
        {
            lstCars.ItemsSource = _db.Cars.Where(x => x.Make == cboMakes.SelectedItem)
                    .Where(x => x.Model == cboModels.SelectedItem)
                    .Distinct().OrderBy(x => x).ToList();
        }
    }

    private void cboModels_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        if (txtNumberPlate.Text.Trim().Length > 0)
        {
            lstCars.ItemsSource = _db.Cars.Where(x => x.Make == cboMakes.SelectedItem)
                .Where(x => x.Model == cboModels.SelectedItem)
                .Where(x => x.NumberPlate.Contains(txtNumberPlate.Text.Trim()))
                .Distinct().OrderBy(x => x).ToList();
        }
        else
        {
            lstCars.ItemsSource = _db.Cars.Where(x => x.Make == cboMakes.SelectedItem)
                    .Where(x => x.Model == cboModels.SelectedItem)
                    .Distinct().OrderBy(x => x).ToList();
        }
    }

    private void lstCars_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        
        //Create  instance of CarDetailes window

        CarDetails carDetailsWindow = new CarDetails();

        Car car = (Car)lstCars.SelectedItem;



       carDetailsWindow.grdCarDetails.Items.Add(new
       {
           Id = car.Id,
           Make = car.Make,
           Model = car.Model,
           YearOfManufacture = car.YearOfManufacture,
           Color = car.Color,
           NumberPlate = car.NumberPlate,
           Milage = car.Milage,
       });

       carDetailsWindow.lstBookings.ItemsSource =
           car.Bookings.ToList();



        carDetailsWindow.Show();
    }

    private void txtVIN_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (txtNumberPlate.Text.Trim().Length > 0)
        {
            lstCars.ItemsSource = _db.Cars.Where(x => x.Make == cboMakes.SelectedItem)
                .Where(x => x.Model == cboModels.SelectedItem)
                .Where(x => x.NumberPlate.Contains(txtNumberPlate.Text.Trim()))
                .Distinct().OrderBy(x => x).ToList();
        }
        else
        {
            lstCars.ItemsSource = _db.Cars.Where(x => x.Make == cboMakes.SelectedItem)
                    .Where(x => x.Model == cboModels.SelectedItem)
                    .Distinct().OrderBy(x => x).ToList();
        }
    }

    private void lstCars_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        if (lstCars.SelectedItem != null)
        {
            btnAddReservation.IsEnabled = true;
        }
        else
        {
            btnAddReservation.IsEnabled = false;
        }
    }

    private void btnAddReservation_Click(object sender, RoutedEventArgs e)
    {
        Window window2 = new Window();
        window2.Show();
    }
}