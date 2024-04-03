namespace ClinicSoftAPI.Repositories
{
    public class AvailabilityRepository : BaseRepository<Availability>, IAvailabilityRepository
    {
        private readonly BookingClinicsContext _context;
        public AvailabilityRepository(BookingClinicsContext context) : base(context)
        {
            _context = context;
        }
    }
}
