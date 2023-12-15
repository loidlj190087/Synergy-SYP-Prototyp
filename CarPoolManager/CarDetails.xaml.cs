namespace CarPoolManager;

public partial class CarDetails : Window
{
    private readonly DatabaseContext _db = new();
    public Car Car { get; set; }
    public CarDetails() => InitializeComponent();
    private void Window_Loaded(object sender, RoutedEventArgs e) => _db.Database.EnsureCreated();

    public void FillGrdView()
    {
        var carDetails = new
        {
            Car.Id,
            Car.Make,
            Car.Model,
            Car.YearOfManufacture,
            Car.Color,
            Car.NumberPlate,
            Car.Milage,
        };
        grdCarDetails.ItemsSource = new List<Object>() { carDetails };
    }

    public void FillLstBox() => lstBookings.ItemsSource = _db.Bookings.Where(x => x.Id == Car.Id).ToList();
}
