using AutoMapper;
using EventHorizon.Models;
using EventHorizon.DTO;

namespace EventHorizon.Mapping
{
  public class EventCategoryMappingProfile : Profile
  {
    public EventCategoryMappingProfile()
    {
      // Map from Event to EventDto
      CreateMap<EventCategory, EventCategoryDto>();

      CreateMap<EventCategoryDto, EventCategory>();
    }
  }
}
