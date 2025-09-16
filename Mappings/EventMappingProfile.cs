using AutoMapper;
using EventHorizon.Models;
using EventHorizon.DTO;

namespace EventHorizon.Mapping
{
  public class EventMappingProfile : Profile
  {
    public EventMappingProfile()
    {
      // Map from Event to EventDto
      CreateMap<Event, EventDto>();

      // Map from Event to EventDto
      CreateMap<Event, EventDetailDto>();

      // Map from Event to EventDto
      CreateMap<Event, CreateEventDto>();

      // Map from Event to EventDto
      CreateMap<Event, UpdateEventDto>();

    }
  }
}
