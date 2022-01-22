using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interop.Dto
{
    public class LibraryDto
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public IEnumerable<EmployeeDto> Employees { get; set; }
        public IEnumerable<int> EmployeeIds { get; set; }
        public IEnumerable<BookDto> Books { get; set; }
        public IEnumerable<int> BookIds { get; set; }
    }
}
