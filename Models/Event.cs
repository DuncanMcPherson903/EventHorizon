using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EventHorizon.Models;

public class Event
{
  public int Id { get; set; }
  [Required]
  [MaxLength(100)]
  public string Name { get; set; }
  [MaxLength(500)]
  public string Description { get; set; }
  [Required]
  public DateTime DateTime { get; set; }
  [Required]
  [Range(30, 50000)]
  public int MaxAttendees { get; set; }
  public int VenueId { get; set; }
  public Venue Venue { get; set; }
  public int EventCategoryId { get; set; }
  public EventCategory EventCategory { get; set; }
  public List<Registration> Registrations { get; set; }
  [Required]
  public bool IsActive { get; set; }

  public Event()
  {
    this.IsActive = true;
  }
}
