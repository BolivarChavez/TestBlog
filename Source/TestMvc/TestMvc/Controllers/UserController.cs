using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMvc.Models;

namespace TestMvc.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            var User = new List<User>()
            {
                new User {user_name = "admin", user_fullname = "Administrador", user_password = "admin123"},
                new User {user_name = "bchavez", user_fullname = "Bolivar Chavez", user_password = "b1234567"}
            };

            return View(User);
        }

        public IActionResult Ingresar()
        {
            return View();
        }
    }
}
