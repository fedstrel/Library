using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using Business.Entities;
using Business.Interop.Dto;
using Business.Repositories.DataRepositories;
using System.Linq;

namespace Business.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;
        public EmployeeService(IEmployeeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public EmployeeDto CreateEmployee(EmployeeDto employee)
        {
            var entity = _mapper.Map<Employee>(employee);
            _repository.CreateOrUpdate(entity);
            return _mapper.Map<EmployeeDto>(entity);
        }

        public Task CreateAsyncEmployee(EmployeeDto employee)
        {
            var entity = new Employee();

            entity.Firstname = employee.Firstname;
            entity.Lastname = employee.Lastname;
            entity.Salary = employee.Salary;
            entity.LibraryId = employee.LibraryId;

            return _repository.CreateAsync(entity);
        }

        public Task UpdateAsyncEmployee(EmployeeDto employee)
        {
            var entity = _repository.Read(employee.Id);

            entity.Firstname = employee.Firstname;
            entity.Lastname = employee.Lastname;
            entity.Salary = employee.Salary;
            entity.LibraryId = employee.LibraryId;

            return _repository.UpdateAsync(entity);
        }

        public void DeleteEmployee(EmployeeDto employee)
        {
            var entity = _mapper.Map<Employee>(employee);
            _repository.Delete(entity);
        }

        public void DeleteEmployeeById(int id)
        {
            _repository.Delete(_repository.Read(id));
        }

        public Task DeleteEmployeeAsyncById(int id)
        {
            return _repository.DeleteAsync(_repository.Read(id));
        }

        public IEnumerable<EmployeeDto> FindByFirstname(string firstname)
        {
            var employees = _repository.Query()
                .Where(i => i.Firstname.Contains(firstname, StringComparison.InvariantCultureIgnoreCase));
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public EmployeeDto FindById(int id)
        {
            return _mapper.Map<EmployeeDto>(_repository.Read(id));
        }

        public IEnumerable<EmployeeDto> FindByLastname(string lastname)
        {
            var employees = _repository.Query()
                .Where(i => i.Lastname.Contains(lastname, StringComparison.InvariantCultureIgnoreCase));
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public IEnumerable<EmployeeDto> FindByLibrary(LibraryDto library)
        {
            var employees = _repository.Query()
                .Where(i => i.Library.Id == library.Id);
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public IEnumerable<EmployeeDto> GetAll()
        {
            return _mapper.Map<IEnumerable<EmployeeDto>>(_repository.Query());
        }

        public EmployeeDto UpdateEmployee(EmployeeDto employee)
        {
            var entity = _mapper.Map<Employee>(employee);
            _repository.Update(entity);
            return _mapper.Map<EmployeeDto>(entity);
        }
    }
}
