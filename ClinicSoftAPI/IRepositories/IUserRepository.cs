using Microsoft.AspNetCore.Mvc;

namespace ClinicSoftAPI.IRepositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User login(string username, string password );
    }
}
