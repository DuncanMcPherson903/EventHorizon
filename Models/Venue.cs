using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EventHorizon.Models;

public class Venue
{
  public int Id { get; set; }
  [Required]
  [MaxLength(100)]
  public string Name { get; set; }
  [Required]
  public string Address { get; set; }
  [Required]
  public string City { get; set; }
  [Required]
  [MaxLength(2)]
  public string State { get; set; }
  [Required]
  [MaxLength(5)]
  public string ZipCode { get; set; }
  [Required]
  public int MaxCapacity { get; set; }
  [MaxLength(500)]
  public string Description { get; set; }
  [Required]
  public bool IsActive { get; set; }

  public Venue()
  {
    this.IsActive = true;
  }
}
