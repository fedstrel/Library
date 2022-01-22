using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Business.Entities;
using Business.Repositories.DataRepositories;
using Repository.Data;

namespace Repository.Repositories
{
    public class BookRepository : AbstractRepository<Book, int>, IBookRepository
    {
        public BookRepository(Context context)
        {
            _context = context;
            table = context.Set<Book>();
        }
        protected override int KeySelector(Book value)
        {
            return value.Id;
        }
        protected override IQueryable<Book> QueryImplementation()
        {
            return _context.Books
                .Include((x) => x.Library)
                .Include((x) => x.Reader)
                .Include((x) => x.Genres)
                .Include((x) => x.Authors);
        }

        protected override Book ReadImplementation(int key)
        {
            return QueryImplementation().FirstOrDefault(i => i.Id == key);
        }
    }
}
