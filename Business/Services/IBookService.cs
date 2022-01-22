using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Business.Interop.Dto;

namespace Business.Services
{
    public interface IBookService
    {
        public BookDto CreateBook(BookDto book);
        public Task CreateAsyncBook(BookDto book);
        public BookDto UpdateBook(BookDto book);
        public Task UpdateAsyncBook(BookDto book);
        public void DeleteBook(BookDto book);
        public void DeleteBookById(int id);
        public Task DeleteBookAsyncById(int id);
        public IEnumerable<BookDto> GetAll();
        public IEnumerable<AuthorDto> GetAllAuthorsByBookId(int id);
        public IEnumerable<GenreDto> GetAllGenresByBookId(int id);
        public IEnumerable<BookDto> GetAllFreeBooks();
        public BookDto FindById(int id); 
        public IEnumerable<BookDto> FindByName(string name);
        public IEnumerable<BookDto> FindByLibrary(LibraryDto library);
        public IEnumerable<BookDto> FindByReader(ReaderDto reader);
    }
}
