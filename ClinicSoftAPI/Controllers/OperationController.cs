using AutoMapper;
using ClinicSoftAPI.DTOs;
using ClinicSoftAPI.DTOs.Reservation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicSoftAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OperationController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("All")]
        public IActionResult GetAll()
        {
            IEnumerable<Operation> P = _unitOfWork.Operations.GetAll("Patient");
            var OperationDetails = _mapper.Map<IEnumerable<OprationwithPatiantDTO>>(P);
            return Ok(OperationDetails);
        }
        [HttpGet("GetByDay")]
        public IActionResult GetByDay(DateOnly date)
        {
            var newOperations =
                _unitOfWork.Operations
                .FindAll(r => r.Date == date, null, ["Patient"]);
            var mapped = _mapper
                         .Map<IEnumerable<OprationwithPatiantDTO>>(newOperations);

            return Ok(mapped);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {

            Operation operation = _unitOfWork.Operations.Find(O => O.Id == id, ["Patient"]);
            if (operation != null)
            {
                var operationDetails = _mapper.Map<OprationwithPatiantDTO>(operation);
               
                return Ok(operationDetails);


            }
            else
            {
                return BadRequest();
            }

           
        }


        [HttpPost]
       //Added witoutValidation
        /*        public IActionResult Add(OperationDTO O)
                {

                    if (!ModelState.IsValid)
                        return BadRequest();
                    var OpetaionAdded = _mapper.Map<Operation>(O);
                    _unitOfWork.Operations.Add(OpetaionAdded);
                    _unitOfWork.Save();
                    return Created();
                }*/
        public IActionResult Add(OperationDTO O)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check if the patient with the provided PatientId exists
            if (O.PatientId != null)
            {
                var patient = _unitOfWork.Patients.Find(p => p.PatientId == O.PatientId);
                if (patient == null)
                {
                    ModelState.AddModelError("PatientId", "Patient with the provided ID does not exist.");
                    return BadRequest(ModelState);
                }
            }
            else
            {
                ModelState.AddModelError("PatientId", "PatientId cannot be null.");
                return BadRequest(ModelState);
            }

            var operationAdded = _mapper.Map<Operation>(O);
            _unitOfWork.Operations.Add(operationAdded);
            _unitOfWork.Save();

            return Created();
        }

        [HttpPut("{id:int}")]
        //Updated Without Validation
        /*        public IActionResult Update(OperationDTO O, int id)
                {
                    if (id == 0) { return NotFound(); }
                    if (O == null) { return BadRequest(); }
                    Operation operation = _unitOfWork.Operations.GetById(id);
                    operation.Date = O.Date;
                    operation.Type = O.Type;
                    operation.PatientId = O.PatientId;

                    _unitOfWork.Operations.Update(operation);
                    _unitOfWork.Save();
                    return Ok();
                }*/

        public IActionResult Update(OperationDTO O, int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            if (O == null)
            {
                return BadRequest();
            }

            // Check if the patient with the provided PatientId exists
            if (O.PatientId != null)
            {
                var patient = _unitOfWork.Patients.Find(p => p.PatientId == O.PatientId);
                if (patient == null)
                {
                    ModelState.AddModelError("PatientId", "Patient with the provided ID does not exist.");
                    return BadRequest(ModelState);
                }
            }
            else
            {
                ModelState.AddModelError("PatientId", "PatientId cannot be null.");
                return BadRequest(ModelState);
            }

            Operation operation = _unitOfWork.Operations.GetById(id);
            if (operation == null)
            {
                return NotFound();
            }
            _mapper.Map(O, operation);
/*            operation.Date = O.Date;
            operation.Type = O.Type;
            operation.PatientId = O.PatientId;*/

           /* _unitOfWork.Operations.Update(operation);*/
            _unitOfWork.Save();
            return Ok();
        }

       

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            Operation exp = _unitOfWork.Operations.GetById(id);
            if (exp == null)
                return NotFound();
            _unitOfWork.Operations.Delete(exp);
            _unitOfWork.Save();
            return Ok();
        }
    }
}
