namespace ClinicSoftAPI.UnitOfWork
{
    public interface IUnitOfWork
    {
        IAvailabilityRepository Availabilities { get; }
        IDoctorRepository Doctors { get; }
        IExpenseRepository Expenses { get; }
        IOperationRepository Operations { get; }
        IPatientRepository Patients { get; }
        IReceptionistRepository Receptionists { get; }
        IReservationRepository Reservations { get; }
        IUserRepository Users { get; }
        int Save();
        void Dispose();
    }
}
