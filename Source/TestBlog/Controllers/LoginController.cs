using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestBlog.Models;
using TestBlog.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace TestBlog.Controllers
{
    //Controller for user (writer or editor) sign in
    public class LoginController : Controller
    {
        private IUser Luser { get; set; }

        public LoginController(IUser _user)
        {
            this.Luser = _user;
        }

        //Show Post list if the user has signed in, if not show the Sig in view
        [AllowAnonymous]
        [Authorize(Roles = "Editor, Writer")]
        public IActionResult Index()
        {
            var user_id = HttpContext.Session.GetString("user_id");
            var user_type = HttpContext.Session.GetString("user_type");

            if (user_id == null || user_id.ToString() == "" || user_id.ToString() == "0")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "PostList");
            }
        }

        //Sing in proccess
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(IFormCollection form)
        {
            List<string> L_Param = new List<string>();
            int response;
            string ncontroller, naction;

            ncontroller = "";
            naction = "";

            L_Param.Add(form["UserName"].ToString().Trim());
            L_Param.Add(form["UserPassword"].ToString().Trim());
            response = Luser.LoginUser(L_Param);

            switch (response)
            {
                case 1:
                    ncontroller = "Index";
                    naction = "PWriter";
                    break;
                case 2:
                    ncontroller = "Index";
                    naction = "PEditor";
                    break;
                case 3:
                    ncontroller = "Index";
                    naction = "PostList";
                    break;
            }

            //Implement cookie authentication
            var userClaims = new List<Claim>()
                        {
                        new Claim(ClaimTypes.Name, HttpContext.Session.GetString("user_id").ToString()),
                        new Claim(ClaimTypes.Role, HttpContext.Session.GetString("user_type").ToString() == "E" ? "Editor" : "Writer"),
                        };

            var identity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
            var userPrincipal = new ClaimsPrincipal(new[] { identity });
            HttpContext.SignInAsync(userPrincipal);

            return RedirectToAction(ncontroller, naction);
        }
    }
}
