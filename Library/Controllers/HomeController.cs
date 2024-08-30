using Library.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Library.Authorization;
using Library.Data;
using Microsoft.AspNetCore.Http;

namespace Library.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LibraryContext _context;

        public HomeController(ILogger<HomeController> logger,LibraryContext context)
        {
            _logger = logger;
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
      
            return View();
        }

        [AllowAnonymous]
        //[AdminstratorAuthorize]
        [HttpGet]
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Username()
        {
            var token = Request.Cookies["RefreshToken"];

            var user = _context.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));
            return View(user.Username);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Logout()
        {
            if (Request.Cookies["RefreshToken"]!=null)  Response.Cookies.Delete("refreshToken");
            if (Request.Cookies["Authorization"] != null) Response.Cookies.Delete("Authorization");
            return RedirectToAction("Index");
        }
    }
}