using CarDbLib;
using System.IO;
using System.Windows;

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
        cboModels.ItemsSource = _db.Cars.Where(x => x.Make==cboMakes.SelectedItem).Select(x => x.Model).Distinct().OrderBy(x => x).ToList();
        cboModels.SelectedIndex = 0;
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
        if (_db == null) return;
        cboModels.ItemsSource = _db.Cars.Where(x => x.Make == cboMakes.SelectedItem).Select(x => x.Model).Distinct().OrderBy(x => x).ToList();
        cboModels.SelectedIndex = 0;
        if (txtNumberPlate.Text.Trim().Length > 0)
        {
            var cars = _db.Cars.Where(x => x.Make == cboMakes.SelectedItem)
                .Where(x => x.Model == cboModels.SelectedItem)
                .Where(x => x.NumberPlate.Contains(txtNumberPlate.Text.Trim()))
                .Where(x => x.Milage <= sldMaxMilage.Value)
                .Distinct().OrderBy(x => x).ToList();
            if (cars.Count > 0)
            {
                lstCars.ItemsSource = cars;
            }
        }
        else
        {
            var cars = _db.Cars.Where(x => x.Make == cboMakes.SelectedItem)
                .Where(x => x.Model == cboModels.SelectedItem)
                .Where(x => x.Milage <= sldMaxMilage.Value)
                .Distinct().OrderBy(x => x).ToList();
            if (cars.Count > 0)
            {
                lstCars.ItemsSource = cars;
            }
        }
    }

    private void cboModels_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        if (_db == null) return;
        if (txtNumberPlate.Text.Trim().Length > 0)
        {
            var cars = _db.Cars.Where(x => x.Make == cboMakes.SelectedItem)
                .Where(x => x.Model == cboModels.SelectedItem)
                .Where(x => x.NumberPlate.Contains(txtNumberPlate.Text.Trim()))
                .Where(x => x.Milage <= sldMaxMilage.Value)
                .Distinct().OrderBy(x => x).ToList();
            if (cars.Count > 0)
            {
                lstCars.ItemsSource = cars;
            }
        }
        else
        {
            var cars = _db.Cars.Where(x => x.Make == cboMakes.SelectedItem)
                .Where(x => x.Model == cboModels.SelectedItem)
                .Where(x => x.Milage <= sldMaxMilage.Value)
                .Distinct().OrderBy(x => x).ToList();
            if (cars.Count > 0)
            {
                lstCars.ItemsSource = cars;
            }
        }
    }

    private void lstCars_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        CarDetails carDetailsWindow = new();

        carDetailsWindow.Car = (Car)lstCars.SelectedItem;
        carDetailsWindow.FillGrdView();
        carDetailsWindow.FillLstBox();


        carDetailsWindow.Show();
    }

    private void txtNumberPlate_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (_db == null) return;
        if (txtNumberPlate.Text.Trim().Length > 0)
        {
            var cars = _db.Cars.Where(x => x.Make == cboMakes.SelectedItem)
                .Where(x => x.Model == cboModels.SelectedItem)
                .Where(x => x.NumberPlate.Contains(txtNumberPlate.Text.Trim()))
                .Where(x => x.Milage <= sldMaxMilage.Value)
                .Distinct().OrderBy(x => x).ToList();
            if (cars.Count > 0)
            {
                lstCars.ItemsSource = cars;
            }
        }
        else
        {
            var cars = _db.Cars.Where(x => x.Make == cboMakes.SelectedItem)
                .Where(x => x.Model == cboModels.SelectedItem)
                .Where(x => x.Milage <= sldMaxMilage.Value)
                .Distinct().OrderBy(x => x).ToList();
            if (cars.Count > 0)
            {
                lstCars.ItemsSource = cars;
            }
        }
    }
    private void sldsldMaxMilage_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (_db == null) return;
        if (txtNumberPlate.Text.Trim().Length > 0)
        {
            var cars = _db.Cars.Where(x => x.Make == cboMakes.SelectedItem)
                .Where(x => x.Model == cboModels.SelectedItem)
                .Where(x => x.NumberPlate.Contains(txtNumberPlate.Text.Trim()))
                .Where(x => x.Milage <= sldMaxMilage.Value)
                .Distinct().OrderBy(x => x).ToList();
            if (cars.Count > 0)
            {
                lstCars.ItemsSource = cars;
            }
        }
        else
        {
            var cars = _db.Cars.Where(x => x.Make == cboMakes.SelectedItem)
                .Where(x => x.Model == cboModels.SelectedItem)
                .Where(x => x.Milage <= sldMaxMilage.Value)
                .Distinct().OrderBy(x => x).ToList();
            if (cars.Count > 0)
            {
                lstCars.ItemsSource = cars;
            }
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