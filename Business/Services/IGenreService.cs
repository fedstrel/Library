using System;
using System.Collections.Generic;
using System.Text;

using Business.Interop.Dto;

namespace Business.Services
{
    public interface IGenreService
    {
        public GenreDto CreateGenre(GenreDto genre);
        public GenreDto UpdateGenre(GenreDto genre);
        public void DeleteGenre(GenreDto genre);
        public void DeleteGenreById(int id);
        public IEnumerable<GenreDto> GetAll();
        public GenreDto FindById(int id);
        public GenreDto FindByName(string name);
    }
}
