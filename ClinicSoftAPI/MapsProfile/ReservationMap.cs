using AutoMapper;
using ClinicSoftAPI.DTOs.Reservation;

namespace ClinicSoftAPI.MapsProfile
{
    public class ReservationMap : Profile
    {
        public ReservationMap() 
        {
            CreateMap<Reservation, GetReservationWithPatientInfoDTO>()
                .ForMember(d => d.PatientName, o => o.MapFrom(s => s.Patient.Fname + " " + s.Patient.Lname));
            CreateMap<AddReservationMultipleDurationsDTO, Reservation>();
        }
    }
}
