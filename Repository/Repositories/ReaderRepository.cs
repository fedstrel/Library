using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Business.Entities;
using Business.Repositories.DataRepositories;
using Repository.Data;

namespace Repository.Repositories
{
    public class ReaderRepository : AbstractRepository<Reader, int>, IReaderRepository
    {
        public ReaderRepository(Context context)
        {
            _context = context;
            table = context.Set<Reader>();
        }
        protected override int KeySelector(Reader value)
        {
            return value.Id;
        }

        protected override IQueryable<Reader> QueryImplementation()
        {
            return _context.Readers;
        }

        protected override Reader ReadImplementation(int key)
        {
            return QueryImplementation().FirstOrDefault(i => i.Id == key);
        }
    }
}
