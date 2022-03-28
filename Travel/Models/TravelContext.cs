using Microsoft.EntityFrameworkCore;

namespace Travel.Models
{
  public class TravelContext : DbContext
  {
    public TravelContext(DbContextOptions<TravelContext> options)
      : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<Destination>()
      .HasData(
        new Destination { DestinationId = 1, Country = "USA", City = "Portland"},
        new Destination { DestinationId = 2, Country = "Canada", City = "Toronot"},
        new Destination { DestinationId = 3, Country = "France", City = "Paris"},
        new Destination { DestinationId = 4, Country = "Netherlands", City = "Amsterdam"},
        new Destination { DestinationId = 5, Country = "United Kingdom", City = "London"}
      );
      builder.Entity<Review>()
      .HasData(
        new Review { ReviewId = 1, DestinationId = 1, Title = "My Review", Content = "Def a city", Rating = 6},
        new Review { ReviewId = 2, DestinationId = 1, Title = "Cool review", Content = "very cool", Rating = 9.9},
        new Review { ReviewId = 3, DestinationId = 1, Title = "Cooler review", Content = "even cooler", Rating = 10},
        new Review { ReviewId = 4, DestinationId = 2, Title = "still a review", Content = "yep", Rating = 0},
        new Review { ReviewId = 5, DestinationId = 2, Title = "revier", Content = "its Amsterdam", Rating = 3.5},
        new Review { ReviewId = 6, DestinationId = 3, Title = "United Kingdom", Content = "its London", Rating = 6.9},
        new Review { ReviewId = 7, DestinationId = 4, Title = "My Review", Content = "Def a city", Rating = 6},
        new Review { ReviewId = 8, DestinationId = 5, Title = "My Review again", Content = "Def a city", Rating = 1}
      );
    }

    public DbSet<Destination> Destinations { get; set; }
    
    public DbSet<Review> Reviews { get; set; }
  }
}