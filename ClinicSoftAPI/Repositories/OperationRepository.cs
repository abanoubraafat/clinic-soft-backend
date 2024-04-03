namespace ClinicSoftAPI.Repositories
{
    public class OperationRepository : BaseRepository<Operation>, IOperationRepository
    {
        private readonly BookingClinicsContext _context;
        public OperationRepository(BookingClinicsContext context) : base(context)
        {
            _context = context;
        }
    }
}
