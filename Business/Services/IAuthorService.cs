using System;
using System.Collections.Generic;
using System.Text;

using Business.Interop.Dto;

namespace Business.Services
{
    public interface IAuthorService
    {
        public AuthorDto CreateAuthor(AuthorDto author);
        public AuthorDto UpdateAuthor(AuthorDto author);
        public void DeleteAuthor(AuthorDto author);
        public void DeleteAuthorById(int id);
        public IEnumerable<AuthorDto> GetAll();
        public AuthorDto FindById(int id);
        public IEnumerable<AuthorDto> FindByName(string name);
    }
}
