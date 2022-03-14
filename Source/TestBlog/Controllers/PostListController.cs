using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TestBlog.Models;
using TestBlog.Interfaces;
using System.Text.Json;

namespace TestBlog.Controllers
{
    //Controller for the post list view
    public class PostListController : Controller
    {
        private IPost Post { get; set; }

        public PostListController(IPost _post)
        {
            this.Post = _post;
        }

        //Visualization of the published post list
        public IActionResult Index()
        {
            List<PostList> l_postlist = new List<PostList>();
            List<string> response = new List<string>();
            List<string> l_param = new List<string>();

            l_param.Add("all");
            response = Post.QueryPost(l_param);

            if (response[0].ToString().Trim() == "0")
            {
                l_postlist = JsonSerializer.Deserialize<List<PostList>>(response[1].ToString().Trim());
                return View(l_postlist);
            }
            else
            {
                return RedirectToAction("Index", "Error", routeValues: new { errormsg = "There are nos posts to show at this time" });
            }
        }
    }
}
