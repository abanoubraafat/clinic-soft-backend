using AutoMapper;
using ClinicSoftAPI.DTOs.Receptionist;

namespace ClinicSoftAPI.MapsProfile
{
    public class ReceptionistMap : Profile
    {
        public ReceptionistMap() 
        {
            CreateMap<Receptionist, GetReceptionistDTO>()
                .ForMember(d => d.UserName, o => o.MapFrom(s => s.User.Fname + " " + s.User.Lname))
                .ForMember(d => d.UserEmail, o => o.MapFrom(s => s.User.Email))
                .ForMember(d => d.UserNational_Id, o => o.MapFrom(s => s.User.NationalId));

        }
    }
}
