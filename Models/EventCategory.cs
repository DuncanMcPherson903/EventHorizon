using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EventHorizon.Models;

public class EventCategory
{
  public int Id { get; set; }
  [Required]
  [MaxLength(100)]
  public string Name { get; set; }
  [MaxLength(500)]
  public string Description { get; set; }
}
