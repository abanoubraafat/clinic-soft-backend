using AutoMapper;
using ClinicSoftAPI.DTOs;

namespace ClinicSoftAPI.MapsProfile
{
    public class ExpensesMap : Profile
    {
        public ExpensesMap()
        {
            CreateMap<ExpenseDTO, Expense>().ReverseMap();
            //CreateMap<Expense, ExpenseDTO>();

        }
    }
}
