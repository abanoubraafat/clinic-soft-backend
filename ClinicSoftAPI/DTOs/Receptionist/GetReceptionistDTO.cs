namespace ClinicSoftAPI.DTOs.Receptionist
{
    public class GetReceptionistDTO
    {
        public int RecepId { get; set; }

        public TimeOnly? StartShiftTime { get; set; }

        public TimeOnly? EndShiftTime { get; set; }

        public DateOnly? StartWorkingDate { get; set; }

        public int? UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string UserNational_Id { get; set; } = string.Empty;
    }
}
