namespace ClinicSoftAPI.Repositories
{
    public class ExpenseRepository : BaseRepository<Expense>, IExpenseRepository
    {
        private readonly BookingClinicsContext _context;
        public ExpenseRepository(BookingClinicsContext context) : base(context)
        {
            _context = context;
        }
    }
}
