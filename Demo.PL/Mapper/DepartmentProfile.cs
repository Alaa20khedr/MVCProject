using AutoMapper;
using Deme.DAL.Entities;
using Demo.PL.Models;

namespace Demo.PL.Mapper
{
    public class DepartmentProfile:Profile
    {
        public DepartmentProfile() {

            CreateMap<Department, DepartmentViewModel>().ReverseMap();
        }
    }
}
