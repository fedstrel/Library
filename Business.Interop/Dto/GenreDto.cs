using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interop.Dto
{
    public class GenreDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<BookDto> Books { get; set; }
    }
}
