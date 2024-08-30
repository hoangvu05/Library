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
    public class ChangeEmployeeController : Controller
    {
        private readonly LibraryContext _context;

        public ChangeEmployeeController(LibraryContext context)
        {
            _context = context;
        }

        //[AdminstratorAuthorize]
        [AdminstratorAuthorize]
        [HttpGet]
        public async Task<IActionResult> Index(int idEmployee)
        {
            //Console.WriteLine(idbook);

            var employee = from book in _context.Employees select book;
            employee = employee.Where(book => book.EmployeeId == idEmployee);

            var allFilter = new AllFilter
            {
                //Grade = new SelectList(await (from m in enrollmentQuery select m.Grade).Distinct().ToListAsync()),
                //Title = new SelectList(await (from m in courseQuery select m.Title).Distinct().ToListAsync()),
                Employee=employee.ToList(),
                Id = idEmployee,
            };



            return View(allFilter);
        }

        //[AdminstratorAuthorize]
        [AdminstratorAuthorize]
        [HttpPost]
        public async Task<IActionResult> Index(int? idEmployee, [Bind("FirstName", "LastName", "Job", "Image", "Salary", "Description")] Employee employee, IFormFile? imgFile)
        {

            try
            {
                if (employee.FirstName != null && employee.LastName != null && employee.Job != null && employee.Salary != null && employee.Description != null)
                {
                    var update = _context.Employees.Find(idEmployee);
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

                        return RedirectToAction("Index", "Employee");
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