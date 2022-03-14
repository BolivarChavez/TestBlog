using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestBlog.Models;
using TestBlog.Interfaces;
using System.Text.Json;

namespace TestBlog.Services
{
    public class CommentService : IComment
    {
        public CommentService(IConfiguration configuration, ISesion sesion)
        {
            Configuration = configuration;
            _sesion = sesion;
        }

        public IConfiguration Configuration { get; }
        private readonly ISesion _sesion;

        //Operation for comment query. List all comments for one post
        public string QueryCommentList(List<string> L_Params)
        {
            string api_url = Configuration["ApiUrl"].Trim();
            List<Comment> L_comment = new List<Comment>();
            string response;

            Task<List<Comment>> ctask = Task.Run<List<Comment>>(async () => await Operations.GetComments(api_url, Int32.Parse(L_Params[0].ToString().Trim())));
            L_comment = ctask.Result.ToList();

            response = JsonSerializer.Serialize<List<Comment>>(L_comment);
            return response;
        }

        //Operation for the insertion of a new post comment
        public string InsertComment(List<string> L_Params)
        {
            string api_url = Configuration["ApiUrl"].Trim();
            string author;
            string response;
            string user_id = string.Empty;
            List<Output> L_output = new List<Output>();

            user_id = _sesion.ValueGet("user_id");

            if (user_id == null || user_id.ToString() == "" || user_id.ToString() == "0")
                author = "Anonymous";
            else
                author = _sesion.ValueGet("full_name");

            L_output = Operations.InsertComment(api_url, Int32.Parse(L_Params[0].ToString().Trim()), L_Params[1].ToString().Trim(), author);
            response = JsonSerializer.Serialize<List<Output>>(L_output);
            return response;
        }
    }
}
