using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelAPI.Models
{
  public class Destination
  {
    public int DestinationId { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    // public List<string> Review { get; set; }
    public double Rating { get; set; }
  }
}