
using ClinicSoftAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClinicSoftAPI.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookingClinicsContext _context;

        public UnitOfWork(BookingClinicsContext context)
        {
            _context = context;
            Availabilities = new AvailabilityRepository(context);
            Doctors = new DoctorRepository(context);
            Expenses = new ExpenseRepository(context);
            Operations = new OperationRepository(context);
            Patients = new PatientRepository(context);
            Receptionists = new ReceptionistRepository(context);
            Reservations = new ReservationRepository(context);
            Users = new UserRepository(context);
        }

        public IAvailabilityRepository Availabilities {  get; private set; }

        public IDoctorRepository Doctors { get; private set; }

        public IExpenseRepository Expenses { get; private set; }

        public IOperationRepository Operations { get; private set; }

        public IPatientRepository Patients { get; private set; }

        public IReceptionistRepository Receptionists { get; private set; }

        public IReservationRepository Reservations { get; private set; }

        public IUserRepository Users { get; private set; }
        

        public int Save()
        {
            return _context.SaveChanges();
        }
        


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
