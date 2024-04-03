using AutoMapper;
using ClinicSoftAPI.DTOs.Reservation;
using ClinicSoftAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClinicSoftAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ReservationController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var reservations = _unitOfWork.Reservations.GetAll("Patient");
            var mapped = _mapper.Map<IEnumerable<GetReservationWithPatientInfoDTO>>(reservations);
            foreach(var reservation in mapped)
                reservation.PatientName ??= string.Empty;
            return Ok(mapped);
        }
        [HttpGet("GetNew")] //returns reservation of today and upcoming days
        public IActionResult GetNew()
        {
            var newReservations =
                _unitOfWork.Reservations
                .FindAll(r => r.Day >= DateOnly.FromDateTime(DateTime.Now), null, ["Patient"]);
            var mapped = _mapper
                         .Map<IEnumerable<GetReservationWithPatientInfoDTO>>(newReservations);
            foreach (var reservation in mapped)
                reservation.PatientName ??= string.Empty;
            return Ok(mapped);
        }
        [HttpGet("GetByDay")]
        public IActionResult GetByDay(DateOnly date)
        {
            var newReservations =
                _unitOfWork.Reservations
                .FindAll(r => r.Day == date, null, ["Patient"]);
            var mapped = _mapper
                         .Map<IEnumerable<GetReservationWithPatientInfoDTO>>(newReservations);
            foreach (var reservation in mapped)
                reservation.PatientName ??= string.Empty;
            return Ok(mapped);
        }
        //[HttpGet("GetUnAvailableTimesByDay")]
        //public IActionResult GetUnAvailableTimesByDay(DateOnly date)
        //{
        //    var TakenReservations =
        //        _unitOfWork.Reservations
        //        .FindAll(r => r.Day == date).Select(r => new { r.FromTime, r.ToTime }).ToList();
        //    return Ok(TakenReservations);
        //}
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var reservation = _unitOfWork.Reservations.Find(r => r.Id == id, ["Patient"]);
            if(reservation == null)
                return NotFound();
            var mapped = _mapper.Map<GetReservationWithPatientInfoDTO>(reservation);
            mapped.PatientName ??= string.Empty;
            return Ok(mapped);
        }
        [HttpPost]
        public IActionResult Add(Reservation reservation)
        {
            if (!ModelState.IsValid)
                return BadRequest("ModelState isn't valid");
            if (reservation.Type != "New" && reservation.Type != "Consult")
                return BadRequest("Type must be New - Consult");
            // 
            var DR_availableTime =
                _unitOfWork.Availabilities
                .Find(a => a.Day == reservation.Day);

            var lastReservationRecordedOfTheDay =
               _unitOfWork.Reservations
               .FindAll(r => r.Day == reservation.Day)
               .OrderBy(r => r.Day)
               .LastOrDefault();
            if (DR_availableTime != null)
            {
                if (lastReservationRecordedOfTheDay == null) //No Reservations Yet
                {
                    reservation.FromTime = DR_availableTime.FromTime;
                    reservation.ReservationNo = 1;
                }
                else if (lastReservationRecordedOfTheDay.ToTime >= DR_availableTime.ToTime)
                    return BadRequest("Dr Unavailable this day because dr.ToTime is reached");
                else
                {
                    reservation.FromTime = lastReservationRecordedOfTheDay.ToTime;
                    reservation.ReservationNo = lastReservationRecordedOfTheDay.ReservationNo + 1;
                }
            }
            else
                return BadRequest("Dr didn't set his available time during that day yet!!");

            if (reservation.Type == "New")
                reservation.ToTime = reservation.FromTime.Value.AddMinutes(15);
            else if (reservation.Type == "Consult")
                reservation.ToTime = reservation.FromTime.Value.AddMinutes(10);
            _unitOfWork.Reservations.Add(reservation);
            _unitOfWork.Save();
            return Created();
        }

        [HttpPost("AddForMultipleDrAppointments")]
        public IActionResult Add(AddReservationMultipleDurationsDTO reservation)
        {
            if (!ModelState.IsValid)
                return BadRequest("ModelState isn't valid");
            if (reservation.Type != "New" && reservation.Type != "Consult")
                return BadRequest("Type must be New - Consult");
            // 
            var DR_availableTime =
                new {reservation.DrToTime, reservation.DrFromTime};
            var mapped = _mapper.Map<Reservation>(reservation);
            var lastReservationRecordedOfTheDay =
               _unitOfWork.Reservations
               .FindAll(r => r.Day == reservation.Day && r.FromTime >= (DR_availableTime.DrFromTime) && r.ToTime <= (DR_availableTime.DrToTime))
               .OrderBy(r => r.Day)
               .LastOrDefault();
            if (DR_availableTime != null)
            {
                if (lastReservationRecordedOfTheDay == null) //No Reservations Yet
                {
                    mapped.FromTime = DR_availableTime.DrFromTime;
                    mapped.ReservationNo = 1;
                }
                //No Reservations before 15 Minutes from Dr.ToTime 
                else if (lastReservationRecordedOfTheDay.ToTime >= DR_availableTime.DrToTime.AddMinutes(-15))
                    return BadRequest("Dr Unavailable this day because dr.ToTime is reached");
                else
                {
                    mapped.FromTime = lastReservationRecordedOfTheDay.ToTime;
                    mapped.ReservationNo = lastReservationRecordedOfTheDay.ReservationNo + 1;
                }
            }
            else
                return BadRequest("Dr didn't set his available time during that day yet!!");

            if (mapped.Type == "New")
                mapped.ToTime = mapped.FromTime.Value.AddMinutes(15);
            else if (mapped.Type == "Consult")
                mapped.ToTime = mapped.FromTime.Value.AddMinutes(10);
            _unitOfWork.Reservations.Add(mapped);
            _unitOfWork.Save();
            return Created();
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(Reservation reservation, int id)
        {
            Reservation r = _unitOfWork.Reservations.GetById(id);
            if(r == null)
                return NotFound();
            if(reservation.Type != null)
            {
                if (reservation.Type != "New" && reservation.Type != "Consult")
                    return BadRequest("Type must be New - Consult");
                if (reservation.Type == "New")
                    r.ToTime = reservation.FromTime.Value.AddMinutes(15);
                else if (reservation.Type == "Consult")
                    r.ToTime = reservation.FromTime.Value.AddMinutes(10);
                r.FromTime = reservation.FromTime;
            }
            if(reservation.Day != r.Day)
            {
                var reservationsOfTheDay = _unitOfWork.Reservations.GetByDay(reservation.Day);
                int i = reservation.ReservationNo.Value;
                foreach (var item in reservationsOfTheDay)
                {
                    if (item.ReservationNo.Value > i)
                        item.ReservationNo = item.ReservationNo.Value - 1;
                }
                if(reservationsOfTheDay.Count() != 0)
                    r.ReservationNo = reservationsOfTheDay.LastOrDefault().ReservationNo + 1;
                else
                    r.ReservationNo = 1;
            }
            else
            {
                r.ReservationNo = reservation.ReservationNo;
            }
            r.Cost = reservation.Cost;
            r.Day = reservation.Day;   
            r.PatientId = reservation.PatientId;
            r.Type = reservation.Type;
            _unitOfWork.Reservations.Update(r);
            _unitOfWork.Save();
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var reservation = _unitOfWork.Reservations.GetById(id);
            if (reservation == null)
                return NotFound();
            var reservationsOfTheDay = _unitOfWork.Reservations.GetByDay(reservation.Day);
            int i = reservation.ReservationNo.Value;
            foreach(var item in reservationsOfTheDay)
            {
                if (item.ReservationNo.Value > i)
                    item.ReservationNo = item.ReservationNo.Value - 1;
            }
            _unitOfWork.Reservations.Delete(reservation);
            _unitOfWork.Save();
            return Ok();
        }

        [HttpGet("IncomeByDay")]
        public IActionResult GetIncomeByDay(DateOnly date)
        {
            return Ok(_unitOfWork.Reservations.GetIncomeByDay(date));
        }
        [HttpGet("IncomeByMonth")]
        public IActionResult GetIncomeByMonth(DateOnly date)
        {
            return Ok(_unitOfWork.Reservations.GetIncomeByMonth(date));
        }
        [HttpGet("IncomeByYear")]
        public IActionResult GetIncomeByYear(int date)
        {
            return Ok(_unitOfWork.Reservations.GetIncomeByYear(date));
        }
        [HttpGet("IncomeAllTime")]
        public IActionResult GetIncomeAllTime()
        {
            return Ok(_unitOfWork.Reservations.GetAllIncome());
        }
        [HttpGet("IsValidLast")]
        public IActionResult GetLastReservationOfTheDay(DateOnly date) 
        {
            var DR_availableTime =
            _unitOfWork.Availabilities
                .Find(a => a.Day == date);

            var lastReservationRecordedOfTheDay =
               _unitOfWork.Reservations
               .FindAll(r => r.Day == date)
               .OrderBy(r => r.Day)
               .LastOrDefault();
            if (DR_availableTime != null)
            {
                if (lastReservationRecordedOfTheDay == null) //No Reservations Yet
                {
                    return Ok(true);
                }
                else if (lastReservationRecordedOfTheDay.ToTime >= DR_availableTime.ToTime)
                    return Ok(false);
                else
                {
                    return Ok(true);
                }
            }
            else
                return Ok(false);
        }
        [HttpGet("GetLastReservation")]
        public IActionResult GetLastReservation(DateOnly date)
        {
            return Ok(_unitOfWork.Reservations.GetLastReservation(date));
        }
    }
}
