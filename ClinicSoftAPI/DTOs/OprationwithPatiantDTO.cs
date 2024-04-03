using AutoMapper;

namespace ClinicSoftAPI.DTOs
{
    public class OprationwithPatiantDTO
    {
        public int Id { get; set; }

        public DateOnly? Date { get; set; }

        public string? Type { get; set; }

        public int? PatientId { get; set; }

        public string? PatientName { get; set; }
    }
}
