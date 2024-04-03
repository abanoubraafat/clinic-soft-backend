namespace ClinicSoftAPI.DTOs
{
    public class OperationDTO
    {
        public int Id { get; set; }

        public DateOnly? Date { get; set; }

        public string? Type { get; set; }

        public int? PatientId { get; set; }
    }
}
