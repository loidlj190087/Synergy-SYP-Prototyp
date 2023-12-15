namespace CarPoolManager;

public partial class MainWindow : Window
{
    private readonly DatabaseContext _db = new();
    public MainWindow() => InitializeComponent();

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        _db.Database.EnsureCreated();
        cboMakes.ItemsSource = _db.Cars.Select(x => x.Make).Distinct().OrderBy(x => x).ToList();
        lstCars.ItemsSource = _db.Cars.OrderBy(x => x.Id).ToList();
    }

    private void FilterChanged()
    {
        if (_db == null || lstCars == null) return;
        if (txtNumberPlate.Text.Trim().Length > 0 && cboMakes.SelectedItem != null && cboModels.SelectedItem != null)
        {
            lstCars.ItemsSource = _db.Cars.Where(x => x.Make == cboMakes.SelectedItem!.ToString())
                .Where(x => x.Model == cboModels.SelectedItem!.ToString())
                .Where(x => x.NumberPlate.Contains(txtNumberPlate.Text.Trim().ToLower()))
                .Where(x => x.Milage <= sldMaxMilage.Value)
                .OrderBy(x => x).ToList();
        }
        else if (cboMakes.SelectedItem != null && cboModels.SelectedItem != null)
        {
            lstCars.ItemsSource = _db.Cars.Where(x => x.Make == cboMakes.SelectedItem!.ToString())
                 .Where(x => x.Model == cboModels.SelectedItem!.ToString())
                 .Where(x => x.Milage <= sldMaxMilage.Value)
                 .OrderBy(x => x).ToList();
        }
    }

    private void cboMakes_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        cboModels.ItemsSource = _db.Cars.Where(x => x.Make == cboMakes.SelectedItem!.ToString()).Select(x => x.Model).Distinct().OrderBy(x => x).ToList();
        cboModels.SelectedIndex = 0;
        FilterChanged();
    }

    private void cboModels_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) => FilterChanged();

    private void lstCars_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        CarDetails carDetailsWindow = new();

        carDetailsWindow.Car = (Car)lstCars.SelectedItem;
        carDetailsWindow.FillGrdView();
        carDetailsWindow.FillLstBox();

        carDetailsWindow.Show();
    }

    private void txtNumberPlate_KeyUp(object sender, System.Windows.Input.KeyEventArgs e) => FilterChanged();
    private void sldMaxMilage_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) => FilterChanged();

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
        Window window2 = new();
        window2.Show();
    }

    private void btnAddNewCarOnclick(object sender, RoutedEventArgs e)
    {
        CreateCarWindow createCarWindow = new();
        var dialogResult = createCarWindow.ShowDialog();
        if (createCarWindow.IsAdded)
        {
            cboMakes.ItemsSource = _db.Cars.Select(x => x.Make).Distinct().OrderBy(x => x).ToList();
            cboMakes.SelectedIndex = 0;
            FilterChanged();
        }
    }
}