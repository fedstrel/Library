using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Entities
{
    public class Reader
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
    }
}
