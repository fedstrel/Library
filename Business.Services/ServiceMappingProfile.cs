using AutoMapper;

using Business.Entities;
using Business.Interop.Dto;

namespace Business.Services
{
    public class ServiceMappingProfile : Profile
    {
		public ServiceMappingProfile()
		{
			// from entity to dto
			CreateMap<Author, AuthorDto>();
			CreateMap<Book, BookDto>();
			CreateMap<Employee, EmployeeDto>();
			CreateMap<Genre, GenreDto>();
			CreateMap<Library, LibraryDto>();
			CreateMap<Reader, ReaderDto>();


			// from dto to entity
			CreateMap<AuthorDto, Author>();
			CreateMap<BookDto, Book>();
			CreateMap<EmployeeDto, Employee>();
			CreateMap<GenreDto, Genre>();
			CreateMap<LibraryDto, Library>();
			CreateMap<ReaderDto, Reader>();
		}
	}
}
