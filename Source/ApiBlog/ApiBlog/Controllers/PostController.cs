using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiBlog.Models;
using Newtonsoft.Json;

// Controller that manage the insertion, edition, publishing and deletion of posts

namespace ApiBlog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private BlogContext Context { get; }

        public PostController(BlogContext _context)
        {
            this.Context = _context;
        }

        //Delete a post 
        [HttpPost]
        [Route("DeletePost")]
        public async Task<string> DeletePost([FromBody] Post PostParam)
        {
            string JSONString = string.Empty;

            await Task.Run(() =>
            {
                List<Output> Outputs = this.Context.DeletePost(PostParam.post_id, PostParam.post_status.Trim()).ToList();
                JSONString = JsonConvert.SerializeObject(Outputs);
            });

            return JSONString;
        }

        //Edit a post
        [HttpPost]
        [Route("EditPost")]
        public async Task<string> EditPost([FromBody] Post PostParam)
        {
            string JSONString = string.Empty;

            await Task.Run(() =>
            {
                List<Output> Outputs = this.Context.EditPost(PostParam.post_id, PostParam.post_title.Trim(), PostParam.post_text.Trim()).ToList();
                JSONString = JsonConvert.SerializeObject(Outputs);
            });

            return JSONString;
        }

        //Insert a post
        [HttpPost]
        [Route("InsertPost")]
        public async Task<string> InsertPost([FromBody] Post PostParam)
        {
            string JSONString = string.Empty;

            await Task.Run(() =>
            {
                List<Output> Outputs = this.Context.InsertPost(PostParam.post_author_id, PostParam.post_title.Trim(), PostParam.post_text.Trim(), PostParam.post_status.Trim(), PostParam.post_date, PostParam.post_editor).ToList();
                JSONString = JsonConvert.SerializeObject(Outputs);
            });

            return JSONString;
        }

        //Publish a post
        [HttpPost]
        [Route("PublishPost")]
        public async Task<string> PublishPost([FromBody] Post PostParam)
        {
            string JSONString = string.Empty;

            await Task.Run(() =>
            {
                List<Output> Outputs = this.Context.PublishPost(PostParam.post_id, PostParam.post_status.Trim(), PostParam.post_date, PostParam.post_editor).ToList();
                JSONString = JsonConvert.SerializeObject(Outputs);
            });

            return JSONString;
        }

        //List all published posts from one author or for all of the authors
        [HttpGet]
        [Route("QueryPostList/{authorid}/{status}")]
        public async Task<string> QueryPostList(Int32 authorid, string status)
        {
            string JSONString = string.Empty;

            await Task.Run(() =>
            {
                List<PostList> PostLists = this.Context.QueryPostList(authorid, status).ToList();
                JSONString = JsonConvert.SerializeObject(PostLists);
            });

            return JSONString;
        }

        //Query all the details for a selected post
        [HttpGet]
        [Route("QueryPost/{postid}")]
        public async Task<string> QueryPost(Int32 postid)
        {
            string JSONString = string.Empty;

            await Task.Run(() =>
            {
                List<Post> Posts = this.Context.QueryPostSingle(postid).ToList();
                JSONString = JsonConvert.SerializeObject(Posts);
            });

            return JSONString;
        }
    }
}
