using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Business.Entities;
using Business.Repositories.DataRepositories;
using Repository.Data;

namespace Repository.Repositories
{
    public class AuthorRepository : AbstractRepository<Author, int>, IAuthorRepository
    {
        public AuthorRepository(Context context)
        {
            _context = context;
            table = context.Set<Author>();
        }
        protected override int KeySelector(Author value)
        {
            return value.Id;
        }

        protected override IQueryable<Author> QueryImplementation()
        {
            return _context.Authors;
        }

        protected override Author ReadImplementation(int key)
        {
            return QueryImplementation().FirstOrDefault(i => i.Id == key);
        }
    }
}
