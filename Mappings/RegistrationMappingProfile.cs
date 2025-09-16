using AutoMapper;
using EventHorizon.Models;
using EventHorizon.DTO;

namespace EventHorizon.Mapping
{
  public class RegistrationMappingProfile : Profile
  {
    public RegistrationMappingProfile()
    {
      // Map from Registration to RegistrationDto
      CreateMap<Registration, RegistrationDto>();
    }
  }
}
