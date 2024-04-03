using AutoMapper;
using ClinicSoftAPI.DTOs.Receptionist;
using ClinicSoftAPI.IRepositories;
using ClinicSoftAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicSoftAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceptionistController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ReceptionistController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var receptionists = _unitOfWork.Receptionists.GetAll("User");
            var mapped = _mapper.Map<IEnumerable<GetReceptionistDTO>>(receptionists);
            foreach (var r in mapped)
                r.UserName ??= string.Empty;
            return Ok(mapped);
        }
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var receptionist = _unitOfWork.Receptionists.Find(r => r.RecepId == id, ["User"]);
            if (receptionist == null)
                return NotFound();
            var mapped = _mapper.Map<GetReceptionistDTO>(receptionist);
            mapped.UserName ??= string.Empty;
            return Ok(mapped);
        }
        [HttpPost]
        public IActionResult Add(Receptionist receptionist)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            if(receptionist.EndShiftTime < receptionist.StartShiftTime)
                return BadRequest("EndShiftTime can't be before StartShiftTime");
            var r = _unitOfWork.Receptionists.Add(receptionist);
            _unitOfWork.Save();
            return Ok(r);
        }
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, Receptionist r)
        {
            var receptionist = _unitOfWork.Receptionists.GetById(id);
            if (receptionist == null)
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            if (r.EndShiftTime < r.StartShiftTime)
                return BadRequest("EndShiftTime can't be before StartShiftTime");
            receptionist.StartShiftTime = r.StartShiftTime;
            receptionist.EndShiftTime = r.EndShiftTime;
            receptionist.StartWorkingDate = r.StartWorkingDate;
            var updated = _unitOfWork.Receptionists.Update(receptionist);
            _unitOfWork.Save();
            return Ok(updated);
        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var receptionist = _unitOfWork.Receptionists.GetById(id);
            if (receptionist == null)
                return NotFound();
            var deleted = _unitOfWork.Receptionists.Delete(receptionist);
            _unitOfWork.Save();
            return Ok(deleted);
        }

    }
}
