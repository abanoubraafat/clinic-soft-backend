using ClinicSoftAPI.DTOs.Reservation;

namespace ClinicSoftAPI.IRepositories
{
    public interface IReservationRepository : IBaseRepository<Reservation>
    {
        IEnumerable<ReservationsIncomeAllTime> GetAllIncome();
        IEnumerable<ReservationsIncomeInMonth> GetIncomeByMonth(DateOnly month);
        ReservationsIncomeInMonth GetIncomeByDay(DateOnly day);
        IEnumerable<ReservationsIncomeInYear> GetIncomeByYear(int year);
        IEnumerable<Reservation> GetByDay(DateOnly day);
        Reservation? GetLastReservation(DateOnly day);
    }
}
