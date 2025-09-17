using EventHorizon.Models;
namespace EventHorizon.DTO;

public class EventDto
{
  public int Id { get; set; }
  public required string Name { get; set; }
  public string Description { get; set; }
  public DateTime DateTime { get; set; }
  public string VenueName { get; set; }
  public string CategoryName { get; set; }
  public bool IsActive { get; set; }
}

public class EventDetailDto
{
  public string Name { get; set; }
  public string Description { get; set; }
  public DateTime DateTime { get; set; }
  public string VenueName { get; set; }
  public string VenueLocation { get; set; }
  public string VenueDescription { get; set; }
  public string CategoryName { get; set; }
  public bool IsActive { get; set; }
}

public class CreateEventDto
{
  public string Name { get; set; }
  public string Description { get; set; }
  public DateTime DateTime { get; set; }
  public Venue Venue { get; set; }
  public EventCategory EventCategory { get; set; }
}

public class UpdateEventDto
{
  public string Name { get; set; }
  public string Description { get; set; }
  public DateTime DateTime { get; set; }
  public Venue Venue { get; set; }
  public EventCategory EventCategory { get; set; }
  public bool IsActive { get; set; }
}
