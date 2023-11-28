namespace CarDbLib;

public class CarContext
{
    public List<Car> Cars { get; set; } = new();
    public CarContext()
    {
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
    }
}
