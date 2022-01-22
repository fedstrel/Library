using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Business.Entities;
using Business.Repositories.DataRepositories;
using Repository.Data;

namespace Repository.Repositories
{
    public class GenreRepository : AbstractRepository<Genre, int>, IGenreRepository
    {
        public GenreRepository(Context context)
        {
            _context = context;
            table = context.Set<Genre>();
        }
        protected override int KeySelector(Genre value)
        {
            return value.Id;
        }

        protected override IQueryable<Genre> QueryImplementation()
        {
            return _context.Genres;
        }

        protected override Genre ReadImplementation(int key)
        {
            return QueryImplementation().FirstOrDefault(i => i.Id == key);
        }
    }
}
