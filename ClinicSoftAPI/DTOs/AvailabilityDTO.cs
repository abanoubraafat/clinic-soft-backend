namespace ClinicSoftAPI.DTOs
{
    public class AvailabilityDTO
    {
        public DateOnly? Month { get; set; }

        public DateOnly? Day { get; set; }

        public TimeOnly? FromTime { get; set; }

        public TimeOnly? ToTime { get; set; }

        public int? DoctorId { get; set; }
    }
}
