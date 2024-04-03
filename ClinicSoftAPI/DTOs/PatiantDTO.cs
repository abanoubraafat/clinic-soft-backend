namespace ClinicSoftAPI.DTOs
{
    public class PatiantDTO
    {
        public int PatientId { get; set; }

        public string? Condition { get; set; }

        public string? BloodType { get; set; }

        public string? Medications { get; set; }

        public string? Notes { get; set; }

        public string? Fname { get; set; }

        public string? Lname { get; set; }

        public long? NationalId { get; set; }

        public List<int?> ReservationNumber { get; set; } = new List<int?>();
        public List<DateOnly?> ReservationDay { get; set; } = new List<DateOnly?>();

        public List<TimeOnly?> ReservationFromTime { get; set; } = new List<TimeOnly?>();
        public List<TimeOnly?> ReservationToTime { get; set; } = new List<TimeOnly?>();
        public List<DateOnly> OperationDate { get; set; } = new List<DateOnly>();
        public List<String?> OperationType { get; set; } = new List<string?>();

    }
}
