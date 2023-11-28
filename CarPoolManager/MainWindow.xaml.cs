using System.IO;
using System.Windows;
using System.Windows.Controls;
using CarDbLib;
namespace CarPoolManager;

public partial class MainWindow : Window
{
    CarContext _cars = new CarContext();
    public List<Car> Cars { get; set; } = new();
    public MainWindow() => InitializeComponent();

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        cbbMakes.ItemsSource = _cars.Cars.Select(x => x.Make).ToList();
        cbbMakes.SelectedIndex = 0;
    }

    public void SeedDatabase()
    {
        _cars.Cars.Clear();
        var lines = File.ReadAllLines("cars.csv").Skip(1);
        Car car;
        foreach (var line in lines)
        {
            var values = line.Split(',');
            car = new Car()
            {
                Id = int.Parse(values[0]),
                Make = values[1],
                Model = values[2],
                YearOfManufacture = values[3],
                Color = values[4],
                NumberPlate = values[5],
                Milage = int.Parse(values[6])
            };
            Cars.Add(car);
        }
        _cars.Cars.AddRange(Cars);
    }

    private void txtId_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (txtId.Text.Trim() == "") return;
        dgrCars.ItemsSource = _cars.Cars.Where(x => x.Id == int.Parse(txtId.Text)).ToList();
    }

    private void cbbMakes_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        cbbModel.ItemsSource = _cars.Cars.Where(x => x.Make == cbbMakes.SelectedItem).Select(x => x.Model).ToList();
        cbbModel.SelectedIndex = 0;
        dgrCars.ItemsSource = _cars.Cars.Where(x => x.Make == cbbMakes.SelectedItem).ToList();
    }

    private void cbbModel_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        dgrCars.ItemsSource = _cars.Cars.Where(x => x.Make == cbbMakes.SelectedItem).Where(x => x.Model == cbbModel.SelectedItem).ToList();
    }

    private void txtVIN_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (txtNumberPlate.Text.Trim() == "") return;
        dgrCars.ItemsSource = _cars.Cars.Where(x => x.NumberPlate == txtNumberPlate.Text).ToList();
    }

    private void sldMaxMilage_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (_cars == null) return;
        dgrCars.ItemsSource = _cars.Cars.Where(x => x.Milage <= sldMaxMilage.Value).ToList();
    }
}