using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Business.Interop.Dto;

namespace Business.Services
{
    public interface IEmployeeService
    {
        public EmployeeDto CreateEmployee(EmployeeDto employee);
        public Task CreateAsyncEmployee(EmployeeDto employee);
        public EmployeeDto UpdateEmployee(EmployeeDto employee);
        public Task UpdateAsyncEmployee(EmployeeDto employee);
        public void DeleteEmployee(EmployeeDto employee);
        public void DeleteEmployeeById(int id);
        public Task DeleteEmployeeAsyncById(int id);
        public IEnumerable<EmployeeDto> GetAll();
        public EmployeeDto FindById(int id);
        public IEnumerable<EmployeeDto> FindByFirstname(string firstname);
        public IEnumerable<EmployeeDto> FindByLastname(string lastname);
        public IEnumerable<EmployeeDto> FindByLibrary(LibraryDto library);
    }
}
