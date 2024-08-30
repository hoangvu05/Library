using Library.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using Library.Models;
using Library.Services;
using Library.Authorization;
using Azure.Identity;
using System;

namespace Library.Controllers
{
    public class LayoutViewComponent : ViewComponent
    {

        private readonly LibraryContext _context;
        private IUserService _userService;
        private IJwtUtils _jwtUtils;

        public LayoutViewComponent(LibraryContext context, IUserService userService, IJwtUtils jwtUtils)
        {
            _context = context;
            _userService = userService;
            _jwtUtils = jwtUtils;
        }

        public IViewComponentResult Invoke(string style)
        {

            //var token = Request.Cookies["RefreshToken"];
            if (style == "username")
            {
                var token = Request.Cookies["Authorization"];
                var userId = _jwtUtils.ValidateJwtToken(token);

                User user = null;
                if (userId != null)
                {
                    // attach user to context on successful jwt validation
                    user = _userService.GetById(userId.Value);
                }


                var username = "";
                if (user == null) username = "Đăng nhập";

                else username = user.Username;
                return View("Username", username);
            }
            else if (style == "search")
            {
                var token = Request.Cookies["Authorization"];
                var userId = _jwtUtils.ValidateJwtToken(token);

                User user = null;
                if (userId != null)
                {
                    user = _userService.GetById(userId.Value);
                }

                var action = "Index";
                if (user != null)
                    if (user.Role == "Adminstrator")
                    {
                        action = "IndexAdmin";
                    }

                return View("Search", action);
            }

            else if (style == "iconAdmin")
            {
                var token = Request.Cookies["Authorization"];
                var userId = _jwtUtils.ValidateJwtToken(token);


                User user = null;
                if (userId != null)
                {
                    user = _userService.GetById(userId.Value);
                }
                if (user != null)
                    if (user.Role == "Adminstrator")
                    {
                        return View("IconAdmin");
                    }
                return Content(string.Empty);
            }
            else if (style == "addbook")
            {
                var token = Request.Cookies["Authorization"];
                var userId = _jwtUtils.ValidateJwtToken(token);


                User user = null;
                if (userId != null)
                {
                    user = _userService.GetById(userId.Value);
                }
                if (user != null)
                    if (user.Role == "Adminstrator")
                    {
                        return View("AddBook");
                    }
                return Content(string.Empty);
            }
            else if (style == "verification")
            {
                var token = Request.Cookies["Authorization"];
                var userId = _jwtUtils.ValidateJwtToken(token);


                User user = null;
                if (userId != null)
                {
                    user = _userService.GetById(userId.Value);
                }
                if (user != null)
                    if (user.Role == "Adminstrator")
                    {
                        return View("Verification");
                    }
                return Content(string.Empty);
            }
            else if (style == "employee")
            {
                var token = Request.Cookies["Authorization"];
                var userId = _jwtUtils.ValidateJwtToken(token);


                User user = null;
                if (userId != null)
                {
                    user = _userService.GetById(userId.Value);
                }
                if (user != null)
                    if (user.Role == "Adminstrator")
                    {
                        return View("Employee");
                    }
                return Content(string.Empty);
            }
            return Content(string.Empty);
        }


        private string ipAddress()
        {
            // get source ip address for the current request
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
