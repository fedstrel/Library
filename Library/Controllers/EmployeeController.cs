using Business.Interop.Dto;
using Business.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Library.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILibraryService _libraryService;
        public EmployeeController(IEmployeeService employeeService,
                            ILibraryService libraryService)
        {
            _employeeService = employeeService;
            _libraryService = libraryService;
        }

        public ActionResult Index()
        {
            var employees = _employeeService.GetAll();
            return View(employees);
        }

        [HttpGet]
        public ActionResult Add(int? id)
        {
            var model = new EmployeeDto();

            if (id != null)
                model = _employeeService.FindById(id.Value);

            ViewData["LibraryIds"] = new SelectList(_libraryService.GetAll(),
                nameof(LibraryDto.Id),
                nameof(LibraryDto.Address),
                model.LibraryId);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(EmployeeDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewData["LibraryIds"] = new SelectList(_libraryService.GetAll(),
                    nameof(LibraryDto.Id),
                    nameof(LibraryDto.Address),
                    dto.LibraryId);

                return View(dto);
            }
            dto.Library = _libraryService.FindById(dto.LibraryId);
            if (_employeeService.FindById(dto.Id) == null)
                await _employeeService.CreateAsyncEmployee(dto);
            else
                await _employeeService.UpdateAsyncEmployee(dto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Delete(int id)
        {
            if (_employeeService.FindById(id) == null)
            {
                return NotFound();
            }
            await _employeeService.DeleteEmployeeAsyncById(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
