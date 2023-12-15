namespace CarDbLib
{
    public class Car
    {
        public int Id { get; set; }
        public required string Make { get; set; } = "";
        public required string Model { get; set; } = "";
        public string YearOfManufacture { get; set; } = "";
        public string Color { get; set; } = "";
        public string NumberPlate { get; set; } = "";
        public int Milage { get; set; }
        public List<Booking> Bookings { get; set; } = [];


        public override string ToString()
        {
            return $"{Make} {Model} {YearOfManufacture} {NumberPlate}";
        }
    }
}
