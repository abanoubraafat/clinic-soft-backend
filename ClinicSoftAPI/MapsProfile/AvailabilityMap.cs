using AutoMapper;
using ClinicSoftAPI.DTOs;

namespace ClinicSoftAPI.MapsProfile
{
    public class AvailabilityMap : Profile
    {
        public AvailabilityMap()
        {
               CreateMap<Availability , AvailabilityDTO>().ReverseMap();
        }
    }
}
