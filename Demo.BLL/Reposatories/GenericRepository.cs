using Deme.DAL.Context;
using Deme.DAL.Entities;
using Demo.BLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Reposatories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppCompanyContext context;

        public GenericRepository(AppCompanyContext context) {
            this.context = context;
        }
        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
           // return context.SaveChanges();
        }

        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
            //return context.SaveChanges();

        }

        public IEnumerable<T> GetAll()
       =>context.Set<T>().ToList();

        public T GetById(int? id)
        => context.Set<T>().Find(id);

        public void Update(T entity)
        {
            context.Set<T>().Update(entity);
           // return context.SaveChanges();
        }
    }
}
