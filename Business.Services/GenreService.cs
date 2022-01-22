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
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _repository;
        private readonly IMapper _mapper;
        public GenreService(IGenreRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public GenreDto CreateGenre(GenreDto genre)
        {
            var entity = _mapper.Map<Genre>(genre);
            _repository.CreateOrUpdate(entity);
            return _mapper.Map<GenreDto>(entity);
        }

        public void DeleteGenre(GenreDto genre)
        {
            var entity = _mapper.Map<Genre>(genre);
            _repository.Delete(entity);
        }

        public void DeleteGenreById(int id)
        {
            _repository.Delete(_repository.Read(id));
        }

        public GenreDto FindById(int id)
        {
            return _mapper.Map<GenreDto>(_repository.Read(id));
        }

        public GenreDto FindByName(string name)
        {
            var genres = _repository.Query()
                .Where(i => i.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase));
            return _mapper.Map<GenreDto>(genres);
        }

        public IEnumerable<GenreDto> GetAll()
        {
            return _mapper.Map<IEnumerable<GenreDto>>(_repository.Query());
        }

        public GenreDto UpdateGenre(GenreDto genre)
        {
            var entity = _mapper.Map<Genre>(genre);
            _repository.Update(entity);
            return _mapper.Map<GenreDto>(entity);
        }
    }
}
