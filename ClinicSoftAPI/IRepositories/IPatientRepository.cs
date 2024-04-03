



namespace ClinicSoftAPI.IRepositories
{
    public interface IPatientRepository : IBaseRepository<Patient>
    {
        IEnumerable<Patient> GetAllWithReservationsAndOperations();
        Patient GetWithReservationAndOperations(int id);
        Patient GetByNationalId(long nationalId);
    }
}
