using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestBlog.Controllers
{
    // Crontroller for the Error message view
    public class ErrorController : Controller
    {
        public IActionResult Index(string errormsg)
        {
            ViewData["Error"] = errormsg;
            return View();
        }
    }
}
