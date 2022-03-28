using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Travel.Models
{
  public class Review
  {
    public int ReviewId { get; set; }
    public int DestinationId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public double Rating { get; set; }
    public virtual Destination Destination { get; set; }
  }
}