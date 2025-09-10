// Mapping/OrderMappingProfile.cs
using AutoMapper;
using EventHorizon.Models;
using EventHorizon.DTO;

namespace EventHorizon.Mapping
{
    public class VenueMappingProfile : Profile
    {
        public VenueMappingProfile()
        {
            // Map from Venue to VenueDto
            CreateMap<Venue, VenueDto>();

        }
    }
}
