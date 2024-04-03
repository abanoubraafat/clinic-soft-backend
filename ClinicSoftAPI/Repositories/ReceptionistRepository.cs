using ClinicSoftAPI.IRepositories;
using ClinicSoftAPI.Models;

namespace ClinicSoftAPI.Repositories
{
    public class ReceptionistRepository : BaseRepository<Receptionist>, IReceptionistRepository
    {
        private readonly BookingClinicsContext _context;
        public ReceptionistRepository(BookingClinicsContext context) : base(context)
        {
            _context = context;
        }

    }
}
