using Microsoft.EntityFrameworkCore;

namespace TravelAPI.Models
{
  public class TravelAPIContext : DbContext
  {
    public TravelAPIContext(DbContextOptions<TravelAPIContext> options)
      : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<Destination>()
      .HasData(
        new Destination { DestinationId = 1, Country = "USA", City = "Portland", Rating = 0, Review = "its portland its got the ports" },
        new Destination { DestinationId = 2, Country = "Canada", City = "Toronot", Rating = 10, Review = "maple syrup eh?" },
        new Destination { DestinationId = 3, Country = "France", City = "Paris", Rating = 2, Review = "baguette " },
        new Destination { DestinationId = 4, Country = "Netherlands", City = "Amsterdam", Rating = 4.7, Review = "The original Las Vegas" },
        new Destination { DestinationId = 5, Country = "United Kingdom", City = "London", Rating = 4.5, Review = "This place is a total place to see." }
      );
    }

    public DbSet<Destination> Destinations { get; set; }
  }
}