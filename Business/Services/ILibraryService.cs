using System;
using System.Collections.Generic;
using System.Text;

using Business.Interop.Dto;

namespace Business.Services
{
    public interface ILibraryService
    {
        public LibraryDto CreateLibrary(LibraryDto library);
        public LibraryDto UpdateLibrary(LibraryDto library);
        public void DeleteLibrary(LibraryDto library);
        public void DeleteLibraryById(int id);
        public IEnumerable<LibraryDto> GetAll();
        public IEnumerable<EmployeeDto> GetAllEmployeesByLibraryId(int id);
        public IEnumerable<BookDto> GetAllBooksByLibraryId(int id);
        public LibraryDto FindById(int id);
    }
}
