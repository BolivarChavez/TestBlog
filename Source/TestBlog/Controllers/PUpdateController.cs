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

namespace TestBlog.Controllers
{
    //Controller for the edition of published posts
    public class PUpdateController : Controller
    {
        private IPost Post { get; set; }

        public PUpdateController(IPost _post)
        {
            this.Post = _post;
        }

        public IConfiguration Configuration { get; }

        //Show Post detail
        public IActionResult Index(Int32 postid)
        {
            List<string> l_param = new List<string>();
            List<string> response = new List<string>();
            Post epost = new Post();
            List<Post> L_post = new List<Post>();

            l_param.Add("unique");
            l_param.Add(postid.ToString().Trim());
            response = Post.QueryPost(l_param);
            L_post = JsonSerializer.Deserialize<List<Post>>(response[1].ToString().Trim());
            epost = L_post.First();

            if (epost.post_status == "N")
                ViewData["post_status"] = "Pending publish approval";
            else
                ViewData["post_status"] = "Approved";

            return View(epost);
        }

        //Perform post update
        [HttpPost]
        public IActionResult SavePost(IFormCollection form)
        {
            List<string> l_param = new List<string>();
            string response;

            if (form["PostStatus"].ToString().Trim() == "Approved")
            {
                l_param.Add(form["PostId"].ToString().Trim());
                l_param.Add(form["Title"].ToString().Trim());
                l_param.Add(form["PostText"].ToString().Trim());
                l_param.Add(HttpContext.Session.GetString("user_id"));
                response = Post.InsertPost(l_param);

                return RedirectToAction("Index", "PWriter");
            }
            else
            {
                return RedirectToAction("Index", "Error", routeValues: new { errormsg = "Post can not be edited because it is not yet approved" });
            }


        }
    }
}
