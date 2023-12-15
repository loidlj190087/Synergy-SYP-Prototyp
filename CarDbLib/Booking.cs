namespace CarDbLib;

public class Booking
{
    public required int Id { get; set; }
    public required int CarId { get; set; }
    public required DateTime StartDate{ get; set; }
    public required DateTime EndDate { get; set; }
    public required string Description { get; set; } = "";
    public virtual Car Car { get; set; }

    public override string? ToString()
    {
        return $"{Description}  von {StartDate} - {EndDate}";
    }
}
