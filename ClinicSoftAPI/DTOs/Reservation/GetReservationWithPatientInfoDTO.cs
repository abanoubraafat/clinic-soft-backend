namespace ClinicSoftAPI.DTOs.Reservation
{
    public class GetReservationWithPatientInfoDTO
    {
        public int Id { get; set; }

        public string? Type { get; set; }

        public DateOnly? Day { get; set; }

        public TimeOnly? FromTime { get; set; }

        public TimeOnly? ToTime { get; set; }

        public decimal? Cost { get; set; }

        public int? ReservationNo { get; set; }

        public int? PatientId { get; set; }

        public string? PatientName { get; set; } = string.Empty;
    }
}
