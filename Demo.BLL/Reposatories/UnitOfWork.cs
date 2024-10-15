using Deme.DAL.Context;
using Demo.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Reposatories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppCompanyContext context;

        public IDepartmentRepository DepartmentRepository { get; set; }
        public IEmployeeRepository EmployeeRepository { get; set; }

        public UnitOfWork(AppCompanyContext context) {
            this.context = context;
            DepartmentRepository = new DepartmentRepository(context);
            EmployeeRepository =new EmployeeRepository(context);
       
        }

        public int complete()
        {
            return context.SaveChanges();
        }
    }
}
