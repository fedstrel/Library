using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Entities;

namespace Business.Repositories.DataRepositories
{
    public interface IBookRepository : IRepository<Book, int>
    {
    }
}
