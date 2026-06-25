using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Homework06Controller
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            if (email != null && password != null)
            {
                HttpContext.Session.SetString("Email", email);
                HttpContext.Session.SetString("Password", password);
                return RedirectToAction("Home");
            }

            ViewBag.Error = "Invalid Credentials";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult Home()
        {
            var email = HttpContext.Session.GetString("Email");
            if (email == null)
            {
                return RedirectToAction("Login");
            }

            ViewBag.Email = email;
            return View();
        }
    }
}
