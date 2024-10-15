using Deme.DAL.Context;
using Deme.DAL.Entities;
using Demo.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Reposatories
{
    public class DepartmentRepository:GenericRepository<Department> , IDepartmentRepository
    {
      // private readonly AppCompanyContext context;

        public DepartmentRepository(AppCompanyContext context):base(context)
        {
            //this.context = context;
        }
       // public int Add(Department department)
       // {
       //     context.Add(department);
       //     return context.SaveChanges();
       // }

       // public int Delete(Department department)
       // {
       //     context.Remove(department);
       //     return context.SaveChanges();
       // }

       // public IEnumerable<Department> GetAll()
       // =>
       //     context.Departments.ToList();


       // public Department GetById(int ? id)
       //=> context.Departments.FirstOrDefault(x => x.Id == id);
       // public int Update(Department department)
       // {
       //     context.Update(department);
       //     return context.SaveChanges();
       // }
    }
}
