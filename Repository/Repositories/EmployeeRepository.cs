using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

using Business.Entities;
using Business.Repositories.DataRepositories;
using Repository.Data;

namespace Repository.Repositories
{
    public class EmployeeRepository : AbstractRepository<Employee, int>, IEmployeeRepository
    {
        public EmployeeRepository(Context context)
        {
            _context = context;
            table = _context.Set<Employee>();
        }
        protected override int KeySelector(Employee value)
        {
            return value.Id;
        }

        protected override IQueryable<Employee> QueryImplementation()
        {
            return _context.Employees.Include((x) => x.Library);
        }

        protected override Employee ReadImplementation(int key)
        {
            return QueryImplementation().FirstOrDefault(i => i.Id == key);
        }
    }
}
