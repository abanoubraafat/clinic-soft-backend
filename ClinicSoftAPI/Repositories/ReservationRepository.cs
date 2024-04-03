
using ClinicSoftAPI.DTOs.Reservation;

namespace ClinicSoftAPI.Repositories
{
    public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {
        private readonly BookingClinicsContext _context;
        public ReservationRepository(BookingClinicsContext context) : base(context)
        {
            _context = context;
        }
        public ReservationsIncomeInMonth GetIncomeByDay(DateOnly day)
        {
            var reservationIncome =
                _context.Reservations
                .Where(r => r.Day.Year == day.Year && r.Day.Month == day.Month && r.Day.Day == day.Day)
                .GroupBy(r => r.Day)
                .Select(d => new ReservationsIncomeInMonth { Day = d.Key, Income = d.Sum(r => r.Cost) })
                .SingleOrDefault();
            reservationIncome ??= new ReservationsIncomeInMonth { Day = day, Income = 0 };
            return reservationIncome;
        }
        public IEnumerable<ReservationsIncomeInMonth> GetIncomeByMonth(DateOnly month)
        {
            int daysInMonth = DateTime.DaysInMonth(month.Year, month.Month);
            List<ReservationsIncomeInMonth> list = new List<ReservationsIncomeInMonth>();
            for (int i = 1; i <= daysInMonth; i++)
            {
                var reservationIncome =
                _context.Reservations
                .Where(r => r.Day.Year == month.Year && r.Day.Month == month.Month && r.Day.Day == i)
                .GroupBy(r => r.Day)
                .Select(d => new ReservationsIncomeInMonth { Day = d.Key, Income = d.Sum(r => r.Cost) })
                .SingleOrDefault();
                reservationIncome ??=
                    new ReservationsIncomeInMonth { Day = new DateOnly(month.Year, month.Month, i), Income = 0 };
                list.Add(reservationIncome);
            }
            return list;
        }
        public IEnumerable<ReservationsIncomeInYear> GetIncomeByYear(int year)
        {
            List<ReservationsIncomeInYear> list = new List<ReservationsIncomeInYear>();
            for (int j = 1; j <= 12; j++)
            {
                int daysInMonth = DateTime.DaysInMonth(year, j);
                List<ReservationsIncomeInMonth> listOfOneMonth = new List<ReservationsIncomeInMonth>();
                for (int i = 1; i <= daysInMonth; i++)
                {
                    var reservationIncome =
                    _context.Reservations
                    .Where(r => r.Day.Year == year && r.Day.Month == j && r.Day.Day == i)
                    .GroupBy(r => r.Day)
                    .Select(d => new ReservationsIncomeInMonth { Day = d.Key, Income = d.Sum(r => r.Cost) })
                    .SingleOrDefault();
                    reservationIncome ??=
                        new ReservationsIncomeInMonth { Day = new DateOnly(year, j, i), Income = 0 };
                    listOfOneMonth.Add(reservationIncome);
                }
                list.Add(new ReservationsIncomeInYear
                {
                    Income = listOfOneMonth.Sum(c => c.Income),
                    Month = j
                });
            }
            return list;
        }
        public IEnumerable<ReservationsIncomeAllTime> GetAllIncome()
        {
            List<ReservationsIncomeAllTime> income = new List<ReservationsIncomeAllTime>();
            for (int k = 2024; k <= DateTime.Now.Year; k++)
            {
                List<ReservationsIncomeInYear> list = new List<ReservationsIncomeInYear>();
                for (int j = 1; j <= 12; j++)
                {
                    int daysInMonth = DateTime.DaysInMonth(k, j);
                    List<ReservationsIncomeInMonth> listOfOneMonth = new List<ReservationsIncomeInMonth>();
                    for (int i = 1; i <= daysInMonth; i++)
                    {
                        var reservationIncome =
                        _context.Reservations
                        .Where(r => r.Day.Year == k && r.Day.Month == j && r.Day.Day == i)
                        .GroupBy(r => r.Day)
                        .Select(d => new ReservationsIncomeInMonth { Day = d.Key, Income = d.Sum(r => r.Cost) })
                        .SingleOrDefault();
                        reservationIncome ??=
                            new ReservationsIncomeInMonth { Day = new DateOnly(k, j, i), Income = 0 };
                        listOfOneMonth.Add(reservationIncome);
                    }
                    list.Add(new ReservationsIncomeInYear
                    {
                        Income = listOfOneMonth.Sum(c => c.Income),
                        Month = j
                    });
                }
                income.Add(new ReservationsIncomeAllTime
                {
                    Income = list.Sum(c => c.Income),
                    Year = k
                });
            }
            return income;
        }

        public IEnumerable<Reservation> GetByDay(DateOnly day)
        {
            return _context.Reservations.Where(r => r.Day == day).ToList();
        }

        public Reservation? GetLastReservation(DateOnly day)
        {
            return GetByDay(day).LastOrDefault();
        }
    }
}
