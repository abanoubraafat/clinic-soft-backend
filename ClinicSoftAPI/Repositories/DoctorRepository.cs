namespace ClinicSoftAPI.Repositories
{
    public class DoctorRepository : BaseRepository<Doctor>, IDoctorRepository
    {
        private readonly BookingClinicsContext _context;
        public DoctorRepository(BookingClinicsContext context) : base(context)
        {
            _context = context;
        }
    }
}
