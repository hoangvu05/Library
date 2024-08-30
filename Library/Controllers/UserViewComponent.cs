using Library.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using Library.Models;
using Library.Services;
using Library.Authorization;
using Azure.Identity;

namespace Library.Views.Shared.Components
{
    public class UserViewComponent : ViewComponent
    {

        private readonly LibraryContext _context;
        private IUserService _userService;
        private IJwtUtils _jwtUtils;

        public UserViewComponent(LibraryContext context, IUserService userService, IJwtUtils jwtUtils)
        {
            _context = context;
            _userService = userService;
            _jwtUtils = jwtUtils;
        }

        public IViewComponentResult Invoke(int id)
        {

            //var token = Request.Cookies["RefreshToken"]
            var user = _context.Users.SingleOrDefault(i => i.UserId==id);
            return View("FindUser", user.Username);

        }


    }
}
