using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interop.Dto
{
    public class ReaderDto
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public IEnumerable<BookDto> Books { get; set; }
        public List<int> BookIds { get; set; } = new List<int>();
    }
}
