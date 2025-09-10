using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EventHorizon.Models;

public class Registration
{
  public int Id { get; set; }
  [Required]
  public int EventId { get; set; }
  [Required]
  public int UserProfileId { get; set; }
  [Required]
  public DateTime RegistrationDate { get; set; }
  [Required]
  [Range(30, 500)]
  public int AttendeeCount { get; set; }
  public string Notes { get; set; }

  // Navigation Properties
  public UserProfile UserProfile { get; set; }
  public Event Event { get; set; }
}
