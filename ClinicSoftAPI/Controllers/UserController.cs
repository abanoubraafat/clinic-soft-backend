using ClinicSoftAPI.DTOs.Receptionist;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicSoftAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
         [HttpGet("getAll")]
        public IActionResult Get()
        {
            var users = _unitOfWork.Users.GetAll();
            return Ok(users);
        }
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var user = _unitOfWork.Users.GetById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }
        [HttpGet("login")]
        public IActionResult login(string email,string passward )
        {
            var users = _unitOfWork.Users.login(email,passward);
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }

        [HttpPost]
        public IActionResult Add(User user)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var u = _unitOfWork.Users.Add(user);
            _unitOfWork.Save();
            return Ok(u);
        }
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, User u)
        {
            var user = _unitOfWork.Users.GetById(id);
            if (user == null)
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            user.Fname = u.Fname;
            user.Lname = u.Lname;
            user.NationalId = u.NationalId;
            user.Password = u.Password;
            user.Email = u.Email;
            user.Type = u.Type;
            _unitOfWork.Save();
            return Ok(user);
        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var user = _unitOfWork.Users.GetById(id);
            if (user == null)
                return NotFound();
            var deleted = _unitOfWork.Users.Delete(user);
            _unitOfWork.Save();
            return Ok(deleted);
        }
        [HttpGet("EmailCheck")]
        public IActionResult IsExistingEmail(string email)
        {
            var user = _unitOfWork.Users.Find(u => u.Email == email);
            if(user == null)
                return Ok(false);
            return Ok(true);
        }
    }
}
