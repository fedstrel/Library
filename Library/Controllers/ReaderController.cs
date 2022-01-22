using Business.Interop.Dto;
using Business.Services;
using Library.Models;

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Controllers
{
    public class ReaderController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IReaderService _readerService;
        public ReaderController(IBookService bookService,
                            IReaderService readerService)
        {
            _bookService = bookService;
            _readerService = readerService;
        }
        public ActionResult Index()
        {
            var readers = _readerService.GetAll();
            return View(readers);
        }
        [HttpGet]
        public ActionResult Overlook(int id)
        {
            List<string> bookNames = new List<string>();
            ReaderDto dto = _readerService.FindById(id);
            List<BookDto> books = new List<BookDto>(_bookService.FindByReader(dto));
            foreach (BookDto book in books)
                bookNames.Add(book.Name);

            ReaderOverlookModel model = new ReaderOverlookModel()
            {
                Reader = dto,
                BookNames = bookNames
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult Add(int? id)
        {
            var model = new ReaderDto();

            if (id != null)
                model = _readerService.FindById(id.Value);

            ViewData["AvailableBookIds"] = new MultiSelectList(_bookService.GetAllFreeBooks(),
                nameof(BookDto.Id),
                nameof(BookDto.Name),
                model.BookIds);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(ReaderDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewData["AvailableBookIds"] = new MultiSelectList(_bookService.GetAllFreeBooks(),
                    nameof(BookDto.Id),
                    nameof(BookDto.Name),
                    dto.BookIds);

                return View(dto);
            }
            List<BookDto> books = new List<BookDto>();
            BookDto tmp = new BookDto();
            foreach (int id in dto.BookIds)
                books.Add(_bookService.FindById(id));
            dto.Books = books;
            if (_bookService.FindById(dto.Id) == null)
                await _readerService.CreateAsyncReader(dto);
            else
                await _readerService.UpdateAsyncReader(dto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Delete(int id)
        {
            if (_readerService.FindById(id) == null)
            {
                return NotFound();
            }
            await _readerService.DeleteReaderAsyncById(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
