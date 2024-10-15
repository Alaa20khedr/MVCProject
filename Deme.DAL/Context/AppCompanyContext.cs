using Deme.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using static Humanizer.In;

namespace Deme.DAL.Context
{
    public class AppCompanyContext :IdentityDbContext<ApplicationUser , ApplicationRole,string>
    {
        public AppCompanyContext(DbContextOptions<AppCompanyContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("server =.; database = AppCompanyDb; trusted_connection = true");
        }
       public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

    }
}
