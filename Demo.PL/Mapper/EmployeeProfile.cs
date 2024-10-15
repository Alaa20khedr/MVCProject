using AutoMapper;
using Deme.DAL.Entities;
using Demo.PL.Models;
namespace Demo.PL.Mapper
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile() {

            //CreateMap<EmployeeViewModel, Employee>();
            //CreateMap<Employee, EmployeeViewModel>();
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
        }
    }
}
