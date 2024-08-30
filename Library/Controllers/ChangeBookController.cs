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
using System.Reflection;
using System.Data;
using static System.Reflection.Metadata.BlobBuilder;
using System.Security.Policy;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNet.Identity;

namespace Library.Controllers
{
    public class ChangeBookController : Controller
    {
        private readonly LibraryContext _context;

        public ChangeBookController(LibraryContext context)
        {
            _context = context;
        }

        //[AdminstratorAuthorize]
        [AdminstratorAuthorize]
        [HttpGet]
        public async Task<IActionResult> Index(int idbook)
        {
            //Console.WriteLine(idbook);
            var genres = from genre in _context.Genres select genre;

            var books = from book in _context.Books select book;
            books = books.Where(book => book.Id == idbook);

            var allFilter = new AllFilter
            {
                //Grade = new SelectList(await (from m in enrollmentQuery select m.Grade).Distinct().ToListAsync()),
                //Title = new SelectList(await (from m in courseQuery select m.Title).Distinct().ToListAsync()),
                Genre = genres.ToList(),
                Book = books.ToList(),
                Id=idbook,
            };



            return View(allFilter);
        }

        //[AdminstratorAuthorize]
        [AdminstratorAuthorize]
        [HttpPost]
        public async Task<IActionResult> Index(int? idbook,[Bind("Title", "Genre", "Description", "Author", "Image", "ReleaseYear", "Publishing", "ISBN")] Book book, IFormFile? imgFile)
        {

            try
            {
                if (book.Title != null && book.Genre != null && book.Description != null && book.Author != null && book.ReleaseYear != null && book.Publishing != null && book.ISBN != null)
                {
                    var update = _context.Books.Find(idbook);
                    if (imgFile != null && update != null)
                    {
                        byte[] p1 = null;
                        using (var fs1 = imgFile.OpenReadStream())
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                        update.Image = Convert.ToBase64String(p1);
                    }

                    TryUpdateModelAsync(update);
                    try
                    {
                        
                        _context.SaveChanges();

                        return RedirectToAction("IndexAdmin","Search");
                    }
                    catch 
                    {
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                    }
                    /*if (imgFile != null)
                    {
                        byte[] p1 = null;
                        using (var fs1 = imgFile.OpenReadStream())
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                        book.Image = Convert.ToBase64String(p1);
                    }
                    
                    _context.Add(book);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");*/
                }
            }
            catch (DataException /* dex */ )
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View();
        }
    }
}