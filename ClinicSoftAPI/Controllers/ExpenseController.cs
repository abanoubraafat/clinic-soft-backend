using AutoMapper;
using ClinicSoftAPI.DTOs;
using ClinicSoftAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicSoftAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ExpenseController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            IEnumerable<Expense> res = _unitOfWork.Expenses.GetAll("Doctor");
            if (res == null)
                return NotFound();
            #region old map
            //List<ExpenseDTO> expensesList = new List<ExpenseDTO>();
            //foreach (var expense in res)
            //{
            //    ExpenseDTO expenseDTO = new ExpenseDTO()
            //    {
            //        Id = expense.Id,
            //        Electricity = expense.Electricity,
            //        Rent = expense.Rent,
            //        Tools = expense.Tools,
            //        Salaries = expense.Salaries,
            //        Others = expense.Others,
            //        Month = expense.Month
            //    };
            //    expensesList.Add(expenseDTO);
            //}
            //return Ok(expensesList);

            #endregion
            return Ok(_mapper.Map<IEnumerable<ExpenseDTO>>(res));
        }
        [HttpGet("{id:int}")]   
        public IActionResult GetById(int id)
        {
            if (id == 0) return NotFound();
            var res = _unitOfWork.Expenses.GetById(id);
            if (res == null)
                return NotFound();
            else
                return Ok(res);
        }
        [HttpPost]
        public IActionResult save(ExpenseDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            Expense expense = _mapper.Map<Expense>(dto);
            _unitOfWork.Expenses.Add(expense);
            _unitOfWork.Save();
            return Created();
        }
        [HttpPut("{id:int}")]
        public IActionResult Update(ExpenseDTO dto, int id)
        {
            if (id == 0) { return NotFound(); }
            if (dto == null) { return BadRequest(); }
            Expense expense1 = _unitOfWork.Expenses.GetById(id);
            #region old map
            //expense1.Electricity = expense.Electricity;
            //expense1.Rent = expense.Rent;
            //expense1.Tools = expense.Tools;
            //expense1.Salaries = expense.Salaries;
            //expense1.Others = expense.Others;
            //expense1.Month = expense.Month;
            //_unitOfWork.Expenses.Update(expense1); 
            #endregion
            _mapper.Map(dto, expense1);
            _unitOfWork.Save();
            return Ok();
        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            Expense exp = _unitOfWork.Expenses.GetById(id);
            if (exp == null)
                return NotFound();
            _unitOfWork.Expenses.Delete(exp);
            _unitOfWork.Save();
            return Ok();
        }
        [HttpGet("GetBymonth")]
        public IActionResult GetBymonth(DateOnly date)
        {
            var exp = _unitOfWork.Expenses
                  .FindAll(r => r.Month == date).ToList();
            return Ok(exp);
        }
    }
}
