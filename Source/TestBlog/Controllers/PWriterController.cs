using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestBlog.Models;
using TestBlog.Interfaces;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace TestBlog.Controllers
{
    //Controller for edition of posts
    [Authorize(AuthenticationSchemes = "Cookies")]
    public class PWriterController : Controller
    {
        private IPost Post { get; set; }

        public PWriterController(IPost _post)
        {
            this.Post = _post;
        }

        //Show posts for the writer
        [Authorize(Roles = "Writer")]
        public IActionResult Index()
        {
            List<PostList> l_postlist = new List<PostList>();
            List<string> response = new List<string>();
            List<string> l_param = new List<string>();

            ViewData["full_name"] = HttpContext.Session.GetString("full_name");
            l_param.Add("writer");
            response = Post.QueryPost(l_param);

            if (response[0].ToString().Trim() == "0")
            {
                l_postlist = JsonSerializer.Deserialize<List<PostList>>(response[1].ToString().Trim());
                return View(l_postlist);
            }
            else
            {
                if (HttpContext.Session.GetString("user_type") != "W" || HttpContext.Session.GetString("user_type") == null)
                    return RedirectToAction("Index", "Error", routeValues: new { errormsg = "User has not loged in as a writer" });
                else
                    return RedirectToAction("Index", "PUpdate", routeValues: new { postid = 0 });
            }
        }

        //Redirect to the new post view for the writer
        [Authorize(Roles = "Writer")]
        public IActionResult InsertPost()
        {
            return RedirectToAction("Index", "PUpdate", routeValues: new { postid = 0 });
        }
    }
}
