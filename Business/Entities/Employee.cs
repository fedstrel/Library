using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int Salary { get; set; }
        public Library Library { get; set; }
        public int? LibraryId { get; set; }
    }
}
