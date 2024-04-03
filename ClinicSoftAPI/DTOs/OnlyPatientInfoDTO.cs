namespace ClinicSoftAPI.DTOs
{
    public class OnlyPatientInfoDTO
    {
        public int PatientId { get; set; }

        public string? Condition { get; set; }

        public string? BloodType { get; set; }

        public string? Medications { get; set; }

        public string? Notes { get; set; }

        public string? Fname { get; set; }

        public string? Lname { get; set; }

        public long? NationalId { get; set; }
    }
}
