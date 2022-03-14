using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiBlog.Models;
using Newtonsoft.Json;
using ApiBlog.Interfaces;

// Controller that manage the insertion, edition, publishing and deletion of posts

namespace ApiBlog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private IPost Post { get; set; }

        public PostController(IPost _post)
        {
            this.Post = _post;
        }

        //Delete a post 
        [HttpPost]
        [Route("DeletePost")]
        public async Task<string> DeletePost([FromBody] Post PostParam)
        {
            string JSONString = string.Empty;
            List<string> L_Param = new List<string>();

            await Task.Run(() =>
            {
                L_Param.Add(PostParam.post_id.ToString());
                L_Param.Add(PostParam.post_status.ToString());
                JSONString = Post.DeletePost(L_Param);
            });

            return JSONString;
        }

        //Edit a post
        [HttpPost]
        [Route("EditPost")]
        public async Task<string> EditPost([FromBody] Post PostParam)
        {
            string JSONString = string.Empty;
            List<string> L_Param = new List<string>();

            await Task.Run(() =>
            {
                L_Param.Add(PostParam.post_id.ToString());
                L_Param.Add(PostParam.post_title.ToString());
                L_Param.Add(PostParam.post_text.ToString());
                JSONString = Post.EditPost(L_Param);
            });

            return JSONString;
        }

        //Insert a post
        [HttpPost]
        [Route("InsertPost")]
        public async Task<string> InsertPost([FromBody] Post PostParam)
        {
            string JSONString = string.Empty;
            List<string> L_Param = new List<string>();

            await Task.Run(() =>
            {
                L_Param.Add(PostParam.post_author_id.ToString());
                L_Param.Add(PostParam.post_title.ToString());
                L_Param.Add(PostParam.post_text.ToString());
                L_Param.Add(PostParam.post_status.ToString());
                L_Param.Add(PostParam.post_editor.ToString());
                JSONString = Post.InsertPost(L_Param);
            });

            return JSONString;
        }

        //Publish a post
        [HttpPost]
        [Route("PublishPost")]
        public async Task<string> PublishPost([FromBody] Post PostParam)
        {
            string JSONString = string.Empty;
            List<string> L_Param = new List<string>();

            await Task.Run(() =>
            {
                L_Param.Add(PostParam.post_id.ToString());
                L_Param.Add(PostParam.post_status.ToString());
                L_Param.Add(PostParam.post_editor.ToString());
                JSONString = Post.PublishPost(L_Param);
            });

            return JSONString;
        }

        //List all published posts from one author or for all of the authors
        [HttpGet]
        [Route("QueryPostList/{authorid}/{status}")]
        public async Task<string> QueryPostList(Int32 authorid, string status)
        {
            string JSONString = string.Empty;
            List<string> L_Param = new List<string>();

            await Task.Run(() =>
            {
                L_Param.Add(authorid.ToString());
                L_Param.Add(status);
                L_Param.Add("0");
                JSONString = Post.QueryPost(L_Param);
            });

            return JSONString;
        }

        //Query all the details for a selected post
        [HttpGet]
        [Route("QueryPost/{postid}")]
        public async Task<string> QueryPost(Int32 postid)
        {
            string JSONString = string.Empty;
            List<string> L_Param = new List<string>();

            await Task.Run(() =>
            {
                L_Param.Add("0");
                L_Param.Add("");
                L_Param.Add(postid.ToString());
                JSONString = Post.QueryPost(L_Param);
            });

            return JSONString;
        }
    }
}
