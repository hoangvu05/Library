namespace Library.Controllers;

using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Library.Authorization;
using Library.Services;
using RestSharp;
using Azure.Core;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Azure;
using System.Net.Http;
using Microsoft.Net.Http.Headers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsersController : Controller
{
    private IUserService _userService;
    private readonly IHttpClientFactory _clientFactory;

    public UsersController(IUserService userService, IHttpClientFactory clientFactory)
    {
        _userService = userService;
        _clientFactory = clientFactory;
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public IActionResult Authenticate( AuthenticateRequest model)
    {
        var response = _userService.Authenticate(model, ipAddress());
        if (response != null)
        {
            //setTokenCookie(response.RefreshToken, response.JwtToken);


            //Request.Headers.Add("Authorization", "Bearer " + response.RefreshToken);
            //var client = _clientFactory.CreateClient();
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.JwtToken);
            //var request = new RestRequest("Login/Authenticate");
            //var client = _clientFactory.CreateClient();
            //HttpResponseMessage responseX = client.Send(request);
            //request.AddHeader("Authorization", "Bearer " + response.JwtToken);

            /*var cookie = new CookieHeaderValue("session-id", "12345");
            cookie.Expires = DateTimeOffset.Now.AddDays(1);
            cookie.Domain = Request.RequestUri.Host;
            cookie.Path = "/";

            resp.Headers.AddCookies(new CookieHeaderValue[] { cookie });*/

            /*var resp = new HttpResponseMessage();
            var cookie = new CookieHeaderValue("session-id", "12345");
            cookie.Expires = DateTimeOffset.Now.AddDays(1);
            cookie.Domain = Request.RequestUri.Host;
            cookie.Path = "/";

            resp.Headers.AddCookies(new CookieHeaderValue[] { cookie });*/
            //return resp;
        }

        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("refresh-token")]
    public IActionResult RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        var response = _userService.RefreshToken(refreshToken, ipAddress());
        setTokenCookie(response.RefreshToken,response.JwtToken);
        return Ok(response);
    }

    [HttpPost("revoke-token")]
    public IActionResult RevokeToken(RevokeTokenRequest model)
    {
        // accept refresh token in request body or cookie
        var token = model.Token ?? Request.Cookies["refreshToken"];

        if (string.IsNullOrEmpty(token))
            return BadRequest(new { message = "Token is required" });

        _userService.RevokeToken(token, ipAddress());
        return Ok(new { message = "Token revoked" });
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Index()
    {
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("Register")]
    public IActionResult Register(RegisterRequest model)
    {
        var response = _userService.Register(model, ipAddress());
        return Ok(response);
    }


    /*[HttpGet]
    public IActionResult GetAll()
    {
        var users = _accountService.GetAll();
        return Ok(users);
    }*/

            [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var user = _userService.GetById(id);
        return Ok(user);
    }

    [HttpGet("{id}/refresh-tokens")]
    public IActionResult GetRefreshTokens(int id)
    {
        var user = _userService.GetById(id);
        return Ok(user.RefreshTokens);
    }

    // helper methods

    private async void setTokenCookie(string refreshtoken,string jwttoken)
    {
        // append cookie with refresh token to the http response
        /*var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(7)
        };
        Response.Cookies.Append("refreshToken", "dasdasddas", cookieOptions);
        Response.Cookies.Append("Authorization", "Asdasdas", cookieOptions);*/
        return;
    }

    private string ipAddress()
    {
        // get source ip address for the current request
        if (Request.Headers.ContainsKey("X-Forwarded-For"))
            return Request.Headers["X-Forwarded-For"];
        else
            return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, UpdateRequest model)
    {
        _userService.Update(id, model);
        return Ok(new { message = "User updated successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _userService.Delete(id);
        return Ok(new { message = "User deleted successfully" });
    }
}