﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Book> Books { get; set; } = new List<Book>();
    }
}
