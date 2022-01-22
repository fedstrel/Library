using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Pages { get; set; }
        public Reader Reader { get; set; }
        public int? ReaderId { get; set; }
        public Library Library { get; set; }
        public int? LibraryId { get; set; }
        public List<Author> Authors { get; set; } = new List<Author>();
        public List<Genre> Genres { get; set; } = new List<Genre>();
    }
}
