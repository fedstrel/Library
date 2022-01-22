using System;
using System.Collections.Generic;
using System.Text;

using AutoMapper;

using Business.Entities;
using Business.Interop.Dto;
using Business.Repositories.DataRepositories;
using System.Linq;

namespace Business.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repository;
        private IMapper _mapper;
        public AuthorService(IAuthorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public AuthorDto CreateAuthor(AuthorDto author)
        {
            var entity = _mapper.Map<Author>(author);
            _repository.CreateOrUpdate(entity);
            return _mapper.Map<AuthorDto>(entity);
        }

        public void DeleteAuthor(AuthorDto author)
        {
            var entity = _mapper.Map<Author>(author);
            _repository.Delete(entity);
        }

        public void DeleteAuthorById(int id)
        {
            _repository.Delete(_repository.Read(id));
        }

        public AuthorDto FindById(int id)
        {
           return _mapper.Map<AuthorDto>(_repository.Read(id));
        }

        public IEnumerable<AuthorDto> FindByName(string name)
        {
            var authors = _repository.Query()
                .Where(i => i.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase));
            return _mapper.Map<IEnumerable<AuthorDto>>(authors);
        }

        public IEnumerable<AuthorDto> GetAll()
        {
            return _mapper.Map<IEnumerable<AuthorDto>>(_repository.Query());
        }

        public AuthorDto UpdateAuthor(AuthorDto author)
        {
            var entity = _mapper.Map<Author>(author);
            _repository.Update(entity);
            return _mapper.Map<AuthorDto>(entity);
        }
    }
}
