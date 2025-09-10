using EventHorizon.Models;
namespace EventHorizon.DTO;

public class EventDto
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  public DateTime DateTime { get; set; }
  public bool IsActive { get; set; }
}

public class EventDetailDto
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  public DateTime DateTime { get; set; }
  public Venue Venue { get; set; }
  public EventCategory EventCategory { get; set; }
  public bool IsActive { get; set; }
}
