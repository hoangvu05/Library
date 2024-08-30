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

namespace Library.Controllers
{
    public class AddEmployeeController : Controller
    {
        private readonly LibraryContext _context;

        public AddEmployeeController(LibraryContext context)
        {
            _context = context;
        }

        //[AdminstratorAuthorize]
        [AdminstratorAuthorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        //[AdminstratorAuthorize]
        [AdminstratorAuthorize]
        [HttpPost]
        public async Task<IActionResult> Index([Bind("FirstName","LastName","Job","Image","Salary","Description")] Employee employee, IFormFile imgFile)
        {

            try
            {
                if (employee.FirstName != null && employee.LastName != null && employee.Job != null && employee.Salary != null && employee.Description != null && imgFile != null)
                {
                    byte[] p1 = null;
                    using (var fs1 = imgFile.OpenReadStream())
                    using (var ms1 = new MemoryStream())
                    {
                        fs1.CopyTo(ms1);
                        p1 = ms1.ToArray();
                    }
                    employee.Image = Convert.ToBase64String(p1);
                    _context.Add(employee);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
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