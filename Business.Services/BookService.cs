using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using Business.Entities;
using Business.Interop.Dto;
using Business.Repositories.DataRepositories;
using System.Linq;

namespace Business.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;
        public BookService(
            IBookRepository repository, 
            IAuthorRepository authorRepository, 
            IGenreRepository genreRepository, 
            IMapper mapper)
        {
            _repository = repository;
            _authorRepository = authorRepository;
            _genreRepository = genreRepository;
            _mapper = mapper;
        }
        public BookDto CreateBook(BookDto book)
        {
            var entity = _mapper.Map<Book>(book);
            _repository.CreateOrUpdate(entity);
            return _mapper.Map<BookDto>(entity);
        }

        public Task CreateAsyncBook(BookDto book)
        {
            var entity = new Book();
            entity.Pages = book.Pages;
            entity.Name = book.Name;
            entity.LibraryId = book.LibraryId;
            if (book.ReaderId != 0)
                entity.ReaderId = book.ReaderId;
            foreach (AuthorDto author in book.Authors)
                entity.Authors.Add(_authorRepository.Read(author.Id));
            foreach (GenreDto genre in book.Genres)
                entity.Genres.Add(_genreRepository.Read(genre.Id));
            return _repository.CreateAsync(entity); 
        }

        public Task UpdateAsyncBook(BookDto book)
        {
            var entity = _repository.Read(book.Id);

            List<Author> authors = new List<Author>();
            List<Genre> genres = new List<Genre>();
            foreach (AuthorDto author in book.Authors)
                authors.Add(_authorRepository.Read(author.Id));
            foreach (GenreDto genre in book.Genres)
                genres.Add(_genreRepository.Read(genre.Id));

            entity.Authors = authors;
            entity.Genres = genres;
            entity.Pages = book.Pages;
            entity.LibraryId = book.LibraryId;
            if (book.ReaderId != 0)
                entity.ReaderId = book.ReaderId;

            return _repository.UpdateAsync(entity);
        }

        public void DeleteBook(BookDto book)
        {
            var entity = _mapper.Map<Book>(book);
            _repository.Delete(entity);
        }

        public void DeleteBookById(int id)
        {
            _repository.Delete(_repository.Read(id));
        }

        public Task DeleteBookAsyncById(int id)
        {
            return _repository.DeleteAsync(_repository.Read(id));
        }

        public BookDto FindById(int id)
        {
            BookDto res = _mapper.Map<BookDto>(_repository.Read(id));
            if (res != null)
            {
                foreach (GenreDto genre in res.Genres)
                    res.GenreIds.Add(genre.Id);
                foreach (AuthorDto author in res.Authors)
                    res.AuthorIds.Add(author.Id);
            }
            return res;
        }

        public IEnumerable<BookDto> FindByLibrary(LibraryDto library)
        {
            var books = _repository.Query()
               .Where(i => i.Library.Id == library.Id);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public IEnumerable<BookDto> FindByName(string name)
        {
            var books = _repository.Query()
               .Where(i => i.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase));
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public IEnumerable<BookDto> FindByReader(ReaderDto reader)
        {
            var books = _repository.Query()
               .Where(i => i.ReaderId == reader.Id);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public IEnumerable<BookDto> GetAll()
        {
            return _mapper.Map<IEnumerable<BookDto>>(_repository.Query());
        }
        public IEnumerable<BookDto> GetAllFreeBooks()
        {
            var books = _repository.Query()
                 .Where(i => i.ReaderId == null);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }
        public IEnumerable<AuthorDto> GetAllAuthorsByBookId(int id)
        {
            var authors = _authorRepository.Query()
                .Where((i) => { 
                    foreach (Book book in i.Books) 
                        if (book.Id == id) 
                            return true;
                    return false;
                });
            return _mapper.Map<IEnumerable<AuthorDto>>(authors);
        }

        public IEnumerable<GenreDto> GetAllGenresByBookId(int id)
        {
            var genres = _genreRepository.Query()
                .Where((i) => {
                    foreach (Book book in i.Books)
                        if (book.Id == id)
                            return true;
                    return false;
                });
            return _mapper.Map<IEnumerable<GenreDto>>(genres);
        }

        public BookDto UpdateBook(BookDto book)
        {
            var entity = _mapper.Map<Book>(book);
            _repository.Update(entity);
            return _mapper.Map<BookDto>(entity);
        }
    }
}
