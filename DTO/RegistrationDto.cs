using EventHorizon.Models;
namespace EventHorizon.DTO;

public class RegistrationDto
{
  public int Id { get; set; }
  public int EventId { get; set; }
  public int UserProfileId { get; set; }
  public DateTime RegistrationDate { get; set; }
  public int AttendeeCount { get; set; }
  public string Notes { get; set; }
}
