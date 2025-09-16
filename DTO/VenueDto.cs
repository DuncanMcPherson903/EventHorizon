using EventHorizon.Models;
namespace EventHorizon.DTO;

public class VenueDto
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string Address { get; set; }
  public string City { get; set; }
  public string State { get; set; }
  public string ZipCode { get; set; }
  public int MaxCapacity { get; set; }
  public string Description { get; set; }
  public bool IsActive { get; set; }
}

public class VenueUpdateDto
{
  public string Name { get; set; }
  public string Address { get; set; }
  public string City { get; set; }
  public string State { get; set; }
  public string ZipCode { get; set; }
  public int MaxCapacity { get; set; }
  public string Description { get; set; }
  public bool IsActive { get; set; }
}
