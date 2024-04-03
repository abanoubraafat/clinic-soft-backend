using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ClinicSoftAPI.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly BookingClinicsContext _context;
        public UserRepository(BookingClinicsContext context) : base(context)
        { _context = context; }

        //public IActionResult login(string email, string passward)
        //{
        //    var q = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == passward);
        //    if (q != null)
        //    {
        //        return q;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        User IUserRepository.login(string email, string passward)
        {
            var q = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == passward);
            return q;
        }
    }
}
