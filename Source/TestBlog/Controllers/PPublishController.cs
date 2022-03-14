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
    //Controller for the publishing and deletion of posts
    public class PPublishController : Controller
    {
        private IPost Post { get; set; }

        public PPublishController(IPost _post)
        {
            this.Post = _post;
        }

        //Show post detail
        public IActionResult Index(Int32 postid)
        {
            Post epost = new Post();
            List<Post> L_post = new List<Post>();
            List<string> l_param = new List<string>();
            List<string> response = new List<string>();

            l_param.Add("unique");
            l_param.Add(postid.ToString());
            response = Post.QueryPost(l_param);
            L_post = JsonSerializer.Deserialize<List<Post>>(response[1].ToString().Trim());
            epost = L_post.First();

            if (epost.post_status == "N")
                ViewData["post_status"] = "Pending publish approval";
            else
                ViewData["post_status"] = "Approved";

            return View(epost);
        }

        //Perform the publish post
        [HttpPost]
        [Route("PublishPost")]
        public IActionResult PublishPost(IFormCollection form)
        {
            List<string> l_param = new List<string>();
            string response;

            l_param.Add(form["PostId"].ToString().Trim());
            l_param.Add(HttpContext.Session.GetString("user_id"));
            response = Post.PublishPost(l_param);

            return RedirectToAction("Index", "PEditor");
        }

        //Perform the post deletion
        [HttpPost]
        [Route("DeletePost")]
        public IActionResult DeletePost(IFormCollection form)
        {
            List<string> l_param = new List<string>();
            string response;

            l_param.Add(form["PostId"].ToString().Trim());
            response = Post.DeletePost(l_param);

            return RedirectToAction("Index", "PEditor");
        }
    }
}
