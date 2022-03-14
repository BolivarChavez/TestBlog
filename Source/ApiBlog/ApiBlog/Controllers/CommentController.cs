using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiBlog.Models;
using Newtonsoft.Json;
using ApiBlog.Interfaces;

// Controller that manage insert and query operations for post comments

namespace ApiBlog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private IComment Comment { get; set; }

        public CommentController(IComment _comment)
        {
            this.Comment = _comment;
        }

        //Insert a comment for a selected post
        [HttpPost]
        [Route("InsertComment")]
        public async Task<string> InsertComment([FromBody] Comment CommentParam)
        {
            string JSONString = string.Empty;
            List<string> L_Param = new List<string>();

            await Task.Run(() =>
            {
                L_Param.Add(CommentParam.comment_post_id.ToString());
                L_Param.Add(CommentParam.comment_text.ToString());
                L_Param.Add(CommentParam.comment_author.ToString());
                JSONString = Comment.InsertComment(L_Param);
            });

            return JSONString;
        }

        //List all the comments for the selected post
        [HttpGet]
        [Route("QueryComment/{postid}")]
        public async Task<string> QueryComment(Int32 postid)
        {
            string JSONString = string.Empty;
            List<string> L_Param = new List<string>();

            await Task.Run(() =>
            {
                L_Param.Add(postid.ToString());
                JSONString = Comment.QueryCommentList(L_Param);
            });

            return JSONString;
        }
    }
}
