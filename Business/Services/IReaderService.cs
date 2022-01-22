using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Business.Interop.Dto;

namespace Business.Services
{
    public interface IReaderService
    {
        public ReaderDto CreateReader(ReaderDto reader);
        public Task CreateAsyncReader(ReaderDto reader);
        public ReaderDto UpdateReader(ReaderDto reader);
        public Task UpdateAsyncReader(ReaderDto reader);
        public void DeleteReader(ReaderDto reader);
        public void DeleteReaderById(int id);
        public Task DeleteReaderAsyncById(int id);
        public IEnumerable<ReaderDto> GetAll();
        public IEnumerable<BookDto> GetAllBooksByReaderId(int id);
        public ReaderDto FindById(int id);
        public IEnumerable<ReaderDto> FindByFirstname(string name);
        public IEnumerable<ReaderDto> FindByLastname(string name);
    }
}
