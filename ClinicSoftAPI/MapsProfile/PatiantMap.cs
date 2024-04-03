using AutoMapper;
using ClinicSoftAPI.DTOs;

namespace ClinicSoftAPI.MapsProfile
{
    public class PatiantMap : Profile
    {
        public PatiantMap()
        {
            CreateMap<Patient, PatiantDTO>()
              .ForMember(d => d.ReservationNumber, O => O.MapFrom(S => S.Reservations.Select(R => R.ReservationNo)))
              .ForMember(d => d.ReservationDay, O => O.MapFrom(S => S.Reservations.Select(R => R.Day)))
              .ForMember(d => d.ReservationFromTime, O => O.MapFrom(S => S.Reservations.Select(R => R.FromTime)))
              .ForMember(d => d.ReservationToTime, O => O.MapFrom(S => S.Reservations.Select(R => R.ToTime)))
              .ForMember(d => d.OperationDate, O => O.MapFrom(S => S.Operations.Select(R => R.Date)))
              .ForMember(d => d.OperationType, O => O.MapFrom(S => S.Operations.Select(R => R.Type)));

            CreateMap<PatiantAddDTO, Patient>();
            CreateMap<Patient, OnlyPatientInfoDTO > (); 

        }
    }
}
