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
    //Controller for post visualization and comment insertion
    public class PostController : Controller
    {
        private IPost Post { get; set; }
        private IComment Comment { get; set; }

        public PostController(IPost _post, IComment _comment)
        {
            this.Post = _post;
            this.Comment = _comment;
        }

        //Post list with comments
        public IActionResult Index(Int32 postid)
        {
            List<Post> l_post= new List<Post>();
            List<Comment> L_comment = new List<Comment>();
            List<string> response = new List<string>();
            List<string> l_param = new List<string>();
            string comment_list;

            l_param.Add("unique");
            l_param.Add(postid.ToString());
            response = Post.QueryPost(l_param);
            l_post = JsonSerializer.Deserialize<List<Post>>(response[1].ToString().Trim());

            response.Clear();
            l_param.Clear();
            l_param.Add(postid.ToString());
            comment_list = Comment.QueryCommentList(l_param);
            L_comment = JsonSerializer.Deserialize<List<Comment>>(comment_list);

            ViewPost mymodel = new ViewPost();
            mymodel.Posts = l_post;
            mymodel.Comments = L_comment;

            return View(mymodel);
        }

        //Insert a new comment for the post
        [HttpPost]
        public IActionResult SaveComment(IFormCollection form)
        {
            List<string> l_param = new List<string>();
            string response;

            l_param.Add(HttpContext.Session.GetString("post_id"));
            l_param.Add(form["CommentText"].ToString().Trim());
            response = Comment.InsertComment(l_param);

            return RedirectToAction("Index", "Post", routeValues: new { postid = Int32.Parse(HttpContext.Session.GetString("post_id")) });
        }
    }
}
