using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EventHorizon.Models;

public class UserProfile
{
  public int Id { get; set; }
  [Required]
  [MaxLength(100)]
  public string FirstName { get; set; }
  [Required]
  [MaxLength(100)]
  public string LastName { get; set; }
  [Required]
  public DateTime JoinDate { get; set; }
  public bool IsActive { get; set; }

  // This connects to the ASP.NET Core Identity user
  public string IdentityUserId { get; set; }
  public IdentityUser IdentityUser { get; set; }

  // Navigation property for registrations
  public List<Registration> Registrations { get; set; }

  public UserProfile()
  {
    this.IsActive = true;
  }
}
