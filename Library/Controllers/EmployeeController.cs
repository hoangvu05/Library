using Library.Authorization;
using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
namespace Library.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly LibraryContext _context;

        public EmployeeController(LibraryContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index(int? page, string searchEmployee, string? currentFilter, string selectSort)
        {

            if (selectSort == null) selectSort = ViewBag.Sort;
            if (searchEmployee != null) page = 1;
            else searchEmployee = currentFilter;

            ViewBag.CurrentFilter = searchEmployee;

            var token = Request.Cookies["RefreshToken"];

            var user = _context.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));
            var employees = from employee in _context.Employees select employee;



            if (employees != null)
            {
                if (selectSort == "LastNameAscend") employees = employees.OrderBy(book => book.LastName);
                else if (selectSort == "LastNameDescend") employees = employees.OrderByDescending(book => book.LastName);
                else if (selectSort == "FirstNameAscend") employees = employees.OrderBy(book => book.FirstName);
                else if (selectSort == "FirstNameDescend") employees = employees.OrderByDescending(book => book.FirstName);
                else if (selectSort == "JobAscend") employees = employees.OrderBy(book => book.Job);
                else if (selectSort == "JobDescend") employees = employees.OrderByDescending(book => book.Job);
                else if (selectSort == "default") employees = employees.OrderBy(book => book.EmployeeId);
            }

            ViewBag.Sort = selectSort;

            ViewBag.i = 0;
            ViewBag.pageSize = 5;


            return View(await PaginatedList<Employee>.CreateAsync(employees, page ?? 1, ViewBag.pageSize));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Index(int? page, string searchEmployee, string? currentFilter, string selectSort, string selectOption)
        {
            if (selectSort == null) selectSort = ViewBag.Sort;
            if (selectOption == null) selectOption = ViewBag.Option;
            if (searchEmployee != null) page = 1;
            else searchEmployee = currentFilter;
            ViewBag.CurrentFilter = searchEmployee;

            var token = Request.Cookies["RefreshToken"];

            var user = _context.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));



            var employees = from employee in _context.Employees select employee;


            if (!String.IsNullOrEmpty(searchEmployee))
            {
                if (selectOption == "quick") employees = employees.Where(book => book.FirstName.Contains(searchEmployee) || book.LastName.Contains(searchEmployee) || book.Job.Contains(searchEmployee));
                else if (selectOption == "name") employees = employees.Where(book => book.FirstName.Contains(searchEmployee) || book.LastName.Contains(searchEmployee));
                else if (selectOption == "job") employees = employees.Where(book => book.Job.Contains(searchEmployee));
            }


            if (employees != null)
            {
                if (selectSort == "LastNameAscend") employees = employees.OrderBy(book => book.LastName);
                else if (selectSort == "LastNameDescend") employees = employees.OrderByDescending(book => book.LastName);
                else if (selectSort == "FirstNameAscend") employees = employees.OrderBy(book => book.FirstName);
                else if (selectSort == "FirstNameDescend") employees = employees.OrderByDescending(book => book.FirstName);
                else if (selectSort == "JobAscend") employees = employees.OrderBy(book => book.Job);
                else if (selectSort == "JobDescend") employees = employees.OrderByDescending(book => book.Job);
                else if (selectSort == "default") employees = employees.OrderBy(book => book.EmployeeId);
            }
            ViewBag.Sort = selectSort;
            ViewBag.Option = selectOption;


            ViewBag.i = 0;
            ViewBag.pageSize = 5;


            return View(await PaginatedList<Employee>.CreateAsync(employees, page ?? 1, ViewBag.pageSize));
        }

        /*[Authorize]
        [HttpPost]
        public async Task<IActionResult> Return(int idbook)
        {
            //var books = from book in _context.books select book;
            var books = _context.books.SingleOrDefault(book => book.Id == idbook);
            if (books != null)
            {
                books.Status = null;
            }


            _context.SaveChanges();
            return Redirect("Index");
        }*/
    }
}