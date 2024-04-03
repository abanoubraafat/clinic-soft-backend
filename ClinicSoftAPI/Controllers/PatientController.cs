using AutoMapper;
using ClinicSoftAPI.DTOs;
using ClinicSoftAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicSoftAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;
        public PatientController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpGet("GetinfoonlyPatient")]
        public IActionResult GetAllPatientsInfo()
        {
            IEnumerable<Patient> patients = _unitOfWork.Patients.GetAll();
            IEnumerable<OnlyPatientInfoDTO> patientInfo = _mapper.Map<IEnumerable<OnlyPatientInfoDTO>>(patients);
            return Ok(patientInfo);
        }

        [HttpGet("{id}/GetinfoonlyPatientbyId")]
        public IActionResult GetPatientInfoById(int id)
        {
            Patient patient = _unitOfWork.Patients.GetById(id);
            if (patient == null)
            {
                return NotFound();
            }
            OnlyPatientInfoDTO patientInfo = _mapper.Map<OnlyPatientInfoDTO>(patient);
            return Ok(patientInfo);
        }

        [HttpGet("All")]
        /*        public IActionResult GetAll()
                {
                 IEnumerable<Patient> P = _unitOfWork.Patients.GetAll("Reservations");
                     var patiantDetails=_mapper.Map<IEnumerable<PatiantDTO>>(P);
                    return Ok(patiantDetails);
                }*/
        public IActionResult GetAll()
        {
            IEnumerable<Patient> patients = _unitOfWork.Patients.GetAllWithReservationsAndOperations();
            var patientDetails = _mapper.Map<IEnumerable<PatiantDTO>>(patients);
            return Ok(patientDetails);
        }


        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            Patient patient = _unitOfWork.Patients.GetWithReservationAndOperations(id);
            if (patient != null)
            {
                var patientDetails = _mapper.Map<PatiantDTO>(patient);
                return Ok(patientDetails);
            }
            else
            {
                return NotFound();
            }
        }

        /*        public IActionResult Get(int id)
                  {

                      Patient patiant = _unitOfWork.Patients.GetWithReservation(id);
                      if (patiant != null)
                      {
                          var patiantDetails=_mapper.Map<PatiantDTO>(patiant);

                          PatiantDTO patiantDetails = new PatiantDTO();
                          patiantDetails.PatientId = patiant.PatientId;
                          patiantDetails.NationalId = patiant.NationalId;
                          patiantDetails.Fname = patiant.Fname;
                          patiantDetails.Lname = patiant.Lname;
                          patiantDetails.Medications = patiant.Medications;
                          patiantDetails.BloodType = patiant.BloodType;
                          patiantDetails.Condition = patiant.Condition;
                          patiantDetails.Notes = patiant.Notes;


                          foreach (var PR in patiant.Reservations)
                          {
                              patiantDetails.ReservationNumber.Add(PR.ReservationNo);
                           }
                          return Ok(patiantDetails);
                          }
                      else
                      {
                          return BadRequest();
                      }

                      var patient = _unitOfWork.Patients.GetById(id);
                      if (patient == null)
                          return NotFound();
                      return Ok(patient);
                  }
        */

        [HttpPost]
        /* public IActionResult Add(PatiantAddDTO p)
         {
             var patiantDetails = _mapper.Map<Patient>(p);
             if (!ModelState.IsValid)
                 return BadRequest();
             var r = _unitOfWork.Patients.Add(patiantDetails);
             _unitOfWork.Save();
             return Ok(r);
         }*/

        public IActionResult Add(PatiantAddDTO p)
        {
            // Map DTO to entity
            var patientToAdd = _mapper.Map<Patient>(p);

            // Check if a patient with the specified national ID already exists
            long nationalId = p.NationalId ?? 0; // Use 0 as a default value if NationalId is null
            var existingPatient = _unitOfWork.Patients.GetByNationalId(nationalId);
            if (existingPatient != null)
            {
                // Patient with the specified national ID already exists
                return Conflict($"A patient with the national ID {nationalId} already exists.");
            }

            if (!ModelState.IsValid)
            {
                // If model state is not valid, return BadRequest
                return BadRequest(ModelState);
            }

            // Add patient to the database
            _unitOfWork.Patients.Add(patientToAdd);
            _unitOfWork.Save();

            return Ok(patientToAdd);
        }


        [HttpPut("{id:int}")]
        /*public IActionResult Update(int id, Patient p)
        {
            var patiant = _unitOfWork.Patients.GetById(id);
            if (patiant == null)
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            patiant.Condition = p.Condition;
            patiant.BloodType = p.BloodType;
            patiant.Medications = p.Medications;
            patiant.Notes = p.Notes;
            patiant.Fname = p.Lname;
            patiant.NationalId = p.NationalId;

            var updated = _unitOfWork.Patients.Update(patiant);
            _unitOfWork.Save();
            return Ok(updated);
        }*/
        
        public IActionResult UpdatePatient(int id, PatiantAddDTO updatedPatientDto)
        {
            // Retrieve the patient to be updated from the database
            var existingPatient = _unitOfWork.Patients.GetById(id);
            if (existingPatient == null)
            {
                return NotFound();
            }

            // Check if a patient with the specified national ID already exists (excluding the current patient)
            if (updatedPatientDto.NationalId.HasValue)
            {
                var patientWithSameNationalId = _unitOfWork.Patients.GetByNationalId(updatedPatientDto.NationalId.Value);
                if (patientWithSameNationalId != null && patientWithSameNationalId.PatientId != id)
                {
                    // Patient with the specified national ID already exists
                    return Conflict($"A patient with the national ID {updatedPatientDto.NationalId} already exists.");
                }
            }

            // Map the updated DTO to the existing patient entity
            _mapper.Map(updatedPatientDto, existingPatient);

            // Save changes to the database
            _unitOfWork.Save();

            return Ok(existingPatient);
        }


        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var patient = _unitOfWork.Patients.GetById(id);
            if (patient == null)
                return NotFound();
            var deleted = _unitOfWork.Patients.Delete(patient);
            _unitOfWork.Save();
            return Ok(deleted);
        }
    }
}
