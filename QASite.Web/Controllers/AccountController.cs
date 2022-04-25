using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using QASite.Data;
using QASite.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QASite.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly string _connectionString;
        public AccountController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        public IActionResult LogIn()
        {
            return View(new ViewModel { Message = (string)TempData["message"] });
        }
        [HttpPost]
        public IActionResult LogIn(string email, string password)
        {
            QARepository repo = new(_connectionString);
            User user = repo.LogIn(email, password);
            if (user == null)
            {
                TempData["message"] = "invalid login";
                return RedirectToAction("login");
            }
            var claims = new List<Claim>
            {
                 new Claim("user", email)
            };

            HttpContext.SignInAsync(new ClaimsPrincipal(
                new ClaimsIdentity(claims, "Cookies", "user", "role"))).Wait();

            return Redirect("/home/home");
        }
        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Signup(string name, string email, string password)
        {
            QARepository repo = new(_connectionString);
            repo.AddUser(new User { Name = name, Email = email, HashPassword = BCrypt.Net.BCrypt.HashPassword(password) });
            return RedirectToAction("login");
        }
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync().Wait();
            return Redirect("/home/home");
        }

    }
}
