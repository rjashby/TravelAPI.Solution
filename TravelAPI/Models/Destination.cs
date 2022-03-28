using System.ComponentModel.DataAnnotations;

namespace TravelAPI.Models
{
  public class Destination
  {
    public int DestinationId { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Review { get; set; }
    public double Rating { get; set; }
  }
}