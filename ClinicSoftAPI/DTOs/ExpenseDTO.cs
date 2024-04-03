namespace ClinicSoftAPI.DTOs
{
    public class ExpenseDTO
    {
        public int Id { get; set; }

        public decimal? Electricity { get; set; }

        public decimal? Rent { get; set; }

        public decimal? Tools { get; set; }

        public decimal? Salaries { get; set; }

        public decimal? Others { get; set; }

        public DateOnly? Month { get; set; }

    }
}
