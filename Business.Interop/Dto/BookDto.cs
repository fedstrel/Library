using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interop.Dto
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Pages { get; set; }
        public ReaderDto Reader { get; set; }
        public int ReaderId { get; set; }
        public LibraryDto Library { get; set; }
        public int LibraryId { get; set; }
        public List<GenreDto> Genres { get; set; }
        public List<int> GenreIds { get; set; } = new List<int>();
        public List<AuthorDto> Authors { get; set; }
        public List<int> AuthorIds { get; set; } = new List<int>();
    }
}
