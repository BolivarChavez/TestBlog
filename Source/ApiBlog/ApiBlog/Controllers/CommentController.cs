using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiBlog.Models;
using Newtonsoft.Json;

// Controller that manage insert and query operations for post comments

namespace ApiBlog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private BlogContext Context { get; }

        public CommentController(BlogContext _context)
        {
            this.Context = _context;
        }

        //Insert a comment for a selected post
        [HttpPost]
        [Route("InsertComment")]
        public async Task<string> InsertComment([FromBody] Comment CommentParam)
        {
            string JSONString = string.Empty;

            await Task.Run(() =>
            {
                List<Output> Outputs = this.Context.InsertComment(CommentParam.comment_post_id, CommentParam.comment_text.Trim(), CommentParam.comment_author.Trim()).ToList();
                JSONString = JsonConvert.SerializeObject(Outputs);
            });

            return JSONString;
        }

        //List all the comments for the selected post
        [HttpGet]
        [Route("QueryComment/{postid}")]
        public async Task<string> QueryComment(Int32 postid)
        {
            string JSONString = string.Empty;

            await Task.Run(() =>
            {
                List<Comment> Comments = this.Context.QueryCommentList(postid).ToList();
                JSONString = JsonConvert.SerializeObject(Comments);
            });

            return JSONString;
        }
    }
}
