using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Business.Entities;
using Business.Repositories.DataRepositories;
using Repository.Data;

namespace Repository.Repositories
{
    public class LibraryRepository : AbstractRepository<Library, int>, ILibraryRepository
    {
        public LibraryRepository(Context context)
        {
            _context = context;
            table = context.Set<Library>();
        }
        protected override int KeySelector(Library value)
        {
            return value.Id;
        }

        protected override IQueryable<Library> QueryImplementation()
        {
            return _context.Libraries.Include(x => x.Employees).Include(x => x.Books);
        }

        protected override Library ReadImplementation(int key)
        {
            return QueryImplementation().FirstOrDefault(i => i.Id == key);
        }
    }
}
