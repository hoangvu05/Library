using Library.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Library.Authorization;
using Library.Data;
using Library.Services;
using AutoMapper.Configuration.Conventions;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using System.Runtime.Intrinsics.Arm;

namespace Library.Controllers
{
    public class VerificationController : Controller
    {
        private readonly LibraryContext _libraryContext;

        public VerificationController(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        /*[Authorize]
        [HttpPost]
        public async Task<IActionResult> Return()
        {
            return RedirectToAction("Index","Your");
        }*/

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index(int? page, string searchbook, string? currentFilter, List<Genre> Genre, int[] idG, List<Author> Author, int[] idA, List<Year> Year, int[] idY, List<Publisher> Publisher, int[] idP, string selectSort)
        {

            if (selectSort == null) selectSort = ViewBag.Sort;
            if (searchbook != null) page = 1;
            else searchbook = currentFilter;

            ViewBag.CurrentFilter = searchbook;

            var books = from book in _libraryContext.Books select book;
            books = books.Where(book => book.Status == false || (book.Status == null && book.UserId != null));
            if (!String.IsNullOrEmpty(searchbook))
            {
                books = books.Where(book => book.Title.Contains(searchbook));
            }

            if (books != null)
            {
                if (selectSort == "titleAscend") books = books.OrderBy(book => book.Title);
                else if (selectSort == "titleDescend") books = books.OrderByDescending(book => book.Title);
                else if (selectSort == "authorAscend") books = books.OrderBy(book => book.Author);
                else if (selectSort == "authorDescend") books = books.OrderByDescending(book => book.Author);
                else if (selectSort == "yearAscend") books = books.OrderBy(book => book.ReleaseYear);
                else if (selectSort == "yearDescend") books = books.OrderByDescending(book => book.ReleaseYear);
                else if (selectSort == "default") books = books.OrderBy(book => book.Id);
            }
            ViewBag.Sort = selectSort;

            IQueryable<Genre> genres = from genre in _libraryContext.Genres select genre;
            IQueryable<Author> authors = from author in _libraryContext.Authors select author;
            IQueryable<Year> years = from year in _libraryContext.Years select year;
            IQueryable<Publisher> publishers = from publisher in _libraryContext.Publishers select publisher;


            ViewBag.i = 0;
            ViewBag.pageSize = 5;
            var allFilter = new AllFilter
            {
                //Grade = new SelectList(await (from m in enrollmentQuery select m.Grade).Distinct().ToListAsync()),
                //Title = new SelectList(await (from m in courseQuery select m.Title).Distinct().ToListAsync()),
                PaginatedList = await PaginatedList<Book>.CreateAsync(books, page ?? 1, ViewBag.pageSize),
                Genre = genres.ToList(),
                Author = authors.ToList(),
                Year = years.ToList(),
                Publisher = publishers.ToList(),
            };



            return View(allFilter);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Index(int? page, string searchbook, string? currentFilter, List<Genre> Genre, int[] idG, List<Author> Author, int[] idA, List<Year> Year, int[] idY, List<Publisher> Publisher, int[] idP, string selectOption, string selectSort, string x)
        {
            if (selectSort == null) selectSort = ViewBag.Sort;
            if (selectOption == null) selectOption = ViewBag.Option;
            if (searchbook != null) page = 1;
            else searchbook = currentFilter;
            ViewBag.CurrentFilter = searchbook;

            var books = from book in _libraryContext.Books select book;
            books = books.Where(book => book.Status == false || (book.Status==null && book.UserId!=null));
            if (!String.IsNullOrEmpty(searchbook))
            {
                if (selectOption == "quick") books = books.Where(book => (book.Title.Contains(searchbook) || book.Author.Contains(searchbook)
                || book.Genre.Contains(searchbook) || book.ISBN.Contains(searchbook) || book.ReleaseYear.Contains(searchbook)
                ));
                else if (selectOption == "title") books = books.Where(book => book.Title.Contains(searchbook));
                else if (selectOption == "author") books = books.Where(book => book.Author.Contains(searchbook));
                else if (selectOption == "genre") books = books.Where(book => book.Genre.Contains(searchbook));
                else if (selectOption == "isbn") books = books.Where(book => book.ISBN.Contains(searchbook));
                else if (selectOption == "yearpub") books = books.Where(book => book.ReleaseYear.Contains(searchbook));
            }


            if (idG.Length > 0)
            {
                var booksX = books;
                int j = 0;
                string[] a = { };
                foreach (var item in Genre)
                {
                    if (idG.Length == j) break;
                    if (item.GenreId == idG[j])
                    {
                        j++;
                        booksX = booksX.Where(book => !book.Genre.Contains(item.Vietnamese));
                    }
                }

                books = books.Except(booksX);
                //Console.WriteLine(a);
            }

            //ViewBag.Genre = Genre;


            if (idA.Length > 0)
            {
                var booksX = books;
                int j = 0;
                string[] a = { };
                foreach (var item in Author)
                {
                    if (idA.Length == j) break;
                    if (item.AuthorId == idA[j])
                    {
                        j++;
                        booksX = booksX.Where(book => !book.Author.Contains(item.Authors));
                    }
                }

                books = books.Except(booksX);
                //Console.WriteLine(a);
            }


            if (idY.Length > 0)
            {
                var booksX = books;
                int j = 0;
                string[] a = { };
                foreach (var item in Year)
                {
                    if (idY.Length == j) break;
                    if (item.YearId == idY[j])
                    {
                        j++;
                        booksX = booksX.Where(book => !book.ReleaseYear.Contains(item.Years));
                    }
                }

                books = books.Except(booksX);
                //Console.WriteLine(a);
            }



            if (idP.Length > 0)
            {
                var booksX = books;
                int j = 0;
                string[] a = { };
                foreach (var item in Publisher)
                {
                    if (idP.Length == j) break;
                    if (item.PublisherId == idP[j])
                    {
                        j++;
                        booksX = booksX.Where(book => !book.Publishing.Contains(item.Publishers));
                    }
                }

                books = books.Except(booksX);
                //Console.WriteLine(a);
            }

            if (books != null)
            {
                if (selectSort == "titleAscend") books = books.OrderBy(book => book.Title);
                else if (selectSort == "titleDescend") books = books.OrderByDescending(book => book.Title);
                else if (selectSort == "authorAscend") books = books.OrderBy(book => book.Author);
                else if (selectSort == "authorDescend") books = books.OrderByDescending(book => book.Author);
                else if (selectSort == "yearAscend") books = books.OrderBy(book => book.ReleaseYear);
                else if (selectSort == "yearDescend") books = books.OrderByDescending(book => book.ReleaseYear);
                else if (selectSort == "default") books = books.OrderBy(book => book.Id);
            }
            ViewBag.Sort = selectSort;
            ViewBag.Option = selectOption;
            ViewBag.idG = idG;
            ViewBag.idA = idA;
            ViewBag.idY = idY;
            ViewBag.idP = idP;

            IQueryable<Genre> genres = from genre in _libraryContext.Genres select genre;
            IQueryable<Author> authors = from author in _libraryContext.Authors select author;
            IQueryable<Year> years = from year in _libraryContext.Years select year;
            IQueryable<Publisher> publishers = from publisher in _libraryContext.Publishers select publisher;

            ViewBag.i = 0;
            ViewBag.pageSize = 5;
            var allFilter = new AllFilter
            {
                //Grade = new SelectList(await (from m in enrollmentQuery select m.Grade).Distinct().ToListAsync()),
                //Title = new SelectList(await (from m in courseQuery select m.Title).Distinct().ToListAsync()),
                PaginatedList = await PaginatedList<Book>.CreateAsync(books, page ?? 1, ViewBag.pageSize),
                Genre = genres.ToList(),
                Author = authors.ToList(),
                Year = years.ToList(),
                Publisher = publishers.ToList(),
            };



            return View(allFilter);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Verify(int idbook)
        {
            //var books = from book in _context.books select book;

            var books = _libraryContext.Books.SingleOrDefault(book => book.Id == idbook);
            if (books != null) books.Status = true;

            _libraryContext.SaveChanges();
            return Redirect("Index");
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Return(int idbook)
        {
            //var books = from book in _context.books select book;

            var books = _libraryContext.Books.SingleOrDefault(book => book.Id == idbook);
            if (books != null) books.UserId= null;

            _libraryContext.SaveChanges();
            return Redirect("Index");
        }
    }
}
