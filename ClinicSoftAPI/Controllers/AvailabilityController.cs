using AutoMapper;
using ClinicSoftAPI.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicSoftAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailabilityController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;  

        public AvailabilityController(IUnitOfWork unitOfWork , IMapper mapper )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet("{id:int}")]
        public IActionResult GetId(int id)
        {
            if (id == 0) return NotFound();
            var res = _unitOfWork.Availabilities.GetById(id);
            if (res == null)
                return NotFound();
            else
                return Ok(res);
        }
        [HttpGet("GetByDay")]
        public IActionResult GetByDay(DateOnly date)
        {
            var availableTime =
                _unitOfWork.Availabilities
                .Find(a => a.Day == date);
            if (availableTime == null)
                return Ok();
            return Ok(availableTime);               
        }
        ///
        [HttpGet("GetBymonth")]
        public IActionResult GetBymonth(DateOnly date)
        {
            var exp = _unitOfWork.Availabilities
                  .FindAll(r => r.Month == date).ToList();
            return Ok(exp);
        }
        ///
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var res = _unitOfWork.Availabilities.GetAll();
            if (res == null)
                return NotFound();
            else
                return Ok(res);
        }
        [HttpPost]
        public IActionResult save(AvailabilityDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            Availability availability = _mapper.Map<Availability>(dto);
            _unitOfWork.Availabilities.Add(availability);
            _unitOfWork.Save();
            return Created();
        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            Availability availability = _unitOfWork.Availabilities.GetById(id);
            if (availability == null)
                return NotFound();
            _unitOfWork.Availabilities.Delete(availability);
            _unitOfWork.Save();
            return Ok();
        }
        [HttpPut("{id:int}")]
        public IActionResult Update(Availability availability, int id)
        {
            if (id == 0) { return NotFound(); }
            if (availability == null) { return BadRequest(); }
            Availability availability1  = _unitOfWork.Availabilities.GetById(id);
            availability1.Month = availability.Month;
            availability1.Day = availability.Day;
            availability1.ToTime = availability.ToTime;
            availability1.FromTime = availability.FromTime;
            availability1.DoctorId = availability.DoctorId;
            _unitOfWork.Availabilities.Update(availability1);
            _unitOfWork.Save();
            return Ok();
        }
    }
}
