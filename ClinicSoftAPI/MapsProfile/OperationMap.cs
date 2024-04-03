using AutoMapper;
using ClinicSoftAPI.DTOs;

namespace ClinicSoftAPI.MapsProfile
{
    public class OperationMap:Profile
    {
        public OperationMap()
        {
            CreateMap<OperationDTO, Operation>();

            CreateMap<Operation, OprationwithPatiantDTO>()
            .ForMember(d => d.PatientName, O => O.MapFrom(S => S.Patient.Fname+" "+S.Patient.Lname));
        }
    }
}
