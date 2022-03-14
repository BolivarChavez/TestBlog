using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestBlog.Models;
using TestBlog.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace TestBlog.Controllers
{
    //Controller for the signing out proccess
    public class LogOutController : Controller
    {
        private IUser Luser { get; set; }

        public LogOutController(IUser _user)
        {
            this.Luser = _user;
        }

        [AllowAnonymous]
        [Authorize(Roles = "Editor, Writer")]
        public IActionResult Index()
        {
            int response = Luser.LogoutUser();

            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "PostList");
        }
    }
}
