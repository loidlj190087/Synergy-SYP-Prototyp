namespace CarPoolManager;

public partial class CreateCarWindow : Window
{
    public event EventHandler? CarCreated;
    public bool IsAdded { get; set; }
    private readonly DatabaseContext _db = new ();
    public CreateCarWindow() => InitializeComponent();
    private void Window_Loaded(object sender, RoutedEventArgs e) => _db.Database.EnsureCreated();
    private void BtnAddVehiclesOnClick(object sender, RoutedEventArgs e)
    {
        var newCar = new Car()
        {
            Make = txtMake.Text,
            Model = txtModel.Text,
            YearOfManufacture = txtYearOfManufacture.Text,
            Color = txtColor.Text,
            NumberPlate = txtNumberPlate.Text,
            Milage = int.Parse(txtMilage.Text),
        };

        _db.Cars.Add(newCar);
        _db.SaveChanges();

        IsAdded = true;
        this.Close();
    }
}