using Business.Interop.Dto;
using Business.Services;

using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ILibraryService _libraryService;
        private readonly IGenreService _genreService;
        private readonly IAuthorService _authorService;
        public BooksController(IBookService bookService, 
                            ILibraryService libraryService, 
                            IGenreService genreService,
                            IAuthorService authorService)
        {
            _bookService = bookService;
            _libraryService = libraryService;
            _genreService = genreService;
            _authorService = authorService;
        }

        public ActionResult Index()
        {
            var books = _bookService.GetAll();
            return View(books);
        }

        [HttpGet]
        public ActionResult Add(int? id)
        {
            var model = new BookDto();

            if (id != null)
                model = _bookService.FindById(id.Value);

            ViewData["LibraryIds"] = new SelectList(_libraryService.GetAll(),
                nameof(LibraryDto.Id),
                nameof(LibraryDto.Address),
                model.LibraryId);

            ViewData["Genres"] = new MultiSelectList(_genreService.GetAll(), 
                nameof(GenreDto.Id), 
                nameof(GenreDto.Name),
                model.GenreIds.ToArray());

            ViewData["Authors"] = new MultiSelectList(_authorService.GetAll(),
                nameof(AuthorDto.Id),
                nameof(AuthorDto.Name),
                model.AuthorIds.ToArray());

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(BookDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewData["LibraryIds"] = new SelectList(_libraryService.GetAll(),
                    nameof(LibraryDto.Id),
                    nameof(LibraryDto.Address),
                    dto.LibraryId);

                ViewData["Genres"] = new MultiSelectList(_genreService.GetAll(), 
                    nameof(GenreDto.Id), 
                    nameof(GenreDto.Name), 
                    dto.GenreIds);

                ViewData["Authors"] = new MultiSelectList(_authorService.GetAll(),
                     nameof(AuthorDto.Id),
                     nameof(AuthorDto.Name),
                     dto.AuthorIds);

                return View(dto);
            }
            List<GenreDto> genres = new List<GenreDto>();
            List<AuthorDto> authors = new List<AuthorDto>();
            foreach (int id in dto.GenreIds)
                genres.Add(_genreService.FindById(id));
            foreach (int id in dto.AuthorIds)
                authors.Add(_authorService.FindById(id));
            dto.Library = _libraryService.FindById(dto.LibraryId);
            dto.Genres = genres;
            dto.Authors = authors;
            if (_bookService.FindById(dto.Id) == null)
                await _bookService.CreateAsyncBook(dto);
            else
                await _bookService.UpdateAsyncBook(dto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Delete(int id)
        {
            if (_bookService.FindById(id) == null)
            {
                return NotFound();
            }
            await _bookService.DeleteBookAsyncById(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
