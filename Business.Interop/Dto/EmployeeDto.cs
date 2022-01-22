using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interop.Dto
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int Salary { get; set; }
        public LibraryDto Library { get; set; }
        public int LibraryId { get; set; }
    }
}
