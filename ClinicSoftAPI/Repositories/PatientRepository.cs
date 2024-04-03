using Microsoft.EntityFrameworkCore;

namespace ClinicSoftAPI.Repositories
{
    public class PatientRepository : BaseRepository<Patient>, IPatientRepository
    {
        private readonly BookingClinicsContext _context;
        public PatientRepository(BookingClinicsContext context) : base(context)
        { _context = context; }

        /*        public Patient GetWithReservation(int id)
                {
                    return _context.Patients.Include(p => p.Reservations).FirstOrDefault(p => p.PatientId == id);
                }
                public Patient GetWithOperations(int id)
                {
                    return _context.Patients.Include(p => p.Operations).FirstOrDefault(p => p.PatientId == id);
                }*/
        public IEnumerable<Patient> GetAllWithReservationsAndOperations()
        {
            return _context.Patients
                .Include(p => p.Reservations)
                .Include(p => p.Operations)
                .ToList();
        }
        public Patient GetByNationalId(long nationalId)
        {
            return _context.Patients.FirstOrDefault(p => p.NationalId == nationalId);
        }

        public Patient GetWithReservationAndOperations(int id)
        {
            return _context.Patients
                .Include(p => p.Reservations)
                .Include(p => p.Operations)
                .FirstOrDefault(p => p.PatientId == id);
        }

    }
}
