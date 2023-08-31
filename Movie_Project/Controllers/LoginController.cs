using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movie_Project.Data;
using Movie_Project.Models;

namespace Movie_Project.Controllers
{
    public class LoginController : Controller
    {

        private readonly MovieDBContext _context;

        public LoginController(MovieDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LogIn(Login LogInUser)
        {

            if (ModelState.IsValid)
            {
                User admin = AuthenticateAdmin(LogInUser.Email, LogInUser.Password);
                if (admin != null)
                {

                    HttpContext.Session.SetInt32("IDAdmin", admin.UserId);
                    HttpContext.Session.SetString("NameAdmin", admin.FirstName);
                    //TempData["NameAdmin"] = GetFirstName(admin);

                    return RedirectToAction("IndexAdmin", "Movie");

                }

                User user = AuthenticateUser(LogInUser.Email, LogInUser.Password);
                if (user != null)
                {
                    
                    HttpContext.Session.SetInt32("IDUser", user.UserId);
                    HttpContext.Session.SetString("NameUser", user.FirstName);
                    //TempData["NameUser"] = GetFirstName(user);
                    return RedirectToAction("IndexUser", "Movie");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect password or Email");
                }


            }
            return View(LogInUser);
        }

        private User AuthenticateUser(string email, string password)
        {

            // Retrieve user from your data store
            // Compare provided email and password with stored values
            // Return User object if authenticated, null otherwise
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user != null && user.Password == password)
            {
				HttpContext.Session.SetString("LoggedIn", "true");
				return user;
            }
			HttpContext.Session.SetString("LoggedIn", "false");
			return null;
        }
        private User AuthenticateAdmin(string email, string password)
        {

            // Retrieve user from your data store
            // Compare provided email and password with stored values
            // Return User object if authenticated, null otherwise
            var admin = _context.Users.FirstOrDefault(u => u.Email == email);

            if (admin != null && admin.Password == password && admin.status == "admin")
            {
				HttpContext.Session.SetString("LoggedIn", "true");
				HttpContext.Session.SetString("state", "true");
                return admin;
            }
			HttpContext.Session.SetString("LoggedIn", "false");
			HttpContext.Session.SetString("state", "false");
            return null;
        }

        private string GetFirstName(User user)
        {

            return (user.FirstName);
        }
    }
}
