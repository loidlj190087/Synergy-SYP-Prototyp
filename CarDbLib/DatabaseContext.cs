using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CarDbLib;

public class DatabaseContext : DbContext
{

    public DatabaseContext(DbContextOptions<DatabaseContext> options):base(options)
    {
        
    }

    public DatabaseContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
        {
            string connectionString = @"server=(LocalDB)\mssqllocaldb;attachdbfilename=C:\Users\User\Documents\Schule\SYP\4C\Synergy-SYP-Prototyp\CarBooking.mdf;database=CarBookingDb;integrated security=True;MultipleActiveResultSets=True;";

            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    public DbSet<Car> Cars { get; set; }
    public DbSet<Booking> Bookings { get; set; }
}
