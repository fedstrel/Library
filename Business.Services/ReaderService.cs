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
    public class ReaderService : IReaderService
    {
        private readonly IReaderRepository _repository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public ReaderService(IReaderRepository repository, IBookRepository bookRepository, IMapper mapper)
        {
            _repository = repository;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        public ReaderDto CreateReader(ReaderDto reader)
        {
            var entity = _mapper.Map<Reader>(reader);
            _repository.CreateOrUpdate(entity);
            return _mapper.Map<ReaderDto>(reader);
        }

        public Task CreateAsyncReader(ReaderDto reader)
        {
            var entity = new Reader();

            entity.Firstname = reader.Firstname;
            entity.Lastname = reader.Lastname;
            foreach (BookDto book in reader.Books)
                entity.Books.Add(_bookRepository.Read(book.Id));

            return _repository.CreateAsync(entity);
        }

        public Task UpdateAsyncReader(ReaderDto reader)
        {
            var entity = _repository.Read(reader.Id);

            entity.Firstname = reader.Firstname;
            entity.Lastname = reader.Lastname;
            foreach (BookDto book in reader.Books)
                entity.Books.Add(_bookRepository.Read(book.Id));

            return _repository.UpdateAsync(entity);
        }

        public void DeleteReader(ReaderDto reader)
        {
            var entity = _mapper.Map<Reader>(reader);
            _repository.Delete(entity);
        }

        public void DeleteReaderById(int id)
        {
            _repository.Delete(_repository.Read(id));
        }

        public Task DeleteReaderAsyncById(int id)
        {
            return _repository.DeleteAsync(_repository.Read(id));
        }

        public IEnumerable<ReaderDto> FindByFirstname(string name)
        {
            var readers = _repository.Query()
                .Where(i => i.Firstname.Contains(name, StringComparison.InvariantCultureIgnoreCase));
            return _mapper.Map<IEnumerable<ReaderDto>>(readers);
        }

        public ReaderDto FindById(int id)
        {
            return _mapper.Map<ReaderDto>(_repository.Read(id));
        }

        public IEnumerable<ReaderDto> FindByLastname(string name)
        {
            var readers = _repository.Query()
                .Where(i => i.Lastname.Contains(name, StringComparison.InvariantCultureIgnoreCase));
            return _mapper.Map<IEnumerable<ReaderDto>>(readers);
        }

        public IEnumerable<ReaderDto> GetAll()
        {
            return _mapper.Map<IEnumerable<ReaderDto>>(_repository.Query());
        }

        public IEnumerable<BookDto> GetAllBooksByReaderId(int id)
        {
            var books = _bookRepository.Query()
                .Where(i => i.Reader.Id == id);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public ReaderDto UpdateReader(ReaderDto reader)
        {
            var entity = _mapper.Map<Reader>(reader);
            _repository.Update(entity);
            return _mapper.Map<ReaderDto>(reader);
        }
    }
}
