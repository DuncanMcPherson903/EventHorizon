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
      CreateMap<Event, EventDto>()
        .ForMember(dest => dest.VenueName, opt =>
          opt.MapFrom(src => src.Venue.Name))
        .ForMember(dest => dest.CategoryName, opt =>
          opt.MapFrom(src => src.EventCategory.Name));

      // Map from Event to EventDto
      CreateMap<Event, EventDetailDto>()
        .ForMember(dest => dest.VenueName, opt =>
          opt.MapFrom(src => src.Venue.Name))
        .ForMember(dest => dest.VenueLocation, opt =>
          opt.MapFrom(src => $"{src.Venue.Address}, {src.Venue.City}, {src.Venue.State}, {src.Venue.ZipCode}"))
        .ForMember(dest => dest.CategoryName, opt =>
          opt.MapFrom(src => src.EventCategory.Name));

      // Map from Event to EventDto
      CreateMap<Event, CreateEventDto>();

      // Map from Event to EventDto
      CreateMap<Event, UpdateEventDto>();

    }
  }
}
