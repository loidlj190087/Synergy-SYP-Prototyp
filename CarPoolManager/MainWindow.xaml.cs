using System.IO;
using System.Windows;
using CarDbLib;
namespace CarPoolManager;

public partial class MainWindow : Window
{
    DatabaseContext _db;

    public List<Car> Cars { get; set; } = new();
    public MainWindow() => InitializeComponent();

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        _db = new DatabaseContext();
        //_db.Database.EnsureDeleted();
        _db.Database.EnsureCreated();
        cboMakes.ItemsSource = _db.Cars.Select(x => x.Make).OrderBy(x => x).ToList();
        cboMakes.SelectedIndex = 0;
        FillGrdWithCars();
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

        foreach(var line in lines)
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

    public void FillGrdWithCars()
    {
        
    }

    //public void FillGrdWithCars()
    //{

    //    var numberplates = _db.Cars.Select(x => x.NumberPlate).ToList();



    //    foreach(var number in numberplates)
    //    {
    //        grdCars.ItemsSource = _db.Bookings.
    //      Where(x => x.Car.NumberPlate == number).ToList();
    //    }


      

    //}


    //public void SeedDatabase()
    //{
    //    _cars.Cars.Clear();
    //    var lines = File.ReadAllLines("cars.csv").Skip(1);
    //    Car car;
    //    foreach (var line in lines)
    //    {
    //        var values = line.Split(',');
    //        car = new Car()
    //        {
    //            Id = int.Parse(values[0]),
    //            Make = values[1],
    //            Model = values[2],
    //            YearOfManufacture = values[3],
    //            Color = values[4],
    //            NumberPlate = values[5],
    //            Milage = int.Parse(values[6])
    //        };
    //        Cars.Add(car);
    //    }
    //    _cars.Cars.AddRange(Cars);
    //}

    //private void txtId_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
    //{
    //    if (txtId.Text.Trim() == "") return;
    //    dgrCars.ItemsSource = _cars.Cars.Where(x => x.Id == int.Parse(txtId.Text)).ToList();
    //}

    //private void cbbMakes_SelectionChanged(object sender, SelectionChangedEventArgs e)
    //{
    //    cbbModel.ItemsSource = _cars.Cars.Where(x => x.Make == cbbMakes.SelectedItem).Select(x => x.Model).ToList();
    //    cbbModel.SelectedIndex = 0;
    //    dgrCars.ItemsSource = _cars.Cars.Where(x => x.Make == cbbMakes.SelectedItem).ToList();
    //}

    //private void cbbModel_SelectionChanged(object sender, SelectionChangedEventArgs e)
    //{
    //    dgrCars.ItemsSource = _cars.Cars.Where(x => x.Make == cbbMakes.SelectedItem).Where(x => x.Model == cbbModel.SelectedItem).ToList();
    //}

    //private void txtVIN_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
    //{
    //    if (txtNumberPlate.Text.Trim() == "") return;
    //    dgrCars.ItemsSource = _cars.Cars.Where(x => x.NumberPlate == txtNumberPlate.Text).ToList();
    //}

    //private void sldMaxMilage_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    //{
    //    //if (_cars == null) return;
    //    //dgrCars.ItemsSource = _cars.Cars.Where(x => x.Milage <= sldMaxMilage.Value).ToList();
    //}
}