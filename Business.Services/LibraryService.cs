using System;
using System.Collections.Generic;
using System.Text;

using AutoMapper;

using Business.Entities;
using Business.Interop.Dto;
using Business.Repositories.DataRepositories;
using System.Linq;

namespace Business.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly ILibraryRepository _repository;
        private readonly IBookRepository _bookRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public LibraryService(
            ILibraryRepository repository,
            IBookRepository bookRepository,
            IEmployeeRepository employeeRepository,
            IMapper mapper)
        {
            _repository = repository;
            _bookRepository = bookRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        public LibraryDto CreateLibrary(LibraryDto library)
        {
            var entity = _mapper.Map<Library>(library);
            _repository.CreateOrUpdate(entity);
            return _mapper.Map<LibraryDto>(library);
        }

        public void DeleteLibrary(LibraryDto library)
        {
            var entity = _mapper.Map<Library>(library);
            _repository.Delete(entity);
        }

        public void DeleteLibraryById(int id)
        {
            _repository.Delete(_repository.Read(id));
        }

        public LibraryDto FindById(int id)
        {
            return _mapper.Map<LibraryDto>(_repository.Read(id));
        }

        public IEnumerable<LibraryDto> GetAll()
        {
            return _mapper.Map<IEnumerable<LibraryDto>>(_repository.Query());
        }

        public IEnumerable<BookDto> GetAllBooksByLibraryId(int id)
        {
            var books = _bookRepository.Query()
                .Where(i => i.Library.Id == id);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public IEnumerable<EmployeeDto> GetAllEmployeesByLibraryId(int id)
        {
            var employees = _bookRepository.Query()
                .Where(i => i.Library.Id == id);
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public LibraryDto UpdateLibrary(LibraryDto library)
        {
            var entity = _mapper.Map<Library>(library);
            _repository.Update(entity);
            return _mapper.Map<LibraryDto>(library);
        }
    }
}
