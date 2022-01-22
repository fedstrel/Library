using System;
using System.Collections.Generic;
using System.Text;
using Business.Entities;

namespace Business.Repositories.DataRepositories
{
    public interface IEmployeeRepository : IRepository<Employee, int>
    {
    }
}
