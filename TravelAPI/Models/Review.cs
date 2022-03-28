using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelAPI.Models
{
  public class Review
  {
    public int ReviewId { get; set; }
    public int DestinationId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public virtual Destination Destination { get; set; }
  }
}