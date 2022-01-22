using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Entities
{
    public class Library
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public List<Employee> Employees { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}
